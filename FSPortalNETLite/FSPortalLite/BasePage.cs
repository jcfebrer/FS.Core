// // <fileheader>
// // <copyright file="BasePage.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSPortal
// //     Solution: FSPortalNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System;
using System.Collections.Specialized;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.UI;

#endregion

namespace FSPortalLite
{
	public class BasePage : Page
	{
		//variables para páginas
		public string contenido = "";
		public string enlace = "";
		public bool hasContent = false;
		public bool noContar = false;
		public int pageId = 0;
		public int paginaId = 0;
		public string paginaP = "";
		public bool requiereLogin = false;
		public bool soloAdmin = false;
		public string tagsPagina = "";
		public string tituloPagina = "";
		public string userInfo = "";
		public string plantilla = "";
		public string plantillaContent = "";

		private bool render = true;

		public string ScriptName {
			get { return HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"]; }
		}

		protected override void Render(HtmlTextWriter writer)
		{
			if (!render)
				return;

			if (plantillaContent == "") plantillaContent = contenido;

			string page = plantillaContent;

			if ((ScriptName.IndexOf("enviarFormulario.aspx") > 0))
			{
				//mostramos solo el contenido
				page = contenido;
			}

			page = page.Replace("{contenido}", contenido);
			page = page.Replace("{contenido()}", contenido);
			page = page.Replace("{frmcontenido()}", contenido);
			page = page.Replace("{Contenido}", contenido);
			page = page.Replace("{Contenido()}", contenido);
			page = page.Replace("{frmContenido()}", contenido);
			page = page.Replace("{paginaid}", Variables.App.Page.paginaId.ToString());
            page = page.Replace("{frmtitulopagina}", Variables.App.Page.tituloPagina);
            page = page.Replace("{frmeditarpagina}", "");

            //inicamos las variables utilizadas en los formularios.
            Parser.IniciarFormulario();

			//sustituimos las funciones
			page = Parser.Procesa(page);

			page = page.Replace("#frmTituloPagina#", Parser.Procesa(Variables.App.Page.tituloPagina));

			writer.Write(page);
		}

		protected override void OnInit(EventArgs e)
		{
			try {

				Variables.App.Page = this;

				Portal portal = new Portal();
				portal.LoadVariables();

				// con la variable "portal" podemos indicar el sitio que deseamos cargar.
				if (HttpContext.Current.Request.QueryString["portal"] != null || HttpContext.Current.Request.QueryString["mobile"] != null || HttpContext.Current.Request.QueryString["movil"] != null) {
					FuncionesWeb.ClearVariables();
					Variables.App.paginaLogin = ""; // forzamos la lectura de las variables
				}

				if (LoadPageVariables()) {
									
					Response.Buffer = true;
	
					// evitamos que se cachee
					Response.Expires = -1;
					Response.ExpiresAbsolute = new System.DateTime(1980, 1, 1, 0, 0, 0);
					Response.AddHeader("pragma", "no-cache");
					Response.AddHeader("cache-control", "private, no-cache, must-revalidate");
					Response.CacheControl = "Private";
				} else {
					render = false;
				}
				
				base.OnInit(e);
				
			} catch (System.Exception ex) {
				throw new Exception("Error", ex);
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			if (render)
			{
				if (!hasContent)
					base.OnLoad(e);
			}
		}


		/// <summary>
		///     Carga de las variables de la página
		/// </summary>
		/// <returns></returns>
		private Boolean LoadPageVariables()
		{
			try {
                string value = Request.QueryString["id"];
                if (value != null)
                {
                    if (value.Contains(","))
                    {
                        value = value.Split(',')[0];
                    }
                    paginaId = Convert.ToInt32(value); // por código de página
                }
				paginaP = Request.QueryString["p"]; // por título de la página
				if(paginaP != null)
					paginaP = paginaP.Replace("_", " ");

				Variables.User.ComeBack = ScriptName;

				if (Request.QueryString.ToString() != "") {
					Variables.User.ComeBack += "?" + Request.QueryString.ToString();
				}

				if (ScriptName != null) {
					string pag;

					if (Variables.App.directorioPortal != "/")
						pag = ScriptName.ToLower().Replace(Variables.App.directorioPortal, "").ToLower();
					else {
						pag = ScriptName.Substring(0, 1) == "/" ? ScriptName.Substring(ScriptName.Length - (ScriptName.Length - 1)) : ScriptName.ToLower();
					}

					pag = HttpUtility.HtmlDecode(pag);

					DataRow[] rowResult;

					if (pag == "pagina.aspx")
					{
						if (!String.IsNullOrEmpty(paginaP))
							rowResult = Funciones.XMLSelect("paginas", "titulo='" + HttpUtility.HtmlDecode(paginaP) + "'");
						else
							rowResult = Funciones.XMLSelect("paginas", "idPagina=" + paginaId);
					}
					else
						rowResult = Funciones.XMLSelect("paginas", "enlace='" + pag + "'");

					if (rowResult.Length != 0)
					{
						enlace = rowResult[0]["enlace"].ToString();
						tituloPagina = rowResult[0]["Titulo"].ToString();
						soloAdmin = Convert.ToBoolean(rowResult[0]["soloAdmin"]);
						requiereLogin = Convert.ToBoolean(rowResult[0]["requiereLogin"]);
						plantilla = rowResult[0]["plantilla"].ToString();
						
						pageId = Convert.ToInt32(rowResult[0]["idPagina"]);

						if (plantilla == "")
							plantilla = "DEFECTO";

						//contenido = @"<div class=""pagina"" id=""P:" + pageId + @""">";
						//contenido += Ui.EditPage("Plantillas", "plantilla", plantilla, "Editar plantilla");
						//contenido += Ui.EditPage("Paginas", "idPagina", Functions.Valor(dtTabla.Rows[0]["idPagina"]),
						//	"Editar página", "Borrar página");

						plantillaContent = FuncionesWeb.ObtenerPlantilla(plantilla);

						if (rowResult[0]["contenido"].ToString() != "")
							hasContent = true;

						contenido += rowResult[0]["contenido"].ToString();
					}
					//else
					//{
					//	throw new Exception("Página no encontrada. Página: " + paginaP + " Enlace: " + pag + " Id: " + paginaId);
					//}
				}

                string loginPage = Variables.App.paginaLogin + (Variables.App.paginaLogin.IndexOf("?") > 0 ? "&" : "?");

                loginPage += "comebackto=" + HttpUtility.UrlEncode(Variables.User.ComeBack);

				
				if (requiereLogin && String.IsNullOrEmpty(Variables.User.Usuario)) {
					Response.Redirect(loginPage, false);
					Context.ApplicationInstance.CompleteRequest();
					return false;
				}
				if (soloAdmin && Variables.User.Administrador == false) {
					Response.Redirect(loginPage, false);
					Context.ApplicationInstance.CompleteRequest();
					return false;
				}

				return true;
				
			} catch (System.Exception ex) {
				throw new Exception("Error", ex);
			}
		}
	}
}