using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace VisorRemoting.V6
{
    public class Helper
    {

        private static DataSet ds = null;
        private static DataSet dsRead = null;
        private static string fName = @"C:\Panel\Panel.xml";
        private static DataTable CreateTable()
        {
            DataTable dt = new DataTable("EQUIPO");
            dt.Columns.Add(new DataColumn("DATEUPDATE", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("ID", typeof(string)));
            dt.Columns.Add(new DataColumn("ANGULO", typeof(int)));
            dt.Columns.Add(new DataColumn("TENSION", typeof(int)));
            dt.Columns.Add(new DataColumn("PRESION", typeof(int)));
            dt.Columns.Add(new DataColumn("APLICACION", typeof(int)));
            dt.Columns.Add(new DataColumn("SENTIDO", typeof(bool)));
            dt.Columns.Add(new DataColumn("HABILITADO", typeof(bool)));
            dt.Columns.Add(new DataColumn("CAMINANDO", typeof(bool)));
            dt.Columns.Add(new DataColumn("ESPERANDO_PRES", typeof(bool)));
            dt.Columns.Add(new DataColumn("SECO", typeof(bool)));
            dt.Columns.Add(new DataColumn("PRESION_NOR", typeof(bool)));
            dt.Columns.Add(new DataColumn("FALLA_ELECTRICA", typeof(bool)));
            dt.Columns.Add(new DataColumn("ALARMA_SEG", typeof(bool)));

            return dt;
        }
        private static bool FinalyPross = false;

        public static void CreateDataSet()
        {
            if (ds == null)
            {
                DataSet dataset = new DataSet("DsEquipo");
                ds = dataset;
                ds.Tables.Add(CreateTable());
            }
        }
        public static void Insert(DateTime dateUpdate,  string id, int presion, bool alarma_seg, int angulo, int aplicacion, bool caminando, bool esperando_pres, bool falla_electrica, bool habilitado, bool presion_nor, bool seco, bool sentido, int tension)
        {
            CreateDataSet();

            DataTable dt = ds.Tables["EQUIPO"];
            DataRow dr = dt.NewRow();
            dr["DATEUPDATE"] = (DateTime)dateUpdate;
            dr["ID"] = (string)id;
            dr["ANGULO"] = (int)angulo;
            dr["TENSION"] = (int)tension;
            dr["PRESION"] = (int)presion;
            dr["APLICACION"] = (int)aplicacion;
            dr["SENTIDO"] = (bool)sentido;
            dr["HABILITADO"] = (bool)habilitado;
            dr["CAMINANDO"] = (bool)caminando;
            dr["ESPERANDO_PRES"] = (bool)esperando_pres;
            dr["SECO"] = (bool)seco;
            dr["PRESION_NOR"] = (bool)presion_nor;
            dr["FALLA_ELECTRICA"] = (bool)falla_electrica;
            dr["ALARMA_SEG"] = (bool)alarma_seg;

            dt.Rows.Add(dr);
            ds.Tables["EQUIPO"].AcceptChanges();
            ds.Tables["EQUIPO"].WriteXml(fName);
            ds.Tables["equipo"].Clear();

            display.FechaUpdate = DateTime.Now;
            
            FinalyPross = true;


        }
        private static Display display = new Display();
        public static Display GetDisplay()
        {
            if (dsRead == null)
            {
                dsRead = new DataSet();
                dsRead.Tables.Add(CreateTable());
            }

            if (FinalyPross)
            {
                dsRead.Tables["EQUIPO"].ReadXml(fName);

                foreach (DataRow dr in dsRead.Tables["Equipo"].Rows)
                {
                    display.FechaUpdate = Convert.ToDateTime(dr["DATEUPDATE"]);
                    display.Id = dr["ID"].ToString();
                    display.Angulo = Convert.ToInt32(dr["ANGULO"]);
                    display.Tension = Convert.ToInt32(dr["TENSION"]);
                    display.Presion = Convert.ToInt32(dr["PRESION"]);
                    display.Aplicacion = Convert.ToInt32(dr["APLICACION"]);
                    display.Sentido = Convert.ToBoolean(dr["SENTIDO"]);
                    display.Habilitado = Convert.ToBoolean(dr["HABILITADO"]);
                    display.Caminando = Convert.ToBoolean(dr["CAMINANDO"]);
                    display.EsperandoPresion = Convert.ToBoolean(dr["ESPERANDO_PRES"]);
                    display.Seco = Convert.ToBoolean(dr["SECO"]);
                    display.PresionNor = Convert.ToBoolean(dr["PRESION_NOR"]);
                    display.FallaElectrica = Convert.ToBoolean(dr["FALLA_ELECTRICA"]);
                    display.AlarmaDeSeguridad = Convert.ToBoolean(dr["ALARMA_SEG"]);
                }

                FinalyPross = false;
            }
            return display;
        }
    }
}