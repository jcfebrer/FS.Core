using System.Collections.Specialized;

namespace FSParser
{
    public interface IParser
    {
        NameValueCollection Memory { get; set; }

        void Abort();
        void Clear();
        void EjecuteScript(string code);
        string MemoryRequest(string name);
        void MemorySave(string name, string value);
        void Stop();
        void ThreadReset();
        void ThreadSet();
        void ThreadWait();
    }
}