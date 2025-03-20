#region

using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

#endregion

namespace FSLibrary
{
    /// <summary>
    /// Funciones de uso general
    /// </summary>
    public class Functions
    {
        /// <summary>
        ///     Devuelve 'true' si el valor existe en el array de valores, en caso contrario 'false'.
        /// </summary>
        /// <param name="valores"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static bool Existe(string[] valores, string valor)
        {
            foreach (var s in valores)
                if (s.ToLower() == valor.ToLower())
                    return true;
            return false;
        }

        /// <summary>
        /// Copia un String de entrada en un string de salida.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public static void CopyTo(Stream input, Stream output)
        {
            byte[] buffer = new byte[16 * 1024]; // Fairly arbitrary size
            int bytesRead;

            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }

        /// <summary>
        /// Convierte una cadena en un valor existe en el enumerador.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns>Devuelve true si ha podido realizar la conversión.</returns>
        public static bool EnumTryParse<TEnum>(string value, out TEnum result)
            where TEnum : struct, IConvertible
        {
            var retValue = value == null ?
                        false :
                        Enum.IsDefined(typeof(TEnum), value);
            result = retValue ?
                        (TEnum)Enum.Parse(typeof(TEnum), value) :
                        default(TEnum);
            return retValue;
        }

        /// <summary>
        /// Converts the string representation of a Guid to its Guid 
        /// equivalent. A return value indicates whether the operation 
        /// succeeded. 
        /// </summary>
        /// <param name="s">A string containing a Guid to convert.</param>
        /// <param name="result">
        /// When this method returns, contains the Guid value equivalent to 
        /// the Guid contained in <paramref name="s"/>, if the conversion 
        /// succeeded, or <see cref="Guid.Empty"/> if the conversion failed. 
        /// The conversion fails if the <paramref name="s"/> parameter is a 
        /// <see langword="null" /> reference (<see langword="Nothing" /> in 
        /// Visual Basic), or is not of the correct format. 
        /// </param>
        /// <value>
        /// <see langword="true" /> if <paramref name="s"/> was converted 
        /// successfully; otherwise, <see langword="false" />.
        /// </value>
        /// <exception cref="ArgumentNullException">
        ///        Thrown if <pararef name="s"/> is <see langword="null"/>.
        /// </exception>
        public static bool GuidTryParse(string s, out Guid result)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            Regex format = new Regex(
                "^[A-Fa-f0-9]{32}$|" +
                "^({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?$|" +
                "^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2}, {0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}})$");
            Match match = format.Match(s);
            if (match.Success)
            {
                result = new Guid(s);
                return true;
            }
            else
            {
                result = Guid.Empty;
                return false;
            }
        }

        /// <summary>
        /// Convierte el valor x proporcionalmente a los valores de entrada.
        /// </summary>
        /// <param name="x">El valor a convertir.</param>
        /// <param name="in_min">The in minimum.</param>
        /// <param name="in_max">The in maximum.</param>
        /// <param name="out_min">The out minimum.</param>
        /// <param name="out_max">The out maximum.</param>
        /// <returns></returns>
        public static long Map(long x, long in_min, long in_max, long out_min, long out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }

        /// <summary>
        /// Método de ordenación Quicksort
        /// </summary>
        /// <param name="Arr">The arr.</param>
        /// <returns></returns>
        public static string[] QuickSort(string[] Arr)
        {
            return QuickSort(Arr, 0, Arr.Length);
        }

