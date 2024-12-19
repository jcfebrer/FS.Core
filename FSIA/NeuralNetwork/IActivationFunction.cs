namespace FSIA
{
    public interface IActivationFunction
    {
        double Activate(double input); // Activa un solo valor de entrada
        double[] Activate(double[] inputs); // Activa un arreglo de entradas
        double Derivative(double input); // Derivada de un solo valor de entrada
        double[] Derivative(double[] inputs, double[] outputs); // Derivada de un arreglo de entradas
    }
}