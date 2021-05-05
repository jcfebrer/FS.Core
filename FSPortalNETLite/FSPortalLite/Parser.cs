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
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

#endregion

namespace FSPortalLite
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
            string regEx = @"\{(?<comando>[a-zA-Z0-9]+)\((?<contenido>[^\{\}]*)\)\}";

            if (data == null || data == "") return "";

			if (data.IndexOf("{") == -1 && data.IndexOf("}") == -1)
			{
				return data;
			}

			data = CommandosPre(data);

			data = GuardaRepetir(data);

			Random rnd = new Random();
			string comm = "";
			string cont = "";
			string[] valores;
			List<string> valoresList = new List<string>();
			Regex reg = new Regex(regEx,
							RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase | RegexOptions.Multiline);

			try
			{
				string dataAnt = "";

				while (reg.IsMatch(data))
				{
					dataAnt = data;

					Match match = reg.Match(data);

					comm = match.Groups["comando"].Value;
					cont = match.Groups["contenido"].Value;

					cont += "";
					valoresList.Clear();

					if (cont != "")
					{
						//con los simbolos {#}, forzamos a que tengan valores de inicio y fin en los casos como: ",,ssss,sss,"
						cont = "{#}" + cont + "{#}";
						string[] valoresFuncion = cont.Split(',');

						foreach (string s in valoresFuncion)
						{
							valoresList.Add(Procesa(s.Replace("{#}", "")));
						}
						cont = cont.Replace("{#}", "");
					}

					valores = valoresList.ToArray();

                    switch (comm.ToLower())
                    {
                        case "frmcampo":
                            int tamano = 0;
                            string obligatorio = "0";
                            string valor2 = "t";
                            Type t = Type.GetType("System.String");

                            if (valores.Length == 0)
                            {
                                data = reg.Replace(data, "Parametros incorrectos en función: frmCampo. Parámetros: 1.- [campo], [t|e|u|n|b|f]", 1);
                            }
                            else
                            {
                                data = reg.Replace(data, valores[0], 1);
                                //data = reg.Replace(data, valores[0], 1);

                                if (valores.Length > 1)
                                {
                                    tamano = Convert.ToInt32(valores[1]);
                                }

                                if (valores.Length > 2)
                                {
                                    valor2 = valores[2];

                                    switch (valor2)
                                    {
                                        case "t":
                                        case "e":
                                        case "u":
                                            t = Type.GetType("System.String");
                                            break;
                                        case "n":
                                            t = Type.GetType("System.Int64");
                                            break;
                                        case "b":
                                            t = Type.GetType("System.Boolean");
                                            break;
                                        case "f":
                                            t = Type.GetType("System.DateTime");
                                            break;
                                    }
                                }

                                if (valores.Length > 3)
                                {
                                    obligatorio = valores[3];
                                }
                            }
                            break;

                        case "frmconn":
                            data = reg.Replace(data, "", 1);
                            Variables.User.frmConn = cont;
                            break;

                        case "frmcontenido":
                            data = reg.Replace(data, "El comando frmContenido() solo puede utilizarse en la plantilla.",
                                1);
                            break;

                        case "frmendsi":
                            if (valores[0] == valores[1])
                            {
                                FuncionesWeb.ClearVariables();
                                data = "";
                            }
                            else
                            {
                                data = reg.Replace(data, "", 1);
                            }
                            break;

                        case "frmes":
                            string sino = "";

                            if (valores.Length == 4)
                            {
                                sino = valores[3];
                            }

                            data = reg.Replace(data,
                                DameComparaValorTabla(Variables.User.frmTabla, valores[0], valores[1],
                                    valores[2], sino, false), 1);
                            break;

                        case "frmespar":
                        case "frmesfilapar":
                            if (Variables.User.frmPosicion % 2 == 0)
                                data = reg.Replace(data, valores[0], 1);
                            else
                                data = reg.Replace(data, "", 1);

                            break;

                        case "frmfila":
                            data = reg.Replace(data, Variables.User.frmPosicion.ToString(), 1);
                            break;

                        case "frmfilexists":
                            if(File.Exists(cont))
                                data = reg.Replace(data, cont, 1);
                            else
                                data = reg.Replace(data, "", 1);
                            break;

                        case "frmfiletypes":
                            data = reg.Replace(data, "", 1);
                            Variables.User.frmFileTypes = cont;
                            break;

                        case "frmfinrepetir":
                            data = reg.Replace(data, "", 1);
                            break;
                        case "frmguardaselect":
                            data = reg.Replace(data, "", 1);
                            Variables.User.frmDataTemp = Variables.User.frmData;
                            break;

                        case "frminiciar":
                            data = reg.Replace(data, "", 1);
                            IniciarFormulario();
                            break;

                        case "frmlinkanterior":
                            data = reg.Replace(data, Variables.User.frmLinkAnterior, 1);
                            break;

                        case "frmlinksiguiente":
                            data = reg.Replace(data, Variables.User.frmLinkSiguiente, 1);
                            break;

                        case "frmlinkprimero":
                            data = reg.Replace(data, Variables.User.frmLinkPrimero, 1);
                            break;

                        case "frmlinkultimo":
                            data = reg.Replace(data, Variables.User.frmLinkUltimo, 1);
                            break;

                        case "frmlinktoolbar":
                            data = reg.Replace(data, LinkToolbar(cont), 1);
                            break;

                        case "frmlogin":
                            data = reg.Replace(data, "", 1);
                            Variables.User.frmModo = VariablesUsuario.FormMod.Login;
                            Variables.User.frmEmailTo = "";
                            break;

                        case "frmmaxregistrospagina":
                            data = reg.Replace(data, "", 1);
                            Variables.User.frmMaxRegistrosPagina = Convert.ToInt32(cont);
                            break;

                        case "frmpaginas":
                            data = reg.Replace(data, Variables.User.frmPaginas.ToString(), 1);
                            break;

                        case "frmpaginaactual":
                            data = reg.Replace(data, Variables.User.frmPaginaActual.ToString(), 1);
                            break;

                        case "frmpalabrasclave":
                            data = reg.Replace(data, Variables.App.palabrasClave, 1);
                            break;

                        case "frmposicion":
                            if (cont != "")
                            {
                                data = reg.Replace(data, "", 1);
                                Variables.User.frmPosicion = Convert.ToInt64(cont);
                            }
                            else
                            {
                                data = reg.Replace(data, (Variables.User.frmPosicion + 1).ToString(), 1);
                            }

                            break;

                        case "frmreemplaza":
                            string reemp;

                            if (valores.Length == 3)
                            {
                                string reemStr = Procesa(valores[0]);
                                reemp = reemStr.Replace(valores[1], valores[2]);
                                data = reg.Replace(data, reemp, 1);
                            }
                            else
                            {
                                data = reg.Replace(data, "Parametros incorrectos en función: frmReemplaza. Parámetros: [cadena], [valor1], [valor2]", 1);
                            }
                            break;

                        case "frmrepetir":
                            string idRepetir = "";
                            if (valores.Length == 1)
                                idRepetir = valores[0];

                            data = reg.Replace(data, ProcesaRepetidos(data, idRepetir), 1);

                            break;

                        case "frmrequest":
                            switch (valores.Length)
                            {
                                case 0:
                                    data = reg.Replace(data, Funciones.RequestQueryForm(), 1);
                                    break;
                                case 1:
                                    string vr = Funciones.Request(valores[0]);
                                    vr = Procesa(vr);
                                    data = reg.Replace(data, vr, 1);
                                    Variables.User.frmRequest = cont + "=" + vr;
                                    break;
                            }
                            break;

                        case "frmrequestdel":
                            data = reg.Replace(data, Funciones.RequestQueryForm(cont), 1);
                            break;

                        case "frmrequestentero":
                        case "frmrequesti":
                            int vri = Funciones.RequestInt(cont);
                            data = reg.Replace(data, vri.ToString(), 1);
                            Variables.User.frmRequest = cont + "=" + vri.ToString();
                            break;

                        case "frmrequestfecha":
                            string dtime = Funciones.RequestDate(cont);
                            data = reg.Replace(data, dtime, 1);
                            Variables.User.frmRequest = cont + "=" + dtime;
                            break;

                        case "frmselect":
                            try
                            {
                                if (cont == "")
                                {
                                    data = reg.Replace(data, Variables.User.frmSelect, 1);
                                }
                                else
                                {
                                    data = reg.Replace(data, cont, 1);

                                    if (HttpContext.Current.Request["pag"] == null || Variables.User.frmSelect != cont)
                                    {
                                        Variables.User.frmPosicion = 0;
                                        Variables.User.frmLinkAnterior = "#";
                                        Variables.User.frmLinkSiguiente = "#";
                                        Variables.User.frmModo = VariablesUsuario.FormMod.Nada;
                                        Variables.User.frmSelect = cont;

                                        LoadSelect(cont);
                                    }
                                }
                            }
                            catch (System.Exception eSelect)
                            {
                                data = reg.Replace(data, eSelect.Message, 1);
                            }
                            break;

                        case "frmsii":
                            switch (valores.Length)
                            {
                                case 4:
                                    data = reg.Replace(data,
                                        DameComparaValor(valores[0], valores[1], valores[2], valores[3]), 1);
                                    break;
                                default:
                                    data = reg.Replace(data, "Parametros incorrectos en función: frmSii. Parámetros: [cadena], [valor], [valorSi], [valorNo]", 1);
                                    break;
                            }
                            break;

                        case "frmsinohaydatos":
                            if (Variables.User.frmData == null || Variables.User.frmData.Rows.Count == 0)
                            {
                                //devolvemos una página en blanco si no hay datos
                                //data = "[ERROR: NO DATA]";
                                throw new Exception("Acceso no válido");
                            }
                            data = reg.Replace(data, "", 1);
                            break;

                        case "frmtitulopagina":
                            if (cont != "")
                            {
                                Variables.App.Page.tituloPagina = cont;
                            }
                            data = reg.Replace(data, "#frmTituloPagina#", 1);

                            break;

                        case "frmusuario":
                        case "usuario":
                            try
                            {
                                if (Variables.User.UserData != null)
                                    data = reg.Replace(data, Variables.User.UserData[valores[0]].ToString(), 1);
                                else
                                    data = reg.Replace(data, "Usuario no validado en el portal", 1);
                            }
                            catch (System.Exception e)
                            {
                                data = reg.Replace(data, e.Message, 1);
                            }

                            break;
                        case "frmvalor":
                            if (valores.Length == 1)
                            {
                                if (Variables.User.frmTabla == "")
                                    data = reg.Replace(data, Procesa(DameValorTabla(valores[0], 0)), 1);
                                else
                                    data = reg.Replace(data, Procesa(DameValorTabla(Variables.User.frmTabla, valores[0], 0)),
                                        1);
                            }
                            else
                            {
                                if (Variables.User.frmTabla == "")
                                    data = reg.Replace(data,
                                        Procesa(DameValorTabla(valores[0], Convert.ToInt32(valores[1]))), 1);
                                else
                                    data = reg.Replace(data,
                                        Procesa(DameValorTabla(Variables.User.frmTabla, valores[0],
                                            Convert.ToInt32(valores[1]))), 1);
                            }
                            break;
                        case "frmvalornext":
                            if (valores.Length == 1)
                            {
                                data = reg.Replace(data, Procesa(DameValorTabla(valores[0], 0, (int)Variables.User.frmPosicion + 1)), 1);
                            }
                            break;
                        case "frmvalorprev":
                            if (valores.Length == 1)
                            {
                                data = reg.Replace(data, Procesa(DameValorTabla(valores[0], 0, (int)Variables.User.frmPosicion - 1)), 1);
                            }
                            break;
                        case "frmvalortempnext":
                            string valorNext = "";
                            if (Variables.User.frmDataTemp != null && valores.Length == 1)
                            {
                                Variables.User.frmPosicionTemp = DamePosicionTemp(valores[0]);
                                int posNext = (int)Variables.User.frmPosicionTemp + 1;
                                if (posNext >= Variables.User.frmDataTemp.Rows.Count)
                                    posNext = Variables.User.frmDataTemp.Rows.Count - 1;
                                valorNext = Variables.User.frmDataTemp.Rows[posNext][valores[0]].ToString(); 
                            }
                            data = reg.Replace(data, valorNext, 1);
                            break;
                        case "frmvalortempprev":
                            string valorPrev = "";
                            if (Variables.User.frmDataTemp != null && valores.Length == 1)
                            {
                                Variables.User.frmPosicionTemp = DamePosicionTemp(valores[0]);
                                int posPrev = (int)Variables.User.frmPosicionTemp - 1;
                                if (posPrev < 0)
                                    posPrev = 0;
                                valorPrev = Variables.User.frmDataTemp.Rows[posPrev][valores[0]].ToString();  
                            }
                            data = reg.Replace(data, valorPrev, 1);
                            break;
                        case "frmvalortabla":
							switch (valores.Length)
							{
								case 1:
									data = reg.Replace(data, Procesa(DameValorTabla(valores[0], 0)), 1);
									break;
								case 2:
									data = reg.Replace(data, Procesa(DameValorTabla(valores[0], valores[1], 0)), 1);
									break;
								case 3:
									data = reg.Replace(data,
										Procesa(DameValorTabla(valores[0], valores[1], Convert.ToInt32(valores[2]))),
										1);
									break;
								default:
									data = reg.Replace(data, "Parametros incorrectos en función: frmValorTabla. Parámetros: 1.- [campo] \n 2.- [tabla], [campo] \n 3.- [tabla], [campo], [tamaño max.]", 1);
									break;
							}
							break;
						case "frmvariable":
							if (valores.Length == 2)
							{
								Variables.User.frmVariable[Convert.ToInt32(valores[0])] = valores[1];
								data = reg.Replace(data, "", 1);
							}
							else
							{
								data = reg.Replace(data, Variables.User.frmVariable[Convert.ToInt32(valores[0])], 1);
							}
							break;
						case "frmvolveratras":
							if (valores.Length == 2)
							{
								Variables.User.frmVolverAtras = valores[0];
							}
							else
								data = reg.Replace(data, Variables.User.frmVolverAtras, 1);
							break;
						default:
							data = reg.Replace(data, "Comando inexistente: " + comm + "(" + cont + ")", 1);
							break;
					}

					if (dataAnt == data)
					{
						break;
					}
				}
			}
			catch (System.Exception e)
			{
				throw new Exception("Ha ocurrido un problema al procesar la página. Por favor, inténtelo más tarde.", e);
			}

			data = ComandosPost(data);

            if (Regex.IsMatch(data, regEx))
            {
                data = Procesa(data);
            }

            data = RestoreScriptCodes(data);

			return data;
		}

        private static int DamePosicionTemp(string campo)
        {
            if (Variables.User.frmDataTemp == null)
                return 0;

            if (Variables.User.frmData == null)
                return 0;

            if (Variables.User.frmData.Rows.Count == 0)
                return 0;

            string valoractual = Variables.User.frmData.Rows[(int)Variables.User.frmPosicion][campo].ToString();

            int f = 0;
            foreach (DataRow dr in Variables.User.frmDataTemp.Rows)
            {
                if (dr[campo].ToString() == valoractual)
                    return f;
                f++;
            }
            return f;
        }


		public static void IniciarFormulario()
		{
			Variables.User.frmData = null;
			Variables.User.frmTabla = "";
			Variables.User.frmSeleccion = "";
			Variables.User.frmSelect = "";
			Variables.User.frmOrden = "";
			Variables.User.frmEmailTo = "";
			Variables.User.frmEmailSubject = "";
			Variables.User.frmPosicion = 0;
			Variables.User.frmRequest = "";
			Variables.User.frmPaginas = 0;
            Variables.User.frmTotalRegistros = 0;
			Variables.User.frmPaginaActual = 0;
			Variables.User.frmLinkPrimero = "";
			Variables.User.frmLinkUltimo = "";
			Variables.User.frmLinkSiguiente = "";
			Variables.User.frmLinkAnterior = "";
			Variables.User.frmFileMaxSize = 100;
			Variables.User.frmFileTypes = "jpg,gif,bmp,jpeg,png";
			Variables.User.frmFileUploadPath = "";
			Variables.User.frmMensajeSinRegistros = "No hay registros.";
			Variables.User.frmMensajeCombo = "Selecciona ...";
			Variables.User.frmComboOnChange = "";
			Variables.User.frmComboSelected = "";
			Variables.User.frmRedirige = "";
			Variables.User.frmTruncar = true;
			Variables.User.frmMensajeOK = "Formulario procesado correctamente.";
			Variables.User.frmMensajeNoOK = "Problemas al procesar el formulario.";
			Variables.User.frmVolverAtras = @"<a href=""javascript:history.back()"">Volver atrás</a>";
			Variables.User.frmIdentity = 0;
			Variables.User.frmVariable.Initialize();
		}

		private static void LoadSelect(string select)
		{
			LoadSelect(select, Variables.User.frmConn);
		}

		private static void LoadSelect(string select, string connString)
		{
			Variables.User.frmData = Funciones.Execute(connString, select);
		}

		private static void LoadData(string tabla)
		{
			LoadData(tabla, Variables.User.frmConn);
		}

		private static void LoadData(string tabla, string connString)
		{
			string ssql = "select * from " + tabla;
			if (Variables.User.frmSeleccion != "")
			{
				ssql = ssql + " WHERE " + Variables.User.frmSeleccion;
			}

			if (Variables.User.frmOrden != "")
			{
				ssql = ssql + " ORDER BY " + Variables.User.frmOrden;
			}


			Variables.User.frmData = Funciones.Execute(connString, ssql);
		}

		private static string ComandosPost(string cad)
		{
			cad = cad.Replace("{frmpaginaactual}", Variables.User.frmPaginaActual.ToString());
			cad = cad.Replace("{frmpaginas}", Variables.User.frmPaginas.ToString());
            cad = cad.Replace("{frmtotalregistros}", Variables.User.frmTotalRegistros.ToString());
            cad = cad.Replace(@"selection=""checked""", @"checked=""checked""");
			cad = cad.Replace(@"selection=""selected""", @"selected=""selected""");
			cad = cad.Replace(@"selection=""""", "");

            return cad;
		}

		private static string CommandosPre(string cad)
		{
			cad = cad.Replace("{c}", @"""");
			cad = cad.Replace("{cs}", "'");
			cad = cad.Replace("{coma}", "&#44;");
			cad = cad.Replace("{directorioportal}", Variables.App.directorioPortal);
			cad = cad.Replace("{directorioweb}", Variables.App.directorioWeb);
			cad = cad.Replace("{directorioPortal}", Variables.App.directorioPortal);
			cad = cad.Replace("{directorioWeb}", Variables.App.directorioWeb);
			cad = cad.Replace("{nombreWeb}", Variables.App.nombreWeb);
			cad = cad.Replace("{fecha}", System.DateTime.Now.ToShortDateString());
            cad = cad.Replace("{fechauno}", new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1).ToShortDateString());
            cad = cad.Replace("{fechacorta}", System.DateTime.Now.ToShortDateString());
			cad = cad.Replace("{fechalarga}", System.DateTime.Now.ToLongDateString());
			cad = cad.Replace("{hora}", System.DateTime.Now.ToShortTimeString());
			cad = cad.Replace("{horalarga}", System.DateTime.Now.ToLongTimeString());
			cad = cad.Replace("{webhttp}", Variables.App.webHttp);
			cad = cad.Replace("{usuario}", (Variables.User.Usuario == "" ? "Usuario Invitado" : Variables.User.Usuario));
			cad = cad.Replace("{paginaconectar}", Variables.App.paginaLogin);
			cad = cad.Replace("{administrador}", Variables.User.Administrador.ToString().ToLower());
			cad = cad.Replace("{paginalogin}", Variables.App.paginaLogin);
			cad = cad.Replace("{sessionid}", Variables.User.sessionID);

			return cad;
		}


		/// <summary>
		/// </summary>
		/// <param name="cadena"></param>
		/// <returns></returns>
		private static string GuardaRepetir(string cadena)
		{
			string data;
			string id;

			if (cadena.IndexOf("frmRepetir") <= 0)
				return cadena;

            Variables.User.contenidoRepite.Clear();

            try
			{
				Regex regRepetir =
					new Regex(
						@"\{frmRepetir\((?<id>\w*)\)\}(?<repetir>((?!frmRepetir).)*)\{frmFinRepetir\((?<id>\w*)\)\}",
						RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase | RegexOptions.Singleline);

				MatchCollection matches = regRepetir.Matches(cadena);

				foreach (Match match in matches)
				{
					data = match.Groups["repetir"].Value;
					id = match.Groups["id"].Value;

					//guardamos el contenido a repetir
					if (!Variables.User.contenidoRepite.ContainsKey(id))
					{
                        Variables.User.contenidoRepite.Add(id, data);
					}

					cadena = cadena.Replace(data, "");
				}
			}
			catch (System.Exception e)
			{
				throw new Exception("Error: " + e + ", Script(GuardaRepetir): frmRepetir");
			}

			return cadena;
		}

        /// <summary>
        /// Devuelve una cadena con los links a cada una de las páginas
        /// </summary>
        /// <param name="pattern">Cadena que se repetira con las páginas</param>
        /// <param name="pattern">Página actual</param>
        /// <returns></returns>
        private static string LinkToolbar(string pattern)
        {
            string link = "";
            int pagActual = Convert.ToInt32(HttpContext.Current.Request["pag"]);
            int pagFinal = (pagActual + 10);
            if (pagFinal > Variables.User.frmPaginas)
                pagFinal = Variables.User.frmPaginas;

            int pagInicial = (pagActual - 10);

            if ((pagFinal - pagInicial) < 10)
                pagInicial = pagFinal - 10;

            if (pagInicial < 0)
                pagInicial = 0;

            string args = Procesa("{frmRequest()}");
            //eliminamos los "pag=xxx&" existentes
            Regex regex = new Regex(@"pag=([0-9]+)\&", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            args = regex.Replace(args, string.Empty);

            for (int f = pagInicial; f < pagFinal; f++)
            {
                string pat = pattern;

                pat = pat.Replace("$1", "?pag=" + f.ToString() + "&" + args);
                if (f == pagActual)
                    pat = pat.Replace("$2", "[" + (f + 1).ToString() + "]");
                else
                    pat = pat.Replace("$2", (f + 1).ToString());

                link += pat;
            }

            return link;
        }


		/// <summary>
		/// Muestra una caja de texto para ir a una página concreta
		/// </summary>
		/// <returns></returns>
		private static string LinkInputToolbar()
		{
			string link = "<form action='?pag=" + "&" + Procesa("{frmRequest()}") + "'><input size=3 type='text'><input type='submit'></form>";
			return link;
		}


		private static string ProcesaRepetidos(string cadena, string id)
		{
			if (Variables.User.frmData == null)
				return "";
            if (Variables.User.contenidoRepite == null)
                return "";
            if (Variables.User.contenidoRepite.Count == 0)
                return "";

			int pag = Convert.ToInt32(HttpContext.Current.Request["pag"]);

			long lrecs = 0;
			long totPag = 0;
			long rp = 0;
			StringBuilder totRegistro = new StringBuilder("");
			string q = "";
			int maxRegs = Variables.User.frmMaxRegistrosPagina;

			if (!Variables.User.contenidoRepite.ContainsKey(id))
			{
				throw new Exception("Error: No existe información para procesar en función 'frmRepetir(" + id + ")'.");
			}

			try
			{
				lrecs = Variables.User.frmData.Rows.Count;

				if (maxRegs == 0)
					maxRegs = Variables.App.registrosPorPagina;

				totPag = lrecs / maxRegs;
				if (lrecs % maxRegs > 0)
				{
					totPag = totPag + 1;
				}

				if (pag > totPag)
					pag = Convert.ToInt32(totPag) - 1;

				rp = (maxRegs * pag);

				while (rp < lrecs)
				{
					Variables.User.frmPosicion = rp;

					totRegistro.Append(
						Procesa(
							Procesa(
                                Variables.User.contenidoRepite[id]
							)));

					rp++;

					if ((rp % maxRegs == 0))
					{
						break;
					}
				}

				q = Funciones.RequestQueryForm();
				q = Funciones.ReplaceREG(q, @"p=([\w\+ ]*)", ""); // quitamos el parámetro p=xxxx si existiera.
				q = Funciones.ReplaceREG(q, @"pag=(\d+)", ""); // quitamos el parámetro pag=xxxx si existiera.

				q = Funciones.ReorgQuery(q);

				Variables.User.frmLinkAnterior = "#";
				Variables.User.frmLinkSiguiente = "#";

				Variables.User.frmLinkPrimero = "?pag=0";
				if (q != "")
					Variables.User.frmLinkPrimero += "&" + q;

				if (pag > 0)
				{
					Variables.User.frmLinkAnterior = "?pag=" + (pag - 1);
					if (q != "")
						Variables.User.frmLinkAnterior += "&" + q;
				}

				if (pag + 1 < totPag)
				{
					Variables.User.frmLinkSiguiente = "?pag=" + (pag + 1);
					if (q != "")
						Variables.User.frmLinkSiguiente += "&" + q;
				}

				if (lrecs == 0)
				{
					totRegistro.Append(Variables.User.frmMensajeSinRegistros);
				}

				Variables.User.frmPaginas = Convert.ToInt32(totPag);
				Variables.User.frmPaginaActual = pag + 1;
                Variables.User.frmTotalRegistros = lrecs;

                long lastPag = totPag - 1;
                if (lastPag < 0) lastPag = 0;
                Variables.User.frmLinkUltimo = "?pag=" + lastPag;
				if (q != "")
					Variables.User.frmLinkUltimo += "&" + q;
			}
			catch (System.Exception e)
			{
				throw new Exception("Error: " + e + ", Script(ProcesaRepetidos): frmRepetir");
			}

			return totRegistro.ToString();
		}

		/// <summary>
		///     Devuelve el valor del campo 'campo' de la tabla 'tabla'.
		/// </summary>
		/// <param name="tabla"></param>
		/// <param name="campo"></param>
		/// <param name="lon">Si la longitud supera lon carácteres, truncamos el valor devuelto y concatenamos '...'.</param>
		/// <returns></returns>
		public static string DameValorTabla(string tabla, string campo, int lon)
		{
			string valorTabla = null;

			if (Variables.User.frmData == null)
				LoadData(tabla);

			try
			{
				valorTabla = Variables.User.frmData.Rows[(int)Variables.User.frmPosicion][campo].ToString();
				valorTabla = valorTabla.Trim();

				if (valorTabla.Length > lon && lon != 0)
				{
					valorTabla = valorTabla.Substring(0, lon) + "...";
				}
			}
			catch (System.Exception ex)
			{
				throw new Exception("Imposible recuperar el valor del campo: " + campo + ", de la tabla: " +
				tabla + " - Excepción: " + ex);
			}

			//evitamos que el "valor" se trate como un nuevo argumento de un comando
			valorTabla = valorTabla.Replace(",", "{coma}");
			return valorTabla;
        }


		/// <summary>
		///     Devuelve el valor del campo 'campo' de la tabla Variables.User.frmData.
		/// </summary>
		/// <param name="campo"></param>
		/// <param name="lon">Si la longitud supera lon carácteres, truncamos el valor devuelto y concatenamos '...'.</param>
		/// <returns></returns>
        public static string DameValorTabla(string campo, int lon)
        {
            return DameValorTabla(campo, lon, 0);
		}
		public static string DameValorTabla(string campo, int lon, int posicion)
		{
			string valorTabla = null;

            if (Variables.User.frmData == null || Variables.User.frmData.Rows.Count == 0)
                return "";

            if (Variables.User.frmData.Rows.Count == 0) return "";

            if(posicion == 0)
                posicion = (int)Variables.User.frmPosicion;
            if (posicion >= Variables.User.frmData.Rows.Count)
                posicion = Variables.User.frmData.Rows.Count - 1;
            if (posicion < 0)
                posicion = 0;


            try
			{
				valorTabla = Variables.User.frmData.Rows[posicion][campo].ToString();
				valorTabla = valorTabla.Trim();

				if (valorTabla.Length > lon && lon != 0)
				{
					valorTabla = valorTabla.Substring(0, lon) + "...";
				}
			}
			catch (System.Exception ex)
			{
                valorTabla = "[Imposible recuperar el valor del campo: " + campo + " - Excepción: " + ex.Message + "]";
			}

			//evitamos que el "valor" se trate como un nuevo argumento de un comando
			valorTabla = valorTabla.Replace(",", "{coma}");
			return valorTabla;
		}

        public static string DameValorTablaTemp(string campo, int lon, int posicion)
        {
            string valorTabla = null;

            if (Variables.User.frmDataTemp == null || Variables.User.frmDataTemp.Rows.Count == 0)
                return "";

            if (posicion == 0)
                posicion = (int)Variables.User.frmPosicionTemp;
            if (posicion >= Variables.User.frmDataTemp.Rows.Count)
                posicion = Variables.User.frmDataTemp.Rows.Count - 1;
            if (posicion < 0)
                posicion = 0;


            try
            {
                valorTabla = Variables.User.frmDataTemp.Rows[posicion][campo].ToString();
                valorTabla = valorTabla.Trim();

                if (valorTabla.Length > lon && lon != 0)
                {
                    valorTabla = valorTabla.Substring(0, lon) + "...";
                }
            }
            catch (System.Exception ex)
            {
                valorTabla = "[Imposible recuperar el valor del campo: " + campo + " - Excepción: " + ex.Message + "]";
            }

            //evitamos que el "valor" se trate como un nuevo argumento de un comando
            valorTabla = valorTabla.Replace(",", "{coma}");
            return valorTabla;
        }
        /// <summary>
        ///     Devuelve el valor del campo 'campo' de la tabla 'tabla'.
        /// </summary>
        /// <param name="tabla"></param>
        /// <param name="campo"></param>
        /// <returns></returns>
        public static string DameValorTabla(string tabla, string campo)
		{
			return DameValorTabla(tabla, campo, 0);
		}


		/// <summary>
		///     Si el valor de un campo coincide con 'valor', se devuelve 'valorSi', 'valorNo' en caso contrario.
		/// </summary>
		/// <param name="tabla"></param>
		/// <param name="campo"></param>
		/// <param name="valor"></param>
		/// <param name="valorSi"></param>
		/// <param name="valorNo"></param>
		/// <param name="novalor">Si es true, devuelve el campo que no coincide, sino el que coincide.</param>
		/// <returns></returns>
		public static string DameComparaValorTabla(string tabla, string campo, string valor, string valorSi, string valorNo,
			bool novalor)
		{
			string valorTabla = null;

			int i = (int)Variables.User.frmPosicion;

			try
			{
				valorTabla = Variables.User.frmData.Rows[i][campo].ToString();
			}
			catch
			{
				valorTabla = campo;
			}

			valorTabla = valorTabla.Trim().ToLower();
			valor = valor.Trim().ToLower();

			if (novalor)
			{
				if (valorTabla != valor)
				{
					return valorSi;
				}
				return valorNo;
			}

			if (valorTabla == valor)
			{
				return valorSi;
			}
			return valorNo;
		}

		/// <summary>
		///     Si el valor de un campo coincide con 'valor', se devuelve 'selected'
		/// </summary>
		/// <param name="tabla"></param>
		/// <param name="campo"></param>
		/// <param name="valor"></param>
		/// <returns></returns>
		public static string DameComparaValorTabla(string tabla, string campo, string valor)
		{
			return DameComparaValorTabla(tabla, campo, valor, "selected");
		}

		/// <summary>
		///     Si el valor de un campo coincide con 'valor', se devuelve 'valorSi', sino "".
		/// </summary>
		/// <param name="tabla"></param>
		/// <param name="campo"></param>
		/// <param name="valor"></param>
		/// <param name="valorSi"></param>
		/// <returns></returns>
		public static string DameComparaValorTabla(string tabla, string campo, string valor, string valorSi)
		{
			return DameComparaValorTabla(tabla, campo, valor, valorSi, "");
		}


		/// <summary>
		///     Si el valor de un campo coincide con 'valor', se devuelve 'valorSi', 'valorNo' en caso contrario.
		/// </summary>
		/// <param name="tabla"></param>
		/// <param name="campo"></param>
		/// <param name="valor"></param>
		/// <param name="valorSi"></param>
		/// <param name="valorNo"></param>
		/// <returns></returns>
		public static string DameComparaValorTabla(string tabla, string campo, string valor, string valorSi, string valorNo)
		{
			return DameComparaValorTabla(tabla, campo, valor, valorSi, valorNo, false);
		}


		/// <summary>
		///     Si el valor coincide con 'valor', se devuelve 'valorSi', 'valorNo' en caso contrario. Realiza la comprobación sin
		///     acceder a ninguna tabla.
		/// </summary>
		/// <param name="cadena"></param>
		/// <param name="valor"></param>
		/// <param name="valorSi"></param>
		/// <param name="valorNo"></param>
		/// <returns></returns>
		public static string DameComparaValor(string cadena, string valor, string valorSi, string valorNo)
		{
			if (cadena.Trim().ToLower() == valor.Trim().ToLower())
			{
				return valorSi;
			}
			return valorNo;
		}
		public static string SaveScriptCodes(string data)
		{
			return data.Replace("{", "@[@").Replace("}", "@]@");
		}
		public static string RestoreScriptCodes(string data)
		{
			return data.Replace("@[@", "{").Replace("@]@", "}");
		}
	}
}