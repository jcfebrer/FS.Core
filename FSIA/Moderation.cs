#if NETCOREAPP

using Betalgo.Ranul.OpenAI;
using Betalgo.Ranul.OpenAI.Managers;
using Betalgo.Ranul.OpenAI.ObjectModels.RequestModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSIA
{
    public class Moderation
    {
        public string key { get; set; }
        public Moderation(string key) 
        {
            this.key = key;
        }

        async public void CreateModeration(string prompt)
        {
            var openAiService = new OpenAIService(new OpenAIOptions()
            {
                ApiKey = key,
            });

            var moderationResponse = await openAiService.Moderation.CreateModeration(new CreateModerationRequest()
            {
                Input = prompt
            });

            if (moderationResponse.Results.FirstOrDefault()?.Flagged != true)
            {
                Debug.WriteLine("Create Moderation test failed", ConsoleColor.DarkRed);
            }

            Debug.WriteLine("Create Moderation test passed.", ConsoleColor.DarkGreen);
        }
    }
}

#endif