using FSDatabase;
using FSLibrary;
using FSPlugin;
using FSPortal;
using System.Data;

namespace FSCustomPlugin
{
    public class ModEventosRecientes : IPlugin
    {
        public string Execute(params string[] p)
        {
            return EventosRecientes();
        }

        public string Name
        {
            get { return "ModEventosRecientes"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string EventosRecientes()
        {
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            string modEventosRecientesReturn = "";

            System.DateTime d = System.DateTime.Now;
            System.DateTime d1 = d.AddMonths(-1);

            string ssql;

            if (Utils.BDType == Utils.TypeBd.MySQL)
            {
                ssql = "SELECT idEventos,fecha,titulo from " + Variables.App.prefijoTablas + "Eventos where fecha>=" +
                    Utils.FormatShortDate(d1) + " and fecha<=" + Utils.FormatShortDate(d) +
                    " and repetir=false LIMIT 10";
            }
            else
            {
                ssql = "SELECT top 10 idEventos,fecha,titulo from " + Variables.App.prefijoTablas + "Eventos where fecha>=" +
                    Utils.FormatShortDate(d1) + " and fecha<=" + Utils.FormatShortDate(d) + " and repetir=false";
            }

            DataTable dtEventos = db.Execute(ssql);

            string msgerr = "";
            if (dtEventos.Rows.Count == 0)
            {
                msgerr = "No hay eventos recientes.";
            }

            foreach (DataRow row in dtEventos.Rows)
            {
                modEventosRecientesReturn = modEventosRecientesReturn + Ui.Lf();
                modEventosRecientesReturn = modEventosRecientesReturn +
                                            Ui.EditPage("eventos", "ideventos", row["ideventos"].ToString(),
                                                "Editar evento", "Borrar evento");
                modEventosRecientesReturn = modEventosRecientesReturn + @"<img alt='' border=""0"" src='" +
                                            Variables.App.directorioPortal + "imagenes/calendario.gif' />&nbsp;" + row["fecha"] +
                                            " - " + row["titulo"];
            }

            if (Utils.BDType == Utils.TypeBd.MySQL)
            {
                ssql = "SELECT idEventos,fecha,titulo from " + Variables.App.prefijoTablas +
                    "Eventos where repetir=true and month(fecha)>=" + System.DateTime.Now.AddDays(1).Month +
                    " and month(fecha)<=" + System.DateTime.Now.Month + " and fecha<" + Utils.FormatShortDate(System.DateTime.Now) +
                       " LIMIT 10";
            }
            else
            {
                ssql = "SELECT top 10 idEventos,fecha,titulo from " + Variables.App.prefijoTablas +
                    "Eventos where repetir=true and month(fecha)>=" + System.DateTime.Now.AddDays(1).Month +
                    " and month(fecha)<=" + System.DateTime.Now.Month + " and fecha<" + Utils.FormatShortDate(System.DateTime.Now);
            }

            dtEventos = db.Execute(ssql);

            if (dtEventos.Rows.Count == 0)
            {
                if (msgerr != "")
                {
                    modEventosRecientesReturn = modEventosRecientesReturn + @"<img alt='' border=""0"" src='" +
                                                Variables.App.directorioPortal + "imagenes/calendario.gif' /> " + msgerr;
                }
            }
            else
            {
                modEventosRecientesReturn = modEventosRecientesReturn + Ui.Lf() + "<hr />" + Ui.Lf() +
                                            "Eventos anuales: " + Ui.Lf();
            }

            foreach (DataRow row in dtEventos.Rows)
            {
                modEventosRecientesReturn = modEventosRecientesReturn + Ui.Lf();
                modEventosRecientesReturn = modEventosRecientesReturn +
                                            Ui.EditPage("eventos", "ideventos", row["ideventos"].ToString(),
                                                "Editar evento", "Borrar evento");
                modEventosRecientesReturn = modEventosRecientesReturn + @"<img alt='' border=""0"" src='" +
                                            Variables.App.directorioPortal + "imagenes/calendario.gif' />&nbsp;" +
                    System.DateTime.Parse(Functions.Valor(row["fecha"])).Day + " de " +
                    DateTimeUtil.NombreMes(System.DateTime.Parse(Functions.Valor(row["fecha"])).Month) + " - " +
                                            row["titulo"];
            }

            modEventosRecientesReturn = modEventosRecientesReturn + Ui.Lf();
            return modEventosRecientesReturn;
        }
    }
}