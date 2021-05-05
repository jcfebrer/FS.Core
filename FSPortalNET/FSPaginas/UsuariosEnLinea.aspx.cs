// <fileheader>
// <copyright file="usuariosEnLinea.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: usuariosEnLinea.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Text;
using FSPortal;

namespace FSPaginas
{
    public class usuariosEnLinea : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            StringBuilder s = new StringBuilder("");

            s.Append(@"<table border=""0"" cellspacing=""1"" cellpadding=""3"" width=""100%"" class=""tb-section"">");
            s.Append("<tr>");
            s.Append(@"<td class=""news-title"" valign=""middle"">");
            s.Append(
                @"<img src=""imagenes/news.gif"" width=""16"" height=""16"" alt=""Noticias y Novedades"" border=""0"" align=""middle"">");
            s.Append("&nbsp;&nbsp;Usuarios en Linea");
            s.Append("</td>");
            s.Append("</tr>");
            s.Append("</table>");
            s.Append(Ui.Lf());

            s.Append(@"<table cellpadding=""2"" cellspacing=""1"" border=""0"" width=""100%"">");
            s.Append(
                "<tr><td>&nbsp;</td><td><strong>Nombre de usuario</strong></td><td><strong>Fecha</strong></td><td><strong>Página actualmente visitada</strong></td><td>&nbsp;</td></tr>");


            s.Append("</table>");

            return s.ToString();
        }
    }
}