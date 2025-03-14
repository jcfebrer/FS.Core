using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            try {
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
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
