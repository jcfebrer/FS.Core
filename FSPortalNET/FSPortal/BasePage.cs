// // <fileheader>
// // <copyright file="BasePage.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSPortal
// //     Solution: FSPortalNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>
using FSPdf;
using FSQueryBuilder;
using FSQueryBuilder.Enums;
using FSQueryBuilder.QueryParts.Where;

#region

using System;
using System.Collections.Specialized;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.UI;
using FSLibrary;
using FSDatabase;
using FSNetwork;
using FSException;
using FSCrypto;

#endregion

namespace FSPortal
{
	public class BasePage : Page
	{
		//variables para páginas
		public bool comentarios = false;
		public string contenido = "";
		public string contenidoMovil = "";
		public string enlace = "";
		public bool hasContent = false;
		public bool hasContentMobile = false;
		public bool noContar = false;
		public int pageId = 0;
		public int paginaId = 0;
		public string paginaP = "";
		public bool requiereLogin = false;
		public bool soloAdmin = false;
		public long startTime = 0;
		public string tagsPagina = "";
		public string tituloPagina = "";
		public string userInfo = "";
		public int accesosPagina = 0;
		public bool checkUrl = false;
		public string plantilla = "";
		public string plantillaContent = "";
		public bool simple = false;
		public bool textEditor = false;

		private bool render = true;

