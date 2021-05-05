// <fileheader>
// <copyright file="FSWebControl1.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: FSWebControl.cs
//     Copyright (c) 2003-2007 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Collections;
using System.Diagnostics;

namespace FSPortalWCLibrary
{
    [DefaultProperty("Text"), ToolboxData("<{0}:WebCustomControl1 runat=server></{0}:WebCustomControl1>")]
    public class FSWebControl : WebControl
    {

        [Bindable(true), Category("Appearance"), DefaultValue(""), Localizable(true)]
        public string Text
        {
            get
            {
                string s = ViewState["Text"].ToString();
                if (s == null)
                {
                    return string.Empty;
                }
                else
                {
                    return s;
                }
            }

            set
            {
                ViewState["Text"] = value;
            }
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(Text);
        }
    }
}
