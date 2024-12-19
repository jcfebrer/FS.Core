using System;

namespace FSIA.ConvolutionalNetwork
{
    public class SoftmaxLayer : ILayer
    {
        private double[] lastInput;

        public double[] Forward(double[] input)
        {
            lastInput = input;
            double max = double.MinValue;
            foreach (var value in input)
                if (value > max) max = value;

            double sum = 0;
            double[] output = new double[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = Math.Exp(input[i] - max); // Evitar overflow
                sum += output[i];
            }

            for (int i = 0; i < output.Length; i++)
            {
                output[i] /= sum;
            }

            return output;
        }

        public double[] Backward(double[] outputGradient)
        {
            double[] gradients = new double[lastInput.Length];
            for (int i = 0; i < lastInput.Length; i++)
            {
                gradients[i] = lastInput[i] * (1 - lastInput[i]) * outputGradient[i];
            }
            return gradients;
        }

        public void UpdateWeights(double learningRate) { /* Softmax no tiene pesos */ }

        public void Initialize(int inputDepth, int inputHeight, int inputWidth) { }

        public int[] GetOutputShape(int inputDepth, int inputHeight, int inputWidth)
        {
            return new int[] { inputDepth * inputHeight * inputWidth };
        }

        public double[,,] Forward(double[,,] input)
        {
            return input;
        }

        public double[,,] Backward(double[,,] outputGradient)
        {
            return outputGradient;
        }
    }
}
