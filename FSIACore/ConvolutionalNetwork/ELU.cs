using System;
using System.Linq;

namespace FSIACore.ConvolutionalNetwork
{
    public class ELU : IActivationFunction
    {
        private readonly double alpha;

        public ELU(double alpha = 1.0)
        {
            this.alpha = alpha;
        }

        public double Activate(double x) =>
            x >= 0 ? x : alpha * (Math.Exp(x) - 1);

        public double[] Activate(double[] inputs) =>
            inputs.Select(Activate).ToArray();

        public double Derivative(double input) =>
            input >= 0 ? 1 : Activate(input) + alpha;

        public double[] Derivative(double[] inputs, double[] outputs) =>
            inputs.Select(Derivative).ToArray();
    }

}
