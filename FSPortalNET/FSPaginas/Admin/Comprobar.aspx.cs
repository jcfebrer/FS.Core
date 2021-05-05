// <fileheader>
// <copyright file="comprobar.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: admin\comprobar.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using FSPlugin;
using FSPortal;
using FSLibrary;
using FSDatabase;

namespace FSPaginas.Admin
{
    public class Comprobar : BasePage
    {
        public string[,] emailList =
        {
            {"ASP Mail", "SMTPsvg.Mailer", "N", "Email"},
            {"CDO NTS", "CDONTS.NewMail", "N", "Email"}, {"CDOSYS", "CDO.Message", "N", "Email"},
            {"Dimac JMail", "JMail.Message", "N", "Email"}, {"Persits ASPEmail", "Persits.MailSender", "N", "Email"}
        };


        public int inCount;

        public string[,] otherList =
        {
            {"ActiveX Data Object", "ADODB.Connection", "Y", "Required for Database Operations"},
            {"File System Object", "Scripting.FileSystemObject", "Y", "Required for Upload Operations"},
            {"Microsoft XML Engine", "Microsoft.XMLDOM", "N", "Required for XML Operations"}
        };

        public string rLang = "";

        public string[,] uploadList =
        {
            {"ASP Simple Upload", "ASPSimpleUpload.Upload", "N", "Upload"},
            {"ASP Smart Upload", "aspSmartUpload.SmartUpload", "N", "Upload"},
            {"Dundas Upload", "Dundas.Upload.2", "N", "Upload"},
            {"Persits File Upload", "Persits.Upload.1", "N", "Upload"},
            {"Soft Artisans File Upload", "SoftArtisans.FileUp", "N", "Upload"}
        };


