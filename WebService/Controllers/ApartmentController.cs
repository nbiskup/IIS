﻿using System.Xml;
using System.Xml.Schema;
using WebService.Models;
using Microsoft.AspNetCore.Mvc;
using Commons.Xml.Relaxng;
using System.Xml.Linq;
using SOAPReference;
using System.Net;
using System.Text;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private IList<Apartment> _apartments;

        public ApartmentController()
        {
            
        }

        [HttpGet("Welcome")]
        public async Task<IActionResult> Index()
        {            
            return Ok("WELCOME TO CORE PROJECT!");
        }

        [HttpPost("XSD")]
        public async Task<IActionResult> XSD(IFormCollection files)
        {   
            if (files.Files == null || files.Files.Count == 0)
                return BadRequest("File not found!");

            var file = files.Files[0];

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

            return Ok(root.Apartments);
        }

        [HttpPost("RNG")]
        public async Task<IActionResult> RNG(IFormCollection files)
        {
            if (files.Files == null || files.Files.Count == 0)
                return BadRequest("File not found!");

            var file = files.Files[0];

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
                        
            return Ok(root.Apartments);
        }        

        [HttpGet("SOAP")]
        public string GetApartmentById(string id)
        {
            var url = "http://localhost:51321/WebService.asmx/Search";
            var postData = "query=2";

            var webRequest = WebRequest.Create(url);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";

            var dataBytes = Encoding.UTF8.GetBytes(postData);
            webRequest.ContentLength = dataBytes.Length;

            using (var requestStream = webRequest.GetRequestStream())
            {
                requestStream.Write(dataBytes, 0, dataBytes.Length);
            }

            using (var response = webRequest.GetResponse())
            {
                using (var rd = new StreamReader(response.GetResponseStream()))
                {
                    var soapResult = rd.ReadToEnd();
                    return soapResult.ToString();
                }
            }
        }


    }
}
