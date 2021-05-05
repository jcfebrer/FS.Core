// <fileheader>
// <copyright file="Default.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: filemanager\Default.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using FSPortal;
using FSLibrary;
using ICSharpCode.SharpZipLib.Zip;
using FSNetwork;

namespace FSPaginas.FileManager
{
    public class Default : BasePage
    {
        #region '" Web Form Designer Generated Code "'

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

            modeGallery = false;
            adminControls = Variables.User.Administrador;
            pathInicial = Variables.App.directorioWeb;
        }


        private void Page_Init(Object sender, EventArgs e)
        {
            InitializeComponent();
        }

        #endregion

        private System.Exception fileOperationException;
        private bool blnFlushContent;
        private string strAllowedPathPattern;
        private string strHideFilePattern;
        private string strHideFolderPattern;
        private string strImagePath;
        public bool adminControls;
        public int columnas = 3;
        private int intRowsRendered;
        public bool modeGallery;
        public string pathInicial;
        private int tamX = 16;
        private int tamY = 16;

        //public string CurrentWebPath
        //{
        //    get { return WebPath(); }
        //}

        private void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        public string Inicio()
        {
            strHideFolderPattern = GetConfigString("HideFolderPattern", "");
            strHideFilePattern = GetConfigString("HideFilePattern", "");
            strAllowedPathPattern = GetConfigString("AllowedPathPattern", "");
            strImagePath = GetConfigString("ImagePath", Variables.App.directorioPortal + "imagenes/filemanager/");
            blnFlushContent = (GetConfigString("FlushContent", "") != "");

            StringBuilder sb = new StringBuilder("");

            sb.Append(HandleAction());

            sb.Append(@"<form name=""frmFile"" action=""" + ScriptName +
                      @""" method=""post"" enctype=""multipart/form-data"">");
            sb.Append(@"<input type=""hidden"" name=""action"" />");

            sb.Append(WriteTable());

            sb.Append("</form>");

            return sb.ToString();
        }

        private string MapPath2(string path)
        {
            if (!Variables.User.Administrador) return Server.MapPath(path);

            if (path.IndexOf(":") != -1)
            {
                return path;
            }
            return Server.MapPath(path);
        }

        private string GetConfigString(string strKey, string strDefaultValue)
        {
            if (ConfigurationManager.AppSettings[strKey] == null)
            {
                return strDefaultValue;
            }
            return Convert.ToString(ConfigurationManager.AppSettings[strKey]);
        }

        private string HandleAction()
        {
            if (Web.Request("action") == "")
            {
                return "";
            }

            string strAction = Web.Request("action").ToLower();
            if (strAction == "")
            {
                return "";
            }

            StringBuilder sb = new StringBuilder("");

            switch (strAction)
            {
                case "newfolder":
                    MakeFolder(GetTargetPath());
                    break;
                case "upload":
                    SaveUploadedFile();
                    break;
                default:
                    ProcessCheckedFiles(strAction);
                    break;
            }

            if (fileOperationException != null)
            {
                sb.Append(WriteError(fileOperationException));
            }

            return sb.ToString();
        }


        private void ProcessCheckedFiles(string strAction)
        {
            int intTagLength = "checked_".Length;
            ArrayList fileList = new ArrayList();

            foreach (string strItem in Request.Form)
            {
                int intLoc = strItem.IndexOf("checked_");
                if (intLoc > -1)
                {
                    fileOperationException = null;
                    string strName = TextUtil.Substring(strItem, intLoc + intTagLength);
                    fileList.Add(strName);
                    switch (strAction)
                    {
                        case "borrar":
                            DeleteFileOrFolder(strName);
                            break;
                        case "mover":
                            MakeFolder(GetTargetPath());
                            MoveFileOrFolder(strName);
                            break;
                        case "copiar":
                            MakeFolder(GetTargetPath());
                            CopyFileOrFolder(strName);
                            break;
                        case "renombrar":
                            RenameFileOrFolder(strName);
                            break;
                        case "unzip":
                            UnZipFile(strName);
                            break;
                    }
                }
            }

            if (strAction == "descargar")
            {
                DownloadFile(Web.Request("filedownload"));
            }

            if (strAction == "zip")
            {
                ZipFileOrFolder(fileList);
            }
        }


        private void DownloadFile(string strFileName)
        {
            Response.Clear();
            Response.ContentType = MimeType.GetMimeType(strFileName); //"application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + strFileName);
            Response.TransmitFile(GetLocalPath(Path.GetFileName(strFileName)));
            Response.End();
        }


        private void SaveUploadedFile()
        {
            if (Request.Files.Count > 0)
            {
                HttpPostedFile pf = Request.Files[0];
                if (pf.ContentLength > 0)
                {
                    string strFilename = pf.FileName;
                    string strTargetFile = GetLocalPath(Path.GetFileName(strFilename));
                    if (System.IO.File.Exists(strTargetFile))
                    {
                        DeleteFileOrFolder(strFilename);
                    }
                    try
                    {
                        pf.SaveAs(strTargetFile);
                    }
                    catch (System.Exception ex)
                    {
                        fileOperationException = ex;
                    }
                }
            }
        }


        private void UnZipFile(string theFile)
        {
            if (!(theFile.ToLower().EndsWith(".zip")))
            {
                return;
            }

            string zipTargetFile = GetLocalPath(theFile);

            ZipInputStream zipIn = new ZipInputStream(System.IO.File.OpenRead(zipTargetFile));

            do
            {
                ZipEntry entry = zipIn.GetNextEntry();
                if (entry != null)
                {
                    if (!(Directory.Exists(Path.GetDirectoryName(GetLocalPath(entry.Name)))))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(GetLocalPath(entry.Name)));
                    }

                    if (!(entry.IsDirectory))
                    {
                        FileStream streamWriter = System.IO.File.Create(GetLocalPath(entry.Name));

                        byte[] data = new byte[2049];
                        do
                        {
                            int size = zipIn.Read(data, 0, data.Length);
                            if ((size > 0))
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        } while (true);
                        streamWriter.Close();
                    }
                }
                else
                {
                    break;
                }
            } while (true);

            zipIn.Close();
        }


        private string WriteTable()
        {
            if (modeGallery)
            {
                tamX = 48;
                tamY = 48;
            }

            StringBuilder sb = new StringBuilder("");

            sb.Append(@"<input type=""hidden"" name=""filedownload"" />");

            if (!modeGallery)
            {
                sb.Append(@"<table width=""100%"" border=0>");
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append(@"<img border=""0"" src=""" + strImagePath +
                          @"file/folder.gif"" align=""absmiddle"">&nbsp;");
                sb.Append(@"<input type=""text"" name=""path"" value=""" + WebPath() + @""" size=""35"" />");
                sb.Append(@"<input type=""submit"" value=""Ir"" />");
                sb.Append(@"</td>");

                sb.Append("<td>");
                if (ShowUp())
                {
                    sb.Append(UpUrl());
                    sb.Append(@"<img border=""0"" src=""" + strImagePath +
                              @"icon/folderup.gif"" align=""absmiddle"">&nbsp;Subir un nivel</a>");
                }
                else sb.Append("&nbsp;");
                sb.Append("</td>");
                sb.Append(@"<td align=right width=""*""><a href=""#bottom"" title=""end key"">Ir al final</a></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append(Environment.NewLine);
                Flush();
            }


