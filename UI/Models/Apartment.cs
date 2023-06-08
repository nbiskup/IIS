using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace UI.Models
{
    [XmlRoot("apartments")]
    public class ApartmentList
    {
        [XmlElement("apartment")]
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
        [XmlElement("idApartment")]
        [JsonProperty(PropertyName = "idapartment")]
        public int IDApartment { get; set; }

        [XmlElement("name")]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [XmlElement("bedrooms")]
        [JsonProperty(PropertyName = "bedrooms")]
        public int Bedrooms { get; set; }

        [XmlElement("bathrooms")]
        [JsonProperty(PropertyName = "bathrooms")]
        public int Bathrooms { get; set; }

        [XmlElement("canSleepMax")]
        [JsonProperty(PropertyName = "cansleepmax")]
        public int CanSleepMax { get; set; }

        [XmlElement("from")]
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

        [XmlElement("from_ShortDate")]
        public string From_ShortDate { get; set; }

        [XmlElement("to")]
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

        [XmlElement("to_ShortDate")]
        public string To_ShortDate { get; set; }
    }
}