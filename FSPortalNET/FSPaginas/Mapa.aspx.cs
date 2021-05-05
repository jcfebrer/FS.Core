// <fileheader>
// <copyright file="mapa.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: mapa.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using FSPortal;

namespace FSPaginas
{
    public class Mapa : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            Modulos modulos = new Modulos();
            return modulos.ModMapa(0);
        }
    }
}