#if NETCOREAPP

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FSException;
using Betalgo.Ranul.OpenAI.Managers;
using Betalgo.Ranul.OpenAI;
using Betalgo.Ranul.OpenAI.ObjectModels.RequestModels;
using Betalgo.Ranul.OpenAI.ObjectModels;

namespace FSIA
{
    public class Whisper
    {
        public string key { get; set; }
        public Whisper(string key)
        {
            this.key = key;
        }

        async public void CreateTranscription(string fileName)
        {
            var openAiService = new OpenAIService(new OpenAIOptions()
            {
                ApiKey = key,
            });

            byte[] sampleFile;
            using (FileStream stream = File.Open($"{fileName}", FileMode.Open))
            {
                sampleFile = new byte[stream.Length];
                await stream.ReadAsync(sampleFile, 0, (int)stream.Length);
            }
            //var sampleFile = await FileExtensions.ReadAllBytesAsync($"SampleData/{fileName}");
            var audioResult = await openAiService.Audio.CreateTranscription(new AudioCreateTranscriptionRequest
            {
                FileName = fileName,
                File = sampleFile,
                Language = "es",
                Model = Models.WhisperV1,
                ResponseFormat = StaticValues.AudioStatics.ResponseFormat.VerboseJson
            });

            if (audioResult.Successful)
            {
                Console.WriteLine(string.Join("\n", audioResult.Text));
            }
            else
            {
                if (audioResult.Error == null)
                {
                    throw new ExceptionUtil("Unknown Error");
                }
                Console.WriteLine($"{audioResult.Error.Code}: {audioResult.Error.Message}");
            }
        }
    }
}
#endif