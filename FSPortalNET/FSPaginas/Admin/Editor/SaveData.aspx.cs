// <fileheader>
// <copyright file="savedata.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: admin\editor\savedata.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using System.Text;
using FSPortal;
using FSLibrary;
using FSDatabase;
using FSNetwork;
using FSException;
using FSMail;

namespace FSPaginas.Admin.Editor
{
	public class SaveData : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			contenido = Inicio();
		}

		private string Inicio()
		{
			string link = "";
			StringBuilder s = new StringBuilder("");
			
			string sTableName = Web.Request("tablename");
			sTableName = System.Web.HttpUtility.HtmlDecode(sTableName);

			int iPlace = TextUtil.IndexOf(sTableName, " where ");
			if (iPlace > 0) {
				sTableName = TextUtil.Substring(sTableName, 0, iPlace);
			}

			string sFieldType = Web.Request("fldtype");
			string sFieldName = Web.Request("fld");
			string sFieldValue = Web.Request("val");	
			bool bAdd = Functions.ValorBool(Web.Request("add"));
            bool bError = false;

            DataTable dtTableColumns;
            DataRow dtTableRow = null;
            string sWhere = "";
            Register rsTable = null;

            if (Variables.App.UseXML)
            {
                XML xml = new XML(Variables.App.directorioWeb + "data");
                xml.Load("" + sTableName + ".xml");
                dtTableColumns = xml.GetSchema();
                if (bAdd)
                {
                    dtTableRow = xml.DataTable.NewRow();
                }
                else
                {
                    if (NumberUtils.IsNumeric(sFieldValue))
                        sWhere = sFieldName + "=" + sFieldValue;
                    else
                        sWhere = sFieldName + "='" + sFieldValue + "'";

                    dtTableRow = xml.SelectRow(sWhere);
                }       
            }
            else
            {
                BdUtils db = new BdUtils(Web.RequestInt("cid"));
                rsTable = new Register();
                dtTableColumns = db.GetSchemaTable(sTableName);
                sWhere = Utils.DameWhere(sFieldName, sFieldType, sFieldValue);
            }

            if (Web.Request("cmdSave") != "" && Variables.User.bRecEdit) {
				foreach (DataRow fld in dtTableColumns.Rows) {
					string sVal = FuncionesWeb.FormatDB(Request.Form[fld["ColumnName"].ToString()]);


					if (!Functions.ValorBool(fld["IsAutoIncrement"])) {
						switch (fld["DataType"].ToString().ToLower()) {
							case "system.boolean":
							case "system.sbyte":
                                if (Variables.App.UseXML)
                                {
                                    if (Functions.Valor(sVal).Length > 0)
                                    {
                                        dtTableRow[fld["ColumnName"].ToString()] = true;
                                    }
                                    else
                                    {
                                        dtTableRow[fld["ColumnName"].ToString()] = false;
                                    }
                                }
                                else
                                {
                                    if (Functions.Valor(sVal).Length > 0)
                                    {
                                        rsTable.Add(new Field(fld["ColumnName"].ToString(), "True", typeof(Boolean)));
                                    }
                                    else
                                    {
                                        rsTable.Add(new Field(fld["ColumnName"].ToString(), "False",
                                            typeof(Boolean)));
                                    }
                                }
								break;
							case "system.byte[]":
								break;
							case "system.datetime":
                                if (Variables.App.UseXML)
                                {
                                    if (FSLibrary.DateTimeUtil.IsDate(Request.Form[fld["ColumnName"].ToString()]))
                                    {
                                        dtTableRow[fld["ColumnName"].ToString()] = sVal;
                                    }
                                }
                                else
                                {
                                    if (FSLibrary.DateTimeUtil.IsDate(Request.Form[fld["ColumnName"].ToString()]))
                                    {
                                        rsTable.Add(new Field(fld["ColumnName"].ToString(), sVal, typeof(System.DateTime)));
                                    }
                                }
								break;
							case "system.decimal":
							case "system.int16":
							case "system.int32":
							case "system,int64":
							case "system.integer":
							case "system.double":
                                if (Variables.App.UseXML)
                                {
                                    if (NumberUtils.IsNumeric(Request.Form[fld["ColumnName"].ToString()]))
                                    {
                                        sVal = TextUtil.Replace(sVal, ",", ".");
                                        dtTableRow[fld["ColumnName"].ToString()] = sVal;
                                    }
                                    else
                                    {
                                        dtTableRow[fld["ColumnName"].ToString()] = "0";
                                    }
                                }
                                else
                                {
                                    if (NumberUtils.IsNumeric(Request.Form[fld["ColumnName"].ToString()]))
                                    {
                                        sVal = TextUtil.Replace(sVal, ",", ".");
                                        rsTable.Add(new Field(fld["ColumnName"].ToString(), sVal, typeof(Int32)));
                                    }
                                    else
                                    {
                                        rsTable.Add(new Field(fld["ColumnName"].ToString(), "0", typeof(Int32)));
                                    }
                                }
								break;
							default:
                                //aplicamos el cambio si la longitud de directorioPortal o directorioWeb es superior a 1, es decir, no cuando sea "/"
                                if(Variables.App.directorioPortal.Length > 1)
                                    sVal = TextUtil.Replace(sVal, Variables.App.directorioPortal, "{directorioportal}");
                                if (Variables.App.directorioWeb.Length > 1)
                                    sVal = TextUtil.Replace(sVal, Variables.App.directorioWeb, "{directorioweb}");

                                if (Variables.App.UseXML)
                                {
                                    dtTableRow[fld["ColumnName"].ToString()] = sVal;
                                }
                                else
                                {
                                    rsTable.Add(new Field(fld["ColumnName"].ToString(), sVal, typeof(String)));
                                }
                                break;
						}


						if (Functions.ValorBool(fld["IsKey"])) {
							sFieldName = fld["ColumnName"].ToString();
							sFieldValue = Functions.Valor(Request.Form[fld["ColumnName"].ToString()]);
						}
					} else {
						if (bAdd) {
							sFieldName = fld["ColumnName"].ToString();
						}
					}
				}

				string accion = "";
				try {
					int bloq = 0;
					if (Request.Form.Count != 0) {
						if (bAdd) {
							accion = "añadido";

                            if (Variables.App.UseXML)
                            {
                                XML xml = new XML(Variables.App.directorioWeb + "data");
                                xml.Insert(dtTableRow);

                                if(xml.Save())
                                    bloq = 1;

                                sFieldValue = dtTableRow[sFieldName].ToString();
                            }
                            else
                            {
                                BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
                                bloq = db.ExecuteNonQuery(db.InsertSql(sTableName, rsTable, Variables.User.UsuarioId));

                                int value = db.GetMaxValue(sTableName, sFieldName);

                                if(value != -1)
                                    sFieldValue = value.ToString();
                                else
                                    sFieldValue = dtTableRow[sFieldName].ToString();

                                //sFieldValue = db.GetScopeIdentity();
                                //sFieldValue = db.GetIdentity();

                            }

							link = "showrecord.aspx?tablename=" + sTableName + "&fld=" + sFieldName + "&val=" +
							sFieldValue + "&fldtype=System.Integer&page=1";
						} else {
							accion = "guardado";
                            if (Variables.App.UseXML)
                            {
                                XML xml = new XML(Variables.App.directorioWeb + "data");
                                xml.Load(sTableName);
                                if (xml.Save())
                                    bloq = 1;
                            }
                            else
                            {
                                BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
                                bloq = db.ExecuteNonQuery(db.UpdateSql(sTableName, rsTable, sWhere));
                            }
						}
					}
					if (bloq == 0) {
						s.Append("Error durante la actualización. 0 registros actualizados.");
						bError = true;
					}
				} catch (System.Exception e) {
					s.Append("Error durante la actualización.");
					s.Append("Descripción: " + e.Message);
					bError = true;

					ExceptionUtil.WriteError("Error durante la actualización.", e);
				}

				if (!bError) {
					s.Append("Registro " + accion);

					//if (accion == "añadido")
					//{
					//    s.Append(" - <a href='showrecord.aspx?q=&amp;tablename=" + sTableName + "&amp;fld=" + sFieldName + "&amp;val=" + ident + "&amp;fldtype=System.Integer&amp;page=1'>[Editar Último registro añadido (" + ident + ")]</a>");
					//}

					string sTo = Variables.App.correoInfo;
					const string sSubject = "Modificación/Creación de Registro";
					string sBody = "La tabla: '" + sTableName + "', ";
					sBody += "ha sido modificada por: " + Variables.User.NombreCompleto + Ui.Lf() + Ui.Lf();
					sBody += "Puedes acceder al registro en:" + Ui.Lf() + Ui.Lf();
					sBody += Variables.App.webHttp + "/admin/editor/showrecord.aspx?tablename=" + sTableName + "&fld=" +
					sFieldName + "&val=" + sFieldValue + "&fldtype=System.Integer&page=1" + Ui.Lf() +
					Ui.Lf() + Ui.Lf();

                    sBody += Ui.Lf() + "------- FORM --------" + Ui.Lf();
                    sBody += "Formulario enviado por internet: " + Variables.User.NombreCompleto + Ui.Lf() + Ui.Lf(); 
                    sBody += Ui.FormToHtml(Request);

					try {
						new SendMail().SendMailAsync(sTo, Variables.App.correoPrueba, Variables.App.correoCopia, sSubject, sBody, Variables.App.correoInfo, Variables.App.nombreWeb, Variables.App.plantillaCorreo);
					} catch (System.Exception e) {
						s.Append(" - Error al enviar correo: " + e.Message);
					}
				}
			} else {
				s.Append(
					"No dispone de permisos suficientes para modificar el registro, o ha sido desconectado del servidor. Intentelo más tarde.");
				link = "/";
			}

			return @"{ ""message"": """ + Web.formatJSON(s.ToString()) + @""",""link"": """ + link + @""" }";
		}
	}
}