// <fileheader>
// <copyright file="desconectar.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: servicios\desconectar.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Text;
using FSPortal;
using FSLibrary;

namespace FSPaginas.Servicios
{
    public class Desconectar : BasePage
    {

        protected void Page_Load(Object sender, EventArgs e)
        {
            Portal portal = new Portal();
            string dir = Variables.App.directorioPortal + "default.aspx?portal=" + Functions.Valor(Variables.App.portal);

            portal.Desconectar();
            Response.Redirect(dir, false);
            Context.ApplicationInstance.CompleteRequest();
        }

    }
}