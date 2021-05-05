using FSPaginas.FileManager;
using FSPlugin;
using FSPortal;

namespace FSCustomPlugin
{
    public class ModGaleria : IPlugin
    {
        public string Execute(params string[] p)
        {
            Default fm = new Default {modeGallery = true, columnas = 4, adminControls = Variables.User.Administrador};

            if (p.Length == 2)
                fm.pathInicial = p[1];
            else
                fm.pathInicial = Variables.App.directorioWeb + "galeria/";

            return fm.Inicio();
        }

        public string Name
        {
            get { return "ModGaleria"; }
        }

        public int Parameters
        {
            get { return 0; }
        }
    }
}