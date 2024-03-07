// // <fileheader>
// // <copyright file="Math.cs" company="Febrer Software">
// //     Fecha: 03/07/2010
// //     Project: FSLibrary
// //     Solution: FSLibraryNET2008
// //     Copyright (c) 2010 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System;
using System.Collections;
using System.Text.RegularExpressions;
using FSExceptionCore;

#endregion

namespace FSLibraryCore
{
    /// <summary>
    ///     Clase para la realización de cálculos matemáticos
    /// </summary>
    public class MathUtil
    {
        /// <summary>
        ///     Modos de cálculo
        /// </summary>
        public enum CalcMode
        {
            /// <summary>
            /// The RAD
            /// </summary>
            RAD,
            /// <summary>
            /// The deg
            /// </summary>
            DEG,
            /// <summary>
            /// The grad
            /// </summary>
            GRAD
        }
        private readonly ArrayList functionList =
            new ArrayList(new[]
            {
                "abs", "acos", "asin", "atan", "ceil", "cos", "cosh", "exp", "floor", "ln", "log", "sign", "sin",
                "sinh", "sqrt", "tan", "tanh"
            });

        private double factor;
        private CalcMode mode;

        /// <summary>
        ///     Constructor
        /// </summary>
        public MathUtil()
        {
            Mode = CalcMode.RAD;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MathUtil"/> class.
        /// </summary>
        /// <param name="mode">Modo de calculo.</param>
        public MathUtil(CalcMode mode)
        {
            Mode = mode;
        }

        /// <summary>
        /// Resultado
        /// </summary>
        public double Result { get; private set; }

        /// <summary>
        ///     Modo de cálculo
        /// </summary>
        public CalcMode Mode
        {
            get { return mode; }
            set
            {
                mode = value;
                switch (value)
                {
                    case CalcMode.RAD:
                        factor = 1.0;
                        break;
                    case CalcMode.DEG:
                        factor = 2.0 * Math.PI / 360.0;
                        break;
                    case CalcMode.GRAD:
                        factor = 2.0 * Math.PI / 400.0;
                        break;
                }
            }
        }


        /// <summary>
        /// Evalua la expresión especificada
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public string Eval(string valor)
        {
            if (Evaluate(valor))
                valor = Result.ToString();

            if (!NumberUtils.IsNumeric(valor))
                valor = "'" + valor + "'";

            return valor;
        }


        /// <summary>
        /// Evaluates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        private bool Evaluate(string expression)
        {
            try
            {
                // ****************************************************************************************
                // ** MathParser in action:                                                              **
                // ** Expression = "-(5 - 10)^(-1)  ( 3 + 2(    cos( 3 Pi )+( 2+ ln( exp(1) ) )    ^3))" **
                // ****************************************************************************************
                //
                //
                // ----------
                // - Step 1 -
                // ----------
                // Remove blank.
                //
                // -(5 - 10)^(-1)  ( 3 + 2(    cos( 3 Pi )+( 2+ ln( exp(1) ) )    ^3)) -> -(5-10)^(-1)(3+2(cos(3Pi)+(2+ln(exp(1)))^3))
                //
                expression = expression.Replace(" ", "");
                //
                // ----------
                // - Step 2 -
                // ----------
                // Insert '*' if necessary.
                //
                //                                                             _    _      _
                // -(5-10)^(-1)(3+2(cos(3Pi)+(2+ln(exp(1)))^3)) -> -(5-10)^(-1)*(3+2*(cos(3*Pi)+(2+ln(exp(1)))^3))
                //             |   |     |
                //
                var regEx =
                    new Regex(
                        @"(?<=[\d\)])(?=[a-df-z\(])|(?<=pi)(?=[^\+\-\*\/\\^!)])|(?<=\))(?=\d)|(?<=[^\/\*\+\-])(?=exp)",
                        RegexOptions.IgnoreCase);
                expression = regEx.Replace(expression, "*");
                //
                // ----------
                // - Step 3 -
                // ----------
                // Replace constants: Pi -> 3,14...
                //
                //                                                                        ____
                // -(5-10)^(-1)*(3+2*(cos(3*Pi)+(2+ln(e))^3)) -> -(5-10)^(-1)*(3+2*(cos(3*3,14)+(2+ln(exp(1)))^3))
                //                          --
                //
                regEx = new Regex("pi", RegexOptions.IgnoreCase);
                expression = regEx.Replace(expression, Math.PI.ToString());
                //
                // ----------
                // - Step 4 -
                // ----------
                // Search for parentheses an solve the expression between it.
                //
                //                                                       _____
                // -(5-10)^(-1)*(3+2*(cos(3*3,14)+(2+ln(exp(1)))^3)) -> -{-5}^(-1)*(3+2*(cos(3*3,14)+(2+ln(exp(1)))^3))
                //  |_____|
                //                                                          __
                // -{-5}^(-1)*(3+2*(cos(3*3,14)+(2+ln(exp(1)))^3)) -> -{-5}^-1*(3+2*(cos(3*3,14)+(2+ln(exp(1)))^3))
                //       |__|
                //                                                                    ____
                // -{-5}^-1*(3+2*(cos(3*3,14)+(2+ln(exp(1)))^3)) -> -{-5}^-1*(3+2*(cos9,42+(2+ln(exp(1)))^3))
                //                   |______|
                //                                                                              _
                // -{-5}^-1*(3+2*(cos9,72+(2+ln(exp(1)))^3)) -> -{-5}^-1*(3+2*(cos9,72+(2+ln(exp1))^3))
                //                                 |_|
                //                                                                        ____
                // -{-5}^-1*(3+2*(cos9,72+(2+ln(exp1))^3)) -> -{-5}^-1*(3+2*(cos9,72+(2+ln2,71)^3))
                //                             |____|
                //                                                                 ____
                // -{-5}^-1*(3+2*(cos9,72+(2+ln2,71)^3)) -> -{-5}^-1*(3+2*(cos9,72+{3}^3))
                //                        |_________|
                //                                                 __
                // -{-5}^-1*(3+2*(cos9,72+{3}^3)) -> -{-5}^-1*(3+2*26)
                //               |_____________|
                //                               __
                // -{-5}^-1*(3+2*26) -> -{-5}^-1*55
                //          |______|
                //
                regEx = new Regex(@"([a-z]*)\(([^\(\)]+)\)(\^|!?)", RegexOptions.IgnoreCase);
                var m = regEx.Match(expression);
                while (m.Success)
                {
                    if (m.Groups[3].Value.Length > 0)
                        expression = expression.Replace(m.Value,
                            "{" + m.Groups[1].Value + Solve(m.Groups[2].Value) + "}" + m.Groups[3].Value);
                    else expression = expression.Replace(m.Value, m.Groups[1].Value + Solve(m.Groups[2].Value));
                    m = regEx.Match(expression);
                }

                //
                // ----------
                // - Step 5 -
                // ----------
                // There are no more parentheses. Solve the expression and convert it to double.
                //                __
                // -{-5}^-1*55 => 11
                // |_________|
                //
                Result = Convert.ToDouble(Solve(expression));
                return true;
            }
            catch
            {
                // Shit!
                return false;
            }
        }

        private string Solve(string expression)
        {
            // Solve Sin, Cos, Tan...
            var regEx = new Regex(@"([a-z]{2,})([\+-]?\d+,*\d*[eE][\+-]*\d+|[\+-]?\d+,*\d*)", RegexOptions.IgnoreCase);
            var m = regEx.Match(expression);
            while (m.Success && functionList.IndexOf(m.Groups[1].Value.ToLower()) > -1)
                switch (m.Groups[1].Value.ToLower())
                {
                    case "abs":
                        expression = expression.Replace(m.Groups[0].Value,
                            Math.Abs(Convert.ToDouble(m.Groups[2].Value)).ToString());
                        m = regEx.Match(expression);
                        continue;
                    case "acos":
                        expression = expression.Replace(m.Groups[0].Value,
                            Math.Acos(factor * Convert.ToDouble(m.Groups[2].Value)).ToString());
                        m = regEx.Match(expression);
                        continue;
                    case "asin":
                        expression = expression.Replace(m.Groups[0].Value,
                            Math.Asin(factor * Convert.ToDouble(m.Groups[2].Value)).ToString());
                        m = regEx.Match(expression);
                        continue;
                    case "atan":
                        expression = expression.Replace(m.Groups[0].Value,
                            Math.Atan(factor * Convert.ToDouble(m.Groups[2].Value)).ToString());
                        m = regEx.Match(expression);
                        continue;
                    case "cos":
                        expression = expression.Replace(m.Groups[0].Value,
                            Math.Cos(factor * Convert.ToDouble(m.Groups[2].Value)).ToString());
                        m = regEx.Match(expression);
                        continue;
                    case "ceil":
                        expression = expression.Replace(m.Groups[0].Value,
                            Math.Ceiling(Convert.ToDouble(m.Groups[2].Value)).ToString());
                        m = regEx.Match(expression);
                        continue;
                    case "cosh":
                        expression = expression.Replace(m.Groups[0].Value,
                            Math.Cosh(factor * Convert.ToDouble(m.Groups[2].Value)).ToString());
                        m = regEx.Match(expression);
                        continue;
                    case "exp":
                        expression = expression.Replace(m.Groups[0].Value,
                            Math.Exp(Convert.ToDouble(m.Groups[2].Value)).ToString());
                        m = regEx.Match(expression);
                        continue;
                    case "floor":
                        expression = expression.Replace(m.Groups[0].Value,
                            Math.Floor(Convert.ToDouble(m.Groups[2].Value)).ToString());
                        m = regEx.Match(expression);
                        continue;
                    case "ln":
                        expression = expression.Replace(m.Groups[0].Value,
                            Math.Log(Convert.ToDouble(m.Groups[2].Value), Math.Exp(1.0)).ToString());
                        m = regEx.Match(expression);
                        continue;
                    case "log":
                        expression = expression.Replace(m.Groups[0].Value,
                            Math.Log10(Convert.ToDouble(m.Groups[2].Value)).ToString());
                        m = regEx.Match(expression);
                        continue;
                    case "sign":
                        expression = expression.Replace(m.Groups[0].Value,
                            Math.Sign(Convert.ToDouble(m.Groups[2].Value)).ToString());
                        m = regEx.Match(expression);
                        continue;
                    case "sin":
                        expression = expression.Replace(m.Groups[0].Value,
                            Math.Sin(factor * Convert.ToDouble(m.Groups[2].Value)).ToString());
                        m = regEx.Match(expression);
                        continue;
                    case "sinh":
                        expression = expression.Replace(m.Groups[0].Value,
                            Math.Sinh(factor * Convert.ToDouble(m.Groups[2].Value)).ToString());
                        m = regEx.Match(expression);
                        continue;
                    case "sqrt":
                        expression = expression.Replace(m.Groups[0].Value,
                            Math.Sqrt(Convert.ToDouble(m.Groups[2].Value)).ToString());
                        m = regEx.Match(expression);
                        continue;
                    case "tan":
                        expression = expression.Replace(m.Groups[0].Value,
                            Math.Tan(factor * Convert.ToDouble(m.Groups[2].Value)).ToString());
                        m = regEx.Match(expression);
                        continue;
                    case "tanh":
                        expression = expression.Replace(m.Groups[0].Value,
                            Math.Tanh(factor * Convert.ToDouble(m.Groups[2].Value)).ToString());
                        m = regEx.Match(expression);
                        continue;
                }

            // Solve Factorial.
            regEx = new Regex(@"\{(.+)\}!"); // Search for patterns like {5}!
            m = regEx.Match(expression);
            while (m.Success)
            {
                var n = Convert.ToDouble(m.Groups[1].Value);
                if (n < 0 && !Equals(n, Math.Round(n)))
                    throw new ExceptionUtil(); // Value negative or not integer -> throw exception
                expression = regEx.Replace(expression, Fact(Convert.ToDouble(m.Groups[1].Value)).ToString(), 1);
                m = regEx.Match(expression);
            }

            regEx = new Regex(@"(\d+,*\d*[eE][\+-]?\d+|\d+,*\d*)!"); // Search for patterns like 5!
            m = regEx.Match(expression);
            while (m.Success)
            {
                var n = Convert.ToDouble(m.Groups[1].Value);
                if (n < 0 && !Equals(n, Math.Round(n)))
                    throw new ExceptionUtil(); // Value negative or not integer -> throw exception
                expression = regEx.Replace(expression, Fact(Convert.ToDouble(m.Groups[1].Value)).ToString(), 1);
                m = regEx.Match(expression);
            }

            // Solve power.
            regEx = new Regex(@"\{(.+)\}\^(-?\d+,*\d*[eE][\+-]?\d+|-?\d+,*\d*)"); // Search for patterns like {-5}^-1
            m = regEx.Match(expression, 0);
            while (m.Success)
            {
                expression = expression.Replace(m.Value,
                    Math.Pow(Convert.ToDouble(m.Groups[1].Value), Convert.ToDouble(m.Groups[2].Value)).ToString());
                m = regEx.Match(expression);
            }

            regEx = new Regex(@"(\d+,*\d*e[\+-]?\d+|\d+,*\d*)\^(-?\d+,*\d*[eE][\+-]?\d+|-?\d+,*\d*)");
            // Search for patterns like 5^-1
            m = regEx.Match(expression, 0);
            while (m.Success)
            {
                expression = regEx.Replace(expression,
                    Math.Pow(Convert.ToDouble(m.Groups[1].Value), Convert.ToDouble(m.Groups[2].Value)).ToString(),
                    1);
                m = regEx.Match(expression);
            }

            // Solve multiplication / division.
            regEx =
                new Regex(@"([\+-]?\d+,*\d*[eE][\+-]?\d+|[\-\+]?\d+,*\d*)([\/\*])(-?\d+,*\d*[eE][\+-]?\d+|-?\d+,*\d*)");
            m = regEx.Match(expression, 0);
            while (m.Success)
            {
                double result;
                switch (m.Groups[2].Value)
                {
                    case "*":
                        result = Convert.ToDouble(m.Groups[1].Value) * Convert.ToDouble(m.Groups[3].Value);
                        if (result < 0 || m.Index == 0)
                            expression = regEx.Replace(expression, result.ToString(), 1);
                        else expression = expression.Replace(m.Value, "+" + result);
                        m = regEx.Match(expression);
                        continue;
                    case "/":
                        result = Convert.ToDouble(m.Groups[1].Value) / Convert.ToDouble(m.Groups[3].Value);
                        if (result < 0 || m.Index == 0)
                            expression = regEx.Replace(expression, result.ToString(), 1);
                        else expression = regEx.Replace(expression, "+" + result, 1);
                        m = regEx.Match(expression);
                        continue;
                }
            }

            // Solve addition / subtraction.
            regEx = new Regex(
                @"([\+-]?\d+,*\d*[eE][\+-]?\d+|[\+-]?\d+,*\d*)([\+-])(-?\d+,*\d*[eE][\+-]?\d+|-?\d+,*\d*)");
            m = regEx.Match(expression, 0);
            while (m.Success)
            {
                double result;
                switch (m.Groups[2].Value)
                {
                    case "+":
                        result = Convert.ToDouble(m.Groups[1].Value) + Convert.ToDouble(m.Groups[3].Value);
                        if (result < 0 || m.Index == 0)
                            expression = regEx.Replace(expression, result.ToString(), 1);
                        else expression = regEx.Replace(expression, "+" + result, 1);
                        m = regEx.Match(expression);
                        continue;
                    case "-":
                        result = Convert.ToDouble(m.Groups[1].Value) - Convert.ToDouble(m.Groups[3].Value);
                        if (result < 0 || m.Index == 0)
                            expression = regEx.Replace(expression, result.ToString(), 1);
                        else expression = regEx.Replace(expression, "+" + result, 1);
                        m = regEx.Match(expression);
                        continue;
                }
            }

            if (expression.StartsWith("--")) expression = expression.Substring(2);
            return expression;
        }

        // Calculate Factorial.
        private double Fact(double n)
        {
            return Equals(n, 0.0) ? 1.0 : n * Fact(n - 1.0);
        }

        /// <summary>
        /// Rotates the left.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static int RotateLeft(int value, int count)
        {
            return (value << count) | (value >> (32 - count));
        }

        /// <summary>
        /// Rotates the right.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static int RotateRight(int value, int count)
        {
            return (value >> count) | (value << (32 - count));
        }
    }
}