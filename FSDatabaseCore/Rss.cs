// // <fileheader>
// // <copyright file="RSS.cs" company="Febrer Software">
// //     Fecha: 03/07/2010
// //     Project: FSLibrary
// //     Solution: FSLibraryNET2008
// //     Copyright (c) 2010 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using FSParserCore;
using System;
using System.Collections;
using System.Net;
using System.Text;
using System.Xml;

#endregion

namespace FSDatabaseCore
{
    /// <summary>
    ///     Colección de líneas de RSS
    /// </summary>
    public class RssCollection : CollectionBase
    {
        /// <summary>
        ///     Línea RSS
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public RSSItem this[int index]
        {
            get { return (RSSItem) List[index]; }
            set { List[index] = value; }
        }

        /// <summary>
        ///     Añadir linea
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Add(RSSItem value)
        {
            return List.Add(value);
        }

        /// <summary>
        ///     Devuelve el indice
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(RSSItem value)
        {
            return List.IndexOf(value);
        }

        /// <summary>
        ///     Insertamos una línea de RSS
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Insert(int index, RSSItem value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        ///     Borramos una línea
        /// </summary>
        /// <param name="value"></param>
        public void Remove(RSSItem value)
        {
            List.Remove(value);
        }

        /// <summary>
        ///     Devuelve true/false si existe la línea en la colección
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(RSSItem value)
        {
            return List.Contains(value);
        }
    }


    /// <summary>
    ///     Enumeración para la versión de RSS
    /// </summary>
    public enum RSSVersion
    {
        /// <summary>
        /// The version unknown
        /// </summary>
        version_Unknown = 0,
        /// <summary>
        /// The version 0.91
        /// </summary>
        version_0_91 = 1,
        /// <summary>
        /// The version 0.92
        /// </summary>
        version_0_92 = 2,
        /// <summary>
        /// The version 2.00
        /// </summary>
        version_2_00 = 3,
        /// <summary>
        /// The version RDF
        /// </summary>
        version_RDF = 4
    }

    /// <summary>
    ///     Enumeración para las líneas del RSS
    /// </summary>
    public struct RSSItem
    {
        /// <summary>
        /// The description
        /// </summary>
        public string Description;
        /// <summary>
        /// The link
        /// </summary>
        public Uri Link;
        /// <summary>
        /// The title
        /// </summary>
        public string Title;
        /// <summary>
        /// The pub date
        /// </summary>
        public System.DateTime pubDate;
    }


    /// <summary>
    ///     Enumeración para la cabecera del RSS
    /// </summary>
    public struct RSSHeader
    {
        /// <summary>
        /// The copyright
        /// </summary>
        public string Copyright;
        /// <summary>
        /// The description
        /// </summary>
        public string Description;
        /// <summary>
        /// The language
        /// </summary>
        public string Language;
        /// <summary>
        /// The link
        /// </summary>
        public Uri Link;
        /// <summary>
        /// The title
        /// </summary>
        public string Title;
        /// <summary>
        /// The last build date
        /// </summary>
        public System.DateTime lastBuildDate;
        /// <summary>
        /// The managing editor
        /// </summary>
        public string managingEditor;
        /// <summary>
        /// The web master
        /// </summary>
        public string webMaster;
    }

    /// <summary>
    ///     Clase principal RSS
    /// </summary>
    public class Rss : XMLParser
    {
        private RSSHeader myRSSHeader;

        /// <summary>
        ///     Cosntructor
        /// </summary>
        public Rss()
        {
            Version = RSSVersion.version_Unknown;
        }

        /// <summary>
        ///     Versión del fichero RSS
        /// </summary>
        public RSSVersion Version { get; set; }


        /// <summary>
        ///     Devuelve/establece la colección líneas
        /// </summary>
        public RssCollection Items { get; set; } = new RssCollection();

        /// <summary>
        ///     Devuelve/establece la cabecera
        /// </summary>
        public RSSHeader Header
        {
            get { return myRSSHeader; }
            set { myRSSHeader = value; }
        }

        /// <summary>
        ///     Devuelve un elemento
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public RSSItem get_Item(int index)
        {
            return Items[index];
        }

        /// <summary>
        ///     Guardamos un valor en el indice indicado
        /// </summary>
        /// <param name="index"></param>
        /// <param name="Value"></param>
        public void set_Item(int index, RSSItem Value)
        {
            Items[index] = Value;
        }


        /// <summary>
        ///     Devuelve la versión del RSS
        /// </summary>
        /// <returns></returns>
        private RSSVersion getRSSVersion()
        {
            XmlNode myrootNode = null;
            XmlAttribute versionAttribute = null;
            RSSVersion myRSSVersion = 0;

            try
            {
                myRSSVersion = RSSVersion.version_Unknown;
                myrootNode = fetchNode(Document.ChildNodes, "rss");
                versionAttribute = fetchAttribute(myrootNode, "version");
                switch (versionAttribute.Value)
                {
                    case "0.91":
                        myRSSVersion = RSSVersion.version_0_91;
                        break;
                    case "0.92":
                        myRSSVersion = RSSVersion.version_0_92;
                        break;
                    case "2.00":
                        myRSSVersion = RSSVersion.version_2_00;
                        break;
                }

                return myRSSVersion;
            }
            catch
            {
                return RSSVersion.version_Unknown;
            }
        }


