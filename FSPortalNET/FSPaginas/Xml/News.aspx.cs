// <fileheader>
// <copyright file="news.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: xml\news.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using FSDatabase;
using FSLibrary;
using FSPortal;
using System;
using System.Data;
using System.Text;

namespace FSPaginas.Xml
{
    public class News : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            simple = true;
            contenido = Inicio();
        }

        public string Inicio()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<?xml version=""1.0"" encoding=""ISO-8859-1""?>");
            sb.AppendLine(@"<rss version=""2.0"">");
            sb.AppendLine("<channel>");
            sb.AppendLine("<title>");
            sb.AppendLine(Variables.App.nombreWeb);
            sb.AppendLine("</title>");
            sb.AppendLine("<link>");
            sb.AppendLine(Variables.App.webHttp);
            sb.AppendLine("</link>");
            sb.AppendLine("<description>");
            sb.AppendLine(Variables.App.descripcionWeb);
            sb.AppendLine("</description>");
            sb.AppendLine("<copyright>Copyright");
            sb.AppendLine(System.DateTime.Now.Year.ToString());
            sb.AppendLine("- Febrer Software - http://www.febrersoftware.com - Todos los derechos reservados.</copyright>");
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            if (db.TableExists((Variables.App.prefijoTablas + "Noticias")))
            {
                string sSQL = ("SELECT noticiaID,titulo,autor,fechaInicio,textocorto,textoLargo FROM "
                            + (Variables.App.prefijoTablas + ("Noticias Where publicar=true and fechaInicio<="
                            + (Utils.FormatShortDate(System.DateTime.Now) + (" and fechaFin>="
                            + (Utils.FormatShortDate(System.DateTime.Now) + " ORDER BY FechaInicio DESC"))))));
                DataTable dt = db.Execute(sSQL);
                foreach (DataRow row in dt.Rows)
                {
                    sb.AppendLine(("<item>" + "\r\n"));
                    sb.AppendLine(("<title>"
                                    + (row["titulo"].ToString() + ("</title>" + "\r\n"))));
                    if ((Functions.Valor(row["textoLargo"]) != ""))
                    {
                        sb.AppendLine(("<link>"
                                        + (Variables.App.directorioPortal + ("detalleNoticia.aspx?id="
                                        + (NumberUtils.NumberDouble(row["NoticiaID"]) + ("</link>" + "\r\n"))))));
                    }

                    sb.AppendLine(("<description>"
                                    + (row["textocorto"].ToString() + ("<br /><br />"
                                    + (row["textolargo"].ToString() + ("</description>" + "\r\n"))))));
                    if (FSLibrary.DateTimeUtil.IsDate(row["fechaInicio"].ToString()))
                    {
                        sb.AppendLine(("<pubDate>"
                                        + (FSLibrary.DateTimeUtil.ToRFC_822(System.DateTime.Parse(row["fechaInicio"].ToString())) + ("</pubDate>" + "\r\n"))));
                    }

                    sb.AppendLine(("</item>" + "\r\n"));
                }

                Response.Cache.SetExpires(System.DateTime.Now);
            }
            sb.AppendLine("</channel>");
            sb.AppendLine("</rss>");
            return sb.ToString();
        }
    }
}