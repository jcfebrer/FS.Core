using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using FSLibrary;
using FSPortal;
using FSQueryBuilder;
using FSQueryBuilder.Enums;
using System.Linq;
using FSDatabase;
using FSNetwork;

namespace FSPortalNET.Sitios.Serport.code
{

	public class documentacion : BasePage
	{
		DataTable dtCamiones;
		DataTable dtRemolques;
		static string folder_id = "";
		static string back_id = "";
		static string root_id = "0B8Es1o63D6UeUEc1cmwwbk4zNEk";
		//"root";
		static string camiones_id = "0B8Es1o63D6UeeFZDN0t3RnlraTg";
		static string remolques_id = "0B8Es1o63D6UeeGxZdlNOWFdTNTA";
		
		public documentacion()
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
			LoadDataCamiones();
			LoadDataRemolques();
			
			//guardamos el ID de la carpeta actual
			folder_id = FSNetwork.Web.Request("gd_id");
			
			string gd = GoogleDriveFiles();

			//guardamos el ID de la carpeta actual
			back_id = folder_id;
			
			return gd;
		}
		
		private void LoadDataCamiones()
		{
			BdUtils db = new BdUtils("OracleConnection");
			StringBuilder sb = new StringBuilder("");
            
			SelectQueryBuilder sqB = new SelectQueryBuilder();
			sqB.Columns.SelectColumns("CODIGOCAMION", "MATRICULACAMION");
			sqB.TableSource = Variables.App.prefijoTablas + "BDGT.CAMIONES";
			sqB.OrderBy.Add(new FSQueryBuilder.QueryParts.OrderBy.OrderByClause("CODIGOCAMION"));
			FSQueryBuilder.Constants.Dbms.dbmsType = DBMSType.Oracle;
            		
			dtCamiones = db.Execute(sqB.BuildQuery());
		}
		
		private void LoadDataRemolques()
		{
			BdUtils db = new BdUtils("OracleConnection");
			StringBuilder sb = new StringBuilder("");
            
			SelectQueryBuilder sqB = new SelectQueryBuilder();
			sqB.Columns.SelectColumns("CODIGOREMOLQUE", "MATRICULAREMOLQUE");
			sqB.TableSource = Variables.App.prefijoTablas + "BDGT.REMOLQUES";
			sqB.OrderBy.Add(new FSQueryBuilder.QueryParts.OrderBy.OrderByClause("CODIGOREMOLQUE"));
			FSQueryBuilder.Constants.Dbms.dbmsType = DBMSType.Oracle;
            		
			dtRemolques = db.Execute(sqB.BuildQuery());
		}
		
		
		
