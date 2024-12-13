using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSLibraryCore
{
    /// <summary>
    /// Exposes static extension methods for checking the contents of a list.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Compares the <paramref name="value" /> object with the
        /// <paramref name="testObjects" /> provided, to see if any of the
        /// <paramref name="testObjects" /> is a match.
        /// </summary>
        /// <typeparam name="T"> Type of the object to be tested. </typeparam>
        /// <param name="value"> Source object to check. </param>
        /// <param name="testObjects">
        /// Object or objects that should be compared to value
        /// with the <see cref="M:System.Object.Equals" /> method.
        /// </param>
        /// <returns>
        /// True if any of the <paramref name="testObjects" /> equals the value;
        /// false otherwise.
        /// </returns>
        public static bool IsAnyOf<T>(this T value, params T[] testObjects)
            => testObjects.Contains(value);
    }

    /// <summary>
    /// Permite rellanar una array con un valor dado.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Propiedad Fill.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="value"></param>
        public static void Fill<T>(this T[] array, T value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }
        }

        /// <summary>
        /// Convierte cada elemento de un array utilizando una función de conversión.
        /// </summary>
        /// <typeparam name="TInput">El tipo del elemento de entrada.</typeparam>
        /// <typeparam name="TOutput">El tipo del elemento de salida.</typeparam>
        /// <param name="array">El array de entrada.</param>
        /// <param name="converter">La función de conversión.</param>
        /// <returns>Un nuevo array con los elementos convertidos.</returns>
        public static TOutput[] ConvertAll<TInput, TOutput>(this TInput[] array, Func<TInput, TOutput> converter)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (converter == null) throw new ArgumentNullException(nameof(converter));

            TOutput[] output = new TOutput[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                output[i] = converter(array[i]);
            }

            return output;
        }
    }
}
