// <fileheader>
// <copyright file="publicidad.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: servicios\publicidad.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Text;
using FSPortal;
using FSLibrary;
using FSNetwork;
using FSMail;

namespace FSPaginas.Servicios
{
    public class Publicidad : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            StringBuilder sb = new StringBuilder("");

            string sErr = "";
            string sCompany = "";
            string sName = "";
            string sEmail = "";
            string sComments = "";

            bool bErr = false;

            if (Request["cmdSend"] != "")
            {
                sName = Web.Request("txtName");
                sCompany = Web.Request("txtCompany");
                sEmail = Web.Request("txtEmail");
                sComments = Web.Request("txtComments");
                if (sName == "")
                {
                    sErr = FuncionesWeb.Idioma(241);
                    bErr = true;
                }

                if (!TextUtil.IsEmail(sEmail))
                {
                    sErr = FuncionesWeb.Idioma(216);
                    bErr = true;
                }

                if (!(bErr))
                {
                    string sAdEmail = Variables.App.correoInfo;

                    string sSubject = FuncionesWeb.Idioma(301);
                    string sBody = FuncionesWeb.Idioma(183) + " : " + sName + "\r\n";
                    sBody = sBody + FuncionesWeb.Idioma(287) + " : " + sCompany + "\r\n";
                    sBody = sBody + FuncionesWeb.Idioma(181) + " : " + sEmail + "\r\n";
                    sBody = sBody + FuncionesWeb.Idioma(288) + " : " + "\r\n";
                    sBody = sBody + sComments + "\r\n" + "\r\n";

					new SendMail().SendMailAsync(sAdEmail, Variables.App.correoPrueba, Variables.App.correoCopia, sSubject, sBody, Variables.App.correoInfo,
						FuncionesWeb.Idioma(292) + " - " + Variables.App.nombreWeb, Variables.App.plantillaCorreo);

                    sb.Append(Ui.Lf() + Ui.Lf());
                    sb.Append("<p class='cabemaspeque'>");
                    sb.Append(FuncionesWeb.Idioma(238) + ", " + Ui.Lf());
                    sb.Append(FuncionesWeb.Idioma(303));
                    sb.Append("</p>");
                    sb.Append("<p class='textomaspeque'>");
                    sb.Append(Ui.Link(FuncionesWeb.Idioma(304), Variables.App.directorioPortal));
                    sb.Append("</p>");
                }
            }

            sb.Append(Ui.Lf());
            sb.Append("<p class='accionpeque'>");
            sb.Append(FuncionesWeb.Idioma(305));
            sb.Append("</p>");

            if (sErr == "")
            {
                sb.Append("<p class='textomaspeque'>");
                sb.Append(FuncionesWeb.Idioma(306));

                sb.Append(FuncionesWeb.Idioma(303));
                sb.Append(Ui.Lf());
            }
            else
            {
                sb.Append(Ui.Lf() + Ui.Lf() + @"<p class=""cabemaspeque"">" + sErr + "</p>");
            }

            sb.Append(Ui.Lf());
            sb.Append(@"<form action=""publicidad.aspx?send=1"" method=""post"">");
            sb.Append(@"<table border=""0"">");
            sb.Append("<tr>");
            sb.Append(@"<td class=""cabemaspeque"">" + FuncionesWeb.Idioma(183) + "</td>");
            sb.Append(@"<td><input type=""text"" name=""txtName"" class=""textboxplano"" value=""" + sName +
                      @"""></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append(@"<td class=""cabemaspeque"">" + FuncionesWeb.Idioma(287) + "</td>");
            sb.Append(@"<td><input type=""text"" name=""txtCompany"" class=""textboxplano"" value=""" + sCompany +
                      @"""></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append(@"<td class=""cabemaspeque"">" + FuncionesWeb.Idioma(181) + "</td>");
            sb.Append(@"<td><input type=""text"" name=""txtEmail"" class=""textboxplano"" value=""" + sEmail +
                      @"""></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append(@"<td class=""cabemaspeque"">" + FuncionesWeb.Idioma(288) + "</td>");
            sb.Append(@"<td><textarea cols=""40"" rows=""5"" name=""txtComments"" class=""textboxplano"">" +
                      sComments + "</textarea></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>&nbsp;</td>");
            sb.Append(@"<td><input type=""submit"" name=""cmdSend"" value="" Enviar "" class=""botonplano"" /></td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("</form>");

            sb.Append(Ui.Lf() + Ui.Lf());

            return sb.ToString();
        }
    }
}