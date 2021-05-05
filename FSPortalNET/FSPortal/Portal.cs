// // <fileheader>
// // <copyright file="Portal.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSPortal
// //     Solution: FSPortalNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System;
using System.Configuration;
using System.Data;
using System.Web;
using FSPlugin;
using FSLibrary;
using FSQueryBuilder;
using FSQueryBuilder.Enums;
using FSQueryBuilder.QueryParts.Where;
using FSDatabase;
using FSNetwork;
using FSException;
using FSMail;
using FSCrypto;
using System.Web.SessionState;
using FSTrace;

#endregion

namespace FSPortal
{
    /// <summary>
    ///     clases genéricas para la utilización del portal
    /// </summary>
    public class Portal
    {
        /// <summary>
        ///     Inicialización
        /// </summary>
        public Portal()
        {
            Variables.App.connectionString = ConfigurationManager.ConnectionStrings[Variables.App.defaultEntry].ConnectionString;
            Variables.App.providerName = ConfigurationManager.ConnectionStrings[Variables.App.defaultEntry].ProviderName;

            LoadVariables();
        }

        /// <summary>
        ///     Inicialización
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="providerName"></param>
        public Portal(string connectionString, string providerName)
        {
            Variables.App.connectionString = connectionString;
            Variables.App.providerName = providerName;

            LoadVariables();
        }

        /// <summary>
        ///     Inicialización
        /// </summary>
        /// <param name="defaultEntry"></param>
        public Portal(string defaultEntry)
        {
            Variables.App.defaultEntry = defaultEntry;

            Variables.App.connectionString = ConfigurationManager.ConnectionStrings[Variables.App.defaultEntry].ConnectionString;
            Variables.App.providerName = ConfigurationManager.ConnectionStrings[Variables.App.defaultEntry].ProviderName;

            LoadVariables();
        }


        /// <summary>
        ///     Método para realizar el login al portal
        /// </summary>
        /// <param name="sUsuario"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public void Login(string sUsuario, string sPassword)
        {
            sUsuario = TextUtil.RemoveIllegalData(sUsuario);
            sPassword = TextUtil.RemoveIllegalData(sPassword);
            
			Crypto crypt = new Crypto();
			string sPassword2 = crypt.Md5(sPassword.ToLower());

            string[] columnas;
            if (Variables.App.modoLite)
            {
                columnas = new string[] { "Clave", "UsuarioId", "Usuario", "grupo", "campo1", "campo2", "campo3", "campo4" };
            }
            else
            {
                columnas = new string[] { "Clave", "UsuarioId", "Usuario", "nombre", "apellido1", "apellido2", "grupo", "idioma", "dto", "UltimaConexion", "paginaInicio", "active", "precioAMostrar", "campo1", "campo2", "campo3", "campo4" };
            }

            DataTable dt;

            if (Variables.App.UseXML)
            {
                XML xml = new XML(Variables.App.directorioWeb + "data");
                xml.Load("usuarios.xml");
                dt = xml.Select(sUsuario.IndexOf("@") > 0 ? "email='" + sUsuario.ToLower() + "'" : "usuario='" + sUsuario.ToLower() + "'");
            }
            else
            {
                BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
                SelectQueryBuilder sqB = new SelectQueryBuilder();

                sqB.Columns.SelectColumns(columnas);
                sqB.TableSource = Variables.App.prefijoTablas + "Usuarios";

                sqB.Where = sUsuario.IndexOf("@") > 0 ? new SimpleWhere(sqB.TableSource, "email", Comparison.Equals, sUsuario.ToLower()) : new SimpleWhere(sqB.TableSource, "usuario", Comparison.Equals, sUsuario.ToLower());

                dt = db.Execute(sqB.BuildQuery());
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                if (Functions.Valor(dt.Rows[0]["Clave"]).ToLower() == sPassword.ToLower() |
                    Functions.Valor(dt.Rows[0]["Clave"]).ToLower() == sPassword2.ToLower())
                {
                    double uid = NumberUtils.NumberDouble(dt.Rows[0]["UsuarioId"]);
                    string use = Functions.Valor(dt.Rows[0]["Usuario"]);

                    Variables.User.Usuario = use;
                    Variables.User.UsuarioId = NumberUtils.NumberInt(uid);
                    Variables.User.GroupId = NumberUtils.NumberInt(dt.Rows[0]["grupo"]);
                    Variables.User.Administrador = FuncionesWeb.EsAdmin(Variables.User.GroupId);

                    Variables.User.Campo1 = Functions.Valor(dt.Rows[0]["campo1"]);
                    Variables.User.Campo2 = Functions.Valor(dt.Rows[0]["campo2"]);
                    Variables.User.Campo3 = Functions.Valor(dt.Rows[0]["campo3"]);
                    Variables.User.Campo4 = Functions.Valor(dt.Rows[0]["campo4"]);

                    if (!Variables.App.modoLite)
                    {
                        if (Functions.ValorBool(dt.Rows[0]["active"]) != true)
                        {
                            throw new ExceptionUtil(
                                "El perfíl de usuario no esta activo. Para más información contacta con el administrador.",
                                ExceptionUtil.ExceptionType.Information);
                        }

                        Variables.User.NombreCompleto =
                            Functions.Valor(dt.Rows[0]["Nombre"] + " " + dt.Rows[0]["Apellido1"] + " " +
                                       dt.Rows[0]["Apellido2"]);

                        Variables.User.Dto = NumberUtils.NumberInt(dt.Rows[0]["dto"]);
                        Variables.User.ultimaConexion = Functions.Valor(dt.Rows[0]["UltimaConexion"]);
                        Variables.User.paginaInicio = NumberUtils.NumberInt(dt.Rows[0]["paginaInicio"]);

                        Variables.User.PrecioAMostrar = Functions.Valor(dt.Rows[0]["precioAMostrar"]);
                        if (Functions.Valor(Variables.User.PrecioAMostrar) == "") Variables.User.PrecioAMostrar = "PrecioA";

                        Variables.User.idiomaSel = Functions.Valor(dt.Rows[0]["idioma"]);
                    }

                    Variables.User.UserData = dt.Rows[0];
                    
                    SendMail.UserPortal = Variables.User.Usuario;
                    SendMail.UserFullName = Variables.User.NombreCompleto;


                    if (Variables.User.Administrador)
                    {
                        Variables.User.bRecAdd = true;
                        Variables.User.bRecEdit = true;
                        Variables.User.bRecDel = true;
                        Variables.User.bQueryExec = true;
                        Variables.User.bSQLExec = true;
                        Variables.User.bTableAdd = true;
                        Variables.User.bTableEdit = true;
                        Variables.User.bTableDel = true;
                        Variables.User.bFldAdd = true;
                        Variables.User.bFldEdit = true;
                        Variables.User.bFldDel = true;
                    }
                    else
                    {
                        Variables.User.bRecAdd = false;
                        Variables.User.bRecEdit = false;
                        Variables.User.bRecDel = false;
                        Variables.User.bQueryExec = false;
                        Variables.User.bSQLExec = false;
                        Variables.User.bTableAdd = false;
                        Variables.User.bTableEdit = false;
                        Variables.User.bTableDel = false;
                        Variables.User.bFldAdd = false;
                        Variables.User.bFldEdit = false;
                        Variables.User.bFldDel = false;
                    }

                    string cla = dt.Rows[0]["Clave"].ToString();

                    if (!Variables.App.modoLite)
                    {
                        if (!Variables.App.UseXML)
                        {
                            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
                            int i = db.ExecuteNonQuery("update " + Variables.App.prefijoTablas + "usuarios set UltimaConexion='" +
                                                       FSLibrary.DateTimeUtil.ShortDate(System.DateTime.Now) + " " + System.DateTime.Now.ToShortTimeString() +
                                                       "' WHERE usuarioID=" + uid);
                            if (i == 0)
                            {
                                throw new ExceptionUtil("Imposible actualizar información del usuario.");
                            }

                            db.ExecuteNonQuery("update " + Variables.App.prefijoTablas + "usuarios set vecesConectado=0 WHERE usuarioID=" +
                                               uid + " and vecesConectado is null");
                            db.ExecuteNonQuery("update " + Variables.App.prefijoTablas +
                                               "usuarios set vecesConectado=vecesConectado+1 WHERE usuarioID=" + uid);

                            if (cla.ToLower() == sPassword.ToLower())
                            {
                                db.ExecuteNonQuery("update " + Variables.App.prefijoTablas + "usuarios set clave='" + sPassword2 +
                                                   "' WHERE usuarioID=" + uid);
                            }
                        }
                    }

                    Log.TraceInfo("El Usuario: " + Variables.User.Usuario + " ha accedido al portal.");
                }
                else
                {
					throw new ExceptionUtil("Clave de acceso incorrecta.", ExceptionUtil.ExceptionType.Information);
                }
            }
            else
            {
				throw new ExceptionUtil("Credenciales incorrectos.", ExceptionUtil.ExceptionType.Information);
            }
        }


