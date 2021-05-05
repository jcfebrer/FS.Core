// <fileheader>
// <copyright file="contactar.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: servicios\contactar.aspx.cs
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
    public class Contactar : BasePage
    {
        public bool bErr;
        public string sBody;
        public string sComments = "";
        public string sCompany = "";
        public string sEmail = "";
        public string sErr = "";
        public string sName = "";
        public string sPhone = "";
        public string sSubject;


        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            StringBuilder sb = new StringBuilder("");

            bErr = false;
            if (Web.Request("cmdSend") != "")
            {
                sName = Web.Request("txtName");
                sCompany = Web.Request("txtCompany");
                sEmail = Web.Request("txtEmail");
                sComments = Web.Request("txtComments");
                sPhone = Web.Request("txtPhone");
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

                if (!bErr)
                {
                    sSubject = FuncionesWeb.Idioma(286) + " - " + Variables.App.nombreWeb;
                    sBody = FuncionesWeb.Idioma(183) + " : " + sName + "\r\n";
                    sBody = sBody + FuncionesWeb.Idioma(287) + " : " + sCompany + "\r\n";
                    sBody = sBody + FuncionesWeb.Idioma(181) + " : " + sEmail + "\r\n";
                    sBody = sBody + FuncionesWeb.Idioma(329) + " : " + sPhone + "\r\n";
                    sBody = sBody + FuncionesWeb.Idioma(288) + " : " + "\r\n";

                    sBody = sBody + sComments + "\r\n" + "\r\n";

                    string strSendMsg = "";
                    try
                    {
						new SendMail().SendMailAsync(Variables.App.correoInfo, Variables.App.correoPrueba, Variables.App.correoCopia, sSubject, sBody, Variables.App.correoInfo, FuncionesWeb.Idioma(55), Variables.App.plantillaCorreo);
                    }
                    catch (System.Exception e)
                    {
                        strSendMsg = e.Message;
                    }

                    sb.Append("\r\n" + Ui.Lf() + Ui.Lf());
                    sb.Append("\r\n" + "<p class='cabemaspeque'>");

                    if (strSendMsg != "")
                    {
                        sb.Append("\r\n" + FuncionesWeb.Idioma(330) + strSendMsg);
                    }
                    else
                    {
                        sb.Append("\r\n" + FuncionesWeb.Idioma(238) + ", " + Ui.Lf() + Ui.Lf());
                        sb.Append("\r\n" + FuncionesWeb.Idioma(294));
                    }

                    sb.Append("\r\n" + "</p>");
                    sb.Append("\r\n" + "<p class='textomaspeque'>");
                    sb.Append("\r\n" + FuncionesWeb.Idioma(240));
                    sb.Append("\r\n" + "</p>");
                }
                else
                {
                    sb.Append(ShowForm());
                }
            }
            else
            {
                sb.Append(ShowForm());
            }

            return sb.ToString();
        }


        public string ShowForm()
        {
            Modulos modulos = new Modulos();
            StringBuilder sb = new StringBuilder("");

            if (Web.Request("comm") != "")
            {
                sComments = Web.Request("comm");
            }

            sb.Append("<p>" + Ui.Lf());
            sb.Append(FuncionesWeb.Idioma(295) + "</p>");

            if (modulos.ModuloActivo("modTemas"))
            {
                sb.Append(@"<p class=""textomaspeque"">" + FuncionesWeb.Idioma(296) + @"<a href=""" + Variables.App.directorioPortal +
                          @"temas/pregunta.aspx?pid=1"">&quot;" + FuncionesWeb.Idioma(297) + @"%&quot;</a>, <a href=""" +
                          Variables.App.directorioPortal + @"temas/recomendar.aspx?pid=1"">&quot;" + FuncionesWeb.Idioma(298) +
                          @"&quot;</a> &oacute; <a href=""" + Variables.App.directorioPortal +
                          @"temas/error.aspx?pid=1"">&quot;" +
                          FuncionesWeb.Idioma(299) + "&quot;</a>" + Ui.Lf());
                sb.Append(Ui.Lf());
                sb.Append(Ui.Lf());
                sb.Append(FuncionesWeb.Idioma(300) + "</p>");
            }

            sb.Append("\r\n" + @"<p class=""error"">");
            sb.Append("\r\n" + sErr);
            sb.Append("\r\n" + "</p>");
            sb.Append("\r\n" + @"<form action=""contactar.aspx?send=1"" method=""post"">");
            sb.Append("\r\n" + @"<table border=""0"">");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(183) + "</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtName"" class='textboxplano' value=""" + sName +
                      @"""/></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(287) + "</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtCompany"" class='textboxplano' value=""" +
                      sCompany + @"""/></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(181) + "</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtEmail"" class='textboxplano' value=""" + sEmail +
                      @"""/></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(329) + "</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtPhone"" class='textboxplano' value=""" + sPhone +
                      @"""/></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(288) + "</td>");
            sb.Append("\r\n" + @"<td><textarea cols=""40"" rows=""5"" name=""txtComments"" class=""textboxplano"">" +
                      sComments + "</textarea></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td>&nbsp;</td>");
            sb.Append("\r\n" +
                      @"<td><input type='submit' name=""cmdSend"" value="" Enviar "" class='botonplano' /></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "</table>");
            sb.Append("\r\n" + "</form>");
            sb.Append("\r\n" + Ui.Lf() + Ui.Lf());

            return sb.ToString();
        }
    }
}