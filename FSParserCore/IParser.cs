using System.Collections.Specialized;

namespace FSParserCore
{
    public interface IParser
    {
        NameValueCollection Memory { get; set; }

        void Abort();
        void Process(string code);
        void Stop();
        void ThreadReset();
        void ThreadSet();
        void ThreadWait();
    }
}