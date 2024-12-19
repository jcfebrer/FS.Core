using FSIA;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FSIA.NeuralNetworkActivations;

namespace FSTests.FSIA
{
    [TestClass]
    public class NeuralNetworkTest
    {
        [TestMethod]
        public void TestNOT()
        {
            // Red con 1 entradas, 2 neuronas en la capa oculta y 1 salida
            int[] layers = new int[] { 1, 2, 1 };
            var activations = new IActivationFunction[]
            {
                new Sigmoid(),  // Capa oculta
                new Sigmoid()   // Capa de salida
            };

            NeuralNetwork nn = new NeuralNetwork(layers, activations);

            double[][] inputs = new double[][]
            {
            new double[] { 0 },
            new double[] { 1 }
            };

            double[][] targets = new double[][]
            {
            new double[] { 1 },
            new double[] { 0 }
            };

            nn.Train(inputs, targets, epochs: 2000, learningRate: 0.1);

            Debug.WriteLine("NOT lógico:");
            nn.TestNetwork(inputs);
        }

        [TestMethod]
        public void TestOR()
        {
            // Red con 2 entradas, 2 neuronas en la capa oculta y 1 salida
            int[] layers = new int[] { 2, 2, 1 };
            var activations = new IActivationFunction[]
            {
                new ReLU(),  // Capa oculta
                new Sigmoid()   // Capa de salida
            };

            NeuralNetwork nn = new NeuralNetwork(layers, activations);

            double[][] inputs = new double[][]
            {
            new double[] { 0, 0 },
            new double[] { 0, 1 },
            new double[] { 1, 0 },
            new double[] { 1, 1 }
            };

            double[][] targets = new double[][]
            {
            new double[] { 0 },
            new double[] { 1 },
            new double[] { 1 },
            new double[] { 1 }
            };

            nn.Train(inputs, targets, epochs: 2000, learningRate: 0.1);

            Debug.WriteLine("OR lógico:");
            nn.TestNetwork(inputs);
        }

        [TestMethod]
        public void TestXOR()
        {
            // Red con 2 entradas, 2 neuronas en la capa oculta y 1 salida
            int[] layers = new int[] { 2, 4, 1 };
            var activations = new IActivationFunction[]
            {
                new ReLU(),  // Capa oculta
                new Sigmoid()   // Capa de salida
            };

            NeuralNetwork nn = new NeuralNetwork(layers, activations);

            double[][] inputs = new double[][]
            {
            new double[] { 0, 0 },
            new double[] { 0, 1 },
            new double[] { 1, 0 },
            new double[] { 1, 1 }
            };

            double[][] targets = new double[][]
            {
            new double[] { 0 },
            new double[] { 1 },
            new double[] { 1 },
            new double[] { 0 }
            };

            nn.Train(inputs, targets, epochs: 5000, learningRate: 0.01);

            Debug.WriteLine("XOR lógico:");
            nn.TestNetwork(inputs);
        }

        [TestMethod]
        public void TestAND()
        {
            // Red con 2 entradas, 2 neuronas en la capa oculta y 1 salida
            int[] layers = new int[] { 2, 2, 1 };
            var activations = new IActivationFunction[]
            {
                new ReLU(),  // Capa oculta
                new Sigmoid()   // Capa de salida
            };

            NeuralNetwork nn = new NeuralNetwork(layers, activations);

            double[][] inputs = new double[][]
            {
            new double[] { 0, 0 },
            new double[] { 0, 1 },
            new double[] { 1, 0 },
            new double[] { 1, 1 }
            };

            double[][] targets = new double[][]
            {
            new double[] { 0 },
            new double[] { 0 },
            new double[] { 0 },
            new double[] { 1 }
            };

            nn.Train(inputs, targets, epochs: 2000, learningRate: 0.01);

            Debug.WriteLine("AND lógico:");
            nn.TestNetwork(inputs);
        }

        [TestMethod]
        public void TestPrediccionContinua()
        {
            int[] layers = new int[] { 1, 5, 1 };  // 1 entrada, 5 neuronas en la capa oculta, 1 salida
            var activations = new IActivationFunction[]
            {
                new ReLU(),  // Capa oculta
                new Sigmoid()   // Capa de salida
            };

            NeuralNetwork nn = new NeuralNetwork(layers, activations);

            double[][] inputs = new double[][]
            {
            new double[] { -1.0 },
            new double[] { -0.5 },
            new double[] { 0.0 },
            new double[] { 0.5 },
            new double[] { 1.0 }
            };

            double[][] targets = new double[][]
            {
            new double[] { 1.0 },
            new double[] { 0.25 },
            new double[] { 0.0 },
            new double[] { 0.25 },
            new double[] { 1.0 }
            };

            nn.Train(inputs, targets, epochs: 10000, learningRate: 0.1);

            Debug.WriteLine("Predicción continua (y = x^2):");
            nn.TestNetwork(inputs);
        }

