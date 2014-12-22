using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Core.BUS;

namespace VisorRemoting.V8
{
    public class RemoteManager3
    {
        private static ManualResetEvent connectDone = 
                new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
                new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
                new ManualResetEvent(false);
        private static ManualResetEvent disconnectDone =
                new ManualResetEvent(false);   
       
        public static void RemoteAccess(IRemote remote)
        {     
            if (!remote.workSocket.Connected)
            {

                        remote.workSocket.Close();
                        remote.workSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        remote.workSocket.Bind(remote.LocalPoint);
                        remote.workSocket.BeginConnect(
                        remote.endPoint, new AsyncCallback(ConnectCallBack),
                        remote.workSocket);
                        connectDone.WaitOne(1000);
                    } else {
                        Send(remote.workSocket, remote.Query);
                        connectDone.WaitOne();
                        System.Threading.Thread.Sleep(3000);
                        Receive(remote.workSocket, remote);
                        receiveDone.WaitOne();  
            }     
        }
        public static void Start()
        {
            CreateLists();
            int count = 0;
            while (true)
            {
                //.................................................................
                if (!remoteList[count].workSocket.Connected)
                {
                    //remoteList[count].workSocket.Shutdown(SocketShutdown.Both);
                    remoteList[count].workSocket.Close();
                    remoteList[count].workSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    remoteList[count].workSocket.Bind(remoteList[count].LocalPoint);
                    remoteList[count].workSocket.BeginConnect(
                    remoteList[count].endPoint, new AsyncCallback(ConnectCallBack),
                    remoteList[count].workSocket);
                    connectDone.WaitOne();
                }
                Send(remoteList[count].workSocket, remoteList[count].Query);
                connectDone.WaitOne();
                System.Threading.Thread.Sleep(3000);
                Receive(remoteList[count].workSocket, remoteList[count]);
                receiveDone.WaitOne();
            }
        }
        private static void Send(Socket sck, string query) {
            try
            {
                query += CalculaCheckSum(query) + Convert.ToChar(13);
                byte[] sendBytes = Encoding.ASCII.GetBytes(query);
                sck.BeginSend(sendBytes, 0, sendBytes.Length, 0, new AsyncCallback(SendCallBack), sck);
            }
            catch (SocketException se) { 
            }
        }
        private static void ConnectCallBack(IAsyncResult ar)
        {
            Socket s = ar.AsyncState as Socket;
            if (s.Connected){
                s.EndConnect(ar);
                connectDone.Set();
            }
        }
        private static void SendCallBack (IAsyncResult ar) {
          Socket client = (Socket)ar.AsyncState;
          int bytesSent = client.EndSend(ar);
          sendDone.Set();
        }
        private static void Receive(Socket sck, RemoteClient remote) {
            try {
                sck.BeginReceive(remote.buffer, 0, RemoteClient.BufferSize, 0,
                    new AsyncCallback(ReceiveCallBack), remote);}
            catch (Exception e) {
                //System.Console.WriteLine(e.Message);
            }
        }
        private static void ReceiveCallBack(IAsyncResult ar){

            RemoteClient remote = ar.AsyncState as RemoteClient;
            Socket sck   = remote.workSocket;
            string Ack   = string.Empty;
            string data  = string.Empty;
            long est=0;
            bool aux;

            try
            {
                int bytesRead = sck.EndReceive(ar);
                if (bytesRead > 0){
                    data = Encoding.ASCII.GetString(remote.buffer, 0, RemoteClient.BufferSize);
                    while (data.Substring(0, 4) == "(999" && data.Substring(7, 2) == "AK" && data.ToString()[13] == Convert.ToChar(13)){
                        data = data.Substring(14);
                        if (data.Substring(0, 4) == "(999" && data.ToString().Substring(10, 2) == "RE" && data.ToString()[32] == Convert.ToChar(13)){
                            if (CheckSum(data)){
                                System.Console.WriteLine(data);
                                Ack = "(" + data.Substring(4, 3) + "999AK" + data.Substring(30, 2);
                                Ack = Ack + CalculaCheckSum(Ack) + Convert.ToChar(13);
                                byte[] byteSend = Encoding.ASCII.GetBytes(Ack);
                                if (data[0] == '(' && data[32] == Convert.ToChar(13)){
                                    est = long.Parse(data.Substring(23, 6), System.Globalization.NumberStyles.HexNumber);
                                    aux = Convert.ToBoolean(est & 0x20000);
                                
                                        if (p.Id.Substring(2, 3) == data.Substring(4, 3)) {
                                            p.Angulo = Convert.ToInt32(data.Substring(12, 3));
                                            p.Tension = Convert.ToInt32(data.Substring(15, 3));
                                            p.Presion = Convert.ToInt32(data.Substring(18, 3));
                                            p.Aplicacion = Convert.ToInt32(data.Substring(21, 3));
                                            p.Sentido = Convert.ToBoolean(est & 0x80000);
                                            p.Habilitado = Convert.ToBoolean(est & 0x40000);
                                            p.Caminando = Convert.ToBoolean(est & 0x20000);
                                            p.EsperandoPresion = Convert.ToBoolean(est & 0x10000);
                                            p.PresionNor = Convert.ToBoolean(est & 0x200);
                                            p.FallaElectrica = Convert.ToBoolean(est & 0x80);
                                            p.AlarmaDeSeguridad = Convert.ToBoolean(est & 0x40);
                                        } 
                                    }   
                                    receiveDone.Set();
                                    sck.BeginSend(byteSend, 0, byteSend.Length, 0,
                                    new AsyncCallback(ReceiveCallBack), remote);                                      
                                }
                            }
                        }
                    }
                }
            }
            catch (ArgumentOutOfRangeException aoe){
                System.Console.WriteLine(aoe.Message);}
            catch (IndexOutOfRangeException ore){
                System.Console.WriteLine(ore.Message);}
            catch (SocketException se){}
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
    }
}