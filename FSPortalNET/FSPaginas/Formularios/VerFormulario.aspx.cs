// <fileheader>
// <copyright file="verFormulario.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: formularios\verFormulario.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using FSPortal;
using FSLibrary;
using FSNetwork;

namespace FSPaginas.Formularios
{
	public class VerFormulario : BasePage
	{
		protected void Page_Load(Object sender, EventArgs e)
		{
			contenido = Inicio();
		}

		private string Inicio()
		{
			int id = Web.RequestInt("id");
			return FSPortal.Formularios.MuestraFormulario(id);
		}
	}
}