            sb.Append(@"<table cellspacing=""0"" border=""0"" width=""100%"">");

            if (!modeGallery)
            {
                sb.Append("<tr>");
                sb.Append(
                    @"<td width=""20"" align=""right""><input name=""all_files_checkbox"" onclick=""javascript:checkall(this);"" type=""checkbox"" /></td>");
                sb.Append(@"<td width=""20"" align=""center"">&nbsp;</td>");
                sb.Append(@"<td align=""left"">" + PageUrl("", "Name") + "Nombre</a></td>");
                sb.Append(@"<td width=""80"" align=""right"">" + PageUrl("", "Size") + "Tamaño</a></td>");
                sb.Append(@"<td width=""30"" align=""left"">&nbsp;</td>");
                sb.Append(@"<td width=""150"" align=""right"">" + PageUrl("", "Created") + "Creado</a></td>");
                sb.Append(@"<td width=""150"" align=""right"">" + PageUrl("", "Modified") + "Modificado</a></td>");
                sb.Append(@"<td width=""45"" align=""right"">" + PageUrl("", "Attr") + "Attr</a></td>");
                sb.Append("</tr>");
                sb.Append(Environment.NewLine);
                Flush();
            }

            sb.Append(WriteRows());

            sb.Append("</table>");
            Flush();

            if (intRowsRendered < 0)
            {
                return sb.ToString();
            }

