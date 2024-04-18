
using FSCryptoCore;
using FSLibraryCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Web;

namespace FSNetworkCore
{
	/// <summary>
	/// Description of Web.
	/// </summary>
	public static class Web
	{
		
		private static string _virtualPath;

		public static string VirtualPath
		{
			set { _virtualPath = value; }
			get
			{
				if (_virtualPath == null)
				{
					string url = HttpContext.Current.Request.PathBase;
					if (url != "/") url += "/";
					_virtualPath = url;
				}
				return _virtualPath;
			}
		}
		
		
		public static string ConvertToTwitter(string msg)
		{
			return ConvertListToLinks(ConvertUserToLinks(ConvertUrlsToLinks(msg)));
		}

		public static string ConvertUrlsToLinks(string msg)
		{
			string regex =
				@"((www\.|(http|https|ftp|news|file)+\:\/\/)[&#95;.a-z0-9-]+\.[a-z0-9\/&#95;:@=.+?,##%&~-]*[^.|\'|\# |!|\(|?|,| |>|<|;|\)])";
			Regex r = new Regex(regex, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			return
				r.Replace(msg,
					"<a href=\"$1\" title=\"Pulsa para abrir en una nueva ventana o pestaña\" target=\"_blank\">$1</a>")
					.Replace("href=\"www", "href=\"http://www");
		}

		public static string ConvertUserToLinks(string msg)
		{
			string regex = @"(\@[a-zA-Z0-9_]+)";
			Regex r = new Regex(regex, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			return
				r.Replace(msg,
					"<a href=\"http://twitter.com/$1\" title=\"Pulsa para acceder al usuario\" target=\"_blank\">$1</a>")
					.Replace("twitter.com/@", "twitter.com/");
		}

		public static string ConvertListToLinks(string msg)
		{
			string regex = @"(\#[a-zA-Z0-9]+)";
			Regex r = new Regex(regex, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			return
				r.Replace(msg,
					"<a href=\"http://twitter.com/#!/search?q=$1\" title=\"Pulsa para acceder al searchtag\" target=\"_blank\">$1</a>")
					.Replace("search?q=#", "search?q=%23");
		}

		public static string FormatHTML(string data)
		{
			data = data.Replace("\n", "<br />");
			data = data.Replace("\n\r", "<br />");

			return data;
		}
		
		public static string RemoveHTML(string html)
		{
			Regex regex = new Regex("</?(.*)>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
			return regex.Replace(html, string.Empty);
		}

		public static string CurrentUrl()
		{
			return @"http://" + HttpContext.Current.GetServerVariable("SERVER_NAME") +
				HttpContext.Current.GetServerVariable("URL");
		}


		public static void SetSessionValue(string name, string value)
		{
			HttpContext.Current.Session.SetString(name, value);
		}

		public static string formatJSON(string data)
		{
			data = HttpUtility.HtmlEncode(data);
			data = data.Replace("\r\n", "<br />");
			data = data.Replace("\r", "<br />");
			data = data.Replace("\n", "<br />");
			data = data.Replace("\t", "");
			data = data.Replace (":", "-");
			data = data.Replace (",", ";");
			data = data.Replace ("\\", "/");

			data = RemoveHTML(data);

			return data;
		}

		public static object GetSessionValue(string name)
		{
			return HttpContext.Current.Session.GetString(name);
		}

		public static object GetCacheValue(string name)
		{
			return HttpContext.Current.Session.GetString(name);
		}

		public static void SetCacheValue(string name, string value)
		{
			if (value == null)
				HttpContext.Current.Session.Remove(name);
			else
				HttpContext.Current.Session.SetString(name, value);
		}

        public static void SetCacheValue(string name, object value)
        {
            if (value == null)
                HttpContext.Current.Session.Remove(name);
            else
                HttpContext.Current.Session.Set(name, NumberUtils.ObjectToByteArray(value));
        }

        public static void ClearSessionVariables()
        {
            foreach (string entry in HttpContext.Current.Session.Keys)
            {
				HttpContext.Current.Session.Remove(entry);
			}
		}

        public static string ServerMapPath(string path)
        {
            string homePath = (string)AppDomain.CurrentDomain.GetData("WebRootPath");
            return Path.Combine(
                homePath,
                path);
        }


        public static string RequestQueryForm()
		{
			return RequestQueryForm("");
		}

		/// <summary>
		/// Recupera los campos del FORM y QUERYSTRING sin repetidos, menos los campos 'keysToDelete' (util para ordenar por columna)
		/// </summary>
		/// <param name="keysToDelete">Campo a eliminar</param>
		/// <returns></returns>
		public static string RequestQueryForm(string keysToDelete)
		{
			string[] arrKeysToDelete = keysToDelete.Split(',');

			//cojemos las colecciones de FORM y QUERYSTRING, y las unificamos sin repetidos
			//prevaleciendo las del FORM
			NameValueCollection nvc = new NameValueCollection();
			foreach (string name in HttpContext.Current.Request.Query.Keys)
			{
				nvc.Add(name, HttpContext.Current.Request.Query[name]);
			}

			foreach (string keyForm in HttpContext.Current.Request.Form.Keys)
			{
				foreach (string value in HttpContext.Current.Request.Form[keyForm])
				{
					//si exite un parametro en la colleccion nvc de querystring, lo borramos, y añadimos el del form.
					if (nvc.Get(keyForm) != null)
						nvc.Remove(keyForm);

					nvc.Add(keyForm, value);
				}
			}


			List<string> urlValues = new List<string>();
			foreach (string key in nvc.AllKeys)
			{
				if (key != null)
				{
					if (!Functions.Existe(arrKeysToDelete, key))
					{
						foreach (string value in nvc.GetValues(key))
						{
							string item = string.Format("{0}={1}", key, value);
							urlValues.Remove(item); //si ya existe el parámetro, lo borramos
							urlValues.Add(item);
						}
					}
				}
			}

			return string.Join("&", urlValues.ToArray());
		}


		public static string GeneraHiddenFields(HttpRequest frm)
		{
			string campos = null;

			campos = "";
			foreach (string name in frm.Form.Keys)
			{
				campos = campos + @"<input type=""hidden"" value=""" + Functions.Valor(frm.Form[name]) + @""" name=""" +
					name + @"""/>" + "\r\n";
			}
			return campos;
		}
		
		
		public static string Request(string name)
		{
			if (HttpContext.Current == null)
                return "";
                
            object dato = null;
            if (HttpContext.Current.Request.HasFormContentType)
                dato = HttpContext.Current.Request.Form[name];
            if (dato == null)
                dato = HttpContext.Current.Request.Query[name];
            if (dato == null) return "";

			dato = TextUtil.OnlyAlfaNumeric(Functions.Valor(dato));

            //seleccionamos el último valor
            string[] datos = dato.ToString().Split(',');
            dato = datos[datos.Length - 1];
			
			return dato.ToString();
		}


		public static int RequestInt(string name)
		{
			if (HttpContext.Current == null)
                return 0;
                
			object dato = null;
            if (HttpContext.Current.Request.HasFormContentType)
                dato = HttpContext.Current.Request.Form[name];
			if (dato == null)
				dato = HttpContext.Current.Request.Query[name];
			if (dato == null) 
				return 0;
			if (dato + "" == "") 
				return 0;

            //seleccionamos el último valor
            string[] datos = dato.ToString().Split(',');
            dato = datos[datos.Length - 1];

            return NumberUtils.NumberInt(dato);
		}

        public static bool RequestBool(string name)
		{
			if (HttpContext.Current == null)
                return false;
                
            object dato = null;
            if (HttpContext.Current.Request.HasFormContentType)
                dato = HttpContext.Current.Request.Form[name];
            if (dato == null)
                dato = HttpContext.Current.Request.Query[name];
            if (dato == null)
			{
				return false;
			}

			//seleccionamos el último valor
			string[] datos = dato.ToString().Split(',');
			dato = datos[datos.Length - 1];

			return Functions.ValorBool(dato);
		}
		
		
		public static string RequestDate(string date)
		{
			if (HttpContext.Current == null)
                return "";
                
            object dato = null;
            if (HttpContext.Current.Request.HasFormContentType)
                dato = HttpContext.Current.Request.Form[date];
            if (dato == null)
                dato = HttpContext.Current.Request.Query[date];
            if (dato == null)
			{
				return "";
			}

            //seleccionamos el último valor
            string[] datos = dato.ToString().Split(',');
            dato = datos[datos.Length - 1];

            if (!FSLibraryCore.DateTimeUtil.IsDate(dato.ToString()))
				return "";

			return FSLibraryCore.DateTimeUtil.ShortDate(Convert.ToDateTime(dato));
		}


		public static string Cookie(string name)
		{
			return HttpContext.Current.Request.Cookies[name];
		}


		public static void SetCookie(string name, string value)
		{
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddMilliseconds(10);
            HttpContext.Current.Response.Cookies.Append(name, value, option);
        }

		public static void WriteCookiesToFile(string file, CookieContainer cookieContainer)
		{
			using (Stream stream = File.Create(file))
			{
				byte[] result = NumberUtils.ObjectToByteArray(cookieContainer);
                stream.Write(result);
			}
		}

		public static CookieContainer ReadCookiesFromFile(string file)
		{
            byte[] buffer = new byte[16 * 1024];

            using (Stream stream = File.Open(file, FileMode.Open))
			{
				using (MemoryStream ms = new MemoryStream())
				{
					int read;
					while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
					{
						ms.Write(buffer, 0, read);
					}

                    byte[] data = ms.ToArray();
                    CookieContainer result = NumberUtils.ByteArrayToObject<CookieContainer>(data);
                    return result;
                }
                
			}
		}


        /// <summary>
		///     Funcion para reemplazar la URL virtual de los recursos dentro del codigo HTML
		///     en una URL absoluta dentro del servidor
		///     NOTA: actualmente solo reemplazan recursos de imagenes (tags 'img')
		/// </summary>
		/// <param name="html">Contenido html</param>
		/// <returns>cadena con el documento HTML modificado con rutas absolutas en los recursos</returns>
		public static string ReplaceUrlRes(string html)
        {
            //Expresion regular para obtener todas las imagenes del documento HTML con agrupacion para obtener
            // solo la ruta de la imagen para poder reemplazar la ruta virtual por la absoluta
            //<\s*img\s*.*src\s*=\s*"\s*[../]*([^""]+)
            Regex expr = new Regex(@"<\s*img\s*.*src\s*=\s*[""]*\s*[../]*([^""""]+)");

            // obtener todas las coincidencias de la expresion regular            
            MatchCollection mc = expr.Matches(html);
            if (mc.Count > 0)
            {
                // recorrer todos los recursos
                foreach (Match m in mc)
                {
                    // obtener el tag de imagen
                    string tagImg = m.Result("$0");
                    // guardar hasta donde se especifica la ruta
                    tagImg = tagImg.Substring(0, tagImg.IndexOf(@"src=""") + 5);
                    // añadirle la ruta absoluta
                    tagImg += Web.ServerMapPath(m.Result("$1"));

                    // reemplazar en el codigo html el tag de la imagen con la nueva ruta
                    html = html.Replace(m.Result("$0"), tagImg);
                }
            }

            // retornar el codigo HTML
            return html;
        }

        /// <summary>
        /// Reemplazamos la dirección (src) de las imagenes por la real
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string ReplaceImg(string html, string seek, string replace)
        {
            //Expresion regular para obtener todas las imagenes del documento HTML con agrupacion para obtener
            // solo la ruta de la imagen para poder reemplazar la ruta virtual por la absoluta
            //<\s*img\s*.*src\s*=\s*"\s*[../]*([^""]+)
            const string pattern = @"(<\s*img\s*src\s*=\s*)([\'|\""](.+?)[\'|\""])";
            Regex expr = new Regex(pattern);

            // obtener todas las coincidencias de la expresion regular            
            MatchCollection mc = expr.Matches(html);
            if (mc.Count > 0)
            {
                // recorrer todos los recursos
                foreach (Match m in mc)
                {

                    string cont = m.Groups[2].Value;
                    cont = TextUtil.Replace(cont, seek, replace);

                    html = html.Replace(m.Groups[2].Value, cont);

                    //html = Regex.Replace(html, pattern, m.Groups[1].Value + cont);
                }
            }

            // retornar el codigo HTML
            return html;
        }
    }
}
