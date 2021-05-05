using System;
using FSLibrary;
using FSPortal;
using FSDatabase;
using FSNetwork;

namespace FSPaginas
{
    public class AddModulo : BasePage
    {
        /// <summary>
        ///     Carga de la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(Object sender, EventArgs e)
        {
            Inicio();
        }

        private void Inicio()
        {
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);


            int pos = Web.RequestInt("pos");

            int orden = NumberUtils.NumberInt(db.ExecuteScalar("select max(orden) from " + Variables.App.prefijoTablas + "modulos"));

            string ssql = "insert into " + Variables.App.prefijoTablas +
                          "modulos (nombre,posicion,orden,titulo,activo) VALUES ('modPagina'," + pos + "," + orden +
                          ",'Modulo nuevo',true)";
            db.ExecuteNonQuery(ssql);

            int idmodulo = NumberUtils.NumberInt(db.GetIdentity());

            ssql = "insert into " + Variables.App.prefijoTablas +
                   "paginas (titulo,contenido) VALUES ('Página de modulo','Contenido del módulo.')";
            db.ExecuteNonQuery(ssql);

            int pagina = NumberUtils.NumberInt(db.GetIdentity());


            ssql = "insert into " + Variables.App.prefijoTablas + "ConfiguracionModulos (idModulo,propiedad,valor) VALUES (" +
                   idmodulo + ",'pagina'," + pagina + ")";
            db.ExecuteNonQuery(ssql);

            //Functions.SetSessionValue("cacheColumnaDerecha", "");
            //Functions.SetSessionValue("cacheColumnaIzquierda", "");
            //Functions.SetSessionValue("cacheColumnaCentro", "");

            Response.Redirect(Variables.App.directorioPortal + "default.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}