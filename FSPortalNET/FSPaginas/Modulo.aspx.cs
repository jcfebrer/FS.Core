// <fileheader>
// <copyright file="modulo.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: modulo.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using FSPortal;
using FSLibrary;
using FSNetwork;

namespace FSPaginas
{
    public class Modulo : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            Modulos modulos = new Modulos();
            return modulos.MuestraModulo(Web.RequestInt("id"), true);
        }
    }
}