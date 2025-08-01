﻿using FSException;
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

#if NET35_OR_GREATER
    using System.Xml.Linq;
#endif

namespace FSLibrary
{
    /// <summary>
    ///     Utilidades para trabajar con XML.
    /// </summary>
    public class XmlUtil
    {
        #region "PrettyXml"

#if NET35_OR_GREATER
        /// <summary>
        /// Formatea un fichero xml en formato texto a xml con intentación y saltos de línea
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string PrettyXml(string xml)
        {
            var stringBuilder = new StringBuilder();

            var element = XElement.Parse(xml);

            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            settings.NewLineOnAttributes = true;

            using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
            {
                element.Save(xmlWriter);
            }

            return stringBuilder.ToString();
        }
#endif

        #endregion

        #region XML Serialization

        /// <summary>
        /// Clase StringWrite con codificación UTF8
        /// </summary>
        public class Utf8StringWriter : StringWriter
        {
            /// <summary>
            /// Codificación en UTF8
            /// </summary>
            public override Encoding Encoding { get { return Encoding.UTF8; } }
        }

        /// <summary>
        /// Convierte en texto el objeto XML model.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string FormatXMLToString(Object model)
        {
            var xml = "";

            //Create our own namespaces for the output
            var xmlNS = new XmlSerializerNamespaces();
            xmlNS.Add("", "");

            var xmlSettings = new XmlWriterSettings();
            xmlSettings.IndentChars = "\t";
            xmlSettings.Indent = true;

            using (var sw = new Utf8StringWriter())
            {
                using (XmlWriter xw = XmlWriter.Create(sw, xmlSettings))
                {
                    var serializer = new XmlSerializer(model.GetType());
                    serializer.Serialize(xw, model, xmlNS);
                    xml = sw.ToString();
                }
            }

            return xml;
        }

        /// <summary>
        ///     Converts an object to an xml string.
        /// </summary>
        /// <param name="objToXml">Object wanted to be converted to xml string</param>
        /// <param name="includeNameSpace">Include namespace</param>
        /// <returns>Xml string of the object</returns>
        public static string ToXml(object objToXml, bool includeNameSpace)
        {
            string buffer;

            using (var memStream = new MemoryStream())
            {
                using (var stWriter = new StreamWriter(memStream))
                {
                    var xmlSerializer = new XmlSerializer(objToXml.GetType());

                    if (!includeNameSpace)
                    {
                        var xs = new XmlSerializerNamespaces();
                        xs.Add("", ""); //To remove namespace and any other inline information tag
                        xmlSerializer.Serialize(stWriter, objToXml, xs);
                    }
                    else
                    {
                        xmlSerializer.Serialize(stWriter, objToXml);
                    }

                    buffer = Encoding.UTF8.GetString(memStream.GetBuffer());
                }
            }

            return buffer;
        }

