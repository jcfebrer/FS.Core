using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using static FSIACore.NeuralNetworkActivations;

namespace FSIACore
{
    public class NeuralNetwork
    {
        private int[] layers; // Cantidad de neuronas por capa
        private double[][] neurons; // Valores de las neuronas, incluyendo preactivación
        private double[][] biases; // Sesgos por capa (excepto capa de entrada)
        private double[][][] weights; // Pesos entre las capas
        private IActivationFunction[] activationFunctions; // Funciones de activación para cada capa oculta/salida

        /// <summary>
        /// Constructor de la red neuronal.
        /// </summary>
        /// <param name="layers">Número de neuronas en cada capa.</param>
        /// <param name="activations">Funciones de activación para las capas ocultas y de salida.</param>
        public NeuralNetwork(int[] layers, IActivationFunction[] activations)
        {
            if (layers.Length - 1 != activations.Length)
                throw new ArgumentException("Debe haber una función de activación por cada capa (excepto la de entrada).");

            this.layers = layers;
            this.activationFunctions = activations;

            InitializeNetwork();
        }

        /// <summary>
        /// Inicializa la red neuronal generando pesos y sesgos aleatorios.
        /// </summary>
        private void InitializeNetwork()
        {
            // Inicialización de las neuronas
            neurons = new double[layers.Length][];
            for (int i = 0; i < layers.Length; i++)
                neurons[i] = new double[layers[i]];

            // Inicialización de los sesgos
            biases = new double[layers.Length][];
            Random rand = new Random();
            for (int i = 1; i < layers.Length; i++)
                biases[i] = Enumerable.Range(0, layers[i]).Select(_ => rand.NextDouble() - 0.5).ToArray();

            // Inicialización de los pesos
            weights = new double[layers.Length - 1][][];
            for (int i = 0; i < layers.Length - 1; i++)
            {
                double scale = activationFunctions[i] is ReLU ? Math.Sqrt(2.0 / layers[i]) : Math.Sqrt(1.0 / layers[i]);
                weights[i] = new double[layers[i + 1]][];
                for (int j = 0; j < layers[i + 1]; j++)
                    weights[i][j] = Enumerable.Range(0, layers[i])
                                              .Select(_ => (rand.NextDouble() - 0.5) * 2 * scale)
                                              .ToArray();
            }
        }

        /// <summary>
        /// Realiza la predicción de la salida dado un conjunto de entradas.
        /// </summary>
        /// <param name="inputs">Vector de entradas.</param>
        /// <returns>Vector de salidas.</returns>
        public double[] Predict(double[] inputs)
        {
            neurons[0] = inputs;

            for (int i = 1; i < layers.Length; i++)
            {
                // Cálculo de preactivación y activación
                for (int j = 0; j < layers[i]; j++)
                {
                    double sum = biases[i][j];
                    for (int k = 0; k < layers[i - 1]; k++)
                        sum += neurons[i - 1][k] * weights[i - 1][j][k];

                    neurons[i][j] = sum; // Almacena la preactivación
                }
                neurons[i] = activationFunctions[i - 1].Activate(neurons[i]); // Aplica activación
            }

            return neurons[neurons.Length - 1];
        }

        /// <summary>
        /// Entrena la red neuronal utilizando retropropagación.
        /// </summary>
        /// <param name="inputs">Datos de entrada.</param>
        /// <param name="expectedOutputs">Salidas esperadas.</param>
        /// <param name="epochs">Número de épocas de entrenamiento.</param>
        /// <param name="learningRate">Tasa de aprendizaje.</param>
        public void Train(double[][] inputs, double[][] expectedOutputs, int epochs, double learningRate)
        {
            for (int epoch = 0; epoch < epochs; epoch++)
            {
                double errorSum = 0;

                for (int sample = 0; sample < inputs.Length; sample++)
                {
                    double[] output = Predict(inputs[sample]);

                    double[][] errors = new double[layers.Length][];
                    errors[layers.Length - 1] = new double[layers[layers.Length - 1]];

                    // Calcular errores para la capa de salida
                    for (int i = 0; i < errors[layers.Length - 1].Length; i++)
                    {
                        errors[layers.Length - 1][i] = expectedOutputs[sample][i] - output[i];
                        errorSum += Math.Pow(errors[layers.Length - 1][i], 2);
                    }

                    // Retropropagación (Backpropagation) para las capas anteriores
                    for (int i = layers.Length - 2; i >= 0; i--)
                    {
                        errors[i] = new double[layers[i]];

                        for (int j = 0; j < layers[i]; j++)
                        {
                            double errorGradientSum = 0;

                            // Sumar contribuciones de la capa siguiente
                            for (int k = 0; k < layers[i + 1]; k++)
                                errorGradientSum += weights[i][k][j] * errors[i + 1][k];

                            // Calcular el error para esta capa
                            if (i == layers.Length - 2 && activationFunctions[i] is Softmax)
                            {
                                // Softmax: derivada simplificada con Cross-Entropy Loss
                                errors[i][j] = errorGradientSum; // Ya calculado arriba
                            }
                            else
                            {
                                // Derivada estándar de otras funciones de activación
                            errors[i][j] = errorGradientSum * activationFunctions[i].Derivative(neurons[i][j]);
                        }
                    }
                    }

                    // Actualizar pesos y sesgos
                    for (int i = 0; i < layers.Length - 1; i++)
                    {
                        for (int j = 0; j < layers[i + 1]; j++)
                        {
                            biases[i + 1][j] += learningRate * errors[i + 1][j];
                            for (int k = 0; k < layers[i]; k++)
                                weights[i][j][k] += learningRate * errors[i + 1][j] * neurons[i][k];
                        }
                    }
                }

                Debug.WriteLine($"Época {epoch + 1}, Error promedio: {errorSum / inputs.Length}");
            }
        }

        /// <summary>
        /// Devuelve el índice del valor máximo en un arreglo.
        /// </summary>
        public int GetMaxIndex(double[] array)
        {
            return Array.IndexOf(array, array.Max());
        }

        /// <summary>
        /// Prueba la red neuronal con entradas dadas.
        /// </summary>
        public void TestNetwork(double[][] inputs)
        {
            int correct = 0;
            foreach (var input in inputs)
            {
                if(TestNetwork(input))
                    correct++;
            }

            Console.WriteLine($"Precisión en datos de prueba: {correct * 100.0 / inputs.Length}%");
        }

        /// <summary>
        /// Muestra los resultados en la ventana de depuración.
        /// </summary>
        public bool TestNetwork(double[] input)
        {
                Step stepFunction = new Step();
            double[] prediction = Predict(input);
            int predictedClass = GetMaxIndex(prediction);
            double[] binaryOutput = prediction.Select(stepFunction.Activate).ToArray();

            // Generar la salida formateada
            Debug.WriteLine(
                $"Entrada: {string.Join(", ", input.Select(i => string.Format("{0:N3}", i)))} => " +
                $"Salida Binaria: {string.Join(", ", binaryOutput)} => " +
                $"Salida: {string.Join(", ", prediction.Select(o => string.Format("{0:N3}", o)))} => " +
                $"Clase Predicha: {0:N3}", predictedClass
            );

            if (GetMaxIndex(prediction) == GetMaxIndex(input))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Codifica una etiqueta en formato one-hot.
        /// </summary>
        public double[] OneHotEncode(int label, int size)
        {
            double[] oneHot = new double[size];
            oneHot[label] = 1;
            return oneHot;
        }
    }
}
