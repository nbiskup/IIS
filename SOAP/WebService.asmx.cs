using SOAP.Modals;
using SOAP.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SOAP
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        private List<Apartment> apartments = Generate.GetApartment();

        [WebMethod]

        public string Search(string query)
        {
            XElement xElement = Generate.GenerateXML();

            string filePath = @"C:\Users\Nikola\apartments.xml";

            IEnumerable<XElement> result = xElement.XPathSelectElements($"//apartment[idApartment='{query}']");

            XElement recipes = new XElement("Apartment", result);
            recipes.Save(filePath);

            StringBuilder sb = new StringBuilder();
            result.ToList().ForEach(e => sb.Append(e));

            return sb.ToString();
        }
    }
}
