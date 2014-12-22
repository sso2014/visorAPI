using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisorAPI.Core.VO;

namespace VisorAPI.View
{
    public interface IEquipoView
    {
        event Action EquipoSelected;
        event Action Closed;
        event Action EquipoCreated;
        event Action DrawerEquipo;

        IList<Equipo> equipos { get; }
        Equipo SelectedEquipo { get; }
        Equipo CreatedEquipo { get; }
        void LoadEquipos(IList<Equipo> equipos);
        void LoadEquipo(Equipo equipo);
       
    }
}
