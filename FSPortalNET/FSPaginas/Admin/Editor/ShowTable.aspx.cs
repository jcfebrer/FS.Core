// <fileheader>
// <copyright file="showtable.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: admin\editor\showtable.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Text;
using FSPortal;
using FSLibrary;
using FSNetwork;
using FSDatabase;

namespace FSPaginas.Admin.Editor
{
    public class ShowTable : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            int cid = 0;
            if (Web.Request("cid") == "")
            {
                cid = Utils.GetConnectionId(Variables.App.defaultEntry);
            }
            else
            {
                cid = Web.RequestInt("cid");
            }
            contenido = Inicio(cid, Web.Request("tablename"), Web.RequestBool("q"), true);
        }

        public string Inicio(int cid, string tableName, bool query, bool showAdd)
        {
            StringBuilder sb = new StringBuilder("");

            string sTableName = tableName;
            sTableName = System.Web.HttpUtility.HtmlDecode(sTableName);
            bool bQuery = query;  
            

            if (Variables.User.Administrador)
            {
                sb.Append("\r\n" + @"<table border=""0"" cellspacing=""1"" cellpadding=""2"" width=""100%"">");

                sb.Append("\r\n" + "<tr>");
                sb.Append("\r\n" + "<td>");
                sb.Append("\r\n" + @"<img border=""0"" src='" + Variables.App.directorioPortal +
                          "imagenes/bullet.gif' alt='' /> <a href='listtables.aspx?cid=" 
                          + cid + "'>Portal</a> <img border='0' src='" +
                          Variables.App.directorioPortal + "imagenes/bullet.gif' alt='' /> " +
                          ((bQuery ? "Consulta" : "[Tabla :" + sTableName + "]")));
                sb.Append("\r\n" + "</td>");
                sb.Append("\r\n" + @"<td align=""right"">");
                
                if (showAdd)
	            {
	                //sb.Append("\r\n" + Ui.Lf() + Ui.Lf());
	                sb.Append(Ui.Button("Añadir registro",
	                    "window.location='showrecord.aspx?simple=0&cid=" + cid + "&tablename=" + sTableName +
	                    @"&add=1&page=1'"));
	                //sb.Append("\r\n" + Ui.Lf() + Ui.Lf());
	            }

                sb.Append("\r\n" + "</td>");
                sb.Append("\r\n" + "</tr>");
                
                sb.Append("\r\n" + "</table>");
            }

            sb.Append(Ui.CreateFlexiGrid("jqgrid", sTableName, cid));

            return Parser.SaveScriptCodes(sb.ToString()); // evitamos que se formateen los datos.
        }
    }
}