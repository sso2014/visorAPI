using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisorRemoting.V5
{
    public abstract class RemotingProtocol
    {
        protected string Ack = string.Empty;
        protected string Response = string.Empty;

        protected void Read(string data)
        {
            try
            {
                string Ack = string.Empty;
                
                while (data.Substring(0, 4) == "(999" && data.Substring(7, 2) == "AK" && data.ToString()[13] == Convert.ToChar(13))
                {
                    data = data.Substring(14);
                }
                if (data.Substring(0, 4) == "(999" && data.ToString().Substring(10, 2) == "RE" && data.ToString()[32] == Convert.ToChar(13))
                {
                    if (CheckSum(data))
                    {
                        Process(data);
                        Ack = "(" + data.Substring(4, 3) + "999AK" + data.Substring(30, 2);
                        Ack = Ack + CalculaCheckSum(Ack) + Convert.ToChar(13);
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
        protected void Process(string data)
        {

            long est;
            bool aux;

            if (data[0] == '(' && data[32] == Convert.ToChar(13))
            {
                //.....................................................................................
                //this.Id = data.Substring(4, 3);
                //this.Angulo = Convert.ToInt32(data.Substring(12, 3));
                //this.Tension = Convert.ToInt32(data.Substring(15, 3));
                //this.Presion = Convert.ToInt32(data.Substring(18, 3));
                //this.Aplicacion = Convert.ToInt32(data.Substring(21, 3));
                //est = long.Parse(data.Substring(23, 6), System.Globalization.NumberStyles.HexNumber);
                //this.Sentido = Convert.ToBoolean(est & 0x80000);
                //this.Habilitado = Convert.ToBoolean(est & 0x40000);
                //aux = Convert.ToBoolean(est & 0x20000);
                //this.Caminando = Convert.ToBoolean(est & 0x20000);
                //this.EsperandoPresion = Convert.ToBoolean(est & 0x10000);
                //this.PresionNor = Convert.ToBoolean(est & 0x200);
                //this.Seco = Convert.ToBoolean(est & 0x1000);
                //this.FallaElectrica = Convert.ToBoolean(est & 0x80);
                //this.AlarmaDeSeguridad = Convert.ToBoolean(est & 0x40);
                //FullResponse = true;
                Response = data;
            }
        }
        protected string CalculaCheckSum(string trama)
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
        protected bool CheckSum(string trama)
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
