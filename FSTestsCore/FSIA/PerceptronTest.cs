using FSIACore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSTestsCore.FSIA
{
    [TestClass]
    public class PerceptronTest
    {
        [TestMethod]
        public void TestAND()
        {
            // Datos de entrenamiento (AND lógico)
            double[][] trainingData = new double[][]
            {
            new double[] { 0, 0 },
            new double[] { 0, 1 },
            new double[] { 1, 0 },
            new double[] { 1, 1 }
            };

            int[] labels = { 0, 0, 0, 1 }; // Salidas esperadas para AND

            Perceptron perceptron = new Perceptron(2); // Perceptrón con 2 entradas

            // Entrenar por 10 épocas
            perceptron.Train(trainingData, labels, 10);

            // Mostrar pesos finales
            perceptron.PrintWeights();

            // Predicciones
            Debug.WriteLine("Pruebas para AND:");
            foreach (var data in trainingData)
            {
                Debug.WriteLine($"Entrada: {string.Join(", ", data)} => Salida: {perceptron.Predict(data)}");
            }
        }

        [TestMethod]
        public void TestOR()
        {
            // Datos de entrenamiento (OR lógico)
            double[][] trainingData = new double[][]
            {
            new double[] { 0, 0 },
            new double[] { 0, 1 },
            new double[] { 1, 0 },
            new double[] { 1, 1 }
            };

            int[] labels = { 0, 1, 1, 1 }; // Salidas esperadas para OR

            Perceptron perceptron = new Perceptron(2); // Perceptrón con 2 entradas

            // Entrenar por 10 épocas
            perceptron.Train(trainingData, labels, 10);

            // Mostrar pesos finales
            perceptron.PrintWeights();

            // Predicciones
            Debug.WriteLine("Pruebas para OR:");
            foreach (var data in trainingData)
            {
                Debug.WriteLine($"Entrada: {string.Join(", ", data)} => Salida: {perceptron.Predict(data)}");
            }
        }

        [TestMethod]
        public void TestPoints()
        {
            // Vamos a implementar un perceptrón que clasifique puntos en un plano cartesiano, dividiendo el espacio en dos regiones (una línea de separación).
            // Datos de entrenamiento: puntos en el plano (x, y)
            double[][] trainingData = new double[][]
            {
            new double[] { 1, 1 },  // Clase 1
            new double[] { 2, 1 },  // Clase 1
            new double[] { -1, -1 }, // Clase 0
            new double[] { -2, -1 }  // Clase 0
            };

            int[] labels = { 1, 1, 0, 0 }; // Salidas esperadas (clases)

            Perceptron perceptron = new Perceptron(2); // Perceptrón con 2 entradas (x, y)

            // Entrenar el perceptrón por 20 épocas
            perceptron.Train(trainingData, labels, 20);

            // Mostrar pesos finales
            perceptron.PrintWeights();

            // Predicciones para nuevos puntos
            double[][] testPoints = new double[][]
            {
            new double[] { 0, 0 },
            new double[] { 1.5, 0.5 },
            new double[] { -1.5, -0.5 }
            };

            Debug.WriteLine("Pruebas para clasificación de puntos:");
            foreach (var point in testPoints)
            {
                Debug.WriteLine($"Punto: {string.Join(", ", point)} => Clase: {perceptron.Predict(point)}");
            }
        }

        [TestMethod]
        public void TestXOR()
        {
            // Este ejercicio no funciona con un Perceptron simple, ya que solo funciona con prediciones lineales. Para que funcione hay que hacerlo con un Perceptron multicapa (NeuralNetwork).
            // Datos de entrenamiento (XOR lógico)
            double[][] trainingData = new double[][]
            {
            new double[] { 0, 0 },
            new double[] { 0, 1 },
            new double[] { 1, 0 },
            new double[] { 1, 1 }
            };

            int[] labels = { 0, 1, 1, 0 }; // Salidas esperadas para XOR

            Perceptron perceptron = new Perceptron(2); // Perceptrón con 2 entradas

            // Entrenar el perceptrón por 10 épocas
            perceptron.Train(trainingData, labels, 10);

            // Mostrar pesos finales
            perceptron.PrintWeights();

            // Predicciones
            Debug.WriteLine("Pruebas para XOR:");
            foreach (var data in trainingData)
            {
                Debug.WriteLine($"Entrada: {string.Join(", ", data)} => Salida: {perceptron.Predict(data)}");
            }
        }

        [TestMethod]
        public void TestTresEntradas()
        {
            // Datos de entrenamiento: tres características
            double[][] trainingData = new double[][]
            {
            new double[] { 0, 0, 0 }, // Clase 0
            new double[] { 0, 1, 1 }, // Clase 1
            new double[] { 1, 0, 1 }, // Clase 1
            new double[] { 1, 1, 1 }, // Clase 1
            new double[] { 0, 0, 1 }  // Clase 0
            };

            int[] labels = { 0, 1, 1, 1, 0 }; // Salidas esperadas

            Perceptron perceptron = new Perceptron(3); // Perceptrón con 3 entradas

            // Entrenar el perceptrón por 15 épocas
            perceptron.Train(trainingData, labels, 15);

            // Mostrar pesos finales
            perceptron.PrintWeights();

            // Predicciones para nuevos cases
            double[][] testData = new double[][]
            {
            new double[] { 0, 0, 0 }, // Clase 0
            new double[] { 0, 1, 1 }, // Clase 1
            new double[] { 1, 0, 1 }, // Clase 1
            new double[] { 1, 1, 1 }, // Clase 1
            new double[] { 0, 0, 1 }, // Clase 0
            new double[] { 0, 1, 0 }  // Clase ? (el sistema predice que es 1)
            };

            // Predicciones
            Debug.WriteLine("Pruebas con 3 entradas:");
            foreach (var data in testData)
            {
                Debug.WriteLine($"Entrada: {string.Join(", ", data)} => Salida: {perceptron.Predict(data)}");
            }
        }
    }
}
