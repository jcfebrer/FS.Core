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
	public class CrearXML : BasePage
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

		
		private string Generar()
		{
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

			DataTable tables = db.GetSchemaTables();

			foreach (DataRow r in tables.Rows) {
				if (TextUtil.Substring(r["TABLE_NAME"].ToString(), 0, 4) != "MSys" &
				    TextUtil.Substring(r["TABLE_NAME"].ToString(), 0, 1) != "_" &
				    TextUtil.Substring(r["TABLE_NAME"].ToString(), 0, 5) != "Stats" &
				    TextUtil.Substring(r["TABLE_NAME"].ToString(), 0, 5) != "Error"
				   ) {
					
					
					SelectQueryBuilder sqB = new SelectQueryBuilder();
					sqB.Columns.SelectColumns("*");
					sqB.TableSource = Variables.App.prefijoTablas + r["TABLE_NAME"].ToString();
					sqB.Where = null;

					DataTable dt = db.Execute(sqB.BuildQuery(), true);
                    dt.TableName = r["TABLE_NAME"].ToString();

                    dt.WriteXml(Server.MapPath(Variables.App.uploadPath) + @"\" + r["TABLE_NAME"].ToString() + ".xml");
					dt.WriteXmlSchema(Server.MapPath(Variables.App.uploadPath) + @"\" + r["TABLE_NAME"].ToString() + ".xsd");
				}
			}

            return "Ficheros XML generados correctamente en:" + Server.MapPath(Variables.App.uploadPath);
		}
	}
}