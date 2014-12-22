using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VisorAPI.Core.VO
{
    public class Equipo:IDisposable
    {
        public string EquipoID { get; set; }
        public string EquipoNombre { get; set; }
        public int Tramos { get; set; }
        public int PosicionInicio { get; set; }
        public int PosicionFin { get; set; }
        public int Angulo { get; set; }
        public int Tension { get; set; }
        public int Presion { get; set; }
        public int Aplicacion { get; set; }
        public bool Sentido { get; set; }
        public bool Habilitado { get; set; }
        public bool Caminando { get; set; }
        public bool EsperandoPresion { get; set; }
        public bool PresionNor { get; set; }
        public bool FallaElectrica { get; set; }
        public bool AlarmaDeSeguridad { get; set; }
        public override string ToString()
        {
            return EquipoNombre;
        }
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}