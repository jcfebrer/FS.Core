// // <fileheader>
// // <copyright file="Modulos.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSPortal
// //     Solution: FSPortalNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System;
using System.Data;
using System.Text;
using FSLibrary;
using FSQueryBuilder;
using FSQueryBuilder.Enums;
using FSQueryBuilder.QueryParts.Where;
using FSDatabase;
using FSNetwork;
using FSException;
using FSTrace;

#endregion

namespace FSPortal
{
	/// <summary>
	///     Clase para mostrar los módulos del portal
	/// </summary>
	public class Modulos
	{
		//public string ___MuestraPie()
		//{
		//    string muestraPieReturn = "";
		//    FSPortal.BD.Utils db = new FSPortal.BD.Utils();

		//    string cache = Functions.Valor(Functions.GetSessionValue("cachePie"));
		//    if (cache != "")
		//    {
		//        return cache;
		//    }

		//    DataTable dtPie = db.Execute("SELECT titulo,mostrarRecuadro,idPagina,contenido FROM " + Variables.App.prefijoTablas + "Paginas where mostrarEnPie=true Order by fechaCreacion DESC");

		//    foreach(DataRow row in dtPie.Rows)
		//    {
		//        if (Functions.NumeroEntero(row["mostrarRecuadro"]) == Functions.NumeroEntero(true))
		//        {
		//            muestraPieReturn = muestraPieReturn + Cabecera(-1, row["titulo"].ToString());
		//        }
		//        muestraPieReturn = muestraPieReturn + UI.Lf();

		//        muestraPieReturn = muestraPieReturn + UI.EditPage("Paginas", "IdPagina", row["idPagina"].ToString(), "Editar Página", "Borrar Página");

		//        if(Functions.IsMobileBrowser())
		//            muestraPieReturn = muestraPieReturn + row["contenidoMovil"].ToString();
		//        else
		//            muestraPieReturn = muestraPieReturn + row["contenido"].ToString();

		//        if (Functions.NumeroEntero(row["mostrarRecuadro"]) == Functions.NumeroEntero(true))
		//        {
		//            muestraPieReturn = muestraPieReturn + Pie(-1);
		//        }
		//    }

		//    muestraPieReturn += UI.CopyRight();

		//    if (Variables.App.estadisticas) muestraPieReturn += Functions.ScriptSaveStatData();

		//    muestraPieReturn = Functions.formatCad(muestraPieReturn);

		//    if (Variables.App.cachearPie) Functions.SetSessionValue("cachePie", muestraPieReturn);

		//    return muestraPieReturn;
		//}

		//public string ___MuestraCabecera()
		//{
		//    string muestraCabeceraReturn = "";
		//    FSPortal.BD.Utils db = new FSPortal.BD.Utils();

		//    string cache = Functions.Valor(Functions.GetSessionValue("cacheCabecera"));
		//    if (cache != "")
		//        return cache;

		//    DataTable dtCabecera = db.Execute("SELECT mostrarRecuadro,idPagina,contenido,tituloRecuadro FROM " + Variables.App.prefijoTablas + "Paginas where mostrarEnCabecera=true Order by fechaCreacion DESC");

		//    foreach(DataRow row in dtCabecera.Rows)
		//    {

		//        if (Functions.ValorBool(row["mostrarRecuadro"]) == true)
		//        {
		//            muestraCabeceraReturn = muestraCabeceraReturn + Cabecera(-1, row["tituloRecuadro"].ToString());
		//        }

		//        muestraCabeceraReturn = muestraCabeceraReturn + UI.EditPage("Paginas", "IdPagina", row["idPagina"].ToString(), "Editar Página", "Borrar Página");

		//        if(Functions.IsMobileBrowser())
		//            muestraCabeceraReturn = muestraCabeceraReturn + row["contenidoMovil"].ToString();
		//        else
		//            muestraCabeceraReturn = muestraCabeceraReturn + row["contenido"].ToString();

		//        if (Functions.ValorBool(row["mostrarRecuadro"]) == true)
		//        {
		//            muestraCabeceraReturn = muestraCabeceraReturn + Pie(-1);
		//        }
		//    }

		//    muestraCabeceraReturn = Functions.formatCad(muestraCabeceraReturn);

		//    if (Variables.App.cachearCabecera) Functions.SetSessionValue("cacheCabecera", muestraCabeceraReturn);

		//    return muestraCabeceraReturn;
		//}

		public bool ModuloActivo(string titModulo)
		{
			return Variables.App.modulosActivos.Contains(titModulo.ToLower());
		}

