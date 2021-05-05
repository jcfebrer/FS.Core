using FSDatabase;
using FSLibrary;
using FSPlugin;
using FSPortal;
using System.Data;

namespace FSCustomPlugin
{
    public class ModProximosEventosDeportivos : IPlugin
    {
        public string Execute(params string[] p)
        {
            return ProximosEventosDeportivos();
        }

        public string Name
        {
            get { return "ModProximosEventosDeportivos"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string ProximosEventosDeportivos()
        {
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            string modProximosEventosDeportivosReturn = "";

            System.DateTime d = System.DateTime.Now;
            System.DateTime d1 = d.AddMonths(1);

            string ssql;

            if (Utils.BDType == Utils.TypeBd.MySQL)
            {
                ssql = "SELECT * from " + Variables.App.prefijoTablas + "EventosDeportivos where fecha>=" + Utils.FormatShortDate(d) +
                    " and fecha<=" + Utils.FormatShortDate(d1) + " and repetir=false LIMIT 10";
            }
            else
            {
                ssql = "SELECT top 10 * from " + Variables.App.prefijoTablas + "EventosDeportivos where fecha>=" +
                    Utils.FormatShortDate(d) + " and fecha<=" + Utils.FormatShortDate(d1) +
                       " and repetir=false";
            }


            DataTable dtEventos = db.Execute(ssql);

            string msgerr = "";
            if (dtEventos.Rows.Count == 0)
            {
                msgerr = "No hay eventos próximos.";
            }

            foreach (DataRow row in dtEventos.Rows)
            {
                modProximosEventosDeportivosReturn = modProximosEventosDeportivosReturn + Ui.Lf();
                modProximosEventosDeportivosReturn = modProximosEventosDeportivosReturn +
                                                     Ui.EditPage("eventos", "ideventos", row["ideventos"].ToString(),
                                                         "Editar evento", "Borrar evento");
                modProximosEventosDeportivosReturn = modProximosEventosDeportivosReturn +
                                                     @"<img alt='' border=""0"" src='" + Variables.App.directorioPortal +
                                                     "imagenes/calendario.gif' />&nbsp;" + row["fecha"] + " - " +
                                                     row["titulo"];
            }

            if (Utils.BDType == Utils.TypeBd.MySQL)
            {
                ssql = "SELECT idEventos,fecha,titulo from " + Variables.App.prefijoTablas +
                    "EventosDeportivos where repetir=true and month(fecha)>=" + System.DateTime.Now.Month +
                    " and month(fecha)<=" + System.DateTime.Now.AddDays(1).Month + " LIMIT 10";
            }
            else
            {
                ssql = "SELECT top 10 idEventos,fecha,titulo from " + Variables.App.prefijoTablas +
                    "EventosDeportivos where repetir=true and month(fecha)>=" + System.DateTime.Now.Month +
                    " and month(fecha)<=" + System.DateTime.Now.AddDays(1).Month;
            }

            dtEventos = db.Execute(ssql);

            if (dtEventos.Rows.Count == 0)
            {
                if (msgerr != "")
                {
                    modProximosEventosDeportivosReturn = modProximosEventosDeportivosReturn +
                                                         @"<img alt='' border=""0"" src='" + Variables.App.directorioPortal +
                                                         "imagenes/calendario.gif' /> " + msgerr;
                }
            }
            else
            {
                modProximosEventosDeportivosReturn = modProximosEventosDeportivosReturn + Ui.Lf() + "<hr />" + Ui.Lf() +
                                                     "Eventos anuales: " + Ui.Lf();
            }

            foreach (DataRow row in dtEventos.Rows)
            {
                modProximosEventosDeportivosReturn = modProximosEventosDeportivosReturn + Ui.Lf();
                modProximosEventosDeportivosReturn = modProximosEventosDeportivosReturn +
                                                     Ui.EditPage("eventos", "ideventos", row["ideventos"].ToString(),
                                                         "Editar evento", "Borrar evento");
                modProximosEventosDeportivosReturn = modProximosEventosDeportivosReturn +
                                                     @"<img alt='' border=""0"" src='" + Variables.App.directorioPortal +
                                                     "imagenes/calendario.gif' />&nbsp;" +
                    System.DateTime.Parse(Functions.Valor(row["fecha"])).Day + " de " +
                    DateTimeUtil.NombreMes(System.DateTime.Parse(Functions.Valor(row["fecha"])).Month) +
                                                     " - " + row["titulo"];
            }

            modProximosEventosDeportivosReturn = modProximosEventosDeportivosReturn + Ui.Lf();
            modProximosEventosDeportivosReturn = Ui.Left(modProximosEventosDeportivosReturn);
            return modProximosEventosDeportivosReturn;
        }
    }
}