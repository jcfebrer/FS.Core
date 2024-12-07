using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using static FSIACore.NeuralNetworkActivations;

namespace FSIACore
{
    public class NeuralNetwork
    {
        private int[] layers;
        private double[][] neurons;
        private double[][] biases;
        private double[][][] weights;
        private IActivationFunction[] activationFunctions;

        public NeuralNetwork(int[] layers, IActivationFunction[] activations)
        {
            if (layers.Length - 1 != activations.Length)
                throw new ArgumentException("Debe haber una función de activación por cada capa (excepto la de entrada).");

            this.layers = layers;
            this.activationFunctions = activations;

            InitializeNetwork();
        }

        private void InitializeNetwork()
        {
            neurons = new double[layers.Length][];
            for (int i = 0; i < layers.Length; i++)
                neurons[i] = new double[layers[i]];

            biases = new double[layers.Length][];
            Random rand = new Random();
            for (int i = 1; i < layers.Length; i++)
                biases[i] = Enumerable.Range(0, layers[i]).Select(_ => rand.NextDouble() - 0.5).ToArray();

            weights = new double[layers.Length - 1][][];
            for (int i = 0; i < layers.Length - 1; i++)
                weights[i] = new double[layers[i + 1]][]
                    .Select(_ => Enumerable.Range(0, layers[i]).Select(__ => rand.NextDouble() - 0.5).ToArray()).ToArray();
        }

        public double[] Predict(double[] inputs)
        {
            neurons[0] = inputs;

            for (int i = 1; i < layers.Length; i++)
            {
                for (int j = 0; j < layers[i]; j++)
                {
                    double sum = biases[i][j];
                    for (int k = 0; k < layers[i - 1]; k++)
                        sum += neurons[i - 1][k] * weights[i - 1][j][k];

                    neurons[i][j] = sum;
                }
                neurons[i] = activationFunctions[i - 1].Activate(neurons[i]);
            }

            return neurons[neurons.Length - 1];
        }

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
                    for (int i = 0; i < errors[layers.Length - 1].Length; i++)
                    {
                        errors[layers.Length - 1][i] = expectedOutputs[sample][i] - output[i];
                        errorSum += Math.Pow(errors[layers.Length - 1][i], 2);
                    }

                    for (int i = layers.Length - 2; i >= 0; i--)
                    {
                        errors[i] = new double[layers[i]];
                        for (int j = 0; j < layers[i]; j++)
                        {
                            double errorGradientSum = 0;
                            for (int k = 0; k < layers[i + 1]; k++)
                                errorGradientSum += weights[i][k][j] * errors[i + 1][k];

                            double[] outputDerivatives = activationFunctions[i].Derivative(neurons[i], neurons[i + 1]);
                            errors[i][j] = errorGradientSum * outputDerivatives[j];
                        }
                    }

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

                Debug.WriteLine($"Época {epoch + 1}, Error: {errorSum / inputs.Length}");
            }
        }

        public int GetMaxIndex(double[] array)
        {
            return Array.IndexOf(array, array.Max());
        }

        public void TestNetwork(double[][] inputs)
        {
            foreach (var input in inputs)
            {
                Step stepFunction = new Step();
                double[] output = Predict(input);
                double[] binaryOutput = output.Select(stepFunction.Activate).ToArray();
                Debug.WriteLine($"Entrada: {string.Join(", ", input)} => Salida: {string.Join(", ", output)} => Salida Binaria: {string.Join(", ", binaryOutput)}");
            }
        }

        // Codificación one-hot de etiquetas
        public double[] OneHotEncode(int label, int size)
        {
            double[] oneHot = new double[size];
            oneHot[label] = 1;
            return oneHot;
        }
    }
}
