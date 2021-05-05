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
using FSNetwork;

namespace FSPaginas.Admin
{
	public class CrearSQL : BasePage
	{
		protected void Page_Load(Object sender, EventArgs e)
		{
			contenido = Inicio();
		}

		private string Inicio()
		{
			//if (db.BDType == BDType.Oledb) {
			return Generar();
			//}
			//return "Este proceso solo funciona con una conexión OLEDB (Access).";
		}


		public string Generar()
		{
			StringBuilder s = new StringBuilder("");
			string err = "";
			string formato = Web.Request("formato");
            Utils.TypeBd b;
			//string df = "ddmmyyyy";
			//string sep = "/";

			switch (formato.ToLower()) {
				case "sqlite":
					b = Utils.TypeBd.SQLite;
					//df = "yyyymmdd";
					//sep = "-";

					s.Append(GenerateData(b, "\r", ""));
					err = WriteData(s, formato);
					break;
				case "mysql":
					b = Utils.TypeBd.MySQL;
					//df = "yyyymmdd";
					//sep = "-";

					s.Append(GenerateData(b, "\r", ""));
					err = WriteData(s, formato);
					break;
				case "sqlserver":
					b = Utils.TypeBd.SQLServer;
					
					s.Append(GenerateData(b, "\r\n", "GO"));
					err = WriteData(s, formato);
					break;
				case "oracle":
					b = Utils.TypeBd.Oracle;
					
					s.Append(GenerateData(b, "\r\n", ""));
					err = WriteData(s, formato);
					break;
				default:
					err = Help();
					break;
			}

			return err;
		}
		
		
		private string Help()
		{
			return "Parámetro no indicado en la generación del fichero." + Ui.Lf() +
			"Si deseas generarlo como SQL Server, pulsa <a href='?formato=sqlserver'>aquí</a>, " +
			Ui.Lf() + "para generarlo en formato MySQL, pulsa <a href='?formato=mysql'>aquí</a>," +
			Ui.Lf() + "para generarlo en formato Oracle, pulsa <a href='?formato=oracle'>aquí</a>," +
			Ui.Lf() + "para generarlo en formato SQLite, pulsa <a href='?formato=sqlite'>aquí</a>.";
		}
		
		
		private string WriteData(StringBuilder s, string formato)
		{
			string err = "";

			try {
				System.IO.File.WriteAllText(Server.MapPath(Variables.App.uploadPath) + @"\" + formato + ".sql", s.ToString(),
					Encoding.UTF8);
				err = "Fichero creado correctamente. " + Ui.Lf() +
				Ui.Link("Pulsa aqui para descargarlo.",
					Server.MapPath(Variables.App.uploadPath) + @"\" + formato + ".sql");
			} catch (System.Exception e) {
				err = "Problemas en la creación del fichero. " + Ui.Lf() + e;
			}

			return err;
		}
		
		private StringBuilder GenerateData(Utils.TypeBd b, string salto, string goComm)
		{
			StringBuilder s = new StringBuilder("");
			StringBuilder commentSql = new StringBuilder("");
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);


			DataTable tables = db.GetSchemaTables();

			
			if (b != Utils.TypeBd.SQLite & b != Utils.TypeBd.Oracle) {
				s.Append("DROP DATABASE PORTALNET;" + salto + goComm + salto + salto);
				s.Append("CREATE DATABASE PORTALNET;" + salto + goComm + salto + salto);
				s.Append("USE PORTALNET;" + salto + goComm + salto + salto);
			}
			
			if (b == Utils.TypeBd.Oracle) {
				s.Append("SET DEFINE OFF;" + salto + goComm + salto + salto);
			}


			foreach (DataRow r in tables.Rows) {
				if (TextUtil.Substring(r["TABLE_NAME"].ToString(), 0, 4) != "MSys" &
				    TextUtil.Substring(r["TABLE_NAME"].ToString(), 0, 1) != "_") {
					StringBuilder insComm = new StringBuilder("");

					DataTable col = db.GetSchemaTable(r["TABLE_NAME"].ToString());

					insComm.Append("CREATE TABLE [" + Variables.App.prefijoTablas + r["TABLE_NAME"] + "] (");
					string campos = "";
					bool hayAutoNumeric = false;
					string tipo;
					
					foreach (DataRow fld in col.Rows) {
						int tamano = int.Parse(fld["ColumnSize"].ToString());
						
						if (tamano > 4000)
							tamano = 4000;
						
						string comentario = db.GetDescription(r["TABLE_NAME"].ToString(), fld["ColumnName"].ToString());
						tipo = DameTipo(fld["DataType"].ToString().ToLower(),
							tamano, b);
						campos = campos +
						PrintField(fld["ColumnName"].ToString(), tamano,
							Functions.ValorBool(fld["IsAutoIncrement"]), ",", tipo, b, comentario, salto);

						if (Functions.ValorBool(fld["IsAutoIncrement"])) {
							hayAutoNumeric = true;
						}

						if (b == Utils.TypeBd.SQLServer) {
							if (comentario != fld["ColumnName"].ToString()) {
								commentSql.Append(
									"EXEC sys.sp_addextendedproperty @name=N'MS_Description',@value=N'" + comentario +
									"',@level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'" +
									r["TABLE_NAME"] + "',@level2type=N'COLUMN',@level2name=N'" + fld["ColumnName"] +
									"'" + salto);
							}
						}
					}


					if (campos != "") {
						campos = TextUtil.Substring(campos, 0, TextUtil.Length(campos) - 1);
					}
					insComm.Append(campos);

					DataTable pk = db.GetPrimaryKeys();

					string keys = "";

					foreach (DataRow p in pk.Rows) {
						if (Functions.Valor(p["PRIMARY_KEY"]).ToLower() == "true" &
						    Functions.Valor(r["TABLE_NAME"]).ToLower() == Functions.Valor(p["TABLE_NAME"]).ToLower()) {
							keys = keys + "[" + p["COLUMN_NAME"] + "],";
						}
					}
					if (keys != "") {
						keys = TextUtil.Substring(keys, 0, TextUtil.Length(keys) - 1);
					}

					if (keys != "") {
						insComm.Append("," + salto + "     Primary Key(" + keys + ")");
					}
					insComm.AppendLine(");");
					insComm.AppendLine("");
					
					//comentarios para ORACLE
					if(b == Utils.TypeBd.Oracle)
					{
						foreach (DataRow fld in col.Rows) {
							string comentario = db.GetDescription(r["TABLE_NAME"].ToString(), fld["ColumnName"].ToString());
							if(comentario != fld["ColumnName"].ToString())
								insComm.AppendLine("COMMENT ON COLUMN " + r["TABLE_NAME"].ToString() + "." + fld["ColumnName"].ToString() + " IS '" + comentario + "';");
						}
					}
					
					//modo utf8 para mysql
					if(b == Utils.TypeBd.MySQL)
					{
						insComm.AppendLine("ALTER TABLE " + r["TABLE_NAME"].ToString() + " CONVERT TO CHARACTER SET utf8;");
					}

					s.Append(Utils.FormatSQL(insComm.ToString(), b));
					s.Append(salto + salto);

					if (TextUtil.Substring(r["TABLE_NAME"].ToString(), 0, 5) != "Stats" &
					    TextUtil.Substring(r["TABLE_NAME"].ToString(), 0, 7) != "Errores") {
						SelectQueryBuilder sqB = new SelectQueryBuilder();
						sqB.Columns.SelectColumns("*");
						sqB.TableSource = Variables.App.prefijoTablas + r["TABLE_NAME"].ToString();
						sqB.Where = null;

						DataTable dt = db.Execute(sqB.BuildQuery());
						StringBuilder ins = new StringBuilder("");

						if (b == Utils.TypeBd.SQLServer & hayAutoNumeric) {
							ins.Append("SET IDENTITY_INSERT [" + r["TABLE_NAME"] + "] ON" + salto + goComm + salto +
							salto);
						}

						foreach (DataRow row in dt.Rows) {
							ins.Append("INSERT INTO [" + r["TABLE_NAME"] + "] (");
							campos = "";
							foreach (DataRow fld in col.Rows) {
								campos = campos + "[" + fld["ColumnName"] + "],";
							}
							if (campos != "") {
								campos = TextUtil.Substring(campos, 0, TextUtil.Length(campos) - 1);
							}
							ins.Append(campos);
							ins.Append(") VALUES ");

							campos = "(";
							foreach (DataRow fld in col.Rows) {
								int tamano = int.Parse(fld["ColumnSize"].ToString());
						
								if (tamano > 4000)
									tamano = 4000;
						
								tipo = DameTipo(fld["DataType"].ToString().ToLower(),
									tamano, b);
								string campo = row[fld["ColumnName"].ToString()].ToString();

								switch (tipo) {
									case "Text":
									case "LongText":
									case "Varchar":
									case "nvarchar":
										campos = campos + "'" +
										formatCad(campo, tamano, b) + "',";
										break;
									case "datetime":
									case "Date":
										campos = campos + formatDate(campo, b) + ",";
										break;
									case "Decimal":
									case "SmallInt":
									case "Int":
									case "BigInt":
									case "Double":
									case "TinyInt":
									case "float":
										string num =
											NumberUtils.NumberDouble(campo).ToString();
										campos = campos + TextUtil.Replace(num, ",", ".") + ",";
										break;
									case "number(1)":
									case "Boolean":
									case "bit":
										campos = campos + campo + ",";
										break;
								}
							}
							if (campos != "") {
								campos = TextUtil.Substring(campos, 0, TextUtil.Length(campos) - 1);
							}
							ins.Append(campos);
							ins.Append(");" + salto);
						}

						if (b == Utils.TypeBd.SQLServer & hayAutoNumeric) {
							ins.Append(salto + "SET IDENTITY_INSERT [" + r["TABLE_NAME"] + "] OFF" + salto + goComm +
							salto + salto);
						}

						ins.Append(salto + salto);

						s.Append(Utils.FormatSQL(ins.ToString(), b));
						s.Append(salto);
					}
				}
			}

			DataTable vds = db.GetSchemaView();

			foreach (DataRow r in vds.Rows) {
				s.Append("CREATE VIEW " + r["TABLE_NAME"] + " AS " + salto);

				s.Append(Utils.FormatSQL(r["VIEW_DEFINITION"].ToString(), b));
				s.Append(salto + goComm + salto + salto);

				//s.Append(r["TABLE_NAME"].ToString() + salto);
			}

			if (b == Utils.TypeBd.SQLServer) {
				s.Append(salto);
				s.Append(commentSql);
				s.Append("GO");
				s.Append(salto);
			}

			return s;
		}
		
		
		private string formatDate(string cad, Utils.TypeBd b)
		{
			System.DateTime date = FSLibrary.DateTimeUtil.ValorFecha(cad, new System.DateTime(2000, 1, 1));
			string dat = FSLibrary.DateTimeUtil.ShortDate(date);
			
			if(b == Utils.TypeBd.MySQL)
				dat = date.ToString("yyyy-MM-dd HH:mm:ss");
			
			return "'" + dat + "'";
		}


