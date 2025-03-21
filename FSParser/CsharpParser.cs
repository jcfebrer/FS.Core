#if NET40_OR_GREATER || NETCOREAPP

using FSLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace FSParser
{
    public class CSharpParser
    {
        private readonly Dictionary<string, object> variables = new Dictionary<string, object>();
        private readonly Dictionary<string, (List<string> Parameters, List<string> Body)> functions = new Dictionary<string, (List<string> Parameters, List<string> Body)>(StringComparer.InvariantCultureIgnoreCase);
        private readonly Dictionary<string, Func<List<string>, object>> customCommands = new Dictionary<string, Func<List<string>, object>>(StringComparer.InvariantCultureIgnoreCase);

        //private readonly static string textMark = "(<*>)"; // Marca que identifica un texto que no se debe de parsear.

        private readonly static string singleLineCommentPattern = @"^\s*//(.*)";
        private readonly static string blockCommentPattern = @"(?<![""'])/\*[^*]*\*+(?:[^/*][^*]*\*+)*/";
        private readonly static string assignmentPattern = @"^\s*(\w+)\s*=\s*(.+);$";
        private readonly static string ifPattern = @"^\s*if\s*\((.+)\)\s*{?$";
        private readonly static string elsePattern = @"^\s*else\s*{?$";
        private readonly static string returnPattern = @"^\s*return\s*(.*);";
        private readonly static string whilePattern = @"^\s*while\s*\((.+)\)\s*{?$";
        private readonly static string functionDefPattern = @"^\s*function\s+(\w+)\s*\((.*?)\)\s*{?$";
        private readonly static string functionCallPattern = @"(\w+\s*)\(((?:""[^""]*""|[^()""]|(?<Open>\()|(?<-Open>\)))*)\)";
        private readonly static string allowedTextInLinePattern = @"(\{|\}|/\*|\*/|else)";

        private readonly Regex singleLineCommentRegex = new Regex(singleLineCommentPattern, RegexOptions.Compiled | RegexOptions.Multiline);
        private readonly Regex blockCommentRegex = new Regex(blockCommentPattern, RegexOptions.Compiled | RegexOptions.Multiline);
        private readonly Regex assignmentRegex = new Regex(assignmentPattern, RegexOptions.Compiled | RegexOptions.Multiline);
        private readonly Regex ifRegex = new Regex(ifPattern, RegexOptions.Compiled | RegexOptions.Multiline);
        private readonly Regex elseRegex = new Regex(elsePattern, RegexOptions.Compiled | RegexOptions.Multiline);
        private readonly Regex returnRegex = new Regex(returnPattern, RegexOptions.Compiled | RegexOptions.Multiline);
        private readonly Regex whileRegex = new Regex(whilePattern, RegexOptions.Compiled | RegexOptions.Multiline);
        private readonly Regex functionDefRegex = new Regex(functionDefPattern, RegexOptions.Compiled | RegexOptions.Multiline);
        private readonly Regex functionCallRegex = new Regex(functionCallPattern, RegexOptions.Compiled | RegexOptions.Multiline);
        private readonly Regex allowedTextInLineRegex = new Regex(allowedTextInLinePattern, RegexOptions.Compiled | RegexOptions.Multiline);
        
        public Dictionary<string, object> Variables => variables;
        public Dictionary<string, Func<List<string>, object>> CustomCommands => customCommands;

        public event EventHandler OnMemoryUpdate;

        private static ManualResetEvent resetEvent = new ManualResetEvent(true);

        bool stop = false;

        public CSharpParser()
        {
        }

        public void Abort()
        {
            stop = true;
        }

        public void Clear()
        {
            //inicializamos las variables
            variables.Clear();
            stop = false;
        }

        public void ThreadWait()
        {
            resetEvent.WaitOne();
        }

        public void ThreadSet()
        {
            resetEvent.Set();
        }

        public void ThreadReset()
        {
            resetEvent.Reset();
        }

        public void Wait(int milisecs)
        {
            Thread.Sleep(milisecs);
        }

        private string RemoveComments(string code)
        {
            // Eliminar comentarios de bloque y de una sola línea
            code = blockCommentRegex.Replace(code, "");
            code = singleLineCommentRegex.Replace(code, "");
            return code;
        }

        // Nuevo método para dividir argumentos separados por comas, teniendo en cuenta paréntesis anidados y cadenas entrecomilladas.
        private List<string> SplitArguments(string arguments)
        {
            if (string.IsNullOrWhiteSpace(arguments))
                return new List<string>(); // Retorna lista vacía si no hay argumentos.

            var result = new List<string>();
            var currentArg = new StringBuilder();
            int parenthesesBalance = 0;
            bool insideString = false;

            for (int i = 0; i < arguments.Length; i++)
            {
                char c = arguments[i];

                if (c == ',' && parenthesesBalance == 0 && !insideString)
                {
                    // Añadir el argumento actual (limpio) a la lista y resetear
                    // Quitamos tambien la @ al comienzo del argumento.
                    result.Add(currentArg.ToString().Trim().TrimStart('@'));
                    currentArg.Clear();
                }
                else
                {
                    if (c == '(' && !insideString) parenthesesBalance++;
                    if (c == ')' && !insideString) parenthesesBalance--;

                    if (c == '"')
                    {
                        // Manejar comillas escapadas (ej: \" )
                        if (i > 0 && arguments[i - 1] == '\\')
                        {
                            currentArg.Length--; // Elimina el carácter de escape
                }
                        else
                        {
                            insideString = !insideString; // Cambia el estado
                        }
                    }

                    currentArg.Append(c);
                }
            }

            // Añadir el último argumento si queda algo
            if (currentArg.Length > 0)
            {
                // Quitamos tambien la @ al comienzo del argumento.
                result.Add(currentArg.ToString().Trim().TrimStart('@'));
            }

            // Verificar balanceo de paréntesis
            if (parenthesesBalance != 0)
                throw new ArgumentException("Los paréntesis no están balanceados en la cadena de argumentos.");

            return result;
        }

        ///// <summary>
        ///// Protegemos las variables para evitar que se procesen como coódigo.
        ///// </summary>
        //public string ProtectData(string data)
        //{
        //    data = data + Environment.NewLine + textMark;
        //    return data;
        //}

        ///// <summary>
        ///// Esta función quita la marca de textMark, para evitar que se trate como código.
        ///// </summary>
        //public string UnProtectData(string data)
        //{
        //    return data.Replace(Environment.NewLine + textMark, "");
        //}

        private List<string> ApplyVariablesToArguments(List<string> arguments)
        {
            var result = new List<string>();
            foreach(string s in arguments)
            {
                result.Add(TextUtil.ApplyVariables(s, variables));
            }
            return result;
        }

        private object EvaluateExpression(string expression, Dictionary<string, object> localVariables = null)
        {
            var variablesToUse = localVariables ?? variables;
            object result = expression;
            object lastResult;

            do
            {
                if (TextUtil.HasProtectText(result.ToString()))
                    break;

                //if (TextUtil.HasQuotes(result.ToString()) && !SimpleExpressionEvaluator.IsSimpleExpression(result.ToString()))
                //    break;

                ////Si la expresión contiene la marca de texto para no evaluar, salimos.
                //if (textMark != null && result.ToString().Contains(textMark))
                //    break;

                lastResult = result; // Guarda la expresión antes de la evaluación

            // Evalúa funciones recursivamente utilizando functionCallRegex.Replace
                result = functionCallRegex.Replace(result.ToString(), match =>
            {
                string functionName = match.Groups[1].Value;
                string argumentList = match.Groups[2].Value;

                // Evalúa los argumentos
                var evaluatedArgs = ApplyVariablesToArguments(SplitArguments(argumentList));

                    object _result;

                if (functions.ContainsKey(functionName))
                {
                        _result = CallFunction(functionName, evaluatedArgs);
                }
                else if (IsSystemFunction(functionName, out MethodInfo methodInfo))
                {
                        _result = CallSystemFunction(methodInfo, evaluatedArgs);
                }
                else if (customCommands.ContainsKey(functionName))
                {
                        _result = customCommands[functionName](evaluatedArgs);
                }
                else
                {
                    throw new Exception($"Función '{functionName}' no definida.");
                }

                    return _result.ToString();
            });

                result = EvaluateFinalExpression(result.ToString(), variablesToUse);

                // Sigue evaluando mientras la expresión cambie después de la evaluación
            } while (!result.Equals(lastResult));

            // Si es una cadena, eliminamos las comillas al comienzo y final de la expresión.
            //if (result is string)
            //    result = TextUtil.RemoveQuotes(result.ToString());

            return result;
        }

        private object EvaluateFinalExpression(string expression, Dictionary<string, object> variablesToUse)
        {
            try
            {
                if (SimpleExpressionEvaluator.IsSimpleExpression(expression))
                {
                    // Evalúa la expresión final si es una operación matemática simple
                    return SimpleExpressionEvaluator.Evaluate(expression, variablesToUse);
                }
                else if (NumberUtils.IsNumeric(expression))
                {
                    // Si es un número, cambia el punto por la coma.
                    return Convert.ToDouble(expression.Replace(".", ","));
                }
                else
                {
                    return expression;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo evaluar la expresión: {expression}. Error: {ex.Message}");
            }
        }

        private void AddGeneralVariables()
        {
            // Salto de linea
            if (!variables.ContainsKey("crlf"))
                variables.Add("crlf", Environment.NewLine);
        }

        // Validación de la sintaxis completa del bloque de código
        private bool IsBlockSyntaxValid(string codeBlock)
        {
            // Comprobar contra cada patrón de bloque (for, if, where)
            string[] blockPatterns = { ifPattern, elsePattern, whilePattern, blockCommentPattern };

            foreach (var pattern in blockPatterns)
            {
                if (Regex.IsMatch(codeBlock, pattern, RegexOptions.Compiled | RegexOptions.Multiline))
                    return true;  // Devuelve true si hay alguna estructura completa de bloque
            }
            return false;
        }

        // Validación de la sintaxis en cada línea de código
        private bool IsLineSyntaxValid(string line)
        {
            //Si es una línea en blanco devolvemos true.
            if (string.IsNullOrEmpty(line.Trim()))
                return true;

            // Comprobar contra cada patrón de línea (var, print, comentario)
            string[] linePatterns = { assignmentPattern, functionCallPattern, functionDefPattern, returnPattern, singleLineCommentPattern, allowedTextInLinePattern };

            foreach (var pattern in linePatterns)
            {
                if (Regex.IsMatch(line, pattern, RegexOptions.Compiled | RegexOptions.Multiline))
                    return true;  // Devuelve verdadero si la línea coincide con alguna estructura válida
            }

            return false;
        }

        public void CheckSyntax(string codeBlock)
        {
            // Separemos el código en líneas
            string[] codeLines = codeBlock.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            List<string> errors = new List<string>();
            bool insideComments = false;

            // Validar la sintaxis del bloque completo
            bool codeBlockValid = IsBlockSyntaxValid(codeBlock);

            // Validar la sintaxis de cada línea individualmente
            for (int i = 0; i < codeLines.Length; i++)
            {
                if (codeLines[i].Contains("/*"))
                    insideComments = true;
                if (codeLines[i].Contains("*/"))
                    insideComments = false;

                if (!IsLineSyntaxValid(codeLines[i]) && !insideComments)
                {
                    errors.Add($"Error de sintaxis en la línea {i + 1}: {codeLines[i]}");
                }
            }

            if (errors.Count > 0)
            {
                string errorString = string.Join(Environment.NewLine, errors.ToArray());
                throw new Exception(errorString);
            }
        }

        public void Parse(string code)
        {
            //Validamos el código
            CheckSyntax(code);

            //Añadimos las variables generales
            AddGeneralVariables();

            //Protegemos las variables para evitar que se procesen datos de la memoria como código o memoria.
            //ProtectVariables();

            code = RemoveComments(code);
            List<string> lines = new List<string>(code.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
            ParseBlock(lines, 0, lines.Count, variables);
        }

        public void Parse(List<string> lines)
        {
            string code = string.Join(Environment.NewLine, lines.ToArray());
            Parse(code);
        }

        private int ParseBlock(List<string> lines, int start, int end, Dictionary<string, object> localVariables = null)
        {
            for (int i = start; i < end; i++)
            {
                if (stop) break;

                ThreadWait();

                string line = lines[i].Trim();

                if (string.IsNullOrEmpty(line))
                    continue;

                if (line.StartsWith("\\"))
                    continue;

                //if (allowedTextInLineRegex.IsMatch(line))
                //    continue;

                var functionDefMatch = functionDefRegex.Match(line);
                if (functionDefMatch.Success)
                {
                    string functionName = functionDefMatch.Groups[1].Value;
                    string parameterList = functionDefMatch.Groups[2].Value;

                    List<string> parameters = new List<string>(parameterList.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

                    if (!line.Contains("{") && lines[i + 1].Trim() == "{") i++;

                    int closingIndex = FindClosingBracket(lines, i);

                    List<string> body = lines.GetRange(i + 1, closingIndex - i - 1);
                    functions[functionName] = (parameters, body);
                    i = closingIndex;
                    continue;
                }

                var assignmentMatch = assignmentRegex.Match(line);
                if (assignmentMatch.Success)
                {
                    HandleAssignment(assignmentMatch, localVariables);

                    if (OnMemoryUpdate != null)
                        OnMemoryUpdate(localVariables, EventArgs.Empty);
                    continue;
                }

                var elseMatch = elseRegex.Match(line);
                if (elseMatch.Success)
                {
                    continue;
                }

                var ifMatch = ifRegex.Match(line);
                if (ifMatch.Success)
                {
                    string condition = ifMatch.Groups[1].Value;
                    bool conditionResult = Convert.ToBoolean(EvaluateExpression(condition, localVariables));

                    if (!line.Contains("{") && lines[i + 1].Trim() == "{") i++;

                    int closingIndex = FindClosingBracket(lines, i);

                    if (conditionResult)
                    {
                        i = ParseBlock(lines, i + 1, closingIndex, localVariables);

                        i = SkipParseElse(lines, closingIndex, false, localVariables);
                    }
                    else
                    {
                        i = SkipParseElse(lines, closingIndex, true, localVariables);
                    }
                    continue;
                }

                var whileMatch = whileRegex.Match(line);
                if (whileMatch.Success)
                {
                    string condition = whileMatch.Groups[1].Value;

                    if (!line.Contains("{") && lines[i + 1].Trim() == "{") i++;

                    int closingIndex = FindClosingBracket(lines, i);

                    while (Convert.ToBoolean(EvaluateExpression(condition, localVariables)))
                        ParseBlock(lines, i + 1, closingIndex, localVariables);

                    i = closingIndex;
                    continue;
                }

                var customCommandMatch = functionCallRegex.Match(line);
                if (customCommandMatch.Success)
                {
                    string commandName = customCommandMatch.Groups[1].Value.ToLower();
                    string argumentList = customCommandMatch.Groups[2].Value;

                    if (customCommands.ContainsKey(commandName))
                    {
                        List<string> evaluatedArgs = ApplyVariablesToArguments(SplitArguments(argumentList))
                            .Select(arg => EvaluateExpression(arg.Trim(), localVariables).ToString())
                            .ToList();

                        customCommands[commandName](evaluatedArgs);
                        continue;
                    }
                }

                throw new Exception($"Instrucción no reconocida: {line}");
            }
            return end;
        }

        /// <summary>
        /// Saltamos la sección del else si la hay
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="closingIndex"></param>
        /// <param name="parseElse"></param>
        /// <param name="localVariables"></param>
        /// <returns></returns>
        private int SkipParseElse(List<string> lines, int closingIndex, bool parseElse, Dictionary<string,object> localVariables)
        {
            int i;
            int elseIndex = closingIndex + 1;

            if (elseIndex < lines.Count && lines[elseIndex].Trim().StartsWith("else"))
            {
                if (!lines[elseIndex].Contains("{") && lines[elseIndex + 1].Trim() == "{")
                    elseIndex++;

                int elseClosingIndex = FindClosingBracket(lines, elseIndex);

                if (parseElse)
                    i = ParseBlock(lines, elseIndex + 1, elseClosingIndex, localVariables);
                else
                    i = elseClosingIndex;
            }
            else
            {
                i = closingIndex;
            }

            return i;
        }

        private int FindClosingBracket(List<string> lines, int start)
        {
            int openBrackets = 0;
            for (int i = start; i < lines.Count; i++)
            {
                if (lines[i].Contains("{"))
                    openBrackets++;
                if (lines[i].Contains("}"))
                    openBrackets--;

                if (openBrackets == 0)
                    return i;
            }
            throw new Exception("Bloque sin cerrar.");
        }

        private void HandleAssignment(Match match, Dictionary<string, object> localVariables = null)
        {
            string variableName = match.Groups[1].Value.Trim();
            string value = match.Groups[2].Value.Trim();
            var variablesToUse = localVariables ?? variables;
            variablesToUse[variableName] = EvaluateExpression(value, variablesToUse);
        }

        private object CallFunction(string functionName, List<string> arguments)
        {
            var function = functions[functionName];
            var parameters = function.Parameters;
            var body = function.Body;

            if (parameters.Count != arguments.Count)
                throw new Exception($"La función '{functionName}' esperaba {parameters.Count} argumentos, pero recibió {arguments.Count}.");

            var localVariables = new Dictionary<string, object>();
            for (int i = 0; i < parameters.Count; i++)
            {
                localVariables[parameters[i].Trim()] = EvaluateExpression(arguments[i]);
            }

            return ExecuteFunctionBody(body, localVariables);
        }

        private object ExecuteFunctionBody(List<string> body, Dictionary<string, object> localVariables)
        {
            for (int i = 0; i < body.Count; i++)
            {
                string line = body[i].Trim();

                if (line.StartsWith("return "))
                {
                    // Extraer el valor después de "return"
                    string returnExpression = line.Substring(7).TrimEnd(';').Trim();
                    return EvaluateExpression(returnExpression, localVariables);
                }
                else if (line == "return;")
                {
                    // Si "return;" se utiliza sin valor explícito, devolver null
                    return null;
                }

                // Parsear la línea como parte normal del bloque
                List<string> singleLine = new List<string> { line };
                ParseBlock(singleLine, 0, 1, localVariables);
            }

            // Si no se encuentra una sentencia "return", devolver null
            return null;
        }

        private bool IsSystemFunction(string functionName, out MethodInfo methodInfo)
        {
            methodInfo = typeof(Math).GetMethod(functionName, BindingFlags.Static | BindingFlags.Public);
            return methodInfo != null;
        }

        private object CallSystemFunction(MethodInfo method, List<string> arguments)
        {
            List<object> doubleArgs = new List<object>();
            foreach (object arg in arguments)
            {
                doubleArgs.Add(Convert.ToDouble(arg));
            }
            return method.Invoke(null, doubleArgs.ToArray());
        }
    }
}

#endif