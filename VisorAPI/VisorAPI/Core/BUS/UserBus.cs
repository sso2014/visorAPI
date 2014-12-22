using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisorAPI.Core.VO;
using System.Data;

namespace VisorAPI.Core.BUS
{
    public class UserBus:IDisposable
    {
        public UserBus(){
            dao = new DAO.UserDao();
        }
        private DAO.UserDao dao;
        public List<Equipo> equipos(){
            List<Equipo>equipos = new List<Equipo>();
            foreach (DataRow dr in dao.getEquipos().Rows){
                equipos.Add(new Equipo(){
                    EquipoID = dr["EQUIPOID"].ToString(),
                    EquipoNombre = (string)dr["EQUIPONOMBRE"]
                });}
            return equipos;
        }
        public void Entry(Equipo equipo) {
            dao.UpdateEquipo(equipo.EquipoID, equipo.EquipoNombre, equipo.PosicionInicio, equipo.PosicionFin, equipo.Tramos);        
        }
        public void InsertEquipo(Equipo equipo) {
            dao.insertEquipo(equipo.EquipoNombre, equipo.PosicionInicio,equipo.PosicionFin,equipo.Tramos);
        }
        public void Dispose()
        {
            
        }
    }
}