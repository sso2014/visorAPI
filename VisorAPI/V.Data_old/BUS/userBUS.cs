using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using V.Core.Data;
using System.Data;
using V.Core.Net;

namespace V.Data.BUS
{
    class userBUS:IDisposable
    {
        public userBUS() {
            dao = new DAO.userDAO();
        }
        private DAO.userDAO dao = null;
        public void InsertEquipo(Equipo equipo)
        {
            if (equipo != null)
            {
                dao.insertIntoEquipo(
                    equipo.Numero,
                    equipo.PkIni,
                    equipo.PkFin,
                    equipo.Tramos
                    );
            }
        }
        public void InsertEquipoSuperficie(Equipo equipo) {
            //if (equipo != null) {              
            //    dao.insertIntoEquipo_superficie(
            //        equipo.Id,
                   
                    
            //}
        }
        public List<Equipo> GetEquipos(){               
            List<Equipo> equipos = new List<Equipo>();
               foreach (DataRow dr in dao.selectEquipojoinSuperficie().Rows)
                {
                    equipos.Add(
                        new Equipo()
                        {
                            Id = dr["ID"].ToString(),
                            Numero = dr["NUMERO"].ToString(),
                            PkIni = Convert.ToInt32(dr["PKINI"]),
                            PkFin = Convert.ToInt32(dr["PKFIN"]),
                            Tramos = Convert.ToInt32(dr["TRAMOS"]),
                            superficie = new Superficie()
                            {
                                PosX = Convert.ToInt32(dr["POSX"]),
                                PosY = Convert.ToInt32(dr["POSY"]),
                                Width = Convert.ToInt32(dr["WIDTH"]),
                                Height = Convert.ToInt32(dr["HEIGHT"])
                            },
                            //Remote = new RemoteConnection() { 
                            //    ID = dr["NUMERO"].ToString(),
                            //    workSocket = new System.Net.Sockets.Socket( System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp),
                            //    sb = new StringBuilder()
                            //}
                        }
                    );
               }
         return equipos;
        }
        public void UpdateEquipo(Equipo equipo) {
            dao.updateEquipo(equipo.Id,equipo.Numero, equipo.PkIni, equipo.PkFin, equipo.Tramos);        
        } 
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}