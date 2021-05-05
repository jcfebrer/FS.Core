using FSPaginas.FileManager;
using FSPlugin;
using FSPortal;

namespace FSCustomPlugin
{
    public class ModFileManager : IPlugin
    {
        public string Execute(params string[] p)
        {
            Default fm = new Default {modeGallery = false, columnas = 3, adminControls = Variables.User.Administrador};

            if (p.Length == 2)
                fm.pathInicial = p[1];
            else
                fm.pathInicial = Variables.App.uploadPath;

            return fm.Inicio();
        }

        public string Name
        {
            get { return "ModFileManager"; }
        }

        public int Parameters
        {
            get { return 0; }
        }
    }
}