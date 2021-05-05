// <fileheader>
// <copyright file="registrar.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: servicios\registrar.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using System.Text;
using FSPortal;
using FSLibrary;
using FSDatabase;
using FSNetwork;

namespace FSPaginas.Servicios
{
    public class Registrar : BasePage
    {
        public string sApellido1;
        public string sApellido2;
        public int sBirthDay;
        public int sBirthMonth;
        public int sBirthYear;
        public int sCountryCode;
        public string sEMail;
        public string sGenderFemale;
        public string sGenderMale;
        public string sGoBackTo;
        public string sNewsletter;
        public string sNombre;
        public string sNotifications;
        public string sRemember;
        public string sUsuario;

        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            Portal portal = new Portal();
            string sReferer = "";

            StringBuilder sb = new StringBuilder("");

            if (Variables.User.Usuario != "")
            {
                sb.Append("\r\n" + Ui.Lf());
                sb.Append("\r\n" + "<p class='accionpeque'>" + FuncionesWeb.Idioma(180) + "</p>");
                sb.Append("\r\n" + "<p>" + FuncionesWeb.Idioma(227) + "</p>");
            }
            else
            {
                if (Web.Request("comebackto") != "")
                {
                    sReferer = Request["comebackto"];
                    sGoBackTo = "?" + Request.QueryString;
                }

                if (Web.Request("cmdsave") != "")
                {
                    sUsuario = Web.Request("txtUsuario");
                    sEMail = Web.Request("txtEmail");
                    sNombre = Web.Request("txtNombre");
                    sApellido1 = Web.Request("txtApellido1");
                    sApellido2 = Web.Request("txtApellido2");
                    if (Request["optGender"] == "1")
                    {
                        sGenderMale = " checked";
                    }
                    else
                    {
                        sGenderFemale = " checked";
                    }
                    sBirthMonth = Web.RequestInt("txtBirthMonth");
                    sBirthDay = Web.RequestInt("txtBirthDay");
                    sBirthYear = Web.RequestInt("txtBirthYear");
                    if (Request["chkNotifications"] != "")
                    {
                        sNotifications = " checked";
                    }
                    if (Request["chkNewsletter"] != "")
                    {
                        sNewsletter = " checked";
                    }
                    sCountryCode = Web.RequestInt("txtCountryCode");
                    if (Web.Cookie(Request.Cookies[Variables.App.strCookieName], "Usuario") != "")
                    {
                        sRemember = " checked";
                    }

                    bool snot = sNotifications != "";
                    bool snews = sNewsletter != "";
                    bool sgfem = sGenderFemale != "";
                    //bool srem = sRemember != "";


                    int sexo = sgfem ? 0 : 1;

                    string sErr = "";

                    try
                    {
                        portal.AddUser(sUsuario, sEMail, sNombre, sApellido1, sApellido2, sBirthMonth, sBirthDay,
                            sBirthYear, snot, snews, sCountryCode, "", 0, sexo, 0);
                    }
                    catch (System.Exception e)
                    {
                        sErr = e.Message;
                    }

                    if (sErr != "")
                    {
                        sb.Append("\r\n" + (ShowProfile(sErr)));
                    }
                    else
                    {
                        sb.Append("\r\n" + Ui.Lf() + Ui.Lf());
                        sb.Append("\r\n" + (FuncionesWeb.Idioma(217)));
                        sb.Append("\r\n" + Ui.Lf() + Ui.Lf());
                        sb.Append("\r\n" +
                                  ("Puedes conectarte <a href='" + Variables.App.paginaLogin +
                                   (Variables.App.paginaLogin.IndexOf("?") > 0 ? "&" : "?") + "comebackto=" +
                                   System.Web.HttpUtility.UrlEncode(sReferer) + "'>aquí</a>."));
                    }
                }
                else
                {
                    sGenderMale = @" checked=""checked""";
                    sNotifications = @" checked=""checked""";
                    sNewsletter = @" checked=""checked""";
                    sRemember = @" checked=""checked""";
                    sCountryCode = 0;

                    sb.Append("\r\n" + ShowProfile(""));
                }
            }

            return sb.ToString();
        }


