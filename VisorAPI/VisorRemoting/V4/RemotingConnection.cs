using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace VisorRemoting.V4
{
    public  class RemotingConnection:IRemoting
    {
        public RemotingConnection() { 
        }
        public RemotingConnection(RemotingConfig RemotingConfig) {
            this.remotingConfig = RemotingConfig;
        }
        private ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private ManualResetEvent receiveDone =
            new ManualResetEvent(false);
        private String response = String.Empty;
        private RemotingConfig remotingConfig;

        private bool Fullresponse = false;
             
        string IRemoting.GetData()
        {
            try
            {
               
                Socket client = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
                client.Bind(new IPEndPoint(IPAddress.Parse(remotingConfig.localHost), remotingConfig.LocalPort));
                client.BeginConnect(remotingConfig.RemoteHost, remotingConfig.RemotePort,
                    new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();
                Send(client, "("+remotingConfig.Id+"999RE");
                sendDone.WaitOne();
                Receive(client);
                receiveDone.WaitOne();
                client.Shutdown(SocketShutdown.Both);
                client.Close();
                return response;
            }
            catch (Exception e)
            {
                return "";
            }
        }
        private void ConnectCallback(IAsyncResult ar)
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
        private void Receive(Socket client)
        {
            try
            {
                StateObject obj = new StateObject();
                obj.workSocket = client;
                client.BeginReceive(obj.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), obj);
            }
            catch (Exception e)
            {
            
            }
        }
        private void ReceiveCallback(IAsyncResult ar)
        {
        try
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket client = state.workSocket;
            int bytesRead = client.EndReceive(ar);

            if (bytesRead > 0)
            {
                if (!Fullresponse)
                    {
                        state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                        Read(state);
                        client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                            new AsyncCallback(ReceiveCallback), state);
                    }
                    else
                    {
             
                        receiveDone.Set();
                    }
            }
     
        }
        catch (SocketException se) {
            System.Console.WriteLine(se.Message);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.ToString());
        }
    }
        private void SendACK(Socket client, String data)
        {

            byte[] byteData = Encoding.ASCII.GetBytes(data);
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }
        private void Send(Socket client, String data)
        {
            data += CalculaCheckSum(data) + Convert.ToChar(13);
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }   
        private void SendCallback(IAsyncResult ar)
    {
        try
        {
           
            Socket client = (Socket)ar.AsyncState;
            int bytesSent = client.EndSend(ar);
            sendDone.Set();
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.ToString());
        }
    }
        private void Process() { 
        
        }
        public void  Read(StateObject obj)
        {
            try
            {
                string data = obj.sb.ToString();
                string Ack = string.Empty;
                while (data.Substring(0, 4) == "(999" && data.Substring(7, 2) == "AK" && data.ToString()[13] == Convert.ToChar(13))
                {
                    data = data.Substring(14);
                }
                if (data.Substring(0, 4) == "(999" && data.ToString().Substring(10, 2) == "RE" && data.ToString()[32] == Convert.ToChar(13))
                {
                    if (CheckSum(data))
                    {
                        this.response = data;
                        this.Fullresponse = true;
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
        //private void ProcessData(string data)
        //{

        //    long est;
        //    bool aux;

        //    if (data[0] == '(' && data[32] == Convert.ToChar(13))
        //    {
        //        //.....................................................................................
        //        this.Id = data.Substring(4, 3);
        //        this.Angulo = Convert.ToInt32(data.Substring(12, 3));
        //        this.Tension = Convert.ToInt32(data.Substring(15, 3));
        //        this.Presion = Convert.ToInt32(data.Substring(18, 3));
        //        this.Aplicacion = Convert.ToInt32(data.Substring(21, 3));
        //        est = long.Parse(data.Substring(23, 6), System.Globalization.NumberStyles.HexNumber);
        //        this.Sentido = Convert.ToBoolean(est & 0x80000);
        //        this.Habilitado = Convert.ToBoolean(est & 0x40000);
        //        aux = Convert.ToBoolean(est & 0x20000);
        //        this.Caminando = Convert.ToBoolean(est & 0x20000);
        //        this.EsperandoPresion = Convert.ToBoolean(est & 0x10000);
        //        this.PresionNor = Convert.ToBoolean(est & 0x200);
        //        this.Seco = Convert.ToBoolean(est & 0x1000);
        //        this.FallaElectrica = Convert.ToBoolean(est & 0x80);
        //        this.AlarmaDeSeguridad = Convert.ToBoolean(est & 0x40);
        //        FullResponse = true;
        //    }
        //}
        private string CalculaCheckSum(string trama)
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
        private bool CheckSum(string trama)
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
