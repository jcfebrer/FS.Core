
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;
using FSDatabase;

namespace FSGestion
{
	public class frmGoogle : System.Windows.Forms.Form
	{
		
		FSFormControls.DBHtmlDocument mDocument;
		internal FSFormControls.DBLabel DbLabel1;
		internal FSFormControls.DBLabel DbLabel2;
		ArrayList procesados = new ArrayList();
        double total = 0;
		
		
#region  Código generado por el Diseñador de Windows Forms
		
		public frmGoogle()
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
		internal System.Windows.Forms.TextBox txtGooglePath;
		internal FSFormControls.DBButton DbButton1;
		internal System.Windows.Forms.ListBox lstResultados;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
            this.txtGooglePath = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.lstResultados = new System.Windows.Forms.ListBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.DbButton1 = new FSFormControls.DBButton();
            this.DbLabel1 = new FSFormControls.DBLabel();
            this.DbLabel2 = new FSFormControls.DBLabel();
            this.SuspendLayout();
            // 
            // txtGooglePath
            // 
            this.txtGooglePath.Location = new System.Drawing.Point(184, 22);
            this.txtGooglePath.Name = "txtGooglePath";
            this.txtGooglePath.Size = new System.Drawing.Size(544, 20);
            this.txtGooglePath.TabIndex = 0;
            this.txtGooglePath.Text = "http://www.google.com/Top/World/Espa%C3%B1ol";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(32, 24);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(92, 13);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "Dirección Google:";
            // 
            // lstResultados
            // 
            this.lstResultados.Location = new System.Drawing.Point(35, 124);
            this.lstResultados.Name = "lstResultados";
            this.lstResultados.Size = new System.Drawing.Size(256, 95);
            this.lstResultados.TabIndex = 9;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(35, 100);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(97, 13);
            this.Label6.TabIndex = 12;
            this.Label6.Text = "Links encontrados:";
            // 
            // DbButton1
            // 
            this.DbButton1.About = null;
            this.DbButton1.ButtonStyle = FSFormControls.DBButton.ButtonStyleType.Normal;
            this.DbButton1.DataControl = null;
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
            this.DbButton1.Text = "Procesar";
            this.DbButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DbButton1.TextColorEnd = System.Drawing.Color.Black;
            this.DbButton1.TextColorStart = System.Drawing.Color.Blue;
            this.DbButton1.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DbButton1.ToolTip = "";
            this.DbButton1.Track = false;
            this.DbButton1.Click += new System.EventHandler(this.DbButton1_Click);
            // 
            // DbLabel1
            // 
            this.DbLabel1.About = null;
            this.DbLabel1.Angle = 0F;
            this.DbLabel1.AutoSize = true;
            this.DbLabel1.BackColor = System.Drawing.Color.Transparent;
            this.DbLabel1.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
            this.DbLabel1.DataControl = null;
            this.DbLabel1.DataType = FSFormControls.DBTextBox.TypeData.All;
            this.DbLabel1.DateFormat = "dd/MM/yyyy";
            this.DbLabel1.Decimals = 2;
            this.DbLabel1.EndColor = System.Drawing.Color.LightSkyBlue;
            this.DbLabel1.Location = new System.Drawing.Point(553, 178);
            this.DbLabel1.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbLabel1.Name = "DbLabel1";
            this.DbLabel1.ShadowColor = System.Drawing.Color.Black;
            this.DbLabel1.Size = new System.Drawing.Size(13, 13);
            this.DbLabel1.StartColor = System.Drawing.Color.White;
            this.DbLabel1.TabIndex = 22;
            this.DbLabel1.TabStop = false;
            this.DbLabel1.Text = "0";
            this.DbLabel1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.DbLabel1.Track = false;
            this.DbLabel1.XOffset = 1F;
            this.DbLabel1.YOffset = 1F;
            // 
            // DbLabel2
            // 
            this.DbLabel2.About = null;
            this.DbLabel2.Angle = 0F;
            this.DbLabel2.AutoSize = true;
            this.DbLabel2.BackColor = System.Drawing.Color.Transparent;
            this.DbLabel2.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
            this.DbLabel2.DataControl = null;
            this.DbLabel2.DataType = FSFormControls.DBTextBox.TypeData.All;
            this.DbLabel2.DateFormat = "dd/MM/yyyy";
            this.DbLabel2.Decimals = 2;
            this.DbLabel2.EndColor = System.Drawing.Color.LightSkyBlue;
            this.DbLabel2.Location = new System.Drawing.Point(406, 178);
            this.DbLabel2.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbLabel2.Name = "DbLabel2";
            this.DbLabel2.ShadowColor = System.Drawing.Color.Black;
            this.DbLabel2.Size = new System.Drawing.Size(66, 13);
            this.DbLabel2.StartColor = System.Drawing.Color.White;
            this.DbLabel2.TabIndex = 23;
            this.DbLabel2.TabStop = false;
            this.DbLabel2.Text = "Procesados:";
            this.DbLabel2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.DbLabel2.Track = false;
            this.DbLabel2.XOffset = 1F;
            this.DbLabel2.YOffset = 1F;
            // 
            // frmGoogle
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(759, 247);
            this.Controls.Add(this.DbLabel2);
            this.Controls.Add(this.DbLabel1);
            this.Controls.Add(this.DbButton1);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.lstResultados);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtGooglePath);
            this.Name = "frmGoogle";
            this.Text = "Base de datos Google Directory";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		
