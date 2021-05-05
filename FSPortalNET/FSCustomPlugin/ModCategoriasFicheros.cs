using FSDatabase;
using FSPlugin;
using FSPortal;
using System.Data;

namespace FSCustomPlugin
{
    public class ModCategoriasFicheros : IPlugin
    {
        public string Execute(params string[] p)
        {
            return CategoriasFicheros();
        }

        public string Name
        {
            get { return "ModCategoriasFicheros"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string CategoriasFicheros()
        {
            string modCategoriasFicherosReturn = @"<table width=""100%"" border=""0"" cellspacing=""1"" cellpadding=""4"">" +
                                          "\r\n";
            modCategoriasFicherosReturn = modCategoriasFicherosReturn + "<tr>" + "\r\n";
            modCategoriasFicherosReturn = modCategoriasFicherosReturn + @"<td width=""50%"" align=""center"">";
            modCategoriasFicherosReturn = modCategoriasFicherosReturn + "Categoria" + "</td>" + "\r\n";
            modCategoriasFicherosReturn = modCategoriasFicherosReturn + @"<td width=""50%"" align=""center"">";
            modCategoriasFicherosReturn = modCategoriasFicherosReturn + "Ficheros" + "</td>" + "\r\n";
            modCategoriasFicherosReturn = modCategoriasFicherosReturn + "</tr>" + "\r\n";

            string ssql = "SELECT " + Variables.App.prefijoTablas + "Categorias_Ficheros.Descripcion, " + Variables.App.prefijoTablas +
                          "Categorias_Ficheros.idCategoria, " + "Count(" + Variables.App.prefijoTablas +
                          "Ficheros.FicheroID) AS totalFicheros " + "FROM " + Variables.App.prefijoTablas +
                          "Categorias_Ficheros INNER JOIN " + Variables.App.prefijoTablas + "Ficheros ON " + Variables.App.prefijoTablas +
                          "Categorias_Ficheros.idCategoria = " +
                          Variables.App.prefijoTablas + "Ficheros.idCategoria " + "GROUP BY " + Variables.App.prefijoTablas +
                          "Categorias_Ficheros.Descripcion, " + Variables.App.prefijoTablas + "Categorias_Ficheros.idCategoria " +
                          "ORDER BY Count(" + Variables.App.prefijoTablas + "Ficheros.FicheroID);";

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            DataTable dtCategorias = db.Execute(ssql);
            foreach (DataRow row in dtCategorias.Rows)
            {
                modCategoriasFicherosReturn = modCategoriasFicherosReturn + "<tr>" + "\r\n";
                modCategoriasFicherosReturn = modCategoriasFicherosReturn + @"<td width=""50%"">";
                modCategoriasFicherosReturn = modCategoriasFicherosReturn +
                                              Ui.Link(row["descripcion"].ToString(),
                                                  Variables.App.directorioPortal + "temas/ficherosCategoria.aspx?cat=" +
                                                  row["idCategoria"]);
                modCategoriasFicherosReturn = modCategoriasFicherosReturn + "</td>" + "\r\n";
                modCategoriasFicherosReturn = modCategoriasFicherosReturn + @"<td width=""50%"" align=""center"">";
                modCategoriasFicherosReturn = modCategoriasFicherosReturn + row["totalFicheros"] + "</td>" + "\r\n";
                modCategoriasFicherosReturn = modCategoriasFicherosReturn + "</tr>" + "\r\n";
            }
            modCategoriasFicherosReturn = modCategoriasFicherosReturn + "</table>" + "\r\n";

            return modCategoriasFicherosReturn;
        }
    }
}