using Microsoft.Win32;
using System.Windows.Forms;

namespace FSLibraryCore
{
    /// <summary>
    /// Funciones para la ejecución automática de la aplicación cuando se inicie el ordenador.
    /// </summary>
    public static class Autorun
    {
        static readonly string runPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        static readonly RegistryKey registryKey = Registry.CurrentUser;

        /// <summary>
        /// Instala la aplicación para ejecutarse al inicio.
        /// </summary>
        public static void Install()
        {
            Install(Application.ProductName, Application.ExecutablePath);
        }

        /// <summary>
        /// Función para instalar la aplicación en el registro
        /// </summary>
        /// <param name="app">Nombre de la aplicación</param>
        /// <param name="appPath">Path al ejecutable</param>
        public static void Install(string app, string appPath)
        {
            RegistryKey rkApp = registryKey.OpenSubKey(runPath, true);

            if (!IsStartupItem())
                rkApp.SetValue(app, appPath);
        }

        /// <summary>
        /// Función para desinstalar la aplicación del inicio del ordenador.
        /// </summary>
        public static void UnInstall()
        {
            UnInstall(Application.ProductName);
        }

        /// <summary>
        /// Función para desinstalar la aplicación del inicio del ordenador.
        /// </summary>
        /// <param name="app"></param>
        public static void UnInstall(string app)
        {
            RegistryKey rkApp = registryKey.OpenSubKey(runPath, true);

            if (IsStartupItem())
                rkApp.DeleteValue(app, false);
        }

        /// <summary>
        /// Función para comprobar si una aplicación esta instalada para ejecutarse al inicio.
        /// </summary>
        /// <returns></returns>
        public static bool IsStartupItem()
        {
            return IsStartupItem(Application.ProductName);
        }

        private static bool IsStartupItem(string app)
        {
            RegistryKey rkApp = registryKey.OpenSubKey(runPath, true);

            if (rkApp.GetValue(app) == null)
                return false;
            else
            {
                if (rkApp.GetValue(Application.ProductName).ToString().ToLower() != Application.ExecutablePath.ToString().ToLower())
                    return false;
                else
                    return true;
            }
        }
    }
}
