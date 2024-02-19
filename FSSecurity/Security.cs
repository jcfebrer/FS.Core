using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace FSSecurity
{
    /// <summary>
    /// Clase con funciones de utilidad.
    /// </summary>
    public class Security
    {
        /// <summary>
        /// Devuelve true o false dependiendo de si la aplicación se esta ejecutando con permisos de administrador.
        /// </summary>
        /// <returns></returns>
        public static bool IsAdmin()
        {
            bool isElevated;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            return isElevated;
        }

        /// <summary>
        /// Devuelve el nombre de usuario conectado.
        /// </summary>
        /// <returns></returns>
        public static string Name()
        {
            string name;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                name = identity.Name;
            }

            return name;
        }
    }
}
