using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using OpenAI.Interfaces;
using OpenAI.Managers;
using OpenAI;
using OpenAI.ObjectModels.ResponseModels.ImageResponseModel;
using System.Threading.Tasks;
using System;

namespace FSAiCore
{
    [CLSCompliant(false)]
    public class DallE
    {
        public string key { get; set; }
        public DallE(string key)
        {
            this.key = key;
        }

        async public Task<ImageCreateResponse> GenerateImage(string prompt)
        {
            var openAiService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = key,
            });

            var imageResult = await openAiService.Image.CreateImage(new ImageCreateRequest
            {
                Prompt = prompt,
                N = 2,
                Size = StaticValues.ImageStatics.Size.Size256,
                ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url,
                User = "TestUser"
            });

            return imageResult;
        }
    }
}
