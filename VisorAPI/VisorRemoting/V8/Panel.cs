using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisorRemoting.V8
{
    public class Panel:IPanel
    {
        public Panel() {
            display = new Display();
        }
        public Panel(string id) {
            this.Id = id;
            display = new Display();
            Access = new RemoteClient(id.Substring(2,3));
            Access.Query = "(" +id.Substring(2, 3)+ "999RE"; 
        }
        public void UpdateDisplay() {

            display.FechaUpdate = DateTime.Now;
            display.Id = this.Id;
            display.Angulo = this.Angulo;
            display.Tension = this.Tension;
            display.Presion = this.Presion;
            display.Aplicacion = this.Aplicacion;
            display.Sentido = this.Sentido;
            display.Habilitado = this.Habilitado;
            display.Caminando = this.Caminando;
            display.EsperandoPresion = this.EsperandoPresion;
            display.PresionNor = this.PresionNor;
            display.Seco = this.Seco;
            display.FallaElectrica = this.FallaElectrica;
            display.AlarmaDeSeguridad = this.AlarmaDeSeguridad;            
        }
        private Display display;
        public RemoteClient GetAccess() {
            return Access;
        }
        public RemoteClient Access = null;
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

        public void Marcha()
        {
            throw new NotImplementedException();
        }
        public void Parada()
        {
            throw new NotImplementedException();
        }
        public void Foward()
        {
            throw new NotImplementedException();
        }
        public void Reversa()
        {
            throw new NotImplementedException();
        }
        public IDisplay Display
        {
            get
            {
                return (IDisplay)this.display;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public void AddListener(IPanelController Control)
        {
            throw new NotImplementedException();
        }
    }
}
