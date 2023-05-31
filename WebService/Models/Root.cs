using System.Xml.Serialization;

namespace WebService.Models
{
    [XmlRoot(ElementName = "root")]
    public class Root
    {
        [XmlElement(ElementName = "row")]
        public Apartment Apartment { get; set; }
    }
}
