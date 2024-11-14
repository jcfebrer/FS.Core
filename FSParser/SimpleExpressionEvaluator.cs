using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FSParser
{
    public class SimpleExpressionEvaluator
    {
        public static object Evaluate(string expression)
        {
            var tokens = Tokenize(expression);
            var rpn = InfixToRPN(tokens);

            if(rpn.Count == 0)
                return expression;
            else
                return EvaluateRPN(rpn);
        }

        // Tokeniza la expresión en números, cadenas y operadores
        private static List<string> Tokenize(string expression)
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
                    tokens.Add(currentToken);
                    currentToken = "";
                }
                // Manejo de operadores de dos caracteres (==, !=, <=, >=)
                else if (i + 1 < expression.Length && (expression[i] == '=' || expression[i] == '!' || expression[i] == '<' || expression[i] == '>'))
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
                // Si es un operador de un solo carácter (como +, -, *, /)
                else if ("+-*/%^()".Contains(c))
                {
                    tokens.Add(c.ToString());
                    i++;
                }
                // Si es una cadena de texto
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
                // Espacios y otros caracteres no significativos
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

        // Convierte la expresión infija a notación polaca inversa (RPN)
        private static List<string> InfixToRPN(List<string> tokens)
        {
            var output = new List<string>();
            var operators = new Stack<string>();

            foreach (var token in tokens)
            {
                if (double.TryParse(token, out _))  // Si el token es un número
                {
                    output.Add(token);
                }
                else if (token.StartsWith("\"") && token.EndsWith("\""))  // Si es una cadena
                {
                    output.Add(token);
                }
                else if (token == "(")  // Si es un paréntesis de apertura
                {
                    operators.Push(token);
                }
                else if (token == ")")  // Si es un paréntesis de cierre
                {
                    while (operators.Count > 0 && operators.Peek() != "(")
                    {
                        output.Add(operators.Pop());
                    }
                    operators.Pop();  // Eliminar el '('
                }
                else if ("+-*/<>!==<=>=&&||".Contains(token))  // Si es un operador
                {
                    while (operators.Count > 0 && GetPrecedence(operators.Peek()) >= GetPrecedence(token))
                    {
                        output.Add(operators.Pop());
                    }
                    operators.Push(token);
                }
            }

            while (operators.Count > 0)
            {
                output.Add(operators.Pop());
            }

            return output;
        }

        // Evalúa la notación polaca inversa (RPN)
        private static object EvaluateRPN(List<string> rpn)
        {
            var stack = new Stack<object>();

            foreach (var token in rpn)
            {
                if (double.TryParse(token.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double number))  // Si es un número
                {
                    stack.Push(number);
                }
                else if (token.StartsWith("\"") && token.EndsWith("\""))  // Si es una cadena
                {
                    stack.Push(token.Trim('\"'));  // Remover las comillas de las cadenas
                }
                else
                {
                    if (stack.Count < 2)  // Comprobamos si la pila tiene al menos dos elementos
                    {
                        throw new InvalidOperationException("No hay suficientes elementos en la pila para realizar la operación.");
                    }

                    var b = stack.Pop();
                    var a = stack.Pop();

                    object result = null;

                    // Evaluar operadores
                    if (token == "+")
                    {
                        if (a is double && b is double)
                        {
                            result = (double)a + (double)b;
                        }
                        else
                        {
                            result = a.ToString() + b.ToString(); // Concatenación de cadenas
                        }
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
            if (operatorToken == "+" || operatorToken == "-") return 1;
            if (operatorToken == "*" || operatorToken == "/") return 2;
            if (operatorToken == ">" || operatorToken == "<" || operatorToken == "==" || operatorToken == "!=" || operatorToken == ">=" || operatorToken == "<=") return 3;
            if (operatorToken == "&&" || operatorToken == "||") return 0;
            return -1;
        }
    }
}