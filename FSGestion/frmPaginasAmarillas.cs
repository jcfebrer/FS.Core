
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;
using FSNetwork;

namespace FSGestion
{
	public class frmPaginasAmarillas : System.Windows.Forms.Form
	{
		
		
		internal FSFormControls.DBLabel DbLabel1;
		internal FSFormControls.DBLabel DbLabel2;
		internal FSFormControls.DBTextBox dbtProvincia;
		internal FSFormControls.DBTextBox dbtActividad;
		internal FSFormControls.DBTextBox dbtPagina;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.ListBox lstCategorias;
		internal System.Windows.Forms.CheckBox chkImagenes;
		internal System.Windows.Forms.Label Label4;
		
		
#region  Código generado por el Diseñador de Windows Forms
		
		public frmPaginasAmarillas()
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
			base.Load += new System.EventHandler(frmPaginasAmarillas_Load);
			this.Label1 = new System.Windows.Forms.Label();
			this.lstResultados = new System.Windows.Forms.ListBox();
			this.Label6 = new System.Windows.Forms.Label();
			this.DbButton1 = new FSFormControls.DBButton();
			this.DbButton1.Click += new System.EventHandler(this.DbButton1_Click);
			this.DbLabel1 = new FSFormControls.DBLabel();
			this.DbLabel2 = new FSFormControls.DBLabel();
			this.dbtProvincia = new FSFormControls.DBTextBox();
			this.dbtActividad = new FSFormControls.DBTextBox();
			this.dbtPagina = new FSFormControls.DBTextBox();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label4 = new System.Windows.Forms.Label();
			this.lstCategorias = new System.Windows.Forms.ListBox();
			this.chkImagenes = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			//
			//txtPath
			//
			this.txtPath.Location = new System.Drawing.Point(184, 22);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(544, 20);
			this.txtPath.TabIndex = 0;
			this.txtPath.Text = "http://www.paginasamarillas.es/search/%actividad%/all-ma/%provincia%/all-is/all-c" + "i/all-ba/all-pu/all-nc/%pagina%";
			//
			//Label1
			//
			this.Label1.AutoSize = true;
			this.Label1.Location = new System.Drawing.Point(32, 24);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(140, 13);
			this.Label1.TabIndex = 1;
			this.Label1.Text = "Dirección Paginas Amarillas:";
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
			//dbtProvincia
			//
			this.dbtProvincia.About = null;
			this.dbtProvincia.AsociatedCombo = null;
			this.dbtProvincia.AsociatedDBFindTextBox = null;
			this.dbtProvincia.BackColorRead = System.Drawing.Color.WhiteSmoke;
			this.dbtProvincia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dbtProvincia.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
			this.dbtProvincia.DataControl = null;
			this.dbtProvincia.DataType = FSFormControls.DBTextBox.TypeData.All;
			this.dbtProvincia.DateFormat = "dd/MM/yyyy";
			this.dbtProvincia.DBField = null;
			this.dbtProvincia.DBFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.dbtProvincia.Decimals = 2;
			this.dbtProvincia.DefaultValue = "";
			this.dbtProvincia.DotNumber = false;
			this.dbtProvincia.Editable = true;
			this.dbtProvincia.Encrypted = false;
			this.dbtProvincia.Expression = "";
			this.dbtProvincia.FormatString = "";
			this.dbtProvincia.GridOperation = FSFormControls.DBColumn.OperationTypes.Sum;
			this.dbtProvincia.Location = new System.Drawing.Point(576, 48);
			this.dbtProvincia.MaskInput = null;
			this.dbtProvincia.MaxLength = 32767;
			this.dbtProvincia.MaxValue = decimal.MaxValue;
			this.dbtProvincia.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
			this.dbtProvincia.Multiline = false;
			this.dbtProvincia.Name = "dbtProvincia";
			this.dbtProvincia.Obligatory = false;
			this.dbtProvincia.PasswordChar = (char)0;
			this.dbtProvincia.ReadOnly = false;
			this.dbtProvincia.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.dbtProvincia.Shadow = false;
			this.dbtProvincia.ShadowColor = System.Drawing.Color.Gray;
			this.dbtProvincia.ShadowSize = 4;
			this.dbtProvincia.ShowAsCombo = false;
			this.dbtProvincia.ShowKeyboard = false;
			this.dbtProvincia.Size = new System.Drawing.Size(152, 20);
			this.dbtProvincia.TabIndex = 24;
			this.dbtProvincia.Text = "bizkaia";
			this.dbtProvincia.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.dbtProvincia.ToolTip = "";
			this.dbtProvincia.Track = false;
			this.dbtProvincia.XMLName = null;
			//
			//dbtActividad
			//
			this.dbtActividad.About = null;
			this.dbtActividad.AsociatedCombo = null;
			this.dbtActividad.AsociatedDBFindTextBox = null;
			this.dbtActividad.BackColorRead = System.Drawing.Color.WhiteSmoke;
			this.dbtActividad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dbtActividad.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
			this.dbtActividad.DataControl = null;
			this.dbtActividad.DataType = FSFormControls.DBTextBox.TypeData.All;
			this.dbtActividad.DateFormat = "dd/MM/yyyy";
			this.dbtActividad.DBField = null;
			this.dbtActividad.DBFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.dbtActividad.Decimals = 2;
			this.dbtActividad.DefaultValue = "";
			this.dbtActividad.DotNumber = false;
			this.dbtActividad.Editable = true;
			this.dbtActividad.Encrypted = false;
			this.dbtActividad.Expression = "";
			this.dbtActividad.FormatString = "";
			this.dbtActividad.GridOperation = FSFormControls.DBColumn.OperationTypes.Sum;
			this.dbtActividad.Location = new System.Drawing.Point(576, 74);
			this.dbtActividad.MaskInput = null;
			this.dbtActividad.MaxLength = 32767;
			this.dbtActividad.MaxValue = decimal.MaxValue;
			this.dbtActividad.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
			this.dbtActividad.Multiline = false;
			this.dbtActividad.Name = "dbtActividad";
			this.dbtActividad.Obligatory = false;
            this.dbtActividad.PasswordChar = (char)0;
			this.dbtActividad.ReadOnly = false;
			this.dbtActividad.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.dbtActividad.Shadow = false;
			this.dbtActividad.ShadowColor = System.Drawing.Color.Gray;
			this.dbtActividad.ShadowSize = 4;
			this.dbtActividad.ShowAsCombo = false;
			this.dbtActividad.ShowKeyboard = false;
			this.dbtActividad.Size = new System.Drawing.Size(152, 20);
			this.dbtActividad.TabIndex = 25;
			this.dbtActividad.Text = "informatica";
			this.dbtActividad.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.dbtActividad.ToolTip = "";
			this.dbtActividad.Track = false;
			this.dbtActividad.XMLName = null;
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
			this.dbtPagina.MaxLength = 32767;
			this.dbtPagina.MaxValue = decimal.MaxValue;
			this.dbtPagina.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
			this.dbtPagina.Multiline = false;
			this.dbtPagina.Name = "dbtPagina";
			this.dbtPagina.Obligatory = false;
			this.dbtPagina.PasswordChar = (char)0;
            this.dbtPagina.ReadOnly = false;
			this.dbtPagina.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.dbtPagina.Shadow = false;
			this.dbtPagina.ShadowColor = System.Drawing.Color.Gray;
			this.dbtPagina.ShadowSize = 4;
			this.dbtPagina.ShowAsCombo = false;
			this.dbtPagina.ShowKeyboard = false;
			this.dbtPagina.Size = new System.Drawing.Size(152, 20);
			this.dbtPagina.TabIndex = 26;
			this.dbtPagina.Text = "1";
			this.dbtPagina.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.dbtPagina.ToolTip = "";
			this.dbtPagina.Track = false;
			this.dbtPagina.XMLName = null;
			//
			//Label2
			//
			this.Label2.AutoSize = true;
			this.Label2.Location = new System.Drawing.Point(462, 48);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(54, 13);
			this.Label2.TabIndex = 27;
			this.Label2.Text = "Provincia:";
			//
			//Label3
			//
			this.Label3.AutoSize = true;
			this.Label3.Location = new System.Drawing.Point(462, 74);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(54, 13);
			this.Label3.TabIndex = 28;
			this.Label3.Text = "Actividad:";
			//
			//Label4
			//
			this.Label4.AutoSize = true;
			this.Label4.Location = new System.Drawing.Point(462, 100);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(43, 13);
			this.Label4.TabIndex = 29;
			this.Label4.Text = "Página:";
			//
			//lstCategorias
			//
			this.lstCategorias.FormattingEnabled = true;
			this.lstCategorias.Location = new System.Drawing.Point(297, 124);
			this.lstCategorias.Name = "lstCategorias";
			this.lstCategorias.Size = new System.Drawing.Size(431, 95);
			this.lstCategorias.TabIndex = 30;
			//
			//chkImagenes
			//
			this.chkImagenes.AutoSize = true;
			this.chkImagenes.Location = new System.Drawing.Point(184, 80);
			this.chkImagenes.Name = "chkImagenes";
			this.chkImagenes.Size = new System.Drawing.Size(100, 17);
			this.chkImagenes.TabIndex = 31;
			this.chkImagenes.Text = "Omitir imagenes";
			this.chkImagenes.UseVisualStyleBackColor = true;
			//
			//frmPaginasAmarillas
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(759, 247);
			this.Controls.Add(this.chkImagenes);
			this.Controls.Add(this.lstCategorias);
			this.Controls.Add(this.Label4);
			this.Controls.Add(this.Label3);
			this.Controls.Add(this.Label2);
			this.Controls.Add(this.dbtPagina);
			this.Controls.Add(this.dbtActividad);
			this.Controls.Add(this.dbtProvincia);
			this.Controls.Add(this.DbLabel2);
			this.Controls.Add(this.DbLabel1);
			this.Controls.Add(this.DbButton1);
			this.Controls.Add(this.Label6);
			this.Controls.Add(this.lstResultados);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.txtPath);
			this.Name = "frmPaginasAmarillas";
			this.Text = "Base de datos Paginas Amarillas";
			this.ResumeLayout(false);
			this.PerformLayout();
			
		}
		
