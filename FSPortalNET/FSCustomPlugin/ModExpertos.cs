using FSDatabase;
using FSPlugin;
using FSPortal;
using System.Data;

namespace FSCustomPlugin
{
    public class ModExpertos : IPlugin
    {
        public string Execute(params string[] p)
        {
            return Expertos();
        }

        public string Name
        {
            get { return "ModExpertos"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string Expertos()
        {
            string modExpertosReturn = "" + "\r\n";

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            string ssql;
            if (Utils.BDType == Utils.TypeBd.MySQL)
            {
                ssql = "SELECT usuario,nombre,apellido1,apellido2,sumadepuntos from " + Variables.App.prefijoTablas +
                       "v_RankingExpertos LIMIT 5";
            }
            else
            {
                ssql = "SELECT top 5 usuario,nombre,apellido1,apellido2,sumadepuntos from " + Variables.App.prefijoTablas +
                       "v_RankingExpertos";
            }

            DataTable dtExpertos = db.Execute(ssql);
            foreach (DataRow row in dtExpertos.Rows)
            {
                modExpertosReturn = modExpertosReturn + @"<img border=""0"" src='" + Variables.App.directorioPortal +
                                    "imagenes/bullet.gif' alt='' /> <a href='" + Variables.App.directorioPortal +
                                    "temas/fichaexperto.aspx?id=" + row["Usuario"] + "'>" + row["Nombre"] + " " +
                                    row["Apellido1"] + " " + row["Apellido2"] + " (" + row["sumadepuntos"] + ")</a>" +
                                    Ui.Lf() + "\r\n";
            }

            return modExpertosReturn;
        }
    }
}