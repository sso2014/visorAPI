using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace VisorRemoting.V3 {

public class Connectioned
{
   
    private const int port = 11000;
   
    private static ManualResetEvent connectDone =
        new ManualResetEvent(false);
    private static ManualResetEvent sendDone =
        new ManualResetEvent(false);
    private static ManualResetEvent receiveDone =
        new ManualResetEvent(false);
    private static String response = String.Empty;

    public static void StartClient()
    {
       
        try
        {
          
            Socket client = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            client.Bind(new IPEndPoint(IPAddress.Parse("105.1.4.222"), 11000));
            client.BeginConnect("105.1.0.125",10000,
                new AsyncCallback(ConnectCallback), client);
            connectDone.WaitOne();
            Send(client, "(125999RE");
            sendDone.WaitOne();
            Receive(client);
            receiveDone.WaitOne();
            // Write the response to the console.
            System.Console.WriteLine("Response received : {0}", response);

            // Release the socket.
            client.Shutdown(SocketShutdown.Both);
            client.Close();

        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.ToString());
        }
    }

    private static void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            Socket client = (Socket)ar.AsyncState;
            client.EndConnect(ar);

            System.Console.WriteLine("Socket connected to {0}",
                client.RemoteEndPoint.ToString());
            connectDone.Set();
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.ToString());
        }
    }

    private static void Receive(Socket client)
    {
        try
        {
            // Create the state object.
            //StateObject state = new StateObject();
            Panel panel = new Panel();
            //state.workSocket = client;
            panel.workSocket = client;

            // Begin receiving the data from the remote device.
            client.BeginReceive(panel.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReceiveCallback), panel);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.ToString());
        }
    }

    private static void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            Panel state = (Panel)ar.AsyncState;
            Socket client = state.workSocket;
            int bytesRead = client.EndReceive(ar);

            if (bytesRead > 0)
            {
               
                state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                System.Console.WriteLine(state.sb.ToString());
                state.Read();
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            else
            {
                // All the data has arrived; put it in response.
                if (state.sb.Length > 1)
                {
                    response = state.sb.ToString();
                }
                // Signal that all bytes have been received.
                receiveDone.Set();
            }
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.ToString());
        }
    }

    private static void Send(Socket client, String data)
    {
        //Send data
        data += CalculaCheckSum(data) + Convert.ToChar(13);
        byte[] byteData = Encoding.ASCII.GetBytes(data);

        // Begin sending the data to the remote device.
        client.BeginSend(byteData, 0, byteData.Length, 0,
            new AsyncCallback(SendCallback), client);
    }

    private static void SendCallback(IAsyncResult ar)
    {
        try
        {
            // Retrieve the socket from the state object.
            Socket client = (Socket)ar.AsyncState;

            // Complete sending the data to the remote device.
            int bytesSent = client.EndSend(ar);
            System.Console.WriteLine("Sent {0} bytes to server.", bytesSent);

            // Signal that all bytes have been sent.
            sendDone.Set();
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.ToString());
        }
    }

  
    private static string CalculaCheckSum(string trama)
    {

        int suma = 0;

        for (int i = 0; i < trama.Length; i++)
        {
            suma = (int)((suma + char.ConvertToUtf32(trama, i)) & 255);
        }

        if (suma.ToString("X2").Length == 1)
        {
            return "0" + suma.ToString("X2");
        }
        else
        {
            return suma.ToString("X2");
        }
    }
    private static bool CheckSum(string trama)
    {

        int suma = 0;
        string suma_hex;

        for (int i = 0; i < 30; i++)
        {
            suma = (int)((suma + char.ConvertToUtf32(trama, i)) & 255);
        }

        suma_hex = suma.ToString("X2");

        if (suma_hex.Length == 1)
        {
            suma_hex = "0" + suma_hex;
        }

        if (suma_hex == trama.Substring(30, 2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
}