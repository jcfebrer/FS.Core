using FSLibraryCore;
using FSParserCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSTestsCore.FSParser
{
    [TestClass()]
    public class CSharpParserTest
    {
        [TestMethod()]
        public void CSharpParser()
        {
            var parser = new CSharpParser();

            parser = CSharpParserCustomCommands.Commands(parser);

            var code = new List<string>
            {
                "var1 = \"hola\";",
                "var2 = \"adios\";",
                "var3 = var1 + var2;",
                "x = 10;",
                "if (x > 5)",
                " {",
                "    y = x + 5;",
                "    if (y > 10) {",
                "        z = y * 2;",
                "    }",
                "}",
                "while (x > 0) {",
                "    x = x - 1;",
                "}"
            };

            string code2 = @"
                x = 10;
                // Ejemplo de comentarios
                // Segunda línea
                /*
                    Esto es otra prueba
                */
                if (x > 5) 
                {
                      y = x + 5;
                      if (y > 10) {
                           z = y * 2;
                      }
                }
                while (x > 0) {
                   x = x - 1;
                }
            ";

            string code3 = @"
                function Suma(a, b) {
                    return a + b;
                }
                extension = "".cs"";
                if(extension == "".cs"") {
                help = help();
                    }
                var1 = ""esto es una prueba"";
                if(contains(var1, ""una"")) {
                    var2 = replace(""Contiene"", ""nti"", ""mto"");
                }
                var3=replacereg(""esto es una prueba"", ""una"", ""dos"");
                x = Suma(5, 10);
                print(x);
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
                "r = Concat(\"hola\",\"ad,ios\", \"gabon\");",
                "x = Add(10, Multiply(3,2));",
                "y = Multiply(x, 2);",
                "if (y > Multiply(10,2)) {",
                "    z = y - 10;",
                "}"
            };

            var code6 = new List<string>
            {
                "hola = \"mundo\";",
                "v1 = \"hola \" + 40 + \"radiola\";",
                "v2 = \"hola \" + hola + \" adios\";",
                "var1 = 100;",
                "var2 = 200;",
                "var3 = 50;",
                "suma1 = var1 + var2 + var3;",
                
                "var1 = \"hola\";",
                "var2 = \"que\";",
                "var3 = \"tal\";",
                "suma2 = var1 + var2 + var3;"
            };

            parser.Parse(code);

            Assert.AreEqual(parser.Variables["x"], 0.0);
            Assert.AreEqual(parser.Variables["y"], 15.0);
            Assert.AreEqual(parser.Variables["z"], 30.0);
            Assert.AreEqual(parser.Variables["var3"], "holaadios");

            parser.Parse(code2);

            Assert.AreEqual(parser.Variables["x"] , 0.0);
            Assert.AreEqual(parser.Variables["y"], 15.0);
            Assert.AreEqual(parser.Variables["z"], 30.0);

            parser.Parse(code3);

            Assert.AreEqual(parser.Variables["x"], 15.0);
            Assert.AreEqual(parser.Variables["y"], 15.0);
            Assert.AreEqual(parser.Variables["z"], 30.0);
            Assert.AreEqual(parser.Variables["var1"], "esto es una prueba");
            Assert.AreEqual(parser.Variables["var2"], "Comtoene");
            Assert.AreEqual(parser.Variables["var3"], "esto es dos prueba");

            parser.Parse(code4);

            Assert.AreEqual(parser.Variables["x"], 0.893996663600558);
            Assert.AreEqual(parser.Variables["y"], 1.0);
            Assert.AreEqual(parser.Variables["z"], 30.0);
            Assert.AreEqual(parser.Variables["result"], 0.893996663600558);

            parser.Parse(code5);

            Assert.AreEqual(parser.Variables["x"], 16.0);
            Assert.AreEqual(parser.Variables["y"], 32.0);
            Assert.AreEqual(parser.Variables["z"], 22.0);
            Assert.AreEqual(parser.Variables["result"], 0.893996663600558);
            Assert.AreEqual(parser.Variables["r"], "\"hola\" \"ad,ios\" \"gabon\"");

            parser.Parse(code6);

            Assert.AreEqual(parser.Variables["v1"], "hola 40radiola");
            Assert.AreEqual(parser.Variables["v2"], "hola mundo adios");
            Assert.AreEqual(parser.Variables["suma1"], 350.0);
            Assert.AreEqual(parser.Variables["suma2"], "holaquetal");
        }
    }
}
