using System;
using System.Collections.Generic;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using OpenAI.Managers;
using OpenAI;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;

namespace FSAi
{
    public class ChatGPT
    {
        public enum ChatQuestionType
        {
            Assistant,
            System,
            User,
            Function
        }
        public string key { get; set; }
        public ChatGPT(string key)
        {
            this.key = key;
        }


        async public Task<string> Question(string question, ChatQuestionType chatQuestionType)
        {
            var openAiService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = key,
            });

            var Messages = new List<ChatMessage>();

            switch (chatQuestionType)
            {
                case ChatQuestionType.Assistant:
                    Messages.Add(ChatMessage.FromAssistant(question));
                    break;
                case ChatQuestionType.Function:
                    Messages.Add(ChatMessage.FromFunction(question));
                    break;
                case ChatQuestionType.System:
                    Messages.Add(ChatMessage.FromSystem(question));
                    break;
                case ChatQuestionType.User:
                    Messages.Add(ChatMessage.FromUser(question));
                    break;
                default:
                    Messages.Add(ChatMessage.FromUser(question));
                    break;
            }

            var chatCompletionCreateRequest = new ChatCompletionCreateRequest
            {
                Model = Models.Gpt_3_5_Turbo,
                MaxTokens = 1000,
                Temperature = 0.7f,
                TopP = 1,
                PresencePenalty = 0,
                FrequencyPenalty = 0,
            };

            chatCompletionCreateRequest.Messages = Messages;

            var completionResult = await openAiService.ChatCompletion.CreateCompletion(chatCompletionCreateRequest);

            if (completionResult.Successful)
                return completionResult.Choices.First().Message.Content;
            else
                return completionResult.Error.Message;
        }
    }
}