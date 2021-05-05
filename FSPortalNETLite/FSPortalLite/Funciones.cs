using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace FSPortalLite
{
    public class Funciones
    {
        private static string _virtualPath;
        public static string VirtualPath
        {
            set { _virtualPath = value; }
            get
            {
                if (_virtualPath == null)
                {
                    string url = HttpContext.Current.Request.ApplicationPath;
                    if (url != "/") url += "/";
                    _virtualPath = url;
                }
                return _virtualPath;
            }
        }

        public static string Md5(string value)
        {
            var data = Encoding.ASCII.GetBytes(value);
            var md5Sp = new MD5CryptoServiceProvider();
            var hashbyte = md5Sp.ComputeHash(data, 0, data.Length);
            var strHash = BitConverter.ToString(hashbyte);
            strHash = strHash.Replace("-", "");
            return strHash;
        }

        public static DataTable Execute(string connString, string sql)
        {
            try
            {
                DataTable dt = new DataTable();

                sql = HttpUtility.HtmlDecode(sql);

                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings[connString].ConnectionString))
                {
                    using (OracleCommand dbc = new OracleCommand(sql, conn))
                    {
                        using (var da = new OracleDataAdapter(dbc))
                        {
                            dt.BeginLoadData();
                            da.Fill(dt);
                            dt.EndLoadData();

                            return dt;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }

        public static DataRow[] XMLSelect(string fileName, string select)
        {
            DataTable dtPaginas = new DataTable();
            dtPaginas.ReadXmlSchema(HttpContext.Current.Server.MapPath("~") + "\\sitios\\" + Variables.App.nombreWeb + "\\data\\" + fileName + ".xsd");
            dtPaginas.ReadXml(HttpContext.Current.Server.MapPath("~") + "\\sitios\\"+ Variables.App.nombreWeb +"\\data\\" + fileName + ".xml");

            return dtPaginas.Select(select);
        }

        public static DataTable XMLDataTable(string fileName, string select)
        {
            DataTable dtPaginas = new DataTable();
            dtPaginas.ReadXmlSchema(HttpContext.Current.Server.MapPath("~") + "\\sitios\\" + Variables.App.nombreWeb + "\\data\\" + fileName + ".xsd");
            dtPaginas.ReadXml(HttpContext.Current.Server.MapPath("~") + "\\sitios\\" + Variables.App.nombreWeb + "\\data\\" + fileName + ".xml");

            return dtPaginas.Select(select).CopyToDataTable();
        }

        public static DataTable XMLDataTable(string fileName)
        {
            DataTable dtPaginas = new DataTable();
            dtPaginas.ReadXmlSchema(HttpContext.Current.Server.MapPath("~") + "\\sitios\\" + Variables.App.nombreWeb + "\\data\\" + fileName + ".xsd");
            dtPaginas.ReadXml(HttpContext.Current.Server.MapPath("~") + "\\sitios\\" + Variables.App.nombreWeb + "\\data\\" + fileName + ".xml");

            return dtPaginas;
        }

        public static string formatJSON(string data)
        {
            data = HttpUtility.HtmlEncode(data);
            data = data.Replace("\r\n", "<br />");
            data = data.Replace("\r", "<br />");
            data = data.Replace("\n", "<br />");
            data = data.Replace("\t", "");
            data = data.Replace(":", "-");
            data = data.Replace(",", ";");
            data = data.Replace("\\", "/");

            data = RemoveHTML(data);

            return data;
        }

        public static string RemoveHTML(string html)
        {
            Regex regex = new Regex("</?(.*)>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return regex.Replace(html, string.Empty);
        }

        public static bool Existe(string[] valores, string valor)
        {
            foreach (var s in valores)
                if (s.ToLower() == valor.ToLower())
                    return true;
            return false;
        }

        public static string ReorgQuery(string query)
        {
            var ret = "";
            //reorganizamos la cadena request
            var lstParam = new List<string>();
            var arrParam = query.Split('&');
            foreach (var param in arrParam)
                if (param != "")
                    lstParam.Add(param);
            ret = string.Join("&", lstParam.ToArray());

            return ret;
        }


        public static string Request(string name)
        {
            object dato = HttpContext.Current.Request.Form[name];
            if (dato == null) dato = HttpContext.Current.Request.QueryString[name];
            if (dato == null) return "";

            //dato = TextUtil.RemoveIllegalData(dato.ToString());

            //dato = Replace(Valor(dato), ",", "{coma}");

            //seleccionamos el último valor
            string[] datos = dato.ToString().Split(',');
            dato = datos[datos.Length - 1];

            return dato + "";
        }


        public static int RequestInt(string name)
        {
            object dato = HttpContext.Current.Request.Form[name];
            if (dato == null) dato = HttpContext.Current.Request.QueryString[name];
            if (dato == null) return 0;
            if (dato + "" == "") return 0;

            if (dato.ToString().IndexOf(',') != -1)
            {
                string[] values = dato.ToString().Split(',');
                dato = values[0];
            }

            return Convert.ToInt32(dato);
        }


        public static bool RequestBool(string name)
        {
            object dato = HttpContext.Current.Request.Form[name];
            if (dato == null) dato = HttpContext.Current.Request.QueryString[name];
            if (dato == null)
            {
                return false;
            }

            return Convert.ToBoolean(dato);
        }

        public static bool IsDate(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return false;

            if (expression.Length < 6)
                return false;

            System.DateTime dateTime;
            if (System.DateTime.TryParse(expression, out dateTime))
                return true;
            return false;
        }


        public static string RequestDate(string date)
        {
            object dato = HttpContext.Current.Request.Form[date];
            if (dato == null) dato = HttpContext.Current.Request.QueryString[date];
            if (dato == null)
            {
                return "";
            }

            //seleccionamos el último valor
            string[] datos = dato.ToString().Split(',');
            dato = datos[datos.Length - 1];

            if (!IsDate(dato.ToString()))
                return "";

            return Convert.ToDateTime(dato).ToShortDateString();
        }

        public static string ReplaceREG(string cadena, string regex, string regreplace)
        {
            return ReplaceREG(cadena, regex, regreplace, RegexOptions.None);
        }

        public static string ReplaceREG(string cadena, string regex, string regreplace, RegexOptions options)
        {
            var oReg = new Regex(regex, options);
            var oMatch = oReg.Match(cadena);
            if (oMatch.Success) return oReg.Replace(cadena, regreplace);
            return cadena;
        }

        public static string RequestQueryForm()
        {
            return RequestQueryForm("");
        }

        public static string RequestQueryForm(string keysToDelete)
        {
            string[] arrKeysToDelete = keysToDelete.Split(',');

            //cojemos las colecciones de FORM y QUERYSTRING, y las unificamos sin repetidos
            //prevaleciendo las del FORM
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add(HttpContext.Current.Request.QueryString);

            foreach (string keyForm in HttpContext.Current.Request.Form.AllKeys)
            {
                //if (nvc.GetValues(keyForm) == null)
                //{
                    foreach (string value in HttpContext.Current.Request.Form.GetValues(keyForm))
                    {
                        //si exite un parametro en la colleccion nvc de querystring, lo borramos, y añadimos el del form.
                        if (nvc.Get(keyForm) != null)
                            nvc.Remove(keyForm);

                        nvc.Add(keyForm, value);
                    }
                //}
            }


            List<string> urlValues = new List<string>();
            foreach (string key in nvc.AllKeys)
            {
                if (key != null)
                {
                    if (!Existe(arrKeysToDelete, key))
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
    }
}
