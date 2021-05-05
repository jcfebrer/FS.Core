using FSPlugin;

namespace FSCustomPlugin
{
    public class ModBannerHorizontal : IPlugin
    {
        public string Execute(params string[] p)
        {
            return ModBanner.Banner(true);
        }

        public string Name
        {
            get { return "ModBannerHorizontal"; }
        }

        public int Parameters
        {
            get { return 0; }
        }
    }
}