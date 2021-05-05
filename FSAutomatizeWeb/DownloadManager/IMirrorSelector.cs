using System.Reflection;
namespace FSAutomatizeWeb.DownloadManager
{
    public interface IMirrorSelector
    {
        void Init(Downloader downloader);

        ResourceLocation GetNextResourceLocation();
    }
}
