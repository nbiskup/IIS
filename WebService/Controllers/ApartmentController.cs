using System.Xml;
using System.Xml.Schema;
using WebService.Models;
using WebService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private IRepository<Apartment> _repository;
        private IList<Apartment> _apartments;

        public ApartmentController(IRepository<Apartment> repository)
        {
            _repository = repository;
            _apartments = _repository.GetAll().Result.ToList();
        }         

        [HttpPost("xsd")]
        public async Task<IActionResult> xsd(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not found!");

            var schemaSet = new XmlSchemaSet();
            using (var fileStream = System.IO.File.OpenRead("./Files/apartment.xsd"))
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

            var root = Utils.XmlValidation.XmlDeserializer<Root>(document);

            if (root == null)
                return BadRequest("Error: object is not deserialized.");

            _apartments.Add(root.Apartment);
            saveApartments();

            return Ok(root.Apartment);
        }

        private void saveApartments()
        {
            XmlSerializer serialiser = new XmlSerializer(typeof(List<Apartment>));
            TextWriter filestream = new StreamWriter(@"C:\Users\Nikola\apartments.xml");

            serialiser.Serialize(filestream, _apartments);
            filestream.Close();
        }


    }
}
