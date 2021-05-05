
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
	public class frmAyuntamientos : System.Windows.Forms.Form
	{
		
		
		internal FSFormControls.DBLabel DbLabel1;
		internal FSFormControls.DBLabel DbLabel2;
		internal FSFormControls.DBTextBox dbtPagina;
		internal System.Windows.Forms.Label Label4;
		
		
#region  Código generado por el Diseñador de Windows Forms
		
		public frmAyuntamientos()
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
		internal System.Windows.Forms.TextBox txtPath;
		internal FSFormControls.DBButton DbButton1;
		internal System.Windows.Forms.ListBox lstResultados;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.txtPath = new System.Windows.Forms.TextBox();
			this.Label1 = new System.Windows.Forms.Label();
			this.lstResultados = new System.Windows.Forms.ListBox();
			this.Label6 = new System.Windows.Forms.Label();
			this.DbButton1 = new FSFormControls.DBButton();
			this.DbButton1.Click += new System.EventHandler(this.DbButton1_Click);
			this.DbLabel1 = new FSFormControls.DBLabel();
			this.DbLabel2 = new FSFormControls.DBLabel();
			this.dbtPagina = new FSFormControls.DBTextBox();
			this.Label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			//
			//txtPath
			//
			this.txtPath.Location = new System.Drawing.Point(184, 22);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(544, 20);
			this.txtPath.TabIndex = 0;
			this.txtPath.Text = "http://www.guiadeayuntamientos.info/index.php?sec=ayuntamientos&id_ayu=%pagina%";
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
			this.lstResultados.Size = new System.Drawing.Size(256, 95);
			this.lstResultados.TabIndex = 9;
			//
			//Label6
			//
			this.Label6.AutoSize = true;
			this.Label6.Location = new System.Drawing.Point(35, 100);
			this.Label6.Name = "Label6";
			this.Label6.Size = new System.Drawing.Size(97, 13);
			this.Label6.TabIndex = 12;
			this.Label6.Text = "Links encontrados:";
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
			this.DbButton1.Text = "Procesar";
			this.DbButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.DbButton1.TextColorEnd = System.Drawing.Color.Black;
			this.DbButton1.TextColorStart = System.Drawing.Color.Blue;
			this.DbButton1.TextFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.DbButton1.ToolTip = "";
			this.DbButton1.Track = false;
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
			this.DbLabel1.Location = new System.Drawing.Point(590, 225);
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
			this.DbLabel2.Location = new System.Drawing.Point(443, 225);
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
			//dbtPagina
			//
			this.dbtPagina.About = null;
			this.dbtPagina.AsociatedCombo = null;
			this.dbtPagina.AsociatedDBFindTextBox = null;
			this.dbtPagina.BackColorRead = System.Drawing.Color.WhiteSmoke;
			this.dbtPagina.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dbtPagina.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
			this.dbtPagina.DataControl = null;
			this.dbtPagina.DataType = FSFormControls.DBTextBox.TypeData.All;
			this.dbtPagina.DateFormat = "dd/MM/yyyy";
			this.dbtPagina.DBField = null;
			this.dbtPagina.DBFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.dbtPagina.Decimals = 2;
			this.dbtPagina.DefaultValue = "";
			this.dbtPagina.DotNumber = false;
			this.dbtPagina.Editable = true;
			this.dbtPagina.Encrypted = false;
			this.dbtPagina.Expression = "";
			this.dbtPagina.FormatString = "";
			this.dbtPagina.GridOperation = FSFormControls.DBColumn.OperationTypes.Sum;
			this.dbtPagina.Location = new System.Drawing.Point(576, 100);
			this.dbtPagina.MaskInput = null;
			this.dbtPagina.MaxLength = (int) ((long) (32767));
			this.dbtPagina.MaxValue = decimal.MaxValue;
			this.dbtPagina.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
			this.dbtPagina.Multiline = false;
			this.dbtPagina.Name = "dbtPagina";
			this.dbtPagina.Obligatory = false;
			this.dbtPagina.ReadOnly = false;
			this.dbtPagina.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.dbtPagina.Shadow = false;
			this.dbtPagina.ShadowColor = System.Drawing.Color.Gray;
			this.dbtPagina.ShadowSize = 4;
			this.dbtPagina.ShowAsCombo = false;
			this.dbtPagina.ShowKeyboard = false;
			this.dbtPagina.Size = new System.Drawing.Size(152, 20);
			this.dbtPagina.TabIndex = 26;
			this.dbtPagina.Text = "8200";
			this.dbtPagina.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.dbtPagina.ToolTip = "";
			this.dbtPagina.Track = false;
			this.dbtPagina.XMLName = null;
			//
			//Label4
			//
			this.Label4.AutoSize = true;
			this.Label4.Location = new System.Drawing.Point(462, 100);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(106, 13);
			this.Label4.TabIndex = 29;
			this.Label4.Text = "Ayuntamientos Total:";
			//
			//frmAyuntamientos
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(759, 247);
			this.Controls.Add(this.Label4);
			this.Controls.Add(this.dbtPagina);
			this.Controls.Add(this.DbLabel2);
			this.Controls.Add(this.DbLabel1);
			this.Controls.Add(this.DbButton1);
			this.Controls.Add(this.Label6);
			this.Controls.Add(this.lstResultados);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.txtPath);
			this.Name = "frmAyuntamientos";
			this.Text = "Base de datos Ayuntamientos";
			this.ResumeLayout(false);
			this.PerformLayout();
			
		}
		
