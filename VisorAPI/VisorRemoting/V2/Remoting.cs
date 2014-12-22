  using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Core;

namespace VisorRemoting.V2
{
    public class Remoting
    {
        public static double Interval = 4000;
        private static Socket s = null;
        private static bool exit = false;
        private static ClassHandlerData hd;
        private static System.Data.DataSet ds = null;
        public static string id { get; set; }
        public static string tdata = string.Empty;
        public static string Query = string.Empty;
   
        public static void doWork() {
            do
            {
                SendQuery();
                System.Threading.Thread.Sleep((int)Interval);
                Receive();

            } while (true);
        }
        private static void Command(CommandType command) { 
            
            switch (command){

                case CommandType.Query:
                    Query = "(" + id + "999RE";    
                    break;
                
                case CommandType.Marcha:
                
                    break;
                case CommandType.Parada:

                    break;
                case CommandType.Reversa:

                    break;
                case CommandType.Foward:

                    break;
                case CommandType.Aplicacion:

                    break;

                default :
                    break;
            }
        }
        private static Socket ConnectSocket(string remoteHost, int remotePort)
        {
            try
            {
                Socket tempSocket =
                new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                tempSocket.Bind(new IPEndPoint(IPAddress.Parse("105.1.4.222"), 11000));
                tempSocket.Connect(remoteHost, remotePort);

                if (tempSocket.Connected)
                {
                   return tempSocket;
                }
                return null;
            }
            catch (SocketException se) {
               return null;
            }
        }
        public static void CreateSocket(string remoteHost, int remotePort) {

            try
            {
                if (s == null)
                {
                    s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    s.Bind(new IPEndPoint(IPAddress.Parse("105.1.4.222"), 11000));
                    s.Connect(remoteHost, remotePort);
                }
                else
                {
                    s.Close();
                    s.Connect(remoteHost, remotePort);
                }
            }
            catch (SocketException se){
            }
            catch (InvalidOperationException ioe){ 
            }
        }
        static void GetSocket_EventData(string data)
        {

            string Ack = string.Empty;

            while (data.Substring(0, 4) == "(999" && data.Substring(7, 2) == "AK" && data.ToString()[13] == Convert.ToChar(13))
            {
                data = data.Substring(14);
            }
            if (data.Substring(0, 4) == "(999" && data.ToString().Substring(10, 2) == "RE" && data.ToString()[32] == Convert.ToChar(13))
            {
                if (CheckSum(data))
                {
                    Read(data);
                    Ack = "(" + data.Substring(4, 3) + "999AK" + data.Substring(30, 2);
                    Ack = Ack + CalculaCheckSum(Ack) + Convert.ToChar(13);
                    Send(s, Ack);
                }
            }
        }        
        public static void SendAndReceive() {

            string query = string.Format(
              "("+id+"999RE");

            Byte[] bytesReceived = new Byte[256];
            string data = string.Empty;
            int bytes = 0;

            if (s != null)
            {
                if (s.Connected)
                {
                        query += CalculaCheckSum(data) + Convert.ToChar(13);
                        byte[] byteData = Encoding.ASCII.GetBytes(query);
                        s.Send(byteData);
                    try
                    {
                        do
                        {
                            bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
                            data = data + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
                            hd.Trigger(data);
                        }
                        while (exit == false);
                    }
                    catch (SocketException se)
                    {
                       
                    }
                }
            }
        }
        public static void Receive() {

            try
            {
                hd = new ClassHandlerData();
                hd.EventData += new ClassHandlerData.HandlerData(hd_EventData);

                Byte[] bytesReceived = new Byte[256];
                string data = string.Empty;
                int bytes = 0;

                if (s.Connected)
                {
                    s.ReceiveTimeout = 3000;

                    do
                    {
                        bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
                        data = data + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
                        hd.Trigger(data);
                    }
                    while (exit == false);
                }
            }
            catch (SocketException se){
            }
            catch (NullReferenceException){ 
            }
        }
        public static void SendQuery() {

            string query = string.Format(
                    "("+id+"999RE");
            try
            {
                if (s == null)
                    CreateSocket("105.1.0." + Convert.ToInt32(id), 10000);
                             
                if (s != null)
                {
                    if (s.Connected)
                    {
                        query += CalculaCheckSum(query) + Convert.ToChar(13);
                        byte[] bytesSent = Encoding.ASCII.GetBytes(query);
                        s.SendTimeout = 1000;
                        s.Send(bytesSent);

                      
                    }
                    else
                    {
                        CreateSocket("105.1.0." + Convert.ToInt32(id), 10000);
                    }
                }
            }
            catch (SocketException se) {
            }
        }
        public static string SocketSendReceive()
        {
            hd = new ClassHandlerData();
            hd.EventData += new ClassHandlerData.HandlerData(hd_EventData);

            string query = string.Format(
                "("+id+"999RE");

            Byte[] bytesReceived = new Byte[256];
            string data = string.Empty;
            int bytes = 0;
            
            try {

                query += CalculaCheckSum(query) + Convert.ToChar(13);
                Byte[] bytesSent = Encoding.ASCII.GetBytes(query);

            s = ConnectSocket("105.1.0." + Convert.ToInt32(id), 10000);

            if (s == null)
                return ("Connection failed");
                    
            if (s.Connected)
            {
                query += CalculaCheckSum(data) + Convert.ToChar(13);
                byte[] byteData = Encoding.ASCII.GetBytes(query);
                s.Send(byteData);
            }
                do
                {
                    bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
                    data = data + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
                    hd.Trigger(data);
                }
                while (exit == false);

                return data;
            }
            catch (SocketException se)
            {
                return "out";
            }
        }
        public static string data = string.Empty;
        static void hd_EventData(string d)
        {
            tdata = d;
            string data = d;
            string Ack = string.Empty;

            try
            {
                System.Console.WriteLine(d);
                //if (data.Substring(10, 1) == "R" && data.Substring(11, 1) == "E")
                //{
                    while (data.Substring(0, 4) == "(999" && data.Substring(7, 2) == "AK" && data.ToString()[13] == Convert.ToChar(13))
                    {
                        data = data.Substring(14);
                    }
                    if (data.Substring(0, 4) == "(999" && data.ToString().Substring(10, 2) == "RE" && data.ToString()[32] == Convert.ToChar(13))
                    {
                        if (CheckSum(data))
                        {
                            Read(data);
                            Ack = "(" + data.Substring(4, 3) + "999AK" + data.Substring(30, 2);
                            Ack = Ack + CalculaCheckSum(Ack) + Convert.ToChar(13);
                            byte[] bytesSend = Encoding.ASCII.GetBytes(Ack);
                            s.Send(bytesSend);
                        }
                    }
                    if (data.Substring(0, 4) == "(999" && data.Substring(7, 2)== "AK") {
                        string s = "ok";
                    } 

                //}
            }
            catch (IndexOutOfRangeException ioore)
            {
            }
            catch (ArgumentOutOfRangeException aoore) { 
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
        private static void Read(string data)
        {
            long est;
            bool aux;
            string Ack = string.Empty;

            if (data[0] == '(' && data[32] == Convert.ToChar(13))
            {
                est = long.Parse(data.Substring(23, 6), System.Globalization.NumberStyles.HexNumber);
                aux = Convert.ToBoolean(est & 0x20000);
                GeneradorDataset.Insert(data.Substring(4, 3),
                    Convert.ToInt32(data.Substring(18, 3)),
                    Convert.ToBoolean(est & 0x40),
                    Convert.ToInt32(data.Substring(12, 3)),
                    Convert.ToInt32(data.Substring(21, 3)),
                    Convert.ToBoolean(est & 0x20000),
                    Convert.ToBoolean(est & 0x10000),
                    Convert.ToBoolean(est & 0x80),
                    Convert.ToBoolean(est & 0x40000),
                    Convert.ToBoolean(est & 0x200),
                    Convert.ToBoolean(est & 0x1000),
                    Convert.ToBoolean(est & 0x80000),
                    Convert.ToInt32(data.Substring(15, 3)));
                GeneradorDataset.Save();
                exit = true;
            }
        }
        private static void Send(Socket sck, string data)
        {
            try
            {
                if (sck.Connected)
                {
                    data += CalculaCheckSum(data) + Convert.ToChar(13);
                    byte[] byteData = Encoding.ASCII.GetBytes(data);
                    sck.Send(byteData);
                }
            }
            catch (Exception e)
            {
               
            }
        }
    }
}
