using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSLibraryCore.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSDatabaseCore;
using System.Data;

namespace FSLibraryCore.BD.Tests
{
    [TestClass()]
    public class UtilsTests
    {
        [TestMethod()]
        public void GetTableNameTest()
        {
            string tableName = FSDatabaseCore.Utils.GetTableName("select * from noticias where noticiaId = 45");

            Assert.AreEqual("Noticias", tableName, "Nombre de tabla incorrecto.");
        }

        [TestMethod()]
        public void GetWhereTest()
        {
            string where = FSDatabaseCore.Utils.GetWhere("select * from noticias where noticiaId = 45");

            Assert.AreEqual("noticiaId = 45", where, "Select incorrecta.");
        }

        [TestMethod()]
        public void CsvReaderTest()
        {
            string data = "\"esto es una \"prueba\", con comillas y coma dentro de la cadena.\",\"123456\",12345";
            CsvReader csvReader = new CsvReader(data);
            
            DataTable table = csvReader.CreateDataTable(false);

            Assert.AreEqual("4", table.Columns.Count.ToString(), "Numero de columnas incorrectas: " + table.Columns.Count);
        }

        [TestMethod()]
        public void CsvDataTableTest()
        {
            string data = "columna1,columna2,columna3";
            data += Environment.NewLine + "\"esto es una \"prueba\", con comillas y coma dentro de la cadena.\",\"123456\",12345";
            string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".csv";
            File.WriteAllText(fileName, data);

            DataTable table = Csv.ConvertCSVtoDataTable(fileName);

            Assert.AreEqual("3", table.Columns.Count.ToString(), "Numero de columnas incorrectas: " + table.Columns.Count);
        }
    }
}