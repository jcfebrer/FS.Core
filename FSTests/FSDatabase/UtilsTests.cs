using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSLibrary.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSLibrary.BD.Tests
{
    [TestClass()]
    public class UtilsTests
    {
        [TestMethod()]
        public void GetTableNameTest()
        {
            string tableName = FSDatabase.Utils.GetTableName("select * from noticias where noticiaId = 45");

            Assert.AreEqual("Noticias", tableName, "Nombre de tabla incorrecto.");
        }

        [TestMethod()]
        public void GetWhereTest()
        {
            string where = FSDatabase.Utils.GetWhere("select * from noticias where noticiaId = 45");

            Assert.AreEqual("noticiaId = 45", where, "Select incorrecta.");
        }
    }
}