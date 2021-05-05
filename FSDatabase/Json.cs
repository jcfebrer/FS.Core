using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace FSDatabase
{
    public class Json
    {
        static JavaScriptSerializer serializer = new JavaScriptSerializer();

        /// <summary>
        /// Longitud máxima de la cadena Json generada
        /// </summary>
        public static int MaxJsonLength
        {
            get { return serializer.MaxJsonLength; }
            set { serializer.MaxJsonLength = value; }
        }

        public static string ObjectToJson(object obj)
        {
            return serializer.Serialize(obj);
        }

        public static object JsonToObject(string json, Type targetType)
        {
            return serializer.Deserialize(json, targetType);
        }

        /// <summary>
        /// Conversión a JSON utilizando el serializado JavaScript interno de .NET
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable dataTable)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dataTable.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }

        /// <summary>
        /// Conversión a JSON sin utilizar ningún serializador externo
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static string DataTableToJson2(DataTable dataTable)
        {
            string[] StrDc = new string[dataTable.Columns.Count];

            string HeadStr = string.Empty;
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {

                StrDc[i] = dataTable.Columns[i].Caption;
                HeadStr += "\"" + StrDc[i] + "\":\"" + StrDc[i] + i.ToString() + "¾" + "\",";

            }

            HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);

            StringBuilder Sb = new StringBuilder();
            Sb.Append("[");

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string TempStr = HeadStr;

                for (int j = 0; j < dataTable.Columns.Count; j++)
                {

                    TempStr = TempStr.Replace(dataTable.Columns[j] + j.ToString() + "¾", dataTable.Rows[i][j].ToString().Trim());
                }
                Sb.Append("{" + TempStr + "},");
            }

            Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));

            if (Sb.ToString().Length > 0)
                Sb.Append("]");

            return StripControlChars(Sb.ToString());

        }

        public static Dictionary<string, object> Load(string fileName)
        {
            using (StreamReader r = new StreamReader(fileName))
            {
                string json = r.ReadToEnd();
                Dictionary<string, object> json_Dictionary = (new JavaScriptSerializer()).Deserialize<Dictionary<string, object>>(json);

                return json_Dictionary;
            }
        }

        public static string StripControlChars(string s)
        {
            return Regex.Replace(s, @"[^\x20-\x7F]", "");
        }

    }
}
