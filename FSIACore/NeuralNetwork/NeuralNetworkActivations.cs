using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSIACore
{
    public class NeuralNetworkActivations
    {
        public interface IActivationFunction
        {
            double[] Activate(double[] inputs);
            double[] Derivative(double[] inputs, double[] outputs);
        }

        public class Sigmoid : IActivationFunction
        {
            public double Activate(double x) =>
                1.0 / (1.0 + Math.Exp(-x));

            public double[] Activate(double[] inputs) =>
                inputs.Select(x => 1.0 / (1.0 + Math.Exp(-x))).ToArray();

            public double[] Derivative(double[] inputs, double[] outputs) =>
                outputs.Select(o => o * (1 - o)).ToArray();
        }

        public class Tanh : IActivationFunction
        {
            public double Activate(double x) =>
                Math.Tanh(x);

            public double[] Activate(double[] inputs) =>
                inputs.Select(Math.Tanh).ToArray();

            public double[] Derivative(double[] inputs, double[] outputs) =>
                outputs.Select(o => 1 - o * o).ToArray();
        }

        public class ReLU : IActivationFunction
        {
            public double Activate(double x) =>
                Math.Max(0, x);

            public double[] Activate(double[] inputs) =>
                inputs.Select(x => Math.Max(0, x)).ToArray();

            public double[] Derivative(double[] inputs, double[] outputs) =>
                inputs.Select(x => x > 0.0 ? 1.0 : 0.0).ToArray();
        }

        public class LeakyReLU : IActivationFunction
        {
            public double Activate(double x) =>
                x > 0 ? x : 0.01 * x;

            public double[] Activate(double[] inputs) =>
                inputs.Select(x => x > 0 ? x : 0.01 * x).ToArray();

            public double[] Derivative(double[] inputs, double[] outputs) =>
                inputs.Select(x => x > 0 ? 1 : 0.01).ToArray();
        }

        public class Step : IActivationFunction
        {
            public double Activate(double x) =>
                x >= 0.5 ? 1.0 : 0.0;

            public double[] Activate(double[] inputs) =>
                inputs.Select(x => x >= 0.5 ? 1.0 : 0.0).ToArray();

            public double[] Derivative(double[] inputs, double[] outputs) =>
                new double[inputs.Length]; // Derivada de Step no es útil.
        }

        public class Softmax : IActivationFunction
        {
            public double[] Activate(double[] inputs)
            {
                double max = inputs.Max();
                double[] exp = inputs.Select(x => Math.Exp(x - max)).ToArray();
                double sum = exp.Sum();
                return exp.Select(x => x / sum).ToArray();
            }

            public double[] Derivative(double[] inputs, double[] outputs)
            {
                double[] derivatives = new double[outputs.Length];
                for (int i = 0; i < outputs.Length; i++)
                {
                    derivatives[i] = outputs[i] * (1 - outputs[i]);
                }
                return derivatives;
            }
        }
    }
}
