using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using FSCrypto;
using FSDatabase;
using FSException;
using FSLibrary;
using FSMail;
using FSNetwork;
using FSParser;
using FSPdf;

namespace FSPortal
{
    public class ParserFunctions
    {
		private static void Evaluator_PreEvaluateVariable(object sender, VariablePreEvaluationEventArg e)
		{
		}

		private static void Evaluator_PreEvaluateFunction(object sender, FunctionPreEvaluationEventArg e)
		{
		}

		private static void Evaluator_EvaluateVariable(object sender, VariableEvaluationEventArg e)
		{
		}

		private static void Evaluator_EvaluateFunction(object sender, FunctionEvaluationEventArg e)
		{
		}

		public static string ProcesaFuncion(string comando, string[] parametros)
		{
			string result = "";
			int pos = 0;

			switch (comando.ToLower())
			{
				case "frmadduser":
					Variables.Parser.frmModo = Variables.FormMod.AddUser;
					Variables.Parser.frmEmailTo = "";
					Variables.Parser.frmPagina = Variables.User.ComeBack2;
					//Variables.Parser.frmTabla = "usuarios";
					break;

				case "frmadmin":
				case "frmadministrador":
					if (Variables.User.Administrador && parametros.Length == 1)
						result = parametros[0];
					else
						result = "";

					break;

				case "frmaleatorio":
					Random rnd = new Random();
					if (parametros.Length == 1 & parametros.Length > 0 && parametros[0] != "")
					{
						result = Convert.ToString(System.Math.Floor((double)rnd.Next(NumberUtils.NumberInt(parametros[0]))));
					}
					else
					{
						result = (Convert.ToString(System.Math.Floor((double)rnd.Next(Variables.Parser.data[Variables.Parser.frmDataPos].dataTable.Rows.Count))));
					}
					break;
				case "frmarbol":
					BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

					if (parametros.Length != 4)
					{
						result = "Parametros incorrectos en función: frmArbol. Parámetros: 1.(tabla)\n 2.(campo agrupación)\n 3.(campo padre)\n 4.(campo mostrar)";
					}
					else
					{
						result = Ui.DameFrmArbol(db.ConnStringEntryId, parametros[0], parametros[1], parametros[2], parametros[3]);
					}
					break;

				case "frmbarcode":
					if (parametros.Length != 2)
					{
						result = "Parametros incorrectos en función: frmBarcode. Parámetros: 1.(texto a codificar)\n 2.(tipo de código)";
					}
					else
					{
						FSBarcode.Barcode bar = new FSBarcode.Barcode();
						result = Ui.Image(bar.Generate(parametros[0], (FSBarcode.Barcode.CodeFormat)Convert.ToInt32(parametros[1])));
					}
					break;

				case "frmcaptcha":
					result = Ui.Captcha();
					Variables.Parser.frmCaptcha = true;
					break;

				case "frmcampo":
					if (Variables.Parser.frmCampos == null)
					{
						Variables.Parser.frmCampos = new Register();
					}

					int tamano = 0;
					string obligatorio = "0";
					string valor2 = "t";
					Type t = Type.GetType("System.String");

					if (parametros.Length == 0)
					{
						result = "Parametros incorrectos en función: frmCampo. Parámetros: 1.- [campo], [t|e|u|n|b|f]";
					}
					else
					{
						result = parametros[0];

						if (parametros.Length > 1)
						{
							tamano = Convert.ToInt32(parametros[1]);
						}

						if (parametros.Length > 2)
						{
							valor2 = parametros[2];

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

						if (parametros.Length > 3)
						{
							obligatorio = parametros[3];
						}


						Field r = new Field(parametros[0], "", t, valor2,
									  tamano,
									  (obligatorio == "1" ? true : false));

						if (Variables.Parser.frmCampos.Find(r.Campo) == null)
							Variables.Parser.frmCampos.Add(r);
					}
					break;

				case "frmcargapagina":
					result = "";
					if (parametros.Length > 0 && parametros[0] != "")
					{
						Variables.App.Page.Response.Redirect(parametros[0], false);
						HttpContext.Current.ApplicationInstance.CompleteRequest();
					}
					break;

				case "frmcalendario":
					int idCalendar = 0;
					if (parametros.Length == 1)
					{
						idCalendar = int.Parse(parametros[0]);
					}
					result = Ui.Calendar(idCalendar);
					break;

				case "frmcombo":
					BdUtils dbCombo = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

					switch (parametros.Length)
					{
						case 1:
							result =
								Ui.DameFrmCombo(dbCombo.ConnStringEntryId, Variables.Parser.data[Variables.Parser.frmDataPos].tabla, parametros[0],
									DameValorTabla(Variables.Parser.data[Variables.Parser.frmDataPos].tabla,
										parametros[0], 0));
							break;
						case 2:
							result =
								Ui.DameFrmCombo(dbCombo.ConnStringEntryId, parametros[0], parametros[1],
									DameValorTabla(parametros[0], parametros[1], 0));
							break;
						case 4:
							result =
								Ui.DameFrmCombo(dbCombo.ConnStringEntryId, parametros[0], parametros[1], parametros[2],
									parametros[3]);
							break;
						case 5:
							result =
								Ui.DameFrmCombo(dbCombo.ConnStringEntryId, parametros[0], parametros[1], parametros[2],
									parametros[3], parametros[4], "");
							break;
						case 6:
							result =
								Ui.DameFrmCombo(dbCombo.ConnStringEntryId, parametros[0], parametros[1], parametros[2],
									parametros[3], parametros[4], parametros[5]);
							break;
						default:
							result = "Parametros incorrectos en función: frmCombo. Parámetros: 1.(combo relacionado): [tabla], [campo] \n 2.(combo relacionado): [campo] \n 2.(combo simple): [tabla], [campoMostrar], [valor], [campoComparar] \n 4.(combo simple): [tabla], [campoMostrar], [valor], [campoComparar], [campoId]";
							break;
					}
					break;

				case "frmcomboonchange":
					result = "";
					if (parametros.Length == 1)
						Variables.Parser.frmComboOnChange = parametros[0];
					else
						Variables.Parser.frmComboOnChange = "";
					break;

				case "frmcomboselected":
					result = "";
					if (parametros.Length == 1)
						Variables.Parser.frmComboSelected = parametros[0];
					else
						Variables.Parser.frmComboSelected = "";
					break;

				case "frmconn":
					result = "";
					if (parametros.Length == 1)
						Variables.Parser.frmConn = parametros[0];
					else
						Variables.Parser.frmConn = "";
					break;

				case "frmparametros":
					result = "El comando frmparametros() solo puede utilizarse en la plantilla.";
					break;

				case "frmcorreoinfo":
					result = Variables.App.correoInfo;
					break;

				case "frmcheck":
					if (parametros.Length == 1)
					{
						Crypto cryptmd5 = new Crypto();
						if (Variables.App.Page.checkUrl)
							result = parametros[0] + "&check=" + HttpUtility.UrlEncode(cryptmd5.Md5(parametros[0], Crypto.Password).Replace("+", "%252b"));
						else
							result = parametros[0];
					}
					break;

				case "frmcrypt":
				case "crypt":
					if (parametros.Length == 1)
					{
						Crypto crypt = new Crypto();
						result = crypt.Crypt(parametros[0]);
					}
					else
						result = "Debe indicar la cadena a encriptar.";
					break;

				case "frmdatepicker":
					result = Ui.DatePicker();
					break;

				case "frmSetDataPos":
					if (parametros.Length == 1)
						Variables.Parser.frmDataPos = Convert.ToInt32(parametros[0]);
					else
						result = Variables.Parser.frmDataPos.ToString();
					break;

				case "frmdecrypt":
				case "decrypt":
					if (parametros.Length == 1)
					{
						Crypto decrypt = new Crypto();
						result = decrypt.Decryp(parametros[0]);
					}
					else
						result = "Debe indicar la cadena a desencriptar.";
					break;

				case "frmdescripcionweb":
					result = Variables.App.descripcionWeb;
					break;

				case "frmeditar":
					if (parametros.Length == 1)
					{
						result =
						Ui.EditPage(Variables.Parser.data[Variables.Parser.frmDataPos].tabla, parametros[0],
							Functions.Valor(
								Variables.Parser.data[Variables.Parser.frmDataPos].dataTable.Rows[(int)Variables.Parser.data[Variables.Parser.frmDataPos].position
								][parametros[0]]), "Editar registro", "Borrar registro");
					}
					else
						result = "Debe indicar el nombre del campo que permitirá la edición.";
					break;

				case "frmeditarpagina":
					result =
						Ui.EditPage("Paginas", "idPagina", Variables.App.Page.paginaId.ToString());
					break;

				case "frmedituser":
					result = "";
					if (Variables.User.Usuario != "")
					{
						Variables.Parser.frmModo = Variables.FormMod.EditUser;
						Variables.Parser.frmEmailTo = "";
						Variables.Parser.frmPagina = Variables.User.ComeBack2;
					}
					break;

				case "frmemail":
				case "frmmail":
					result = "";
					if (parametros.Length == 2)
					{
						Variables.Parser.frmEmailTo = parametros[0];
						Variables.Parser.frmEmailSubject = parametros[1];
						Variables.Parser.frmPagina = Variables.User.ComeBack2;
					}
					else
						result = "Parametros incorrectos en función: frmEmail. Parámetros: 1.(para)\n 2.(subject)";
					break;

				case "frmes":
					string sino = "";

					if (parametros.Length == 4)
					{
						sino = parametros[3];
					}

					result =
						DameComparaValorTabla(Variables.Parser.data[Variables.Parser.frmDataPos].tabla, parametros[0], parametros[1],
							parametros[2], sino, false);
					break;

				case "frmespar":
				case "frmesfilapar":
					if (parametros.Length == 1 && Variables.Parser.data[Variables.Parser.frmDataPos].position % 2 == 0)
						result = parametros[0];
					else
						result = "";

					break;

				case "frmeval":
				case "frmevaluate":
					if (parametros.Length == 1)
					{
						ExpressionEvaluator evaluator = new ExpressionEvaluator();
						// always evaluated before other var and func evaluations 
						evaluator.PreEvaluateVariable += Evaluator_PreEvaluateVariable;
						evaluator.PreEvaluateFunction += Evaluator_PreEvaluateFunction;

						// evaluated if no existing var or func exists
						evaluator.EvaluateVariable += Evaluator_EvaluateVariable;
						evaluator.EvaluateFunction += Evaluator_EvaluateFunction;

						result = evaluator.ScriptEvaluate(parametros[0]).ToString();
					}
					break;

				case "frmexecute":
					if (parametros.Length == 1)
					{
						BdUtils dbExecute = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
						dbExecute.Execute(parametros[0]);
						result = "";
					}
					
					break;

				case "frmfilexists":
					if (System.IO.File.Exists(parametros[0]))
						result = parametros[0];
					else
						result = "";
					break;

				case "frmextraetxt":
					result = ConvertDataToXML(Variables.Parser.data[Variables.Parser.frmDataPos].dataTable, Variables.Parser.data[Variables.Parser.frmDataPos].tabla);
					break;

				case "frmfila":
					result = Variables.Parser.data[Variables.Parser.frmDataPos].position.ToString();
					break;

				case "frmfilemaxsize":
					result = "";
					Variables.Parser.frmFileMaxSize = NumberUtils.NumberInt(parametros[0]);
					break;

				case "frmfiletypes":
					result = "";
					Variables.Parser.frmFileTypes = parametros[0];
					break;

				case "frmfileuploadpath":
					result = "";
					Variables.Parser.frmFileUploadPath = parametros[0];
					break;

				case "frmfinrepetir":
					result = "";
					break;

				case "frmfinsi":
					result = "";
					break;

				case "frmformulario":
				case "formulario":
					result = Formularios.MuestraFormulario(NumberUtils.NumberInt(parametros[0]));
					break;
				case "frmgooglemaps":
					if (parametros.Length != 3)
						result = "Parametros incorrectos en función: frmGoogleMaps. Parámetros: 1.(longitud)\n 2.(latitud)\n 3.(zoom)";
					else
						result = Ui.GoogleMaps(Convert.ToDecimal(parametros[0].Replace(".", ",")), Convert.ToDecimal(parametros[1].Replace(".", ",")), Convert.ToInt32(parametros[2]));
					break;

				case "frmguardavalor":
					switch (parametros.Length)
					{
						case 2:
							result = GuardaValorTabla(parametros[0], parametros[1]);
							break;
						case 4:
							result =
								GuardaValorTabla(parametros[0], parametros[1], parametros[2],
									parametros[3]);
							break;
						default:
							result = "Parametros incorrectos en función: frmGuardaValor. Parámetros: 1.- [tabla], [campo], [valor], [condicion] \n 2.- [campo], [valor]";
							break;
					}
					break;

				case "frmhttp":
					if (parametros.Length == 1)
						result = Http.ToAbsoluteUrl(parametros[0]);
					else
						result = "";
					break;

				case "frmhtmlpdf":
					CreatePDF.Generate(HttpContext.Current, Ui.MuestraPagina(Convert.ToInt32(parametros[0])), Variables.Parser.frmRotatePdf);

					Variables.Parser.frmRotatePdf = false;
					break;

				case "frmidentity":
					result = Variables.Parser.frmIdentity.ToString();
					break;

				case "frmidioma":
				case "idioma":
					result = FuncionesWeb.Idioma(NumberUtils.NumberInt(parametros[0]));
					break;

				case "frminiciar":
					result = "";
					Parser.IniciarFormulario();
					break;

				case "frmilikeit":
					result = Ui.IlikeIt();
					break;

				case "frmlinkanterior":
					pos = 0;
					if (parametros.Length == 1)
						pos = Convert.ToInt32(parametros[0]);

					if (Variables.Parser.contenidoRepite.Count > 0)
						result = Variables.Parser.contenidoRepite[pos].linkPrevious;
					else
						result = "";
					break;

				case "frmlinksiguiente":
					pos = 0;
					if (parametros.Length == 1)
						pos = Convert.ToInt32(parametros[0]);

					if (Variables.Parser.contenidoRepite.Count > 0)
						result = Variables.Parser.contenidoRepite[pos].linkNext;
					else
						result = "";
					break;
				case "frmlinkprimero":
					pos = 0;
					if (parametros.Length == 1)
						pos = Convert.ToInt32(parametros[0]);

					if (Variables.Parser.contenidoRepite.Count > 0)
						result = Variables.Parser.contenidoRepite[pos].linkFirst;
					else
						result = "";
					break;

				case "frmlinkultimo":
					pos = 0;
					if (parametros.Length == 1)
						pos = Convert.ToInt32(parametros[0]);

					if (Variables.Parser.contenidoRepite.Count > 0)
						result = Variables.Parser.contenidoRepite[pos].linkLast;
					else
						result = "";
					break;

				case "frmlinkinputtoolbar":
					result = LinkInputToolbar();
					break;

				case "frmlinktoolbar":
					if (parametros.Length == 1)
						result = LinkToolbar(parametros[0]);
					else
						result = "";
					break;

				case "frmlogin":
					result = "";
					Variables.Parser.frmModo = Variables.FormMod.Login;
					Variables.Parser.frmEmailTo = "";
					Variables.Parser.frmPagina = Variables.User.ComeBack2;
					break;

				case "frmlogeado":
					if (parametros.Length == 1 && Variables.User.Usuario != "")
						result = parametros[0];
					else
						result = "";
					break;

				case "frmmaxregistrospagina":
					result = "";
					if (parametros.Length == 1)
						Variables.Parser.frmMaxRegistrosPagina = Convert.ToInt32(parametros[0]);
					else
						Variables.Parser.frmMaxRegistrosPagina = 0;
					break;

				case "frmmd5":
					if (parametros.Length == 1)
					{
						Crypto crypto = new Crypto();
						result = crypto.Md5(parametros[0]);
					}
					else
						result = "Parametros incorrectos en función: frmMd5. Parámetro: [cadena]";

					break;

				case "frmmensajesinregistros":
					result = "";
					if (parametros.Length == 1)
						Variables.Parser.frmMensajeSinRegistros = parametros[0];
					else
						Variables.Parser.frmMensajeSinRegistros = "";
					break;

				case "frmmensajecombo":
					result = "";
					if (parametros.Length == 1)
						Variables.Parser.frmMensajeCombo = parametros[0];
					else
						Variables.Parser.frmMensajeCombo = "";
					break;

				case "frmmensajeok":
					result = "";
					if (parametros.Length == 1)
						Variables.Parser.frmMensajeOK = parametros[0];
					else
						Variables.Parser.frmMensajeOK = "";
					break;

				case "frmmensajenook":
					result = "";
					if (parametros.Length == 1)
						Variables.Parser.frmMensajeNoOK = parametros[0];
					else
						Variables.Parser.frmMensajeNoOK = "";
					break;

				case "frmmodbannerhorizontal":
					result = Variables.App.Plugins.Execute("ModBannerHorizontal");
					break;

				case "frmmodbannervertical":
					result = Variables.App.Plugins.Execute("ModBannerVertical");
					break;

				case "frmmodulo":
				case "modulo":
					Modulos modulos = new Modulos();
					if (parametros.Length != 1)
						result = "Parametros incorrectos en función: frmModulo. Parámetro: [nombre_modulo]";
					else
					{
						if (NumberUtils.IsNumeric(parametros[0]))
							result = modulos.MuestraModulo(NumberUtils.NumberInt(parametros[0]), false);
						else
							result = modulos.MuestraModulo(parametros[0], false);
					}

					break;

				case "frmnoes":
					string sinoes = "";

					if (parametros.Length == 4)
					{
						sinoes = parametros[3];
					}

					result =
						DameComparaValorTabla(Variables.Parser.data[Variables.Parser.frmDataPos].tabla, parametros[0], parametros[1],
							parametros[2], sinoes, true);
					break;

				case "frmpagina":
				case "pagina":
					string pag = "";

					if (parametros.Length == 2)
					{
						if (NumberUtils.IsNumeric(parametros[0]))
							pag = Ui.MuestraPagina(NumberUtils.NumberInt(parametros[0]),
								Functions.ValorBool(parametros[1]));
						else
							pag = Ui.MuestraPagina(parametros[0], Functions.ValorBool(parametros[1]));
					}
					else
					{
						if (NumberUtils.IsNumeric(parametros[0]))
							pag = Ui.MuestraPagina(NumberUtils.NumberInt(parametros[0]));
						else
							pag = Ui.MuestraPagina(parametros[0]);
					}

					result = pag;

					break;

				case "frmpaginas":
					result = Variables.Parser.frmPaginas.ToString();
					break;

				case "frmpaginaactual":
					result = Variables.Parser.frmPaginaActual.ToString();
					break;

				case "frmpalabrasclave":
					result = Variables.App.palabrasClave;
					break;

				case "frmpagepdf":
					CreatePDF.Generate(HttpContext.Current, parametros[0], Variables.Parser.frmRotatePdf);

					Variables.Parser.frmRotatePdf = false;
					break;

				case "frmposicion":
					if (parametros.Length > 0 && parametros[0] != "")
					{
						result = "";
						Variables.Parser.data[Variables.Parser.frmDataPos].position = Convert.ToInt64(NumberUtils.NumberDouble(parametros[0]));
					}
					else
					{
						result = (Variables.Parser.data[Variables.Parser.frmDataPos].position + 1).ToString();
					}

					break;

				case "frmplugin":
					if (NumberUtils.IsNumeric(parametros[0]))
						result =
							Variables.App.Plugins.Execute(NumberUtils.NumberInt(parametros[0]), parametros);
					else
						result = Variables.App.Plugins.Execute(parametros[0], parametros);
					break;

				case "frmrandom":
					result = "";
					Variables.Parser.frmRandom = Functions.ValorBool(parametros[0]);
					break;

				case "frmrecordarpassword":
					result = "";
					Variables.Parser.frmModo = Variables.FormMod.RecordarPassword;
					Variables.Parser.frmEmailTo = "";
					Variables.Parser.frmPagina = Variables.User.ComeBack2;
					break;
				case "frmreemplaza":
					string reemp;

					if (parametros.Length == 3)
					{
						reemp = TextUtil.Replace(parametros[0], parametros[1], parametros[2]);
						result = reemp;
					}
					else
					{
						result = "Parametros incorrectos en función: frmReemplaza. Parámetros: [cadena], [valor1], [valor2]";
					}
					break;

				case "frmredirige":
					result = "";
					Variables.Parser.frmRedirige = Functions.Valor(parametros[0]);
					break;

				case "frmrepetir":
					result = ProcesaRepetidos();
					break;

				case "frmrequest":
					switch (parametros.Length)
					{
						case 0:
							result = Web.RequestQueryForm();
							break;
						case 1:
							string vr = Web.Request(parametros[0]);
							//vr = Procesa(vr);
							result = vr;
							Variables.Parser.frmRequest = parametros[0] + "=" + vr;
							break;
					}
					break;

				case "frmrequestdel":
					result = Web.RequestQueryForm(parametros[0]);
					break;

				case "frmrequestentero":
				case "frmrequesti":
					int vri = Web.RequestInt(parametros[0]);
					result = vri.ToString();
					Variables.Parser.frmRequest = parametros[0] + "=" + vri.ToString();
					break;

				case "frmrequestfecha":
					string dtime = Web.RequestDate(parametros[0]);
					result = dtime;
					Variables.Parser.frmRequest = parametros[0] + "=" + dtime;
					break;

				case "frmrotatepdf":
					result = "";
					Variables.Parser.frmRotatePdf = true;
					break;

				case "frmselect":
					if (parametros.Length > 0 && parametros[0] != "")
					{
						result = parametros[0];

						if (HttpContext.Current.Request["pag"] == null || Variables.Parser.data[Variables.Parser.frmDataPos].sql != parametros[0])
						{
							Variables.Parser.frmModo = Variables.FormMod.Nada;

							ParserData parserData2 = new ParserData();
							parserData2.position = 0;
							parserData2.sql = parametros[0];
							parserData2.dataTable = LoadSelect(parametros[0]);

							if (Variables.Parser.frmRandom)
								parserData2.dataTable = FSDatabase.Utils.RandomizeDataTable(parserData2.dataTable);

							if (parserData2.dataTable.Rows.Count == 0)
							{
								Variables.Parser.frmModo = Variables.FormMod.AddRecord;
							}
							else
							{
								Variables.Parser.frmModo = Variables.FormMod.EditRecord;
							}

							Variables.Parser.data.Add(parserData2);
							Variables.Parser.frmDataPos = Variables.Parser.data.Count - 1;
						}
					}
					else
					{
						result = Variables.Parser.data[Variables.Parser.frmDataPos].sql;
					}
					break;

				case "frmsendmail":
				case "frmsendemail":
					switch (parametros.Length)
					{
						case 3:
							new SendMail().SendMailAsync(parametros[0], Variables.App.correoPrueba, Variables.App.correoCopia, parametros[1], parametros[2], Variables.App.correoInfo, Variables.App.nombreWeb, Variables.App.plantillaCorreo);
							result = "";
							break;
						case 5:
							new SendMail().SendMailAsync(parametros[0], Variables.App.correoPrueba, Variables.App.correoCopia, parametros[1], parametros[2], parametros[3], parametros[4], Variables.App.plantillaCorreo);
							result = "";
							break;
						default:
							result = "Parametros incorrectos en función: frmSendMail. Parámetros: [to], [subject], [body], [from], [fromName]";
							break;
					}
					break;

				case "frmsession":
					if (parametros.Length == 2)
					{
						result = "";
						Variables.App.Page.Session[parametros[0]] = parametros[1];
					}
					else
					{
						result = Functions.Valor(Variables.App.Page.Session[parametros[0]]);
					}
					break;

				case "frmsi":
					result = ProcesaSi();
					break;

				case "frmsii":
					switch (parametros.Length)
					{
						case 4:
							result = DameComparaValor(parametros[0], parametros[1], parametros[2], parametros[3]);
							break;
						default:
							result = "Parametros incorrectos en función: frmSii. Parámetros: [cadena], [valor], [valorSi], [valorNo]";
							break;
					}
					break;

				case "frmsino":
					result = "";
					break;

				case "frmtabla":
					string sel = "";
					result = "";

					ParserData parserData = new ParserData();
					if (HttpContext.Current.Request["pag"] == null)
					{
						if (parametros.Length == 1)
						{
							Variables.Parser.frmModo = Variables.FormMod.Nada;

							parserData.tabla = parametros[0];
							parserData.seleccion = "";
							parserData.orderby = "";
							parserData.position = 0;
							parserData.dataTable = LoadData(parserData.tabla);

							Variables.Parser.data.Add(parserData);
						}
						else
						{
							Variables.Parser.frmPagina = Variables.User.ComeBack2;

							if (parametros.Length > 1)
							{
								sel = parametros[1];
							}

							if (parametros.Length == 3)
								parserData.orderby = parametros[2];
							else
								parserData.orderby = "";

							parserData.tabla = parametros[0];
							sel = HttpUtility.HtmlDecode(sel);
							parserData.seleccion = TextUtil.Replace(sel, @"""", "'");
							parserData.position = 0;
							parserData.dataTable = LoadData(parserData.tabla, parserData.seleccion, parserData.orderby);

							Variables.Parser.data.Add(parserData);
							Variables.Parser.frmDataPos = Variables.Parser.data.Count - 1;

							if (Variables.Parser.frmRandom)
								Variables.Parser.data[Variables.Parser.frmDataPos].dataTable = FSDatabase.Utils.RandomizeDataTable(Variables.Parser.data[Variables.Parser.frmDataPos].dataTable);

							if (Variables.Parser.data[Variables.Parser.frmDataPos].dataTable.Rows.Count == 0 | Variables.Parser.data[Variables.Parser.frmDataPos].seleccion == "")
							{
								Variables.Parser.frmModo = Variables.FormMod.AddRecord;
							}
							else
							{
								Variables.Parser.frmModo = Variables.FormMod.EditRecord;
							}
						}
					}

					break;

				case "frmtiempocarga":
				case "tiempocarga":
					result = ((System.DateTime.Now.Ticks - Variables.App.Page.startTime) / 10000).ToString();
					break;
				case "frmtitulopagina":
					if (parametros.Length > 0 && parametros[0] != "")
					{
						Variables.App.Page.tituloPagina = Functions.Valor(parametros[0]);
					}
					result = "";

					break;
				case "frmtruncar":
					result = "";
					Variables.Parser.frmTruncar = Functions.ValorBool(parametros[0]);
					break;
				case "frmtwitterfoto":
					List<Twitter.TwitterMsg> twitterMens = Twitter.Mensajes(parametros[0], 10, false);
					result = twitterMens[0].Imagen;
					
					break;
				case "frmtwittermensaje":
					List<Twitter.TwitterMsg> twitterMens1 = Twitter.Mensajes(parametros[0], 10, false);
					result = twitterMens1[0].Mensaje;

					break;
				case "frmtwittertotalamigos":
					List<Twitter.TwitterMsg> twitterMens2 = Twitter.Mensajes(parametros[0], 10, false);
					result = twitterMens2[0].TotalAmigos.ToString();
					
					break;
				case "frmtwittertotalmensajes":
					List<Twitter.TwitterMsg> twitterMens3 = Twitter.Mensajes(parametros[0], 10, false);
					result = twitterMens3[0].TotalMensajes.ToString();

					break;
				case "frmtwittertotalseguidores":
					List<Twitter.TwitterMsg> twitterMens4 = Twitter.Mensajes(parametros[0], 10, false);
					result = twitterMens4[0].TotalSeguidores.ToString();
					
					break;
				case "frmusuario":
				case "usuario":
					if (Variables.User.UserData != null)
						result = Functions.Valor(Variables.User.UserData[parametros[0]]);
					else
						result = "Usuario no validado en el portal";

					break;
				case "frmvalor":
					if (parametros.Length == 1)
					{
						if (Variables.Parser.data[Variables.Parser.frmDataPos].tabla == "")
							result = DameValorTabla(parametros[0], 0);
						else
							result = DameValorTabla(Variables.Parser.data[Variables.Parser.frmDataPos].tabla, parametros[0], 0);
					}
					else
					{
						if (Variables.Parser.data[Variables.Parser.frmDataPos].tabla == "")
							result = DameValorTabla(parametros[0], NumberUtils.NumberInt(parametros[1]));
						else
							result = DameValorTabla(Variables.Parser.data[Variables.Parser.frmDataPos].tabla, parametros[0],
									NumberUtils.NumberInt(parametros[1]));
					}
					break;
				case "frmvalortabla":
					switch (parametros.Length)
					{
						case 1:
							result = DameValorTabla(parametros[0], 0);
							break;
						case 2:
							result = DameValorTabla(parametros[0], parametros[1], 0);
							break;
						case 3:
							result = DameValorTabla(parametros[0], parametros[1], NumberUtils.NumberInt(parametros[2]));
							break;
						default:
							result = "Parametros incorrectos en función: frmValorTabla. Parámetros: 1.- [campo] \n 2.- [tabla], [campo] \n 3.- [tabla], [campo], [tamaño max.]";
							break;
					}
					break;
				case "frmvariable":
					if (parametros.Length == 2)
					{
						ParserVariable parserVariable = new ParserVariable();
						parserVariable.name = parametros[0];
						parserVariable.value = parametros[1];
						Variables.Parser.variable.Add(parserVariable);
						result = "";
					}
					else
					{
						result = Variables.Parser.variable.Find(x => x.name == parametros[0]).value;
					}
					break;
				case "frmvolveratras":
					if (parametros.Length == 2)
					{
						Variables.Parser.frmVolverAtras = parametros[0];
					}
					else
						result = Variables.Parser.frmVolverAtras;
					break;
				default:
					result = "Comando inexistente: " + comando + "(" + (parametros.Length > 0 ? parametros[0] : "") + ")";
					break;
			}

			return result;
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
			int pagActual = Web.RequestInt("pag");
			int pagFinal = (pagActual + 10);
			if (pagFinal > Variables.Parser.frmPaginas)
				pagFinal = Variables.Parser.frmPaginas;

			int pagInicial = (pagActual - 10);

			if ((pagFinal - pagInicial) < 10)
				pagInicial = pagFinal - 10;

			if (pagInicial < 0)
				pagInicial = 0;

			string args = Web.RequestQueryForm();

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
			string link = "<form action='?pag=" + "&" + Web.RequestQueryForm() + "'><input size=3 type='text'><input type='submit'></form>";
			return link;
		}


		/// <summary>
		///     Devuelve el valor del campo 'campo' de la tabla 'tabla'.
		/// </summary>
		/// <param name="tabla"></param>
		/// <param name="campo"></param>
		/// <param name="lon">Si la longitud supera lon carácteres, truncamos el valor devuelto y concatenamos '...'.</param>
		/// <returns></returns>
		private static string DameValorTabla(string tabla, string campo, int lon)
		{
			string valorTabla;

			if (Variables.Parser.frmModo == Variables.FormMod.AddRecord | Variables.Parser.frmModo == Variables.FormMod.AddUser)
			{
				return "";
			}

			if (Variables.Parser.data[Variables.Parser.frmDataPos].dataTable == null)
				return "";

			if (Variables.Parser.data[Variables.Parser.frmDataPos].dataTable == null)
				Variables.Parser.data[Variables.Parser.frmDataPos].dataTable = LoadData(tabla, Variables.Parser.data[Variables.Parser.frmDataPos].seleccion, Variables.Parser.data[Variables.Parser.frmDataPos].orderby);

			try
			{
				valorTabla = Functions.Valor(Variables.Parser.data[Variables.Parser.frmDataPos].dataTable.Rows[(int)Variables.Parser.data[Variables.Parser.frmDataPos].position][campo]);
				valorTabla = valorTabla.Trim();

				if (!Variables.App.UseXML)
				{
					BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
					DataTable sch = db.GetSchemaTable(tabla);
					Field fd = db.GetField(campo, sch);
					if (fd != null)
					{
						switch (fd.Tipo.ToString().ToLower())
						{
							case "system.datetime":
								if (valorTabla != "")
									valorTabla = FSLibrary.DateTimeUtil.ShortDate(Convert.ToDateTime(valorTabla));
								break;
						}
					}
				}

				if (valorTabla.Length > lon && lon != 0)
				{
					valorTabla = TextUtil.Substring(valorTabla, 0, lon) + "...";
				}
			}
			catch (System.Exception ex)
			{
				throw new ExceptionUtil("Imposible recuperar el valor del campo: " + campo + ", de la tabla: " +
				Variables.App.prefijoTablas + tabla + " - Excepción: " + ex);
			}

			//evitamos que el "valor" se trate como un nuevo argumento de un comando
			valorTabla = valorTabla.Replace(",", "{coma}");
			return valorTabla;
		}


		/// <summary>
		///     Devuelve el valor del campo 'campo' de la tabla Variables.Parser.frmData.
		/// </summary>
		/// <param name="campo"></param>
		/// <param name="lon">Si la longitud supera lon carácteres, truncamos el valor devuelto y concatenamos '...'.</param>
		/// <returns></returns>
		private static string DameValorTabla(string campo, int lon)
		{
			string valorTabla;

			if (Variables.Parser.frmModo == Variables.FormMod.AddRecord | Variables.Parser.frmModo == Variables.FormMod.AddUser)
			{
				return "";
			}

			if (Variables.Parser.data == null || Variables.Parser.data[Variables.Parser.frmDataPos].dataTable == null || Variables.Parser.data[Variables.Parser.frmDataPos].dataTable.Rows.Count == 0)
				return "";

			try
			{
				valorTabla = Functions.Valor(Variables.Parser.data[Variables.Parser.frmDataPos].dataTable.Rows[(int)Variables.Parser.data[Variables.Parser.frmDataPos].position][campo]);
				valorTabla = valorTabla.Trim();

				if (valorTabla.Length > lon && lon != 0)
				{
					valorTabla = TextUtil.Substring(valorTabla, 0, lon) + "...";
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
		private static string DameValorTabla(string tabla, string campo)
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
		private static string DameComparaValorTabla(string tabla, string campo, string valor, string valorSi, string valorNo,
			bool novalor)
		{
			string valorTabla;

			if (Variables.Parser.frmModo == Variables.FormMod.AddRecord)
			{
				return "";
			}

			try
			{
				valorTabla = DameValorTabla(tabla, campo);
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
		public string DameComparaValorTabla(string tabla, string campo, string valor)
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
		public string DameComparaValorTabla(string tabla, string campo, string valor, string valorSi)
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
		public string DameComparaValorTabla(string tabla, string campo, string valor, string valorSi, string valorNo)
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
		private static string DameComparaValor(string cadena, string valor, string valorSi, string valorNo)
		{
			if (cadena.Trim().ToLower() == valor.Trim().ToLower())
			{
				return valorSi;
			}
			return valorNo;
		}

		/// <summary>
		///     Guarda el valor 'valor', en el campo 'campo' de la tabla Variables.Parser.frmTabla (es actualizada desde frmTabla).
		/// </summary>
		/// <param name="campo"></param>
		/// <param name="valor"></param>
		/// <returns></returns>
		private static string GuardaValorTabla(string campo, string valor)
		{
			return GuardaValorTabla(Variables.Parser.data[Variables.Parser.frmDataPos].tabla, campo, valor, Variables.Parser.data[Variables.Parser.frmDataPos].seleccion);
		}

		/// <summary>
		///     Guarda el valor 'valor', en el campo 'campo' de la tabla 'tabla', si se cumple la condición 'condición'.
		/// </summary>
		/// <param name="tabla"></param>
		/// <param name="campo"></param>
		/// <param name="valor"></param>
		/// <param name="condicion"></param>
		/// <returns></returns>
		private static string GuardaValorTabla(string tabla, string campo, string valor, string condicion)
		{
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
			FSLibrary.MathUtil math = new FSLibrary.MathUtil();

			if (math.Evaluate(valor))
			{
				valor = math.Result.ToString();
			}

			if (!NumberUtils.IsNumeric(valor))
			{
				valor = "'" + valor + "'";
			}

			//try
			//{
			if (
				db.ExecuteNonQuery("update " + Variables.App.prefijoTablas + tabla + " set " + campo + " = " + valor + " where " +
				condicion) == 0)
			{
				throw new ExceptionUtil("Imposible guardar el valor: '" + valor + "', en el campo '" + campo +
				"', de la tabla: " + Variables.App.prefijoTablas + tabla);
			}
			//}
			//catch (System.Exception ex)
			//{
			//    throw new FSException("Imposible guardar el valor: '" + valor + "', en el campo '" + campo + "', de la tabla: " + Variables.App.prefijoTablas + tabla + " - Excepción: " + ex.ToString());
			//}
			return "";
		}

		private static DataTable LoadSelect(string select)
		{
			return LoadSelect(select, Variables.Parser.frmConn);
		}

		private static DataTable LoadSelect(string select, string connStringEntryName)
		{
			if (Variables.App.UseXML && connStringEntryName == "")
			{
				string tabla = FSDatabase.Utils.GetTableName(select);
				string where = FSDatabase.Utils.GetWhere(select);

				XML xml = new XML(Variables.App.directorioWeb + "data");
				xml.Load(tabla + ".xml");
				return xml.Select(where);
			}
			else
			{
				if (connStringEntryName == "")
					throw new Exception("No se ha definido el nombre de la conexión. Tiene que existir en el web.config dicha entrada.");

				BdUtils db2 = new BdUtils(connStringEntryName);
				return db2.Execute(select);
			}
		}

		private static DataTable LoadData(string tabla)
		{
			return LoadData(tabla, "", "", Variables.Parser.frmConn);
		}

		private static DataTable LoadData(string tabla, string selection)
		{
			return LoadData(tabla, selection, "", Variables.Parser.frmConn);
		}

		private static DataTable LoadData(string tabla, string selection, string orderby)
		{
			return LoadData(tabla, selection, orderby, Variables.Parser.frmConn);
		}

		private static DataTable LoadData(string tabla, string selection, string orderby, string connStringEntryName)
		{
			DataTable dataTable;

			if (Variables.App.UseXML)
			{
				XML xml = new XML(Variables.App.directorioWeb + "data");
				xml.Load(tabla + ".xml");
				dataTable = xml.Select(selection);
			}
			else
			{
				BdUtils db2 = new BdUtils(connStringEntryName);

				string ssql = "select * from " + tabla;
				if (selection != "")
				{
					ssql = ssql + " WHERE " + selection;
				}

				if (orderby != "")
				{
					ssql = ssql + " ORDER BY " + orderby;
				}


				dataTable = db2.Execute(ssql);
			}

			if (dataTable == null)
				throw new ExceptionUtil("Tabla inexistente: " + tabla);

			return dataTable;
		}

		private static string ConvertDataToXML(DataTable frmData, string tableName)
		{
			string data = @"<?xml version=""1.0"" encoding=""utf-8""?>";
			data += "\n<" + tableName + ">";

			data += "\n<Columns>";
			foreach (DataColumn col in frmData.Columns)
			{
				data += "<Col>";
				data += col.ColumnName;
				data += "</Col>";
				data += "\n";
			}
			data += "</Columns>\n<Rows>";

			foreach (DataRow row in frmData.Rows)
			{
				data += "<Row>";

				foreach (DataColumn col in frmData.Columns)
				{
					data += "<Value>";
					data += row[col.ColumnName];
					data += "</Value>";
					data += "\n";
				}

				data += "</Row>";
				data += "\n";
			}

			data += "</Rows>\n</" + tableName + ">";

			return data;
		}


		private static string ProcesaRepetidos()
		{
			if (Variables.Parser.data.Count == 0)
				throw new ExceptionUtil("Error: frmData no inicializado. Utilice frmTabla o frmSelect para inicializarlo.");

			if (Variables.Parser.contenidoRepite == null || Variables.Parser.contenidoRepite.Count == 0)
				return "";

			int pag = Web.RequestInt("pag");

			StringBuilder totRegistro = new StringBuilder("");
			int maxRegs = Variables.Parser.frmMaxRegistrosPagina;

			try
			{
				foreach (ParserRepeat parserRepeat in Variables.Parser.contenidoRepite)
				{
					foreach (ParserData parserData in Variables.Parser.data)
					{
						long lrecs = parserData.dataTable.Rows.Count;

						if (maxRegs == 0)
							maxRegs = Variables.App.registrosPorPagina;

						long totPag = lrecs / maxRegs;
						if (lrecs % maxRegs > 0)
						{
							totPag = totPag + 1;
						}

						if (pag > totPag)
							pag = Convert.ToInt32(totPag) - 1;

						long rp = (maxRegs * pag);

						while (rp < lrecs)
						{
							parserData.position = rp;

							totRegistro.Append(Parser.Procesa(parserRepeat.data));

							rp++;

							if ((rp % maxRegs == 0))
							{
								break;
							}
						}

						string requestQueryForm = Web.RequestQueryForm();
						requestQueryForm = TextUtil.ReplaceREG(requestQueryForm, @"p=([\w\+ ]*)", ""); // quitamos el parámetro p=xxxx si existiera.
						requestQueryForm = TextUtil.ReplaceREG(requestQueryForm, @"pag=(\d+)", ""); // quitamos el parámetro pag=xxxx si existiera.

						requestQueryForm = Functions.ReorgQuery(requestQueryForm);

						parserRepeat.linkPrevious = "#";
						parserRepeat.linkNext = "#";

						parserRepeat.linkFirst = "?pag=0";
						if (requestQueryForm != "")
							parserRepeat.linkFirst += "&" + requestQueryForm;

						if (pag > 0)
						{
							parserRepeat.linkPrevious = "?pag=" + (pag - 1);
							if (requestQueryForm != "")
								parserRepeat.linkPrevious += "&" + requestQueryForm;
						}

						if (pag + 1 < totPag)
						{
							parserRepeat.linkNext = "?pag=" + (pag + 1);
							if (requestQueryForm != "")
								parserRepeat.linkNext += "&" + requestQueryForm;
						}

						if (lrecs == 0)
						{
							totRegistro.Append(Variables.Parser.frmMensajeSinRegistros);
						}

						Variables.Parser.frmPaginas = NumberUtils.NumberInt(totPag);
						Variables.Parser.frmPaginaActual = pag + 1;

						Variables.Parser.frmTotalRegistros = lrecs;

						long lastPag = totPag - 1;
						if (lastPag < 0) lastPag = 0;
						parserRepeat.linkLast = "?pag=" + lastPag;

						if (requestQueryForm != "")
							parserRepeat.linkLast += "&" + requestQueryForm;
					}
				}
			}
			catch (System.Exception e)
			{
				throw new ExceptionUtil("Error: " + e + ", Script(ProcesaRepetidos): frmRepetir");
			}

			return totRegistro.ToString();
		}

		private static string ProcesaSi()
		{
			StringBuilder totRegistro = new StringBuilder("");

			if (Variables.Parser.contenidoSi == null || Variables.Parser.contenidoSi.Count == 0)
				return "";

			try
			{
				foreach (ParserYesNo contenido in Variables.Parser.contenidoSi)
				{
					if (Convert.ToBoolean(FSParser.Utils.EvaluatorExpressionIf(contenido.condition)))
						totRegistro.Append(Parser.Procesa(contenido.datayes));
					else
						totRegistro.Append(Parser.Procesa(contenido.datano));
				}
			}
			catch (System.Exception e)
			{
				throw new ExceptionUtil("Error: " + e + ", Script(ProcesaSi): frmSi");
			}

			return totRegistro.ToString();
		}
	}
}
