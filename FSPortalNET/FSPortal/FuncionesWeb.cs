// // <fileheader>
// // <copyright file="FuncionesWeb.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSPortal
// //     Solution: FSPortalNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web;
using FSLibrary;
using FSQueryBuilder;
using FSQueryBuilder.Enums;
using FSQueryBuilder.QueryParts.Where;
using FSNetwork;
using FSDatabase;
using FSException;

#endregion

namespace FSPortal
{
	public static class FuncionesWeb
	{
		public static string Error(System.Exception e)
		{
			string s;

			if (Variables.App.ModoPrueba || Variables.User.Administrador)
				s = e.ToString();
			else
				s = e.Message;

			return s;
		}

		public static string CameBackTo()
		{
			if (Variables.User.ComeBack2.Contains("comebackto")) {
				return Variables.App.directorioPortal + "default.aspx";
			}
			return HttpUtility.UrlEncode(Variables.User.ComeBack2);
		}

		public static string Enlace(string lnk)
		{
			if ((TextUtil.Left(lnk, 4).ToLower() == "http") || ((TextUtil.Left(lnk, 3)).ToLower() == "www")) {
				return lnk;
			}
			return Variables.App.directorioPortal + lnk;
		}
			

		//public static void AddMetaContentType()
		//{

		//    System.Web.UI.HtmlControls.HtmlMeta meta = new System.Web.UI.HtmlControls.HtmlMeta();
		//    meta.HttpEquiv = "content-type";

		//    meta.Content = Variables.App.Page.Response.ContentType + "; charset=" + Variables.App.Page.Response.ContentEncoding.HeaderName;
		//    Variables.App.Page.Header.Controls.Add(meta);
		//}


		//public static void AddMetaTag(string name, string value, bool httpequiv)
		//{

		//    if ((string.IsNullOrEmpty(name) | string.IsNullOrEmpty(value)))
		//    {
		//        return;
		//    }

		//    System.Web.UI.HtmlControls.HtmlMeta meta = new System.Web.UI.HtmlControls.HtmlMeta();

		//    if (httpequiv)
		//    {
		//        meta.HttpEquiv = name;
		//    }
		//    else
		//    {
		//        meta.Name = name;
		//    }

		//    meta.Content = value;
		//    Variables.App.Page.Header.Controls.Add(meta);
		//}

		//public static void AddMetaTag(string name, string value)
		//{
		//    AddMetaTag(name, value, false);
		//}


		//public static void AddEmbeddedJavaScript(string name)
		//{

		//    System.Web.UI.HtmlControls.HtmlGenericControl script = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
		//    script.Attributes["type"] = "text/javascript";

		//    Type rstype = typeof(FSPortalWCLibrary.FSWebControl1);
		//    script.Attributes["src"] = Variables.App.Page.ClientScript.GetWebResourceUrl(rstype, name);
		//    Variables.App.Page.Header.Controls.Add(script);
		//}


		//public static void AddJavaScript(string file)
		//{

		//    System.Web.UI.HtmlControls.HtmlGenericControl script = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
		//    script.Attributes["type"] = "text/javascript";

		//    script.Attributes["src"] = file;
		//    Variables.App.Page.Header.Controls.Add(script);
		//}


		//public static void AddJavaStriptCode(string code)
		//{

		//    System.Web.UI.HtmlControls.HtmlGenericControl script = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
		//    script.Attributes["type"] = "text/javascript";

		//    script.InnerHtml = code;
		//    Variables.App.Page.Header.Controls.Add(script);
		//}

		//public static void AddLink(string relation, string type, string title, string href)
		//{

		//    System.Web.UI.HtmlControls.HtmlLink link = new System.Web.UI.HtmlControls.HtmlLink();
		//    link.Attributes["rel"] = relation;
		//    link.Attributes["title"] = title;
		//    link.Attributes["href"] = href;
		//    link.Attributes["type"] = type;
		//    Variables.App.Page.Header.Controls.Add(link);
		//}


		public static long NumeroArticulos(long cesta)
		{
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

			string ssql = "select unidades from " + Variables.App.prefijoTablas + "lineascesta," + Variables.App.prefijoTablas +
			                       "articulos where (lineasCesta.idArticulo=articulos.idArticulo) and idCabeceraCesta=" + cesta;

			DataTable dtArticulos = db.Execute(ssql);

			long numeroArticulosReturn = 0;

			foreach (DataRow row in dtArticulos.Rows) {
				numeroArticulosReturn += Convert.ToInt64(row["unidades"]);
			}

			return numeroArticulosReturn;
		}


