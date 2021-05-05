using FSDatabase;
using FSPlugin;
using FSPortal;
using System.Data;

namespace FSCustomPlugin
{
    public class ModRadio : IPlugin
    {
        public string Execute(params string[] p)
        {
            return Radio();
        }

        public string Name
        {
            get { return "ModRadio"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string Radio()
        {
            string modRadioReturn = "<form action='' name='frmRadio' id='frmRadio'>" + "\r\n";
            modRadioReturn = modRadioReturn +
                             "<select name='radioURL' class='textboxplano' onchange='open_radio(frmRadio.radioURL.value," +
                             Variables.App.directorioPortal + ");'>" + "\r\n";
            modRadioReturn = modRadioReturn + "<option value='1'>Selecciona ...</option>" + "\r\n";

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            string ssql = "SELECT idEmisora,nombreEmisora from " + Variables.App.prefijoTablas + "Emisoras";
            DataTable dtRadio = db.Execute(ssql);
            foreach (DataRow row in dtRadio.Rows)
            {
                modRadioReturn = modRadioReturn + "<option value='" + row["idEmisora"] + "'>" + row["nombreEmisora"] +
                                 "</option>" + "\r\n";
            }

            modRadioReturn = modRadioReturn + "</select>" + "\r\n";
            modRadioReturn = modRadioReturn + "</form>" + "\r\n";
            return modRadioReturn;
        }
    }
}