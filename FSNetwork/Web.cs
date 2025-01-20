
using FSCrypto;
using FSLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace FSNetwork
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
					string url;
					if(HttpContext.Current == null)
						url = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
					else
						url = HttpContext.Current.Request.ApplicationPath;

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
			return @"http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] +
				HttpContext.Current.Request.ServerVariables["URL"];
		}
		
		
				public static void SetSessionValue(string name, object value)
		{
			HttpContext.Current.Session[name] = value;
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
			return HttpContext.Current.Session[name];
		}

		public static object GetCacheValue(string name)
		{
			return (HttpContext.Current == null ? HttpRuntime.Cache.Get(name) : HttpContext.Current.Cache.Get(name));
		}

		public static void SetCacheValue(string name, object value)
		{
			if (HttpContext.Current == null)
			{
				//HttpRuntime se utiliza en windows forms
				if (value == null)
					HttpRuntime.Cache.Remove(name);
				else
					HttpRuntime.Cache.Insert(name, value);
			}
			else
			{
				if (value == null)
					HttpContext.Current.Cache.Remove(name);
				else
					HttpContext.Current.Cache.Insert(name, value);
			}
		}

        public static void ClearCacheVariables()
        {
            foreach (System.Collections.DictionaryEntry entry in HttpContext.Current.Cache)
            {
				HttpContext.Current.Cache.Remove(entry.Key.ToString());
			}

			//HttpRuntime se utiliza en windows forms
			foreach (System.Collections.DictionaryEntry entry in HttpRuntime.Cache)
			{
				HttpRuntime.Cache.Remove(entry.Key.ToString());
			}
		}

		public static string ServerMapPath(string path)
		{
			string serverMapPath = null;

            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Server != null)
                    serverMapPath = HttpContext.Current.Server.MapPath(path);
            }

			if (serverMapPath.EndsWith("\\")) serverMapPath = serverMapPath.Substring(0, serverMapPath.Length - 1);
			return serverMapPath;
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
			nvc.Add(HttpContext.Current.Request.QueryString);

			foreach (string keyForm in HttpContext.Current.Request.Form.AllKeys)
			{
				foreach (string value in HttpContext.Current.Request.Form.GetValues(keyForm))
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
			string generaHiddenFieldsReturn = null;
			string campos = null;

			campos = "";
			for (int f = 0; f < frm.Form.Count - 1; f++)
			{
				campos = campos + @"<input type=""hidden"" value=""" + Functions.Valor(frm.Form.Keys[f]) + @""" name=""" +
					frm.Form.Get(f) + @"""/>" + "\r\n";
			}
			generaHiddenFieldsReturn = campos;
			return generaHiddenFieldsReturn;
		}
		
		
		public static string Request(string name)
		{
            if (HttpContext.Current == null)
                return "";

            object dato = HttpContext.Current.Request.Form[name];
			if (dato == null) dato = HttpContext.Current.Request.QueryString[name];
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

            object dato = HttpContext.Current.Request.Form[name];
			if (dato == null) dato = HttpContext.Current.Request.QueryString[name];
			if (dato == null) return 0;
			if (dato + "" == "") return 0;

			//seleccionamos el último valor
			string[] datos = dato.ToString().Split(',');
			dato = datos[datos.Length - 1];

			return NumberUtils.NumberInt(dato);
		}


		public static bool RequestBool(string name)
		{
			if (HttpContext.Current == null)
				return false;

			object dato = HttpContext.Current.Request.Form[name];
			if (dato == null) dato = HttpContext.Current.Request.QueryString[name];
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

            object dato = HttpContext.Current.Request.Form[date];
			if (dato == null) dato = HttpContext.Current.Request.QueryString[date];
			if (dato == null)
			{
				return "";
			}

            //seleccionamos el último valor
            string[] datos = dato.ToString().Split(',');
            dato = datos[datos.Length - 1];

            if (!FSLibrary.DateTimeUtil.IsDate(dato.ToString()))
				return "";

			return FSLibrary.DateTimeUtil.ShortDate(Convert.ToDateTime(dato));
		}


		public static string Cookie(HttpCookie dato)
		{
			if (dato == null)
			{
				return "";
			}
			return dato.Value;
		}


		public static string Cookie(HttpCookie dato, string valor)
		{
			if (dato == null)
			{
				return "";
			}
			if (dato[valor] + "" == "")
			{
				return "";
			}
			return dato[valor];
		}

		public static void WriteCookiesToFile(string file, CookieContainer cookieContainer)
		{
			using (Stream stream = File.Create(file))
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, cookieContainer);
			}
		}

		public static CookieContainer ReadCookiesFromFile(string file)
		{
			using (Stream stream = File.Open(file, FileMode.Open))
			{
				BinaryFormatter formatter = new BinaryFormatter();
				return (CookieContainer)formatter.Deserialize(stream);
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

        /// <summary>
        /// Validates a URI
        /// </summary>
        /// <param name="url">URI as string</param>
        /// <returns>True if valid</returns>
        public static bool ValidateURI(string url)
        {
            Uri locurl;
            if (System.Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out locurl))
            {
                if (
                    !(locurl.IsAbsoluteUri &&
                      (locurl.Scheme == "http" || locurl.Scheme == "https")) ||
                    !locurl.IsAbsoluteUri)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
