using System;
using System.Net.Sockets;
using System.Text;

namespace VisorRemoting.V5
{
    public class Remoting : RemotingProtocol, IRemoting
    {
        public Remoting()
        {

        }
        public Remoting(string id)
        {
            this.ID = id;
            this.Command = ValleyCommandType.Query;
            this.connected = false;
            this.SetCommand(ValleyCommandType.Query);
        }

        private byte[] buffer = new byte[bufferSize];
        private const int bufferSize = 1024;
        private Socket sck;
        private bool connected;
        public ValleyCommandType Command { get; set; }
        public string ID { get; set; }
        private string Query = string.Empty;
        
        bool IRemoting.Connected
        {
            get { return this.connected; }
            set { this.connected = value; }
        }
        void IRemoting.Connect()
        {
            try
            {
                LingerOption op = new LingerOption(false, 1);
                sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, op);
                sck.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, 10000 + Convert.ToInt32(this.ID)));


                sck.Connect("105.1.0." + Convert.ToInt32(this.ID), 10000);

                if (sck.Connected)
                {
                    this.connected = true;
                    System.Console.WriteLine("Conectado...");
                }
                else
                {
                    this.connected = false;
                }
            }
            catch (SocketException se)
            {
                if (!sck.Connected)
                {
                    this.sck.Close();
                    this.connected = false;
                }
            }
        }
        void IRemoting.Receive()
        {

            sck.ReceiveTimeout = 10000;//time out sreceive

            string Ack = string.Empty;
            string data = string.Empty;

            try
            {
                int BytesReceive = sck.Receive(buffer, 0, bufferSize, SocketFlags.None);

                if (BytesReceive > 0)
                {
                    data = Encoding.ASCII.GetString(buffer);

                    System.Console.WriteLine(data);

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
                            sck.SendTimeout = 10000;
                            sck.Send(BytesSend);
                        }
                    }
                }
            }
            catch (SocketException se)
            {
                if (!sck.Connected)
                {
                    sck.Close();
                    this.connected = false;
                }
            }
            catch (ArgumentOutOfRangeException ae)
            {

            }
        }
        void IRemoting.SendCommand()
        {
            Send(this.Query);           
        }
        string IRemoting.GetData()
        {
            return Response;
        }

        private void Send(string query)
        {
            sck.SendTimeout = 10000;//time Out Send
            query += CalculaCheckSum(query) + Convert.ToChar(13);
            byte[] byteData = Encoding.ASCII.GetBytes(query);

            try
            {
                sck.Send(byteData);
            }
            catch (SocketException se)
            {
                if (!sck.Connected)
                {
                    sck.Close();
                    this.connected = false;
                }
            }
        }
        public void Disconnect()
        {
            this.sck.Shutdown(SocketShutdown.Both);
            this.sck.Close();
        }
        public void SetCommand(ValleyCommandType ValleyCommand)
        {
            switch (ValleyCommand)
            {
                case ValleyCommandType.Query:
                    Query = "(" + this.ID + "999RE";
                    this.Command = ValleyCommandType.Query;
                    break;
                default:
                    Query = "(" + this.ID + "999RE";
                    this.Command = ValleyCommandType.Query;
                    break;
            }
        }
    }
}
