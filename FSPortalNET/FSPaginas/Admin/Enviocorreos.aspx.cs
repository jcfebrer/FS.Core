// <fileheader>
// <copyright file="enviocorreos.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: admin\enviocorreos.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using System.Text;
using FSPortal;
using FSLibrary;
using FSNetwork;
using FSDatabase;
using FSMail;

namespace FSPaginas.Admin
{
    public class EnvioCorreos : BasePage
    {
        public bool bAllUsers;
        public bool bNewsletter;
        public bool bNotification;
        public string sBody = "";
        public string sFrom = "";
        public string sSubject = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Process()
        {
            StringBuilder sb = new StringBuilder("");
            string sFilter = "";

            Server.ScriptTimeout = 4000;

			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            if (Web.Request("cmdSend") != "")
            {
                sSubject = Web.Request("txtSubject");
                sFrom = Web.Request("txtFrom");
                string sGetBody = Functions.Valor("ctl00$ContenidoPH$txtBody");
                if (Web.Request("chkNotification") != "") sFilter = " WHERE EnvioNotificaciones = True";
                if (Web.Request("chkNewsletter") != "")
                {
                    if (sFilter != "")
                        sFilter = sFilter + " OR EnvioCorreos = True";
                    else
                        sFilter = " WHERE EnvioCorreos = True";
                }

                if (Web.Request("chkAllUsers") != "") sFilter = "";

                string sSql = "SELECT nombre,apellido1,apellido2,email FROM " + Variables.App.prefijoTablas + "Usuarios" + sFilter;
                DataTable dt = db.Execute(sSql);

                sb.Append(Ui.Lf() + FuncionesWeb.Idioma(25) + Ui.Lf() + Ui.Lf());

                foreach (DataRow row in dt.Rows)
                {
                    string sFirstName = Functions.Valor(row["Nombre"]);
                    string sApe1 = Functions.Valor(row["apellido1"]);
                    string sApe2 = Functions.Valor(row["apellido2"]);
                    string sTo = Functions.Valor(row["eMail"]);

                    sBody = sGetBody.Replace("*nombre*", sFirstName);
                    sBody = sBody.Replace("*apellido1*", sApe1);
                    sBody = sBody.Replace("*apellido2*", sApe2);

                    if (sTo != "")
                    {
                        try
                        {
							new SendMail().SendMailAsync(sTo, sSubject,Variables.App.correoPrueba, Variables.App.correoCopia, sBody, Variables.App.correoInfo, sFrom, Variables.App.plantillaCorreo);
                        }
                        catch (System.Exception e)
                        {
                            sb.Append(e.Message);
                        }

                        sb.Append(FuncionesWeb.Idioma(26) + " " + sFirstName + " " + sApe1 + " " + sApe2 + " :: " + sTo +
                                  Ui.Lf());
                    }
                }

                sb.Append(Ui.Lf() + FuncionesWeb.Idioma(27) + " <a href=\"enviocorreos.aspx\">" + FuncionesWeb.Idioma(28) + "</a> " +
                          FuncionesWeb.Idioma(29));
            }

            return sb.ToString();
        }


        public string Inicio()
        {
            StringBuilder sb = new StringBuilder("");

            sb.Append(Process());

            sb.Append(Ui.Lf());

            sb.Append("<p class='accionpeque'>" + FuncionesWeb.Idioma(24) + "</p>");

            sb.Append(FuncionesWeb.Idioma(30));
            sb.Append(Ui.Lf() + Ui.Lf());

            sb.Append(@"<form action=""enviocorreos.aspx"" method=""post"">");
            sb.Append(@"<table border=""0"">");
            sb.Append("<tr>");
            sb.Append(@"<td class=""cabemaspeque"">" + FuncionesWeb.Idioma(34) + "</td>");
            sb.Append(@"<td><input size=""70"" type=""text"" name=""txtSubject"" class=""textboxplano"" value=""" +
                      sSubject + @"""></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append(@"<td class=""cabemaspeque"">" + FuncionesWeb.Idioma(35) + "</td>");
            sb.Append(@"<td><input size=""70"" type=""text"" name=""txtFrom"" class=""textboxplano"" value=""" +
                      sFrom + @"""></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append(@"<td class=""cabemaspeque"">" + FuncionesWeb.Idioma(36) + "</td>");
            sb.Append("<td>");
            sb.Append(Ui.HtmlEditor("txtBody", ""));
            sb.Append(Ui.Lf());
            sb.Append(FuncionesWeb.Idioma(31) + Ui.Lf());
            sb.Append(FuncionesWeb.Idioma(32) + Ui.Lf());
            sb.Append(FuncionesWeb.Idioma(33));
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>&nbsp;</td>");
            sb.Append(@"<td><input type=""submit"" name=""cmdSend"" value=""" + FuncionesWeb.Idioma(37) +
                      @""" class=""botonplano"" /></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>&nbsp;</td>");
            sb.Append(@"<td>" + Ui.Lf() + Ui.Lf() + @"<input type=""checkbox"" name=""chkNotification"" value=""1""" +
                      bNotification + "> " + FuncionesWeb.Idioma(38) + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>&nbsp;</td>");
            sb.Append(@"<td><input type=""checkbox"" name=""chkNewsletter"" value=""1""" + bNewsletter + "> " +
                      FuncionesWeb.Idioma(39) + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>&nbsp;</td>");
            sb.Append(@"<td><input type=""checkbox"" name=""chkAllUsers"" value=""1""" + bAllUsers + "> " +
                      FuncionesWeb.Idioma(40) + "</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("</form>");

            sb.Append(@"<p class=""accionmaspeque"">");
            sb.Append(FuncionesWeb.Idioma(41));
            sb.Append("</p>");

            return sb.ToString();
        }
    }
}