		//public string MuestraModulos(int posicion)
		//{
		//    string cache ="";
		//    switch(posicion)
		//    {
		//        case 0:
		//            cache = Functions.Valor(Functions.GetSessionValue("cacheColumnaIzquierda"));
		//            if (cache != "")
		//               return cache;
		//            break;
		//        case 1:
		//            cache = Functions.Valor(Functions.GetSessionValue("cacheColumnaDerecha"));
		//            if (cache != "")
		//               return cache;
		//            break;
		//        case 2:
		//            cache = Functions.Valor(Functions.GetSessionValue("cacheColumnaCentro"));
		//            if (cache != "")
		//               return cache;
		//            break;
		//    }


		//    string muestraModulosReturn ="";
		//    FSPortal.BD.Utils db = new FSPortal.BD.Utils();

		//    string ssql = "SELECT idModulo,titulo,nombre from " + Variables.App.prefijoTablas + "Modulos where posicion=" + posicion + " and activo=true order by orden";
		//    DataTable dtMMod = db.Execute(ssql);

		//    foreach(DataRow row in dtMMod.Rows)
		//    {
		//        muestraModulosReturn += MuestraModulo(row, true);
		//    }

		//    //if (Variables.User.Administrador & !(Variables.App.sinColumnaIzq))
		//    //{
		//    //    muestraModulosReturn += @"<a href=""" + Variables.App.directorioPortal + @"addModulo.aspx?pos=" + posicion + @""">Añadir Modulo</a>";
		//    //}

		//    //switch(posicion)
		//    //{
		//    //    case 0:
		//    //        if (Variables.App.cachearColumnaIzquierda)Functions.SetSessionValue("cacheColumnaIzquierda", muestraModulosReturn);
		//    //        break;
		//    //    case 1:
		//    //        if (Variables.App.cachearColumnaDerecha) Functions.SetSessionValue("cacheColumnaDerecha", muestraModulosReturn);
		//    //        break;
		//    //    case 2:
		//    //        if (Variables.App.cachearColumnaCentro) Functions.SetSessionValue("cacheColumnaCentro", muestraModulosReturn);
		//    //        break;
		//    //}

		//    return muestraModulosReturn;
		//}

		//public bool DameMostrarRecuadro(long id)
		//{
		//    bool mosR = false;
		//    FSPortal.BD.Utils db = new FSPortal.BD.Utils();


		//    string ssql = "SELECT mostrarRecuadro from " + Variables.App.prefijoTablas + "Modulos where idModulo=" + id;
		//    DataTable dtMod = db.Execute(ssql);

		//    if (dtMod.Rows.Count > 0)
		//    {
		//        mosR = Functions.ValorBool(dtMod.Rows[0]["mostrarRecuadro"]);
		//    }

		//    return mosR;
		//}

		public string MuestraModulo(DataRow drModulo, bool muestraRecuadro)
		{
			string muestraModuloReturn = "";

			int idModulo = 0;
			string titulo = null;
			string nombre = null;
			string cont = "";

			idModulo = NumberUtils.NumberInt(drModulo["idModulo"]);
			titulo = Functions.Valor(drModulo["titulo"]);
			nombre = Functions.Valor(drModulo["nombre"]);

			Variables.App.modulosActivos.Add(nombre.ToLower());

			Log.TraceInfo("MOD:" + nombre);

			switch (Functions.Valor(nombre).ToLower()) {
				case "modacceso":
					cont += ModAcceso(idModulo);
					break;
				case "modbuscar":
					cont += ModBuscar(idModulo);
					break;
				case "modusuariosonline":
					cont += ModUsuariosOnLine(idModulo);
					break;
				case "modpaginas":
					cont += ModPaginas(idModulo);
					break;
				case "modidioma":
					cont += ModIdioma(idModulo);
					break;
				case "modpagina":
					cont += ModPagina(idModulo);
					break;
				case "modmodulos":
					cont += ModModulos(idModulo);
					break;
				case "modcontactar":
					cont += ModContactar(idModulo);
					break;
				case "modmapa":
					cont += ModMapa(idModulo);
					break;
				case "modformularios":
					cont += ModFormularios(idModulo);
					break;
				case "modteclasacceso":
					cont += ModTeclasAcceso(idModulo);
					break;
				default:
                    //if (Functions.EsLocalhost() || Variables.User.Administrador)
                    //    cont += "Función para modulo: " + nombre + ", inexistente.";
                    //else
					throw new ExceptionUtil("Función para modulo: " + nombre + ", inexistente.");
			//break;
			}

			muestraModuloReturn = muestraModuloReturn + cont;

			Log.TraceInfo("MOD:Fin" + nombre);

			if (Variables.App.plantillaModulos != "" && muestraRecuadro) {
				BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
				
				SelectQueryBuilder sqB = new SelectQueryBuilder();
	            sqB.Columns.SelectColumns("contenido");
	            sqB.TableSource = Variables.App.prefijoTablas + "Plantillas";
	
	            sqB.Where = new SimpleWhere(sqB.TableSource, "plantilla", Comparison.Equals, Variables.App.plantillaModulos);

				string plantilla =
					Functions.Valor(
						db.ExecuteScalar(sqB.BuildQuery()));

				muestraModuloReturn = plantilla.Replace("{modulo}", muestraModuloReturn);
				muestraModuloReturn = muestraModuloReturn.Replace("{moduloTitulo}", titulo);
				muestraModuloReturn = muestraModuloReturn.Replace("{moduloId}", idModulo.ToString());
				muestraModuloReturn = muestraModuloReturn.Replace("{moduloControl}", ModControl(titulo, idModulo));
			}

			if (cont == "")
				muestraModuloReturn = "";

			return muestraModuloReturn;
		}