        /// <summary>
        ///     Lectura de las variables globales y conexión a la base de datos
        /// </summary>
        public void LoadVariables()
        {
            if (Variables.App.Page != null && Variables.App.Page.Session != null)
            {
                Variables.User.sessionID = Variables.App.Page.Session.SessionID;
                Variables.App.HTTP_HOST = Functions.Valor(HttpContext.Current.Request.ServerVariables["HTTP_HOST"]);
                Variables.User.HTTP_USER_AGENT = Functions.Valor(HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"]);

                Variables.App.webHttp = "http://" + Functions.Valor(HttpContext.Current.Request.ServerVariables["SERVER_NAME"]);
            }
            
            if (Variables.App.prefijoTablas == null)
                Variables.App.prefijoTablas = Functions.Valor(ConfigurationManager.AppSettings["prefijoTablas"]);

            Variables.App.defaultEntry = "FSConnection";

            //leemos los plugins
            Variables.App.Plugins = (PluginCollection)ConfigurationManager.GetSection("plugins");

            Variables.App.directorioPortal = Web.VirtualPath;
            Variables.App.ModoPrueba = Functions.ValorBool(ConfigurationManager.AppSettings["ModoPrueba"]) || Http.IsLocalhost();
            Variables.App.UseXML = Functions.ValorBool(ConfigurationManager.AppSettings["UseXML"]);

            Variables.App.estado = NumberUtils.NumberInt(ConfigurationManager.AppSettings["Estado"]);
            Variables.App.moneda = Functions.Valor(ConfigurationManager.AppSettings["Moneda"]);
            Variables.App.Iva = NumberUtils.NumberDouble(ConfigurationManager.AppSettings["Iva"]);

            Variables.App.estadisticas = Functions.ValorBool(ConfigurationManager.AppSettings["Estadisticas"]);

            Variables.App.tallasYColores = Functions.ValorBool(ConfigurationManager.AppSettings["TallasYColores"]);

            Variables.App.plantillaModulos = Functions.Valor(ConfigurationManager.AppSettings["PlantillaModulos"]);
            Variables.App.plantillaCorreo = Functions.Valor(ConfigurationManager.AppSettings["PlantillaCorreo"]);

            Variables.App.MobileMode = Web.RequestBool("movil");
            Variables.App.MobileMode = Web.RequestBool("mobile");

            string portal = Web.Request("portal");
            
            if (portal == "")
                portal = Functions.Valor(ConfigurationManager.AppSettings["DefaultPortal"]);

            Variables.App.portal = portal;
            Variables.App.directorioWeb = Variables.App.directorioPortal + "sitios/" + portal + "/";

            Variables.App.descripcionWeb = Functions.Valor(ConfigurationManager.AppSettings["DescripcionWeb"]);
            Variables.App.palabrasClave = Functions.Valor(ConfigurationManager.AppSettings["PalabrasClave"]);

            //id de la encuesta a mostrar 
            Variables.App.encuesta = Functions.Valor(ConfigurationManager.AppSettings["Encuesta"]);

            Variables.App.registrosPorPagina = NumberUtils.NumberInt(ConfigurationManager.AppSettings["RegistrosPorPagina"]);
            Variables.App.direccionFisica = Functions.Valor(ConfigurationManager.AppSettings["DireccionFisica"]);
            Variables.App.codigoPostal = Functions.Valor(ConfigurationManager.AppSettings["CodigoPostal"]);
            Variables.App.provincia = Functions.Valor(ConfigurationManager.AppSettings["Provincia"]);
            Variables.App.nombreWeb = Functions.Valor(ConfigurationManager.AppSettings["NombreWeb"]);

            //correo 
            Variables.App.correoInfo = Functions.Valor(ConfigurationManager.AppSettings["CorreoInfo"]);
            Variables.App.servidorCorreo = Functions.Valor(ConfigurationManager.AppSettings["ServidorCorreo"]);
            if (NumberUtils.NumberInt(ConfigurationManager.AppSettings["CorreoPuerto"]) != 0)
                Variables.App.correoPuerto = NumberUtils.NumberInt(ConfigurationManager.AppSettings["CorreoPuerto"]);
            Variables.App.correoActivarSSL = Functions.ValorBool(ConfigurationManager.AppSettings["CorreoActivarSSL"]);
            Variables.App.correoPrueba = Functions.Valor(ConfigurationManager.AppSettings["CorreoPrueba"]);
            Variables.App.correoCopia = Functions.Valor(ConfigurationManager.AppSettings["CorreoCopia"]);
            Variables.App.correoUsuario = Functions.Valor(ConfigurationManager.AppSettings["CorreoUsuario"]);
            Variables.App.correoPassword = Functions.Valor(ConfigurationManager.AppSettings["CorreoPassword"]);

            Variables.App.plantillaCorreo = Functions.Valor(ConfigurationManager.AppSettings["PlantillaCorreo"]);
            Variables.App.jqueryTema = Functions.Valor(ConfigurationManager.AppSettings["JqueryTema"]);
            Variables.App.multiCestas = Functions.ValorBool(ConfigurationManager.AppSettings["MultiCestas"]);


            Variables.App.uploadPath = Functions.Valor(ConfigurationManager.AppSettings["UploadPath"]);
            Variables.App.explorerPath = Functions.Valor(ConfigurationManager.AppSettings["ExplorerPath"]);
            Variables.App.strCookieName = Functions.Valor(ConfigurationManager.AppSettings["StrCookieName"]);

            //paypal 
            Variables.App.payPalURL = Functions.Valor(ConfigurationManager.AppSettings["PaypalURL"]);
            Variables.App.payPalEmail = Functions.Valor(ConfigurationManager.AppSettings["PaypalEmail"]);
            Variables.App.payPalNotifyURL = Functions.Valor(ConfigurationManager.AppSettings["PaypalNotifyURL"]);
            Variables.App.cuentaPago = Functions.Valor(ConfigurationManager.AppSettings["CuentaPago"]);

            //4b 
            Variables.App.codigoTienda4b = Functions.Valor(ConfigurationManager.AppSettings["CodigoTienda4B"]);
            Variables.App.url4b = Functions.Valor(ConfigurationManager.AppSettings["Url4B"]);

            //bbva 
            Variables.App.bbvaURL = Functions.Valor(ConfigurationManager.AppSettings["BbvaURL"]);
            Variables.App.bbvaComercio = Functions.Valor(ConfigurationManager.AppSettings["BbvaComercio"]);
            Variables.App.bbvaTerminal = Functions.Valor(ConfigurationManager.AppSettings["BbvaTerminal"]);
            Variables.App.bbvaPalClave = Functions.Valor(ConfigurationManager.AppSettings["BbvaPalClave"]);
            Variables.App.bbvaClaveXOR = Functions.Valor(ConfigurationManager.AppSettings["BbvaClaveXOR"]);

            //laCaixa 
            Variables.App.laCaixaNroComercio = Functions.Valor(ConfigurationManager.AppSettings["LaCaixaNroComercio"]);
            Variables.App.laCaixaClaveEnc = Functions.Valor(ConfigurationManager.AppSettings["LaCaixaClaveEnc"]);
            Variables.App.laCaixaTerminal = NumberUtils.NumberInt(ConfigurationManager.AppSettings["LaCaixaTerminal"]);
            Variables.App.laCaixaURLTpvVirtual = Functions.Valor(ConfigurationManager.AppSettings["LaCaixaURLTpvVirtual"]);

            Variables.App.idioma = Functions.Valor(ConfigurationManager.AppSettings["Idioma"]);

            Variables.App.disponibilidad = Functions.ValorBool(ConfigurationManager.AppSettings["Disponibilidad"]);

            Variables.App.htmlEditor = (Variables.HtmlEditorType)NumberUtils.NumberInt(ConfigurationManager.AppSettings["HtmlEditor"]);
            
            Variables.App.anchoPopUp = Functions.Valor(ConfigurationManager.AppSettings["AnchoPopUp"]);
            Variables.App.altoPopUp = Functions.Valor(ConfigurationManager.AppSettings["AltoPopUp"]);

            Variables.App.correoBienvenida = NumberUtils.NumberInt(ConfigurationManager.AppSettings["CorreoBienvenida"]);

            Variables.App.paginaRegistro = Functions.Valor(ConfigurationManager.AppSettings["PaginaRegistro"]);
            Variables.App.paginaPerfil = Functions.Valor(ConfigurationManager.AppSettings["PaginaPerfil"]);
            Variables.App.paginaLogin = Functions.Valor(ConfigurationManager.AppSettings["PaginaLogin"]);
            Variables.App.paginaRecordar = Functions.Valor(ConfigurationManager.AppSettings["BbvaComercio"]);

            Variables.App.paginaRegistro = Variables.App.directorioPortal + Variables.App.paginaRegistro;
            Variables.App.paginaPerfil = Variables.App.directorioPortal + Variables.App.paginaPerfil;
            Variables.App.paginaLogin = Variables.App.directorioPortal + Variables.App.paginaLogin;
            Variables.App.paginaRecordar = Variables.App.directorioPortal + Variables.App.paginaRecordar;
            
            
            //establecemos las variables para el envio de correos.
            SendMail.EnableSSL = Variables.App.correoActivarSSL;
			SendMail.Server = Variables.App.servidorCorreo;
			SendMail.User = Variables.App.correoUsuario;
			SendMail.Password = Variables.App.correoPassword;
			SendMail.Port = Variables.App.correoPuerto;

            //variable para indicar si se debe mostrar una página "aspx" no registrada en la tabla "Paginas"
            Variables.App.MostrarPaginasNoRegistradas = Functions.ValorBool(ConfigurationManager.AppSettings["MostrarPaginasNoRegistradas"]);


            if (!Variables.App.UseXML)
            {
                //BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
                //if (db.TableExists("Forum"))
                //{
                Variables.App.modoLite = false;
                //}
                //else
                //{
                //    Variables.App.modoLite = true;
                //}
            }
            else
                Variables.App.modoLite = true;

        }


        /// <summary>
        ///     Método para añadir un usuario al portal
        /// </summary>
        /// <param name="sUsuario"></param>
        /// <param name="sEMail"></param>
        /// <param name="sNombre"></param>
        /// <param name="sApellido1"></param>
        /// <param name="sApellido2"></param>
        /// <param name="sBirthMonth"></param>
        /// <param name="sBirthDay"></param>
        /// <param name="sBirthYear"></param>
        /// <param name="sNotifications"></param>
        /// <param name="sNewsletter"></param>
        /// <param name="sCountryCode"></param>
        /// <param name="sPass"></param>
        /// <param name="sProvincia"></param>
        /// <param name="sSexo"></param>
        /// <param name="iEdad"></param>
        /// <returns></returns>
        public void AddUser(string sUsuario, string sEMail, string sNombre, string sApellido1, string sApellido2,
            int sBirthMonth, int sBirthDay, int sBirthYear, bool sNotifications, bool sNewsletter, int sCountryCode, string sPass, int sProvincia, int sSexo, int iEdad)
        {
            //Modulos modulos = new Modulos();

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            string sSql = "SELECT usuario FROM " + Variables.App.prefijoTablas + "Usuarios WHERE Usuario = '" + sUsuario + "'";

            DataTable dt = db.Execute(sSql);

            if (dt.Rows.Count > 0)
            {
                if (TextUtil.IndexOf(sUsuario, "@") > 0)
                {
                    throw new ExceptionUtil(
                        "Ya existe un usuario con dicha dirección de correo. Utiliza una dirección de correo diferente.",
						ExceptionUtil.ExceptionType.Information);
                }
				throw new ExceptionUtil(FuncionesWeb.Idioma(215, false), ExceptionUtil.ExceptionType.Information);
            }

            if (TextUtil.IndexOf(sUsuario, "@") > 0)
                sEMail = sUsuario;

            if (!TextUtil.IsEmail(sEMail))
            {
				throw new ExceptionUtil(FuncionesWeb.Idioma(216, false), ExceptionUtil.ExceptionType.Information);
            }

            if (sUsuario == "")
            {
				throw new ExceptionUtil(FuncionesWeb.Idioma(218, false), ExceptionUtil.ExceptionType.Information);
            }


            Random r = new Random();
            string sPassword = "";


            if (sPass != "")
            {
                sPassword = sPass;
            }
            else
            {
                for (int iTemp = 1; iTemp <= 10; iTemp++)
                {
                    int iChar = NumberUtils.NumberInt(97 + (r.Next(25)));
                    sPassword = sPassword + Convert.ToChar(iChar);
                }
            }


            string[] sArrFields = new string[20];
            string[] sArrData = new string[20];
            Type[] sArrType = new Type[20];

            sArrType[0] = typeof (string);
            sArrType[1] = typeof (string);
            sArrType[2] = typeof (string);
            sArrType[3] = typeof (string);
            sArrType[4] = typeof (string);
            sArrType[5] = typeof (string);
            sArrType[6] = typeof (int);
			sArrType[7] = typeof (System.DateTime);
            sArrType[8] = typeof (bool);
            sArrType[9] = typeof (bool);
            sArrType[10] = typeof (int);
			sArrType[11] = typeof (System.DateTime);
            sArrType[12] = typeof (int);
            sArrType[13] = typeof (bool);
            sArrType[14] = typeof (int);
            sArrType[15] = typeof (bool);
            sArrType[16] = typeof (string);
            sArrType[17] = typeof (int);
            sArrType[18] = typeof (int);
            sArrType[19] = typeof (string);

            sArrFields[0] = "usuario";
            sArrFields[1] = "clave";
            sArrFields[2] = "email";
            sArrFields[3] = "nombre";
            sArrFields[4] = "apellido1";
            sArrFields[5] = "apellido2";
            sArrFields[6] = "sexo";
            sArrFields[7] = "fechaNacimiento";
            sArrFields[8] = "envioNotificaciones";
            sArrFields[9] = "envioCorreos";
            sArrFields[10] = "Pais";
            sArrFields[11] = "FechaCreacion";
            sArrFields[12] = "dto";
            sArrFields[13] = "active";
            sArrFields[14] = "Grupo";
            //sArrFields[15] = "rich_editor";
            //sArrFields[16] = "User_code";
            sArrFields[15] = "provincia";
            sArrFields[16] = "edad";
            sArrFields[17] = "ip";

			Crypto crypt = new Crypto ();

            sArrData[0] = sUsuario;
            sArrData[1] = crypt.Md5(sPassword.ToLower());
            sArrData[2] = sEMail;
            sArrData[3] = sNombre;
            sArrData[4] = sApellido1;
            sArrData[5] = sApellido2;
            sArrData[6] = sSexo.ToString();
            System.DateTime dBirthdate = new System.DateTime(sBirthYear, sBirthMonth, sBirthDay );
            sArrData[7] = FSLibrary.DateTimeUtil.ShortDate(dBirthdate);
            if (sNotifications)
            {
                sArrData[8] = Functions.Valor(true);
            }
            else
            {
                sArrData[8] = Functions.Valor(false);
            }
            if (sNewsletter)
            {
                sArrData[9] = Functions.Valor(true);
            }
            else
            {
                sArrData[9] = Functions.Valor(false);
            }
            sArrData[10] = sCountryCode.ToString();
			sArrData[11] = FSLibrary.DateTimeUtil.ShortDate(System.DateTime.Now);
            sArrData[12] = Functions.Valor(5);

            //por defecto el usuario esta desactivado hasta que por correo electrónico no lo active.
            sArrData[13] = Functions.Valor(false);
            sArrData[14] = Functions.Valor(Variables.App.invitado);
            //sArrData[15] = Functions.Valor(true);
            //string strUserCode = Functions.UserCode(sUsuario);
            //sArrData[16] = strUserCode;

            sArrData[15] = sProvincia.ToString();
            sArrData[16] = iEdad.ToString();
            sArrData[17] = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            db.ExecuteNonQuery(db.InsertSql(Variables.App.prefijoTablas + "usuarios", sArrFields, sArrData, sArrType, Variables.User.UsuarioId));

            string sSubject = Ui.TituloPagina(Variables.App.correoBienvenida);
            string sBody = Ui.MuestraPagina(Variables.App.correoBienvenida, false);
            sBody = TextUtil.Replace(sBody, "{usuarioRegistrado}", sUsuario);
            sBody = TextUtil.Replace(sBody, "{usuarioPassword}", sPassword);
            sBody = TextUtil.Replace(sBody, "{direccionActivar}",
				Variables.App.webHttp + "/servicios/activarUsuario.aspx?u=" + HttpUtility.HtmlEncode(crypt.Md5(sUsuario)));

            new SendMail().SendMailAsync(sEMail,Variables.App.correoPrueba,Variables.App.correoCopia, sSubject, sBody, Variables.App.correoInfo, FuncionesWeb.Idioma(226, false), Variables.App.plantillaCorreo);

            sSubject = FuncionesWeb.Idioma(223, false);
            sBody = FuncionesWeb.Idioma(224, false) + "\r\n" + "\r\n";
            sBody = sBody + FuncionesWeb.Idioma(182, false) + ": " + sUsuario + "\r\n";
            sBody = sBody + FuncionesWeb.Idioma(225, false) + ": " + sNombre + " " + sApellido1 + " " + sApellido2 + "\r\n";
            sBody = sBody + FuncionesWeb.Idioma(181, false) + ": " + sEMail + "\r\n";

            new SendMail().SendMailAsync(Variables.App.correoInfo,Variables.App.correoPrueba,Variables.App.correoCopia, sSubject, sBody, Variables.App.correoInfo, FuncionesWeb.Idioma(226, false), Variables.App.plantillaCorreo);
        }

        /// <summary>
        ///     Método para añadir un usuario al portal
        /// </summary>
        /// <param name="sUsuario"></param>
        /// <param name="sEMail"></param>
        /// <param name="sNombre"></param>
        /// <param name="sApellido1"></param>
        /// <param name="sApellido2"></param>
        /// <param name="sBirthMonth"></param>
        /// <param name="sBirthDay"></param>
        /// <param name="sBirthYear"></param>
        /// <param name="sNotifications"></param>
        /// <param name="sNewsletter"></param>
        /// <param name="sCountryCode"></param>
        /// <param name="sRemember"></param>
        /// <returns></returns>
        public void AddUser(string sUsuario, string sEMail, string sNombre, string sApellido1, string sApellido2,
            int sBirthMonth, int sBirthDay, int sBirthYear, bool sNotifications, bool sNewsletter, int sCountryCode,
            bool sRemember)
        {
            AddUser(sUsuario, sEMail, sNombre, sApellido1, sApellido2, sBirthMonth, sBirthDay, sBirthYear,
                sNotifications, sNewsletter, sCountryCode, "");
        }

        /// <summary>
        ///     Método para añadir un usuario al portal
        /// </summary>
        /// <param name="sUsuario"></param>
        /// <param name="sEMail"></param>
        /// <param name="sNombre"></param>
        /// <param name="sApellido1"></param>
        /// <param name="sApellido2"></param>
        /// <param name="sBirthMonth"></param>
        /// <param name="sBirthDay"></param>
        /// <param name="sBirthYear"></param>
        /// <param name="sNotifications"></param>
        /// <param name="sNewsletter"></param>
        /// <param name="sCountryCode"></param>
        /// <param name="sPass"></param>
        /// <returns></returns>
        public void AddUser(string sUsuario, string sEMail, string sNombre, string sApellido1, string sApellido2,
            int sBirthMonth, int sBirthDay, int sBirthYear, bool sNotifications, bool sNewsletter, int sCountryCode, string sPass)
        {
            AddUser(sUsuario, sEMail, sNombre, sApellido1, sApellido2, sBirthMonth, sBirthDay, sBirthYear,
                sNotifications, sNewsletter, sCountryCode, sPass, 0);
        }

        /// <summary>
        ///     Método para añadir un usuario al portal
        /// </summary>
        /// <param name="sUsuario"></param>
        /// <param name="sEMail"></param>
        /// <param name="sNombre"></param>
        /// <param name="sApellido1"></param>
        /// <param name="sApellido2"></param>
        /// <param name="sBirthMonth"></param>
        /// <param name="sBirthDay"></param>
        /// <param name="sBirthYear"></param>
        /// <param name="sNotifications"></param>
        /// <param name="sNewsletter"></param>
        /// <param name="sCountryCode"></param>
        /// <param name="sPass"></param>
        /// <param name="sProvincia"></param>
        /// <returns></returns>
        private void AddUser(string sUsuario, string sEMail, string sNombre, string sApellido1, string sApellido2,
            int sBirthMonth, int sBirthDay, int sBirthYear, bool sNotifications, bool sNewsletter, int sCountryCode, string sPass, int sProvincia)
        {
            AddUser(sUsuario, sEMail, sNombre, sApellido1, sApellido2, sBirthMonth, sBirthDay, sBirthYear,
                sNotifications, sNewsletter, sCountryCode, sPass, sProvincia, 0);
        }

        /// <summary>
        ///     Método para añadir un usuario al portal
        /// </summary>
        /// <param name="sUsuario"></param>
        /// <param name="sEMail"></param>
        /// <param name="sNombre"></param>
        /// <param name="sApellido1"></param>
        /// <param name="sApellido2"></param>
        /// <param name="sBirthMonth"></param>
        /// <param name="sBirthDay"></param>
        /// <param name="sBirthYear"></param>
        /// <param name="sNotifications"></param>
        /// <param name="sNewsletter"></param>
        /// <param name="sCountryCode"></param>
        /// <param name="sPass"></param>
        /// <param name="sProvincia"></param>
        /// <param name="sSexo"></param>
        /// <returns></returns>
        private void AddUser(string sUsuario, string sEMail, string sNombre, string sApellido1, string sApellido2,
            int sBirthMonth, int sBirthDay, int sBirthYear, bool sNotifications, bool sNewsletter, int sCountryCode, string sPass, int sProvincia, int sSexo)
        {
            AddUser(sUsuario, sEMail, sNombre, sApellido1, sApellido2, sBirthMonth, sBirthDay, sBirthYear,
                sNotifications, sNewsletter, sCountryCode, sPass, sProvincia, sSexo, 0);
        }

        /// <summary>
        ///     Método para editar el perfíl de un usuario.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="sUsuario"></param>
        /// <param name="sPassword"></param>
        /// <param name="sPasswordRe"></param>
        /// <param name="sEMail"></param>
        /// <param name="sNombre"></param>
        /// <param name="sApellido1"></param>
        /// <param name="sApellido2"></param>
        /// <param name="sDireccion"></param>
        /// <param name="sPortal"></param>
        /// <param name="sPiso"></param>
        /// <param name="sPoblacion"></param>
        /// <param name="sCodigoPostal"></param>
        /// <param name="sDni"></param>
        /// <param name="sTelefono1"></param>
        /// <param name="sTelefono2"></param>
        /// <param name="sPaginaPrincipal"></param>
        /// <param name="sAim"></param>
        /// <param name="sYahoo"></param>
        /// <param name="sMsn"></param>
        /// <param name="sIcq"></param>
        /// <param name="sOcupacion"></param>
        /// <param name="sInteres"></param>
        /// <param name="sNotificarRespMensajes"></param>
        /// <param name="sMostrarEmail"></param>
        /// <param name="iBirthMonth"></param>
        /// <param name="sBirthDay"></param>
        /// <param name="sBirthYear"></param>
        /// <param name="sNotifications"></param>
        /// <param name="sNewsletter"></param>
        /// <param name="sCountryCode"></param>
        /// <param name="iProvincia"></param>
        /// <param name="sexo"></param>
        /// <param name="sNotificarCorreo"></param>
        /// <param name="sNoRecibirCorreo"></param>
        /// <param name="sEdad"></param>
        /// <returns></returns>
        public void EditUser(int usuarioId, string sUsuario, string sPassword, string sPasswordRe, string sEMail,
            string sNombre, string sApellido1, string sApellido2, string sDireccion, string sPortal, string sPiso,
            string sPoblacion, string sCodigoPostal, string sDni, string sTelefono1, string sTelefono2,
            string sPaginaPrincipal, string sAim, string sYahoo, string sMsn, string sIcq, string sOcupacion,
            string sInteres, bool sNotificarRespMensajes, bool sMostrarEmail, int iBirthMonth, int sBirthDay,
            int sBirthYear, bool sNotifications, bool sNewsletter, int sCountryCode, int iProvincia,
            int sexo, bool sNotificarCorreo, bool sNoRecibirCorreo, string sEdad)
        {
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
			System.DateTime dBirthDate = new System.DateTime();

            string ssql = "SELECT usuarioID FROM " + Variables.App.prefijoTablas + "Usuarios WHERE UsuarioID = " + usuarioId.ToString();
            DataTable dt = db.Execute(ssql);

            if (sUsuario == "")
            {
				throw new ExceptionUtil(FuncionesWeb.Idioma(218, false), ExceptionUtil.ExceptionType.Information);
            }

            if (TextUtil.IndexOf(sUsuario, "@") > 0)
                sEMail = sUsuario;

            if (!TextUtil.IsEmail(sEMail))
            {
				throw new ExceptionUtil(FuncionesWeb.Idioma(216, false), ExceptionUtil.ExceptionType.Information);
            }

            if (sNombre == "")
            {
				throw new ExceptionUtil(FuncionesWeb.Idioma(214, false), ExceptionUtil.ExceptionType.Information);
            }

            //if (sApellido1 == "")
            //{
            //    throw new FSException(FuncWeb.Idioma(278, false), FSException.ExceptionType.Information);
            //}

            if (sPassword == "")
            {
				throw new ExceptionUtil(FuncionesWeb.Idioma(279, false), ExceptionUtil.ExceptionType.Information);
            }

            if (sPasswordRe != "")
            {
                if (sPassword != sPasswordRe)
                {
					throw new ExceptionUtil(FuncionesWeb.Idioma(280, false), ExceptionUtil.ExceptionType.Information);
                }
            }


            string[] sArrFields = new string[32];
            string[] sArrData = new string[32];
            Type[] sArrType = new Type[32];

            sArrType[0] = typeof (string);
            sArrType[1] = typeof (string);
            sArrType[2] = typeof (string);
            sArrType[3] = typeof (string);
            sArrType[4] = typeof (string);
            sArrType[5] = typeof (string);
            sArrType[6] = typeof (int);
			sArrType[7] = typeof (System.DateTime);
            sArrType[8] = typeof (bool);
            sArrType[9] = typeof (bool);
            sArrType[10] = typeof (bool);
            sArrType[11] = typeof (bool);
            sArrType[12] = typeof (int);
            sArrType[13] = typeof (string);
            sArrType[14] = typeof (string);
            sArrType[15] = typeof (string);
            sArrType[16] = typeof (string);
            sArrType[17] = typeof (int);
            sArrType[18] = typeof (string);
            sArrType[19] = typeof (string);
            sArrType[20] = typeof (string);
            sArrType[21] = typeof (string);
            sArrType[22] = typeof (string);
            sArrType[23] = typeof (string);
            sArrType[24] = typeof (string);
            sArrType[25] = typeof (string);
            sArrType[26] = typeof (string);
            sArrType[27] = typeof (string);
            sArrType[28] = typeof (string);
            sArrType[29] = typeof (bool);
            sArrType[30] = typeof (bool);
            sArrType[31] = typeof (int);

            sArrFields[0] = "usuario";
            sArrFields[1] = "clave";
            sArrFields[2] = "email";
            sArrFields[3] = "nombre";
            sArrFields[4] = "apellido1";
            sArrFields[5] = "apellido2";
            sArrFields[6] = "sexo";
            sArrFields[7] = "fechaNacimiento";
            sArrFields[8] = "envioNotificaciones";
            sArrFields[9] = "envioCorreos";
            sArrFields[10] = "notificarCorreo";
            sArrFields[11] = "NoRecibirCorreo";
            sArrFields[12] = "Pais";
            sArrFields[13] = "domicilio";
            sArrFields[14] = "portal";
            sArrFields[15] = "piso";
            sArrFields[16] = "Poblacion";
            sArrFields[17] = "Provincia";
            sArrFields[18] = "CodigoPostal";
            sArrFields[19] = "DNI";
            sArrFields[20] = "Telefono1";
            sArrFields[21] = "Telefono2";
            sArrFields[22] = "homepage";
            sArrFields[23] = "AIM";
            sArrFields[24] = "Yahoo";
            sArrFields[25] = "MSN";
            sArrFields[26] = "ICQ";
            sArrFields[27] = "occupation";
            sArrFields[28] = "Interests";
            sArrFields[29] = "show_email";
            sArrFields[30] = "reply_notify";
            sArrFields[31] = "edad";

			Crypto crypt = new Crypto ();

            sArrData[0] = sUsuario;
            if (sPassword.Length > 15)
            {
                sArrData[1] = sPassword;
            }
            else
            {
				sArrData[1] = crypt.Md5(sPassword);
            }
            sArrData[2] = sEMail;
            sArrData[3] = sNombre;
            sArrData[4] = sApellido1;
            sArrData[5] = sApellido2;

            sArrData[6] = sexo.ToString();

            if ((sBirthYear > 0) & iBirthMonth > 0 & (sBirthDay > 0))
            {
				dBirthDate = new System.DateTime(sBirthYear, iBirthMonth, sBirthDay);
            }
            if (FSLibrary.DateTimeUtil.IsDate(FSLibrary.DateTimeUtil.ShortDate(dBirthDate)))
            {
                sArrData[7] = FSLibrary.DateTimeUtil.ShortDate(dBirthDate);
            }
            if (Functions.Valor(sNotifications) != "")
            {
                sArrData[8] = Functions.Valor(true);
            }
            else
            {
                sArrData[8] = Functions.Valor(false);
            }
            if (Functions.Valor(sNewsletter) != "")
            {
                sArrData[9] = Functions.Valor(true);
            }
            else
            {
                sArrData[9] = Functions.Valor(false);
            }
            if (Functions.Valor(sNotificarCorreo) != "")
            {
                sArrData[10] = Functions.Valor(true);
            }
            else
            {
                sArrData[10] = Functions.Valor(false);
            }
            if (Functions.Valor(sNoRecibirCorreo) != "")
            {
                sArrData[11] = Functions.Valor(true);
            }
            else
            {
                sArrData[11] = Functions.Valor(false);
            }

            sArrData[12] = sCountryCode.ToString();
            sArrData[13] = sDireccion;
            sArrData[14] = sPortal;
            sArrData[15] = sPiso;
            sArrData[16] = sPoblacion;
            sArrData[17] = iProvincia.ToString();
            sArrData[18] = sCodigoPostal;
            sArrData[19] = sDni;
            sArrData[20] = sTelefono1;
            sArrData[21] = sTelefono2;

            sArrData[22] = sPaginaPrincipal;
            sArrData[23] = sAim;
            sArrData[24] = sYahoo;
            sArrData[25] = sMsn;
            sArrData[26] = sIcq;
            sArrData[27] = sOcupacion;
            sArrData[28] = sInteres;
            if (Functions.Valor(sMostrarEmail) != "")
            {
                sArrData[29] = Functions.Valor(true);
            }
            else
            {
                sArrData[29] = Functions.Valor(false);
            }
            if (Functions.Valor(sNotificarRespMensajes) != "")
            {
                sArrData[30] = Functions.Valor(true);
            }
            else
            {
                sArrData[30] = Functions.Valor(false);
            }

            sArrData[31] = sEdad;

            db.ExecuteNonQuery(db.UpdateSql("usuarios", sArrFields, sArrData, sArrType,
                "UsuarioID=" + dt.Rows[0]["usuarioID"], Variables.User.UsuarioId));


            string sSubject = FuncionesWeb.Idioma(281);
            string sBody = FuncionesWeb.Idioma(282) + "\r\n" + "\r\n";
            sBody = sBody + FuncionesWeb.Idioma(283) + "servicios/perfil.aspx" + "\r\n";

            new SendMail().SendMailAsync(sEMail,Variables.App.correoPrueba,Variables.App.correoCopia, sSubject, sBody, Variables.App.correoInfo, FuncionesWeb.Idioma(285, false) + ":" + Variables.App.nombreWeb, Variables.App.plantillaCorreo);
        }

        /// <summary>
        ///     Método para recordar el password de un usuario.
        /// </summary>
        /// <param name="sEmail"></param>
        /// <returns></returns>
        public void RecordarPassword(string sEmail)
        {
            string sPassword = "";

            if (sEmail.Length < 6)
            {
				throw new ExceptionUtil(FuncionesWeb.Idioma(216, false), ExceptionUtil.ExceptionType.Information);
            }

            if ((sEmail.IndexOf("@") + 1 == 0) | (sEmail.IndexOf(".") + 1 == 0))
            {
				throw new ExceptionUtil(FuncionesWeb.Idioma(228, false), ExceptionUtil.ExceptionType.Information);
            }
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            string sSql = "SELECT usuario FROM " + Variables.App.prefijoTablas + "Usuarios WHERE eMail = '" + sEmail + "'";
            DataTable dt = db.Execute(sSql);

            if (dt.Rows.Count == 0)
            {
				throw new ExceptionUtil(FuncionesWeb.Idioma(229, false), ExceptionUtil.ExceptionType.Information);
            }
            Random r = new Random();
            
            for (int iTemp = 1; iTemp <= 10; iTemp++)
            {
                int iChar = NumberUtils.NumberInt(97 + r.Next(25));
                sPassword = sPassword + Convert.ToChar(iChar);
            }
            string sUsuario = Functions.Valor(dt.Rows[0]["Usuario"]);

			Crypto crypt = new Crypto ();

			sSql = "UPDATE " + Variables.App.prefijoTablas + "usuarios SET clave='" + crypt.Md5(sPassword) + "' WHERE usuario='" +
                   sUsuario + "'";
            db.ExecuteNonQuery(sSql);


            string sSubject = FuncionesWeb.Idioma(230) + ": " + Variables.App.nombreWeb;
            string sBody = FuncionesWeb.Idioma(231) + "\r\n" + "\r\n";
            sBody = sBody + FuncionesWeb.Idioma(232) + "\r\n";
            sBody = sBody + FuncionesWeb.Idioma(233) + "\r\n" + "\r\n";
            sBody = sBody + FuncionesWeb.Idioma(234) + ": " + sUsuario + "\r\n";
            sBody = sBody + FuncionesWeb.Idioma(235) + ": " + sPassword + "\r\n" + "\r\n";
            sBody = sBody + FuncionesWeb.Idioma(236) + "\r\n";

            new SendMail().SendMailAsync(sEmail,Variables.App.correoPrueba,Variables.App.correoCopia, sSubject, sBody, Variables.App.correoInfo, FuncionesWeb.Idioma(237, false), Variables.App.plantillaCorreo);
        }

        /// <summary>
        ///     Limpiamos las variables y cache del portal
        /// </summary>
        public void Limpiar()
        {
            //realizamos una desconexión normal
            Desconectar();

            //limpiamos variables sesión
            FuncionesWeb.ClearVariables();
            
            //limpiamos las variasbles de usuario
            Parser.IniciarFormulario();

            //forzamos la lectura de las variables static
            Variables.App.paginaLogin = "";
        }


        /// <summary>
        ///     Desconexión del portal
        /// </summary>
        public void Desconectar()
        {
            if (Variables.App.Page == null) return;

            string usuarioId = Functions.Valor(Variables.User.UsuarioId);

            if (usuarioId == "")
            {
                BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
                usuarioId = "invitado-" + Variables.App.Page.Session.SessionID;

                string ssql = "select UsuarioID from " + Variables.App.prefijoTablas + "usuarios where usuario='" + usuarioId + "'";
                DataTable dtCli = db.Execute(ssql);
                if (dtCli.Rows.Count > 0)
                {
                    usuarioId = Functions.Valor(dtCli.Rows[0]["UsuarioID"]);

                    //borramos en cascada la cabecera y las lineas
                    ssql = "delete from " + Variables.App.prefijoTablas + "cabeceraCesta where idCliente=" + usuarioId;
                    db.Execute(ssql);

                    ssql = "delete from " + Variables.App.prefijoTablas + "usuarios where usuarioID=" + usuarioId;
                    db.Execute(ssql);
                }
            }

            FuncionesWeb.ClearVariables();
        }
    }
}