// // <fileheader>
// // <copyright file="HttpCompressionModule.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSLibrary
// //     Solution: FSLibraryNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System;
using System.Configuration;
using System.IO.Compression;
using System.Web;
using FSLibrary;
using FSException;

#endregion

namespace FSPortal
{
    public class HttpCompressionModule : IHttpModule
    {
        private const string Gzip = "gzip";
        private const string Deflate = "deflate";

        void IHttpModule.Dispose()
        {
            Dispose();
        }

        void IHttpModule.Init(HttpApplication context)
        {
            try
            {
                if (context != null)
                    Init(context);
            }
            catch(System.Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            if (Functions.ValorBool(ConfigurationManager.AppSettings["EnableHttpCompression"]))
            {
                context.BeginRequest += context_BeginRequest;
            }
        }

        public void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication app = ((HttpApplication) (sender));

            if ((CanCompressUrl(app.Request.Url.OriginalString.ToUpperInvariant())))
            {
                if ((IsEncodingAccepted(Gzip)))
                {
                    app.Response.Filter = new GZipStream(app.Response.Filter, CompressionMode.Compress);
                    SetEncoding(Gzip);
                }
                else if ((IsEncodingAccepted(Deflate)))
                {
                    app.Response.Filter = new DeflateStream(app.Response.Filter, CompressionMode.Compress);
                    SetEncoding(Deflate);
                }
            }
        }


        private bool CanCompressUrl(string url)
        {
            if (url.Contains("CONNECTOR.ASPX")) return false; // este fichero lo utiliza ckfinder para generar un xml
            return (url.Contains(".ASPX") || url.EndsWith(".CSS") || url.EndsWith(".JS"));
        }


        private bool IsEncodingAccepted(string encoding)
        {
            try
            {
                string s = HttpContext.Current.Request.Headers["Accept-encoding"];
                return (s != null && s.Contains(encoding));
            }
            catch
            {
                return false;
            }
        }


        private void SetEncoding(string encoding)
        {
            try
            {
                HttpContext.Current.Response.AppendHeader("Content-encoding", encoding);
            }
            catch(System.Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }
    }
}