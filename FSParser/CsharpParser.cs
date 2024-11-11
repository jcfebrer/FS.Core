using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace FSParser
{
    public class CSharpParser
    {
        private readonly Dictionary<string, object> variables = new Dictionary<string, object>();
        private readonly Dictionary<string, (List<string> Parameters, List<string> Body)> functions = new Dictionary<string, (List<string> Parameters, List<string> Body)>();
        private readonly Dictionary<string, Func<List<object>, object>> customCommands = new Dictionary<string, Func<List<object>, object>>();

        // Expresiones regulares para identificar instrucciones de C#
        private readonly Regex assignmentRegex = new Regex(@"^\s*(\w+)\s*=\s*(.+);$", RegexOptions.Compiled);
        private readonly Regex ifRegex = new Regex(@"^\s*if\s*\((.+)\)\s*{?$", RegexOptions.Compiled);
        private readonly Regex whileRegex = new Regex(@"^\s*while\s*\((.+)\)\s*{?$", RegexOptions.Compiled);
        private readonly Regex functionDefRegex = new Regex(@"^\s*function\s+(\w+)\s*\((.*?)\)\s*{?$", RegexOptions.Compiled);
        private readonly Regex functionCallRegex = new Regex(@"(\w+)\s*\((.*?)\)");

        public Dictionary<string, object> Variables => variables;

        public CSharpParser()
        {
            customCommands["Print"] = args =>
            {
                Console.WriteLine(string.Join(" ", args));
                return null;
            };
        }

        private object EvaluateExpression(string expression, Dictionary<string, object> localVariables = null)
        {
            var variablesToUse = localVariables ?? variables;

            foreach (var variable in variablesToUse)
            {
                expression = Regex.Replace(expression, $@"\b{variable.Key}\b", variable.Value.ToString());
            }

            var match = functionCallRegex.Match(expression);
            while (match.Success)
            {
                string functionName = match.Groups[1].Value;
                string argumentList = match.Groups[2].Value;

                List<string> arguments = new List<string>(argumentList.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

                if (functions.ContainsKey(functionName))
                {
                    object functionResult = CallFunction(functionName, arguments);
                    expression = expression.Replace(match.Value, functionResult.ToString());
                }
                else if (IsSystemFunction(functionName, out MethodInfo methodInfo))
                {
                    object functionResult = CallSystemFunction(methodInfo, arguments);

                    // Si el resultado es de tipo double, convierte a string usando formato invariante
                    if (functionResult is double doubleResult)
                    {
                        // Convierte el resultado a una cadena con punto decimal
                        functionResult = doubleResult.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    }
                    
                    expression = expression.Replace(match.Value, functionResult.ToString());
                }
                else if (customCommands.ContainsKey(functionName))
                {
                    List<object> evaluatedArgs = arguments.ConvertAll(arg => EvaluateExpression(arg, localVariables));
                    object result = customCommands[functionName](evaluatedArgs);
                    expression = expression.Replace(match.Value, result?.ToString() ?? string.Empty);
                }
                else
                {
                    throw new Exception($"Función '{functionName}' no definida.");
                }

                match = functionCallRegex.Match(expression);
            }

            try
            {
                if (int.TryParse(expression, out int numericResult))
                    return numericResult;

                var result = new DataTable().Compute(expression, null);
                return Convert.ToInt32(result);
            }
            catch
            {
                throw new Exception($"No se pudo evaluar la expresión: {expression}");
            }
        }

        public void Parse(string code)
        {
            List<string> lines = new List<string>(code.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            Parse(lines);
        }

        public void Parse(List<string> lines)
        {
            ParseBlock(lines, 0, lines.Count);
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
                    int closingIndex = FindClosingBracket(lines, i);

                    if (!line.Contains("{") && lines[i + 1].Trim() == "{") i++;

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
                    int closingIndex = FindClosingBracket(lines, i);

                    if (!line.Contains("{") && lines[i + 1].Trim() == "{") i++;

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
                    int closingIndex = FindClosingBracket(lines, i);

                    if (!line.Contains("{") && lines[i + 1].Trim() == "{") i++;

                    while (Convert.ToBoolean(EvaluateExpression(condition, localVariables)))
                        ParseBlock(lines, i + 1, closingIndex, localVariables);

                    i = closingIndex;
                    continue;
                }

                // Verifica si la línea es una llamada a un comando personalizado
                var customCommandMatch = functionCallRegex.Match(line);
                if (customCommandMatch.Success)
                {
                    string commandName = customCommandMatch.Groups[1].Value;
                    string argumentList = customCommandMatch.Groups[2].Value;

                    // Si el comando existe en customCommands, lo ejecuta
                    if (customCommands.ContainsKey(commandName))
                    {
                        List<object> evaluatedArgs = argumentList
                            .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(arg => EvaluateExpression(arg.Trim(), localVariables))
                            .ToList();

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
            variablesToUse[variableName] = EvaluateExpression(value, localVariables);
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
                localVariables[parameters[i].Trim()] = EvaluateExpression(arguments[i].Trim());
            }

            return ExecuteFunctionBody(body, localVariables);
        }

        private object ExecuteFunctionBody(List<string> body, Dictionary<string, object> localVariables)
        {
            object returnValue = null;

            for (int i = 0; i < body.Count; i++)
            {
                string line = body[i].Trim();

                if (line.StartsWith("return"))
                {
                    string returnExpression = line.Substring(6).TrimEnd(';').Trim();
                    returnValue = EvaluateExpression(returnExpression, localVariables);
                    break;
                }

                ParseBlock(body, i, body.Count, localVariables);
            }

            return returnValue ?? 0;
        }

        private bool IsSystemFunction(string functionName, out MethodInfo methodInfo)
        {
            methodInfo = typeof(Math).GetMethod(functionName, BindingFlags.Static | BindingFlags.Public);
            return methodInfo != null;
        }

        private object CallSystemFunction(MethodInfo method, List<string> arguments)
        {
            var parameterValues = new List<object>();
            foreach (string arg in arguments)
            {
                parameterValues.Add(Convert.ToDouble(EvaluateExpression(arg)));
            }
            return method.Invoke(null, parameterValues.ToArray());
        }
    }
}