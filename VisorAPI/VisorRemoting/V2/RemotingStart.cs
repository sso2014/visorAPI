using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;


namespace VisorRemoting.V2
{
    public class RemotingStart
    {
        public RemotingStart(RemotingConnection remotingConnection)
        {
            //this.Interval = 1000;
            //remotingList.Add(remotingConnection);
            //remotingList[0].RemotingEvent+=new RemotingConnection.RemotingEventHandler(remoto_RemotingEvent);
            //remotingList[0].Connect();
        }
        public RemotingStart()
        {
            remotingList = new List<RemotingConnection>();
        }
        public void Add(RemotingConnection remotingConnection)
        {
            remotingList.Add(remotingConnection);
        }
        private List<RemotingConnection> remotingList;
        private RemotingObjectVisorEventArgs Args = new RemotingObjectVisorEventArgs();
        public delegate void ObjectVisorEventHandler(object sender, RemotingObjectVisorEventArgs e);
        public event ObjectVisorEventHandler ObjectVisorEvent;
        private RemotingConfig RemotingConfig = new RemotingConfig();

        private void Trigger(object sender, RemotingObjectVisorEventArgs e)
        {
            if (ObjectVisorEvent != null)
            {
                ObjectVisorEvent(this, e);
            }
        }
        public int Interval { get; set; }
        public void Pooling()
        {
            //init.......
            ConnectList();

            do
            {
                foreach (RemotingConnection conn in remotingList)
                {
                    if (conn.State)
                    {
                        Args.RemoteConfig = conn.RemoteConfig;
                        conn.SendQuery();
                        System.Threading.Thread.Sleep(3000);
                        conn.Receive();
                    }
                }
            }
            while (true);
        }
        private void ConnectList()
        {
            try
            {

                foreach (RemotingConnection conn in remotingList)
                {
                    conn.Connect();
                    conn.RemotingEvent += new RemotingConnection.RemotingEventHandler(conn_RemotingEvent);
                }
            }
            catch (SocketException se) { 
            }
        }
        void conn_RemotingEvent(object sender, RemotingEventArgs e)
        {
            string Ack = string.Empty;
            System.Console.WriteLine(e.Data);

            try
            {
                while (e.Data.Substring(0, 4) == "(999" && e.Data.Substring(7, 2) == "AK" && e.Data.ToString()[13] == Convert.ToChar(13))
                {
                    e.Data = e.Data.Substring(14);
                }
                if (e.Data.Substring(0, 4) == "(999" && e.Data.ToString().Substring(10, 2) == "RE" && e.Data.ToString()[32] == Convert.ToChar(13))
                {
                    if (CheckSum(e.Data))
                    {
                        Read(e.Data);
                        Ack = "(" + e.Data.Substring(4, 3) + "999AK" + e.Data.Substring(30, 2);
                        Ack = Ack + CalculaCheckSum(Ack) + Convert.ToChar(13);
                        byte[] bytesAck = Encoding.ASCII.GetBytes(Ack);
                        e.WorkingSoket.Send(bytesAck);
                    }
                }
            }
            catch (ArgumentOutOfRangeException aore)
            {
            }
            catch (IndexOutOfRangeException ioore)
            {
            }
        }
        string CalculaCheckSum(string trama)
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
        bool CheckSum(string trama)
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
        void Read(string data)
        {
            long est;
            bool aux;
            try
            {

                if (data[0] == '(' && data[32] == Convert.ToChar(13))
                {

                    //System.Console.WriteLine("ID: {0}", data.Substring(4, 3));
                    //System.Console.WriteLine("ANGULO ACTUAL: {0}", Convert.ToInt32(data.Substring(12, 3)));
                    //System.Console.WriteLine("TENSION: {0}", Convert.ToInt32(data.Substring(15, 3)));
                    //System.Console.WriteLine("PRESION: {0}", Convert.ToInt32(data.Substring(18, 3)));
                    //System.Console.WriteLine("APLICACION: {0}", Convert.ToInt32(data.Substring(21, 3)));

                    est = long.Parse(data.Substring(23, 6), System.Globalization.NumberStyles.HexNumber);

                    //System.Console.WriteLine("SENTIDO: {0}", Convert.ToBoolean(est & 0x80000));
                    //System.Console.WriteLine("HABILITADO: {0}", Convert.ToBoolean(est & 0x40000));

                    aux = Convert.ToBoolean(est & 0x20000);

                    //System.Console.WriteLine("CAMINANDO: {0}", Convert.ToBoolean(est & 0x20000));
                    //System.Console.WriteLine("ESPERANDO PRESION: {0}", Convert.ToBoolean(est & 0x10000));
                    //System.Console.WriteLine("PRESION NOR: {0}", Convert.ToBoolean(est & 0x200));
                    //System.Console.WriteLine("FALLA ELECTRICA: {0}", Convert.ToBoolean(est & 0x80));
                    //System.Console.WriteLine("ALARMA SEG: {0}", Convert.ToBoolean(est & 0x40));

                    Args.Panel.Id = data.Substring(4, 3);
                    Args.Panel.Angulo = Convert.ToInt32(data.Substring(12, 3));
                    Args.Panel.Tension = Convert.ToInt32(data.Substring(15, 3));
                    Args.Panel.Presion = Convert.ToInt32(data.Substring(18, 3));
                    Args.Panel.Aplicacion = Convert.ToInt32(data.Substring(21, 3));
                    Args.Panel.Sentido = Convert.ToBoolean(est & 0x80000);
                    Args.Panel.Habilitado = Convert.ToBoolean(est & 0x40000);
                    Args.Panel.Caminando = Convert.ToBoolean(est & 0x20000);
                    Args.Panel.EsperandoPresion = Convert.ToBoolean(est & 0x10000);
                    Args.Panel.PresionNor = Convert.ToBoolean(est & 0x200);
                    Args.Panel.FallaElectrica = Convert.ToBoolean(est & 0x80);
                    Args.Panel.AlarmaDeSeguridad = Convert.ToBoolean(est & 0x40);
                    Trigger(this, Args);
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
        void Send(Socket sck, string data)
        {
            try
            {
                if (sck.Connected)
                {
                    data += CalculaCheckSum(data) + Convert.ToChar(13);
                    byte[] byteData = Encoding.ASCII.GetBytes(data);
                    sck.Send(byteData);
                }
            }
            catch (SocketException e)
            {

            }
        }
        public void Close()
        {
            foreach (RemotingConnection rc in remotingList) {
                rc.Disconnect();
                rc.Dispose();
            }
        }
    }
}
