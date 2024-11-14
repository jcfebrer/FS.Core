using FSParserCore;
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
        }
    }
}
