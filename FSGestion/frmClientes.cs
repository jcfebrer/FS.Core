
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System;
using System.Collections;
using System.Windows.Forms;
using FSTrace;

namespace FSGestion
{
	public class frmClientes : FSFormControls.DBForm
	{
		
#region  Código generado por el Diseñador de Windows Forms
		
		public frmClientes()
		{
			
			//El Diseñador de Windows Forms requiere esta llamada.
			InitializeComponent();
			
			this.MdiParent = Global.mdiP;
		}
		
		//Form reemplaza a Dispose para limpiar la lista de componentes.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

        private System.ComponentModel.IContainer components;

        //NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
        //Puede modificarse utilizando el Diseñador de Windows Forms.
        //No lo modifique con el editor de código.
        internal FSFormControls.DBControl DbClientes;
		internal FSFormControls.DBGrid DbGrid1;
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal FSFormControls.DBLabel DbLabel1;
		internal FSFormControls.DBLabel DbLabel2;
		internal FSFormControls.DBLabel DbLabel3;
		internal FSFormControls.DBControl DbTipoUsuario;
		internal FSFormControls.DBCombo dbcTecnicoResponsable;
		internal FSFormControls.DBButton dbbLocalizar;
		internal FSFormControls.DBTextBox txtNombreCliente;
		internal FSFormControls.DBCombo dbcTipoCliente;
		internal FSFormControls.DBButton dbbTodas;
		internal FSFormControls.DBControl DbTipoEstado;
		internal FSFormControls.DBLabel DbLabel4;
		internal FSFormControls.DBCombo dbcEstado;
		internal FSFormControls.DBColumn empDbColumn17;
		internal FSFormControls.DBColumn empDbColumn22;
		internal FSFormControls.DBColumn empDbColumn1;
		internal FSFormControls.DBColumn empDbColumn2;
		internal FSFormControls.DBColumn empDbColumn3;
		internal FSFormControls.DBColumn empDbColumn4;
		internal FSFormControls.DBColumn empDbColumn5;
		internal FSFormControls.DBColumn empDbColumn6;
		internal FSFormControls.DBColumn empDbColumn7;
		internal FSFormControls.DBColumn empDbColumn11;
		internal FSFormControls.DBControl DbTipoCliente;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.DbClientes = new FSFormControls.DBControl();
            this.DbGrid1 = new FSFormControls.DBGrid();
            this.empDbColumn17 = new FSFormControls.DBColumn();
            this.empDbColumn22 = new FSFormControls.DBColumn();
            this.DbTipoEstado = new FSFormControls.DBControl();
            this.empDbColumn1 = new FSFormControls.DBColumn();
            this.empDbColumn2 = new FSFormControls.DBColumn();
            this.empDbColumn3 = new FSFormControls.DBColumn();
            this.empDbColumn4 = new FSFormControls.DBColumn();
            this.empDbColumn5 = new FSFormControls.DBColumn();
            this.empDbColumn6 = new FSFormControls.DBColumn();
            this.empDbColumn7 = new FSFormControls.DBColumn();
            this.empDbColumn11 = new FSFormControls.DBColumn();
            this.DbTipoUsuario = new FSFormControls.DBControl();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.dbcEstado = new FSFormControls.DBCombo();
            this.DbLabel4 = new FSFormControls.DBLabel();
            this.dbbTodas = new FSFormControls.DBButton();
            this.dbcTecnicoResponsable = new FSFormControls.DBCombo();
            this.DbLabel3 = new FSFormControls.DBLabel();
            this.dbbLocalizar = new FSFormControls.DBButton();
            this.txtNombreCliente = new FSFormControls.DBTextBox();
            this.dbcTipoCliente = new FSFormControls.DBCombo();
            this.DbTipoCliente = new FSFormControls.DBControl();
            this.DbLabel2 = new FSFormControls.DBLabel();
            this.DbLabel1 = new FSFormControls.DBLabel();
            ((System.ComponentModel.ISupportInitialize)(this.DbStatusBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbStatusBarPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbStatusBarProgressPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbSBarPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbSBarPanel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbSBarPanel3)).BeginInit();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbcEstado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbcTecnicoResponsable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombreCliente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbcTipoCliente)).BeginInit();
            this.SuspendLayout();
            // 
            // DbStatusBar1
            // 
            this.DbStatusBar1.Location = new System.Drawing.Point(0, 358);
            this.DbStatusBar1.Size = new System.Drawing.Size(1065, 20);
            // 
            // DbToolBar1
            // 
            this.DbToolBar1.ShowScrollBar = false;
            this.DbToolBar1.Size = new System.Drawing.Size(1065, 50);
            // 
            // mnuForm
            // 
            this.mnuForm.OwnerDraw = true;
            // 
            // DbSBarPanel1
            // 
            this.DbSBarPanel1.Width = 804;
            // 
            // DbClientes
            // 
            this.DbClientes.About = null;
            this.DbClientes.ArrayList = null;
            this.DbClientes.AutoConnect = true;
            this.DbClientes.DataControl = null;
            this.DbClientes.DataSet = null;
            this.DbClientes.DataTable = null;
            this.DbClientes.DataView = null;
            this.DbClientes.DBFieldData = "";
            this.DbClientes.DBPosition = 0;
            this.DbClientes.EraseDBControl = null;
            this.DbClientes.Filter = "";
            this.DbClientes.isEOF = true;
            this.DbClientes.Location = new System.Drawing.Point(734, 92);
            this.DbClientes.LOCK = null;
            this.DbClientes.LOPD = null;
            this.DbClientes.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbClientes.Name = "DbClientes";
            this.DbClientes.Page = 0;
            this.DbClientes.PageSettings = null;
            this.DbClientes.Paging = false;
            this.DbClientes.PagingSize = 0;
            this.DbClientes.ReadOnly = false;
            this.DbClientes.RelationDataControl = null;
            this.DbClientes.RelationDBField = "";
            this.DbClientes.RelationParentDBField = "";
            this.DbClientes.SaveError = false;
            this.DbClientes.SaveOnChangeRecord = false;
            this.DbClientes.Selection = "select * from clientes order by estado,nombre";
            this.DbClientes.Size = new System.Drawing.Size(112, 60);
            this.DbClientes.StoreInBase64Format = false;
            this.DbClientes.TabIndex = 3;
            this.DbClientes.TableName = "clientes";
            this.DbClientes.TabStop = false;
            this.DbClientes.Text = "SQL: select * from clientes order by estado,nombre";
            this.DbClientes.Track = false;
            this.DbClientes.TypeDB = FSFormControls.DBControl.DbType.SQLServer;
            this.DbClientes.Versionable = false;
            this.DbClientes.VersionableDateField = "";
            this.DbClientes.VersionableTable = "";
            this.DbClientes.VersionableUserField = "";
            this.DbClientes.VersionableVersionField = "";
            this.DbClientes.Visible = false;
            this.DbClientes.XmlFile = "";
            this.DbClientes.XMLName = "";
            // 
            // DbGrid1
            // 
            this.DbGrid1.About = null;
            this.DbGrid1.AllowAddNew = true;
            this.DbGrid1.AllowDelete = false;
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
            this.DbGrid1.CaptionText = "Listado de clientes.";
            this.DbGrid1.CaptionVisible = true;
            this.DbGrid1.ColumnHeadersVisible = true;
            this.DbGrid1.Columns.AddRange(new FSFormControls.DBColumn[] {
            this.empDbColumn17,
            this.empDbColumn22,
            this.empDbColumn1,
            this.empDbColumn2,
            this.empDbColumn3,
            this.empDbColumn4,
            this.empDbColumn5,
            this.empDbColumn6,
            this.empDbColumn7,
            this.empDbColumn11});
            this.DbGrid1.CurrentRowIndex = -1;
            this.DbGrid1.CustomColumnHeaders = false;
            this.DbGrid1.DataControl = this.DbClientes;
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
            this.DbGrid1.Location = new System.Drawing.Point(8, 166);
            this.DbGrid1.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbGrid1.Name = "DbGrid1";
            this.DbGrid1.RecordMode = false;
            this.DbGrid1.RowHeadersVisible = true;
            this.DbGrid1.RowHeight = 16;
            this.DbGrid1.RowSel = -1;
            this.DbGrid1.RowsInCaption = 2;
            this.DbGrid1.ShowRecordScrollBar = false;
            this.DbGrid1.ShowTotals = false;
            this.DbGrid1.Size = new System.Drawing.Size(1049, 186);
            this.DbGrid1.TabIndex = 4;
            this.DbGrid1.TotalOperation = FSFormControls.DBColumn.OperationTypes.Sum;
            this.DbGrid1.Track = false;
            this.DbGrid1.XMLName = "";
            this.DbGrid1.DoubleClick += new FSFormControls.DBGrid.DoubleClickEventHandler(this.DbGrid1_DoubleClick);
            // 
            // empDbColumn17
            // 
            this.empDbColumn17.ActiveColumnDBButtonOnReadMode = true;
            this.empDbColumn17.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.empDbColumn17.AllowNull = false;
            this.empDbColumn17.AllowRowFiltering = false;
            this.empDbColumn17.AsociatedButtonColumn = -1;
            this.empDbColumn17.AsociatedComboColumn = -1;
            this.empDbColumn17.ColumnBackColor = System.Drawing.Color.Empty;
            this.empDbColumn17.ColumnDBControl = null;
            this.empDbColumn17.ColumnDBFieldData = "";
            this.empDbColumn17.ColumnForeColor = System.Drawing.Color.Empty;
            this.empDbColumn17.ColumnType = FSFormControls.DBColumn.ColumnTypes.NumberColumn;
            this.empDbColumn17.ComboBlankSelection = true;
            this.empDbColumn17.ComboImageList = null;
            this.empDbColumn17.ComboListField = "";
            this.empDbColumn17.DBGridViewFilters = null;
            this.empDbColumn17.Decimals = 0;
            this.empDbColumn17.DefaultValue = "";
            this.empDbColumn17.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.empDbColumn17.Encrypted = false;
            this.empDbColumn17.Expression = "";
            this.empDbColumn17.FieldDB = "idCliente";
            this.empDbColumn17.Font = null;
            this.empDbColumn17.Format = null;
            this.empDbColumn17.FormatString = "";
            this.empDbColumn17.HeaderCaption = "Código";
            this.empDbColumn17.Hidden = false;
            this.empDbColumn17.LastValue = false;
            this.empDbColumn17.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.empDbColumn17.MaskInput = null;
            this.empDbColumn17.MaxLength = 0;
            this.empDbColumn17.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.empDbColumn17.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.empDbColumn17.MultiLine = false;
            this.empDbColumn17.NullValue = null;
            this.empDbColumn17.Obligatory = false;
            this.empDbColumn17.PromptChar = '\0';
            this.empDbColumn17.ReadColumn = true;
            this.empDbColumn17.ShowSelectForm = true;
            this.empDbColumn17.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.empDbColumn17.ToolTip = "";
            this.empDbColumn17.Unique = false;
            this.empDbColumn17.Width = 0;
            // 
            // empDbColumn22
            // 
            this.empDbColumn22.ActiveColumnDBButtonOnReadMode = true;
            this.empDbColumn22.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.empDbColumn22.AllowNull = false;
            this.empDbColumn22.AllowRowFiltering = false;
            this.empDbColumn22.AsociatedButtonColumn = -1;
            this.empDbColumn22.AsociatedComboColumn = -1;
            this.empDbColumn22.ColumnBackColor = System.Drawing.Color.Empty;
            this.empDbColumn22.ColumnDBControl = this.DbTipoEstado;
            this.empDbColumn22.ColumnDBFieldData = "";
            this.empDbColumn22.ColumnForeColor = System.Drawing.Color.Empty;
            this.empDbColumn22.ColumnType = FSFormControls.DBColumn.ColumnTypes.ComboColumn;
            this.empDbColumn22.ComboBlankSelection = true;
            this.empDbColumn22.ComboImageList = null;
            this.empDbColumn22.ComboListField = "descripcion";
            this.empDbColumn22.DBGridViewFilters = null;
            this.empDbColumn22.Decimals = 2;
            this.empDbColumn22.DefaultValue = "";
            this.empDbColumn22.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.empDbColumn22.Encrypted = false;
            this.empDbColumn22.Expression = "";
            this.empDbColumn22.FieldDB = "estado";
            this.empDbColumn22.Font = null;
            this.empDbColumn22.Format = null;
            this.empDbColumn22.FormatString = "";
            this.empDbColumn22.HeaderCaption = "Estado";
            this.empDbColumn22.Hidden = false;
            this.empDbColumn22.LastValue = false;
            this.empDbColumn22.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.empDbColumn22.MaskInput = null;
            this.empDbColumn22.MaxLength = 0;
            this.empDbColumn22.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.empDbColumn22.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.empDbColumn22.MultiLine = false;
            this.empDbColumn22.NullValue = null;
            this.empDbColumn22.Obligatory = false;
            this.empDbColumn22.PromptChar = '\0';
            this.empDbColumn22.ReadColumn = false;
            this.empDbColumn22.ShowSelectForm = true;
            this.empDbColumn22.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.empDbColumn22.ToolTip = "";
            this.empDbColumn22.Unique = false;
            this.empDbColumn22.Width = 0;
            // 
            // DbTipoEstado
            // 
            this.DbTipoEstado.About = null;
            this.DbTipoEstado.ArrayList = null;
            this.DbTipoEstado.AutoConnect = true;
            this.DbTipoEstado.DataControl = null;
            this.DbTipoEstado.DataSet = null;
            this.DbTipoEstado.DataTable = null;
            this.DbTipoEstado.DataView = null;
            this.DbTipoEstado.DBFieldData = "";
            this.DbTipoEstado.DBPosition = 0;
            this.DbTipoEstado.EraseDBControl = null;
            this.DbTipoEstado.Filter = "";
            this.DbTipoEstado.isEOF = true;
            this.DbTipoEstado.Location = new System.Drawing.Point(622, 114);
            this.DbTipoEstado.LOCK = null;
            this.DbTipoEstado.LOPD = null;
            this.DbTipoEstado.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbTipoEstado.Name = "DbTipoEstado";
            this.DbTipoEstado.Page = 0;
            this.DbTipoEstado.PageSettings = null;
            this.DbTipoEstado.Paging = false;
            this.DbTipoEstado.PagingSize = 0;
            this.DbTipoEstado.ReadOnly = false;
            this.DbTipoEstado.RelationDataControl = null;
            this.DbTipoEstado.RelationDBField = "";
            this.DbTipoEstado.RelationParentDBField = "";
            this.DbTipoEstado.SaveError = false;
            this.DbTipoEstado.SaveOnChangeRecord = false;
            this.DbTipoEstado.Selection = "select * from tipoEstado";
            this.DbTipoEstado.Size = new System.Drawing.Size(74, 53);
            this.DbTipoEstado.StoreInBase64Format = false;
            this.DbTipoEstado.TabIndex = 8;
            this.DbTipoEstado.TableName = "tipoEstado";
            this.DbTipoEstado.TabStop = false;
            this.DbTipoEstado.Text = "SQL: select * from tipoEstado";
            this.DbTipoEstado.Track = false;
            this.DbTipoEstado.TypeDB = FSFormControls.DBControl.DbType.SQLServer;
            this.DbTipoEstado.Versionable = false;
            this.DbTipoEstado.VersionableDateField = "";
            this.DbTipoEstado.VersionableTable = "";
            this.DbTipoEstado.VersionableUserField = "";
            this.DbTipoEstado.VersionableVersionField = "";
            this.DbTipoEstado.Visible = false;
            this.DbTipoEstado.XmlFile = "";
            this.DbTipoEstado.XMLName = "";
            // 
            // empDbColumn1
            // 
            this.empDbColumn1.ActiveColumnDBButtonOnReadMode = true;
            this.empDbColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.empDbColumn1.AllowNull = false;
            this.empDbColumn1.AllowRowFiltering = false;
            this.empDbColumn1.AsociatedButtonColumn = -1;
            this.empDbColumn1.AsociatedComboColumn = -1;
            this.empDbColumn1.ColumnBackColor = System.Drawing.Color.Empty;
            this.empDbColumn1.ColumnDBControl = null;
            this.empDbColumn1.ColumnDBFieldData = "";
            this.empDbColumn1.ColumnForeColor = System.Drawing.Color.Empty;
            this.empDbColumn1.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.empDbColumn1.ComboBlankSelection = true;
            this.empDbColumn1.ComboImageList = null;
            this.empDbColumn1.ComboListField = "";
            this.empDbColumn1.DBGridViewFilters = null;
            this.empDbColumn1.Decimals = 2;
            this.empDbColumn1.DefaultValue = "";
            this.empDbColumn1.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.empDbColumn1.Encrypted = false;
            this.empDbColumn1.Expression = "";
            this.empDbColumn1.FieldDB = "nombre";
            this.empDbColumn1.Font = null;
            this.empDbColumn1.Format = null;
            this.empDbColumn1.FormatString = "";
            this.empDbColumn1.HeaderCaption = "Nombre";
            this.empDbColumn1.Hidden = false;
            this.empDbColumn1.LastValue = false;
            this.empDbColumn1.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.empDbColumn1.MaskInput = null;
            this.empDbColumn1.MaxLength = 0;
            this.empDbColumn1.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.empDbColumn1.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.empDbColumn1.MultiLine = false;
            this.empDbColumn1.NullValue = null;
            this.empDbColumn1.Obligatory = false;
            this.empDbColumn1.PromptChar = '\0';
            this.empDbColumn1.ReadColumn = false;
            this.empDbColumn1.ShowSelectForm = true;
            this.empDbColumn1.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.empDbColumn1.ToolTip = "";
            this.empDbColumn1.Unique = false;
            this.empDbColumn1.Width = 0;
            // 
            // empDbColumn2
            // 
            this.empDbColumn2.ActiveColumnDBButtonOnReadMode = true;
            this.empDbColumn2.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.empDbColumn2.AllowNull = false;
            this.empDbColumn2.AllowRowFiltering = false;
            this.empDbColumn2.AsociatedButtonColumn = -1;
            this.empDbColumn2.AsociatedComboColumn = -1;
            this.empDbColumn2.ColumnBackColor = System.Drawing.Color.Empty;
            this.empDbColumn2.ColumnDBControl = null;
            this.empDbColumn2.ColumnDBFieldData = "";
            this.empDbColumn2.ColumnForeColor = System.Drawing.Color.Empty;
            this.empDbColumn2.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.empDbColumn2.ComboBlankSelection = true;
            this.empDbColumn2.ComboImageList = null;
            this.empDbColumn2.ComboListField = "";
            this.empDbColumn2.DBGridViewFilters = null;
            this.empDbColumn2.Decimals = 2;
            this.empDbColumn2.DefaultValue = "";
            this.empDbColumn2.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.empDbColumn2.Encrypted = false;
            this.empDbColumn2.Expression = "";
            this.empDbColumn2.FieldDB = "direccion";
            this.empDbColumn2.Font = null;
            this.empDbColumn2.Format = null;
            this.empDbColumn2.FormatString = "";
            this.empDbColumn2.HeaderCaption = "Dirección";
            this.empDbColumn2.Hidden = false;
            this.empDbColumn2.LastValue = false;
            this.empDbColumn2.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.empDbColumn2.MaskInput = null;
            this.empDbColumn2.MaxLength = 0;
            this.empDbColumn2.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.empDbColumn2.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.empDbColumn2.MultiLine = false;
            this.empDbColumn2.NullValue = null;
            this.empDbColumn2.Obligatory = false;
            this.empDbColumn2.PromptChar = '\0';
            this.empDbColumn2.ReadColumn = false;
            this.empDbColumn2.ShowSelectForm = true;
            this.empDbColumn2.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.empDbColumn2.ToolTip = "";
            this.empDbColumn2.Unique = false;
            this.empDbColumn2.Width = 0;
            // 
            // empDbColumn3
            // 
            this.empDbColumn3.ActiveColumnDBButtonOnReadMode = true;
            this.empDbColumn3.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.empDbColumn3.AllowNull = false;
            this.empDbColumn3.AllowRowFiltering = false;
            this.empDbColumn3.AsociatedButtonColumn = -1;
            this.empDbColumn3.AsociatedComboColumn = -1;
            this.empDbColumn3.ColumnBackColor = System.Drawing.Color.Empty;
            this.empDbColumn3.ColumnDBControl = null;
            this.empDbColumn3.ColumnDBFieldData = "";
            this.empDbColumn3.ColumnForeColor = System.Drawing.Color.Empty;
            this.empDbColumn3.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.empDbColumn3.ComboBlankSelection = true;
            this.empDbColumn3.ComboImageList = null;
            this.empDbColumn3.ComboListField = "";
            this.empDbColumn3.DBGridViewFilters = null;
            this.empDbColumn3.Decimals = 2;
            this.empDbColumn3.DefaultValue = "";
            this.empDbColumn3.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.empDbColumn3.Encrypted = false;
            this.empDbColumn3.Expression = "";
            this.empDbColumn3.FieldDB = "telefono1";
            this.empDbColumn3.Font = null;
            this.empDbColumn3.Format = null;
            this.empDbColumn3.FormatString = "";
            this.empDbColumn3.HeaderCaption = "Teléfono 1";
            this.empDbColumn3.Hidden = false;
            this.empDbColumn3.LastValue = false;
            this.empDbColumn3.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.empDbColumn3.MaskInput = null;
            this.empDbColumn3.MaxLength = 0;
            this.empDbColumn3.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.empDbColumn3.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.empDbColumn3.MultiLine = false;
            this.empDbColumn3.NullValue = null;
            this.empDbColumn3.Obligatory = false;
            this.empDbColumn3.PromptChar = '\0';
            this.empDbColumn3.ReadColumn = false;
            this.empDbColumn3.ShowSelectForm = true;
            this.empDbColumn3.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.empDbColumn3.ToolTip = "";
            this.empDbColumn3.Unique = false;
            this.empDbColumn3.Width = 0;
            // 
            // empDbColumn4
            // 
            this.empDbColumn4.ActiveColumnDBButtonOnReadMode = true;
            this.empDbColumn4.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.empDbColumn4.AllowNull = false;
            this.empDbColumn4.AllowRowFiltering = false;
            this.empDbColumn4.AsociatedButtonColumn = -1;
            this.empDbColumn4.AsociatedComboColumn = -1;
            this.empDbColumn4.ColumnBackColor = System.Drawing.Color.Empty;
            this.empDbColumn4.ColumnDBControl = null;
            this.empDbColumn4.ColumnDBFieldData = "";
            this.empDbColumn4.ColumnForeColor = System.Drawing.Color.Empty;
            this.empDbColumn4.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.empDbColumn4.ComboBlankSelection = true;
            this.empDbColumn4.ComboImageList = null;
            this.empDbColumn4.ComboListField = "";
            this.empDbColumn4.DBGridViewFilters = null;
            this.empDbColumn4.Decimals = 2;
            this.empDbColumn4.DefaultValue = "";
            this.empDbColumn4.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.empDbColumn4.Encrypted = false;
            this.empDbColumn4.Expression = "";
            this.empDbColumn4.FieldDB = "telefono2";
            this.empDbColumn4.Font = null;
            this.empDbColumn4.Format = null;
            this.empDbColumn4.FormatString = "";
            this.empDbColumn4.HeaderCaption = "Teléfono 2";
            this.empDbColumn4.Hidden = false;
            this.empDbColumn4.LastValue = false;
            this.empDbColumn4.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.empDbColumn4.MaskInput = null;
            this.empDbColumn4.MaxLength = 0;
            this.empDbColumn4.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.empDbColumn4.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.empDbColumn4.MultiLine = false;
            this.empDbColumn4.NullValue = null;
            this.empDbColumn4.Obligatory = false;
            this.empDbColumn4.PromptChar = '\0';
            this.empDbColumn4.ReadColumn = false;
            this.empDbColumn4.ShowSelectForm = true;
            this.empDbColumn4.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.empDbColumn4.ToolTip = "";
            this.empDbColumn4.Unique = false;
            this.empDbColumn4.Width = 0;
            // 
            // empDbColumn5
            // 
            this.empDbColumn5.ActiveColumnDBButtonOnReadMode = true;
            this.empDbColumn5.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.empDbColumn5.AllowNull = false;
            this.empDbColumn5.AllowRowFiltering = false;
            this.empDbColumn5.AsociatedButtonColumn = -1;
            this.empDbColumn5.AsociatedComboColumn = -1;
            this.empDbColumn5.ColumnBackColor = System.Drawing.Color.Empty;
            this.empDbColumn5.ColumnDBControl = null;
            this.empDbColumn5.ColumnDBFieldData = "";
            this.empDbColumn5.ColumnForeColor = System.Drawing.Color.Empty;
            this.empDbColumn5.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.empDbColumn5.ComboBlankSelection = true;
            this.empDbColumn5.ComboImageList = null;
            this.empDbColumn5.ComboListField = "";
            this.empDbColumn5.DBGridViewFilters = null;
            this.empDbColumn5.Decimals = 2;
            this.empDbColumn5.DefaultValue = "";
            this.empDbColumn5.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.empDbColumn5.Encrypted = false;
            this.empDbColumn5.Expression = "";
            this.empDbColumn5.FieldDB = "fax";
            this.empDbColumn5.Font = null;
            this.empDbColumn5.Format = null;
            this.empDbColumn5.FormatString = "";
            this.empDbColumn5.HeaderCaption = "Fax";
            this.empDbColumn5.Hidden = false;
            this.empDbColumn5.LastValue = false;
            this.empDbColumn5.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.empDbColumn5.MaskInput = null;
            this.empDbColumn5.MaxLength = 0;
            this.empDbColumn5.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.empDbColumn5.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.empDbColumn5.MultiLine = false;
            this.empDbColumn5.NullValue = null;
            this.empDbColumn5.Obligatory = false;
            this.empDbColumn5.PromptChar = '\0';
            this.empDbColumn5.ReadColumn = false;
            this.empDbColumn5.ShowSelectForm = true;
            this.empDbColumn5.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.empDbColumn5.ToolTip = "";
            this.empDbColumn5.Unique = false;
            this.empDbColumn5.Width = 0;
            // 
            // empDbColumn6
            // 
            this.empDbColumn6.ActiveColumnDBButtonOnReadMode = true;
            this.empDbColumn6.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.empDbColumn6.AllowNull = false;
            this.empDbColumn6.AllowRowFiltering = false;
            this.empDbColumn6.AsociatedButtonColumn = -1;
            this.empDbColumn6.AsociatedComboColumn = -1;
            this.empDbColumn6.ColumnBackColor = System.Drawing.Color.Empty;
            this.empDbColumn6.ColumnDBControl = null;
            this.empDbColumn6.ColumnDBFieldData = "";
            this.empDbColumn6.ColumnForeColor = System.Drawing.Color.Empty;
            this.empDbColumn6.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.empDbColumn6.ComboBlankSelection = true;
            this.empDbColumn6.ComboImageList = null;
            this.empDbColumn6.ComboListField = "";
            this.empDbColumn6.DBGridViewFilters = null;
            this.empDbColumn6.Decimals = 2;
            this.empDbColumn6.DefaultValue = "";
            this.empDbColumn6.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.empDbColumn6.Encrypted = false;
            this.empDbColumn6.Expression = "";
            this.empDbColumn6.FieldDB = "personaContacto";
            this.empDbColumn6.Font = null;
            this.empDbColumn6.Format = null;
            this.empDbColumn6.FormatString = "";
            this.empDbColumn6.HeaderCaption = "Persona de Contacto";
            this.empDbColumn6.Hidden = false;
            this.empDbColumn6.LastValue = false;
            this.empDbColumn6.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.empDbColumn6.MaskInput = null;
            this.empDbColumn6.MaxLength = 0;
            this.empDbColumn6.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.empDbColumn6.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.empDbColumn6.MultiLine = false;
            this.empDbColumn6.NullValue = null;
            this.empDbColumn6.Obligatory = false;
            this.empDbColumn6.PromptChar = '\0';
            this.empDbColumn6.ReadColumn = false;
            this.empDbColumn6.ShowSelectForm = true;
            this.empDbColumn6.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.empDbColumn6.ToolTip = "";
            this.empDbColumn6.Unique = false;
            this.empDbColumn6.Width = 0;
            // 
            // empDbColumn7
            // 
            this.empDbColumn7.ActiveColumnDBButtonOnReadMode = true;
            this.empDbColumn7.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.empDbColumn7.AllowNull = false;
            this.empDbColumn7.AllowRowFiltering = false;
            this.empDbColumn7.AsociatedButtonColumn = -1;
            this.empDbColumn7.AsociatedComboColumn = -1;
            this.empDbColumn7.ColumnBackColor = System.Drawing.Color.Empty;
            this.empDbColumn7.ColumnDBControl = null;
            this.empDbColumn7.ColumnDBFieldData = "";
            this.empDbColumn7.ColumnForeColor = System.Drawing.Color.Empty;
            this.empDbColumn7.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.empDbColumn7.ComboBlankSelection = true;
            this.empDbColumn7.ComboImageList = null;
            this.empDbColumn7.ComboListField = "";
            this.empDbColumn7.DBGridViewFilters = null;
            this.empDbColumn7.Decimals = 2;
            this.empDbColumn7.DefaultValue = "";
            this.empDbColumn7.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.empDbColumn7.Encrypted = false;
            this.empDbColumn7.Expression = "";
            this.empDbColumn7.FieldDB = "cif";
            this.empDbColumn7.Font = null;
            this.empDbColumn7.Format = null;
            this.empDbColumn7.FormatString = "";
            this.empDbColumn7.HeaderCaption = "C.I.F.";
            this.empDbColumn7.Hidden = false;
            this.empDbColumn7.LastValue = false;
            this.empDbColumn7.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.empDbColumn7.MaskInput = null;
            this.empDbColumn7.MaxLength = 0;
            this.empDbColumn7.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.empDbColumn7.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.empDbColumn7.MultiLine = false;
            this.empDbColumn7.NullValue = null;
            this.empDbColumn7.Obligatory = false;
            this.empDbColumn7.PromptChar = '\0';
            this.empDbColumn7.ReadColumn = false;
            this.empDbColumn7.ShowSelectForm = true;
            this.empDbColumn7.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.empDbColumn7.ToolTip = "";
            this.empDbColumn7.Unique = false;
            this.empDbColumn7.Width = 0;
            // 
            // empDbColumn11
            // 
            this.empDbColumn11.ActiveColumnDBButtonOnReadMode = true;
            this.empDbColumn11.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.empDbColumn11.AllowNull = false;
            this.empDbColumn11.AllowRowFiltering = false;
            this.empDbColumn11.AsociatedButtonColumn = -1;
            this.empDbColumn11.AsociatedComboColumn = -1;
            this.empDbColumn11.ColumnBackColor = System.Drawing.Color.Empty;
            this.empDbColumn11.ColumnDBControl = this.DbTipoUsuario;
            this.empDbColumn11.ColumnDBFieldData = "";
            this.empDbColumn11.ColumnForeColor = System.Drawing.Color.Empty;
            this.empDbColumn11.ColumnType = FSFormControls.DBColumn.ColumnTypes.ComboColumn;
            this.empDbColumn11.ComboBlankSelection = true;
            this.empDbColumn11.ComboImageList = null;
            this.empDbColumn11.ComboListField = "nombre";
            this.empDbColumn11.DBGridViewFilters = null;
            this.empDbColumn11.Decimals = 2;
            this.empDbColumn11.DefaultValue = "";
            this.empDbColumn11.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.empDbColumn11.Encrypted = false;
            this.empDbColumn11.Expression = "";
            this.empDbColumn11.FieldDB = "tecnicoResponsable";
            this.empDbColumn11.Font = null;
            this.empDbColumn11.Format = null;
            this.empDbColumn11.FormatString = "";
            this.empDbColumn11.HeaderCaption = "Comercial";
            this.empDbColumn11.Hidden = false;
            this.empDbColumn11.LastValue = false;
            this.empDbColumn11.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.empDbColumn11.MaskInput = null;
            this.empDbColumn11.MaxLength = 0;
            this.empDbColumn11.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.empDbColumn11.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.empDbColumn11.MultiLine = false;
            this.empDbColumn11.NullValue = null;
            this.empDbColumn11.Obligatory = false;
            this.empDbColumn11.PromptChar = '\0';
            this.empDbColumn11.ReadColumn = false;
            this.empDbColumn11.ShowSelectForm = true;
            this.empDbColumn11.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.empDbColumn11.ToolTip = "";
            this.empDbColumn11.Unique = false;
            this.empDbColumn11.Width = 250;
            // 
            // DbTipoUsuario
            // 
            this.DbTipoUsuario.About = null;
            this.DbTipoUsuario.ArrayList = null;
            this.DbTipoUsuario.AutoConnect = true;
            this.DbTipoUsuario.DataControl = null;
            this.DbTipoUsuario.DataSet = null;
            this.DbTipoUsuario.DataTable = null;
            this.DbTipoUsuario.DataView = null;
            this.DbTipoUsuario.DBFieldData = "";
            this.DbTipoUsuario.DBPosition = 0;
            this.DbTipoUsuario.EraseDBControl = null;
            this.DbTipoUsuario.Filter = "";
            this.DbTipoUsuario.isEOF = true;
            this.DbTipoUsuario.Location = new System.Drawing.Point(558, 53);
            this.DbTipoUsuario.LOCK = null;
            this.DbTipoUsuario.LOPD = null;
            this.DbTipoUsuario.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbTipoUsuario.Name = "DbTipoUsuario";
            this.DbTipoUsuario.Page = 0;
            this.DbTipoUsuario.PageSettings = null;
            this.DbTipoUsuario.Paging = false;
            this.DbTipoUsuario.PagingSize = 0;
            this.DbTipoUsuario.ReadOnly = false;
            this.DbTipoUsuario.RelationDataControl = null;
            this.DbTipoUsuario.RelationDBField = "";
            this.DbTipoUsuario.RelationParentDBField = "";
            this.DbTipoUsuario.SaveError = false;
            this.DbTipoUsuario.SaveOnChangeRecord = false;
            this.DbTipoUsuario.Selection = "select * from usuarios where tipoUsuario=1";
            this.DbTipoUsuario.Size = new System.Drawing.Size(80, 64);
            this.DbTipoUsuario.StoreInBase64Format = false;
            this.DbTipoUsuario.TabIndex = 7;
            this.DbTipoUsuario.TableName = "usuarios";
            this.DbTipoUsuario.TabStop = false;
            this.DbTipoUsuario.Text = "SQL: select * from usuarios where tipoUsuario=1";
            this.DbTipoUsuario.Track = false;
            this.DbTipoUsuario.TypeDB = FSFormControls.DBControl.DbType.SQLServer;
            this.DbTipoUsuario.Versionable = false;
            this.DbTipoUsuario.VersionableDateField = "";
            this.DbTipoUsuario.VersionableTable = "";
            this.DbTipoUsuario.VersionableUserField = "";
            this.DbTipoUsuario.VersionableVersionField = "";
            this.DbTipoUsuario.Visible = false;
            this.DbTipoUsuario.XmlFile = "";
            this.DbTipoUsuario.XMLName = "";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.dbcEstado);
            this.GroupBox1.Controls.Add(this.DbLabel4);
            this.GroupBox1.Controls.Add(this.dbbTodas);
            this.GroupBox1.Controls.Add(this.dbcTecnicoResponsable);
            this.GroupBox1.Controls.Add(this.DbLabel3);
            this.GroupBox1.Controls.Add(this.dbbLocalizar);
            this.GroupBox1.Controls.Add(this.txtNombreCliente);
            this.GroupBox1.Controls.Add(this.dbcTipoCliente);
            this.GroupBox1.Controls.Add(this.DbLabel2);
            this.GroupBox1.Controls.Add(this.DbLabel1);
            this.GroupBox1.Location = new System.Drawing.Point(8, 56);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(544, 104);
            this.GroupBox1.TabIndex = 5;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Selección:";
            // 
            // dbcEstado
            // 
            this.dbcEstado.About = null;
            this.dbcEstado.Appearance = null;
            this.dbcEstado.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.dbcEstado.BlankSelection = false;
            this.dbcEstado.DataControl = null;
            this.dbcEstado.DataControlList = this.DbTipoEstado;
            this.dbcEstado.DBFieldData = "";
            this.dbcEstado.DBFieldList = "descripcion";
            this.dbcEstado.DisplayMember = "";
            this.dbcEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dbcEstado.DroppedDown = false;
            this.dbcEstado.Editable = true;
            this.dbcEstado.GridMode = false;
            this.dbcEstado.IsInEditMode = true;
            this.dbcEstado.Location = new System.Drawing.Point(377, 22);
            this.dbcEstado.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
            this.dbcEstado.Name = "dbcEstado";
            this.dbcEstado.Obligatory = false;
            this.dbcEstado.OrderBy = null;
            this.dbcEstado.ReadOnly = false;
            this.dbcEstado.SelectedIndex = -1;
            this.dbcEstado.SelectedItem = null;
            this.dbcEstado.SelectedOption = null;
            this.dbcEstado.SelectedValue = null;
            this.dbcEstado.ShowCode = false;
            this.dbcEstado.ShowEdit = true;
            this.dbcEstado.Size = new System.Drawing.Size(143, 21);
            this.dbcEstado.Sort = true;
            this.dbcEstado.SortStyle = FSFormControls.DBCombo.SortStyleEnum.Ascending;
            this.dbcEstado.TabIndex = 11;
            this.dbcEstado.Track = false;
            this.dbcEstado.Value = null;
            this.dbcEstado.ValueMember = "";
            // 
            // DbLabel4
            // 
            this.DbLabel4.About = null;
            this.DbLabel4.Angle = 0F;
            this.DbLabel4.Appearance = null;
            this.DbLabel4.AutoSize = true;
            this.DbLabel4.BackColor = System.Drawing.Color.Transparent;
            this.DbLabel4.BorderStyleInner = System.Windows.Forms.BorderStyle.None;
            this.DbLabel4.BorderStyleOuter = System.Windows.Forms.BorderStyle.None;
            this.DbLabel4.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
            this.DbLabel4.DataControl = null;
            this.DbLabel4.DataType = FSFormControls.DBTextBox.TypeData.All;
            this.DbLabel4.DateFormat = "dd/MM/yyyy";
            this.DbLabel4.Decimals = 2;
            this.DbLabel4.EndColor = System.Drawing.Color.LightSkyBlue;
            this.DbLabel4.Location = new System.Drawing.Point(328, 24);
            this.DbLabel4.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbLabel4.Name = "DbLabel4";
            this.DbLabel4.ShadowColor = System.Drawing.Color.Black;
            this.DbLabel4.Size = new System.Drawing.Size(43, 13);
            this.DbLabel4.StartColor = System.Drawing.Color.White;
            this.DbLabel4.TabIndex = 10;
            this.DbLabel4.TabStop = false;
            this.DbLabel4.Text = "Estado:";
            this.DbLabel4.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.DbLabel4.Track = false;
            this.DbLabel4.XOffset = 1F;
            this.DbLabel4.YOffset = 1F;
            // 
            // dbbTodas
            // 
            this.dbbTodas.About = null;
            this.dbbTodas.Appearance = null;
            this.dbbTodas.ButtonStyle = FSFormControls.DBButton.ButtonStyleType.Normal;
            this.dbbTodas.DataControl = null;
            this.dbbTodas.DialogResult = System.Windows.Forms.DialogResult.None;
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
            this.dbbTodas.Key = "Button1";
            this.dbbTodas.Location = new System.Drawing.Point(377, 48);
            this.dbbTodas.Name = "dbbTodas";
            this.dbbTodas.Size = new System.Drawing.Size(143, 24);
            this.dbbTodas.TabIndex = 9;
            this.dbbTodas.Text = "Mostrar Todas";
            this.dbbTodas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dbbTodas.TextColorEnd = System.Drawing.Color.Black;
            this.dbbTodas.TextColorStart = System.Drawing.Color.Blue;
            this.dbbTodas.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dbbTodas.ToolTip = "";
            this.dbbTodas.Track = false;
            this.dbbTodas.Click += new System.EventHandler(this.dbbTodas_Click);
            // 
            // dbcTecnicoResponsable
            // 
            this.dbcTecnicoResponsable.About = null;
            this.dbcTecnicoResponsable.Appearance = null;
            this.dbcTecnicoResponsable.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.dbcTecnicoResponsable.BlankSelection = true;
            this.dbcTecnicoResponsable.DataControl = null;
            this.dbcTecnicoResponsable.DataControlList = this.DbTipoUsuario;
            this.dbcTecnicoResponsable.DBFieldData = "id";
            this.dbcTecnicoResponsable.DBFieldList = "nombre";
            this.dbcTecnicoResponsable.DisplayMember = "";
            this.dbcTecnicoResponsable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dbcTecnicoResponsable.DroppedDown = false;
            this.dbcTecnicoResponsable.Editable = true;
            this.dbcTecnicoResponsable.GridMode = false;
            this.dbcTecnicoResponsable.IsInEditMode = true;
            this.dbcTecnicoResponsable.Location = new System.Drawing.Point(109, 72);
            this.dbcTecnicoResponsable.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
            this.dbcTecnicoResponsable.Name = "dbcTecnicoResponsable";
            this.dbcTecnicoResponsable.Obligatory = false;
            this.dbcTecnicoResponsable.OrderBy = null;
            this.dbcTecnicoResponsable.ReadOnly = false;
            this.dbcTecnicoResponsable.SelectedIndex = -1;
            this.dbcTecnicoResponsable.SelectedItem = null;
            this.dbcTecnicoResponsable.SelectedOption = null;
            this.dbcTecnicoResponsable.SelectedValue = null;
            this.dbcTecnicoResponsable.ShowCode = false;
            this.dbcTecnicoResponsable.ShowEdit = true;
            this.dbcTecnicoResponsable.Size = new System.Drawing.Size(262, 21);
            this.dbcTecnicoResponsable.Sort = true;
            this.dbcTecnicoResponsable.SortStyle = FSFormControls.DBCombo.SortStyleEnum.Ascending;
            this.dbcTecnicoResponsable.TabIndex = 6;
            this.dbcTecnicoResponsable.Track = false;
            this.dbcTecnicoResponsable.Value = null;
            this.dbcTecnicoResponsable.ValueMember = "";
            // 
            // DbLabel3
            // 
            this.DbLabel3.About = null;
            this.DbLabel3.Angle = 0F;
            this.DbLabel3.Appearance = null;
            this.DbLabel3.AutoSize = true;
            this.DbLabel3.BackColor = System.Drawing.Color.Transparent;
            this.DbLabel3.BorderStyleInner = System.Windows.Forms.BorderStyle.None;
            this.DbLabel3.BorderStyleOuter = System.Windows.Forms.BorderStyle.None;
            this.DbLabel3.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
            this.DbLabel3.DataControl = null;
            this.DbLabel3.DataType = FSFormControls.DBTextBox.TypeData.All;
            this.DbLabel3.DateFormat = "dd/MM/yyyy";
            this.DbLabel3.Decimals = 2;
            this.DbLabel3.EndColor = System.Drawing.Color.LightSkyBlue;
            this.DbLabel3.Location = new System.Drawing.Point(19, 72);
            this.DbLabel3.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbLabel3.Name = "DbLabel3";
            this.DbLabel3.ShadowColor = System.Drawing.Color.Black;
            this.DbLabel3.Size = new System.Drawing.Size(56, 13);
            this.DbLabel3.StartColor = System.Drawing.Color.White;
            this.DbLabel3.TabIndex = 5;
            this.DbLabel3.TabStop = false;
            this.DbLabel3.Text = "Comercial:";
            this.DbLabel3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.DbLabel3.Track = false;
            this.DbLabel3.XOffset = 1F;
            this.DbLabel3.YOffset = 1F;
            // 
            // dbbLocalizar
            // 
            this.dbbLocalizar.About = null;
            this.dbbLocalizar.Appearance = null;
            this.dbbLocalizar.ButtonStyle = FSFormControls.DBButton.ButtonStyleType.Normal;
            this.dbbLocalizar.DataControl = null;
            this.dbbLocalizar.DialogResult = System.Windows.Forms.DialogResult.None;
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
            this.dbbLocalizar.Key = "Button1";
            this.dbbLocalizar.Location = new System.Drawing.Point(377, 72);
            this.dbbLocalizar.Name = "dbbLocalizar";
            this.dbbLocalizar.Size = new System.Drawing.Size(143, 24);
            this.dbbLocalizar.TabIndex = 4;
            this.dbbLocalizar.Text = "Localizar";
            this.dbbLocalizar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dbbLocalizar.TextColorEnd = System.Drawing.Color.Black;
            this.dbbLocalizar.TextColorStart = System.Drawing.Color.Blue;
            this.dbbLocalizar.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dbbLocalizar.ToolTip = "";
            this.dbbLocalizar.Track = false;
            this.dbbLocalizar.Click += new System.EventHandler(this.dbbLocalizar_Click);
            // 
            // txtNombreCliente
            // 
            this.txtNombreCliente.About = null;
            this.txtNombreCliente.AcceptsReturn = false;
            this.txtNombreCliente.Appearance = null;
            this.txtNombreCliente.AsociatedCombo = null;
            this.txtNombreCliente.AsociatedDBFindTextBox = null;
            this.txtNombreCliente.BackColorRead = System.Drawing.Color.WhiteSmoke;
            this.txtNombreCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtNombreCliente.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
            this.txtNombreCliente.DataControl = null;
            this.txtNombreCliente.DataType = FSFormControls.DBTextBox.TypeData.All;
            this.txtNombreCliente.DateFormat = "dd/mm/yyyy";
            this.txtNombreCliente.DBField = null;
            this.txtNombreCliente.DBFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreCliente.Decimals = 2;
            this.txtNombreCliente.DefaultValue = null;
            this.txtNombreCliente.DotNumber = false;
            this.txtNombreCliente.Editable = true;
            this.txtNombreCliente.EditAs = FSFormControls.DBTextBox.EditAsType.UseSpecifiedMask;
            this.txtNombreCliente.Encrypted = false;
            this.txtNombreCliente.Expression = "";
            this.txtNombreCliente.FireTextChanged = true;
            this.txtNombreCliente.FormatString = "";
            this.txtNombreCliente.GridOperation = FSFormControls.DBColumn.OperationTypes.Sum;
            this.txtNombreCliente.InputMask = null;
            this.txtNombreCliente.Location = new System.Drawing.Point(109, 24);
            this.txtNombreCliente.MaskInput = null;
            this.txtNombreCliente.MaxLength = 32767;
            this.txtNombreCliente.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.txtNombreCliente.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.txtNombreCliente.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
            this.txtNombreCliente.Multiline = false;
            this.txtNombreCliente.Name = "txtNombreCliente";
            this.txtNombreCliente.NonAutoSizeHeight = 0;
            this.txtNombreCliente.NumericType = FSFormControls.DBTextBox.NumericTypeEnum.Double;
            this.txtNombreCliente.Obligatory = false;
            this.txtNombreCliente.PasswordChar = '\0';
            this.txtNombreCliente.PromptChar = '\0';
            this.txtNombreCliente.ReadOnly = false;
            this.txtNombreCliente.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNombreCliente.SelectAllBehavior = FSFormControls.DBTextBox.SelectAllBehaviorEnum.SelectAllCharacters;
            this.txtNombreCliente.SelectionLength = 0;
            this.txtNombreCliente.SelectionStart = 0;
            this.txtNombreCliente.SendCommaAsPoint = true;
            this.txtNombreCliente.SendTabAsEnter = true;
            this.txtNombreCliente.Shadow = false;
            this.txtNombreCliente.ShadowColor = System.Drawing.Color.Gray;
            this.txtNombreCliente.ShadowSize = 4;
            this.txtNombreCliente.ShowAsCombo = false;
            this.txtNombreCliente.ShowKeyboard = false;
            this.txtNombreCliente.ShowScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNombreCliente.Size = new System.Drawing.Size(203, 20);
            this.txtNombreCliente.TabIndex = 3;
            this.txtNombreCliente.TabNavigation = FSFormControls.DBTextBox.TabNavigationEnum.NextControl;
            this.txtNombreCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNombreCliente.ToolTip = "";
            this.txtNombreCliente.Track = false;
            this.txtNombreCliente.Value = "";
            this.txtNombreCliente.XMLName = null;
            // 
            // dbcTipoCliente
            // 
            this.dbcTipoCliente.About = null;
            this.dbcTipoCliente.Appearance = null;
            this.dbcTipoCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.dbcTipoCliente.BlankSelection = true;
            this.dbcTipoCliente.DataControl = null;
            this.dbcTipoCliente.DataControlList = this.DbTipoCliente;
            this.dbcTipoCliente.DBFieldData = "id";
            this.dbcTipoCliente.DBFieldList = "descripcion";
            this.dbcTipoCliente.DisplayMember = "";
            this.dbcTipoCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.dbcTipoCliente.DroppedDown = false;
            this.dbcTipoCliente.Editable = true;
            this.dbcTipoCliente.GridMode = false;
            this.dbcTipoCliente.IsInEditMode = true;
            this.dbcTipoCliente.Location = new System.Drawing.Point(109, 48);
            this.dbcTipoCliente.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
            this.dbcTipoCliente.Name = "dbcTipoCliente";
            this.dbcTipoCliente.Obligatory = false;
            this.dbcTipoCliente.OrderBy = null;
            this.dbcTipoCliente.ReadOnly = false;
            this.dbcTipoCliente.SelectedIndex = -1;
            this.dbcTipoCliente.SelectedItem = null;
            this.dbcTipoCliente.SelectedOption = null;
            this.dbcTipoCliente.SelectedValue = null;
            this.dbcTipoCliente.ShowCode = false;
            this.dbcTipoCliente.ShowEdit = true;
            this.dbcTipoCliente.Size = new System.Drawing.Size(262, 21);
            this.dbcTipoCliente.Sort = true;
            this.dbcTipoCliente.SortStyle = FSFormControls.DBCombo.SortStyleEnum.Ascending;
            this.dbcTipoCliente.TabIndex = 2;
            this.dbcTipoCliente.Track = false;
            this.dbcTipoCliente.Value = null;
            this.dbcTipoCliente.ValueMember = "";
            // 
            // DbTipoCliente
            // 
            this.DbTipoCliente.About = null;
            this.DbTipoCliente.ArrayList = null;
            this.DbTipoCliente.AutoConnect = true;
            this.DbTipoCliente.DataControl = null;
            this.DbTipoCliente.DataSet = null;
            this.DbTipoCliente.DataTable = null;
            this.DbTipoCliente.DataView = null;
            this.DbTipoCliente.DBFieldData = "";
            this.DbTipoCliente.DBPosition = 0;
            this.DbTipoCliente.EraseDBControl = null;
            this.DbTipoCliente.Filter = "";
            this.DbTipoCliente.isEOF = true;
            this.DbTipoCliente.Location = new System.Drawing.Point(655, 64);
            this.DbTipoCliente.LOCK = null;
            this.DbTipoCliente.LOPD = null;
            this.DbTipoCliente.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbTipoCliente.Name = "DbTipoCliente";
            this.DbTipoCliente.Page = 0;
            this.DbTipoCliente.PageSettings = null;
            this.DbTipoCliente.Paging = false;
            this.DbTipoCliente.PagingSize = 0;
            this.DbTipoCliente.ReadOnly = false;
            this.DbTipoCliente.RelationDataControl = null;
            this.DbTipoCliente.RelationDBField = "";
            this.DbTipoCliente.RelationParentDBField = "";
            this.DbTipoCliente.SaveError = false;
            this.DbTipoCliente.SaveOnChangeRecord = false;
            this.DbTipoCliente.Selection = "select * from TipoCliente";
            this.DbTipoCliente.Size = new System.Drawing.Size(64, 44);
            this.DbTipoCliente.StoreInBase64Format = false;
            this.DbTipoCliente.TabIndex = 8;
            this.DbTipoCliente.TableName = "TipoCliente";
            this.DbTipoCliente.TabStop = false;
            this.DbTipoCliente.Text = "SQL: select * from TipoCliente";
            this.DbTipoCliente.Track = false;
            this.DbTipoCliente.TypeDB = FSFormControls.DBControl.DbType.SQLServer;
            this.DbTipoCliente.Versionable = false;
            this.DbTipoCliente.VersionableDateField = "";
            this.DbTipoCliente.VersionableTable = "";
            this.DbTipoCliente.VersionableUserField = "";
            this.DbTipoCliente.VersionableVersionField = "";
            this.DbTipoCliente.Visible = false;
            this.DbTipoCliente.XmlFile = "";
            this.DbTipoCliente.XMLName = "";
            // 
            // DbLabel2
            // 
            this.DbLabel2.About = null;
            this.DbLabel2.Angle = 0F;
            this.DbLabel2.Appearance = null;
            this.DbLabel2.AutoSize = true;
            this.DbLabel2.BackColor = System.Drawing.Color.Transparent;
            this.DbLabel2.BorderStyleInner = System.Windows.Forms.BorderStyle.None;
            this.DbLabel2.BorderStyleOuter = System.Windows.Forms.BorderStyle.None;
            this.DbLabel2.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
            this.DbLabel2.DataControl = null;
            this.DbLabel2.DataType = FSFormControls.DBTextBox.TypeData.All;
            this.DbLabel2.DateFormat = "dd/MM/yyyy";
            this.DbLabel2.Decimals = 2;
            this.DbLabel2.EndColor = System.Drawing.Color.LightSkyBlue;
            this.DbLabel2.Location = new System.Drawing.Point(19, 24);
            this.DbLabel2.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbLabel2.Name = "DbLabel2";
            this.DbLabel2.ShadowColor = System.Drawing.Color.Black;
            this.DbLabel2.Size = new System.Drawing.Size(84, 13);
            this.DbLabel2.StartColor = System.Drawing.Color.White;
            this.DbLabel2.TabIndex = 1;
            this.DbLabel2.TabStop = false;
            this.DbLabel2.Text = "Nombre Cliente:";
            this.DbLabel2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.DbLabel2.Track = false;
            this.DbLabel2.XOffset = 1F;
            this.DbLabel2.YOffset = 1F;
            // 
            // DbLabel1
            // 
            this.DbLabel1.About = null;
            this.DbLabel1.Angle = 0F;
            this.DbLabel1.Appearance = null;
            this.DbLabel1.AutoSize = true;
            this.DbLabel1.BackColor = System.Drawing.Color.Transparent;
            this.DbLabel1.BorderStyleInner = System.Windows.Forms.BorderStyle.None;
            this.DbLabel1.BorderStyleOuter = System.Windows.Forms.BorderStyle.None;
            this.DbLabel1.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
            this.DbLabel1.DataControl = null;
            this.DbLabel1.DataType = FSFormControls.DBTextBox.TypeData.All;
            this.DbLabel1.DateFormat = "dd/MM/yyyy";
            this.DbLabel1.Decimals = 2;
            this.DbLabel1.EndColor = System.Drawing.Color.LightSkyBlue;
            this.DbLabel1.Location = new System.Drawing.Point(19, 48);
            this.DbLabel1.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbLabel1.Name = "DbLabel1";
            this.DbLabel1.ShadowColor = System.Drawing.Color.Black;
            this.DbLabel1.Size = new System.Drawing.Size(84, 13);
            this.DbLabel1.StartColor = System.Drawing.Color.White;
            this.DbLabel1.TabIndex = 0;
            this.DbLabel1.TabStop = false;
            this.DbLabel1.Text = "Tipo Cliente:";
            this.DbLabel1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.DbLabel1.Track = false;
            this.DbLabel1.XOffset = 1F;
            this.DbLabel1.YOffset = 1F;
            // 
            // frmClientes
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1065, 378);
            this.Controls.Add(this.DbTipoUsuario);
            this.Controls.Add(this.DbTipoEstado);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.DbClientes);
            this.Controls.Add(this.DbGrid1);
            this.Controls.Add(this.DbTipoCliente);
            this.DataControl = this.DbClientes;
            this.Name = "frmClientes";
            this.ShowScrollBar = false;
            this.Text = "Listado de Clientes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmClientes_Load);
            this.Controls.SetChildIndex(this.DbToolBar1, 0);
            this.Controls.SetChildIndex(this.DbStatusBar1, 0);
            this.Controls.SetChildIndex(this.DbTipoCliente, 0);
            this.Controls.SetChildIndex(this.DbGrid1, 0);
            this.Controls.SetChildIndex(this.DbClientes, 0);
            this.Controls.SetChildIndex(this.GroupBox1, 0);
            this.Controls.SetChildIndex(this.DbTipoEstado, 0);
            this.Controls.SetChildIndex(this.DbTipoUsuario, 0);
            ((System.ComponentModel.ISupportInitialize)(this.DbStatusBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbStatusBarPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbStatusBarProgressPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbSBarPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbSBarPanel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbSBarPanel3)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbcEstado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbcTecnicoResponsable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombreCliente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbcTipoCliente)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		
