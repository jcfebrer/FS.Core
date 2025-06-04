// // <fileheader>
// // <copyright file="DAL.cs" company="Febrer Software">
// //     Fecha: 03/07/2010
// //     Project: FSLibrary
// //     Solution: FSLibraryNET2008
// //     Copyright (c) 2010 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using DateTime = System.DateTime;
using FSTrace;

#endregion

namespace FSDatabase
{
    /// <summary>
    ///     Esta clase encapsula el uso de las operaciones habituales de acceso a datos mediante OleDb
    /// </summary>
    public sealed class Dal
    {
        #region Datos privados

        /// <summary>
        ///     Almacena la cadena de conexión que usaremos para establecer las conexiones
        /// </summary>
        private readonly string _connectionString;

        #endregion

        #region Construtores

        ///// <summary>
        /////     Este contructor establece la cadena de conexión que se utilizará para realizar las
        /////     operaciones contra la base de datos
        ///// </summary>
        ///// <param name="connectionStr">
        /////     Cadena de conexión que se usará para establece las conexiones.
        /////     Puede ser una cadena de conexión explita o el nombre de una
        /////     cadena de conexión con nombre establecida en la sección
        /////     connectionStrings de el archivo de configuración de la aplicación.
        ///// </param>
        //public Dal(string connectionStr)
        //{
        //    _connectionString = ConfigurationManager.ConnectionStrings[connectionStr] != null
        //        ? ConfigurationManager.ConnectionStrings[connectionStr].ConnectionString
        //        : connectionStr;
        //}

        #endregion

        #region Propiedades

        /// <summary>
        ///     Devuelve una conexión construida a partir de la cadena de conexión que se
        ///     proporciono en el constructor
        /// </summary>
        /// <remarks>
        ///     La conexión se debe cerrar explicitamente una vez se haya terminado con ella, llamando a su metodo Close o Dispose
        ///     Una manera de asegurar que siempre cerramos la conexión es usar código de estilo del siguiente
        ///     <code>
        ///  using (OleDbConnection cn = dah.Connection)
        /// 	{
        /// 		//Operaciones con la conexión obtenida
        /// 	}
        /// 	</code>
        /// </remarks>
        public OleDbConnection Connection
        {
            get
            {
                var cn = new OleDbConnection(_connectionString);
                cn.Open();
                return cn;
            }
        }

        #endregion

        #region Sobrecargas

        /// <summary>
        ///     Devuelve la cadena de conexión que se esta usando para hace las conexiones
        /// </summary>
        /// <returns></returns>
        public new string ToString()
        {
            return _connectionString;
        }

        #endregion

        #region Metodos de ayuda

        /// <summary>
        ///     Prepara un comando para su ejecución asignandole una conexión, transacción, tipo de comando y parametros
        /// </summary>
        /// <param name="connection">Conexión que se utilizara para ejecutar el comando</param>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Sentencia SQL o nombre del procedimiento almacenado</param>
        /// <param name="commandParameters">Parámetros con los que se ejecutará el comando, puede ser null cuando hay parametros</param>
        /// <returns>El comando listo para su ejecución</returns>
        private static OleDbCommand PrepareCommand(OleDbConnection connection, CommandType commandType,
            string commandText, OleDbParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");

            var command = new OleDbCommand(commandText);

            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Connection = connection;
            command.CommandTimeout = 200;


            // Establecer la sentencia SQL o procedimiento almacenado a utilizar
            // Establecemos el tipo de comando
            // Asociar la conexion con el comando
            // ponemos el tiempo a 200, ya que 30 puede ser bajo
            // Asignamos los parametros al comando
            if (commandParameters != null)
            {
                command.Parameters.Clear();
                AttachParameters(command, commandParameters);
            }

            Log.TraceInfo("SQL Command: {0}", BuildCommandTextForTracing(commandText, commandParameters));

            return command;
        }

        /// <summary>
        ///     Prepara un comando para su ejecución asignandole una conexión, transacción, tipo de comando y parametros
        /// </summary>
        /// <param name="transaction">
        ///     Transacción en la que se englobará la ejecución del comando, puede ser null cuando no es
        ///     necesaria una transacción
        /// </param>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Sentencia SQL o nombre del procedimiento almacenado</param>
        /// <param name="commandParameters">Parámetros con los que se ejecutará el comando, puede ser null cuando hay parametros</param>
        /// <returns>El comando listo para su ejecución</returns>
        private static OleDbCommand PrepareCommand(OleDbTransaction transaction, CommandType commandType,
            string commandText, OleDbParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");
            if (transaction.Connection == null)
                throw new ArgumentException("Transaction already terminated, use an open transaction", "transaction");

            var command = new OleDbCommand(commandText);

            // Establecemos el tipo de comando
            command.CommandType = commandType;
            command.Connection = transaction.Connection;
            //Asignamos la transaccion
            command.Transaction = transaction;


            // Asignamos los parametros al comando
            if (commandParameters != null)
            {
                command.Parameters.Clear();
                AttachParameters(command, commandParameters);
            }

            Log.TraceInfo("SQL Command inside transaction: {0}",
                BuildCommandTextForTracing(commandText, commandParameters));

            return command;
        }

        /// <summary>
        ///     Este metodo se usa para asignar los parametros a el comando.
        /// </summary>
        /// <param name="command">Comando al que asignaremos los parametros</param>
        /// <param name="commandParameters">Array de parametros que asignaremos</param>
        private static void AttachParameters(OleDbCommand command, IEnumerable<OleDbParameter> commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
                foreach (var p in commandParameters)
                    if (p != null)
                        command.Parameters.Add(p);
        }


