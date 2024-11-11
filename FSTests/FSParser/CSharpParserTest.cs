using FSParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSTests.FSParser
{
    [TestClass()]
    public class CSharpParserTest
    {
        [TestMethod()]
        public void CSharpParser()
        {
            var parser = new CSharpParser();
            var code = new List<string>
            {
                "x = 10;",
                "if (x > 5) {",
                "    y = x + 5;",
                "    if (y > 10) {",
                "        z = y * 2;",
                "    }",
                "}",
                "while (x > 0) {",
                "    x = x - 1;",
                "}"
            };

            string code2 = 
                "x = 10;" + Environment.NewLine +
                "if (x > 5) {" + Environment.NewLine +
                "      y = x + 5;" + Environment.NewLine +
                "      if (y > 10) {" + Environment.NewLine +
                "           z = y * 2;" + Environment.NewLine +
                "      }" + Environment.NewLine +
                "}" + Environment.NewLine +
                "while (x > 0) {" + Environment.NewLine +
                "   x = x - 1;" + Environment.NewLine +
                "}";

            string code3 = @"
                function Suma(a, b) {
                    return a + b;
                }
                x = Suma(5, 10);
                Print(x);
            ";

            var code4 = new List<string>
            {
                "x = Sin(90);",
                "y = Cos(0);",
                "Print(x, y);",
                "function Multiply(a, b) {",
                "    return a * b;",
                "}",
                "result = Multiply(x, y);",
                "Print(result);"
            };

            var code5 = new List<string>
            {
                "function Add(a, b) {",
                "    return a + b;",
                "}",
                "function Multiply(a, b) {",
                "    return a * b;",
                "}",
                "x = Add(10, 5);",
                "y = Multiply(x, 2);",
                "if (y > 20) {",
                "    z = y - 10;",
                "}"
            };

            parser.Parse(code);

            Assert.AreEqual(parser.Variables["x"], 0);
            Assert.AreEqual(parser.Variables["y"], 15);
            Assert.AreEqual(parser.Variables["z"], 30);

            parser.Parse(code2);

            Assert.AreEqual(parser.Variables["x"] , 0);
            Assert.AreEqual(parser.Variables["y"], 15);
            Assert.AreEqual(parser.Variables["z"], 30);

            parser.Parse(code3);

            Assert.AreEqual(parser.Variables["x"], 15);

            parser.Parse(code4);

            Assert.AreEqual(parser.Variables["x"], 1);
            Assert.AreEqual(parser.Variables["y"], 1);
            Assert.AreEqual(parser.Variables["z"], 30);
            Assert.AreEqual(parser.Variables["result"], 1);

            parser.Parse(code5);

            Assert.AreEqual(parser.Variables["x"], 15);
            Assert.AreEqual(parser.Variables["y"], 30);
            Assert.AreEqual(parser.Variables["z"], 20);
            Assert.AreEqual(parser.Variables["result"], 1);
        }
    }
}
