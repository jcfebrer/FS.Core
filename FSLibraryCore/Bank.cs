using System;
using System.Collections;

namespace FSLibraryCore
{
    /// <summary>
    ///     Servicios de validación de las cuentas bancarias españolas
    /// </summary>
    public static class Bank
    {
        /// <summary>
        ///     Validación de una cuenta bancaria española
        /// </summary>
        /// <param name="banco">Código del banco en formato "0000"</param>
        /// <param name="oficina">Código de la sucursal en formato "0000"</param>
        /// <param name="dc">Dígito de control en formato "00"</param>
        /// <param name="cuenta">Número de cuenta en formato "0000000000"</param>
        /// <returns>true si el número de cuenta es correcto</returns>
        public static bool ValidaCuentaBancaria(string banco, string oficina, string dc, string cuenta)
        {
            // Se comprueba que realmente sean números los datos pasados como parámetros y que las longitudes
            // sean correctas
            if (!NumberUtils.IsNumeric(banco) || banco.Length != 4)
                throw new ArgumentException("El banco no tiene un formato adecuado");

            if (!NumberUtils.IsNumeric(oficina) || oficina.Length != 4)
                throw new ArgumentException("La oficina no tiene un formato adecuado");

            if (!NumberUtils.IsNumeric(dc) || dc.Length != 2)
                throw new ArgumentException("El dígito de control no tiene un formato adecuado");

            if (!NumberUtils.IsNumeric(cuenta) || cuenta.Length != 10)
                throw new ArgumentException("El número de cuenta no tiene un formato adecuado");

            return CompruebaCuenta(banco, oficina, dc, cuenta);
        }

        /// <summary>
        ///     Validación de una cuenta bancaria española
        /// </summary>
        /// <param name="cuentaCompleta">Número de cuenta completa con carácteres numéricos y 20 dígitos</param>
        /// <returns>true si el número de cuenta es correcto</returns>
        public static bool ValidaCuentaBancaria(string cuentaCompleta)
        {
            // Comprobaciones de la cadena
            if (cuentaCompleta.Length != 20)
                throw new ArgumentException("El número de cuenta no el formato adecuado");

            var banco = cuentaCompleta.Substring(0, 4);
            var oficina = cuentaCompleta.Substring(4, 4);
            var dc = cuentaCompleta.Substring(8, 2);
            var cuenta = cuentaCompleta.Substring(10, 10);

            return ValidaCuentaBancaria(banco, oficina, dc, cuenta);
        }

        /// <summary>
        ///     Validación de una cuenta bancaria española
        /// </summary>
        /// <param name="banco">Código del banco</param>
        /// <param name="oficina">Código de la oficina</param>
        /// <param name="dc">Dígito de control</param>
        /// <param name="cuenta">Número de cuenta</param>
        /// <returns>true si el número de cuenta es correcto</returns>
        public static bool ValidaCuentaBancaria(int banco, int oficina, int dc, int cuenta)
        {
            return ValidaCuentaBancaria(
                banco.ToString("0000")
                , oficina.ToString("0000")
                , dc.ToString("00")
                , cuenta.ToString("0000000000")
            );
        }


        /// <summary>
        ///     Una cuenta bancaria está validada si los dígitos de control calculados coinciden con los
        ///     que se han pasado en los argumentos
        /// </summary>
        private static bool CompruebaCuenta(string banco, string oficina, string dc, string cuenta)
        {
            return GetDigitoControl("00" + banco + oficina) + GetDigitoControl(cuenta) == dc;
        }

        /// <summary>
        ///     Obtiene el dígito de control de una cuenta bancaria. La función sólo devuelve un número
        ///     que corresponderá a una de las dos opciones.
        ///     - Codigo + CódigoOficina
        ///     - CuentaBancaria
        /// </summary>
        private static string GetDigitoControl(string CadenaNumerica)
        {
            int[] pesos = {1, 2, 4, 8, 5, 10, 9, 7, 3, 6};
            uint suma = 0;
            uint resto;

            for (var i = 0; i < pesos.Length; i++) suma += (uint) pesos[i] * uint.Parse(CadenaNumerica.Substring(i, 1));
            resto = 11 - suma % 11;

            if (resto == 10)
                resto = 1;
            if (resto == 11)
                resto = 0;

            return resto.ToString("0");
        }

