using FSDatabase;
using FSLibrary;
using FSPlugin;
using FSPortal;
using System.Data;

namespace FSCustomPlugin
{
    public class ModRSS : IPlugin
    {
        public string Execute(params string[] p)
        {
            return Rss();
        }

        public string Name
        {
            get { return "ModRSS"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string Rss()
        {
            string modRssReturn = @"<form method=""post"" action='" + Variables.App.directorioPortal +
                           "rss/rss.aspx' name='frmRSS' id='frmRSS'>" + "\r\n";
            modRssReturn = modRssReturn + Ui.Lf() + @"Selecciona un sitio: " + Ui.Lf() +
                           @"<select style=""width: 150px"" name='rssURL' class='textboxplano' onchange='submit()'>" +
                           "\r\n";
            modRssReturn = modRssReturn + "<option>&nbsp;</option>" + "\r\n";

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            string ssql = "SELECT link,titulo from " + Variables.App.prefijoTablas + "RSS";
            DataTable dtRss = db.Execute(ssql);
            foreach (DataRow row in dtRss.Rows)
            {
                modRssReturn = modRssReturn + "<option value='" + row["link"] + "'>" +
                               TextUtil.Substring(Functions.Valor(row["titulo"]), 0, 20) + "</option>" + "\r\n";
            }

            modRssReturn = modRssReturn + "</select>" + "\r\n";
            modRssReturn = modRssReturn + Ui.Lf() + "RSS Url:" + Ui.Lf() +
                           "<input type='text' name='rssURL2' size='10' />" + Ui.Lf() +
                           "<input type='submit' name='cmdRSS' class='botonplano' value='Ir' />" + Ui.Lf() + "\r\n";
            modRssReturn = modRssReturn + "</form>" + "\r\n";
            return modRssReturn;
        }
    }
}