            if (adminControls)
            {
                sb.Append(@"<a name=""bottom""></a>");
                sb.Append(@"<table class=""Header"" width=""100%"">");
                sb.Append("<tr>");
                sb.Append(@"<td width=""300"" valign=""top"">");
                sb.Append(@"<img src=""" + strImagePath + @"file/folder.gif"" align=absmiddle>");
                sb.Append(@"&nbsp;<input type=""text"" name=""targetfolder"" size=""35"">");
                sb.Append(@"</td>");
                sb.Append(@"<td width=""*"" valign=""top"" rowspan=2>&nbsp;");
                sb.Append(@"</td>");
                sb.Append(@"</tr>");
                sb.Append(@"</table>");

                sb.Append("<table>");
                sb.Append("<tr>");
                sb.Append(@"<td width=""140"">");
                sb.Append(@"<a href=""javascript:newfolder();"">");
                sb.Append(@"<img border=""0"" src=""" + strImagePath +
                          @"icon/newfolder.gif"" width=""19"" height=""16"" align=""absmiddle"">");
                sb.Append("&nbsp;Nueva carpeta</a>");
                sb.Append(@"</td>");
                sb.Append(@"<td width=""140"">");
                sb.Append(@"<a href=""javascript:confirmfiles('copiar');"">");
                sb.Append(@"<img border=""0"" src=""" + strImagePath +
                          @"icon/copy.gif"" align=""absmiddle"">&nbsp;Copiar a carpeta</a>");
                sb.Append(@"</td>");
                sb.Append(@"<td width=""140"">");
                sb.Append(@"<a href=""javascript:confirmfiles('mover');"">");
                sb.Append(@"<img border=""0"" src=""" + strImagePath +
                          @"icon/move.gif"" align=""absmiddle"">&nbsp;Mover a carpeta</a>");
                sb.Append(@"</td>");
                sb.Append(@"<td width=""*"">");

                sb.Append(@"<a href=""javascript:confirmfiles('unzip');"">");
                sb.Append(@"<img border=""0"" src=""" + strImagePath + @"icon/zip.gif"" align=""absmiddle"">");
                sb.Append("&nbsp;UnZip</a>");
                sb.Append(@"</td>");
                sb.Append(@"</tr>");

                sb.Append("<tr>");
                sb.Append(@"<td width=""140"">");
                sb.Append(@"<a href=""javascript:upload();"">");
                sb.Append(@"<img border=""0"" src=""" + strImagePath +
                          @"icon/upload.gif"" align=absmiddle>&nbsp;Subir un fichero</a>");
                sb.Append(@"</td>");
                sb.Append(@"<td width=""140"">");
                sb.Append(@"<a href=""javascript:confirmfiles('borrar');"">");
                sb.Append(@"<img border=""0"" src=""" + strImagePath +
                          @"icon/delete.gif"" align=absmiddle>&nbsp;Borrar</a>");
                sb.Append(@"</td>");
                sb.Append(@"<td width=""140"">");
                sb.Append(@"<a href=""javascript:confirmfiles('renombrar');"">");
                sb.Append(@"<img border=""0"" src=""" + strImagePath +
                          @"icon/rename.gif"" align=absmiddle>&nbsp;Renombrar</a>");
                sb.Append(@"</td>");
                sb.Append(@"<td width=""*"">");
                sb.Append(@"<a href=""javascript:confirmfiles('zip');"">");
                sb.Append(@"<img border=""0"" src=""" + strImagePath + @"icon/zip.gif"" align=""absmiddle"">");
                sb.Append("&nbsp;Zip</a>");
                sb.Append(@"</td>");
                sb.Append(@"<td width=""*"">");
                sb.Append(@"<a href=""javascript:history.go(0);"">");
                sb.Append(@"<img border=""0"" src=""" + strImagePath + @"icon/refresh.gif"" align=""absmiddle"">");
                sb.Append("&nbsp;Actualizar</a>");
                sb.Append(@"</td>");
                sb.Append(@"</tr>");
                sb.Append("</table>");

                sb.Append("<tr>");
                sb.Append(@"<td width=""*"" valign=""top"">");
                sb.Append(@"<img src=""" + strImagePath + @"file/generic.gif"" align=absmiddle>");
                sb.Append(@"&nbsp;<input type=""file"" name=""fileupload"" />");
                sb.Append(@"</td>");
                sb.Append(@"<td width=""*"">&nbsp;");
                sb.Append(@"</td>");
                sb.Append(@"</tr>");
                sb.Append("</table>");
            }

            Flush();

