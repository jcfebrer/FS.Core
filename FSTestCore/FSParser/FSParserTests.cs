using FSParserCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FSTestsCore.FSParser
{
    [TestClass()]
    public class FSParserTests
    {
        [TestMethod()]
        public void CsvParser()
        {
            string data = "\"esto es una \"prueba\", con comillas y coma dentro de la cadena.\",\"123456\",12345";
            CsvParser csv = new CsvParser(data);

            int f = 0;
            foreach (var item in csv)
            {
                Console.WriteLine(item);
                f++;
            }

            Assert.AreEqual("3", f.ToString(), "Numero de tokens: " + f.ToString());
        }

        [TestMethod()]
        public void CalcJS()
        {
            object res = ScriptEngine.Eval("jscript", "1+2/3");
            Assert.AreEqual("1,66666666666667", res.ToString(), "Cálculo de 1+2/3 en javascript: " + res.ToString());
        }

        [TestMethod()]
        public void CalcJSFunc()
        {
            using (ScriptEngine engine = new ScriptEngine("jscript"))
            {
                ParsedScript parsed = engine.Parse("function MyFunc(x){return 1+2+x}");
                object res = parsed.CallMethod("MyFunc", 3);

                Assert.AreEqual("6", res.ToString(), "Cálculo de 1+2+3 en javascript: " + res.ToString());
            }
        }

        [TestMethod()]
        public void CalcJSFunc2()
        {
            using (ScriptEngine engine = new ScriptEngine("jscript"))
            {
                ParsedScript parsed = engine.Parse("function MyFunc(x){return 1+2+x+My.Num}");
                MyItem item = new MyItem();
                item.Num = 4;
                engine.SetNamedItem("My", item);
                object res = parsed.CallMethod("MyFunc", 3);

                Assert.AreEqual("10", res.ToString(), "Cálculo de 1+2+3+4 en javascript: " + res.ToString());
            }
        }


        [TestMethod()]
        public void Evaluator1()
        {
            ExpressionEvaluator evaluator = new ExpressionEvaluator();
            object res = evaluator.Evaluate("Sqrt(2) / 3");
            Assert.AreEqual("0,471404520791032", res.ToString(), "Cálculo de Sqrt(2) / 3: " + res.ToString());
        }

        [TestMethod()]
        public void Evaluator2()
        {
            ExpressionEvaluator evaluator = new ExpressionEvaluator();
            object res = evaluator.Evaluate("Max(1+1, 2+3, 2*6, Pow(2, 3))");
            Assert.AreEqual("12", res.ToString(), "Cálculo de Max(1+1, 2+3, 2*6, Pow(2, 3)): " + res.ToString());
        }

        [TestMethod()]
        public void Evaluator3()
        {
            string expr = @"Regex.Match(""Test 34 Hello / -World"", @""\d+"").Value";
            ExpressionEvaluator evaluator = new ExpressionEvaluator();
            object res = evaluator.Evaluate(expr);
            Assert.AreEqual("34", res.ToString(), "Cálculo de " + expr + "): " + res.ToString());
        }

        [TestMethod()]
        public void Evaluator4()
        {
            string expr = @"""Hello,Test,What"".Split(new char[] {','})[1]";
            ExpressionEvaluator evaluator = new ExpressionEvaluator();
            object res = evaluator.Evaluate(expr);
            Assert.AreEqual("Test", res.ToString(), "Cálculo de " + expr + "): " + res.ToString());
        }

        [TestMethod()]
        public void EvaluatorScript5()
        {
            string expr = @"x = 0;
            result = """";

            while(x < 5)
            {
                result += $""{x},"";
                x++;
            }

            result.Remove(result.Length - 1);";
            ExpressionEvaluator evaluator = new ExpressionEvaluator();
            object res = evaluator.ScriptEvaluate(expr);
            Assert.AreEqual("0,1,2,3,4", res.ToString(), "Cálculo de " + expr + "): " + res.ToString());
        }

        [TestMethod()]
        public void EvaluatorScript6()
        {
            string expr = "value = 1+2;\r\nif(value > 2)\r\nreturn \"OK\";\r\nelse\r\nreturn \"NOK\";";
            ExpressionEvaluator evaluator = new ExpressionEvaluator();
            object res = evaluator.ScriptEvaluate(expr);
            Assert.AreEqual("OK", res.ToString(), "Cálculo de " + expr + "): " + res.ToString());
        }

        [TestMethod()]
        public void EvaluatorScript7()
        {
            string condition = "\"hola\" == \"adios\"";
            string exprIf = "if(" + condition + ")\r\nreturn true;\r\nelse\r\nreturn false;";
            ExpressionEvaluator evaluator = new ExpressionEvaluator();
            bool res = Convert.ToBoolean(evaluator.ScriptEvaluate(exprIf));
            Assert.AreEqual(false, res, "Cálculo de " + exprIf + "): " + res.ToString());
        }
    }


    [ComVisible(true)] // Script engines are COM components.
    public class MyItem
    {
        public int Num { get; set; }
    }
}