#endregion
		
		
		double total = 0;
		FSFormControls.DBHtmlDocument mDocument;
		int pagina;
		string actividad;
		//Dim procesados As New ArrayList
		//Dim antLink As String = ""
		FSFormControls.DBControl db = new FSFormControls.DBControl("select * from PaginasAmarillas");
		
		
		private void DbButton1_Click(System.Object sender, System.EventArgs e)
		{
			Procesa();
			
			MessageBox.Show("Finalizado!");
		}
		
		public void Procesa()
		{
			int f = default(int);
			
			if (this.txtPath.Text.StartsWith("/"))
			{
				this.txtPath.Text = this.txtPath.Text.Substring(0, this.txtPath.Text.Length - 1);
			}
			
			if (this.lstCategorias.Items.Count > 0 && this.dbtActividad.Text == "")
			{
				for (f = 0; f <= this.lstCategorias.Items.Count - 1; f++)
				{
					pagina = int.Parse(this.dbtPagina.Text);
					actividad = (string) (this.lstCategorias.Items[f]);
					procesaDirectorio(null, this.txtPath.Text);
				}
			}
			else
			{
				pagina = int.Parse(this.dbtPagina.Text);
				actividad = this.dbtActividad.Text;
				procesaDirectorio(null, this.txtPath.Text);
			}
		}
		
		public void procesaDirectorio(FSFormControls.DBHtmlElement nodeDir, string path)
		{
			string html = default(string);
			
			path = path.Replace("%pagina%", pagina.ToString());
			path = path.Replace("%provincia%", this.dbtProvincia.Text);
			path = path.Replace("%actividad%", actividad);
			
			html = Http.GetHTTP(path, System.Text.Encoding.UTF8);
			
			
			this.lstResultados.Items.Add(path);
			
			mDocument = FSFormControls.DBHtmlDocument.Create(html, false);
			
			FSFormControls.DBHtmlNodeCollection n = new FSFormControls.DBHtmlNodeCollection();
			FSFormControls.DBHtmlNode node = default(FSFormControls.DBHtmlNode);
			
			//n = mDocument.Nodes.FindByName("a", True)
			n = mDocument.Nodes.FindByAttributeNameValue("itemtype", "http://schema.org/LocalBusiness", true, true);
			
			bool hayAlgunLink = false;
			foreach (FSFormControls.DBHtmlNode tempLoopVar_node in n)
			{
				node = tempLoopVar_node;
				//If directorio Then procesaDirectorio(node, path)
				
				if (ProcesaData((FSFormControls.DBHtmlElement) node, actividad))
				{
					hayAlgunLink = true;
				}
			}
			
			if (hayAlgunLink)
			{
				pagina++;
				procesaDirectorio(null, this.txtPath.Text);
			}
		}
		
		
		
		public bool ProcesaData(FSFormControls.DBHtmlElement nodeLnk, string path)
		{
			
			string email = "";
			FSFormControls.DBHtmlNodeCollection emailN = nodeLnk.Nodes.FindByAttributeName("href", true);
			foreach (FSFormControls.DBHtmlNode node in emailN)
			{
				if (((FSFormControls.DBHtmlElement) node).HTML.IndexOf("emailus") > 0)
				{
					email = ((FSFormControls.DBHtmlElement) node).HTML;
					System.Int32 temp_index = 0;
					email = FSLibrary.TextUtil.GetDelimited(email, "businessEmail=", "&amp;",ref temp_index);
					email = email.Replace("%40", "@");
					break;
				}
			}
			
			FSFormControls.DBHtmlNodeCollection anuncianteN = nodeLnk.Nodes.FindByAttributeNameValue("itemprop", "name", true);
			FSFormControls.DBHtmlNodeCollection descripcionN = nodeLnk.Nodes.FindByAttributeNameValue("itemprop", "description", true);
			FSFormControls.DBHtmlNodeCollection webN = nodeLnk.Nodes.FindByAttributeNameValue("itemprop", "url", true);
			FSFormControls.DBHtmlNodeCollection direccionN = nodeLnk.Nodes.FindByAttributeNameValue("itemprop", "streetAddress", true);
			FSFormControls.DBHtmlNodeCollection logoN = nodeLnk.Nodes.FindByAttributeNameValue("itemprop", "logo", true);
			FSFormControls.DBHtmlNodeCollection postalCodeN = nodeLnk.Nodes.FindByAttributeNameValue("itemprop", "postalCode", true);
			FSFormControls.DBHtmlNodeCollection localidadN = nodeLnk.Nodes.FindByAttributeNameValue("itemprop", "addressLocality", true);
			FSFormControls.DBHtmlNodeCollection provinciaN = nodeLnk.Nodes.FindByAttributeNameValue("class", "region", true);
			FSFormControls.DBHtmlNodeCollection telefonoN = nodeLnk.Nodes.FindByAttributeNameValue("class", "m-icon--single-phone", true);
			
			string telefono = "";
			if (telefonoN.Count > 0)
			{
				telefono = ((FSFormControls.DBHtmlElement) (telefonoN[0])).Text;
			}
			string provincia = "";
			if (provinciaN.Count > 0)
			{
				provincia = ((FSFormControls.DBHtmlElement) (provinciaN[0])).Text;
			}
			string localidad = "";
			if (localidadN.Count > 0)
			{
				localidad = ((FSFormControls.DBHtmlElement) (localidadN[0])).Text;
			}
			string postalCode = "";
			if (postalCodeN.Count > 0)
			{
				postalCode = ((FSFormControls.DBHtmlElement) (postalCodeN[0])).Text;
			}
			string web = "";
			if (webN.Count > 0)
			{
				web = ((FSFormControls.DBHtmlElement) (webN[0])).Text;
			}
			string descripcion = "";
			if (descripcionN.Count > 0)
			{
				descripcion = ((FSFormControls.DBHtmlElement) (descripcionN[0])).HTML;
			}
			string anunciante = "";
			if (anuncianteN.Count > 0)
			{
				anunciante = ((FSFormControls.DBHtmlElement) (anuncianteN[0])).Text;
			}
			string direccion = "";
			if (direccionN.Count > 0)
			{
				direccion = ((FSFormControls.DBHtmlElement) (direccionN[0])).Text;
			}
			string logo = "";
			if (logoN.Count == 1)
			{
				logo = ((FSFormControls.DBHtmlElement) (logoN[0])).Attributes["src"].Value;
			}
			if (logoN.Count == 2)
			{
				logo = ((FSFormControls.DBHtmlElement) (logoN[1])).Attributes["src"].Value;
			}
			if (logoN.Count == 3)
			{
				logo = ((FSFormControls.DBHtmlElement) (logoN[2])).Attributes["src"].Value;
			}
			
			if (logo.ToLower().StartsWith("d:"))
			{
				if (logoN.Count == 1)
				{
					logo = "";
				}
				if (logoN.Count == 2)
				{
					logo = ((FSFormControls.DBHtmlElement) (logoN[1])).Attributes["src"].Value;
				}
			}
			
			Application.DoEvents();
			
			if (anunciante == "")
			{
				return false;
			}
			
			//If antLink = title Then Return False
			//If procesados.Contains(link) Then Return False
			
			if (logo != "" && !chkImagenes.Checked)
			{
				try
				{
					System.IO.Stream ImageStream = new System.Net.WebClient().OpenRead(logo);
					logo = DateTime.Now.Year.ToString().PadLeft(4, '0') + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(4, '0') + "_" + Convert.ToInt32(total).ToString().PadLeft(6, '0') + logo.Substring(0, 4);
					
					Image logoPIC = Image.FromStream(ImageStream);
					string pathLogo = Application.StartupPath + "\\logos\\" + logo;
					
					System.IO.File.Delete(pathLogo);
					
					logoPIC.Save(pathLogo);
					
					ImageStream.Close();
				}
				catch (System.Exception ex)
				{
					Global.Err.ErrorMessage(this.FindForm(), ex, true);
					logo = "";
				}
			}
			
			//Dim ssql As String
			
			try
			{
				db.AddNew();
				//db.SetField("idLink", total)
				db.SetField("title", anunciante);
				db.SetField("link", web);
				db.SetField("descripcion", descripcion);
				db.SetField("path", path);
				db.SetField("direccion", direccion);
				db.SetField("portalPA", false.ToString());
				db.SetField("logo", logo);
				db.SetField("email", email);
				db.SetField("telefono", telefono);
				db.SetField("poblacion", localidad);
				db.SetField("provincia", provincia);
				db.SetField("cp", postalCode);
				db.Save();
				
				//ssql = "insert into paginasAmarillas (idLink,title,link,descripcion,path,direccion,portalPA,logo,email,telefono,poblacion,provincia,cp) values (" & total & ",""" & anunciante & """,""" & web & """,""" & descripcion & """,""" & path & """,""" & direccion & """,false,""" & logo & """,""" & email & """,""" & telefono & """,""" & localidad & """,""" & provincia & """,""" & postalCode & """)"
				
				//db.Execute(ssql)
			}
			catch (System.Exception e)
			{
				Global.Err.ErrorMessage(e);
			}
			
			//procesados.Add(link)
			//antLink = title
			
			DbLabel1.Text = "Página: " + pagina.ToString() + " - Total: " + total.ToString();
			
			total++;
			
			this.lstResultados.Items.Add(web);
			
			return true;
		}
		
		private void frmPaginasAmarillas_Load(System.Object sender, System.EventArgs e)
		{
			FSFormControls.DBControl dbc = new FSFormControls.DBControl("select * from categoriasEnlaces where procesar=true");
			System.Data.DataRow r = default(System.Data.DataRow);
			
			foreach (System.Data.DataRow tempLoopVar_r in dbc.DataTable.Rows)
			{
				r = tempLoopVar_r;
				this.lstCategorias.Items.Add(r["descripcion"]);
			}
		}
	}
	
}
