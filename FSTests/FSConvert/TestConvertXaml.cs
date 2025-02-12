using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FSConvert.Tests
{
    [TestClass]
    public class TestConvertXaml
    {
        [TestMethod]
        public void TestXaml()
        {
            // Ejemplo de uso
            string winFormsCode = @"
                this.button1 = new System.Windows.Forms.Button();
                this.button1.Location = new System.Drawing.Point(50, 50);
                this.button1.Size = new System.Drawing.Size(75, 23);
                this.button1.Text = ""Click Me"";
                this.button1.UseVisualStyleBackColor = true;
                this.button1.Click += new System.EventHandler(this.Button1_Click);
                this.button1.Name = ""button1"";
                ";

            string xamlCode = ConvertToWPF.Convert(winFormsCode);
            Console.WriteLine(xamlCode);
        }
    }
}