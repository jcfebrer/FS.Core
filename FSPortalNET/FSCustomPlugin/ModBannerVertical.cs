using FSPlugin;

namespace FSCustomPlugin
{
    public class ModBannerVertical : IPlugin
    {
        public string Execute(params string[] p)
        {
            return ModBanner.Banner(false);
        }

        public string Name
        {
            get { return "ModBannerVertical"; }
        }

        public int Parameters
        {
            get { return 0; }
        }
    }
}