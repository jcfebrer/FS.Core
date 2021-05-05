// <fileheader>
// <copyright file="perfil.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: servicios\perfil.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using System.Text;
using FSPortal;
using FSLibrary;
using FSQueryBuilder;
using FSQueryBuilder.Enums;
using FSQueryBuilder.QueryParts.Where;
using FSDatabase;
using FSNetwork;

namespace FSPaginas.Servicios
{
    public class Perfil : BasePage
    {
        public bool bErr;
        public bool bMostrarEmail;

        public bool bNewsletter,
            bNoRecibirCorreo,
            bNotificarCorreo;

        public bool bNotificarRespMensajes,
            bNotifications;

        public bool bRemember;
        public System.DateTime dBirthDate;
        public int iBirthMonth;
        public int iTemp;

        public string sAim;

        public string sApellido1, sApellido2;
        public int sBirthDay, sBirthYear;

        public string sCodigoPostal;

        public int sCountryCode;

        public string sDni;

        public string sDireccion;
        public string sEMail;
        public string sGenderFemale;
        public string sGenderMale;

        public string sIcq;
        public string sInteres;
        public string sMsn;

        public string sMostrarEmail;

        public string sNewsletter;

        public string sNoEnviarCorreo;

        public string sNoRecibirCorreo;

        public string sNombre;

        public string sNotificarCorreo;

        public string sNotificarRespMensajes,
            sNotifications;

        public string sOcupacion;
        public string sPaginaPrincipal;
        public string sPassword;

        public string sPasswordRe;

        public string sPiso,
            sPoblacion;

        public string sPortal;
        public int sProvincia;
        public string sRemember;

        public string sSmallerFonts;

        public string sTelefono1,
            sTelefono2;

        public string sUsuario;
        public string sYahoo;

        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            StringBuilder sb = new StringBuilder("");
            Portal portal = new Portal();
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            if (Functions.Valor(Variables.User.Usuario) == "")
            {
                Response.Redirect(Variables.App.paginaLogin + (Variables.App.paginaLogin.IndexOf("?") > 0 ? "&" : "?") + "comebackto=" +
                                  Request.ServerVariables["SCRIPT_NAME"] + "?" +
                                  Server.UrlEncode(Request.QueryString.ToString()), false);
            	Context.ApplicationInstance.CompleteRequest();
            }

