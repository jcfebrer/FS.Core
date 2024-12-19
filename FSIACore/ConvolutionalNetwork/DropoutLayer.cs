using System;

namespace FSIACore.ConvolutionalNetwork
{
    public class DropoutLayer : ILayer
    {
        private double dropoutRate;
        private Random rand = new Random();
        private double[,,] mask;

        public DropoutLayer(double rate)
        {
            dropoutRate = rate;
        }

        public double[,,] Forward(double[,,] input)
        {
            mask = new double[input.GetLength(0), input.GetLength(1), input.GetLength(2)];
            double[,,] output = new double[input.GetLength(0), input.GetLength(1), input.GetLength(2)];

            for (int d = 0; d < input.GetLength(0); d++)
                for (int h = 0; h < input.GetLength(1); h++)
                    for (int w = 0; w < input.GetLength(2); w++)
                    {
                        mask[d, h, w] = rand.NextDouble() > dropoutRate ? 1.0 : 0.0;
                        output[d, h, w] = input[d, h, w] * mask[d, h, w];
                    }

            return output;
        }

        public double[,,] Backward(double[,,] outputGradient)
        {
            double[,,] inputGradient = new double[outputGradient.GetLength(0), outputGradient.GetLength(1), outputGradient.GetLength(2)];

            for (int d = 0; d < outputGradient.GetLength(0); d++)
                for (int h = 0; h < outputGradient.GetLength(1); h++)
                    for (int w = 0; w < outputGradient.GetLength(2); w++)
                        inputGradient[d, h, w] = outputGradient[d, h, w] * mask[d, h, w];

            return inputGradient;
        }

        public void UpdateWeights(double learningRate) { /* No aplica */ }

        public double[] Forward(double[] input) => input;

        public double[] Backward(double[] outputGradient) => outputGradient;

        public void Initialize(int inputDepth, int inputHeight, int inputWidth) { /* Nada */ }

        public int[] GetOutputShape(int inputDepth, int inputHeight, int inputWidth)
        {
            return new[] { inputDepth, inputHeight, inputWidth };
        }
    }
}
