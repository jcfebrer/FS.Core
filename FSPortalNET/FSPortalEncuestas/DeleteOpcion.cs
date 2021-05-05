using FSDatabase;
using FSPortal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSPortalEncuestas
{
    class DeleteOpcion : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }
        public string Inicio()
        {

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            long lOpcionID = Convert.ToInt32(Request["ID"]);
            db.Execute("DELETE From " + Variables.App.prefijoTablas + "OpcionesEncuesta WHERE OpcionID = " + lOpcionID);
            db.Execute("DELETE FROM " + Variables.App.prefijoTablas + "RespuestasEncuestas WHERE OpcionID = " + lOpcionID);


            return "Opciones eliminadas de la eni/cuesta: " + lOpcionID;
        }
    }
}
