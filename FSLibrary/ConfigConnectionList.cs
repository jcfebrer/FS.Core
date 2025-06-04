using FSException;
using System.Configuration;

namespace FSLibrary
{
    /// <summary>
    /// Clase lista de conexiones a la base de datos definidas en el fichero web.config.
    /// </summary>
    public class ConfigConnectionList
    {
        /// <summary>
        /// Devuelve el id de la entrada ConnectionString en el fichero web.config.
        /// </summary>
        /// <param name="entryName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Devuelve el nombre de la entrada ConnectionString en el fichero web.config.
        /// </summary>
        /// <param name="entryId"></param>
        /// <returns></returns>
        /// <exception cref="ExceptionUtil"></exception>
        public static string GetConnectionName(int entryId)
        {
            if (ConfigurationManager.ConnectionStrings.Count == 0)
                throw new ExceptionUtil("No ay entradas ConnectionStrings en el fichero web.config");
            if (ConfigurationManager.ConnectionStrings[entryId] == null)
                throw new ExceptionUtil("Entrada de ConnectionString inexistente en web.config (" + entryId + ")");
            return ConfigurationManager.ConnectionStrings[entryId].Name;
        }

        /// <summary>
        /// Devuelve la cadena de conexión de la entrada ConnectionString en el fichero web.config.
        /// </summary>
        /// <param name="entryId"></param>
        /// <returns></returns>
        /// <exception cref="ExceptionUtil"></exception>
        public static string GetConnectionString(int entryId)
        {
            if (ConfigurationManager.ConnectionStrings.Count == 0)
                throw new ExceptionUtil("No ay entradas ConnectionStrings en el fichero web.config");
            if (ConfigurationManager.ConnectionStrings[entryId] == null)
                throw new ExceptionUtil("Entrada de ConnectionString inexistente en web.config (" + entryId + ")");
            return ConfigurationManager.ConnectionStrings[entryId].ConnectionString;
        }
    }
}
