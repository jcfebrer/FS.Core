using FSIA;
using FSIA.ConvolutionalNetwork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FSTestsCore.FSIA
{
    [TestClass]
    public class ConvolutionalNetworkTest
    {
        [TestMethod]
        public void TestConvolutionalNetwork()
        {
            // Rutas de los archivos MNIST
            string trainingImagesPath = "C:\\Users\\jcfeb\\Downloads\\train-images.idx3-ubyte";
            string trainingLabelsPath = "C:\\Users\\jcfeb\\Downloads\\train-labels.idx1-ubyte";
            string testImagesPath = "C:\\Users\\jcfeb\\Downloads\\t10k-images.idx3-ubyte";
            string testLabelsPath = "C:\\Users\\jcfeb\\Downloads\\t10k-labels.idx1-ubyte";

            // Cargar los datos
            var mnistLoader = new MNISTLoader();
            var (trainImagesRaw, trainLabelsRaw) = mnistLoader.Load(trainingImagesPath, trainingLabelsPath);
            var (testImagesRaw, testLabelsRaw) = mnistLoader.Load(testImagesPath, testLabelsPath);

            // Normalizar imágenes
            double[][,,] trainImages = mnistLoader.NormalizeImages3D(trainImagesRaw);
            double[][,,] testImages = mnistLoader.NormalizeImages3D(testImagesRaw);

            // Convertir etiquetas a int[]
            int[] trainLabels = trainLabelsRaw.Select(label => (int)label).ToArray();
            int[] testLabels = testLabelsRaw.Select(label => (int)label).ToArray();

            // Crear la red neuronal convolucional
            var cnn = new ConvolutionalNeuralNetwork();

            // Configurar las capas
            cnn.AddLayer(new ConvolutionalLayer(1, 8, 3, stride: 1, padding: 1));       // Capa convolucional
            cnn.AddLayer(Activators.ReLU());                                            // Activación
            cnn.AddLayer(new PoolingLayer(2, 2));                                       // Pooling (max-pooling 2x2)
            cnn.AddLayer(new ConvolutionalLayer(8, 16, 3, stride: 1, padding: 1));      // Segunda convolucional
            cnn.AddLayer(Activators.ReLU());                                            // Activación
            cnn.AddLayer(new PoolingLayer(2, 2));                                       // Pooling (max-pooling 2x2)
            cnn.AddLayer(new FlattenLayer());                                           // Aplanado
            cnn.AddLayer(new DenseLayer(16 * 7 * 7, 128));                              // Capa densa
            cnn.AddLayer(Activators.ReLU());                                            // Activación
            cnn.AddLayer(new DenseLayer(128, 10));                                      // Capa de salida
            cnn.AddLayer(new SoftmaxLayer());                                           // Softmax para clasificación

            // Visualizar una imagen
            mnistLoader.PrintImage3D(trainImages[0]);

            // Entrenar la red
            Debug.WriteLine("Iniciando entrenamiento...(probamos con las 10 primeras imágenes)");
            cnn.Train(trainImages, trainLabels, epochs: 5, batchSize: 32, learningRate: 0.01);

            // Evaluación
            Debug.WriteLine("Evaluando la red neuronal...");
            double accuracy = cnn.Test(testImages, testLabels);
            Debug.WriteLine($"Precisión en el conjunto de prueba: {accuracy:P2}");

            // Evaluar en el conjunto de prueba y visualizar imágenes
            Debug.WriteLine("Evaluando en ejemplos individuales del conjunto de prueba...");
            for (int i = 0; i < 10; i++)
            {
                double[,,] image = testImages[i];
                double[,,] predictedOutput = cnn.Forward(image);

                int predictedClass = cnn.GetPredictedLabel(predictedOutput);
                int actualClass = testLabels[i];

                Debug.WriteLine($"Clase real: {actualClass}, Clase predicha: {predictedClass}");

                // Visualizar la imagen
                mnistLoader.PrintImage3D(image);
            }
        }

        [TestMethod]
        public void TestDenseLayer()
        {
            var layers = new List<ILayer>();

            // Agrega una capa densa a la lista de capas
            layers.Add(new DenseLayer(inputSize: 128, outputSize: 64));

            // Propagación hacia adelante en la red
            double[] inputs = new double[128]; // Datos simulados de entrada
            foreach (var layer in layers)
            {
                inputs = layer.Forward(inputs);
            }

            // Simulación de gradientes para retropropagación
            double[] outputGradients = new double[64];
            for (int i = layers.Count - 1; i >= 0; i--)
            {
                outputGradients = layers[i].Backward(inputs);
            }

            // Actualización de pesos
            foreach (var layer in layers)
            {
                layer.UpdateWeights(learningRate: 0.01);
            }
        }
    }
}