        /// <summary>
        ///     Serializes a given object to the file given with the path
        /// </summary>
        /// <param name="objToXml">Object wanted to be serialized to the file</param>
        /// <param name="filePath">Path of the file</param>
        /// <param name="includeNameSpace">Include namespace</param>
        public static void ToXml(object objToXml, string filePath, bool includeNameSpace)
        {
            XmlSerializer xmlSerializer;
            try
            {
                using (var stWriter = new StreamWriter(filePath))
                {
                    xmlSerializer = new XmlSerializer(objToXml.GetType());

                    if (!includeNameSpace)
                    {
                        var xs = new XmlSerializerNamespaces();
                        xs.Add("", ""); //To remove namespace and any other inline information tag
                        xmlSerializer.Serialize(stWriter, objToXml, xs);
                    }
                    else
                    {
                        xmlSerializer.Serialize(stWriter, objToXml);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Serializes a given object to the file given with the path
        /// </summary>
        /// <param name="objToXml">Object wanted to be serialized to the file</param>
        /// <param name="includeStartDocument">
        ///     Includes the line if it is true, otherwise start document line is excluded
        ///     from the exported xml.
        /// </param>
        /// <param name="filePath">Path of the file</param>
        /// <param name="includeNameSpace">Include namespace</param>
        public static void ToXml(object objToXml, string filePath, bool includeNameSpace, bool includeStartDocument)
        {
            XmlSerializer xmlSerializer;
            try
            {
                using (var stWriter = new SpecialXmlWriter(filePath, null, includeStartDocument))
                {
                    xmlSerializer = new XmlSerializer(objToXml.GetType());

                    if (!includeNameSpace)
                    {
                        var xs = new XmlSerializerNamespaces();
                        xs.Add("", ""); //To remove namespace and any other inline information tag
                        xmlSerializer.Serialize(stWriter, objToXml, xs);
                    }
                    else
                    {
                        xmlSerializer.Serialize(stWriter, objToXml);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Deserializes the object given with the type from the given string
        /// </summary>
        /// <param name="xmlString">String containing the serialized xml form of the object</param>
        /// <param name="type">Type of the object to be deserialized</param>
        /// <returns>Deserialized object</returns>
        public static object XmlTo(string xmlString, Type type)
        {
            XmlSerializer xmlSerializer;
            try
            {
                using (var memStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
                {
                    xmlSerializer = new XmlSerializer(type);
                    var objectFromXml = xmlSerializer.Deserialize(memStream);
                    return objectFromXml;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Deserializes the object given with the type from the given string
        /// </summary>
        /// <param name="filePath">String containing the serialized xml form of the object</param>
        /// <param name="type">Type of the object to be deserialized</param>
        /// <returns>Deserialized object</returns>
        public static object XmlToFromFile(string filePath, Type type)
        {
            XmlSerializer xmlSerializer;
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    xmlSerializer = new XmlSerializer(type);
                    var objectFromXml = xmlSerializer.Deserialize(fileStream);
                    return objectFromXml;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Serializes the object XML.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="typeObj">The type object.</param>
        /// <returns></returns>
        public static string SerializeObjectXML(object o, Type typeObj)
        {
            return SerializeObjectXML(o, typeObj, false, "");
        }

        /// <summary>
        /// Serializes the object XML.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="typeObj">The type object.</param>
        /// <param name="soap">if set to <c>true</c> [SOAP].</param>
        /// <returns></returns>
        public static string SerializeObjectXML(object o, Type typeObj, bool soap)
        {
            return SerializeObjectXML(o, typeObj, soap, "");
        }

        /// <summary>
        /// Serializes the object XML.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="typeObj">The type object.</param>
        /// <param name="soap">if set to <c>true</c> [SOAP].</param>
        /// <param name="nameSpace">The name space.</param>
        /// <returns></returns>
        public static string SerializeObjectXML(object o, Type typeObj, bool soap, string nameSpace)
        {
            XmlSerializer serializer;

            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;
            settings.ConformanceLevel = ConformanceLevel.Auto;

            if (soap)
            {
                var importer = new SoapReflectionImporter();
                XmlTypeMapping typeMapping;
                if (nameSpace != "")
                    typeMapping = importer.ImportTypeMapping(typeObj, nameSpace);
                else
                    typeMapping = importer.ImportTypeMapping(typeObj);
                serializer = new XmlSerializer(typeMapping);
            }
            else
            {
                if (nameSpace != "")
                    serializer = new XmlSerializer(typeObj, nameSpace);
                else
                    serializer = new XmlSerializer(typeObj);
            }

            using (var sww = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sww, settings))
                {
                    writer.WriteStartElement("root");
                    serializer.Serialize(writer, o);
                    writer.WriteEndElement();

                    return sww.ToString();
                }
            }
        }

        #endregion

        /// <summary>
        ///     This class can be used to implement special affects while producing xml documents.
        ///     At the moment it is only used for excluding the xml start line.
        /// </summary>
        public class SpecialXmlWriter : XmlTextWriter
        {
            private readonly bool m_includeStartDocument = true;

            /// <summary>
            /// Initializes a new instance of the <see cref="SpecialXmlWriter"/> class.
            /// </summary>
            /// <param name="tw">The tw.</param>
            /// <param name="includeStartDocument">if set to <c>true</c> [include start document].</param>
            public SpecialXmlWriter(TextWriter tw, bool includeStartDocument) : base(tw)
            {
                m_includeStartDocument = includeStartDocument;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="SpecialXmlWriter"/> class.
            /// </summary>
            /// <param name="sw">The sw.</param>
            /// <param name="encoding">The encoding.</param>
            /// <param name="includeStartDocument">if set to <c>true</c> [include start document].</param>
            public SpecialXmlWriter(Stream sw, Encoding encoding, bool includeStartDocument) : base(sw, null)
            {
                m_includeStartDocument = includeStartDocument;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="SpecialXmlWriter"/> class.
            /// </summary>
            /// <param name="filePath">The file path.</param>
            /// <param name="encoding">The encoding.</param>
            /// <param name="includeStartDocument">if set to <c>true</c> [include start document].</param>
            public SpecialXmlWriter(string filePath, Encoding encoding, bool includeStartDocument) : base(filePath, null)
            {
                m_includeStartDocument = includeStartDocument;
            }

            /// <summary>
            /// Writes the XML declaration with the version "1.0".
            /// </summary>
            public override void WriteStartDocument()
            {
                if (m_includeStartDocument) base.WriteStartDocument();
            }
        }
    }
}