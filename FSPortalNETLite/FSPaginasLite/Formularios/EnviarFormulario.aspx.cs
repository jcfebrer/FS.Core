// <fileheader>
// <copyright file="EnviarFormulario.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: formularios\EnviarFormulario.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using FSPortalLite;

namespace FSPaginasLite.Formularios
{
    /// <summary>
    ///     Clase para enviar un formulario
    /// </summary>
    public class EnviarFormulario : BasePage
    {
        /// <summary>
        ///     Carga de la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            Portal portal = new Portal();

            string msg = Variables.User.frmMensajeOK;

            try
            {
                VariablesUsuario.FormMod m = Variables.User.frmModo;
                string mo = Request.QueryString["modo"];
                if (mo != "")
                {
                    switch (mo)
                    {
                        case "login":
                            m = VariablesUsuario.FormMod.Login;
                            break;
                    }
                }

                switch (m)
                {
                    case VariablesUsuario.FormMod.Login:

                        portal.Login(Request.Form["txtUsuario"], Request.Form["txtClave"]);
                        msg = "";

                        Variables.User.frmEmailTo = "";
                        break;
                }

                return @"{ ""message"": """ + Funciones.formatJSON(msg) + @""" }";
            }
            catch (System.Exception e)
            {
                return @"{ ""message"": """ + Variables.User.frmMensajeNoOK + "<br />" + Funciones.formatJSON(e.Message) + @""" }";
            }
        }
    }
}