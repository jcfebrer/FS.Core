// // <fileheader>
// // <copyright file="SendPDF.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSPortal
// //     Solution: FSPortalNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>
using FSPortal.Funciones;


#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using iTextSharp.text;

//using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

//using iTextSharp.text.xml.simpleparser;
using iTextSharp.tool.xml;

#endregion

/// <summary>
/// Nombre de espacio con clases de utilidad comunes
/// </summary>

namespace FSPortal
{
	/// <summary>
	///     Clase para crear y enviar paginas html en formato PDF. Mediante esta clase, se pretende
	///     disponer de una alternativa de descarga de plantillas web con recursos incrustados y multiplataforma.
	/// </summary>
	public class SendPDF
	{
		#region [variables miembro]

		/// <summary>Objeto para encapsular la informacion de respuesta HTTP en la operacion ASP.NET</summary>
		private readonly HttpResponse m_response;

		/// <summary>Objeto de utilidad para codificar y decodificar direcciones URL</summary>
		private readonly HttpServerUtility m_server;

		/// <summary>Objeto de aplicacion asp.net</summary>
		private HttpApplication m_appInstance;

		/// <summary>Variable para contener la url base</summary>
		//private string m_baseUrl = "";

		/// <summary>objeto de la libreria 'iTextSharp' para manejar documentos pdf</summary>
		//private Document m_pdfDocument;

		/// <summary>Objeto para leer los valores HTTP de una solicitud</summary>
		//private HttpRequest m_request;

		#endregion [variables miembro]

        #region [constructores]

        /// <summary>
        ///     Constructor por defecto de la clase
        /// </summary>
        /// <param name="_context">Contexto de la pagina aspx (propiedad 'Context')</param>
        public SendPDF (HttpContext _context)
		{
			// obtener el contexto de la aplicacion
			m_appInstance = _context.ApplicationInstance;

			// guardar el objeto para la respuesta ASP.NET
			m_response = m_appInstance.Response;
			// guardar el objeto server
			m_server = m_appInstance.Server;
			// guardar el objeto HttpRequest
			//m_request = m_appInstance.Request;
			// obtener url
			//m_baseUrl = m_request.Url.AbsoluteUri.Substring(0, m_request.Url.AbsoluteUri.LastIndexOf('/') + 1);
		}

		#endregion [constructores]

		#region [funciones privadas]

		/// <summary>
		///     Funcion para reemplazar la URL virtual de los recursos dentro del codigo HTML
		///     en una URL absoluta dentro del servidor
		///     NOTA: actualmente solo reemplazan recursos de imagenes (tags 'img')
		/// </summary>
		/// <param name="_html">Contenido html</param>
		/// <returns>cadena con el documento HTML modificado con rutas absolutas en los recursos</returns>
		private string replaceUrlRes (string html)
		{
			//Expresion regular para obtener todas las imagenes del documento HTML con agrupacion para obtener
			// solo la ruta de la imagen para poder reemplazar la ruta virtual por la absoluta
			//<\s*img\s*.*src\s*=\s*"\s*[../]*([^""]+)
			Regex expr = new Regex (@"<\s*img\s*.*src\s*=\s*""\s*[../]*([^""""]+)");

			// obtener todas las coincidencias de la expresion regular            
			MatchCollection mc = expr.Matches (html);
			if (mc.Count > 0) {
				// recorrer todos los recursos
				foreach (Match m in mc) {
					// obtener el tag de imagen
					string tagImg = m.Result ("$0");
					// guardar hasta donde se especifica la ruta
					tagImg = tagImg.Substring (0, tagImg.IndexOf (@"src=""") + 5);
					// añadirle la ruta absoluta
					tagImg += m_server.MapPath (m.Result ("$1"));

