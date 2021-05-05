// <fileheader>
// <copyright file="detalleLinea.ascx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: tienda\detalleLinea.ascx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System.Data;
using System.Web.UI;

namespace FSTienda
{
    public class DetalleLinea : UserControl
    {
        public DataTable dt;
        public bool esPedido;
        public double importeTotal;
        public double precioPortes;
    }
}