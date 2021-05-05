// // <fileheader>
// // <copyright file="Parser.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSPortal
// //     Solution: FSPortalNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using FSLibrary;
using FSNetwork;
using FSException;
using System.Runtime.Remoting.Lifetime;
using FSParser;

#endregion

namespace FSPortal
{
	/// <summary>
	///     Clase para la utilización de programación en plantillas.
	/// </summary>
	public class Parser
	{
		/// <summary>
		///     Procesamos la cadena
		/// </summary>
		/// <param name="data"></param>
		/// <param name="rowPos"></param>
		/// <returns></returns>
		public static string Procesa(string data)
		{
			string regEx = @"\{(?<funcion>[a-zA-Z0-9]+)\((?<contenido>[^\{\}]*)\)\}";

			string funcion = "";
			string contenido = "";
			string[] parametros;
			List<string> valoresList = new List<string>();
			Regex reg = new Regex(regEx,
							RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase | RegexOptions.Multiline);


			if (String.IsNullOrEmpty(data))
				return "";

			//Si no hay corchetes, no procesamos nada
			if (data.IndexOf("{") == -1 && data.IndexOf("}") == -1)
			{
				return data;
			}

			//sustituimos las variables antes de procesar la página .....
			data = ReplaceVariables(data);

			try {
				string dataAnt = "";

				while (reg.IsMatch(data)) {
					dataAnt = data;

					Match match = reg.Match(data);

					funcion = match.Groups["funcion"].Value;
					contenido = match.Groups["contenido"].Value;

					valoresList.Clear();
					
					if (contenido != "") {
						//con los simbolos {#}, forzamos a que tengan valores de inicio y fin en los casos como: ",,ssss,sss,"
						contenido = "{#}" + contenido + "{#}";
						string[] valoresFuncion = contenido.Split(',');

						foreach (string s in valoresFuncion)
						{
							valoresList.Add(Procesa(s.Replace("{#}", "")));
						}
					}
					
					parametros = valoresList.ToArray();

					switch (funcion.ToLower())
					{
						case "frmsi":
							data = GuardaSi(data);
							break;
						case "frmrepetir":
							data = GuardaRepetir(data);
							break;
						case "frmsinohaydatos": //Esta función se utiliza para evitar que moficique la url y accedan a un registro que no puede acceder por permisos
							string message = "Acceso no valido"; //devolvemos un mensaje de error si no hay datos
							if (parametros.Length == 1)
								message = parametros[0];
							if (Variables.Parser.data[Variables.Parser.frmDataPos].dataTable == null || Variables.Parser.data[Variables.Parser.frmDataPos].dataTable.Rows.Count == 0)
								throw new Exception(message);
							break;
					}

					string result = ParserFunctions.ProcesaFuncion(funcion, parametros);
					data = reg.Replace(data, result, 1);

					//evitamos entrar en un buble infinito
					if (dataAnt == data) {
						break;
					}
				}
			} catch (System.Exception e) {
				string sErr = "";

				if (Variables.App.Page != null)
				{
					sErr = Ui.EditAll();
					sErr += Ui.Lf();
				}

				sErr += "Error: " + e + Ui.Lf() + Ui.Lf();
				sErr += (e.InnerException != null ? "InnerException: " + e.InnerException + Ui.Lf() + Ui.Lf() : "");
				sErr += "Script(Procesa): " + funcion + Ui.Lf() + Ui.Lf();

				if (contenido != "")
					sErr += "Contenido: " + contenido;

				sErr = Web.FormatHTML(sErr);
				sErr = ReplaceVariables(sErr);

				if (Http.IsLocalhost() || Variables.User.Administrador)
					return sErr;
				else
					throw new ExceptionUtil("Ha ocurrido un problema al procesar la página. Por favor, inténtelo más tarde.", e);
			}

			//... y después de procesar la página
			data = ReplaceVariables(data);

			if (Regex.IsMatch(data, regEx))
			{
				data = Procesa(data);
			}

			data = RestoreScriptCodes(data);

			return data;
		}

		public static void IniciarFormulario()
		{
			Variables.Parser.variable.Clear();
			Variables.Parser.contenidoRepite.Clear();
			Variables.Parser.contenidoSi.Clear();
			Variables.Parser.data.Clear();

			Variables.Parser.frmModo = Variables.FormMod.Nada;
			Variables.Parser.frmEmailTo = "";
			Variables.Parser.frmEmailSubject = "";
			Variables.Parser.frmRequest = "";
			Variables.Parser.frmPaginas = 0;
			Variables.Parser.frmPaginaActual = 0;
			Variables.Parser.frmFileMaxSize = 100;
			Variables.Parser.frmFileTypes = "jpg,gif,bmp,jpeg,png";
			Variables.Parser.frmFileUploadPath = "";
			Variables.Parser.frmMensajeSinRegistros = "No hay registros.";
			Variables.Parser.frmMensajeCombo = "Selecciona ...";
			Variables.Parser.frmComboOnChange = "";
			Variables.Parser.frmComboSelected = "";
			Variables.Parser.frmRedirige = "";
			Variables.Parser.frmTruncar = true;
			Variables.Parser.frmMensajeOK = "Formulario procesado correctamente.";
			Variables.Parser.frmMensajeNoOK = "Problemas al procesar el formulario.";
			Variables.Parser.frmVolverAtras = @"<a href=""javascript:history.back()"">Volver atrás</a>";
			Variables.Parser.frmIdentity = 0;
			Variables.Parser.frmRandom = false;
			Variables.Parser.frmCaptcha = false;
		}

