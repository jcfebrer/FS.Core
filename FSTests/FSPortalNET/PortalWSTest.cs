﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSNetwork;
using FSParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSTests.FSParser
{
    [TestClass()]
    public class PortalWSTest
    {
        [TestMethod()]
        public void TestWSLogin()
        {
            //FSTests.com.febrersoftware.www.PortalWS portal = new FSTests.com.febrersoftware.www.PortalWS();

            //string respuesta = portal.Login("admin", "rerbeF2009");

            //if (respuesta != "OK")
            //    Assert.Fail();

            //string respuesta2 = portal.NombreCompleto();

            //if (respuesta2 == "")
            //    Assert.Fail();

            //FSTests.com.febrersoftware.www1.AdminWS admin = new com.febrersoftware.www1.AdminWS();
            //string respuesta3 = admin.Tablas();



            //ServiceReference1.PortalWSSoap p;
            //p.Login(new ServiceReference1.LoginRequest());


            WebService ws = new WebService("http://www.febrersoftware.com/xml/portalws.asmx", "Login");
            ws.IsAspNet = true;
            ws.NameSpace = "http://febrersoftware.com";

            ws.Params.Add("username", "admin");
            ws.Params.Add("password", "rerbeF2009");
            ws.Invoke();

            if (!ws.ResultString.Contains(">OK<"))
                Assert.Fail();

            ws.MethodName = "NombreCompleto";
            ws.Invoke();

            Assert.AreNotEqual(ws.ResultString, "Juan Carlos Febrer");
        }

        [TestMethod()]
        public void TestWSNombreCompleto()
        {
            WebService ws = new WebService("http://www.febrersoftware.com/xml/portalws.asmx", "NombreCompleto");
            ws.IsAspNet = true;
            ws.NameSpace = "http://febrersoftware.com";

            ws.Invoke();

            Assert.AreNotEqual(ws.ResultString, "Juan Carlos Febrer");
        }

        [TestMethod()]
        public void TestWSTablas()
        {
            WebService ws = new WebService("http://www.febrersoftware.com/xml/adminws.asmx", "Tablas");
            ws.IsAspNet = true;
            ws.NameSpace = "http://febrersoftware.com";

            ws.Invoke();

            Assert.AreNotEqual(ws.ResultString, "xxxx");
        }
    }
}
