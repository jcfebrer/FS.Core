using Microsoft.Win32;
using System.Windows.Forms;

namespace FSLibrary
{
    public static class Autorun
    {
        static string runPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        public static RegistryKey registryKey = Registry.CurrentUser;

        public static void Install()
        {
            Install(Application.ProductName, Application.ExecutablePath);
        }

        public static void Install(string app, string appPath)
        {
            RegistryKey rkApp = registryKey.OpenSubKey(runPath, true);

            if (!IsStartupItem())
                rkApp.SetValue(app, appPath);
        }

        public static void UnInstall()
        {
            UnInstall(Application.ProductName);
        }

        public static void UnInstall(string app)
        {
            RegistryKey rkApp = registryKey.OpenSubKey(runPath, true);

            if (IsStartupItem())
                rkApp.DeleteValue(app, false);
        }

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
