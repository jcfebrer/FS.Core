using FSException;
using FSLibrary;
using FSSystemInfo;

#if NETCOREAPP
    using Microsoft.AspNetCore.Http;
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;

#if NET35_OR_GREATER || NETCOREAPP
    using System.Linq;
#endif

using System.Text;
using System.Web;

namespace FSDatabase
{
    public static class Utils
    {
        public static string m_simbDate = "#";
        public static ServerTypeEnum ServerType { get; set; }
        public enum ServerTypeEnum
        {
            SQLServer,
            MySQL,
            Odbc,
            Oledb,
            Access97,
            Access2000,
            Oracle,
            SQLite
        }

        public enum FieldTypeEnum
        {
            DateTime,
            String,
            Email,
            Number,
            Boolean
        }

        private static IEnumerable<DataRow[]> ToChunks(IEnumerable<DataRow> source, int chunkSize)
        {
            if (source == null)
                yield break;

            List<DataRow> chunk = new List<DataRow>();
            foreach (DataRow row in source)
            {
                chunk.Add(row);
                if (chunk.Count == chunkSize)
                {
                    yield return chunk.ToArray();
                    chunk.Clear();
                }
            }
            if (chunk.Count > 0)
                yield return chunk.ToArray();
        }

#if NET35_OR_GREATER || NETCOREAPP
        private static IEnumerable<IEnumerable<T>> ToChunks<T>(IEnumerable<T> enumerable,
                                              int chunkSize)
        {
            int itemsReturned = 0;
            var list = enumerable.ToList();
            int count = list.Count;
            while (itemsReturned < count)
            {
                int currentChunkSize = Math.Min(chunkSize, count - itemsReturned);
                yield return list.GetRange(itemsReturned, currentChunkSize);
                itemsReturned += currentChunkSize;
            }
        }
#endif

#if NET35_OR_GREATER || NETCOREAPP
        public static DataTable SplitDatatable(DataTable dataTable, int page, int size)
        {
            IEnumerable<DataTable> dataTables = ToChunks(dataTable.AsEnumerable(), size)
                          .Select(rows => rows.CopyToDataTable());

            List<DataTable> dtList = dataTables.ToList();
            if (page > dtList.Count)
                page = dtList.Count;

            return dtList[page];
        }
#else
        public static DataTable SplitDatatable(DataTable dataTable, int page, int size)
        {
            List<DataTable> dtList = new List<DataTable>();
            List<DataRow> dataRows = new List<DataRow>();

            foreach (DataRow row in dataTable.Rows)
            {
                dataRows.Add(row);
            }

            IEnumerable<DataRow[]> chunks = ToChunks(dataRows, size);

            foreach (DataRow[] rows in chunks)
            {
                dtList.Add(CopyRowsToDataTable(rows, dataTable)); // Usamos CopyRowsToDataTable de la respuesta anterior
            }

            if (page > dtList.Count - 1) // Ajustamos el índice a dtList.Count - 1
                page = dtList.Count - 1;

            if (dtList.Count == 0)
            {
                return dataTable.Clone(); // Return a clone of original table if dtList is empty.
            }

            return dtList[page];
        }
#endif

        public static FieldTypeEnum GetFSTypeFromSystemType(Type type)
        {
            return GetFSTypeFromType(type.ToString().ToLower());
        }

        public static FieldTypeEnum GetFSTypeFromType(string type)
        {
            switch (type.ToLower())
            {
                case "system.int16":
                case "system.int32":
                case "system.int64":
                case "system.double":
                case "system.single":
                case "system.byte":
                case "system.decimal":
                    return FieldTypeEnum.Number;
                case "system.datetime":
                    return FieldTypeEnum.DateTime;
                case "system.char":
                case "system.string":
                    return FieldTypeEnum.String;
                case "system.boolean":
                    return FieldTypeEnum.Boolean;
            }
            return FieldTypeEnum.String;
        }

