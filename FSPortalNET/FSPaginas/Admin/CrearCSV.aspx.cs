// <fileheader>
// <copyright file="crearScript.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: admin\crearScript.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using System.Text;
using FSPortal;
using FSLibrary;
using FSQueryBuilder;
using FSDatabase;
using FSException;

namespace FSPaginas.Admin
{
	public class CrearCSV : BasePage
	{
		protected void Page_Load(Object sender, EventArgs e)
		{
			contenido = Inicio();
		}

		private string Inicio()
		{
            if (Utils.BDType == Utils.TypeBd.Oledb) {
				 return Generar();
			}
			return "Este proceso solo funciona con una conexión OLEDB (Access).";
		}


		public string Generar()
		{
			StringBuilder s = new StringBuilder("");

			return GenerateData();
		}
		
		
		private string WriteData(StringBuilder s, string fileName)
		{
			try {
				System.IO.File.WriteAllText(Server.MapPath(Variables.App.uploadPath) + @"\" + fileName + ".csv", s.ToString(),
					Encoding.UTF8);
				return "Fichero creado correctamente. " + Ui.Lf() +
				Ui.Link("Pulsa aqui para descargarlo.",
					Server.MapPath(Variables.App.uploadPath) + @"\" + fileName + ".csv");
			} catch (System.Exception e) {
				throw new ExceptionUtil("Problemas en la creación del fichero. " + Ui.Lf() + e);
			}
		}
		
		private string GenerateData()
		{
			string outmsg = "";
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

			DataTable tables = db.GetSchemaTables();


			foreach (DataRow r in tables.Rows) {
				if (TextUtil.Substring(r["TABLE_NAME"].ToString(), 0, 4) != "MSys" &
				    TextUtil.Substring(r["TABLE_NAME"].ToString(), 0, 1) != "_" &
				    TextUtil.Substring(r["TABLE_NAME"].ToString(), 0, 5) != "Stats" &
				    TextUtil.Substring(r["TABLE_NAME"].ToString(), 0, 5) != "Error"
				   ) {
					
					StringBuilder s = new StringBuilder("");
					StringBuilder line = new StringBuilder("");

					DataTable col = db.GetSchemaTable(r["TABLE_NAME"].ToString());

					outmsg += Ui.Lf() + "Tabla:" + Variables.App.prefijoTablas + r["TABLE_NAME"] + Ui.Lf();
					
					string campos = "";

					foreach (DataRow fld in col.Rows) {
						campos += "'" + fld["ColumnName"].ToString() + "',";
					}


					if (campos != "") {
						campos = TextUtil.Substring(campos, 0, TextUtil.Length(campos) - 1);
					}
					s.AppendLine(campos);

					
					SelectQueryBuilder sqB = new SelectQueryBuilder();
					sqB.Columns.SelectColumns("*");
					sqB.TableSource = Variables.App.prefijoTablas + r["TABLE_NAME"].ToString();
					sqB.Where = null;

					DataTable dt = db.Execute(sqB.BuildQuery());
					StringBuilder ins = new StringBuilder("");


					foreach (DataRow row in dt.Rows) {
														
						campos = "";
						foreach (DataRow fld in col.Rows) {


							switch (fld["DataType"].ToString().ToLower()) {
								case "system.string":
									campos = campos + "'" +
									formatSQL(row[fld["ColumnName"].ToString()].ToString()) + "',";
									break;
								case "system.datetime":
									campos = campos + "'" + FSLibrary.DateTimeUtil.ShortDate(FSLibrary.DateTimeUtil.ValorFecha(row[fld["ColumnName"].ToString()].ToString(), new System.DateTime(2000,1,1))) + "',";
									break;
								case "system.int16":
								case "system.int32":
								case "system.integer":
								case "system.byte":
								case "system.int64":
								case "system.single":
								case "system.double":
								case "system.decimal":
									string num =
										NumberUtils.NumberDouble(row[fld["ColumnName"].ToString()].ToString()).ToString();
									campos = campos + TextUtil.Replace(num, ",", ".") + ",";
									break;
								case "system.boolean":
								case "system.sbyte":
									campos = campos + row[fld["ColumnName"].ToString()] + ",";
									break;
							}
						}
						if (campos != "") {
							campos = TextUtil.Substring(campos, 0, TextUtil.Length(campos) - 1);
						}
						ins.AppendLine(campos);
					}

					s.AppendLine(ins.ToString());
	
					outmsg += WriteData(s,r["TABLE_NAME"].ToString());
				}
				
			}
			
			return outmsg;
		}


		private string formatSQL(string cad)
		{
			return TextUtil.Replace(cad, "'", "&#39;");
		}

	}
}