using Microsoft.Win32;
using System.Runtime.CompilerServices;

namespace FSLibrary
{
    /// <summary>
    /// Funciones para la ejecución automática de la aplicación cuando se inicie el ordenador.
    /// </summary>
    public static class Autorun
    {
        static readonly string runPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        static readonly RegistryKey registryKey = Registry.CurrentUser;
        static RegistryKey rkApp = registryKey.OpenSubKey(runPath, true);

        static string _app;
        static string _appPath;

        /// <summary>
        /// Función para instalar la aplicación en el registro
        /// </summary>
        /// <param name="app">Nombre de la aplicación</param>
        /// <param name="appPath">Path al ejecutable</param>
        /// <param name="force">Desinstalamos la aplicación si esta instalada</param>
        public static void Install(string app, string appPath, bool force = false)
        {
            _app = System.IO.Path.GetFileNameWithoutExtension(app);
            _appPath = appPath;

            if (force)
                UnInstall(_app);

            if (!IsStartupItem(_app))
                rkApp.SetValue(_app, _appPath);
        }

        /// <summary>
        /// Función para desinstalar la aplicación del inicio del ordenador.
        /// </summary>
        public static void UnInstall()
        {
            UnInstall(_app);
        }

        /// <summary>
        /// Función para desinstalar la aplicación del inicio del ordenador.
        /// </summary>
        /// <param name="app"></param>
        public static void UnInstall(string app)
        {
            if (IsStartupItem(app))
                rkApp.DeleteValue(app, false);
        }

        /// <summary>
        /// Función para comprobar si una aplicación esta instalada para ejecutarse al inicio.
        /// </summary>
        /// <returns></returns>
        public static bool IsStartupItem()
        {
            return IsStartupItem(_app);
        }

        private static bool IsStartupItem(string app)
        {
            if (rkApp.GetValue(app) == null)
                return false;
            else
            {
                if (rkApp.GetValue(app).ToString().ToLower() != _appPath.ToLower())
                    return false;
                else
                    return true;
            }
        }
    }
}
