// <fileheader>
// <copyright file="createView.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: admin\editor\createView.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Text;
using FSPortal;
using FSDatabase;
using FSNetwork;

namespace FSPaginas.Admin.Editor
{
    public class CreateView : BasePage
    {
        private string sName;
        private string sQuery;

        protected void Page_Load(object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Process()
        {
            StringBuilder sb = new StringBuilder("");

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            if (Web.Request("cmdSend") != "")
            {
                sName = Web.Request("txtName");
                sQuery = Request.Form["txtQuery"];

                try
                {
                    db.ExecuteNonQuery("CREATE VIEW [" + sName + "] AS " + sQuery);

                    sb.Append(Ui.Lf() + "Sentencia " + sName + " - Creada correctamente." + Ui.Lf() + Ui.Lf());
                    sb.Append("\r\n" + @"<a href=""javascript:history.go(-2);"">Volver</a>");
                }
                catch (System.Exception e)
                {
                    sb.Append(Ui.Lf() + "Error: " + e.Message + Ui.Lf());
                }
            }
            return sb.ToString();
        }


        public string Inicio()
        {
            StringBuilder sb = new StringBuilder("");

            sb.Append(Process());

            sb.Append(Ui.Lf());

            sb.Append("<p class='accionpeque'>Crear vista</p>");

            sb.Append(Ui.Lf() + Ui.Lf());

            sb.Append(@"<form action=""createView.aspx"" method=""post"">");
            sb.Append(@"<table border=""0"">");
            sb.Append("<tr>");
            sb.Append(@"<td class=""cabemaspeque"">Nombre:</td>");
            sb.Append(@"<td><input size=""70"" type=""text"" name=""txtName"" class=""textboxplano"" value=""" + sName +
                      @"""></td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td class='cabemaspeque'>Consulta:</td>");
            sb.Append(@"<td><textarea cols=50 rows=15 name=""txtQuery"" class='textboxplano'>" + sQuery);
            sb.Append("</textarea></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>&nbsp;</td>");
            sb.Append(@"<td><input type=""submit"" name=""cmdSend"" value=""" + FuncionesWeb.Idioma(37) +
                      @""" class=""botonplano"" /></td>");
            sb.Append("</tr>");

            sb.Append("</table>");
            sb.Append("</form>");

            return sb.ToString();
        }
    }
}