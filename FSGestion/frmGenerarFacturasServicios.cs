
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;



namespace FSGestion
{
	public class frmGenerarFacturasServicios : FSFormControls.DBForm
	{
		
#region  Código generado por el Diseñador de Windows Forms
		
		public frmGenerarFacturasServicios()
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
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal FSFormControls.DBDate DbDate2;
		internal FSFormControls.DBDate DbDate1;
		internal FSFormControls.DBLabel DbLabel4;
		internal FSFormControls.DBButton dbbTodas;
		internal FSFormControls.DBButton dbbLocalizar;
		internal FSFormControls.DBGrid DbGrid1;
		internal FSFormControls.DBButton cmdGenerar;
		internal FSFormControls.DBControl DbControl1;
		internal FSFormControls.DBLabel DbLabel1;
		internal FSFormControls.DBControl DbControl8;
		internal FSFormControls.DBControl DbControl13;
		internal FSFormControls.DBColumn DbColumn1;
		internal FSFormControls.DBColumn DbColumn2;
		internal FSFormControls.DBColumn DbColumn3;
		internal FSFormControls.DBColumn DbColumn4;
		internal FSFormControls.DBColumn DbColumn5;
		internal FSFormControls.DBColumn DbColumn7;
		internal FSFormControls.DBColumn DbColumn6;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			this.DbLabel1 = new FSFormControls.DBLabel();
			this.DbDate2 = new FSFormControls.DBDate();
			this.DbDate1 = new FSFormControls.DBDate();
			this.DbLabel4 = new FSFormControls.DBLabel();
			this.dbbTodas = new FSFormControls.DBButton();
			this.dbbTodas.Click += new System.EventHandler(this.dbbTodas_Click);
			this.dbbLocalizar = new FSFormControls.DBButton();
			this.dbbLocalizar.Click += new System.EventHandler(this.dbbLocalizar_Click);
			this.DbGrid1 = new FSFormControls.DBGrid();
			this.DbColumn7 = new FSFormControls.DBColumn();
			this.DbColumn1 = new FSFormControls.DBColumn();
			this.DbControl8 = new FSFormControls.DBControl();
			this.DbColumn2 = new FSFormControls.DBColumn();
			this.DbColumn3 = new FSFormControls.DBColumn();
			this.DbControl13 = new FSFormControls.DBControl();
			this.DbColumn4 = new FSFormControls.DBColumn();
			this.DbColumn5 = new FSFormControls.DBColumn();
			this.DbColumn6 = new FSFormControls.DBColumn();
			this.DbControl1 = new FSFormControls.DBControl();
			this.cmdGenerar = new FSFormControls.DBButton();
			this.cmdGenerar.Click += new System.EventHandler(this.cmdGenerar_Click);
			this.GroupBox1.SuspendLayout();
			this.SuspendLayout();
			//
			//GroupBox1
			//
			this.GroupBox1.Controls.Add(this.DbLabel1);
			this.GroupBox1.Controls.Add(this.DbDate2);
			this.GroupBox1.Controls.Add(this.DbDate1);
			this.GroupBox1.Controls.Add(this.DbLabel4);
			this.GroupBox1.Controls.Add(this.dbbTodas);
			this.GroupBox1.Controls.Add(this.dbbLocalizar);
			this.GroupBox1.Location = new System.Drawing.Point(16, 64);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(464, 80);
			this.GroupBox1.TabIndex = 3;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "Selección de Fechas";
			//
			//DbLabel1
			//
			this.DbLabel1.About = null;
			this.DbLabel1.AutoSize = false;
			this.DbLabel1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.DbLabel1.Location = new System.Drawing.Point(168, 24);
			this.DbLabel1.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbLabel1.Name = "DbLabel1";
			this.DbLabel1.Size = new System.Drawing.Size(40, 16);
			this.DbLabel1.TabIndex = 14;
			this.DbLabel1.Text = "Hasta:";
			this.DbLabel1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
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
			this.DbDate2.Location = new System.Drawing.Point(216, 24);
			this.DbDate2.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
			this.DbDate2.Name = "DbDate2";
			this.DbDate2.Obligatory = false;
			this.DbDate2.Size = new System.Drawing.Size(92, 20);
			this.DbDate2.TabIndex = 13;
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
			this.DbDate1.Location = new System.Drawing.Point(72, 24);
			this.DbDate1.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
			this.DbDate1.Name = "DbDate1";
			this.DbDate1.Obligatory = false;
			this.DbDate1.Size = new System.Drawing.Size(92, 20);
			this.DbDate1.TabIndex = 12;
			//
			//DbLabel4
			//
			this.DbLabel4.About = null;
			this.DbLabel4.AutoSize = false;
			this.DbLabel4.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.DbLabel4.Location = new System.Drawing.Point(16, 24);
			this.DbLabel4.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbLabel4.Name = "DbLabel4";
			this.DbLabel4.Size = new System.Drawing.Size(48, 16);
			this.DbLabel4.TabIndex = 11;
			this.DbLabel4.Text = "Desde:";
			this.DbLabel4.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			//
			//dbbTodas
			//
			this.dbbTodas.About = null;
			this.dbbTodas.ButtonStyle = FSFormControls.DBButton.ButtonStyleType.Normal;
			this.dbbTodas.DropDownMenu = null;
			this.dbbTodas.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.dbbTodas.Image = null;
			this.dbbTodas.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.dbbTodas.Location = new System.Drawing.Point(328, 16);
			this.dbbTodas.Name = "dbbTodas";
			this.dbbTodas.Size = new System.Drawing.Size(120, 24);
			this.dbbTodas.TabIndex = 6;
			this.dbbTodas.Text = "Mostrar todos";
			this.dbbTodas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.dbbTodas.ToolTip = "";
			//
			//dbbLocalizar
			//
			this.dbbLocalizar.About = null;
			this.dbbLocalizar.ButtonStyle = FSFormControls.DBButton.ButtonStyleType.Normal;
			this.dbbLocalizar.DropDownMenu = null;
			this.dbbLocalizar.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.dbbLocalizar.Image = null;
			this.dbbLocalizar.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.dbbLocalizar.Location = new System.Drawing.Point(328, 48);
			this.dbbLocalizar.Name = "dbbLocalizar";
			this.dbbLocalizar.Size = new System.Drawing.Size(120, 24);
			this.dbbLocalizar.TabIndex = 5;
			this.dbbLocalizar.Text = "Localizar";
			this.dbbLocalizar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.dbbLocalizar.ToolTip = "";
			//
			//DbGrid1
			//
			this.DbGrid1.About = null;
			this.DbGrid1.AllowDelete = true;
			this.DbGrid1.AllowDrop = true;
			this.DbGrid1.AlternatingColor = System.Drawing.Color.Empty;
			this.DbGrid1.Anchor = (System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.DbGrid1.AutoSize = true;
			this.DbGrid1.BackGroundColor = System.Drawing.Color.LightGray;
			this.DbGrid1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.DbGrid1.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.DbGrid1.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Bold);
			this.DbGrid1.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.DbGrid1.CaptionText = null;
			this.DbGrid1.CaptionVisible = true;
			this.DbGrid1.ColumnHeadersVisible = true;
			this.DbGrid1.Columns.AddRange(new FSFormControls.DBColumn[] {this.DbColumn7, this.DbColumn1, this.DbColumn2, this.DbColumn3, this.DbColumn4, this.DbColumn5, this.DbColumn6});
			this.DbGrid1.DataControl = this.DbControl1;
			this.DbGrid1.Editable = true;
			this.DbGrid1.FlatMode = System.Convert.ToBoolean(System.Windows.Forms.FlatStyle.Flat);
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
			this.DbGrid1.RowSel = -1;
			this.DbGrid1.ShowRecordScrollBar = false;
			this.DbGrid1.ShowTotals = false;
			this.DbGrid1.Size = new System.Drawing.Size(784, 152);
			this.DbGrid1.TabIndex = 4;
			this.DbGrid1.XMLName = "";
			//
			//DbColumn7
			//
			this.DbColumn7.ActiveColumnDBButtonOnReadMode = true;
			this.DbColumn7.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbColumn7.AsociatedButtonColumn = -1;
			this.DbColumn7.ColumnBackColor = System.Drawing.Color.Empty;
			this.DbColumn7.ColumnDBControl = null;
			this.DbColumn7.ColumnForeColor = System.Drawing.Color.Empty;
			this.DbColumn7.ColumnType = FSFormControls.DBColumn.ColumnTypes.NumberColumn;
			this.DbColumn7.ComboBlankSelection = true;
			this.DbColumn7.ColumnDBFieldData = "";
			this.DbColumn7.ComboListField = "";
			this.DbColumn7.Decimals = 0;
			this.DbColumn7.DefaultValue = "";
			this.DbColumn7.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn7.Encrypted = false;
			this.DbColumn7.Expression = "";
			this.DbColumn7.FieldDB = "idServicio";
			this.DbColumn7.Font = null;
			this.DbColumn7.FormatString = null;
			this.DbColumn7.HeaderCaption = "Cód. Servicio";
			this.DbColumn7.Hidden = false;
			this.DbColumn7.MaskInput = null;
			this.DbColumn7.MaxLength = 0;
			this.DbColumn7.MaxValue = decimal.MaxValue;
			this.DbColumn7.Obligatory = false;
			this.DbColumn7.ReadColumn = false;
			this.DbColumn7.ShowSelectForm = true;
			this.DbColumn7.Width = 0;
			this.DbColumn7.Unique = false;
			//
			//DbColumn1
			//
			this.DbColumn1.ActiveColumnDBButtonOnReadMode = true;
			this.DbColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbColumn1.AsociatedButtonColumn = -1;
			this.DbColumn1.ColumnBackColor = System.Drawing.Color.Empty;
			this.DbColumn1.ColumnDBControl = this.DbControl8;
			this.DbColumn1.ColumnForeColor = System.Drawing.Color.Empty;
			this.DbColumn1.ColumnType = FSFormControls.DBColumn.ColumnTypes.ButtonColumn;
			this.DbColumn1.ComboBlankSelection = true;
			this.DbColumn1.ColumnDBFieldData = "";
			this.DbColumn1.ComboListField = "";
			this.DbColumn1.Decimals = 2;
			this.DbColumn1.DefaultValue = "";
			this.DbColumn1.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn1.Encrypted = false;
			this.DbColumn1.Expression = "";
			this.DbColumn1.FieldDB = "codServicio";
			this.DbColumn1.Font = null;
			this.DbColumn1.FormatString = null;
			this.DbColumn1.HeaderCaption = "Código";
			this.DbColumn1.Hidden = false;
			this.DbColumn1.MaskInput = null;
			this.DbColumn1.MaxLength = 0;
			this.DbColumn1.MaxValue = decimal.MaxValue;
			this.DbColumn1.Obligatory = false;
			this.DbColumn1.ReadColumn = false;
			this.DbColumn1.ShowSelectForm = true;
			this.DbColumn1.Width = 0;
			this.DbColumn1.Unique = false;
			//
			//DbControl8
			//
			this.DbControl8.About = null;
			this.DbControl8.DataTable = null;
			//this.DbControl8.DBConnection = null;
			this.DbControl8.DBFieldData = "";
			this.DbControl8.DBPosition = 0;
			this.DbControl8.EraseDBControl = null;
			this.DbControl8.Filter = "";
			this.DbControl8.isEOF = true;
			this.DbControl8.Location = new System.Drawing.Point(400, 216);
			this.DbControl8.LOCK = null;
			this.DbControl8.LOPD = null;
			this.DbControl8.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbControl8.Name = "DbControl8";
			this.DbControl8.Page = 0;
			this.DbControl8.PageSettings = null;
			this.DbControl8.Paging = false;
			this.DbControl8.PagingSize = 0;
			this.DbControl8.RelationDataControl = null;
			this.DbControl8.RelationDBField = "";
			this.DbControl8.RelationParentDBField = "";
			this.DbControl8.SaveError = false;
			this.DbControl8.Selection = "select * from servicios";
			this.DbControl8.Size = new System.Drawing.Size(80, 48);
			this.DbControl8.TabIndex = 17;
			this.DbControl8.TableName = "servicios";
			this.DbControl8.Versionable = false;
			this.DbControl8.VersionableDateField = "";
			this.DbControl8.VersionableTable = "";
			this.DbControl8.VersionableUserField = "";
			this.DbControl8.VersionableVersionField = "";
			this.DbControl8.Visible = false;
			this.DbControl8.XMLName = "";
			//
			//DbColumn2
			//
			this.DbColumn2.ActiveColumnDBButtonOnReadMode = true;
			this.DbColumn2.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbColumn2.AsociatedButtonColumn = 1;
			this.DbColumn2.ColumnBackColor = System.Drawing.Color.Empty;
			this.DbColumn2.ColumnDBControl = null;
			this.DbColumn2.ColumnForeColor = System.Drawing.Color.Empty;
			this.DbColumn2.ColumnType = FSFormControls.DBColumn.ColumnTypes.DescriptionColumn;
			this.DbColumn2.ComboBlankSelection = true;
			this.DbColumn2.ColumnDBFieldData = "";
			this.DbColumn2.ComboListField = "";
			this.DbColumn2.Decimals = 2;
			this.DbColumn2.DefaultValue = "";
			this.DbColumn2.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn2.Encrypted = false;
			this.DbColumn2.Expression = "";
			this.DbColumn2.FieldDB = "descripcion";
			this.DbColumn2.Font = null;
			this.DbColumn2.FormatString = null;
			this.DbColumn2.HeaderCaption = "Descripción";
			this.DbColumn2.Hidden = false;
			this.DbColumn2.MaskInput = null;
			this.DbColumn2.MaxLength = 0;
			this.DbColumn2.MaxValue = decimal.MaxValue;
			this.DbColumn2.Obligatory = false;
			this.DbColumn2.ReadColumn = false;
			this.DbColumn2.ShowSelectForm = true;
			this.DbColumn2.Width = 0;
			this.DbColumn2.Unique = false;
			//
			//DbColumn3
			//
			this.DbColumn3.ActiveColumnDBButtonOnReadMode = true;
			this.DbColumn3.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbColumn3.AsociatedButtonColumn = -1;
			this.DbColumn3.ColumnBackColor = System.Drawing.Color.Empty;
			this.DbColumn3.ColumnDBControl = this.DbControl13;
			this.DbColumn3.ColumnForeColor = System.Drawing.Color.Empty;
			this.DbColumn3.ColumnType = FSFormControls.DBColumn.ColumnTypes.ComboColumn;
			this.DbColumn3.ComboBlankSelection = true;
			this.DbColumn3.ColumnDBFieldData = "idPeriodicidad";
			this.DbColumn3.ComboListField = "descripcion";
			this.DbColumn3.Decimals = 2;
			this.DbColumn3.DefaultValue = "";
			this.DbColumn3.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn3.Encrypted = false;
			this.DbColumn3.Expression = "";
			this.DbColumn3.FieldDB = "Periodicidad";
			this.DbColumn3.Font = null;
			this.DbColumn3.FormatString = null;
			this.DbColumn3.HeaderCaption = "Periodicidad";
			this.DbColumn3.Hidden = false;
			this.DbColumn3.MaskInput = null;
			this.DbColumn3.MaxLength = 0;
			this.DbColumn3.MaxValue = decimal.MaxValue;
			this.DbColumn3.Obligatory = false;
			this.DbColumn3.ReadColumn = false;
			this.DbColumn3.ShowSelectForm = true;
			this.DbColumn3.Width = 0;
			this.DbColumn3.Unique = false;
			//
			//DbControl13
			//
			this.DbControl13.About = null;
			this.DbControl13.DataTable = null;
			//this.DbControl13.DBConnection = null;
			this.DbControl13.DBFieldData = "";
			this.DbControl13.DBPosition = 0;
			this.DbControl13.EraseDBControl = null;
			this.DbControl13.Filter = "";
			this.DbControl13.isEOF = true;
			this.DbControl13.Location = new System.Drawing.Point(560, 192);
			this.DbControl13.LOCK = null;
			this.DbControl13.LOPD = null;
			this.DbControl13.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbControl13.Name = "DbControl13";
			this.DbControl13.Page = 0;
			this.DbControl13.PageSettings = null;
			this.DbControl13.Paging = false;
			this.DbControl13.PagingSize = 0;
			this.DbControl13.RelationDataControl = null;
			this.DbControl13.RelationDBField = "";
			this.DbControl13.RelationParentDBField = "";
			this.DbControl13.SaveError = false;
			this.DbControl13.Selection = "select * from periodos";
			this.DbControl13.Size = new System.Drawing.Size(96, 40);
			this.DbControl13.TabIndex = 16;
			this.DbControl13.TableName = "periodos";
			this.DbControl13.Versionable = false;
			this.DbControl13.VersionableDateField = "";
			this.DbControl13.VersionableTable = "";
			this.DbControl13.VersionableUserField = "";
			this.DbControl13.VersionableVersionField = "";
			this.DbControl13.Visible = false;
			this.DbControl13.XMLName = "";
			//
			//DbColumn4
			//
			this.DbColumn4.ActiveColumnDBButtonOnReadMode = true;
			this.DbColumn4.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbColumn4.AsociatedButtonColumn = -1;
			this.DbColumn4.ColumnBackColor = System.Drawing.Color.Empty;
			this.DbColumn4.ColumnDBControl = null;
			this.DbColumn4.ColumnForeColor = System.Drawing.Color.Empty;
			this.DbColumn4.ColumnType = FSFormControls.DBColumn.ColumnTypes.DateColumn;
			this.DbColumn4.ComboBlankSelection = true;
			this.DbColumn4.ColumnDBFieldData = "";
			this.DbColumn4.ComboListField = "";
			this.DbColumn4.Decimals = 2;
			this.DbColumn4.DefaultValue = "";
			this.DbColumn4.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn4.Encrypted = false;
			this.DbColumn4.Expression = "";
			this.DbColumn4.FieldDB = "fechaInicio";
			this.DbColumn4.Font = null;
			this.DbColumn4.FormatString = null;
			this.DbColumn4.HeaderCaption = "Fecha Inicio";
			this.DbColumn4.Hidden = false;
			this.DbColumn4.MaskInput = null;
			this.DbColumn4.MaxLength = 0;
			this.DbColumn4.MaxValue = decimal.MaxValue;
			this.DbColumn4.Obligatory = false;
			this.DbColumn4.ReadColumn = false;
			this.DbColumn4.ShowSelectForm = true;
			this.DbColumn4.Width = 0;
			this.DbColumn4.Unique = false;
			//
			//DbColumn5
			//
			this.DbColumn5.ActiveColumnDBButtonOnReadMode = true;
			this.DbColumn5.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbColumn5.AsociatedButtonColumn = -1;
			this.DbColumn5.ColumnBackColor = System.Drawing.Color.Empty;
			this.DbColumn5.ColumnDBControl = null;
			this.DbColumn5.ColumnForeColor = System.Drawing.Color.Empty;
			this.DbColumn5.ColumnType = FSFormControls.DBColumn.ColumnTypes.DateColumn;
			this.DbColumn5.ComboBlankSelection = true;
			this.DbColumn5.ColumnDBFieldData = "";
			this.DbColumn5.ComboListField = "";
			this.DbColumn5.Decimals = 2;
			this.DbColumn5.DefaultValue = "";
			this.DbColumn5.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn5.Encrypted = false;
			this.DbColumn5.Expression = "";
			this.DbColumn5.FieldDB = "fechaFin";
			this.DbColumn5.Font = null;
			this.DbColumn5.FormatString = null;
			this.DbColumn5.HeaderCaption = "Fecha Fin";
			this.DbColumn5.Hidden = false;
			this.DbColumn5.MaskInput = null;
			this.DbColumn5.MaxLength = 0;
			this.DbColumn5.MaxValue = decimal.MaxValue;
			this.DbColumn5.Obligatory = false;
			this.DbColumn5.ReadColumn = false;
			this.DbColumn5.ShowSelectForm = true;
			this.DbColumn5.Width = 0;
			this.DbColumn5.Unique = false;
			//
			//DbColumn6
			//
			this.DbColumn6.ActiveColumnDBButtonOnReadMode = true;
			this.DbColumn6.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbColumn6.AsociatedButtonColumn = 1;
			this.DbColumn6.ColumnBackColor = System.Drawing.Color.Empty;
			this.DbColumn6.ColumnDBControl = null;
			this.DbColumn6.ColumnForeColor = System.Drawing.Color.Empty;
			this.DbColumn6.ColumnType = FSFormControls.DBColumn.ColumnTypes.DescriptionColumn;
			this.DbColumn6.ComboBlankSelection = true;
			this.DbColumn6.ColumnDBFieldData = "";
			this.DbColumn6.ComboListField = "";
			this.DbColumn6.Decimals = 2;
			this.DbColumn6.DefaultValue = "";
			this.DbColumn6.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.NumberDescription;
			this.DbColumn6.Encrypted = false;
			this.DbColumn6.Expression = "";
			this.DbColumn6.FieldDB = "precio";
			this.DbColumn6.Font = null;
			this.DbColumn6.FormatString = null;
			this.DbColumn6.HeaderCaption = "Precio";
			this.DbColumn6.Hidden = false;
			this.DbColumn6.MaskInput = null;
			this.DbColumn6.MaxLength = 0;
			this.DbColumn6.MaxValue = decimal.MaxValue;
			this.DbColumn6.Obligatory = false;
			this.DbColumn6.ReadColumn = false;
			this.DbColumn6.ShowSelectForm = true;
			this.DbColumn6.Width = 0;
			this.DbColumn6.Unique = false;
			//
			//DbControl1
			//
			this.DbControl1.About = null;
			this.DbControl1.DataTable = null;
			//this.DbControl1.DBConnection = null;
			this.DbControl1.DBFieldData = "";
			this.DbControl1.DBPosition = 0;
			this.DbControl1.EraseDBControl = null;
			this.DbControl1.Filter = "";
			this.DbControl1.isEOF = true;
			this.DbControl1.Location = new System.Drawing.Point(528, 80);
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
			this.DbControl1.Selection = "select * from servicioscliente";
			this.DbControl1.Size = new System.Drawing.Size(96, 40);
			this.DbControl1.TabIndex = 6;
			this.DbControl1.TableName = "servicioscliente";
			this.DbControl1.Versionable = false;
			this.DbControl1.VersionableDateField = "";
			this.DbControl1.VersionableTable = "";
			this.DbControl1.VersionableUserField = "";
			this.DbControl1.VersionableVersionField = "";
			this.DbControl1.Visible = false;
			this.DbControl1.XMLName = "";
			//
			//cmdGenerar
			//
			this.cmdGenerar.About = null;
			this.cmdGenerar.Anchor = (System.Windows.Forms.AnchorStyles) (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.cmdGenerar.ButtonStyle = FSFormControls.DBButton.ButtonStyleType.Normal;
			this.cmdGenerar.DropDownMenu = null;
			this.cmdGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.cmdGenerar.Image = null;
			this.cmdGenerar.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.cmdGenerar.Location = new System.Drawing.Point(16, 312);
			this.cmdGenerar.Name = "cmdGenerar";
			this.cmdGenerar.Size = new System.Drawing.Size(88, 24);
			this.cmdGenerar.TabIndex = 5;
			this.cmdGenerar.Text = "Generar!";
			this.cmdGenerar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.cmdGenerar.ToolTip = "";
			//
			//frmGenerarFacturasServicios
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(816, 361);
			this.Controls.Add(this.DbControl8);
			this.Controls.Add(this.DbControl13);
			this.Controls.Add(this.DbControl1);
			this.Controls.Add(this.GroupBox1);
			this.Controls.Add(this.cmdGenerar);
			this.Controls.Add(this.DbGrid1);
			this.DataControl = this.DbControl1;
			this.Name = "frmGenerarFacturasServicios";
			this.Text = "Generación de Facturas Servicios";
			this.Controls.SetChildIndex(this.DbGrid1, 0);
			this.Controls.SetChildIndex(this.cmdGenerar, 0);
			this.Controls.SetChildIndex(this.GroupBox1, 0);
			this.Controls.SetChildIndex(this.DbControl1, 0);
			this.Controls.SetChildIndex(this.DbControl13, 0);
			this.Controls.SetChildIndex(this.DbControl8, 0);
			this.GroupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			
		}
		
#endregion
		
		
		private void dbbLocalizar_Click(System.Object sender, System.EventArgs e)
		{
			string filtro = default(string);
			filtro = "";
			
			if (this.DbDate1.Text != "" && DbDate2.Text != "")
			{
				if (filtro != "")
				{
					filtro = filtro + " and ";
				}
				filtro = filtro + "fechainicio >=\'" + this.DbDate1.Text + "\' and fechainicio <=\'" + DbDate2.Text + "\' and fechafin >=\'" + this.DbDate2.Text + "\'";
			}
			
			this.DbControl1.Filter = filtro;
		}
		
		private void dbbTodas_Click(System.Object sender, System.EventArgs e)
		{
			this.DbControl1.Filter = "";
		}
		
		private void cmdGenerar_Click(System.Object sender, System.EventArgs e)
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
					strWhere = strWhere + "idServicio = " + DbGrid1.get_RowValue(0, f) + " or ";
				}
			}
			
			if (strWhere == "")
			{
				Global.Err.ErrorMessage(this.FindForm(), "Debes seleccionar los servicios que deseas generar facturas.");
				return;
			}
			
			strWhere = strWhere.Substring(0, strWhere.Length - 4);
			
			MessageBox.Show(strWhere);
		}
	}
	
}
