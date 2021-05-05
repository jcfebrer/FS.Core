using FSDatabase;
using FSPlugin;
using FSPortal;
using System;
using System.Data;

namespace FSCustomPlugin
{
    public class ModFrases : IPlugin
    {
        public string Execute(params string[] p)
        {
            return Frases();
        }

        public string Name
        {
            get { return "ModFrases"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string Frases()
        {
            Random r = new Random();

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            string ssql = "SELECT frase,tipo from " + Variables.App.prefijoTablas + "Frases where idFrase=" + r.Next(4600);
            DataTable dtFrases = db.Execute(ssql);

            string modFrasesReturn = "" + "\r\n";
            modFrasesReturn = modFrasesReturn + @"<img border=""0"" src='" + Variables.App.directorioPortal +
                              "imagenes/bullet.gif' alt='' /><b>" + dtFrases.Rows[0]["Tipo"] + ":</b>" + Ui.Lf() +
                              dtFrases.Rows[0]["Frase"] + "\r\n";

            return modFrasesReturn;
        }
    }
}