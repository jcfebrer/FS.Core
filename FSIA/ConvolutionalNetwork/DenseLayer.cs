using System;

namespace FSIA.ConvolutionalNetwork
{
    public class DenseLayer : ILayer
    {
        private double[,] weights;
        private double[] biases;
        private double[] lastInput;
        private double[] lastOutput;

        public DenseLayer(int inputSize, int outputSize)
        {
            weights = new double[inputSize, outputSize];
            biases = new double[outputSize];
            InitializeWeights();
        }

        private void InitializeWeights()
        {
            var rand = new Random();
            for (int i = 0; i < weights.GetLength(0); i++)
                for (int j = 0; j < weights.GetLength(1); j++)
                    weights[i, j] = rand.NextDouble() * 0.1;
        }

        public double[] Forward(double[] input)
        {
            lastInput = input;
            int outputSize = biases.Length;
            double[] output = new double[outputSize];

            for (int j = 0; j < outputSize; j++)
            {
                double sum = biases[j];
                for (int i = 0; i < input.Length; i++)
                    sum += input[i] * weights[i, j];
                output[j] = sum;
            }
            lastOutput = output;
            return output;
        }

        public double[] Backward(double[] outputGradient)
        {
            int inputSize = lastInput.Length;
            int outputSize = outputGradient.Length;

            double[] inputGradient = new double[inputSize];
            double[,] weightGradients = new double[inputSize, outputSize];
            double[] biasGradients = new double[outputSize];

            for (int j = 0; j < outputSize; j++)
            {
                biasGradients[j] = outputGradient[j];
                for (int i = 0; i < inputSize; i++)
                {
                    weightGradients[i, j] = lastInput[i] * outputGradient[j];
                    inputGradient[i] += weights[i, j] * outputGradient[j];
                }
            }

            for (int i = 0; i < inputSize; i++)
                for (int j = 0; j < outputSize; j++)
                    weights[i, j] -= weightGradients[i, j];

            for (int j = 0; j < outputSize; j++)
                biases[j] -= biasGradients[j];

            return inputGradient;
        }

        public void UpdateWeights(double learningRate) { /* Ya actualizados */ }

        public double[,,] Forward(double[,,] input) => input;

        public double[,,] Backward(double[,,] outputGradient) => outputGradient;

        public void Initialize(int inputDepth, int inputHeight, int inputWidth) { /* No aplica */ }

        public int[] GetOutputShape(int inputDepth, int inputHeight, int inputWidth)
        {
            return new[] { biases.Length }; // Salida plana
        }
    }
}
