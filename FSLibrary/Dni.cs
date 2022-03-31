using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSLibrary
{
    /// <summary>
    /// Clase para el tratamiento del dni
    /// </summary>
    public class Dni
    {
        /// <summary>
        /// Devuelve true si es valido el DNI
        /// </summary>
        public static bool Check(string dni)
        {
            //Comprobamos si el DNI tiene 9 digitos
            if (dni.Length != 9)
                return false;

            //Extraemos los números y la letra
            string dniNumbers = dni.Substring(0, dni.Length - 1);
            string dniLetter = dni.Substring(dni.Length - 1, 1);
            dniLetter = dniLetter.ToUpper();

            //Intentamos convertir los números del DNI a integer
            var numbersValid = int.TryParse(dniNumbers, out int dniInteger);
            if (!numbersValid)
                return false;

            if (CalculateDNILetter(dniInteger) != dniLetter)
                return false;

            return true;
        }

        /// <summary>
        /// Calcula la letra del dni
        /// </summary>
        /// <param name="dni"></param>
        /// <returns></returns>
        public static string CalculateDNILetter(string dni)
        {
            //Intentamos convertir los números del DNI a integer
            var numbersValid = int.TryParse(dni, out int dniInteger);
            if (!numbersValid)
                return "Invalid";

            return CalculateDNILetter(dniInteger);  
        }

        /// <summary>
        /// Calcula la letra del dni
        /// </summary>
        /// <param name="dniNumbers"></param>
        /// <returns></returns>
        public static string CalculateDNILetter(int dniNumbers)
        {
            //Cargamos los digitos de control
            string[] control = { "T", "R", "W", "A", "G", "M", "Y", "F", "P", "D", "X", "B", "N", "J", "Z", "S", "Q", "V", "H", "L", "C", "K", "E" };
            var mod = dniNumbers % 23;
            return control[mod];
        }
    }
}