		private string GoogleDriveFiles()
		{
			string folder_name = "";
			string folder_id = "";
			FSGoogleDrive.Library fsgd = new FSGoogleDrive.Library();
			
							
			IList<FSGoogleDrive.FileInfo> filesD = null;
			if (Web.Request("gd_id") != "") {
				switch(Web.Request("gd_action"))
				{
					case "openfolder":
						folder_id = Web.Request("gd_id");

						if (folder_id == "root" || folder_id == "")
							folder_id = root_id;
						filesD = fsgd.FindFiles(folder_id);
						break;
					case "openfoldername":
						folder_id = FSNetwork.Web.Request("gd_id");
						
						folder_name = FSNetwork.Web.Request("gd_name");
						
						filesD = fsgd.FindFolder(folder_name, folder_id);
						break;
					case "openfile":
						string fileGD = FSLibrary.KnownFolders.GetPath(KnownFolder.Downloads) + @"\" + fsgd.GetName(FSNetwork.Web.Request("gd_id"));
						fsgd.Download(FSNetwork.Web.Request("gd_id"), fileGD);
									
						System.IO.FileInfo file = new System.IO.FileInfo(fileGD);
						if (file.Exists) {
							FSPortal.Variables.App.Page.Response.Clear();
							FSPortal.Variables.App.Page.Response.ClearHeaders();
							FSPortal.Variables.App.Page.Response.ClearContent();
							FSPortal.Variables.App.Page.Response.AddHeader("Content-Disposition", @"attachment; filename=""" + file.Name + @"""");
							FSPortal.Variables.App.Page.Response.AddHeader("Content-Length", file.Length.ToString());
							FSPortal.Variables.App.Page.Response.ContentType = MimeType.GetMimeType(file.Name);
							FSPortal.Variables.App.Page.Response.Flush();
							FSPortal.Variables.App.Page.Response.TransmitFile(file.FullName);
							FSPortal.Variables.App.Page.Response.End();
						}
						break;
						
				}				
			} else {
				if (root_id != "")
					filesD = fsgd.FindFiles(root_id);
				else
					filesD = fsgd.FindRoot();
			}

							
			string filesDrive = "";
							
			if (filesD != null) {
				
				
				filesDrive += WriteHeader();
				
				DataColumn[] pk = new DataColumn[1];
				pk[0] = dtCamiones.Columns["CODIGOCAMION"];
				dtCamiones.PrimaryKey = pk;
				pk[0] = dtRemolques.Columns["CODIGOREMOLQUE"];
				dtRemolques.PrimaryKey = pk;
				
				foreach (FSGoogleDrive.FileInfo file in filesD) {
					if (folder_id == camiones_id) {
						DataRow dr = dtCamiones.Rows.Find(file.name);

						if (dr != null)
							file.name = dr["MATRICULACAMION"].ToString();
					} else if (folder_id == remolques_id) {
						
						DataRow dr = dtRemolques.Rows.Find(file.name);

						if (dr != null)
							file.name = dr["MATRICULAREMOLQUE"].ToString();
					}
				}
				
				List<FSGoogleDrive.FileInfo> SortedList = filesD.OrderBy(o => o.name).ToList();
				
				
				foreach (FSGoogleDrive.FileInfo file in SortedList) {
					
					bool showFolder = true;
						
					if (folder_id == root_id || folder_id == "" || folder_id == "root") {
						showFolder = false;
						if (file.id == camiones_id)
							showFolder = true;
						if (file.id == remolques_id)
							showFolder = true;
					}
					if (showFolder)
						filesDrive += WriteViewRow(file);
				}
				
				filesDrive += WriteFooter();
			}
							
			return filesDrive;
		}
		
		private string WriteFooter()
		{
			StringBuilder sb = new StringBuilder();
			
			sb.Append("</table>");
			sb.Append("<br />");
			if (back_id != "") {
				sb.Append(@"<a href=""?{frmcheck(gd_action=openfolder&gd_id=" + back_id + @")}""><img src=""" + Variables.App.directorioPortal + @"imagenes/filemanager/icon/folderup.gif"" border=""0""></a>");
				sb.Append("<br />");
			} else {
				sb.Append(@"<a href=""?{frmcheck(gd_action=openfolder)}" + @"""><img src=""" + Variables.App.directorioPortal + @"imagenes/filemanager/icon/folderup.gif"" border=""0""></a>");
				sb.Append("<br />");
			}
			sb.Append(@"<a href=""?{frmcheck(gd_action=openfolder)}"">Raíz</a>");
			
			
			//envio de fichero html
			if(folder_id!="")
			{
				sb.Append(@"<br /><form action=""subirdocumento.aspx"" id=""fileForm"" method=""post"" runat=""server"">" +
				@"<br /><input type=""hidden"" name=""gd_id"" value=""" + folder_id + @""">" + 
				@"<br /><button type=""submit"">Enviar fichero!</button><br />" +
				@"</form><br />");
			}
			
			return sb.ToString();
		}
		
		private string WriteHeader()
		{
			StringBuilder sb = new StringBuilder("");
			sb.Append(@"<table cellspacing=""0"" border=""0"" width=""100%"">");
			sb.Append("<tr>");
			sb.Append(
				@"<td width=""20"" align=""right""><input name=""all_files_checkbox"" onclick=""javascript:checkall(this);"" type=""checkbox"" /></td>");
			sb.Append(@"<td width=""20"" align=""center"">&nbsp;</td>");
			sb.Append(@"<td align=""left"">" + PageUrl("", "Name") + "Nombre</a></td>");
			//sb.Append(@"<td width=""80"" align=""right"">" + PageUrl("", "Size") + "Tamaño</a></td>");
			//sb.Append(@"<td width=""30"" align=""left"">&nbsp;</td>");
			//sb.Append(@"<td width=""150"" align=""right"">" + PageUrl("", "Created") + "Creado</a></td>");
			//sb.Append(@"<td width=""150"" align=""right"">" + PageUrl("", "Modified") + "Modificado</a></td>");
			//sb.Append(@"<td width=""45"" align=""right"">" + PageUrl("", "Attr") + "Attr</a></td>");
			sb.Append("</tr>");
			return sb.ToString();
		}
		
		private string WriteViewRow(FSGoogleDrive.FileInfo file)
		{
			string strFileLink;
			string strFileName = file.name;
			string strFileId = file.id;
			bool blnFolder = file.isFolder;

			if (blnFolder) {
				//+ @"<img border=""0"" src=""" + FileIconLookup(file) + @""" >" 
				strFileLink = PageUrl(strFileId, "") + Ui.Lf() + strFileName + "</a>";
			} else {
				strFileLink = @"<a href=""?{frmcheck(gd_action=openfile&gd_id=" + strFileId + @")}""" +
				// (IsImage(strFileName) ? @" class=""fancybox"" rel=""images""" : "") +
				@" target=""_blank"">" + strFileName + "</a>";
				//strFileLink = strFileLink + @"&nbsp;<a href=""javascript:download('" + strFileName +
				//@"');""><img src=""" + Variables.App.directorioPortal + @"imagenes/filemanager/icon/download.gif"" border=""0""></a>";
			}

			StringBuilder sb = new StringBuilder("");


			sb.Append("<tr>");
			sb.Append(@"<td align=""right"">");

			if (Variables.User.Administrador) {
				sb.Append(@"<input name=""");
				sb.Append("checked_");
				sb.Append(strFileName);
				sb.Append(@""" type=""checkbox"" />");
			} else
				sb.Append("&nbsp;");

			sb.Append("</td>");
			sb.Append(@"<td align=""center""><img src=""");
			sb.Append(FileIconLookup(file));
			sb.Append(@""" ");
			sb.Append(">");
			sb.Append("</td>");
			sb.Append("<td>");
			sb.Append(strFileLink);
			sb.Append("</td>");

//			if (blnFolder) {
//				sb.Append(@"<td align=""left"">&nbsp;</td>");
//				sb.Append(@"<td align=""left"">&nbsp;</td>");
//			} else {
//				sb.Append(@"<td align=""right"">");
//				sb.Append(FormatKB(Convert.ToInt64(drv["Size"])));
//				sb.Append("</td>");
//				sb.Append(@"<td align=""left"">kb");
//				sb.Append("</td>");
//			}

//			sb.Append(@"<td align=""right"">");
//			sb.Append(Convert.ToString(drv["Created"]));
//			sb.Append("</td>");
//			sb.Append(@"<td align=""right"">");
//			sb.Append(Convert.ToString(drv["Modified"]));
//			sb.Append("</td>");
//			sb.Append(@"<td align=""right"">");
//			sb.Append(Convert.ToString(drv["Attr"]));
//			sb.Append("</td>");
			sb.Append("</tr>");

			sb.Append(Environment.NewLine);

			return sb.ToString();
		}
		
		
		private string PageUrl(string newPath, string newSortColumn)
		{
			bool blnSortProvided = (newSortColumn != "");

			if (newPath == "") {
				newPath = "root";
			}
//            if (newSortColumn == "")
//            {
//                newSortColumn = SortColumn();
//            }

			StringBuilder sb = new StringBuilder("");

			sb.Append(@"<a href=""");
			sb.Append(ScriptName);
			sb.Append("?");
			sb.Append("{frmcheck(");
			sb.Append("gd_id");
			sb.Append("=");
			sb.Append(newPath);
			sb.Append("&");
			sb.Append("gd_action");
			sb.Append("=");
			sb.Append("openfolder");
			sb.Append(")}");
//            if (newSortColumn != "")
//            {
//                sb.Append("&");
//                sb.Append("sort");
//                sb.Append("=");
//                if (blnSortProvided & (newSortColumn.ToLower() == SortColumn().ToLower()))
//                {
//                    sb.Append("-");
//                }
//                sb.Append(newSortColumn);
//            }
			sb.Append(@""">");


			return sb.ToString();
		}
		
		private string FileIconLookup(FSGoogleDrive.FileInfo file)
		{
			if (file.isFolder) {
				return Variables.App.directorioPortal + "imagenes/filemanager/file/folder.gif";
			}

//            if (IsImage(file.name))
//            {
//                return Variables.App.directorioPortal + "galeria/thumbnail.aspx?ForceAspect=false&height=" + tamY + "&width=" +
//                       tamX + "&image=" + WebPathCombine(WebPath(), Convert.ToString(drv["Name"]));
//            }

			switch (System.IO.Path.GetExtension(file.name).ToLower()) {
				case ".txt":
					return Variables.App.directorioPortal + "imagenes/filemanager/file/text.gif";
				case ".htm":
				case ".xml":
				case ".xsl":
				case ".css":
				case ".html":
				case ".config":
					return Variables.App.directorioPortal + "imagenes/filemanager/file/html.gif";
				case ".mp3":
				case ".wav":
				case ".wma":
				case ".au":
				case ".mid":
				case ".ram":
				case ".rm":
				case ".snd":
				case ".asf":
					return Variables.App.directorioPortal + "imagenes/filemanager/file/audio.gif";
				case ".zip":
				case "tar":
				case ".gz":
				case ".rar":
				case ".cab":
				case ".tgz":
					return Variables.App.directorioPortal + "imagenes/filemanager/file/compressed.gif";
				case ".asp":
				case ".wsh":
				case ".js":
				case ".vbs":
				case ".aspx":
				case ".cs":
				case ".vb":
					return Variables.App.directorioPortal + "imagenes/filemanager/file/script.gif";
				default:
					return Variables.App.directorioPortal + "imagenes/filemanager/file/generic.gif";
			}
		}
		
		
		private bool IsImage(string file)
		{
			switch (System.IO.Path.GetExtension(file).ToLower()) {
				case ".gif":
				case ".jpeg":
				case ".jpe":
				case ".jpg":
				case ".png":
				case ".bmp":
					return true;
				default:
					return false;
			}
		}

	}
}
