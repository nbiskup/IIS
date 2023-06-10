using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UI.Models;
using FormCollection = System.Web.Mvc.FormCollection;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
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

        public ActionResult Soap()
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
        public async Task<ActionResult> XSD()
        {
            var files = Request.Files;

            if (files != null && files.Count > 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    var file = files[0];

                    var content = new MultipartFormDataContent();
                    content.Add(new StreamContent(file.InputStream)
                    {
                        Headers =
                        {
                            ContentLength = file.ContentLength,
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
                
            }
            else 
            {
                ViewBag.xml = "Select a file!";
            }            

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RNG()
        {
            var files = Request.Files;

            if (files != null && files.Count > 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    var file = files[0];

                    var content = new MultipartFormDataContent();
                    content.Add(new StreamContent(file.InputStream)
                    {
                        Headers =
                        {
                            ContentLength = file.ContentLength,
                            ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType)
                        }
                    }, "File", file.FileName);

                    var response = await client.PostAsync("http://localhost:5223/api/Apartment/RNG", content);

                    string message_response = "";

                    if (response.IsSuccessStatusCode)
                        message_response = "good";
                    else
                        message_response = "not valid!";

                    string valid = $"XML is {message_response}";

                    ViewBag.xml = valid;
                }

            }
            else
            {
                ViewBag.xml = "Select a file!";
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SOAP(FormCollection f)
        {
            var id = f.Get("id");
            var url = "http://localhost:51321/WebService.asmx/Search";
            var contentData = $"query={id}";

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(contentData, Encoding.UTF8, "application/x-www-form-urlencoded");

                using (var response = await httpClient.PostAsync(url, content))
                {
                    response.EnsureSuccessStatusCode();
                    var soapResult = await response.Content.ReadAsStringAsync();

                    var apartments = JsonConvert.DeserializeObject<List<Apartment>>(soapResult);
                    var apartmentList = new ApartmentList(apartments);

                    ViewData["apartments"] = apartmentList;

                    return View();
                }
            }
        }

    }
}