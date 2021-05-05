// <fileheader>
// <copyright file="deleterecord.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: admin\editor\deleterecord.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Text;
using FSPortal;
using FSDatabase;
using FSNetwork;
using FSLibrary;
using System.Data;

namespace FSPaginas.Admin.Editor
{
    public class DeleteRecord : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            bool error = false;
            StringBuilder sb = new StringBuilder("");
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            string sTableName = Web.Request("tablename");
            string sFieldName = Web.Request("fld");
            string sFieldValue = Web.Request("val");


            if ((Web.Request("sure") != "") & Variables.User.bRecDel)
            {
                if (Variables.App.UseXML)
                {
                    DataRow row;
                    XML xml = new XML(Variables.App.directorioWeb + "data");
                    xml.Load(sTableName + ".xml");
                    if(NumberUtils.IsNumeric(sFieldValue))
                        row = xml.SelectRow(sFieldName + "=" + sFieldValue);
                    else
                        row = xml.SelectRow(sFieldName + "='" + sFieldValue + "'");

                    xml.DeleteRow(row);
                    error = !xml.Save();
                }
                else
                {
                    string sFieldType = db.GetField(sFieldName, sTableName).Tipo.ToString();
                    //Functions.ValorRequest("fldtype");


                    string sWhere = Utils.DameWhere(sFieldName, sFieldType, sFieldValue);

                    string ssql = "DELETE FROM [" + sTableName + "]" + " WHERE " + sWhere;

                    try
                    {
                        db.Execute(ssql);
                    }
                    catch (System.Exception e)
                    {
                        sb.Append("\r\n" + "<p>" + e.Message + "</p>" + Ui.Lf());
                        error = true;
                    }
                }


                if (error)
                    sb.Append("\r\n" +
                              "<p>Se han registrado problemas al borrar el registro. Intentelo de nuevo más tarde.</p>");
                else
                    sb.Append("\r\n" + "<p>Registro eliminado.</p>");

                sb.Append("\r\n" + @"<a href=""javascript:history.go(-3);"">Volver</a>");
            }
            else
            {
                sb.Append("\r\n" + @"<table border=""0"" cellspacing=""1"" cellpadding=""2"" width=""100%"">");
                sb.Append("\r\n" + "<tr>");
                sb.Append("\r\n" + "<td>");

                sb.Append("\r\n" + @"<img border=""0"" src=""");
                sb.Append("\r\n" + Variables.App.directorioPortal + @"imagenes/bullet.gif""> <a href=""listtables.aspx?");
                sb.Append("\r\n" + @""">Portal</a> <img border=""0"" src=""");
                sb.Append("\r\n" + Variables.App.directorioPortal + @"imagenes/bullet.gif""> <a href=""showtable.aspx?");
                sb.Append("\r\n" + "tablename=" + Server.UrlEncode(Request["tablename"]) + "&amp;page=");
                sb.Append("\r\n" + Web.RequestInt("page") + (Variables.User.bQueryExec ? "&amp;q=1" : "") +
                          ">Tabla [" + Request["tablename"] + @"]</a> <img border=""0"" src=""");
                sb.Append("\r\n" + Variables.App.directorioPortal + @"imagenes/bullet.gif""> Borrar registro");

                sb.Append("\r\n" + "</td>");
                sb.Append("\r\n" + @"<td width=""130"" align=""right"">");

                sb.Append("\r\n" + "</td>");
                sb.Append("\r\n" + "</tr>");
                sb.Append("\r\n" + "</table>");
                sb.Append("\r\n" + Ui.Lf());

                sb.Append("\r\n" + "<p>¿Estás seguro de querer borrar el registro " + sFieldValue +
                          " de la tabla '" + sTableName + "'?</p>");
                sb.Append("\r\n" + @"<a href=""deleterecord.aspx?" + Request.QueryString +
                          @"&amp;sure=1"">Si</a>&nbsp;");
                sb.Append("\r\n" + @"<a href=""javascript:history.back();"">No</a>");
            }

            return sb.ToString();
        }
    }
}