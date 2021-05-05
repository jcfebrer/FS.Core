
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;



namespace FSGestion
{
	public class frmFactura : FSFormControls.DBForm
	{
		
#region  Código generado por el Diseñador de Windows Forms
		
		public frmFactura()
		{
			
			//El Diseñador de Windows Forms requiere esta llamada.
			InitializeComponent();
			
			this.MdiParent = Global.mdiP;
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
		internal FSFormControls.DBRecord DbRecord1;
		internal FSFormControls.DBControl DbControl1;
		internal FSFormControls.DBGrid DbGrid1;
		internal FSFormControls.DBColumn DbColumn1;
		internal FSFormControls.DBColumn DbColumn2;
		internal FSFormControls.DBColumn DbColumn3;
		internal FSFormControls.DBColumn DbColumn4;
		internal FSFormControls.DBColumn DbColumn5;
		internal FSFormControls.DBColumn DbColumn6;
		internal FSFormControls.DBColumn DbColumn7;
		internal FSFormControls.DBControl DbControl2;
		internal FSFormControls.DBColumn DbColumn8;
		internal FSFormControls.DBColumn DbColumn9;
		internal FSFormControls.DBControl DbControl3;
		internal FSFormControls.DBLabel DbLabel1;
		internal FSFormControls.DBTextBox txtTotal;
		internal FSFormControls.DBTextBox txtIVA;
		internal FSFormControls.DBLabel DbLabel2;
		internal FSFormControls.DBTextBox txtImporte;
		internal FSFormControls.DBLabel DbLabel3;
		internal FSFormControls.DBButton cmdImpFactura;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.DbRecord1 = new FSFormControls.DBRecord();
			base.Load += new System.EventHandler(frmFactura_Load);
			this.DbColumn8 = new FSFormControls.DBColumn();
			this.DbColumn9 = new FSFormControls.DBColumn();
			this.DbControl1 = new FSFormControls.DBControl();
			this.DbGrid1 = new FSFormControls.DBGrid();
			this.DbColumn1 = new FSFormControls.DBColumn();
			this.DbControl3 = new FSFormControls.DBControl();
			this.DbColumn2 = new FSFormControls.DBColumn();
			this.DbColumn7 = new FSFormControls.DBColumn();
			this.DbColumn3 = new FSFormControls.DBColumn();
			this.DbColumn4 = new FSFormControls.DBColumn();
			this.DbColumn5 = new FSFormControls.DBColumn();
			this.DbColumn6 = new FSFormControls.DBColumn();
			this.DbControl2 = new FSFormControls.DBControl();
			this.DbLabel1 = new FSFormControls.DBLabel();
			this.txtTotal = new FSFormControls.DBTextBox();
			this.txtIVA = new FSFormControls.DBTextBox();
			this.DbLabel2 = new FSFormControls.DBLabel();
			this.txtImporte = new FSFormControls.DBTextBox();
			this.DbLabel3 = new FSFormControls.DBLabel();
			this.cmdImpFactura = new FSFormControls.DBButton();
			this.cmdImpFactura.Click += new System.EventHandler(this.cmdImpFactura_Click);
			this.SuspendLayout();
			//
			//mnuForm
			//
			this.mnuForm.OwnerDraw = true;
			//
			//DbRecord1
			//
			this.DbRecord1.About = null;
			this.DbRecord1.AllowAddNew = true;
			this.DbRecord1.AllowCancel = true;
			this.DbRecord1.AllowDelete = true;
			this.DbRecord1.AllowEdit = true;
			this.DbRecord1.AllowFilter = true;
			this.DbRecord1.AllowList = true;
			this.DbRecord1.AllowNavigate = true;
			this.DbRecord1.AllowPrint = true;
			this.DbRecord1.AllowRecord = true;
			this.DbRecord1.AllowSave = true;
			this.DbRecord1.AllowSearch = true;
			this.DbRecord1.Anchor = (System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.DbRecord1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.DbRecord1.Columns.AddRange(new FSFormControls.DBColumn[] {this.DbColumn8, this.DbColumn9});
			this.DbRecord1.DataControl = this.DbControl1;
			this.DbRecord1.DateType = FSFormControls.DBRecord.t_date.Normal;
			this.DbRecord1.DoubleHeightInLargeText = false;
			this.DbRecord1.LabelAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbRecord1.LabelYIncrement = 30;
			this.DbRecord1.Location = new System.Drawing.Point(16, 64);
			this.DbRecord1.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbRecord1.Name = "DbRecord1";
			this.DbRecord1.PosXLabel = 20;
			this.DbRecord1.PosYLabel = 20;
			this.DbRecord1.ShowAddNew = true;
			this.DbRecord1.ShowCancel = true;
			this.DbRecord1.ShowClose = true;
			this.DbRecord1.ShowComboEdit = false;
			this.DbRecord1.ShowDelete = true;
			this.DbRecord1.ShowEdit = true;
			this.DbRecord1.ShowFilter = true;
			this.DbRecord1.ShowList = true;
			this.DbRecord1.ShowMode = FSFormControls.DBRecord.t_showmode.Vertical;
			this.DbRecord1.ShowNavigate = true;
			this.DbRecord1.ShowPrint = true;
			this.DbRecord1.ShowRecord = true;
			this.DbRecord1.ShowSave = true;
			this.DbRecord1.ShowScrollBar = false;
			this.DbRecord1.ShowSearch = true;
			this.DbRecord1.ShowToolBar = false;
			this.DbRecord1.Size = new System.Drawing.Size(781, 112);
			this.DbRecord1.TabIndex = 2;
			this.DbRecord1.TextBoxShadow = false;
			this.DbRecord1.Track = false;
			//
			//DbColumn8
			//
			this.DbColumn8.ActiveColumnDBButtonOnReadMode = true;
			this.DbColumn8.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbColumn8.AsociatedButtonColumn = -1;
			this.DbColumn8.AsociatedComboColumn = -1;
			this.DbColumn8.ColumnBackColor = System.Drawing.Color.Empty;
			this.DbColumn8.ColumnDBControl = null;
			this.DbColumn8.ColumnDBFieldData = "";
			this.DbColumn8.ColumnForeColor = System.Drawing.Color.Empty;
			this.DbColumn8.ColumnType = FSFormControls.DBColumn.ColumnTypes.DateColumn;
			this.DbColumn8.ComboBlankSelection = true;
			this.DbColumn8.ComboImageList = null;
			this.DbColumn8.ComboListField = "";
			this.DbColumn8.Decimals = 2;
			this.DbColumn8.DefaultValue = "";
			this.DbColumn8.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn8.Encrypted = false;
			this.DbColumn8.Expression = "";
			this.DbColumn8.FieldDB = "fecha";
			this.DbColumn8.Font = null;
			this.DbColumn8.FormatString = null;
			this.DbColumn8.HeaderCaption = "Fecha";
			this.DbColumn8.Hidden = false;
			this.DbColumn8.LastValue = false;
			this.DbColumn8.MaskInput = null;
			this.DbColumn8.MaxLength = 0;
			this.DbColumn8.MaxValue = decimal.MaxValue;
			this.DbColumn8.Obligatory = false;
			this.DbColumn8.ReadColumn = false;
			this.DbColumn8.ShowSelectForm = true;
			this.DbColumn8.Width = 0;
			this.DbColumn8.ToolTip = "";
			this.DbColumn8.Unique = false;
			//
			//DbColumn9
			//
			this.DbColumn9.ActiveColumnDBButtonOnReadMode = true;
			this.DbColumn9.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbColumn9.AsociatedButtonColumn = -1;
			this.DbColumn9.AsociatedComboColumn = -1;
			this.DbColumn9.ColumnBackColor = System.Drawing.Color.Empty;
			this.DbColumn9.ColumnDBControl = null;
			this.DbColumn9.ColumnDBFieldData = "";
			this.DbColumn9.ColumnForeColor = System.Drawing.Color.Empty;
			this.DbColumn9.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
			this.DbColumn9.ComboBlankSelection = true;
			this.DbColumn9.ComboImageList = null;
			this.DbColumn9.ComboListField = "";
			this.DbColumn9.Decimals = 2;
			this.DbColumn9.DefaultValue = "";
			this.DbColumn9.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn9.Encrypted = false;
			this.DbColumn9.Expression = "";
			this.DbColumn9.FieldDB = "formaPago";
			this.DbColumn9.Font = null;
			this.DbColumn9.FormatString = null;
			this.DbColumn9.HeaderCaption = "Forma de Pago";
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
			this.DbControl1.Location = new System.Drawing.Point(528, 64);
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
			this.DbControl1.Selection = "select * from CabeceraPedido where codigo=?";
			this.DbControl1.Size = new System.Drawing.Size(112, 48);
			this.DbControl1.TabIndex = 3;
			this.DbControl1.TableName = "cabecerapedido";
			this.DbControl1.TabStop = false;
			this.DbControl1.Text = "SQL: select * from CabeceraPedido where codigo=?";
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
			this.DbGrid1.Columns.AddRange(new FSFormControls.DBColumn[] {this.DbColumn1, this.DbColumn2, this.DbColumn7, this.DbColumn3, this.DbColumn4, this.DbColumn5, this.DbColumn6});
			this.DbGrid1.CurrentRowIndex = -1;
			this.DbGrid1.CustomColumnHeaders = false;
			this.DbGrid1.DataControl = this.DbControl2;
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
			this.DbGrid1.Location = new System.Drawing.Point(16, 184);
			this.DbGrid1.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbGrid1.Name = "DbGrid1";
			this.DbGrid1.RecordMode = false;
			this.DbGrid1.RowHeadersVisible = true;
			this.DbGrid1.RowHeight = 16;
			this.DbGrid1.RowSel = -1;
			this.DbGrid1.RowsInCaption = 2;
			this.DbGrid1.ShowRecordScrollBar = false;
			this.DbGrid1.ShowTotals = true;
			this.DbGrid1.Size = new System.Drawing.Size(781, 171);
			this.DbGrid1.TabIndex = 4;
			this.DbGrid1.TotalOperation = FSFormControls.DBColumn.OperationTypes.Sum;
			this.DbGrid1.Track = false;
			this.DbGrid1.XMLName = "";
			//
			//DbColumn1
			//
			this.DbColumn1.ActiveColumnDBButtonOnReadMode = true;
			this.DbColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbColumn1.AsociatedButtonColumn = -1;
			this.DbColumn1.AsociatedComboColumn = -1;
			this.DbColumn1.ColumnBackColor = System.Drawing.Color.Empty;
			this.DbColumn1.ColumnDBControl = this.DbControl3;
			this.DbColumn1.ColumnDBFieldData = "codArticulo";
			this.DbColumn1.ColumnForeColor = System.Drawing.Color.Empty;
			this.DbColumn1.ColumnType = FSFormControls.DBColumn.ColumnTypes.ButtonColumn;
			this.DbColumn1.ComboBlankSelection = true;
			this.DbColumn1.ComboImageList = null;
			this.DbColumn1.ComboListField = "";
			this.DbColumn1.Decimals = 0;
			this.DbColumn1.DefaultValue = "";
			this.DbColumn1.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn1.Encrypted = false;
			this.DbColumn1.Expression = "";
			this.DbColumn1.FieldDB = "codArticulo";
			this.DbColumn1.Font = null;
			this.DbColumn1.FormatString = null;
			this.DbColumn1.HeaderCaption = "Código";
			this.DbColumn1.Hidden = false;
			this.DbColumn1.LastValue = false;
			this.DbColumn1.MaskInput = null;
			this.DbColumn1.MaxLength = 0;
			this.DbColumn1.MaxValue = decimal.MaxValue;
			this.DbColumn1.Obligatory = true;
			this.DbColumn1.ReadColumn = false;
			this.DbColumn1.ShowSelectForm = true;
			this.DbColumn1.Width = 0;
			this.DbColumn1.ToolTip = "";
			this.DbColumn1.Unique = false;
			//
			//DbControl3
			//
			this.DbControl3.About = null;
			this.DbControl3.AutoConnect = true;
			this.DbControl3.DataControl = null;
			this.DbControl3.DataTable = null;
			//this.DbControl3.DBConnection = null;
			this.DbControl3.DBFieldData = "";
			this.DbControl3.DBPosition = 0;
			this.DbControl3.EraseDBControl = null;
			this.DbControl3.Filter = "";
			this.DbControl3.isEOF = true;
			this.DbControl3.Location = new System.Drawing.Point(672, 72);
			this.DbControl3.LOCK = null;
			this.DbControl3.LOPD = null;
			this.DbControl3.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbControl3.Name = "DbControl3";
			this.DbControl3.Page = 0;
			this.DbControl3.PageSettings = null;
			this.DbControl3.Paging = false;
			this.DbControl3.PagingSize = 0;
			this.DbControl3.ReadOnly = false;
			this.DbControl3.RelationDataControl = null;
			this.DbControl3.RelationDBField = "";
			this.DbControl3.RelationParentDBField = "";
			this.DbControl3.SaveError = false;
			this.DbControl3.SaveOnChangeRecord = false;
			this.DbControl3.Selection = "select * from articulos";
			this.DbControl3.Size = new System.Drawing.Size(80, 56);
			this.DbControl3.TabIndex = 6;
			this.DbControl3.TableName = "articulos";
			this.DbControl3.TabStop = false;
			this.DbControl3.Text = "SQL: select * from articulos";
			this.DbControl3.Track = false;
			this.DbControl3.TypeDB = FSFormControls.DBControl.DbType.SQLServer;
			this.DbControl3.Versionable = false;
			this.DbControl3.VersionableDateField = "";
			this.DbControl3.VersionableTable = "";
			this.DbControl3.VersionableUserField = "";
			this.DbControl3.VersionableVersionField = "";
			this.DbControl3.Visible = false;
			this.DbControl3.XmlFile = "";
			this.DbControl3.XMLName = "";
			//
			//DbColumn2
			//
			this.DbColumn2.ActiveColumnDBButtonOnReadMode = true;
			this.DbColumn2.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbColumn2.AsociatedButtonColumn = 0;
			this.DbColumn2.AsociatedComboColumn = -1;
			this.DbColumn2.ColumnBackColor = System.Drawing.Color.Empty;
			this.DbColumn2.ColumnDBControl = null;
			this.DbColumn2.ColumnDBFieldData = "";
			this.DbColumn2.ColumnForeColor = System.Drawing.Color.Empty;
			this.DbColumn2.ColumnType = FSFormControls.DBColumn.ColumnTypes.DescriptionColumn;
			this.DbColumn2.ComboBlankSelection = true;
			this.DbColumn2.ComboImageList = null;
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
			//DbColumn7
			//
			this.DbColumn7.ActiveColumnDBButtonOnReadMode = true;
			this.DbColumn7.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbColumn7.AsociatedButtonColumn = -1;
			this.DbColumn7.AsociatedComboColumn = -1;
			this.DbColumn7.ColumnBackColor = System.Drawing.Color.Empty;
			this.DbColumn7.ColumnDBControl = null;
			this.DbColumn7.ColumnDBFieldData = "";
			this.DbColumn7.ColumnForeColor = System.Drawing.Color.Empty;
			this.DbColumn7.ColumnType = FSFormControls.DBColumn.ColumnTypes.NumberColumn;
			this.DbColumn7.ComboBlankSelection = true;
			this.DbColumn7.ComboImageList = null;
			this.DbColumn7.ComboListField = "";
			this.DbColumn7.Decimals = 0;
			this.DbColumn7.DefaultValue = "";
			this.DbColumn7.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn7.Encrypted = false;
			this.DbColumn7.Expression = "";
			this.DbColumn7.FieldDB = "almacen";
			this.DbColumn7.Font = null;
			this.DbColumn7.FormatString = null;
			this.DbColumn7.HeaderCaption = "Almacen";
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
			this.DbColumn3.ColumnType = FSFormControls.DBColumn.ColumnTypes.NumberColumn;
			this.DbColumn3.ComboBlankSelection = true;
			this.DbColumn3.ComboImageList = null;
			this.DbColumn3.ComboListField = "";
			this.DbColumn3.Decimals = 0;
			this.DbColumn3.DefaultValue = "1";
			this.DbColumn3.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn3.Encrypted = false;
			this.DbColumn3.Expression = "";
			this.DbColumn3.FieldDB = "cantidad";
			this.DbColumn3.Font = null;
			this.DbColumn3.FormatString = null;
			this.DbColumn3.HeaderCaption = "Cantidad";
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
			//DbColumn4
			//
			this.DbColumn4.ActiveColumnDBButtonOnReadMode = true;
			this.DbColumn4.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbColumn4.AsociatedButtonColumn = 0;
			this.DbColumn4.AsociatedComboColumn = -1;
			this.DbColumn4.ColumnBackColor = System.Drawing.Color.Empty;
			this.DbColumn4.ColumnDBControl = null;
			this.DbColumn4.ColumnDBFieldData = "";
			this.DbColumn4.ColumnForeColor = System.Drawing.Color.Empty;
			this.DbColumn4.ColumnType = FSFormControls.DBColumn.ColumnTypes.NumberColumn;
			this.DbColumn4.ComboBlankSelection = true;
			this.DbColumn4.ComboImageList = null;
			this.DbColumn4.ComboListField = "";
			this.DbColumn4.Decimals = 2;
			this.DbColumn4.DefaultValue = "";
			this.DbColumn4.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.NumberDescription;
			this.DbColumn4.Encrypted = false;
			this.DbColumn4.Expression = "";
			this.DbColumn4.FieldDB = "precio";
			this.DbColumn4.Font = null;
			this.DbColumn4.FormatString = null;
			this.DbColumn4.HeaderCaption = "Precio";
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
			//DbColumn5
			//
			this.DbColumn5.ActiveColumnDBButtonOnReadMode = true;
			this.DbColumn5.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbColumn5.AsociatedButtonColumn = -1;
			this.DbColumn5.AsociatedComboColumn = -1;
			this.DbColumn5.ColumnBackColor = System.Drawing.Color.Empty;
			this.DbColumn5.ColumnDBControl = null;
			this.DbColumn5.ColumnDBFieldData = "";
			this.DbColumn5.ColumnForeColor = System.Drawing.Color.Empty;
			this.DbColumn5.ColumnType = FSFormControls.DBColumn.ColumnTypes.NumberColumn;
			this.DbColumn5.ComboBlankSelection = true;
			this.DbColumn5.ComboImageList = null;
			this.DbColumn5.ComboListField = "";
			this.DbColumn5.Decimals = 2;
			this.DbColumn5.DefaultValue = "0";
			this.DbColumn5.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn5.Encrypted = false;
			this.DbColumn5.Expression = "";
			this.DbColumn5.FieldDB = "dto";
			this.DbColumn5.Font = null;
			this.DbColumn5.FormatString = null;
			this.DbColumn5.HeaderCaption = "Dto.";
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
			//DbColumn6
			//
			this.DbColumn6.ActiveColumnDBButtonOnReadMode = true;
			this.DbColumn6.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbColumn6.AsociatedButtonColumn = -1;
			this.DbColumn6.AsociatedComboColumn = -1;
			this.DbColumn6.ColumnBackColor = System.Drawing.Color.Empty;
			this.DbColumn6.ColumnDBControl = null;
			this.DbColumn6.ColumnDBFieldData = "";
			this.DbColumn6.ColumnForeColor = System.Drawing.Color.Empty;
			this.DbColumn6.ColumnType = FSFormControls.DBColumn.ColumnTypes.FormulaColumn;
			this.DbColumn6.ComboBlankSelection = true;
			this.DbColumn6.ComboImageList = null;
			this.DbColumn6.ComboListField = "";
			this.DbColumn6.Decimals = 2;
			this.DbColumn6.DefaultValue = "";
			this.DbColumn6.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn6.Encrypted = false;
			this.DbColumn6.Expression = "(cantidad*precio) - ((cantidad * precio) * dto/100)";
			this.DbColumn6.FieldDB = "subtotal";
			this.DbColumn6.Font = null;
			this.DbColumn6.FormatString = null;
			this.DbColumn6.HeaderCaption = "SubTotal";
			this.DbColumn6.Hidden = false;
			this.DbColumn6.LastValue = false;
			this.DbColumn6.MaskInput = null;
			this.DbColumn6.MaxLength = 0;
			this.DbColumn6.MaxValue = decimal.MaxValue;
			this.DbColumn6.Obligatory = false;
			this.DbColumn6.ReadColumn = true;
			this.DbColumn6.ShowSelectForm = true;
			this.DbColumn6.Width = 0;
			this.DbColumn6.ToolTip = "";
			this.DbColumn6.Unique = false;
			//
			//DbControl2
			//
			this.DbControl2.About = null;
			this.DbControl2.AutoConnect = true;
			this.DbControl2.DataControl = null;
			this.DbControl2.DataTable = null;
			//this.DbControl2.DBConnection = null;
			this.DbControl2.DBFieldData = "";
			this.DbControl2.DBPosition = 0;
			this.DbControl2.EraseDBControl = null;
			this.DbControl2.Filter = "";
			this.DbControl2.isEOF = true;
			this.DbControl2.Location = new System.Drawing.Point(568, 136);
			this.DbControl2.LOCK = null;
			this.DbControl2.LOPD = null;
			this.DbControl2.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbControl2.Name = "DbControl2";
			this.DbControl2.Page = 0;
			this.DbControl2.PageSettings = null;
			this.DbControl2.Paging = false;
			this.DbControl2.PagingSize = 0;
			this.DbControl2.ReadOnly = false;
			this.DbControl2.RelationDataControl = this.DbControl1;
			this.DbControl2.RelationDBField = "codigoPedido";
			this.DbControl2.RelationParentDBField = "codigo";
			this.DbControl2.SaveError = false;
			this.DbControl2.SaveOnChangeRecord = false;
			this.DbControl2.Selection = "select * from LineasPedido where codigopedido=?";
			this.DbControl2.Size = new System.Drawing.Size(120, 40);
			this.DbControl2.TabIndex = 5;
			this.DbControl2.TableName = "lineaspedido";
			this.DbControl2.TabStop = false;
			this.DbControl2.Text = "SQL: select * from LineasPedido where codigopedido=?";
			this.DbControl2.Track = false;
			this.DbControl2.TypeDB = FSFormControls.DBControl.DbType.SQLServer;
			this.DbControl2.Versionable = false;
			this.DbControl2.VersionableDateField = "";
			this.DbControl2.VersionableTable = "";
			this.DbControl2.VersionableUserField = "";
			this.DbControl2.VersionableVersionField = "";
			this.DbControl2.Visible = false;
			this.DbControl2.XmlFile = "";
			this.DbControl2.XMLName = "";
			//
			//DbLabel1
			//
			this.DbLabel1.About = null;
			this.DbLabel1.Anchor = (System.Windows.Forms.AnchorStyles) (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.DbLabel1.Angle = (float) (0.0F);
			this.DbLabel1.BackColor = System.Drawing.Color.Transparent;
			this.DbLabel1.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
			this.DbLabel1.DataControl = null;
			this.DbLabel1.DataType = FSFormControls.DBTextBox.TypeData.All;
			this.DbLabel1.DateFormat = "dd/MM/yyyy";
			this.DbLabel1.Decimals = 2;
			this.DbLabel1.EndColor = System.Drawing.Color.LightSkyBlue;
			this.DbLabel1.Location = new System.Drawing.Point(637, 367);
			this.DbLabel1.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbLabel1.Name = "DbLabel1";
			this.DbLabel1.ShadowColor = System.Drawing.Color.Black;
			this.DbLabel1.Size = new System.Drawing.Size(56, 16);
			this.DbLabel1.StartColor = System.Drawing.Color.White;
			this.DbLabel1.TabIndex = 7;
			this.DbLabel1.TabStop = false;
			this.DbLabel1.Text = "Total:";
			this.DbLabel1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.DbLabel1.Track = false;
			this.DbLabel1.XOffset = (float) (1.0F);
			this.DbLabel1.YOffset = (float) (1.0F);
			//
			//txtTotal
			//
			this.txtTotal.About = null;
			this.txtTotal.Anchor = (System.Windows.Forms.AnchorStyles) (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.txtTotal.AsociatedCombo = null;
			this.txtTotal.AsociatedDBFindTextBox = null;
			this.txtTotal.BackColorRead = System.Drawing.Color.WhiteSmoke;
			this.txtTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.txtTotal.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
			this.txtTotal.DataControl = this.DbControl2;
			this.txtTotal.DataType = FSFormControls.DBTextBox.TypeData.Formula;
			this.txtTotal.DateFormat = "dd/MM/yyyy";
			this.txtTotal.DBField = null;
			this.txtTotal.DBFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.txtTotal.Decimals = 2;
			this.txtTotal.DefaultValue = null;
			this.txtTotal.DotNumber = false;
			this.txtTotal.Editable = false;
			this.txtTotal.Encrypted = false;
			this.txtTotal.Expression = "sum(subtotal)";
			this.txtTotal.FormatString = "";
			this.txtTotal.GridOperation = FSFormControls.DBColumn.OperationTypes.Sum;
			this.txtTotal.Location = new System.Drawing.Point(701, 363);
			this.txtTotal.MaskInput = null;
			this.txtTotal.MaxLength = 32767;
			this.txtTotal.MaxValue = decimal.MaxValue;
			this.txtTotal.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
			this.txtTotal.Multiline = false;
			this.txtTotal.Name = "txtTotal";
			this.txtTotal.Obligatory = false;
			this.txtTotal.PasswordChar = (char)0;
			this.txtTotal.ReadOnly = true;
			this.txtTotal.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.txtTotal.Shadow = false;
			this.txtTotal.ShadowColor = System.Drawing.Color.Gray;
			this.txtTotal.ShadowSize = 4;
			this.txtTotal.ShowAsCombo = false;
			this.txtTotal.ShowKeyboard = false;
			this.txtTotal.Size = new System.Drawing.Size(96, 20);
			this.txtTotal.TabIndex = 8;
			this.txtTotal.Text = "DbTextBox1";
			this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtTotal.ToolTip = "";
			this.txtTotal.Track = false;
			this.txtTotal.XMLName = null;
			//
			//txtIVA
			//
			this.txtIVA.About = null;
			this.txtIVA.Anchor = (System.Windows.Forms.AnchorStyles) (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.txtIVA.AsociatedCombo = null;
			this.txtIVA.AsociatedDBFindTextBox = null;
			this.txtIVA.BackColorRead = System.Drawing.Color.WhiteSmoke;
			this.txtIVA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.txtIVA.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
			this.txtIVA.DataControl = null;
			this.txtIVA.DataType = FSFormControls.DBTextBox.TypeData.Numeric;
			this.txtIVA.DateFormat = "dd/MM/yyyy";
			this.txtIVA.DBField = null;
			this.txtIVA.DBFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.txtIVA.Decimals = 2;
			this.txtIVA.DefaultValue = null;
			this.txtIVA.DotNumber = false;
			this.txtIVA.Editable = false;
			this.txtIVA.Encrypted = false;
			this.txtIVA.Expression = "";
			this.txtIVA.FormatString = "";
			this.txtIVA.GridOperation = FSFormControls.DBColumn.OperationTypes.Sum;
			this.txtIVA.Location = new System.Drawing.Point(529, 363);
			this.txtIVA.MaskInput = null;
			this.txtIVA.MaxLength = 32767;
			this.txtIVA.MaxValue = decimal.MaxValue;
			this.txtIVA.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
			this.txtIVA.Multiline = false;
			this.txtIVA.Name = "txtIVA";
			this.txtIVA.Obligatory = false;
			this.txtIVA.PasswordChar = (char)0;
			this.txtIVA.ReadOnly = true;
			this.txtIVA.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.txtIVA.Shadow = false;
			this.txtIVA.ShadowColor = System.Drawing.Color.Gray;
			this.txtIVA.ShadowSize = 4;
			this.txtIVA.ShowAsCombo = false;
			this.txtIVA.ShowKeyboard = false;
			this.txtIVA.Size = new System.Drawing.Size(96, 20);
			this.txtIVA.TabIndex = 10;
			this.txtIVA.Text = "DbTextBox1";
			this.txtIVA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtIVA.ToolTip = "";
			this.txtIVA.Track = false;
			this.txtIVA.XMLName = null;
			//
			//DbLabel2
			//
			this.DbLabel2.About = null;
			this.DbLabel2.Anchor = (System.Windows.Forms.AnchorStyles) (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.DbLabel2.Angle = (float) (0.0F);
			this.DbLabel2.BackColor = System.Drawing.Color.Transparent;
			this.DbLabel2.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
			this.DbLabel2.DataControl = null;
			this.DbLabel2.DataType = FSFormControls.DBTextBox.TypeData.All;
			this.DbLabel2.DateFormat = "dd/MM/yyyy";
			this.DbLabel2.Decimals = 2;
			this.DbLabel2.EndColor = System.Drawing.Color.LightSkyBlue;
			this.DbLabel2.Location = new System.Drawing.Point(465, 367);
			this.DbLabel2.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbLabel2.Name = "DbLabel2";
			this.DbLabel2.ShadowColor = System.Drawing.Color.Black;
			this.DbLabel2.Size = new System.Drawing.Size(56, 16);
			this.DbLabel2.StartColor = System.Drawing.Color.White;
			this.DbLabel2.TabIndex = 9;
			this.DbLabel2.TabStop = false;
			this.DbLabel2.Text = "I.V.A.:";
			this.DbLabel2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.DbLabel2.Track = false;
			this.DbLabel2.XOffset = (float) (1.0F);
			this.DbLabel2.YOffset = (float) (1.0F);
			//
			//txtImporte
			//
			this.txtImporte.About = null;
			this.txtImporte.Anchor = (System.Windows.Forms.AnchorStyles) (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.txtImporte.AsociatedCombo = null;
			this.txtImporte.AsociatedDBFindTextBox = null;
			this.txtImporte.BackColorRead = System.Drawing.Color.WhiteSmoke;
			this.txtImporte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.txtImporte.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
			this.txtImporte.DataControl = null;
			this.txtImporte.DataType = FSFormControls.DBTextBox.TypeData.Numeric;
			this.txtImporte.DateFormat = "dd/MM/yyyy";
			this.txtImporte.DBField = null;
			this.txtImporte.DBFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.txtImporte.Decimals = 2;
			this.txtImporte.DefaultValue = null;
			this.txtImporte.DotNumber = false;
			this.txtImporte.Editable = false;
			this.txtImporte.Encrypted = false;
			this.txtImporte.Expression = "";
			this.txtImporte.FormatString = "";
			this.txtImporte.GridOperation = FSFormControls.DBColumn.OperationTypes.Sum;
			this.txtImporte.Location = new System.Drawing.Point(357, 363);
			this.txtImporte.MaskInput = null;
			this.txtImporte.MaxLength = 32767;
			this.txtImporte.MaxValue = decimal.MaxValue;
			this.txtImporte.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
			this.txtImporte.Multiline = false;
			this.txtImporte.Name = "txtImporte";
			this.txtImporte.Obligatory = false;
			this.txtImporte.PasswordChar = (char)0;
			this.txtImporte.ReadOnly = true;
			this.txtImporte.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.txtImporte.Shadow = false;
			this.txtImporte.ShadowColor = System.Drawing.Color.Gray;
			this.txtImporte.ShadowSize = 4;
			this.txtImporte.ShowAsCombo = false;
			this.txtImporte.ShowKeyboard = false;
			this.txtImporte.Size = new System.Drawing.Size(96, 20);
			this.txtImporte.TabIndex = 12;
			this.txtImporte.Text = "DbTextBox1";
			this.txtImporte.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtImporte.ToolTip = "";
			this.txtImporte.Track = false;
			this.txtImporte.XMLName = null;
			//
			//DbLabel3
			//
			this.DbLabel3.About = null;
			this.DbLabel3.Anchor = (System.Windows.Forms.AnchorStyles) (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.DbLabel3.Angle = (float) (0.0F);
			this.DbLabel3.BackColor = System.Drawing.Color.Transparent;
			this.DbLabel3.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
			this.DbLabel3.DataControl = null;
			this.DbLabel3.DataType = FSFormControls.DBTextBox.TypeData.All;
			this.DbLabel3.DateFormat = "dd/MM/yyyy";
			this.DbLabel3.Decimals = 2;
			this.DbLabel3.EndColor = System.Drawing.Color.LightSkyBlue;
			this.DbLabel3.Location = new System.Drawing.Point(293, 367);
			this.DbLabel3.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbLabel3.Name = "DbLabel3";
			this.DbLabel3.ShadowColor = System.Drawing.Color.Black;
			this.DbLabel3.Size = new System.Drawing.Size(56, 16);
			this.DbLabel3.StartColor = System.Drawing.Color.White;
			this.DbLabel3.TabIndex = 11;
			this.DbLabel3.TabStop = false;
			this.DbLabel3.Text = "Importe:";
			this.DbLabel3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.DbLabel3.Track = false;
			this.DbLabel3.XOffset = (float) (1.0F);
			this.DbLabel3.YOffset = (float) (1.0F);
			//
			//cmdImpFactura
			//
			this.cmdImpFactura.About = null;
			this.cmdImpFactura.Anchor = (System.Windows.Forms.AnchorStyles) (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.cmdImpFactura.ButtonStyle = FSFormControls.DBButton.ButtonStyleType.Normal;
			this.cmdImpFactura.DataControl = null;
			this.cmdImpFactura.DropDownMenu = null;
			this.cmdImpFactura.FillColorEnd = System.Drawing.Color.White;
			this.cmdImpFactura.FillColorStart = System.Drawing.Color.LightGray;
			this.cmdImpFactura.FillHoverColorEnd = System.Drawing.Color.Beige;
			this.cmdImpFactura.FillHoverColorStart = System.Drawing.Color.Beige;
			this.cmdImpFactura.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.cmdImpFactura.Gradient = false;
			this.cmdImpFactura.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			this.cmdImpFactura.Image = null;
			this.cmdImpFactura.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.cmdImpFactura.Location = new System.Drawing.Point(16, 363);
			this.cmdImpFactura.Name = "cmdImpFactura";
			this.cmdImpFactura.Size = new System.Drawing.Size(112, 20);
			this.cmdImpFactura.TabIndex = 13;
			this.cmdImpFactura.Text = "Imprimir Factura";
			this.cmdImpFactura.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.cmdImpFactura.TextColorEnd = System.Drawing.Color.Black;
			this.cmdImpFactura.TextColorStart = System.Drawing.Color.Blue;
			this.cmdImpFactura.TextFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.cmdImpFactura.ToolTip = "";
			this.cmdImpFactura.Track = false;
			//
			//frmFactura
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(813, 408);
			this.Controls.Add(this.cmdImpFactura);
			this.Controls.Add(this.txtImporte);
			this.Controls.Add(this.DbLabel3);
			this.Controls.Add(this.txtIVA);
			this.Controls.Add(this.DbLabel2);
			this.Controls.Add(this.txtTotal);
			this.Controls.Add(this.DbLabel1);
			this.Controls.Add(this.DbControl3);
			this.Controls.Add(this.DbControl2);
			this.Controls.Add(this.DbGrid1);
			this.Controls.Add(this.DbControl1);
			this.Controls.Add(this.DbRecord1);
			this.DataControl = this.DbControl1;
			this.Name = "frmFactura";
			this.Text = "Detalle Factura";
			this.Controls.SetChildIndex(this.DbRecord1, 0);
			this.Controls.SetChildIndex(this.DbControl1, 0);
			this.Controls.SetChildIndex(this.DbGrid1, 0);
			this.Controls.SetChildIndex(this.DbControl2, 0);
			this.Controls.SetChildIndex(this.DbControl3, 0);
			this.Controls.SetChildIndex(this.DbLabel1, 0);
			this.Controls.SetChildIndex(this.txtTotal, 0);
			this.Controls.SetChildIndex(this.DbLabel2, 0);
			this.Controls.SetChildIndex(this.txtIVA, 0);
			this.Controls.SetChildIndex(this.DbLabel3, 0);
			this.Controls.SetChildIndex(this.txtImporte, 0);
			this.Controls.SetChildIndex(this.cmdImpFactura, 0);
			this.ResumeLayout(false);
			this.PerformLayout();
			
		}
		
#endregion
		
		
		//Private Sub DbGrid1_ColumnChanged(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles DbGrid1.ColumnChanged
		//    If e.Column.ColumnName.ToLower = "cantidad" Or e.Column.ColumnName.ToLower = "precio" Or e.Column.ColumnName.ToLower = "dto" Then
		//        Dim t As Double = Functions.CDbl2(e.Row.Item("cantidad")) * Functions.CDbl2(e.Row.Item("precio"))
		//        e.Row.Item("subtotal") = t - (t * (Functions.CDbl2(e.Row("dto")) / 100))
		//    End If
		//    Call CalculaTotal()
		//End Sub
		
		//Private Sub CalculaTotal()
		//    Me.txtImporte.Text = CStr(Me.DbControl2.SumColumn("subtotal") & "")
		//    Me.txtIVA.Text = CStr(Me.DbControl2.SumColumn("subtotal") * 0.16 & "")
		//    Me.txtTotal.Text = Functions.CDbl2(Me.txtImporte.Text) * 1.16
		//End Sub
		
		private void frmFactura_Load(object sender, System.EventArgs e)
		{
			//Call CalculaTotal()
			Global.AplicaSeguridad(this);
			Global.AplicaToolbar(this);
		}
		
		private void cmdImpFactura_Click(System.Object sender, System.EventArgs e)
		{
			frmImpFactura n = new frmImpFactura();
			n.MdiParent = this.MdiParent;
			n.WindowState = FormWindowState.Maximized;

			System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();

			builder.ConnectionString = FSFormControls.Global.ConnectionStringSetting.ConnectionString;

			string server = builder["Data Source"] as string;
			//string database = builder["Initial Catalog"] as string;

			n.DbReport1.Database = server;
			n.DbReport1.Selection = "{v_Pedidos.cabeceraPedido.codigo} = " + DbControl1.GetField("codigo");
			n.DbReport1.ReportFile = "Informes/rptFactura.rpt";
			n.DbReport1.Parameters = "prueba1=kk&prueba2=kki";
			n.DbReport1.Connect();
			
			n.Show();
		}
	}
	
}