            return sb.ToString();
        }

        private bool ShowUp()
        {
            int i = (TextUtil.IndexOf(UpUrlStr(), pathInicial.Remove(pathInicial.Length - 1)));
            if (Variables.User.Administrador)
            {
                return true;
            }

            return (i != -1);
        }


        private string WriteRows()
        {
            StringBuilder sb = new StringBuilder("");
            const string strPathError = "El path '{0}' {1} <a href='javascript:history.back();'>Volver</a>";

            if (strAllowedPathPattern != "" && !(Regex.IsMatch(WebPath(), strAllowedPathPattern)))
            {
                intRowsRendered = -1;
                return
                    WriteErrorRow(string.Format(strPathError, WebPath(),
                        "no esta permitido porque no coincide con el patrón: '" +
                        Server.HtmlEncode(strAllowedPathPattern) + "'."));
            }

            string strLocalPath = GetLocalPath("");
            if (!(Directory.Exists(strLocalPath)))
            {
                intRowsRendered = -1;
                return WriteErrorRow(string.Format(strPathError, WebPath(), "no existe."));
            }

            DirectoryInfo[] da;
            FileInfo[] fa;
            try
            {
                DirectoryInfo di = new DirectoryInfo(strLocalPath);
                da = di.GetDirectories();
                fa = di.GetFiles();
            }
            catch (System.Exception ex)
            {
                intRowsRendered = -1;
                return WriteErrorRow(ex);
            }

            DataTable dt = GetFileInfoTable();
            dt.BeginLoadData();
            foreach (DirectoryInfo d in da)
            {
                AddRowToFileInfoTable(d, dt);
            }
            foreach (FileInfo f in fa)
            {
                AddRowToFileInfoTable(f, dt);
            }
            dt.EndLoadData();
            dt.AcceptChanges();

            if (dt.Rows.Count == 0)
            {
                intRowsRendered = 0;
                sb.Append(WriteErrorRow("(sin ficheros)"));
                return sb.ToString();
            }

            DataView dv;
            if (SortColumn() == "")
            {
                dv = dt.DefaultView;
            }
            else
            {
                dv = new DataView(dt);
                if (SortColumn().StartsWith("-"))
                {
                    dv.Sort = "IsFolder, " + TextUtil.Substring(SortColumn(), 1) + " desc";
                }
                else
                {
                    dv.Sort = "IsFolder desc, " + SortColumn();
                }
            }


            intRowsRendered = 0;
            foreach (DataRowView drv in dv)
            {
                if ((intRowsRendered == 0) & modeGallery)
                {
                    sb.Append("<tr>");
                    if (ShowUp())
                    {
                        sb.Append(@"<td align=""center"">" + UpUrl());
                        sb.Append(@"<img border=""0"" src=""" + strImagePath +
                                  @"icon/folderup.gif"" align=""absmiddle"">" + Ui.Lf() + "Subir un nivel</a>" +
                                  Ui.Lf() + "</td>");
                        intRowsRendered = 1;
                    }
                }
                else if ((intRowsRendered%columnas == 0) & modeGallery)
                {
                    sb.Append("<tr>");
                }
                string viewRow = WriteViewRow(drv);
                if (viewRow != "")
                {
                    sb.Append(viewRow);
                    intRowsRendered += 1;
                }
                if ((intRowsRendered%columnas == 0) & modeGallery)
                {
                    sb.Append("</tr>");
                }
            }

            return sb.ToString();
        }


        private DataTable GetFileInfoTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Name", typeof (String)));
            dt.Columns.Add(new DataColumn("IsFolder", typeof (Boolean)));
            dt.Columns.Add(new DataColumn("FileExtension", typeof (String)));
            dt.Columns.Add(new DataColumn("Attr", typeof (String)));
            dt.Columns.Add(new DataColumn("Size", typeof (Int64)));
            dt.Columns.Add(new DataColumn("Modified", typeof (System.DateTime)));
			dt.Columns.Add(new DataColumn("Created", typeof (System.DateTime)));

            return dt;
        }


        private void AddRowToFileInfoTable(FileSystemInfo fi, DataTable dt)
        {
            DataRow dr = dt.NewRow();
            string attr = AttribString(fi.Attributes);

            dr["Name"] = fi.Name;
            dr["FileExtension"] = Path.GetExtension(fi.Name);
            dr["Attr"] = attr;
            if (attr.IndexOf("d") > -1)
            {
                dr["IsFolder"] = true;
                dr["Size"] = 0;
            }
            else
            {
                dr["IsFolder"] = false;
                dr["Size"] = new FileInfo(fi.FullName).Length;
            }
            dr["Modified"] = fi.LastWriteTime;
            dr["Created"] = fi.CreationTime;

            dt.Rows.Add(dr);
        }


        private string SortColumn()
        {
            if (Web.Request("sort") == "")
            {
                return "Name";
            }
            return Web.Request("sort");
        }


        private string WebPath()
        {
            string strPath = Web.Request("path");

            if (string.IsNullOrEmpty(strPath))
            {
                strPath = pathInicial;
            }

            if (!Variables.User.Administrador)
            {
                if (!strPath.EndsWith("/")) strPath = strPath + "/";
                strPath = TextUtil.Replace(strPath, pathInicial, "");
                strPath = pathInicial + strPath;
            }

            return strPath;
        }

        private string UpUrlStr()
        {
            string p = WebPath();
            if (p.EndsWith("/")) p = p.Remove(p.Length - 1);
            string strUp = Regex.Replace(p, "/[^/]+$", "");
            if (strUp == "" | strUp == "/")
            {
                strUp = pathInicial;
            }
            return strUp;
        }

        private string UpUrl()
        {
            string strUp = UpUrlStr();
            return PageUrl(strUp, "");
        }


        private string PageUrl(string newPath, string newSortColumn)
        {
            bool blnSortProvided = (newSortColumn != "");

            if (newPath == "")
            {
                newPath = WebPath();
            }
            if (newSortColumn == "")
            {
                newSortColumn = SortColumn();
            }

            StringBuilder sb = new StringBuilder("");

            sb.Append(@"<a href=""");
            sb.Append(ScriptName);
            sb.Append("?");
            sb.Append("path");
            sb.Append("=");
            sb.Append(newPath);
            if (newSortColumn != "")
            {
                sb.Append("&");
                sb.Append("sort");
                sb.Append("=");
                if (blnSortProvided & (newSortColumn.ToLower() == SortColumn().ToLower()))
                {
                    sb.Append("-");
                }
                sb.Append(newSortColumn);
            }
            sb.Append(@""">");


            return sb.ToString();
        }


        private string FormatKB(long fileLength)
        {
            return string.Format("{0:N0}", (fileLength/1024));
        }


        private string AttribString(FileAttributes a)
        {
            StringBuilder sb = new StringBuilder();
            if (Convert.ToDouble((a & FileAttributes.ReadOnly)) > 0)
            {
                sb.Append("r");
            }
            if (Convert.ToDouble((a & FileAttributes.Hidden)) > 0)
            {
                sb.Append("h");
            }
            if (Convert.ToDouble((a & FileAttributes.System)) > 0)
            {
                sb.Append("s");
            }
            if (Convert.ToDouble((a & FileAttributes.Directory)) > 0)
            {
                sb.Append("d");
            }
            if (Convert.ToDouble((a & FileAttributes.Archive)) > 0)
            {
                sb.Append("a");
            }
            if (Convert.ToDouble((a & FileAttributes.Compressed)) > 0)
            {
                sb.Append("c");
            }
            return sb.ToString();
        }


        private string WebPathCombine(string path1, string path2)
        {
            string strTemp = Path.Combine(path1, path2).Replace(@"\", "/");
            if (strTemp.IndexOf("~/") > -1)
            {
                strTemp = strTemp.Replace("~/", Page.ResolveUrl("~/"));
            }
            return strTemp;
        }


        private string FileIconLookup(DataRowView drv)
        {
            if (IsDirectory(drv))
            {
                return WebPathCombine(strImagePath, "file/folder.gif");
            }

            if (IsImage(drv))
            {
                return Variables.App.directorioPortal + "galeria/thumbnail.aspx?ForceAspect=false&height=" + tamY + "&width=" +
                       tamX + "&image=" + WebPathCombine(WebPath(), Convert.ToString(drv["Name"]));
            }

            switch (Convert.ToString(drv["FileExtension"]))
            {
                case ".txt":
                    return WebPathCombine(strImagePath, "file/text.gif");
                case ".htm":
                case ".xml":
                case ".xsl":
                case ".css":
                case ".html":
                case ".config":
                    return WebPathCombine(strImagePath, "file/html.gif");
                case ".mp3":
                case ".wav":
                case ".wma":
                case ".au":
                case ".mid":
                case ".ram":
                case ".rm":
                case ".snd":
                case ".asf":
                    return WebPathCombine(strImagePath, "file/audio.gif");
                case ".zip":
                case "tar":
                case ".gz":
                case ".rar":
                case ".cab":
                case ".tgz":
                    return WebPathCombine(strImagePath, "file/compressed.gif");
                case ".asp":
                case ".wsh":
                case ".js":
                case ".vbs":
                case ".aspx":
                case ".cs":
                case ".vb":
                    return WebPathCombine(strImagePath, "file/script.gif");
                default:
                    return WebPathCombine(strImagePath, "file/generic.gif");
            }
        }


        private bool IsImage(DataRowView drv)
        {
            switch (Convert.ToString(drv["FileExtension"]).ToLower())
            {
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


        private string WriteViewRow(DataRowView drv)
        {
            string strFileLink;
            string strFileName = Convert.ToString(drv["Name"]);
            string strFilePath = WebPathCombine(WebPath(), strFileName);
            bool blnFolder = IsDirectory(drv);

            if (blnFolder)
            {
                if (strHideFolderPattern != "" &&
                    Regex.IsMatch(strFileName, strHideFolderPattern, RegexOptions.IgnoreCase))
                {
                    return "";
                }
                if (!modeGallery)
                {
                    strFileLink = PageUrl(strFilePath, "") + strFileName + "</a>";
                }
                else
                {
                    strFileLink = PageUrl(strFilePath, "") + @"<img border=""0"" src=""" + FileIconLookup(drv) +
                                  @""" >" + Ui.Lf() + strFileName + "</a>";
                }
            }
            else
            {
                if (strHideFilePattern != "" &&
                    Regex.IsMatch(strFileName, strHideFilePattern, RegexOptions.IgnoreCase))
                {
                    return "";
                }

                if (!modeGallery)
                {
                    strFileLink = @"<a href=""editor.aspx?path=" + WebPath() + "&file=" + MapPath2(strFilePath) +
                                  @"&KeepThis=true&TB_iframe=true&height=500&width=600"" title=""" +
                                  MapPath2(strFilePath) + @""" class=""magnify""><img src=""" +
                                  WebPathCombine(strImagePath, "icon/edit.gif") + @""" border=""0""></a>&nbsp;";
                    strFileLink = strFileLink + @"<a href=""" + strFilePath + @"""" +
                                  (IsImage(drv) ? @" class=""magnify"" rel=""images""" : "") +
                                  @" target=""_blank"">" + strFileName + "</a>";
                    strFileLink = strFileLink + @"&nbsp;<a href=""javascript:download('" + strFileName +
                                  @"');""><img src=""" + strImagePath + @"icon/download.gif"" border=""0""></a>";
                }
                else
                {
                    strFileLink = @"<a href=""" + strFilePath + @"""" +
                                  (IsImage(drv) ? @" class=""fancybox"" rel=""images""" : "") +
                                  @" target=""_blank"">" + @"<img src=""" + FileIconLookup(drv) +
                                  @""" border=""0"">" + Ui.Lf() + strFileName + "</a>";
                }
            }

            StringBuilder sb = new StringBuilder("");


            if (!modeGallery)
            {
                sb.Append("<tr>");
                sb.Append(@"<td align=""right"">");

                if (adminControls)
                {
                    sb.Append(@"<input name=""");
                    sb.Append("checked_");
                    sb.Append(strFileName);
                    sb.Append(@""" type=""checkbox"" />");
                }
                else sb.Append("&nbsp;");

                sb.Append("</td>");
                sb.Append(@"<td align=""center""><img src=""");
                sb.Append(FileIconLookup(drv));
                sb.Append(@""" ");
                sb.Append(">");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(strFileLink);
                sb.Append("</td>");

                if (blnFolder)
                {
                    sb.Append(@"<td align=""left"">&nbsp;</td>");
                    sb.Append(@"<td align=""left"">&nbsp;</td>");
                }
                else
                {
                    sb.Append(@"<td align=""right"">");
                    sb.Append(FormatKB(Convert.ToInt64(drv["Size"])));
                    sb.Append("</td>");
                    sb.Append(@"<td align=""left"">kb");
                    sb.Append("</td>");
                }

                sb.Append(@"<td align=""right"">");
                sb.Append(Convert.ToString(drv["Created"]));
                sb.Append("</td>");
                sb.Append(@"<td align=""right"">");
                sb.Append(Convert.ToString(drv["Modified"]));
                sb.Append("</td>");
                sb.Append(@"<td align=""right"">");
                sb.Append(Convert.ToString(drv["Attr"]));
                sb.Append("</td>");
                sb.Append("</tr>");
            }
            else
            {
                sb.Append(@"<td align=""center"">");

                if (adminControls)
                {
                    sb.Append(@"<input name=""");
                    sb.Append("checked_");
                    sb.Append(strFileName);
                    sb.Append(@""" type=""checkbox"" />");
                }

                sb.Append(strFileLink);
                sb.Append(Ui.Lf());
                sb.Append(Ui.Lf());
                sb.Append("</td>");
            }


            sb.Append(Environment.NewLine);

            Flush();

            return sb.ToString();
        }


        private void Flush()
        {
            if (blnFlushContent)
            {
                //tal y como se estan generando las páginas, no es posible controlar el buffer, por lo que esta opción esta deshabilitada.
                //Response.Flush();
            }
        }


        private string GetLocalPath(string strFilename)
        {
            return Path.Combine(MapPath2(WebPath()), strFilename);
        }

        private string GetLocalPath()
        {
            return GetLocalPath("");
        }


        private string MakeRelativePath(string strFilename)
        {
            string strRelativePath = strFilename.Replace(MapPath2(WebPath()), "");
            if (strRelativePath.StartsWith(@"\"))
            {
                return TextUtil.Substring(strRelativePath, 1);
            }
            return strRelativePath;
        }


        private string GetTargetPath(string strFilename)
        {
            return Path.Combine(Path.Combine(GetLocalPath(), Web.Request("targetfolder")), strFilename);
        }

        private string GetTargetPath()
        {
            return GetTargetPath("");
        }


        private bool IsDirectory(string strFilepath)
        {
            return Directory.Exists(strFilepath);
        }


        private bool IsDirectory(DataRowView drv)
        {
            return Convert.ToString(drv["attr"]).IndexOf("d") > -1;
        }


        private void DeleteFileOrFolder(string strName)
        {
            string strLocalPath = GetLocalPath(strName);
            try
            {
                RemoveReadOnly(strLocalPath);
                if (IsDirectory(strLocalPath))
                {
                    Directory.Delete(strLocalPath, true);
                }
                else
                {
                    System.IO.File.Delete(strLocalPath);
                }
            }
            catch (System.Exception ex)
            {
                fileOperationException = ex;
            }
        }


        private void MoveFileOrFolder(string strName)
        {
            string strLocalPath = GetLocalPath(strName);
            string strTargetPath = GetTargetPath(strName);
            try
            {
                if (IsDirectory(strLocalPath))
                {
                    Directory.Move(strLocalPath, strTargetPath);
                }
                else
                {
                    System.IO.File.Move(strLocalPath, strTargetPath);
                }
            }
            catch (System.Exception ex)
            {
                fileOperationException = ex;
            }
        }


        private void CopyFileOrFolder(string strName)
        {
            string strLocalPath = GetLocalPath(strName);
            string strTargetPath = GetTargetPath(strName);

            try
            {
                if (IsDirectory(strLocalPath))
                {
                    CopyFolder(strLocalPath, strTargetPath, true);
                }
                else
                {
                    System.IO.File.Copy(strLocalPath, strTargetPath);
                }
            }
            catch (System.Exception ex)
            {
                fileOperationException = ex;
            }
        }


        private void ZipFileOrFolder(ArrayList fileList)
        {
            string zipTargetFile = GetLocalPath(fileList.Count == 1 ? Path.ChangeExtension(Convert.ToString(fileList[0]), ".zip") : "FicheroZIP.zip");

            FileStream zfs = null;
            ZipOutputStream zs = null;
            try
            {
                zfs = System.IO.File.Exists(zipTargetFile) ? System.IO.File.OpenWrite(zipTargetFile) : System.IO.File.Create(zipTargetFile);

                zs = new ZipOutputStream(zfs);

                ExpandFileList(ref fileList);

                foreach (string strName in fileList)
                {
                    ZipEntry ze;
                    if (strName.IndexOf(@"\") > -1 & !(strName.StartsWith(@"\")))
                    {
                        ze = new ZipEntry(@"\" + strName);
                    }
                    else
                    {
                        ze = new ZipEntry(strName);
                    }

					ze.DateTime = System.DateTime.Now;
                    zs.PutNextEntry(ze);

                    FileStream fs = null;
                    try
                    {
                        fs = System.IO.File.OpenRead(GetLocalPath(strName));
                        byte[] buffer = new byte[2049];
                        int len = fs.Read(buffer, 0, buffer.Length);
                        while (len > 0)
                        {
                            zs.Write(buffer, 0, len);
                            len = fs.Read(buffer, 0, buffer.Length);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        fileOperationException = ex;
                    }
                    finally
                    {
                        if (fs != null)
                        {
                            fs.Close();
                        }
                        zs.CloseEntry();
                    }
                }
            }
            finally
            {
                if (zs != null)
                {
                    zs.Close();
                }
                if (zfs != null)
                {
                    zfs.Close();
                }
            }
        }


        private void RenameFileOrFolder(string strName)
        {
            int intTagLoc = strName.IndexOf("_2_");
            if (intTagLoc == -1)
            {
                return;
            }

            string strOldName = TextUtil.Substring(strName, 0, intTagLoc);
            string strNewName = TextUtil.Substring(strName, intTagLoc + "_2_".Length);
            if (strOldName == strNewName)
            {
                return;
            }

            string strOldPath = GetLocalPath(strOldName);
            string strNewPath = GetLocalPath(strNewName);

            try
            {
                if (IsDirectory(strOldPath))
                {
                    Directory.Move(strOldPath, strNewPath);
                }
                else
                {
                    System.IO.File.Move(strOldPath, strNewPath);
                }
            }
            catch (System.Exception ex)
            {
                fileOperationException = ex;
            }
        }


        private void MakeFolder(string strFilename)
        {
            string strLocalPath = GetLocalPath(strFilename);
            try
            {
                if (!(Directory.Exists(strLocalPath)))
                {
                    Directory.CreateDirectory(strLocalPath);
                }
            }
            catch (System.Exception ex)
            {
                fileOperationException = ex;
            }
        }


        private void CopyFolder(string strSourceFolderPath, string strDestinationFolderPath, bool blnOverwrite)
        {
            if (!(Directory.Exists(strDestinationFolderPath)))
            {
                Directory.CreateDirectory(strDestinationFolderPath);
            }

            foreach (string strFilePath in Directory.GetFiles(strSourceFolderPath))
            {
                string strFileName = Path.GetFileName(strFilePath);
                System.IO.File.Copy(strFilePath, Path.Combine(strDestinationFolderPath, strFileName), blnOverwrite);
            }

            foreach (string strFolderPath in Directory.GetDirectories(strSourceFolderPath))
            {
                string strFolderName = Regex.Match(strFolderPath, @"[^\\]+$").ToString();
                CopyFolder(strFolderPath, Path.Combine(strDestinationFolderPath, strFolderName), blnOverwrite);
            }
        }


        private void ExpandFileList(ref ArrayList fileList)
        {
            ArrayList newFileList = new ArrayList();

            for (int i = fileList.Count - 1; i >= 0; i += -1)
            {
                string strLocalPath = GetLocalPath(Convert.ToString(fileList[i]));
                if (IsDirectory(strLocalPath))
                {
                    fileList.Remove(fileList[i]);
                    AddFilesFromFolder(strLocalPath, ref newFileList);
                }
            }

            if (newFileList.Count > 0)
            {
                fileList.AddRange(newFileList);
            }
        }


        private void AddFilesFromFolder(string strFolderName, ref ArrayList fileList)
        {
            if (!(Directory.Exists(strFolderName)))
            {
                return;
            }

            try
            {
                foreach (string strName in Directory.GetFiles(strFolderName))
                {
                    fileList.Add(MakeRelativePath(strName));
                }
            }
            catch (System.Exception ex)
            {
                fileOperationException = ex;
            }

            try
            {
                foreach (string strName in Directory.GetDirectories(strFolderName))
                {
                    AddFilesFromFolder(strName, ref fileList);
                }
            }
            catch (System.Exception ex)
            {
                fileOperationException = ex;
            }
        }


        private void RemoveReadOnly(string strPath)
        {
            if (IsDirectory(strPath))
            {
                foreach (string strFile in Directory.GetFiles(strPath))
                {
                    RemoveReadOnly(strFile);
                }
                foreach (string strFolder in Directory.GetDirectories(strPath))
                {
                    RemoveReadOnly(strFolder);
                }
            }
            else
            {
                FileInfo fi = new FileInfo(strPath);
                if (Convert.ToInt32(fi.Attributes & FileAttributes.ReadOnly) != 0)
                {
                    fi.Attributes = fi.Attributes ^ FileAttributes.ReadOnly;
                }
            }
        }


        private string CurrentIdentity()
        {
            return WindowsIdentity.GetCurrent().Name;
        }


        private string GetFriendlyErrorMessage(System.Exception ex)
        {
            string strMessage = ex.Message;
            if (ex is UnauthorizedAccessException)
            {
                strMessage += " La cuenta '" + CurrentIdentity() +
                              "' puede no tener permisos en este fichero o carpeta.";
            }
            return strMessage;
        }


        private string WriteError(System.Exception ex)
        {
            return WriteError(GetFriendlyErrorMessage(ex));
        }

        private string WriteError(string strText)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(@"<div class=""Error"">");
            sb.Append(strText);
            sb.Append("</div>");

            return sb.ToString();
        }

        private string WriteErrorRow(System.Exception ex)
        {
            return WriteErrorRow(GetFriendlyErrorMessage(ex));
        }

        private string WriteErrorRow(string strText)
        {
            StringBuilder sb = new StringBuilder("");

            sb.Append(@"<tr><td><td><td colspan=""5""><div class=""Error"">");
            sb.Append(strText);
            sb.Append("</div>");
            sb.Append(Ui.Lf() + Ui.Lf());
            sb.Append("</td>");
            sb.Append("</td>");
            sb.Append("</td>");
            sb.Append("</tr>");

            return sb.ToString();
        }
    }
}