        /// <summary>
        ///     Sustituye las ? de la sentencia SQL por el valor de los parámetros. Se
        ///     usa en el traceo de sentencias SQL ejecutadas.
        /// </summary>
        /// <param name="commandText">Texto del comando SQL.</param>
        /// <param name="commandParameters">Parámetros con los que se ejecuta el comando.</param>
        /// <returns>Sentencia SQL con los ? de las condiciones ya sustituidos por el parámetro correspondiente.</returns>
        private static string BuildCommandTextForTracing(string commandText, OleDbParameter[] commandParameters)
        {
            // Puede que no existan parámetros
            if (commandParameters == null || commandParameters.Length == 0)
                return commandText;

            var newCommandText = commandText;

            // Sustituimos las '?' de las sentencias SQL por el valor del parametro
            var initPos = 0;
            foreach (var p in commandParameters)
                // No saltamos las condiciones is (not) null
                if (p != null)
                {
                    initPos = newCommandText.IndexOf('?', initPos);
                    // Cuando encuentra el último '?' salimos
                    if (initPos == -1) break;

                    newCommandText = newCommandText.Substring(0, initPos) +
                                     (p.Value == null ? "NULL" : p.Value.ToString()) +
                                     newCommandText.Substring(initPos + 1, newCommandText.Length - initPos - 1);
                }

            return newCommandText;
        }

        #endregion

        #region ExecuteNonQuery

        /// <summary>
        ///     Ejecuta una sentencia SQL o procedimiento almacenado que no devuelve resultados y que no espera parametros
        /// </summary>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <returns>Número de filas afectadas por el comando</returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(commandType, commandText, null);
        }

