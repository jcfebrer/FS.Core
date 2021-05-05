// // <fileheader>
// // <copyright file="Global.asax.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSPortal
// //     Solution: FSPortalNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System;
using System.IO;
using System.Web;

#endregion

namespace FSPortalLite
{
    public class Global : HttpApplication
    {
        public void Application_Error(object sender, EventArgs e)
        {
            throw new Exception("Application_Error", Server.GetLastError());
        }


        public void Application_Start(object sender, EventArgs e)
        {
        }


        public void Application_End(object sender, EventArgs e)
        {
        }


        public void Session_Start(object sender, EventArgs e)
        {
        }


        public void Session_End(object sender, EventArgs e)
        {
        }


        public void Application_BeginRequest(object sender, EventArgs e)
        {
        }
    }
}