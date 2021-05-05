using FSPlugin;
using FSLibrary;

namespace FSLigaDeiaPlugin
{
    public class ModLigaDeia : IPlugin
    {
        public string Execute(params string[] p)
        {
            return Modulos.ModBanner(Functions.ValorBool(p[1]));
        }

        public string Name
        {
            get { return "ModLigaDeia"; }
        }

        public int Parameters
        {
            get { return 1; }
        }
    }
}