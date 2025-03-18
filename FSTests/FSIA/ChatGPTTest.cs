using FSIA;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace FSTests.FSIA
{
    [TestClass()]
    public class ChatGPTTest
    {
        [TestMethod()]
        public void TestChatGPT()
        {
            ChatGPT chatGPT = new ChatGPT(ConfigurationManager.AppSettings["ChatGPTKey"], ConfigurationManager.AppSettings["ChatGPTOrganization"]);

            Task<string> respuesta = chatGPT.Question("Cual es la capital de Nueva York?", ChatGPT.ChatQuestionType.User);

            Assert.IsNotNull(respuesta);
        }

        [TestMethod()]
        public async void TestChat2GPT()
        {
            CancellationToken token = new CancellationToken();
            ChatGPT2 chatGPT2 = new ChatGPT2(ConfigurationManager.AppSettings["ChatGPTKey"], ConfigurationManager.AppSettings["ChatGPTOrganization"], ConfigurationManager.AppSettings["ChatGPTUrl"]);

            var respuesta = await chatGPT2.Question("Cual es la capital de Nueva York?", "Actua como un reconocido filosofo de la antigua grecia.", token);

            Assert.IsNotNull(respuesta);
        }

#if NETCOREAPP
        [TestMethod()]
        public async Task TestAIApiAsync()
        {
            var openAIApiClient = new OpenAIApiClient(ConfigurationManager.AppSettings["ChatGPTKey"], ConfigurationManager.AppSettings["ChatGPTOrganization"]);
            var response = await openAIApiClient.SendPrompt("Cual es la capital de Nueva York?", "gpt-3.5-turbo");  //davinci
            
            Assert.IsNotNull(response);
        }
#endif
    }
}
