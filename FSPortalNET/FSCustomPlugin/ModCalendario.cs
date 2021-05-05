using FSNetwork;
using FSLibrary;
using FSPlugin;
using FSPortal;

namespace FSCustomPlugin
{
    public class ModCalendario : IPlugin
    {
        public string Execute(params string[] p)
        {
            return Calendario();
        }

        public string Name
        {
            get { return "ModCalendario"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string Calendario()
        {
            int ano = Web.RequestInt("ano") != 0 ? Web.RequestInt("ano") : System.DateTime.Now.Year;
            int mes = Web.RequestInt("mes") != 0 ? Web.RequestInt("mes") : System.DateTime.Now.Month;
            int dia = Web.RequestInt("ano") != 0 ? Web.RequestInt("dia") : System.DateTime.Now.Day;

            Calendario cal = new Calendario();
            string modCalendarioReturn = cal.CrearCalendario(ano, mes, dia, Variables.App.directorioPortal + "eventos/default.aspx", 0);
            return modCalendarioReturn;
        }
    }
}