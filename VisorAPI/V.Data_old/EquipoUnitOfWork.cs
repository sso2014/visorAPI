using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using V.Core.Data;
using V.Data.BUS;

namespace V.Data
{
    class EquipoUnitOfWork : IUnitOfWork, IDisposable
    {
        public EquipoUnitOfWork(userBUS db) {
            this.db = db;
        }

        private userBUS db = null;
        List<Equipo> equipos = new List<Equipo>();

        public void MarkDirty(object entity)
        {
            Equipo equipo = (Equipo)entity;
            equipos.Add(equipo);
        }
        public void MarkNew(object entity)
        {
            throw new NotImplementedException();
        }
        public void MarkDeleted(object entity)
        {
            throw new NotImplementedException();
        }
        public void Commit()
        {
            if (equipos != null)
            {              
                foreach (Equipo eq in equipos)
                {
                    db.InsertEquipo(eq);
                }
                equipos = null;
            }
        }
        public void Rollback()
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public void MarkUpdate(object entity)
        {
            Equipo equipo = entity as Equipo;
            db.UpdateEquipo(equipo);
        }
    }
}