		public string MuestraModulo(int codModulo, bool muestraRecuadro)
		{
			DataTable dt;

			if (Variables.App.UseXML)
			{
				XML xml = new XML(Variables.App.directorioWeb + "data");
				xml.Load("modulos.xml");
				dt = xml.Select("idModulo=" + codModulo);
			}
			else
			{
				BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

				SelectQueryBuilder sqB = new SelectQueryBuilder();
				sqB.Columns.SelectColumns("idModulo", "titulo", "nombre");
				sqB.TableSource = Variables.App.prefijoTablas + "modulos";
				sqB.Where = new SimpleWhere(sqB.TableSource, "idModulo", Comparison.Equals, codModulo);

				dt = db.Execute(sqB.BuildQuery());
			}

			if (dt == null || dt.Rows.Count == 0)
				return "Modulo inexistente.";

			return MuestraModulo(dt.Rows[0], muestraRecuadro);
		}

		public string MuestraModulo(string modulo, bool muestraRecuadro)
		{
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

			SelectQueryBuilder sqB = new SelectQueryBuilder();
			sqB.Columns.SelectColumns("idModulo", "titulo", "nombre");
			sqB.TableSource = Variables.App.prefijoTablas + "modulos";
			sqB.Where = new SimpleWhere(sqB.TableSource, "nombre", Comparison.Equals, modulo);

			DataTable dt = db.Execute(sqB.BuildQuery());

			if (dt.Rows.Count == 0)
				return "Modulo inexistente.";

			return MuestraModulo(dt.Rows[0], muestraRecuadro);
		}

		private string ModControl(string titulo, long id)
		{
			StringBuilder sb = new StringBuilder("");
			if (id >= 0 & Variables.User.Administrador) {
				sb.Append(Ui.Link(Ui.Image("edi.gif", 0, "Editar"),
					Variables.App.directorioPortal +
					"admin/editor/showrecord.aspx?q=&amp;tablename=Modulos&amp;fld=idModulo&amp;val=" + id +
					"&amp;fldtype=System.Integer&amp;page=1"));
			}

			return sb.ToString();
		}