#endregion
		
		
		private void DbGrid1_DoubleClick(object sender, System.EventArgs e)
		{
            Log.TraceInfo("Comenzamos la traza en la carga del formulario de cliente.");
            //mostramos la ficha del cliente.
			Global.MuestraCliente(this.DbGrid1.get_RowValue(0) + "");
            Log.TraceInfo("Fin traza");
		}
		
		private void dbbLocalizar_Click(System.Object sender, System.EventArgs e)
		{
			string filtro = default(string);
			filtro = "";
			
			if (this.txtNombreCliente.Text != "")
			{
				filtro = "Nombre like \'%" + this.txtNombreCliente.Text + "%\'";
			}
			if (dbcTipoCliente.SelectedValue != null)
			{
				if (filtro != "")
				{
					filtro += " and ";
				}
				filtro += "TipoActividad =" + this.dbcTipoCliente.SelectedValue.ToString();
			}
			if (dbcTecnicoResponsable.SelectedValue != null)
			{
				if (filtro != "")
				{
					filtro += " and ";
				}
				filtro += "TecnicoResponsable =" + this.dbcTecnicoResponsable.SelectedValue.ToString();
			}
			if (dbcEstado.SelectedValue != null)
			{
				if (filtro != "")
				{
					filtro += " and ";
				}
				filtro += "Estado =" + this.dbcEstado.SelectedValue.ToString();
			}
			
			this.DbClientes.Filter = filtro;
		}
		
		private void dbbTodas_Click(System.Object sender, System.EventArgs e)
		{
			this.DbClientes.Filter = "";
		}
		
		private void frmClientes_Load(System.Object sender, System.EventArgs e)
		{
			Global.AplicaSeguridad(this);
			Global.AplicaToolbar(this);
        }
    }
}