		private string formatCad(string cad, int size, Utils.TypeBd b)
		{
			if(b == Utils.TypeBd.Oracle)
				if (cad.Length > size)
					cad = cad.Substring(0, size);
			
			return TextUtil.Replace(cad, "'", "&#39;");
		}


		private string PrintField(string nomCampo, int tamano, Boolean esautonumerico, string coma, string tipoM,
            Utils.TypeBd bd, string comentario, string salto)
		{
			StringBuilder s = new StringBuilder("");
			string auto = "";

			if (esautonumerico) {
				if (bd == Utils.TypeBd.MySQL | bd == Utils.TypeBd.SQLite) {
					auto = " auto_increment ";
				}
				if (bd == Utils.TypeBd.SQLServer) {
					auto = " IDENTITY(1,1) ";
				}
			}

			s.Append(salto);
			
			string collate = "";
			if (bd == Utils.TypeBd.SQLite) {
				collate = " collate nocase";
			}
			
			if (tipoM == "Varchar" | tipoM == "nvarchar") {
				if (tamano != 0) {
					s.Append("     [" + nomCampo + "] " + tipoM + "(" + tamano + ")" + collate);
				} else {
					s.Append("     [" + nomCampo + "] " + tipoM + "(50)" + collate);
				}
			} else {
				s.Append("     [" + nomCampo + "] " + tipoM + auto);
			}

			if (bd == Utils.TypeBd.MySQL) {
				if (comentario != nomCampo) {
					s.Append(" COMMENT '" + comentario + "'");
				}
			}

			s.Append(coma);

			return s.ToString();
		}


