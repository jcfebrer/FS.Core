using FSParserCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSTestCore.FSParser
{
    [TestClass()]
    public class SimpleExpressionEvaluatorTest
    {
        [TestMethod()]
        public void CSharpParser()
        {
            double result;
            result = (double)SimpleExpressionEvaluator.Evaluate("5 * 6");

            Assert.AreEqual(result, 30.0);

            result = (double)SimpleExpressionEvaluator.Evaluate("5 * (6 * 2)");

            Assert.AreEqual(result, 60.0);

            result = (double)SimpleExpressionEvaluator.Evaluate("5.5 * 2");

            Assert.AreEqual(result, 11.0);

            result = (double)SimpleExpressionEvaluator.Evaluate("5,5 * 2");

            Assert.AreEqual(result, 11.0);

            bool boolresult = (bool)SimpleExpressionEvaluator.Evaluate("\"gato\" == \"perro\"");

            Assert.AreEqual(boolresult, false);

            boolresult = (bool)SimpleExpressionEvaluator.Evaluate("\"gato\" == \"gato\"");

            Assert.AreEqual(boolresult, true);

            boolresult = (bool)SimpleExpressionEvaluator.Evaluate("!True");

            Assert.AreEqual(boolresult, false);

            boolresult = (bool)SimpleExpressionEvaluator.Evaluate("5 != 8");

            Assert.AreEqual(boolresult, true);

            Dictionary<string, object> memoria = new Dictionary<string, object>();
            memoria.Add("var1", 15);
            memoria.Add("var2", 24);
            result = (double)SimpleExpressionEvaluator.Evaluate("var1 + var2", memoria);

            Assert.AreEqual(result, 39.0);

            Dictionary<string, object> memoria2 = new Dictionary<string, object>();
            memoria2.Add("var1", "hola");
            memoria2.Add("var2", "adios");
            string resultStr = (string)SimpleExpressionEvaluator.Evaluate("var1 + var2", memoria2);

            Assert.AreEqual(resultStr, "holaadios");
        }
    }
}
