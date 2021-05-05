using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Web;
using FSLibrary;
using FSPortal;

namespace FSPaginas.FileManager
{
	/// <summary>
	/// Description of GoogleDrive.
	/// </summary>
	public class GoogleDrive : BasePage
	{
		static string back_id = "";
		static string root_id = "0B8Es1o63D6UeUEc1cmwwbk4zNEk";  //"root";
		
		public GoogleDrive()
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
			string gd = GoogleDriveFiles();

			//guardamos el ID de la carpeta actual
			back_id = FSNetwork.Web.Request("gd_id");
			
			return gd;
		}
		
		
		private void SaveFile()
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
					
					
					FSGoogleDrive.Library gd = new FSGoogleDrive.Library();
					//gd.UploadFile(fileName, filePath);
					
					//guardamos directamente en Drive el Stream del fichero
					gd.UploadFile(filePosted.FileName,filePosted.InputStream, back_id);
				}
			}
		}
        
		private string GoogleDriveFiles()
		{
			FSGoogleDrive.Library fsgd = new FSGoogleDrive.Library();
			
			SaveFile();
							
			IList<FSGoogleDrive.FileInfo> filesD = null;
			if (FSNetwork.Web.Request("gd_id") != "") {
				if (FSNetwork.Web.Request("gd_action") != "") {
					if (FSNetwork.Web.Request("gd_action") == "openfolder")
					{
						string folder = FSNetwork.Web.Request("gd_id");
						if(folder=="root" || folder =="") folder = root_id;
						filesD = fsgd.FindFiles(folder);
					}
									
					if (FSNetwork.Web.Request("gd_action") == "openfile") {
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
					}
				}
								
			} else {
				if(root_id != "")
					filesD = fsgd.FindFiles(root_id);
				else
					filesD = fsgd.FindRoot();
			}

							
			string filesDrive = "";
							
			if (filesD != null) {
				
				
				filesDrive += WriteHeader();
				
				foreach (FSGoogleDrive.FileInfo file in filesD) {	
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
				sb.Append(@"<a href=""?gd_action=openfolder&gd_id=" + back_id + @"""><img src=""" + Variables.App.directorioPortal + @"imagenes/filemanager/icon/folderup.gif"" border=""0""></a>");
				sb.Append("<br />");
			} else {
				sb.Append(@"<a href=""?gd_action=openfolder" + @"""><img src=""" + Variables.App.directorioPortal + @"imagenes/filemanager/icon/folderup.gif"" border=""0""></a>");
				sb.Append("<br />");
			}
			sb.Append(@"<a href=""?gd_action=openfolder"">Raíz</a>");
			
			
			//envio de fichero html
			sb.Append(@"<br /><form id=""fileForm"" enctype=""multipart/form-data"" method=""post"" runat=""server"">" +
			@"<br />" +
			@"Nombre del fichero a subir:<input name=""ficheroASubir"" type=""file"" runat=""server"" />" +
			@"<button type=""submit"">Enviar fichero!</button><br />" +
			@"&nbsp;</form><br />");
			
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
				strFileLink = @"<a href=""?gd_action=openfile&gd_id=" + strFileId + @"""" +
				// (IsImage(strFileName) ? @" class=""fancybox"" rel=""images""" : "") +
				@" target=""_blank"">" + strFileName + "</a>";
				strFileLink = strFileLink + @"&nbsp;<a href=""javascript:download('" + strFileName +
				@"');""><img src=""" + Variables.App.directorioPortal + @"imagenes/filemanager/icon/download.gif"" border=""0""></a>";
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
			sb.Append("gd_id");
			sb.Append("=");
			sb.Append(newPath);
			sb.Append("&");
			sb.Append("gd_action");
			sb.Append("=");
			sb.Append("openfolder");
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
