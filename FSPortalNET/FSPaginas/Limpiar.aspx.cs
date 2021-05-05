// <fileheader>
// <copyright file="limpiar.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: limpiar.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using FSPortal;
using FSLibrary;

namespace FSPaginas
{
    public class Limpiar : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            Portal portal = new Portal();
            string dir = Variables.App.directorioPortal + "default.aspx?portal=" + Functions.Valor(Variables.App.portal);

            portal.Limpiar();

            Response.Redirect(dir, false);
            Context.ApplicationInstance.CompleteRequest();

            return dir;
        }
    }
}