using FSDatabase;
using FSLibrary;
using FSPlugin;
using FSPortal;
using System.Data;

namespace FSCustomPlugin
{
    public class ModNoticias : IPlugin
    {
        public string Execute(params string[] p)
        {
            return Noticias();
        }

        public string Name
        {
            get { return "ModNoticias"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string Noticias()
        {
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            string modNoticiasReturn = "";

            string ssql = "SELECT noticiaID,titulo,autor,fechaInicio,textocorto,textoLargo FROM " + Variables.App.prefijoTablas +
                          "Noticias Where publicar=true and fechaInicio<=" + Utils.FormatShortDate(System.DateTime.Now) +
                          " and fechaFin>=" + Utils.FormatShortDate(System.DateTime.Now) + " ORDER BY FechaInicio DESC";

            DataTable dtNoticias = db.Execute(ssql);

            if (dtNoticias.Rows.Count == 0)
            {
                modNoticiasReturn = modNoticiasReturn + "No hay noticias.";
            }

            foreach (DataRow row in dtNoticias.Rows)
            {
                modNoticiasReturn = modNoticiasReturn +
                                    @"<table cellpadding=""2"" cellspacing=""0"" width=""100%"" border=""0""><tr>";
                modNoticiasReturn = modNoticiasReturn + @"<td class=""textopeque"">";
                modNoticiasReturn = modNoticiasReturn + @"<img border=""0"" src='" + Variables.App.directorioPortal +
                                    "imagenes/bullet.gif' alt='' />";

                modNoticiasReturn = modNoticiasReturn + "&nbsp;" +
                                    Ui.EditPage("Noticias", "NoticiaID", row["NoticiaID"].ToString(), "Editar Noticia",
                                        "Borrar Noticia");

                if (Functions.Valor(row["textoLargo"]) != "")
                {
                    modNoticiasReturn = modNoticiasReturn + @"&nbsp;<b><a href=""" + Variables.App.directorioPortal +
                                        "detalleNoticia.aspx?id=" + row["noticiaID"] + @""">" + row["Titulo"] +
                                        "</a></b>";
                }
                else
                {
                    modNoticiasReturn = modNoticiasReturn + "&nbsp;<b>" + row["Titulo"] + "</b>";
                }

                if (Functions.Valor(row["autor"]) != "")
                {
                    modNoticiasReturn = modNoticiasReturn + "<em> (por " + row["Autor"] + ")</em>";
                }
                modNoticiasReturn = modNoticiasReturn + @"</td><td align=""right"">";
                modNoticiasReturn = modNoticiasReturn + row["FechaInicio"];
                modNoticiasReturn = modNoticiasReturn + @"</td></tr><tr><td class=""textomaspeque"" colspan=""2"">";
                modNoticiasReturn = modNoticiasReturn + row["TextoCorto"] + "<hr />";
                modNoticiasReturn = modNoticiasReturn + "</td></tr></table>";
                modNoticiasReturn = modNoticiasReturn + Ui.Lf();
            }


            if (Variables.User.Administrador)
            {
                modNoticiasReturn = modNoticiasReturn + Ui.Lf() + Ui.Lf() +
                                    Ui.Link("Añadir noticia",
                                        Variables.App.directorioPortal +
                                        "admin/editor/showrecord.aspx?tablename=Noticias&amp;add=1&amp;page=1");
            }

            modNoticiasReturn = modNoticiasReturn +
                                Ui.Right(@"<a href=""" + Variables.App.directorioPortal +
                                         @"xml/news.aspx"" target=""_blank""><img src=""" + Variables.App.directorioPortal +
                                         @"imagenes/rss.gif"" alt=""RSS"" border=""0"" /></a>");
            return modNoticiasReturn;
        }
    }
}