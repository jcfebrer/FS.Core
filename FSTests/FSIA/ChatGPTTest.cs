using FSAi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            ChatGPT chatGPT = new ChatGPT(ConfigurationManager.AppSettings["ChatGPTKey"], ConfigurationManager.AppSettings["ChatGPTOrganization"]);

            Task<string> respuesta = chatGPT.Question("Cual es la capital de Nueva York?", ChatGPT.ChatQuestionType.User);

            Assert.IsNotNull(respuesta);
        }

        [TestMethod()]
        public async Task TestAIApiAsync()
        {
            var openAIApiClient = new OpenAIApiClient(ConfigurationManager.AppSettings["ChatGPTKey"], ConfigurationManager.AppSettings["ChatGPTOrganization"]);
            var response = await openAIApiClient.SendPrompt("Cual es la capital de Nueva York?", "gpt-3.5-turbo");  //davinci
            
            Assert.IsNotNull(response);
        }
    }
}
