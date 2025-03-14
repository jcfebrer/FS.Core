using FSIA.GenerativeNetwork;
using MathNet.Numerics.LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FSTestsFSIACore
{
    [TestClass]
    public class AutoregressiveModeTest
    {
        [TestMethod]
        public void TestAutoregressiveMode()
        {
            // Parámetros del modelo autoregresivo
            int sequenceLength = 10;
            int inputSize = 5;

            AutoregressiveModel model = new AutoregressiveModel(inputSize, ActivationFunctions.ReLU);

            // Secuencia inicial (puede modificarse según el caso)
            var initialInput = Vector<double>.Build.Random(inputSize);

            Debug.WriteLine("Entrada inicial: " + initialInput);

            // Generar secuencia
            var generatedSequence = model.GenerateSequence(initialInput, sequenceLength);

            Debug.WriteLine("Secuencia generada:");
            foreach (var step in generatedSequence)
            {
                Debug.WriteLine(step);
            }
        }

        [TestMethod]
        public void SimpleTemporarySerieTest()
        {
            // Parámetros del modelo autoregresivo
            int sequenceLength = 10;
            int inputSize = 5;

            AutoregressiveModel model = new AutoregressiveModel(inputSize, ActivationFunctions.Sigmoid);

            // Crear una entrada inicial que simule una tendencia lineal
            var initialInput = Vector<double>.Build.DenseOfArray(new double[] { 1, 2, 3, 4, 5 });

            Debug.WriteLine("Entrada inicial: " + initialInput);

            // Generar una secuencia de datos
            var sequence = model.GenerateSequence(initialInput, sequenceLength);

            // Mostrar la secuencia generada
            Debug.WriteLine("Serie temporal generada:");
            for (int i = 0; i < sequence.Count; i++)
            {
                Debug.WriteLine($"Paso {i + 1}: {sequence[i]}");
            }
        }

        [TestMethod]
        public void SimpleTextFormat()
        {
            Debug.WriteLine("Iniciando modelo autoregresivo...");

            // Parámetros del modelo autoregresivo
            int sequenceLength = 20;
            int inputSize = 10;

            AutoregressiveModel model = new AutoregressiveModel(inputSize, ActivationFunctions.ReLU);

            // Secuencia inicial
            var initialInput = Vector<double>.Build.DenseOfArray(new double[] { 0.2, 0.4, 0.6, 0.8, 0.1, 0.3, 0.5, 0.7, 0.9, 0.2 });

            Debug.WriteLine("Entrada inicial: " + initialInput);

            // Generar secuencia
            var generatedSequence = model.GenerateSequence(initialInput, sequenceLength);

            // Diccionario ampliado de palabras
            var dictionary = new Dictionary<int, string>();
            var words = new string[] {
                "Hola", "Mundo", "Este", "Es", "Un", "Ejemplo", "De", "Texto", "Generativo", "Con",
                "Más", "Opciones", "Para", "Experimentar", "Aprender", "Y", "Diversificar", "La", "Generación", "Automática"
            };

            for (int i = 0; i < words.Length; i++)
            {
                dictionary[i] = words[i];
            }

            Debug.WriteLine("Secuencia generada como texto:");

            foreach (var vector in generatedSequence)
            {
                // Diversificar el índice utilizando la suma de todos los valores del vector
                var combinedValue = vector.Sum() * 7; // Multiplicador ajustado para mayor variación
                var wordIndex = (int)Math.Abs(Math.Round(combinedValue)) % dictionary.Count;
                Debug.WriteLine(dictionary[wordIndex]);
            }
        }

        [TestMethod]
        public void TextGeneratorCorpusBased()
        {
            Debug.WriteLine("Generación de texto basada en un corpus.");

            // Corpus básico
            var corpus = new List<string>
            {
                "Hola Mundo Este es un ejemplo",
                "Este es un modelo generativo básico",
                "Generación automática de texto con IA",
                "Aprender redes generativas es interesante"
            };

            // Crear el modelo de lenguaje
            var languageModel = new LanguageModel(corpus);

            // Generar texto a partir de un prompt inicial
            string prompt = "Hola";
            int length = 10; // Número de palabras a generar

            Debug.WriteLine("Texto generado:");
            Debug.WriteLine(languageModel.GenerateText(prompt, length));
        }

        [TestMethod]
        public void TextGeneratorCorpusBasedFile()
        {
            Console.WriteLine("Iniciando modelo autoregresivo con corpus...");

            // Ruta al archivo del corpus
            string corpusPath = "corpus.txt"; // Asegúrate de tener este archivo en el directorio del proyecto

            if (!System.IO.File.Exists(corpusPath))
            {
                Console.WriteLine("Error: No se encontró el archivo del corpus.");
                return;
            }

            // Leer el corpus y construir el diccionario
            var dictionary = LanguageModel.BuildDictionary(corpusPath);

            // Parámetros del modelo autoregresivo
            int sequenceLength = 20;
            int inputSize = dictionary.Count;

            AutoregressiveModel model = new AutoregressiveModel(inputSize, ActivationFunctions.ReLU);

            // Entrada inicial (vector aleatorio)
            var initialInput = Vector<double>.Build.Random(inputSize, new Random().Next(42));

            Console.WriteLine("Generando secuencia basada en el corpus...");

            // Generar secuencia
            var generatedSequence = model.GenerateSequence(initialInput, sequenceLength);

            // Convertir la secuencia generada en texto
            Console.WriteLine("Secuencia generada como texto:");
            foreach (var vector in generatedSequence)
            {
                var wordIndex = (int)Math.Abs(vector.Sum() * 7) % dictionary.Count;
                Console.WriteLine(dictionary[wordIndex]);
            }
        }
    }
}
