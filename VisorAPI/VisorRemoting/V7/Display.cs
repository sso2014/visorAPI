using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisorRemoting.V7
{
    public class Display
    {
        public DateTime FechaUpdate { get; set; }
        public string Id { get; set; }
        public int Angulo { get; set; }
        public int Tension { get; set; }
        public int Presion { get; set; }
        public int Aplicacion { get; set; }
        public bool Sentido { get; set; }
        public bool Habilitado { get; set; }
        public bool Caminando { get; set; }
        public bool EsperandoPresion { get; set; }
        public bool PresionNor { get; set; }
        public bool Seco { get; set; }
        public bool FallaElectrica { get; set; }
        public bool AlarmaDeSeguridad { get; set; }
    }
}
