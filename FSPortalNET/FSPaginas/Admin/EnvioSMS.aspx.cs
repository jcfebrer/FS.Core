// <fileheader>
// <copyright file="envioSMS.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: admin\envioSMS.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using FSPortal;
using System.Text;
using FSMail;

namespace FSPaginas.Admin
{
    public class EnvioSms : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }
        public string Inicio()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<script type=""text/javascript"" language=""javascript"">");
            sb.AppendLine("function textCounter(tb, tnumber, maxCar) {");
            sb.AppendLine("var tot;");
            sb.AppendLine("tot = maxCar - tb.value.length;");
            sb.AppendLine("if (tot < 0) {");
            sb.AppendLine(@"alert(""Imposible enviar un mensaje superior a "" + maxCar + "" caracteres."");");
            sb.AppendLine("return false;");
            sb.AppendLine("}");
            sb.AppendLine("tnumber.value = tot;");
            sb.AppendLine("return true;");
            sb.AppendLine("}");
            sb.AppendLine("</script>");
            sb.AppendLine("<br />");
            string sErr = "";
            string sTo = "";
            string sBody = "";
            bool bErr = false;
            if ((Request["cmdSend"] != ""))
            {
                sTo = FSNetwork.Web.Request(Request["txtTo"]);
                sBody = FSNetwork.Web.Request(Request["txtBody"]);
                if ((sTo == ""))
                {
                    bErr = true;
                    sErr = "Por favor, introduzca un movíl.";
                }

                if ((sBody == ""))
                {
                    bErr = true;
                    sErr = "Por favor, introduzca un mensaje.";
                }

                if ((sBody.Length > 160))
                {
                    bErr = true;
                    sErr = "El tamaño del mensaje no debe superar los 160 caracteres.";
                }

                if (!bErr)
                {
                    //Implementar función de envio por SMS del proveedor de internet
                    //SendMail.SendSMS(sBody, sTo, "EsMasPadel");
                }
                sb.AppendLine("<p class='accionpeque'>Mensaje enviado!</p>");
                sb.AppendLine("<p class='cabemaspeque'>El mensaje será recibido por");
                sb.AppendLine(sTo);
                sb.AppendLine("en breves instantes.</p>");
                sb.AppendLine(@"<p class='textomaspeque'><a href=""default.aspx"">Pulsa aquí</a> para volver al menú de administración.</p>");
                Response.End();
            }

            if (Request["movil"] != "")
                sTo = FSNetwork.Web.Request(Request["movil"]);
            sb.AppendLine("<p class='accionpeque'>");
            sb.AppendLine("Envío de mensajes SMS");
            sb.AppendLine("</p>");
            sb.AppendLine("<p class='textomaspeque'>");
            sb.AppendLine(@"Para enviar un mensaje, rellena el siguiente formulario. En el campo ""Movíl"", indicar el/los <strong>movíl/es</strong> de/los usuario/s al que deseas enviar el SMS. Para indicar mas de un movíl, debes separarlos mediante "";"".</p>");
            sb.AppendLine("<p class='accionpeque'>");
            sb.AppendLine(sErr);
            sb.AppendLine("</p>");
            sb.AppendLine(@"<form action=""envioSMS.aspx"" method=""post"" name=""frmEnvioSms"">");
            sb.AppendLine(@"<table border=""0"">");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td class='cabemaspeque'>Movíl</td>");
            sb.AppendLine(@"<td><textarea name=""txtTo"" cols=""50"" rows=""4"" class='textboxplano'>");
            sb.AppendLine(sTo);
            sb.AppendLine(@"</textarea><br /><a href=""usuariosMovil.aspx?movil=");
            sb.AppendLine(sTo);
            sb.AppendLine(@""">Ver usuarios</a></td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td class='cabemaspeque'>Mensaje</td>");
            sb.AppendLine(@"<td><input name=""textfield3"" type='text' class=""caracteres"" value=""160"" size=""4"" maxlength=""4"" readonly> Caracteres restantes<br /><textarea cols=50 rows=15 name=""txtBody"" id=""txtBody"" class='textboxplano' onKeyDown="" textCounter(document.frmEnvioSms.txtBody, document.frmEnvioSms.textfield3, 160); "" onKeyUp="" textCounter(document.frmEnvioSms.txtBody, document.frmEnvioSms.textfield3, 160); "">");
            sb.AppendLine(sBody);
            sb.AppendLine("</textarea>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td> </td>");
            sb.AppendLine(@"<td><input type='submit' name=""cmdSend"" value="" Enviar! "" class='botonplano' /> <input type='submit' name=""cmdCancel"" value="" Cancelar "" class='botonplano' />");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("</form>");
            sb.AppendLine("<br /><br />");
            return sb.ToString();
        }

    }
}