
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;


using FSFormControls;


namespace FSGestion
{
	public class frmLogin : FSFormControls.DBForm
	{
		
		public FSFormControls.DBForm frmDest;
		
#region  Código generado por el Diseñador de Windows Forms
		
		public frmLogin()
		{
			
			//El Diseñador de Windows Forms requiere esta llamada.
			InitializeComponent();
			
			//Agregar cualquier inicialización después de la llamada a InitializeComponent()
			this.MdiParent = Global.mdiP;
			
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
		internal FSFormControls.DBButton DbButton1;
		internal FSFormControls.DBLabel DbLabel1;
		internal FSFormControls.DBLabel DbLabel2;
		internal FSFormControls.DBTextBox DbTextBox1;
		internal FSFormControls.DBTextBox DbTextBox2;
		internal FSFormControls.DBLabel DbLabel3;
		internal FSFormControls.DBControl DbControl1;
		internal FSFormControls.DBPicture DbPicture1;
		internal FSFormControls.DBPicture DbPicture2;
		internal System.Windows.Forms.PictureBox PictureBox2;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.DbButton1 = new FSFormControls.DBButton();
            this.DbLabel1 = new FSFormControls.DBLabel();
            this.DbLabel2 = new FSFormControls.DBLabel();
            this.DbTextBox1 = new FSFormControls.DBTextBox();
            this.DbTextBox2 = new FSFormControls.DBTextBox();
            this.DbLabel3 = new FSFormControls.DBLabel();
            this.DbControl1 = new FSFormControls.DBControl();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.DbPicture2 = new FSFormControls.DBPicture();
            ((System.ComponentModel.ISupportInitialize)(this.DbStatusBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbStatusBarPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbStatusBarProgressPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbSBarPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbSBarPanel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbSBarPanel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbTextBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbTextBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // DbStatusBar1
            // 
            this.DbStatusBar1.Location = new System.Drawing.Point(0, 115);
            // 
            // mnuForm
            // 
            this.mnuForm.OwnerDraw = true;
            this.mnuForm.Visible = false;
            // 
            // DbButton1
            // 
            this.DbButton1.About = null;
            this.DbButton1.Appearance = null;
            this.DbButton1.ButtonStyle = FSFormControls.DBButton.ButtonStyleType.Normal;
            this.DbButton1.DataControl = null;
            this.DbButton1.DBField = null;
            this.DbButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.DbButton1.DropDownMenu = null;
            this.DbButton1.FillColorEnd = System.Drawing.Color.White;
            this.DbButton1.FillColorStart = System.Drawing.Color.LightGray;
            this.DbButton1.FillHoverColorEnd = System.Drawing.Color.Beige;
            this.DbButton1.FillHoverColorStart = System.Drawing.Color.Beige;
            this.DbButton1.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.DbButton1.Gradient = false;
            this.DbButton1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.DbButton1.Image = null;
            this.DbButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DbButton1.Key = "Button1";
            this.DbButton1.Location = new System.Drawing.Point(168, 184);
            this.DbButton1.Name = "DbButton1";
            this.DbButton1.Size = new System.Drawing.Size(88, 24);
            this.DbButton1.TabIndex = 5;
            this.DbButton1.Text = "Aceptar";
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
            this.DbLabel1.Angle = 12F;
            this.DbLabel1.Appearance = null;
            this.DbLabel1.BackColor = System.Drawing.Color.Transparent;
            this.DbLabel1.BorderStyleInner = System.Windows.Forms.BorderStyle.None;
            this.DbLabel1.BorderStyleOuter = System.Windows.Forms.BorderStyle.None;
            this.DbLabel1.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
            this.DbLabel1.DataControl = null;
            this.DbLabel1.DataType = FSFormControls.DBTextBox.TypeData.All;
            this.DbLabel1.DateFormat = "dd/MM/yyyy";
            this.DbLabel1.DBField = null;
            this.DbLabel1.Decimals = 2;
            this.DbLabel1.EndColor = System.Drawing.Color.LightSkyBlue;
            this.DbLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DbLabel1.Location = new System.Drawing.Point(104, 112);
            this.DbLabel1.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbLabel1.Name = "DbLabel1";
            this.DbLabel1.ShadowColor = System.Drawing.Color.Blue;
            this.DbLabel1.Size = new System.Drawing.Size(78, 23);
            this.DbLabel1.StartColor = System.Drawing.Color.White;
            this.DbLabel1.TabIndex = 1;
            this.DbLabel1.TabStop = false;
            this.DbLabel1.Text = "Usuario:";
            this.DbLabel1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.DbLabel1.Track = false;
            this.DbLabel1.XOffset = 1F;
            this.DbLabel1.YOffset = 1F;
            // 
            // DbLabel2
            // 
            this.DbLabel2.About = null;
            this.DbLabel2.Angle = 0F;
            this.DbLabel2.Appearance = null;
            this.DbLabel2.BackColor = System.Drawing.Color.Transparent;
            this.DbLabel2.BorderStyleInner = System.Windows.Forms.BorderStyle.None;
            this.DbLabel2.BorderStyleOuter = System.Windows.Forms.BorderStyle.None;
            this.DbLabel2.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
            this.DbLabel2.DataControl = null;
            this.DbLabel2.DataType = FSFormControls.DBTextBox.TypeData.All;
            this.DbLabel2.DateFormat = "dd/MM/yyyy";
            this.DbLabel2.DBField = null;
            this.DbLabel2.Decimals = 2;
            this.DbLabel2.EndColor = System.Drawing.Color.LightSkyBlue;
            this.DbLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DbLabel2.Location = new System.Drawing.Point(104, 144);
            this.DbLabel2.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbLabel2.Name = "DbLabel2";
            this.DbLabel2.ShadowColor = System.Drawing.Color.Black;
            this.DbLabel2.Size = new System.Drawing.Size(78, 23);
            this.DbLabel2.StartColor = System.Drawing.Color.White;
            this.DbLabel2.TabIndex = 2;
            this.DbLabel2.TabStop = false;
            this.DbLabel2.Text = "Password:";
            this.DbLabel2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.DbLabel2.Track = false;
            this.DbLabel2.XOffset = 1F;
            this.DbLabel2.YOffset = 1F;
            // 
            // DbTextBox1
            // 
            this.DbTextBox1.About = null;
            this.DbTextBox1.AcceptsReturn = false;
            this.DbTextBox1.Appearance = null;
            this.DbTextBox1.AsociatedCombo = null;
            this.DbTextBox1.AsociatedDBFindTextBox = null;
            this.DbTextBox1.BackColorRead = System.Drawing.Color.WhiteSmoke;
            this.DbTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DbTextBox1.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
            this.DbTextBox1.DataControl = null;
            this.DbTextBox1.DataType = FSFormControls.DBTextBox.TypeData.All;
            this.DbTextBox1.DateFormat = "dd/mm/yyyy";
            this.DbTextBox1.DBField = null;
            this.DbTextBox1.DBFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DbTextBox1.Decimals = 2;
            this.DbTextBox1.DefaultValue = null;
            this.DbTextBox1.DotNumber = false;
            this.DbTextBox1.Editable = true;
            this.DbTextBox1.EditAs = FSFormControls.DBTextBox.EditAsType.UseSpecifiedMask;
            this.DbTextBox1.Encrypted = false;
            this.DbTextBox1.Expression = "";
            this.DbTextBox1.FireTextChanged = true;
            this.DbTextBox1.FormatString = "";
            this.DbTextBox1.GridOperation = FSFormControls.DBColumn.OperationTypes.Sum;
            this.DbTextBox1.InputMask = "";
            this.DbTextBox1.Location = new System.Drawing.Point(184, 112);
            this.DbTextBox1.MaskInput = "";
            this.DbTextBox1.MaxLength = 32767;
            this.DbTextBox1.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbTextBox1.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbTextBox1.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
            this.DbTextBox1.Multiline = false;
            this.DbTextBox1.Name = "DbTextBox1";
            this.DbTextBox1.NonAutoSizeHeight = 0;
            this.DbTextBox1.NumericType = FSFormControls.DBTextBox.NumericTypeEnum.Double;
            this.DbTextBox1.Obligatory = false;
            this.DbTextBox1.PasswordChar = '\0';
            this.DbTextBox1.PromptChar = '\0';
            this.DbTextBox1.ReadOnly = false;
            this.DbTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DbTextBox1.SelectAllBehavior = FSFormControls.DBTextBox.SelectAllBehaviorEnum.SelectAllCharacters;
            this.DbTextBox1.SelectionLength = 0;
            this.DbTextBox1.SelectionStart = 0;
            this.DbTextBox1.SendCommaAsPoint = true;
            this.DbTextBox1.SendTabAsEnter = true;
            this.DbTextBox1.Shadow = false;
            this.DbTextBox1.ShadowColor = System.Drawing.Color.Gray;
            this.DbTextBox1.ShadowSize = 4;
            this.DbTextBox1.ShowAsCombo = false;
            this.DbTextBox1.ShowKeyboard = false;
            this.DbTextBox1.ShowScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DbTextBox1.Size = new System.Drawing.Size(112, 20);
            this.DbTextBox1.TabIndex = 3;
            this.DbTextBox1.TabNavigation = FSFormControls.DBTextBox.TabNavigationEnum.NextControl;
            this.DbTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbTextBox1.ToolTip = "";
            this.DbTextBox1.Track = false;
            this.DbTextBox1.Value = "";
            this.DbTextBox1.XMLName = null;
            // 
            // DbTextBox2
            // 
            this.DbTextBox2.About = null;
            this.DbTextBox2.AcceptsReturn = false;
            this.DbTextBox2.Appearance = null;
            this.DbTextBox2.AsociatedCombo = null;
            this.DbTextBox2.AsociatedDBFindTextBox = null;
            this.DbTextBox2.BackColorRead = System.Drawing.Color.WhiteSmoke;
            this.DbTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DbTextBox2.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
            this.DbTextBox2.DataControl = null;
            this.DbTextBox2.DataType = FSFormControls.DBTextBox.TypeData.All;
            this.DbTextBox2.DateFormat = "dd/mm/yyyy";
            this.DbTextBox2.DBField = null;
            this.DbTextBox2.DBFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DbTextBox2.Decimals = 2;
            this.DbTextBox2.DefaultValue = null;
            this.DbTextBox2.DotNumber = false;
            this.DbTextBox2.Editable = true;
            this.DbTextBox2.EditAs = FSFormControls.DBTextBox.EditAsType.UseSpecifiedMask;
            this.DbTextBox2.Encrypted = false;
            this.DbTextBox2.Expression = "";
            this.DbTextBox2.FireTextChanged = true;
            this.DbTextBox2.FormatString = "";
            this.DbTextBox2.GridOperation = FSFormControls.DBColumn.OperationTypes.Sum;
            this.DbTextBox2.InputMask = "";
            this.DbTextBox2.Location = new System.Drawing.Point(184, 144);
            this.DbTextBox2.MaskInput = "";
            this.DbTextBox2.MaxLength = 32767;
            this.DbTextBox2.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbTextBox2.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbTextBox2.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
            this.DbTextBox2.Multiline = false;
            this.DbTextBox2.Name = "DbTextBox2";
            this.DbTextBox2.NonAutoSizeHeight = 0;
            this.DbTextBox2.NumericType = FSFormControls.DBTextBox.NumericTypeEnum.Double;
            this.DbTextBox2.Obligatory = false;
            this.DbTextBox2.PasswordChar = '*';
            this.DbTextBox2.PromptChar = '\0';
            this.DbTextBox2.ReadOnly = false;
            this.DbTextBox2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DbTextBox2.SelectAllBehavior = FSFormControls.DBTextBox.SelectAllBehaviorEnum.SelectAllCharacters;
            this.DbTextBox2.SelectionLength = 0;
            this.DbTextBox2.SelectionStart = 0;
            this.DbTextBox2.SendCommaAsPoint = true;
            this.DbTextBox2.SendTabAsEnter = true;
            this.DbTextBox2.Shadow = false;
            this.DbTextBox2.ShadowColor = System.Drawing.Color.Gray;
            this.DbTextBox2.ShadowSize = 4;
            this.DbTextBox2.ShowAsCombo = false;
            this.DbTextBox2.ShowKeyboard = false;
            this.DbTextBox2.ShowScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DbTextBox2.Size = new System.Drawing.Size(112, 20);
            this.DbTextBox2.TabIndex = 4;
            this.DbTextBox2.TabNavigation = FSFormControls.DBTextBox.TabNavigationEnum.NextControl;
            this.DbTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbTextBox2.ToolTip = "";
            this.DbTextBox2.Track = false;
            this.DbTextBox2.Value = "";
            this.DbTextBox2.XMLName = null;
            // 
            // DbLabel3
            // 
            this.DbLabel3.About = null;
            this.DbLabel3.Angle = 0F;
            this.DbLabel3.Appearance = null;
            this.DbLabel3.BackColor = System.Drawing.Color.White;
            this.DbLabel3.BorderStyleInner = System.Windows.Forms.BorderStyle.None;
            this.DbLabel3.BorderStyleOuter = System.Windows.Forms.BorderStyle.None;
            this.DbLabel3.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
            this.DbLabel3.DataControl = null;
            this.DbLabel3.DataType = FSFormControls.DBTextBox.TypeData.All;
            this.DbLabel3.DateFormat = "dd/MM/yyyy";
            this.DbLabel3.DBField = null;
            this.DbLabel3.Decimals = 2;
            this.DbLabel3.EndColor = System.Drawing.Color.LightSkyBlue;
            this.DbLabel3.Location = new System.Drawing.Point(96, 24);
            this.DbLabel3.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbLabel3.Name = "DbLabel3";
            this.DbLabel3.ShadowColor = System.Drawing.Color.Black;
            this.DbLabel3.Size = new System.Drawing.Size(295, 47);
            this.DbLabel3.StartColor = System.Drawing.Color.White;
            this.DbLabel3.TabIndex = 5;
            this.DbLabel3.TabStop = false;
            this.DbLabel3.Text = "Introduzca su información personalizada de acceso. Si desconoce esta información," +
    " póngase en contacto con su administrador.";
            this.DbLabel3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.DbLabel3.Track = false;
            this.DbLabel3.XOffset = 1F;
            this.DbLabel3.YOffset = 1F;
            // 
            // DbControl1
            // 
            this.DbControl1.About = null;
            this.DbControl1.ArrayList = null;
            this.DbControl1.AutoConnect = true;
            this.DbControl1.DataControl = null;
            this.DbControl1.DataSet = null;
            this.DbControl1.DataTable = null;
            this.DbControl1.DataView = null;
            this.DbControl1.DBField = null;
            this.DbControl1.DBFieldData = "";
            this.DbControl1.DBPosition = 0;
            this.DbControl1.EraseDBControl = null;
            this.DbControl1.Filter = "";
            this.DbControl1.isEOF = true;
            this.DbControl1.Location = new System.Drawing.Point(319, 103);
            this.DbControl1.LOCK = null;
            this.DbControl1.LOPD = null;
            this.DbControl1.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbControl1.Name = "DbControl1";
            this.DbControl1.Page = 0;
            this.DbControl1.PageSettings = null;
            this.DbControl1.Paging = false;
            this.DbControl1.PagingSize = 0;
            this.DbControl1.ReadOnly = false;
            this.DbControl1.RelationDataControl = null;
            this.DbControl1.RelationDBField = "";
            this.DbControl1.RelationParentDBField = "";
            this.DbControl1.SaveError = false;
            this.DbControl1.SaveOnChangeRecord = false;
            this.DbControl1.Selection = "select * from usuarios where usuario=\'?1\' and clave=\'?2\'";
            this.DbControl1.Size = new System.Drawing.Size(72, 64);
            this.DbControl1.StoreInBase64Format = false;
            this.DbControl1.TabIndex = 6;
            this.DbControl1.TableName = "usuarios";
            this.DbControl1.TabStop = false;
            this.DbControl1.Text = "SQL: select * from usuarios where usuario=\'?1\' and clave=\'?2\'";
            this.DbControl1.Track = false;
            this.DbControl1.TypeDB = FSFormControls.DBControl.DbType.OleDB;
            this.DbControl1.Versionable = false;
            this.DbControl1.VersionableDateField = "";
            this.DbControl1.VersionableTable = "";
            this.DbControl1.VersionableUserField = "";
            this.DbControl1.VersionableVersionField = "";
            this.DbControl1.Visible = false;
            this.DbControl1.XmlFile = "";
            this.DbControl1.XMLName = "";
            // 
            // PictureBox2
            // 
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(32, 16);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(57, 55);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PictureBox2.TabIndex = 9;
            this.PictureBox2.TabStop = false;
            // 
            // DbPicture2
            // 
            this.DbPicture2.About = null;
            this.DbPicture2.BorderSize = 1;
            this.DbPicture2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DbPicture2.Color = System.Drawing.Color.White;
            this.DbPicture2.DataControl = null;
            this.DbPicture2.DBField = null;
            this.DbPicture2.Dock = System.Windows.Forms.DockStyle.Top;
            this.DbPicture2.Enabled = false;
            this.DbPicture2.Location = new System.Drawing.Point(0, 0);
            this.DbPicture2.Name = "DbPicture2";
            this.DbPicture2.PictureType = FSFormControls.DBPicture.t_PictureType.Square;
            this.DbPicture2.Size = new System.Drawing.Size(439, 80);
            this.DbPicture2.TabIndex = 7;
            this.DbPicture2.TabStop = false;
            this.DbPicture2.Track = false;
            // 
            // frmLogin
            // 
            this.AutomaticConnect = false;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(439, 223);
            this.Controls.Add(this.PictureBox2);
            this.Controls.Add(this.DbControl1);
            this.Controls.Add(this.DbLabel3);
            this.Controls.Add(this.DbTextBox2);
            this.Controls.Add(this.DbTextBox1);
            this.Controls.Add(this.DbLabel2);
            this.Controls.Add(this.DbLabel1);
            this.Controls.Add(this.DbButton1);
            this.Controls.Add(this.DbPicture2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.ShowMenu = false;
            this.ShowStatusBar = false;
            this.ShowToolBar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Validación";
            this.Activated += new System.EventHandler(this.login_Activated);
            this.Load += new System.EventHandler(this.login_Load);
            this.Controls.SetChildIndex(this.DbToolBar1, 0);
            this.Controls.SetChildIndex(this.DbStatusBar1, 0);
            this.Controls.SetChildIndex(this.DbPicture2, 0);
            this.Controls.SetChildIndex(this.DbButton1, 0);
            this.Controls.SetChildIndex(this.DbLabel1, 0);
            this.Controls.SetChildIndex(this.DbLabel2, 0);
            this.Controls.SetChildIndex(this.DbTextBox1, 0);
            this.Controls.SetChildIndex(this.DbTextBox2, 0);
            this.Controls.SetChildIndex(this.DbLabel3, 0);
            this.Controls.SetChildIndex(this.DbControl1, 0);
            this.Controls.SetChildIndex(this.PictureBox2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.DbStatusBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbStatusBarPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbStatusBarProgressPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbSBarPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbSBarPanel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbSBarPanel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbTextBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbTextBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		
#endregion
		
		string s;
		
		private void DbButton1_Click(System.Object sender, System.EventArgs e)
		{
            string d = s.Replace("?1", this.DbTextBox1.Text);
			this.DbControl1.Selection = d.Replace("?2", this.DbTextBox2.Text);
			
			//this.DbControl1.DBConnection = FSFormControls.Global.DBconnection;
			this.DbControl1.Connect(true);
			
			if (this.DbControl1.isEOF)
			{
                MessageBox.Show("Usuario incorrecto!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				//this.DbControl1.CloseConnection();
				this.DbTextBox1.Focus();
			}
			else
			{
                FSFormControls.Global.UserName = this.DbControl1.GetField("nombre").ToString();
				Global.tipoUsuario = int.Parse(this.DbControl1.GetField("tipoUsuario").ToString());
				
				((mdiPrincipal) Global.mdiP).lblUltimaConexion.Text = this.DbControl1.GetField("ultimaConexion").ToString();
				((mdiPrincipal)Global.mdiP).lblUsuario.Text = FSFormControls.Global.UserName;
				
				this.DbControl1.SetField("ultimaConexion", DateTime.Now.ToString());
				this.DbControl1.Save();
				
				if (Global.tipoUsuario == 0)
				{
					((mdiPrincipal) Global.mdiP).ToolBar1.Buttons[2].Enabled = true;
				}
				else
				{
					((mdiPrincipal) Global.mdiP).ToolBar1.Buttons[2].Enabled = false;
				}
				
				if (frmDest != null)
				{
					//frmDest.DBConnection = FSFormControls.Global.DBconnection;
					frmDest.Show();
				}
				this.Close();
			}
		}
		
		private void login_Load(System.Object sender, System.EventArgs e)
		{
			s = this.DbControl1.Selection;
		}
		
		
		private void login_Activated(object sender, System.EventArgs e)
		{
			this.DbTextBox1.Focus();
		}
	}
	
}
