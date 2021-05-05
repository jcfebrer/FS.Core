using System;
using System.Data;
using System.Diagnostics;
using System.Text;
using FSPortal;
using FSQueryBuilder;
using FSQueryBuilder.Enums;
using FSQueryBuilder.QueryParts.Where;
using FSDatabase;

namespace FSPortalNET.Sitios.Serport.code
{
	/// <summary>
	/// Description of proveedores
	/// </summary>
	public class choferes : BasePage
	{
		public choferes()
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
			BdUtils db = new BdUtils("OracleConnection");
			StringBuilder sb = new StringBuilder("");
            
			SelectQueryBuilder sqB = new SelectQueryBuilder();
			sqB.Columns.SelectColumns("APELLIDOSCHOFER", "NOMBRECHOFER", "CODIGOCAMION", "CODIGOREMOLQUE");
			sqB.Columns.AliasColumns("Apellidos", "Nombre", "Camión", "Remolque");
			sqB.TableSource = Variables.App.prefijoTablas + "BDGT.CHOFERES";
			sqB.OrderBy.Add(new FSQueryBuilder.QueryParts.OrderBy.OrderByClause("APELLIDOSCHOFER"));
			sqB.Where = new SimpleWhere(sqB.TableSource, "FECHABAJA", Comparison.Is, null);
			FSQueryBuilder.Constants.Dbms.dbmsType = DBMSType.Oracle;
            		
			DataTable dt = db.Execute(sqB.BuildQuery());
			
			
			sb.Append("<table>");
			
			//cabecera
			sb.AppendLine("<tr>");
			for (int i = 0; i < dt.Columns.Count; i++)
				sb.AppendLine("<td>" + dt.Columns[i].ColumnName + "</td>");
			sb.AppendLine("</tr>");
			
			//lineas
			for (int i = 0; i < dt.Rows.Count; i++) {
				sb.AppendLine("<tr>");
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					switch(j)
					{
						case 2:
							sb.AppendLine("<td><a href='" + "documentacion.aspx?{frmcheck(gd_action=openfoldername&gd_id=0B8Es1o63D6UeeFZDN0t3RnlraTg&gd_name=" + dt.Rows[i][j].ToString() + ")}'>" + dt.Rows[i][j].ToString() + "</a></td>");
							break;
						case 3:
							sb.AppendLine("<td><a href='" + "documentacion.aspx?{frmcheck(gd_action=openfoldername&gc_id=0B8Es1o63D6UeeGxZdlNOWFdTNTA&gd_name=" + dt.Rows[i][j].ToString() + ")}'>" + dt.Rows[i][j].ToString() + "</a></td>");
							break;
						default:
							sb.AppendLine("<td>" + dt.Rows[i][j].ToString() + "</td>");
							break;
					}
				}
				sb.AppendLine("</tr>");
			}
			sb.AppendLine("</table>");
			
			
			//FSQueryBuilder.Constants.Dbms.dbmsType = DBMSType.Access;

			return sb.ToString();
		}
	}
}
