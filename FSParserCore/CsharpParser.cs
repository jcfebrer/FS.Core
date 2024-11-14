using FSLibraryCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace FSParserCore
{
    public class CSharpParser
    {
        private readonly Dictionary<string, object> variables = new Dictionary<string, object>();
        private readonly Dictionary<string, (List<string> Parameters, List<string> Body)> functions = new Dictionary<string, (List<string> Parameters, List<string> Body)>();
        private readonly Dictionary<string, Func<List<object>, object>> customCommands = new Dictionary<string, Func<List<object>, object>>();

        private readonly Regex assignmentRegex = new Regex(@"^\s*(\w+)\s*=\s*(.+);$", RegexOptions.Compiled | RegexOptions.Multiline);
        private readonly Regex ifRegex = new Regex(@"^\s*if\s*\((.+)\)\s*{?$", RegexOptions.Compiled | RegexOptions.Multiline);
        private readonly Regex whileRegex = new Regex(@"^\s*while\s*\((.+)\)\s*{?$", RegexOptions.Compiled | RegexOptions.Multiline);
        private readonly Regex functionDefRegex = new Regex(@"^\s*function\s+(\w+)\s*\((.*?)\)\s*{?$", RegexOptions.Compiled | RegexOptions.Multiline);
        private readonly Regex functionCallRegex = new Regex(@"(\w+)\s*\((([^()]|(?<Open>\()|(?<-Open>\)))*)\)", RegexOptions.Compiled | RegexOptions.Multiline);

        public Dictionary<string, object> Variables => variables;

        public Dictionary<string, Func<List<object>, object>> CustomCommands => customCommands;

        public CSharpParser()
        {
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

        private object EvaluateExpression(string expression, Dictionary<string, object> localVariables = null)
        {
            var variablesToUse = localVariables ?? variables;

            // Reemplaza variables en la expresión
            foreach (var variable in variablesToUse)
            {
                var v = variable.Value.ToString();
                if (NumberUtils.IsNumeric(v))
                    v = v.Replace(",", ".");
                else
                    v = "\"" + v + "\"";

                expression = Regex.Replace(expression, $@"\b{variable.Key}\b", v);
            }

            // Evalúa llamadas a funciones dentro de la expresión de manera recursiva
            var match = functionCallRegex.Match(expression);
            while (match.Success)
            {
                string functionName = match.Groups[1].Value;
                string argumentList = match.Groups[2].Value;

                // Divide y evalúa cada argumento, permitiendo funciones como argumentos
                List<object> evaluatedArgs = SplitArguments(argumentList)
                    .Select(arg => EvaluateExpression(arg.Trim(), localVariables))
                    .ToList();

                object result;
                if (functions.ContainsKey(functionName))
                {
                    // Llama a una función definida en el parser
                    result = CallFunction(functionName, evaluatedArgs);
                }
                else if (IsSystemFunction(functionName, out MethodInfo methodInfo))
                {
                    // Llama a una función del sistema
                    result = CallSystemFunction(methodInfo, evaluatedArgs);
                }
                else if (customCommands.ContainsKey(functionName))
                {
                    // Llama a un comando personalizado
                    result = customCommands[functionName](evaluatedArgs);
                }
                else
                {
                    throw new Exception($"Función '{functionName}' no definida.");
                }

                // Reemplaza la llamada de la función en la expresión original
                var v = result.ToString();
                if (NumberUtils.IsNumeric(v))
                    v = v.Replace(",", ".");

                expression = expression.Replace(match.Value, v);
                match = functionCallRegex.Match(expression);
            }

            // Evalúa la expresión final en caso de que sea una operación matemática simple
            try
            {
                return SimpleExpressionEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo evaluar la expresión: {expression}. Error: " + ex.Message);
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

        private object CallFunction(string functionName, List<object> arguments)
        {
            var function = functions[functionName];
            var parameters = function.Parameters;
            var body = function.Body;

            if (parameters.Count != arguments.Count)
                throw new Exception($"La función '{functionName}' esperaba {parameters.Count} argumentos, pero recibió {arguments.Count}.");

            var localVariables = new Dictionary<string, object>();
            for (int i = 0; i < parameters.Count; i++)
            {
                localVariables[parameters[i].Trim()] = arguments[i];
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

        private object CallSystemFunction(MethodInfo method, List<object> arguments)
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