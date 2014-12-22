﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Data;
using System.Threading;
namespace VisorRemoting.V1
{
    public class Remoting
    {
        private static ManualResetEvent connectDone    = new ManualResetEvent(false);
        private static ManualResetEvent sendDone       = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone    = new ManualResetEvent(false);
        private static ManualResetEvent disconnectDone = new ManualResetEvent(false);
        private static string response = string.Empty;     

        public static void Start()
        {
            ObjectState obj = new ObjectState();
            obj.ID = "125";
            obj.workSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            obj.workSocket.Bind(new IPEndPoint(IPAddress.Parse("105.1.4.222"), 11000 + Convert.ToInt32(obj.ID)));
            StartConnect(obj);
        }
        public static void Send() {           
     
        }
        private static void StartConnect(ObjectState obj)
        {
            try
            {
                    Connect(obj);
                    Send("(" + obj.ID + "999RE", obj.workSocket);
                    sendDone.WaitOne();
                    Receive(obj.workSocket);
                 
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            finally
            {
            }
        }
        #region Connected
        private static void Connect(ObjectState obj)
        {
            try
            {
                obj.workSocket.BeginConnect("105.1.0." +
                Convert.ToInt32(obj.ID), 10000, new AsyncCallback(ConnectCallBack), obj.workSocket);
                connectDone.WaitOne(TimeSpan.FromSeconds(1));
            }
            catch (Exception e) {

                System.Console.WriteLine(e.Message);
            }
        }
        private static void ConnectCallBack(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
                System.Console.WriteLine("Socket connected to {0}",
                client.RemoteEndPoint.ToString());
                connectDone.Set();
            }
            catch (Exception exp)
            {
                System.Console.WriteLine(exp.Message);
            }
        }
        #endregion

        #region Send
        private static void Send(string data, Socket s)
        {
            try
            {
                if (s.Connected)
                {
                    data += CalculaCheckSum(data) + Convert.ToChar(13);
                    byte[] byteData = Encoding.ASCII.GetBytes(data.ToCharArray());
                    s.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), s);
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
                sendDone.Set();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region Received
        private static void Receive(Socket client)
        {
            try
            {
                ObjectState state = new ObjectState();
                state.workSocket = client;
                client.BeginReceive(state.buffer, 0, state.buffer.Length,SocketFlags.Peek,
                new AsyncCallback(ReceiveCallback),
                state);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                ObjectState state = (ObjectState)ar.AsyncState;
                Socket client = state.workSocket;
                

                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    //state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    ////System.Console.WriteLine(state.sb.ToString());
                    //Process(state.ToString(), state);
                    //client.BeginReceive(state.buffer, 0, ObjectState.BufferSize, 0,
                    //new AsyncCallback(ReceiveCallback), state);
                    state.data += Encoding.ASCII.GetString(state.buffer, 0, bytesRead);
                    Process(state);
                    client.BeginReceive(state.buffer, 0, ObjectState.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                        receiveDone.Set();
                    }
                   
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region CheckSum
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
        #endregion
        private static void Process(ObjectState o)
        {
            try
            {
                string Ack = string.Empty;

                while (o.data.Substring(0, 4) == "(999" && o.data.Substring(7, 2) == "AK" && o.data.ToString()[13] == Convert.ToChar(13))
                {
                    o.data = o.data.Substring(14);
                }
                if (o.data.Substring(0, 4) == "(999" && o.data.ToString().Substring(10, 2) == "RE" && o.data.ToString()[32] == Convert.ToChar(13))
                {
                    if (CheckSum(o.data))
                    {
                        //System.Console.WriteLine(o.data);
                        Read(o.data);
                        Ack = "(" + o.data.Substring(4, 3) + "999AK" + o.data.Substring(30, 2);
                        Ack = Ack + CalculaCheckSum(Ack) + Convert.ToChar(13);
                        Send(Ack, o.workSocket);
                    }
                }
            }
            catch (Exception e)
            {
                //System.Console.WriteLine(e.Message);
            }
        }

        private static void Read(string data)
        {
            long est;
            bool aux;

            if (data[0] == '(' && data[32] == Convert.ToChar(13))
            {
                System.Console.WriteLine("ID: {0}", data.Substring(4, 3));
                System.Console.WriteLine("ANGULO ACTUAL: {0}", Convert.ToInt32(data.Substring(12, 3)));
                System.Console.WriteLine("TENSION: {0}", Convert.ToInt32(data.Substring(15, 3)));
                System.Console.WriteLine("PRESION: {0}", Convert.ToInt32(data.Substring(18, 3)));
                System.Console.WriteLine("APLICACION: {0}", Convert.ToInt32(data.Substring(21, 3)));
                est = long.Parse(data.Substring(23, 6), System.Globalization.NumberStyles.HexNumber);
                System.Console.WriteLine("SENTIDO: {0}", Convert.ToBoolean(est & 0x80000));
                System.Console.WriteLine("HABILITADO: {0}", Convert.ToBoolean(est & 0x40000));
                aux = Convert.ToBoolean(est & 0x20000);
                System.Console.WriteLine("CAMINANDO: {0}", Convert.ToBoolean(est & 0x20000));
                System.Console.WriteLine("ESPERANDO PRESION: {0}", Convert.ToBoolean(est & 0x10000));
                System.Console.WriteLine("PRESION NOR: {0}", Convert.ToBoolean(est & 0x200));
                System.Console.WriteLine("FALLA ELECTRICA: {0}", Convert.ToBoolean(est & 0x80));
                System.Console.WriteLine("ALARMA SEG: {0}", Convert.ToBoolean(est & 0x40));
            }
        }
    }
}