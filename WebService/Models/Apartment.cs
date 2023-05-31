using Newtonsoft.Json;
using System.Xml.Serialization;

namespace WebService.Models
{
    public class Apartment
    {
        [XmlElement("idApartment")]
        public int IDApartment { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("bedrooms")]
        public int Bedrooms { get; set; }

        [XmlElement("bathrooms")]
        public int Bathrooms { get; set; }

        [XmlElement("canSleepMax")]
        public int CanSleepMax { get; set; }

        [XmlElement("from")]
        public DateTime From { get; set; }

        [XmlElement("from_ShortDate")]
        public string From_ShortDate { get; set; }

        [XmlElement("to")]
        public DateTime To { get; set; }

        [XmlElement("to_ShortDate")]
        public string To_ShortDate { get; set; }

    }
}
