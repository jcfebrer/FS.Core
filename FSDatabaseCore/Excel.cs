using System;
using System.Data;
using System.Data.OleDb;

namespace FSDatabaseCore
{
    public class Excel
    {
        public string ConnectionString { get; set; }

        public Excel(string fileName)
        {
            ConnectionString = string.Format("provider=Microsoft.Jet.OLEDB.4.0; data source={0};Extended Properties=Excel 8.0;", fileName);
        }
        
        public DataSet Parse()
        {
            DataSet data = new DataSet();

            foreach (var sheetName in GetExcelSheetNames())
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionString))
                {
                    DataTable dataTable = new DataTable();
                    string query = string.Format("SELECT * FROM [{0}]", sheetName);
                    con.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(query, con);
                    adapter.Fill(dataTable);
                    data.Tables.Add(dataTable);
                }
            }

            return data;
        }

        public DataTable Parse(string sheetName)
        {
            using (OleDbConnection con = new OleDbConnection(ConnectionString))
            {
                DataTable dataTable = new DataTable();
                string query = string.Format("SELECT * FROM [{0}]", sheetName);
                con.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, con);
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        private string[] GetExcelSheetNames()
        {
            OleDbConnection con = null;
            DataTable dt = null;
            con = new OleDbConnection(ConnectionString);
            con.Open();
            dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dt == null)
            {
                return null;
            }

            String[] excelSheetNames = new String[dt.Rows.Count];
            int i = 0;

            foreach (DataRow row in dt.Rows)
            {
                excelSheetNames[i] = row["TABLE_NAME"].ToString();
                i++;
            }

            return excelSheetNames;
        }
    }
}
