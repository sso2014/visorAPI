using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using V.Core.Data;


using System.Net.Sockets;

namespace V.Net
{
    public class Manager
    {
        public Manager(EquipoRepository repository)
        {
            this.repository = repository;
        }
        private EquipoRepository repository = null;
        public void Start()
        {
            //Equipo equipo = repository.GetEquipoByID("P_222");
            //Socket sck = equipo.Remote.workSocket;
            //if (!sck.Connected)
            //{
            //    //remoteList[count].workSocket.Shutdown(SocketShutdown.Both);
            //    //remoteList[count].workSocket.Close();
            //    sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);                
            //    //remoteList[count].workSocket.BeginConnect(
            //    //remoteList[count].endPoint, new AsyncCallback(ConnectCallBack),
            //    //remoteList[count].workSocket);
            //    //connectDone.WaitOne();
            //}
            //do
            //{
            //} while (true);
        }
    }
}