        public string[,] wLcid =
        {
            {"1078", "Afrikaans", "af"}, {"1052", "Albanian", "sq"}, {"1025", "Arabic(Saudi Arabia)", "ar-sa"},
            {"2049", "Arabic(Iraq)", "ar-iq"}, {"3073", "Arabic(Egypt)", "ar-eg"},
            {"4097", "Arabic(Libya)", "ar-ly"}, {"5121", "Arabic(Algeria)", "ar-dz"},
            {"6145", "Arabic(Morocco)", "ar-ma"}, {"7169", "Arabic(Tunisia)", "ar-tn"},
            {"8193", "Arabic(Oman)", "ar-om"}, {"9217", "Arabic(Yemen)", "ar-ye"},
            {"10241", "Arabic(Syria)", "ar-sy"},
            {
                "11265", "Arabic(Jordan)",
                "ar-jo"
            },
            {"12289", "Arabic(Lebanon)", "ar-lb"}, {"13313", "Arabic(Kuwait)", "ar-kw"},
            {"14337", "Arabic(U.A.E.)", "ar-ae"}, {"15361", "Arabic(Bahrain)", "ar-bh"},
            {"16385", "Arabic(Qatar)", "ar-qa"}, {"1069", "Basque", "eu"}, {"1026", "Bulgarian", "bg"},
            {"1059", "Belarusian", "be"}, {"1027", "Catalan", "ca"}, {"1028", "Chinese(Taiwan)", "zh-tw"},
            {"2052", "Chinese(PRC)", "zh-cn"}, {"3076", "Chinese(Hong Kong)", "zh-hk"},
            {"4100", "Chinese(Singapore)", "zh-sg"},
            {
                "1050",
                "Croatian", "hr"
            },
            {"1029", "Czech", "cs"}, {"1030", "Danish", "da"}, {"1043", "Dutch(Standard)", "n"},
            {"2067", "Dutch(Belgian)", "nl-be"}, {"9", "English", "en"}, {"1033", "English(United States)", "en-us"},
            {"2057", "English(British)", "en-gb"}, {"3081", "English(Australian)", "en-au"},
            {"4105", "English(Canadian)", "en-ca"}, {"5129", "English(New Zealand)", "en-nz"},
            {"6153", "English(Ireland)", "en-ie"}, {"7177", "English(South Africa)", "en-za"},
            {
                "8201", "English(Jamaica)",
                "en-jm"
            },
            {"9225", "English(Caribbean)", "en"}, {"10249", "English(Belize)", "en-bz"},
            {"11273", "English(Trinidad)", "en-tt"}, {"1061", "Estonian", "et"}, {"1080", "Faeroese", "fo"},
            {"1065", "Farsi", "fa"}, {"1035", "Finnish", "fi"}, {"1036", "French(Standard)", "fr"},
            {"2060", "French(Belgian)", "fr-be"}, {"3084", "French(Canadian)", "fr-ca"},
            {"4108", "French(Swiss)", "fr-ch"}, {"5132", "French(Luxembourg)", "fr-lu"},
            {"1071", "FYRO Macedonian", "mk"},
            {
                "1084", "Gaelic(Scots)",
                "gd"
            },
            {"2108", "Gaelic(Irish)", "gd-ie"}, {"1031", "German(Standard)", "de"},
            {"2055", "German(Swiss)", "de-ch"}, {"3079", "German(Austrian)", "de-at"},
            {"4103", "German(Luxembourg)", "de-lu"}, {"5127", "German(Liechtenstein)", "de-li"},
            {"1032", "Greek ", "e"}, {"1037", "Hebrew", "he"}, {"1081", "Hindi", "hi"}, {"1038", "Hungarian", "hu"},
            {"1039", "Icelandic", "is"}, {"1057", "Indonesian", "in"}, {"1040", "Italian(Standard)", "it"},
            {"2064", "Italian(Swiss)", "it-ch"}
            , {"1041", "Japanese", "ja"}, {"1042", "Korean", "ko"}, {"2066", "Korean(Johab)", "ko"},
            {"1062", "Latvian", "lv"}, {"1063", "Lithuanian", "lt"}, {"1086", "Malaysian", "ms"},
            {"1082", "Maltese", "mt"}, {"1044", "Norwegian(Bokmal)", "no"}, {"2068", "Norwegian(Nynorsk)", "no"},
            {"1045", "Polish", "p"}, {"1046", "Portuguese(Brazil)", "pt-br"}, {"2070", "Portuguese(Portugal)", "pt"},
            {"1047", "Rhaeto-Romanic", "rm"}, {"1048", "Romanian", "ro"},
            {
                "2072", "Romanian(Moldavia)",
                "ro-mo"
            },
            {"1049", "Russian", "ru"}, {"2073", "Russian(Moldavia)", "ru-mo"}, {"1083", "Sami(Lappish)", "sz"},
            {"3098", "Serbian(Cyrillic)", "sr"}, {"2074", "Serbian(Latin)", "sr"}, {"1051", "Slovak", "sk"},
            {"1060", "Slovenian", "s"}, {"1070", "Sorbian", "sb"},
            {"1034", "Spanish(Spain - Traditional Sort)", "es"}, {"2058", "Spanish(Mexican)", "es-mx"},
            {"3082", "Spanish(Spain - Modern Sort)", "es"}, {"4106", "Spanish(Guatemala)", "es-gt"},
            {
                "5130", "Spanish(Costa Rica)", "es-cr"
            },
            {"6154", "Spanish(Panama)", "es-pa"}, {"7178", "Spanish(Dominican Republic)", "es-do"},
            {"8202", "Spanish(Venezuela)", "es-ve"}, {"9226", "Spanish(Colombia)", "es-co"},
            {"10250", "Spanish(Peru)", "es-pe"}, {"11274", "Spanish(Argentina)", "es-ar"},
            {"12298", "Spanish(Ecuador)", "es-ec"}, {"13322", "Spanish(Chile)", "es-c"},
            {"14346", "Spanish(Uruguay)", "es-uy"}, {"15370", "Spanish(Paraguay)", "es-py"},
            {"16394", "Spanish(Bolivia)", "es-bo"},
            {
                "17418", "Spanish(El Salvador)",
                "es-sv"
            },
            {"18442", "Spanish(Honduras)", "es-hn"}, {"19466", "Spanish(Nicaragua)", "es-ni"},
            {"20490", "Spanish(Puerto Rico)", "es-pr"}, {"1072", "Sutu", "sx"}, {"1053", "Swedish", "sv"},
            {"2077", "Swedish(Finland)", "sv-fi"}, {"1054", "Thai", "th"}, {"1073", "Tsonga", "ts"},
            {"1074", "Tswana", "tn"}, {"1055", "Turkish", "tr"}, {"1058", "Ukrainian", "uk"}, {"1056", "Urdu", "ur"},
            {"1075", "Venda", "ve"}, {"1066", "Vietnamese", "vi"}, {"1076", "Xhosa", "xh"},
            {
                "1085",
                "Yiddish", "ji"
            },
            {"1077", "Zulu", "zu"}
        };


        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        public string Inicio()
        {
            StringBuilder sb = new StringBuilder("");

            sb.Append(Ui.Lf() + "<p class='cabepeque'>");

            sb.Append(FuncionesWeb.Idioma(4));

            sb.Append("</p>");

            sb.Append(FuncionesWeb.Idioma(5) + Ui.Lf());
            sb.Append(FuncionesWeb.Idioma(6) + Ui.Lf());
            sb.Append(FuncionesWeb.Idioma(7) + Ui.Lf());
            sb.Append(FuncionesWeb.Idioma(8) + Ui.Lf());
            sb.Append(FuncionesWeb.Idioma(9) + Ui.Lf());
            sb.Append(FuncionesWeb.Idioma(10) + Ui.Lf());
            sb.Append(FuncionesWeb.Idioma(11) + Ui.Lf() + Ui.Lf());

            sb.Append(@"<table border=""0"" width=""100%"">" + "\r\n");
            sb.Append("<theader><tr>" + "\r\n");
            sb.Append(@"<th colspan=""2"">Components</th>" + "\r\n");
            sb.Append("</tr></theader><tbody>" + "\r\n");
            sb.Append(CheckCom(emailList, ref inCount));
            if (inCount == 0)
            {
                sb.Append("<tr>" + "\r\n");
                sb.Append(
                    @"<td colspan=""2"" class=""error""><b>At Least one Component is Required for Emailing</b></td>" +
                    "\r\n");
                sb.Append("</tr>" + "\r\n");
            }
            sb.Append(CheckCom(uploadList, ref inCount));
            if (inCount == 0)
            {
                sb.Append("<tr>" + "\r\n");
                sb.Append(
                    @"<td colspan=""2"" class=""error""><b>At Least one Component is Required for Uploading</b></td>" +
                    "\r\n");
                sb.Append("</tr>" + "\r\n");
            }
            sb.Append(CheckCom(otherList, ref inCount));
            sb.Append("</tr>" + "\r\n");
            sb.Append("<tr>" + "\r\n");
            sb.Append("</tr>" + "\r\n");
            ////sb.Append("<tr>" + "\r\n");
            ////sb.Append("<td>" + Globals.ScriptEngine + " ( require 5.6 )</td>" + "\r\n");
            ////sb.Append("<td class=""cabepeque"">" + "Your Server Version " + Globals.ScriptEngineMajorVersion + "." + Globals.ScriptEngineMinorVersion + "." + Globals.ScriptEngineBuildVersion);
            ////if (double.Parse(Globals.ScriptEngineMajorVersion + "." + Globals.ScriptEngineMinorVersion) < double.Parse("5.6"))
            ////    {
            ////    sb.Append(" ( Needs Updating )");
            ////    }
            ////else
            ////    {
            ////    sb.Append(" ( OK )");
            ////    }
            ////sb.Append("</td>" + "\r\n");
            ////sb.Append("</tr>" + "\r\n");
            /// 
            sb.Append(ShowValue("Browser LCID: ",SetLcid(wLcid, ref rLang)));
            sb.Append(ShowValue("Server LCID: ",Session.LCID.ToString()));
            sb.Append(ShowValue("DateTime (14/3/2012 15:26:10) - ",new System.DateTime(2012,3,14,15,26,10).ToString("d")));
            sb.Append(ShowValue("Currency: ",String.Format("{0:C}", 1234567.890m)));
            sb.Append(ShowServerVariables());
            sb.Append(ShowCookies());
            sb.Append(ShowSession());
            sb.Append(ShowVariables());
            sb.Append(ShowCache());
            sb.Append(ShowDataServer());
            sb.Append(ShowPlugins());
            sb.Append(ShowLibraries());
            sb.Append(ShowDataProvider());

            sb.Append("</tbody></table>" + Ui.Lf() + "\r\n");


            sb.Append("Is custom errors enabled: " + Context.IsCustomErrorEnabled + Ui.Lf());
            sb.Append("Is debugging enabled: " + Context.IsDebuggingEnabled + Ui.Lf());
            sb.Append("Trace Enabled: " + Context.Trace.IsEnabled + Ui.Lf());
            sb.Append("Number of items in Application state: " + Context.Application.Count + Ui.Lf());
            try
            {
                sb.Append("Number of items in Session state: " + Context.Session.Count + Ui.Lf());
            }
            catch
            {
                sb.Append("Session state not enabled. " + Ui.Lf());
            }

            sb.Append("Number of items in the cache: " + Context.Cache.Count + Ui.Lf());

            sb.Append("Timestamp for the HTTP request: " + Context.Timestamp + Ui.Lf());

            sb.Append("Compilation type: " + NumberUtils.CompilationSize());

            return sb.ToString();
        }

