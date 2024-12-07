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
            // Red con 2 entradas, 4 neuronas en la capa oculta y 1 salida
            int[] layers = new int[] { 2, 4, 1 };
            var activations = new IActivationFunction[]
            {
                new Sigmoid(),  // Capa oculta
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

            nn.Train(inputs, targets, epochs: 10000, learningRate: 0.1);

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
                new Sigmoid(),  // Capa oculta
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

            nn.Train(inputs, targets, epochs: 2000, learningRate: 0.1);

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
                new ReLU(),  // Capa oculta
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

            nn.Train(inputs, targets, epochs: 10000, learningRate: 0.1);

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

            nn.Train(inputs, targets, epochs: 5000, learningRate: 0.1);

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
            int[] layers = new int[] { 4, 8, 3 };  // 4 entradas, 8 neuronas en la capa oculta, 3 salidas
            var activations = new IActivationFunction[]
            {
                new ReLU(),  // Capa oculta1
                new Softmax()   // Capa de salida
            };

            var nn = new NeuralNetwork(layers, activations);

            // Dataset (entrada: características; salida: clase esperada)
            var inputs = new double[][]
            {
            new double[] { 5.1, 3.5, 1.4, 0.2 }, // Clase 0
            new double[] { 4.9, 3.0, 1.4, 0.2 }, // Clase 0
            new double[] { 6.2, 3.4, 5.4, 2.3 }, // Clase 2
            new double[] { 5.9, 3.0, 5.1, 1.8 }, // Clase 2
            new double[] { 6.0, 2.2, 4.0, 1.0 }, // Clase 1
            new double[] { 5.5, 2.3, 4.0, 1.3 }  // Clase 1
            };

            var outputs = new double[][]
            {
            new double[] { 1, 0, 0 }, // Clase 0
            new double[] { 1, 0, 0 }, // Clase 0
            new double[] { 0, 0, 1 }, // Clase 2
            new double[] { 0, 0, 1 }, // Clase 2
            new double[] { 0, 1, 0 }, // Clase 1
            new double[] { 0, 1, 0 }  // Clase 1
            };

            // Entrenar la red
            nn.Train(inputs, outputs, 1000, 0.01);

            // Probar con datos nuevos
            var testInput = new double[] { 5.7, 2.8, 4.1, 1.3 }; // Esperado: Clase 1
            var prediction = nn.Predict(testInput);

            Debug.WriteLine("Predicción:");
            Debug.WriteLine(string.Join(", ", prediction));
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

            // Cargar datos del MNIST
            string imagesPath = "C:\\Users\\jcfeb\\Downloads\\train-images.idx3-ubyte";
            string labelsPath = "C:\\Users\\jcfeb\\Downloads\\train-labels.idx1-ubyte";

            MNISTLoader loader = new MNISTLoader(imagesPath, labelsPath);

            Debug.WriteLine($"Total de imágenes: {loader.Images.Count}");
            Debug.WriteLine($"Dimensiones: {loader.ImageWidth}x{loader.ImageHeight}");

            // Preparar datos
            var inputs = loader.Images.Select(img => loader.GetNormalizedImage(loader.Images.IndexOf(img))).ToArray();
            var labels = loader.Labels.Select(label => nn.OneHotEncode(label, 10)).ToArray();

            // Entrenar la red
            Debug.WriteLine("Iniciando el entrenamiento...");
            nn.Train(inputs, labels, epochs: 5000, learningRate: 0.01);

            // Probar la red neuronal
            Debug.WriteLine("Probando con los primeros 5 ejemplos:");
            for (int i = 0; i < 5; i++)
            {
                var input = inputs[i];
                var expected = loader.Labels[i];
                var prediction = nn.Predict(input);

                int predictedLabel = Array.IndexOf(prediction, prediction.Max());
                Debug.WriteLine($"Etiqueta esperada: {expected}, Predicción: {predictedLabel}");
            }


            // Visualizar una imagen
            loader.PrintImage(0);

            // Normalizar una imagen y mostrar los primeros 10 valores
            double[] normalizedImage = loader.GetNormalizedImage(0);
            Console.WriteLine($"Imagen normalizada (primeros 10 valores): {string.Join(", ", normalizedImage.Take(10))}");


            // Evaluar con datos de prueba
            int correct = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                var prediction = nn.Predict(inputs[i]);
                if (nn.GetMaxIndex(prediction) == nn.GetMaxIndex(labels[i]))
                    correct++;
            }

            Debug.WriteLine($"Precisión en datos de prueba: {correct * 100.0 / inputs.Length}%");
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
                new ReLU()  // Capa oculta1
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
    }
}