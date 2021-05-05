using FSDatabase;
using FSPlugin;
using FSPortal;
using System.Data;

namespace FSCustomPlugin
{
    public class ModUltimasDescargas : IPlugin
    {
        public string Execute(params string[] p)
        {
            return UltimasDescargas();
        }

        public string Name
        {
            get { return "ModUltimasDescargas"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string UltimasDescargas()
        {
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            string modUltimasDescargasReturn = @"<table width=""100%"" border=""0"" cellspacing=""1"" cellpadding=""4"">" +
                                        "\r\n";
            modUltimasDescargasReturn = modUltimasDescargasReturn + "<tr>" + "\r\n";
            modUltimasDescargasReturn = modUltimasDescargasReturn + @"<td width=""50%"" align=""center"">";
            modUltimasDescargasReturn = modUltimasDescargasReturn + "Fichero" + "</td>" + "\r\n";
            modUltimasDescargasReturn = modUltimasDescargasReturn + @"<td width=""50%"" align=""center"">";
            modUltimasDescargasReturn = modUltimasDescargasReturn + "Fecha" + "</td>" + "\r\n";
            modUltimasDescargasReturn = modUltimasDescargasReturn + "</tr>" + "\r\n";

            string ssql;

            if (Utils.BDType == Utils.TypeBd.MySQL)
            {
                ssql = "SELECT fechaInsercion,nombre,temaid,ficheroid FROM " + Variables.App.prefijoTablas +
                       "Ficheros ORDER BY fechaInsercion DESC LIMIT 10";
            }
            else
            {
                ssql = "SELECT Top 10 fechaInsercion,nombre,temaid,ficheroid FROM " + Variables.App.prefijoTablas +
                       "Ficheros ORDER BY fechaInsercion DESC";
            }

            DataTable dtDescargas = db.Execute(ssql);
            foreach (DataRow row in dtDescargas.Rows)
            {
                modUltimasDescargasReturn = modUltimasDescargasReturn + "<tr>" + "\r\n";
                modUltimasDescargasReturn = modUltimasDescargasReturn + @"<td width=""50%"">";
                modUltimasDescargasReturn = modUltimasDescargasReturn +
                                            Ui.Link(row["nombre"].ToString(),
                                                Variables.App.directorioPortal + "temas/detalleFichero.aspx?pid=" + row["temaID"] +
                                                "&amp;rid=" + row["FicheroID"]);
                modUltimasDescargasReturn = modUltimasDescargasReturn + "</td>" + "\r\n";
                modUltimasDescargasReturn = modUltimasDescargasReturn + @"<td width=""50%"" align=""center"">";
                modUltimasDescargasReturn = modUltimasDescargasReturn + row["fechaInsercion"] + "</td>" + "\r\n";
                modUltimasDescargasReturn = modUltimasDescargasReturn + "</tr>" + "\r\n";
            }
            modUltimasDescargasReturn = modUltimasDescargasReturn + "</table>" + "\r\n";

            return modUltimasDescargasReturn;
        }
    }
}