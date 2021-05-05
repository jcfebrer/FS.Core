// // <fileheader>
// // <copyright file="HtmlUrlRewriter.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSPortal
// //     Solution: FSPortalNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System;
using System.Globalization;
using System.Web;

#endregion

namespace FSPortal
{
    public class HtmlUrlRewriter : IHttpModule
    {
        private HttpApplication contx;

        private void context_BeginRequest(object sender, EventArgs e)
        {
            string path = contx.Request.Path;
            if (path.EndsWith("html", true, CultureInfo.CurrentCulture))
                contx.Server.Transfer(RewriteUri(contx.Request.Url).PathAndQuery);
        }

        public static Uri RewriteUri(Uri uri)
        {
            UriBuilder newUri = new UriBuilder(uri);
            newUri.Path = newUri.Path.Replace(".html", ".aspx");

            return newUri.Uri;
        }

        #region IHttpModule Members

        public void Init(HttpApplication context)
        {
            contx = context;
            context.BeginRequest += context_BeginRequest;
        }

        public void Dispose()
        {
        }

        #endregion
    }
}