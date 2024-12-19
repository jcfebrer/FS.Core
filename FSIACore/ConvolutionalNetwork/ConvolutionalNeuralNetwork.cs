using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FSIACore.ConvolutionalNetwork
{
    public class ConvolutionalNeuralNetwork
    {
        private readonly List<ILayer> layers = new List<ILayer>();

        public void AddLayer(ILayer layer)
        {
            layers.Add(layer);
        }

        /// <summary>
        /// Realiza la propagación hacia adelante.
        /// </summary>
        public double[,,] Forward(double[,,] input)
        {
            double[,,] output = input;
            foreach (var layer in layers)
            {
                output = layer.Forward(output);
            }
            return output;
        }

        /// <summary>
        /// Realiza la propagación hacia atrás.
        /// </summary>
        public double[,,] Backward(double[,,] lossGradient)
        {
            double[,,] gradient = lossGradient;
            for (int i = layers.Count - 1; i >= 0; i--)
            {
                gradient = layers[i].Backward(gradient);
            }
            return gradient;
        }

        /// <summary>
        /// Entrena la red neuronal en un conjunto de datos.
        /// </summary>
        public void Train(
            double[][,,] trainImages,
            int[] trainLabels,
            int epochs,
            int batchSize,
            double learningRate)
        {
            int numSamples = trainImages.Length;

            for (int epoch = 0; epoch < epochs; epoch++)
            {
                double epochLoss = 0.0;

                for (int batchStart = 0; batchStart < numSamples; batchStart += batchSize)
                {
                    int batchEnd = Math.Min(batchStart + batchSize, numSamples);
                    int currentBatchSize = batchEnd - batchStart;

                    var batchImages = new double[currentBatchSize][,,];
                    Array.Copy(trainImages, batchStart, batchImages, 0, currentBatchSize);

                    var batchLabels = new int[currentBatchSize];
                    Array.Copy(trainLabels, batchStart, batchLabels, 0, currentBatchSize);

                    double batchLoss = 0.0;

                    for (int i = 0; i < currentBatchSize; i++)
                    {
                        double[,,] output = Forward(batchImages[i]);
                        double[] oneHotLabel = OneHotEncode(batchLabels[i], layers[layers.Count - 1].GetOutputShape(1, 1, 1)[0]);
                        double[] lossGradient1D = ComputeLossGradient(output, oneHotLabel, ref batchLoss);
                        double[,,] lossGradient3D = ConvertTo3D(lossGradient1D, output.GetLength(0), output.GetLength(1), output.GetLength(2));
                        Backward(lossGradient3D);
                    }

                    epochLoss += batchLoss / currentBatchSize;

                    foreach (var layer in layers)
                    {
                        layer.UpdateWeights(learningRate);
                    }
                }

                Debug.WriteLine($"Epoch {epoch + 1}/{epochs}: Loss = {epochLoss / numSamples}");
            }
        }

        /// <summary>
        /// Evalúa la red neuronal en un conjunto de datos de prueba.
        /// </summary>
        public double Test(double[][,,] testImages, int[] testLabels)
        {
            int numSamples = testImages.Length;
            int correctPredictions = 0;

            for (int i = 0; i < numSamples; i++)
            {
                double[,,] output = Forward(testImages[i]);
                int predictedLabel = GetPredictedLabel(output);
                if (predictedLabel == testLabels[i])
                {
                    correctPredictions++;
                }
            }

            return (double)correctPredictions / numSamples;
        }

        /// <summary>
        /// Calcula el gradiente de la pérdida.
        /// </summary>
        private double[] ComputeLossGradient(double[,,] output, double[] target, ref double batchLoss)
        {
            int size = output.Length;
            double[] gradient = new double[size];

            batchLoss += ComputeLoss(output, target);

            int index = 0;
            for (int d = 0; d < output.GetLength(0); d++)
            {
                for (int h = 0; h < output.GetLength(1); h++)
                {
                    for (int w = 0; w < output.GetLength(2); w++)
                    {
                        gradient[index] = output[d, h, w] - target[index++];
                    }
                }
            }

            return gradient;
        }

        /// <summary>
        /// Calcula la pérdida entre la salida y el objetivo.
        /// </summary>
        private double ComputeLoss(double[,,] output, double[] target)
        {
            double loss = 0.0;
            int index = 0;

            for (int d = 0; d < output.GetLength(0); d++)
            {
                for (int h = 0; h < output.GetLength(1); h++)
                {
                    for (int w = 0; w < output.GetLength(2); w++)
                    {
                        loss += Math.Pow(output[d, h, w] - target[index++], 2);
                    }
                }
            }

            return loss / 2;
        }

        private double[] OneHotEncode(int label, int numClasses)
        {
            double[] oneHot = new double[numClasses];
            oneHot[label] = 1.0;
            return oneHot;
        }

        public int GetPredictedLabel(double[,,] output)
        {
            int index = 0;
            double max = double.MinValue;

            for (int d = 0; d < output.GetLength(0); d++)
            {
                for (int h = 0; h < output.GetLength(1); h++)
                {
                    for (int w = 0; w < output.GetLength(2); w++)
                    {
                        if (output[d, h, w] > max)
                        {
                            max = output[d, h, w];
                            index = w; // Supone que cada clase es un valor en un eje diferente
                        }
                    }
                }
            }

            return index;
        }

        private double[,,] ConvertTo3D(double[] input, int depth, int height, int width)
        {
            double[,,] output = new double[depth, height, width];
            int index = 0;

            for (int d = 0; d < depth; d++)
            {
                for (int h = 0; h < height; h++)
                {
                    for (int w = 0; w < width; w++)
                    {
                        output[d, h, w] = input[index++];
                    }
                }
            }

            return output;
        }
    }
}