using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using V.Core.Data;

namespace V.Data
{
    public interface IEquipoRepository
    {
        IList<Equipo> SelectEquipoAll();
        Equipo GetEquipoByID(string id);
        void InsertEquipo(Equipo obj);
        void UpdateEquipo(Equipo obj);
        void DeleteEquipo(string id);
        void Save();
    }
}