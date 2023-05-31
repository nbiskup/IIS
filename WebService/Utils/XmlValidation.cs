using System.Xml.Serialization;
using System.Xml;

namespace WebService.Utils
{
    public static class XmlValidation
    {
        public static T XmlDeserializer<T>(XmlDocument xml)
        {
            var xs= new XmlSerializer(typeof(T));

            using (XmlNodeReader xnr = new XmlNodeReader(xml.DocumentElement))
            {
                if (xs.CanDeserialize(xnr))
                {
                    return (T)xs.Deserialize(xnr);
                }
            }
            return default(T);
        }
    }
}
