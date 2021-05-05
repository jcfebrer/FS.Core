using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FSSyscat.Classes
{
    static class Autorun
    {
        public static RegistryKey registryKey = Registry.CurrentUser;

        public static void Install()
        {
            RegistryKey rkApp = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (!IsStartupItem())
                rkApp.SetValue(Application.ProductName, Application.ExecutablePath.ToString());
        }

        public static void UnInstall()
        {
            RegistryKey rkApp = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (IsStartupItem())
                rkApp.DeleteValue(Application.ProductName, false);
        }

        private static bool IsStartupItem()
        {
            RegistryKey rkApp = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (rkApp.GetValue(Application.ProductName) == null)
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
