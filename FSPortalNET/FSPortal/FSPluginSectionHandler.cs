// // <fileheader>
// // <copyright file="FSPluginSectionHandler.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSPortal
// //     Solution: FSPortalNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System;
using System.Configuration;
using System.Xml;
using FSPlugin;
using FSLibrary;
using FSException;

#endregion

namespace FSPortal
{
    /// <summary>
    ///     This class implements IConfigurationSectionHandler and allows
    ///     us to parse the "plugin" XML nodes found inside App.Config
    ///     and return a PluginCollection object
    /// </summary>
    public class PluginSectionHandler : IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler Members

        /// <summary>
        ///     Iterate through all the child nodes
        ///     of the XMLNode that was passed in and create instances
        ///     of the specified Types by reading the attribite values of the nodes
        ///     we use a try/Catch here because some of the nodes
        ///     might contain an invalid reference to a plugin type
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="configContext"></param>
        /// <param name="section">The XML section we will iterate against</param>
        /// <returns></returns>
        public object Create(object parent, object configContext, XmlNode section)
        {
            PluginCollection plugins = new PluginCollection();
            foreach (XmlNode node in section.ChildNodes)
            {
                try
                {
                    //Use the Activator class's 'CreateInstance' method
                    //to try and create an instance of the plugin by
                    //passing in the type name specified in the attribute value
                    object plugObject = Activator.CreateInstance(Type.GetType(node.Attributes["type"].Value));

                    //Cast this to an IPlugin interface and add to the collection
                    IPlugin plugin = (IPlugin) plugObject;
                    plugins.Add(plugin);
                }
                catch (System.Exception e)
                {
                    throw new ExceptionUtil(e);
                }
            }

            return plugins;
        }

        #endregion
    }
}