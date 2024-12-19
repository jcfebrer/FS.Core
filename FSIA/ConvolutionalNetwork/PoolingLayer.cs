using System;

namespace FSIA.ConvolutionalNetwork
{
    public class PoolingLayer : ILayer
    {
        private readonly int poolSize;
        private readonly int stride;
        private int inputDepth, inputHeight, inputWidth;
        private int outputHeight, outputWidth;

        private double[,,] lastInput;
        private int[,,] maxIndices;

        public PoolingLayer(int poolSize, int stride)
        {
            this.poolSize = poolSize;
            this.stride = stride;
        }

        public double[,,] Forward(double[,,] input)
        {
            inputDepth = input.GetLength(0);
            inputHeight = input.GetLength(1);
            inputWidth = input.GetLength(2);

            outputHeight = (inputHeight - poolSize) / stride + 1;
            outputWidth = (inputWidth - poolSize) / stride + 1;

            double[,,] output = new double[inputDepth, outputHeight, outputWidth];
            maxIndices = new int[inputDepth, outputHeight, outputWidth];
            lastInput = input;

            for (int d = 0; d < inputDepth; d++)
            {
                for (int h = 0; h < outputHeight; h++)
                {
                    for (int w = 0; w < outputWidth; w++)
                    {
                        int hStart = h * stride;
                        int wStart = w * stride;

                        double maxVal = double.MinValue;
                        int maxIndex = -1;

                        for (int ph = 0; ph < poolSize; ph++)
                        {
                            for (int pw = 0; pw < poolSize; pw++)
                            {
                                int hIndex = hStart + ph;
                                int wIndex = wStart + pw;

                                double val = input[d, hIndex, wIndex];
                                if (val > maxVal)
                                {
                                    maxVal = val;
                                    maxIndex = ph * poolSize + pw;
                                }
                            }
                        }

                        output[d, h, w] = maxVal;
                        maxIndices[d, h, w] = maxIndex;
                    }
                }
            }
            return output;
        }

        public double[,,] Backward(double[,,] outputGradient)
        {
            double[,,] inputGradient = new double[inputDepth, inputHeight, inputWidth];

            for (int d = 0; d < inputDepth; d++)
            {
                for (int h = 0; h < outputHeight; h++)
                {
                    for (int w = 0; w < outputWidth; w++)
                    {
                        int maxIndex = maxIndices[d, h, w];
                        int hIndex = h * stride + maxIndex / poolSize;
                        int wIndex = w * stride + maxIndex % poolSize;

                        inputGradient[d, hIndex, wIndex] += outputGradient[d, h, w];
                    }
                }
            }

            return inputGradient;
        }

        public void UpdateWeights(double learningRate) { /* PoolingLayer no tiene pesos */ }

        public void Initialize(int inputDepth, int inputHeight, int inputWidth) { }

        public int[] GetOutputShape(int inputDepth, int inputHeight, int inputWidth)
        {
            int outputHeight = (inputHeight - poolSize) / stride + 1;
            int outputWidth = (inputWidth - poolSize) / stride + 1;
            return new int[] { inputDepth, outputHeight, outputWidth };
        }

        public double[] Forward(double[] input)
        {
            return input;
        }

        public double[] Backward(double[] outputGradient)
        {
            return outputGradient;
        }
    }
}
