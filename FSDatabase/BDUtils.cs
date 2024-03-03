// <fileheader>
//  <copyright file="BDUtils.cs" company="Febrer Software">
//      Fecha: 03/07/2010
//      Project: FSLibrary
//      Solution: FSLibraryNET2008
//      Copyright (c) 2010 Febrer Software. Todos los derechos reservados.
//      http://www.febrersoftware.com
//  </copyright>
// </fileheader>


using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Web;
using FSDisk;
using FSNetwork;
using FSLibrary;
using FSQueryBuilder.Constants;
using FSQueryBuilder.Enums;
using DateTime = System.DateTime;
using FSException;
using System.CodeDom;
using System.Web.UI.WebControls;
using FSQueryBuilder;
using FSQueryBuilder.QueryParts.Where;
using FSTrace;
using FSSecurity;

namespace FSDatabase
{
    public class BdUtils
    {
        /// <summary>
        ///     ConnString
        /// </summary>
        public string ConnString { get; set; } = "";

        /// <summary>
        ///     ConnStringEntryName
        /// </summary>
        public string ConnStringEntryName { get; set; } = "";

        /// <summary>
        ///     TablePrefix
        /// </summary>
        public string TablePrefix { get; set; } = "";

        /// <summary>
        ///     ConnStringEntryId
        /// </summary>
        public int ConnStringEntryId { get; set; }

        /// <summary>
        ///     ProviderName
        /// </summary>
        public string ProviderName { get; set; } = "";

        /// <summary>
        ///     Transacción
        /// </summary>
        public DbTransaction Transaction { get; set; }


        private DbProviderFactory m_providerFactory;

        //~BdUtils()
        //{
        //    Close();
        //}


        public BdUtils(string connectionString, string providerName, string defaultEntryName, string tablePrefix)
        {
            ConnStringEntryName = defaultEntryName;
            TablePrefix = tablePrefix;

            if (String.IsNullOrEmpty(ConnStringEntryName))
            {
                if (!String.IsNullOrEmpty(providerName))
                    ProviderName = providerName;
                else
                    throw new ExceptionUtil("No se ha definido ProviderName en FSDatabase.Contants");

                if (!String.IsNullOrEmpty(connectionString))
                    ConnString = connectionString;
                else
                    throw new ExceptionUtil("No se ha definido ConnectionString en FSDatabase.Contants");

                ConnStringEntryId = 0;
            }
            else
            {
                ProviderName = ConfigurationManager.ConnectionStrings[ConnStringEntryName].ProviderName;
                ConnString = ConfigurationManager.ConnectionStrings[ConnStringEntryName].ConnectionString;

                ConnStringEntryId = Utils.GetConnectionId(ConnStringEntryName);
            }

            SetVariables();
            SetBDType();
            SetDBMSType();
        }

        public BdUtils(string connectionString, string providerName)
        {
            ConnString = connectionString;
            ProviderName = providerName;

            SetVariables();
            SetBDType();
            SetDBMSType();
        }


        public BdUtils(ConnectionStringSettings connectionString)
        {
            ConnString = connectionString.ConnectionString;
            ProviderName = connectionString.ProviderName;

            SetVariables();
            SetBDType();
            SetDBMSType();
        }


        public BdUtils(string connectionString, Utils.ServerTypeEnum typeBd)
        {
            ConnString = connectionString;
            Utils.ServerType = typeBd;

            SetVariables();
            SetDBMSType();
        }

        public BdUtils(string connStringEntryName)
        {
            if (String.IsNullOrEmpty(connStringEntryName))
                throw new Exception("Nombre de conexión incorrecta.");
            else
                ConnStringEntryName = connStringEntryName;

            if (ConfigurationManager.ConnectionStrings.Count == 0)
                throw new Exception("No ay entradas ConnectionStrings en el fichero web.config");
            if (ConfigurationManager.ConnectionStrings[ConnStringEntryName] == null)
                throw new ExceptionUtil("No se ha encontrado la entrada: " + ConnStringEntryName + ", en web.config.");

            ProviderName = ConfigurationManager.ConnectionStrings[ConnStringEntryName].ProviderName;
            ConnString = ConfigurationManager.ConnectionStrings[ConnStringEntryName].ConnectionString;

            ConnStringEntryId = Utils.GetConnectionId(ConnStringEntryName);

            SetVariables();
            SetBDType();
            SetDBMSType();
        }

        public BdUtils(int connStringEntryId)
        {
            ConnStringEntryId = connStringEntryId;
            ConnStringEntryName = Utils.GetConnectionName(ConnStringEntryId);

            if (ConfigurationManager.ConnectionStrings.Count == 0)
                throw new Exception("No ay entradas ConnectionStrings en el fichero web.config");
            if (ConfigurationManager.ConnectionStrings[ConnStringEntryName] == null)
                throw new ExceptionUtil("No se ha encontrado la entrada: " + ConnStringEntryName + ", en web.config.");

            ProviderName = ConfigurationManager.ConnectionStrings[ConnStringEntryName].ProviderName;
            ConnString = ConfigurationManager.ConnectionStrings[ConnStringEntryName].ConnectionString;

            SetVariables();
            SetBDType();
            SetDBMSType();
        }

        //public void Close()
        //{
        //    if (m_connection != null)
        //    {
        //        try
        //        {
        //            m_connection.Close();
        //        }
        //        catch
        //        {
        //            m_connection.Dispose();
        //        }
        //    }
        //}

        private void SetVariables()
        {
            if (TextUtil.IndexOf(ConnString, "{root}") > 0)
                ConnString = TextUtil.Replace(ConnString, "{root}", Web.ServerMapPath("~"));

            if (TextUtil.IndexOf(ConnString, "{portal}") > 0)
                ConnString = TextUtil.Replace(ConnString, "{portal}",
                    ConfigurationManager.AppSettings["DefaultPortal"]);

            if (TextUtil.IndexOf(ConnString, "{app_path}") > 0)
                ConnString = TextUtil.Replace(ConnString, "{app_path}", FileUtils.ApplicationPath());

            //inicialiamos la variable SimbDate
            if (Utils.ServerType == Utils.ServerTypeEnum.Oledb || Utils.ServerType == Utils.ServerTypeEnum.Odbc)
                Utils.m_simbDate = "#";
            else
                Utils.m_simbDate = "'";
        }


        public int GetConnectionId()
        {
            return Utils.GetConnectionId(ConnStringEntryName);
        }

        private void SetBDType()
        {
            if (TextUtil.IndexOf(ProviderName, "sqlclient") >= 0 || TextUtil.IndexOf(ConnString, "sqloledb") >= 0)
            {
                Utils.ServerType = Utils.ServerTypeEnum.SQLServer;
            }


            if (TextUtil.IndexOf(ProviderName, "mysql") >= 0)
            {
                Utils.ServerType = Utils.ServerTypeEnum.MySQL;
            }

            if (TextUtil.IndexOf(ProviderName, "oracle") >= 0)
            {
                Utils.ServerType = Utils.ServerTypeEnum.Oracle;
            }

            if (TextUtil.IndexOf(ProviderName, "odbc") >= 0)
            {
                Utils.ServerType = Utils.ServerTypeEnum.Odbc;
            }

            if (TextUtil.IndexOf(ProviderName, "oledb") >= 0)
            {
                Utils.ServerType = Utils.ServerTypeEnum.Oledb;
            }

            if (TextUtil.IndexOf(ProviderName, "sqlite") >= 0)
            {
                Utils.ServerType = Utils.ServerTypeEnum.SQLite;
            }
        }

        /// <summary>
        /// Establecemos el tipo de base de datos para la libreria FSQueryBuilder
        /// </summary>
        private void SetDBMSType()
        {
            if (Utils.ServerType == Utils.ServerTypeEnum.SQLServer)
            {
                Dbms.dbmsType = DBMSType.SQLServer;
            }


            if (Utils.ServerType == Utils.ServerTypeEnum.MySQL)
            {
                Dbms.dbmsType = DBMSType.MySQL;
            }

            if (Utils.ServerType == Utils.ServerTypeEnum.Oracle)
            {
                Dbms.dbmsType = DBMSType.Oracle;
            }

            if (Utils.ServerType == Utils.ServerTypeEnum.Odbc)
            {
                Dbms.dbmsType = DBMSType.Odbc;
            }

            if (Utils.ServerType == Utils.ServerTypeEnum.Oledb)
            {
                Dbms.dbmsType = DBMSType.Access;
            }

            if (Utils.ServerType == Utils.ServerTypeEnum.SQLite)
            {
                Dbms.dbmsType = DBMSType.SQLite;
            }
        }


        /// <summary>
        ///     Provider Factory
        /// </summary>
        public DbProviderFactory ProviderFactory()
        {
            if (m_providerFactory != null)
                return m_providerFactory;


            if (String.IsNullOrEmpty(ProviderName))
                throw new ExceptionUtil("No se ha definido el nombre del proveedor.");

            //DbProviderFactories.RegisterFactory("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);
            //DbProviderFactories.RegisterFactory("MySql.Data.MySqlClient", MySql.Data.MySqlClient.MySqlClientFactory.Instance);
            //DbProviderFactories.RegisterFactory("Npgsql", Npgsql.NpgsqlFactory.Instance);
            //DbProviderFactories.RegisterFactory("Oracle.ManagedDataAccess.Client", Oracle.ManagedDataAccess.Client.OracleClientFactory.Instance);
            //DbProviderFactories.RegisterFactory("System.Data.SQLite.EF6", System.Data.SQLite.EF6.SQLiteProviderFactory.Instance);
            //DbProviderFactories.RegisterFactory("System.Data.SQLite", System.Data.SQLite.SQLiteFactory.Instance);

            m_providerFactory = DbProviderFactories.GetFactory(ProviderName);

            return m_providerFactory;
        }


