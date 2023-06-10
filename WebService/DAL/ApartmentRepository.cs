using Newtonsoft.Json;
using RestSharp;
using System.Xml.Serialization;
using WebService.Interfaces;
using WebService.Models;

namespace WebService.DAL
{
    public class ApartmentRepository : IRepository<Apartment>
    {
        private const string API = "http://localhost:5078/apartment/AsXmlDocument";

        public List<Apartment> apartments;

        public async Task<IList<Apartment>> GetAll()
        {            
            var client = new RestClient(API);
            var request = new RestRequest();
         
            var apiResult = client.Execute<List<Apartment>>(request);
            apartments = JsonConvert.DeserializeObject<List<Apartment>>(apiResult.Content);

            return apartments;
        }

        public async Task<IList<Apartment>> GetAllXml()
        {
            var client = new RestClient(API);
            var request = new RestRequest();

            var apiResult = client.Execute(request);

            var serializer = new XmlSerializer(typeof(List<Apartment>));
            using (var reader = new StringReader(apiResult.Content))
            {
                var apartments = (List<Apartment>)serializer.Deserialize(reader);
                return apartments;
            }
        }

        public Apartment GetById(int id)
        {
            return apartments.SingleOrDefault(a => a.IDApartment == id);
        }
    }
}
