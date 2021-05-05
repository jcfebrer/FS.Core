using System;
using System.Configuration;
using System.Xml;

namespace FSPlugin
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
                    Type pluginType = Type.GetType(node.Attributes["type"].Value);
                    object plugObject = Activator.CreateInstance(pluginType);

                    if (plugObject == null)
                        throw new Exception("No es posible instanciar el plugin: " + pluginType.ToString());

                    //Cast this to an IPlugin interface and add to the collection
                    IPlugin plugin = (IPlugin) plugObject;
                    plugins.Add(plugin);
                }
                catch
                {
                }
            }

            return plugins;
        }

        #endregion
    }
}