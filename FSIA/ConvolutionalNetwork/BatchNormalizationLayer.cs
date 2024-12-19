using System;

namespace FSIA.ConvolutionalNetwork
{
    public class BatchNormalizationLayer : ILayer
    {
        private double[] gamma;
        private double[] beta;

        private double[] runningMean;
        private double[] runningVariance;

        private double[,,] lastInput;
        private double epsilon = 1e-5;

        public void Initialize(int inputDepth, int inputHeight, int inputWidth)
        {
            int size = inputDepth * inputHeight * inputWidth;
            gamma = new double[size];
            beta = new double[size];
            runningMean = new double[size];
            runningVariance = new double[size];

            for (int i = 0; i < size; i++) gamma[i] = 1.0; // Escalamiento inicial
        }

        public double[,,] Forward(double[,,] input)
        {
            lastInput = input;
            int depth = input.GetLength(0);
            int height = input.GetLength(1);
            int width = input.GetLength(2);
            double[,,] output = new double[depth, height, width];

            for (int d = 0; d < depth; d++)
                for (int h = 0; h < height; h++)
                    for (int w = 0; w < width; w++)
                    {
                        int index = d * height * width + h * width + w;
                        double norm = (input[d, h, w] - runningMean[index]) / Math.Sqrt(runningVariance[index] + epsilon);
                        output[d, h, w] = gamma[index] * norm + beta[index];
                    }

            return output;
        }

        public double[,,] Backward(double[,,] outputGradient) => throw new NotImplementedException();

        public void UpdateWeights(double learningRate) => throw new NotImplementedException();

        public double[] Forward(double[] input) => throw new NotImplementedException();

        public double[] Backward(double[] outputGradient) => throw new NotImplementedException();

        public int[] GetOutputShape(int inputDepth, int inputHeight, int inputWidth) => new[] { inputDepth, inputHeight, inputWidth };
    }
}
