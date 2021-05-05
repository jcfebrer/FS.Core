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
using FSException;
using System.Configuration;
using FSTrace;

#endregion

namespace FSPortal
{
    public class Global : HttpApplication
    {
        public void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            
            FSMail.SendMail.SendErrorMail(ex.Message, ex);
            Log.TraceError(ex.ToString());

            throw new ExceptionUtil(ex);

            //// Evitamos que se lanzen errores al no poder acceder a las imagenes en el editor fckeditor.
            ////if (Server.GetLastError().TargetSite.Name != "CheckVirtualFileExists" && Server.GetLastError().TargetSite.Name != "GetFileInfo")
            ////{
            //    // No enviamos correo cuando se realizen ataques en los formularios con código html
            //    //if (!(Server.GetLastError() is HttpRequestValidationException))
            //        FSMail.Mail.SendErrorMail(ex);
            ////}
        }


        public void Application_Start(object sender, EventArgs e)
        {
            Application["ApplicationCounter"] = 0;
            Application["NumberOfUsers"] = 0;
            Application["ServerStartTime"] = System.DateTime.Now;
        }


        public void Application_End(object sender, EventArgs e)
        {
        }


        public void Session_Start(object sender, EventArgs e)
        {
            Log.TraceInfo(
                "******************************************** SESSION_START ***************************************************");

            Application.Lock();
            Application["NumberOfUsers"] = (int)Application["NumberOfUsers"] + 1;
            Application.UnLock();
        }


        public void Session_End(object sender, EventArgs e)
        {
            Log.TraceInfo(
                "******************************************** SESSION_END ***************************************************");

            Application.Lock();
            if((int)Application["NumberOfUsers"] > 0)
                Application["NumberOfUsers"] = (int)Application["NumberOfUsers"] - 1;
            Application.UnLock();
        }


        public void Application_BeginRequest(object sender, EventArgs e)
        {
            Application.Lock();
            Application["ApplicationCounter"] = (int)Application["ApplicationCounter"] + 1;
            Application.UnLock();


            //permite incluir la aplicación en un frame OJO!!!!
            HttpContext.Current.Response.AddHeader("p3p", "CP=\"CAO PSA OUR\"");

            if ((Request.Path.IndexOf(Convert.ToChar(92)) >= 0 |
                 Path.GetFullPath(Request.PhysicalPath) != Request.PhysicalPath))
            {
                throw new HttpException(404, "No encontrado.");
            }

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["UrlRewrite"]))
            {
                Log.TraceInfo("Comienzo de UrlRewrite");
                FSPortal.UrlRewriter urlRewrite = new FSPortal.UrlRewriter();
                urlRewrite.Process();
                Log.TraceInfo("Fin de UrlRewrite");
            }
        }


        //private void Application_EndRequest(object source, EventArgs e)
        //{
        //}
    }
}