		public static long TotalComentarios(int idPagina)
		{
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
			string ssql = "select count(*) from " + Variables.App.prefijoTablas + "comentarios where idPagina=" + idPagina;
			int i = NumberUtils.NumberInt(db.ExecuteScalar(ssql));

			return i;
		}


		public static long TotalUnidades(long articulo, int mes)
		{
			long totalUnidadesReturn;
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
			string ssql;

			if (mes == 0) {
				ssql = "select sum(sumadeunidades) as sumadeunidades from " + Variables.App.prefijoTablas +
				"v_InformeVentas where idArticulo=" + articulo;
			} else {
				ssql = "select sumadeunidades from " + Variables.App.prefijoTablas + "v_InformeVentas where idArticulo=" +
				articulo + " and mes=" + mes;
			}
			DataTable dtArticulos = db.Execute(ssql);

			if (dtArticulos.Rows.Count == 0) {
				totalUnidadesReturn = 0;
			} else {
				totalUnidadesReturn = Convert.ToInt64(dtArticulos.Rows[0]["sumadeunidades"]);
			}

			return totalUnidadesReturn;
		}


		public static double TotalImporte(long articulo, int mes)
		{
			double totalImporteReturn;
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

			string ssql = null;

			if (mes == 0) {
				ssql = "select sum(total) as totalI from " + Variables.App.prefijoTablas + "v_InformeVentas where idArticulo=" +
				articulo;
			} else {
				ssql = "select total as totalI from v_InformeVentas where idArticulo=" + articulo + " and mes=" + mes;
			}

			DataTable dtArticulos = db.Execute(ssql);

			if (dtArticulos.Rows.Count == 0) {
				totalImporteReturn = 0;
			} else {
				totalImporteReturn = Convert.ToDouble(dtArticulos.Rows[0]["totalI"]);
			}

			return totalImporteReturn;
		}


		public static double TotalCesta(long idCesta)
		{
			double totalCestaReturn = 0;
			if (idCesta == 0) {
				totalCestaReturn = 0;
				return totalCestaReturn;
			}

			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

			string ssql = "select unidades from " + Variables.App.prefijoTablas + "lineascesta," + Variables.App.prefijoTablas +
			                       "articulos where (lineasCesta.idArticulo=articulos.idArticulo) and idCabeceraCesta=" + idCesta;
			DataTable dtc = db.Execute(ssql);

			totalCestaReturn = 0;


			foreach (DataRow row in dtc.Rows) {
				totalCestaReturn += (NumberUtils.NumberInt(row[Variables.User.PrecioAMostrar]) * NumberUtils.NumberInt(row["unidades"]));
			}

			double t_dto = 0;
			double totalIva = 0;

			if (totalCestaReturn != 0) {
				t_dto = totalCestaReturn - (totalCestaReturn * NumberUtils.NumberDouble(Variables.User.Dto) / 100);
				totalIva = (t_dto * NumberUtils.NumberDouble(Variables.App.Iva)) / 100;
				totalCestaReturn = totalIva + t_dto;
			}

			return totalCestaReturn;
		}


		public static long NumeroLineasPedido(long pedido)
		{
			int numeroLineasPedidoReturn = 0;
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

			string ssql = "select count(*) from " + Variables.App.prefijoTablas + "lineasPedido where idCabeceraPedido=" + pedido;
			numeroLineasPedidoReturn = NumberUtils.NumberInt(db.ExecuteScalar(ssql));

			return numeroLineasPedidoReturn;
		}


		public static double TotalPedido(long pedido)
		{
			double totalPedidoReturn = 0;
			if (pedido == 0) {
				totalPedidoReturn = 0;
				return totalPedidoReturn;
			}

			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
			string ssql = "select total,portes from " + Variables.App.prefijoTablas + "CabeceraPedido where idCabeceraPedido=" +
			                       pedido;
			DataTable dt = db.Execute(ssql);

			if (dt.Rows.Count == 0) {
				totalPedidoReturn = 0;
			} else {
				totalPedidoReturn = NumberUtils.NumberInt(dt.Rows[0]["total"]) + NumberUtils.NumberInt(dt.Rows[0]["portes"]);
			}

			return totalPedidoReturn;
		}