		private static string ReplaceVariables(string data)
		{
			data = TextUtil.Replace(data, "{c}", @"""");
			data = TextUtil.Replace(data, "{cs}", "'");
			data = TextUtil.Replace(data, "{coma}", "&#44;");
			data = TextUtil.Replace(data, "{directorioportal}", Variables.App.directorioPortal);
			data = TextUtil.Replace(data, "{directorioweb}", Variables.App.directorioWeb);
			data = TextUtil.Replace(data, "{nombreWeb}", Variables.App.nombreWeb);
			data = TextUtil.Replace(data, "{fecha}", FSLibrary.DateTimeUtil.ShortDate(System.DateTime.Now));
			data = TextUtil.Replace(data, "{fechauno}", new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1).ToShortDateString());
			data = TextUtil.Replace(data, "{fecha31}", new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.DaysInMonth(System.DateTime.Now.Year, System.DateTime.Now.Month)).ToShortDateString());
			data = TextUtil.Replace(data, "{fechacorta}", FSLibrary.DateTimeUtil.ShortDate(System.DateTime.Now));
			data = TextUtil.Replace(data, "{fechalarga}", FSLibrary.DateTimeUtil.LongDate(System.DateTime.Now));
			data = TextUtil.Replace(data, "{hora}", System.DateTime.Now.ToShortTimeString());
			data = TextUtil.Replace(data, "{horalarga}", System.DateTime.Now.ToLongTimeString());
			data = TextUtil.Replace(data, "{nombredia}", FSLibrary.DateTimeUtil.NombreDia(System.DateTime.Now.DayOfWeek));
			data = TextUtil.Replace(data, "{nombremes}", FSLibrary.DateTimeUtil.NombreMes(System.DateTime.Now.Month));
			data = TextUtil.Replace(data, "{hora}", System.DateTime.Now.ToShortTimeString());
			data = TextUtil.Replace(data, "{horalarga}", System.DateTime.Now.ToLongTimeString());
			data = TextUtil.Replace(data, "{ultimaconexion}", Variables.User.ultimaConexion);
			data = TextUtil.Replace(data, "{usuariosactivos}", Variables.App.Page.Application["NumberOfUsers"].ToString());
			data = TextUtil.Replace(data, "{contadoraplicaciones}", Variables.App.Page.Application["ApplicationCounter"].ToString());
			data = TextUtil.Replace(data, "{inicioservidor}", Variables.App.Page.Application["ServerStartTime"].ToString());
			data = TextUtil.Replace(data, "{webhttp}", Variables.App.webHttp);
			data = TextUtil.Replace(data, "{correoinfo}", Variables.App.correoInfo);
			data = TextUtil.Replace(data, "{provincia}", Variables.App.provincia);
			data = TextUtil.Replace(data, "{direccionfisica}", Variables.App.direccionFisica);
			data = TextUtil.Replace(data, "{codigopostal}", Variables.App.codigoPostal);
			data = TextUtil.Replace(data, "{correobienvenida}", Variables.App.correoBienvenida.ToString());
			data = TextUtil.Replace(data, "{usuarioid}", Variables.User.UsuarioId.ToString());
			data = TextUtil.Replace(data, "{usuario}", (Variables.User.Usuario == "" ? "Usuario Invitado" : Variables.User.Usuario));
			data = TextUtil.Replace(data, "{usuariolink}",
				(Variables.User.Usuario == ""
					? Ui.Link("Usuario Invitado", Variables.App.paginaLogin)
					: Ui.Link(TextUtil.Left(Variables.User.Usuario, 25), Variables.App.paginaPerfil)));
			data = TextUtil.Replace(data, "{nombrecompleto}", Variables.User.NombreCompleto);
			data = TextUtil.Replace(data, "{paginaconectar}", Variables.App.paginaLogin);
			data = TextUtil.Replace(data, "{administrador}", Variables.User.Administrador.ToString().ToLower());
			data = TextUtil.Replace(data, "{paginalogin}", Variables.App.paginaLogin);
			data = TextUtil.Replace(data, "{paginarecordar}", Variables.App.paginaRecordar);
			data = TextUtil.Replace(data, "{paginaregistro}", Variables.App.paginaRegistro);
			data = TextUtil.Replace(data, "{paginaperfil}", Variables.App.paginaPerfil);
			data = TextUtil.Replace(data, "{jquerytema}", Variables.App.jqueryTema);
			data = TextUtil.Replace(data, "{comebackto}", Variables.User.ComeBackTo);
			data = TextUtil.Replace(data, "{sessionid}", Variables.User.sessionID);
			data = TextUtil.Replace(data, "{htmlEditor}", Variables.App.htmlEditor.ToString());
			if (TextUtil.IndexOf(data, "{ip}") != -1)
				data = TextUtil.Replace(data, "{ip}", Http.IpAddress());
			if (TextUtil.IndexOf(data, "{pais}") != -1)
				data = TextUtil.Replace(data, "{pais}", FuncionesWeb.GetCountryName(Net.Dot2LongIP(Http.IpAddress())).ToLower());

			data = TextUtil.Replace(data, "{frmpaginaactual}", (Variables.Parser.frmPaginaActual + 1).ToString());
			data = TextUtil.Replace(data, "{frmpaginas}", (Variables.Parser.frmPaginas + 1).ToString());
			data = TextUtil.Replace(data, "{frmtotalregistros}", Variables.Parser.frmTotalRegistros.ToString());

			data = TextUtil.Replace(data, "{usuariosconectados}", Variables.Parser.frmPaginas.ToString());
			data = TextUtil.Replace(data, @"selection=""checked""", @"checked=""checked""");
			data = TextUtil.Replace(data, @"selection=""selected""", @"selected=""selected""");
			data = TextUtil.Replace(data, @"selection=""""", "");
			data = TextUtil.Replace(data, "{tiempocarga}", ((System.DateTime.Now.Ticks - Variables.App.Page.startTime) / 10000).ToString());

			return data;
		}


		/// <summary>
		/// Guarda el contenido entre el comando frmRepetir() y frmFinRepetir()
		/// </summary>
		/// <param name="cadena"></param>
		/// <returns></returns>
		private static string GuardaRepetir(string cadena)
		{
			string data;
			int id = 0;

			if (TextUtil.IndexOf(cadena, "frmRepetir") == -1)
				return cadena;

			Variables.Parser.contenidoRepite.Clear();

			try {
				Regex regRepetir =
					new Regex(
						@"\{frmRepetir\((?<id>[0-9]+)?\)\}(?<repetir>((?!frmRepetir).)*)\{frmFinRepetir\((?<id>[0-9]+)?\)\}",
						RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase | RegexOptions.Singleline);

				MatchCollection matches = regRepetir.Matches(cadena);

				foreach (Match match in matches) {
					data = match.Groups["repetir"].Value;
					if(match.Groups["id"].Value != "")
						id = Convert.ToInt32(match.Groups["id"].Value);

					//guardamos el contenido a repetir
					ParserRepeat parserRepeat = new ParserRepeat();
					parserRepeat.id = id;
					parserRepeat.data = data;
					Variables.Parser.contenidoRepite.Add(parserRepeat);

					cadena = TextUtil.Replace(cadena, data, "");
				}
			} catch (System.Exception e) {
				throw new ExceptionUtil("Error: " + e + ", Script(GuardaRepetir): frmRepetir");
			}

			return cadena;
		}

		/// <summary>
		/// Guarda el contenido entre el comando frmSi() y frmFinSi()
		/// </summary>
		/// <param name="cadena"></param>
		/// <returns></returns>
		private static string GuardaSi(string cadena)
		{
			string datasi;
			string datano;
			string condicion;

			if (TextUtil.IndexOf(cadena, "frmSi") == -1)
				return cadena;

			Variables.Parser.contenidoSi.Clear();

			try
			{
				Regex regSi =
					new Regex(
						@"\{frmSi\((?<condicion>([^\{\}]*))\)\}(?<bloquesi>((?!frmSi).)*)\{frmSiNo\(\)\}(?<bloqueno>((?!frmSi).)*)\{frmFinSi\(\)\}",
						RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase | RegexOptions.Singleline);

				MatchCollection matches = regSi.Matches(cadena);

				foreach (Match match in matches)
				{
					datasi = match.Groups["bloquesi"].Value;
					datano = match.Groups["bloqueno"].Value;
					condicion = match.Groups["condicion"].Value;

					//guardamos el contenido del Si
					ParserYesNo parseryesno = new ParserYesNo();
					parseryesno.condition = condicion;
					parseryesno.datayes = datasi;
					parseryesno.datano = datano;
					Variables.Parser.contenidoSi.Add(parseryesno);

					cadena = TextUtil.Replace(cadena, datasi, "");
					cadena = TextUtil.Replace(cadena, datano, "");
				}
			}
			catch (System.Exception e)
			{
				throw new ExceptionUtil("Error: " + e + ", Script(GuardaSi): frmSi");
			}

			return cadena;
		}

		/// <summary>
		/// Reemplazamos los corchetes para evitar que sea procesado por la función "Procesa".
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static string SaveScriptCodes(string data)
		{
			return data.Replace("{", "@[@").Replace("}", "@]@");
		}

		/// <summary>
		/// Restauramos los corchetes.
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static string RestoreScriptCodes(string data)
		{
			return data.Replace("@[@", "{").Replace("@]@", "}");
		}
	}
}