		private static string DameTipo(string tipo, int size, Utils.TypeBd bd)
		{
			string t;
			switch (tipo) {
				case "system.int16":
				case "system.byte":
					t = "SmallInt";
					break;
				case "system.integer":
				case "system.int32":
					t = "Int";
					break;
				case "system,int64":
					t = "BigInt";
					break;
				case "system.single":
					t = "TinyInt";
					break;
				case "system.double":
					t = bd == Utils.TypeBd.SQLServer ? "float" : "Double";
					break;
				case "system.datetime":
					t = (bd == Utils.TypeBd.SQLServer | bd== Utils.TypeBd.MySQL) ? "datetime" : "Date";
					break;
				case "system.string":
					t = bd == Utils.TypeBd.SQLServer ? "nvarchar" : "Varchar";
					break;
				case "system.decimal":
					t = "Decimal";
					break;
				case "system.boolean":
				case "system.sbyte":
					t = bd == Utils.TypeBd.SQLServer ? "bit" : (bd == Utils.TypeBd.Oracle) ? "number(1)" : "Boolean";
					break;
				default:
					t = tipo;
					break;
			}


			if (size >= 4000) {
				t = (bd == Utils.TypeBd.SQLServer) ? "Text" : (bd == Utils.TypeBd.Oracle) ? "Varchar" : "LongText";
			}

			return t;
		}
	}
}