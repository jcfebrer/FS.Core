using FSLibrary;
using System;

namespace FSIA.ConvolutionalNetwork
{
    public class ActivationLayer : ILayer
    {
        private readonly Func<double, double> activationFunc;
        private readonly Func<double, double> activationDerivative;
        private double[,,] lastInput;

        public ActivationLayer(Func<double, double> func, Func<double, double> derivative)
        {
            activationFunc = func;
            activationDerivative = derivative;
        }

        public double[] Forward(double[] input) => input.ConvertAll(activationFunc);

        public double[,,] Forward(double[,,] input)
        {
            lastInput = input;
            int depth = input.GetLength(0), height = input.GetLength(1), width = input.GetLength(2);
            double[,,] output = new double[depth, height, width];

            for (int d = 0; d < depth; d++)
                for (int h = 0; h < height; h++)
                    for (int w = 0; w < width; w++)
                        output[d, h, w] = activationFunc(input[d, h, w]);

            return output;
        }

        public double[] Backward(double[] outputGradient) => throw new NotImplementedException();

        public double[,,] Backward(double[,,] outputGradient)
        {
            int depth = outputGradient.GetLength(0), height = outputGradient.GetLength(1), width = outputGradient.GetLength(2);
            double[,,] inputGradient = new double[depth, height, width];

            for (int d = 0; d < depth; d++)
                for (int h = 0; h < height; h++)
                    for (int w = 0; w < width; w++)
                        inputGradient[d, h, w] = outputGradient[d, h, w] * activationDerivative(lastInput[d, h, w]);

            return inputGradient;
        }

        public void UpdateWeights(double learningRate) { /* No tiene pesos */ }

        public void Initialize(int inputDepth, int inputHeight, int inputWidth) { /* Sin inicialización */ }

        public int[] GetOutputShape(int inputDepth, int inputHeight, int inputWidth) => new[] { inputDepth, inputHeight, inputWidth };
    }
}
