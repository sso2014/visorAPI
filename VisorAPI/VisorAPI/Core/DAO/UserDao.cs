using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace VisorAPI.Core.DAO
{
    public class UserDao
    {
        public UserDao(){
            conn = new dbConnection();
        }
        private dbConnection conn = null;
        public DataTable getEquipos()
        {
            string query = string.Format(
            "READ_ALL_EQUIPO");
            return conn.executeSelectQuery(query, null);
        }
        public bool UpdateEquipo(string EquipoID, string equipoNombre, int PosicionIni, int PosicionFin, int Tramos)
        {
            try
            {
                string query = string.Format(
                       "UPDATE_EQUIPO");
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@EQUIPOID", SqlDbType.Int);
                parameters[0].Value = Convert.ToInt32(EquipoID);
                parameters[1] = new SqlParameter("@NOMBREEQUIPO", SqlDbType.NChar);
                parameters[1].Value = equipoNombre;
                parameters[2] = new SqlParameter("@POSICIONINICIO", SqlDbType.Int);
                parameters[2].Value = PosicionIni;
                parameters[3] = new SqlParameter("@POSICIONFIN", SqlDbType.Int);
                parameters[3].Value = PosicionFin;
                parameters[4] = new SqlParameter("@TRAMOS", SqlDbType.Int);
                parameters[4].Value = Tramos;
                return conn.executeUpdateQuery(query, parameters);
            }
            catch (SqlException exp)
            {
                return false;
            }
        }
        public bool insertEquipo(string EquipoNombre, int PosicionIni, int posicionFin, int Tramos) {
           
            string query =
                string.Format("SAVE_EQUIPO");

            try
            {              
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@EQUIPONOMBRE", SqlDbType.NChar);
                parameters[0].Value = EquipoNombre;
                parameters[1] = new SqlParameter("@POSICIONINICIO", SqlDbType.Int);
                parameters[1].Value = PosicionIni;
                parameters[2] = new SqlParameter("@POSICIONFIN", SqlDbType.Int);
                parameters[2].Value = posicionFin;
                parameters[3] = new SqlParameter("@TRAMOS", SqlDbType.Int);
                parameters[3].Value = Tramos;
                return conn.executeUpdateQuery(query, parameters);
            }
            catch (SqlException exp)
            {
                return false;
            }
        }
    }
}
