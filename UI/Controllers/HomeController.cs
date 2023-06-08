using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<HomeController> _logger;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Xsd()
        {
            return View();
        }

        public ActionResult Rng()
        {
            return View();
        }

        public ActionResult API()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> API(string apartmentName)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string api = $"http://localhost:5078/apartment/Index?apartmentName={apartmentName}";

                HttpResponseMessage httpResponse = await httpClient.GetAsync(api);

                string responseContent = await httpResponse.Content.ReadAsStringAsync();

                var apartments = JsonConvert.DeserializeObject<List<Apartment>>(responseContent);
                var apartmentList = new ApartmentList(apartments);

                ViewData["apartments"] = apartmentList;

                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> XSD(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var client = _clientFactory.CreateClient();
                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(file.OpenReadStream())
                {
                    Headers =
                    {
                        ContentLength = file.Length,
                        ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType)
                    }
                }, "File", file.FileName);

                var response = await client.PostAsync("http://localhost:5223/api/Apartment/XSD", content);

                string message_response = "";
                
                if (response.IsSuccessStatusCode)
                    message_response = "good";
                else
                    message_response = "not valid!";

                string valid = $"XML is {message_response}";

                ViewBag.xml = valid;
            }

            return View();
        }

    }
}