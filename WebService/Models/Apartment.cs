using Newtonsoft.Json;
using System.Xml.Serialization;

namespace WebService.Models
{
    [XmlRoot("apartments")]
    public class ApartmentList
    {
        [XmlElement(ElementName = "apartment")]
        public List<Apartment> Apartments { get; set; }

        public ApartmentList()
        {
            Apartments = new List<Apartment>();
        }

        public ApartmentList(List<Apartment> apartments)
        {
            Apartments = apartments;
        }
    }
    
    public class Apartment
    {
        [XmlElement(ElementName = "idApartment")]
        [JsonProperty(PropertyName = "idapartment")]
        public int IDApartment { get; set; }

        [XmlElement(ElementName = "name")]
        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }

        [XmlElement(ElementName = "bedrooms")]
        [JsonProperty(PropertyName = "bedrooms")]
        public int Bedrooms { get; set; }

        [XmlElement(ElementName = "bathrooms")]
        [JsonProperty(PropertyName = "bathrooms")]
        public int Bathrooms { get; set; }

        [XmlElement(ElementName = "canSleepMax")]
        [JsonProperty(PropertyName = "cansleepmax")]
        public int CanSleepMax { get; set; }

        [XmlElement(ElementName = "from")]
        [JsonProperty(PropertyName = "from")]
        public DateTime From
        {
            get { return from; }
            set
            {
                from = value;
                From_ShortDate = from.ToShortDateString();
            }
        }
        private DateTime from;

        [XmlElement(ElementName = "from_ShortDate")]
        public string? From_ShortDate { get; set; }

        [XmlElement(ElementName = "to")]
        [JsonProperty(PropertyName = "to")]
        public DateTime To
        {
            get { return to; }
            set
            {
                to = value;
                To_ShortDate = to.ToShortDateString();
            }
        }

        private DateTime to;

        [XmlElement(ElementName = "to_ShortDate")]
        public string? To_ShortDate { get; set; }

    }
}
