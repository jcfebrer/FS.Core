using FSDatabase;
using FSPlugin;
using FSPortal;
using System.Data;

namespace FSCustomPlugin
{
    public class ModFicherosRecomendados : IPlugin
    {
        public string Execute(params string[] p)
        {
            return FicherosRecomendados();
        }

        public string Name
        {
            get { return "ModFicherosRecomendados"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string FicherosRecomendados()
        {
            string modFicherosRecomendadosReturn = @"<table width=""100%"" border=""0"" cellspacing=""1"" cellpadding=""4"">" +
                                            "\r\n";
            modFicherosRecomendadosReturn = modFicherosRecomendadosReturn + "<tr>" + "\r\n";
            modFicherosRecomendadosReturn = modFicherosRecomendadosReturn + @"<td width=""50%"" align=""center"">";
            modFicherosRecomendadosReturn = modFicherosRecomendadosReturn + "Fichero" + "</td>" + "\r\n";
            modFicherosRecomendadosReturn = modFicherosRecomendadosReturn + @"<td width=""50%"" align=""center"">";
            modFicherosRecomendadosReturn = modFicherosRecomendadosReturn + "Fecha" + "</td>" + "\r\n";
            modFicherosRecomendadosReturn = modFicherosRecomendadosReturn + "</tr>" + "\r\n";

            string ssql = "SELECT fechaInsercion,nombre,temaid,ficheroid FROM " + Variables.App.prefijoTablas +
                          "Ficheros where recomendado=true ORDER BY fechaInsercion DESC";
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            DataTable dtFicheros = db.Execute(ssql);
            foreach (DataRow row in dtFicheros.Rows)
            {
                modFicherosRecomendadosReturn = modFicherosRecomendadosReturn + "<tr>" + "\r\n";
                modFicherosRecomendadosReturn = modFicherosRecomendadosReturn + @"<td width=""50%"">";
                modFicherosRecomendadosReturn = modFicherosRecomendadosReturn +
                                                Ui.Link(row["nombre"].ToString(),
                                                    Variables.App.directorioPortal + "temas/detalleFichero.aspx?pid=" +
                                                    row["temaID"] + "&amp;rid=" + row["FicheroID"]);
                modFicherosRecomendadosReturn = modFicherosRecomendadosReturn + "</td>" + "\r\n";
                modFicherosRecomendadosReturn = modFicherosRecomendadosReturn + @"<td width=""50%"" align=""center"">";
                modFicherosRecomendadosReturn = modFicherosRecomendadosReturn + row["fechaInsercion"] + "</td>" + "\r\n";
                modFicherosRecomendadosReturn = modFicherosRecomendadosReturn + "</tr>" + "\r\n";
            }
            modFicherosRecomendadosReturn = modFicherosRecomendadosReturn + "</table>" + "\r\n";

            return modFicherosRecomendadosReturn;
        }
    }
}