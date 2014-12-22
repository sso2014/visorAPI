using System;
using System.Net.Sockets;
using System.Text;

namespace VisorRemoting.V8
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
        private const int bufferSize = 256;
        private Socket sck;
        private bool connected;
        public ValleyCommandType Command { get; set; }
        public string ID { get; set; }
        private string Query = string.Empty;
        private string Response = string.Empty;
        //public delegate void RemotingEventHandler(object sender, EventArgs args);
        //public event RemotingEventHandler RemotingEvent;
        //private void Trigger() { 
        //}
         
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
                    this.connected = true;
                else
                    this.connected = false;
            }
            catch (SocketException se)
            {
                //this.sck.Close();
            }
        }
        void IRemoting.Receive()
        {
            sck.ReceiveTimeout = 10000; //time out receive
            string Ack = string.Empty;
            string data = string.Empty;
       
            try
            {
                int BytesReceive = sck.Receive(buffer, 0, bufferSize, SocketFlags.None);

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
                            if (data[0] == '(' && data[32] == Convert.ToChar(13))
                            {
                                this.Response = data;
                                this.Data = data;
                                Ack = "(" + data.Substring(4, 3) + "999AK" + data.Substring(30, 2);
                                Ack = Ack + CalculaCheckSum(Ack) + Convert.ToChar(13);
                                byte[] BytesSend = Encoding.ASCII.GetBytes(Ack);
                                sck.SendTimeout = 10000; //Time Out!
                                sck.Send(BytesSend);
                            }
                        }
                    }
                }
            }
            catch (SocketException se)
            {
                this.connected = false;
            }
            catch (ArgumentOutOfRangeException ae)
            {
            }
            catch (ObjectDisposedException ode)
            {

            }
        }
        void IRemoting.SendCommand()
        {
            Send(this.Query);           
        }
        string IRemoting.GetData()
        {
            if (Response != "")
            {
                return this.Response;
            }
            else {
                return "Not data";
            }
        }
        private void Send(string query)
        {
            sck.SendTimeout = 10000;//time Out Send
            query += CalculaCheckSum(query) + Convert.ToChar(13);
            byte[] byteData = Encoding.ASCII.GetBytes(query);
            try
            {
                 int bytesCount = sck.Send(byteData);

                 if (bytesCount > 0) {
                     SetCommand(ValleyCommandType.Query);
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
        }
        public void Disconnect()
        {
            this.sck.Shutdown(SocketShutdown.Both);
            this.sck.Close();
            this.sck.Dispose();
        }
        public void SetCommand(ValleyCommandType ValleyCommand)
        {
            switch (ValleyCommand)
            {
                case ValleyCommandType.Query:
                    Query = "(" + this.ID + "999RE";
                    this.Command = ValleyCommandType.Query;
                    break;
                case ValleyCommandType.Foward:
                    Query = "(" + this.ID + "999SDF;RE";
                    break;
                case ValleyCommandType.Reversa:
                    Query = "("+ this.ID + "999SDR;RE";
                    break;
                default:
                    Query = "(" + this.ID + "999RE";
                    this.Command = ValleyCommandType.Query;
                    break;
                #region
                //                     Case 1
                //                    Trama = "(" & num_pivot & "999SDF;RE"
                //'                    If Trama <> "" Then EnviaComandoValley02 Trama, i
                //                Case 2
                //                    Trama = "(" & num_pivot & "999SDR;RE"
                //'                    If Trama <> "" Then EnviaComandoValley02 Trama, i
                //                Case 3
                //                    Trama = "(" & num_pivot & "999POW;RE"
                //'                    If Trama <> "" Then EnviaComandoValley02 Trama, i
                //                Case 4
                //                    Trama = "(" & num_pivot & "999POD;RE"
                //'                    If Trama <> "" Then EnviaComandoValley02 Trama, i
                //                Case 5
                //                    Trama = "(" & num_pivot & "999RS;RE"
                //'                    If Trama <> "" Then EnviaComandoValley02 Trama, i
                //                Case 6
                //                    Trama = "(" & num_pivot & "999SO;RE"
                //'                    If Trama <> "" Then EnviaComandoValley02 Trama, i
                //                Case 7
                //                    Trama = "(" & num_pivot & "999SP" & Trim(Str(atributo)) & ";RE"
                //'                    If Trama <> "" Then EnviaComandoValley02 Trama, i
                //                Case 8
                //                    Trama = "(" & num_pivot & "999SA" & Trim(Str(atributo)) & ";RE"
                //'                    If Trama <> "" Then EnviaComandoValley02 Trama, i

                #endregion
            }
        }


        public string Data
        {
            get;
            set;
            
        }
    }
}
