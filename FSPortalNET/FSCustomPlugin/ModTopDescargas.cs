using FSDatabase;
using FSPlugin;
using FSPortal;
using System.Data;

namespace FSCustomPlugin
{
    public class ModTopDescargas : IPlugin
    {
        public string Execute(params string[] p)
        {
            return TopDescargas();
        }

        public string Name
        {
            get { return "ModTopDescargas"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string TopDescargas()
        {
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            string modTopDescargasReturn = @"<table width=""100%"" border=""0"" cellspacing=""1"" cellpadding=""4"">" + "\r\n";
            modTopDescargasReturn = modTopDescargasReturn + "<tr>" + "\r\n";
            modTopDescargasReturn = modTopDescargasReturn + @"<td width=""50%"" align=""center"">";
            modTopDescargasReturn = modTopDescargasReturn + "Fichero" + "</td>" + "\r\n";
            modTopDescargasReturn = modTopDescargasReturn + @"<td width=""50%"" align=""center"">";
            modTopDescargasReturn = modTopDescargasReturn + "Descargas" + "</td>" + "\r\n";
            modTopDescargasReturn = modTopDescargasReturn + "</tr>" + "\r\n";

            string ssql;

            if (Utils.BDType == Utils.TypeBd.MySQL)
            {
                ssql = "SELECT descargas,nombre,temaid,ficheroid FROM " + Variables.App.prefijoTablas +
                       "Ficheros ORDER BY descargas DESC LIMIT 10";
            }
            else
            {
                ssql = "SELECT Top 10 descargas,nombre,temaid,ficheroid FROM " + Variables.App.prefijoTablas +
                       "Ficheros ORDER BY descargas DESC";
            }


            DataTable dtDescargas = db.Execute(ssql);
            foreach (DataRow row in dtDescargas.Rows)
            {
                modTopDescargasReturn = modTopDescargasReturn + "<tr>" + "\r\n";
                modTopDescargasReturn = modTopDescargasReturn + @"<td width=""50%"">";
                modTopDescargasReturn = modTopDescargasReturn +
                                        Ui.Link(row["nombre"].ToString(),
                                            Variables.App.directorioPortal + "temas/detalleFichero.aspx?pid=" + row["temaID"] +
                                            "&amp;rid=" + row["FicheroID"]);
                modTopDescargasReturn = modTopDescargasReturn + "</td>" + "\r\n";
                modTopDescargasReturn = modTopDescargasReturn + @"<td width=""50%"" align=""center"">";
                modTopDescargasReturn = modTopDescargasReturn + row["descargas"] + "</td>" + "\r\n";
                modTopDescargasReturn = modTopDescargasReturn + "</tr>" + "\r\n";
            }
            modTopDescargasReturn = modTopDescargasReturn + "</table>" + "\r\n";

            return modTopDescargasReturn;
        }
    }
}