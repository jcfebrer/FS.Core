using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FSDatabase
{
    public class Csv
    {
        /// <summary>
        /// Convierte un fichero csv en un Datatable, teniendo en cuenta los campos entre comillas.
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            StreamReader sr = new StreamReader(strFilePath);
            string[] headers = sr.ReadLine().Split(',');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// Convierte una cadena separada por comas en un array de strings, teniendo en cuenta los campos entre comillas.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static String[] ConvertCSVtoArray(string data)
        {
            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            String[] Fields = CSVParser.Split(data);

            return Fields;
        }
    }
}
