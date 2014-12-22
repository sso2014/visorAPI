using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisorRemoting.V8
{
    public interface IDisplay
    {
        DateTime FechaUpdate { get; set; }
        string Id { get; set; }
        int Angulo { get; set; }
        int Tension { get; set; }
        int Presion { get; set; }
        int Aplicacion { get; set; }
        bool Sentido { get; set; }
        bool Habilitado { get; set; }
        bool Caminando { get; set; }
        bool EsperandoPresion { get; set; }
        bool PresionNor { get; set; }
        bool Seco { get; set; }
        bool FallaElectrica { get; set; }
        bool AlarmaDeSeguridad { get; set; }
    }
}
