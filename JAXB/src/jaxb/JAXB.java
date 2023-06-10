/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Main.java to edit this template
 */
package jaxb;

import generated.Apartments;
import generated.Apartments.Apartment;
import java.io.File;
import javax.xml.XMLConstants;
import javax.xml.bind.JAXBContext;
import javax.xml.bind.Unmarshaller;
import javax.xml.validation.Schema;
import javax.xml.validation.SchemaFactory;
import org.xml.sax.SAXException;

/**
 *
 * @author Nikola
 */
public class JAXB {

    /**
     * @param args the command line arguments
     * @throws org.xml.sax.SAXException
     */

public static void main(String[] args)throws SAXException {

        try {
            File xmlFile = new File("src/packages/apartments.xml");
            File xsdFile = new File("src/packages/apartmentXSD.xsd");
 

            // Create JAXB context for the generated classes
            JAXBContext jaxbContext = JAXBContext.newInstance("generated"); 

            // Create an unmarshaller
            Unmarshaller unmarshaller = jaxbContext.createUnmarshaller(); 

            // Create a schema factory
            SchemaFactory schemaFactory = SchemaFactory.newInstance(XMLConstants.W3C_XML_SCHEMA_NS_URI); 

            // Load the XSD schema
            Schema schema = schemaFactory.newSchema(xsdFile);

            // Set the schema on the unmarshaller
            unmarshaller.setSchema(schema); 

            // Unmarshal the XML file into the generated classes
            Apartments apartments = (Apartments) unmarshaller.unmarshal(xmlFile); 

            System.out.println("XML file is valid!"); 

        } catch (Exception e) {
            System.out.println("XML file is NOT valid!");            
        }

    }
    
}