        /// <summary>
        ///     M¨¦todo que muestra los ensamblados de la aplicación.
        /// </summary>
        private static string RetrieveAssembliesVersion(string appPath)
        {
            StringBuilder s = new StringBuilder("");

            if (!string.IsNullOrEmpty(appPath) && Directory.Exists(appPath))
            {
                DirectoryInfo dInfo = new DirectoryInfo(appPath + @"\bin\");
                FileInfo[] assemblies = dInfo.GetFiles("*.dll");

                if (assemblies.Length > 0)
                {
                    Assembly executingAssembly = Assembly.GetExecutingAssembly();
                    s.AppendLine(executingAssembly.GetName().FullName + "</br>");

                    foreach (FileInfo file in assemblies)
                    {
                        FileVersionInfo fVersion = FileVersionInfo.GetVersionInfo(file.FullName);
                        string versionString = GetFileVersionString(fVersion);
                        s.AppendLine(versionString + Ui.Lf());
                    }
                }
            }

            return s.ToString();
        }


        /// <summary>
        ///     Obtiene una cadena con nombre de ensamblado y version
        /// </summary>
        /// <param name="fv">Información sobre la versión del archivo</param>
        /// <returns></returns>
        private static string GetFileVersionString(FileVersionInfo fv)
        {
            return fv.OriginalFilename + " - " +
                   fv.FileMajorPart + "." +
                   fv.FileMinorPart + "." +
                   fv.FileBuildPart;
        }


        public bool CheckObject(string comIdentity)
        {
            bool chk = false;
            try
            {
                object obj = Server.CreateObject(comIdentity);
                if(obj!= null) chk = true;
            }
            catch
            {
                chk = false;
            }
            return chk;
        }

        public string CheckCom(string[,] comList, ref int icount)
        {
            string strTxt = null;
            int idx = 0;
            strTxt = "";
            icount = 0;

            for (idx = comList.GetLowerBound(0); idx <= comList.GetUpperBound(0); idx++)
            {
                if (CheckObject(comList[idx, 1]))
                {
                    strTxt = strTxt + "<tr><td>" + comList[idx, 0] + "</font></td>" + "\r\n";
                    strTxt = strTxt + @"<td class=""cabepeque"">OK ( " + comList[idx, 3] + " )</td>" + "\r\n";
                    icount = icount + 1;
                }
                else
                {
                    strTxt = strTxt + "<tr><td>" + comList[idx, 0] + "</td>" + "\r\n";
                    strTxt = strTxt + @"<td class=""error"">Not Installed ( " + comList[idx, 3] + " )</td>" + "\r\n";
                }
            }
            strTxt = strTxt + "</tr>" + "\r\n";
            return strTxt;
        }
        
        string ShowValue(string caption, string value)
        {
        	StringBuilder sb = new StringBuilder("");
        	sb.Append("<tr>" + "\r\n");
            sb.Append(@"<td>" + caption + "</td>" + "\r\n");
            sb.Append(@"<td class=""cabepeque"">" + value + "</td>" + "\r\n");
            sb.Append("</tr>" + "\r\n");
            return sb.ToString();
        }


        public string ShowServerVariables()
        {
            StringBuilder sb = new StringBuilder("");

            sb.Append("<tr>" + "\r\n");
            sb.Append(@"<td colspan=""2"" class=""cabepeque""><b>Server Variables</b></td>" + "\r\n");
            sb.Append("</tr>" + "\r\n");

            try
            {
                foreach (string key in Request.ServerVariables)
                {
                    if (Request.ServerVariables[key] != "")
                    {
                        string t = Request.ServerVariables[key];

                        if (key.ToUpper() == "AUTH_PASSWORD")
                        {
                            t = new string('*', t.Length);
                        }
                        
                        //if (Functions.InStr(Key, "ALL_") == -1 && Functions.InStr(Key, "HTTP_AUTHORIZATION") == -1)
                        //{
                        sb.Append("\r\n" + @"<tr><td>" + key + "</td>" + "\r\n");
                        sb.Append("\r\n" + @"<td>" + t + "</td></tr>" + "\r\n");
                    }
                }

                return sb.ToString();
            }
            catch (System.Exception e)
            {
                return "<tr><td>" + e.Message + "</td></tr>";
            }
        }


        public string ShowCache()
        {
            StringBuilder sb = new StringBuilder("");

            sb.Append("<tr>" + "\r\n");
            sb.Append(@"<td colspan=""2"" class=""cabepeque""><b>Cache Variables</b></td>" + "\r\n");
            sb.Append("</tr>" + "\r\n");

            try
            {
                IDictionaryEnumerator c = Cache.GetEnumerator();
                while (c.MoveNext())
                {
                    if (Functions.Valor(c.Current.ToString()) != "")
                    {
                        sb.Append("\r\n" + @"<tr><td>" + c.Key + "</td>" + "\r\n");
                        sb.Append("\r\n" + @"<td class=""cabepeque"">" +
                                  Server.HtmlEncode(TextUtil.Left(c.Value.ToString(), 50)) + "</td></tr>" + "\r\n");
                    }
                }
                return sb.ToString();
            }
            catch (System.Exception e)
            {
                return "<tr><td>" + e.Message + "</td></tr>";
            }
        }


        public string ShowCookies()
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append("<tr>" + "\r\n");
            sb.Append(@"<td colspan=""2"" class=""cabepeque""><b>Cookies</b></td>" + "\r\n");
            sb.Append("</tr>" + "\r\n");

            try
            {
                foreach (string x in Request.Cookies)
                {
                    sb.Append("<tr>");
                    if (Request.Cookies[x].HasKeys)
                    {
                        foreach (string y in Request.Cookies[x].Values)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td>" + x + ":" + y + "</td><td>" + Request.Cookies[x][y]);
                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }
                    }
                    else
                    {
                        sb.Append("<td>" + x + "</td><td>" + Request.Cookies[x].Value + "</td>");
                    }
                    sb.Append("</tr>");
                }

                return sb.ToString();
            }
            catch (System.Exception e)
            {
                return "<tr><td>" + e.Message + "</td></tr>";
            }
        }


