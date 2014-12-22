using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace V.Data.DAO
{
    class userDAO
    {
        public userDAO()
        {
            conn = new dbConnection();
        }
        private dbConnection conn = null;
        public DataTable selectEquiposTable()
        {
            string query = string.Format(
                "READ_ALL_EQUIPO");
            return conn.executeSelectQuery(query, null);
        }
        public void insertIntoEquipo(string Numero, int pkini, int pkfin, int tramos)
        {
            string query = string.Format(
                "SAVE_EQUIPO");
            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@NUMERO", SqlDbType.NChar);
            parameter[0].Value = Numero;
            parameter[0] = new SqlParameter("@PKINI", SqlDbType.Int);
            parameter[0].Value = pkini;
            parameter[0] = new SqlParameter("@PKFIN", SqlDbType.Int);
            parameter[0].Value = pkfin;
            parameter[0] = new SqlParameter("@TRAMOS", SqlDbType.Int);
            parameter[0].Value = tramos;
            conn.executeInsertQuery(query, parameter);
        }
        public void insertIntoEquipo_superficie(string id, int posx, int posy, int width, int height, int tramos)
        {
            string query = string.Format(
                "SAVE_EQUIPO_SUPERFICIE");
            SqlParameter[] parameter = new SqlParameter[5];
            parameter[0] = new SqlParameter("@ID", SqlDbType.Int);
            parameter[0].Value = id;
            parameter[1] = new SqlParameter("@POSX", SqlDbType.Int);
            parameter[1].Value = posx;
            parameter[2] = new SqlParameter("@POSY", SqlDbType.Int);
            parameter[2].Value = posy;
            parameter[3] = new SqlParameter("@WIDTH", SqlDbType.Int);
            parameter[3].Value = width;
            parameter[4] = new SqlParameter("@HEGIHT", SqlDbType.Int);
            parameter[4].Value = height;
            parameter[5] = new SqlParameter("@TRAMOS", SqlDbType.Int);
            parameter[5].Value = tramos;
            conn.executeInsertQuery(query, parameter);
        }
        public void updateEquipo(string id, string numero, int pkini, int pkfin, int tramos) {
            string query =
                string.Format("UPDATE_EQUIPO");
            SqlParameter[] parameter = new SqlParameter[5];
            parameter[0] = new SqlParameter("@ID", SqlDbType.Int);
            parameter[0].Value = id;
            parameter[1] = new SqlParameter("@NUMERO", SqlDbType.NChar);
            parameter[1].Value = numero;
            parameter[2] = new SqlParameter("@PKINI", SqlDbType.Int);
            parameter[2].Value = pkini;
            parameter[3] = new SqlParameter("@PKFIN", SqlDbType.Int);
            parameter[3].Value = pkfin;
            parameter[4] = new SqlParameter("@TRAMOS", SqlDbType.Int);
            parameter[4].Value = tramos;
            conn.executeUpdateQuery(query, parameter);
        }
        public DataTable selectEquipojoinSuperficie()
        {
            string query =
                string.Format(
                "SELECT_EQUIPO_AND_SUPERFICIE");
            return conn.executeSelectQuery(query, null);
        }
    }
}