        public static FieldTypeEnum ConvertStringToFieldType(string fieldType)
        {
            switch (fieldType.ToLower())
            {
                case "number":
                    return FieldTypeEnum.Number;
                case "datetime":
                    return FieldTypeEnum.DateTime;
                case "string":
                    return FieldTypeEnum.String;
                case "boolean":
                    return FieldTypeEnum.Boolean;
            }

            return FieldTypeEnum.String;
        }

        public static DataTable SelectAndCopy(DataTable dtPaginas, string select)
        {
            DataTable newTable = dtPaginas.Clone(); // Crea una copia de la estructura de la tabla

            foreach (DataRow row in dtPaginas.Select(select)) // Obtiene las filas filtradas
            {
                DataRow newRow = newTable.NewRow();
                newRow.ItemArray = row.ItemArray; // Copia los valores de la fila
                newTable.Rows.Add(newRow);
            }

            return newTable;
        }

        public static DataTable CopyRowsToDataTable(DataRow[] rows, DataTable sourceTable)
        {
            if (rows == null || rows.Length == 0)
                return sourceTable.Clone(); // Devuelve una tabla vacía con la misma estructura

            DataTable newTable = sourceTable.Clone(); // Crea una copia de la estructura de la tabla

            foreach (DataRow row in rows)
            {
                DataRow newRow = newTable.NewRow();
                newRow.ItemArray = row.ItemArray; // Copia los valores de la fila
                newTable.Rows.Add(newRow);
            }

            return newTable;
        }

        public static DataRow GetFirstOrDefault(DataRow[] rows)
        {
            if (rows == null || rows.Length == 0)
                return null; // Devuelve null si el array es nulo o vacío

            return rows[0]; // Devuelve el primer elemento del array
        }

        public static string FormatSQL(string sql)
        {
            return FormatSQL(sql, ServerType);
        }

        public static string FormatSQL(string sql, ServerTypeEnum bdType)
        {
            switch (bdType)
            {
                case ServerTypeEnum.SQLServer:
                    sql = TextUtil.Replace(sql, "true", "1");
                    sql = TextUtil.Replace(sql, "false", "0");
                    break;
                case ServerTypeEnum.Oracle:
                    sql = TextUtil.Replace(sql, "true", "1");
                    sql = TextUtil.Replace(sql, "false", "0");
                    sql = TextUtil.Replace(sql, "[", "");
                    sql = TextUtil.Replace(sql, "]", "");
                    break;
                case ServerTypeEnum.SQLite:
                case ServerTypeEnum.MySQL:
                    sql = TextUtil.Replace(sql, "true", "1");
                    sql = TextUtil.Replace(sql, "false", "0");
                    sql = TextUtil.Replace(sql, "len(", "length(");
                    sql = TextUtil.Replace(sql, "[", "`");
                    sql = TextUtil.Replace(sql, "]", "`");
                    break;
            }


            return sql;
        }

