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
using System.Configuration;
using FSDatabase;
using FSNetwork;

namespace FSPaginas.Admin.Editor
{
    public class ExecuteCommand : BasePage
    {
        private string sQuery;

        protected void Page_Load(object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Process()
        {
            StringBuilder sb = new StringBuilder("");

            if (Web.Request("cmdSend") != "")
            {
            	sQuery = Page.Request.Form["txtQuery"];

                string[] aQuery = sQuery.Split(';');

                BdUtils db;
                if (!String.IsNullOrEmpty(Web.Request("conexionSel")))
                {
                    db = new BdUtils(Web.Request("conexionSel"));
                }
                else
                {
                    db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
                }

                foreach (string sql in aQuery)
                {
                    try
                    {
                        if(sql != "")
                        {
                            int tot = db.ExecuteNonQuery(sql.Trim());

                            sb.Append(Ui.Lf() + "Sentencia: (" + sql + ") - Registros: " + tot + Ui.Lf() + Ui.Lf());
                        }
                    }
                    catch (System.Exception e)
                    {
                        sb.Append(Ui.Lf() + "Error: " + e.Message + Ui.Lf());
                    }
                }
            }


            return sb.ToString();
        }


        public string Inicio()
        {
            StringBuilder sb = new StringBuilder("");

            sb.Append(Process());

            sb.Append(Ui.Lf());

            sb.Append("<p class='accionpeque'>Ejecutar comando</p>");

            sb.Append(Ui.Lf() + Ui.Lf());

            sb.Append(@"<form action=""executeCommand.aspx"" method=""post"">");
            sb.Append(@"<table border=""0"">");
            sb.Append("<tr>");
            sb.Append("<td class='cabemaspeque'>Selecciona la conexión:</td>");
            sb.Append(@"<td class='cabemaspeque'>");
            sb.Append(@"<select name=""conexionSel"">");
            foreach (ConnectionStringSettings con in ConfigurationManager.ConnectionStrings)
            {
                if (con.ConnectionString.ToString().Trim() != "")
                {
                    sb.Append(@"<option value=""" + con.Name.ToString() + @""">" + con.Name.ToString() + "</option>");
                }
            }
            sb.Append("</td></tr>");
            sb.Append("<tr>");
            sb.Append("<td class='cabemaspeque'>Sentencia:</td>");
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