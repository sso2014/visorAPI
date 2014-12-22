using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisorAPI.Core.VO
{
    public class Superficie
    {
        public int AnguloOff { get; set; }
        public string Sentido { get; set; }
        public int INICIO { get; set; }
        public int FIN { get; set; }
        public int HAS { get; set; }
        public string ID { get; set; }
        public string NOMBRE { get; set; }
        public float ANGULO { get; set; }
        public string Cultivo { get; set; }
        public string Campo { get; set; }      
    }
}
