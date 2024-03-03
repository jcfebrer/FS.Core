#region

using System.IO;
using System.Net;
using System.Text;
using System.Xml;

#endregion

namespace FSLibraryCore
{
    /// <summary>
    /// Funciones para el manejo de XML.
    /// </summary>
    public class XMLParser
    {
        /// <summary>
        /// The document
        /// </summary>
        public XmlDocument Document = new XmlDocument();
        /// <summary>
        /// The document root
        /// </summary>
        public XmlNode DocumentRoot;

        /// <summary>
        /// Loads from text.
        /// </summary>
        /// <param name="xml">The XML.</param>
        public void LoadFromText(string xml)
        {
            Document.LoadXml(xml);
        }

        /// <summary>
        /// Loads from file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void LoadFromFile(string fileName)
        {
            Document.Load(fileName);
        }

        /// <summary>
        /// Loads from URL.
        /// </summary>
        /// <param name="Url">The URL.</param>
        public void LoadFromUrl(string Url)
        {
            LoadFromUrl(Url, Encoding.UTF8);
        }

        /// <summary>
        /// Loads from URL.
        /// </summary>
        /// <param name="Url">The URL.</param>
        /// <param name="enc">The enc.</param>
        public void LoadFromUrl(string Url, Encoding enc)
        {
            HttpWebRequest myRequest = null;
            var responseText = "";

            myRequest = (HttpWebRequest) WebRequest.Create(Url);
            var MyResponse = (HttpWebResponse) myRequest.GetResponse();
            var receiveStream = MyResponse.GetResponseStream();
            var readStream = new StreamReader(receiveStream, enc);
            responseText = readStream.ReadToEnd();

            MyResponse.Close();
            readStream.Close();

            Document.LoadXml(responseText);
        }

        /// <summary>
        /// Saves to file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public void SaveToFile(string filename)
        {
            Document.Save(filename);
        }

        /// <summary>
        /// Fetches the node.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="nodeName">Name of the node.</param>
        /// <returns></returns>
        public XmlNode fetchNode(XmlNodeList list, string nodeName)
        {
            for (int i = 0; i <= list.Count - 1; i++)
                if (list.Item(i).Name == nodeName)
                    return list.Item(i);
            return null;
        }

        /// <summary>
        /// Fetches the attribute.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns></returns>
        public XmlAttribute fetchAttribute(XmlNode node, string attributeName)
        {
            for (int i = 0; i <= node.Attributes.Count - 1; i++)
                if (node.Attributes[i].Name == attributeName)
                    return node.Attributes[i];
            return null;
        }


        /// <summary>
        /// Sets the node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="nodeName">Name of the node.</param>
        /// <param name="value">The value.</param>
        public void SetNode(XmlNode node, string nodeName, string value)
        {
            foreach(XmlNode xmlNode in DocumentRoot.ChildNodes)
            {
                if (xmlNode.Name == node.Name)
                    node.InnerText = value;
            }
        }
    }
}