					// reemplazar en el codigo html el tag de la imagen con la nueva ruta
					html = html.Replace (m.Result ("$0"), tagImg);
				}
			}

			// retornar el codigo HTML
			return html;
		}

		//        /// <summary>
		//        ///     Funcion para extraer los estilos del documento HTML e insertarlos en un objeto
		//        ///     para poder ser usados al crear el pdf
		//        /// </summary>
		//        /// <param name="_html">referencia a una cadena con el codigo HTML del cual se extraeran los estilos</param>
		//        /// <returns>
		//        ///     Objeto de tipo iTextSharp.text.html.simpleparser.StyleSheet con los estilos extraidos
		//        ///     para añadirlos al documento pdf
		//        /// </returns>
		//        private StyleSheet extractStyles(ref string _html)
		//        {
		//            // crear un objeto para guardar los estilos
		//            StyleSheet styles =
		//                new StyleSheet();
		//
		//            // Expresion regular para obtener todos los tags style del documento HTML
		//            // <style[^>]*>[^<]*</style>
		//            Regex expr = new Regex(@"<style[^>]*>[^<]*</style>");
		//
		//            // obtener todas las coincidencias de la expresion regular
		//            MatchCollection mc = expr.Matches(_html);
		//            if (mc.Count > 0)
		//            {
		//                // recorrer todos los recursos
		//                foreach (Match m in mc)
		//                {
		//                    // obtener el tag con los estilos
		//                    string tagStyle = m.Result("$0");
		//
		//                    // eliminarlo del codigo html (se tienen que eliminar para que no salgan en el psf)
		//                    _html = _html.Replace(tagStyle, "");
		//
		//                    //...
		//                    // TODO: aqui se parsearia el tag styles para añadirlo a 'styles'
		//                    // styles.LoadTagStyle()
		//                    //...
		//                }
		//            }
		//
		//            // retornar los estilos obtenidos
		//            return styles;
		//        }

		/// <summary>
		///     Funcion para crear un archivo pdf desde un codigo HTML
		/// </summary>
		/// <param name="_html">Array de bytes con el codigo HTML</param>
		/// <param name="_styles">Estilos a utilizar en el documento pdf</param>
		/// <param name="_size">
		///     Tamaño del documento pdf a generar. La estructura iTextSharp.text.PageSize
		///     contiene los tamaños standard ya definidos
		/// </param>
		private byte[] _createPdf (string name, string _html, Rectangle _size)
		{
			// bytes de retorno
			byte[] ret = null;

			try {
				//-----------------------------------------------------
				// Crear el documento PDF en un stream de memoria (evita crear el archivo en disco)

				//// crear el documento PDF
				//if (_size == null)
				//	m_pdfDocument = new Document (PageSize.A4, 40, 25, 15, 30);
				//else
				//	m_pdfDocument = new Document (_size);

				//// crear un stream en memoria para no crear el archivo en el servidor
				//MemoryStream streamPdf = new MemoryStream();

				// Asociar la instancia del docuemento al stream en memoria y abrir el documento
				//PdfWriter.GetInstance (m_pdfDocument, new FileStream (Funciones.Func.ServerMapPath ("~") + "/App_Data/" + name + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf", FileMode.Create));
				//m_pdfDocument.Open ();


				//-----------------------------------------------------
				// Parsear HTML y añadirlo al PDF

				// codificar el codigo HTML en un stream para parsearlo
				//_html = getImage (_html);

				//MemoryStream streamMem = new MemoryStream (_html);
				//StreamReader reader = new StreamReader (_html);
				//StringReader reader = new StringReader (_html);

//                // establecer los estilos a utilizar
//                StyleSheet styles = null;
//                if (_styles.classMap.Count > 0 || _styles.tagMap.Count > 0)
//                    styles = _styles;

//                // obtener una lista de todos los elementos del documento
//				List<IElement> htmlarraylist = HTMLWorker.ParseToList(stream, null);
//                // recorrerlos para insertarlos en el documento
//				for (int a = 0; a < htmlarraylist.Count; a++)
//                {
//					m_pdfDocument.Add((IElement)htmlarraylist[a]);
//                }

				//_html = @"<html><body><p>This <em>is </em><span class=""headline"" style=""text-decoration: underline;"">some</span> <strong>sample <em> text</em></strong><span style=""color: red;"">!!!</span></p></body></html>";

				//_html = "<html><body>" + _html + "</body></html>";

				using (MemoryStream ms = new MemoryStream (Encoding.Default.GetBytes (_html))) {
					using (TextReader tr = new StreamReader(ms)) {
						using (Document pdfDoc = new Document ()) {
							using (PdfWriter pdfWriter = PdfWriter.GetInstance (pdfDoc, ms)) {

								//document header attributes
								pdfDoc.AddAuthor("FebrerSoftware");
								pdfDoc.AddCreationDate();
								pdfDoc.AddProducer();
								pdfDoc.AddCreator("FebrerSoftware.com");
								pdfDoc.AddTitle(name);
								pdfDoc.SetPageSize(PageSize.A4);
								pdfDoc.Open ();

								pdfWriter.CloseStream = false;

								XMLWorkerHelper helper = XMLWorkerHelper.GetInstance ();
								helper.ParseXHtml (
									pdfWriter, pdfDoc, tr
								);
								ret = ms.ToArray ();

								//pdfDoc.Close();
								//ms.Position = 0;
							}
						}
					}
				}

				//XMLWorkerHelper. worker = new HTMLWorker (m_pdfDocument);

				//worker.StartDocument ();
				//worker.Parse (reader);
				//worker.EndDocument ();
				//worker.Close ();

				//m_pdfDocument.Close ();
				//ret = reader.ToString();

				// liberar recursos
				//streamMem.Dispose ();
				//reader.Dispose();
				//streamPdf.Dispose();
			} catch (Exception ex) {
				throw new FsException (ex);
			}

			// retornar el array de bytes con el documento PDF
			return ret;
		}


		private byte[] createPdf (string htmlString, Rectangle size)
		{
			byte[] ret = null;

			try
			{
				if (size == null)
					size = PageSize.A4;
				
				if (!string.IsNullOrEmpty(htmlString))
				{
					//añadimos las direcciones absolutas de las imagenes
					htmlString = getImage (htmlString);

					using (MemoryStream ms = new MemoryStream())
					{
						using (MemoryStream inputMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(htmlString)))
						{
							using (TextReader textReader = new StreamReader(inputMemoryStream))
							{
								using (Document pdfDoc = new Document(size))
								{
									using (PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, ms))
									{
										XMLWorkerHelper helper = XMLWorkerHelper.GetInstance();
										pdfDoc.Open();
										helper.ParseXHtml(pdfWriter, pdfDoc, textReader);
										pdfWriter.CloseStream = false;

										pdfDoc.Close();
									}
								}

								ret = ms.ToArray();
							}
						}
					}
				}
				else
				{
					throw new FsException("Parámetros incorrectos. Html sin datos.");
				}
			}
			catch (Exception ex)
			{
				throw new FsException (ex);
			}

			return ret;
		}


		public string getImage (string input)
		{
			if (input == null)
				return string.Empty;
			string tempInput = input;
			string pattern = @"<img(.|\n)+?>";
			string src = string.Empty;
			HttpContext context = HttpContext.Current;

			//Change the relative URL's to absolute URL's for an image, if any in the HTML code.
			foreach (Match m in Regex.Matches(input, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline |

				RegexOptions.RightToLeft)) {
				if (m.Success) {
					string tempM = m.Value;
					string pattern1 = "src=[\'|\"](.+?)[\'|\"]";
					Regex reImg = new Regex (pattern1, RegexOptions.IgnoreCase | RegexOptions.Multiline);
					Match mImg = reImg.Match (m.Value);

					if (mImg.Success) {
						src = mImg.Value.ToLower ().Replace ("src=", "").Replace ("\"", "");

						if (src.ToLower ().Contains ("http://") == false) {
							//Insert new URL in img tag
							src = Func.HtmlDecode(src);
							try {
								tempM = tempM.Remove (mImg.Index, mImg.Length);
								tempM = tempM.Insert (mImg.Index, src);

								//insert new url img tag in whole html code
								tempInput = tempInput.Remove (m.Index, m.Length);
								tempInput = tempInput.Insert (m.Index, tempM);
							} catch (Exception e) {
								throw new FsException (e);
							}
						}
					}
				}
			}
			return tempInput;
		}

		string getSrc (string input)
		{
			string pattern = "src=[\'|\"](.+?)[\'|\"]";
			System.Text.RegularExpressions.Regex reImg = new System.Text.RegularExpressions.Regex (pattern,
				                                             System.Text.RegularExpressions.RegexOptions.IgnoreCase |

				                                             System.Text.RegularExpressions.RegexOptions.Multiline);
			System.Text.RegularExpressions.Match mImg = reImg.Match (input);
			if (mImg.Success) {
				return mImg.Value.Replace ("src=", "").Replace ("\"", "");
				;
			}

			return string.Empty;
		}

		#endregion [funciones privadas]

		#region [funciones publicas]

		/// <summary>
		///     Funcion para crear un documento pdf mediante un archivo Html/aspx y enviarlo al cliente por HTTP
		/// </summary>
		/// <param name="_url">ruta virtual del archivo html/aspx</param>
		public void SendFilePdf (string url)
		{
			SendFilePdf (url, null);
		}

		/// <summary>
		///     Funcion para crear un documento pdf mediante un archivo Html/aspx y enviarlo al cliente por HTTP
		/// </summary>
		/// <param name="_url">ruta virtual del archivo html/aspx</param>
		/// <param name="_size">
		///     Tamaño del documento pdf a generar. La estructura iTextSharp.text.PageSize
		///     contiene los tamaños standard ya definidos
		/// </param>
		public void SendFilePdf (string url, Rectangle size)
		{
			try {
				//------------------------------------------------------
				// peticion web al recurso con el codigo HTML/ASPX
				// Crear una peticion a una URL            
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create (url);
				// obtener la respuesta del recurso solicitado
				HttpWebResponse response = (HttpWebResponse)request.GetResponse ();

				// obtener el stream del recurso
				Stream dataStream = response.GetResponseStream ();
				// Crear un reader para leer del stream
				StreamReader reader = new StreamReader (dataStream, Encoding.Default);
				// Leer el contenido
				string responseFromServer = reader.ReadToEnd ();

				// Limpiar recursos utilizados
				reader.Close ();
				dataStream.Close ();
				response.Close ();

				//------------------------------------------------------
				// Generar el archivo PDF

				//// extraer del codigo HTML los posibles estilos de la pagina
				//StyleSheet styles = extractStyles(ref responseFromServer);

				// crear el archivo pdf con el html y estilos generados
				byte[] pdf = createPdf (responseFromServer, size);

				// escribir la respuesta HTTP con el envio del documento pdf   
				m_response.Clear ();
				m_response.Buffer = true;
				m_response.AddHeader ("Content-Disposition", "attachment; filename=");
				m_response.ContentType = "application/pdf";
				m_response.BinaryWrite (pdf);
				m_response.End ();
			} catch (Exception ex) {
				throw new FsException (ex);
			}
		}

		/// <summary>
		///     Funcion para crear un documento pdf mediante un archivo Html/aspx y enviarlo al cliente por HTTP
		/// </summary>
		/// <param name="_html">ruta virtual del archivo html/aspx</param>
		public void SendHtmlPdf (string html)
		{
			SendHtmlPdf (html, null);
		}

		/// <summary>
		///     Funcion para crear un documento pdf mediante un archivo Html/aspx y enviarlo al cliente por HTTP
		/// </summary>
		/// <param name="_html">ruta virtual del archivo html/aspx</param>
		/// <param name="_size">
		///     Tamaño del documento pdf a generar. La estructura iTextSharp.text.PageSize
		///     contiene los tamaños standard ya definidos
		/// </param>
		public void SendHtmlPdf (string html, Rectangle size)
		{
			try {
				//------------------------------------------------------
				// Generar el archivo PDF

				//// extraer del codigo HTML los posibles estilos de la pagina
				//StyleSheet styles = extractStyles(ref _html);

				// crear el archivo pdf con el html y estilos generados
				byte[] pdf = createPdf (html, size);

				// escribir la respuesta HTTP con el envio del documento pdf   
				m_response.Clear ();
				m_response.Buffer = true;
				m_response.AddHeader ("Content-Disposition", "attachment; filename=");
				m_response.ContentType = "application/pdf";
				m_response.BinaryWrite (pdf);
				m_response.End ();
			} catch (Exception ex) {
				throw new FsException (ex);
			}
		}

		#endregion [funciones publicas]
	}
}