        /// <summary>
        ///     Comprueba si un DataSet esta vacio
        /// </summary>
        /// <param name="dataSet">DataSet que se desea comprobar</param>
        /// <returns>True si el DataSet es nulo o está vacio</returns>
        public static bool IsEmpty(DataSet dataSet)
        {
            try
            {
                if (dataSet == null)
                    return true;

                foreach (DataTable t in dataSet.Tables)
                    if (!IsEmpty(t))
                        return false;

                return true;
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }

        /// <summary>
        ///     Comprueba si un DataTable esta vacio
        /// </summary>
        /// <param name="dataTable">DataTable que se desea comprobar</param>
        /// <returns>True si el DataTable es nulo o está vacio</returns>
        public static bool IsEmpty(DataTable dataTable)
        {
            try
            {
                if (dataTable == null)
                    return true;

                return dataTable.Rows.Count == 0;
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public static DataTable OrderBy(DataTable dt, string field, bool ascendent)
        {
            try
            {
                var dv = dt.DefaultView;
                dv.Sort = field + (ascendent ? " asc" : " desc");
                return dv.ToTable();
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public static DataTable RandomizeDataTable(DataTable dt)
        {
            try
            {
                var _rand = new Random();
                var indices = new int[dt.Rows.Count];

                for (var i = 0; i < indices.Length; i++)
                    indices[i] = i;

                // Knuth-Fisher-Yates shuffle indices randomly 

                for (var i = indices.Length - 1; i > 0; i--)
                {
                    var n = _rand.Next(i + 1);
                    var tmp = indices[i];
                    indices[i] = indices[n];
                    indices[n] = tmp;
                }

                // Add new column to data table (if it's not there already) 
                // to store shuffle index 

                if (dt.Columns["rndSortId"] == null)
                    dt.Columns.Add(new DataColumn("rndSortId", typeof(int)));

                var rndSortColIdx = dt.Columns["rndSortId"].Ordinal;

                for (var i = 0; i < dt.Rows.Count; i++)
                    dt.Rows[i][rndSortColIdx] = indices[i];

                var dv = new DataView(dt);
                dv.Sort = "rndSortId";
                return dv.ToTable();
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public static void CheckData(HttpRequest frm, Register frmCampos, bool truncar)
        {
            try
            {
                if (frmCampos == null) return;

#if NETFRAMEWORK
                for (int f = 0; f <= frm.Form.Count - 1; f++)
                {
                    string value = frm.Form.Get(f);
                    string name = frm.Form.Keys[f];
#else
                var dict = frm.Form.ToDictionary(x => x.Key, x => x.Value.ToString());

                foreach (var item in dict)
                {
                    string value = item.Value;
                    string name = item.Key;
#endif

                    Field field = frmCampos.Find(name);

                    if (field != null)
                    {
                        if (field.Obligatorio & (value == ""))
                            throw new ExceptionUtil("Debes rellenar todos los campos obligatorios.",
                                ExceptionUtil.ExceptionType.Information);

                        if (value.Length > field.Tamano)
                        {
                            if (truncar)
                                value = TextUtil.Left(value, field.Tamano);
                            else
                                throw new ExceptionUtil("El tamaño del campo: [" + name + "], supera los " + field.Tamano +
                                                        " carácteres.");
                        }

                        switch (field.Tipo)
                        {
                            case FieldTypeEnum.DateTime:
                                if (!FSLibrary.DateTimeUtil.IsDate(value) && value != "")
                                    throw new ExceptionUtil("El valor: [" + value + "], no es una fecha valida.",
                                        ExceptionUtil.ExceptionType.Information);
                                break;
                            case FieldTypeEnum.Email:
                                if (!TextUtil.IsEmail(value))
                                    throw new ExceptionUtil("El valor: [" + value + "], no es un email valido.",
                                        ExceptionUtil.ExceptionType.Information);
                                break;
                            //case "u":
                                //var ex = ExisteValor(field.Campo, value, frmTabla);

                                //if (ex)
                                //    throw new ExceptionUtil(
                                //        "Ya existe el valor: [" + value +
                                //        "], en la base de datos. Utiliza un valor diferente.",
                                //        ExceptionUtil.ExceptionType.Information);
                                //break;
                            case FieldTypeEnum.Number:
                                if (value != "")
                                    if (!NumberUtils.IsNumeric(value))
                                        throw new ExceptionUtil("El valor: [" + value + "], no es un valor numérico.",
                                            ExceptionUtil.ExceptionType.Information);
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public static string DameWhere(string fieldName, FieldTypeEnum fieldType, string value)
        {
            try
            {
                string sWhere;

                switch (fieldType)
                {
                    case FieldTypeEnum.DateTime:
                        sWhere = "[" + fieldName + "]=" + m_simbDate + value + m_simbDate;
                        break;
                    case FieldTypeEnum.Number:
                        sWhere = "[" + fieldName + "]=" + NumberUtils.NumberDouble(value);
                        break;
                    default:
                        sWhere = "[" + fieldName + "]='" + value + "'";
                        break;
                }

                return sWhere;
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public static void ExportXml(string fileName, DataSet ds)
        {
            try
            {
                ds.WriteXml(fileName);
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public static void ExportHtml(string fileName, DataSet ds)
        {
            try
            {
                var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                var bs = new BufferedStream(fs);
                var sw = new StreamWriter(bs);
                sw.WriteLine("<html>");
                sw.WriteLine("<head>");
                sw.WriteLine("<title>");
                sw.WriteLine(ds.DataSetName);
                sw.WriteLine("</title>");
                sw.WriteLine("</head>");
                sw.WriteLine("<body>");
                int i = 0, r = 0, c = 0;
                for (i = 0; i <= ds.Tables.Count - 1; i++)
                {
                    sw.WriteLine("<table border=1>");
                    sw.WriteLine("<tr>");
                    for (c = 0; c <= ds.Tables[i].Columns.Count - 1; c++)
                        sw.Write("<td>{0}</td>", ds.Tables[i].Columns[c].ColumnName);
                    sw.WriteLine("</tr>");
                    for (r = 0; r <= ds.Tables[i].Rows.Count - 1; r++)
                    {
                        sw.WriteLine("<tr>");
                        for (c = 0; c <= ds.Tables[i].Columns.Count - 1; c++)
                            sw.Write("<td>{0}</td>", ds.Tables[i].Rows[r][c]);
                        sw.WriteLine("</tr>");
                    }

                    sw.WriteLine("</table>");
                    sw.WriteLine("</hr>");
                }

                sw.WriteLine("</body>");
                sw.WriteLine("</html>");
                sw.Close();
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public static DataTable ConvertArrayListToDataTable(ArrayList arrayList)
        {
            var dt = new DataTable();

            for (var i = 0; i <= arrayList.Count - 1; i++)
            {
                var GenericObject = arrayList[i];
                var NbrProp = GenericObject.GetType().GetProperties().Length;

                foreach (var item in GenericObject.GetType().GetProperties())
                {
                    var column = new DataColumn();
                    var ColName = item.Name;

                    column.ColumnName = ColName;
                    if (!dt.Columns.Contains(ColName))
                        dt.Columns.Add(column);
                }

                var row = dt.NewRow();

                var j = 0;
                foreach (var item in GenericObject.GetType().GetProperties())
                {
                    row[j] = item.GetValue(GenericObject, null);
                    j += 1;
                }

                dt.Rows.Add(row);
            }

            return dt;
        }

        public static string ShowDataTable(DataTable dataTable)
        {
            var g = 0;
            var c = "Columnas: ";

            int f;
            for (f = 0; f <= dataTable.Columns.Count - 1; f++)
                try
                {
                    c = c + dataTable.Columns[f].ColumnName + "-";
                }
                catch (Exception e)
                {
                    throw new ExceptionUtil(e);
                }

            c += "Filas: ";

            for (f = 0; f <= dataTable.Rows.Count - 1; f++)
            {
                for (g = 0; g <= dataTable.Columns.Count - 1; g++)
                    try
                    {
                        c = c + dataTable.Rows[f][g] + "-";
                    }
                    catch (Exception ex)
                    {
                        throw new ExceptionUtil(ex);
                    }

                c += "\r\n";
            }

            return c;
        }

        public static DataTable SelectDataTable(DataTable dt, string filter, string sort)
        {
            try
            {
                var dtNew = dt.Clone();
                var rows = dt.Select(filter, sort);
                foreach (var dr in rows) dtNew.ImportRow(dr);
                return dtNew;
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public static double SumColumn(DataTable dataTable, string column)
        {
            double total = 0;

            try
            {
                total = NumberUtils.NumberDouble(dataTable.Compute("Sum(" + column + ")", ""));
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }

            return total;
        }

        public static double MaxColumn(DataTable dataTable, string column)
        {
            double max = 0;

            try
            {
                max = NumberUtils.NumberDouble(dataTable.Compute("Max(" + column + ")", ""));
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }

            return max;
        }


        public static double MinColumn(DataTable dataTable, string column)
        {
            double min;

            try
            {
                min = NumberUtils.NumberDouble(dataTable.Compute("Min(" + column + ")", ""));
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }

            return min;
        }


        public static double AvgColumn(DataTable dataTable, string column)
        {
            double avg;
            try
            {
                avg = NumberUtils.NumberDouble(dataTable.Compute("Avg(" + column + ")", ""));
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }

            return avg;
        }


        public static object Compute(DataTable dataTable, string expression, string filter)
        {
            return dataTable.Compute(expression, filter);
        }

        public static string GetTableName(string sql)
        {
            int pos = sql.ToLower().IndexOf("from");
            if (pos == -1)
                return "NoName";
            int posIni = sql.ToLower().IndexOf(" ", pos);
            int posEnd = sql.ToLower().IndexOf(" ", posIni + 1);
            if (posEnd == -1)
                sql = sql.Substring(posIni + 1);
            else
                sql = sql.Substring(posIni + 1, posEnd - posIni - 1);

            sql = sql.Replace(".", "_");
            sql = TextUtil.Capitalize(sql);

            return sql;
        }

        public static string GetWhere(string sql)
        {
            string strWhere = "where";
            string strOrderBy = "order by";
            int posWhere = sql.ToLower().IndexOf(strWhere);
            if (posWhere == -1)
                return "";

            posWhere += strWhere.Length;

            int posOrder = sql.ToLower().IndexOf(strOrderBy);
            if (posOrder > 0)
                sql = sql.Substring(posWhere + 1, posOrder - posWhere);
            else
                sql = sql.Substring(posWhere + 1);

            return sql;
        }

        public static DataTable GetSortDataTable(DataTable dt, string filter, string sort)
        {
            var dtNew = dt.Clone();
            var rows = dt.Select(filter, sort);
            foreach (var dr in rows) dtNew.ImportRow(dr);
            return dtNew;
        }

        public static bool FieldExists(DataTable dataTable, string fieldName)
        {
            foreach (DataColumn dataColumn in dataTable.Columns)
                if (dataColumn.ColumnName.ToLower() == fieldName.ToLower())
                    return true;
            return false;
        }

        public static List<DataColumn> GetChangedColumns(DataTable table)
        {
            return GetChangedColumns(table, StringComparison.InvariantCultureIgnoreCase, true);
        }

        public static List<DataColumn> GetChangedColumns(DataTable table, bool ignoreWhitespace)
        {
            return GetChangedColumns(table, StringComparison.InvariantCultureIgnoreCase, ignoreWhitespace);
        }

        public static List<DataColumn> GetChangedColumns(DataTable table, DataRow row)
        {
            return GetChangedColumns(table, row, true);
        }

        public static List<DataColumn> GetChangedColumns(DataTable table, DataRow row, bool ignoreWhitespace)
        {
            return GetChangedColumns(table, row, ignoreWhitespace);
        }

        public static List<DataColumn> GetChangedColumns(DataTable table, StringComparison stringComparison,
            bool ignoreWhitespace)
        {
            if (table == null) throw new ArgumentNullException("table");

            var columnsChanged = new List<DataColumn>();
            foreach (DataRow row in table.GetChanges().Rows)
                foreach (DataColumn col in row.Table.Columns)
                    if (!columnsChanged.Contains(col) && hasColumnChanged(stringComparison, ignoreWhitespace, row, col))
                        columnsChanged.Add(col);
            return columnsChanged;
        }

        public static bool hasColumnChanged(DataRow row, DataColumn col)
        {
            return hasColumnChanged(StringComparison.InvariantCultureIgnoreCase, true, row, col);
        }

        public static bool hasColumnChanged(StringComparison stringComparison, bool ignoreWhitespace, DataRow row,
            DataColumn col)
        {
            var isEqual = true;
            if (row[col, DataRowVersion.Original] != DBNull.Value && row[col, DataRowVersion.Current] != DBNull.Value)
            {
                if (row[col] is byte[])
                    return !NumberUtils.ByteArrayCompare((byte[])row[col, DataRowVersion.Original],
                        (byte[])row[col, DataRowVersion.Current]);

                var originalVersionToCompare = row[col, DataRowVersion.Original].ToString();
                var currentVersionToCompare = row[col, DataRowVersion.Current].ToString();
                if (ignoreWhitespace)
                {
                    originalVersionToCompare = originalVersionToCompare.Trim();
                    currentVersionToCompare = currentVersionToCompare.Trim();
                }

                isEqual = originalVersionToCompare.Equals(currentVersionToCompare, stringComparison);
            }

            return !isEqual;
        }


        public static string FormatShortDate(System.DateTime date)
        {
            var dat = date.ToShortDateString();

            if (ServerType == ServerTypeEnum.MySQL)
                dat = date.ToString("yyyy-MM-dd");

            return m_simbDate + dat + m_simbDate;
        }

        public static string FormatShortDateTime(System.DateTime date)
        {
            var dat = date.ToShortDateString();

            if (ServerType == ServerTypeEnum.MySQL)
                dat = date.ToString("yyyy-MM-dd");

            return m_simbDate + dat + " " + date.ToString("HH:mm:ss") + m_simbDate;
        }


        public static string FormatLongDate(System.DateTime date)
        {
            var dat = date.ToLongDateString();

            if (ServerType == ServerTypeEnum.MySQL)
                dat = date.ToString("yyyy-MM-dd");

            return m_simbDate + dat + m_simbDate;
        }

        public static int GetConnectionId(string entryName)
        {
            var f = 0;
            foreach (ConnectionStringSettings con in ConfigurationManager.ConnectionStrings)
            {
                if (con.Name == entryName) return f;
                f++;
            }

            return 0;
        }

        public static string GetConnectionName(int entryId)
        {
            if (ConfigurationManager.ConnectionStrings.Count == 0)
                throw new ExceptionUtil("No ay entradas ConnectionStrings en el fichero web.config");
            if (ConfigurationManager.ConnectionStrings[entryId] == null)
                throw new ExceptionUtil("Entrada de ConnectionString inexistente en web.config (" + entryId + ")");
            return ConfigurationManager.ConnectionStrings[entryId].Name;
        }

        public static Version GetMdacVersion()
        {
            try
            {
                var reg = new RegistryUtil(RegistryUtil.RegSource.LocalMachine);
                var ver = reg.GetValue(@"SOFTWARE\Microsoft\DataAccess", "FullInstallVer", "0.0.0.0");
                var verTokens = ver.Split('.');
                return new Version(int.Parse(verTokens[0]), int.Parse(verTokens[1]), int.Parse(verTokens[2]),
                    int.Parse(verTokens[3]));
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }

        /// <summary>
        /// Convierte el tipo a cadena de base de datos.
        /// </summary>
        /// <param name="fieldType">Tipo de dato.</param>
        /// <returns></returns>
        public static string ToDataBaseType(Type fieldType)
        {
            switch (fieldType.ToString().ToLower())
            {
                case "system.int16":
                case "system.int32":
                case "system.int64":
                case "system.double":
                case "system.single":
                case "system.byte":
                case "system.decimal":
                    return "int";
                case "system.datetime":
                    return "datetime";
                case "system.char":
                case "system.string":
                    return "char";
                case "system.boolean":
                    return "bit";
                case "system.byte[]":
                    return "binary";
            }

            return "char";
        }

        /// <summary>
        /// Convierte el tipo a parametro de base de datos.
        /// </summary>
        /// <param name="fieldType">Type of the field.</param>
        /// <returns></returns>
        public static DbType ToParameterType(Type fieldType)
        {
            switch (fieldType.ToString().ToLower())
            {
                case "system.int16":
                case "system.int32":
                case "system.int64":
                case "system.double":
                case "system.single":
                case "system.byte":
                case "system.decimal":
                    return DbType.Int32;
                case "system.datetime":
                    return DbType.Date;
                case "system.char":
                case "system.string":
                    return DbType.String;
                case "system.boolean":
                    return DbType.Boolean;
                case "system.byte[]":
                    return DbType.Binary;
            }

            return DbType.String;
        }
    }
}
