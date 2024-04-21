using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSLibraryCore.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}