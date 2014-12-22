using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace VisorRemoting.V3
{
    public class RemotingStart
    {
        //    private static ManualResetEvent connectDone =
        //        new  ManualResetEvent(false);
        //    private static ManualResetEvent sendDone =
        //        new ManualResetEvent(false);
        //    private static ManualResetEvent receiveDone =
        //        new ManualResetEvent(false);
        //    private static String response = String.Empty;
        //    private ConfigConnection configuration;


        //    public void StartClient()
        //    {
        //        try
        //        {

        //            client.Bind(new IPEndPoint(IPAddress.Parse(configuration.localHost), configuration.LocalPort));
        //            client.BeginConnect(configuration.RemoteHost, configuration.RemotePort,
        //                new AsyncCallback(ConnectCallback), client);
        //            connectDone.WaitOne();
        //            Send(client, "(125999RE");
        //            sendDone.WaitOne();
        //            Receive(client);
        //            receiveDone.WaitOne();
        //            client.Shutdown(SocketShutdown.Both);
        //            client.Close();
        //        }
        //        catch (Exception e)
        //        {

        //        }
        //    }
        //    private void ConnectCallback(IAsyncResult ar)
        //{
        //    try
        //    {
        //        Socket client = (Socket)ar.AsyncState;
        //        client.EndConnect(ar);

        //        System.Console.WriteLine("Socket connected to {0}",
        //            client.RemoteEndPoint.ToString());
        //        connectDone.Set();
        //    }
        //    catch (Exception e)
        //    {
        //        System.Console.WriteLine(e.ToString());
        //    }
        //}
        //    private void Receive(Socket client)
        //{
        //    try
        //    {

        //        panel.workSocket = client;
        //        client.ReceiveTimeout = 3000;
        //        client.BeginReceive(panel.buffer, 0, StateObject.BufferSize, 0,
        //            new AsyncCallback(ReceiveCallback), panel);
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}
        //    private void ReceiveCallback(IAsyncResult ar)
        //{
        //    try
        //    {
        //        Panel state = (Panel)ar.AsyncState;
        //        Socket client = state.workSocket;
        //        int bytesRead = client.EndReceive(ar);

        //        if (bytesRead > 0)
        //        {
        //            if (!state.FullResponse)
        //            {
        //                state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
        //                state.Read();
        //                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
        //                    new AsyncCallback(ReceiveCallback), state);
        //            }
        //            else
        //            {   
        //                string Ack = string.Empty;
        //                Ack = "(" + state.sb.ToString().Substring(4, 3) + "999AK" + state.sb.ToString().Substring(30, 2);
        //                Ack = Ack + CalculaCheckSum(Ack) + Convert.ToChar(13);
        //                SendACK(client, Ack);
        //                receiveDone.Set();
        //            }
        //        }
        //        else
        //        {

        //            if (state.sb.Length > 1)
        //            {
        //                response = state.sb.ToString();
        //            }
        //            receiveDone.Set();
        //        }
        //    }
        //    catch (SocketException se) {
        //        System.Console.WriteLine(se.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        System.Console.WriteLine(e.ToString());
        //    }
        //}
        //    private void SendACK(Socket client, String data)
        //    {

        //        byte[] byteData = Encoding.ASCII.GetBytes(data);
        //        client.BeginSend(byteData, 0, byteData.Length, 0,
        //            new AsyncCallback(SendCallback), client);
        //    }
        //    private void Send(Socket client, String data)
        //{

        //    data += CalculaCheckSum(data) + Convert.ToChar(13);
        //    byte[] byteData = Encoding.ASCII.GetBytes(data);
        //    client.BeginSend(byteData, 0, byteData.Length, 0,
        //        new AsyncCallback(SendCallback), client);
        //}
        //    private void SendCallback(IAsyncResult ar)
        //{
        //    try
        //    {

        //        Socket client = (Socket)ar.AsyncState;
        //        int bytesSent = client.EndSend(ar);
        //        sendDone.Set();
        //    }
        //    catch (Exception e)
        //    {
        //        System.Console.WriteLine(e.ToString());
        //    }
        //}
        //    private string CalculaCheckSum(string trama)
        //{

        //    int suma = 0;

        //    for (int i = 0; i < trama.Length; i++)
        //    {
        //        suma = (int)((suma + char.ConvertToUtf32(trama, i)) & 255);
        //    }

        //    if (suma.ToString("X2").Length == 1)
        //    {
        //        return "0" + suma.ToString("X2");
        //    }
        //    else
        //    {
        //        return suma.ToString("X2");
        //    }
        //}
        //    private bool CheckSum(string trama)
        //{

        //    int suma = 0;
        //    string suma_hex;

        //    for (int i = 0; i < 30; i++)
        //    {
        //        suma = (int)((suma + char.ConvertToUtf32(trama, i)) & 255);
        //    }

        //    suma_hex = suma.ToString("X2");

        //    if (suma_hex.Length == 1)
        //    {
        //        suma_hex = "0" + suma_hex;
        //    }

        //    if (suma_hex == trama.Substring(30, 2))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //}
    }
}
