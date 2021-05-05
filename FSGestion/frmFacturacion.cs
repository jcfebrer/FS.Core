
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;



namespace FSGestion
{
	public class frmFacturacion : FSFormControls.DBForm
	{
		
#region  Código generado por el Diseñador de Windows Forms
		
		public frmFacturacion()
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
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal FSFormControls.DBGrid DbGrid1;
		internal FSFormControls.DBControl DbControl1;
		internal FSFormControls.DBLabel DbLabel1;
		internal FSFormControls.DBLabel DbLabel2;
		internal FSFormControls.DBButton dbbLocalizar;
		internal FSFormControls.DBColumn DbColumn1;
		internal FSFormControls.DBColumn DbColumn2;
		internal FSFormControls.DBColumn DbColumn4;
		internal FSFormControls.DBButton dbbTodas;
		internal FSFormControls.DBTextBox txtNombre;
		internal FSFormControls.DBControl DbControl4;
		internal FSFormControls.DBControl DbControl5;
		internal FSFormControls.DBCombo dbcClientes;
		internal FSFormControls.DBColumn DbColumn10;
		internal FSFormControls.DBTextBox txtContrato;
		internal FSFormControls.DBLabel DbLabel3;
		internal FSFormControls.DBLabel DbLabel4;
		internal FSFormControls.DBDate DbDate1;
		internal FSFormControls.DBDate DbDate2;
		internal System.Windows.Forms.Button cmdImprimir;
		internal System.Windows.Forms.Button cmdGenerar;
		internal FSFormControls.DBColumn DbColumn3;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			base.Load += new System.EventHandler(frmHotSpots_Load);
			this.DbDate2 = new FSFormControls.DBDate();
			this.DbDate1 = new FSFormControls.DBDate();
			this.DbLabel4 = new FSFormControls.DBLabel();
			this.txtContrato = new FSFormControls.DBTextBox();
			this.DbLabel3 = new FSFormControls.DBLabel();
			this.dbcClientes = new FSFormControls.DBCombo();
			this.DbControl4 = new FSFormControls.DBControl();
			this.dbbTodas = new FSFormControls.DBButton();
			this.dbbTodas.Click += new System.EventHandler(this.dbbTodas_Click);
			this.dbbLocalizar = new FSFormControls.DBButton();
			this.dbbLocalizar.Click += new System.EventHandler(this.dbbLocalizar_Click);
			this.DbLabel2 = new FSFormControls.DBLabel();
			this.txtNombre = new FSFormControls.DBTextBox();
			this.DbLabel1 = new FSFormControls.DBLabel();
			this.DbControl5 = new FSFormControls.DBControl();
			this.DbGrid1 = new FSFormControls.DBGrid();
			this.DbGrid1.DoubleClick += new FSFormControls.DBGrid.DoubleClickEventHandler(this.DbGrid1_DoubleClick);
			this.DbColumn10 = new FSFormControls.DBColumn();
			this.DbColumn3 = new FSFormControls.DBColumn();
			this.DbColumn1 = new FSFormControls.DBColumn();
			this.DbColumn2 = new FSFormControls.DBColumn();
			this.DbColumn4 = new FSFormControls.DBColumn();
			this.DbControl1 = new FSFormControls.DBControl();
			this.cmdImprimir = new System.Windows.Forms.Button();
			this.cmdImprimir.Click += new System.EventHandler(this.cmdImprimir_Click);
			this.cmdGenerar = new System.Windows.Forms.Button();
			this.cmdGenerar.Click += new System.EventHandler(this.cmdGenerar_Click);
			this.GroupBox1.SuspendLayout();
			this.SuspendLayout();
			//
			//mnuForm
			//
			this.mnuForm.OwnerDraw = true;
			//
			//GroupBox1
			//
			this.GroupBox1.Controls.Add(this.DbDate2);
			this.GroupBox1.Controls.Add(this.DbDate1);
			this.GroupBox1.Controls.Add(this.DbLabel4);
			this.GroupBox1.Controls.Add(this.txtContrato);
			this.GroupBox1.Controls.Add(this.DbLabel3);
			this.GroupBox1.Controls.Add(this.dbcClientes);
			this.GroupBox1.Controls.Add(this.dbbTodas);
			this.GroupBox1.Controls.Add(this.dbbLocalizar);
			this.GroupBox1.Controls.Add(this.DbLabel2);
			this.GroupBox1.Controls.Add(this.txtNombre);
			this.GroupBox1.Controls.Add(this.DbLabel1);
			this.GroupBox1.Location = new System.Drawing.Point(16, 64);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(724, 80);
			this.GroupBox1.TabIndex = 2;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "Selección de Facturas";
			//
			//DbDate2
			//
			this.DbDate2.About = null;
			this.DbDate2.AllowNullValue = true;
			this.DbDate2.CustomFormat = "dd/MM/yyyy";
			this.DbDate2.DataControl = null;
			this.DbDate2.DBField = null;
			this.DbDate2.Editable = true;
			this.DbDate2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.DbDate2.IsNull = false;
			this.DbDate2.Location = new System.Drawing.Point(480, 48);
			this.DbDate2.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
			this.DbDate2.Name = "DbDate2";
			this.DbDate2.Obligatory = false;
			this.DbDate2.Size = new System.Drawing.Size(92, 20);
			this.DbDate2.TabIndex = 13;
			this.DbDate2.Text = "06/07/2008";
			this.DbDate2.Track = false;
			//
			//DbDate1
			//
			this.DbDate1.About = null;
			this.DbDate1.AllowNullValue = true;
			this.DbDate1.CustomFormat = "dd/MM/yyyy";
			this.DbDate1.DataControl = null;
			this.DbDate1.DBField = null;
			this.DbDate1.Editable = true;
			this.DbDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.DbDate1.IsNull = false;
			this.DbDate1.Location = new System.Drawing.Point(388, 48);
			this.DbDate1.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
			this.DbDate1.Name = "DbDate1";
			this.DbDate1.Obligatory = false;
			this.DbDate1.Size = new System.Drawing.Size(92, 20);
			this.DbDate1.TabIndex = 12;
			this.DbDate1.Text = "06/07/2008";
			this.DbDate1.Track = false;
			//
			//DbLabel4
			//
			this.DbLabel4.About = null;
			this.DbLabel4.Angle = (float) (0.0F);
			this.DbLabel4.BackColor = System.Drawing.Color.Transparent;
			this.DbLabel4.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
			this.DbLabel4.DataControl = null;
			this.DbLabel4.DataType = FSFormControls.DBTextBox.TypeData.All;
			this.DbLabel4.DateFormat = "dd/MM/yyyy";
			this.DbLabel4.Decimals = 2;
			this.DbLabel4.EndColor = System.Drawing.Color.LightSkyBlue;
			this.DbLabel4.Location = new System.Drawing.Point(288, 52);
			this.DbLabel4.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbLabel4.Name = "DbLabel4";
			this.DbLabel4.ShadowColor = System.Drawing.Color.Black;
			this.DbLabel4.Size = new System.Drawing.Size(96, 16);
			this.DbLabel4.StartColor = System.Drawing.Color.White;
			this.DbLabel4.TabIndex = 11;
			this.DbLabel4.TabStop = false;
			this.DbLabel4.Text = "Intervalo Fechas:";
			this.DbLabel4.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.DbLabel4.Track = false;
			this.DbLabel4.XOffset = (float) (1.0F);
			this.DbLabel4.YOffset = (float) (1.0F);
			//
			//txtContrato
			//
			this.txtContrato.About = null;
			this.txtContrato.AsociatedCombo = null;
			this.txtContrato.AsociatedDBFindTextBox = null;
			this.txtContrato.BackColorRead = System.Drawing.Color.WhiteSmoke;
			this.txtContrato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.txtContrato.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
			this.txtContrato.DataControl = null;
			this.txtContrato.DataType = FSFormControls.DBTextBox.TypeData.All;
			this.txtContrato.DateFormat = "dd/MM/yyyy";
			this.txtContrato.DBField = null;
			this.txtContrato.DBFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.txtContrato.Decimals = 2;
			this.txtContrato.DefaultValue = null;
			this.txtContrato.DotNumber = false;
			this.txtContrato.Editable = true;
			this.txtContrato.Encrypted = false;
			this.txtContrato.Expression = "";
			this.txtContrato.FormatString = "";
			this.txtContrato.GridOperation = FSFormControls.DBColumn.OperationTypes.Sum;
			this.txtContrato.Location = new System.Drawing.Point(128, 24);
			this.txtContrato.MaskInput = null;
			this.txtContrato.MaxLength = 32767;
			this.txtContrato.MaxValue = decimal.MaxValue;
			this.txtContrato.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
			this.txtContrato.Multiline = false;
			this.txtContrato.Name = "txtContrato";
			this.txtContrato.Obligatory = false;
			this.txtContrato.PasswordChar = (char)0;
			this.txtContrato.ReadOnly = false;
			this.txtContrato.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.txtContrato.Shadow = false;
			this.txtContrato.ShadowColor = System.Drawing.Color.Gray;
			this.txtContrato.ShadowSize = 4;
			this.txtContrato.ShowAsCombo = false;
			this.txtContrato.ShowKeyboard = false;
			this.txtContrato.Size = new System.Drawing.Size(144, 20);
			this.txtContrato.TabIndex = 10;
			this.txtContrato.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtContrato.ToolTip = "";
			this.txtContrato.Track = false;
			this.txtContrato.XMLName = null;
			//
			//DbLabel3
			//
			this.DbLabel3.About = null;
			this.DbLabel3.Angle = (float) (0.0F);
			this.DbLabel3.BackColor = System.Drawing.Color.Transparent;
			this.DbLabel3.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
			this.DbLabel3.DataControl = null;
			this.DbLabel3.DataType = FSFormControls.DBTextBox.TypeData.All;
			this.DbLabel3.DateFormat = "dd/MM/yyyy";
			this.DbLabel3.Decimals = 2;
			this.DbLabel3.EndColor = System.Drawing.Color.LightSkyBlue;
			this.DbLabel3.Location = new System.Drawing.Point(288, 28);
			this.DbLabel3.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbLabel3.Name = "DbLabel3";
			this.DbLabel3.ShadowColor = System.Drawing.Color.Black;
			this.DbLabel3.Size = new System.Drawing.Size(44, 16);
			this.DbLabel3.StartColor = System.Drawing.Color.White;
			this.DbLabel3.TabIndex = 8;
			this.DbLabel3.TabStop = false;
			this.DbLabel3.Text = "Cliente:";
			this.DbLabel3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.DbLabel3.Track = false;
			this.DbLabel3.XOffset = (float) (1.0F);
			this.DbLabel3.YOffset = (float) (1.0F);
			//
			//dbcClientes
			//
			this.dbcClientes.About = null;
			this.dbcClientes.BlankSelection = true;
			this.dbcClientes.DataControl = null;
			this.dbcClientes.DataControlList = this.DbControl4;
			this.dbcClientes.DBField = null;
			this.dbcClientes.DBFieldData = "idCliente";
			this.dbcClientes.DBFieldList = "nombre";
			this.dbcClientes.DisplayMember = "";
			this.dbcClientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.dbcClientes.DroppedDown = false;
			this.dbcClientes.Editable = true;
			this.dbcClientes.Location = new System.Drawing.Point(336, 24);
			this.dbcClientes.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
			this.dbcClientes.Name = "dbcClientes";
			this.dbcClientes.Obligatory = false;
			this.dbcClientes.OrderBy = null;
			this.dbcClientes.SelectedIndex = -1;
			this.dbcClientes.SelectedItem = null;
			this.dbcClientes.SelectedOption = null;
			this.dbcClientes.SelectedValue = null;
			this.dbcClientes.ShowCode = false;
			this.dbcClientes.ShowEdit = false;
			this.dbcClientes.Size = new System.Drawing.Size(236, 21);
			this.dbcClientes.Sort = true;
			this.dbcClientes.TabIndex = 7;
			this.dbcClientes.Track = false;
			this.dbcClientes.ValueMember = "";
			//
			//DbControl4
			//
			this.DbControl4.About = null;
			this.DbControl4.AutoConnect = true;
			this.DbControl4.DataControl = null;
			this.DbControl4.DataTable = null;
			//this.DbControl4.DBConnection = null;
			this.DbControl4.DBFieldData = "";
			this.DbControl4.DBPosition = 0;
			this.DbControl4.EraseDBControl = null;
			this.DbControl4.Filter = "";
			this.DbControl4.isEOF = true;
			this.DbControl4.Location = new System.Drawing.Point(680, 72);
			this.DbControl4.LOCK = null;
			this.DbControl4.LOPD = null;
			this.DbControl4.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbControl4.Name = "DbControl4";
			this.DbControl4.Page = 0;
			this.DbControl4.PageSettings = null;
			this.DbControl4.Paging = false;
			this.DbControl4.PagingSize = 0;
			this.DbControl4.ReadOnly = false;
			this.DbControl4.RelationDataControl = null;
			this.DbControl4.RelationDBField = "";
			this.DbControl4.RelationParentDBField = "";
			this.DbControl4.SaveError = false;
			this.DbControl4.SaveOnChangeRecord = false;
			this.DbControl4.Selection = "select * from clientes order by nombre";
			this.DbControl4.Size = new System.Drawing.Size(72, 48);
			this.DbControl4.TabIndex = 6;
			this.DbControl4.TableName = "clientes";
			this.DbControl4.TabStop = false;
			this.DbControl4.Text = "SQL: select * from clientes order by nombre";
			this.DbControl4.Track = false;
			this.DbControl4.TypeDB = FSFormControls.DBControl.DbType.SQLServer;
			this.DbControl4.Versionable = false;
			this.DbControl4.VersionableDateField = "";
			this.DbControl4.VersionableTable = "";
			this.DbControl4.VersionableUserField = "";
			this.DbControl4.VersionableVersionField = "";
			this.DbControl4.Visible = false;
			this.DbControl4.XmlFile = "";
			this.DbControl4.XMLName = "";
			//
			//dbbTodas
			//
			this.dbbTodas.About = null;
			this.dbbTodas.ButtonStyle = FSFormControls.DBButton.ButtonStyleType.Normal;
			this.dbbTodas.DataControl = null;
			this.dbbTodas.DropDownMenu = null;
			this.dbbTodas.FillColorEnd = System.Drawing.Color.White;
			this.dbbTodas.FillColorStart = System.Drawing.Color.LightGray;
			this.dbbTodas.FillHoverColorEnd = System.Drawing.Color.Beige;
			this.dbbTodas.FillHoverColorStart = System.Drawing.Color.Beige;
			this.dbbTodas.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.dbbTodas.Gradient = false;
			this.dbbTodas.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			this.dbbTodas.Image = null;
			this.dbbTodas.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.dbbTodas.Location = new System.Drawing.Point(584, 16);
			this.dbbTodas.Name = "dbbTodas";
			this.dbbTodas.Size = new System.Drawing.Size(120, 24);
			this.dbbTodas.TabIndex = 6;
			this.dbbTodas.Text = "Mostrar todos";
			this.dbbTodas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.dbbTodas.TextColorEnd = System.Drawing.Color.Black;
			this.dbbTodas.TextColorStart = System.Drawing.Color.Blue;
			this.dbbTodas.TextFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.dbbTodas.ToolTip = "";
			this.dbbTodas.Track = false;
			//
			//dbbLocalizar
			//
			this.dbbLocalizar.About = null;
			this.dbbLocalizar.ButtonStyle = FSFormControls.DBButton.ButtonStyleType.Normal;
			this.dbbLocalizar.DataControl = null;
			this.dbbLocalizar.DropDownMenu = null;
			this.dbbLocalizar.FillColorEnd = System.Drawing.Color.White;
			this.dbbLocalizar.FillColorStart = System.Drawing.Color.LightGray;
			this.dbbLocalizar.FillHoverColorEnd = System.Drawing.Color.Beige;
			this.dbbLocalizar.FillHoverColorStart = System.Drawing.Color.Beige;
			this.dbbLocalizar.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.dbbLocalizar.Gradient = false;
			this.dbbLocalizar.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			this.dbbLocalizar.Image = null;
			this.dbbLocalizar.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.dbbLocalizar.Location = new System.Drawing.Point(584, 44);
			this.dbbLocalizar.Name = "dbbLocalizar";
			this.dbbLocalizar.Size = new System.Drawing.Size(120, 24);
			this.dbbLocalizar.TabIndex = 5;
			this.dbbLocalizar.Text = "Localizar";
			this.dbbLocalizar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.dbbLocalizar.TextColorEnd = System.Drawing.Color.Black;
			this.dbbLocalizar.TextColorStart = System.Drawing.Color.Blue;
			this.dbbLocalizar.TextFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.dbbLocalizar.ToolTip = "";
			this.dbbLocalizar.Track = false;
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
			this.DbLabel2.Location = new System.Drawing.Point(24, 48);
			this.DbLabel2.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbLabel2.Name = "DbLabel2";
			this.DbLabel2.ShadowColor = System.Drawing.Color.Black;
			this.DbLabel2.Size = new System.Drawing.Size(72, 16);
			this.DbLabel2.StartColor = System.Drawing.Color.White;
			this.DbLabel2.TabIndex = 4;
			this.DbLabel2.TabStop = false;
			this.DbLabel2.Text = "Nombre:";
			this.DbLabel2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.DbLabel2.Track = false;
			this.DbLabel2.XOffset = (float) (1.0F);
			this.DbLabel2.YOffset = (float) (1.0F);
			//
			//txtNombre
			//
			this.txtNombre.About = null;
			this.txtNombre.AsociatedCombo = null;
			this.txtNombre.AsociatedDBFindTextBox = null;
			this.txtNombre.BackColorRead = System.Drawing.Color.WhiteSmoke;
			this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.txtNombre.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
			this.txtNombre.DataControl = null;
			this.txtNombre.DataType = FSFormControls.DBTextBox.TypeData.All;
			this.txtNombre.DateFormat = "dd/MM/yyyy";
			this.txtNombre.DBField = null;
			this.txtNombre.DBFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.txtNombre.Decimals = 2;
			this.txtNombre.DefaultValue = null;
			this.txtNombre.DotNumber = false;
			this.txtNombre.Editable = true;
			this.txtNombre.Encrypted = false;
			this.txtNombre.Expression = "";
			this.txtNombre.FormatString = "";
			this.txtNombre.GridOperation = FSFormControls.DBColumn.OperationTypes.Sum;
			this.txtNombre.Location = new System.Drawing.Point(128, 48);
			this.txtNombre.MaskInput = null;
			this.txtNombre.MaxLength = 32767;
			this.txtNombre.MaxValue = decimal.MaxValue;
			this.txtNombre.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
			this.txtNombre.Multiline = false;
			this.txtNombre.Name = "txtNombre";
			this.txtNombre.Obligatory = false;
			this.txtNombre.PasswordChar = (char)0;
			this.txtNombre.ReadOnly = false;
			this.txtNombre.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.txtNombre.Shadow = false;
			this.txtNombre.ShadowColor = System.Drawing.Color.Gray;
			this.txtNombre.ShadowSize = 4;
			this.txtNombre.ShowAsCombo = false;
			this.txtNombre.ShowKeyboard = false;
			this.txtNombre.Size = new System.Drawing.Size(144, 20);
			this.txtNombre.TabIndex = 3;
			this.txtNombre.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtNombre.ToolTip = "";
			this.txtNombre.Track = false;
			this.txtNombre.XMLName = null;
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
			this.DbLabel1.Location = new System.Drawing.Point(24, 24);
			this.DbLabel1.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbLabel1.Name = "DbLabel1";
			this.DbLabel1.ShadowColor = System.Drawing.Color.Black;
			this.DbLabel1.Size = new System.Drawing.Size(72, 16);
			this.DbLabel1.StartColor = System.Drawing.Color.White;
			this.DbLabel1.TabIndex = 1;
			this.DbLabel1.TabStop = false;
			this.DbLabel1.Text = "Contrato:";
			this.DbLabel1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.DbLabel1.Track = false;
			this.DbLabel1.XOffset = (float) (1.0F);
			this.DbLabel1.YOffset = (float) (1.0F);
			//
			//DbControl5
			//
			this.DbControl5.About = null;
			this.DbControl5.AutoConnect = true;
			this.DbControl5.DataControl = null;
			this.DbControl5.DataTable = null;
			//this.DbControl5.DBConnection = null;
			this.DbControl5.DBFieldData = "";
			this.DbControl5.DBPosition = 0;
			this.DbControl5.EraseDBControl = null;
			this.DbControl5.Filter = "";
			this.DbControl5.isEOF = true;
			this.DbControl5.Location = new System.Drawing.Point(752, 72);
			this.DbControl5.LOCK = null;
			this.DbControl5.LOPD = null;
			this.DbControl5.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbControl5.Name = "DbControl5";
			this.DbControl5.Page = 0;
			this.DbControl5.PageSettings = null;
			this.DbControl5.Paging = false;
			this.DbControl5.PagingSize = 0;
			this.DbControl5.ReadOnly = false;
			this.DbControl5.RelationDataControl = null;
			this.DbControl5.RelationDBField = "";
			this.DbControl5.RelationParentDBField = "";
			this.DbControl5.SaveError = false;
			this.DbControl5.SaveOnChangeRecord = false;
			this.DbControl5.Selection = "select * from clientes order by nombre";
			this.DbControl5.Size = new System.Drawing.Size(152, 20);
			this.DbControl5.TabIndex = 9;
			this.DbControl5.TableName = "clientes";
			this.DbControl5.TabStop = false;
			this.DbControl5.Text = "SQL: select * from clientes order by nombre";
			this.DbControl5.Track = false;
			this.DbControl5.TypeDB = FSFormControls.DBControl.DbType.SQLServer;
			this.DbControl5.Versionable = false;
			this.DbControl5.VersionableDateField = "";
			this.DbControl5.VersionableTable = "";
			this.DbControl5.VersionableUserField = "";
			this.DbControl5.VersionableVersionField = "";
			this.DbControl5.Visible = false;
			this.DbControl5.XmlFile = "";
			this.DbControl5.XMLName = "";
			//
			//DbGrid1
			//
			this.DbGrid1.About = null;
			this.DbGrid1.AllowAddNew = true;
			this.DbGrid1.AllowDelete = true;
			this.DbGrid1.AllowDrop = true;
			this.DbGrid1.AllowSorting = true;
			this.DbGrid1.AlternatingColor = System.Drawing.Color.Empty;
			this.DbGrid1.Anchor = (System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.DbGrid1.AutoSave = true;
			this.DbGrid1.AutoSize = true;
			this.DbGrid1.BackGroundColor = System.Drawing.Color.LightGray;
			this.DbGrid1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.DbGrid1.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.DbGrid1.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Bold);
			this.DbGrid1.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.DbGrid1.CaptionText = null;
			this.DbGrid1.CaptionVisible = true;
			this.DbGrid1.ColumnHeadersVisible = true;
			this.DbGrid1.Columns.AddRange(new FSFormControls.DBColumn[] {this.DbColumn10, this.DbColumn3, this.DbColumn1, this.DbColumn2, this.DbColumn4});
			this.DbGrid1.CurrentRowIndex = -1;
			this.DbGrid1.CustomColumnHeaders = false;
			this.DbGrid1.DataControl = this.DbControl1;
			this.DbGrid1.DefaultDecimals = 2;
			this.DbGrid1.DefaultHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.DbGrid1.Editable = true;
			this.DbGrid1.FlatMode = false;
			this.DbGrid1.GridLineColor = System.Drawing.SystemColors.Control;
			this.DbGrid1.GridLineStyle = System.Windows.Forms.DataGridLineStyle.Solid;
			this.DbGrid1.HeaderBackColor = System.Drawing.SystemColors.Control;
			this.DbGrid1.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.DbGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.DbGrid1.LastCol = -1;
			this.DbGrid1.LastRow = -1;
			this.DbGrid1.Location = new System.Drawing.Point(16, 152);
			this.DbGrid1.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbGrid1.Name = "DbGrid1";
			this.DbGrid1.RecordMode = false;
			this.DbGrid1.RowHeadersVisible = true;
			this.DbGrid1.RowHeight = 16;
			this.DbGrid1.RowSel = -1;
			this.DbGrid1.RowsInCaption = 2;
			this.DbGrid1.ShowRecordScrollBar = false;
			this.DbGrid1.ShowTotals = false;
			this.DbGrid1.Size = new System.Drawing.Size(900, 211);
			this.DbGrid1.TabIndex = 3;
			this.DbGrid1.TotalOperation = FSFormControls.DBColumn.OperationTypes.Sum;
			this.DbGrid1.Track = false;
			this.DbGrid1.XMLName = "";
			//
			//DbColumn10
			//
			this.DbColumn10.ActiveColumnDBButtonOnReadMode = true;
			this.DbColumn10.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbColumn10.AsociatedButtonColumn = -1;
			this.DbColumn10.AsociatedComboColumn = -1;
			this.DbColumn10.ColumnBackColor = System.Drawing.Color.Empty;
			this.DbColumn10.ColumnDBControl = null;
			this.DbColumn10.ColumnDBFieldData = "";
			this.DbColumn10.ColumnForeColor = System.Drawing.Color.Empty;
			this.DbColumn10.ColumnType = FSFormControls.DBColumn.ColumnTypes.NumberColumn;
			this.DbColumn10.ComboBlankSelection = true;
			this.DbColumn10.ComboImageList = null;
			this.DbColumn10.ComboListField = "";
			this.DbColumn10.Decimals = 2;
			this.DbColumn10.DefaultValue = "";
			this.DbColumn10.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn10.Encrypted = false;
			this.DbColumn10.Expression = "";
			this.DbColumn10.FieldDB = "codigo";
			this.DbColumn10.Font = null;
			this.DbColumn10.FormatString = null;
			this.DbColumn10.HeaderCaption = "Código";
			this.DbColumn10.Hidden = false;
			this.DbColumn10.LastValue = false;
			this.DbColumn10.MaskInput = null;
			this.DbColumn10.MaxLength = 0;
			this.DbColumn10.MaxValue = decimal.MaxValue;
			this.DbColumn10.Obligatory = false;
			this.DbColumn10.ReadColumn = true;
			this.DbColumn10.ShowSelectForm = true;
			this.DbColumn10.Width = 0;
			this.DbColumn10.ToolTip = "";
			this.DbColumn10.Unique = false;
			//
			//DbColumn3
			//
			this.DbColumn3.ActiveColumnDBButtonOnReadMode = true;
			this.DbColumn3.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbColumn3.AsociatedButtonColumn = -1;
			this.DbColumn3.AsociatedComboColumn = -1;
			this.DbColumn3.ColumnBackColor = System.Drawing.Color.Empty;
			this.DbColumn3.ColumnDBControl = null;
			this.DbColumn3.ColumnDBFieldData = "";
			this.DbColumn3.ColumnForeColor = System.Drawing.Color.Empty;
			this.DbColumn3.ColumnType = FSFormControls.DBColumn.ColumnTypes.AutoNumericColumn;
			this.DbColumn3.ComboBlankSelection = true;
			this.DbColumn3.ComboImageList = null;
			this.DbColumn3.ComboListField = "";
			this.DbColumn3.Decimals = 2;
			this.DbColumn3.DefaultValue = "";
			this.DbColumn3.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn3.Encrypted = false;
			this.DbColumn3.Expression = "";
			this.DbColumn3.FieldDB = "nroFactura";
			this.DbColumn3.Font = null;
			this.DbColumn3.FormatString = null;
			this.DbColumn3.HeaderCaption = "Nº Factura";
			this.DbColumn3.Hidden = false;
			this.DbColumn3.LastValue = false;
			this.DbColumn3.MaskInput = null;
			this.DbColumn3.MaxLength = 0;
			this.DbColumn3.MaxValue = decimal.MaxValue;
			this.DbColumn3.Obligatory = false;
			this.DbColumn3.ReadColumn = false;
			this.DbColumn3.ShowSelectForm = true;
			this.DbColumn3.Width = 0;
			this.DbColumn3.ToolTip = "";
			this.DbColumn3.Unique = false;
			//
			//DbColumn1
			//
			this.DbColumn1.ActiveColumnDBButtonOnReadMode = true;
			this.DbColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbColumn1.AsociatedButtonColumn = -1;
			this.DbColumn1.AsociatedComboColumn = -1;
			this.DbColumn1.ColumnBackColor = System.Drawing.Color.Empty;
			this.DbColumn1.ColumnDBControl = null;
			this.DbColumn1.ColumnDBFieldData = "";
			this.DbColumn1.ColumnForeColor = System.Drawing.Color.Empty;
			this.DbColumn1.ColumnType = FSFormControls.DBColumn.ColumnTypes.DateColumn;
			this.DbColumn1.ComboBlankSelection = true;
			this.DbColumn1.ComboImageList = null;
			this.DbColumn1.ComboListField = "";
			this.DbColumn1.Decimals = 2;
			this.DbColumn1.DefaultValue = "";
			this.DbColumn1.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn1.Encrypted = false;
			this.DbColumn1.Expression = "";
			this.DbColumn1.FieldDB = "fecha";
			this.DbColumn1.Font = null;
			this.DbColumn1.FormatString = null;
			this.DbColumn1.HeaderCaption = "Fecha";
			this.DbColumn1.Hidden = false;
			this.DbColumn1.LastValue = false;
			this.DbColumn1.MaskInput = null;
			this.DbColumn1.MaxLength = 0;
			this.DbColumn1.MaxValue = decimal.MaxValue;
			this.DbColumn1.Obligatory = false;
			this.DbColumn1.ReadColumn = false;
			this.DbColumn1.ShowSelectForm = true;
			this.DbColumn1.Width = 0;
			this.DbColumn1.ToolTip = "";
			this.DbColumn1.Unique = false;
			//
			//DbColumn2
			//
			this.DbColumn2.ActiveColumnDBButtonOnReadMode = true;
			this.DbColumn2.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbColumn2.AsociatedButtonColumn = -1;
			this.DbColumn2.AsociatedComboColumn = -1;
			this.DbColumn2.ColumnBackColor = System.Drawing.Color.Empty;
			this.DbColumn2.ColumnDBControl = null;
			this.DbColumn2.ColumnDBFieldData = "";
			this.DbColumn2.ColumnForeColor = System.Drawing.Color.Empty;
			this.DbColumn2.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
			this.DbColumn2.ComboBlankSelection = true;
			this.DbColumn2.ComboImageList = null;
			this.DbColumn2.ComboListField = "";
			this.DbColumn2.Decimals = 2;
			this.DbColumn2.DefaultValue = "";
			this.DbColumn2.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn2.Encrypted = false;
			this.DbColumn2.Expression = "";
			this.DbColumn2.FieldDB = "formaPago";
			this.DbColumn2.Font = null;
			this.DbColumn2.FormatString = null;
			this.DbColumn2.HeaderCaption = "Forma de Pago";
			this.DbColumn2.Hidden = false;
			this.DbColumn2.LastValue = false;
			this.DbColumn2.MaskInput = null;
			this.DbColumn2.MaxLength = 0;
			this.DbColumn2.MaxValue = decimal.MaxValue;
			this.DbColumn2.Obligatory = false;
			this.DbColumn2.ReadColumn = false;
			this.DbColumn2.ShowSelectForm = true;
			this.DbColumn2.Width = 0;
			this.DbColumn2.ToolTip = "";
			this.DbColumn2.Unique = false;
			//
			//DbColumn4
			//
			this.DbColumn4.ActiveColumnDBButtonOnReadMode = true;
			this.DbColumn4.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbColumn4.AsociatedButtonColumn = -1;
			this.DbColumn4.AsociatedComboColumn = -1;
			this.DbColumn4.ColumnBackColor = System.Drawing.Color.Empty;
			this.DbColumn4.ColumnDBControl = null;
			this.DbColumn4.ColumnDBFieldData = "";
			this.DbColumn4.ColumnForeColor = System.Drawing.Color.Empty;
			this.DbColumn4.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
			this.DbColumn4.ComboBlankSelection = true;
			this.DbColumn4.ComboImageList = null;
			this.DbColumn4.ComboListField = "";
			this.DbColumn4.Decimals = 2;
			this.DbColumn4.DefaultValue = "";
			this.DbColumn4.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn4.Encrypted = false;
			this.DbColumn4.Expression = "";
			this.DbColumn4.FieldDB = "idContrato";
			this.DbColumn4.Font = null;
			this.DbColumn4.FormatString = null;
			this.DbColumn4.HeaderCaption = "Contrato";
			this.DbColumn4.Hidden = false;
			this.DbColumn4.LastValue = false;
			this.DbColumn4.MaskInput = null;
			this.DbColumn4.MaxLength = 0;
			this.DbColumn4.MaxValue = decimal.MaxValue;
			this.DbColumn4.Obligatory = false;
			this.DbColumn4.ReadColumn = false;
			this.DbColumn4.ShowSelectForm = true;
			this.DbColumn4.Width = 0;
			this.DbColumn4.ToolTip = "";
			this.DbColumn4.Unique = false;
			//
			//DbControl1
			//
			this.DbControl1.About = null;
			this.DbControl1.AutoConnect = true;
			this.DbControl1.DataControl = null;
			this.DbControl1.DataTable = null;
			//this.DbControl1.DBConnection = null;
			this.DbControl1.DBFieldData = "";
			this.DbControl1.DBPosition = 0;
			this.DbControl1.EraseDBControl = null;
			this.DbControl1.Filter = "";
			this.DbControl1.isEOF = true;
			this.DbControl1.Location = new System.Drawing.Point(656, 128);
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
			this.DbControl1.Selection = "select * from cabeceraPedido";
			this.DbControl1.Size = new System.Drawing.Size(88, 56);
			this.DbControl1.TabIndex = 4;
			this.DbControl1.TableName = "cabecerapedido";
			this.DbControl1.TabStop = false;
			this.DbControl1.Text = "SQL: select * from cabeceraPedido";
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
			//cmdImprimir
			//
			this.cmdImprimir.Anchor = (System.Windows.Forms.AnchorStyles) (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.cmdImprimir.Location = new System.Drawing.Point(16, 371);
			this.cmdImprimir.Name = "cmdImprimir";
			this.cmdImprimir.Size = new System.Drawing.Size(188, 24);
			this.cmdImprimir.TabIndex = 7;
			this.cmdImprimir.Text = "Imprimir Facturas Seleccionadas";
			//
			//cmdGenerar
			//
			this.cmdGenerar.Anchor = (System.Windows.Forms.AnchorStyles) (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.cmdGenerar.Location = new System.Drawing.Point(216, 371);
			this.cmdGenerar.Name = "cmdGenerar";
			this.cmdGenerar.Size = new System.Drawing.Size(188, 24);
			this.cmdGenerar.TabIndex = 8;
			this.cmdGenerar.Text = "Generar Facturas de Servicios";
			//
			//frmFacturacion
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(932, 424);
			this.Controls.Add(this.cmdGenerar);
			this.Controls.Add(this.cmdImprimir);
			this.Controls.Add(this.DbControl4);
			this.Controls.Add(this.DbControl1);
			this.Controls.Add(this.DbGrid1);
			this.Controls.Add(this.GroupBox1);
			this.Controls.Add(this.DbControl5);
			this.DataControl = this.DbControl1;
			this.Name = "frmFacturacion";
			this.Text = "Facturación";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Controls.SetChildIndex(this.DbControl5, 0);
			this.Controls.SetChildIndex(this.GroupBox1, 0);
			this.Controls.SetChildIndex(this.DbGrid1, 0);
			this.Controls.SetChildIndex(this.DbControl1, 0);
			this.Controls.SetChildIndex(this.DbControl4, 0);
			this.Controls.SetChildIndex(this.cmdImprimir, 0);
			this.Controls.SetChildIndex(this.cmdGenerar, 0);
			this.GroupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
			
		}
		
#endregion
		
		
		private void DbGrid1_DoubleClick(object sender, System.EventArgs e)
		{
			Global.MuestraFactura(this.DbGrid1.get_RowValue(0) + "");
		}
		
		private void dbbLocalizar_Click(System.Object sender, System.EventArgs e)
		{
			string filtro = default(string);
			filtro = "";
			
			if (this.txtNombre.Text != "")
			{
				filtro = "Nombre like \'%" + this.txtNombre.Text + "%\'";
			}
			if (txtContrato.Text != "")
			{
				if (filtro != "")
				{
					filtro = filtro + " and ";
				}
				filtro = filtro + "idContrato =" + this.txtContrato.Text;
			}
			if (dbcClientes.SelectedValue.ToString() != "")
			{
				if (filtro != "")
				{
					filtro = filtro + " and ";
				}
				filtro = filtro + "idCliente =" + this.dbcClientes.SelectedValue.ToString();
			}
			if (this.DbDate1.Text != "" && DbDate2.Text != "")
			{
				if (filtro != "")
				{
					filtro = filtro + " and ";
				}
				filtro = filtro + "fecha >=" + this.DbDate1.Text + " and fecha <=" + DbDate2.Text;
			}
			
			this.DbControl1.Filter = filtro;
		}
		
		private void dbbTodas_Click(System.Object sender, System.EventArgs e)
		{
			this.DbControl1.Filter = "";
		}
		
		private void frmHotSpots_Load(System.Object sender, System.EventArgs e)
		{
			Global.AplicaSeguridad(this);
			Global.AplicaToolbar(this);
		}
		
		private void cmdImprimir_Click(System.Object sender, System.EventArgs e)
		{
			int f = default(int);
			string strWhere = "";
			
			if (this.DbGrid1.Rows() == 0)
			{
				Global.Err.ErrorMessage(this.FindForm(), "No hay lineas en el grid.");
				return;
			}
			
			for (f = 0; f <= this.DbGrid1.Rows() - 1; f++)
			{
				if (this.DbGrid1.IsSelected(f))
				{
					strWhere = strWhere + "{v_Pedidos.cabeceraPedido.codigo} = " + DbGrid1.get_RowValue(0, f) + " or ";
				}
			}
			
			if (strWhere == "")
			{
				Global.Err.ErrorMessage(this.FindForm(), "Debes seleccionar las facturas que desees imprimir.");
				return;
			}
			
			strWhere = strWhere.Substring(0, strWhere.Length - 4);
			
			frmImpFactura n = new frmImpFactura();
			n.MdiParent = this.MdiParent;
			n.WindowState = FormWindowState.Maximized;

			n.DbReport1.Database = FSFormControls.Global.ConnectionStringSetting.ConnectionString;
			n.DbReport1.Selection = strWhere;
			n.DbReport1.ReportFile = "Informes/rptFactura.rpt";
			n.DbReport1.Connect();
			
			n.Show();
		}
		
		private void cmdGenerar_Click(System.Object sender, System.EventArgs e)
		{
			frmGenerarFacturasServicios n = new frmGenerarFacturasServicios();
			n.MdiParent = this.MdiParent;
			//n.DBConnection = this.DBConnection;
			n.WindowState = FormWindowState.Maximized;
			
			n.Show();
		}
	}
	
}
