using FSParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FSTest.FSParser
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

            var objresult = SimpleExpressionEvaluator.Evaluate("\"esto es una prueba\"");

            Assert.AreEqual(objresult, "\"esto es una prueba\"");

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
            string resultStr2 = (string)SimpleExpressionEvaluator.Evaluate("var1 + var2", memoria2);

            Assert.AreEqual(resultStr2, "\"holaadios\"");

            Dictionary<string, object> memoria3 = new Dictionary<string, object>();
            memoria3.Add("var1", "hola");
            memoria3.Add("var2", "adios");
            memoria3.Add("var3", "var1");
            string resultStr3 = (string)SimpleExpressionEvaluator.Evaluate("var1 + var2 + var3", memoria3);

            Assert.AreEqual(resultStr3, "\"holaadioshola\"");

            Dictionary<string, object> memoria4 = new Dictionary<string, object>();
            memoria4.Add("var1", "\"hola\"");
            memoria4.Add("var2", "\"adios\"");
            memoria4.Add("var3", "\"var1\"");
            string resultStr4 = (string)SimpleExpressionEvaluator.Evaluate("var1 + var2 + var3", memoria4);

            Assert.AreEqual(resultStr4, "\"holaadiosvar1\"");

            Dictionary<string, object> memoria5 = new Dictionary<string, object>();
            memoria5.Add("var1", "\"hola\"");
            memoria5.Add("var2", "var1 + \"prueba\"");
            memoria5.Add("var3", "\" var1\"");
            string resultStr5 = (string)SimpleExpressionEvaluator.Evaluate("var1 + var2 + var3", memoria5);

            Assert.AreEqual(resultStr5, "\"holaholaprueba var1\"");

            Dictionary<string, object> memoria6 = new Dictionary<string, object>();
            memoria6.Add("result", "\"hola\"");
            memoria6.Add("result2", "\"hola\" + result");
            string resultStr6 = (string)SimpleExpressionEvaluator.Evaluate("result + result2", memoria6);

            Assert.AreEqual(resultStr6, "\"holaholahola\"");
        }
    }
}
