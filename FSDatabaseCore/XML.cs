using FSExceptionCore;
using FSLibraryCore;
using FSNetworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FSDatabaseCore
{
    public class XML
    {
        DataTable m_dataTable;
        string m_fileName;
        string m_directory;

        public XML(string directory)
        {
            m_directory = directory;
        }

        /// <summary>
        /// fileName es el nombre del fichero xml. Si existe el fichero xsd, lo carga tambien.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool Load(string fileName)
        {
            m_fileName = m_directory + (m_directory.EndsWith("\\") ? "" : "\\") + fileName;
            if (!m_fileName.ToLower().EndsWith(".xml"))
                m_fileName += ".xml";

            //m_fileName = HttpContext.Current.Server.MapPath(m_fileName);

            m_dataTable = (DataTable)Web.GetCacheValue(m_fileName);

            if (m_dataTable == null)
            {
                m_dataTable = new DataTable();

                if (File.Exists(FSLibraryCore.TextUtil.Replace(m_fileName, ".xml", ".xsd")))
                    m_dataTable.ReadXmlSchema(FSLibraryCore.TextUtil.Replace(m_fileName, ".xml", ".xsd"));
                if (File.Exists(m_fileName))
                    m_dataTable.ReadXml(m_fileName);
                else
                {
                    throw new ExceptionUtil("Fichero: " + m_fileName + ", no existe.");
                }

                Web.SetCacheValue(m_fileName, m_dataTable);
            }

            return true;
        }

        public void DeleteRow(DataRow row)
        {
            if (m_dataTable == null)
                throw new ExceptionUtil("Datatable nulo. Revisa la carga del fichero XML.");

            m_dataTable.Rows.Remove(row);
        }

        public bool Insert(DataRow row)
        {
            try
            {
                if (m_dataTable == null)
                    throw new ExceptionUtil("Datatable nulo. Revisa la carga del fichero XML.");

                m_dataTable.Rows.Add(row);
                return true;
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }

        public bool Save()
        {
            try
            {
                if (m_dataTable == null)
                    throw new ExceptionUtil("Datatable nulo. Revisa la carga del fichero XML.");

                m_dataTable.WriteXml(m_fileName);
                return true;
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }

        public DataTable GetSchemaTables()
        {
            DirectoryInfo d = new DirectoryInfo(m_directory); // new DirectoryInfo(HttpContext.Current.Server.MapPath(m_directory));
            FileInfo[] Files = d.GetFiles("*.xml");
            
            DataTable schema = new DataTable();
            schema.Columns.Add("TABLE_NAME", typeof(String));

            foreach (FileInfo file in Files)
            {
                DataRow row = schema.NewRow();
                row["TABLE_NAME"] = file.Name.Replace(".xml", "");

                schema.Rows.Add(row);
            }
            return schema;
        }

        ///// <summary>
        ///// Obtenemos un DataRow de un xml que coincida con la select especificada
        ///// </summary>
        ///// <param name="select"></param>
        ///// <returns></returns>
        //public static DataRow[] Select(string select)
        //{
        //    return m_XMLDataTable.Select(select);
        //}

        /// <summary>
        /// Obtenemos un DataTable con la select especificada
        /// </summary>
        /// <param name="select"></param>
        /// <returns></returns>
        public DataTable Select(string filter)
        {
            if (m_dataTable == null)
                throw new ExceptionUtil("Datatable nulo. Revisa la carga del fichero XML.");

            //si hay fechas, las cambiamos al formato MM/dd/yyyy
            if (filter.IndexOf('#') > 0)
                filter = FSLibraryCore.DateTimeUtil.ToISO_8601(filter);

            DataRow[] rows = m_dataTable.Select(filter);
            if (rows.Length == 0)
                return null;
            return rows.CopyToDataTable();
        }

        public DataTable Select(string filter, string sort)
        {
            if (m_dataTable == null)
                throw new ExceptionUtil("Datatable nulo. Revisa la carga del fichero XML.");

            DataRow[] rows = m_dataTable.Select(filter, sort);
            if (rows.Length == 0)
                return null;
            return rows.CopyToDataTable();
        }

        public DataTable Select()
        {
            if (m_dataTable == null)
                throw new ExceptionUtil("Datatable nulo. Revisa la carga del fichero XML.");

            return m_dataTable;
        }

        public DataRow SelectRow(string filter)
        {
            if (m_dataTable == null)
                throw new ExceptionUtil("Datatable nulo. Revisa la carga del fichero XML.");

            DataRow[] rows = m_dataTable.Select(filter);
            if (rows.Length == 0)
                return null;
            return rows.FirstOrDefault();
        }

        /// <summary>
        /// Obtenemos el DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable DataTable
        {
            get
            {
                return m_dataTable;
            }
        }

        public DataTable GetSchema(string tableName)
        {
            Load(m_directory + (m_directory.EndsWith("\\") ? "" : "\\") + tableName + ".xml");
            return GetSchema();
        }

        public DataTable GetSchema()
        {
            if (m_dataTable == null)
                throw new ExceptionUtil("Datatable nulo. Revisa la carga del fichero XML.");

            DataTable schema = (DataTable)Web.GetCacheValue("schema_" + m_dataTable.TableName);

            if (schema == null)
            {
                schema = new DataTable();
                schema.Columns.Add("ColumnName", typeof(String));
                schema.Columns.Add("ColumnSize", typeof(int));
                schema.Columns.Add("DataType", typeof(Type));
                schema.Columns.Add("IsAutoIncrement", typeof(bool));
                schema.Columns.Add("AllowDBNull", typeof(bool));
                schema.Columns.Add("IsKey", typeof(bool));
                foreach (DataColumn column in m_dataTable.Columns)
                {
                    DataRow row = schema.NewRow();
                    row["ColumnName"] = column.ColumnName;
                    row["ColumnSize"] = column.MaxLength;
                    row["DataType"] = column.DataType;
                    row["IsAutoIncrement"] = column.AutoIncrement;
                    row["AllowDBNull"] = column.AllowDBNull;

                    foreach (DataColumn primaryColumn in m_dataTable.PrimaryKey)
                    {
                        if (primaryColumn.ColumnName == column.ColumnName)
                            row["IsKey"] = true;
                        else
                            row["IsKey"] = false;
                    }
                    schema.Rows.Add(row);
                }

                Web.SetCacheValue("schema_" + m_dataTable.TableName, schema);
            }

            return schema;
        }

        public static DataRow[] XMLSelect(string select, string fileNameXml, string fileNameXsd)
        {
            DataTable dtPaginas = new DataTable();
            dtPaginas.ReadXmlSchema(fileNameXsd);
            dtPaginas.ReadXml(fileNameXml);

            return dtPaginas.Select(select);
        }

        public static DataTable XMLDataTable(string select, string fileNameXml, string fileNameXsd)
        {
            DataTable dtPaginas = new DataTable();
            dtPaginas.ReadXmlSchema(fileNameXsd);
            dtPaginas.ReadXml(fileNameXml);

            return dtPaginas.Select(select).CopyToDataTable();
        }

        public static DataTable XMLDataTable(string fileNameXml, string fileNameXsd)
        {
            DataTable dtPaginas = new DataTable();
            dtPaginas.ReadXmlSchema(fileNameXsd);
            dtPaginas.ReadXml(fileNameXml);

            return dtPaginas;
        }
    }
}
