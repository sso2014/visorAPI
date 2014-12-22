using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using V.Core.Data;
//Falta di
namespace V.Data
{
    public class EquipoCollection : IEnumerable,IDisposable
    {
        public EquipoCollection(){
        }
        public IEnumerator GetEnumerator(){
            using (EquipoRepository repositorio = new EquipoRepository())
            {
                foreach (Equipo eq in repositorio.SelectEquipoAll())
                {
                    yield return eq;
                }
            }
        }
        private bool disposed;
        public void Dispose()
        {
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
    }
}