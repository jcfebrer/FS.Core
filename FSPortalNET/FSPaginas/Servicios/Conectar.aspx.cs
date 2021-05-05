// <fileheader>
// <copyright file="conectar.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: servicios\conectar.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Web.UI.WebControls;
using FSPortal;
using FSLibrary;
using FSNetwork;

namespace FSPaginas.Servicios
{
    public class Conectar : BasePage
    {
        protected Button cmdLogin;
        protected Label sMessage;
        public string sReferer = "";
        protected TextBox txtPassword;
        protected TextBox txtUsuario;

        protected void cmdLogin_Click(object sender, EventArgs e)
        {
            Login(txtUsuario.Text, txtPassword.Text);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Web.Request("comebackto") != "")
            {
                sReferer = Web.Request("comebackto");
            }

            if (!IsPostBack)
            {
                if (Web.Request("txtUsuario") != "")
                {
                    Login(Web.Request("txtUsuario"), Web.Request("txtPassword"));
                }
            }
            cmdLogin.Click += cmdLogin_Click;
        }


        public void Login(string sUsuario, string sPassword)
        {
            Portal portal = new Portal();

            string ret = "";
            try
            {
                portal.Login(sUsuario, sPassword);
            }
            catch (System.Exception e)
            {
                ret = e.Message;
            }

            if (ret != "")
            {
                sMessage.Text = ret;
            }
            else
            {
                if (Variables.User.paginaInicio != 0)
                {
                    sReferer = Variables.App.directorioPortal + "page" + Variables.User.paginaInicio + ".aspx";
                }

                if (sReferer == "")
                {
                    Response.Redirect(Variables.App.directorioPortal + "default.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    Response.Redirect(Server.HtmlEncode(sReferer), false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }
    }
}