        public DbConnection Connection()
        {
            if (String.IsNullOrEmpty(ConnString))
                throw new ExceptionUtil("No se ha definido la cadena de conexión.");

            DbConnection dbConnection = ProviderFactory().CreateConnection();
            dbConnection.ConnectionString = ConnString;
            dbConnection.Open();

            return dbConnection;
        }


        /// <summary>
        ///     Comienza la transacción
        /// </summary>
        public void BeginTransaction()
        {
            try
            {
                var conn = Connection();
                if (Transaction == null)
                    Transaction = conn.BeginTransaction();
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        /// <summary>
        ///     Finaliza la transacción
        /// </summary>
        public void CommitTransaction()
        {
            if (Transaction != null)
                if (Transaction.Connection != null)
                    Transaction.Commit();
        }

        /// <summary>
        ///     Cancela la transacción
        /// </summary>
        public void RollbackTransaction()
        {
            if (Transaction != null)
                if (Transaction.Connection != null)
                    Transaction.Rollback();
        }


        /// <summary>
        ///     Limpiamos el Log de SQL Server.
        /// </summary>
        /// <returns></returns>
        public bool ClearLog()
        {
            try
            {
                using (DbConnection conn = Connection())
                {
                    Execute("USE " + conn.DataSource);
                    Execute("CHECKPOINT");
                    Execute("DUMP TRANSACTION " + conn.DataSource + " WITH TRUNCATE_ONLY");
                    Execute("DBCC SHRINKFILE (" + conn.Database + "_log)");
                    return true;
                }
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        /// <summary>
        ///     Devuelve true/false si existe la tabla en los esquemas.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="connString"></param>
        /// <returns></returns>
        public bool TableExists(string tableName)
        {
            var sch = GetSchemaTables(false, false);
            var dr = sch.Select("TABLE_NAME='" + tableName + "'");

            if (dr.Length == 0)
                return false;
            return true;
        }


        /// <summary>
        ///     Devuelve true/false si existe el campo en la tabla indicada
        /// </summary>
        /// ///
        /// <param name="fieldName"></param>
        /// <param name="tableNameName"></param>
        /// <param name="connString"></param>
        /// <returns></returns>
        public bool FieldExists(string fieldName, string tableName)
        {
            var sch = GetSchemaTable(tableName);
            var dr = sch.Select("ColumnName='" + fieldName + "'");

            if (dr.Length == 0)
                return false;
            return true;
        }


        public bool IsControlField(string campo)
        {
            if (campo.ToUpper() == "FECHAMODIFICACION" ||
                campo.ToUpper() == "FECHACREACION" ||
                campo.ToUpper() == "USUARIOMODIFICACION" ||
                campo.ToUpper() == "USUARIOCREACION")
                return true;
            return false;
        }


        /// <summary>
        ///     Devuelve la versión del servidor de datos
        /// </summary>
        /// <returns></returns>
        public string ServerVersion()
        {
            try
            {
                using (DbConnection conn = Connection())
                {
                    return conn.ServerVersion;
                }
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }

        /// <summary>
        ///     Devuelve un datatable con los esquemas
        /// </summary>
        /// <returns></returns>
        public DataTable GetSchemaTables()
        {
            return GetSchemaTables(true, false);
        }

        /// <summary>
        ///     Devuelve un datatable con los esquemas de la bd
        /// </summary>
        /// <param name="sort"></param>
        /// <param name="connString"></param>
        /// <returns></returns>
        public DataTable GetSchemaTables(bool sort, bool countTotalRows)
        {
            try
            {
                DataTable dt;
                dt = (DataTable) Web.GetCacheValue("cacheSchemaTable_ID" + ConnStringEntryId);
                if (dt != null)
                    return dt;


                Log.TraceInfo("DB:GetSchemaTable");

                string[] restrictions;
                if (Utils.ServerType == Utils.ServerTypeEnum.Oracle)
                {
                    restrictions = new string[] {null};
                }
                else
                {
                    restrictions = new string[4];
                    restrictions[0] = null;
                    restrictions[1] = null;
                    restrictions[2] = null;
                    restrictions[3] = "TABLE";
                }

                using (DbConnection conn = Connection())
                {

                    dt = Utils.ServerType == Utils.ServerTypeEnum.Oledb || Utils.ServerType == Utils.ServerTypeEnum.Oracle
                        ? conn.GetSchema("Tables", restrictions)
                        : conn.GetSchema("Tables");

                    if (countTotalRows)
                    {
                        //añadimos una nueva columna para guardar el total de registros
                        dt.Columns.Add(new DataColumn("TOTAL_ROWS"));
                    }

                    for (var i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        var r = dt.Rows[i];

                        var add = true;
                        string schema;
                        if (Utils.ServerType == Utils.ServerTypeEnum.Oracle)
                            schema = Functions.Valor(dt.Rows[i]["OWNER"]);
                        else
                            schema = Functions.Valor(r["TABLE_SCHEMA"]);
                        if (schema != "")
                            schema += ".";
                        var tableName = schema + r["TABLE_NAME"];
                        if (Utils.ServerType == Utils.ServerTypeEnum.MySQL)
                            add = Functions.Valor(r["TABLE_SCHEMA"]).ToLower() == "portalnet";
                        if (Utils.ServerType == Utils.ServerTypeEnum.Access2000 || Utils.ServerType == Utils.ServerTypeEnum.Access97)
                            add = TextUtil.Substring(r["TABLE_NAME"].ToString(), 0, 4) != "MSys";
                        if (Utils.ServerType == Utils.ServerTypeEnum.Oracle)
                            add = Functions.Valor(r["TYPE"]).ToLower() == "user";

                        r["TABLE_NAME"] = tableName;

                        if(countTotalRows)
                            r["TOTAL_ROWS"] = Counter(tableName);

                        if (!add) r.Delete();
                    }

                    dt.AcceptChanges();

                    if (sort)
                        dt = Utils.GetSortDataTable(dt, "", "TABLE_NAME");

                    Web.SetCacheValue("cacheSchemaTable_ID" + ConnStringEntryId, dt);

                    return dt;
                }
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public DataTable GetSchemaTable(string tableName)
        {
            try
            {
                var dt = (DataTable) Web.GetCacheValue("SchemaColumn_" + tableName + "_ID" + ConnStringEntryId);
                if (dt != null)
                    return dt;

                Log.TraceInfo("DB:GetSchemaColumn: " + tableName);

                using (DbConnection conn = Connection())
                {
                    using (var cmd = ProviderFactory().CreateCommand())
                    {
                        if (Transaction != null)
                            cmd.Transaction = Transaction;

                        cmd.CommandText = "select * from " + tableName;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;
                        var rdr = cmd.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo);
                        dt = rdr.GetSchemaTable();

                        //if (dt == null)
                        //    dt = GetSchemaColumnV2(tableName);

                        if (dt != null)
                            Web.SetCacheValue("SchemaColumn_" + tableName + "_ID" + ConnStringEntryId, dt);

                        rdr.Dispose();

                        return dt;
                    }
                }
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }

        private DataTable GetSchemaTableV2(string tableName)
        {
            try
            {
                if (string.IsNullOrEmpty(tableName))
                    return null;

                var dt = (DataTable) Web.GetCacheValue("SchemaColumnV2_" + tableName + "_ID" + ConnStringEntryId);
                if (dt != null)
                    return dt;

                Log.TraceInfo("DB:GetSchemaColumnV2: " + tableName);

                var restrictions = new string[3];
                var columnOrder = "ORDINAL_POSITION";

                restrictions[0] = null;
                restrictions[1] = null;
                if (Utils.ServerType == Utils.ServerTypeEnum.Oracle)
                {
                    restrictions[2] = tableName.IndexOf(".") > 0 ? tableName.Split('.')[1] : tableName;
                    columnOrder = "";
                }
                else
                {
                    restrictions[2] = tableName;
                }
                //restrictions[3] = null;

                using (DbConnection conn = Connection())
                {
                    dt = conn.GetSchema("Columns", restrictions);
                    dt = Utils.GetSortDataTable(dt, "", columnOrder);

                    Web.SetCacheValue("SchemaColumnV2_" + tableName + "_ID" + ConnStringEntryId, dt);
                    return dt;
                }
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public DataTable GetSchemaForeignKeys()
        {
            try
            {
                var dt = (DataTable) Web.GetCacheValue("cacheSchemaForeignKeys_ID" + ConnStringEntryId);
                if (dt != null)
                    return dt;

                Log.TraceInfo("DB:GetSchemaForeignKeys");

                if (Utils.ServerType == Utils.ServerTypeEnum.Oledb)
                {
                    using (var conn2 = new OleDbConnection(ConnString))
                    {
                        conn2.Open();

                        dt = conn2.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, null);
                    }
                }
                else if (Utils.ServerType == Utils.ServerTypeEnum.SQLServer)
                {
                    dt = GetSchemaForeignKeysSql();
                }
                else if (Utils.ServerType == Utils.ServerTypeEnum.Oracle)
                {
                    dt = GetSchemaForeignKeysOracle();
                }
                else
                {
                    dt = Execute("select fk_table_name,pk_table_name,fk_column_name,pk_table_name from " +
                                    TablePrefix +
                                    "relaciones");
                }

                Web.SetCacheValue("cacheSchemaForeignKeys_ID" + ConnStringEntryId, dt);
                return dt;
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }

        /// <summary>
        ///     Obtenemos una tabla equivalente a la función GetSchemaForeignKeys, de OleDb.
        /// </summary>
        /// <returns></returns>
        public DataTable GetSchemaForeignKeysSql()
        {
            try
            {
                //Information_Schema.TABLES
                //Information_Schema.TABLE_CONSTRAINTS
                //Information_Schema.KEY_COLUMN_USAGE
                //Information_Schema.CONSTRAINT_COLUMN_USAGE

                var dt = (DataTable) Web.GetCacheValue("cacheSchemaForeignKeysSql_ID" + ConnStringEntryId);
                if (dt != null)
                    return dt;

                dt = Execute(@"SELECT ccu.table_name AS FK_TABLE_NAME,
                                ccu.column_name AS FK_COLUMN_NAME,
                                rc.constraint_name AS PK_COLUMN_NAME,
                                ccu2.table_name AS PK_TABLE_NAME
                            FROM information_schema.constraint_column_usage ccu 
                            JOIN information_schema.referential_constraints rc ON ccu.constraint_name=rc.constraint_name
                            JOIN information_schema.constraint_column_usage ccu2 ON rc.unique_constraint_name=ccu2.constraint_name");

                Web.SetCacheValue("cacheSchemaForeignKeysSql_ID" + ConnStringEntryId, dt);
                return dt;
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public string[] GetTables()
        {
            var schema = GetSchemaTables(true, false);
            var tables = new string[schema.Rows.Count];
            var pos = 0;
            foreach (DataRow row in schema.Rows)
            {
                tables[pos] = row["TABLE_NAME"].ToString();
                pos++;
            }

            return tables;
        }


        /// <summary>
        ///     Obtenemos una tabla equivalente a la función GetSchemaForeignKeys, de Oracle.
        /// </summary>
        /// <returns></returns>
        public DataTable GetSchemaForeignKeysOracle()
        {
            try
            {
                var dt = (DataTable) Web.GetCacheValue("cacheSchemaForeignKeysOracle_ID" + ConnStringEntryId);
                if (dt != null)
                    return dt;

                dt = Execute(@"SELECT a.table_name FK_TABLE_NAME, 
                            a.column_name FK_COLUMN_NAME,
                            c_pk.constraint_name PK_COLUMN_NAME,
                            c_pk.table_name PK_TABLE_NAME
                       FROM all_cons_columns a
                    JOIN all_constraints c ON a.owner = c.owner
                        AND a.constraint_name = c.constraint_name
                    JOIN all_constraints c_pk ON c.r_owner = c_pk.owner
                           AND c.r_constraint_name = c_pk.constraint_name
                    WHERE c.constraint_type = 'R'");

                Web.SetCacheValue("cacheSchemaForeignKeysOracle_ID" + ConnStringEntryId, dt);
                return dt;
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public DataTable GetPrimaryKeys()
        {
            try
            {
                Log.TraceInfo("DB:GetPrimaryKeys");

                var dt = (DataTable) Web.GetCacheValue("cachePrimaryKeys_ID" + ConnStringEntryId);
                if (dt != null)
                    return dt;

                using (DbConnection conn = Connection())
                {
                    dt = conn.GetSchema("Indexes");

                    Web.SetCacheValue("cachePrimaryKeys_ID" + ConnStringEntryId, dt);
                    return dt;
                }
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }

        /// <summary>
        ///     Recuperamos el campo indice primario que servirá para relacionar la tabla e incluirlo en un combo
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string GetTablePrimaryKey(string tableName)
        {
            try
            {
                var dtColumns = GetSchemaTable(tableName);
                foreach (DataRow r in dtColumns.Rows)
                    if (r["IsKey"].ToString().ToLower() == "true")
                        return r["ColumnName"].ToString();
                return "<Sin key>";
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }

        /// <summary>
        ///     Recuperamos el campo que describira la tabla para mostrarlo en un combo
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string GetTableDescriptionField(string tableName)
        {
            try
            {
                var dtColumns = GetSchemaTable(tableName);
                foreach (DataRow r in dtColumns.Rows)
                    if (r["DataType"].ToString().ToLower() == "system.string" && Convert.ToInt32(r["ColumnSize"]) >= 10)
                        return r["ColumnName"].ToString();
                return "<Sin desc>";
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        /// <summary>
        ///     Recuperamos los esquemas de Oracle de tipo "User"
        /// </summary>
        /// <returns></returns>
        public string[] GetOracleSchemas()
        {
            try
            {
                var schemas = (List<string>) Web.GetCacheValue("cacheOracleSchemas");
                if (schemas != null)
                    return schemas.ToArray();

                schemas = new List<string>();
                DataTable tables;

                using (DbConnection conn = Connection())
                {
                    tables = conn.GetSchema("Tables");

                    foreach (DataRow r in tables.Rows)
                    {
                        var schema = Functions.Valor(r["OWNER"]);
                        if (r["TYPE"].ToString().ToLower() == "user")
                            if (!schemas.Contains(schema))
                                schemas.Add(schema);
                    }

                    Web.SetCacheValue("cacheOracleSchemas", schemas);
                    return schemas.ToArray();
                }
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        /// <summary>
        ///     Recuperamos del esquema las vistas
        /// </summary>
        /// <returns></returns>
        public DataTable GetSchemaView()
        {
            try
            {
                Log.TraceInfo("DB:GetSchemaView");

                var dt = (DataTable) Web.GetCacheValue("cacheSchemaView_ID" + ConnStringEntryId);
                if (dt != null)
                    return dt;

                using (DbConnection conn = Connection())
                {
                    dt = conn.GetSchema("Views");

                    // en Oracle solo mostramos las vistas que pertenezcan a esquemas de tipo "User"
                    if (Utils.ServerType == Utils.ServerTypeEnum.Oracle)
                    {
                        var schemas = GetOracleSchemas();

                        //eliminamos las consultas de sistema
                        for (var i = dt.Rows.Count - 1; i >= 0; i--)
                        {
                            var r = dt.Rows[i];
                            if (!Functions.Existe(schemas, r["OWNER"].ToString()))
                                r.Delete();
                            else
                                r["VIEW_NAME"] = r["OWNER"] + "." + r["VIEW_NAME"];
                        }

                        dt.AcceptChanges();

                        Web.SetCacheValue("cacheSchemaView_ID" + ConnStringEntryId, dt);
                    }

                    return dt;
                }
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }

        /// <summary>
        ///     Recuperamos del esquema las columnas con sus propiedades
        /// </summary>
        /// <returns></returns>
        public DataTable GetSchemaViewColumns()
        {
            try
            {
                Log.TraceInfo("DB:GetSchemaViewColumns");

                var dt = (DataTable) Web.GetCacheValue("cacheSchemaViewColumns_ID" + ConnStringEntryId);
                if (dt != null)
                    return dt;

                using (DbConnection conn = Connection())
                {
                    dt = conn.GetSchema("ViewColumns");

                    Web.SetCacheValue("cacheSchemaViewColumns_ID" + ConnStringEntryId, dt);
                    return dt;
                }
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        /// <summary>
        ///     Recuperamos la cadena de conexión
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            try
            {
                using (DbConnection conn = Connection())
                {
                    return conn.ConnectionString;
                }
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        /// <summary>
        ///     Ejecutamos un DbCommand
        /// </summary>
        /// <param name="dbc"></param>
        /// <returns></returns>
        public DataTable Execute(DbCommand dbc)
        {
            try
            {
                var dt = new DataTable();

                Log.TraceInfo("DB:Execute: ");

                using (DbConnection conn = Connection())
                {
                    dbc.Connection = conn;

                    if (Transaction != null)
                        dbc.Transaction = Transaction;

                    using (var da = ProviderFactory().CreateDataAdapter())
                    {
                        da.SelectCommand = dbc;

                        dt.BeginLoadData();
                        da.Fill(dt);
                        dt.EndLoadData();

                        da.Dispose();
                        dbc.Dispose();

                        return dt;
                    }
                }
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public DataTable Execute(string sql)
        {
            return Execute(sql, false);
        }

        /// <summary>
        ///     Ejecutamos la sentencia Sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable Execute(string sql, bool loadSchema)
        {
            try
            {
                var dt = new DataTable();
                dt.TableName = Utils.GetTableName(sql);

                sql = Utils.FormatSQL(sql);
                sql = HttpUtility.HtmlDecode(sql);

                Log.TraceInfo("DB:Execute: " + sql);

                using (DbConnection conn = Connection())
                {
                    using (var dbc = ProviderFactory().CreateCommand())
                    {
                        if (Transaction != null)
                            dbc.Transaction = Transaction;

                        dbc.Connection = conn;
                        dbc.CommandType = CommandType.Text;
                        dbc.CommandText = sql;

                        using (var da = ProviderFactory().CreateDataAdapter())
                        {
                            da.SelectCommand = dbc;

                            dt.BeginLoadData();
                            if (loadSchema)
                                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                            da.Fill(dt);
                            dt.EndLoadData();

                            da.Dispose();
                            dbc.Dispose();

                            return dt;
                        }
                    }
                }
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        /// <summary>
        ///     Obtenemos los registros de una Tabla, paginados y ordenados
        /// </summary>
        /// <param name="sTableName"></param>
        /// <param name="iPage"></param>
        /// <param name="sWhere"></param>
        /// <param name="sOrderBy"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public DataTable Execute(string sTableName, int iPage, string sWhere, string sOrderBy, int iPageSize)
        {
            try
            {
                string sql;
                var sOrderByInv = "";

                var dt = new DataTable();

                Log.TraceInfo(
                    "DB:Execute: " + sTableName + " ,iPage: " + iPage + " ,sWhere: " + sWhere + " ,sOrderBy: " +
                    sOrderBy +
                    " ,iPageSize: " + iPageSize);

                if (iPage == 0) iPage = 1;

                if (sOrderBy == "")
                {
                    sOrderBy = " ORDER BY 1 ASC ";
                }
                else
                {
                    if (TextUtil.IndexOf(sOrderBy, "order") == 0) sOrderBy = " ORDER BY " + sOrderBy;

                    if ((TextUtil.IndexOf(sOrderBy, " asc") == 0) & (TextUtil.IndexOf(sOrderBy, " desc") == 0))
                    {
                        sOrderBy += " ASC";
                        sOrderBy = TextUtil.Replace(sOrderBy, ",", " ASC,");
                    }
                }

                if (TextUtil.IndexOf(sOrderBy, " asc") > 0) sOrderByInv = TextUtil.Replace(sOrderBy, " asc", " DESC");
                if (TextUtil.IndexOf(sOrderBy, " desc") > 0) sOrderByInv = TextUtil.Replace(sOrderBy, " desc", " ASC");

                if (sWhere != "")
                {
                    if (TextUtil.IndexOf(sOrderBy, "where") == 0) sWhere = " WHERE " + sWhere;
                    sOrderBy = sWhere + " " + sOrderBy;
                }

                if (Utils.ServerType == Utils.ServerTypeEnum.MySQL || Utils.ServerType == Utils.ServerTypeEnum.SQLite)
                    sql = "select * from ( select * from (select * from " + sTableName + sOrderBy + " limit " +
                          iPageSize * iPage + ") " + sOrderByInv + " limit " + iPageSize + ") as " + sOrderBy;
                else
                    sql = "select * from ( select top " + iPageSize + " * from (select top " + iPageSize * iPage +
                          " * from " +
                          sTableName + " " + sOrderBy + ") " + sOrderByInv + ") " + sOrderBy;
                if (Utils.ServerType == Utils.ServerTypeEnum.Oracle)
                    sql = "select * from ( select * from (select * from " +
                          sTableName + " " + sOrderBy + ") WHERE ROWNUM <=" + iPageSize * iPage + " " + sOrderByInv +
                          ") WHERE ROWNUM <=" + iPageSize + sOrderBy;

                using (DbConnection conn = Connection())
                {
                    using (var comtmp = ProviderFactory().CreateCommand())
                    {
                        if (Transaction != null)
                            comtmp.Transaction = Transaction;

                        comtmp.Connection = conn;
                        comtmp.CommandType = CommandType.Text;

                        sql = Utils.FormatSQL(sql);

                        comtmp.CommandText = HttpUtility.HtmlDecode(sql);

                        using (var da = ProviderFactory().CreateDataAdapter())
                        {
                            da.SelectCommand = comtmp;

                            dt.BeginLoadData();
                            da.Fill(dt);
                            dt.EndLoadData();

                            da.Dispose();
                            comtmp.Dispose();

                            return dt;
                        }
                    }
                }
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        /// <summary>
        ///     Obetenemos la página indicada de la sentencia Sql
        /// </summary>
        /// <param name="sSql"></param>
        /// <param name="iPage"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public DataTable Execute(string sSql, int iPage, int iPageSize)
        {
            try
            {
                string sql;
                string sql2 = sSql;
                string sOrderBy = "";
                string sOrderByInv = "";

                DataTable dt = new DataTable();

                sql2 = Utils.FormatSQL(sql2);

                if (iPage == 0) iPage = 1;

                sql2 = TextUtil.Replace(sql2, ";", "");

                if (Utils.ServerType != Utils.ServerTypeEnum.MySQL) sql2 = TextUtil.Replace(sql2, "select", "select top " + iPageSize * iPage);

                int iOrder = TextUtil.IndexOf(sql2, "order by");

                if (iOrder > 0)
                {
                    sOrderBy = TextUtil.Substring(sql2, iOrder);
                    sql2 = TextUtil.Substring(sql2, 0, iOrder);
                }

                if ((TextUtil.IndexOf(sOrderBy, " asc") == 0) & (TextUtil.IndexOf(sOrderBy, " desc") == 0))
                {
                    sOrderBy += " ASC";
                    sOrderBy = TextUtil.Replace(sOrderBy, ",", " ASC,");
                }

                if (TextUtil.IndexOf(sOrderBy, " asc") > 0) sOrderByInv = TextUtil.Replace(sOrderBy, " asc", " DESC");
                if (TextUtil.IndexOf(sOrderBy, " desc") > 0) sOrderByInv = TextUtil.Replace(sOrderBy, " desc", " ASC");

                if (Utils.ServerType == Utils.ServerTypeEnum.MySQL)
                    sql = "select * from ( select * from (" + sql2 + sOrderBy + " limit " + iPageSize * iPage + ") p " +
                          sOrderByInv + " limit " + iPageSize + ") q " + sOrderBy;
                else
                    sql = "select * from ( select top " + iPageSize + " * from (" + sql2 + sOrderBy + ") p " +
                          sOrderByInv +
                          ") q " + sOrderBy;

                using (DbConnection conn = Connection())
                {
                    using (var comtmp = ProviderFactory().CreateCommand())
                    {
                        if (Transaction != null)
                            comtmp.Transaction = Transaction;

                        comtmp.Connection = conn;
                        comtmp.CommandType = CommandType.Text;
                        comtmp.CommandText = HttpUtility.HtmlDecode(sql);

                        Log.TraceInfo(
                            "DB:Execute: " + comtmp.CommandText + " ,iPage: " + iPage + " ,iPageSize: " + iPageSize);


                        using (var da = ProviderFactory().CreateDataAdapter())
                        {
                            da.SelectCommand = comtmp;

                            dt.BeginLoadData();
                            da.Fill(dt);
                            dt.EndLoadData();

                            da.Dispose();
                            comtmp.Dispose();

                            return dt;
                        }
                    }
                }
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        /// <summary>
        ///     Ejecutamos una consulta Sql, y devolvemos el resultado de esta. Este mecanismo se suele utilizar cuando la consulta
        ///     devuelve un campo con un total, por ejemplo.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sql)
        {
            try
            {
                sql = Utils.FormatSQL(sql);

                using (DbConnection conn = Connection())
                {
                    using (var comtmp = ProviderFactory().CreateCommand())
                    {
                        if (Transaction != null)
                            comtmp.Transaction = Transaction;

                        comtmp.Connection = conn;
                        comtmp.CommandType = CommandType.Text;
                        comtmp.CommandText = HttpUtility.HtmlDecode(sql);

                        Log.TraceInfo("DB:ExecuteScalar: " + comtmp.CommandText);

                        var result = comtmp.ExecuteScalar();

                        comtmp.Dispose();

                        return result;
                    }
                }
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }

        /// <summary>
        ///     Recuperamos el identificativo del último registro introducido en SQL Server.
        /// </summary>
        /// <returns></returns>
        public string GetIdentity()
        {
            try
            {
                if (Utils.ServerType == Utils.ServerTypeEnum.SQLServer)
                    return ExecuteScalar("select @@IDENTITY").ToString();
                return null;
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        /// <summary>
        ///     Recuperamos el identificativo del último registro introducido en SQL Server. Otra manera de realizarlo.
        /// </summary>
        /// <returns></returns>
        public string GetScopeIdentity()
        {
            try
            {
                if (Utils.ServerType == Utils.ServerTypeEnum.SQLServer)
                    return ExecuteScalar("select SCOPE_IDENTITY()").ToString();
                return null;
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        /// <summary>
        ///     Recuperamoss en un DataSet, el resultado de una sentencia SQL.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string sql)
        {
            try
            {
                sql = Utils.FormatSQL(sql);

                using (DbConnection conn = Connection())
                {
                    using (var comtmp = ProviderFactory().CreateCommand())
                    {
                        if (Transaction != null)
                            comtmp.Transaction = Transaction;

                        comtmp.Connection = conn;
                        comtmp.CommandType = CommandType.Text;
                        comtmp.CommandText = HttpUtility.HtmlDecode(sql);

                        Log.TraceInfo("DB:ExecuteDataSet: " + comtmp.CommandText);

                        using (var da = ProviderFactory().CreateDataAdapter())
                        {
                            var ds = new DataSet();
                            da.SelectCommand = comtmp;

                            da.Fill(ds);

                            da.Dispose();

                            return ds;
                        }
                    }
                }
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        /// <summary>
        ///     Ejecutamos una sentencia SQL, obteniendo únicamente el número de registros afectados. Util en comandos de tipo
        ///     INSERT, UPDATE o DELETE.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql)
        {
            try
            {
                sql = Utils.FormatSQL(sql);

                using (DbConnection conn = Connection())
                {
                    using (var comtmp = ProviderFactory().CreateCommand())
                    {
                        if (Transaction != null)
                            comtmp.Transaction = Transaction;

                        comtmp.Connection = conn;
                        comtmp.CommandType = CommandType.Text;
                        comtmp.CommandText = HttpUtility.HtmlDecode(sql);

                        Log.TraceInfo("DB:ExecuteNonQuery: " + comtmp.CommandText);

                        var ret = comtmp.ExecuteNonQuery();
                        comtmp.Dispose();

                        return ret;
                    }
                }
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public string ExecuteReaderSingleRow(string sql, string field)
        {
            try
            {
                var retValue = "";
                sql = Utils.FormatSQL(sql);

                using (DbConnection conn = Connection())
                {
                    using (var comtmp = ProviderFactory().CreateCommand())
                    {
                        if (Transaction != null)
                            comtmp.Transaction = Transaction;

                        comtmp.Connection = conn;
                        comtmp.CommandType = CommandType.Text;
                        comtmp.CommandText = HttpUtility.HtmlDecode(sql);

                        using (var dr = comtmp.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (dr.Read()) retValue = dr[field].ToString();
                            dr.Close();
                            comtmp.Dispose();

                            return retValue;
                        }
                    }
                }
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public DbDataReader ExecuteReader(string sql)
        {
            try
            {
                sql = Utils.FormatSQL(sql);

                using (DbConnection conn = Connection())
                {
                    using (var comtmp = ProviderFactory().CreateCommand())
                    {
                        if (Transaction != null)
                            comtmp.Transaction = Transaction;

                        comtmp.Connection = conn;
                        comtmp.CommandType = CommandType.Text;
                        comtmp.CommandText = HttpUtility.HtmlDecode(sql);

                        using (var dr = comtmp.ExecuteReader())
                        {
                            return dr;
                        }
                    }
                }
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        /// <summary>
        ///     Devuelve el número de registros de la tabla indicada o sentencia SELECT.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int GetRecordCount(string tableName)
        {
            try
            {
                if (TextUtil.IndexOf(tableName, "select") > 0)
                {
                    Log.TraceInfo("DB:GetRecordCount: " + tableName);

                    using (DbConnection conn = Connection())
                    {
                        using (var comm = ProviderFactory().CreateCommand())
                        {
                            if (Transaction != null)
                                comm.Transaction = Transaction;

                            comm.CommandType = CommandType.Text;
                            comm.CommandText = tableName;
                            comm.Connection = conn;

                            var rs = comm.ExecuteReader();
                            var tot = 0;
                            while (rs.Read()) tot += 1;
                            rs.Dispose();

                            comm.Dispose();

                            return tot;
                        }
                    }
                }

                var ssql = "select count(*) from " + tableName;
                return Convert.ToInt32(ExecuteScalar(ssql));
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        /// <summary>
        ///     Recuperamos el valor máximo de un campo en la tabla indicada o senetencia SELECT.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public int GetMaxValue(string tableName, string fieldName)
        {
            try
            {
                if (TextUtil.IndexOf(tableName, "select") > 0)
                {
                    Log.TraceInfo("DB:GetMaxValue: " + tableName + "," + fieldName);

                    using (DbConnection conn = Connection())
                    {
                        using (var comm = ProviderFactory().CreateCommand())
                        {
                            if (Transaction != null)
                                comm.Transaction = Transaction;

                            comm.CommandType = CommandType.Text;
                            comm.CommandText = tableName;
                            comm.Connection = conn;

                            var rs = comm.ExecuteReader();
                            var tot = 0;
                            while (rs.Read()) tot += 1;
                            rs.Dispose();

                            comm.Dispose();

                            return tot;
                        }
                    }
                }

                var ssql = "select max(" + fieldName + ") from " + tableName;
                var maxValue = ExecuteScalar(ssql);
                if (NumberUtils.IsNumeric(maxValue.ToString()))
                    return Convert.ToInt32(maxValue);
                else
                    return -1;
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public double MaxColumnSQL(string tableName, string column)
        {
            double max = 0;
            //double max2 = 0;
            try
            {
                max = NumberUtils.NumberDouble(ExecuteScalar("select max(" + column + ") from " + tableName));
                //max2 = MaxColumn(column);
                //if (max2 > max)
                //{
                //    max = max2;
                //}
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }

            return max;
        }


        public double MinColumnSQL(string tableName, string column)
        {
            double min;
            //double min2 = 0;
            try
            {
                min = NumberUtils.NumberDouble(ExecuteScalar("select min(" + column + ") from " + tableName));
                //min2 = MinColumn(column);
                //if (min2 < min)
                //{
                //    min = min2;
                //}
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }

            return min;
        }

        public double SumColumnSQL(string tableName, string column)
        {
            double sum;
            try
            {
                sum = NumberUtils.NumberDouble(ExecuteScalar("select sum(" + column + ") from " + tableName));
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }

            return sum;
        }


        public double AverageColumnSQL(string tableName, string column)
        {
            double avg;
            try
            {
                avg = NumberUtils.NumberDouble(ExecuteScalar("select avg(" + column + ") from " + tableName));
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }

            return avg;
        }


        public long LastInsertValue(string tableName, string idField)
        {
            var ssql = "Select @@IDENTITY";
            var id = Convert.ToInt64(ExecuteScalar(ssql));

            if (id == 0)
            {
                ssql = "select max(" + idField + ") from " + tableName;
                id = Convert.ToInt64(ExecuteScalar(ssql));
            }

            return id;
        }

        //        public string CompactJetDatabase(string fileName)
        //        {
        //            try
        //            {
        //                Functions.WriteTrace("DB:Compactar: " + fileName, MsgType.Info);
        //
        //                const string accessOleDbConnectionStringFormat = "Data Source={0};Provider=Microsoft.Jet.OLEDB.4.0;";
        //                string oldFileName = fileName;
        //                string newFileName = Path.Combine(Path.GetDirectoryName(oldFileName),
        //                    Guid.NewGuid().ToString("N") + ".mdb");
        //                JetEngine engine = ((JetEngine) (Variables.App.Page.Server.CreateObject("JRO.JetEngine")));
        //                engine.CompactDatabase(string.Format(accessOleDbConnectionStringFormat, oldFileName),
        //                    string.Format(accessOleDbConnectionStringFormat, newFileName));
        //                File.Delete(oldFileName);
        //                File.Move(newFileName, oldFileName);
        //                return String.Empty;
        //            }
        //            catch (System.Exception e)
        //            {
        //                throw new FSLibrary.Exception(e);
        //            }
        //        }

        public string Column(string tableName, int colPos)
        {
            try
            {
                var dtSchema = GetSchemaTable(tableName);

                return dtSchema.Rows[colPos]["ColumnName"].ToString();
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public bool ExisteValor(string campo, string valor, string tabla)
        {
            try
            {
                DataTable dataTable = Execute("select " + campo + " from " + tabla + " where " + campo + "='" + valor + "'");
                if (dataTable.Rows.Count > 0)
                    return true;
                return false;
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public Field GetField(string fieldName, string tableName)
        {
            return GetField(fieldName, GetSchemaTable(tableName));
        }


        public Field GetField(string fieldName, DataTable schema)
        {
            try
            {
                var field = new Field();

                if (schema == null)
                    return null;

                foreach (DataRow fld in schema.Rows)
                {
                    var c = Functions.Valor(fld["ColumnName"]);
                    if (c.ToLower() == fieldName.ToLower())
                    {
                        field.Campo = c;
                        field.Tipo = Utils.GetFSTypeFromType(Functions.Valor(fld["DataType"]));
                        field.Tamano = NumberUtils.NumberInt(fld["ColumnSize"]);
                        return field;
                    }
                }

                return null;
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public decimal FieldMaxValue(string fieldName)
        {
            var maxValue = decimal.MaxValue;
            var schema = GetSchemaTables();

            if (schema == null || schema.Columns[fieldName] == null) return maxValue;

            var typ = schema.Columns[fieldName].DataType.ToString();
            switch (typ.ToLower())
            {
                case "system.int16":
                    maxValue = short.MaxValue;
                    break;
                case "system.int32":
                    maxValue = int.MaxValue;
                    break;
                case "system.int64":
                    maxValue = long.MaxValue;
                    break;
            }

            return maxValue;
        }

        public int Counter(string tableName)
        {
            return NumberUtils.NumberInt(ExecuteScalar("select count(*) from " + tableName));
        }


        public string PathDb()
        {
            var l = ConnString.Length;
            var p = TextUtil.IndexOf(ConnString, "data source=");
            return TextUtil.Substring(ConnString, l - p - 11);
        }


        /// <summary>
        ///     Devuelve true si la tabla tiene relaciones.
        /// </summary>
        /// <param name="tabla"></param>
        /// <param name="campo"></param>
        /// <returns></returns>
        public bool HasRelation(string tabla, string campo)
        {
            try
            {
                if (tabla.IndexOf(".") > 0) tabla = tabla.Split('.')[1];

                var dtSchemaFk = GetSchemaForeignKeys();

                foreach (DataRow row in dtSchemaFk.Rows)
                    if ((Functions.Valor(row["FK_TABLE_NAME"]).ToLower() == tabla.ToLower()) &
                        (Functions.Valor(row["FK_Column_Name"]).ToLower() == campo.ToLower()))
                        return true;
                return false;
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        /// <summary>
        ///     Devuelve la tabla relacionada.
        /// </summary>
        /// <param name="tabla"></param>
        /// <param name="campo"></param>
        /// <returns></returns>
        public string RelationTable(string tabla, string campo)
        {
            try
            {
                var dtSchemaFk = GetSchemaForeignKeys();

                var schema = "";
                if (tabla.IndexOf(".") > 0)
                {
                    schema = tabla.Split('.')[0] + ".";
                    tabla = tabla.Split('.')[1];
                }

                foreach (DataRow row in dtSchemaFk.Rows)
                    if ((Functions.Valor(row["FK_TABLE_NAME"]).ToLower() == tabla.ToLower()) &
                        (Functions.Valor(row["FK_Column_Name"]).ToLower() == campo.ToLower()))
                        return schema + row["PK_TABLE_NAME"];
                return string.Empty;
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public string GetDescription(string tableName, string columnName)
        {
            try
            {
                var s = "";

                //if (Constants.BdType == BDType.Oledb) {
                var pk = new DataColumn[1];

                var dt = GetSchemaTableV2(tableName);
                pk[0] = dt.Columns["COLUMN_NAME"];

                dt.PrimaryKey = pk;

                var dr = dt.Rows.Find(columnName);

                if (dr != null)
                    switch (Utils.ServerType)
                    {
                        case Utils.ServerTypeEnum.MySQL:
                            s = Functions.ValorZero(dr["COLUMN_COMMENT"].ToString());
                            break;
                        case Utils.ServerTypeEnum.Odbc:
                            s = Functions.ValorZero(dr["REMARKS"].ToString());
                            break;
                        default:
                            s = Functions.ValorZero(dr["DESCRIPTION"].ToString());
                            break;
                    }

                if (s == "") s = columnName;
                //} else {
                //	s = columnName;
                //}

                return s;
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public string CreateTableSql(string table, HttpRequest frm, Register frmCampos)
        {
            try
            {
                var sFields = "";
                Field c = null;

                for (var f = 0; f <= frm.Form.Count - 1; f++)
                {
                    if (frmCampos != null) c = frmCampos.Find(frm.Form.Keys[f]);

                    if (c != null)
                        switch (c.Tipo)
                        {
                            case Utils.FieldTypeEnum.Number:
                                sFields = sFields + "[" + frm.Form.Keys[f] + "] int NULL,";
                                break;
                            case Utils.FieldTypeEnum.String:
                                sFields = sFields + "[" + frm.Form.Keys[f] + "] nvarchar(" + c.Tamano + ")" + " NULL,";
                                break;
                            case Utils.FieldTypeEnum.Boolean:
                                sFields = sFields + "[" + frm.Form.Keys[f] + "] bit NULL,";
                                break;
                            case Utils.FieldTypeEnum.DateTime:
                                sFields = sFields + "[" + frm.Form.Keys[f] + "] datetime NULL,";
                                break;
                        }
                    else
                        sFields = sFields + "[" + frm.Form.Keys[f] + "] nvarchar(50)" + " NULL,";
                }

                sFields = TextUtil.Substring(sFields, 0, TextUtil.Length(sFields) - 1);

                return "CREATE TABLE [" + table + "] (" + sFields + ")";
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public string InsertSql(string tableName, HttpRequest frm, Register frmCampos, int usuarioId = 0)
        {
            try
            {
                var sFields = "";
                var sData = "";
                Field c = null;

                var sch = GetSchemaTable(tableName);

                for (var f = 0; f <= frm.Form.Count - 1; f++)
                {
                    var campo = frm.Form.Keys[f];

                    if (!IsControlField(campo))
                        if (TextUtil.Substring(campo, 0, 3) != "cmd")
                        {
                            if (frmCampos != null) c = frmCampos.Find(campo);

                            if (c == null) c = GetField(campo, sch);

                            sFields = sFields + "[" + campo + "]" + ",";

                            var v = Functions.Valor(frm.Form.Get(f));
                            if (c != null)
                            {
                                if (c.Valor != "") v = c.Valor;

                                switch (c.Tipo)
                                {
                                    case Utils.FieldTypeEnum.Number:
                                        sData += NumberUtils.NumberDouble(v) + ",";
                                        break;
                                    case Utils.FieldTypeEnum.String:
                                        sData += "'" + TextUtil.Left(v.Trim(), c.Tamano) + "',";
                                        break;
                                    case Utils.FieldTypeEnum.Boolean:
                                        sData += Functions.ValorBool(v) + ",";
                                        break;
                                    case Utils.FieldTypeEnum.DateTime:
                                        if (!FSLibrary.DateTimeUtil.IsDate(v))
                                            sData += "Null,";
                                        else
                                            //DateTime.cDate2(DateTime.cDate3(v), DateFormat, DateSeparator)
                                            sData += Utils.FormatShortDate(Convert.ToDateTime(v)) + ",";
                                        break;
                                }
                            }
                            else
                            {
                                sData += "'" + frm.Form.Get(f).Trim() + "',";
                            }
                        }
                }

                if (HasUserData(tableName))
                {
                    //campos de seguimiento
                    sFields += "[FECHAMODIFICACION],";
                    sFields += "[FECHACREACION]";
                    sData += Utils.FormatShortDateTime(DateTime.Now) + ",";
                    sData += Utils.FormatShortDateTime(DateTime.Now);

                    if (usuarioId != 0)
                    {
                        sFields += ",[USUARIOMODIFICACION]";
                        sFields += ",[USUARIOCREACION]";
                        sData += "," + usuarioId;
                        sData += "," + usuarioId;
                    }
                }
                else
                {
                    sFields = sFields.Substring(0, sFields.Length - 1);
                    sData = sData.Substring(0, sData.Length - 1);
                }

                return "INSERT into [" + tableName + "] (" + sFields + ") VALUES (" + sData + ")";
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public string UpdateSql(string tableName, HttpRequest frm, Register frmCampos, string condicion, int usuarioId = 0)
        {
            try
            {
                var sFields = "";
                Field c = null;

                if (tableName == "")
                    throw new ExceptionUtil("Error! Valor de tabla no especificado en función 'UpdateSQL'.");

                if (frm.Form.Count == 0)
                    throw new ExceptionUtil("Error! El formulario no ha sido enviado correctamente.");

                var sch = GetSchemaTable(tableName);

                for (var f = 0; f <= frm.Form.Count - 1; f++)
                {
                    var campo = frm.Form.Keys[f];

                    if (!IsControlField(campo))
                        if (TextUtil.Substring(campo, 0, 3) != "cmd")
                        {
                            if (frmCampos != null) c = frmCampos.Find(campo);

                            var v = Functions.Valor(frm.Form.Get(f));

                            if (c == null) c = GetField(campo, sch);

                            if (c != null)
                            {
                                if (c.Valor != "") v = c.Valor;

                                switch (c.Tipo)
                                {
                                    case Utils.FieldTypeEnum.Number:
                                        sFields = sFields + "[" + campo + "]=" + NumberUtils.NumberDouble(v) + ",";
                                        break;
                                    case Utils.FieldTypeEnum.String:
                                        sFields = sFields + "[" + campo + "]='" + TextUtil.Left(v.Trim(), c.Tamano) +
                                                  "',";
                                        break;
                                    case Utils.FieldTypeEnum.Boolean:
                                        sFields = sFields + "[" + campo + "]='" + Functions.ValorBool(v) + "',";
                                        break;
                                    case Utils.FieldTypeEnum.DateTime:
                                        if (!FSLibrary.DateTimeUtil.IsDate(v))
                                            sFields = sFields + "[" + campo + "]=Null,";
                                        else
                                            //DateTime.cDate2(DateTime.cDate3(v), DateFormat, DateSeparator)
                                            sFields = sFields + "[" + campo + "]=" +
                                                      Utils.FormatShortDate(Convert.ToDateTime(v)) + ",";
                                        break;
                                }
                            }
                            else
                            {
                                sFields = sFields + "[" + campo + "]='" + frm.Form.Get(f).Trim() + "',";
                            }
                        }
                }

                if (HasUserData(tableName))
                {
                    //campos de seguimiento
                    sFields += "[FECHAMODIFICACION]= " + Utils.FormatShortDateTime(DateTime.Now);
                    if(usuarioId != 0)
                        sFields += ",[USUARIOMODIFICACION]=" + usuarioId;
                }
                else
                {
                    sFields = sFields.Substring(0, sFields.Length - 1);
                }

                return "UPDATE [" + tableName + "] SET " + sFields + " WHERE " + condicion;
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public string InsertSql(string tableName, string[] sArrFields, string[] sArrData, Type[] sArrType, int usuarioId = 0)
        {
            try
            {
                var sFields = "";
                var sData = "";
                var f = 0;

                foreach (var campo in sArrFields)
                    if (IsControlField(campo))
                        sFields = sFields + "[" + campo + "]" + ",";

                foreach (var s in sArrData)
                {
                    var campo = sArrFields[f];

                    if (!IsControlField(campo))
                    {
                        switch (sArrType[f].ToString().ToLower())
                        {
                            case "system.string":
                                sData += "'" + s + "',";
                                break;
                            case "system.integer":
                            case "system.int32":
                            case "system.int64":
                            case "system.double":
                            case "system.long":
                                if (NumberUtils.IsNumeric(s))
                                    sData += s + ",";
                                else
                                    sData += "0,";
                                break;
                            case "system.boolean":
                            case "system.sbyte":
                                if (s.ToLower() == "true")
                                    sData += "true,";
                                else
                                    sData += "false,";
                                break;
                            case "system.datetime":
                                if (FSLibrary.DateTimeUtil.IsDate(s))
                                    //DateTime.cDate2(DateTime.cDate3(s), DateFormat, DateSeparator)
                                    sData += Utils.FormatShortDate(Convert.ToDateTime(s)) + ",";
                                else
                                    sData += "Null,";
                                break;
                        }


                        f += 1;
                    }
                }

                if (HasUserData(tableName))
                {
                    //campos de seguimiento
                    sFields += "[FECHAMODIFICACION],";
                    sFields += "[FECHACREACION]";
                    sData += Utils.FormatShortDateTime(DateTime.Now) + ",";
                    sData += Utils.FormatShortDateTime(DateTime.Now);

                    if (usuarioId != 0)
                    {
                        sFields += ",[USUARIOMODIFICACION]";
                        sFields += ",[USUARIOCREACION]";
                        sData += "," + usuarioId;
                        sData += "," + usuarioId;
                    }
                }
                else
                {
                    sFields = sFields.Substring(0, sFields.Length - 1);
                    sData = sData.Substring(0, sData.Length - 1);
                }

                return "INSERT into [" + tableName + "] (" + sFields + ") VALUES (" + sData + ")";
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public string InsertSql(string tableName, Register registro, int usuarioId = 0)
        {
            try
            {
                var sFields = new StringBuilder("");
                var sData = new StringBuilder("");

                for (var f = 0; f <= registro.Count - 1; f++)
                {
                    var campo = registro.get_List(f).Campo;

                    if (!IsControlField(campo))
                    {
                        sFields.Append("[" + campo + "],");

                        switch (registro.get_List(f).Tipo)
                        {
                            case Utils.FieldTypeEnum.String:
                                sData.Append("'" + registro.get_List(f).Valor + "',");
                                break;
                            case Utils.FieldTypeEnum.Number:
                                if (NumberUtils.IsNumeric(registro.get_List(f).Valor))
                                    sData.Append(registro.get_List(f).Valor + ",");
                                else
                                    sData.Append("0,");
                                break;
                            case Utils.FieldTypeEnum.Boolean:
                                sData.Append(registro.get_List(f).Valor.ToLower() == "true" ? "true," : "false,");
                                break;
                            case Utils.FieldTypeEnum.DateTime:
                                var dateTime = registro.get_List(f).Valor;
                                if (FSLibrary.DateTimeUtil.IsDate(dateTime))
                                {
                                    if (dateTime.Contains(" ")) // si tiene separador de hora
                                        sData.Append(Utils.m_simbDate + dateTime + Utils.m_simbDate + ",");
                                    else
                                        //DateTime.cDate2(DateTime.cDate3(dateTime), DateFormat, DateSeparator)
                                        sData.Append(Utils.FormatShortDate(Convert.ToDateTime(dateTime)) + ",");
                                }
                                else
                                {
                                    sData.Append("Null,");
                                }

                                break;
                        }
                    }
                }

                if (HasUserData(tableName))
                {
                    //campos de seguimiento
                    sFields.Append("[FECHAMODIFICACION],[FECHACREACION]");
                    sData.Append(Utils.FormatShortDateTime(DateTime.Now) + ",");
                    sData.Append(Utils.FormatShortDateTime(DateTime.Now));

                    if (usuarioId != 0)
                    {
                        sFields.Append(",[USUARIOMODIFICACION],[USUARIOCREACION]");
                        sData.Append("," + usuarioId + ",");
                        sData.Append(usuarioId);
                    }
                }
                else
                {
                    sFields = sFields.Remove(sFields.Length - 1, 1);
                    sData = sData.Remove(sData.Length - 1, 1);
                }

                return "INSERT into [" + tableName + "] (" + sFields + ") VALUES (" + sData + ")";
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public string UpdateSql(string tableName, string[] sArrFields, string[] sArrData, Type[] sArrType, string condition, int usuarioId = 0)
        {
            try
            {
                var sData = "";
                var f = 0;

                foreach (var s in sArrData)
                {
                    var campo = sArrFields[f];

                    if (!IsControlField(campo))
                    {
                        switch (sArrType[f].ToString().ToLower())
                        {
                            case "system.string":
                                sData += "[" + campo + "]='" + s + "',";
                                break;
                            case "system.integer":
                            case "system.int32":
                            case "system.int64":
                            case "system.double":
                            case "system.long":
                                if (NumberUtils.IsNumeric(s))
                                    sData += "[" + campo + "]=" + s + ",";
                                else
                                    sData += "[" + campo + "]=0,";
                                break;
                            case "system.boolean":
                            case "system.sbyte":
                                if (s.ToLower() == "true")
                                    sData += "[" + campo + "]=true,";
                                else
                                    sData += "[" + campo + "]=false,";
                                break;
                            case "system.datetime":
                                if (FSLibrary.DateTimeUtil.IsDate(s))
                                    //DateTime.cDate2(DateTime.cDate3(s), DateFormat, DateSeparator)
                                    sData += "[" + campo + "]=" + Utils.FormatShortDate(Convert.ToDateTime(s)) + ",";
                                else
                                    sData += "[" + campo + "]= Null,";
                                break;
                        }


                        f += 1;
                    }
                }

                if (HasUserData(tableName))
                {
                    //campos de seguimiento
                    sData += "[FECHAMODIFICACION]= " + Utils.FormatShortDateTime(DateTime.Now);

                    if(usuarioId != 0)
                        sData += ",[USUARIOMODIFICACION]=" + usuarioId;
                }
                else
                {
                    sData = sData.Substring(0, sData.Length - 1);
                }

                condition = condition.Replace("where", "");

                return "UPDATE [" + tableName + "] SET " + sData + " WHERE " + condition;
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public string UpdateSql(string tableName, Register registro, string condition, int usuarioId = 0)
        {
            try
            {
                var sData = "";

                for (var f = 0; f <= registro.Count - 1; f++)
                {
                    var campo = registro.get_List(f).Campo;

                    if (!IsControlField(campo))
                        switch (registro.get_List(f).Tipo)
                        {
                            case Utils.FieldTypeEnum.String:
                                sData += "[" + campo + "]='" + registro.get_List(f).Valor + "',";
                                break;
                            case Utils.FieldTypeEnum.Number:
                                if (NumberUtils.IsNumeric(registro.get_List(f).Valor))
                                    sData += "[" + campo + "]=" + registro.get_List(f).Valor + ",";
                                else
                                    sData += "[" + campo + "]=0,";
                                break;
                            case Utils.FieldTypeEnum.Boolean:
                                if (registro.get_List(f).Valor.ToLower() == "true")
                                    sData += "[" + campo + "]=true,";
                                else
                                    sData += "[" + campo + "]=false,";
                                break;
                            case Utils.FieldTypeEnum.DateTime:
                                if (FSLibrary.DateTimeUtil.IsDate(registro.get_List(f).Valor))
                                    //DateTime.cDate2(DateTime.cDate3(registro.get_List(f).Valor), DateFormat, DateSeparator)
                                    sData += "[" + campo + "]=" +
                                            Utils.FormatShortDate(Convert.ToDateTime(registro.get_List(f).Valor)) + ",";
                                else
                                    sData += "[" + campo + "]= Null,";
                                break;
                        }
                }

                if (HasUserData(tableName))
                {
                    //campos de seguimiento
                    sData += "[FECHAMODIFICACION]= " + Utils.FormatShortDateTime(DateTime.Now) + ",";
                    sData += "[USUARIOMODIFICACION]=" + usuarioId;
                }
                else
                {
                    sData = sData.Substring(0, sData.Length - 1);
                }

                condition = TextUtil.Replace(condition, "where", "");

                return "UPDATE [" + tableName + "] SET " + sData + " WHERE " + condition;
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public string CommandInsert(string tableName, DataRow row, string userName)
        {
            DataTable d_schema = GetSchemaTable(tableName);
            var iqb = new InsertQueryBuilder();
            iqb.TableSource = tableName;

            var columns = new List<string>();
            var values = new List<object>();
            string columnName;
            var pk = PrimaryKeyName(tableName);

            foreach (DataRow dataColumn in d_schema.Rows)
            {
                columnName = dataColumn["ColumnName"].ToString();

                if (pk != columnName) //!Convert.ToBoolean(dataColumn["IsAutoIncrement"])
                {
                    var value = row[columnName];

                    if (HasUserData(tableName))
                    {
                        if (columnName.ToLower() == "usuariomodificacion")
                            value = userName;
                        if (columnName.ToLower() == "fechamodificacion")
                            value = DateTime.Now;
                        if (columnName.ToLower() == "usuariocreacion")
                            value = userName;
                        if (columnName.ToLower() == "fechacreacion")
                            value = DateTime.Now;
                    }

                    columns.Add(columnName);
                    values.Add(value);
                }
            }

            iqb.Columns.SelectColumns(columns);
            iqb.Values.SelectValues(values);

            return iqb.BuildQuery();
        }

        public string CommandUpdate(string tableName, DataRow row, string userName)
        {
            DataTable d_schema = GetSchemaTable(tableName);
            UpdateQueryBuilder uqb = new UpdateQueryBuilder();
            uqb.TableSource = tableName;

            string columnName;
            var pk = PrimaryKeyName(tableName);
            foreach (DataRow dataColumn in d_schema.Rows)
            {
                columnName = dataColumn["ColumnName"].ToString();

                if (pk != columnName) //!Convert.ToBoolean(dataColumn["IsAutoIncrement"])
                {
                    var value = row[columnName];

                    //if (value is byte[])
                    //{
                    //    break;
                    //}

                    //if (value is Decimal || value is Int32 || value is float)
                    //{
                    //    value = value.ToString().Replace(",", ".");
                    //}

                    if (HasUserData(tableName))
                    {
                        if (columnName.ToLower() == "usuariomodificacion")
                            value = userName;
                        if (columnName.ToLower() == "fechamodificacion")
                            value = DateTime.Now;
                    }

                    uqb.Assignments.AddAssignment(columnName, value);
                }
            }


            var andW = new AndWhere();
            andW.Add(new SimpleWhere(uqb.TableSource, pk, Comparison.Equals, row[pk]));

            uqb.Where = andW;

            return uqb.BuildQuery();
        }


        public string CommandDelete(string tableName, DataRow row)
        {
            var dqb = new DeleteQueryBuilder();
            dqb.TableSource = tableName;

            var pk = PrimaryKeyName(tableName);
            var andW = new AndWhere();
            andW.Add(new SimpleWhere(dqb.TableSource, pk, Comparison.Equals, row[pk]));
            dqb.Where = andW;

            return dqb.BuildQuery();
        }

        public bool HasUserData(string tableName)
        {
            DataTable d_schema = GetSchemaTable(tableName);
            foreach (DataRow dataColumn in d_schema.Rows)
                if (dataColumn["ColumnName"].ToString().ToLower() == "fechamodificacion" ||
                    dataColumn["ColumnName"].ToString().ToLower() == "usuariomodificacion")
                    return true;

            return false;
        }

        public string PrimaryKeyName(string tableName)
        {
            DataTable d_schema = GetSchemaTable(tableName);
            string pk;
            try
            {
                pk = d_schema.Select("IsKey=true")[0]["ColumnName"].ToString();
            }
            catch
            {
                pk = "";
            }

            return pk;
        }

        public string PrimaryKeyType(string tableName)
        {
            DataTable d_schema = GetSchemaTable(tableName);
            string pk;
            try
            {
                pk = d_schema.Select("IsKey=true")[0]["DataType"].ToString();
            }
            catch
            {
                pk = "";
            }

            return pk;
        }

        public string GetAutoNumeric(string tableName)
        {
            DataTable d_schema = GetSchemaTable(tableName);
            foreach (DataRow dataColumn in d_schema.Rows)
                if (Convert.ToBoolean(dataColumn["IsAutoIncrement"]))
                    return dataColumn["ColumnName"].ToString();

            return string.Empty;
        }

        public bool HasAutoNumeric(string tableName)
        {
            DataTable d_schema = GetSchemaTable(tableName);
            foreach (DataRow dataColumn in d_schema.Rows)
                if (Convert.ToBoolean(dataColumn["IsAutoIncrement"]))
                    return true;

            return false;
        }

        public void CheckConnection()
        {
            try
            {
                foreach (ConnectionStringSettings con in ConfigurationManager.ConnectionStrings)
                {
                    using (DbConnection db = new OleDbConnection())
                    {
                        db.ConnectionString = con.ConnectionString;
                        db.Open();
                    }
                }
            }
            catch (ExceptionUtil ex)
            {
                throw new ExceptionUtil(ex);
            }
        }

        public void SqlBlob2File(string DestFilePath)
        {
            try
            {
                var PictureCol = 0;
                using (DbConnection conn = Connection())
                {
                    using (var cmd = ProviderFactory().CreateCommand())
                    {
                        cmd.CommandText = "SELECT Picture FROM Categories WHERE CategoryName='Test'";
                        cmd.Connection = conn;

                        conn.Open();
                        var dr = cmd.ExecuteReader();
                        dr.Read();
                        var b = new byte[dr.GetBytes(PictureCol, 0, null, 0, int.MaxValue) - 1 + 1];
                        dr.GetBytes(PictureCol, 0, b, 0, b.Length);
                        dr.Close();
                        conn.Close();
                        var fs = new FileStream(DestFilePath, FileMode.Create, FileAccess.Write);
                        fs.Write(b, 0, b.Length);
                        fs.Close();
                    }
                }
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public void OleDbBlob2File(string DestFilePath)
        {
            try
            {
                var PictureCol = 0;
                var cn =
                    new OleDbConnection(ConnString);
                var cmd = new OleDbCommand("SELECT Picture FROM Categories WHERE CategoryName='Test'", cn);
                cn.Open();
                var dr = cmd.ExecuteReader();
                dr.Read();
                var b = new byte[dr.GetBytes(PictureCol, 0, null, 0, int.MaxValue) - 1 + 1];
                dr.GetBytes(PictureCol, 0, b, 0, b.Length);
                dr.Close();
                cn.Close();
                var fs = new FileStream(DestFilePath, FileMode.Create, FileAccess.Write);
                fs.Write(b, 0, b.Length);
                fs.Close();
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public void File2SqlBlob(string SourceFilePath)
        {
            try
            {
                using (DbConnection conn = Connection())
                {
                    using (var cmd = ProviderFactory().CreateCommand())
                    {
                        cmd.CommandText = "UPDATE Categories SET Picture=@Picture WHERE CategoryName='Test'";
                        cmd.Connection = conn;

                        var fs = new FileStream(SourceFilePath, FileMode.Open, FileAccess.Read);
                        var b = new byte[fs.Length - 1 + 1];
                        fs.Read(b, 0, b.Length);
                        fs.Close();
                        var P = ProviderFactory().CreateParameter();

                        P.ParameterName = "@Picture";
                        P.Direction = ParameterDirection.Input;
                        P.IsNullable = false;
                        P.DbType = DbType.VarNumeric;
                        P.Size = b.Length;
                        P.Value = b;
                        P.SourceVersion = DataRowVersion.Current;

                        cmd.Parameters.Add(P);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }

        private void File2OleDbBlob(string SourceFilePath)
        {
            try
            {
                var cn =
                    new OleDbConnection(ConnString);
                var cmd = new OleDbCommand("UPDATE Categories SET Picture=? WHERE CategoryName='Test'", cn);
                var fs = new FileStream(SourceFilePath, FileMode.Open, FileAccess.Read);
                var b = new byte[fs.Length - 1 + 1];
                fs.Read(b, 0, b.Length);
                fs.Close();
                var P = new OleDbParameter();
                P.ParameterName = "@Picture";
                P.Direction = ParameterDirection.Input;
                P.IsNullable = false;
                P.OleDbType = OleDbType.LongVarBinary;
                P.Precision = 0;
                P.Size = b.Length;
                P.Value = b;
                P.SourceVersion = DataRowVersion.Current;

                cmd.Parameters.Add(P);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public string ProvidersList()
        {
            try
            {
                var table = DbProviderFactories.GetFactoryClasses();
                var sb = new StringBuilder();

                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn column in table.Columns) sb.AppendLine("[" + row[column] + "]");
                    sb.AppendLine("/");
                }

                return sb.ToString();
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public string Test()
        {
            try
            {
                var sb = new StringBuilder("");
                //sb.Append("OdbcPermission: " + Permission.TestPermission(new OdbcPermission(PermissionState.Unrestricted)));
                sb.Append("OleDbPermission: " +
                          Permission.TestPermission(new OleDbPermission(System.Security.Permissions.PermissionState.Unrestricted)));
                //sb.Append("DbPermission: " + Permission.TestPermission(new permission(PermissionState.Unrestricted)));
                //sb.Append("SqlClientPermission: " + Permission.TestPermission(new SqlClientPermission(PermissionState.Unrestricted)));
                return sb.ToString();
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public string GenerateConnectionString(Utils.ServerTypeEnum baseType, string baseName, string server, string fileName,
            string userName, string password)
        {
            try
            {
                switch (baseType)
                {
                    case Utils.ServerTypeEnum.Odbc:
                        return "Provider=MSDASQL.1;Password=" + password + ";Persist Security Info=True;User ID=" +
                               userName +
                               ";Data Source=" + baseName;
                    case Utils.ServerTypeEnum.Oracle:
                        return "Provider=OraOLEDB.Oracle.1;Password=" + password +
                               ";Persist Security Info=True;User ID=" +
                               userName + ";Data Source=" + baseName;
                    case Utils.ServerTypeEnum.SQLServer:
                        return "Provider=SQLOLEDB.1;Password=" + password + ";Persist Security Info=True;User ID=" +
                               userName + ";Initial Catalog=" + baseName + ";Data Source=" + server;
                    case Utils.ServerTypeEnum.Access2000:
                        return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName +
                               ";Persist Security Info=False";
                    case Utils.ServerTypeEnum.Access97:
                        return "Provider=Microsoft.Jet.OLEDB.3.51;Persist Security Info=False;Data Source=" + fileName;
                }

                return string.Empty;
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }
        }
    }
}