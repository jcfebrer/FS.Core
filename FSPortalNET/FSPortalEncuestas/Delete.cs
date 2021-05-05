using FSDatabase;
using FSPortal;
using System;

namespace FSPortalEncuestas
{
    public class Delete : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }
        public string Inicio()
        {
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            long lEncuestaID = Convert.ToInt32(Request["ID"]);

            db.Execute("DELETE FROM " + Variables.App.prefijoTablas + "Encuestas WHERE EncuestaID = " + lEncuestaID);
            db.Execute("DELETE From " + Variables.App.prefijoTablas + "OpcionesEncuesta WHERE EncuestaID = " + lEncuestaID);
            db.Execute("DELETE FROM " + Variables.App.prefijoTablas + "RespuestasEncuestas WHERE EncuestaID = " + lEncuestaID);

            return "Encuesta " + lEncuestaID + " eliminada.";
        }
    }
}
