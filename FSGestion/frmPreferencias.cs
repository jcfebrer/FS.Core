
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;



namespace FSGestion
{
	public class frmPreferencias : FSFormControls.DBForm
	{
		
#region  Código generado por el Diseñador de Windows Forms
		
		public frmPreferencias()
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
		internal FSFormControls.DBLabel DbLabel1;
		internal FSFormControls.DBTextBox DbTextBox1;
		internal FSFormControls.DBLabel DbLabel2;
		internal FSFormControls.DBCombo DbCombo1;
		internal FSFormControls.DBControl DbControl1;
		internal FSFormControls.DBButton DbButton1;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.DbLabel1 = new FSFormControls.DBLabel();
			this.DbTextBox1 = new FSFormControls.DBTextBox();
			this.DbLabel2 = new FSFormControls.DBLabel();
			this.DbCombo1 = new FSFormControls.DBCombo();
			this.DbControl1 = new FSFormControls.DBControl();
			this.DbButton1 = new FSFormControls.DBButton();
			this.SuspendLayout();
			//
			//DbLabel1
			//
			this.DbLabel1.About = null;
			this.DbLabel1.AutoSize = true;
			this.DbLabel1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.DbLabel1.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
			this.DbLabel1.DataControl = null;
			this.DbLabel1.DataType = FSFormControls.DBTextBox.TypeData.All;
			this.DbLabel1.DateFormat = "dd/MM/yyyy";
			this.DbLabel1.Decimals = 2;
			this.DbLabel1.Location = new System.Drawing.Point(40, 56);
			this.DbLabel1.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbLabel1.Name = "DbLabel1";
			this.DbLabel1.Size = new System.Drawing.Size(55, 16);
			this.DbLabel1.TabIndex = 0;
			this.DbLabel1.TabStop = false;
			this.DbLabel1.Text = "Conexión:";
			this.DbLabel1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.DbLabel1.Track = false;
			//
			//DbTextBox1
			//
			this.DbTextBox1.About = null;
			this.DbTextBox1.AsociatedCombo = null;
			this.DbTextBox1.AsociatedDBFindTextBox = null;
			this.DbTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.DbTextBox1.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
			this.DbTextBox1.DataControl = null;
			this.DbTextBox1.DataType = FSFormControls.DBTextBox.TypeData.All;
			this.DbTextBox1.DateFormat = "dd/MM/yyyy";
			this.DbTextBox1.DBField = null;
			this.DbTextBox1.DBFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.DbTextBox1.Decimals = 2;
			this.DbTextBox1.DefaultValue = "";
			this.DbTextBox1.DotNumber = false;
			this.DbTextBox1.Editable = true;
			this.DbTextBox1.Encrypted = false;
			this.DbTextBox1.Expression = "";
			this.DbTextBox1.FormatString = "";
			this.DbTextBox1.GridOperation = FSFormControls.DBColumn.OperationTypes.Sum;
			this.DbTextBox1.Location = new System.Drawing.Point(120, 56);
			this.DbTextBox1.MaskInput = null;
			this.DbTextBox1.MaxLength = (int) ((long) (32767));
			this.DbTextBox1.MaxValue = decimal.MaxValue;
			this.DbTextBox1.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
			this.DbTextBox1.Multiline = false;
			this.DbTextBox1.Name = "DbTextBox1";
			this.DbTextBox1.Obligatory = false;
			this.DbTextBox1.PasswordChar = (char)0;
            this.DbTextBox1.ReadOnly = false;
			this.DbTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.DbTextBox1.Shadow = false;
			this.DbTextBox1.ShadowColor = System.Drawing.Color.Gray;
			this.DbTextBox1.ShadowSize = 4;
			this.DbTextBox1.ShowKeyboard = false;
			this.DbTextBox1.Size = new System.Drawing.Size(264, 20);
			this.DbTextBox1.TabIndex = 1;
			this.DbTextBox1.Text = "DbTextBox1";
			this.DbTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbTextBox1.ToolTip = "";
			this.DbTextBox1.Track = false;
			this.DbTextBox1.XMLName = null;
			//
			//DbLabel2
			//
			this.DbLabel2.About = null;
			this.DbLabel2.AutoSize = true;
			this.DbLabel2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.DbLabel2.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
			this.DbLabel2.DataControl = null;
			this.DbLabel2.DataType = FSFormControls.DBTextBox.TypeData.All;
			this.DbLabel2.DateFormat = "dd/MM/yyyy";
			this.DbLabel2.Decimals = 2;
			this.DbLabel2.Location = new System.Drawing.Point(40, 88);
			this.DbLabel2.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbLabel2.Name = "DbLabel2";
			this.DbLabel2.Size = new System.Drawing.Size(53, 16);
			this.DbLabel2.TabIndex = 2;
			this.DbLabel2.TabStop = false;
			this.DbLabel2.Text = "Empresa:";
			this.DbLabel2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.DbLabel2.Track = false;
			//
			//DbCombo1
			//
			this.DbCombo1.About = null;
			this.DbCombo1.BlankSelection = false;
			this.DbCombo1.DataControl = null;
			this.DbCombo1.DataControlList = this.DbControl1;
			this.DbCombo1.DBField = null;
			this.DbCombo1.DBFieldData = "idEmpresa";
			this.DbCombo1.DBFieldList = "Nombre";
			this.DbCombo1.DisplayMember = "";
			this.DbCombo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DbCombo1.DroppedDown = false;
			this.DbCombo1.Editable = true;
			this.DbCombo1.Location = new System.Drawing.Point(120, 88);
			this.DbCombo1.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
			this.DbCombo1.Name = "DbCombo1";
			this.DbCombo1.Obligatory = false;
			this.DbCombo1.OrderBy = null;
			this.DbCombo1.SelectedIndex = -1;
			this.DbCombo1.SelectedOption = null;
			this.DbCombo1.SelectedValue = null;
			this.DbCombo1.ShowCode = false;
			this.DbCombo1.Size = new System.Drawing.Size(264, 22);
			this.DbCombo1.Sort = true;
			this.DbCombo1.TabIndex = 3;
			this.DbCombo1.Track = false;
			this.DbCombo1.ValueMember = "";
			//
			//DbControl1
			//
			this.DbControl1.About = null;
			this.DbControl1.AutoConnect = true;
			this.DbControl1.DataTable = null;
			//this.DbControl1.DBConnection = null;
			this.DbControl1.DBFieldData = "";
			this.DbControl1.DBPosition = 0;
			this.DbControl1.EraseDBControl = null;
			this.DbControl1.Filter = "";
			this.DbControl1.isEOF = true;
			this.DbControl1.Location = new System.Drawing.Point(248, 152);
			this.DbControl1.LOCK = null;
			this.DbControl1.LOPD = null;
			this.DbControl1.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbControl1.Name = "DbControl1";
			this.DbControl1.Page = 0;
			this.DbControl1.PageSettings = null;
			this.DbControl1.Paging = false;
			this.DbControl1.PagingSize = 0;
			this.DbControl1.RelationDataControl = null;
			this.DbControl1.RelationDBField = "";
			this.DbControl1.RelationParentDBField = "";
			this.DbControl1.SaveError = false;
			this.DbControl1.SaveOnChangeRecord = false;
			this.DbControl1.Selection = "select * from empresas";
			this.DbControl1.Size = new System.Drawing.Size(80, 48);
			this.DbControl1.TabIndex = 4;
			this.DbControl1.TableName = "empresas";
			this.DbControl1.TabStop = false;
			this.DbControl1.Track = false;
			this.DbControl1.TypeDB = FSFormControls.DBControl.DbType.SQLServer;
			this.DbControl1.Versionable = false;
			this.DbControl1.VersionableDateField = "";
			this.DbControl1.VersionableTable = "";
			this.DbControl1.VersionableUserField = "";
			this.DbControl1.VersionableVersionField = "";
			this.DbControl1.Visible = false;
			this.DbControl1.XmlFile = "";
			this.DbControl1.XMLName = "";
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
			this.DbButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.DbButton1.Location = new System.Drawing.Point(160, 208);
			this.DbButton1.Name = "DbButton1";
			this.DbButton1.Size = new System.Drawing.Size(80, 24);
			this.DbButton1.TabIndex = 5;
			this.DbButton1.Text = "Guardar";
			this.DbButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.DbButton1.TextColorEnd = System.Drawing.Color.Black;
			this.DbButton1.TextColorStart = System.Drawing.Color.Blue;
			this.DbButton1.TextFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.DbButton1.ToolTip = "";
			this.DbButton1.Track = false;
			//
			//frmPreferencias
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(432, 273);
			this.Controls.Add(this.DbCombo1);
			this.Controls.Add(this.DbButton1);
			this.Controls.Add(this.DbControl1);
			this.Controls.Add(this.DbLabel2);
			this.Controls.Add(this.DbTextBox1);
			this.Controls.Add(this.DbLabel1);
			this.Name = "frmPreferencias";
			this.ShowMenu = false;
			this.ShowStatusBar = false;
			this.ShowToolBar = false;
			this.Text = "Preferencias";
			this.Controls.SetChildIndex(this.DbLabel1, 0);
			this.Controls.SetChildIndex(this.DbTextBox1, 0);
			this.Controls.SetChildIndex(this.DbLabel2, 0);
			this.Controls.SetChildIndex(this.DbControl1, 0);
			this.Controls.SetChildIndex(this.DbButton1, 0);
			this.Controls.SetChildIndex(this.DbCombo1, 0);
			this.ResumeLayout(false);
			
		}
		
#endregion
		
	}
	
}
