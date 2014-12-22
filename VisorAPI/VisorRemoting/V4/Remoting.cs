using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace VisorRemoting.V4
{
    public class Remoting:RemotingProtocol, IRemoting
    {
        public Remoting() { 
        }
        public Remoting(string id) {
            this.ID = id;
        }
        
        private byte[] buffer = new byte[BufferSize];
        private const int BufferSize = 256;
        StringBuilder sb = new StringBuilder();
        private Socket sck = null;

        public string ID { get; set; }
        public bool Connected { get; set; }

        public bool Connect() {

            try
            {
                LingerOption op = new LingerOption(false, 1);
                sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, op);
                sck.Bind(new IPEndPoint(IPAddress.Parse("105.1.4.222"), 10000 + Convert.ToInt32(ID)));//local
                sck.Connect("105.1.0." + Convert.ToInt32(this.ID), 10000);

                if (sck.Connected)
                {
                    this.Connected = true;
                    return true;
                }
                else
                {
                    this.Connected = false;
                    return false;
                }
            }
            catch (SocketException se) {

                sck.Close();
                this.Connected = false;
                //Disconnect();
                return false;
            }
        }
        private bool Send(string query) {

            sck.SendTimeout = 10000;//time Out Send
            query += CalculaCheckSum(query) + Convert.ToChar(13);
            byte[] byteData = Encoding.ASCII.GetBytes(query);

            try
            {

                int bytesSend = sck.Send(byteData);

                if (bytesSend > 0)
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (SocketException se)
            {
                //Disconnect();
                sck.Close();
                this.Connected = false;
                return false;
            }
            catch (Exception e) {
                this.Connected = false;
                return false;
            }
        }
        public bool SendCommand(ValleyCommandType command) {

            bool sendQuery = false;
            
            switch (command) { 
                case ValleyCommandType.Query:
                    sendQuery = Send("(" + this.ID + "999RE");
                    break;
                default:
                    break;
            }
            return sendQuery;
        }
        public void Receive()
        {
            sck.ReceiveTimeout = 10000;//time out sreceive
            string Ack = string.Empty;
            string data = string.Empty;

            try
            {
                System.Console.WriteLine(data);
                int BytesReceive = sck.Receive(buffer, 0, BufferSize, SocketFlags.None);

                if (BytesReceive > 0)
                {
                    data = Encoding.ASCII.GetString(buffer);

                    while (data.Substring(0, 4) == "(999" && data.Substring(7, 2) == "AK" && data.ToString()[13] == Convert.ToChar(13))
                    {
                        data = data.Substring(14);
                    }
                    if (data.Substring(0, 4) == "(999" && data.ToString().Substring(10, 2) == "RE" && data.ToString()[32] == Convert.ToChar(13))
                    {
                        if (CheckSum(data))
                        {
                            Process(data);
                            Ack = "(" + data.Substring(4, 3) + "999AK" + data.Substring(30, 2);
                            Ack = Ack + CalculaCheckSum(Ack) + Convert.ToChar(13);
                            Response = "";
                            byte[] BytesSend = Encoding.ASCII.GetBytes(Ack);
                            sck.Send(BytesSend);
                        }
                    }
                }
            }
            catch (SocketException se)
            {

                //Disconnect();
                sck.Close();
              
                this.Connected = false;
                
               
            }
            catch (ArgumentOutOfRangeException ae) { 
            
            }
        }
        public void Disconnect()
        {
            sck.Shutdown(SocketShutdown.Both);
            sck.Close();
        }
        public string GetData()
        {
            return this.Response;
        }
    }
}
