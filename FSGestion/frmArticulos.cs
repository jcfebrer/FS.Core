
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System;
using System.Collections;
using System.Windows.Forms;



namespace FSGestion
{
	public class frmArticulos : FSFormControls.DBForm
	{
		
#region  Código generado por el Diseñador de Windows Forms
		
		public frmArticulos()
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

        private System.ComponentModel.IContainer components;

        //Requerido por el Diseñador de Windows Forms
		
		//NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
		//Puede modificarse utilizando el Diseñador de Windows Forms.
		//No lo modifique con el editor de código.
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal FSFormControls.DBGrid DbGrid1;
		internal FSFormControls.DBControl DbControl1;
		internal FSFormControls.DBLabel DbLabel2;
		internal FSFormControls.DBButton dbbLocalizar;
		internal FSFormControls.DBColumn DbColumn1;
		internal FSFormControls.DBColumn DbColumn4;
		internal FSFormControls.DBColumn DbColumn5;
		internal FSFormControls.DBColumn DbColumn6;
		internal FSFormControls.DBColumn DbColumn7;
		internal FSFormControls.DBButton dbbTodas;
		internal FSFormControls.DBLabel DbLabel3;
		internal FSFormControls.DBControl DbControl4;
		internal FSFormControls.DBColumn DbColumn9;
		internal FSFormControls.DBControl DbControl5;
		internal FSFormControls.DBColumn DbColumn10;
		internal FSFormControls.DBCombo dbcFamilias;
		internal FSFormControls.DBTextBox txtDescripcion;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.DbControl5 = new FSFormControls.DBControl();
            this.DbLabel3 = new FSFormControls.DBLabel();
            this.dbcFamilias = new FSFormControls.DBCombo();
            this.dbbTodas = new FSFormControls.DBButton();
            this.dbbLocalizar = new FSFormControls.DBButton();
            this.DbLabel2 = new FSFormControls.DBLabel();
            this.txtDescripcion = new FSFormControls.DBTextBox();
            this.DbControl4 = new FSFormControls.DBControl();
            this.DbGrid1 = new FSFormControls.DBGrid();
            this.DbColumn10 = new FSFormControls.DBColumn();
            this.DbColumn1 = new FSFormControls.DBColumn();
            this.DbColumn9 = new FSFormControls.DBColumn();
            this.DbColumn4 = new FSFormControls.DBColumn();
            this.DbColumn5 = new FSFormControls.DBColumn();
            this.DbColumn6 = new FSFormControls.DBColumn();
            this.DbColumn7 = new FSFormControls.DBColumn();
            this.DbControl1 = new FSFormControls.DBControl();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuForm
            // 
            this.mnuForm.OwnerDraw = true;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.DbControl5);
            this.GroupBox1.Controls.Add(this.DbLabel3);
            this.GroupBox1.Controls.Add(this.dbcFamilias);
            this.GroupBox1.Controls.Add(this.dbbTodas);
            this.GroupBox1.Controls.Add(this.dbbLocalizar);
            this.GroupBox1.Controls.Add(this.DbLabel2);
            this.GroupBox1.Controls.Add(this.txtDescripcion);
            this.GroupBox1.Location = new System.Drawing.Point(16, 64);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(632, 80);
            this.GroupBox1.TabIndex = 2;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Selección de Articulos";
            // 
            // DbControl5
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
            this.DbControl5.Location = new System.Drawing.Point(280, 24);
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
            this.DbControl5.Selection = "select * from familias order by nombre";
            this.DbControl5.Size = new System.Drawing.Size(152, 20);
            this.DbControl5.TabIndex = 9;
            this.DbControl5.TableName = "familias";
            this.DbControl5.TabStop = false;
            this.DbControl5.Text = "SQL: select * from familias order by nombre";
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
            // DbLabel3
            // 
            this.DbLabel3.About = null;
            this.DbLabel3.Angle = 0F;
            this.DbLabel3.BackColor = System.Drawing.Color.Transparent;
            this.DbLabel3.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
            this.DbLabel3.DataControl = null;
            this.DbLabel3.DataType = FSFormControls.DBTextBox.TypeData.All;
            this.DbLabel3.DateFormat = "dd/MM/yyyy";
            this.DbLabel3.Decimals = 2;
            this.DbLabel3.EndColor = System.Drawing.Color.LightSkyBlue;
            this.DbLabel3.Location = new System.Drawing.Point(24, 28);
            this.DbLabel3.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbLabel3.Name = "DbLabel3";
            this.DbLabel3.ShadowColor = System.Drawing.Color.Black;
            this.DbLabel3.Size = new System.Drawing.Size(76, 16);
            this.DbLabel3.StartColor = System.Drawing.Color.White;
            this.DbLabel3.TabIndex = 8;
            this.DbLabel3.TabStop = false;
            this.DbLabel3.Text = "Familia:";
            this.DbLabel3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.DbLabel3.Track = false;
            this.DbLabel3.XOffset = 1F;
            this.DbLabel3.YOffset = 1F;
            // 
            // dbcFamilias
            // 
            this.dbcFamilias.About = null;
            this.dbcFamilias.BlankSelection = true;
            this.dbcFamilias.DataControl = null;
            this.dbcFamilias.DataControlList = this.DbControl5;
            this.dbcFamilias.DBField = null;
            this.dbcFamilias.DBFieldData = "codigo";
            this.dbcFamilias.DBFieldList = "nombre";
            this.dbcFamilias.DisplayMember = "";
            this.dbcFamilias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dbcFamilias.DroppedDown = false;
            this.dbcFamilias.Editable = true;
            this.dbcFamilias.Location = new System.Drawing.Point(128, 24);
            this.dbcFamilias.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
            this.dbcFamilias.Name = "dbcFamilias";
            this.dbcFamilias.Obligatory = false;
            this.dbcFamilias.OrderBy = null;
            this.dbcFamilias.SelectedIndex = -1;
            this.dbcFamilias.SelectedItem = null;
            this.dbcFamilias.SelectedOption = null;
            this.dbcFamilias.SelectedValue = null;
            this.dbcFamilias.ShowCode = false;
            this.dbcFamilias.ShowEdit = false;
            this.dbcFamilias.Size = new System.Drawing.Size(144, 21);
            this.dbcFamilias.Sort = true;
            this.dbcFamilias.TabIndex = 7;
            this.dbcFamilias.Track = false;
            this.dbcFamilias.ValueMember = "";
            // 
            // dbbTodas
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
            this.dbbTodas.Location = new System.Drawing.Point(496, 16);
            this.dbbTodas.Name = "dbbTodas";
            this.dbbTodas.Size = new System.Drawing.Size(120, 24);
            this.dbbTodas.TabIndex = 6;
            this.dbbTodas.Text = "Mostrar todos";
            this.dbbTodas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dbbTodas.TextColorEnd = System.Drawing.Color.Black;
            this.dbbTodas.TextColorStart = System.Drawing.Color.Blue;
            this.dbbTodas.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dbbTodas.ToolTip = "";
            this.dbbTodas.Track = false;
            this.dbbTodas.Click += new System.EventHandler(this.dbbTodas_Click);
            // 
            // dbbLocalizar
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
            this.dbbLocalizar.Location = new System.Drawing.Point(496, 48);
            this.dbbLocalizar.Name = "dbbLocalizar";
            this.dbbLocalizar.Size = new System.Drawing.Size(120, 24);
            this.dbbLocalizar.TabIndex = 5;
            this.dbbLocalizar.Text = "Localizar";
            this.dbbLocalizar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dbbLocalizar.TextColorEnd = System.Drawing.Color.Black;
            this.dbbLocalizar.TextColorStart = System.Drawing.Color.Blue;
            this.dbbLocalizar.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dbbLocalizar.ToolTip = "";
            this.dbbLocalizar.Track = false;
            this.dbbLocalizar.Click += new System.EventHandler(this.dbbLocalizar_Click);
            // 
            // DbLabel2
            // 
            this.DbLabel2.About = null;
            this.DbLabel2.Angle = 0F;
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
            this.DbLabel2.Text = "Descripción:";
            this.DbLabel2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.DbLabel2.Track = false;
            this.DbLabel2.XOffset = 1F;
            this.DbLabel2.YOffset = 1F;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.About = null;
            this.txtDescripcion.AsociatedCombo = null;
            this.txtDescripcion.AsociatedDBFindTextBox = null;
            this.txtDescripcion.BackColorRead = System.Drawing.Color.WhiteSmoke;
            this.txtDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDescripcion.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
            this.txtDescripcion.DataControl = null;
            this.txtDescripcion.DataType = FSFormControls.DBTextBox.TypeData.All;
            this.txtDescripcion.DateFormat = "dd/MM/yyyy";
            this.txtDescripcion.DBField = null;
            this.txtDescripcion.DBFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Decimals = 2;
            this.txtDescripcion.DefaultValue = null;
            this.txtDescripcion.DotNumber = false;
            this.txtDescripcion.Editable = true;
            this.txtDescripcion.Encrypted = false;
            this.txtDescripcion.Expression = "";
            this.txtDescripcion.FormatString = "";
            this.txtDescripcion.GridOperation = FSFormControls.DBColumn.OperationTypes.Sum;
            this.txtDescripcion.Location = new System.Drawing.Point(128, 48);
            this.txtDescripcion.MaskInput = null;
            this.txtDescripcion.MaxLength = 32767;
            this.txtDescripcion.MaxValue = decimal.MaxValue;
            this.txtDescripcion.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
            this.txtDescripcion.Multiline = false;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Obligatory = false;
            this.txtDescripcion.PasswordChar = '\0';
            this.txtDescripcion.ReadOnly = false;
            this.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDescripcion.Shadow = false;
            this.txtDescripcion.ShadowColor = System.Drawing.Color.Gray;
            this.txtDescripcion.ShadowSize = 4;
            this.txtDescripcion.ShowAsCombo = false;
            this.txtDescripcion.ShowKeyboard = false;
            this.txtDescripcion.Size = new System.Drawing.Size(144, 20);
            this.txtDescripcion.TabIndex = 3;
            this.txtDescripcion.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDescripcion.ToolTip = "";
            this.txtDescripcion.Track = false;
            this.txtDescripcion.XMLName = null;
            // 
            // DbControl4
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
            this.DbControl4.Selection = "select * from familias order by nombre";
            this.DbControl4.Size = new System.Drawing.Size(72, 48);
            this.DbControl4.TabIndex = 6;
            this.DbControl4.TableName = "familias";
            this.DbControl4.TabStop = false;
            this.DbControl4.Text = "SQL: select * from familias order by nombre";
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
            // DbGrid1
            // 
            this.DbGrid1.About = null;
            this.DbGrid1.AllowAddNew = true;
            this.DbGrid1.AllowDelete = true;
            this.DbGrid1.AllowDrop = true;
            this.DbGrid1.AllowSorting = true;
            this.DbGrid1.AlternatingColor = System.Drawing.Color.Empty;
            this.DbGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DbGrid1.AutoSave = true;
            this.DbGrid1.AutoSize = true;
            this.DbGrid1.BackGroundColor = System.Drawing.Color.LightGray;
            this.DbGrid1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DbGrid1.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.DbGrid1.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.DbGrid1.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.DbGrid1.CaptionText = null;
            this.DbGrid1.CaptionVisible = true;
            this.DbGrid1.ColumnHeadersVisible = true;
            this.DbGrid1.Columns.AddRange(new FSFormControls.DBColumn[] {
            this.DbColumn10,
            this.DbColumn1,
            this.DbColumn9,
            this.DbColumn4,
            this.DbColumn5,
            this.DbColumn6,
            this.DbColumn7});
            this.DbGrid1.CurrentRowIndex = -1;
            this.DbGrid1.CustomColumnHeaders = false;
            this.DbGrid1.DataControl = this.DbControl1;
            this.DbGrid1.DefaultDecimals = 2;
            this.DbGrid1.DefaultHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DbGrid1.Editable = true;
            this.DbGrid1.FlatMode = false;
            this.DbGrid1.GridLineColor = System.Drawing.SystemColors.Control;
            this.DbGrid1.GridLineStyle = System.Windows.Forms.DataGridLineStyle.Solid;
            this.DbGrid1.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.DbGrid1.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.DbGrid1.ShowRecordScrollBar = true;
            this.DbGrid1.ShowTotals = false;
            this.DbGrid1.Size = new System.Drawing.Size(905, 365);
            this.DbGrid1.TabIndex = 3;
            this.DbGrid1.TotalOperation = FSFormControls.DBColumn.OperationTypes.Sum;
            this.DbGrid1.Track = false;
            this.DbGrid1.XMLName = "";
            this.DbGrid1.DoubleClick += new FSFormControls.DBGrid.DoubleClickEventHandler(this.DbGrid1_DoubleClick);
            // 
            // DbColumn10
            // 
            this.DbColumn10.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn10.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn10.AsociatedButtonColumn = -1;
            this.DbColumn10.AsociatedComboColumn = -1;
            this.DbColumn10.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn10.ColumnDBControl = null;
            this.DbColumn10.ColumnDBFieldData = "";
            this.DbColumn10.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn10.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn10.ComboBlankSelection = true;
            this.DbColumn10.ComboImageList = null;
            this.DbColumn10.ComboListField = "";
            this.DbColumn10.Decimals = 2;
            this.DbColumn10.DefaultValue = "";
            this.DbColumn10.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn10.Encrypted = false;
            this.DbColumn10.Expression = "";
            this.DbColumn10.FieldDB = "codArticulo";
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
            // DbColumn1
            // 
            this.DbColumn1.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn1.AsociatedButtonColumn = -1;
            this.DbColumn1.AsociatedComboColumn = -1;
            this.DbColumn1.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn1.ColumnDBControl = null;
            this.DbColumn1.ColumnDBFieldData = "";
            this.DbColumn1.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn1.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn1.ComboBlankSelection = true;
            this.DbColumn1.ComboImageList = null;
            this.DbColumn1.ComboListField = "";
            this.DbColumn1.Decimals = 2;
            this.DbColumn1.DefaultValue = "";
            this.DbColumn1.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn1.Encrypted = false;
            this.DbColumn1.Expression = "";
            this.DbColumn1.FieldDB = "descripcion";
            this.DbColumn1.Font = null;
            this.DbColumn1.FormatString = null;
            this.DbColumn1.HeaderCaption = "Descripción";
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
            // DbColumn9
            // 
            this.DbColumn9.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn9.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn9.AsociatedButtonColumn = -1;
            this.DbColumn9.AsociatedComboColumn = -1;
            this.DbColumn9.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn9.ColumnDBControl = this.DbControl4;
            this.DbColumn9.ColumnDBFieldData = "codigo";
            this.DbColumn9.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn9.ColumnType = FSFormControls.DBColumn.ColumnTypes.ComboColumn;
            this.DbColumn9.ComboBlankSelection = true;
            this.DbColumn9.ComboImageList = null;
            this.DbColumn9.ComboListField = "nombre";
            this.DbColumn9.Decimals = 2;
            this.DbColumn9.DefaultValue = "";
            this.DbColumn9.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn9.Encrypted = false;
            this.DbColumn9.Expression = "";
            this.DbColumn9.FieldDB = "familia";
            this.DbColumn9.Font = null;
            this.DbColumn9.FormatString = null;
            this.DbColumn9.HeaderCaption = "Familia";
            this.DbColumn9.Hidden = false;
            this.DbColumn9.LastValue = false;
            this.DbColumn9.MaskInput = null;
            this.DbColumn9.MaxLength = 0;
            this.DbColumn9.MaxValue = decimal.MaxValue;
            this.DbColumn9.Obligatory = false;
            this.DbColumn9.ReadColumn = false;
            this.DbColumn9.ShowSelectForm = true;
            this.DbColumn9.Width = 0;
            this.DbColumn9.ToolTip = "";
            this.DbColumn9.Unique = false;
            // 
            // DbColumn4
            // 
            this.DbColumn4.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn4.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn4.AsociatedButtonColumn = -1;
            this.DbColumn4.AsociatedComboColumn = -1;
            this.DbColumn4.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn4.ColumnDBControl = null;
            this.DbColumn4.ColumnDBFieldData = "";
            this.DbColumn4.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn4.ColumnType = FSFormControls.DBColumn.ColumnTypes.MoneyColumn;
            this.DbColumn4.ComboBlankSelection = true;
            this.DbColumn4.ComboImageList = null;
            this.DbColumn4.ComboListField = "";
            this.DbColumn4.Decimals = 2;
            this.DbColumn4.DefaultValue = "";
            this.DbColumn4.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn4.Encrypted = false;
            this.DbColumn4.Expression = "";
            this.DbColumn4.FieldDB = "precioa";
            this.DbColumn4.Font = null;
            this.DbColumn4.FormatString = null;
            this.DbColumn4.HeaderCaption = "Precio A";
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
            // DbColumn5
            // 
            this.DbColumn5.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn5.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn5.AsociatedButtonColumn = -1;
            this.DbColumn5.AsociatedComboColumn = -1;
            this.DbColumn5.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn5.ColumnDBControl = null;
            this.DbColumn5.ColumnDBFieldData = "";
            this.DbColumn5.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn5.ColumnType = FSFormControls.DBColumn.ColumnTypes.MoneyColumn;
            this.DbColumn5.ComboBlankSelection = true;
            this.DbColumn5.ComboImageList = null;
            this.DbColumn5.ComboListField = "";
            this.DbColumn5.Decimals = 2;
            this.DbColumn5.DefaultValue = "";
            this.DbColumn5.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn5.Encrypted = false;
            this.DbColumn5.Expression = "";
            this.DbColumn5.FieldDB = "precioB";
            this.DbColumn5.Font = null;
            this.DbColumn5.FormatString = null;
            this.DbColumn5.HeaderCaption = "Precio B";
            this.DbColumn5.Hidden = false;
            this.DbColumn5.LastValue = false;
            this.DbColumn5.MaskInput = null;
            this.DbColumn5.MaxLength = 0;
            this.DbColumn5.MaxValue = decimal.MaxValue;
            this.DbColumn5.Obligatory = false;
            this.DbColumn5.ReadColumn = false;
            this.DbColumn5.ShowSelectForm = true;
            this.DbColumn5.Width = 0;
            this.DbColumn5.ToolTip = "";
            this.DbColumn5.Unique = false;
            // 
            // DbColumn6
            // 
            this.DbColumn6.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn6.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn6.AsociatedButtonColumn = -1;
            this.DbColumn6.AsociatedComboColumn = -1;
            this.DbColumn6.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn6.ColumnDBControl = null;
            this.DbColumn6.ColumnDBFieldData = "";
            this.DbColumn6.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn6.ColumnType = FSFormControls.DBColumn.ColumnTypes.NumberColumn;
            this.DbColumn6.ComboBlankSelection = true;
            this.DbColumn6.ComboImageList = null;
            this.DbColumn6.ComboListField = "";
            this.DbColumn6.Decimals = 2;
            this.DbColumn6.DefaultValue = "";
            this.DbColumn6.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn6.Encrypted = false;
            this.DbColumn6.Expression = "";
            this.DbColumn6.FieldDB = "stock";
            this.DbColumn6.Font = null;
            this.DbColumn6.FormatString = null;
            this.DbColumn6.HeaderCaption = "Stock";
            this.DbColumn6.Hidden = false;
            this.DbColumn6.LastValue = false;
            this.DbColumn6.MaskInput = null;
            this.DbColumn6.MaxLength = 0;
            this.DbColumn6.MaxValue = decimal.MaxValue;
            this.DbColumn6.Obligatory = false;
            this.DbColumn6.ReadColumn = false;
            this.DbColumn6.ShowSelectForm = true;
            this.DbColumn6.Width = 0;
            this.DbColumn6.ToolTip = "";
            this.DbColumn6.Unique = false;
            // 
            // DbColumn7
            // 
            this.DbColumn7.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn7.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn7.AsociatedButtonColumn = -1;
            this.DbColumn7.AsociatedComboColumn = -1;
            this.DbColumn7.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn7.ColumnDBControl = null;
            this.DbColumn7.ColumnDBFieldData = "";
            this.DbColumn7.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn7.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn7.ComboBlankSelection = true;
            this.DbColumn7.ComboImageList = null;
            this.DbColumn7.ComboListField = "";
            this.DbColumn7.Decimals = 2;
            this.DbColumn7.DefaultValue = "";
            this.DbColumn7.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn7.Encrypted = false;
            this.DbColumn7.Expression = "";
            this.DbColumn7.FieldDB = "titulo";
            this.DbColumn7.Font = null;
            this.DbColumn7.FormatString = null;
            this.DbColumn7.HeaderCaption = "Titulo";
            this.DbColumn7.Hidden = false;
            this.DbColumn7.LastValue = false;
            this.DbColumn7.MaskInput = null;
            this.DbColumn7.MaxLength = 0;
            this.DbColumn7.MaxValue = decimal.MaxValue;
            this.DbColumn7.Obligatory = false;
            this.DbColumn7.ReadColumn = false;
            this.DbColumn7.ShowSelectForm = true;
            this.DbColumn7.Width = 0;
            this.DbColumn7.ToolTip = "";
            this.DbColumn7.Unique = false;
            // 
            // DbControl1
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
            this.DbControl1.Selection = "select * from articulos";
            this.DbControl1.Size = new System.Drawing.Size(88, 56);
            this.DbControl1.TabIndex = 4;
            this.DbControl1.TableName = "articulos";
            this.DbControl1.TabStop = false;
            this.DbControl1.Text = "SQL: select * from articulos";
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
            // frmArticulos
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(937, 542);
            this.Controls.Add(this.DbControl4);
            this.Controls.Add(this.DbControl1);
            this.Controls.Add(this.DbGrid1);
            this.Controls.Add(this.GroupBox1);
            this.DataControl = this.DbControl1;
            this.Name = "frmArticulos";
            this.Text = "Articulos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmArticulos_Load);
            this.Controls.SetChildIndex(this.GroupBox1, 0);
            this.Controls.SetChildIndex(this.DbGrid1, 0);
            this.Controls.SetChildIndex(this.DbControl1, 0);
            this.Controls.SetChildIndex(this.DbControl4, 0);
            this.GroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		
#endregion
		
		
		private void DbGrid1_DoubleClick(object sender, System.EventArgs e)
		{
			Global.MuestraArticulos(this.DbGrid1.get_RowValue(0) + "");
		}
		
		private void dbbLocalizar_Click(System.Object sender, System.EventArgs e)
		{
			string filtro = default(string);
			filtro = "";
			
			if (this.txtDescripcion.Text != "")
			{
				filtro = "descripcion like \'%" + this.txtDescripcion.Text + "%\' or titulo like \'%" + this.txtDescripcion.Text + "%\'";
			}
			if (dbcFamilias.SelectedValue.ToString() != "")
			{
				if (filtro != "")
				{
					filtro = filtro + " and ";
				}
				filtro = filtro + "familia =" + this.dbcFamilias.SelectedValue.ToString();
			}
			
			this.DbControl1.Filter = filtro;
		}
		
		private void dbbTodas_Click(System.Object sender, System.EventArgs e)
		{
			this.DbControl1.Filter = "";
		}
		
		private void frmArticulos_Load(System.Object sender, System.EventArgs e)
		{
			Global.AplicaSeguridad(this);
			Global.AplicaToolbar(this);
		}
	}
	
}