        /// <summary>
        ///     Ejecuta una sentencia SQL que no devuelve resultados y que no espera parametros
        /// </summary>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <returns>Número de filas afectadas por el comando</returns>
        public int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(CommandType.Text, commandText);
        }


        /// <summary>
        ///     Ejecuta una sentencia SQL o procedimiento almacenado que no devuelve resultados y que no espera parametros dentro
        ///     de una transacción
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <returns>Número de filas afectadas por el comando</returns>
        public static int ExecuteNonQuery(OleDbTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(transaction, commandType, commandText, null);
        }

        /// <summary>
        ///     Ejecuta una sentencia SQL que no devuelve resultados y que no espera parametros dentro de una transacción
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <returns>Número de filas afectadas por el comando</returns>
        public static int ExecuteNonQuery(OleDbTransaction transaction, string commandText)
        {
            return ExecuteNonQuery(transaction, CommandType.Text, commandText);
        }


        /// <summary>
        ///     Ejecuta una sentencia SQL o procedimiento almacenado que no devuelve resultados
        /// </summary>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Número de filas afectadas por el comando</returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText,
            params OleDbParameter[] commandParameters)
        {
            if (commandType != CommandType.StoredProcedure && commandType != CommandType.Text)
                throw new ArgumentException("Command type must be CommandType.Text or CommandType.StoredProcedure",
                    "commandType");
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");

            //Obtenemos la conexión
            using (var conn = Connection)
                //Preparamos el comando
            using (var command = PrepareCommand(conn, commandType, commandText, commandParameters))
            {
                //Ejecutamos el comando
                var rt = command.ExecuteNonQuery();
                command.Dispose();
                return rt;
            }
        }

        /// <summary>
        ///     Ejecuta una sentencia SQL que no devuelve resultados
        /// </summary>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Número de filas afectadas por el comando</returns>
        public int ExecuteNonQuery(string commandText, params OleDbParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");

            //Obtenemos la conexión
            using (var conn = Connection)
                //Preparamos el comando
            using (var command = PrepareCommand(conn, CommandType.Text, commandText, commandParameters))
            {
                //Ejecutamos el comando
                var rt = command.ExecuteNonQuery();
                command.Dispose();
                return rt;
            }
        }

        /// <summary>
        ///     Ejecuta una sentencia SQL o procedimiento almacenado que no devuelve resultados dentro de una transación
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Número de filas afectadas por el comando</returns>
        public static int ExecuteNonQuery(OleDbTransaction transaction, CommandType commandType, string commandText,
            params OleDbParameter[] commandParameters)
        {
            if (commandType != CommandType.StoredProcedure && commandType != CommandType.Text)
                throw new ArgumentException("Command type must be CommandType.Text or CommandType.StoredProcedure",
                    "commandType");
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");

            //Preparamos el comando
            var command = PrepareCommand(transaction, commandType, commandText, commandParameters);
            //Ejecutamos el comando
            var rt = command.ExecuteNonQuery();
            command.Dispose();
            return rt;
        }

        /// <summary>
        ///     Ejecuta una sentencia SQL que no devuelve resultados dentro de una transación
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Número de filas afectadas por el comando</returns>
        public static int ExecuteNonQuery(OleDbTransaction transaction, string commandText,
            params OleDbParameter[] commandParameters)
        {
            return ExecuteNonQuery(transaction, CommandType.Text, commandText, commandParameters);
        }

        #endregion

        #region ExecuteScalar

        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado y devuelve el valor de la primera fila de la primera columna
        /// </summary>
        /// <remarks>
        ///     ejemplo:
        ///     int orderCount = (int)ExecuteScalar(CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("prodid", 24));
        /// </remarks>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <returns>El contenido de la primera fila de la primera columna del conjunto de datos</returns>
        public object ExecuteScalar(CommandType commandType, string commandText)
        {
            return ExecuteScalar(commandType, commandText, null);
        }

        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado y devuelve el valor de la primera fila de la primera columna
        /// </summary>
        /// <remarks>
        ///     ejemplo:
        ///     int orderCount = (int)ExecuteScalar("GetOrderCount", new SqlParameter("prodid", 24));
        /// </remarks>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <returns>El contenido de la primera fila de la primera columna del conjunto de datos</returns>
        public object ExecuteScalar(string commandText)
        {
            return ExecuteScalar(CommandType.Text, commandText);
        }


        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacena y devuelve el valor de la primera fila de la primera columna
        /// </summary>
        /// <remarks>
        ///     ejemplo:
        ///     int orderCount = (int)ExecuteScalar(CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("prodid", 24));
        /// </remarks>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <returns>El contenido de la primera fila de la primera columna del conjunto de datos</returns>
        public static object ExecuteScalar(OleDbTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteScalar(transaction, commandType, commandText, null);
        }

        /// <summary>
        ///     Ejecuta una sentencia y devuelve el valor de la primera fila de la primera columna
        /// </summary>
        /// <remarks>
        ///     ejemplo:
        ///     int orderCount = (int)ExecuteScalar("GetOrderCount", new SqlParameter("prodid", 24));
        /// </remarks>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <returns>El contenido de la primera fila de la primera columna del conjunto de datos</returns>
        public static object ExecuteScalar(OleDbTransaction transaction, string commandText)
        {
            return ExecuteScalar(transaction, CommandType.Text, commandText);
        }


        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacena y devuelve el valor de la primera fila de la primera columna
        /// </summary>
        /// <remarks>
        ///     ejemplo:
        ///     int orderCount = (int)ExecuteScalar(CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("prodid", 24));
        /// </remarks>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>El contenido de la primera fila de la primera columna del conjunto de datos</returns>
        public object ExecuteScalar(CommandType commandType, string commandText,
            params OleDbParameter[] commandParameters)
        {
            if (commandType != CommandType.StoredProcedure && commandType != CommandType.Text)
                throw new ArgumentException("Command type must be CommandType.Text or CommandType.StoredProcedure",
                    "commandType");
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");

            //Obtenemos la conexión
            using (var conn = Connection)
                //Preparamos el comando
            using (var command = PrepareCommand(conn, commandType, commandText, commandParameters))
            {
                //Ejecutamos el comando
                var scalarResult = command.ExecuteScalar();
                command.Dispose();
                return scalarResult;
            }
        }

        /// <summary>
        ///     Ejecuta una sentencia devuelve el valor de la primera fila de la primera columna
        /// </summary>
        /// <remarks>
        ///     ejemplo:
        ///     int orderCount = (int)ExecuteScalar(CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("prodid", 24));
        /// </remarks>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>El contenido de la primera fila de la primera columna del conjunto de datos</returns>
        public object ExecuteScalar(string commandText, params OleDbParameter[] commandParameters)
        {
            return ExecuteScalar(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        ///     Ejecuta una sentencia y devuelve el valor de la primera fila de la primera columna
        /// </summary>
        /// <remarks>
        ///     ejemplo:
        ///     int orderCount = (int)ExecuteScalar(CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("prodid", 24));
        /// </remarks>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>El contenido de la primera fila de la primera columna del conjunto de datos</returns>
        public static object ExecuteScalar(OleDbTransaction transaction, CommandType commandType, string commandText,
            params OleDbParameter[] commandParameters)
        {
            if (commandType != CommandType.StoredProcedure && commandType != CommandType.Text)
                throw new ArgumentException("Command type must be CommandType.Text or CommandType.StoredProcedure",
                    "commandType");
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");

            //Preparamos el comando
            using (var command = PrepareCommand(transaction, commandType, commandText, commandParameters))
            {
                //Ejecutamos el comando
                var scalarResult = command.ExecuteScalar();
                command.Dispose();
                return scalarResult;
            }
        }

        /// <summary>
        ///     Ejecuta una sentencia y devuelve el valor de la primera fila de la primera columna
        /// </summary>
        /// <remarks>
        ///     ejemplo:
        ///     int orderCount = (int)ExecuteScalar("SELECT...", new SqlParameter("prodid", 24));
        /// </remarks>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>El contenido de la primera fila de la primera columna del conjunto de datos</returns>
        public static object ExecuteScalar(OleDbTransaction transaction, string commandText,
            params OleDbParameter[] commandParameters)
        {
            return ExecuteScalar(transaction, CommandType.Text, commandText, commandParameters);
        }

        #endregion

        #region ExecuteDataTable

        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado que devuelve un conjunto de registros y devuelve un dataset
        /// </summary>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <returns>Un DataTable con el conjunto de resultados</returns>
        public DataTable ExecuteDataTable(CommandType commandType, string commandText)
        {
            return ExecuteDataTable(commandType, commandText, null);
        }

        /// <summary>
        ///     Ejecuta una sentencia que devuelve un conjunto de registros y devuelve un dataset
        /// </summary>
        /// <param name="commandText">Sentencia SQL a ejecutar</param>
        /// <returns>Un DataTable con el conjunto de resultados</returns>
        public DataTable ExecuteDataTable(string commandText)
        {
            return ExecuteDataTable(CommandType.Text, commandText);
        }

        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado que devuelve un conjunto de registros y devuelve un dataset
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <returns>Un DataTable con el conjunto de resultados</returns>
        public static DataTable ExecuteDataTable(OleDbTransaction transaction, CommandType commandType,
            string commandText)
        {
            return ExecuteDataTable(transaction, commandType, commandText, null);
        }

        /// <summary>
        ///     Ejecuta una sentencia que devuelve un conjunto de registros y devuelve un dataset
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <returns>Un DataTable con el conjunto de resultados</returns>
        public static DataTable ExecuteDataTable(OleDbTransaction transaction, string commandText)
        {
            return ExecuteDataTable(transaction, CommandType.Text, commandText);
        }

        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado que devuelve un conjunto de registros y devuelve un dataset
        /// </summary>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Un DataTable con el conjunto de resultados</returns>
        public DataTable ExecuteDataTable(CommandType commandType, string commandText,
            params OleDbParameter[] commandParameters)
        {
            if (commandType != CommandType.StoredProcedure && commandType != CommandType.Text)
                throw new ArgumentException("Command type must be CommandType.Text or CommandType.StoredProcedure",
                    "commandType");
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");

            //Obtenemos la conexión
            using (var conn = Connection)
            using (var command = PrepareCommand(conn, commandType, commandText, commandParameters))
            using (var da = new OleDbDataAdapter(command))
            {
                //Llenamos el DataSet
                var rt = new DataTable();
                rt.Locale = CultureInfo.CurrentCulture;
                da.Fill(rt);
                command.Dispose();
                return rt;
            }
        }

        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado que devuelve un conjunto de registros y devuelve un dataset
        /// </summary>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Un DataTable con el conjunto de resultados</returns>
        public DataTable ExecuteDataTable(string commandText, params OleDbParameter[] commandParameters)
        {
            return ExecuteDataTable(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado que devuelve un conjunto de registros y devuelve un dataset
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Un DataTable con el conjunto de resultados</returns>
        public static DataTable ExecuteDataTable(OleDbTransaction transaction, CommandType commandType,
            string commandText, params OleDbParameter[] commandParameters)
        {
            if (commandType != CommandType.StoredProcedure && commandType != CommandType.Text)
                throw new ArgumentException("Command type must be CommandType.Text or CommandType.StoredProcedure",
                    "commandType");
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");

            //Preparamos el comando
            using (var command = PrepareCommand(transaction, commandType, commandText, commandParameters))
            using (var da = new OleDbDataAdapter(command))
            {
                //Llenamos el DataSet
                var rt = new DataTable();
                rt.Locale = CultureInfo.InvariantCulture;
                da.Fill(rt);
                command.Dispose();
                return rt;
            }
        }

        /// <summary>
        ///     Ejecuta una sentencia que devuelve un conjunto de registros y devuelve un dataset
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Un DataTable con el conjunto de resultados</returns>
        public static DataTable ExecuteDataTable(OleDbTransaction transaction, string commandText,
            params OleDbParameter[] commandParameters)
        {
            return ExecuteDataTable(transaction, CommandType.Text, commandText, commandParameters);
        }

        #endregion

        #region ExecuteDataset

        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado que devuelve un conjunto de registros y devuelve un dataset
        /// </summary>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <returns>Un dataset con el conjunto de resultados</returns>
        public DataSet ExecuteDataSet(CommandType commandType, string commandText)
        {
            return ExecuteDataSet(commandType, commandText, null);
        }

        /// <summary>
        ///     Ejecuta una sentencia que devuelve un conjunto de registros y devuelve un dataset
        /// </summary>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <returns>Un dataset con el conjunto de resultados</returns>
        public DataSet ExecuteDataSet(string commandText)
        {
            return ExecuteDataSet(CommandType.Text, commandText);
        }

        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado que devuelve un conjunto de registros y devuelve un dataset
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <returns>Un dataset con el conjunto de resultados</returns>
        public DataSet ExecuteDataSet(OleDbTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteDataSet(transaction, commandType, commandText, null);
        }

        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado que devuelve un conjunto de registros y devuelve un dataset
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <returns>Un dataset con el conjunto de resultados</returns>
        public DataSet ExecuteDataSet(OleDbTransaction transaction, string commandText)
        {
            return ExecuteDataSet(transaction, CommandType.Text, commandText);
        }


        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado que devuelve un conjunto de registros y devuelve un dataset
        /// </summary>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Un dataset con el conjunto de resultados</returns>
        public DataSet ExecuteDataSet(CommandType commandType, string commandText,
            params OleDbParameter[] commandParameters)
        {
            if (commandType != CommandType.StoredProcedure && commandType != CommandType.Text)
                throw new ArgumentException("Command type must be CommandType.Text or CommandType.StoredProcedure",
                    "commandType");
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");

            //Obtenemos la conexión
            using (var conn = Connection)
            using (var command = PrepareCommand(conn, commandType, commandText, commandParameters))
            using (var da = new OleDbDataAdapter(command))
            {
                //Llenamos el DataSet
                var rt = new DataSet();
                rt.Locale = CultureInfo.CurrentCulture;
                da.Fill(rt);
                command.Dispose();
                return rt;
            }
        }

        /// <summary>
        ///     Ejecuta una sentencia que devuelve un conjunto de registros y devuelve un dataset
        /// </summary>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Un dataset con el conjunto de resultados</returns>
        public DataSet ExecuteDataSet(string commandText, params OleDbParameter[] commandParameters)
        {
            return ExecuteDataSet(CommandType.Text, commandText, commandParameters);
        }


        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado que devuelve un conjunto de registros y devuelve un dataset
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Un dataset con el conjunto de resultados</returns>
        public DataSet ExecuteDataSet(OleDbTransaction transaction, CommandType commandType, string commandText,
            params OleDbParameter[] commandParameters)
        {
            if (commandType != CommandType.StoredProcedure && commandType != CommandType.Text)
                throw new ArgumentException("Command type must be CommandType.Text or CommandType.StoredProcedure",
                    "commandType");
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");

            //Preparamos el comando
            using (var command = PrepareCommand(transaction, commandType, commandText, commandParameters))
            using (var da = new OleDbDataAdapter(command))
            {
                //Llenamos el DataSet
                var rt = new DataSet();
                rt.Locale = CultureInfo.InvariantCulture;
                da.Fill(rt);
                command.Dispose();
                return rt;
            }
        }

        /// <summary>
        ///     Ejecuta una sentencia que devuelve un conjunto de registros y devuelve un dataset
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Un dataset con el conjunto de resultados</returns>
        public DataSet ExecuteDataSet(OleDbTransaction transaction, string commandText,
            params OleDbParameter[] commandParameters)
        {
            return ExecuteDataSet(transaction, CommandType.Text, commandText, commandParameters);
        }

        #endregion

        #region ExecuteRangeDataSet

        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado y devuelve un rango de registros en un DataSet
        /// </summary>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="fromRecord">Primer registro a devolver.</param>
        /// <param name="toRecord">Ultimo registro a devolver.</param>
        /// <returns>Un dataSet con el rango de filas indicado.</returns>
        public DataSet ExecuteRangeDataSet(CommandType commandType, string commandText, int fromRecord, int toRecord)
        {
            return ExecuteRangeDataSet(commandType, commandText, fromRecord, toRecord, null);
        }

        /// <summary>
        ///     Ejecuta una sentencia y devuelve un rango de registros en un DataSet.
        /// </summary>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="fromRecord">Primer registro a devolver.</param>
        /// <param name="toRecord">Ultimo registro a devolver.</param>
        /// <returns>Un DataSet con el rango de filas indicado.</returns>
        public DataSet ExecuteRangeDataSet(string commandText, int fromRecord, int toRecord)
        {
            return ExecuteRangeDataSet(CommandType.Text, commandText, fromRecord, toRecord);
        }

        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado y devuelve un rango de registros en un DataSet.
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="fromRecord">Primer registro a devolver.</param>
        /// <param name="toRecord">Ultimo registro a devolver.</param>
        /// <returns>Un dataset con el rango de filas indicado.</returns>
        public static DataSet ExecuteRangeDataSet(OleDbTransaction transaction, CommandType commandType,
            string commandText, int fromRecord, int toRecord)
        {
            return ExecuteRangeDataSet(transaction, commandType, commandText, fromRecord, toRecord, null);
        }

        /// <summary>
        ///     Ejecuta una sentencia y devuelve un rango de registros en un DataSet.
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="fromRecord">Primer registro a devolver.</param>
        /// <param name="toRecord">Ultimo registro a devolver.</param>
        /// <returns>Un dataset con el rango de filas indicado.</returns>
        public static DataSet ExecuteRangeDataSet(OleDbTransaction transaction, string commandText, int fromRecord,
            int toRecord)
        {
            return ExecuteRangeDataSet(transaction, CommandType.Text, commandText, fromRecord, toRecord);
        }

        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado y devuelve un rango de registros en un DataTable.
        /// </summary>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="fromRecord">Primer registro a devolver.</param>
        /// <param name="toRecord">Ultimo registro a devolver. Si -1 devuelve hasta el final</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Un dataSet con el rango de filas indicado.</returns>
        public DataSet ExecuteRangeDataSet(CommandType commandType, string commandText, int fromRecord, int toRecord,
            params OleDbParameter[] commandParameters)
        {
            #region Control de argumentos erroneos

            if (commandType != CommandType.StoredProcedure && commandType != CommandType.Text)
                throw new ArgumentException("Command type must be CommandType.Text or CommandType.StoredProcedure",
                    "commandType");
            if (string.IsNullOrEmpty(commandText))
                throw new ArgumentNullException("commandText");
            if (toRecord != -1 && toRecord < fromRecord)
                throw new ArgumentException("Wrong rows range", "fromRecord,toRecord");

            #endregion

            //Obtenemos la conexión
            using (var conn = Connection)
                //Preparamos el comando
            using (var command = PrepareCommand(conn, commandType, commandText, commandParameters))
                //Llenamos el DataSet
            using (var da = new OleDbDataAdapter(command))
            {
                var rt = new DataSet();
                rt.Locale = CultureInfo.InvariantCulture;
                da.Fill(rt, fromRecord - 1, toRecord == -1 ? 0 : toRecord - fromRecord + 1, "Search");
                // Si la select no devuelve ningun registro no se añade tabla al dataset
                command.Dispose();
                return rt;
            }
        }

        /// <summary>
        ///     Ejecuta una sentencia y devuelve un rango de registros en un DataSet.
        /// </summary>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="fromRecord">Primer registro a devolver.</param>
        /// <param name="toRecord">Ultimo registro a devolver. Si -1 devuelve hasta el final</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Un dataSet con el rango de filas indicado.</returns>
        public DataSet ExecuteRangeDataSet(string commandText, int fromRecord, int toRecord,
            params OleDbParameter[] commandParameters)
        {
            return ExecuteRangeDataSet(CommandType.Text, commandText, fromRecord, toRecord, commandParameters);
        }

        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado y devuelve un rango de registros en un DataSet.
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="fromRecord">Primer registro a devolver.</param>
        /// <param name="toRecord">Ultimo registro a devolver.</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Un DataSet con el rango de filas indicado.</returns>
        public static DataSet ExecuteRangeDataSet(OleDbTransaction transaction, CommandType commandType,
            string commandText, int fromRecord, int toRecord, params OleDbParameter[] commandParameters)
        {
            #region Control de argumentos erroneos

            if (commandType != CommandType.StoredProcedure && commandType != CommandType.Text)
                throw new ArgumentException("Command type must be CommandType.Text or CommandType.StoredProcedure",
                    "commandType");
            if (string.IsNullOrEmpty(commandText))
                throw new ArgumentNullException("commandText");
            if (toRecord < fromRecord)
                throw new ArgumentException("Wrong rows range", "fromRecord,toRecord");
            // Si el 'desde' es negativo lo ponemos a 0
            if (fromRecord < 0) fromRecord = 0;

            #endregion

            //Preparamos el comando
            var command = PrepareCommand(transaction, commandType, commandText, commandParameters);
            //Llenamos el DataSet
            var da = new OleDbDataAdapter(command);
            var rt = new DataSet();
            rt.Locale = CultureInfo.InvariantCulture;
            da.Fill(rt, fromRecord - 1, toRecord - fromRecord + 1, "Search");
            command.Dispose();
            return rt;
        }

        /// <summary>
        ///     Ejecuta una sentencia y devuelve un rango de registros en un DataSet.
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="fromRecord">Primer registro a devolver.</param>
        /// <param name="toRecord">Ultimo registro a devolver.</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Un DataSet con el rango de filas indicado.</returns>
        public static DataSet ExecuteRangeDataSet(OleDbTransaction transaction, string commandText, int fromRecord,
            int toRecord, params OleDbParameter[] commandParameters)
        {
            return ExecuteRangeDataSet(transaction, CommandType.Text, commandText, fromRecord, toRecord,
                commandParameters);
        }

        #endregion

        #region ExecuteRangeDatatable

        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado y devuelve un rango de registros en un DataTable.
        /// </summary>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="fromRecord">Primer registro a devolver.</param>
        /// <param name="toRecord">Ultimo registro a devolver.</param>
        /// <returns>Un datatable con el rango de filas indicado.</returns>
        public DataTable ExecuteRangeDataTable(CommandType commandType, string commandText, int fromRecord,
            int toRecord)
        {
            return ExecuteRangeDataTable(commandType, commandText, fromRecord, toRecord, null);
        }

        /// <summary>
        ///     Ejecuta una sentencia y devuelve un rango de registros en un DataTable.
        /// </summary>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="fromRecord">Primer registro a devolver.</param>
        /// <param name="toRecord">Ultimo registro a devolver.</param>
        /// <returns>Un datatable con el rango de filas indicado.</returns>
        public DataTable ExecuteRangeDataTable(string commandText, int fromRecord, int toRecord)
        {
            return ExecuteRangeDataTable(CommandType.Text, commandText, fromRecord, toRecord);
        }

        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado y devuelve un rango de registros en un DataTable.
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="fromRecord">Primer registro a devolver.</param>
        /// <param name="toRecord">Ultimo registro a devolver.</param>
        /// <returns>Un datatable con el rango de filas indicado.</returns>
        public static DataTable ExecuteRangeDataTable(OleDbTransaction transaction, CommandType commandType,
            string commandText, int fromRecord, int toRecord)
        {
            return ExecuteRangeDataTable(transaction, commandType, commandText, fromRecord, toRecord, null);
        }

        /// <summary>
        ///     Ejecuta una sentencia y devuelve un rango de registros en un DataTable.
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="fromRecord">Primer registro a devolver.</param>
        /// <param name="toRecord">Ultimo registro a devolver.</param>
        /// <returns>Un datatable con el rango de filas indicado.</returns>
        public static DataTable ExecuteRangeDataTable(OleDbTransaction transaction, string commandText, int fromRecord,
            int toRecord)
        {
            return ExecuteRangeDataTable(transaction, CommandType.Text, commandText, fromRecord, toRecord);
        }

        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado y devuelve un rango de registros en un DataTable.
        /// </summary>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="fromRecord">Primer registro a devolver.</param>
        /// <param name="toRecord">Ultimo registro a devolver. Si -1 devuelve hasta el final</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Un datatable con el rango de filas indicado.</returns>
        public DataTable ExecuteRangeDataTable(CommandType commandType, string commandText, int fromRecord,
            int toRecord,
            params OleDbParameter[] commandParameters)
        {
            var rt = ExecuteRangeDataSet(commandType, commandText, fromRecord, toRecord, commandParameters);
            // Si la select no devuelve ningun registro no se añade tabla al dataset
            return rt.Tables.Count > 0 ? rt.Tables[0] : null;
        }

        /// <summary>
        ///     Ejecuta una sentencia y devuelve un rango de registros en un DataTable.
        /// </summary>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="fromRecord">Primer registro a devolver.</param>
        /// <param name="toRecord">Ultimo registro a devolver. Si -1 devuelve hasta el final</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Un datatable con el rango de filas indicado.</returns>
        public DataTable ExecuteRangeDataTable(string commandText, int fromRecord, int toRecord,
            params OleDbParameter[] commandParameters)
        {
            return ExecuteRangeDataTable(CommandType.Text, commandText, fromRecord, toRecord, commandParameters);
        }


        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado y devuelve un rango de registros en un DataTable.
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="fromRecord">Primer registro a devolver.</param>
        /// <param name="toRecord">Ultimo registro a devolver.</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Un datatable con el rango de filas indicado.</returns>
        public static DataTable ExecuteRangeDataTable(OleDbTransaction transaction, CommandType commandType,
            string commandText, int fromRecord, int toRecord, params OleDbParameter[] commandParameters)
        {
            var rt = ExecuteRangeDataSet(transaction, commandType, commandText, fromRecord, toRecord,
                commandParameters);
            // Si la select no devuelve ningun registro no se añade tabla al dataset
            return rt.Tables.Count > 0 ? rt.Tables[0] : null;
        }

        /// <summary>
        ///     Ejecuta una sentencia y devuelve un rango de registros en un DataTable.
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="fromRecord">Primer registro a devolver.</param>
        /// <param name="toRecord">Ultimo registro a devolver.</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Un datatable con el rango de filas indicado.</returns>
        public static DataTable ExecuteRangeDataTable(OleDbTransaction transaction, string commandText, int fromRecord,
            int toRecord, params OleDbParameter[] commandParameters)
        {
            return ExecuteRangeDataTable(transaction, CommandType.Text, commandText, fromRecord, toRecord,
                commandParameters);
        }

        #endregion

        #region ExecuteReader

        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado que devuelve un conjunto de registros y devuelve un DataReader
        /// </summary>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <returns>Un datareader sobre el conjunto de resultados</returns>
        public OleDbDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            return ExecuteReader(commandType, commandText, null);
        }

        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado que devuelve un conjunto de registros y devuelve un DataReader
        /// </summary>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <returns>Un datareader sobre el conjunto de resultados</returns>
        public OleDbDataReader ExecuteReader(string commandText)
        {
            return ExecuteReader(CommandType.Text, commandText);
        }


        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado que devuelve un conjunto de registros y devuelve un dataset
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <returns>Un datareader sobre el conjunto de resultados</returns>
        public static OleDbDataReader ExecuteReader(OleDbTransaction transaction, CommandType commandType,
            string commandText)
        {
            return ExecuteReader(transaction, commandType, commandText, null);
        }

        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado que devuelve un conjunto de registros y devuelve un dataset
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <returns>Un datareader sobre el conjunto de resultados</returns>
        public static OleDbDataReader ExecuteReader(OleDbTransaction transaction, string commandText)
        {
            return ExecuteReader(transaction, CommandType.Text, commandText);
        }


        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado con parametros que devuelve un conjunto de registros y devuelve un
        ///     dataset
        /// </summary>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Un datareader sobre el conjunto de resultados</returns>
        public OleDbDataReader ExecuteReader(CommandType commandType, string commandText,
            params OleDbParameter[] commandParameters)
        {
            if (commandType != CommandType.StoredProcedure && commandType != CommandType.Text)
                throw new ArgumentException("Command type must be CommandType.Text or CommandType.StoredProcedure",
                    "commandType");
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");

            //Preparamos el comando
            using (var command = PrepareCommand(Connection, commandType, commandText, commandParameters))
            {
                //Construimos el datareader
                var result = command.ExecuteReader(CommandBehavior.CloseConnection);
                command.Dispose();
                return result;
            }
        }

        /// <summary>
        ///     Ejecuta una sentencia con parametros que devuelve un conjunto de registros y devuelve un dataset
        /// </summary>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Un datareader sobre el conjunto de resultados</returns>
        public OleDbDataReader ExecuteReader(string commandText, params OleDbParameter[] commandParameters)
        {
            return ExecuteReader(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        ///     Ejecuta una sentencia o procedimiento almacenado y devuelve el valor de la primera fila de la primera columna
        /// </summary>
        /// <param name="transaction">Tansacción en la que se englobara la ejecución</param>
        /// <param name="commandType">
        ///     Puede ser CommandType.Text (si se trata de una sentencia SQL) o CommandType.StoredProcedure
        ///     (si se trata de un procedimiento almacenado)
        /// </param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Un datareader sobre el conjunto de resultados</returns>
        public static OleDbDataReader ExecuteReader(OleDbTransaction transaction, CommandType commandType,
            string commandText, params OleDbParameter[] commandParameters)
        {
            if (commandType != CommandType.StoredProcedure && commandType != CommandType.Text)
                throw new ArgumentException("Command type must be CommandType.Text or CommandType.StoredProcedure",
                    "commandType");
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");

            //Preparamos el comando
            var command = PrepareCommand(transaction, commandType, commandText, commandParameters);
            {
                //Construimos el datareader
                var result = command.ExecuteReader(CommandBehavior.CloseConnection);
                command.Dispose();
                return result;
            }
        }

        /// <summary>
        ///     Ejecuta una sentencia y devuelve el valor de la primera fila de la primera columna
        /// </summary>
        /// <param name="transaction">Transacción en la que se englobara la ejecución</param>
        /// <param name="commandText">Sentencia SQL o procedimiento almacenado a ejecutar</param>
        /// <param name="commandParameters">Parametros con los que se ejecutará la sentencia o procedimiento</param>
        /// <returns>Un datareader sobre el conjunto de resultados</returns>
        public static OleDbDataReader ExecuteReader(OleDbTransaction transaction, string commandText,
            params OleDbParameter[] commandParameters)
        {
            return ExecuteReader(transaction, CommandType.Text, commandText, commandParameters);
        }

        #endregion

        #region UpdateDataset

        /// <summary>
        ///     Ejecuta el comando adecuado para cada fila cambiada, añadida o borrada
        /// </summary>
        /// <param name="insertCommand">Comando que se utilizará para realizar la inserción de registros</param>
        /// <param name="deleteCommand">Comando que se utilizará para realizar el borrado de registros</param>
        /// <param name="updateCommand">Comando que se utilizará para realizar la actualización de registros</param>
        /// <param name="dataTable">DataTable que contiene los datos</param>
        public void UpdateDataSet(OleDbCommand insertCommand, OleDbCommand deleteCommand, OleDbCommand updateCommand,
            DataTable dataTable)
        {
            if (dataTable == null) throw new ArgumentNullException("dataTable");

            // Create a SqlDataAdapter, and dispose of it after we are done
            using (var da = new OleDbDataAdapter())
            {
                // Set the data adapter commands
                //
                if (updateCommand != null && updateCommand.Connection == null)
                {
                    updateCommand.Connection = Connection;
                    da.UpdateCommand = updateCommand;
                }

                if (insertCommand != null && insertCommand.Connection == null)
                {
                    insertCommand.Connection = Connection;
                    da.InsertCommand = insertCommand;
                }

                if (deleteCommand != null && deleteCommand.Connection == null)
                {
                    da.DeleteCommand = deleteCommand;
                    deleteCommand.Connection = Connection;
                }

                // Update the dataset changes in the data source
                da.Update(dataTable);

                Log.TraceInfo("Updating datable {0}. UpdateCommand:'{1}'. InsertCommand:'{2}'. DeleteCommand:'{3}'",
                    dataTable.TableName, updateCommand == null ? "" : updateCommand.CommandText,
                    insertCommand == null ? "" : insertCommand.CommandText,
                    deleteCommand == null ? "" : deleteCommand.CommandText);
            }
        }

        /// <summary>
        ///     Actualiza el DataTable usando comandos construidos automaticamente
        /// </summary>
        /// <param name="selectCommand">Select utilizada para obtener los datos del DataTable</param>
        /// <param name="dataTable">DataTable que contiene los datos</param>
        public void UpdateDataSet(OleDbCommand selectCommand, DataTable dataTable)
        {
            if (selectCommand == null) throw new ArgumentNullException("selectCommand");
            if (dataTable == null) throw new ArgumentNullException("dataTable");

            if (selectCommand.Connection == null)
                selectCommand.Connection = Connection;

            using (var da = new OleDbDataAdapter(selectCommand))
            {
                da.Update(dataTable);
            }

            Log.TraceInfo("Updating datatable {0}. SelectCommand:'{1}'",
                dataTable.TableName, selectCommand.CommandText);
        }

        #endregion

        #region CreateParam

        /// <summary>
        ///     Crea un parámetro y establece adecuadamente sus propiedades
        /// </summary>
        /// <param name="name">Nombre del parámetro</param>
        /// <param name="value">Valor del parámetro</param>
        /// <returns>El parámetro creado y con sus propiedades adecuadamente establecidas</returns>
        public static OleDbParameter CreateParam(string name, string value)
        {
            if (string.IsNullOrEmpty(value))
                return CreateParam(name, DBNull.Value);

            var p = new OleDbParameter();
            p.ParameterName = name;
            p.Value = value;
            p.OleDbType = OleDbType.VarWChar;

            return p;
        }

        /// <summary>
        ///     Crea un parámetro y establece adecuadamente sus propiedades
        /// </summary>
        /// <param name="name">Nombre del parámetro</param>
        /// <param name="value">Valor del parámetro</param>
        /// <returns>El parámetro creado y con sus propiedades adecuadamente establecidas</returns>
        public static OleDbParameter CreateParam(string name, DateTime? value)
        {
            if (value == null)
                return CreateParam(name, DBNull.Value);

            var p = new OleDbParameter();
            p.ParameterName = name;
            p.Value = value;
            p.OleDbType = OleDbType.DBTimeStamp;

            return p;
        }

        /// <summary>
        ///     Crea un parámetro y establece adecuadamente sus propiedades
        /// </summary>
        /// <param name="name">Nombre del parámetro</param>
        /// <param name="value">Valor del parámetro</param>
        /// <returns>El parámetro creado y con sus propiedades adecuadamente establecidas</returns>
        public static OleDbParameter CreateParam(string name, int? value)
        {
            if (value == null)
                return CreateParam(name, DBNull.Value);

            var p = new OleDbParameter();
            p.ParameterName = name;
            p.Value = value;
            p.OleDbType = OleDbType.Integer;

            return p;
        }

        /// <summary>
        ///     Crea un parámetro y establece adecuadamente sus propiedades
        /// </summary>
        /// <param name="name">Nombre del parámetro</param>
        /// <param name="value">Valor del parámetro</param>
        /// <returns>El parámetro creado y con sus propiedades adecuadamente establecidas</returns>
        public static OleDbParameter CreateParam(string name, bool? value)
        {
            if (value == null)
                return CreateParam(name, DBNull.Value);

            //Ojo, por compatibilidad con Oracle los booleanos se tratan como enteros
            return CreateParam(name, Convert.ToInt32(value, CultureInfo.InstalledUICulture));
        }


        /// <summary>
        ///     Crea un parámetro y establece adecuadamente sus propiedades
        /// </summary>
        /// <param name="name">Nombre del parámetro</param>
        /// <param name="value">Valor del parámetro</param>
        /// <returns>El parámetro creado y con sus propiedades adecuadamente establecidas</returns>
        public static OleDbParameter CreateParam(string name, long? value)
        {
            if (value == null)
                return CreateParam(name, DBNull.Value);

            var p = new OleDbParameter();
            p.ParameterName = name;
            p.Value = value;
            p.OleDbType = OleDbType.BigInt;

            return p;
        }

        /// <summary>
        ///     Crea un parámetro y establece adecuadamente sus propiedades
        /// </summary>
        /// <param name="name">Nombre del parámetro</param>
        /// <param name="value">Valor del parámetro</param>
        /// <returns>El parámetro creado y con sus propiedades adecuadamente establecidas</returns>
        public static OleDbParameter CreateParam(string name, decimal? value)
        {
            if (value == null)
                return CreateParam(name, DBNull.Value);

            var p = new OleDbParameter();
            p.ParameterName = name;
            p.Value = value;
            p.OleDbType = OleDbType.Decimal;

            return p;
        }

        /// <summary>
        ///     Crea un parámetro y establece adecuadamente sus propiedades
        /// </summary>
        /// <param name="name">Nombre del parámetro</param>
        /// <param name="value">Valor del parámetro</param>
        /// <returns>El parámetro creado y con sus propiedades adecuadamente establecidas</returns>
        public static OleDbParameter CreateParam(string name, Stream value)
        {
            if (value == null)
                return CreateParam(name, DBNull.Value);
            var valueInBytes = ReadStream(value);

            var p = new OleDbParameter(name, OleDbType.LongVarBinary, valueInBytes.Length);
            p.Value = valueInBytes;

            return p;
        }

        /// <summary>
        ///     Lee un Stream y lo vuelca en un array de bytes
        /// </summary>
        /// <param name="stream">el stream a leer</param>
        /// <returns>el array de bytes</returns>
        private static byte[] ReadStream(Stream stream)
        {
            using (var memStream = new MemoryStream())
            {
                int bytesRead;
                var respBuffer = new byte[2048];

                while ((bytesRead = stream.Read(respBuffer, 0, respBuffer.Length)) != 0)
                    memStream.Write(respBuffer, 0, bytesRead);

                return memStream.ToArray();
            }
        }

        /// <summary>
        ///     Crea un parámetro y establece adecuadamente sus propiedades
        /// </summary>
        /// <param name="name">Nombre del parámetro</param>
        /// <param name="value">Valor del parámetro</param>
        /// <returns>El parámetro creado y con sus propiedades adecuadamente establecidas</returns>
        public static OleDbParameter CreateParam(string name, DBNull value)
        {
            var p = new OleDbParameter();
            p.ParameterName = name;
            p.Value = value;

            return p;
        }

        #endregion
    }
}