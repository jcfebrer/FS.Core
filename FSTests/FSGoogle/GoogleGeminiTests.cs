#if NET8_0_OR_GREATER

using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSTests.FSGoogle
{
    [TestClass()]
    public class GeminiTests
    {
        IConfiguration config;
        FSGoogleGemini.Library googleGemini;

        public GeminiTests()
        {
            config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

            googleGemini = new FSGoogleGemini.Library(config["GoogleGemini:ApiKey"]);
        }

        [TestMethod()]
        public void TestListModelsAsync()
        {
            var response = googleGemini.ListModelsAsync();

            Assert.IsNotNull(response);
        }

        [TestMethod()]
        public void TestQuestion()
        {
            var response = googleGemini.Question("cual es la capital de checoslovaquia?");

            Assert.IsNotNull(response);
        }

        [TestMethod()]
        public void TestQuestionAsync()
        {
            var response = googleGemini.QuestionAsync("What is the capital of France?");

            Assert.IsNotNull(response);
        }
    }
}

#endif