        public string ShowDataServer()
        {
            StringBuilder sb = new StringBuilder("");
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            sb.Append("<tr>" + "\r\n");
            sb.Append(@"<td colspan=""2"" class=""cabepeque""><b>Data Server</b></td>" + "\r\n");
            sb.Append("</tr>" + "\r\n");

            sb.Append("<tr><td>Versión</td><td>" + db.ServerVersion() + "</td></tr>");
            sb.Append("<tr><td>ConnectionString</td><td>" + db.ConnString + "</td></tr>");

            return sb.ToString();
        }

        public string ShowLibraries()
        {
            StringBuilder sb = new StringBuilder("");

            sb.Append("<tr>" + "\r\n");
            sb.Append(@"<td colspan=""2"" class=""cabepeque""><b>Librerias</b></td>" + "\r\n");
            sb.Append("</tr>" + "\r\n");

            sb.Append("<tr><td>&nbsp;</td><td>" + RetrieveAssembliesVersion(Server.MapPath(Variables.App.directorioPortal)) +
                      "</td></tr>");

            return sb.ToString();
        }


        public string ShowPlugins()
        {
            StringBuilder sb = new StringBuilder("");

            sb.Append("<tr>" + "\r\n");
            sb.Append(@"<td colspan=""2"" class=""cabepeque""><b>Plugins</b></td>" + "\r\n");
            sb.Append("</tr>" + "\r\n");

            int pos = 0;
            
            foreach (IPlugin plugin in Variables.App.Plugins)
            {
                sb.Append("<tr><td>" + pos + @"</td><td><a class=""_editbox"" href=""" + Variables.App.directorioPortal +
                          "plugin.aspx?simple=0&c=" + plugin.Name + @""">" + plugin.Name + "(" + plugin.Parameters +
                          ")</a></td></tr>");
                pos++;
            }

            return sb.ToString();
        }


        public string ShowSession()
        {
            StringBuilder sb = new StringBuilder("");

            sb.Append("<tr>" + "\r\n");
            sb.Append(@"<td colspan=""2"" class=""cabepeque""><b>Session</b></td>" + "\r\n");
            sb.Append("</tr>" + "\r\n");

            try
            {
                for (int f = 0; f < Variables.App.Page.Session.Count - 1; f++)
                {
                    sb.Append("<tr><td>");
                    sb.Append(Variables.App.Page.Session.Keys[f]);
                    sb.Append("</td><td>");
                    sb.Append(Variables.App.Page.Session[f]);
                    sb.Append("</td></tr>");
                }

                sb.Append("<tr><td>sessionID</td><td>" + Variables.App.Page.Session.SessionID + "</td></tr>");
            }
            catch
            {
                sb.Append("<tr><td>Error!</td><td>Imposible recuperar variables Session.</td></tr>");
            }

            return sb.ToString();
        }


        public string ShowVariables()
        {
            StringBuilder sb = new StringBuilder("");

            sb.Append("<tr>" + "\r\n");
            sb.Append(@"<td colspan=""2"" class=""cabepeque""><b>Variables Usuario</b></td>" + "\r\n");
            sb.Append("</tr>" + "\r\n");

            sb.Append("<tr><td>Usuario</td><td>" + Variables.User.Usuario + "</td></tr>");
            sb.Append("<tr><td>UsuarioId</td><td>" + Variables.User.UsuarioId + "</td></tr>");
            sb.Append("<tr><td>NombreCompleto</td><td>" + Variables.User.NombreCompleto + "</td></tr>");
            sb.Append("<tr><td>Administrador</td><td>" + Variables.User.Administrador + "</td></tr>");
            sb.Append("<tr><td>Dto</td><td>" + Variables.User.Dto + "</td></tr>");
            sb.Append("<tr><td>UltimaConexion</td><td>" + Variables.User.ultimaConexion + "</td></tr>");
            sb.Append("<tr><td>Comeback</td><td>" + Variables.User.ComeBack + "</td></tr>");
            sb.Append("<tr><td>PrecioaMostrar</td><td>" + Variables.User.PrecioAMostrar + "</td></tr>");

            sb.Append("<tr>" + "\r\n");
            sb.Append(@"<td colspan=""2"" class=""cabepeque""><b>Variables Globales</b></td>" + "\r\n");
            sb.Append("</tr>" + "\r\n");

            sb.Append("<tr><td>Moneda</td><td>" + Variables.App.moneda + "</td></tr>");
            sb.Append("<tr><td>Iva</td><td>" + Variables.App.Iva + "</td></tr>");
            sb.Append("<tr><td>DireccionFisica</td><td>" + Variables.App.direccionFisica + "</td></tr>");
            sb.Append("<tr><td>CodigoPostal</td><td>" + Variables.App.codigoPostal + "</td></tr>");
            sb.Append("<tr><td>Provincia</td><td>" + Variables.App.provincia + "</td></tr>");
            sb.Append("<tr><td>DirectorioPortal</td><td>" + Variables.App.directorioPortal + "</td></tr>");
            sb.Append("<tr><td>NombreWeb</td><td>" + Variables.App.nombreWeb + "</td></tr>");
            sb.Append("<tr><td>CorreoInfo</td><td>" + Variables.App.correoInfo + "</td></tr>");
            sb.Append("<tr><td>ServidorCorreo</td><td>" + Variables.App.servidorCorreo + "</td></tr>");
            sb.Append("<tr><td>WebHttp</td><td>" + Variables.App.webHttp + "</td></tr>");
            sb.Append("<tr><td>MultiCestas</td><td>" + Variables.App.multiCestas + "</td></tr>");
            sb.Append("<tr><td>UploadPath</td><td>" + Variables.App.uploadPath + "</td></tr>");
            sb.Append("<tr><td>ExplorerPath</td><td>" + Variables.App.explorerPath + "</td></tr>");
            sb.Append("<tr><td>Http_Host</td><td>" + Variables.App.HTTP_HOST + "</td></tr>");

            return sb.ToString();
        }


        public string SetLcid(string[,] lcid, ref string lang)
        {
            try
            {
                int rCount;
                string wLang = Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"];
                int wPos = wLang.IndexOf(",", 0) + 1;
                if (wPos > 0)
                {
                    wLang = TextUtil.Substring(wLang, 0, wPos - 1);
                }

                for (rCount = 0; rCount <= wLcid.GetUpperBound(0); rCount++)
                {
                    if (wLang.ToLower() == lcid[rCount, 2])
                    {
                        string setLcidReturn = lcid[rCount, 0];
                        lang = lcid[rCount, 1];
                        return setLcidReturn;
                    }
                }
                return "";
            }
            catch (System.Exception e)
            {
                return e.Message;
            }
        }
        
        
        public string ShowDataProvider()
		{
			StringBuilder sb = new StringBuilder("");

            sb.Append("<tr>" + "\r\n");
            sb.Append(@"<td colspan=""2"" class=""cabepeque""><b>Proveedores de datos instalados</b></td>" + "\r\n");
            sb.Append("</tr>" + "\r\n");
            
		    // Retrieve the installed providers and factories.
		    DataTable table = System.Data.Common.DbProviderFactories.GetFactoryClasses();
		
		    // Display each row and column value.
		    foreach (DataRow row in table.Rows)
		    {
		    	sb.Append("<tr><td>");
                foreach (DataColumn column in table.Columns)
		        {
		            sb.Append(row[column] + "<br />");
                }
                sb.Append("<br />-----------------------<br />");
                sb.Append("</td></tr>");
		    }
		    return sb.ToString();
		}
    }
}