        [TestMethod]
        public void TestClasificacionMultiple()
        {
            int[] layers = new int[] { 4, 5, 3 };  // 4 entradas, 5 neuronas en la capa oculta, 3 salidas
            var activations = new IActivationFunction[]
            {
                new Sigmoid(),  // Capa oculta
                new Sigmoid()   // Capa de salida
            };

            NeuralNetwork nn = new NeuralNetwork(layers, activations);

            double[][] inputs = new double[][]
            {
            new double[] { 0.2, 0.4, 0.1, 0.9 },
            new double[] { 0.8, 0.3, 0.5, 0.2 },
            new double[] { 0.9, 0.1, 0.4, 0.7 }
            };

            double[][] targets = new double[][]
            {
            new double[] { 1, 0, 0 },
            new double[] { 0, 1, 0 },
            new double[] { 0, 0, 1 }
            };

            nn.Train(inputs, targets, epochs: 10000, learningRate: 0.01);

            Debug.WriteLine("Clasificación múltiple:");
            nn.TestNetwork(inputs);
        }

        [TestMethod]
        public void TestX2()
        {
            // Definir estructura de la red: 1 entrada, 3 capas ocultas de 5 neuronas cada una, 1 salida
            int[] layers = new int[] { 1, 5, 5, 5, 1 };

            // Usar LeakyReLU para capas ocultas y Sigmoid para la salida
            var activations = new IActivationFunction[]
            {
                new LeakyReLU(), // Oculta 1
                new LeakyReLU(),  // Oculta 2
                new LeakyReLU(), // Oculta 3
                new Sigmoid()   // Capa de salida
            };

            // Crear la red neuronal
            NeuralNetwork nn = new NeuralNetwork(layers, activations);

            // Entradas: Valores entre -1 y 1
            double[][] inputs = new double[][]
            {
            new double[] { -1.0 },
            new double[] { -0.5 },
            new double[] { 0.0 },
            new double[] { 0.5 },
            new double[] { 1.0 }
            };

            double[][] targets = new double[][]
            {
            new double[] { 1.0 },
            new double[] { 0.25 },
            new double[] { 0.0 },
            new double[] { 0.25 },
            new double[] { 1.0 }
            };

            nn.Train(inputs, targets, epochs: 10000, learningRate: 0.1);

            Debug.WriteLine("Aproximación de la función y = x^2:");
            nn.TestNetwork(inputs);
        }

        [TestMethod]
        public void Text3Capas()
        {
            int[] layers = new int[] { 2, 3, 2, 1 };  // 2 entradas, 3 neuronas en la capa oculta, 2 en la segunda capa oculta, 1 salida
            var activations = new IActivationFunction[]
            {
                new ReLU(),  // Capa oculta1
                new ReLU(),  // Capa oculta2
                new Sigmoid()   // Capa de salida
            };

            NeuralNetwork nn = new NeuralNetwork(layers, activations);

            double[][] inputs = new double[][]
            {
            new double[] { 0, 0 },
            new double[] { 0, 1 },
            new double[] { 1, 0 },
            new double[] { 1, 1 }
            };

            double[][] targets = new double[][]
            {
            new double[] { 0 },
            new double[] { 1 },
            new double[] { 1 },
            new double[] { 0 }
            };

            nn.Train(inputs, targets, epochs: 5000, learningRate: 0.01);

            Debug.WriteLine("Red con 3 capas:");
            nn.TestNetwork(inputs);
        }

