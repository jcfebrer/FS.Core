using System;

namespace FSIACore.ConvolutionalNetwork
{
    public static class Activators
    {
        public static ActivationLayer ReLU()
        {
            return new ActivationLayer(
                x => Math.Max(0, x),
                x => x > 0 ? 1 : 0
            );
        }

        public static ActivationLayer LeakyReLU(double alpha = 0.01)
        {
            return new ActivationLayer(
                x => x > 0 ? x : alpha * x,
                x => x > 0 ? 1 : alpha
            );
        }

        public static ActivationLayer Sigmoid()
        {
            return new ActivationLayer(
                x => 1 / (1 + Math.Exp(-x)),
                x =>
                {
                    double sigmoid = 1 / (1 + Math.Exp(-x));
                    return sigmoid * (1 - sigmoid);
                }
            );
        }

        public static ActivationLayer Tanh()
        {
            return new ActivationLayer(
                x => Math.Tanh(x),
                x => 1 - Math.Pow(Math.Tanh(x), 2)
            );
        }
    }
}
