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
using FSPortal;
using FSLibrary;
using FSNetwork;
using FSMail;

namespace FSPaginas.ErrorPages
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

			sb.Append(Ui.Lf() + Ui.Lf());
			sb.Append("<b>Lamentamos las molestias.</b>" + Ui.Lf());
			sb.Append(Ui.Lf());
			sb.Append("Se ha producido un problema temporal en la página que desea acceder." + Ui.Lf());
			sb.Append(Ui.Lf() +
			"");
			sb.Append(Ui.Lf() + Ui.Lf());
			sb.Append(Ui.Link("Volver a la página de inicio.", Variables.App.directorioPortal));

			try {
				System.Exception e = Server.GetLastError();
				if (e != null) {
					if (Variables.User.Administrador) {
						sb.Append("Error: " + e);
					}
					SendMail.SendErrorMail("", e);

					Server.ClearError();
				}
			} catch {
			}

			return sb.ToString();
		}
	}
}