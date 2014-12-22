using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Data;

namespace VisorRemoting.V2
{
    public class RemotingConnection : BaseRemoting, IDisposable
    {
        public RemotingConnection(string Id, RemotingConfig remotingConfig)
        {
            this.Args.PrimeraLectura = true;
            this.id = Id;
            this.config = remotingConfig;
        }
        public RemotingConnection()
        {
            this.config = new RemotingConfig();
        }
        
        private Socket sck = null;
        private bool disposed;
        public delegate void RemotingEventHandler(object sender, RemotingEventArgs e);
        public event RemotingEventHandler RemotingEvent;
        private RemotingEventArgs Args = new RemotingEventArgs();
        private RemotingConfig config = null;
        private string CommandQuery = string.Empty;
        private string id = string.Empty;
        private Byte[] bytesReceived = new Byte[BufferSize];
        public const int BufferSize = 256;
        string response = string.Empty;
         private System.Threading.ManualResetEvent receiveDone =
            new System.Threading.ManualResetEvent(false);

        public RemotingConfig RemoteConfig
        {
            get { return config; }
            set { config = value; }

        }
        public string ID {
            get { return id; }
            set { id = value; }
        }

        public string LocalHost  { get; set; }
        public string RemoteHost { get; set; }
        public int    LocalPort  { get; set; }
        public int    RemotePort { get; set; }
        public bool   State      { get; set; }

        public ValleyCommandType ValleyCommand
        {
            set
            {
                switch (value)
                {
                    case ValleyCommandType.Query:
                        CommandQuery = "(" + id + "999RE";
                        break;
                    case ValleyCommandType.Marcha:
                        CommandQuery = "(" + id + "999POW;RE";
                        break;
                    case ValleyCommandType.Parada:
                        break;
                    case ValleyCommandType.Foward:
                        CommandQuery = "(" + id + "999SDF;RE";
                        break;
                    case ValleyCommandType.Reversa:
                        CommandQuery = "(" + id + "999SDR;RE";
                        break;
                    case ValleyCommandType.Aplicacion:
                        break;
                }
            }
        }
        private void Trigger(object sender, RemotingEventArgs e)
        {
            if (RemotingEvent != null)
            {
                RemotingEvent(sender, e);
            }
        }
        public bool Connect()
        {
            try
            {
                LingerOption Ops = new LingerOption(true, 1);
                sck =
                new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sck.Bind(new IPEndPoint(IPAddress.Parse(config.localHost), config.LocalPort));
                sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger,false);
                sck.Connect(config.RemoteHost, config.RemotePort);

                if (sck.Connected)
                {
                    this.State = sck.Connected;
                    return true;
                }
                else
                {
                    this.State = false;
                    return false;
                }
            }
            catch (SocketException se)
            {
                this.State = false;
                System.Console.WriteLine(se.Message);
                return false;
            }
        }
        public void SendQuery(){
            try
            {
                if (sck.Connected)
                {
                    CommandQuery += CalculaCheckSum(CommandQuery) + Convert.ToChar(13);
                    byte[] byteSend = Encoding.ASCII.GetBytes(CommandQuery);
                    int num = sck.Send(byteSend);

                    if (num > 0)
                    {
                        ValleyCommand = ValleyCommandType.Query;
                    }
                }
            }
            catch (SocketException e) {
                System.Console.WriteLine(e.Message);
            }
        }
        public void SendAndReceive() {

            try
            {
                Connect();
                

                    if (sck.Connected)
                    {
                        CommandQuery += CalculaCheckSum(CommandQuery) + Convert.ToChar(13);
                        byte[] byteSend = Encoding.ASCII.GetBytes(CommandQuery);
                        int num = sck.Send(byteSend);

                        if (num > 0)
                        {
                            Byte[] bytesReceived = new Byte[256];
                            string data = string.Empty;
                            int bytes = 0;

                            if (sck.Connected)
                            {
                                sck.ReceiveTimeout = 3000;
                                bytes = sck.Receive(bytesReceived, bytesReceived.Length, 0);
                                data = data + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
                                Args.Data = data;
                                Trigger(this, Args);
                            }
                        }
                    }
     
            }
            catch (SocketException e)
            {
                System.Console.WriteLine(e.Message);
            }
            finally {
                this.Dispose();
            }
        }
        public void Receive()
        {
            try
            {
                //Byte[] bytesReceived = new Byte[256];
                string data = string.Empty;
                int bytes = 0;

                if (sck.Connected)
                {
                    sck.ReceiveTimeout = 3000;

                    bytes = sck.Receive(bytesReceived, bytesReceived.Length, 0);
                    data = data + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
                    Args.Data = data;
                    Trigger(this, Args);
                }
            }
            catch (SocketException se)
            {
                System.Console.WriteLine(se.Message);
            }
            catch (NullReferenceException)
            {
            }
        }
        //public void Receive()
        //{
        //    try
        //    {
        //        sck.ReceiveTimeout = 2000;

        //        StateObject state = new StateObject();

        //        state.workSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


        //        //sck.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
        //        //    ReceiveCallBack, state);
        //        //receiveDone.WaitOne();
        //    }
        //    catch (Exception e)
        //    {
        //        System.Console.WriteLine(e.ToString());
        //    }
        //}
        private void ReceiveCallBack(IAsyncResult ar) {
           
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                
                int bytesRead = sck.EndReceive(ar);

                if (bytesRead > 0)
                {
                    //state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    //Args.Data = state.sb.ToString();
                    //Trigger(this, Args);
                    //sck.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    //   ReceiveCallBack, state);
                    receiveDone.Set();
                }
                else
                {
                    if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                    }
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }
        }
        public void Disconnect()
        {
            if (this.sck.Connected)
            {
                this.sck.Shutdown(SocketShutdown.Both);
                this.sck.Close();
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (sck != null)
                        sck.Close();
                }
                disposed = true;
            }
        }
    }
}