            if (Web.Request("cmdsave") != "")
            {
                sUsuario = Web.Request("txtUsuario");
                sPassword = Web.Request("txtPassword");
                sPasswordRe = Web.Request("txtPasswordRe");
                sEMail = Web.Request("txtEmail");
                sNombre = Web.Request("txtNombre");
                sApellido1 = Web.Request("txtApellido1");
                sApellido2 = Web.Request("txtApellido2");
                if (Web.Request("optGender") == "1")
                {
                    sGenderMale = " checked";
                }
                else
                {
                    sGenderFemale = " checked";
                }
                iBirthMonth = Web.RequestInt("txtBirthMonth");
                sBirthDay = Web.RequestInt("txtBirthDay");
                sBirthYear = Web.RequestInt("txtBirthYear");
                sDireccion = Web.Request("txtDireccion");
                sPortal = Web.Request("txtPortal");
                sPiso = Web.Request("txtPiso");
                sPoblacion = Web.Request("txtPoblacion");
                sProvincia = Web.RequestInt("txtProvincia");
                sCodigoPostal = Web.Request("txtCodigoPostal");
                sDni = Web.Request("txtDNI");
                sTelefono1 = Web.Request("txtTelefono1");
                sTelefono2 = Web.Request("txtTelefono2");

                sPaginaPrincipal = Web.Request("txtPaginaPrincipal");
                sAim = Web.Request("txtAIM");
                sYahoo = Web.Request("txtYahoo");
                sMsn = Web.Request("txtMSN");
                sIcq = Web.Request("txtICQ");
                sOcupacion = Web.Request("txtOcupacion");
                sInteres = Web.Request("txtInteres");

                if (Web.Request("chkMostrarEmail") != "")
                {
                    sMostrarEmail = @" checked=""checked""";
                    bMostrarEmail = true;
                }
                else bMostrarEmail = false;
                if (Web.Request("chkNotificarRespMensajes") != "")
                {
                    sNotificarRespMensajes = @" checked=""checked""";
                    bNotificarRespMensajes = true;
                }
                else bNotificarRespMensajes = false;
                if (Web.Request("chkNotifications") != "")
                {
                    sNotifications = @" checked=""checked""";
                    bNotifications = true;
                }
                else bNotifications = false;
                if (Web.Request("chkNewsletter") != "")
                {
                    sNewsletter = @" checked=""checked""";
                    bNewsletter = true;
                }
                else bNewsletter = false;
                if (Web.Request("chkNoRecibirCorreo") != "")
                {
                    sNoRecibirCorreo = @" checked=""checked""";
                    bNoRecibirCorreo = true;
                }
                else bNoRecibirCorreo = false;
                if (Web.Request("chkNotificarCorreo") != "")
                {
                    sNotificarCorreo = @" checked=""checked""";
                    bNotificarCorreo = true;
                }
                else bNotificarCorreo = false;

                sCountryCode = Web.RequestInt("txtCountryCode");
                if (Web.Cookie(Request.Cookies[Variables.App.strCookieName], "Usuario") != "")
                {
                    sRemember = @" checked=""checked""";
                    bRemember = true;
                }
                else bRemember = false;

                portal.EditUser(Variables.User.UsuarioId, sUsuario, sPassword, sPasswordRe, sEMail, sNombre, sApellido1,
                    sApellido2, sDireccion, sPortal, sPiso, sPoblacion, sCodigoPostal, sDni, sTelefono1, sTelefono2,
                    sPaginaPrincipal, sAim, sYahoo, sMsn, sIcq, sOcupacion, sInteres, bNotificarRespMensajes,
                    bMostrarEmail, iBirthMonth, sBirthDay, sBirthYear, bNotifications, bNewsletter, sCountryCode, NumberUtils.NumberInt(sProvincia), ((sGenderMale != "") ? 1 : 0), bNotificarCorreo,
                    Web.RequestBool("chkNoRecibirCorreo"), "0");

                sb.Append(ShowProfile(""));
            }
            else
            {
                SelectQueryBuilder sqB = new SelectQueryBuilder();
                sqB.Columns.SelectColumns("*");
                sqB.TableSource = Variables.App.prefijoTablas + "Usuarios";

                if (Web.Request("idCliente") != "" & Functions.ValorBool(Variables.User.Administrador))
                {
                    sqB.Where = new SimpleWhere(sqB.TableSource, "usuarioID", Comparison.Equals,
                        Web.Request("idCliente"));
                }
                else
                {
                    sqB.Where = new SimpleWhere(sqB.TableSource, "Usuario", Comparison.Equals,
                        Functions.Valor(Variables.User.Usuario));
                }

                DataTable dt = db.Execute(sqB.BuildQuery());

                sUsuario = dt.Rows[0]["Usuario"].ToString();
                sPassword = dt.Rows[0]["Clave"].ToString();
                sEMail = dt.Rows[0]["Email"].ToString();
                sNombre = dt.Rows[0]["Nombre"].ToString();
                sApellido1 = dt.Rows[0]["Apellido1"].ToString();
                sApellido2 = dt.Rows[0]["Apellido2"].ToString();
                if (dt.Rows[0]["sexo"].ToString() == "1")
                {
                    sGenderMale = @"checked=""checked""";
                }
                else
                {
                    sGenderFemale = @"checked=""checked""";
                }
                dBirthDate = FSLibrary.DateTimeUtil.ValorFecha(dt.Rows[0]["FechaNacimiento"].ToString(), new System.DateTime(2000, 1, 1));
                if (FSLibrary.DateTimeUtil.IsDate(FSLibrary.DateTimeUtil.ShortDate(dBirthDate)))
                {
                    iBirthMonth = System.DateTime.Parse(dBirthDate.ToString()).Month;
					sBirthDay = System.DateTime.Parse(dBirthDate.ToString()).Day;
					sBirthYear = System.DateTime.Parse(dBirthDate.ToString()).Year;
                }
                if (Convert.ToBoolean(dt.Rows[0]["envioNotificaciones"]))
                {
                    sNotifications = @"checked=""checked""";
                    bNotifications = true;
                }
                else bNotifications = false;
                if (Convert.ToBoolean(dt.Rows[0]["envioCorreos"]))
                {
                    sNewsletter = @"checked=""checked""";
                    bNewsletter = true;
                }
                else bNewsletter = false;
                sCountryCode = NumberUtils.NumberInt(dt.Rows[0]["Pais"]);
                sDireccion = dt.Rows[0]["domicilio"].ToString();
                sPortal = dt.Rows[0]["Portal"].ToString();
                sPiso = dt.Rows[0]["Piso"].ToString();
                sPoblacion = dt.Rows[0]["Poblacion"].ToString();
                sProvincia = NumberUtils.NumberInt(dt.Rows[0]["Provincia"]);
                sCodigoPostal = dt.Rows[0]["CodigoPostal"].ToString();
                sDni = dt.Rows[0]["DNI"].ToString();
                sTelefono1 = dt.Rows[0]["Telefono1"].ToString();
                sTelefono2 = dt.Rows[0]["Telefono2"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["NoRecibirCorreo"]))
                {
                    sNoEnviarCorreo = @" checked=""checked""";
                }
                if (Convert.ToBoolean(dt.Rows[0]["NotificarCorreo"]))
                {
                    sNotificarCorreo = @"checked=""checked""";
                }

