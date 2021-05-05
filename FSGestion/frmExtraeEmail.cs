
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;
using FSNetwork;
using FSDatabase;

namespace FSGestion
{
	public class frmExtraeEmail : System.Windows.Forms.Form
	{
		
		const int maxpaginas = 50;
		FSFormControls.DBHtmlDocument mDocument;
		internal FSFormControls.DBLabel DbLabel1;
		internal FSFormControls.DBLabel DbLabel2;
		ArrayList procesados = new ArrayList();
		ArrayList emailsPro = new ArrayList();
		double total = 0;
		internal FSFormControls.DBButton DbButton2;
		string pagInicial = "";
		int paginas_totales = 0;
		
		
#region  Código generado por el Diseñador de Windows Forms
		
		public frmExtraeEmail()
		{
			
			//El Diseñador de Windows Forms requiere esta llamada.
			InitializeComponent();
			
			//Agregar cualquier inicialización después de la llamada a InitializeComponent()
			
		}
		
		//Form reemplaza a Dispose para limpiar la lista de componentes.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (!(components == null))
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		//Requerido por el Diseñador de Windows Forms
		private System.ComponentModel.Container components = null;
		
		//NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
		//Puede modificarse utilizando el Diseñador de Windows Forms.
		//No lo modifique con el editor de código.
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Label Label6;
		internal System.Windows.Forms.TextBox txtWWW;
		internal FSFormControls.DBButton DbButton1;
		internal System.Windows.Forms.ListBox lstResultados;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.txtWWW = new System.Windows.Forms.TextBox();
			this.Label1 = new System.Windows.Forms.Label();
			this.lstResultados = new System.Windows.Forms.ListBox();
			this.Label6 = new System.Windows.Forms.Label();
			this.DbLabel2 = new FSFormControls.DBLabel();
			this.DbLabel1 = new FSFormControls.DBLabel();
			this.DbButton1 = new FSFormControls.DBButton();
			this.DbButton1.Click += new System.EventHandler(this.DbButton1_Click);
			this.DbButton2 = new FSFormControls.DBButton();
			this.DbButton2.Click += new System.EventHandler(this.DbButton2_Click);
			this.SuspendLayout();
			//
			//txtWWW
			//
			this.txtWWW.Location = new System.Drawing.Point(184, 22);
			this.txtWWW.Name = "txtWWW";
			this.txtWWW.Size = new System.Drawing.Size(414, 20);
			this.txtWWW.TabIndex = 0;
			this.txtWWW.Text = "http://www.febrersoftware.com";
			//
			//Label1
			//
			this.Label1.AutoSize = true;
			this.Label1.Location = new System.Drawing.Point(32, 24);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(55, 13);
			this.Label1.TabIndex = 1;
			this.Label1.Text = "Dirección:";
			//
			//lstResultados
			//
			this.lstResultados.Location = new System.Drawing.Point(35, 124);
			this.lstResultados.Name = "lstResultados";
			this.lstResultados.Size = new System.Drawing.Size(693, 95);
			this.lstResultados.TabIndex = 9;
			//
			//Label6
			//
			this.Label6.AutoSize = true;
			this.Label6.Location = new System.Drawing.Point(35, 100);
			this.Label6.Name = "Label6";
			this.Label6.Size = new System.Drawing.Size(102, 13);
			this.Label6.TabIndex = 12;
			this.Label6.Text = "Emails encontrados:";
			//
			//DbLabel2
			//
			this.DbLabel2.About = null;
			this.DbLabel2.Angle = (float) (0.0F);
			this.DbLabel2.BackColor = System.Drawing.Color.Transparent;
			this.DbLabel2.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
			this.DbLabel2.DataControl = null;
			this.DbLabel2.DataType = FSFormControls.DBTextBox.TypeData.All;
			this.DbLabel2.DateFormat = "dd/MM/yyyy";
			this.DbLabel2.Decimals = 2;
			this.DbLabel2.EndColor = System.Drawing.Color.LightSkyBlue;
			this.DbLabel2.Location = new System.Drawing.Point(420, 225);
			this.DbLabel2.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbLabel2.Name = "DbLabel2";
			this.DbLabel2.ShadowColor = System.Drawing.Color.Black;
			this.DbLabel2.Size = new System.Drawing.Size(141, 24);
			this.DbLabel2.StartColor = System.Drawing.Color.White;
			this.DbLabel2.TabIndex = 23;
			this.DbLabel2.TabStop = false;
			this.DbLabel2.Text = "Procesados:";
			this.DbLabel2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.DbLabel2.Track = false;
			this.DbLabel2.XOffset = (float) (1.0F);
			this.DbLabel2.YOffset = (float) (1.0F);
			//
			//DbLabel1
			//
			this.DbLabel1.About = null;
			this.DbLabel1.Angle = (float) (0.0F);
			this.DbLabel1.BackColor = System.Drawing.Color.Transparent;
			this.DbLabel1.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
			this.DbLabel1.DataControl = null;
			this.DbLabel1.DataType = FSFormControls.DBTextBox.TypeData.All;
			this.DbLabel1.DateFormat = "dd/MM/yyyy";
			this.DbLabel1.Decimals = 2;
			this.DbLabel1.EndColor = System.Drawing.Color.LightSkyBlue;
			this.DbLabel1.Location = new System.Drawing.Point(567, 225);
			this.DbLabel1.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbLabel1.Name = "DbLabel1";
			this.DbLabel1.ShadowColor = System.Drawing.Color.Black;
			this.DbLabel1.Size = new System.Drawing.Size(141, 24);
			this.DbLabel1.StartColor = System.Drawing.Color.White;
			this.DbLabel1.TabIndex = 22;
			this.DbLabel1.TabStop = false;
			this.DbLabel1.Text = "0";
			this.DbLabel1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.DbLabel1.Track = false;
			this.DbLabel1.XOffset = (float) (1.0F);
			this.DbLabel1.YOffset = (float) (1.0F);
			//
			//DbButton1
			//
			this.DbButton1.About = null;
			this.DbButton1.ButtonStyle = FSFormControls.DBButton.ButtonStyleType.Normal;
			this.DbButton1.DropDownMenu = null;
			this.DbButton1.FillColorEnd = System.Drawing.Color.White;
			this.DbButton1.FillColorStart = System.Drawing.Color.LightGray;
			this.DbButton1.FillHoverColorEnd = System.Drawing.Color.Beige;
			this.DbButton1.FillHoverColorStart = System.Drawing.Color.Beige;
			this.DbButton1.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.DbButton1.Gradient = false;
			this.DbButton1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			this.DbButton1.Image = null;
			this.DbButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.DbButton1.Location = new System.Drawing.Point(184, 48);
			this.DbButton1.Name = "DbButton1";
			this.DbButton1.Size = new System.Drawing.Size(129, 26);
			this.DbButton1.TabIndex = 21;
			this.DbButton1.Text = "Procesa BD";
			this.DbButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.DbButton1.TextColorEnd = System.Drawing.Color.Black;
			this.DbButton1.TextColorStart = System.Drawing.Color.Blue;
			this.DbButton1.TextFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.DbButton1.ToolTip = "";
			this.DbButton1.Track = false;
			//
			//DbButton2
			//
			this.DbButton2.About = null;
			this.DbButton2.ButtonStyle = FSFormControls.DBButton.ButtonStyleType.Normal;
			this.DbButton2.DropDownMenu = null;
			this.DbButton2.FillColorEnd = System.Drawing.Color.White;
			this.DbButton2.FillColorStart = System.Drawing.Color.LightGray;
			this.DbButton2.FillHoverColorEnd = System.Drawing.Color.Beige;
			this.DbButton2.FillHoverColorStart = System.Drawing.Color.Beige;
			this.DbButton2.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.DbButton2.Gradient = false;
			this.DbButton2.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			this.DbButton2.Image = null;
			this.DbButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.DbButton2.Location = new System.Drawing.Point(612, 22);
			this.DbButton2.Name = "DbButton2";
			this.DbButton2.Size = new System.Drawing.Size(116, 20);
			this.DbButton2.TabIndex = 24;
			this.DbButton2.Text = "Procesa";
			this.DbButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.DbButton2.TextColorEnd = System.Drawing.Color.Black;
			this.DbButton2.TextColorStart = System.Drawing.Color.Blue;
			this.DbButton2.TextFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.DbButton2.ToolTip = "";
			this.DbButton2.Track = false;
			//
			//frmExtraeEmail
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(759, 247);
			this.Controls.Add(this.DbButton2);
			this.Controls.Add(this.DbLabel2);
			this.Controls.Add(this.DbLabel1);
			this.Controls.Add(this.DbButton1);
			this.Controls.Add(this.Label6);
			this.Controls.Add(this.lstResultados);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.txtWWW);
			this.Name = "frmExtraeEmail";
			this.Text = "Extrae Email";
			this.ResumeLayout(false);
			this.PerformLayout();
			
		}
		
