// <fileheader>
// <copyright file="loaddata.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: admin\editor\loaddata.cs
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

namespace FSPaginas.Admin.Editor
{
	public class LoadData : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			contenido = Inicio();
		}

		private string Inicio()
		{
			string sTableName = Web.Request("tablename");
			sTableName = System.Web.HttpUtility.HtmlDecode(sTableName);

			if (sTableName == "")
				return "";

			StringBuilder sb = new StringBuilder("");

			sb.Append("{"); // JSON Start

			string sOrderBy = "";
			string sWhere = "";
			long lRecs = 0;

			//guardamos en una variable el nombre de la tabla, ya que la variable sTableName, se modifica.
			string sTableN = sTableName;

			//DataTable dt = db.GetSchemaColumn2(sTableName);

			string orderby = Web.Request("sidx");
			string direction = Web.Request("sord");

			string searchField = Web.Request("searchField");
			string searchString = Web.Request("searchString");
			string searchOper = Web.Request("searchOper");

			//bw - begins with ( LIKE val% )
			//eq - equal ( = )
			//ne - not equal ( <> )
			//lt - little ( < )
			//le - little or equal ( <= )
			//gt - greater ( > )
			//ge - greater or equal ( >= )
			//ew - ends with (LIKE %val )
			//cn - contain (LIKE %val% )

			switch (searchOper) {
				case "bw":
					sWhere = " WHERE [" + searchField + "] LIKE '" + searchString + "%' ";
					break;
				case "ew":
					sWhere = " WHERE [" + searchField + "] LIKE '%" + searchString + "' ";
					break;
				case "cn":
					sWhere = " WHERE [" + searchField + "] LIKE '%" + searchString + "%' ";
					break;
				case "eq":
					sWhere = " WHERE [" + searchField + "] = '" + searchString + "' ";
					break;
				case "ne":
					sWhere = " WHERE [" + searchField + "] <> '" + searchString + "' ";
					break;
				case "lt":
					sWhere = " WHERE [" + searchField + "] < " + searchString + " ";
					break;
				case "le":
					sWhere = " WHERE [" + searchField + "] <= " + searchString + " ";
					break;
				case "gt":
					sWhere = " WHERE [" + searchField + "] > " + searchString + " ";
					break;
				case "ge":
					sWhere = " WHERE [" + searchField + "] >= " + searchString + " ";
					break;
			}

			if (orderby != "") {
				sOrderBy = " ORDER BY [" + orderby + "]";
				switch (direction) {
					case "desc":
						sOrderBy = sOrderBy + " DESC";
						break;
					case "asc":
						sOrderBy = sOrderBy + " ASC";
						break;
					default:
						sOrderBy = sOrderBy + " ASC";
						break;
				}
			}

			string sSql = "SELECT * FROM [" + sTableName + "] " + sWhere + sOrderBy;

			int iPage = Web.RequestInt("page");
			if (iPage < 1) {
				iPage = 1;
			}

			int pageSize = Web.RequestInt("rows");

			if (pageSize == 0)
			{
				pageSize = Variables.App.registrosPorPagina;
			}

			DataTable dtData;
			DataTable dtSchema;

			if (Variables.App.UseXML)
			{
				XML xml = new XML(Variables.App.directorioWeb + "data");
				xml.Load("" + sTableName + ".xml");
				dtSchema = xml.GetSchema();
				sWhere = sWhere.Replace("WHERE", "");
				dtData = xml.Select(sWhere, orderby + " " + direction);
			}
			else
			{
				BdUtils db = new BdUtils(Web.RequestInt("cid"));
				dtSchema = db.GetSchemaTable(sTableN);
				dtData = sWhere == "" ? db.Execute(sTableName, iPage, "", sOrderBy, pageSize) : db.Execute(sSql);

				lRecs = db.GetRecordCount(sTableN);
			}

			int iPageCount = NumberUtils.NumberInt(lRecs / pageSize);
			if (lRecs % pageSize > 0) {
				iPageCount = iPageCount + 1;
			}

			sb.Append(@"""page"": " + iPage + @",");
			sb.Append(@"""total"": " + iPageCount + @",");
			sb.Append(@"""records"": " + lRecs + @",");

			//if (iPage > iPageCount)
			//{
			//    iPage = iPageCount;
			//}

			bool first = true;

			sb.Append(@"""rows"":[");

			foreach (DataRow row in dtData.Rows) {
				if (!first)
					sb.Append(",");

				sb.Append(@"{""id"":""" + row[0] + @""",""cell"":[");

				bool subFirst = true;
				int tot = 0;
				bool existFechaModificacion = false;

				foreach (DataRow fld in dtSchema.Rows) {
					string fieldName = fld["ColumnName"].ToString();

                    int columnSize = NumberUtils.NumberInt(fld["ColumnSize"].ToString());
                    if (columnSize < 65535)
                    {  // solo mostramos las columnas que no son MEMO

						if (fieldName != "fechaModificacion")
						{
							if (!subFirst)
								sb.Append(",");

							string sVal = row[fieldName].ToString();
							//sVal = Text.Substring(sVal, 0, 60);
							//sVal = sVal.Replace(@"""","'");

							sb.Append(@"""" + Web.formatJSON(sVal) + @"""");
						}
						else
							existFechaModificacion = true;

                        subFirst = false;
                    }
                    
					tot++;
                    if (tot > Ui.MaxVisibleGridFields)
                        break;
                }
				
				if(existFechaModificacion)
				{
					sb.Append(",");
					string sVal = row["fechaModificacion"].ToString();
					sb.Append(@"""" + Web.formatJSON(sVal) + @"""");
				}

				sb.Append("]}");

				first = false;
			}

			sb.Append("]}"); // end JSON

			return sb.ToString();
		}
	}
}