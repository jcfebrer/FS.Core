using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;

namespace FSIA.GenerativeNetwork
{
    public class AutoregressiveModel
    {
        private Matrix<double> weights;
        private Vector<double> bias;
        private Func<double, double> activationFunction;

        public AutoregressiveModel(int inputSize, Func<double, double> activationFunction)
        {
            // Inicializar pesos y sesgos aleatorios
            weights = Matrix<double>.Build.Random(inputSize, inputSize, new Random().Next(42));
            bias = Vector<double>.Build.Random(inputSize, new Random().Next(42));
            this.activationFunction = activationFunction;
        }

        public Vector<double> PredictNext(Vector<double> input)
        {
            // Calcular la siguiente predicción: (pesos * entrada) + sesgo
            var output = weights * input + bias;
            return output.Map(activationFunction);
        }

        public List<Vector<double>> GenerateSequence(Vector<double> initialInput, int length)
        {
            var sequence = new List<Vector<double>> { initialInput };

            Vector<double> currentInput = initialInput;
            for (int i = 1; i < length; i++)
            {
                var nextOutput = PredictNext(currentInput);
                sequence.Add(nextOutput);
                currentInput = nextOutput;
            }

            return sequence;
        }
    }
}