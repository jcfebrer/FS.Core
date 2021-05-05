// <fileheader>
// <copyright file="usuariosMovil.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: admin\usuariosMovil.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using FSNetwork;
using FSLibrary;
using FSPortal;
using System;
using System.Data;
using System.Text;
using FSDatabase;

namespace FSPaginas.Admin
{
    public class UsuariosMovil : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }
        public string Inicio()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<br />");
            sb.AppendLine("<p class='cabepeque'>");
            sb.AppendLine("Usuarios del portal");
            sb.AppendLine("</p>");
            string sSql;
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            // OpenDB Variables.App.ConnPortal
            sSql = ("SELECT * FROM "
                        + (Variables.App.prefijoTablas + "Usuarios"));
            DataTable dt = db.Execute(sSql);
            sb.AppendLine(@"<table border=""0"" width=""100%"">");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td width=15% align=center>Usuario</td>");
            sb.AppendLine("<td width=70% align=left>Nombre y Apelldos</td>");
            sb.AppendLine("<td width=15% align=center>Movíl</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            foreach (DataRow row in dt.Rows)
            {
                sb.AppendLine(@"<table border=""0"" width=""100%"">");
                sb.AppendLine(@"<tr bgcolor=""#fdf5e6"">");
                sb.AppendLine(@"<td width=""15%"" align=""center""><a href=""");
                sb.AppendLine(Variables.App.directorioPortal);
                sb.AppendLine("admin/envioSMS.aspx?movil=");
                sb.AppendLine(Web.Request(Request["movil"]) + ";" + Functions.Valor(row["telefono2"]));
                sb.AppendLine(@""">");
                sb.AppendLine(Functions.Valor(row["usuario"]));
                sb.AppendLine("</a></td>");
                sb.AppendLine(@"<td width=""70%"" class='cabemaspeque'>#");
                sb.AppendLine(Functions.Valor(row["Nombre"]) + " " + Functions.Valor(row["Apellido1"]) + " " + Functions.Valor(row["Apellido2"]));
                sb.AppendLine("</td>");
                sb.AppendLine(@"<td width=""15%"" class='cabemaspeque'>");
                if ((Functions.Valor(row["telefono1"]).Substring(0, 1) == "6"))
                {
                    sb.AppendLine(Functions.Valor(row["telefono1"]));
                }
                else
                {
                    sb.AppendLine(Functions.Valor(row["telefono2"]));
                }
            }
            sb.AppendLine("<br />");
            return sb.ToString();
        }

    }
}