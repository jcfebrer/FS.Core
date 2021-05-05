// <fileheader>
// <copyright file="searchtable.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: admin\editor\searchtable.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using System.Text;
using FSPortal;
using FSLibrary;
using FSNetwork;
using FSDatabase;

namespace FSPaginas.Admin.Editor
{
    public class SearchTable : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            int cid = Web.RequestInt("cid");
            StringBuilder s = new StringBuilder("");
            BdUtils db = new BdUtils(cid);

            s.Append(@"<table border=""0"" cellspacing=""1"" cellpadding=""2"" width=""100%"">");
            s.Append("<tr>");
            s.Append("<td>");
            s.Append(@"<img border=""0"" src=""" + Variables.App.directorioPortal +
                     @"imagenes/bullet.gif"" alt="""" /> <a href=""listtables.aspx?cid=" + cid + @""">Portal</a> <img border=""0"" src=""" +
                     Variables.App.directorioPortal + @"imagenes/bullet.gif"" alt="""" /> <a href=""showtable.aspx?cid=" +
                     cid + "&amp;tablename=" + Web.Request(
                         "tablename") + "&amp;page=" + Web.RequestInt("page") + @""">Tabla [" +
                     Web.Request("tablename") + @"]</a> <img border=""0"" src=""" + Variables.App.directorioPortal +
                     @"imagenes/bullet.gif"" alt="""" /> Buscar registro");
            s.Append("</td>");
            s.Append(@"<td width=""130"" align=""right"">");

            s.Append("</td>");
            s.Append("</tr>");
            s.Append("</table>");

            s.Append(@"<form action=""showtable.aspx?" + Request.QueryString +
                     @""" method=""post"" name=""searchForm"">");
            s.Append(@"<table border=""1"" cellspacing=""2"" cellpadding=""3"" width=""100%"">");

            string sTableName = Web.Request("tablename");

            //string sSql = "SELECT * FROM [" + sTableName + "]";

            //DataTable dtT = db.Execute(sSql);

            string sValue = "";

            DataTable dtTableColumns = null;
            if (Variables.App.UseXML)
            {
                XML xml = new XML(Variables.App.directorioWeb + "data");
                xml.Load(sTableName + ".xml");
                dtTableColumns = xml.GetSchema();
            }
            else
            {
                dtTableColumns = db.GetSchemaTable(sTableName);
            }

            StringBuilder sb = new StringBuilder("");

            foreach (DataRow fld in dtTableColumns.Rows)
            {
                string sEditable = Variables.User.bRecEdit ? "" : "disabled";
                string description = fld["ColumnName"].ToString();

                if (!Variables.App.UseXML)
                    description = db.GetDescription(sTableName, fld["ColumnName"].ToString());

                sb.Append("\r\n" + "<tr>");
                sb.Append("\r\n" + "<td>" + description + "</td>");
                switch (fld["DataType"].ToString().ToLower())
                {
                    case "system.byte[]":
                        sb.Append("\r\n" + "<td>Objeto OLE</td>");
                        break;
                    case "system.boolean":
                    case "system.sbyte":
                        sb.Append("\r\n" + "<td><input " + sEditable + @" type=""checkbox"" name=""" +
                                  fld["ColumnName"] + @"""");
                        sb.Append("\r\n" + "></td>");
                        break;
                    default:
                        if (sValue != "")
                        {
                            sValue = TextUtil.Replace(sValue, @"""", "&amp;quot;");
                        }
                        switch (fld["DataType"].ToString().ToLower())
                        {
                            case "system.int16":
                            case "system.int32":
                            case "system,int64":
                            case "system.integer":
                            case "system.double":

                                if (!Variables.App.UseXML && db.HasRelation(sTableName, fld["ColumnName"].ToString()))
                                {
                                    sb.Append("<td>" +
                                              Ui.DameFrmCombo(cid, sTableName, fld["ColumnName"].ToString(), sValue));
                                }
                                else
                                {
                                    sb.Append("\r\n" + @"<td><input size=""60"" " + sEditable +
                                              @" type=""text"" name=""" + fld["ColumnName"] + @""" value=""" +
                                              sValue + @"""/></td>");
                                }

                                break;
                            default:
                                sb.Append("\r\n" + "<td>");
                                sb.Append("\r\n" + "<input " + sEditable + @" size=""64"" type=""text"" name=""" +
                                          fld["ColumnName"] + @""" value=""" + sValue + @""" maxlength=""" +
                                          fld["ColumnSize"] + @""">");
                                sb.Append("\r\n" + "</td>");
                                break;
                        }

                        break;
                }


                sb.Append("\r\n" + "</tr>");
            }
            s.Append(sb);


            s.Append("<tr>");
            s.Append("<td>&nbsp;</td>");
            s.Append(
                @"<td><input type=""checkbox"" name=""chkSubstring""> Cadena de búsqueda (solo para campos no numéricos)</td>");
            s.Append("</tr>");

            s.Append("<tr>");
            s.Append("<td>&nbsp;</td>");
            s.Append("<td>");
            s.Append(@"<input type=""submit"" name=""cmdSearch"" value=""  Buscar  "" />");
            s.Append("</td>");
            s.Append("</tr>");
            s.Append("</table>");
            s.Append("</form>");

            return s.ToString();
        }
    }
}