#endregion
		
		
		private void DbButton1_Click(System.Object sender, System.EventArgs e)
		{
			if (this.txtGooglePath.Text.StartsWith("/"))
			{
				this.txtGooglePath.Text = this.txtGooglePath.Text.Substring(0, this.txtGooglePath.Text.Length - 1);
			}
			
			ProcesaGoogle();
			
			MessageBox.Show("Finalizado!");
		}
		
		public void ProcesaGoogle()
		{
			procesaDirectorio(null, this.txtGooglePath.Text);
		}
		
		public void procesaDirectorio(FSFormControls.DBHtmlElement nodeDir, string path)
		{
			bool directorio = false;
			bool nextIsData = false;
			string path2 = "";
			
			string html = default(string);
			
			if (nodeDir != null)
			{
				path2 = nodeDir.Attributes[0].Value;
			}
			else
			{
				path2 = "";
			}
			
			if (path2.StartsWith("/"))
			{
				path = "http://www.google.com" + path2;
			}
			else
			{
				path = path + "/" + path2;
			}
			
			if (path.Substring(0, this.txtGooglePath.Text.Length) != this.txtGooglePath.Text)
			{
				return;
			}
			
			html = FSNetwork.Http.GetHTTP(path);
			
			if (procesados.Contains(path))
			{
				return;
			}
			
			procesados.Add(path);
			
			this.lstResultados.Items.Add(path);
			
			mDocument = FSFormControls.DBHtmlDocument.Create(html, false);
			
			FSFormControls.DBHtmlNodeCollection n = new FSFormControls.DBHtmlNodeCollection();
			FSFormControls.DBHtmlNode node = default(FSFormControls.DBHtmlNode);
			
			int pagerank = default(int);
			
			n = mDocument.Nodes.FindByName("a", true);
			
			foreach (FSFormControls.DBHtmlNode tempLoopVar_node in n)
			{
				node = tempLoopVar_node;
				//Me.lstResultados.Items.Add(CType(node, FSFormControls.DBHtmlElement).HTML)
				
				if (System.Convert.ToBoolean((((FSFormControls.DBHtmlElement) node).HTML).IndexOf("dmoz") + 1))
				{
					break;
				}
				
				if (((string) (((FSFormControls.DBHtmlElement) node).Text)).ToLower().Substring(0, 7) == "sugiera")
				{
					break;
				}
				
				if (System.Convert.ToBoolean((((FSFormControls.DBHtmlElement) node).HTML).IndexOf("Mostrar en orden") + 1))
				{
					directorio = false;
				}
				
				if (System.Convert.ToBoolean((node.Parent.HTML).IndexOf("Categor&iacute;a relacionada") + 1))
				{
					directorio = false;
				}
				
				if (directorio)
				{
					procesaDirectorio((FSFormControls.DBHtmlElement) node, path);
				}
				
				if (((FSFormControls.DBHtmlElement) node).Text.ToLower() == "[english]")
				{
					directorio = true;
				}
				
				if (nextIsData)
				{
					ProcesaData((FSFormControls.DBHtmlElement) node, pagerank, path);
					nextIsData = false;
				}
				
				if (System.Convert.ToBoolean((((FSFormControls.DBHtmlElement) node).HTML).IndexOf("pagerank") + 1))
				{
					pagerank = ProcesaLink((FSFormControls.DBHtmlElement) node);
					nextIsData = true;
				}
			}
		}
		
		public int ProcesaLink(FSFormControls.DBHtmlElement nodeLnk)
		{
			string w = ((FSFormControls.DBHtmlElement) (nodeLnk.Nodes[0])).Attributes[1].Value;
			w.Replace(",", "");
			return System.Convert.ToInt32(w);
		}
		
		public void ProcesaData(FSFormControls.DBHtmlElement nodeLnk, int pagerank, string path)
		{
			string link = nodeLnk.Text;
			string lnk = nodeLnk.Attributes[0].Value;
			string title = ((FSFormControls.DBHtmlElement) nodeLnk.Next.Next.Next.Next).Text;
			
			link = link.Replace("\'", "?");
			lnk = lnk.Replace("\'", "?");
			title = title.Replace("\'", "?");
			path = path.Replace("\'", "?");
			
			//MsgBox(path & "-" & pagerank & "-" & lnk & "-" & link & "-" & title)
			BdUtils db = new BdUtils(FSFormControls.Global.ConnectionStringSetting);
			string ssql = default(string);
			ssql = "insert into google (title,link,pagerank,descripcion,path) values (\'" + lnk + "\',\'" + link + "\'," + pagerank.ToString() + ",\'" + title + "\',\'" + path + "\')";
			
			db.ExecuteNonQuery(ssql);
			
			total++;
			
			DbLabel1.Text = total.ToString();
			
			Application.DoEvents();
		}

    }
	
}
