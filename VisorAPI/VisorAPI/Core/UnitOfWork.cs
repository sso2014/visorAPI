using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisorAPI.Core.VO;

namespace VisorAPI.Core
{
    public class EquipoUnitOfWork : IUnitOfWork
    {        
        public EquipoUnitOfWork() {
            equipos = new List<Equipo>();
        }
        private List<Equipo> equipos = null;
        private BUS.UserBus Setdb = new BUS.UserBus();        
        public void registerNew(object obj)
        {
           
        }
        public void registerDirty(object obj)
        {
            equipos.Add(obj as Equipo);
        }
        public void registerClean(object obj)
        {
            //this.equipos.Clear();
        }
        public void registerDeleted(object obj)
        {
            Equipo eq = obj as Equipo;
            eq.Dispose();
        }
        public void commit() {
            foreach (Equipo eq in equipos) {
                Setdb.InsertEquipo(eq);
            }
            equipos.Clear();
        }
        public void rollback()
        {
            throw new NotImplementedException();
        }
    }
}
