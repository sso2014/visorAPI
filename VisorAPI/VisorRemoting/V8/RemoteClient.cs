using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace VisorRemoting.V8
{
    public class RemoteClient
    {
        public RemoteClient(string id) {
            this.ID = id;
            this.workSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.endPoint = new IPEndPoint(IPAddress.Parse("105.1.0." + Convert.ToInt32(this.ID)), 10000);
            this.LocalPoint = new IPEndPoint(IPAddress.Any, 11000 + Convert.ToInt32(this.ID));
            this.workSocket.Bind(LocalPoint);
        }
        public string ID { get; set; }
        public Socket workSocket = null;
        public const int BufferSize = 256;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
        public string Query = string.Empty;
        public IPEndPoint endPoint = null;
        public IPEndPoint LocalPoint = null;
    }
}
