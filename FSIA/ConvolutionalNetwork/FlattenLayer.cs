using System;

namespace FSIA.ConvolutionalNetwork
{
    public class FlattenLayer : ILayer
    {
        private int inputDepth;
        private int inputHeight;
        private int inputWidth;

        public double[] Forward(double[,,] input)
        {
            inputDepth = input.GetLength(0);
            inputHeight = input.GetLength(1);
            inputWidth = input.GetLength(2);

            int flattenedSize = inputDepth * inputHeight * inputWidth;
            double[] output = new double[flattenedSize];

            int index = 0;
            for (int d = 0; d < inputDepth; d++)
            {
                for (int h = 0; h < inputHeight; h++)
                {
                    for (int w = 0; w < inputWidth; w++)
                    {
                        output[index++] = input[d, h, w];
                    }
                }
            }
            return output;
        }

        public double[,,] Backward(double[] outputGradient, int inputWidth, int inputHeight, int inputDepth)
        {
            double[,,] reshapedGradients = new double[inputDepth, inputHeight, inputWidth];
            int index = 0;

            for (int d = 0; d < inputDepth; d++)
            {
                for (int h = 0; h < inputHeight; h++)
                {
                    for (int w = 0; w < inputWidth; w++)
                    {
                        reshapedGradients[d, h, w] = outputGradient[index++];
                    }
                }
            }
            return reshapedGradients;
        }

        public double[,,] Backward(double[] outputGradient)
        {
            return Backward(outputGradient, inputWidth, inputHeight, inputDepth);
        }

        public double[] Forward(double[] input)
        {
            return input;
        }

        public double[,,] Backward(double[,,] outputGradient)
        {
            return outputGradient;
        }

        double[,,] ILayer.Forward(double[,,] input)
        {
            return input;
        }

        double[] ILayer.Backward(double[] outputGradient)
        {
            return outputGradient;
        }

        public void UpdateWeights(double learningRate) { }

        public void Initialize(int inputDepth, int inputHeight, int inputWidth) { }

        public int[] GetOutputShape(int inputDepth, int inputHeight, int inputWidth)
        {
            return new int[] { inputDepth * inputHeight * inputWidth };
        }
    }
}
