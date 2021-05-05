// // <fileheader>
// // <copyright file="URLRewriter.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSLibrary
// //     Solution: FSLibraryNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using FSLibrary;

#endregion

namespace FSPortal
{
    public class UrlRewriter : IConfigurationSectionHandler
    {
        private XmlNode mORules;

        private string GetSubstitution(string zPath)
        {
            foreach (XmlNode oNode in mORules.SelectNodes("rule"))
            {
                Regex oReg = new Regex(oNode.SelectSingleNode("url/text()").Value);
                Match oMatch = oReg.Match(zPath);

                if ((oMatch.Success))
                {
                    return oReg.Replace(zPath, oNode.SelectSingleNode("rewrite/text()").Value);
                }
            }

            return zPath;
        }

        public void Process()
        {
            string path = HttpContext.Current.Request.Path;
            string queryString = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString.ToString());
            string webHttp = "http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"];

            if (TextUtil.IndexOf(queryString, ";") > 0)
            {
                path = queryString.Split(';')[1];
                path = path.Replace(webHttp, "");

                queryString = "";
            }

            Process(path, queryString, true);
        }


        private void Process(string sUrl, string queryString, bool processquery)
        {
            UrlRewriter oRewriter = ((UrlRewriter) (ConfigurationManager.GetSection("urlrewrites")));
            string zSubst = oRewriter.GetSubstitution(sUrl);
            string url = zSubst;
            string c = "?";

            if (url.IndexOf("?") > 0)
            {
                c = "&";
            }

            if (processquery)
            {
                if (queryString != "")
                {
                    url = url + c + queryString;
                }
            }

            if ((zSubst.Length > 0))
            {
                HttpContext.Current.RewritePath(url);
            }
        }

        #region '"Implementation of IConfigurationSectionHandler"'

        // interface methods implemented by Create
        object IConfigurationSectionHandler.Create(object parent, object configContext, XmlNode section)
        {
            return Create(section);
        }

        private object Create(XmlNode section)
        {
            mORules = section;
            return this;
        }

        #endregion
    }
}