#endregion
		
		
		double total = 0;
		FSFormControls.DBHtmlDocument mDocument;
		int pagina;
		string antLink = "";
		
		
		private void DbButton1_Click(System.Object sender, System.EventArgs e)
		{
			Procesa();
			
			MessageBox.Show("Finalizado!");
		}
		
		public void Procesa()
		{
			
			if (txtPath.ToString().EndsWith("/"))
			{
				this.txtPath.Text = this.txtPath.ToString().Substring(0, this.txtPath.Text.Length);
			}
			
			
			pagina = 1; //CInt(Me.dbtPagina.Text)
			
			int f = default(int);
			for (f = 1; f <= int.Parse(this.dbtPagina.Text); f++)
			{
				pagina = f;
				procesaDirectorio(null, this.txtPath.Text);
			}
		}
		
		public void procesaDirectorio(FSFormControls.DBHtmlElement nodeDir, string path)
		{
			WinInet http = new WinInet();
			string html = default(string);
			
			path = path.Replace("%pagina%", pagina.ToString());
			
			html = http.GetHttpFile(path);
			
			this.lstResultados.Items.Add(path);
			
			mDocument = FSFormControls.DBHtmlDocument.Create(html, false);
			
			ProcesaData(mDocument);
		}
		
		
		
		public bool ProcesaData(FSFormControls.DBHtmlDocument doc)
		{
			
			FSFormControls.DBHtmlNodeCollection nodes1 = default(FSFormControls.DBHtmlNodeCollection);
			nodes1 = doc.Nodes.FindByAttributeNameValue("class", "txt_blanco", true);
			
			
			FSFormControls.DBHtmlNodeCollection nodes = default(FSFormControls.DBHtmlNodeCollection);
			nodes = doc.Nodes.FindByAttributeNameValue("class", "txt_contenido", true);
			
			string nombre = ((FSFormControls.DBHtmlElement) (nodes1[0])).FirstChild.FirstChild.HTML.Replace( "Ayuntamiento de ", "");
			string alcalde = ((FSFormControls.DBHtmlElement) (nodes[0])).Text;
			string partido = ((FSFormControls.DBHtmlElement) (nodes[1])).Text;
			string direccion = ((FSFormControls.DBHtmlElement) (nodes[2])).Text;
			string cp = ((FSFormControls.DBHtmlElement) (nodes[3])).Text;
			string habitantes = ((FSFormControls.DBHtmlElement) (nodes[4])).Text;
			string telefono = ((FSFormControls.DBHtmlElement) (nodes[5])).Text;
			
			FSFormControls.DBHtmlNodeCollection nodes2 = default(FSFormControls.DBHtmlNodeCollection);
			nodes2 = doc.Nodes.FindByName("table", true);
			
			string links = ((FSFormControls.DBHtmlElement) (nodes2[33])).HTML;
			
			if (cp == "")
			{
				return default(bool);
			}
			
			Application.DoEvents();
			
			links = links.Replace("\'", "?");
			nombre = nombre.Replace("\'", "?");
			partido = partido.Replace("\'", "?");
			alcalde = alcalde.Replace("\'", "?");
			direccion = direccion.Replace("\'", "?");
			nombre = FSFormControls.DBHtmlEncoder.DecodeValue(nombre);
			links = FSFormControls.DBHtmlEncoder.DecodeValue(links);
			telefono = telefono.Replace(" ", "");
			
			BdUtils db = new BdUtils(FSFormControls.Global.ConnectionStringSetting);
			string ssql = default(string);
			ssql = "insert into ayuntamientos (idAyuntamiento,nombre,alcalde,partido,direccion,habitantes,telefono,cp,informacion) values (" + total.ToString() + ",\'" + nombre + "\',\'" + alcalde + "\',\'" + partido + "\',\'" + direccion + "\'," + habitantes + ",\'" + telefono + "\'," + cp + ",\'" + links + "\')";

            db.ExecuteNonQuery(ssql);
			
			//procesados.Add(link)
			
			total++;
			
			DbLabel1.Text = total.ToString();
			
			this.lstResultados.Items.Add(nombre);
			
			return true;
		}
		
		
	}
	
}
