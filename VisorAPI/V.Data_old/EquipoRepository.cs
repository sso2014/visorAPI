using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using V.Core.Data;

namespace V.Data
{
    public class EquipoRepository : IEquipoRepository, IDisposable
    {
        public EquipoRepository() {
            db = new BUS.userBUS();
            uow = new  EquipoUnitOfWork(db);
        }
        private BUS.userBUS db = null;
        private bool disposed;
        private IUnitOfWork uow = null;
        //public EquipoRepository()
        //{
        //    this.db = new BUS.userBUS();
        //}
        //public void Update(Equipo obj)
        //{
        //    //db.Entry(obj);
        //}
        //public void DeleteEquipo(string id)
        //{
        //    Equipo existing = db.GetEquipos().Find(delegate(Equipo eq)
        //    {
        //        return eq.Id == id;
        //    });
        //    db.GetEquipos().Remove(existing);
        //}
        ////IEnumerable<Equipo> IEquipoRepository.SelectAll()
        ////{
        ////    return db.GetEquipos().ToList();
        ////}
        //Equipo IEquipoRepository.GetEquipoByID(string id)
        //{
        //    Equipo rest = db.GetEquipos().Find(eq => eq.Id == id);
        //    return rest;
        //}
        //void IEquipoRepository.InsertEquipo(Equipo obj)
        //{
        //    db.GetEquipos().Add(obj);
        //}
        //void IEquipoRepository.UpdateEquipo(Equipo obj)
        //{
        //    //db.Entry(obj);
        //}
        //void IEquipoRepository.Save()
        //{
        //    //db.SaveChange();
        //}
        ////IList<Equipo> IEquipoRepository.FindAll()
        ////{

        ////    IList<Equipo> equipos = db.GetEquipos();
        ////    return equipos;

        ////}
        //IList<Equipo> IEquipoRepository.SelectEquipoAll()
        //{
        //    IList<Equipo> equipos = db.GetEquipos().ToList();
        //    return equipos;
        //}
        public void Dispose(){
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    
                }
            }
            disposed = true;
        }
        public IList<Equipo> SelectEquipoAll() {
            IList<Equipo>equipos = db.GetEquipos().ToList();
            return equipos;
        }
        public Equipo GetEquipoByID(string id) {
            return db.GetEquipos().Find(eq => eq.Numero == id);
        }
        public void InsertEquipo(Equipo equipo){
            if (equipo != null) {
                uow.MarkDirty(equipo);
            }
        }
        public void UpdateEquipo(Equipo equipo){
            if (equipo != null) {
                uow.MarkUpdate(equipo);
            }
        }
        public void DeleteEquipo(string id) {
            //throw new NotImplementedException();
        }
        public void Save() {
            uow.Commit();
        }
    }
}