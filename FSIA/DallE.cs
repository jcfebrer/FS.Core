using System.Threading.Tasks;
using System;
using Betalgo.Ranul.OpenAI.ObjectModels.ResponseModels.ImageResponseModel;
using Betalgo.Ranul.OpenAI.Managers;
using Betalgo.Ranul.OpenAI.ObjectModels.RequestModels;
using Betalgo.Ranul.OpenAI;
using Betalgo.Ranul.OpenAI.ObjectModels;

namespace FSIA
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
            var openAiService = new OpenAIService(new OpenAIOptions()
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
