// <fileheader>
// <copyright file="comentarios.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: comentarios.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Text;
using FSPortal;
using FSNetwork;
using FSDatabase;
using FSMail;

namespace FSPaginas
{
    public class Comentarios : BasePage
    {
        public bool bErr;
        public string sBody;
        public string sComentario = "";
        public string sEnviadoPor = "";
        public string sErr = "";
        public string sSubject;


        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            StringBuilder sb = new StringBuilder("");
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            bErr = false;
            if (Web.Request("cmdSend") != "")
            {
                sComentario = Web.Request("txtComentario");
                sEnviadoPor = Web.Request("txtEnviadoPor");

                if (sComentario == "")
                {
                    sErr = "Debes indicar tu comentario.";
                    bErr = true;
                }

                if (sEnviadoPor == "")
                {
                    sErr = "Debes indicar el nombre de la persona que envia el comentario.";
                    bErr = true;
                }


                if (!bErr)
                {
                    db.ExecuteNonQuery("insert into comentarios (idPagina,comentario,fecha,enviadoPor) values (" +
                                       Web.RequestInt("idPagina") + ",'" + sComentario + "'," +
						Utils.FormatLongDate(System.DateTime.Now) + ",'" + sEnviadoPor + "')");

                    sSubject = "Web: " + Variables.App.nombreWeb;
                    sBody = "Comentario : " + sComentario + "\r\n";
                    sBody = sBody + "Nombre : " + sEnviadoPor + "\r\n";


                    string strSendMsg = "";
                    try
                    {
						new SendMail().SendMailAsync(Variables.App.correoInfo, Variables.App.correoPrueba, Variables.App.correoCopia, sSubject, sBody, Variables.App.correoInfo, "Comentarios", Variables.App.plantillaCorreo);
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
            StringBuilder sb = new StringBuilder("");

            sb.Append("\r\n" + @"<p class=""error"">");
            sb.Append("\r\n" + sErr);
            sb.Append("\r\n" + "</p>");
            sb.Append("\r\n" + @"<form action=""comentarios.aspx?send=1"" method=""post"">");
            sb.Append("\r\n" + @"<input type=""hidden"" name=""idPagina"" value=""" + Web.RequestInt("id") +
                      @""">");
            sb.Append("\r\n" + @"<table border=""0"">");
            sb.Append("\r\n" + "<tr><td><b>Envia tu comentario</b></td></tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>* Comentario</td>");
            sb.Append("\r\n" + @"<td><textarea rows=""10"" cols=""40"" name=""txtComentario"" class=""textboxplano"">" +
                      sComentario + "</textarea></td>");
            sb.Append("\r\n" + "</tr>");

            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>* Nombre</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtEnviadoPor"" class=""textboxplano"" value=""" +
                      sEnviadoPor + @"""></td>");
            sb.Append("\r\n" + "</tr>");


            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td>&nbsp;</td>");
            sb.Append("\r\n" +
                      @"<td><input type='submit' name=""cmdSend"" value="" Enviar comentario "" class=""botonplano"" /></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "</table>");
            sb.Append("\r\n" + "</form>");

            return sb.ToString();
        }
    }
}