        /// <summary>
        /// Clasificación Multiclase (Iris Dataset simplificado)
        /// Este ejemplo simula un problema de clasificación con 3 categorías. Usaremos un dataset simplificado.
        /// </summary>
        [TestMethod]
        public void Example_ClassificationMulticlass()
        {
            // Configuración de la red
            int[] layers = new int[] { 4, 5, 3 }; // 4 entradas, 5 ocultas, 3 salidas
            IActivationFunction[] activations = new IActivationFunction[]
            {
            new Tanh(),    // Capa oculta
            new Softmax()  // Capa de salida
            };

            // Crear la red neuronal
            var nn = new NeuralNetwork(layers, activations);

            // Datos de entrenamiento: Características del Iris Dataset simplificado
            double[][] inputs = new double[][]
            {
            new double[] { 5.1, 3.5, 1.4, 0.2 }, // Iris-setosa
            new double[] { 4.9, 3.0, 1.4, 0.2 }, // Iris-setosa
            new double[] { 6.3, 3.3, 6.0, 2.5 }, // Iris-virginica
            new double[] { 5.8, 2.7, 5.1, 1.9 }, // Iris-virginica
            new double[] { 5.7, 2.8, 4.1, 1.3 }, // Iris-versicolor
            new double[] { 6.0, 2.7, 5.1, 1.6 }  // Iris-versicolor
            };

            // Etiquetas en formato one-hot: tres especies (setosa, versicolor, virginica)
            double[][] outputs = new double[][]
            {
            new double[] { 1, 0, 0 }, // Iris-setosa
            new double[] { 1, 0, 0 }, // Iris-setosa
            new double[] { 0, 0, 1 }, // Iris-virginica
            new double[] { 0, 0, 1 }, // Iris-virginica
            new double[] { 0, 1, 0 }, // Iris-versicolor
            new double[] { 0, 1, 0 }  // Iris-versicolor
            };

            // Entrenamiento
            nn.Train(inputs, outputs, epochs: 1000, learningRate: 0.1);

            // Pruebas
            Console.WriteLine("Resultados de la red neuronal para Iris Dataset simplificado:");
            nn.TestNetwork(inputs);

            // Probar con datos nuevos
            var testInput = new double[] { 5.9, 3.0, 5.1, 1.8 }; // Esperado: Clase 2

            Debug.WriteLine("Predicción:");
            nn.TestNetwork(testInput);
        }

        /// <summary>
        /// Predicción de Serie Temporal (Sine Wave)
        /// Este ejemplo predice valores futuros en una función sinusoidal.
        /// </summary>
        [TestMethod]
        public void Example_SineWavePrediction()
        {
            int[] layers = new int[] { 1, 16, 1 };  // 1 entradas, 16 neuronas en la capa oculta, 1 salida
            var activations = new IActivationFunction[]
            {
                new Tanh(),  // Capa oculta1
            };

            // Configurar la red neuronal
            var nn = new NeuralNetwork(layers, activations);

            // Generar datos de entrenamiento
            var inputs = new List<double[]>();
            var outputs = new List<double[]>();
            for (double x = 0; x < 2 * Math.PI; x += 0.1)
            {
                inputs.Add(new double[] { x });
                outputs.Add(new double[] { Math.Sin(x) });
            }

            // Entrenar la red
            nn.Train(inputs.ToArray(), outputs.ToArray(), 5000, 0.01);

            // Probar con datos nuevos
            for (double x = 0; x < 2 * Math.PI; x += 0.2)
            {
                var prediction = nn.Predict(new double[] { x });
                Debug.WriteLine($"x: {x}, Sin(x): {Math.Sin(x)}, Predicción: {prediction[0]}");
            }
        }

