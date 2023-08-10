using System;
using System.Collections.Generic;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using OpenAI.Managers;
using OpenAI;
using System.Linq;

namespace FSAi
{
    public class ChatGPT
    {
        public string key { get; set; }
        public ChatGPT(string key)
        {
            this.key = key;
        }


        async public void Init()
        {
            var openAiService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = key,
            });

            var completionResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
    {
        ChatMessage.FromSystem("You are a helpful assistant."),
        ChatMessage.FromUser("Who won the world series in 2020?"),
        ChatMessage.FromAssistant("The Los Angeles Dodgers won the World Series in 2020."),
        ChatMessage.FromUser("Where was it played?")
    },
                Model = Models.Gpt_3_5_Turbo,
                MaxTokens = 1000,
                Temperature = 0.7f,
                TopP = 1,
                PresencePenalty = 0,
                FrequencyPenalty = 0,
            });

            if (completionResult.Successful)
            {
                Console.WriteLine(completionResult.Choices.First().Message.Content);
            }
        }
    }
}