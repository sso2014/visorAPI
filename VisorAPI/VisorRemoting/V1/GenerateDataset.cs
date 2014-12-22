using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace VisorRemoting.V1
{
    public class GenerateDataset
    {
        public DataSet GetDsPanel()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(CreateTablePanel());
            return ds;
        }
        public static DataTable CreateTablePanel()
        {

            DataTable dt = new DataTable("Panel");
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("ANGULO_ACTUAL", typeof(int));
            dt.Columns.Add("TENSION", typeof(int));
            dt.Columns.Add("PRESION", typeof(int));
            dt.Columns.Add("APLICACION", typeof(int));
            dt.Columns.Add("SENTIDO", typeof(bool));
            dt.Columns.Add("HABILITADO", typeof(bool));
            dt.Columns.Add("CAMINANDO", typeof(bool));
            dt.Columns.Add("ESPERANDO_PRESION", typeof(int));
            dt.Columns.Add("PRESION_NOR", typeof(bool));
            dt.Columns.Add("FALLA_ELECTRICA", typeof(bool));
            dt.Columns.Add("ALARMA_SEGURIDAD", typeof(bool));
            return dt;
        }
    }
}

