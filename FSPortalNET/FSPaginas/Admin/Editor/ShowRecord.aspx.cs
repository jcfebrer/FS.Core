// <fileheader>
// <copyright file="showrecord.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: admin\editor\showrecord.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using FSPortal;
using FSLibrary;
using FSDatabase;
using FSNetwork;
using FSException;

namespace FSPaginas.Admin.Editor
{
	public class ShowRecord : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            int cid = 0;
            if(Web.Request("cid")== "")
            {
                cid = Utils.GetConnectionId(Variables.App.defaultEntry);
            }
            else
            {
                cid = Web.RequestInt("cid");
            }
			contenido = Inicio(cid, Web.Request("tablename"),
				Web.Request("fld"),
				Web.Request("val"),
				Web.Request("q"),
				Web.RequestBool("add"),
				Web.Request("autoSel"),
				Web.Request("autoSelField"),
				Web.RequestInt("page"));
		}

		public string Inicio(int cid, string tableName, string fieldName, string fieldValue, string query, bool add,
			string autoSel, string autoSelField, int page)
		{
			if (fieldValue == "" && !add)
				throw new ExceptionUtil("Parametró VAL incorrecto.");

			StringBuilder header = new StringBuilder("");
			StringBuilder footer = new StringBuilder("");
			StringBuilder tabGeneral = new StringBuilder("");
			Dictionary<string, string> tabEditor = new Dictionary<string, string>();
			Dictionary<string, string> tabContent = new Dictionary<string, string>();
			
			string sFieldName = fieldName;
            string sFieldType = "";
            string sFieldValue = fieldValue;
            string sWhere = "";

            int iPage = page;
            bool bAdd = add;

            string sQuery = query;
			bool bQuery = sQuery.Length > 0;

			string sTableName = tableName;
			sTableName = HttpUtility.HtmlDecode(sTableName);

			int iPlace = TextUtil.IndexOf(sTableName, " where ");
			if (iPlace > 0) {
				sTableName = TextUtil.Substring(sTableName, 0, iPlace);
			}


            DataTable dtTableColumns;
            DataTable dtTable;
            DataRow rsT = null;

            if (Variables.App.UseXML)
            {
				XML xml = new XML(Variables.App.directorioWeb + "data");
				xml.Load("" + sTableName + ".xml");
                if (!bAdd)
                {
                    if (NumberUtils.IsNumeric(sFieldValue))
                        sWhere = sFieldName + "=" + sFieldValue;
                    else
                        sWhere = sFieldName + "='" + sFieldValue + "'";
                }
                dtTable = xml.Select(sWhere);
                dtTableColumns = xml.GetSchema();

                if(dtTable != null && dtTable.Rows.Count > 0)
                    rsT = dtTable.Rows[0];
            }
            else
            {
                BdUtils db = new BdUtils(cid);

                if (sFieldName != "")
                    sFieldType = db.GetField(sFieldName, sTableName).Tipo.ToString();
                //Functions.ValorRequest("fldtype");

                if (sFieldType == null & !bAdd)
                {
                    Response.Redirect(Variables.App.directorioPortal + "default.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }

                sWhere = (bAdd ? "" : Utils.DameWhere(sFieldName, sFieldType, sFieldValue));


                string sSql = "SELECT * FROM " + sTableName;
                if (sWhere != "")
                    sSql += " WHERE " + sWhere;

                dtTable = db.Execute(sSql);

                if (!Utils.IsEmpty(dtTable))
                {
                    rsT = dtTable.Rows[0];
                }

                dtTableColumns = db.GetSchemaTable(sTableName);
            }


            if (Variables.User.bRecEdit) {
				header.Append("\r\n" + @"<form action=""" + Variables.App.directorioPortal +
				"admin/editor/savedata.aspx?cid=" + cid + "&tablename=" + sTableName + "&amp;where=" + sWhere +
				"&amp;fld=" + sFieldName + "&amp;val=" + sFieldValue + "&amp;fldtype=" +
				sFieldType + "&amp;page=" + iPage + "&amp;add=" + bAdd + "&amp;cmdsave=1" +
				(bQuery ? "&amp;q=1" : "") +
				@""" method='post' id='showRecordForm' class='frmForm'>");
			}

			if (!simple) {
				header.Append("\r\n" +
				@"<table border=""0"" cellspacing=""1"" cellpadding=""2"" width=""100%"">");
				header.Append("\r\n" + "<tr>");
				header.Append("\r\n" + "<td>");
				if (Variables.User.Administrador) {
					header.Append(@"<img alt="""" border=""0"" src=""");
					header.Append(Variables.App.directorioPortal + @"imagenes/bullet.gif""/> <a href=""" +
					Variables.App.directorioPortal + "admin/editor/listtables.aspx?cid=" + cid);
					header.Append(@""">Portal</a> <img alt="""" border=""0"" src=""");
					header.Append(Variables.App.directorioPortal + @"imagenes/bullet.gif""/> <a href=""" +
					Variables.App.directorioPortal + "admin/editor/showtable.aspx?");
					header.Append("cid=" + cid + "&tablename=" + Server.UrlEncode(sTableName) + "&amp;page=");
					header.Append(page + ((bQuery ? "&amp;q=1" : "")) + @""">Tabla [" + sTableName +
					@"]</a> <img alt="""" border=""0"" src=""");
					header.Append(Variables.App.directorioPortal + @"imagenes/bullet.gif""/> ");
					header.Append((add ? "Añadir" : "Editar") + " Registro");
				}
                
                
                
				header.Append("\r\n" + @"</td><td align=""right"">");
                
                
				header.Append("\r\n" + Ui.Button("Atrás", "javascript:history.back();"));

				header.Append("\r\n" +
				Ui.Button("Volver",
					"javascript:window.location='" +
					HttpContext.Current.Request.ServerVariables["HTTP_REFERER"] + "'"));

				header.Append("\r\n" + Ui.Button("Actualizar", "javascript:location.reload();"));
                
				if (tableName.ToLower() == "paginas") {
					header.Append("\r\n");
					header.Append(Ui.Button("Ver", "javascript:window.location='" +
					Variables.App.directorioPortal + "pagina.aspx?id=" + sFieldValue + "'"));
				}
				
                
				header.Append("<br />");
                
				if (Variables.User.bRecEdit) {
					header.Append("\r\n" + Ui.Button("Guardar", "", "cmdSave")); 
				}

				if (Variables.User.bRecAdd) {
					header.Append("\r\n" +
					Ui.Button("Añadir",
						"javascript:window.location='" + Variables.App.directorioPortal +
						"admin/editor/showrecord.aspx?cid=" + cid + "&tablename=" + Server.UrlEncode(sTableName) +
						@"&add=1&page=1'"));
				}
				//header.Append("\r\n" + @"<a href=""" + Variables.App.directorioPortal + "admin/editor/searchtable.aspx?tablename=" + Server.UrlEncode(sTableName) + @""">Búscar</a>");
				string p = "cid=" + cid + "&q=&amp;tablename=" + Server.UrlEncode(sTableName) + "&amp;fld=" + sFieldName +
				           "&amp;val=" + sFieldValue + @"&amp;page=1";
				header.Append("\r\n" +
				Ui.Button("Borrar",
					"javascript:window.location='" + Variables.App.directorioPortal +
					"admin/editor/deleterecord.aspx?" + p + "'"));

				header.Append("\r\n" + "</td>");
				header.Append("\r\n" + "</tr>");
				header.Append("\r\n" + "</table>");

				header.Append(Ui.Lf());
			}

			header.Append("\r\n" + "<fieldset>"); //<legend>Tabla: " + sTableName + "</legend>");

			tabGeneral.Append("<table><tbody>");

			string sValue = "";
			
			foreach (DataRow fld in dtTableColumns.Rows) {
				if (!bAdd) {
					sValue = Functions.Valor(rsT[fld["ColumnName"].ToString()]);
				}

				if (sValue != "") {
					sValue = Server.HtmlEncode(sValue);
					sValue = TextUtil.Replace(sValue, @"""", "&amp;quot;");
				}

				string sEditable = Variables.User.bRecEdit ? "" : "disabled";
                string sDescription = fld["ColumnName"].ToString();
                bool hasRelation = false;
                string relationTable = "";

                if(!Variables.App.UseXML)
                {
                    BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
                    sDescription = db.GetDescription(sTableName, fld["ColumnName"].ToString());

                    if (db.IsControlField(fld["ColumnName"].ToString()))
                    {
                        sEditable = "disabled";
                    }

                    hasRelation = db.HasRelation(sTableName, fld["ColumnName"].ToString());
                    relationTable = db.RelationTable(sTableName, fld["ColumnName"].ToString());
                }

				tabGeneral.Append("<tr>");

				switch (fld["DataType"].ToString().ToLower()) {
					case "system.int16":
					case "system.int32":
                    case "system.decimal":
					case "system,int64":
					case "system.integer":
					case "system.double":
						tabGeneral.Append("\r\n" + @"<td><label for=""" + fld["ColumnName"] + @""">" +
						sDescription + "</label></td>");

						if (autoSel != "" && autoSelField.ToLower() == Functions.Valor(fld["ColumnName"]).ToLower()) {
							sValue = autoSel;
						}

						if (hasRelation) {
							tabGeneral.Append("<td>" +
							Ui.DameFrmCombo(cid, sTableName, fld["ColumnName"].ToString(), sValue));
							tabGeneral.Append("\r\n" + @"<a href=""" + Variables.App.directorioPortal +
							"admin/editor/showtable.aspx?cid=" + cid + "&tablename=" +
							relationTable +
							@"""><img border=""0"" src=""" + Variables.App.directorioPortal +
							@"imagenes/edit.gif"" alt=""Editar desplegable"" /></a></td>");
						} else {
							bool isAutoIncrement = Functions.ValorBool(fld["IsAutoIncrement"]);


							if (isAutoIncrement) {
								tabGeneral.Append("\r\n" + "<td>Auto numérico");
								if (sValue != "") {
									tabGeneral.Append("\r\n" + " (" + sValue + ")");
								}
								tabGeneral.Append("</td>");
							} else {
								tabGeneral.Append("\r\n" + @"<td><input size=""60"" " + sEditable +
								@" type=""text"" name=""" + fld["ColumnName"] + @""" value=""" +
								sValue + @"""/></td>");
								if (!(Functions.ValorBool(fld["AllowDBNull"]))) {
									tabGeneral.Append("\r\n" + "<td><span class='textomaspeque'>*</span></td>");
								}
							}
						}
						break;
					case "system.byte[]":
						tabGeneral.Append("\r\n" + @"<td><label>" +
						sDescription +
						"</label></td><td>Objeto OLE</td>");
						break;
					case "system.boolean":
					case "system.sbyte":
						tabGeneral.Append("\r\n" + @"<td><label for=""" + fld["ColumnName"] + @""">" +
                        sDescription + "</label></td>");
						tabGeneral.Append("\r\n" + "<td><input " + sEditable + @" type=""checkbox"" name=""" +
						fld["ColumnName"] + @"""");

						if (!bAdd) {
							if (sValue.ToLower() == "true" || sValue == "-1") {
								tabGeneral.Append(@" checked=""checked"" /></td>");
							} else {
								tabGeneral.Append(" /></td>");
							}
						}
						break;
					case "system.datetime":
						string datePicker = @"class=""datepicker""";
						if (sEditable != "")
							datePicker = "";
						tabGeneral.Append("\r\n" + @"<td><label for=""" + fld["ColumnName"] + @""">" +
                        sDescription + "</label></td>");
						tabGeneral.Append("\r\n" + @"<td><input " + datePicker + @" size=""25"" " + sEditable +
						@" type=""text"" name=""" + fld["ColumnName"] + @""" value=""" +
						sValue + @""" maxlength=""20""/></td>");
						break;
					case "system.string":

						if (int.Parse(fld["ColumnSize"].ToString()) >= 65535 || int.Parse(fld["ColumnSize"].ToString()) == 0) {
							string editorCode = "";
                            
                           
							if (sTableName.ToLower() == "plantillas" || textEditor == true) {
								editorCode = Ui.HtmlEditor(fld["ColumnName"].ToString(), sValue, 100, false);
							} else {
								editorCode = Ui.HtmlEditor(fld["ColumnName"].ToString(), sValue);
							}

							tabEditor.Add(sDescription, editorCode);
						} else {
							tabGeneral.Append("\r\n" + @"<td><label for=""" + fld["ColumnName"] + @""">" +
							sDescription + "</label></td>");

							string defaultValue = "";

							if ((sValue == "" & autoSel != "") &
							    (Functions.Valor(fld["ColumnName"]).ToLower() == autoSelField.ToLower())) {
								defaultValue = autoSel;
							}
							int fldSize = NumberUtils.NumberInt(fld["ColumnSize"]);
							if (hasRelation) {
								tabGeneral.Append("<td>" +
								Ui.DameFrmCombo(cid, sTableName, fld["ColumnName"].ToString(),
									sValue));
								tabGeneral.Append("\r\n" + @"<a href=""" + Variables.App.directorioPortal +
								"admin/editor/showtable.aspx?cid=" + cid + "&tablename=" +
								relationTable +
								@"""><img border=""0"" src=""" + Variables.App.directorioPortal +
								@"imagenes/edit.gif"" alt=""Editar desplegable"" /></a></td>");
							} else {
								tabGeneral.Append("\r\n" + @"<td><input size=""60"" " + sEditable +
								@" type=""text"" name=""" + fld["ColumnName"] + @""" value=""" +
								sValue + defaultValue + @""" maxlength=""" + fldSize +
								@"""/></td>");
								
								if (fld["ColumnName"].ToString().ToLower() == "enlace" && sTableName == "Paginas") {
									tabGeneral.Append("\r\n" + "<td>" + Ui.Button("Ver", "javascript:window.location='" +
									Variables.App.directorioPortal + sValue + "'") + "</td>");
								}
							}
						}
						break;
				}
				tabGeneral.Append("</tr>");
			}

			tabGeneral.Append("\r\n" + "</tbody></table>");

			if (Variables.User.bRecEdit) {
				footer.Append("\r\n" + "</fieldset>");
				footer.Append("\r\n" + "</form>");
			}

			tabContent.Add("General", tabGeneral.ToString());
			foreach (KeyValuePair<string, string> kv in tabEditor) {
				tabContent.Add(kv.Key, kv.Value);
			}

            if(cid == Utils.GetConnectionId(Variables.App.defaultEntry))
			    tabContent.Add("Relación", new ShowTable().Inicio(cid, "Paginas", false, false));

			//creamos el control TABCONTROL:
			StringBuilder s = new StringBuilder("");

			s.Append(header);

			s.Append(Ui.TabControl(tabContent));

			s.Append(Ui.DatePicker());

			s.Append(footer);
			string result = s.ToString();

			result = TextUtil.Replace(result, "{directorioportal}", Variables.App.directorioPortal);
			result = TextUtil.Replace(result, "{directorioweb}", Variables.App.directorioWeb);

			return Parser.SaveScriptCodes(result); // evitamos que se formateen los datos.
		}
	}
}