        /// <summary>
        /// Método de ordenación Quicksort
        /// </summary>
        /// <param name="Arr">The arr.</param>
        /// <param name="loBound">The lo bound.</param>
        /// <param name="hiBound">The hi bound.</param>
        /// <returns></returns>
        public static string[] QuickSort(string[] Arr, int loBound, int hiBound)
        {
            string pivot = null;
            string temp = null;
            var loSwap = 0;
            var hiSwap = 0;

            if (hiBound - loBound == 1)
                if (double.Parse(Arr[loBound]) > double.Parse(Arr[hiBound]))
                {
                    temp = Arr[loBound];
                    Arr[loBound] = Arr[hiBound];
                    Arr[hiBound] = temp;
                }

            pivot = Arr[Convert.ToInt32(Math.Floor((double)(loBound + hiBound) / 2))];
            Arr[Convert.ToInt32(Math.Floor((double)(loBound + hiBound) / 2))] = Arr[loBound];
            Arr[loBound] = pivot;
            loSwap = loBound + 1;
            hiSwap = hiBound;
            do
            {
                while ((loSwap < hiSwap) & (double.Parse(Arr[loSwap]) <= double.Parse(pivot))) loSwap = loSwap + 1;
                while (double.Parse(Arr[hiSwap]) > double.Parse(pivot)) hiSwap = hiSwap - 1;
                if (loSwap < hiSwap)
                {
                    temp = Arr[loSwap];
                    Arr[loSwap] = Arr[hiSwap];
                    Arr[hiSwap] = temp;
                }
            } while (loSwap < hiSwap);

            Arr[loBound] = Arr[hiSwap];
            Arr[hiSwap] = pivot;
            if (loBound < hiSwap - 1) QuickSort(Arr, loBound, hiSwap - 1);
            if (hiSwap + 1 < hiBound) QuickSort(Arr, hiSwap + 1, hiBound);
            return Arr;
        }

        /// <summary>
        /// Devuelve en cadena el valor.
        /// </summary>
        /// <param name="value">Valor.</param>
        /// <returns></returns>
        public static string Valor(object value)
        {
            if (value == null) return string.Empty;
            if (value is Array) return "(Byte)";
            return value.ToString();
        }

        /// <summary>
        /// Devuelve en cadena el valor.
        /// </summary>
        /// <param name="value">Valor.</param>
        /// <returns></returns>
        public static string ValorZero(object value)
        {
            if (value == null)
                return string.Empty;

            if (value is Array) return "(Byte)";

            var s = (string) value;
            var z = s.IndexOf((char) 0) + 1;
            if (z > 0) value = TextUtil.Substring(s, 0, z);

            return value.ToString();
        }


        /// <summary>
        /// Convierte el valor a booleano.
        /// </summary>
        /// <param name="value">Valor.</param>
        /// <returns></returns>
        public static bool ValorBool(object value)
        {
            if (value == null) return false;
            if (value is string)
                if (Valor(value) == "")
                    return false;
            if (value is DBNull) return false;
            if (Valor(value) == "0") return false;
            if (Valor(value) == "1") return true;
            if (Convert.ToBoolean(value)) return true;
            return bool.Parse(value.ToString());
        }


        /// <summary>
        /// Determines whether the specified value is empty.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEmpty(object value)
        {
            if (value == null) return true;
            if (string.IsNullOrEmpty(value.ToString())) return true;
            return false;
        }

        //public static string String2(int len, string car)
        //{
        //    var s = car != "" ? "car" : " ";
        //    return s.PadLeft(len);
        //}

        //public static string String2(int len)
        //{
        //    return String2(len, "");
        //}


        /// <summary>
        /// Arrays to string.
        /// </summary>
        /// <param name="arr">The arr.</param>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        public static string ArrayToString(string[] arr, string separator)
        {
            var s = "";
            var f = 0;

            for (f = 0; f <= arr.GetUpperBound(0); f++) s = s + arr[f] + separator;
            s = TextUtil.Substring(s, 0, TextUtil.Length(s) - 1);
            return s;
        }

        /// <summary>
        /// Arrays to string.
        /// </summary>
        /// <param name="arr">The arr.</param>
        /// <returns></returns>
        public static string ArrayToString(string[] arr)
        {
            return ArrayToString(arr, ",");
        }

        /// <summary>
        /// Reorganiza la cadena request.
        /// </summary>
        /// <param name="query">Consulta.</param>
        /// <returns></returns>
        public static string ReorgQuery(string query)
        {
            string ret;
            var lstParam = new List<string>();
            var arrParam = query.Split('&');
            foreach (var param in arrParam)
                if (!String.IsNullOrEmpty(param))
                    lstParam.Add(param);
            ret = string.Join("&", lstParam.ToArray());

            return ret;
        }

