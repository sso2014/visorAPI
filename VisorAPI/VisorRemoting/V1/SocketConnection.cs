using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace VisorRemoting.V1
{
    public class SocketConnection
    {
        public SocketConnection()
        {
            CreateSocket();
        }
        private Socket s = null;
        private ManualResetEvent connectDone = 
            new ManualResetEvent(false);
        private IPEndPoint endPoint;
        private bool isConnectionSucessFull = false;
        object obj = new object();

        public void On()
        {
            Connect();
        }
        private void CreateSocket()
        {
            endPoint =
            new IPEndPoint(IPAddress.Parse("105.1.0.125"), 10000);
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Bind(new IPEndPoint(IPAddress.Parse("105.1.4.222"), 11000));
        }
        private void Connect()
        {
            try
            {

                s.BeginConnect(endPoint, new AsyncCallback(ConnectCallBack), s);
                connectDone.WaitOne(2000);

            }
            catch (InvalidOperationException e) { 
            
            }
        }
        private void Send() {

            try
            {
                byte[] byteSend = Encoding.ASCII.GetBytes("test");
                s.SendTimeout = 3000;
                s.Send(byteSend);
            }
            catch (SocketException se) {

              
            }
        }
        private void ConnectCallBack(IAsyncResult iar)
        {
            Socket s = iar.AsyncState as Socket;

            if (s.Connected)
            {
                s.EndConnect(iar);
                isConnectionSucessFull = true;
                connectDone.Set();
            }
            else
            {
                isConnectionSucessFull = false;
            }
        }
    }
}