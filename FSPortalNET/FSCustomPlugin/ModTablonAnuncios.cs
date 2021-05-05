using FSDatabase;
using FSLibrary;
using FSPlugin;
using FSPortal;
using System.Data;

namespace FSCustomPlugin
{
    public class ModTablonAnuncios : IPlugin
    {
        public string Execute(params string[] p)
        {
            return TablonAnuncios();
        }

        public string Name
        {
            get { return "ModTablonAnuncios"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string TablonAnuncios()
        {
            string ssql = "SELECT anuncioID,titulo,autor,fecha,texto FROM " + Variables.App.prefijoTablas +
                          "tablonAnuncios Where publicar=true ORDER BY Fecha DESC";
            string modTablonAnunciosReturn = "";

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            DataTable dtTablon = db.Execute(ssql);

            if (dtTablon.Rows.Count == 0)
            {
                modTablonAnunciosReturn = modTablonAnunciosReturn + "No hay anuncios.";
            }

            foreach (DataRow row in dtTablon.Rows)
            {
                modTablonAnunciosReturn = modTablonAnunciosReturn +
                                          @"<table cellpadding=""2"" cellspacing=""0"" width=""100%"" border=""0""><tr>";
                modTablonAnunciosReturn = modTablonAnunciosReturn + @"<td class=""textopeque"">";
                modTablonAnunciosReturn = modTablonAnunciosReturn + @"<img border=""0"" src='" + Variables.App.directorioPortal +
                                          "imagenes/bullet.gif' alt='' />";

                modTablonAnunciosReturn = modTablonAnunciosReturn + "&nbsp;" +
                                          Ui.EditPage("tablonAnuncios", "AnuncioID", row["AnuncioID"].ToString(),
                                              "Editar Anuncio", "Borrar Anuncio");
                modTablonAnunciosReturn = modTablonAnunciosReturn + "&nbsp;<b>" + row["Titulo"] + "</b>";
                if (Functions.Valor(row["autor"]) != "")
                {
                    modTablonAnunciosReturn = modTablonAnunciosReturn + "<em> (por " + row["Autor"] + ")</em>";
                }
                modTablonAnunciosReturn = modTablonAnunciosReturn + @"</td><td align=""right"">";
                modTablonAnunciosReturn = modTablonAnunciosReturn + row["Fecha"];
                modTablonAnunciosReturn = modTablonAnunciosReturn +
                                          @"</td></tr><tr><td class=""textomaspeque"" colspan=""2"">";
                modTablonAnunciosReturn = modTablonAnunciosReturn + row["Texto"] + "<hr />";
                modTablonAnunciosReturn = modTablonAnunciosReturn + "</td></tr></table>";
                modTablonAnunciosReturn = modTablonAnunciosReturn + Ui.Lf();
            }

            if (Variables.User.Usuario != "")
            {
                modTablonAnunciosReturn = modTablonAnunciosReturn + Ui.Lf() + Ui.Lf() +
                                          Ui.Link("Añadir anuncio",
                                              Variables.App.directorioPortal + @"tablonAnuncios/nuevoAnuncio.aspx");
            }
            else
            {
                modTablonAnunciosReturn = modTablonAnunciosReturn + Ui.Lf() + Ui.Lf() +
                                          "Debes registrarte si deseas añadir un anuncio.";
            }

            return modTablonAnunciosReturn;
        }
    }
}