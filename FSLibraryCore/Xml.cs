#region

using FSExceptionCore;
using System;
using System.Xml;

#endregion

namespace FSLibraryCore
{
    /// <summary>
    /// Clase para la utilización de XML con ficheros.
    /// </summary>
    public class Xml
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Xml"/> class.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public Xml(string filename)
        {
            Filename = filename;

            Document.Load(Filename);
        }

        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        /// <value>
        /// The filename.
        /// </value>
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the document.
        /// </summary>
        /// <value>
        /// The document.
        /// </value>
        public XmlDocument Document { get; set; } = new XmlDocument();

        /// <summary>
        /// Gets the configuration value.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns></returns>
        /// <exception cref="ExceptionUtil">
        /// </exception>
        public string GetConfigValue(string sectionName, string settingName)
        {
            string valueRet = null;

            var xpath = string.Format("//section[@name='{0}']/setting[@name='{1}']", sectionName, settingName);
            var node = Document.DocumentElement.SelectSingleNode(xpath);

            if (node == null)
                throw new ExceptionUtil(string.Format("No such setting, using the following xpath:{0}{1}",
                    Environment.NewLine, xpath));

            var xmlAttr = node.Attributes["value"];
            if (xmlAttr == null)
                throw new ExceptionUtil(string.Format("No value for this setting, using the following xpath:{0}{1}",
                    Environment.NewLine, xpath));
            valueRet = xmlAttr.Value;

            return valueRet;
        }


        /// <summary>
        /// Sets the configuration value.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="settingValue">The setting value.</param>
        /// <exception cref="ExceptionUtil">
        /// </exception>
        public void SetConfigValue(string sectionName, string settingName, string settingValue)
        {
            var xpath = string.Format("//section[@name='{0}']/setting[@name='{1}']", sectionName, settingName);
            var node = Document.DocumentElement.SelectSingleNode(xpath);

            object transTemp2 = node;
            if (transTemp2 == null)
                throw new ExceptionUtil(string.Format("No such setting, using the following xpath:{0}{1}",
                    Environment.NewLine, xpath));

            var xmlAttr = node.Attributes["value"];
            object transTemp3 = xmlAttr;
            if (transTemp3 == null)
                throw new ExceptionUtil(string.Format("No value for this setting, using the following xpath:{0}{1}",
                    Environment.NewLine, xpath));
            xmlAttr.Value = settingValue;
        }

        /// <summary>
        /// Saves the file in XML.
        /// </summary>
        public void Save()
        {
            Document.Save(Filename);
        }
    }
}