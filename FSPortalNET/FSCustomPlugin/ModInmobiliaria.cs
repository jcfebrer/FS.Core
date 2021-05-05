using FSDatabase;
using FSLibrary;
using FSPlugin;
using FSPortal;
using System.Data;

namespace FSCustomPlugin
{
    public class ModInmobiliaria : IPlugin
    {
        public string Execute(params string[] p)
        {
            return Inmobiliaria();
        }

        public string Name
        {
            get { return "ModInmobiliaria"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string Inmobiliaria()
        {
            string modInmobiliariaReturn = "<form action='" + Variables.App.directorioPortal +
                                    @"inmobiliaria/resultados.aspx' name='frmInmo' id='frmInmo' method=""post"">" +
                                    "\r\n";
            modInmobiliariaReturn = modInmobiliariaReturn + Ui.Lf() + "Selecciona un modo:" + Ui.Lf() +
                                    "<select name='modo' class='textboxplano' style='width:135px'>" + "\r\n";

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            string ssql = "SELECT idmodo,modo from " + Variables.App.prefijoTablas + "inmo_modos";
            DataTable dtModos = db.Execute(ssql);
            foreach (DataRow row in dtModos.Rows)
            {
                modInmobiliariaReturn = modInmobiliariaReturn + @"<option value=""" + row["idmodo"] + @""">" +
                                        row["modo"] + "</option>" + "\r\n";
            }

            modInmobiliariaReturn = modInmobiliariaReturn + "</select>" + Ui.Lf() + "\r\n";

            modInmobiliariaReturn = modInmobiliariaReturn + Ui.Lf() + "Selecciona un tipo:" + Ui.Lf() +
                                    "<select name='tipo' class='textboxplano' style='width:135px'>" + "\r\n";
            modInmobiliariaReturn = modInmobiliariaReturn + @"<option value=""-1"">Todos</option>" + "\r\n";

            ssql = "SELECT idTipo,tipo from " + Variables.App.prefijoTablas + "inmo_tipos";
            DataTable dtTipos = db.Execute(ssql);
            foreach (DataRow row in dtTipos.Rows)
            {
                modInmobiliariaReturn = modInmobiliariaReturn + @"<option value=""" + row["idTipo"] + @""">" +
                                        row["tipo"] + "</option>" + "\r\n";
            }

            modInmobiliariaReturn = modInmobiliariaReturn + "</select>" + Ui.Lf() + "\r\n";

            modInmobiliariaReturn = modInmobiliariaReturn + Ui.Lf() + "Selecciona una población: " + Ui.Lf() +
                                    "<select name='poblacion' class='textboxplano' style='width:135px'>" + "\r\n";
            modInmobiliariaReturn = modInmobiliariaReturn + "<option>Todos</option>" + "\r\n";

            ssql = "SELECT poblacion from " + Variables.App.prefijoTablas + "inmo_poblaciones order by poblacion asc";
            DataTable dtPoblacion = db.Execute(ssql);
            foreach (DataRow row in dtPoblacion.Rows)
            {
                modInmobiliariaReturn = modInmobiliariaReturn + "<option>" + Functions.Valor(row["poblacion"]).ToUpper() +
                                        "</option>" + "\r\n";
            }

            modInmobiliariaReturn = modInmobiliariaReturn + "</select>" + "\r\n";


            modInmobiliariaReturn = modInmobiliariaReturn + Ui.Lf() + "Nº Habitaciones:" + Ui.Lf() +
                                    "<select name='habitaciones' class='textboxplano' style='width:135px'>" + "\r\n";
            modInmobiliariaReturn = modInmobiliariaReturn + "<option>&nbsp;</option>";
            modInmobiliariaReturn = modInmobiliariaReturn + "<option>1</option>";
            modInmobiliariaReturn = modInmobiliariaReturn + "<option>2</option>";
            modInmobiliariaReturn = modInmobiliariaReturn + "<option>3</option>";
            modInmobiliariaReturn = modInmobiliariaReturn + "<option>4</option>";
            modInmobiliariaReturn = modInmobiliariaReturn + "<option>5</option>";
            modInmobiliariaReturn = modInmobiliariaReturn + "<option>6</option>";
            modInmobiliariaReturn = modInmobiliariaReturn + "</select>" + Ui.Lf();

            modInmobiliariaReturn = modInmobiliariaReturn + Ui.Lf() + "Nº de baños:" + Ui.Lf() +
                                    "<select name='baños' class='textboxplano' style='width:135px'>" + "\r\n";
            modInmobiliariaReturn = modInmobiliariaReturn + "<option>&nbsp;</option>";
            modInmobiliariaReturn = modInmobiliariaReturn + "<option>1</option>";
            modInmobiliariaReturn = modInmobiliariaReturn + "<option>2</option>";
            modInmobiliariaReturn = modInmobiliariaReturn + "<option>3</option>";
            modInmobiliariaReturn = modInmobiliariaReturn + "<option>4</option>";
            modInmobiliariaReturn = modInmobiliariaReturn + "<option>5</option>";
            modInmobiliariaReturn = modInmobiliariaReturn + "<option>6</option>";
            modInmobiliariaReturn = modInmobiliariaReturn + "</select>" + Ui.Lf();

            modInmobiliariaReturn = modInmobiliariaReturn + Ui.Lf() + "Precio Desde:";
            modInmobiliariaReturn = modInmobiliariaReturn + Ui.Lf() +
                                    @"<input type=""text"" size=""18"" name=""precio_desde"" />";

            modInmobiliariaReturn = modInmobiliariaReturn + Ui.Lf() + Ui.Lf() + "Precio Hasta:";
            modInmobiliariaReturn = modInmobiliariaReturn + Ui.Lf() +
                                    @"<input type=""text"" size=""18"" name=""precio_hasta"" />";

            modInmobiliariaReturn = modInmobiliariaReturn + Ui.Lf() + Ui.Lf() +
                                    @"<input type=""submit"" name=""Buscar"" value=""Encuentre !!!"" />";

            modInmobiliariaReturn = modInmobiliariaReturn + "</form>" + "\r\n";
            modInmobiliariaReturn = modInmobiliariaReturn +
                                    Ui.Link("Desearía vender mi vivienda.",
                                        Variables.App.directorioPortal + "servicios/contactar.aspx?comm=" +
                                        Variables.App.Page.Server.UrlEncode(
                                            "Desearía vender mi vivienda, por favor, ponganse en contacto conmigo. Gracias.")) +
                                    Ui.Lf() + "\r\n";

            return modInmobiliariaReturn;
        }
    }
}