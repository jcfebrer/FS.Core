// // <fileheader>
// // <copyright file="UI.cs" company="Febrer Software">
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
using System.Drawing;
using System.IO;
using System.Text;
using FSLibrary;
using FSQueryBuilder;
using FSQueryBuilder.Enums;
using FSQueryBuilder.QueryParts.Where;
using System.Configuration;
using FSNetwork;
using FSDatabase;
using FSException;
using System.Web;

#endregion

namespace FSPortal
{
	public static class Ui
	{
		public const int MaxVisibleGridFields = 1000;

		public static string Form(string id, string action)
		{
			return @"<form name=""" + id + @""" method=""post"" action=""" + action + @""" />";
		}

		public static string EndForm()
		{
			return @"<form />";
		}
		
		public static string Button(string value, string javascript, string name)
		{
			return @"<input type=""" + ((javascript != "") ? "button" : "submit") + @"""" +
			((name != "") ? @" name=""" + name + @"""" : "") +
			@" value=""" + value + @"""" +
			((javascript != "") ? @" onclick=""" + javascript + @"""" : "") + @" />";
		}

		public static string Button(string value, string javascript)
		{
			return Button(value, javascript, "");
		}

		public static string TextBox(string name, string value, int size)
		{
			return @"<input size=""" + size + @""" type=""text"" name=""" + name + @""" value=""" + value + @""" />";
		}

        public static string Checkbox(string name, string value)
        {
            return Checkbox(name, value, true);
        }

        public static string Checkbox(string name, string value, bool enable)
        {
            return @"<input type=""checkbox"" name=""" + name + (value.ToLower()=="true"?"checked":"") + (enable?"":"disabled") + " />";
        }

        public static string Link(string text, string link)
		{
			return @"<a href=""" + link + @""">" + text + "</a>";
		}

		public static string Center(string text)
		{
			return @"<div align=""center"">" + text + "</div>";
		}

		public static string Left(string text)
		{
			return @"<div align=""left"">" + text + "</div>";
		}

		public static string Right(string text)
		{
			return @"<div align=""right"">" + text + "</div>";
		}

		public static string Lf()
		{
			return "<br />";
		}

		public static string Italic(string text)
		{
			return "<i>" + text + "</i>";
		}

		public static string Bold(string text)
		{
			return "<b>" + text + "</b>";
		}
		
		public static string HtmlTable(DataTable dt)
		{
			return HtmlTable(dt, -1, "");
		}

		public static string HtmlTable(DataTable dt, int linkColumn, string linkPage)
        {
            StringBuilder html = new StringBuilder();

            if (dt.TableName.ToString() != ""){
                html.AppendLine("<p><b>" + dt.TableName + "</b></p>");
            }
			html.AppendLine("<table>");
			
			//cabecera
			html.AppendLine("<tr>");
			for (int i = 0; i < dt.Columns.Count; i++)
				html.AppendLine("<td>" + dt.Columns[i].ColumnName + "</td>");
			html.AppendLine("</tr>");
			
			//lineas
			for (int i = 0; i < dt.Rows.Count; i++) {
				html.AppendLine("<tr>");
				for (int j = 0; j < dt.Columns.Count; j++)
				{
                    if (j == linkColumn)
                        html.AppendLine("<td><a href='" + linkPage + dt.Rows[i][j].ToString() + "'>" + dt.Rows[i][j].ToString() + "</a></td>");
                    else
                    {
                        if (Functions.Valor(dt.Rows[i][j]).ToLower() == "true")
                        {
                            html.AppendLine(@"<td><input type=""checkbox"" checked disabled></td>");
                        }
                        else if (Functions.Valor(dt.Rows[i][j]).ToLower() == "false")
                        {
                            html.AppendLine(@"<td><input type=""checkbox"" disabled></td>");
                        }
                        else
                        {
                            html.AppendLine("<td>" + dt.Rows[i][j].ToString() + "</td>");
                        }
                    }
				}
				html.AppendLine("</tr>");
			}
			html.AppendLine("</table>");
			return html.ToString();
		}

        public static string Captcha()
        {
            string pk = ConfigurationManager.AppSettings["Google.ReCaptcha.Secret"];
            string data = "<div id=\"html_recaptcha\"></div>";

            data += @"<script type=""text/javascript"">";
            data += @"var verifyCallback = function(response) {";
            data += @"//alert(response);";
            data += @"};";
            data += @"var onloadCallback = function() {";
            data += @"grecaptcha.render('html_recaptcha', {";
            data += @"'sitekey' : '" + pk + "',";
            data += @"'callback' : verifyCallback,";
            data += @"'theme' : 'light'";
            data += @"});";
            data += @"};";
            data += @"</script>";
            data += @"<script src=""https://www.google.com/recaptcha/api.js?hl=es&onload=onloadCallback&render=explicit"" async defer>";
            data += @"</script>";

            return data;
        }


        public static string GoogleMaps(decimal longitude, decimal latitude, int zoom)
        {
            string key = ConfigurationManager.AppSettings["Google.Maps.KEY"];
            string data = "<div id=\"map\"></div>";

            data += "<script>";
            data += "var map;";
            data += "function initMap() {";
            data += "    map = new google.maps.Map(document.getElementById('map'), {";
            data += "    center: { lat: " + latitude.ToString().Replace(",",".") + ", lng: " + longitude.ToString().Replace(",",".") + "},zoom: " + zoom.ToString();
            data += "});";
            data += "}";
            data += "</script>";
            data += @"<script src=""https://maps.googleapis.com/maps/api/js?key=" + key + @"&callback=initMap"" async defer></script>";

            return data;
        }
        public static string IlikeIt()
		{
			string data = @"<iframe src=""http://www.facebook.com/plugins/like.php?href=" + Web.CurrentUrl() + @"""";
			data += @" scrolling=""no"" frameborder=""0"" style=""border:none; width:450px; height:80px""></iframe>";
			return data;
		}