		public static long ArticulosCategoria(long categoria)
		{
			long articulosCategoriaReturn = 0;
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

			string ssql = "select count(*) as total from " + Variables.App.prefijoTablas +
			                       "categorias_articulos where idCategoria=" + categoria;
			DataTable dt = db.Execute(ssql);

			if (dt.Rows.Count == 0) {
				articulosCategoriaReturn = 0;
			} else {
				articulosCategoriaReturn = Convert.ToInt64(dt.Rows[0]["total"]);
			}

			return articulosCategoriaReturn;
		}


		public static long TotalMensajes()
		{
			long totalMensajesReturn = 0;
			if (Variables.User.Usuario == "") {
				totalMensajesReturn = 0;
				return totalMensajesReturn;
			}

			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

			string ssql = "select count(*) as total from " + Variables.App.prefijoTablas + "Correo where ([To]='" +
			                       Variables.User.Usuario + "' or [To]='Todos') and Leido=False";
			DataTable dtMensaje = db.Execute(ssql);

			if (dtMensaje.Rows.Count == 0) {
				totalMensajesReturn = 0;
			} else {
				totalMensajesReturn = Convert.ToInt64(dtMensaje.Rows[0]["total"]);
			}

			return totalMensajesReturn;
		}


		public static string CodigoCesta(int cesta)
		{
			string result = null;

			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
			string idu = Variables.User.UsuarioId.ToString();
			string ssql = null;

			if (idu == "0") {
				idu = "invitado-" + Variables.User.sessionID;

				ssql = "select UsuarioID from " + Variables.App.prefijoTablas + "usuarios where usuario='" + idu + "'";
				DataTable dtCli = db.Execute(ssql);
				if (dtCli.Rows.Count != 0) {
					idu = Functions.Valor(dtCli.Rows[0]["UsuarioID"]);
				} else {
					return "0";
				}
			}

			ssql = "select codigo from " + Variables.App.prefijoTablas + "cabeceracesta where idCliente=" + idu;
			DataTable dtCesta = db.Execute(ssql);

			if (dtCesta.Rows.Count == 0) {
				result = "0";
			} else {
				result = dtCesta.Rows[0]["codigo"].ToString();
			}

			return result;
		}


		public static string Idioma(long idIdioma)
		{
			return Idioma(idIdioma, true);
		}


		public static string Idioma(long idIdioma, bool showEdit)
		{
			Modulos modulos = new Modulos();
			DataColumn[] pk = new DataColumn[1];

			string sIdi = "";
			string i = Functions.Valor(Variables.User.idiomaSel);
			if (i == "")
				i = Variables.App.idioma; //idioma por defecto
			
			DataTable dtIdioma = (DataTable)Web.GetCacheValue("cacheIdiomas");
			if (dtIdioma == null) {
				if (Variables.App.UseXML)
				{
					XML xml = new XML(Variables.App.directorioWeb + "data");
					xml.Load("idiomas.xml");
					dtIdioma = xml.DataTable;
				}
				else
				{
					BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
					string ssql = "select idIdioma," + i + " from " + Variables.App.prefijoTablas + "Idiomas";
					dtIdioma = db.Execute(ssql);
				}

				Web.SetCacheValue("cacheIdiomas", dtIdioma);
			}

			pk[0] = dtIdioma.Columns["idIdioma"];
			dtIdioma.PrimaryKey = pk;
			DataRow dr = dtIdioma.Rows.Find(idIdioma);

			if (dr != null)
				sIdi = dr[i].ToString();
			else
				sIdi = "sin definir(" + idIdioma + ")";

			if (sIdi == "")
				sIdi = "sin valor(" + idIdioma + ")";

			if (Variables.User.Administrador & showEdit) {
				sIdi = "{" + idIdioma + "} " + sIdi;
			}
            
			//Ui.Link(Text.Substring(sIdi, 0, 1), Variables.App.directorioPortal + "admin/editor/showrecord.aspx?q=False&amp;tablename=Idiomas&amp;fld=idIdioma&amp;val=" + idIdioma + "&amp;page=1")

			return sIdi;
		}

		public static string ASCIIDecode(string cad)
		{
			//esta función deberia ser temporal
			return TextUtil.Replace(cad, "&#44;", ",");
		}
			

      


