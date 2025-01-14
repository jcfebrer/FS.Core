using System;
using System.Collections.Generic;
using System.Linq;

namespace FSIACore.GenerativeNetwork
{
    public class LanguageModel
    {
        private Dictionary<string, List<string>> wordPairs = new Dictionary<string, List<string>>();

        public LanguageModel(IEnumerable<string> corpus)
        {
            // Construir diccionario de pares de palabras
            foreach (var sentence in corpus)
            {
                var words = sentence.Split(' ');
                for (int i = 0; i < words.Length - 1; i++)
                {
                    if (!wordPairs.ContainsKey(words[i]))
                        wordPairs[words[i]] = new List<string>();

                    wordPairs[words[i]].Add(words[i + 1]);
                }
            }
        }

        public string GenerateText(string prompt, int length)
        {
            var result = new List<string> { prompt };
            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                var lastWord = result.Last();
                if (wordPairs.ContainsKey(lastWord))
                {
                    var nextWords = wordPairs[lastWord];
                    result.Add(nextWords[random.Next(nextWords.Count)]);
                }
                else
                {
                    break; // No hay más palabras en el contexto
                }
            }

            return string.Join(" ", result);
        }

        // Construir un diccionario basado en el corpus
        public static Dictionary<int, string> BuildDictionary(string corpusPath)
        {
            var words = System.IO.File.ReadAllText(corpusPath)
                .Split(new[] { ' ', '\n', '\r', '.', ',', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
                .Distinct()
                .ToArray();

            var dictionary = new Dictionary<int, string>();
            for (int i = 0; i < words.Length; i++)
            {
                dictionary[i] = words[i];
            }

            return dictionary;
        }
    }
}
