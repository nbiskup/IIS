/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package wetherserver;

import java.net.*;
import java.io.*;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.xpath.*;
import org.w3c.dom.*;
import org.xml.sax.SAXException;

/**
 *
 * @author Nikola
 */
public class WeatherTools {

 

    public static String get(String city) {
        String xmlString = GetXML();

 

        try {
            DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
           Document doc = factory.newDocumentBuilder().parse(new org.xml.sax.InputSource(new java.io.StringReader(xmlString)));

 

            XPath xpath = XPathFactory.newInstance().newXPath();
            String expression = "//Grad[translate(GradIme, 'ABCDEFGHIJKLMNOPQRSTUVWXYZČĆĐŠŽ', 'abcdefghijklmnopqrstuvwxyzčćđšž')='" + city.toLowerCase() + "']/Podatci/Temp";
            Node node = (Node) xpath.evaluate(expression, doc, XPathConstants.NODE);
            return node.getTextContent();

 

        } catch (IOException | ParserConfigurationException | XPathExpressionException | DOMException | SAXException e) {
            e.printStackTrace();
        }
        return "";
    }

 

    public static class XmlRpcHandler {
        public String get(String city) {
            return WeatherTools.get(city);
        }
    }

 

    public static String GetXML() {
        String xmlString;
        try {
            URL url = new URL("https://vrijeme.hr/hrvatska_n.xml");
            HttpURLConnection con = (HttpURLConnection) url.openConnection();
            con.setRequestMethod("GET");

 

            StringBuilder content;
            try (BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()))) {
                String inputLine;
                content = new StringBuilder();
                while ((inputLine = in.readLine()) != null) {
                    content.append(inputLine);
                }
            }
            con.disconnect();
            xmlString = content.toString();
            return xmlString;
        } catch (IOException e) {
            e.printStackTrace();
        }
        return "";
    }
}