#endregion
		
		
		private void DbButton1_Click(System.Object sender, System.EventArgs e)
		{
			FSFormControls.DBControl dbc = new FSFormControls.DBControl("select * from v_www");
			DataRow r = default(DataRow);
			string l = default(string);
			
			FSFormControls.Global.SilentError = true;
			FSFormControls.Global.Errors.Clear();
			
			foreach (DataRow tempLoopVar_r in dbc.DataTable.Rows)
			{
				r = tempLoopVar_r;
				paginas_totales = 0;
				l = r["link"].ToString();
				if (l.StartsWith("http"))
				{
					l = "http://" + l;
				}
				if (l.EndsWith("/"))
				{
					this.txtWWW.Text = l.Substring(0, l.Length - 1);
				}
				else
				{
					this.txtWWW.Text = l;
				}
				Application.DoEvents();
				
				ProcesaWWW(l);
				
				//Functions.Sleep(3000)
			}
			
			FSFormControls.Global.SilentError = false;
			
			foreach (Exception ed in FSFormControls.Global.Errors)
			{
				MessageBox.Show(ed.ToString());
			}
			
			MessageBox.Show("Finalizado!");
		}
		
		public void ProcesaWWW(string page)
		{
			pagInicial = page;
			procesaDirectorio(null, page);
		}
		
		public void procesaDirectorio(FSFormControls.DBHtmlElement nodeDir, string path)
		{
			string path2 = "";
			
			if (paginas_totales >= maxpaginas)
			{
				return;
			}
			
			WinInet http = new WinInet();
			
			string html = default(string);
			
			if (nodeDir != null)
			{
				if (!(nodeDir.Attributes["href"] == null))
				{
					path2 = nodeDir.Attributes["href"].Value;
				}
				else
				{
					return;
				}
			}
			else
			{
				path2 = "";
			}
			
			if (path2.IndexOf("#") + 1 > 0)
			{
				return;
			}
			
			if (path2.StartsWith("http"))
			{
				path = path2;
				path2 = "";
			}
			
			if (path2.StartsWith("/"))
			{
				path = pagInicial + path2;
			}
			else
			{
				path = pagInicial + "/" + path2;
			}
			
			if (path.IndexOf("javascript") + 1 > 0)
			{
				return;
			}
			
			if (path.IndexOf(".pdf") + 1 > 0)
			{
				return;
			}
			if (path.IndexOf(".gif") + 1 > 0)
			{
				return;
			}
			
			if (path.Substring(0, pagInicial.Length) != pagInicial)
			{
				return;
			}
			
			if (procesados.Contains(path))
			{
				return;
			}
			else
			{
				procesados.Add(path);
			}
			
			emailsPro.Add("Procesando: " + path);
			
			try
			{
				//html = http.GetHttpFile(path)
				html = FSNetwork.Http.GetHTTP(path);
			}
			catch (System.Exception ex)
			{
				Global.Err.ErrorMessage(ex);
				return;
			}
			
			//Me.lstResultados.Items.Add(path)
			Application.DoEvents();
			
			mDocument = FSFormControls.DBHtmlDocument.Create(html, false);
			
			FSFormControls.DBHtmlNodeCollection n = new FSFormControls.DBHtmlNodeCollection();
			FSFormControls.DBHtmlNode node = default(FSFormControls.DBHtmlNode);
			
			
			n = mDocument.Nodes.FindByName("a", true);
			bool add = false;
			
			foreach (FSFormControls.DBHtmlNode tempLoopVar_node in n)
			{
				node = tempLoopVar_node;
				//Me.lstResultados.Items.Add(CType(node, FSFormControls.DBHtmlElement).HTML)
				if (!(((FSFormControls.DBHtmlElement) node).Attributes["href"] == null))
				{
					if ((((FSFormControls.DBHtmlElement) node).Attributes["href"].Value).ToString().StartsWith("mailto"))
					{
						add = true;
						string email = ((FSFormControls.DBHtmlElement) node).Attributes["href"].Value.Replace("mailto:", "");
						ProcesaData(email, pagInicial);
					}
				}
				
				if (!add)
				{
					procesaDirectorio((FSFormControls.DBHtmlElement) node, path);
				}
				
				add = false;
			}
			
			paginas_totales++;
			//Functions.Sleep(3000)
		}
		
		public void ProcesaData(string email, string path)
		{
			BdUtils db = new BdUtils(FSFormControls.Global.ConnectionStringSetting);
			string ssql = default(string);
			
			if (emailsPro.Contains(email))
			{
				return;
			}
			else
			{
				emailsPro.Add(email);
			}
			
			ssql = "insert into emails (link,email) values (\'" + path + "\',\'" + email + "\')";
			
			try
			{
				db.ExecuteNonQuery(ssql);
			}
			catch (System.Exception e)
			{
				Global.Err.ErrorMessage(e);
			}
			
			total++;
			
			DbLabel1.Text = total.ToString();
			
			this.lstResultados.Items.Add(email);
			
			Application.DoEvents();
		}
		
		private void DbButton2_Click(System.Object sender, System.EventArgs e)
		{
			string l = this.txtWWW.Text;
			if (!l.StartsWith("http"))
			{
				l = "http://" + l;
			}
			ProcesaWWW(l);
			
			MessageBox.Show("Finalizado!");
		}
	}
	
}
