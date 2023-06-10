using SOAP.Modals;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SOAP.Repository
{
    public static class Generate
    {
        public static XElement GenerateXML()
        {
            XElement xElement = new XElement("Apartments");

            List<Apartment> apartments = GetApartment();

            foreach (var item in apartments)
            {
                xElement.Add(
                        new XElement("apartment",
                        new XElement("idApartment", item.IDApartment),
                        new XElement("name", item.Name),
                        new XElement("bedrooms", item.Bedrooms),
                        new XElement("bathrooms", item.Bathrooms),
                        new XElement("canSleepMax", item.CanSleepMax),
                        new XElement("from", item.From),
                        new XElement("from_ShortDate", item.From_ShortDate),
                        new XElement("to", item.To),
                        new XElement("to_ShortDate", item.To_ShortDate)
                    ));
            }

            return xElement;
        }

        public static List<Apartment> GetApartment()
        {
            List<Apartment> apartments = new List<Apartment>
           {
               new Apartment
               {
                   IDApartment = 1,
                   Name = "Vila Kirel",
                   Bathrooms = 3,
                   Bedrooms = 4,
                   CanSleepMax = 7,
                   To = new System.DateTime(2023-05-11),
                   From_ShortDate = "09/05/2023",
                   To_ShortDate = "11/05/2023"
               },
               new Apartment
               {
                   IDApartment = 2,
                   Name = "Apartment Bili Biser",
                   Bathrooms = 2,
                   Bedrooms = 1,
                   CanSleepMax = 4,
                   From_ShortDate = "05/07/2023",
                   To_ShortDate = "12/07/2023"
               },
               new Apartment
               {
                   IDApartment = 3,
                   Name = "Kuca za odmor",
                   Bathrooms = 3,
                   Bedrooms = 3,
                   CanSleepMax = 6,
                   From_ShortDate = "10/06/2023",
                   To_ShortDate = "24/06/2023"
               }
           };

            return apartments;
        }

    }
}