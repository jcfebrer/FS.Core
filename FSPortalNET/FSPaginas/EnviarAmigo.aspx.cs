// <fileheader>
// <copyright file="enviarAmigo.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: enviarAmigo.aspx.cs
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

namespace FSPaginas
{
    public class EnviarAmigo : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            StringBuilder sb = new StringBuilder("");


            bool bErr = false;
            string sName = "";
            string sEmail = "";
            string sComments = "";
            string sErr = "";

            if (Web.Request("cmdSend") != "")
            {
                sName = Web.Request("txtName");
                sEmail = Web.Request("txtEmail");
                sComments = Web.Request("txtComments");
                if (sName == "")
                {
                    sErr = FuncionesWeb.Idioma(214);
                    bErr = true;
                }

                if (!TextUtil.IsEmail(sEmail))
                {
                    sErr = FuncionesWeb.Idioma(216);
                    bErr = true;
                }

                if (bErr) return sErr;
                const string sSubject = "Recomendación";
                string sBody = FuncionesWeb.Idioma(183) + " : " + sName + "\r\n";
                sBody = sBody + FuncionesWeb.Idioma(181) + " : " + sEmail + "\r\n";
                sBody = sBody + FuncionesWeb.Idioma(288) + " : " + "\r\n";
                sBody = sBody + sComments + "\r\n" + "\r\n";

				new SendMail().SendMailAsync(Variables.App.correoInfo, Variables.App.correoPrueba, Variables.App.correoCopia, sSubject, sBody, Variables.App.correoInfo, FuncionesWeb.Idioma(55), Variables.App.plantillaCorreo);

                sb.Append("\r\n" + Ui.Lf() + Ui.Lf());
                sb.Append("\r\n" + "<p class='cabemaspeque'>");
                sb.Append("\r\n" + FuncionesWeb.Idioma(238) + ", " + Ui.Lf() + Ui.Lf());
                sb.Append("\r\n" + FuncionesWeb.Idioma(294));
                sb.Append("\r\n" + "</p>");
                sb.Append("\r\n" + "<p class='textomaspeque'>");
                sb.Append("\r\n" + FuncionesWeb.Idioma(240));
                sb.Append("\r\n" + "</p>");
            }
            else
            {
                sb.Append("\r\n" + Ui.Lf());
                sb.Append("\r\n" + "<p class='cabepeque'>");
                sb.Append("\r\n" + "Recomienda");
                sb.Append("\r\n" + "</p>");
                sb.Append("\r\n" + "Recomienda una página a un amigo");


                sb.Append("\r\n" + Ui.Lf() + Ui.Lf() + Ui.Lf());
                sb.Append("\r\n" + @"<p class=""error"">");
                sb.Append("\r\n" + sErr);
                sb.Append("\r\n" + "</p>");
                sb.Append("\r\n" + @"<form action=""enviarAmigo.aspx"" method=""post"">");
                sb.Append("\r\n" + @"<table border=""0"">");
                sb.Append("\r\n" + "<tr>");
                sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(183) + "</td>");
                sb.Append("\r\n" + @"<td><input type='text' name=""txtName"" class='textboxplano' value=""" + sName +
                          @"""></td>");
                sb.Append("\r\n" + "</tr>");
                sb.Append("\r\n" + "<tr>");
                sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(181) + "</td>");
                sb.Append("\r\n" + @"<td><input type='text' name=""txtEmail"" class='textboxplano' value=""" + sEmail +
                          @"""></td>");
                sb.Append("\r\n" + "</tr>");
                sb.Append("\r\n" + "<tr>");
                sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(288) + "</td>");
                sb.Append("\r\n" + @"<td><textarea cols=40 rows=5 name=""txtComments"" class='textboxplano'>" +
                          sComments + "</textarea></td>");
                sb.Append("\r\n" + "</tr>");
                sb.Append("\r\n" + "<tr>");
                sb.Append("\r\n" + "<td>&nbsp;</td>");
                sb.Append("\r\n" +
                          @"<td><input type='submit' name=""cmdSend"" value="" Enviar "" class='botonplano' /></td>");
                sb.Append("\r\n" + "</tr>");
                sb.Append("\r\n" + "</table>");
                sb.Append("\r\n" + "</form>");
                sb.Append("\r\n" + "</p>");
                sb.Append("\r\n" + Ui.Lf() + Ui.Lf());
            }

            return sb.ToString();
        }
    }
}