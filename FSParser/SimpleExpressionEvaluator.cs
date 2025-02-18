using FSLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace FSParser
{
    public class SimpleExpressionEvaluator
    {
        // Evalúa una expresión matemática o lógica, reemplazando las variables con sus valores.
        public static object Evaluate(string expression, Dictionary<string, object> localVariables = null, string textMark = null)
        {
            var tokens = Tokenize(expression, localVariables, textMark);

            // Si hay solo un token, significa que es una constante o variable, retornamos directamente.
            if (tokens.Count == 1)
                return expression;

            // Convierte la expresión infija a notación polaca inversa (RPN).
            var rpn = InfixToRPN(tokens);

            // Si la conversión a RPN es vacía, retornamos la expresión original.
            if (rpn.Count == 0)
                return expression;
            else
                // Evalúa la expresión en RPN.
                return EvaluateRPN(rpn);
        }

        // Tokeniza la expresión en componentes: números, operadores y variables.
        private static List<string> Tokenize(string expression, Dictionary<string, object> localVariables, string textMark)
        {
            var tokens = new List<string>();
            var currentToken = "";
            var i = 0;

            while (i < expression.Length)
            {
                char c = expression[i];

                // Si es un número o parte de una cadena
                if (char.IsDigit(c) || c == '.' || c == ',' || (c == '-' && i + 1 < expression.Length && char.IsDigit(expression[i + 1])))
                {
                    currentToken += c;
                    i++;
                    while (i < expression.Length && (char.IsDigit(expression[i]) || expression[i] == '.' || expression[i] == ','))
                    {
                        currentToken += expression[i];
                        i++;
                    }
                    tokens.Add(currentToken);
                    currentToken = "";
                }
                // Si es una letra (parte de un identificador o nombre de función)
                else if (char.IsLetter(c) || c == '_')
                {
                    currentToken += c;
                    i++;
                    while (i < expression.Length && (char.IsLetterOrDigit(expression[i]) || expression[i] == '_'))
                    {
                        currentToken += expression[i];
                        i++;
                    }

                    currentToken = TextUtil.ApplyVariables(currentToken, localVariables, textMark);

                    tokens.Add(currentToken);
                    currentToken = "";
                }
                // Si se encuentra un operador de dos caracteres (==, !=, <=, >=).
                else if (i + 1 < expression.Length && (c == '=' || c == '!' || c == '<' || c == '>'))
                {
                    string doubleCharOperator = expression.Substring(i, 2);
                    if (doubleCharOperator == "==" || doubleCharOperator == "!=" || doubleCharOperator == "<=" || doubleCharOperator == ">=")
                    {
                        tokens.Add(doubleCharOperator);
                        i += 2; // Avanzamos dos caracteres
                    }
                    else
                    {
                        tokens.Add(expression[i].ToString());
                        i++;
                    }
                }
                // Si es un operador de un solo carácter (como !, +, -, *, etc.).
                else if ("!+-*/%^()".Contains(c))
                {
                    tokens.Add(c.ToString());
                    i++;
                }
                // Si es una cadena de texto.
                else if (c == '"')
                {
                    currentToken += c;
                    i++;
                    while (i < expression.Length && expression[i] != '"')
                    {
                        currentToken += expression[i];
                        i++;
                    }
                    currentToken += '"';
                    tokens.Add(currentToken);
                    currentToken = "";
                    i++;  // Avanza después de la comilla final
                }
                // Si es un espacio o carácter no significativo, lo ignoramos.
                else if (char.IsWhiteSpace(c))
                {
                    i++;
                }
                else
                {
                    throw new Exception($"Carácter no reconocido: {c}");
                }
            }

            return tokens;
        }

        // Convierte la expresión infija a notación polaca inversa (RPN).
        private static List<string> InfixToRPN(List<string> tokens)
        {
            var output = new List<string>();
            var operators = new Stack<string>();

            foreach (var token in tokens)
            {
                if (double.TryParse(token, out _))  // Si el token es un número.
                {
                    output.Add(token);
                }
                else if (token.StartsWith("\"") && token.EndsWith("\""))  // Si es una cadena.
                {
                    output.Add(token);
                }
                else if (token.ToLower() == "true" || token.ToLower() == "false")  // Si es un valor booleano.
                {
                    output.Add(token.ToLower());
                }
                else if (token == "(")  // Si es un paréntesis de apertura.
                {
                    operators.Push(token);
                }
                else if (token == ")")  // Si es un paréntesis de cierre.
                {
                    while (operators.Count > 0 && operators.Peek() != "(")
                    {
                        output.Add(operators.Pop());
                    }
                    operators.Pop(); // Eliminar el '('.
                }
                else if (token == "!")  // Si es el operador unario de negación.
                {
                    operators.Push(token); // Se maneja con mayor precedencia.
                }
                else if ("+-*/<>!==<=>=&&||".Contains(token))  // Si es un operador binario.
                {
                    while (operators.Count > 0 && GetPrecedence(operators.Peek()) >= GetPrecedence(token))
                    {
                        output.Add(operators.Pop());
                    }
                    operators.Push(token);
                }
            }

            // Agregar los operadores restantes a la salida.
            while (operators.Count > 0)
            {
                output.Add(operators.Pop());
            }

            return output;
        }

        // Evalúa la expresión en notación polaca inversa (RPN).
        private static object EvaluateRPN(List<string> rpn)
        {
            var stack = new Stack<object>();

            foreach (var token in rpn)
            {
                if (double.TryParse(token.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double number))  // Si es un número.
                {
                    stack.Push(number);
                }
                else if (token.StartsWith("\"") && token.EndsWith("\""))  // Si es una cadena.
                {
                    stack.Push(token.Trim('"'));  // Remover las comillas de las cadenas.
                }
                else if(token == "true" || token == "false")
                {
                    stack.Push(Convert.ToBoolean(token));
                }
                else if (token == "!")  // Si es el operador unario de negación.
                {
                    if (stack.Count < 1)
                        throw new InvalidOperationException("Operación unaria '!' requiere un operando.");

                    var operand = stack.Pop();

                    if (operand is bool)
                    {
                        stack.Push(!(bool)operand);  // Negación lógica para booleanos.
                    }
                    else
                    {
                        throw new InvalidOperationException("Operador '!' solo se aplica a valores booleanos.");
                    }
                }
                else
                {
                    // Operadores binarios.
                    if (stack.Count < 2)
                        throw new InvalidOperationException("No hay suficientes elementos en la pila para realizar la operación.");

                    var b = stack.Pop();
                    var a = stack.Pop();

                    object result = null;

                    if (token == "+")
                    {
                        if (a is double && b is double)
                            result = (double)a + (double)b;
                        else
                            result = a.ToString() + b.ToString();
                    }
                    else if (token == "-")
                    {
                        result = (double)a - (double)b;
                    }
                    else if (token == "*")
                    {
                        result = (double)a * (double)b;
                    }
                    else if (token == "/")
                    {
                        result = (double)a / (double)b;
                    }
                    else if (token == "==")
                    {
                        // Asegurarse de que ambos sean cadenas antes de comparar
                        if (a is string && b is string)
                        {
                            result = (string)a == (string)b;
                        }
                        else if (a is double && b is double)
                        {
                            result = (double)a == (double)b;
                        }
                        else
                        {
                            throw new Exception("No se puede comparar valores de diferentes tipos.");
                        }
                    }
                    else if (token == "!=")
                    {
                        result = !a.Equals(b);
                    }
                    else if (token == ">")
                    {
                        result = (double)a > (double)b;
                    }
                    else if (token == "<")
                    {
                        result = (double)a < (double)b;
                    }
                    else if (token == ">=")
                    {
                        result = (double)a >= (double)b;
                    }
                    else if (token == "<=")
                    {
                        result = (double)a <= (double)b;
                    }
                    else if (token == "&&")
                    {
                        result = (bool)a && (bool)b;
                    }
                    else if (token == "||")
                    {
                        result = (bool)a || (bool)b;
                    }

                    stack.Push(result);
                }
            }

            if (stack.Count != 1)
            {
                throw new InvalidOperationException("La pila no contiene exactamente un valor al final de la evaluación.");
            }

            return stack.Pop();
        }



        // Determina la precedencia de los operadores
        private static int GetPrecedence(string operatorToken)
        {
            if (operatorToken == "!") return 4; // Mayor precedencia.
            if (operatorToken == "+" || operatorToken == "-") return 1;
            if (operatorToken == "*" || operatorToken == "/") return 2;
            if (operatorToken == ">" || operatorToken == "<" || operatorToken == "==" || operatorToken == "!=" || operatorToken == ">=" || operatorToken == "<=") return 3;
            if (operatorToken == "&&" || operatorToken == "||") return 0;
            return -1;
        }
    }
}
