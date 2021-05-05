// <fileheader>
// <copyright file="listtables.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: admin\editor\listtables.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using System.Text;
using FSPortal;
using System.Configuration;
using FSNetwork;
using FSDatabase;

namespace FSPaginas.Admin.Editor
{

    public class ListTables : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            int cid = Web.RequestInt("cid");
            StringBuilder sb = new StringBuilder("");


            sb.Append(@"<table border=""0"" cellspacing=""1"" cellpadding=""2"" width=""100%"">");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td>");
            sb.Append("\r\n" + @"<img border=""0"" src=""" + Variables.App.directorioPortal +
                      @"imagenes/bullet.gif"" alt="""" />Portal");
            sb.Append("\r\n" + "</td>");
            sb.Append("\r\n" + @"<td width=""130"" align=""right"">");

            sb.Append("\r\n" + "</td>");
            sb.Append("\r\n" + "</tr>");

            if (ConfigurationManager.ConnectionStrings.Count > 0)
            {
                sb.Append("<tr>");
                sb.Append("<td class='cabemaspeque'>Selecciona la conexión:</td>");
                sb.Append(@"<td class='cabemaspeque'>");
                sb.Append(@"<select id=""conexionSel"">");
                int id = 0;
                foreach (ConnectionStringSettings con in ConfigurationManager.ConnectionStrings)
                {
                    if (con.ConnectionString.ToString().Trim() != "")
                    {
                        sb.Append(@"<option value=""" + id.ToString() + @""">" + con.Name.ToString() + "</option>");
                    }
                    id++;
                }
                sb.Append(Ui.Button("Seleccionar",
                                      "window.location='listtables.aspx?cid=' + conexionSel.value;"));
                sb.Append("</td></tr>");
            }
            sb.Append("\r\n" + "</table>");

            sb.Append("\r\n" + "<p>");
            sb.Append("\r\n" + "Seleccionar tabla");

            sb.Append("\r\n" + "</p>");

            DataTable dtS;
            DataTable dtSView;

            if (Variables.App.UseXML)
            {
                XML xml = new XML(Variables.App.directorioWeb + "data");
                dtS = xml.GetSchemaTables();
                dtSView = null;
            }
            else
            {
                if (cid == 0)
                {
                    cid = Utils.GetConnectionId(Variables.App.defaultEntry);
                }

                BdUtils db = new BdUtils(cid);
                dtS = db.GetSchemaTables();
                dtSView = db.GetSchemaView();
            }

            sb.Append("\r\n" +
                      @"<table class=""tablesorter"" id=""tableList"" border=""0"" cellspacing=""1"" cellpadding=""2"" width=""100%"">");

            sb.Append("\r\n" + "<thead><tr>");
            sb.Append("\r\n" + "<th>Tablas</th>");
            sb.Append("\r\n" + @"<th>Acción</th>");
            sb.Append("\r\n" + "</tr></thead>");

            sb.Append("\r\n" + "<tbody>");
            //bool haz = true;
            foreach (DataRow r in dtS.Rows)
            {
                string tableName =  r["TABLE_NAME"].ToString();
  
                sb.Append("\r\n" + "<tr>");
                sb.Append("\r\n" + @"<td>");
                sb.Append(Ui.Button(tableName,
                    "window.location='showtable.aspx?cid=" + cid + "&tablename=" + tableName + "'"));
                sb.Append("</td>");
                sb.Append("\r\n" + @"<td align=""center"">");
                sb.Append("\r\n" +
                          Ui.Button("Añadir",
                              "window.location='showrecord.aspx?cid=" + cid + "&simple=0&tablename=" + tableName +
                              @"&add=1&page=1'"));
                sb.Append("\r\n" +
                          Ui.Button("Buscar",
                              "window.location='searchtable.aspx?cid=" + cid + "&tablename=" + tableName + "'"));
                sb.Append("\r\n" +
                          Ui.Button("Borrar",
                              "window.location='deletetable.aspx?cid=" + cid + "&tablename=" + tableName + "'"));
                sb.Append("\r\n" + "</td>");
                sb.Append("\r\n" + "</tr>");
            }

            sb.Append("\r\n" + "</tbody>");
            sb.Append("\r\n" + "</table>");


            if (Variables.User.bQueryExec && !Variables.App.UseXML)
            {
                sb.Append("\r\n" + Ui.Lf() +
                          @"<table class=""tablesorter"" id=""tableList2"" border=""0"" cellspacing=""1"" cellpadding=""2"" width=""100%"">");

                sb.Append("\r\n" + "<thead><tr>");

                sb.Append("\r\n" + "<th>Consultas</th>");
                sb.Append("\r\n" + "</tr></thead>");

                sb.Append("\r\n" + "<tbody>");

                
                foreach (DataRow r in dtSView.Rows)
                {
                    string tableName;

                    if (Utils.BDType == Utils.TypeBd.Oracle)
                    {
                        tableName = r["VIEW_NAME"].ToString();
                    }
                    else
                        tableName = r["TABLE_NAME"].ToString();

                    sb.Append("\r\n" + "<tr>");
                    sb.Append("\r\n" + "<td>");
                    sb.Append(Ui.Button(tableName,
                        "window.location='showtable.aspx?cid=" + cid + "&tablename=" + tableName + "'"));
                    sb.Append("</td>");
                    
                    sb.Append("\r\n" + "<td>");
                    sb.Append("\r\n" +
                              Ui.Button("Buscar",
                                  "window.location='searchtable.aspx?cid=" + cid + "&tablename=" + tableName + "'"));
                    sb.Append("\r\n" +
                              Ui.Button("Borrar",
                                  "window.location='deletetable.aspx?cid=" + cid + "&tablename=" + tableName + "'"));
                    sb.Append("\r\n" + "</td>");
                    
                    sb.Append("\r\n" + "</tr>");
                }

                sb.Append("\r\n" + "</tbody>");
                sb.Append("\r\n" + "</table>");

                sb.Append("\r\n" + Ui.Lf() +
                          Ui.Button("Crear nueva consulta", "window.location='createview.aspx?cid=" + cid + "'"));
                sb.Append("\r\n" + Ui.Lf() +
                          Ui.Button("Ejecutar sentencia SQL", "window.location='executecommand.aspx?cid=" + cid + "'"));
            }

            return sb.ToString();
        }
    }
}