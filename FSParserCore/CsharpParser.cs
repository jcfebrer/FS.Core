using FSLibraryCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace FSParserCore
{
    public class CSharpParser
    {
        private readonly Dictionary<string, object> variables = new Dictionary<string, object>();
        private readonly Dictionary<string, (List<string> Parameters, List<string> Body)> functions = new Dictionary<string, (List<string> Parameters, List<string> Body)>();
        private readonly Dictionary<string, Func<List<string>, object>> customCommands = new Dictionary<string, Func<List<string>, object>>();

        private readonly static string singleLineCommentPattern = @"(\s|\t)*//.*";
        private readonly static string blockCommentPattern = @"/\*.*?\*/";
        private readonly static string assignmentPattern = @"^\s*(\w+)\s*=\s*(.+);$";
        private readonly static string ifPattern = @"^\s*if\s*\((.+)\)\s*{?$";
        private readonly static string returnPattern = @"^\s*return\s*(.*);";
        private readonly static string whilePattern = @"^\s*while\s*\((.+)\)\s*{?$";
        private readonly static string functionDefPattern = @"^\s*function\s+(\w+)\s*\((.*?)\)\s*{?$";
        private readonly static string functionCallPattern = @"(\w+)\s*\((([^()]|(?<Open>\()|(?<-Open>\)))*)\)";
        private readonly static string functionCallProtectPattern = @"(\#\w+\#)\s*\((([^()]|(?<Open>\()|(?<-Open>\)))*)\)";
        private readonly static string allowedTextInLine = @"(\{|\}|/\*|\*/)";

        private readonly Regex singleLineCommentRegex = new Regex(singleLineCommentPattern, RegexOptions.Compiled | RegexOptions.Multiline);
        private readonly Regex blockCommentRegex = new Regex(blockCommentPattern, RegexOptions.Compiled | RegexOptions.Singleline);
        private readonly Regex assignmentRegex = new Regex(assignmentPattern, RegexOptions.Compiled | RegexOptions.Multiline);
        private readonly Regex ifRegex = new Regex(ifPattern, RegexOptions.Compiled | RegexOptions.Multiline);
        private readonly Regex returnRegex = new Regex(returnPattern, RegexOptions.Compiled | RegexOptions.Multiline);
        private readonly Regex whileRegex = new Regex(whilePattern, RegexOptions.Compiled | RegexOptions.Multiline);
        private readonly Regex functionDefRegex = new Regex(functionDefPattern, RegexOptions.Compiled | RegexOptions.Multiline);
        private readonly Regex functionCallRegex = new Regex(functionCallPattern, RegexOptions.Compiled | RegexOptions.Multiline);
        private readonly Regex functionCallProtectRegex = new Regex(functionCallProtectPattern, RegexOptions.Compiled | RegexOptions.Multiline);

        public Dictionary<string, object> Variables => variables;
        public Dictionary<string, Func<List<string>, object>> CustomCommands => customCommands;

        public CSharpParser()
        {
        }

        private string RemoveComments(string code)
        {
            // Eliminar comentarios de bloque y de una sola línea
            code = blockCommentRegex.Replace(code, "");
            code = singleLineCommentRegex.Replace(code, "");
            return code;
        }

        // Nuevo método para dividir argumentos teniendo en cuenta paréntesis anidados
        private List<string> SplitArguments(string arguments)
        {
            var result = new List<string>();
            var currentArg = "";
            int parenthesesBalance = 0;
            bool insideString = false;

            foreach (char c in arguments)
            {
                if (c == ',' && parenthesesBalance == 0 && !insideString)
                {
                    result.Add(TextUtil.RemoveQuotes(currentArg.Trim()));
                    currentArg = "";
                }
                else
                {
                    if (c == '(' && !insideString) parenthesesBalance++;
                    if (c == ')' && !insideString) parenthesesBalance--;
                    if (c == '\"') insideString = !insideString; // Toggle insideString on encountering "

                    currentArg += c;
                }
            }

            if (!string.IsNullOrWhiteSpace(currentArg))
                result.Add(TextUtil.RemoveQuotes(currentArg.Trim()));

            return result;
        }

        /// <summary>
        /// Esta función evita que se evalue funciones de C# que existen en este parser.
        /// Pone una almohadilla detrás y delante del nombre de la función para que no se parsee.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string ProtectData(string code)
        {
            // Usa un Regex para realizar un reemplazo eficiente
            return functionCallRegex.Replace(code, match =>
            {
                string functionName = match.Groups[1].Value;
                string arguments = match.Groups[2].Value;

                // Construye el nuevo formato de la función
                return $"#{functionName}#({arguments})";
            });
        }

        /// <summary>
        /// Esta función restaura el código a protegido con la función ProtectData.
        /// Quita la almohadilla detrás y delante del nombre de la función.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string UnProtectData(string code)
        {
            return functionCallProtectRegex.Replace(code, match =>
            {
                string functionName = match.Groups[1].Value.Replace("#", "");
                string arguments = match.Groups[2].Value;
                return $"{functionName}({arguments})";
            });
        }

        private List<string> ApplyVariables(List<string> arguments)
        {
            var result = new List<string>();
            foreach(string s in arguments)
            {
                result.Add(ApplyVariables(s));
            }
            return result;
        }

        private string ApplyVariables(string expression, Dictionary<string, object> localVariables = null, HashSet<string> resolvedVariables = null)
        {
            var variablesToUse = localVariables ?? variables;

            // Crear el conjunto para rastrear variables resueltas, si no se proporcionó
            if (resolvedVariables == null)
                resolvedVariables = new HashSet<string>();

            // Copiar las claves y valores del diccionario para evitar conflictos durante la iteración
            var variablesSnapshot = variablesToUse.ToList();

            foreach (var variable in variablesSnapshot)
            {
                // Evitar resolver variables ya visitadas en esta llamada recursiva
                if (resolvedVariables.Contains(variable.Key))
                    continue;

                var variableValue = variable.Value.ToString();

                // Si el valor de la variable contiene otras variables
                if (variablesToUse.Keys.Any(key => variableValue.Contains(key)))
                {
                    if (resolvedVariables.Contains(variable.Key))
                        throw new Exception($"Referencia cíclica detectada en la variable '{variable.Key}'");

                    // Marcar la variable como visitada
                    resolvedVariables.Add(variable.Key);

                    // Resolver el valor recursivamente
                    variableValue = ApplyVariables(variableValue, variablesToUse, resolvedVariables);

                    // Actualizar el valor resuelto
                    variablesToUse[variable.Key] = variableValue;

                    // Eliminar la marca de la variable para futuros usos
                    resolvedVariables.Remove(variable.Key);
                }

                // Aplicar formato correcto a los valores no numéricos
                if (NumberUtils.IsNumeric(variableValue))
                {
                    variableValue = variableValue.Replace(",", ".");
                }
                //else if (!(variableValue.StartsWith("\"") && variableValue.EndsWith("\"")))
                //{
                //    variableValue = "\"" + variableValue + "\"";
                //}

                // Reemplazar las apariciones de la variable en la expresión
                expression = Regex.Replace(expression, $@"\[{variable.Key}\]", variableValue);
            }

            return expression;
        }

        private bool IsSimpleMathExpression(string expression)
        {
            // Reemplazamos las cadenas entre comillas para que no se tengan en cuenta en la evaluación
            expression = TextUtil.ReplaceStrings(expression, "dummy");

            // Patrón para caracteres válidos en una expresión matemática
            var validExpressionPattern = @"^[\d\s\+\-\*/\^<>=!()\[\]\.\,\""\w]*$";

            // Patrón para detectar al menos un operador válido
            var hasOperatorPattern = @"(\+|\-|\*|/|\^|!|<|>|<=|>=|==|!=)";

            // Verifica que todos los caracteres sean válidos y que haya al menos un operador
            return Regex.IsMatch(expression, validExpressionPattern) && Regex.IsMatch(expression, hasOperatorPattern);
        }

        private object EvaluateExpression(string expression, Dictionary<string, object> localVariables = null)
        {
            var variablesToUse = localVariables ?? variables;

            // Evalúa funciones recursivamente utilizando functionCallRegex.Replace
            expression = functionCallRegex.Replace(expression, match =>
            {
                string functionName = match.Groups[1].Value;
                string argumentList = match.Groups[2].Value;

                // Evalúa los argumentos
                var evaluatedArgs = ApplyVariables(SplitArguments(argumentList));

                object result;

                // Usa un bloque `switch` tradicional
                if (functions.ContainsKey(functionName))
                {
                    result = CallFunction(functionName, evaluatedArgs);
                }
                else if (IsSystemFunction(functionName, out MethodInfo methodInfo))
                {
                    result = CallSystemFunction(methodInfo, evaluatedArgs);
                }
                else if (customCommands.ContainsKey(functionName))
                {
                    result = customCommands[functionName](evaluatedArgs);
                }
                else
                {
                    throw new Exception($"Función '{functionName}' no definida.");
                }

                // Devuelve el resultado formateado
                return FormatResult(result);
            });

            // Evalúa la expresión final si es una operación matemática simple
            return EvaluateFinalExpression(expression, variablesToUse);
        }

        private string FormatResult(object result)
        {
            string value = result.ToString();
            if (NumberUtils.IsNumeric(value))
            {
                return value.Replace(",", ".");
            }
            return value;
        }

        private object EvaluateFinalExpression(string expression, Dictionary<string, object> variablesToUse)
        {
            try
            {
                if (IsSimpleMathExpression(expression))
                {
                    return SimpleExpressionEvaluator.Evaluate(expression, variablesToUse);
                }
                else if (NumberUtils.IsNumeric(expression))
                {
                    return Convert.ToDouble(expression.Replace(".", ","));
                }
                else
                {
                    return TextUtil.RemoveQuotes(expression);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo evaluar la expresión: {expression}. Error: {ex.Message}");
            }
        }


        // Validación de la sintaxis completa del bloque de código
        private bool IsBlockSyntaxValid(string codeBlock)
        {
            // Comprobar contra cada patrón de bloque (for, if, where)
            string[] blockPatterns = { ifPattern, whilePattern, blockCommentPattern };

            foreach (var pattern in blockPatterns)
            {
                if (Regex.IsMatch(codeBlock, pattern))
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
            string[] linePatterns = { assignmentPattern, functionCallPattern, functionDefPattern, returnPattern, singleLineCommentPattern, allowedTextInLine };

            foreach (var pattern in linePatterns)
            {
                if (Regex.IsMatch(line, pattern))
                    return true;  // Devuelve verdadero si la línea coincide con alguna estructura válida
            }

            return false;
        }

        public void CheckSyntax(string codeBlock)
        {
            // Separemos el código en líneas
            string[] codeLines = codeBlock.Split(Environment.NewLine.ToCharArray());
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

            code = RemoveComments(code);
            List<string> lines = new List<string>(code.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
            ParseBlock(lines, 0, lines.Count);
        }

        private void AddGeneralVariables()
        {
            // Salto de linea
            if(!variables.ContainsKey("crlf"))
                variables.Add("crlf", Environment.NewLine);
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
                string line = lines[i].Trim();

                if (string.IsNullOrEmpty(line))
                    continue;

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
                        i = ParseBlock(lines, i + 1, closingIndex, localVariables);
                    else
                        i = closingIndex;
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
                    string commandName = customCommandMatch.Groups[1].Value;
                    string argumentList = customCommandMatch.Groups[2].Value;

                    if (customCommands.ContainsKey(commandName))
                    {
                        List<string> evaluatedArgs = SplitArguments(argumentList);
                            //.Select(arg => EvaluateExpression(arg.Trim(), localVariables))
                            //.ToList();

                        customCommands[commandName](evaluatedArgs);
                        continue;
                    }
                }

                throw new Exception($"Instrucción no reconocida: {line}");
            }
            return end;
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