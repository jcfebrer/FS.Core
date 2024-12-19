using System;

namespace FSIA.ConvolutionalNetwork
{
    public class SwishLayer : ILayer
    {
        private double[] lastInput;

        public double[] Forward(double[] input)
        {
            lastInput = input;
            double[] output = new double[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                output[i] = input[i] / (1 + Math.Exp(-input[i]));
            }

            return output;
        }

        public double[] Backward(double[] outputGradient)
        {
            double[] gradients = new double[lastInput.Length];

            for (int i = 0; i < lastInput.Length; i++)
            {
                double sigmoid = 1 / (1 + Math.Exp(-lastInput[i]));
                gradients[i] = outputGradient[i] * (sigmoid + lastInput[i] * sigmoid * (1 - sigmoid));
            }

            return gradients;
        }

        public void UpdateWeights(double learningRate) { /* Swish no tiene pesos */ }

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
