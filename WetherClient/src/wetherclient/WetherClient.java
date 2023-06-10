/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Main.java to edit this template
 */
package wetherclient;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.Scanner;
import org.apache.xmlrpc.XmlRpcException;
import org.apache.xmlrpc.client.XmlRpcClient;
import org.apache.xmlrpc.client.XmlRpcClientConfigImpl;

/**
 *
 * @author Nikola
 */
public class WetherClient {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        try {
            // Connecting RPC jar service and server
            XmlRpcClientConfigImpl config = new XmlRpcClientConfigImpl();
            config.setServerURL(new URL("http://localhost:8080"));
            config.setEnabledForExceptions(true);
            config.setContentLengthOptional(false);
            config.setEnabledForExtensions(true);

 

            XmlRpcClient client = new XmlRpcClient();
            client.setConfig(config);

 

            BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
            boolean loop = true;
            do {
                System.out.println("Unesite grad: ");
                Scanner scanner = new Scanner(System.in);
                String inputString = scanner.nextLine();
                Object[] parameters = new Object[]{inputString};
                System.out.println(client.execute("WeatherTools.get", parameters));

 

                System.out.println("Dalje? 1:0");
                int option = Integer.parseInt(scanner.nextLine());
                if (option == 0) {
                    loop = false;
                }
            } while (loop);

 

        } catch (MalformedURLException | XmlRpcException ex) {
        }
    }
    }
    

