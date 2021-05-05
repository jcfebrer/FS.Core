using System;
using System.Text;
using FSLibrary;
using System.Data;
using FSPortal;
using FSDatabase;

namespace FSPaginas.Admin
{
    public class CrearClass : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        public string Inicio()
        {
            StringBuilder sb = new StringBuilder();
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            DataTable tables = db.GetSchemaTables();

            foreach(DataRow row in tables.Rows)
            {
                sb.AppendLine(CreaClaseTabla(row["TABLE_NAME"].ToString()) + Ui.Lf());
            }

            return sb.ToString();
        }

        public string CreaClaseTabla(string tableName)
        {
            StringBuilder sb = new StringBuilder();
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            DataTable dt = db.GetSchemaTable(tableName);

            sb.AppendLine("public class " + TextUtil.Capitalize(tableName) + " {" + Ui.Lf() + Ui.Lf());

            foreach(DataRow row in dt.Rows)
            {
                string columnType = row["DataType"].ToString();
                string columnName = row["ColumnName"].ToString();
                string nullableSign = "";

                if (bool.Parse(row["AllowDBNull"].ToString()) == true && columnType!="System.String") nullableSign = "?";

                sb.AppendLine("public " + columnType + nullableSign + " " + TextUtil.Capitalize(columnName) + " { get; set; }" + Ui.Lf());
            }

            sb.AppendLine("}" + Ui.Lf());

            return sb.ToString();
        }
        
    }
}