        public static string Calendar(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<div id=""datepicker" + id + @""" type=""text"">");
            sb.AppendLine("&nbsp;</div>");
            sb.AppendLine(@"<div id=""showCalendar" + id + @""" type=""text"">");
            sb.AppendLine("&nbsp;</div>");
            sb.AppendLine(@"<script type=""text/javascript"">");
            sb.AppendLine("$(document).ready(function(){");
            sb.AppendLine(@"$(""#datepicker" + id + @""").datepicker({firstDay:1,monthNames:[""Enero"",""Febrero"",""Marzo"",""Abril"",""Mayo"",""Junio"",""Julio"",""Agosto"",""Septiembre"",""Octubre"",""Noviembre"",""Diciembre""],dayNamesMin:[""Do"",""Lu"",""Ma"",""Mi"",""Ju"",""Vi"",""Sa""],dayNames: [""Domingo"",""Lunes"", ""Martes"", ""Miercoles"", ""Jueves"", ""Viernes"", ""Sabado""],onChangeMonthYear: function(year, month, inst) { $(""#showCalendar" + id + @""").load(""{directorioPortal}pagina.aspx?simple=1&id=""+month+""&month=""+month+""&year=""+year); }});");
            sb.AppendLine("});");
            sb.AppendLine("</script>");
            return sb.ToString();
        }
        
        
		public static string Image(string image, int border, string alt)
		{
			return @"<img src=""" + Variables.App.directorioPortal + "imagenes/" + image + @"""" + @" border=""" + border +
			@""" alt=""" + alt + @""" />";
		}
        
		public static string Image(Bitmap bitmap)
		{
			string file = Guid.NewGuid().ToString() + ".jpg";
			string path = Web.ServerMapPath("~\\temp\\") + "\\" + file;
			bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
			return String.Format("<img src=\"{0}\" />", Variables.App.directorioPortal + "temp/" + file);
		}
        
		public static string ImageMemory(Bitmap bitmap)
		{
			byte[] imgBytes = TurnImageToByteArray(bitmap);
			string imgString = Convert.ToBase64String(imgBytes);
			return String.Format("<img src=\"data:image/Bmp;base64,{0}\" />", imgString);
		}
        
		private static byte[] TurnImageToByteArray(System.Drawing.Image img)
		{
			MemoryStream ms = new MemoryStream();
			img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
			return ms.ToArray();
		}
		
		
		public static string HtmlEditor(string name, string value, int columns, bool activate)
		{
			// este cambio nos permite visualizar correctamente las imagenes en el editor.
			//value = Text.Replace(value, @"src=""{directorioPortal}", @"src=""" + Variables.App.directorioPortal);
			//value = Text.Replace(value, @"src=""{directorioWeb}", @"src=""" + Variables.App.directorioWeb);
			value = System.Web.HttpUtility.HtmlDecode(value);
			value = TextUtil.Replace(value, "&quot;", @"""");
			//value = Text.Replace(value, @"""""", @"""");
            
			value = Web.ReplaceImg(value, "{directorioPortal}", Variables.App.directorioPortal);
			value = Web.ReplaceImg(value, "{directorioWeb}", Variables.App.directorioWeb);
            
			value = System.Web.HttpUtility.HtmlEncode(value);
			value = TextUtil.Replace(value, @"""", "&quot;");
   
			// este cambio nos permite visualizar correctamente la comita simple.
			value = TextUtil.Replace(value, "{cs}", "'");

			//value = Text.ReplaceRecursive(value, "&quot;&quot;", "&quot;");
			
            switch(Variables.App.htmlEditor)
            {
                case Variables.HtmlEditorType.CKeditor:
                    return CkEditorCode(name, value, columns, activate);
                case Variables.HtmlEditorType.TextArea:
                    return TextArea(name, value, columns, activate);
                case Variables.HtmlEditorType.TinyMce:
                    return TinyMce(name, value, columns, activate);
                default:
                    return TextArea(name, value, columns, activate);
            }
		}

		public static string HtmlEditor(string name, string value)
		{
			return HtmlEditor(name, value, 100, true);  //20
		}
		
		
		/// <summary>
		///     Editor Textarea
		/// </summary>
		private static string TextArea(string name, string value, int columns, bool activate)
		{

			return @"<textarea " + (activate ? "" : "readonly") + @" cols=""" + columns + @""" id=""" + name + @""" name=""" + name +
			@""" rows=""15"">" + value + "</textarea>";
		}


		/// <summary>
		///     Editor CKEditor
		/// </summary>
		private static string CkEditorCode(string name, string value, int columns, bool activate)
		{

			string editor = @"<textarea cols=""" + columns + @""" id=""" + name + @""" name=""" + name +
			                @""" rows=""15"">" + value + "</textarea>";
			if (activate) {
				editor += @"<script type=""text/javascript"" language=""javascript"">";
				//editor += "CKEDITOR.replace('" + name + "', ";
				//editor += "{";
				//editor += "filebrowserBrowseUrl : '" + Variables.App.directorioPortal + "javascript/ckfinder/ckfinder.html',";
				//editor += "filebrowserImageBrowseUrl : '" + Variables.App.directorioPortal + "javascript/ckfinder/ckfinder.html?Type=Images',";
				//editor += "filebrowserFlashBrowseUrl : '" + Variables.App.directorioPortal + "javascript/ckfinder/ckfinder.html?Type=Flash',";
				//editor += "filebrowserUploadUrl : '" + Variables.App.directorioPortal + "javascript/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files',";
				//editor += "filebrowserImageUploadUrl : '" + Variables.App.directorioPortal + "javascript/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images',";
				//editor += "filebrowserFlashUploadUrl : '" + Variables.App.directorioPortal + "javascript/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash'";
				//editor += "});";

				editor += @"$(""#" + name + @""").ckeditor( function() {},";

				editor += "{";
				editor += "filebrowserBrowseUrl : '" + Variables.App.directorioPortal + "javascript/ckfinder/ckfinder.html',";
				editor += "filebrowserImageBrowseUrl : '" + Variables.App.directorioPortal +
				"javascript/ckfinder/ckfinder.html?Type=Images',";
				editor += "filebrowserFlashBrowseUrl : '" + Variables.App.directorioPortal +
				"javascript/ckfinder/ckfinder.html?Type=Flash',";
				editor += "filebrowserUploadUrl : '" + Variables.App.directorioPortal +
				"javascript/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files',";
				editor += "filebrowserImageUploadUrl : '" + Variables.App.directorioPortal +
				"javascript/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images',";
				editor += "filebrowserFlashUploadUrl : '" + Variables.App.directorioPortal +
				"javascript/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash'";
				editor += "});";

				editor += "</script>";
			}
			return editor;
		}
		

		/// <summary>
		///     Editor TinyMCE
		/// </summary>
		private static string TinyMce(string name, string value, int columns, bool activate)
		{
			string editor = @"<textarea cols=""" + columns + @""" id=""" + name + @""" name=""" + name +
			                @""" rows=""15"">" + value + "</textarea>";
			if (activate) {
				editor += @"<script type=""text/javascript"" language=""javascript"">";
				editor += "tinymce.init({";
				//editor += "mode: 'textareas',";
				editor += "selector: '#" + name + "',";
				editor += "language: 'es',";
				editor += "height: 500,";
				editor += "theme: 'modern',";
				editor += "plugins: [";
				editor += "'advlist autolink lists link image charmap preview hr anchor pagebreak',";
				editor += "'searchreplace visualblocks visualchars fullscreen',";
				editor += "'insertdatetime media nonbreaking save table contextmenu directionality',";
				editor += "'paste textcolor colorpicker textpattern imagetools toc code'";
				editor += "],";
				editor += "toolbar1: 'undo redo | insert | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image";
				editor += " | preview media | forecolor backcolor | code fullscreen',";
				editor += "image_advtab: true,";
				editor += "menubar: false,";
				
				editor += "setup: function (editor) {";
				editor += "editor.on('change', function () {";
				editor += "editor.save();";
				editor += "});";
				editor += "}";

				editor += "});";

				editor += "</script>";
			}
			return editor;
		}

		///// <summary>
		/////     Editor FCK
		///// </summary>
		///// <param name="name"></param>
		///// <param name="value"></param>
		///// <param name="width"></param>
		///// <param name="height"></param>
		///// <returns></returns>
		//public static string _FCKEditorCode(string name, string value, int width, int height)
		//{
		//    return _FCKEditorCode(name, value, "", "", width, height, false);
		//}

		///// <summary>
		/////     Editor FCK
		///// </summary>
		///// <param name="name"></param>
		///// <param name="value"></param>
		///// <param name="cssPath"></param>
		///// <param name="toolbar"></param>
		///// <param name="width"></param>
		///// <param name="height"></param>
		///// <param name="fullHtml"></param>
		///// <returns></returns>
		//public static string _FCKEditorCode(string name, string value, string cssPath, string toolbar, int width,
		//    int height, bool fullHtml)
		//{
		//    string fckEditorPath = Variables.App.directorioPortal + "editorHtml/";

		//    if (toolbar == "") toolbar = "Editor";
		//    if (cssPath == "") cssPath = Variables.App.directorioWeb + "estilos/estilofp.css";

		//    string code = "<div><input type='hidden' id='" + name + "' name='" + name + "' value='" + value +
		//                  "' /><input type='hidden' id='" + name +
		//                  "___Config' value='EditorAreaCSS=" + cssPath + "&HtmlEncodeOutput=true&FullPage=" + fullHtml +
		//                  "' /><iframe id='" + name + "___Frame' src='" + fckEditorPath +
		//                  "editor/fckeditor.html?InstanceName=" + name + "&Toolbar=" + toolbar + "' width='" + width +
		//                  "px' height='" + height +
		//                  "px' frameborder='no' scrolling='no'></iframe></div>";
		//    return code;
		//}


		//public static string ScriptCookies()
		//{
		//    Modulos modulos = new Modulos();
		//    StringBuilder sb = new StringBuilder("");

		//    sb.Append("\r\n" + @"<script type=""text/javascript"" language=""javascript"">");
		//    sb.Append("\r\n" + "<!--");
		//    sb.Append("\r\n" + @"document.cookie = ""TestCookie=1"";");
		//    sb.Append("\r\n" + @"if (document.cookie == """")");
		//    sb.Append("\r\n" + @"window.location = """ + Variables.App.directorioPortal + @"includes/error.aspx?id=1"";");
		//    sb.Append("\r\n" + "// -->");
		//    sb.Append("\r\n" + "</script>");
		//    sb.Append("\r\n" + "<noscript>");
		//    sb.Append("\r\n" + "<center>");
		//    //sb.Append("\r\n" + modulos.Cabecera(-1, "Scripts"));
		//    sb.Append("\r\n" + "Su explorador no permite la ejecución de scripts." + Lf());
		//    sb.Append("\r\n" + @"<a href=""" + Variables.App.directorioPortal + @"includes/error.aspx?id=2"">Más información.</a>");
		//    //sb.Append("\r\n" + modulos.Pie(-1));
		//    sb.Append("\r\n" + "</center>");
		//    sb.Append("\r\n" + "</noscript>");

		//    return sb.ToString();
		//}

		public static string EditAll()
		{
			string edit;
			edit = Ui.EditPage("Plantillas", "plantilla", Variables.App.Page.plantilla, "Editar plantilla", "", "{directorioPortal}imagenes/editarplantilla.png", "");
			edit += Ui.EditPage("Paginas", "idPagina", Variables.App.Page.pageId.ToString(), "Editar página", "", "{directorioPortal}imagenes/editarpagina.png", "");
			edit += Ui.Link(Ui.Image("configuracion.png", 0, "Administración"), "{directorioPortal}admin/default.aspx");
			edit += Ui.Link(Ui.Image("salir.png", 0, "Salir"), "{directorioPortal}servicios/Desconectar.aspx");

			return edit;
		}

		public static string EditPage(string tableName, string idName, string idValue)
		{
			return EditPage(tableName, idName, idValue, "Editar");
		}

		public static string EditPage(string tableName, string idName, string idValue, string altEditar)
		{
			return EditPage(tableName, idName, idValue, altEditar, "Borrar");
		}

        public static string EditPage(string tableName, string idName, string idValue, string altEditar,
            string altBorrar)
        {
            return EditPage(tableName, idName, idValue, altEditar, altBorrar, "{directorioPortal}imagenes/edit.gif", "{directorioPortal}imagenes/papelera.gif");
        }


        public static string EditPage(string tableName, string idName, string idValue, string altEditar,
			string altBorrar, string imagenEditar, string imagenBorrar)
		{
			StringBuilder sb = new StringBuilder("");

			if (Variables.User.Administrador) {
				int lenPt = TextUtil.Length(Variables.App.prefijoTablas);
				if (TextUtil.Substring(tableName, 0, lenPt) != Variables.App.prefijoTablas) {
					tableName = Variables.App.prefijoTablas + tableName;
				}
				string p = "?simple=0&q=&amp;tablename=" + tableName + "&amp;fld=" + idName + "&amp;val=" + idValue + @"&amp;page=1";

                if (imagenEditar != "")
                {
                    sb.Append(@"<a href=""" + Variables.App.directorioPortal +
                    "admin/editor/showrecord.aspx" + p + @"""><img src=""" + imagenEditar + @""" title=""" + altEditar + "(" + idValue + ")" + @""" alt=""" + altEditar +
                    "(" + idValue + ")" + @""" border=""0"" /></a>");
                }

                if (imagenBorrar != "")
                {
                    sb.Append(@"<a href=""" + Variables.App.directorioPortal +
                    "admin/editor/deleterecord.aspx" + p + @"""><img src=""" + imagenBorrar + @""" title=""" + altBorrar + "(" + idValue + ")" + @""" alt=""" +
                    altBorrar + "(" + idValue + ")" + @""" border=""0"" /></a>");
                }
			}

			return sb.ToString();
		}

		//public static string CopyRight()
		//{
		//    StringBuilder sb = new StringBuilder("");

		//    if (ConfigurationManager.AppSettings["copyright"] != "")
		//    {
		//        sb.Append(Functions.Decrypt(ConfigurationManager.AppSettings["copyright"]));
		//    }
		//    else
		//    {
		//        sb.Append("© " + DateTime.Now.Year +
		//                  @" FebrerSoftware - <a href=""http://www.febrersoftware.com"" target=""_blank"">http://www.febrersoftware.com</a>");
		//    }

		//    return Center(sb.ToString());
		//}


		private static string GridNames(string tableName, int cid)
		{
			StringBuilder sb = new StringBuilder("");
			DataTable dtSchema;

			if (Variables.App.UseXML)
			{
				XML xml = new XML(Variables.App.directorioWeb + "data");
				xml.Load(tableName + ".xml");
				dtSchema = xml.GetSchema();
			}
			else
			{
				BdUtils db = new BdUtils(cid);
				dtSchema = db.GetSchemaTable(tableName);
			}

			bool first = true;
			int tot = 0;
			bool existFechaModificacion = false;

			foreach (DataRow fld in dtSchema.Rows) {
				string fieldName = fld["ColumnName"].ToString();
				string desc = "";
				if (!Variables.App.UseXML)
				{
					BdUtils db = new BdUtils(cid);
					desc = db.GetDescription(tableName, fieldName);
				}
				if (desc != "") {
					fieldName = desc;
				}

                int columnSize = NumberUtils.NumberInt(fld["ColumnSize"].ToString());
                if (columnSize < 65535)
                {  // solo mostramos las columnas que no son MEMO
					if (fld["ColumnName"].ToString() != "fechaModificacion")
					{
						if (!first)
							sb.Append(",");
						sb.Append("'" + fieldName + "'");
					}
					else
						existFechaModificacion = true;

					first = false;
                }

				tot++;
                if (tot > MaxVisibleGridFields)
                    break;
            }
			
			if (existFechaModificacion) {
				sb.Append(",");
				sb.Append("'Fecha Modificación'");
			}

			return sb.ToString();
		}


		private static string GridModel(string tableName, int cid)
		{
			StringBuilder sb = new StringBuilder("");
			DataTable dtSchema;

			if (Variables.App.UseXML)
			{
				XML xml = new XML(Variables.App.directorioWeb + "data");
				xml.Load("" + tableName + ".xml");
				dtSchema = xml.GetSchema();
			}
			else
			{
				BdUtils db = new BdUtils(cid);
				dtSchema = db.GetSchemaTable(tableName);
			}

			bool first = true;
			int tot = 0;
			bool existFechaModificacion = false;

			foreach (DataRow fld in dtSchema.Rows) {
				string fieldName = fld["ColumnName"].ToString();
                int columnSize = NumberUtils.NumberInt(fld["ColumnSize"].ToString());
                if (columnSize < 75) columnSize = 75;
                if (columnSize < 65535)
                {  // solo mostramos las columnas que no son MEMO
                    if (fieldName != "fechaModificacion")
                    {
                        if (!first)
                            sb.Append(",");
                        sb.AppendLine("{name: '" + fieldName + "',index: '" + fieldName +
                        "', search: true, sortable: true, width: " + columnSize * 3 + ", align: 'left'}");
                    }
					else
						existFechaModificacion = true;

					first = false;
                }
				tot++;
                if (tot > MaxVisibleGridFields)
                    break;
            }
			
			if (existFechaModificacion) {
				sb.Append(",");
				sb.Append("{name: 'fechaModificacion',index: 'fechaModificacion', search: true, sortable: true, width: 80, align: 'left'}");
			}
			
			return sb.ToString();
		}


		public static string CreateFlexiGrid(string gridName, string tableName, int cid)
		{
			StringBuilder sb = new StringBuilder("");
			string sortName1;
			string sortName0;

			if (Variables.App.UseXML)
			{
				XML xml = new XML(Variables.App.directorioWeb + "data");
				xml.Load("" + tableName + ".xml");
				sortName1 = xml.DataTable.Columns[1].ColumnName;
				sortName0 = xml.DataTable.Columns[0].ColumnName;
			}
			else
			{
				BdUtils db = new BdUtils(cid);
				sortName1 = db.Column(tableName, 1);
				sortName0 = db.Column(tableName, 0);
			}

			sb.Append(@"<table id=""" + gridName + @"""></table>");
			sb.Append(@"<div id=""jqpager" + gridName + @"""></div>");

			sb.AppendLine(@"<script type=""text/javascript"">");

			sb.AppendLine(@"$(""#" + gridName + @""").jqGrid(");
			sb.AppendLine("{");
			sb.AppendLine("url: '" + Variables.App.directorioPortal + "admin/editor/loaddata.aspx?cid=" + cid + "&tablename=" + tableName + "',");
			sb.AppendLine("datatype: 'json',");
			sb.AppendLine("colNames: [");
			sb.AppendLine(GridNames(tableName, cid));
			sb.AppendLine("],");
			sb.AppendLine("colModel: [");
			sb.AppendLine(GridModel(tableName, cid));
			sb.AppendLine("],");
            //sb.AppendLine("shrinkToFit : true,");
			sb.AppendLine("autowidth: true,");
			sb.AppendLine("sortname: '" + sortName1 + "',");
			sb.AppendLine("viewrecords: true,");
			sb.AppendLine("rowNum: " + Variables.App.registrosPorPagina + ",");
			sb.AppendLine("rowList: [" + Variables.App.registrosPorPagina + "," + Variables.App.registrosPorPagina * 3 + "," +
			Variables.App.registrosPorPagina * 5 + "],");
			sb.AppendLine("pager: '#jqpager" + gridName + "',");
			//sb.AppendLine("width: 700,");
			sb.AppendLine("height: 250,");
			sb.AppendLine("caption: '" + tableName + "',");

			sb.AppendLine("onSelectRow: function(id) {");

			sb.AppendLine(@"location.href = '" + Variables.App.directorioPortal +
			"admin/editor/showrecord.aspx?simple=0&cid=" + cid + "&q=&tablename=" + tableName + "&fld=" +
			sortName0 + @"&val=' + id + '&page=1';");

			//sb.AppendLine(@"$.ajax( { url: '" + Variables.App.directorioPortal + "admin/editor/showrecord.aspx?simple=0&q=&tablename=" + tableName + "&fld=" + db.Column(tableName, 0) + @"&val=' + id + '&page=1' , ");

			//sb.AppendLine("success: function(data) { $.fancybox(data,{");

			//sb.AppendLine("'autoDimensions': false,'frameWidth': 700, 'frameHeight': 450,");

			//sb.AppendLine("'onComplete': function() { editorActions(); }");

			//sb.AppendLine("} ); },");

			//sb.AppendLine("});");

			sb.AppendLine("}");

			sb.AppendLine("}");
			sb.AppendLine(");");

			sb.AppendLine(@"$(""#" + gridName + @""").jqGrid('navGrid','#jqpager" + gridName +
			"',{edit:false,add:false,del:false,search:true,refresh:true});");

			sb.AppendLine("</script>");

			return sb.ToString();
		}


		/// <summary>
		///     Devuelve el código html de un combo, con el elemento seleccionado 'valor'.
		///     Para la tabla 'tabla'. Campo, se utiliza para indicar el nombre del combo.
		///     Este método se utiliza para tablas relacionadas.
		/// </summary>
		/// <param name="tabla"></param>
		/// <param name="campo"></param>
		/// <param name="valor"></param>
		/// <returns></returns>
		public static string DameFrmCombo(int cid, string tabla, string campo, string valor)
		{
            string schema = "";
			BdUtils db = new BdUtils(cid);
			StringBuilder s = new StringBuilder("");
			DataTable dt2Table = db.GetSchemaForeignKeys();

            if (tabla.IndexOf('.') > 0)
            {
                schema = tabla.Split('.')[0] + ".";
                tabla = tabla.Split('.')[1];
            }

			foreach (DataRow rs2 in dt2Table.Rows) {
				string tableData = schema + rs2["PK_TABLE_NAME"].ToString();
				if (Functions.Valor(rs2["FK_TABLE_NAME"]).ToLower() == tabla.ToLower() &
				    Functions.Valor(rs2["FK_Column_Name"]).ToLower() == campo.ToLower()) {
					DataTable dtConsulta = (DataTable)Web.GetCacheValue("tabla_" + tableData);

					//DataTable dtColumns = db.GetSchemaColumn2(tableData);
                    string keyColumn = db.GetTablePrimaryKey(tableData); //dtColumns.Rows[0]["ColumnName"].ToString();
                    string dataColumn = db.GetTableDescriptionField(tableData); //dtColumns.Rows[1]["ColumnName"].ToString();

					if (dtConsulta == null) {
						string sSql = "SELECT " + keyColumn + "," + dataColumn + " FROM " + tableData + " ORDER BY " +
						              dataColumn;

						dtConsulta = db.Execute(sSql);
						Web.SetCacheValue("tabla_" + tableData + "_ID" + db.ConnStringEntryId, dtConsulta);
					}

					s.Append(@"<select name=""" + campo + @""">" + "\r\n");
					s.Append(@"<option value="""">&nbsp;</option>" + "\r\n");

					foreach (DataRow row in dtConsulta.Rows) {
						string flagnbh = valor == row[keyColumn].ToString() ? @" selected=""selected""" : "";

						s.Append(@"<option value=""" + row[keyColumn] + @"""" + flagnbh + ">" +
						TextUtil.Substring(row[1].ToString(), 0, 40) + "</option>" + "\r\n");
					}
					s.Append(@"</select>");

					break;
				}
			}

			return s.ToString();
		}


		/// <summary>
		///     Devuelve el contanido de una tabla en formato arbol
		/// </summary>
		/// <param name="tabla"></param>
		/// <param name="campoAgrupar"></param>
		/// <param name="campoPadre"></param>
		/// ///
		/// <param name="campoMostrar"></param>
		/// <returns></returns>
		public static string DameFrmArbol(int cid, string tabla, string campoAgrupar, string campoPadre, string campoMostrar)
		{
			BdUtils db = new BdUtils(cid);

			DataTable dtData =
				db.Execute("select * from " + Variables.App.prefijoTablas + tabla + " order by " + campoAgrupar + "," +
				campoMostrar);

			StringBuilder sb = new StringBuilder("");

			foreach (DataRow row in dtData.Rows) {
				bool hasParent = HasParent(row, campoPadre);

				if (hasParent) {
					//sb.Append("\r\n" + ("<div " + Functions.MuestraOcultaModulo(row[campoAgrupar].ToString()) + " id='mm_" + row[campoAgrupar].ToString() + "'>"));
					sb.Append("<b>[+" + row[campoAgrupar]);
					sb.Append("#" + InvertParent(Parent(dtData, campoPadre, campoAgrupar, row[campoPadre].ToString())) +
					"]</b>");
					sb.Append(" " + row[campoMostrar] + Lf());
				} else {
					sb.Append("[" + row[campoAgrupar] + "] " + row[campoMostrar] + Lf());
				}
			}


			//http://bassistance.de/jquery-plugins/jquery-plugin-treeview/
			sb.Append(
				@"<h4>Sample 1 - default</h4><ul id=""browser"" class=""filetree""><li><span class=""folder"">Folder 1</span><ul><li><span class=""file"">Item 1.1</span></li></ul></li><li><span class=""folder"">Folder 2</span><ul><li><span class=""folder"">Subfolder 2.1</span><ul id=""folder21""><li><span class=""file"">File 2.1.1</span></li><li><span class=""file"">File 2.1.2</span></li></ul></li><li><span class=""file"">File 2.2</span></li></ul></li><li class=""closed""><span class=""folder"">Folder 3 (closed at start)</span><ul><li><span class=""file"">File 3.1</span></li></ul></li><li><span class=""file"">File 4</span></li></ul>");
			sb.Append(@"<script>$(""#browser"").treeview();</script>");

			return sb.ToString();
		}

		private static string InvertParent(string parent)
		{
			string invParent = "";

			string[] parentList = parent.Split('#');

			for (int f = parentList.Length - 1; f >= 0; f--) {
				invParent = invParent + parentList[f] + "#";
			}

			return invParent.Remove(invParent.Length - 1);
		}

		private static bool HasParent(DataRow row, string campoPadre)
		{
			bool hasParent = row[campoPadre] != null && row[campoPadre].ToString() != "";

			return hasParent;
		}


		private static string Parent(DataTable listado, string campoPadre, string campoAgrupar, string idCampoPadre)
		{
			foreach (DataRow row in listado.Rows) {
				if (row[campoAgrupar].ToString() == idCampoPadre) {
					bool hasParent = HasParent(row, campoPadre);
					if (hasParent) {
						return idCampoPadre + "#" +
						Parent(listado, campoPadre, campoAgrupar, row[campoPadre].ToString());
					}
					return idCampoPadre;
				}
			}

			return "";
		}


		/// <summary>
		///     Devuelve el código html de un combo, con el elemento seleccionado 'valor'.
		///     Para la tabla 'tabla'. CampoId, se utiliza para indicar el nombre del combo.
		/// </summary>
		/// <param name="tabla"></param>
		/// <param name="campoMostrar"></param>
		/// <param name="valor"></param>
		/// <param name="campoComparar"></param>
		/// <param name="campoId"></param>
		/// <param name="whereCondition"></param>
		/// <returns></returns>
		public static string DameFrmCombo(int cid, string tabla, string campoMostrar, string valor, string campoComparar,
			string campoId, string whereCondition)
		{
			BdUtils db = new BdUtils(cid);
			string valorTabla = "";

			if (valor == "")
				valor = campoMostrar;

			string campos = campoMostrar + "," + valor;
			if (campoMostrar == valor)
				campos = campoMostrar;
			if (whereCondition != "")
				whereCondition = " where " + whereCondition;

			DataTable dtData =
				db.Execute("select " + campos + " from " + Variables.App.prefijoTablas + tabla + whereCondition + " order by " +
				campoMostrar);

			string id = campoId == "" ? tabla : campoId;

			string combo = @"<select size=""1"" name=""" + id + @"""" +
			               (Variables.Parser.frmComboOnChange != "" ? @" onChange=""" + Variables.Parser.frmComboOnChange + @"" : "") +
			               @""">";

			combo = combo + "<option>" + (Variables.Parser.frmMensajeCombo != "" ? Variables.Parser.frmMensajeCombo : "&nbsp;") +
			"</option>";

			foreach (DataRow row in dtData.Rows) {
				string valorMostrar = Functions.Valor(row[campoMostrar]);
				string valorDato = Functions.Valor(row[valor]);

				if (campoComparar != "") {
					string vt = Functions.Valor(Variables.Parser.data[Variables.Parser.frmDataPos].dataTable.Rows[0][campoComparar]);
					valorTabla = vt.Trim();
				}

				string sele = valorDato.Trim().ToLower() == valorTabla.Trim().ToLower() ? @" selected=""selected"" " : "";

				if (Variables.Parser.frmComboSelected != "") {
					sele = valorDato.Trim().ToLower() == Variables.Parser.frmComboSelected.ToLower() ? @" selected=""selected"" " : "";
				}

				combo = combo + @"<option value=""" + valorDato + @"""" + sele + ">" + valorMostrar + "</option>" +
				"\r\n";
			}

			combo = combo + "</select>";

			return combo;
		}

		/// <summary>
		///     Devuelve el código html de un combo, con el elemento seleccionado 'valor'.
		///     Para la tabla 'tabla'.
		/// </summary>
		/// <param name="tabla"></param>
		/// <param name="campoMostrar"></param>
		/// <param name="valor"></param>
		/// <param name="campoComparar"></param>
		/// <returns></returns>
		public static string DameFrmCombo(int cid, string tabla, string campoMostrar, string valor, string campoComparar)
		{
			return DameFrmCombo(cid, tabla, campoMostrar, valor, campoComparar, "", "");
		}

		/// <summary>
		///     Devuelve el título de la página indicada en 'cod'
		/// </summary>
		/// <param name="cod">Código de la página</param>
		/// <returns></returns>
		public static string TituloPagina(int cod)
		{
			StringBuilder sb = new StringBuilder("");
			DataTable dt = GetPage(cod);

			if (dt!= null && dt.Rows.Count > 0) {
				sb.Append(dt.Rows[0]["titulo"]);
			} else {
				sb.Append("Página no encontrada. Página: " + cod);
			}

			return sb.ToString();
		}

		/// <summary>
		///     Devuelve el contenido de la página indicada en 'cod'
		/// </summary>
		/// <param name="cod">Código de la página</param>
		/// <returns></returns>
		public static string MuestraPagina(int cod)
		{
			DataTable dt = GetPage(cod);
			if (dt == null || dt.Rows.Count == 0)
				throw new ExceptionUtil("Página no econtrada. Id Página: " + cod);
			return MuestraPagina(dt, true);
		}

		public static string MuestraPagina(int cod, bool edit)
		{
			DataTable dt = GetPage(cod);
			if (dt == null || dt.Rows.Count == 0)
				throw new ExceptionUtil("Página no econtrada. Id Página: " + cod);
			return MuestraPagina(dt, edit);
		}

		public static string MuestraPagina(string titulo)
		{
			DataTable dt = GetPage(titulo);
			if (dt == null || dt.Rows.Count == 0)
				throw new ExceptionUtil("Página no econtrada. Título página: " + titulo);
			return MuestraPagina(dt, true);
		}

		public static string MuestraPagina(string titulo, bool edit)
		{
			DataTable dt = GetPage(titulo);
			if (dt == null || dt.Rows.Count == 0)
				throw new ExceptionUtil("Página no econtrada. Título página: " + titulo);
			return MuestraPagina(dt, edit);
		}

		private static string MuestraPagina(DataTable dtPagina, bool edit)
		{
			StringBuilder sb = new StringBuilder("");

			if (Functions.ValorBool(dtPagina.Rows[0]["soloAdmin"])) {
				if (!Variables.User.Administrador) {
					return "";
				}
			}
			sb.Append(@"<div class=""pagina"" id=""P:" + dtPagina.Rows[0]["idPagina"] + @""">");
			if (edit) {
				sb.Append(EditPage("Paginas", "idPagina", dtPagina.Rows[0]["idPagina"].ToString(), "Editar Página",
					"Borrar Página"));
			}

			sb.Append(!Variables.App.modoLite && FuncionesWeb.IsMobileBrowser() && Functions.Valor(dtPagina.Rows[0]["contenidoMovil"]) != "" ? Functions.Valor(dtPagina.Rows[0]["contenidoMovil"]) : Functions.Valor(dtPagina.Rows[0]["contenido"]));
			sb.Append("</div>");

			return sb.ToString();
		}

		private static DataTable GetPage(int cod)
		{
			if (Variables.App.UseXML)
			{
				XML xml = new XML(Variables.App.directorioWeb + "data");
				xml.Load("paginas.xml");
				return xml.Select("idPagina=" + cod);
			}
			else
			{
				BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

				SelectQueryBuilder sqB = new SelectQueryBuilder();
				string[] columnas;
				if (Variables.App.modoLite)
				{
					columnas = new string[] { "contenido", "soloAdmin", "titulo", "idPagina" };
				}
				else
				{
					columnas = new string[] { "contenido", "contenidoMovil", "soloAdmin", "titulo", "idPagina" };
				}
				sqB.Columns.SelectColumns(columnas);
				sqB.TableSource = Variables.App.prefijoTablas + "Paginas";
				sqB.Where = new SimpleWhere(sqB.TableSource, "idPagina", Comparison.Equals, cod);

				return db.Execute(sqB.BuildQuery());
			}
		}

		private static DataTable GetPage(string titulo)
		{
			if (Variables.App.UseXML)
			{
				XML xml = new XML(Variables.App.directorioWeb + "data");
				xml.Load("paginas.xml");
				return xml.Select("titulo='" + titulo + "'");
			}
			else
			{
				BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

				SelectQueryBuilder sqB = new SelectQueryBuilder();
				sqB.Columns.SelectColumns("contenido", "contenidoMovil", "soloAdmin", "titulo", "idPagina");
				sqB.TableSource = Variables.App.prefijoTablas + "Paginas";
				sqB.Where = new SimpleWhere(sqB.TableSource, "titulo", Comparison.Like, titulo);

				return db.Execute(sqB.BuildQuery());
			}
		}

		public static string DatePicker()
		{
			return @"<script language=""javascript"">
                            $("".datepicker"").datepicker({
			                showOn: 'button',
			                buttonImage: '" + Variables.App.directorioPortal + @"imagenes/calendario.gif',
			                buttonImageOnly: true,
                            showButtonPanel: true,
                            changeMonth: true,
			                changeYear: true
		                    });
                            </script>";
		}

		public static string TabControl(Dictionary<string, string> tabContent)
		{
			StringBuilder s = new StringBuilder();

			s.Append(@"<div id=""tabs"" class=""ui-tabs"">");

			s.Append(@"<ul>");

			int f = 1;
			foreach (KeyValuePair<string, string> kv in tabContent) {
				s.Append(@"<li><a href=""#tabs-" + f + @""">" + kv.Key + "</a></li>");
				f++;
			}
			s.Append("</ul>");

			f = 1;
			foreach (KeyValuePair<string, string> kv in tabContent) {
				s.Append(@"<div id=""tabs-" + f + @""" class=""ui-tabs"">");
				s.Append(kv.Value);
				s.Append("</div>"); // fin tabs-f
				f++;
			}

			s.Append("</div>"); //fin tabs

			s.Append(@"<script>$(function(){$(""#tabs"").tabs();});</script>");

			return s.ToString();
		}

		public static string FormToHtml(HttpRequest frm)
		{
			StringBuilder sBody = new StringBuilder();

			for (int f = 0; f <= frm.Form.Count - 1; f++)
			{
				if (frm.Form.Keys[f].Length > 3)
					if (frm.Form.Keys[f].Substring(0, 3).ToLower() != "cmd") //ignoramos los botones
					{
						sBody.Append("[" + frm.Form.Keys[f] + "]: " + frm.Form.Get(f).Trim() + Ui.Lf());
					}
			}

			return sBody.ToString();
		}
	}
}