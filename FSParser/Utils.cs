using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    }
}
