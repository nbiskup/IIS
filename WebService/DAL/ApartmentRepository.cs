using Newtonsoft.Json;
using RestSharp;
using WebService.Interfaces;
using WebService.Models;

namespace WebService.DAL
{
    public class ApartmentRepository : IRepository<Apartment>
    {
        private const string API = "http://localhost:5078/apartment";

        public List<Apartment> apartments;

        public async Task<IList<Apartment>> GetAll()
        {            
            var client = new RestClient(API);
            var request = new RestRequest();
         
            var apiResult = client.Execute<List<Apartment>>(request);
            apartments = JsonConvert.DeserializeObject<List<Apartment>>(apiResult.Content);

            return apartments;
        }

        public Apartment GetById(int id)
        {
            return apartments.SingleOrDefault(a => a.IDApartment == id);
        }
    }
}
