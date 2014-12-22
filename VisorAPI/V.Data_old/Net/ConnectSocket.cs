using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace V.Data.Net
{
    class ConnectSocket
    {
        public ConnectSocket(Socket socket){
            this.socket = socket;
        }
        private Socket socket = null;
        public void Process() {
            //socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //socket.BeginConnect(
        }
    }
}
