using System;
using System.Diagnostics;

namespace FSIACore
{
    public class Perceptron
    {
        private double[] weights; // Pesos
        private double bias; // Sesgo
        private double learningRate; // Tasa de aprendizaje

        public Perceptron(int inputSize, double learningRate = 0.1)
        {
            // Inicialización de pesos y sesgo
            weights = new double[inputSize];
            bias = 0;
            this.learningRate = learningRate;

            // Inicializamos los pesos aleatoriamente
            Random rand = new Random();
            for (int i = 0; i < inputSize; i++)
            {
                weights[i] = rand.NextDouble() - 0.5; // Pesos entre -0.5 y 0.5
            }
        }

        // Función de activación (escalón)
        private int StepFunction(double value)
        {
            return value >= 0 ? 1 : 0;
        }

        // Predicción
        public int Predict(double[] inputs)
        {
            double sum = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                sum += weights[i] * inputs[i];
            }

            sum += bias;

            return StepFunction(sum);
        }

        // Entrenamiento
        public void Train(double[][] trainingData, int[] labels, int epochs)
        {
            for (int epoch = 0; epoch < epochs; epoch++)
            {
                Debug.WriteLine($"Epoch {epoch + 1}:");

                for (int i = 0; i < trainingData.Length; i++)
                {
                    int prediction = Predict(trainingData[i]);
                    int error = labels[i] - prediction;

                    // Actualización de pesos y sesgo
                    for (int j = 0; j < weights.Length; j++)
                    {
                        weights[j] += learningRate * error * trainingData[i][j];
                    }

                    bias += learningRate * error;

                    Debug.WriteLine($"Error: {error}");
                }
            }
        }

        // Mostrar pesos
        public void PrintWeights()
        {
            Debug.WriteLine("Pesos:");
            for (int i = 0; i < weights.Length; i++)
            {
                Debug.WriteLine($"w{i + 1}: {weights[i]}");
            }
            Debug.WriteLine($"Sesgo: {bias}");
        }
    }
}