		public static string checkSessionID(string lngAspSessionID)
		{
			string checkSessionIDReturn = null;
			checkSessionIDReturn = "";
			if (lngAspSessionID != Variables.User.sessionID) {
				checkSessionIDReturn = (Variables.App.directorioPortal + "forum/insufficient_permission.aspx?M=sID");
			}
			return checkSessionIDReturn;
		}


		public static string ObtenerPlantilla(string nombrePlantilla)
		{
			string page = "";
			string[] columnas;

			if(!Variables.App.modoLite)
			{
				columnas = new string[] { "contenido", "plantilla", "contenidoMovil" };
			}
			else
			{
				columnas = new string[] { "contenido", "plantilla" };
			}

			DataTable dtPlantilla;
			if (Variables.App.UseXML)
			{
				XML xml = new XML(Variables.App.directorioWeb + "data");
				xml.Load("plantillas.xml");
				dtPlantilla = xml.Select("plantilla='" + nombrePlantilla + "'");
			}
			else
			{
				BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
				
				SelectQueryBuilder sqB = new SelectQueryBuilder();
				sqB.Columns.SelectColumns(columnas);
				sqB.TableSource = Variables.App.prefijoTablas + "Plantillas";

				if (nombrePlantilla == "")
					sqB.Where = new SimpleWhere(sqB.TableSource, "porDefecto", Comparison.Equals, true);
				else
					sqB.Where = new SimpleWhere(sqB.TableSource, "plantilla", Comparison.Equals, nombrePlantilla);

				dtPlantilla = db.Execute(sqB.BuildQuery());
			}

			if (dtPlantilla != null && dtPlantilla.Rows.Count > 0)
			{
				if (!Variables.App.modoLite && IsMobileBrowser())
				{
					page = Functions.Valor(dtPlantilla.Rows[0]["contenidoMovil"]);
					if (page == "")
						page = Functions.Valor(dtPlantilla.Rows[0]["contenido"]);
				}
				else
				{
					page = Functions.Valor(dtPlantilla.Rows[0]["contenido"].ToString());
				}
			}

			if (page == "")
			{
				if (Variables.App.UseXML)
				{
					XML xml = new XML(Variables.App.directorioWeb + "data");
					dtPlantilla = xml.Select("porDefecto=true");
				}
				else
				{
					BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

					SelectQueryBuilder sqB = new SelectQueryBuilder();
					sqB.Where = new SimpleWhere(sqB.TableSource, "porDefecto", Comparison.Equals, true);

					dtPlantilla = db.Execute(sqB.BuildQuery());
				}

				if (dtPlantilla != null && dtPlantilla.Rows.Count > 0)
				{
					if (IsMobileBrowser() && Functions.Valor(dtPlantilla.Rows[0]["contenidoMovil"]) != "")
						page = Functions.Valor(dtPlantilla.Rows[0]["contenidoMovil"]);
					else
						page = Functions.Valor(dtPlantilla.Rows[0]["contenido"]);
				}

				if (page == "")
				{
					if (nombrePlantilla != "")
						throw new ExceptionUtil("Contenido de la plantilla: " + nombrePlantilla + ", sin definir.");
					throw new ExceptionUtil("Conenido de la plantilla por defecto, sin definir.");
				}
			}
			
			if(TextUtil.IndexOf(page, "contenido") == -1)
			{
				page +=Ui.Lf() + "ERROR! Falta sentencia para mostrar el contenido." + Ui.Lf() + "{frmContenido()}" + Ui.Lf();
			}

			return page;
		}
           
                
		public static string FormatDB(string data)
		{
			data = Functions.Valor(data);
			
			if (data == "")
				return "";
        	
			//al guardar los datos, reestablecemos el directorio web y del portal, con las funciones {..}
			//data = Text.Replace(data, @"src=""" + Variables.App.directorioPortal, @"src=""{directorioPortal}");
			//data = Text.Replace(data, @"src=""" + Variables.App.directorioWeb, @"src=""{directorioWeb}");

			data = Web.ReplaceImg(data, Variables.App.directorioWeb, "{directorioWeb}");
			data = Web.ReplaceImg(data, Variables.App.directorioPortal, "{directorioPortal}");
			
			data = HttpUtility.HtmlDecode(data);

			data = TextUtil.Replace(data, "'", "{cs}");

			
			return data;
		}


