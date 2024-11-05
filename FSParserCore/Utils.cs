using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSParserCore
{
    public class Utils
    {
        public static bool EvaluatorExpressionIf(string condition)
        {
            string exprIf = "if(" + condition + ")\r\nreturn true;\r\nelse\r\nreturn false;";
            try {
            ExpressionEvaluator evaluator = new ExpressionEvaluator();
            return Convert.ToBoolean(evaluator.ScriptEvaluate(exprIf));
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
