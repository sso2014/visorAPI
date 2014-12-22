using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Xml;

namespace VisorRemoting.V1
{
    public class Remoting4
    {
        public static void Start()
        {
            
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Bind(new IPEndPoint(IPAddress.Parse("105.1.4.222"), 11000));
            SocketAsyncEventArgs sck = new SocketAsyncEventArgs();
            sck.RemoteEndPoint = new IPEndPoint(IPAddress.Parse("105.1.0.125"), 10000);
            s.ConnectAsync(sck);
            
        }
        public void Connect(SocketAsyncEventArgs e) {

            Socket sck = e.ConnectSocket;
            if (sck.Connected)
            {
                System.Console.WriteLine("Conectado");
            }
            
        }
        public void ResetBuffer(SocketAsyncEventArgs e) { 
        }
        public void SocketReceive(Object sender, SocketAsyncEventArgs e) { 
        }
        public void ProcessData(Byte[] data, Int32 count) { 
        }
    }
}