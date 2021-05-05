#region

using System;
using System.Windows.Forms;
using Microsoft.Win32;

#endregion

namespace FSFormControls
{
    public class Registry
    {
        private const string SOFTWARE_KEY = "Software";
        private const string COMPANY_NAME = "MyCompany";
        private const string APPLICATION_NAME = "MyApplication";

        public string GetStringRegisTryValue(string key, string defaultValue)
        {
            RegistryKey rkCompany = null;
            RegistryKey rkApplication = null;

            rkCompany = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, false)
                .OpenSubKey(COMPANY_NAME, false);
            if (!(rkCompany == null))
            {
                rkApplication = rkCompany.OpenSubKey(APPLICATION_NAME, true);
                if (!(rkApplication == null))
                    foreach (var sKey in rkApplication.GetValueNames())
                        if (sKey == key)
                            return Convert.ToString(rkApplication.GetValue(sKey));
            }

            return defaultValue;
        }


        public void SetStringRegisTryValue(string key, string stringValue)
        {
            RegistryKey rkSoftware = null;
            RegistryKey rkCompany = null;
            RegistryKey rkApplication = null;

            rkSoftware = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, true);
            rkCompany = rkSoftware.CreateSubKey(COMPANY_NAME);
            if (!(rkCompany == null))
            {
                rkApplication = rkCompany.CreateSubKey(APPLICATION_NAME);
                if (!(rkApplication == null)) rkApplication.SetValue(key, stringValue);
            }
        }


        public string GetUserRegistryValue(string key, string defaultValue)
        {
            return Convert.ToString(Application.UserAppDataRegistry.GetValue(key, defaultValue));
        }


        public void SetUserRegistryValue(string key, string value)
        {
            Application.UserAppDataRegistry.SetValue(key, value);
        }
    }
}