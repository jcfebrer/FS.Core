// <fileheader>
// <copyright file="mark_posts_as_read.aspx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\mark_posts_as_read.aspx.cs
//     Copyright (c) 2003-2007 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>
using FSPortal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace FSForum
{
    public class mark_posts_as_read : FSPortal.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            contenido = Inicio();
        }
        public static string Inicio()
        {
            FSPortal.Variables.App.Page.Response.Buffer = true;
            FSPortal.Variables.App.Page.Response.Cookies[FSPortal.Variables.App.strCookieName]["LTVST"] = DateTime.Now.ToOADate().ToString();
            FSPortal.Variables.App.Page.Response.Cookies["LTVST"].Expires = DateTime.Now.AddYears(1);
            // Reset the session variable holding the users last visit to the forum to now
            FSPortal.Variables.App.Page.Session["dtmLastVisit"] = DateTime.Now;
            // Return to the forum
            FSPortal.Variables.App.Page.Response.Redirect("default.aspx");

            return "";
        }
    }
}