		public static bool IsMobileBrowser()
		{
			if (Variables.App.MobileMode)
				return true;

			if (HttpContext.Current.Request.Browser.IsMobileDevice) {
				return true;
			}
			if (HttpContext.Current.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null) {
				return true;
			}
			if (HttpContext.Current.Request.ServerVariables["HTTP_ACCEPT"] != null &&
			             HttpContext.Current.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap")) {
				return true;
			}
			if (HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"] != null) {
				string[] mobiles = {
                    "midp", "j2me", "avant", "docomo",
                    "novarra", "palmos", "palmsource",
                    "240x320", "opwv", "chtml",
                    "pda", "windows ce", "mmp/",
                    "blackberry", "mib/", "symbian",
                    "wireless", "nokia", "hand", "mobi",
                    "phone", "cdm", "up.b", "audio",
                    "SIE-", "SEC-", "samsung", "HTC",
                    "mot-", "mitsu", "sagem", "sony"
                    , "alcatel", "lg", "eric", "vx",
                    "philips", "mmm", "xx",
                    "panasonic", "sharp", "wap", "sch",
                    "rover", "pocket", "benq", "java",
                    "pt", "pg", "vox", "amoi",
                    "bird", "compal", "kg", "voda",
                    "sany", "kdd", "dbt", "sendo",
                    "sgh", "gradi", "jb", "dddi",
                    "moto", "iphone", "ipad", "ipod",
                    "android"
                };

                foreach (string s in mobiles)
                {
                    if (HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].
                        ToLower().Contains(s.ToLower()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public static void GetStats(long lDescargas, long lPaginasVistas, int lTemaID)
        {
            string ssql = "SELECT Descargas, PaginasVistas FROM " + Variables.App.prefijoTablas + "Temas WHERE TemaID = " +
                          lTemaID;

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            DataTable dt = db.Execute(ssql);

			lDescargas = Convert.ToInt64(NumberUtils.NumberDouble(dt.Rows[0]["Descargas"]));
			lPaginasVistas = Convert.ToInt64(NumberUtils.NumberDouble(dt.Rows[0]["PaginasVistas"]));
            db.ExecuteNonQuery("UPDATE " + Variables.App.prefijoTablas +
                               "Temas SET PaginasVistas = PaginasVistas + 1 WHERE TemaID = " + lTemaID);
        }
			

        public static string GetUserName(long id)
        {
            string getUserNameReturn = null;

            string ssql = "SELECT nombre,apellido1,apellido2 FROM " + Variables.App.prefijoTablas + "Usuarios WHERE UsuarioID=" +
                          id;

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            DataTable dtU = db.Execute(ssql);

            if (dtU.Rows.Count == 0)
            {
                getUserNameReturn = "";
            }
            else
            {
                getUserNameReturn = dtU.Rows[0]["nombre"] + " " + dtU.Rows[0]["apellido1"] + " " +
                                    dtU.Rows[0]["apellido2"];
            }

            return getUserNameReturn;
        }


        public static string GetUserEmail(long id)
        {
            string getUserEmailReturn = null;

            string ssql = "SELECT email FROM Usuarios WHERE UsuarioID=" + id;

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            DataTable dtU = db.Execute(ssql);

            if (dtU.Rows.Count == 0)
            {
                getUserEmailReturn = "";
            }
            else
            {
                getUserEmailReturn = dtU.Rows[0]["email"].ToString();
            }

            return getUserEmailReturn;
        }


        public static bool TieneCategorias(long idCategoria)
        {
            bool tieneCategoriasReturn = false;
            string ssql = "select idCategoriaPadre from " + Variables.App.prefijoTablas + "categorias where idCategoriaPadre=" +
                          idCategoria;

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            DataTable dtCat = db.Execute(ssql);
            if (dtCat.Rows.Count > 0)
            {
                tieneCategoriasReturn = true;
            }
            else
            {
                tieneCategoriasReturn = false;
            }

            return tieneCategoriasReturn;
        }


        public static bool TienePaginas(int idPagina)
        {
            string ssql = "select idPagina from " + Variables.App.prefijoTablas + "paginas where padre=" + idPagina;

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            int total = db.ExecuteNonQuery(ssql);

            if (total > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string ImagenMasMenosModulo(string estado)
        {
            string pos;
            if (estado == "hide")
            {
                pos = "mas";
            }
            else
            {
                pos = "menos";
            }
            return pos;
        }

        public static string MuestraOcultaModulo(string estado)
        {
            string estilo = null;
            if (estado == "hide")
            {
                estilo = "style='display:none'";
            }
            else
            {
                estilo = "";
            }
            return estilo;
        }

        public static string MuestraSubCategorias(long idCategoria)
        {
            string ssql = "select idCategoria,titulo from " + Variables.App.prefijoTablas + "categorias where idCategoriaPadre=" +
                          idCategoria;

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            StringBuilder sb = new StringBuilder("");
            DataTable dtCat = db.Execute(ssql);

            ssql = "select titulo from " + Variables.App.prefijoTablas + "categorias where idCategoria=" + idCategoria;
            DataTable dtCate = db.Execute(ssql);

            string catID = "cat" + idCategoria;

            sb.Append(@"<a href=""javascript:hideshow('" + catID + "','desplegar" + catID + "','" + Variables.App.directorioPortal +
                      @"');""><img alt="""" id=""desplegar" + catID + @""" src=""" + Variables.App.directorioPortal + "imagenes/" +
                      ImagenMasMenosModulo(catID) + @".gif"" border=""0""/></a>");
            sb.Append("\r\n" +
                      ("<a href='verProductos.aspx?modo=lista&amp;cat=" + idCategoria + "'>" + dtCate.Rows[0]["titulo"] +
                       "</a>"));
            sb.Append("\r\n" + ("<div " + MuestraOcultaModulo(catID) + " id='" + catID + "'>"));
            sb.Append("\r\n" + ("<table>"));

            foreach (DataRow row in dtCat.Rows)
            {
                sb.Append("\r\n" + ("<tr><td width='25'>&nbsp;</td><td>"));

                if (TieneCategorias(Convert.ToInt64(row["idCategoria"])))
                {
                    sb.Append(MuestraSubCategorias(Convert.ToInt64(row["idCategoria"])));
                }
                else
                {
                    sb.Append("\r\n" +
                              (@"<img alt="""" border=""0"" src='" + Variables.App.directorioWeb +
                               "tienda/imagenes/arrow_c.gif' />"));

                    sb.Append("\r\n" +
                              Ui.EditPage("categorias", idCategoria.ToString(), row["idCategoria"].ToString(),
                                  "Editar Categoría", "Borrar Categoría"));

                    sb.Append("\r\n" +
                              ("<a href='verProductos.aspx?modo=lista&amp;cat=" + row["idCategoria"] + "'>" +
                               row["titulo"]));

					sb.Append(" (" + ArticulosCategoria(long.Parse(Functions.Valor(row["idCategoria"]))) + ")</a>");
                }

                sb.Append("\r\n" + ("</td></tr>"));
            }
            sb.Append("\r\n" + ("</table></div>"));

            return sb.ToString();
        }


        public static string MuestraSubPaginas(int idPagina)
        {
            string ssql = "select idPagina,nuevaPagina,titulo,enlace from " + Variables.App.prefijoTablas +
                          "paginas where Padre=" + idPagina + " order by orden DESC";

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            StringBuilder sb = new StringBuilder("");
            string nuevaPag = "";
            DataTable dtCat = db.Execute(ssql);

            ssql = "select nuevaPagina,enlace,titulo from " + Variables.App.prefijoTablas + "paginas where idPagina=" + idPagina;
            DataTable dtCate = db.Execute(ssql);

            sb.Append(@"&nbsp;<a href=""javascript:hideshow('mm_" + idPagina + "','desplegar" + idPagina + "','" +
                      Variables.App.directorioPortal + @"');""><img alt='' id='desplegar" + idPagina + "' src='" +
                      Variables.App.directorioPortal + "imagenes/" + ImagenMasMenosModulo(idPagina.ToString()) +
                      ".gif' border='0'/></a>");

			if (Functions.ValorBool(dtCate.Rows[0]["nuevaPagina"]) == false)
            {
                nuevaPag = "";
            }
            else
            {
                nuevaPag = "target='_blank'";
            }
			if (Functions.Valor(dtCate.Rows[0]["enlace"]) != "")
            {
                sb.Append("\r\n" +
                          ("<a " + nuevaPag + " href='" + Variables.App.directorioPortal + dtCate.Rows[0]["enlace"] + "'>" +
                           dtCate.Rows[0]["titulo"] + "</a>"));
            }
            else
            {
                sb.Append("\r\n" +
                          ("<a " + nuevaPag + " href='pagina.aspx?id=" + idPagina + "'>" + dtCate.Rows[0]["titulo"] +
                           "</a>"));
            }

            sb.Append("\r\n" + ("<div " + MuestraOcultaModulo(idPagina.ToString()) + " id='mm_" + idPagina + "'>"));
            sb.Append("\r\n" + ("<table>"));

            foreach (DataRow row in dtCat.Rows)
            {
                sb.Append("\r\n" + ("<tr><td width='25'>&nbsp;</td><td>"));

                if (TienePaginas(NumberUtils.NumberInt(row["idPagina"])))
                {
                    sb.Append(MuestraSubPaginas(NumberUtils.NumberInt(row["idPagina"])));
                }
                else
                {
                    sb.Append("\r\n" +
                              (@"<img alt="""" border=""0"" src='" + Variables.App.directorioPortal + "imagenes/arrow_c.gif' />"));

                    sb.Append("\r\n" +
                              Ui.EditPage("paginas", "idPagina", row["idPagina"].ToString(), "Editar Página",
                                  "Borrar Página"));

                    string subbaj = "";
                    if (Variables.User.Administrador)
                    {
                        subbaj = "<a href='?subir=" + row["idPagina"] + @"'><img alt='' border=""0"" src='" +
                                 Variables.App.directorioPortal + "imagenes/arr.gif' /></a>&nbsp;<a href='?bajar=" +
                                 row["idPagina"] + @"'><img alt='' border=""0"" src='" + Variables.App.directorioPortal +
                                 "imagenes/aba.gif' /></a>&nbsp;";
                    }
                    nuevaPag = "";
                    if (row["enlace"] + "" != "")
                    {
						if (Functions.ValorBool(row["nuevaPagina"]) == false)
                        {
                            nuevaPag = "";
                        }
                        else
                        {
                            nuevaPag = "target='_blank'";
                        }
                        sb.Append(subbaj + "&nbsp;<a class='modlink' " + nuevaPag + " href='" + Variables.App.directorioPortal +
                                  row["enlace"] + "'>" + row["titulo"] + "</a>" + Ui.Lf() + "\r\n");
                    }
                    else
                    {
                        sb.Append(subbaj + "&nbsp;<a class='modlink' " + nuevaPag + " href='" + Variables.App.directorioPortal +
                                  "pagina.aspx?id=" + row["idPagina"] + "'>" + row["titulo"] + "</a>" + Ui.Lf() + "\r\n");
                    }
                }

                sb.Append("\r\n" + ("</td></tr>"));
            }
            sb.Append("\r\n" + ("</table></div>"));

            return sb.ToString();
        }


        public static string MuestraSubCategorias2(long idCategoria)
        {
            string ssql = "select idCategoria,titulo from " + Variables.App.prefijoTablas + "categorias where idCategoriaPadre=" +
                          idCategoria;
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            StringBuilder sb = new StringBuilder("");
            DataTable dtCat1 = db.Execute(ssql);

            foreach (DataRow row in dtCat1.Rows)
            {
                sb.Append("\r\n" + "<a href='verProductos.aspx?modo=lista&amp;cat=" + row["idCategoria"] + "'>[" +
                          row["titulo"] + "]</a>&nbsp;");
            }

            return sb.ToString();
        }


        public static string ScriptEditorGenerate(string field)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(@"<script type=""text/javascript"" language=""javascript"">");
            sb.Append("\r\n" + "editor_generate('" + field + "');");
            sb.Append("\r\n" + "</script>");
            return sb.ToString();
        }


        public static string ScriptSaveStatData()
        {
            StringBuilder sb = new StringBuilder("");

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            if (db.TableExists(Variables.App.prefijoTablas + "Stats"))
            {
                sb.Append(@"<script type=""text/javascript"" language=""javascript"">");
                sb.Append("\r\n" + "contar();");
                sb.Append("\r\n" + "</script>");
            }
            return sb.ToString();
        }

        public static string ScriptStats()
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append("\r\n" + @"<script type=""text/javascript"" language=""javascript"">");
            sb.Append("\r\n" + "var file='" + Variables.App.webHttp + Variables.App.directorioPortal + "contar.aspx';");
            sb.Append("\r\n" + "var d=new Date();");
            sb.Append("\r\n" + "var s=d.getSeconds();");
            sb.Append("\r\n" + "var m=d.getMinutes();");
            sb.Append("\r\n" + "var x=s*m;");
            sb.Append("\r\n" + "f='' + escape(document.referrer);");
            sb.Append("\r\n" + "if (navigator.appName=='Netscape'){b='NS';}");
            sb.Append("\r\n" + "if (navigator.appName=='Microsoft Internet Explorer'){b='MSIE';}");
            sb.Append("\r\n" + "if (navigator.appVersion.indexOf('MSIE 3')>0) {b='MSIE';}");
            sb.Append("\r\n" + "u='' + escape(document.URL); w=screen.width; h=screen.height;");
            sb.Append("\r\n" + "v=navigator.appName;");
            sb.Append("\r\n" + "fs = window.screen.fontSmoothingEnabled;");
            sb.Append("\r\n" + "if (v != 'Netscape') {c=screen.colorDepth;}");
            sb.Append("\r\n" + "else {c=screen.pixelDepth;}");
            sb.Append("\r\n" + "j=navigator.javaEnabled();");
            sb.Append("\r\n" +
                      "info='w=' + w + '&amp;h=' + h + '&amp;c=' + c + '&amp;r=' + f + '&amp;u='+ u + '&amp;fs=' + fs + '&amp;b=' + b + '&amp;x=' + x;");
            sb.Append("\r\n" +
                      @"document.write('<img src=""' + file + '?'+info+ '"" width=""0"" height=""0"" alt="""" border=""0""/>');");
            sb.Append("\r\n" + "</script>");
            return sb.ToString();
        }




        public static string AddShareButton()
        {
            string button =
                @"<!-- AddThis Button BEGIN --><a class=""addthis_button"" href=""http://www.addthis.com/bookmark.php?v=250&amp;username=xa-4b89ac81270faca2""><img src=""http://s7.addthis.com/static/btn/v2/lg-share-en.gif"" width=""125"" height=""16"" alt=""Bookmark and Share"" style=""border:0""/></a><script type=""text/javascript"" src=""http://s7.addthis.com/js/250/addthis_widget.js#username=xa-4b89ac81270faca2""></script><!-- AddThis Button END -->";
            return button;
        }

        

        public static string CambiaModo(int modo)
        {
            string mo = "SE ";
            if (modo == 0)
            {
                mo += "VENDE";
            }
            if (modo == 1)
            {
                mo += "ALQUILA";
            }
            if (modo == 2)
            {
                mo += "TRASPASA";
            }
            return mo;
        }

        public static string GetCountryName(double longIP)
        {
            if (longIP == 2130706433.0) return "(localhost)"; //127.0.0.1

            string getCountryNameReturn = "(spain)";
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            if (db.TableExists(Variables.App.prefijoTablas + "Ip_Paises"))
            {
                string ssql = "SELECT countryLONG FROM " + Variables.App.prefijoTablas + "Ip_Paises WHERE " + longIP +
                              " BETWEEN IPFROM AND IPTO";

                DataTable dtU = db.Execute(ssql);

                if (dtU.Rows.Count == 0)
                {
                    getCountryNameReturn = "(desconocido)";
                }
                else
                {
                    getCountryNameReturn = dtU.Rows[0]["countryLONG"].ToString();
                }
            }

            return getCountryNameReturn;
        }


		public static void ClearVariables()
		{
			BasePage basePage = Variables.App.Page;
			if (basePage != null)
				basePage.Session.Abandon();

			Variables.User = null;
			Variables.App = null;
			Variables.Parser = null;
			Variables.User = new VariablesUsuario();
			Variables.App = new VariablesAplicacion();
			Variables.Parser = new VariablesParser();

			Variables.App.Page = basePage;

			Portal portal = new Portal();
			portal.LoadVariables();

			////limpiamos variables cache
			Web.ClearCacheVariables();
		}

        public static bool EsAdmin(int grupo)
        {
			if(Variables.App.modoLite)
			{
				if (grupo == 1)
					return true;
				else
					return false;
			}

			if (Variables.App.UseXML)
			{
				XML xml = new XML(Variables.App.directorioWeb + "data");
				xml.Load("grupos.xml");
				DataTable dtGrupo = xml.Select("grupoId=" + grupo);
				return Functions.ValorBool(dtGrupo.Rows[0]["administrar"]);
			}
			else
			{
				BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
				return Functions.ValorBool(db.ExecuteScalar("select administrar from grupos where grupoId = " + grupo));
			}
        }
    }
}