        public string ShowProfile(string sErr)
        {
            StringBuilder sb = new StringBuilder("");
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

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
            sb.Append("\r\n" + "<p class='accionpeque'>" + FuncionesWeb.Idioma(180) + "</p>");
            sb.Append("\r\n" + "<p>" + FuncionesWeb.Idioma(212));
            sb.Append("\r\n" + "</p>");
            sb.Append("\r\n" + "<p class='error'>");
            sb.Append("\r\n" + sErr);
            sb.Append("\r\n" + "</p>");
            sb.Append("\r\n" + "<form action='registrar.aspx" + sGoBackTo + "' method='post'>");
            sb.Append("\r\n" + "<input type='hidden' name='sessionID' value='" + Variables.User.sessionID + "' />");
            sb.Append("\r\n" + @"<table border=""0"">");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>*" + FuncionesWeb.Idioma(182) + "</td>");
            sb.Append("\r\n" + "<td><input type='text' class='textboxplano' name='txtUsuario' value='" + sUsuario +
                      "' /> " + FuncionesWeb.Idioma(211) + "</td>");
            sb.Append("\r\n" + "<td class='textomaspeque'></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>*" + FuncionesWeb.Idioma(181) + "</td>");
            sb.Append("\r\n" + "<td><input type='text' name='txtEMail' value='" + sEMail +
                      "' size='30' class='textboxplano' /></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(183) + "</td>");
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
            sb.Append("\r\n" + "<input type='radio' name='optGender' value='1'" + sGenderMale + " />" +
                      FuncionesWeb.Idioma(187));
            sb.Append("\r\n" + "<input type='radio' name='optGender' value='0'" + sGenderFemale + " />" +
                      FuncionesWeb.Idioma(188));
            sb.Append("\r\n" + "</td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(189) + ": </td>");
            sb.Append("\r\n" + "<td class='textomaspeque'><table><tr>");
            sb.Append("\r\n" + "<td width='50'>" + FuncionesWeb.Idioma(190) +
                      ": </td><td><select name='txtBirthMonth' class='textboxplano'>");

            for (int iTemp = 1; iTemp <= 12; iTemp++)
            {
                if (Convert.ToInt32(sBirthMonth) == iTemp)
                {
                    sb.Append("\r\n" + @"<option value=""" + iTemp + @""" selected>" + arrMonths[iTemp] +
                              "</option>");
                }
                else
                {
                    sb.Append("\r\n" + @"<option value=""" + iTemp + @""">" + arrMonths[iTemp] + "</option>");
                }
            }

            sb.Append("\r\n" + "</select></td></tr></table>");
            sb.Append("\r\n" + "</td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'></td>");
            sb.Append("\r\n" + "<td class='textomaspeque'>");
            sb.Append("\r\n" + "<table><tr><td width='50'>" + FuncionesWeb.Idioma(203) +
                      ":</td><td><input type='text' name='txtBirthDay' size='2' maxlength='2' class='textboxplano' value='" +
                      sBirthDay + "' /></td></tr></table>");
            sb.Append("\r\n" + "</td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'></td>");
            sb.Append("\r\n" + "<td class='textomaspeque'>");
            sb.Append("\r\n" + "<table><tr><td width='50'>" + FuncionesWeb.Idioma(204) +
                      ":</td><td><input type='text' name='txtBirthYear' size='4' maxlength='4' class='textboxplano' value='" +
                      sBirthYear + "' /></td></tr></table>");
            sb.Append("\r\n" + "</td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(205) + "</td>");
            sb.Append("\r\n" + "<td class='textomaspeque'><input type='checkbox' name='chkNotifications' value='1' " +
                      sNotifications + " />" + FuncionesWeb.Idioma(207) + "</td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(206) + "</td>");
            sb.Append("\r\n" + "<td class='textomaspeque'><input type='checkbox' name='chkNewsletter' value='1'" +
                      sNewsletter + " />" + FuncionesWeb.Idioma(208) + "</td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(209) + "</td>");
            sb.Append("\r\n" + "<td class='textomaspeque'>");
            sb.Append("\r\n" + "<select name='txtCountryCode' class='textboxplano'>");

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
            sb.Append("\r\n" + "<td>&nbsp;</td>");
            sb.Append("\r\n" + "<td><input type='submit' name='cmdSave' value=' " + FuncionesWeb.Idioma(210) +
                      " ' class='botonplano' /></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "</table>");
            sb.Append("\r\n" + "</form>");
            sb.Append("\r\n" + Ui.Lf());
            sb.Append("\r\n" + "<font size='1'  class='navlink'>");
            sb.Append("\r\n" + FuncionesWeb.Idioma(213));
            sb.Append("\r\n" + "</font>" + Ui.Lf());

            return sb.ToString();
        }
    }
}