		public string ModAcceso(long id)
		{
			string modAccesoReturn = "";
			if (Functions.Valor(Variables.User.Usuario) != "") {
				modAccesoReturn = Ui.Lf() + "\r\n";
				if (Variables.User.paginaInicio != 0) {
					modAccesoReturn += @"<img border=""0"" src='" + Variables.App.directorioPortal +
					"imagenes/bullet.gif' alt='' /> <a class='modlink' href='" + Variables.App.directorioPortal +
					"pagina.aspx?id=" + Variables.User.paginaInicio + "'>Página de inicio</a>" + Ui.Lf() +
					"\r\n";
				}
				modAccesoReturn += @"<img border=""0"" src='" + Variables.App.directorioPortal +
				"imagenes/bullet.gif' alt='' /> <a class='modlink' href='" + Variables.App.paginaPerfil +
				"'>Modificar mis datos</a>" + Ui.Lf() + "\r\n";
				modAccesoReturn += @"<img border=""0"" src='" + Variables.App.directorioPortal +
				"imagenes/bullet.gif' alt='' /> <a class='modlink' href='" + Variables.App.directorioPortal +
				"servicios/desconectar.aspx?comebackto='" + Variables.User.ComeBack + ">Desconectar</a>" +
				"\r\n";
			} else {
				string strLogin = Variables.App.directorioPortal + "formularios/EnviarFormulario.aspx?modo=login";
				modAccesoReturn += @"<form method=""post"" id=""frmLogin"" class=""frmLogin"" action=""" + strLogin +
				"&comebackto=" + Variables.App.Page.Server.HtmlEncode(Web.Request("comebackto")) + @""">" +
				"\r\n";
				modAccesoReturn += FuncionesWeb.Idioma(290, true) + " / E-mail: " + Ui.Lf() + "\r\n";
				modAccesoReturn += @"<input type='text' name='txtUsuario' size=""10"" />" + "\r\n";
				modAccesoReturn += Ui.Lf() + "\r\n";
				modAccesoReturn += FuncionesWeb.Idioma(222, true) + ": " + Ui.Lf() + "\r\n";
				modAccesoReturn += @"<input type='password' name='txtClave'  size=""10"" />" + "\r\n";
				modAccesoReturn += Ui.Lf() + "\r\n";
				modAccesoReturn += "<input type='checkbox' name='chkRemember' value='1' />" + "\r\n";
				modAccesoReturn += FuncionesWeb.Idioma(248, true) + Ui.Lf() + "\r\n";
				modAccesoReturn += Ui.Lf() + "\r\n";
				modAccesoReturn += "<input type='submit' name='cmdLogin' value='" + FuncionesWeb.Idioma(310, true) +
				"' class='botonplano' />" + "\r\n";
				modAccesoReturn += Ui.Lf() + Ui.Lf() + "\r\n";

				modAccesoReturn += @"<img border=""0"" alt='' src='" + Variables.App.directorioPortal +
				"imagenes/registrate.gif' /> <a class='modlink' href='" + Variables.App.paginaRegistro + "'>" +
				FuncionesWeb.Idioma(180, true) + "</a>" + Ui.Lf() + "\r\n";
				modAccesoReturn += @"<img border=""0"" alt='' src='" + Variables.App.directorioPortal +
				"imagenes/password.gif' /> <a class='modlink' href='" + Variables.App.paginaRecordar + "'>" +
				FuncionesWeb.Idioma(312, true) + "</a>" + "\r\n";
				modAccesoReturn += "</form>" + "\r\n";
			}
			return modAccesoReturn;
		}

		public string ModPaginas(long id)
		{
			string modPaginasReturn = null;
			modPaginasReturn = "";
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
			
			SelectQueryBuilder sqB = new SelectQueryBuilder();
            sqB.Columns.SelectColumns("idPagina","titulo","nuevaPagina","enlace","teclaDeAcceso");
            sqB.TableSource = Variables.App.prefijoTablas + "Usuarios";
            
            sqB.OrderBy.Add( new FSQueryBuilder.QueryParts.OrderBy.OrderByClause("fechaCreacion",Sorting.Descending));
            sqB.OrderBy.Add( new FSQueryBuilder.QueryParts.OrderBy.OrderByClause("titulo",Sorting.Ascending));

            DataTable dtPaginas = db.Execute(sqB.BuildQuery());

			foreach (DataRow row in dtPaginas.Rows) {
				modPaginasReturn += @"<div class=""pagina"" id=""P:" + row["idPagina"] + @""">";
				modPaginasReturn += @"<img border=""0"" src='" + Variables.App.directorioPortal + "imagenes/bullet.gif' alt='' />";

				modPaginasReturn += "&nbsp;" +
				Ui.EditPage("Paginas", "IdPagina", row["idPagina"].ToString(), "Editar Página",
					"Borrar Página");

				string nuevaPag = "";
				string teclaAcceso = "";

				if (Functions.ValorBool(row["nuevaPagina"])) {
					nuevaPag = "target='_blank'";
				}

				if (Functions.Valor(row["teclaDeAcceso"]) != "") {
					teclaAcceso = @"accesskey=""" + Functions.Valor(row["teclaDeAcceso"]) + @"""";
				}

				if (Functions.Valor(row["enlace"]) != "") {
					modPaginasReturn += " <a class='modlink' " + teclaAcceso + " " + nuevaPag + @" href=""" +
					FuncionesWeb.Enlace(Functions.Valor(row["enlace"])) + @""">" + row["titulo"] + "</a>" +
					Ui.Lf() + "\r\n";
				} else {
					modPaginasReturn += " <a class='modlink' " + teclaAcceso + " " + nuevaPag + @" href=""" +
					Variables.App.directorioPortal + "pagina.aspx?id=" + row["idPagina"] + @""">" +
					row["titulo"] + "</a>" + Ui.Lf() + "\r\n";
				}
				modPaginasReturn += "</div>";
			}

			if (Variables.User.Administrador) {
				modPaginasReturn += Ui.Lf() + @"<img border=""0"" src='" + Variables.App.directorioPortal +
				"imagenes/bullet.gif' alt='' /> <a class='modlink' href='" + Variables.App.directorioPortal +
				"admin/editor/showrecord.aspx?tablename=Paginas&amp;add=1&amp;page=1'>Nueva Página</a>" +
				Ui.Lf() + "\r\n";
			}
			return modPaginasReturn;
		}

		public string ModPagina(long id)
		{
			string modPaginaReturn = null;
			bool SoloAdmin = false;
			bool RequiereLogin = false;
			long pagina = 0;
			SoloAdmin = false;
			RequiereLogin = false;

			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

			SelectQueryBuilder sqB = new SelectQueryBuilder();
            sqB.Columns.SelectColumns("valor","propiedad");
            sqB.TableSource = Variables.App.prefijoTablas + "ConfiguracionModulos";

            SimpleWhere w1 = new SimpleWhere(sqB.TableSource, "idModulo", Comparison.Equals, id);
            SimpleWhere w2 = new SimpleWhere(sqB.TableSource, "propiedad", Comparison.Equals, "pagina");
            AndWhere andw = new AndWhere();
            andw.Add(w1);
            andw.Add(w2);
            sqB.Where = andw;
            
            DataTable dtPagina = db.Execute(sqB.BuildQuery());
            
			if (dtPagina.Rows.Count == 0) {
				pagina = -1;
			} else {
				pagina = Convert.ToInt64(NumberUtils.NumberDouble(dtPagina.Rows[0]["valor"]));
			}

			modPaginaReturn = "";
			
			SelectQueryBuilder sqB2 = new SelectQueryBuilder();
            sqB2.Columns.SelectColumns("soloAdmin","requiereLogin","contenido","idPagina");
            sqB2.TableSource = Variables.App.prefijoTablas + "Paginas";

            sqB2.Where = new SimpleWhere(sqB.TableSource, "idPagina", Comparison.Equals, pagina);

            dtPagina = db.Execute(sqB2.BuildQuery());
			
			if (dtPagina.Rows.Count > 0) {
				SoloAdmin = Convert.ToBoolean(dtPagina.Rows[0]["soloAdmin"]);
				RequiereLogin = Convert.ToBoolean(dtPagina.Rows[0]["requiereLogin"]);

				modPaginaReturn += @"<div class=""pagina"" id=""P:" + pagina + @""">";

				modPaginaReturn += Ui.EditPage("Paginas", "IdPagina", dtPagina.Rows[0]["idPagina"].ToString(),
					"Editar Página", "Borrar Página");

				if (FuncionesWeb.IsMobileBrowser() && Functions.Valor(dtPagina.Rows[0]["contenidoMovil"]) != "")
					modPaginaReturn += Functions.Valor(dtPagina.Rows[0]["contenidoMovil"]);
				else
					modPaginaReturn += Functions.Valor(dtPagina.Rows[0]["contenido"]);

				modPaginaReturn += "</div>";
			} else {
				if (pagina == -1) {
					modPaginaReturn += "Propiedad (pagina) del modulo " + id + ", inexistente." + Ui.Lf();
				} else {
					modPaginaReturn += "Página " + pagina + ", inexistente." + Ui.Lf();
				}
			}

			if (RequiereLogin & Variables.User.Usuario.Length <= 0) {
				modPaginaReturn = "";
			}

			if (SoloAdmin & Variables.User.Administrador == false) {
				modPaginaReturn = "";
			}
			return modPaginaReturn;
		}

		public string ModModulos(long id)
		{
			string modModulosReturn = null;
			modModulosReturn = "" + "\r\n";

			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

			SelectQueryBuilder sqB = new SelectQueryBuilder();
            sqB.Columns.SelectColumns("idModulo","titulo");
            sqB.TableSource = Variables.App.prefijoTablas + "Modulos";
            sqB.OrderBy.Add(new FSQueryBuilder.QueryParts.OrderBy.OrderByClause("titulo", Sorting.Ascending));

            DataTable dtModulos = db.Execute(sqB.BuildQuery());
            
			foreach (DataRow row in dtModulos.Rows) {
				modModulosReturn = modModulosReturn +
				Ui.EditPage("modulos", "idmodulo", row["idmodulo"].ToString(), "Editar Módulo",
					"Borrar Módulo") + @"<img border=""0"" src='" + Variables.App.directorioPortal +
				"imagenes/bullet.gif' alt='' /> <a href='" + Variables.App.directorioPortal + "modulo.aspx?id=" +
				row["idModulo"] + "'>" + row["Titulo"] + "</a>" + Ui.Lf() + "\r\n";
			}

			return modModulosReturn;
		}

		public string ModFormularios(long id)
		{
			string modFormulariosReturn = null;
			modFormulariosReturn = "" + "\r\n";

			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

			string ssql = "SELECT idFormulario,nombre from " + Variables.App.prefijoTablas + "Formularios where activo=true";
			DataTable dtFormularios = db.Execute(ssql);

			foreach (DataRow row in dtFormularios.Rows) {
				modFormulariosReturn = modFormulariosReturn + "\r\n" +
				Ui.EditPage("formularios", "IdFormulario", Functions.Valor(row["idFormulario"]),
					"Editar Formulario", "Borrar Formulario");
				modFormulariosReturn = modFormulariosReturn + @"<img border=""0"" src='" + Variables.App.directorioPortal +
				"imagenes/bullet.gif' alt='' /> <a href='" + Variables.App.directorioPortal +
				"formularios/verFormulario.aspx?id=" + Functions.Valor(row["idFormulario"]) + "'>" +
				row["nombre"] + "</a>" + Ui.Lf() + "\r\n";
			}

			if (Variables.User.Administrador) {
				modFormulariosReturn = modFormulariosReturn + Ui.Lf() + @"<img border=""0"" src='" +
				Variables.App.directorioPortal + "imagenes/bullet.gif' alt='' /> <a class='modlink' href='" +
				Variables.App.directorioPortal +
				"admin/editor/showrecord.aspx?tablename=Formularios&amp;add=1&amp;page=1'>Nuevo formulario</a>" +
				Ui.Lf() + "\r\n";
			}

			return modFormulariosReturn;
		}

		public string ModBuscar(long id)
		{
			string result = Ui.Lf() + "<form action='" + Variables.App.directorioPortal +
			"buscar.aspx' name='frmBuscar' id='frmBuscar' onsubmit='return checkField(frmBuscar.txtSearch.value);'>" +
			"\r\n";
			result = result + "<input type='text' name='txtSearch' size='10' />" + Ui.Lf() +
			"<input type='submit' name='cmdSearch' class='botonplano' value='Buscar' />" + Ui.Lf() +
			"\r\n";
			result = result + @"<img border=""0"" src='" + Variables.App.directorioPortal +
			"imagenes/bullet.gif' alt='' /> <a class='modlink' href='" + Variables.App.directorioPortal +
			"buscar.aspx'>Avanzado</a>" + "\r\n";
			result = result + "</form>" + "\r\n";
			return result;
		}

		public string ModContactar(long id)
		{
			string result = @"<form action='" + Variables.App.directorioPortal +
			"formularios/EnviarFormulario.aspx?modo=email' class='frmForm' id='frmContactar' method='post'>" +
			"\r\n";

			result = result + Ui.Lf() + "Nombre:" + Ui.Lf() +
			@"<input type=""text"" size=""18"" name=""{frmCampo(Nombre,50,t,1)}"" />";
			result = result + Ui.Lf() + "E-Mail:" + Ui.Lf() +
			@"<input type=""text"" size=""18"" name=""{frmCampo(Email,100,e,1)}"" />";
			result = result + Ui.Lf() + "Teléfono:" + Ui.Lf() +
			@"<input type=""text"" size=""18"" name=""txtPhone"" />";
			result = result + Ui.Lf() + "Comentarios:" + Ui.Lf() +
			@"<textarea name=""txtAsunto"" cols=""15"" rows=""5""></textarea>";
			result = result + Ui.Lf() + Ui.Lf() +
			@"<input type=""submit"" name=""cmdSend"" value=""Contactar"" />";

			result = result + "</form>" + "\r\n";
			return result;
		}

		public string ModUsuariosOnLine(long id)
		{
			string modUsuariosOnLineReturn = null;
			modUsuariosOnLineReturn = "";

			int iTotalInvitados = 0;


			modUsuariosOnLineReturn = modUsuariosOnLineReturn + "Total invitados: " + iTotalInvitados;
			if (Variables.User.Administrador) {
				modUsuariosOnLineReturn += Ui.Lf();
				modUsuariosOnLineReturn += @"<img border=""0"" src='" + Variables.App.directorioPortal +
				"imagenes/bullet.gif' alt='' /> <a href='" + Variables.App.directorioPortal +
				"usuariosEnlinea.aspx'>Detalle</a>";
			}
			return modUsuariosOnLineReturn;
		}

		public string ModTeclasAcceso(long id)
		{
			string modTeclasAccesoReturn = null;
			string ssql = "SELECT tecladeAcceso,titulo,idPagina FROM " + Variables.App.prefijoTablas +
			                       "paginas Where tecladeAcceso<>'' ORDER BY tecladeAcceso";
			modTeclasAccesoReturn = "";

			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

			DataTable dtTa = db.Execute(ssql);

			if (dtTa.Rows.Count == 0) {
				modTeclasAccesoReturn = modTeclasAccesoReturn + @"No hay páginas con tecla de acceso rápido.";
			} else {
				foreach (DataRow row in dtTa.Rows) {
					modTeclasAccesoReturn = modTeclasAccesoReturn + @"<img border=""0"" src='" + Variables.App.directorioPortal +
					"imagenes/bullet.gif' alt='' />";

					modTeclasAccesoReturn = modTeclasAccesoReturn + "&nbsp;" +
					Ui.EditPage("paginas", "idPagina", row["idPagina"].ToString(),
						"Editar Página", "Borrar Página");

					modTeclasAccesoReturn = modTeclasAccesoReturn + "[" + Functions.Valor(row["teclaDeAcceso"]) + "] - " +
					Functions.Valor(row["titulo"]) + Ui.Lf();
				}

				modTeclasAccesoReturn = modTeclasAccesoReturn + Ui.Lf() + Ui.Lf() +
				@"<font size=""1"">Para acceder a la Página deseada por teclado, utilizar: ALT + teclaDeAcceso + ENTER</font>";
			}

			return modTeclasAccesoReturn;
		}

		public string ModIdioma(long id)
		{
			string modIdiomaReturn = null;
			modIdiomaReturn = "";
			modIdiomaReturn = modIdiomaReturn +
			@"<table width=""100%"" border=""0"" cellspacing=""1"" cellpadding=""4"">" + "\r\n";
			modIdiomaReturn = modIdiomaReturn + "<tr>" + "\r\n";
			modIdiomaReturn = modIdiomaReturn + @"<td width=""50%"" align=""center"">";
			modIdiomaReturn = modIdiomaReturn +
			Ui.Link(Ui.Image("castellano.gif", 0, "") + Ui.Lf() + "Castellano",
				Variables.App.directorioPortal + "cambiaIdioma.aspx?idioma=castellano&amp;comeback=" +
				Variables.User.ComeBack) + "</td>" + "\r\n";
			modIdiomaReturn = modIdiomaReturn + @"<td width=""50%"" align=""center"">";
			modIdiomaReturn = modIdiomaReturn +
			Ui.Link(Ui.Image("ingles.gif", 0, "") + Ui.Lf() + "Ingles",
				Variables.App.directorioPortal + "cambiaIdioma.aspx?idioma=ingles&amp;comeback=" +
				Variables.User.ComeBack) + "</td>" + "\r\n";
			modIdiomaReturn = modIdiomaReturn + "</tr>" + "\r\n";
			modIdiomaReturn = modIdiomaReturn + "<tr>" + "\r\n";
			modIdiomaReturn = modIdiomaReturn + @"<td width=""50%"" align=""center"">";
			modIdiomaReturn = modIdiomaReturn +
			Ui.Link(Ui.Image("euskera.gif", 0, "") + Ui.Lf() + "Euskera",
				Variables.App.directorioPortal + "cambiaIdioma.aspx?idioma=euskera&amp;comeback=" +
				Variables.User.ComeBack) + "</td>" + "\r\n";
			modIdiomaReturn = modIdiomaReturn + @"<td width=""50%"" align=""center"">";
			modIdiomaReturn = modIdiomaReturn +
			Ui.Link(Ui.Image("frances.gif", 0, "") + Ui.Lf() + "Frances",
				Variables.App.directorioPortal + "cambiaIdioma.aspx?idioma=frances&amp;comeback=" +
				Variables.User.ComeBack) + "</td>" + "\r\n";
			modIdiomaReturn = modIdiomaReturn + "</tr>" + "\r\n";
			modIdiomaReturn = modIdiomaReturn + "</table>" + "\r\n";
			return modIdiomaReturn;
		}

		public string ModMapa(long id)
		{
			StringBuilder sb = new StringBuilder("");
			int cat = 0;
			string nuevaPag = null;
			string teclaAcceso = "";

			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);


			if (Web.Request("subir") != "") {
				db.ExecuteNonQuery("update " + Variables.App.prefijoTablas + "paginas set orden=0 where idPagina=" +
				Web.Request("subir") + " and orden is null");
				db.ExecuteNonQuery("update " + Variables.App.prefijoTablas + "paginas set orden=orden+1 where idPagina=" +
				Web.Request("subir"));
			}

			if (Web.Request("bajar") != "") {
				db.ExecuteNonQuery("update " + Variables.App.prefijoTablas + "paginas set orden=0 where idPagina=" +
				Web.Request("bajar") + " and orden is null");
				db.ExecuteNonQuery("update " + Variables.App.prefijoTablas + "paginas set orden=orden-1 where idPagina=" +
				Web.Request("bajar"));
			}


			if (Web.Request("cat") == "") {
				cat = 1;
			} else {
				cat = Web.RequestInt("cat");
			}

			DataTable dtPaginas =
				db.Execute("SELECT idPagina,enlace,titulo,nuevaPagina,teclaDeAcceso FROM " + Variables.App.prefijoTablas +
				"paginas WHERE Padre=" + cat + " ORDER BY orden DESC");

			sb.Append("\r\n" + @"<table width=""75%"" border=""0"" class=""texto"">");

			foreach (DataRow row in dtPaginas.Rows) {
				sb.Append("\r\n" + "<tr><td>");
				sb.Append("\r\n" + @"<img alt="""" border=""0"" src=""" + Variables.App.directorioPortal +
				@"imagenes/arrow_c.gif""/>");
				sb.Append("\r\n" +
				Ui.EditPage("paginas", "idpagina", row["IdPagina"].ToString(), "Editar Página",
					"Borrar Página"));

				string subbaj = "";
				if (Variables.User.Administrador) {
					subbaj = "&nbsp;<a href='?subir=" + row["idPagina"] + @"'><img alt='' border=""0"" src='" +
					Variables.App.directorioPortal + "imagenes/arr.gif' /></a>&nbsp;<a href='?bajar=" + row["idPagina"] +
					@"'><img alt='' border=""0"" src='" + Variables.App.directorioPortal + "imagenes/aba.gif' /></a>";
				}

				if (Functions.Valor(row["teclaDeAcceso"]) != "") {
					teclaAcceso = @"accesskey=""" + Functions.Valor(row["teclaDeAcceso"]) + @"""";
				}

				if (!(FuncionesWeb.TienePaginas(NumberUtils.NumberInt(row["idPagina"])))) {
					nuevaPag = "";
					if (row["enlace"] + "" != "") {
						if (Functions.ValorBool(row["nuevaPagina"]) == false) {
							nuevaPag = "";
						} else {
							nuevaPag = "target='_blank'";
						}
						sb.Append(subbaj + "&nbsp;<a class='modlink' " + teclaAcceso + " " + nuevaPag + @" href=""" +
						FuncionesWeb.Enlace(row["enlace"].ToString()) + @""">" + row["titulo"] + "</a>" + Ui.Lf() +
						"\r\n");
					} else {
						sb.Append(subbaj + "&nbsp;<a class='modlink' " + teclaAcceso + " " + nuevaPag + @" href=""" +
						Variables.App.directorioPortal + "pagina.aspx?id=" + row["idPagina"] + @""">" + row["titulo"] +
						"</a>" + Ui.Lf() + "\r\n");
					}
				} else {
					sb.Append("\r\n" + subbaj + FuncionesWeb.MuestraSubPaginas(NumberUtils.NumberInt(row["idPagina"])));
				}
				sb.Append("\r\n" + "</td>");
				sb.Append("\r\n" + "</tr>");
			}


			sb.Append("\r\n" + "</table>");

			if (Variables.User.Administrador) {
				sb.Append(Ui.Lf() + @"<img border=""0"" src='" + Variables.App.directorioPortal +
				"imagenes/bullet.gif' alt='' /> <a class='modlink' href='" + Variables.App.directorioPortal +
				"admin/editor/showrecord.aspx?tablename=Paginas&amp;add=1&amp;page=1'>Nueva Página</a>" +
				Ui.Lf() + "\r\n");
			}

			if (cat != 1) {
				sb.Append("\r\n" + Ui.Lf() +
				"<img src='../imagenes/bullet.gif' align='middle'> <a href='mapa.aspx'>Volver atrás.</a>");
			}

			return sb.ToString();
		}
	}
}