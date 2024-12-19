using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSIACore.ConvolutionalNetwork
{
    public interface ILayer
    {
        /// <summary>
        /// Realiza la propagación hacia adelante para la capa.
        /// </summary>
        /// <param name="input">Entrada a la capa.</param>
        /// <returns>Salida de la capa.</returns>
        double[] Forward(double[] input);

        /// <summary>
        /// Realiza la propagación hacia adelante para datos de entrada 3D.
        /// </summary>
        /// <param name="input">Entrada a la capa en formato 3D.</param>
        /// <returns>Salida de la capa como un vector plano.</returns>
        double[,,] Forward(double[,,] input);

        /// <summary>
        /// Realiza la propagación hacia atrás para calcular los gradientes.
        /// </summary>
        /// <param name="outputGradient">Gradiente de salida desde la capa superior.</param>
        /// <returns>Gradiente de entrada hacia la capa inferior.</returns>
        double[] Backward(double[] outputGradient);

        /// <summary>
        /// Realiza la propagación hacia atrás para calcular los gradientes.
        /// </summary>
        /// <param name="outputGradient">Gradiente de salida desde la capa superior en formato 3D.</param>
        /// <returns>Gradiente de entrada hacia la capa inferior en formato 3D.</returns>
        double[,,] Backward(double[,,] outputGradient);

        /// <summary>
        /// Actualiza los pesos de la capa utilizando un valor de tasa de aprendizaje.
        /// </summary>
        /// <param name="learningRate">Tasa de aprendizaje para ajustar los pesos.</param>
        void UpdateWeights(double learningRate);

        /// <summary>
        /// Inicializa la capa con las dimensiones de entrada especificadas.
        /// </summary>
        /// <param name="inputDepth">Profundidad de entrada.</param>
        /// <param name="inputHeight">Altura de entrada.</param>
        /// <param name="inputWidth">Ancho de entrada.</param>
        void Initialize(int inputDepth, int inputHeight, int inputWidth);

        /// <summary>
        /// Obtiene la forma de salida de la capa dado un tamaño de entrada.
        /// </summary>
        /// <param name="inputDepth">Profundidad de entrada.</param>
        /// <param name="inputHeight">Altura de entrada.</param>
        /// <param name="inputWidth">Ancho de entrada.</param>
        /// <returns>Dimensiones de la salida.</returns>
        int[] GetOutputShape(int inputDepth, int inputHeight, int inputWidth);
    }

}