                sPaginaPrincipal = dt.Rows[0]["homepage"].ToString();
                sAim = dt.Rows[0]["AIM"].ToString();
                sYahoo = dt.Rows[0]["Yahoo"].ToString();
                sMsn = dt.Rows[0]["MSN"].ToString();
                sIcq = dt.Rows[0]["ICQ"].ToString();
                sOcupacion = dt.Rows[0]["occupation"].ToString();
                sInteres = dt.Rows[0]["Interests"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["show_email"]))
                {
                    sMostrarEmail = @"checked=""checked""";
                }
                if (Convert.ToBoolean(dt.Rows[0]["reply_notify"]))
                {
                    sNotificarRespMensajes = @"checked=""checked""";
                }

                if (Web.Cookie(Request.Cookies[Variables.App.strCookieName], "Usuario") != "")
                {
                    sRemember = @"checked=""checked""";
                }

                if (Web.Request("devSmallerFonts") != "")
                {
                    sSmallerFonts = @"checked=""checked""";
                }

                sb.Append(ShowProfile(""));
            }

            return sb.ToString();
        }


        public string ShowProfile(string sErr)
        {
            Modulos modulos = new Modulos();
            StringBuilder sb = new StringBuilder("");
            string[] arrMonths = new string[13];
            arrMonths[1] = FuncionesWeb.Idioma(191);
            arrMonths[2] = FuncionesWeb.Idioma(192);
            arrMonths[3] = FuncionesWeb.Idioma(193);
            arrMonths[4] = FuncionesWeb.Idioma(194);
            arrMonths[5] = FuncionesWeb.Idioma(195);
            arrMonths[6] = FuncionesWeb.Idioma(196);
            arrMonths[7] = FuncionesWeb.Idioma(197);
            arrMonths[8] = FuncionesWeb.Idioma(198);
            arrMonths[9] = FuncionesWeb.Idioma(199);
            arrMonths[10] = FuncionesWeb.Idioma(200);
            arrMonths[11] = FuncionesWeb.Idioma(201);
            arrMonths[12] = FuncionesWeb.Idioma(202);

            sb.Append("\r\n" + Ui.Lf());
            sb.Append("\r\n" + "<p class='accionpeque'>" + FuncionesWeb.Idioma(245) + "</p>");
            sb.Append("\r\n" + "<p class='error'>");
            sb.Append("\r\n" + sErr);
            sb.Append("\r\n" + "</p>");
            sb.Append("\r\n" + "<form action='perfil.aspx' method='post'>");
            sb.Append("\r\n" + @"<table border=""0"">");
            sb.Append("\r\n" + "<tr>");

            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(182) + "</td>");
            sb.Append("\r\n" +
                      @"<td><input type='text' class='textboxplano' name='txtUsuario' readonly=""readonly"" value='" +
                      sUsuario + "' /></td>");
            sb.Append("\r\n" + "<td class='textomaspeque'></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");

            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(222) + "</td>");
            sb.Append("\r\n" + "<td><input type='password' class='textboxplano' name='txtPassword' value='" +
                      sPassword + "' /></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");

            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(246) + "</td>");
            sb.Append("\r\n" + "<td><input type='password' class='textboxplano' name='txtPasswordRe' value='" +
                      sPassword + "' /></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");

            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(247) + "</td>");
            sb.Append("\r\n" + "<td class='textomaspeque'><input type='checkbox' name='chkRemember' value='1' " +
                      sRemember + " />");
            sb.Append("\r\n" + FuncionesWeb.Idioma(248) + "</td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(181) + "</td>");
            sb.Append("\r\n" + "<td><input type='text' name='txtEMail' value='" + sEMail +
                      "' size='30' class='textboxplano' /></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");

            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(182) + "</td>");
            sb.Append("\r\n" + "<td><input type='text' class='textboxplano' name='txtNombre' value='" + sNombre +
                      "' /></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");

            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(184) + "</td>");
            sb.Append("\r\n" + "<td><input type='text' class='textboxplano' name='txtApellido1' value='" +
                      sApellido1 + "' /></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");

            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(185) + "</td>");
            sb.Append("\r\n" + "<td><input type='text' class='textboxplano' name='txtApellido2' value='" +
                      sApellido2 + "' /></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");

            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(186) + "</td>");
            sb.Append("\r\n" + "<td class='textomaspeque'>");
            sb.Append("\r\n" + "<input type='radio' name='optGender' value='1' " + sGenderMale + " />");
            sb.Append("\r\n" + FuncionesWeb.Idioma(187));
            sb.Append("\r\n" + "<input type='radio' name='optGender' value='0' " + sGenderFemale + " />");
            sb.Append("\r\n" + FuncionesWeb.Idioma(188) + "</td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");

            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(189) + ": " + FuncionesWeb.Idioma(190) + "</td>");
            sb.Append("\r\n" + "<td class='textomaspeque'>");
            sb.Append("\r\n" + "<select name='txtBirthMonth' class='textboxplano'>");

            for (int i = 1; i <= 12; i++)
            {
                if (iBirthMonth == i)
                {
                    sb.Append("\r\n" + @"<option value=""" + i + @""" selected=""selected"">" + arrMonths[i] +
                              "</option>");
                }
                else
                {
                    sb.Append("\r\n" + @"<option value=""" + i + @""">" + arrMonths[i] + "</option>");
                }
            }

            sb.Append("\r\n" + "</select>");
            sb.Append("\r\n" + "</td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");

            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(203) + "</td>");
            sb.Append("\r\n" + "<td class='textomaspeque'>");
            sb.Append("\r\n" +
                      "<input type='text' name='txtBirthDay' size='2' maxlength='2' class='textboxplano' value='" +
                      sBirthDay + "' />");
            sb.Append("\r\n" + "</td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");

            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(204) + "</td>");
            sb.Append("\r\n" + "<td class='textomaspeque'>");
            sb.Append("\r\n" +
                      "<input type='text' name='txtBirthYear' size='4' maxlength='4' class='textboxplano' value='" +
                      sBirthYear + "' />");
            sb.Append("\r\n" + "</td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(249) + "</td>");
            sb.Append("\r\n" + "<td class='textomaspeque'>");
            sb.Append("\r\n" +
                      "<input type='text' name='txtDireccion' size='50' maxlength='50' class='textboxplano' value='" +
                      sDireccion + "' />");
            sb.Append("\r\n" + "</td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(45) + "</td>");
            sb.Append("\r\n" + "<td class='textomaspeque'>");
            sb.Append("\r\n" +
                      "<input type='text' name='txtPortal' size='4' maxlength='4' class='textboxplano' value='" +
                      sPortal + "' /> " + FuncionesWeb.Idioma(250) +
                      ": <input type='text' name='txtPiso' size='4' maxlength='4' class='textboxplano' value='" +
                      sPiso + "' />");
            sb.Append("\r\n" + "</td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(255) + "</td>");
            sb.Append("\r\n" + "<td class='textomaspeque'>");
            sb.Append("\r\n" +
                      "<input type='text' name='txtPoblacion' size='50' maxlength='50' class='textboxplano' value='" +
                      sPoblacion + "' />");
            sb.Append("\r\n" + "</td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(256) + "</td>");
            sb.Append("\r\n" + "<td class='textomaspeque'>");
            sb.Append("\r\n" +
                      "<input type='text' name='txtProvincia' size='50' maxlength='50' class='textboxplano' value='" +
                      sProvincia + "' />");
            sb.Append("\r\n" + "</td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(257) + "</td>");
            sb.Append("\r\n" + "<td class='textomaspeque'>");
            sb.Append("\r\n" +
                      "<input type='text' name='txtCodigoPostal' size='10' maxlength='10' class='textboxplano' value='" +
                      sCodigoPostal + "' />");
            sb.Append("\r\n" + "</td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(258) + "</td>");
            sb.Append("\r\n" + "<td class='textomaspeque'>");
            sb.Append("\r\n" +
                      "<input type='text' name='txtDNI' size='15' maxlength='15' class='textboxplano' value='" +
                      sDni + "' />");
            sb.Append("\r\n" + "</td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(259) + "</td>");
            sb.Append("\r\n" + "<td class='textomaspeque'>");
            sb.Append("\r\n" +
                      "<input type='text' name='txtTelefono1' size='15' maxlength='15' class='textboxplano' value='" +
                      sTelefono1 + "' />");
            sb.Append("\r\n" + "</td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(260) + "</td>");
            sb.Append("\r\n" + "<td class='textomaspeque'>");
            sb.Append("\r\n" +
                      "<input type='text' name='txtTelefono2' size='15' maxlength='15' class='textboxplano' value='" +
                      sTelefono2 + "' />");
            sb.Append("\r\n" + "</td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(209) + "</td>");
            sb.Append("\r\n" + "<td class='textomaspeque'>");
            sb.Append("\r\n" + "<select name='txtCountryCode' class='textboxplano'>");

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            DataTable dt = db.Execute("select idPais,nombre from " + Variables.App.prefijoTablas + "paises order by nombre");
            foreach (DataRow row in dt.Rows)
            {
                if (sCountryCode == NumberUtils.NumberInt(row["idPais"]))
                {
                    sb.Append("\r\n" + @"<option value=""" + row["idPais"] + @""" selected=""selected"">" +
                              row["nombre"] + "</option>");
                }
                else
                {
                    sb.Append("\r\n" + @"<option value=""" + row["idPais"] + @""">" + row["nombre"] + "</option>");
                }
            }

            sb.Append("\r\n" + "</select>");
            sb.Append("\r\n" + "</td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='accionpeque'>" + Ui.Lf() + FuncionesWeb.Idioma(74) + Ui.Lf() + Ui.Lf() + "</td>");
            sb.Append("\r\n" + "<td class='textomaspeque'></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(205) + "</td>");
            sb.Append("\r\n" + "<td class='textomaspeque'><input type='checkbox' name='chkNotifications' value='1' " +
                      sNotifications + " />");
            sb.Append("\r\n" + FuncionesWeb.Idioma(261) + "</td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");

            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(206) + "</td>");
            sb.Append("\r\n" + "<td class='textomaspeque'><input type='checkbox' name='chkNewsletter' value='1' " +
                      sNewsletter + " />");
            sb.Append("\r\n" + FuncionesWeb.Idioma(208) + "</td>");
            sb.Append("\r\n" + "</tr>");

            if (modulos.ModuloActivo("modCorreo"))
            {
                sb.Append("\r\n" + "<tr>");
                sb.Append("\r\n" + "<td class='accionpeque'>" + Ui.Lf() + FuncionesWeb.Idioma(262) + Ui.Lf() + Ui.Lf() +
                          "</td>");
                sb.Append("\r\n" + "<td class='textomaspeque'></td>");
                sb.Append("\r\n" + "</tr>");
                sb.Append("\r\n" + "<tr>");
                sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(263) + "</td>");
                sb.Append("\r\n" + "<td><input type='checkbox' name='chkNoRecibirCorreo' value='0' " +
                          sNoRecibirCorreo + " /> " + FuncionesWeb.Idioma(264) + "</td>");
                sb.Append("\r\n" + "</tr>");
                sb.Append("\r\n" + "<tr>");
                sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(265) + "</td>");
                sb.Append("\r\n" + "<td><input type='checkbox' name='chkNotificarCorreo' value='1' " +
                          sNotificarCorreo + " /> " + FuncionesWeb.Idioma(266) + "</td>");
                sb.Append("\r\n" + "</tr>");
            }

            if (modulos.ModuloActivo("modTopPost") | modulos.ModuloActivo("modTopPosters"))
            {
                sb.Append("\r\n" + "<tr>");
                sb.Append("\r\n" + "<td class='accionpeque'>" + Ui.Lf() + FuncionesWeb.Idioma(50) + Ui.Lf() + Ui.Lf() +
                          "<a href='condiciones.aspx'>" + FuncionesWeb.Idioma(121) + "</a></td>");
                sb.Append("\r\n" + "<td class='textomaspeque'></td>");
                sb.Append("\r\n" + "</tr>");
                sb.Append("\r\n" + "<tr>");
                sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(267) + "</td>");
                sb.Append("\r\n" +
                          "<td><input type='text' class='textboxplano' name='txtPaginaPrincipal' size='50' maxlength='48' value='" +
                          sPaginaPrincipal + "' /></td>");
                sb.Append("\r\n" + "</tr>");
                sb.Append("\r\n" + "<tr>");
                sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(268) + "</td>");
                sb.Append("\r\n" +
                          "<td><input type='text' class='textboxplano' name='txtICQ' size='50' maxlength='15' value='" +
                          sIcq + "' /></td>");
                sb.Append("\r\n" + "</tr>");
                sb.Append("\r\n" + "<tr>");
                sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(269) + "</td>");
                sb.Append("\r\n" +
                          "<td><input type='text' class='textboxplano' name='txtAIM' size='50' maxlength='60' value='" +
                          sAim + "' /></td>");
                sb.Append("\r\n" + "</tr>");
                sb.Append("\r\n" + "<tr>");
                sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(270) + "</td>");
                sb.Append("\r\n" +
                          "<td><input type='text' class='textboxplano' name='txtMSN' size='50' maxlength='60' value='" +
                          sMsn + "' /></td>");
                sb.Append("\r\n" + "</tr>");
                sb.Append("\r\n" + "<tr>");
                sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(271) + "</td>");
                sb.Append("\r\n" +
                          "<td><input type='text' class='textboxplano' name='txtYahoo' size='50' maxlength='60' value='" +
                          sYahoo + "' /></td>");
                sb.Append("\r\n" + "</tr>");
                sb.Append("\r\n" + "<tr>");
                sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(272) + "</td>");
                sb.Append("\r\n" +
                          "<td><input type='text' class='textboxplano' name='txtOcupacion' size='50' maxlength='40' value='" +
                          sOcupacion + "' /></td>");
                sb.Append("\r\n" + "</tr>");
                sb.Append("\r\n" + "<tr>");
                sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(273) + "</td>");
                sb.Append("\r\n" +
                          "<td><input type='text' class='textboxplano' name='txtInteres' size='50' maxlength='130' value='" +
                          sInteres + "' /></td>");
                sb.Append("\r\n" + "</tr>");

                sb.Append("\r\n" + "<tr>");
                sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(274) + "</td>");
                sb.Append("\r\n" + "<td><input type='checkbox' name='chkMostrarEmail' value='0'" + sMostrarEmail +
                          " />" + FuncionesWeb.Idioma(275) + "</td>");
                sb.Append("\r\n" + "</tr>");
                sb.Append("\r\n" + "<tr>");
                sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(276) + "</td>");
                sb.Append("\r\n" + "<td><input type='checkbox' name='chkNotificarRespMensajes' value='0' " +
                          sNotificarRespMensajes + " />" + FuncionesWeb.Idioma(277) + "</td>");
                sb.Append("\r\n" + "</tr>");
            }

            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td>" + Ui.Lf() + Ui.Lf() + "</td>");
            sb.Append("\r\n" + "<td><input type='submit' name='cmdSave' value='" + FuncionesWeb.Idioma(210, false) +
                      "' class='botonplano' /></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "</table>");
            sb.Append("\r\n" + "</form>");
            sb.Append("\r\n" + Ui.Lf() + Ui.Lf());

            return sb.ToString();
        }
    }
}