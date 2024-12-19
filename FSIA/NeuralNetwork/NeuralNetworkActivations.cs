using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSIA
{
    // Interfaz para funciones de activación
    public class NeuralNetworkActivations
    {
        // Función Sigmoid: f(x) = 1 / (1 + exp(-x))
        public class Sigmoid : IActivationFunction
        {
            public double Activate(double x) =>
                1.0 / (1.0 + Math.Exp(-x)); // Calcula la activación Sigmoid de un valor

            public double[] Activate(double[] inputs) =>
                inputs.Select(x => 1.0 / (1.0 + Math.Exp(-x))).ToArray(); // Aplica Sigmoid a un arreglo de entradas

            public double Derivative(double input)
            {
                double output = Activate(input); // Calcula la salida de la activación
                return output * (1 - output); // Derivada de la función Sigmoid
            }

            public double[] Derivative(double[] inputs, double[] outputs) =>
                outputs.Select(o => o * (1 - o)).ToArray(); // Derivada para un arreglo de salidas
        }

        // Función Tanh: f(x) = tanh(x)
        public class Tanh : IActivationFunction
        {
            public double Activate(double x) =>
                Math.Tanh(x); // Calcula la activación Tanh de un valor

            public double[] Activate(double[] inputs) =>
                inputs.Select(Math.Tanh).ToArray(); // Aplica Tanh a un arreglo de entradas

            public double Derivative(double input)
            {
                double output = Activate(input); // Calcula la salida de la activación
                return 1 - output * output; // Derivada de la función Tanh
            }

            public double[] Derivative(double[] inputs, double[] outputs) =>
                outputs.Select(o => 1 - o * o).ToArray(); // Derivada para un arreglo de salidas
        }

        // Función ReLU: f(x) = max(0, x)
        public class ReLU : IActivationFunction
        {
            public double Activate(double x) =>
                Math.Max(0, x); // ReLU devuelve el valor de entrada o 0 si es negativo

            public double[] Activate(double[] inputs) =>
                inputs.Select(x => Math.Max(0, x)).ToArray(); // Aplica ReLU a un arreglo de entradas

            public double Derivative(double input) =>
                input > 0 ? 1 : 0; // Derivada de ReLU es 1 si x > 0, y 0 si no

            public double[] Derivative(double[] inputs, double[] outputs) =>
                inputs.Select(x => x > 0.0 ? 1.0 : 0.0).ToArray(); // Derivada de ReLU aplicada a un arreglo
        }

        // Función LeakyReLU: f(x) = x si x > 0, alpha * x si no
        public class LeakyReLU : IActivationFunction
        {
            private readonly double alpha; // Parámetro para controlar la pendiente cuando x < 0

            public LeakyReLU(double alpha = 0.01)
            {
                this.alpha = alpha; // Valor por defecto de alpha es 0.01
            }

            public double Activate(double x) =>
                x > 0 ? x : alpha * x; // LeakyReLU con pendiente en negativo

            public double[] Activate(double[] inputs) =>
                inputs.Select(x => x > 0 ? x : alpha * x).ToArray(); // Aplica LeakyReLU a un arreglo de entradas

            public double Derivative(double input) =>
                input > 0 ? 1 : alpha; // Derivada de LeakyReLU, 1 si x > 0, alpha si no

            public double[] Derivative(double[] inputs, double[] outputs) =>
                inputs.Select(x => x > 0 ? 1 : alpha).ToArray(); // Derivada aplicada a un arreglo de entradas
        }

        // Función Step: f(x) = 1 si x > 0.5, 0 si no
        public class Step : IActivationFunction
        {
            public double Activate(double x) =>
                x > 0.5 ? 1.0 : 0.0; // Función escalón, 1 si x > 0.5

            public double[] Activate(double[] inputs) =>
                inputs.Select(x => x > 0.5 ? 1.0 : 0.0).ToArray(); // Aplica la función Step a un arreglo de entradas

            public double Derivative(double input) => 0; // La derivada de Step es 0 en todo el dominio

            public double[] Derivative(double[] inputs, double[] outputs) =>
                new double[inputs.Length]; // La derivada de Step es 0 para todos los valores de la entrada
        }

        // Función Softmax: Aplica la activación Softmax a un arreglo de entradas
        public class Softmax : IActivationFunction
        {
            // No tiene sentido calcular Softmax de un único valor, por eso lanzamos una excepción
            public double Activate(double input)
            {
                throw new NotSupportedException("Softmax no se puede calcular en un único valor. Use el método Activate(double[] inputs).");
            }

            // Calcula Softmax para un arreglo de entradas
            public double[] Activate(double[] inputs)
            {
                double max = inputs.Max(); // Prevenir overflow exponencial
                double[] exp = inputs.Select(x => Math.Exp(x - max)).ToArray(); // Calcula la exponencial de cada entrada
                double sum = exp.Sum(); // Suma de todas las exponenciales
                return exp.Select(x => x / sum).ToArray(); // Normaliza las exponenciales para obtener la salida Softmax
            }

            // No se puede derivar Softmax para un único valor
            public double Derivative(double input) =>
                throw new NotSupportedException("Softmax no se puede derivar escalarmente.");

            // Derivada de Softmax para un arreglo de salidas
            public double[] Derivative(double[] inputs, double[] outputs)
            {
                double[] derivatives = new double[outputs.Length];
                // La derivada de Softmax está relacionada con las salidas mismas
                for (int i = 0; i < outputs.Length; i++)
                {
                    derivatives[i] = outputs[i] * (1 - outputs[i]); // Derivada de cada componente de Softmax
                }
                return derivatives;
            }
        }
    }
}