using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VisorRemoting.V3
{
    public class Panel : StateObject, Core.EngineGraphics_1_1.IPaintable
    {
        public Panel(string id)
        {
            this.Id = id;
        }
        public Panel()
        {
            Connected();
        }
        public Panel(string id, ConfigConnection config)
        {
            this.Id = id;
            this.Configuracion = config;
        }
        public Panel(ConfigConnection config)
        {
        }

        private string commandQuery = string.Empty;

        public string Id { get; set; }
        public string Nombre { get; set; }
        public int Angulo { get; set; }
        public int Tension { get; set; }
        public int Presion { get; set; }
        public int Aplicacion { get; set; }
        public bool Sentido { get; set; }
        public bool Habilitado { get; set; }
        public bool Seco { get; set; }
        public bool Caminando { get; set; }
        public bool EsperandoPresion { get; set; }
        public bool PresionNor { get; set; }
        public bool FallaElectrica { get; set; }
        public bool AlarmaDeSeguridad { get; set; }

        public bool FullResponse = false;

        public void Read()
        {
            try
            {
                string data = sb.ToString();
                string Ack = string.Empty;
                while (data.Substring(0, 4) == "(999" && data.Substring(7, 2) == "AK" && data.ToString()[13] == Convert.ToChar(13))
                {
                    data = data.Substring(14);
                }
                if (data.Substring(0, 4) == "(999" && data.ToString().Substring(10, 2) == "RE" && data.ToString()[32] == Convert.ToChar(13))
                {
                    if (CheckSum(data))
                    {
                        ProcessData(data);
                        Ack = "(" + data.Substring(4, 3) + "999AK" + data.Substring(30, 2);
                        Ack = Ack + CalculaCheckSum(Ack) + Convert.ToChar(13);

                    }
                }
            }
            catch (Exception e)
            {

            }
        }
        private void ProcessData(string data)
        {

            long est;
            bool aux;

            if (data[0] == '(' && data[32] == Convert.ToChar(13))
            {
                //.....................................................................................
                this.Id = data.Substring(4, 3);
                this.Angulo = Convert.ToInt32(data.Substring(12, 3));
                this.Tension = Convert.ToInt32(data.Substring(15, 3));
                this.Presion = Convert.ToInt32(data.Substring(18, 3));
                this.Aplicacion = Convert.ToInt32(data.Substring(21, 3));
                est = long.Parse(data.Substring(23, 6), System.Globalization.NumberStyles.HexNumber);
                this.Sentido = Convert.ToBoolean(est & 0x80000);
                this.Habilitado = Convert.ToBoolean(est & 0x40000);
                aux = Convert.ToBoolean(est & 0x20000);
                this.Caminando = Convert.ToBoolean(est & 0x20000);
                this.EsperandoPresion = Convert.ToBoolean(est & 0x10000);
                this.PresionNor = Convert.ToBoolean(est & 0x200);
                this.Seco = Convert.ToBoolean(est & 0x1000);
                this.FallaElectrica = Convert.ToBoolean(est & 0x80);
                this.AlarmaDeSeguridad = Convert.ToBoolean(est & 0x40);
                FullResponse = true;
            }
        }
        public ConfigConnection Configuracion { get; set; }
        //public ValleyCommandType ValleyCommand
        //{
        //    set
        //    {
        //        switch (value)
        //        {
        //            case ValleyCommandType.Query:
        //                CommandQuery = "(" + id + "999RE";
        //                break;
        //            case ValleyCommandType.Marcha:
        //                CommandQuery = "(" + id + "999POW;RE";
        //                break;
        //            case ValleyCommandType.Parada:
        //                break;
        //            case ValleyCommandType.Foward:
        //                CommandQuery = "(" + id + "999SDF;RE";
        //                break;
        //            case ValleyCommandType.Reversa:
        //                CommandQuery = "(" + id + "999SDR;RE";
        //                break;
        //            case ValleyCommandType.Aplicacion:
        //                break;
        //        }
        //    }
        //}
        private string CalculaCheckSum(string trama)
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
        private bool CheckSum(string trama)
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
        public void Connected()
        {
            ConfigConnection config = new ConfigConnection();
            config.localHost = "105.1.4.222";
            config.LocalPort = 11000;
            config.RemoteHost = "105.1.0.125";
            config.RemotePort = 10000;
            RemotingConnection conn = new RemotingConnection(config);
            conn.StartClient(this);
        }
        public void Drawer(System.Drawing.Graphics g)
        {
            try
            {
                g.FillRectangle(System.Drawing.Brushes.LightGreen,
                    new System.Drawing.Rectangle(0, 0, 200, 200));

                Font f = new Font("", 5, FontStyle.Bold);

                g.DrawString(this.Id, f, Brushes.Green, 30, 20,
                   new StringFormat(StringFormatFlags.FitBlackBox));

                g.DrawString(this.Angulo.ToString(), f, Brushes.Green, 30, 50,
                  new StringFormat(StringFormatFlags.FitBlackBox));

                g.DrawString(this.Aplicacion.ToString(), f, Brushes.Green, 30, 80,
                  new StringFormat(StringFormatFlags.FitBlackBox));

                g.DrawString(this.Tension.ToString(), f, Brushes.Green, 30, 100,
                  new StringFormat(StringFormatFlags.FitBlackBox));
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
        public void Move(int x, int y)
        {

        }
        public void Close()
        {
            this.workSocket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
            this.workSocket.Close();
        }
    }
}