        /// <summary>
        /// Reconocimiento de dígitos escritos a mano (MNIST simplificado)
        /// Un ejemplo más avanzado sería implementar un clasificador de dígitos escritos a mano(similar al MNIST). Esto requiere una red con más capas y un dataset.
        /// </summary>
        [TestMethod]
        public void Example_MNIST()
        {
            int[] layers = new int[] { 784, 128, 64, 10 };  // 784 entradas, 128 neuronas en la capa oculta, 64 en la segunda capa, 10 salidas
            var activations = new IActivationFunction[]
            {
                new ReLU(),  // Capa oculta1
                new ReLU(),  // Capa oculta2
                new Softmax()   // Capa de salida
            };

            // Configurar la red neuronal (entrada: 28x28 píxeles = 784, salida: 10 clases)
            var nn = new NeuralNetwork(layers, activations);

            // Ruta a los archivos MNIST
            string trainingImagesPath = "C:\\Users\\jcfeb\\Downloads\\train-images.idx3-ubyte";
            string trainingLabelsPath = "C:\\Users\\jcfeb\\Downloads\\train-labels.idx1-ubyte";
            string testImagesPath = "C:\\Users\\jcfeb\\Downloads\\t10k-images.idx3-ubyte";
            string testLabelsPath = "C:\\Users\\jcfeb\\Downloads\\t10k-labels.idx1-ubyte";


            // Normaliza imágenes y codifica etiquetas
            var mnistLoader = new MNISTLoader();
            var (trainImagesRaw, trainLabelsRaw) = mnistLoader.Load(trainingImagesPath, trainingLabelsPath);
            var (testImagesRaw, testLabelsRaw) = mnistLoader.Load(testImagesPath, testLabelsPath);

            double[][] trainImages = mnistLoader.NormalizeImages(trainImagesRaw);
            double[][] trainLabels = mnistLoader.OneHotEncodeLabels(trainLabelsRaw, 10);
            double[][] testImages = mnistLoader.NormalizeImages(testImagesRaw);
            double[][] testLabels = mnistLoader.OneHotEncodeLabels(testLabelsRaw, 10);

            Debug.WriteLine($"Total de imágenes: {trainImages.Length}");
            Debug.WriteLine($"Dimensiones: {trainImagesRaw[0].Length}x{trainImages[0].Length}");

            // Entrenar la red
            Console.WriteLine("Iniciando entrenamiento...");
            nn.Train(trainImages, trainLabels, epochs: 10, learningRate: 0.01);

            // Probar la red neuronal
            int correct = 0;
            Debug.WriteLine("Probando con los primeros 5 ejemplos:");
            for (int f = 0; f < 5; f++)
            {
                var input = trainImages[f];
                var expected = trainLabels[f];
                var prediction = nn.Predict(input);

                int predictedLabel = Array.IndexOf(prediction, prediction.Max());
                Debug.WriteLine($"Etiqueta esperada: {expected}, Predicción: {predictedLabel}");

                // Visualizar una imagen
                mnistLoader.PrintImage(trainImages[f]);

                if (nn.GetMaxIndex(prediction) == nn.GetMaxIndex(trainLabels[f]))
                    correct++;
            }

            Debug.WriteLine($"Precisión en datos de prueba: {correct * 100.0 / trainImages.Length}%");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Example_HousePricePrediction()
        {
            int[] layers = new int[] { 3, 10, 1 };  // 784 entradas, 128 neuronas en la capa oculta, 64 en la segunda capa, 10 salidas
            var activations = new IActivationFunction[]
            {
                new ReLU(),  // Capa oculta1
                new Softmax()
            };

            // Configurar la red neuronal
            var nn = new NeuralNetwork(layers, activations);

            // Dataset (entrada: características; salida: precio)
            var inputs = new double[][]
            {
            new double[] { 2000, 3, 1 }, // 2000 pies cuadrados, 3 habitaciones, 1 baño
            new double[] { 1500, 2, 1 },
            new double[] { 2500, 4, 2 },
            new double[] { 1800, 3, 2 }
            };

            var outputs = new double[][]
            {
            new double[] { 500000 }, // Precio
            new double[] { 350000 },
            new double[] { 700000 },
            new double[] { 450000 }
            };

            // Escalar datos (opcional, mejora la convergencia)
            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    inputs[i][j] /= 1000.0;
                }
                outputs[i][0] /= 1000000.0;
            }

            // Entrenar la red
            nn.Train(inputs, outputs, 1000, 0.01);

            // Probar con datos nuevos
            var testInput = new double[] { 2200, 3, 2 }; // Casa nueva
            var prediction = nn.Predict(testInput.Select(x => x / 1000.0).ToArray());
            Debug.WriteLine($"Predicción del precio (en millones): {prediction[0]}");
        }

        [TestMethod]
        public void TestActivationDerivatives()
        {
            // Prueba para Sigmoid
            IActivationFunction sigmoid = new Sigmoid();
            double input = 0.5;
            double expectedSigmoidDerivative = sigmoid.Activate(input) * (1 - sigmoid.Activate(input));
            Assert.AreEqual(expectedSigmoidDerivative, sigmoid.Derivative(input), 1e-6);

            // Prueba para ReLU
            IActivationFunction relu = new ReLU();
            Assert.AreEqual(1, relu.Derivative(1.0));
            Assert.AreEqual(0, relu.Derivative(-1.0));

            // Prueba para LeakyReLU
            IActivationFunction leakyRelu = new LeakyReLU();
            Assert.AreEqual(1, leakyRelu.Derivative(1.0));
            Assert.AreEqual(0.01, leakyRelu.Derivative(-1.0));

            // Prueba para Tanh
            IActivationFunction tanh = new Tanh();
            double tanhOutput = tanh.Activate(input);
            double expectedTanhDerivative = 1 - tanhOutput * tanhOutput;
            Assert.AreEqual(expectedTanhDerivative, tanh.Derivative(input), 1e-6);

            // Prueba para Step
            IActivationFunction step = new Step();
            Assert.AreEqual(0, step.Derivative(0.5));
            Assert.AreEqual(0, step.Derivative(-1.0));
        }
    }
}