// <fileheader>
// <copyright file="comunicaciones.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: errorPages\500-100.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Text;
using FSPortalLite;

namespace FSPaginasLite.ErrorPages
{
	/// <summary>
	///     Manejador del error 500
	/// </summary>
	public class Error : BasePage
	{
		protected void Page_Load(Object sender, EventArgs e)
		{
			contenido = Inicio();
		}

		private string Inicio()
		{
			StringBuilder sb = new StringBuilder("");

			sb.Append("<br />");
			sb.Append("<b>Lamentamos las molestias.</b><br /><br />");
			sb.Append("Se ha producido un problema temporal en la página que desea acceder.<br />");
			sb.Append("<br /><br /><br />");
			sb.Append("<a href='" + Variables.App.directorioPortal + "'>Volver a la página de inicio.</a>");

			try {
				System.Exception e = Server.GetLastError();
				if (e != null) {
					if (Variables.User.Administrador) {
						sb.Append("Error: " + e);
					}

					Server.ClearError();
				}
			} catch {
			}

			return sb.ToString();
		}
	}
}