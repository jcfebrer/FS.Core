using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSLibrary
{
    /// <summary>
    /// Funciones para acceso al fichero de configuración App.config
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Actualizar propiedad.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void UpdateProperty(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection appSettings = config.AppSettings.Settings;


            // update SaveBeforeExit
            config.AppSettings.Settings[key].Value = value;
            
            //save the file
            config.Save(ConfigurationSaveMode.Modified);
            
            //relaod the section you modified
            ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
        }

        /// <summary>
        /// Actualizar propiedad elimandola antes.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void UpdatePropertyWithRemove(string key, string value)
        {
            // Get the application configuration file.
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Remove(key);
            config.AppSettings.Settings.Add(key, value);

            SaveConfigFile(config);
        }

        /// <summary>
        /// Leemos las propiedades de AppSettings
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<KeyValuePair<string, string>> ReadProperties()
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            var section = ConfigurationManager.GetSection("applicationSettings");

            // Get the AppSettings section.
            NameValueCollection appSettings = ConfigurationManager.AppSettings;

            if (appSettings.Count == 0)
            {
                throw new Exception(string.Format("[ReadAppSettings: {0}]", "AppSettings is empty Use GetSection command first."));
            }
            for (int i = 0; i < appSettings.Count; i++)
            {
                KeyValuePair<string, string> keyValue = new KeyValuePair<string, string>(appSettings.GetKey(i), appSettings[i]);

                result.Add(keyValue);
            }

            return result;
        }

        /// <summary>
        /// Añadimos una propiedad.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void InsertProperty(string key, string value)
        {
            // Get the application configuration file.
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Add(key, value);

            SaveConfigFile(config);
        }

        /// <summary>
        /// Borramos una propiedad.
        /// </summary>
        /// <param name="key"></param>
        public static void DeleteProperty(string key)
        {
            // Get the application configuration file.
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove(key);

            SaveConfigFile(config);
        }

        /// <summary>
        /// Recuperamos el valor de una propiedad.
        /// </summary>
        /// <param name="key"></param>
        public static string ValueProperty(string key)
        {
            // Get the application configuration file.
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            return config.AppSettings.Settings[key].Value;
        }

        /// <summary>
        /// Guardamos el valor de una propiedad.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetProperty(string key, string value)
        {
            // Get the application configuration file.
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;

            SaveConfigFile(config);
        }

        /// <summary>
        /// Guardamos la configuracíón en el fichero config.
        /// </summary>
        public static void SaveConfigFile()
        {
            // Get the application configuration file.
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            SaveConfigFile(config);
        }

        /// <summary>
        /// Guardamos la configuracíón en el fichero config.
        /// </summary>
        /// <param name="config"></param>
        private static void SaveConfigFile(Configuration config)
        {
            string sectionName = "appSettings";

            // Save the configuration file.
            config.Save(ConfigurationSaveMode.Modified);

            // Force a reload of the changed section. This  
            // makes the new values available for reading.
            ConfigurationManager.RefreshSection(sectionName);

            // Get the AppSettings section.
            AppSettingsSection appSettingSection =
              (AppSettingsSection)config.GetSection(sectionName);

            //Console.WriteLine(appSettingSection.SectionInformation.GetRawXml());
        }
    }
}