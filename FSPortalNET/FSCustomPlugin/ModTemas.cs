using FSDatabase;
using FSPlugin;
using FSPortal;
using System.Data;

namespace FSCustomPlugin
{
    public class ModTemas : IPlugin
    {
        public string Execute(params string[] p)
        {
            return Temas();
        }

        public string Name
        {
            get { return "ModTemas"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string Temas()
        {
            string modTemasReturn = "" + "\r\n";

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            string ssql;
            if (Utils.BDType == Utils.TypeBd.MySQL)
            {
                ssql = "SELECT temaid,nombre FROM " + Variables.App.prefijoTablas + "Temas order by PaginasVistas DESC LIMIT 10";
            }
            else
            {
                ssql = "SELECT top 10 temaid,nombre FROM " + Variables.App.prefijoTablas + "Temas order by PaginasVistas DESC";
            }
            DataTable dtTemas = db.Execute(ssql);
            foreach (DataRow row in dtTemas.Rows)
            {
                modTemasReturn = modTemasReturn + @"<img border=""0"" src='" + Variables.App.directorioPortal +
                                 "imagenes/bullet.gif' alt='' /> <a href='" + Variables.App.directorioPortal +
                                 "temas/tema.aspx?pid=" + row["temaid"] + "'>" + row["nombre"] + "</a>" + Ui.Lf() +
                                 "\r\n";
            }

            modTemasReturn = modTemasReturn + @"<img border=""0"" src='" + Variables.App.directorioPortal +
                             "imagenes/bullet.gif' alt='' />&nbsp;<b><a href='" + Variables.App.directorioPortal +
                             "temas/default.aspx'>Todos los temas</a></b>" + Ui.Lf() + "\r\n";
            modTemasReturn = modTemasReturn + Ui.Lf() + "\r\n";
            return modTemasReturn;
        }
    }
}