        /// <summary>
        /// Sleeps the specified milliseconds.
        /// </summary>
        /// <param name="milliseconds">The milliseconds.</param>
        public static void Sleep(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        

        /// <summary>
        /// Compara dos array list.
        /// </summary>
        /// <param name="arr1">The arr1.</param>
        /// <param name="arr2">The arr2.</param>
        /// <returns></returns>
        public static bool ArrayListCompare(ArrayList arr1, ArrayList arr2)
        {
            var f = 0;

            if (arr1.Count != arr2.Count) return false;

            for (f = 0; f <= arr1.Count - 1; f++)
                if (arr1[f].ToString() != arr2[f].ToString())
                    return false;

            return true;
        }

        /// <summary>
        /// Gets the environment variable.
        /// </summary>
        /// <param name="environmentVariable">The environment variable.</param>
        /// <returns></returns>
        public static string GetEnvironmentVariable(string environmentVariable)
        {
            return Environment.GetEnvironmentVariable(environmentVariable);
        }

        /// <summary>
        /// Devuelve el Id del usuario.
        /// </summary>
        /// <returns></returns>
        public static string UserId()
        {
            return GetEnvironmentVariable("COMPUTERNAME") + "#" + GetEnvironmentVariable("USERNAME");
        }

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <returns></returns>
        public static string GetUserName()
        {
            return GetEnvironmentVariable("USERNAME");
        }

        /// <summary>
        /// Gets the machine identifier.
        /// </summary>
        /// <returns></returns>
        public static string GetMachineId()
        {
            return GetEnvironmentVariable("COMPUTERNAME");
        }

        /// <summary>
        /// Gets the temporary dir.
        /// </summary>
        /// <returns></returns>
        public static string GetTempDir()
        {
            return GetEnvironmentVariable("TEMP");
        }

        /// <summary>
        /// Get the OS version.
        /// </summary>
        /// <returns></returns>
        public static string OSVersion()
        {
            return Environment.OSVersion.ToString();
        }

        /// <summary>
        /// Degreeses to radians.
        /// </summary>
        /// <param name="nDegrees">The n degrees.</param>
        /// <returns></returns>
        public static double DegreesToRadians(double nDegrees)
        {
            return nDegrees * Math.PI / 180;
        }

        /// <summary>
        /// Radianses to degrees.
        /// </summary>
        /// <param name="nRadians">The n radians.</param>
        /// <returns></returns>
        public static double RadiansToDegrees(double nRadians)
        {
            return nRadians * 180 / Math.PI;
        }

        /// <summary>
        /// Celsiuses to fahrenheit.
        /// </summary>
        /// <param name="nTemperature">The n temperature.</param>
        /// <returns></returns>
        public static double CelsiusToFahrenheit(double nTemperature)
        {
            return nTemperature * 9 / 5 + 32;
        }

        /// <summary>
        /// Fahrenheits to celsius.
        /// </summary>
        /// <param name="nTemperature">The n temperature.</param>
        /// <returns></returns>
        public static double FahrenheitToCelsius(double nTemperature)
        {
            return (nTemperature - 32) * 5 / 9;
        }


        /// <summary>
        /// Devuelve true/false si la propiedad, evento o método existe en el objeto.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="propertyEventMethodName">Name of the property event method.</param>
        /// <returns></returns>
        public static bool MemberExists(object obj, string propertyEventMethodName)
        {
            var pdc = TypeDescriptor.GetProperties(obj);


            foreach (MemberDescriptor md in pdc)
                if (md.Name.ToLower() == propertyEventMethodName.ToLower())
                    return true;

            var edc = TypeDescriptor.GetEvents(obj);

            foreach (EventDescriptor ed in edc)
                if (ed.Name.ToLower() == propertyEventMethodName.ToLower())
                    return true;

            return false;
        }

        /// <summary>
        /// Devuelve la distancia por aire entre dos coordenadas dadas en longitud, latitud.
        /// </summary>
        /// <param name="latA"></param>
        /// <param name="longA"></param>
        /// <param name="latB"></param>
        /// <param name="longB"></param>
        /// <returns></returns>
        public static decimal CalcAirDistance(double latA, double longA, double latB, double longB)
        {

            double theDistance = (Math.Sin(Functions.DegreesToRadians(latA)) *
                    Math.Sin(Functions.DegreesToRadians(latB)) +
                    Math.Cos(Functions.DegreesToRadians(latA)) *
                    Math.Cos(Functions.DegreesToRadians(latB)) *
                    Math.Cos(Functions.DegreesToRadians(longA - longB)));

            return Convert.ToDecimal((Functions.RadiansToDegrees(Math.Acos(theDistance)))) * 69.09M * 1.6093M;
        }
    }
}