using System.Xml;
using System.Xml.Schema;
using WebService.Models;
using WebService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using Commons.Xml.Relaxng;
using System.Xml.Linq;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private IList<Apartment> _apartments;

        public ApartmentController(IRepository<Apartment> repository)
        {
            //_repository = repository;
            //_apartments = _repository.GetAll().Result.ToList();
            _apartments = new List<Apartment>();
            loadApartments();
        }         

        private void loadApartments()
        {
            try
            {
                using (var reader = new StreamReader(@"C:\Users\Nikola\apartments.xml"))
                {
                    if (string.IsNullOrWhiteSpace(reader.ReadToEnd())) 
                        return;

                    XmlSerializer serializer = new XmlSerializer(typeof(ApartmentList));
                    var apartments = (ApartmentList)serializer.Deserialize(reader);

                    if (apartments == null)
                        return;

                    apartments.Apartments.ForEach(a => _apartments.Add(a));                        
                }
            }
            catch (Exception)
            {
                        
            }
        }

        [HttpPost("XSD")]
        public async Task<IActionResult> XSD(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not found!");

            var schemaSet = new XmlSchemaSet();
            using (var fileStream = System.IO.File.OpenRead("./Files/apartmentXSD.xsd"))
            {
                schemaSet.Add("", XmlReader.Create(fileStream));
            }

            XmlDocument document = new XmlDocument();
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                document.Load(memoryStream);
            }

            bool xmlValid = true;
            string validationErrorMessage = "";

            document.Schemas.Add(schemaSet);
            document.Validate((sender, args) =>
            {
                xmlValid = false;
                validationErrorMessage = $"Not valid XML: {args.Message}";
            });

            if (!xmlValid)
                return BadRequest(validationErrorMessage);

            var root = Utils.XmlValidation.XmlDeserializer<ApartmentList>(document);

            if (root == null)
                return BadRequest("Error: Object is not deserialized.");

            root.Apartments.ForEach(a => _apartments.Add(a));
            SaveApartments();

            return Ok(root.Apartments);
        }

        [HttpPost("RNG")]
        public async Task<IActionResult> RNG(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not found!");

            XmlDocument document = new XmlDocument();
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                document.Load(memoryStream);
            }

            XmlReader xml = new XmlNodeReader(document);
            XmlReader rng = XmlReader.Create(Path.GetFullPath("./Files/apartmentRNG.rng"));

            bool xmlValid = true;
            string validationErrorMessage = "";

            using (var reader = new RelaxngValidatingReader(xml, rng))
            {
                reader.InvalidNodeFound += (sender, message) =>
                {
                    xmlValid = false;
                    validationErrorMessage = $"XML is not valid {message}";
                    return true;
                };

                XDocument xDocument = XDocument.Load(reader);
            }

            if (!xmlValid)
                return BadRequest(validationErrorMessage);

            var root = Utils.XmlValidation.XmlDeserializer<ApartmentList>(document);

            if (root == null)
                return BadRequest("Error: Object is not deserialized.");

            root.Apartments.ForEach(a => _apartments.Add(a));
            SaveApartments();

            return Ok(root.Apartments);
        }

        private void SaveApartments()
        {
            XmlSerializer serialiser = new XmlSerializer(typeof(ApartmentList));
            TextWriter filestream = new StreamWriter(@"C:\Users\Nikola\apartments.xml");

            serialiser.Serialize(filestream, _apartments);
            filestream.Close();
        }
    }
}