        /// <summary>
        ///     Rellenamos la clase con el contenido del fichero RSS especificado.
        /// </summary>
        private void PopulateMembers()
        {
            XmlNode myNode = null;
            XmlNode myItemNode = null;
            var i = 0;
            myRSSHeader.Copyright = "";
            myRSSHeader.Description = "";
            myRSSHeader.Language = "";
            myRSSHeader.Title = "";
            myRSSHeader.webMaster = "";
            myRSSHeader.managingEditor = "";
            myRSSHeader.lastBuildDate = new System.DateTime(0);
            myRSSHeader.Link = null;

            DocumentRoot = fetchNode(Document.ChildNodes, "rss");

            if (DocumentRoot == null)
            {
                DocumentRoot = fetchNode(Document.ChildNodes, "rdf:RDF");
                DocumentRoot = fetchNode(DocumentRoot.ChildNodes, "channel");
                Version = RSSVersion.version_RDF;
            }
            else
            {
                DocumentRoot = fetchNode(DocumentRoot.ChildNodes, "channel");
                Version = getRSSVersion();
            }

            myNode = fetchNode(DocumentRoot.ChildNodes, "title");
            if ((myNode != null)) myRSSHeader.Title = myNode.InnerText;
            myNode = fetchNode(DocumentRoot.ChildNodes, "link");
            if ((myNode != null) & (myNode.InnerText != "")) myRSSHeader.Link = new Uri(myNode.InnerText);
            myNode = fetchNode(DocumentRoot.ChildNodes, "description");
            if (myNode != null) myRSSHeader.Description = myNode.InnerText;
            myNode = fetchNode(DocumentRoot.ChildNodes, "language");
            if (myNode != null) myRSSHeader.Language = myNode.InnerText;
            myNode = fetchNode(DocumentRoot.ChildNodes, "copyright");
            if (myNode != null) myRSSHeader.Copyright = myNode.InnerText;
            myNode = fetchNode(DocumentRoot.ChildNodes, "managingEditor");
            if (myNode != null) myRSSHeader.managingEditor = myNode.InnerText;
            myNode = fetchNode(DocumentRoot.ChildNodes, "webMaster");
            if (myNode != null) myRSSHeader.webMaster = myNode.InnerText;

            switch (Version)
            {
                case RSSVersion.version_RDF:
                    myNode = fetchNode(DocumentRoot.ChildNodes, "dc:date");
                    if (myNode != null) myRSSHeader.lastBuildDate = System.DateTime.Parse(myNode.InnerText);
                    break;
                default:
                    myNode = fetchNode(DocumentRoot.ChildNodes, "lastBuildDate");
                    if (myNode != null) myRSSHeader.lastBuildDate = System.DateTime.Parse(myNode.InnerText);
                    break;
            }


            Items.Clear();

            if (Version == RSSVersion.version_RDF) DocumentRoot = fetchNode(Document.ChildNodes, "rdf:RDF");

            for (i = 0; i <= DocumentRoot.ChildNodes.Count - 1; i++)
                if (DocumentRoot.ChildNodes[i].Name == "item")
                {
                    myItemNode = DocumentRoot.ChildNodes[i];
                    RSSItem myItem;

                    myItem.pubDate = new System.DateTime(0);
                    myItem.Link = null;
                    myItem.Description = "";
                    myItem.Title = "";


                    myNode = fetchNode(myItemNode.ChildNodes, "description");
                    if (myNode != null) myItem.Description = myNode.InnerText;
                    myNode = fetchNode(myItemNode.ChildNodes, "title");
                    if (myNode != null) myItem.Title = myNode.InnerText;
                    myNode = fetchNode(myItemNode.ChildNodes, "link");
                    if ((myNode != null) & (myNode.InnerText != "")) myItem.Link = new Uri(myNode.InnerText);

                    switch (Version)
                    {
                        case RSSVersion.version_RDF:
                            myNode = fetchNode(myItemNode.ChildNodes, "dc:date");
                            if (myNode != null) myItem.pubDate = System.DateTime.Parse(myNode.InnerText);
                            break;
                        default:
                            myNode = fetchNode(myItemNode.ChildNodes, "lastBuildDate");
                            if (myNode != null) myItem.pubDate = System.DateTime.Parse(myNode.InnerText);

                            myNode = fetchNode(myItemNode.ChildNodes, "pubDate");
                            if (myNode != null) myItem.pubDate = System.DateTime.Parse(myNode.InnerText);
                            break;
                    }


                    Items.Add(myItem);
                }
        }


        /// <summary>
        ///     Carga del fichero RSS
        /// </summary>
        /// <param name="filename"></param>
        public void Load(string filename)
        {
            LoadFromFile(filename);
            PopulateMembers();
        }

        /// <summary>
        ///     Carga desde dirección de internet
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="enc"></param>
        public void LoadFromHttp(string Url, Encoding enc)
        {
            LoadFromUrl(Url, enc);
            PopulateMembers();
        }
    }
}