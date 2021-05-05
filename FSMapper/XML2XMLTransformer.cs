using System;
using System.IO;
using System.Xml;


/// -----------------------------------------------------------------------------
/// Project	 :
/// Class	 :
///
/// -----------------------------------------------------------------------------
/// <summary>
/// Transforms XML Document to XML Document with XSL transformation.
/// </summary>
/// <remarks>
/// </remarks>
/// <history>
/// 	[smiriya]	8/8/2006	Created
/// </history>
/// -----------------------------------------------------------------------------
namespace XML2XML
{
	public class XML2XMLTransformer : ITransform
	{
		System.Xml.XmlDocument m_InputXmlDocument = null;
		System.String m_Output;
		string m_XSLFilePath;
		XmlDocument m_XSLDoc;
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Create a instance of this class.
		/// </summary>
		/// <param name="xslFilePath">XSL file path.</param>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[smiriya]	8/8/2006	Created
		/// </history>
		/// -----------------------------------------------------------------------------
		public XML2XMLTransformer(string xslFilePath)
		{
			m_XSLFilePath = xslFilePath;
			LoadXSLURL(m_XSLFilePath);
		}
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Create a instance of this class.
		/// </summary>
		/// <param name="xslDoc">XMLDocument object with xslt data.</param>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[smiriya]	8/8/2006	Created
		/// </history>
		/// -----------------------------------------------------------------------------
		public XML2XMLTransformer(XmlDocument xslDoc)
		{
			m_XSLDoc = xslDoc;
		}
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets Source Xml document.
		/// </summary>
		/// <value></value>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[smiriya]	8/8/2006	Created
		/// </history>
		/// -----------------------------------------------------------------------------
		public System.Xml.XmlDocument InputXmlDocument {
			get { return m_InputXmlDocument; }
		}
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Get's Output XML after transformation.
		/// </summary>
		/// <value></value>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[smiriya]	8/8/2006	Created
		/// </history>
		/// -----------------------------------------------------------------------------
		public System.String Output {
			get { return m_Output; }
		}
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Get's XSL File Path.
		/// </summary>
		/// <value></value>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[smiriya]	8/8/2006	Created
		/// </history>
		/// -----------------------------------------------------------------------------
		public object XSLFilePath {
			get { return m_XSLFilePath; }
		}
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Applies XSL Transformations on the source XML document and returns the resulted XML Document.
		/// </summary>
		/// <param name="sourceXmlDoc">Source XML Document</param>
		/// <returns>Transformed XML Document.</returns>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[smiriya]	8/8/2006	Created
		/// </history>
		/// -----------------------------------------------------------------------------
		public System.String Transform(System.Xml.XmlDocument sourceXmlDoc)
		{
			m_Output = Transform(sourceXmlDoc, m_XSLDoc, null, null);
			return m_Output;
		}
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Applies XSL Transformations on the source XML document and returns the resulted XML Document.
		/// </summary>
		/// <param name="xmlUrl">XML file path.</param>
		/// <param name="xslUrl">XSL file path.</param>
		/// <returns>Transformed XML Document.</returns>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[smiriya]	8/8/2006	Created
		/// </history>
		/// -----------------------------------------------------------------------------
		public string Transform(string xmlUrl, string xslUrl)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xmlUrl);
			LoadXSLURL(xslUrl);
			return Transform(xmlDoc, m_XSLDoc, null, null);
		}
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Applies XSL Transformations on the source XML document and returns the resulted XML Document.
		/// </summary>
		/// <param name="docXml">Source XML Document</param>
		/// <param name="xslDoc">XSLT Document</param>
		/// <param name="xslParamNames">XSL Parameter Names</param>
		/// <param name="xslParamValues">XSL Parameter Values</param>
		/// <returns>Transformed XML Document.</returns>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[smiriya]	8/8/2006	Created
		/// </history>
		/// -----------------------------------------------------------------------------
		public string Transform(System.Xml.XmlDocument docXml, XmlDocument xslDoc, string[] xslParamNames, string[] xslParamValues)
		{
			StringWriter sResult;
			System.Xml.Xsl.XslCompiledTransform xslTransform = new System.Xml.Xsl.XslCompiledTransform();
			//load xsl to a xsltransform object
			try {
			
				using (StringReader sr = new StringReader(xslDoc.InnerXml)) {
					using (XmlReader xr = XmlReader.Create(sr)) {
						xslTransform.Load(xr);
					}
				}
			
				//load up the xsl parameters, if any
				System.Xml.Xsl.XsltArgumentList xslArgs = new System.Xml.Xsl.XsltArgumentList();
				if ((xslParamNames != null)) {
					int counter = 0;
					string paramname = null;
					foreach (string paramname_loopVariable in xslParamNames) {
						paramname = paramname_loopVariable;
						xslArgs.AddParam(paramname, null, xslParamValues[counter]);
					}
				}
			

				using (StringReader sr = new StringReader(docXml.InnerXml)) {
					using (XmlReader xr = XmlReader.Create(sr)) {
						using (sResult = new StringWriter()) {
						
							// call transform
							if ((xslParamNames != null)) {
								xslTransform.Transform(xr, xslArgs, sResult);
							} else {
								xslTransform.Transform(xr, null, sResult);
							}
						
						}
					}
				}
			
			} catch (Exception ex) {
				throw new ApplicationException("Fallo al transformar XML mediante XLS.", ex);
			}
			return sResult.ToString();
		}
		//Transform
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Initializes the XSL Document.
		/// </summary>
		/// <param name="xslUrl">XSl File Location.</param>
		/// <returns>XSL Document</returns>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[smiriya]	8/8/2006	Created
		/// </history>
		/// -----------------------------------------------------------------------------
		public XmlDocument LoadXSLURL(string xslUrl)
		{
			try {
				if (m_XSLDoc == null) {
					m_XSLDoc = new XmlDocument();
				}
				m_XSLDoc.Load(xslUrl);
				//Setting up NSManager
				XmlNamespaceManager nmManger = new XmlNamespaceManager(m_XSLDoc.NameTable);
				nmManger.AddNamespace("xsl", "http://www.w3.org/1999/XSL/Transform");
				return m_XSLDoc;
			} catch (Exception ex) {
				throw new ApplicationException("Imposible cargar documento/script XSL, comprueba el Script XSLT.", ex);
			}
		}
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Initializes the XSL Document.
		/// </summary>
		/// <param name="xslText">XSLT Text</param>
		/// <returns>XSLT Document</returns>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[smiriya]	8/8/2006	Created
		/// </history>
		/// -----------------------------------------------------------------------------
		public XmlDocument LoadXSLText(string xslText)
		{
			try {
				if (m_XSLDoc == null) {
					m_XSLDoc = new XmlDocument();
				}
				m_XSLDoc.LoadXml(xslText);
				return m_XSLDoc;
			} catch (Exception ex) {
				throw new ApplicationException("Imposible cargar documento/script XSL, comprueba el Script XSLT.", ex);
			}
		}
	}
}