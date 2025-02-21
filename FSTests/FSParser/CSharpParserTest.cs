using FSLibrary;
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

            parser.Parse(code);

            Assert.AreEqual(parser.Variables["x"], 0.0);
            Assert.AreEqual(parser.Variables["y"], 15.0);
            Assert.AreEqual(parser.Variables["z"], 30.0);
            Assert.AreEqual(parser.Variables["var3"], "\"holaadios\"");

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

            parser.Parse(code2);

            Assert.AreEqual(parser.Variables["x"], 0.0);
            Assert.AreEqual(parser.Variables["y"], 15.0);
            Assert.AreEqual(parser.Variables["z"], 30.0);

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

            parser.Parse(code3);

            Assert.AreEqual(parser.Variables["x"], 15.0);
            Assert.AreEqual(parser.Variables["y"], 15.0);
            Assert.AreEqual(parser.Variables["z"], 30.0);
            Assert.AreEqual(parser.Variables["var1"], "\"esto es una prueba\"");
            Assert.AreEqual(parser.Variables["var2"], "Comtoene");
            Assert.AreEqual(parser.Variables["var3"], "esto es dos prueba");

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

            parser.Parse(code4);

            Assert.AreEqual(parser.Variables["x"], 0.893996663600558);
            Assert.AreEqual(parser.Variables["y"], 1.0);
            Assert.AreEqual(parser.Variables["z"], 30.0);
            Assert.AreEqual(parser.Variables["result"], 0.893996663600558);

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

            parser.Parse(code5);

            Assert.AreEqual(parser.Variables["x"], 16.0);
            Assert.AreEqual(parser.Variables["y"], 32.0);
            Assert.AreEqual(parser.Variables["z"], 22.0);
            Assert.AreEqual(parser.Variables["result"], 0.893996663600558);
            Assert.AreEqual(parser.Variables["r"], "\"hola\" \"ad,ios\" \"gabon\"");

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

            parser.Parse(code6);

            Assert.AreEqual(parser.Variables["v1"], "\"hola 40radiola\"");
            Assert.AreEqual(parser.Variables["v2"], "\"hola mundo adios\"");
            Assert.AreEqual(parser.Variables["suma1"], 350.0);
            Assert.AreEqual(parser.Variables["suma2"], "\"holaquetal\"");

// Prueba de redundancia ciclica:
            // la variable result crea redundancia ciclica en SimpleExpressionEvaluator
            var code7 = new List<string>
            {
                "result = \"prueba con result bien\" + \"var1\";",
                "result = \"cadena1;\" + result;"
            };

            parser.Parse(code7);

            Assert.AreEqual(parser.Variables["result"], "\"cadena1;prueba con result bienvar1\"");

            // Prueba de anidamiento
            var code8 = new List<string>
            {
                "result = \"cadena1\";",
                "result = \"using System.Data;\" + crlf + \"using FSFormControls;\" + crlf + result;"
            };

            parser.Parse(code8);

            Assert.AreEqual(parser.Variables["result"], "\"using System.Data;\r\nusing FSFormControls;\r\ncadena1\"");


            var code9 = new List<string>
            {
                "var1 = \"contenido\";",
                "expression = \"esto es una prueba: var1\" + var1;"
            };

            parser.Parse(code9);

            Assert.AreEqual(parser.Variables["expression"], "\"esto es una prueba: var1contenido\"");

            var code10 = new List<string>
            {
                "var1 = \"cont\" + \"enido\";",
                "expression = \"esto es una prueba: \" + var1;"
            };

            parser.Parse(code10);

            Assert.AreEqual(parser.Variables["expression"], "\"esto es una prueba: contenido\"");

            var code11 = new List<string>
            {
                "var1 = \"cont\"enido\";",
                "expression = \"esto es una prueba: \" + var1;"
            };

            parser.Parse(code11);

            Assert.AreEqual(parser.Variables["expression"], "\"esto es una prueba: contenido\"");
        }
    }
}
