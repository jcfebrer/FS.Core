/*
 * Created by SharpDevelop.
 * User: febrer
 * Date: 05/06/2017
 * Time: 10:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;

namespace XML2XML
{
	/// <summary>
	/// Description of XML2XMLHelper.
	/// </summary>
	public static class XML2XMLHelper
	{
		public static XmlDocument SourceXML = new XmlDocument();
		public static XmlDocument TargetXML = new XmlDocument();
		
		public static string SourceXMLFilePath;
		public static string TargetXMLFilePath;
		
		public static string SourceXMLPrefix;
		public static string SourceXMLNameSpaceURI;
		public static string TargetXMLPrefix;
		public static string TargetXMLNameSpaceURI;
		
		public static string xslPrefix = "xsl";
		//public static string nsPrefix = "xmlns";

		public static string xslNameSpaceURI = "http://www.w3.org/1999/XSL/Transform";

		public static Collection<NodeMapping> nodeMappings = new Collection<NodeMapping>();
		//public static bool MappingChanged;

		//public static bool IsDropped;

		//public static bool IsNodeClicked;
		
		public static NodeMapping FindKeyNodeMapping(Collection<NodeMapping> nodeMapping, string findValue)
		{
			foreach (NodeMapping nm in nodeMapping) {
				if (nm.Key == findValue)
					return nm;
			}
			
			return null;
		}
		public static NodeMapping FindValueNodeMapping(Collection<NodeMapping> nodeMapping, string findValue)
		{
			foreach (NodeMapping nm in nodeMapping) {
				if (nm.Value == findValue)
					return nm;
			}
			
			return null;
		}
		
		public static void BlankNodes(XmlNode node)
		{
			if (node.NodeType == XmlNodeType.Text) {
				node.Value = "";
			}
 
			XmlNodeList children = node.ChildNodes;
			foreach (XmlNode child in children) {
				BlankNodes(child);
			}
		}
		
		
		public static string Beautify(XmlDocument doc)
		{
			StringBuilder sb = new StringBuilder();
			XmlWriterSettings settings = new XmlWriterSettings();
		    
			settings.Indent = true;
			settings.IndentChars = "  ";
			settings.NewLineChars = "\r\n";
			settings.NewLineHandling = NewLineHandling.Replace;

			using (XmlWriter writer = XmlWriter.Create(sb, settings)) {
				doc.Save(writer);
			}
			return sb.ToString();
		}
		
		public static string FindXPath(XmlNode node)
		{
			if (node.NodeType == XmlNodeType.Text)
				return "";
			
			StringBuilder builder = new StringBuilder();
			while (node != null) {
				switch (node.NodeType) {
					case XmlNodeType.Attribute:
						builder.Insert(0, "/@" + node.Name);
						node = ((XmlAttribute)node).OwnerElement;
						break;
					case XmlNodeType.Element:
						int index = FindElementIndex((XmlElement)node);
						builder.Insert(0, "/" + node.Name + "[" + index + "]");
						node = node.ParentNode;
						break;
					case XmlNodeType.Document:
						return builder.ToString();
					default:
						throw new ArgumentException("Sólo estan soportados elementos y atributos.");
				}
			}
			return "Nodo no encontrado.";
		}

		private static int FindElementIndex(XmlElement element)
		{
			XmlNode parentNode = element.ParentNode;
			if (parentNode is XmlDocument) {
				return 1;
			}
			XmlElement parent = (XmlElement)parentNode;
			int index = 1;
			foreach (XmlNode candidate in parent.ChildNodes) {
				if (candidate is XmlElement && candidate.Name == element.Name) {
					if (candidate == element) {
						return index;
					}
					index++;
				}
			}
			throw new ArgumentException("No se pudo encontrar elemento dentro de parent");
		}
		
		
		public static XmlNode FindXMLNode(XmlNode node, string value)
		{
			if (FindXPath(node) == value)
				return node;
			
			XmlNodeList children = node.ChildNodes;
			foreach (XmlNode child in children) {
				XmlNode findNode = FindXMLNode(child, value);
				if (findNode != null)
					return findNode;
			}
			
			return null;
		}

	}
}
