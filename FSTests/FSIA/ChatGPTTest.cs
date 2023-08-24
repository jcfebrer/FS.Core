using FSAi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Threading.Tasks;

namespace FSTests.FSIA
{
    [TestClass()]
    public class ChatGPTTest
    {
        [TestMethod()]
        public void TestChatGPT()
        {
            ChatGPT chatGPT = new ChatGPT(ConfigurationManager.AppSettings["ChatGPTKey"]);

            Task<string> respuesta = chatGPT.Question("Cual es la capital de Nueva York?", ChatGPT.ChatQuestionType.User);

            Assert.IsNotNull(respuesta);
        }
    }
}
