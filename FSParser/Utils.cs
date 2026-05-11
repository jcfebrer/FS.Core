using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;

namespace FSParser
{
    public class Utils
    {
        public static bool EvaluatorExpressionIf(string condition)
        {
            return EvaluatorExpressionIf(condition, null);
        }

        public static bool EvaluatorExpressionIf(string condition, NameValueCollection variables)
        {
            string cleancondition = condition.Replace("<", "").Replace(">", "");
            string exprIf = "if(" + cleancondition + ")\r\nreturn true;\r\nelse\r\nreturn false;";
            try
            {
#if NET45_OR_GREATER || NETCOREAPP
                ExpressionEvaluator evaluator = new ExpressionEvaluator();

                //añadimos las variables
                if (variables != null)
                {
                    for (int f = 0; f < variables.Count; f++)
                    {
                        evaluator.Variables.Add(variables.Keys[f], variables[f]);
                    }
                }
                return Convert.ToBoolean(evaluator.ScriptEvaluate(exprIf));
#else
                Dictionary<string, object> vars = new Dictionary<string, object>();
                //añadimos las variables
                if (variables != null)
                {
                    for (int f = 0; f < variables.Count; f++)
                    {
                        vars.Add(variables.Keys[f], variables[f]);
                    }
                }

                return Convert.ToBoolean(SimpleExpressionEvaluator.Evaluate(condition, vars));
#endif
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static string DinamicVariablesReplace(string content, Type globalType)
        {
            // 1. Procesar Propiedades (si usas { get; set; })
            PropertyInfo[] porperties = globalType.GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (var prop in porperties)
            {
                string mark = "{" + prop.Name + "}";
                if (content.Contains(mark))
                {
                    object value = prop.GetValue(null, null);
                    content = content.Replace(mark, value?.ToString() ?? "");
                }
            }

            // 2. Procesar Campos (si usas variables directas como: public static string Servidor;)
            FieldInfo[] fields = globalType.GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields)
            {
                string mark = "{" + field.Name + "}";
                if (content.Contains(mark))
                {
                    object value = field.GetValue(null);
                    content = content.Replace(mark, value?.ToString() ?? "");
                }
            }

            return content;
        }
    }
}
