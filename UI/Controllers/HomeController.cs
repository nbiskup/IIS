using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Web.Mvc;

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

        [HttpPost]
        public ActionResult API(string apartmentName)
        {
            var client = new RestClient("http://localhost:5078/apartment/Index");
            var request = new RestRequest("", Method.Post);

            request.AddParameter("apartmentName", apartmentName);

            RestResponse response = client.Execute(request);
            var apartments = JsonConvert.DeserializeObject<List<UI.Models.ApartmentList>>(response.Content);

            ViewData["apartments"] = apartments;

            return View(apartments);
        }

        public ActionResult API()
        {
            return View();
        }

    }
}