		public string ScriptName {
			get { return HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"]; }
		}

		protected override void Render(HtmlTextWriter writer)
		{
            string editPage = "";

			if (!render)
				return;
			
			string page = "";

			if (Request.Form.Count > 0 && Variables.Parser.frmCampos != null) {
				try {
					FSDatabase.Utils.CheckData(Request, Variables.Parser.frmCampos, Variables.Parser.frmTruncar);
				} catch (System.Exception ex) {
					page = @"{ ""message"": """ + Web.formatJSON(Variables.Parser.frmMensajeNoOK + " " + ex.Message) + @""" }";
				}
			}


			if (page == "") {
				
                if (Variables.User.Administrador)
                {
					editPage += Ui.EditAll();
                }

                if (plantillaContent=="")plantillaContent = contenido;
				
				page += plantillaContent;

				if (contenido.Contains("{simple}")) {
					contenido = TextUtil.Replace(contenido, "{simple}", "");
					simple = true;
				}

				if ((simple ||
					TextUtil.IndexOf(ScriptName, "EnviarFormulario.aspx") > 0 ||
					TextUtil.IndexOf(ScriptName, "savedata.aspx") > 0 ||
					TextUtil.IndexOf(ScriptName, "loaddata.aspx") > 0
					))
				{
					//mostramos solo el contenido
					if (simple)
						page = Ui.EditPage("Paginas", "idPagina", pageId.ToString()) + contenido;
					else
						page = contenido;
				}
				else
				{
					page = TextUtil.Replace(page, "{contenido}", contenido);
					page = TextUtil.Replace(page, "{contenido()}", contenido);
					page = TextUtil.Replace(page, "{frmcontenido()}", contenido);
					page = TextUtil.Replace(page, "{paginaid}", Variables.App.Page.paginaId.ToString());
					page = TextUtil.Replace(page, "{frmtitulopagina}", Variables.App.Page.tituloPagina);

					if (TextUtil.IndexOf(page, "{frmeditarpagina}") == -1)
						page = editPage + page;
					else
						page = TextUtil.Replace(page, "{frmeditarpagina}", editPage);
				}


				if (TextUtil.IndexOf(ScriptName, "loaddata.aspx") == -1)
				{
					//inicamos las variables utilizadas en los formularios.
					Parser.IniciarFormulario();
				}

				//page = TextUtil.Replace(page, "#frmTituloPagina#", Variables.App.Page.tituloPagina);
			}

			page = Parser.Procesa(page);

			// si el parámetro "view" es pdf, generamos un fichero pdf con el contenido.
			if (HttpContext.Current.Request.QueryString["view"] == "pdf")
			{

				CreatePDF.Generate(Variables.App.Page.Context, GetPdfData(page), Variables.Parser.frmRotatePdf);
				Variables.Parser.frmRotatePdf = false;
			}

			// si el parámetro "view" es clean, mostramos el contenido como un pdf, pero sin generar.
			if (HttpContext.Current.Request.QueryString["view"] == "clean")
				page = GetPdfData(page);

			writer.Write(page);
		}

		private string GetPdfData(string html)
		{
			string divPdfData = TextUtil.GetDivHtml(html, "pdfdata");

			if (divPdfData != "") {
				string pdfData = "<html><head>";
				//pdfData += Functions.GetHeadHtml(html);
				pdfData += "</head><body>";
				pdfData += divPdfData;
				pdfData += "</body>";
				pdfData += "</html>";

				pdfData = TextUtil.RemoveStyles(pdfData);
				pdfData = TextUtil.RemoveLinks(pdfData);
				pdfData = TextUtil.SetTableBorder(pdfData, true);
				pdfData = TextUtil.ChangeTH_TD(pdfData);

				return pdfData;
			}
			return html;
		}

		protected override void OnInit(EventArgs e)
		{
			try {

				Variables.App.Page = this;

				Portal portal = new Portal();
				//portal.LoadVariables();

                simple = Web.RequestBool("simple");
				textEditor = Web.RequestBool("texteditor");

				// con la variable "portal" podemos indicar el sitio que deseamos cargar.
				if (HttpContext.Current.Request.QueryString["portal"] != null || HttpContext.Current.Request.QueryString["mobile"] != null || HttpContext.Current.Request.QueryString["movil"] != null) {
					FuncionesWeb.ClearVariables();
					Variables.App.paginaLogin = ""; // forzamos la lectura de las variables
				}

				if (LoadPageVariables()) {
					
					CheckUrl();
									
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
				throw new ExceptionUtil(ex);
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			if (render) {
			
				if (FuncionesWeb.IsMobileBrowser()) {
					if (!hasContentMobile)
						base.OnLoad(e);
				} else {
					if (!hasContent)
						base.OnLoad(e);
				}
			}
		}
		
		
		private void CheckUrl()
		{
			if (Request.QueryString.ToString() != "" && checkUrl) {
				if (Functions.Valor(Request.QueryString["check"]) != "") {
					
					String check = HttpUtility.UrlDecode(Request.QueryString["check"]).Replace("%2b", "+");

					// hacemos la colección editable
					PropertyInfo isreadonly = typeof(NameValueCollection).GetProperty("IsReadOnly",
						                           BindingFlags.Instance | BindingFlags.NonPublic);
					isreadonly.SetValue(HttpContext.Current.Request.QueryString, false, null);
					//System.Web.HttpContext.Current.Request.QueryString.Add("check", Functions.MD5(System.Web.HttpContext.Current.Request.QueryString.ToString()));
					Request.QueryString.Remove("check");

					if (!Web.CheckQueryString(Request.QueryString.ToString(), check, Crypto.Password)) {
						throw new ExceptionUtil("Validación incorrecta de URL");
					}

				} else {
					throw new ExceptionUtil(@"Falta parámetro de validación. Parámetros: " +
					Request.QueryString);
				}
			}
		}
			

		/// <summary>
		///     Eliminamos los valores secundarios separados por comas
		///     Por ejemplo: "uno,dos,tres" -> "uno"
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private string GetFirstValue(string value)
		{
			//value = value.Replace("{coma}", ",");
			string[] values = value.Split(',');
			return values[0];
		}


		/// <summary>
		///     Carga de las variables de la página
		/// </summary>
		/// <returns></returns>
		private Boolean LoadPageVariables()
		{
			try {
				////Comprobamos que todas las conexiones son correctas
				//BdUtils.CheckConnection();

				startTime = System.DateTime.Now.Ticks; // para medir el tiempo de carga de la página

				paginaId = Web.RequestInt("id"); // por código de página
				paginaP = Web.Request("p"); // por título de la página
				paginaP = paginaP.Replace("_", " ");

				// si el parámetro 'p' esta duplicado, solo cojemos el primero.
				paginaP = GetFirstValue(paginaP);

				Variables.User.ComeBack = ScriptName;
				Variables.User.ComeBack2 = Variables.User.ComeBack;

				if (Request.QueryString.ToString() != "") {
					Variables.User.ComeBack += "?" + Server.UrlEncode(Request.QueryString.ToString());
					Variables.User.ComeBack2 += "?" + Request.QueryString;
				}

				if (Request.QueryString["comebackto"] != "") {
					Variables.User.ComeBackTo = Web.Request("comebackto");
				}

				if (ScriptName != null) {
					string pag;

					if (Variables.App.directorioPortal != "/")
						pag = TextUtil.Replace(ScriptName.ToLower(), Variables.App.directorioPortal, "").ToLower();
					else {
						pag = TextUtil.Substring(ScriptName, 0, 1) == "/" ? TextUtil.Substring(ScriptName, ScriptName.Length - (TextUtil.Length(ScriptName) - 1)) : ScriptName.ToLower();
					}

					pag = HttpUtility.HtmlDecode(pag);

					
					string[] columnas;

					if(!Variables.App.modoLite)
					{
						columnas = new string[] { "idPagina", "checkURL", "comentarios", "noContar", "Titulo", "tags", "soloAdmin", "contenido", "contenidoMovil", "accesos", "requiereLogin", "enlace", "plantilla" };
					}
					else
					{
						columnas = new string[] { "idPagina", "Titulo", "soloAdmin", "contenido", "requiereLogin", "enlace", "plantilla"};
					}

					DataTable dtTabla;

					if (Variables.App.UseXML)
					{
						XML xml = new XML(Variables.App.directorioWeb + "data");
						xml.Load("paginas.xml");

						if (pag == "pagina.aspx")
						{
							if (!String.IsNullOrEmpty(paginaP))
								dtTabla = xml.Select("titulo='" + HttpUtility.HtmlDecode(paginaP) + "'");
							else
								dtTabla = xml.Select("idPagina=" + paginaId);
						}
						else
							dtTabla = xml.Select("enlace='" + pag + "'");
					}
					else
					{
						SelectQueryBuilder sqB = new SelectQueryBuilder();
						sqB.Columns.SelectColumns(columnas);
						sqB.TableSource = Variables.App.prefijoTablas + "Paginas";

						if (pag == "pagina.aspx")
						{
							if (!String.IsNullOrEmpty(paginaP))
								sqB.Where = new SimpleWhere(sqB.TableSource, "titulo", Comparison.Equals, HttpUtility.HtmlDecode(paginaP));
							else
								sqB.Where = new SimpleWhere(sqB.TableSource, "idPagina", Comparison.Equals, paginaId);
						}
						else
						{
							SimpleWhere sw1 = new SimpleWhere(sqB.TableSource, "enlace", Comparison.Equals, pag);
							SimpleWhere sw2 = new SimpleWhere(sqB.TableSource, "enlace", Comparison.Equals, TextUtil.Replace(pag, "aspx", "asp"));
							OrWhere orw = new OrWhere();
							orw.Add(sw1);
							orw.Add(sw2);
							sqB.Where = orw;
						}

						BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
						dtTabla = db.Execute(sqB.BuildQuery());
					}

					if (dtTabla == null || dtTabla.Rows.Count == 0) {
                        if (!Variables.App.MostrarPaginasNoRegistradas)
                        {
                            comentarios = false;
                            noContar = false;
                            tituloPagina = "";
                            soloAdmin = false;
                            requiereLogin = false;

                            string sMsg = "Página no registrada. Página: ";
                            if (paginaP != "")
                                sMsg += paginaP;
                            else
                                sMsg += pag;

                            if (Variables.User.Administrador)
                            {
                                if (paginaP != "")
                                    sMsg += Ui.Lf() + @"<img border=""0"" src='" + Variables.App.directorioPortal +
                                    @"imagenes/bullet.gif' alt='' /> <a class=""_editbox"" href='" +
                                    Variables.App.directorioPortal + "admin/editor/showrecord.aspx?simple=0&amp;tablename=" +
                                    Variables.App.prefijoTablas + "Paginas&amp;add=1&amp;page=1&autoSel=" + paginaP +
                                    "&autoSelField=titulo'>Añadir Página</a>" + Ui.Lf() + "\r\n";
                                else
                                    sMsg += Ui.Lf() + @"<img border=""0"" src='" + Variables.App.directorioPortal +
                                    @"imagenes/bullet.gif' alt='' /> <a class=""_editbox"" href='" +
                                    Variables.App.directorioPortal + "admin/editor/showrecord.aspx?simple=0&amp;tablename=" +
                                    Variables.App.prefijoTablas + "Paginas&amp;add=1&amp;page=1&autoSel=" + pag +
                                    "&autoSelField=enlace'>Añadir Página</a>" + Ui.Lf() + "\r\n";
                            }

                            sMsg += Ui.Lf() + Ui.Lf() + Ui.Link("Volver a inicio", Variables.App.directorioPortal);

                            contenido = sMsg;
                            hasContent = true;
                        }
						
					} else {
						pageId = NumberUtils.NumberInt(dtTabla.Rows[0]["idPagina"]);
						enlace = Functions.Valor(dtTabla.Rows[0]["enlace"].ToString());
						tituloPagina = Functions.Valor(dtTabla.Rows[0]["Titulo"]);
						soloAdmin = Functions.ValorBool(dtTabla.Rows[0]["soloAdmin"]);
						requiereLogin = Functions.ValorBool(dtTabla.Rows[0]["requiereLogin"]);
						plantilla = Functions.Valor(dtTabla.Rows[0]["plantilla"]);

						if (!Variables.App.modoLite)
						{
							noContar = Functions.ValorBool(dtTabla.Rows[0]["noContar"]);
							comentarios = Functions.ValorBool(dtTabla.Rows[0]["comentarios"]);
							tagsPagina = Functions.Valor(dtTabla.Rows[0]["tags"].ToString());
							checkUrl = Functions.ValorBool(dtTabla.Rows[0]["checkURL"]);
							contenidoMovil = Functions.Valor(dtTabla.Rows[0]["contenidoMovil"]);
							accesosPagina = NumberUtils.NumberInt(dtTabla.Rows[0]["accesos"]);
						}

						if (plantilla == "")
							plantilla = "DEFECTO";

						//contenido = @"<div class=""pagina"" id=""P:" + pageId + @""">";
						//contenido += Ui.EditPage("Plantillas", "plantilla", plantilla, "Editar plantilla");
						//contenido += Ui.EditPage("Paginas", "idPagina", Functions.Valor(dtTabla.Rows[0]["idPagina"]),
						//	"Editar página", "Borrar página");
						
						plantillaContent = FuncionesWeb.ObtenerPlantilla(plantilla);

						if (Functions.Valor(dtTabla.Rows[0]["contenido"]) != "")
							hasContent = true;
						if (contenidoMovil != "")
							hasContentMobile = true;

						if (FuncionesWeb.IsMobileBrowser()) {
							if (hasContentMobile)
								contenido += contenidoMovil;
							else
								contenido += Functions.Valor(dtTabla.Rows[0]["contenido"]);
						} else
							contenido += Functions.Valor(dtTabla.Rows[0]["contenido"]);

						//contenido += "</div>";

//						if (requiereLogin && Variables.User.Usuario == "") {
//							contenido = "Debes conectarte al portal para acceder a esta página." + Ui.Lf() + Ui.Lf();
//							contenido += "Pulsa <a href='" + Variables.App.paginaLogin +
//							(Variables.App.paginaLogin.IndexOf("?") > 0 ? "&" : "?") + "comebackto=" + Variables.User.ComeBack2 +
//							"'>aquí</a> para conectar.";
//							
//							hasContent = true;
//						}

//						if (soloAdmin && Variables.User.Administrador == false) {
//							contenido =
//                            "El acceso a esta página, esta restringido a usuarios con derechos de administrador." +
//							Ui.Lf() + Ui.Lf();
//							contenido += "Pulsa <a href='" + Variables.App.paginaLogin +
//							(Variables.App.paginaLogin.IndexOf("?") > 0 ? "&" : "?") + "comebackto=" + Variables.User.ComeBack2 +
//							"'>aquí</a> para conectar.";
//							
//							hasContent = true;
//						}

						if (Variables.App.ModoPrueba)
							tituloPagina += "/********** MODO PRUEBA **********/";
					}
				}

				string loginPage = Variables.App.paginaLogin + (Variables.App.paginaLogin.IndexOf("?") > 0 ? "&" : "?");

				if (Web.RequestInt("simple") == 1)
					loginPage += "simple=1&";

				loginPage += "comebackto=" + HttpUtility.UrlEncode(Variables.User.ComeBack);


				if (!Variables.App.modoLite)
				{
					UpdateQueryBuilder uqb = new UpdateQueryBuilder();
					uqb.TableSource = Variables.App.prefijoTablas + "Paginas";

					if (accesosPagina == 0)
					{
						uqb.Assignments.AddAssignment("accesos", 1);
						SimpleWhere swa1 = new SimpleWhere(uqb.TableSource, "idPagina", Comparison.Equals, pageId);
						SimpleWhere swa2 = new SimpleWhere(uqb.TableSource, "accesos", Comparison.Is, null);
						AndWhere aw = new AndWhere();
						aw.Add(swa1);
						aw.Add(swa2);
						uqb.Where = aw;
					}
					else
					{
						uqb.Assignments.AddAssignment("accesos", new SqlLiteral("accesos+1"));
						uqb.Where = new SimpleWhere(uqb.TableSource, "idPagina", Comparison.Equals, pageId);
					}

					BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
					db.ExecuteNonQuery(uqb.BuildQuery());
				}


                if (requiereLogin && Variables.User.Usuario == "")
                {
                    Response.Redirect(loginPage, false);
                    Context.ApplicationInstance.CompleteRequest();
                    return false;
                }
                if (soloAdmin && Variables.User.Administrador == false)
                {
                    Response.Redirect(loginPage, false);
                    Context.ApplicationInstance.CompleteRequest();
                    return false;
                }


                return true;
				
			} catch (System.Exception ex) {
				throw new ExceptionUtil(ex);
			}
		}
	}
}