using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace VisorAPI.Core.VO
{
    public class EquipoCollection:IEnumerable
    {
        public EquipoCollection() { 
            using (BUS.UserBus bus = new BUS.UserBus()){
                equipos = bus.equipos();
            }
        }
        private List<Equipo> equipos = null;
        public IEnumerator GetEnumerator()
        {
            foreach (Equipo equipo in equipos) {
                yield return equipo;
            }
        }
    }
}