        private static string CalcularIban(string ccc)
        {
            return CalcularIban(ccc, "ES");
        }

        private static string CalcularIban(string ccc, string pais)
        {
            // Calculamos el IBAN
            ccc = ccc.Trim();
            if (ccc.Length != 20) return "La CCC debe tener 20 dígitos";
            // Le añadimos el codigo del pais al ccc
            ccc = ccc + countryCode(pais); //ES = "142800";

            // Troceamos el ccc en partes (26 digitos)
            var partesCCC = new string[5];
            partesCCC[0] = ccc.Substring(0, 5);
            partesCCC[1] = ccc.Substring(5, 5);
            partesCCC[2] = ccc.Substring(10, 5);
            partesCCC[3] = ccc.Substring(15, 5);
            partesCCC[4] = ccc.Substring(20, 6);

            var iResultado = int.Parse(partesCCC[0]) % 97;
            var resultado = iResultado.ToString();
            for (var i = 0; i < partesCCC.Length - 1; i++)
            {
                iResultado = int.Parse(resultado + partesCCC[i + 1]) % 97;
                resultado = iResultado.ToString();
            }

            // Le restamos el resultado a 98
            var iRestoIban = 98 - int.Parse(resultado);
            var restoIban = iRestoIban.ToString();
            if (restoIban.Length == 1)
                restoIban = "0" + restoIban;

            return pais + restoIban + ccc;
        }


        private static string countryCode(string country)
        {
            var ascii = TextUtil.Ascii(country.ToUpper());

//				A = 10  F = 15	K = 20	P = 25	U = 30	Z = 35
//				B = 11	G = 16	L = 21	Q = 26	V = 31
//				C = 12	H = 17	M = 22	R = 27	W = 32
//				D = 13	I = 18	N = 23	S = 28	X = 33
//				E = 14	J = 19	O = 24	T = 29	Y = 34

            return ascii[0] - 65 + 10 + (ascii[1] - 65 + 10).ToString() + "00";
        }

        /// <summary>
        /// Validates the card number.
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        /// <returns></returns>
        public static bool ValidateCardNumber(string cardNumber)
        {
            try
            {
                cardNumber = TextUtil.Replace(cardNumber, " ", "");
                cardNumber = TextUtil.Replace(cardNumber, "-", "");
                cardNumber = TextUtil.Replace(cardNumber, ".", "");

                var CheckNumbers = new ArrayList();
                var CardLength = cardNumber.Length;

                var i = 0;
                for (i = CardLength - 2; i >= Convert.ToInt32(i >= 0); i += -2)
                    CheckNumbers.Add(int.Parse(cardNumber.Substring(i, 1)) * 2);

                var CheckSum = 0;

                var iCount = 0;
                for (iCount = 0; iCount <= CheckNumbers.Count - 1; iCount++)
                {
                    var _count = 0;

                    if (Convert.ToDouble(CheckNumbers[iCount]) > 9)
                    {
                        var _numLength = CheckNumbers[iCount].ToString().Length;
                        var x = 0;
                        for (x = 0; x <= _numLength - 1; x++)
                            _count = _count + int.Parse(CheckNumbers[iCount].ToString().Substring(x, 1));
                    }
                    else
                    {
                        _count = Convert.ToInt32(CheckNumbers[iCount]);
                    }

                    CheckSum = CheckSum + _count;
                }

                var OriginalSum = 0;
                var y = 0;
                for (y = CardLength - 1; y >= 0; y += -2)
                    OriginalSum = OriginalSum + int.Parse(cardNumber.Substring(y, 1));

                return (OriginalSum + CheckSum) % 10 == 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }
    }
}