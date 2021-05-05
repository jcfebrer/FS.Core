// <fileheader>
// <copyright file="subirficheros.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: admin\subirficheros.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Text;
using FSPortal;
using FSLibrary;
using FSNetwork;

namespace FSPaginas.Admin
{
    public class SubirFicheros : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            StringBuilder sb = new StringBuilder("");
            Upload funcUpload = new Upload();
            string strFileUploadPath = Variables.App.uploadPath;
            const string strImageTypes = "";
            // "zip;rar;arj;pdf;doc;ppt;xls"
            const int intMaxImageSize = 0;
            //500

            Server.ScriptTimeout = 1000;


            Response.Buffer = true;

            long lngErrorFileSize = 0;

            bool blnExtensionOk = true;

            sb.Append(@"<script type=""text/javascript"" language=""javascript"">");
            sb.Append("function checkData()");
            sb.Append("{");
            sb.Append("if (!document.forms.frmImageUp.file.value) { ");
            sb.Append(@"alert(""Debe indicarse un nombre de fichero."");");
            sb.Append("document.forms.frmImageUp.file.focus();");
            sb.Append("return false;");
            sb.Append("}");
            sb.Append("alert('Por favor, tenga paciencia mientras la imagen es enviada al servidor.');");
            sb.Append("return true;");
            sb.Append("}");
            sb.Append("</script>");

            sb.Append(Ui.Lf());
            sb.Append("<p class='accionpeque'>");
            sb.Append("Envio de ficheros");
            sb.Append("</p>");
            sb.Append("<p class='textomaspeque'>");

            if (Request.QueryString["PB"] == "Y")
            {
                string[] saryFileUploadTypes = strImageTypes.Trim().Split(';');
                funcUpload.FileUpload(Server.MapPath(strFileUploadPath), saryFileUploadTypes, intMaxImageSize,
					ref lngErrorFileSize, ref blnExtensionOk); //Variables.Parser.frmCampos
            }

            sb.Append("Seleccione el fichero que desee subir, y pulse 'enviar'. La carpeta destino es: " +
                      strFileUploadPath);

            sb.Append("</p>");
            sb.Append(Ui.Lf());
            sb.Append(Ui.Lf());
            sb.Append(
                @"<form action=""subirficheros.aspx?PB=Y"" method=""post"" enctype=""multipart/form-data"" name=""frmImageUp"" id=""frmImageUp"" onSubmit=""return checkData();"">");
            sb.Append(@"<table width=""100%"" border=""0"" align=""center"" cellpadding=""1"" cellspacing=""0"">");
            sb.Append("<tr>");
            sb.Append(@"<td width=""100%"">");

            sb.Append(@"<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""1"">");
            sb.Append("<tr>");
            sb.Append(
                @"<td align=""center"">Fichero: <input name=""file"" type=""file"" size=""25"" onFocus=""document.forms.frmImageUp.Submit.disabled=false;"" onchange=""document.forms.frmImageUp.Submit.disabled=false;"" />");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append(@"<tr align=""center"">");
            sb.Append(@"<td colspan=""2"" class=""text"">" + Ui.Lf());

            if (blnExtensionOk == false)
            {
                sb.Append("<span class=\"error\">La extensión de la imagen es erronea." + Ui.Lf() +
                          "La extensión puede ser: " + strImageTypes.Replace(";", ", ") + "</span>");
            }
            else if (lngErrorFileSize != 0)
            {
                sb.Append("<span class=\"error\">" + "Tamaño del fichero demasiado grande: " + lngErrorFileSize +
                          " Kb." + Ui.Lf() + "El tamaño máximo del fichero puede ser: " + intMaxImageSize +
                          " Kb</span>");
            }

            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append(@"<tr align=""center"">");
            sb.Append(@"<td>" + Ui.Lf() + @"<input type='submit' name=""Submit"" value=""Enviar"" disabled></td>");
            sb.Append("</tr>");
            sb.Append("</form>");
            sb.Append("</table>");

            return sb.ToString();
        }
    }
}