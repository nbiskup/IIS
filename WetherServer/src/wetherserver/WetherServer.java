/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Main.java to edit this template
 */
package wetherserver;

import java.io.IOException;
import org.apache.xmlrpc.XmlRpcException;
import org.apache.xmlrpc.server.PropertyHandlerMapping;
import org.apache.xmlrpc.server.XmlRpcServer;
import org.apache.xmlrpc.server.XmlRpcServerConfigImpl;
import org.apache.xmlrpc.webserver.WebServer;

/**
 *
 * @author Nikola
 */
public class WetherServer {

    /**
     * @param args the command line arguments
     * @throws java.io.IOException
     * @throws org.apache.xmlrpc.XmlRpcException
     */
    public static void main(String[] args) throws IOException, XmlRpcException {
        System.out.println("Creating server ...");
        WebServer server = new WebServer(8090);

        XmlRpcServer xmlServer = server.getXmlRpcServer();
        PropertyHandlerMapping phm = new PropertyHandlerMapping();
        phm.addHandler("WeatherTools", WeatherTools.XmlRpcHandler.class);
        xmlServer.setHandlerMapping(phm);

        XmlRpcServerConfigImpl config = (XmlRpcServerConfigImpl) xmlServer.getConfig();
        config.setEnabledForExceptions(true);
        config.setContentLengthOptional(false);
        config.setEnabledForExtensions(true);

        System.out.println("Starting server ...");
        server.start();
        System.out.println("Server started.");
    }
    
}
