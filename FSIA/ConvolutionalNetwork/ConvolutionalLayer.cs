using System;

namespace FSIA.ConvolutionalNetwork
{
    public class ConvolutionalLayer : ILayer
    {
        private double[,,,] filters; // Dimensiones: [número de filtros, profundidad, altura, ancho]
        private double[] biases;
        private double[,,] lastInput;
        private int stride;
        private int padding;

        public ConvolutionalLayer(int inputDepth, int numFilters, int filterSize, int stride = 1, int padding = 0)
        {
            this.stride = stride;
            this.padding = padding;

            filters = new double[numFilters, inputDepth, filterSize, filterSize];
            biases = new double[numFilters];

            InitializeWeights();
        }

        private void InitializeWeights()
        {
            var rand = new Random();
            for (int f = 0; f < filters.GetLength(0); f++)
                for (int d = 0; d < filters.GetLength(1); d++)
                    for (int h = 0; h < filters.GetLength(2); h++)
                        for (int w = 0; w < filters.GetLength(3); w++)
                            filters[f, d, h, w] = rand.NextDouble() * 0.1; // Inicialización aleatoria pequeña
        }

        public double[,,] Forward(double[,,] input)
        {
            lastInput = input;

            int inputDepth = input.GetLength(0);
            int inputHeight = input.GetLength(1);
            int inputWidth = input.GetLength(2);

            int filterSize = filters.GetLength(2);
            int numFilters = filters.GetLength(0);

            int outputHeight = (inputHeight - filterSize + 2 * padding) / stride + 1;
            int outputWidth = (inputWidth - filterSize + 2 * padding) / stride + 1;

            double[,,] output = new double[numFilters, outputHeight, outputWidth];

            for (int f = 0; f < numFilters; f++)
                for (int h = 0; h < outputHeight; h++)
                    for (int w = 0; w < outputWidth; w++)
                    {
                        double sum = biases[f];
                        for (int d = 0; d < inputDepth; d++)
                            for (int fh = 0; fh < filterSize; fh++)
                                for (int fw = 0; fw < filterSize; fw++)
                                {
                                    int ih = h * stride + fh - padding;
                                    int iw = w * stride + fw - padding;

                                    if (ih >= 0 && ih < inputHeight && iw >= 0 && iw < inputWidth)
                                        sum += input[d, ih, iw] * filters[f, d, fh, fw];
                                }
                        output[f, h, w] = sum;
                    }

            return output;
        }

        public double[,,] Backward(double[,,] outputGradient) => throw new NotImplementedException();

        public void UpdateWeights(double learningRate) => throw new NotImplementedException();

        public double[] Forward(double[] input) => throw new NotImplementedException();

        public double[] Backward(double[] outputGradient) => throw new NotImplementedException();

        public void Initialize(int inputDepth, int inputHeight, int inputWidth) { /* Nada adicional */ }

        public int[] GetOutputShape(int inputDepth, int inputHeight, int inputWidth)
        {
            int filterSize = filters.GetLength(2);
            int outputHeight = (inputHeight - filterSize + 2 * padding) / stride + 1;
            int outputWidth = (inputWidth - filterSize + 2 * padding) / stride + 1;

            return new[] { filters.GetLength(0), outputHeight, outputWidth }; // [num filtros, alto, ancho]
        }
    }
}
