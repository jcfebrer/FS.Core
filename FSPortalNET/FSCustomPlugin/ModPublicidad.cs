using FSPlugin;
using FSPortal;

namespace FSCustomPlugin
{
    public class ModPublicidad : IPlugin
    {
        public string Execute(params string[] p)
        {
            return Publicidad();
        }

        public string Name
        {
            get { return "ModPublicidad"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string Publicidad()
        {
            string modPublicidadReturn = Ui.Link("Publicidad aquí.", Variables.App.directorioPortal + "servicios/publicidad.aspx") +
                                         "\r\n";
            return modPublicidadReturn;
        }
    }
}