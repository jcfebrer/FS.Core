
using System;
using System.Data;
using System.Diagnostics;
using System.Text;
using FSPortal;
using FSQueryBuilder;
using FSQueryBuilder.Enums;
using FSQueryBuilder.QueryParts.Where;
using System.Web;
using FSDatabase;

namespace FSPortalNET.Sitios.Serport.code
{
	/// <summary>
	/// Description of subirdocumento
	/// </summary>
	public class subirdocumento : BasePage
	{
		static string folder_id = "";
		static string camiones_id = "0B8Es1o63D6UeeFZDN0t3RnlraTg";
		static string remolques_id = "0B8Es1o63D6UeeGxZdlNOWFdTNTA";
		
		public subirdocumento()
		{
		}
		
		[DebuggerStepThrough]
		private void InitializeComponent()
		{
			Load += Page_Load;
		}
		
		protected override void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			Init += Page_Init;
			base.OnInit(e);
		}


		private void Page_Init(Object sender, EventArgs e)
		{
			InitializeComponent();
		}
        
		private void Page_Load(Object sender, EventArgs e)
		{
			contenido = Inicio();
		}
		
		private string Inicio()
		{
			DataTable isos = IsoType();
			StringBuilder sb = new StringBuilder();
			
			//guardamos el ID de la carpeta actual
			folder_id = FSNetwork.Web.Request("gd_id");
			
			sb.Append(SaveFile());
			
			//envio de fichero html
			sb.Append(@"<form id=""fileForm"" enctype=""multipart/form-data"" method=""post"" runat=""server"">" +
			@"<input type=""hidden"" name=""gd_id"" value=""" + folder_id + @""">" +
			@"<table border=""0""><tr><td>Selecciona el tipo documento:</td><td><select name=""iso"">");
			
			foreach (DataRow dr in isos.Rows) {
				sb.Append(@"<option value=""" + dr["codigotipocontroliso"] + @""">" + dr["descripcion"] + "</option>");
			}
			
			sb.Append("</select></td></tr>");
			
			sb.Append(@"<tr><td>Fecha vencimiento ISO:</td><td><input type=""text"" name=""fechaIso""></td></tr>");
			
			sb.Append(@"<tr><td>Descripción:</td><td><input type=""text"" name=""descripcion""></td></tr>");
			
			sb.Append(@"<tr><td>Nombre del fichero a subir:</td><td><input name=""ficheroASubir"" type=""file"" runat=""server"" /></td></tr></table>" +
			@"<br /><br /><button type=""submit"">Enviar fichero!</button><br />" +
			@"</form><br />");
			
			return sb.ToString();
		}
		
		
		private DataTable IsoType()
		{
			BdUtils db = new BdUtils("OracleConnection");
			StringBuilder sb = new StringBuilder("");
            
			SelectQueryBuilder sqB = new SelectQueryBuilder();
			sqB.Columns.SelectColumns("CODIGOTIPOCONTROLISO", "DESCRIPCION");
			sqB.TableSource = Variables.App.prefijoTablas + "BDGT.TIPOSCONTROLESISO";
			sqB.OrderBy.Add(new FSQueryBuilder.QueryParts.OrderBy.OrderByClause("DESCRIPCION"));
			FSQueryBuilder.Constants.Dbms.dbmsType = DBMSType.Oracle;
            		
			return db.Execute(sqB.BuildQuery());
		}
		
		private string SaveFile()
		{
			if (FSPortal.Variables.App.Page.Request.Files.Count > 0) {
				HttpPostedFile filePosted = FSPortal.Variables.App.Page.Request.Files[0];
			
				if (filePosted != null && filePosted.ContentLength > 0) {
					//string fileName = System.IO.Path.GetFileName(filePosted.FileName);
					//string fileExtension = System.IO.Path.GetExtension(fileName);
			
					// generating a random guid for a new file at server for the uploaded file
					//string newFile = Guid.NewGuid().ToString() + fileExtension;
					// getting a valid server path to save
					//string filePath = System.IO.Path.Combine(Server.MapPath("uploads"), newFile);
			
					//filePosted.SaveAs(filePath);
					
					string descripcion = FSNetwork.Web.Request("descripcion");
					
					string fechaIso = FSNetwork.Web.RequestDate("fechaIso");
					if (!FSLibrary.DateTimeUtil.IsDate(fechaIso)) {
						return @"<font color=""red"">*** Fecha incorrecta: Formato: dd/mm/aaaa</font>";
					}
					
					
					FSGoogleDrive.Library gd = new FSGoogleDrive.Library();
					//gd.UploadFile(fileName, filePath);
					
					bool camion = false;
					bool remolque = false;
					
					if (gd.GetParent(folder_id)[0] == camiones_id) {
						//el id es de un camión
						camion = true;
					}
					if (gd.GetParent(folder_id)[0] == remolques_id) {
						//el ID es de un remolque
						remolque = true;
					}
					
					string id = gd.GetName(folder_id);
					
					
					
					BdUtils db = new BdUtils("OracleConnection");
					
					db.BeginTransaction();
					
					//StringBuilder sb = new StringBuilder("");
		            
					UpdateQueryBuilder uqb = new UpdateQueryBuilder();
					if (camion) {
						uqb.TableSource = Variables.App.prefijoTablas + "BDGT.CAMIONESCONTROLESISO";
					}
					if (remolque) {
						uqb.TableSource = Variables.App.prefijoTablas + "BDGT.REMOLQUESCONTROLESISO";
					}
					uqb.Assignments.AddAssignment("FECHAVENCIMIENTO", fechaIso);
					uqb.Assignments.AddAssignment("OBSERVACIONES", descripcion);
					AndWhere andW = new AndWhere();
					if (camion) {
						andW.Add(new SimpleWhere(uqb.TableSource, "CODIGOCAMION", Comparison.Equals, id));
					}
					if (remolque) {
						andW.Add(new SimpleWhere(uqb.TableSource, "CODIGOREMOLQUE", Comparison.Equals, id));
					}
					andW.Add(new SimpleWhere(uqb.TableSource, "CODIGOTIPOCONTROLISO", Comparison.Equals, id));
					uqb.Where = andW;
					
					FSQueryBuilder.Constants.Dbms.dbmsType = DBMSType.Oracle;
		            		
					if (db.ExecuteNonQuery(uqb.BuildQuery()) == 0) {
						return @"<font color=""red"">*** No se ha actualizado ningún registro. Posiblemente el conductor no disponga del tipo de ISO especificado.</font>";
					}
					
					
					InsertQueryBuilder iqb = new InsertQueryBuilder();
					iqb.TableSource = Variables.App.prefijoTablas + "BDGT.DOCUMENTOSGRUPOS";

					iqb.Columns.SelectColumns("DESCRIPCION", "CODIGODOCUMENTOGRUPOPADRE", "PATHFICHERO");
					iqb.Values.SelectValues(id, (camion ? "1" : "5"), @"\\192.168.5.212\Datos\DocumentosGt\" + id + @"\");

					if (db.ExecuteNonQuery(iqb.BuildQuery()) == 0) {
						return @"<font color=""red"">*** Error. No se ha añadido ningún registro en la tabla: 'DocumentosGrupos'.</font>";
					}
					
					int idgrupo = db.GetMaxValue("BDGT.DOCUMENTOSGRUPOS", "CODIGODOCUMENTOCODIGO");
					
					
                    InsertQueryBuilder iqb2 = new InsertQueryBuilder();
					iqb2.TableSource = Variables.App.prefijoTablas + "BDGT.DOCUMENTOS";

					iqb2.Columns.SelectColumns("CODIGODOCUMENTOGRUPO", "NOMBREFICHERO", "DESCRIPCION", "FECHACREACION", "FECHAACTUALIZACION");
					iqb2.Values.SelectValues(idgrupo, filePosted.FileName, descripcion, DateTime.Now, DateTime.Now);

					if (db.ExecuteNonQuery(iqb2.BuildQuery()) == 0) {
						return @"<font color=""red"">*** Error. No se ha añadido ningún registro en la tabla: 'Documentos'.</font>";
					}  
					                              
					                              
					//guardamos directamente en Drive el Stream del fichero
					gd.UploadFile(filePosted.FileName, filePosted.InputStream, folder_id);
					
					db.CommitTransaction();
					
					return @"<font color=""green"">Fichero subido correctamente.</font>";
				} else {
					return @"<font color=""red"">*** No se ha especificado ningún fichero.</font>";
				}
			} else {
				return "";
			}
		}
	}
}
