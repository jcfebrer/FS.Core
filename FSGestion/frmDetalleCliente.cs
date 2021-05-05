
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;



namespace FSGestion
{
	public class frmDetalleCliente : FSFormControls.DBForm
	{
		
#region  Código generado por el Diseñador de Windows Forms
		
		public frmDetalleCliente()
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
		internal System.Windows.Forms.TabControl TabControl1;
		internal FSFormControls.DBColumn DbColumn1;
		internal FSFormControls.DBColumn DbColumn2;
		internal FSFormControls.DBColumn DbColumn3;
		internal FSFormControls.DBColumn DbColumn7;
		internal FSFormControls.DBColumn DbColumn8;
		internal FSFormControls.DBColumn DbColumn9;
		internal FSFormControls.DBColumn DbColumn10;
		internal FSFormControls.DBColumn DbColumn13;
		internal FSFormControls.DBColumn DbColumn14;
		internal FSFormControls.DBColumn DbColumn15;
		internal FSFormControls.DBColumn DbColumn16;
		internal FSFormControls.DBColumn DbColumn18;
		internal FSFormControls.DBColumn DbColumn17;
		internal FSFormControls.DBColumn DbColumn19;
		internal FSFormControls.DBColumn DbColumn20;
		internal FSFormControls.DBColumn DbColumn21;
		internal FSFormControls.DBColumn DbColumn22;
		internal FSFormControls.DBColumn DbColumn23;
		internal FSFormControls.DBColumn DbColumn24;
		internal System.Windows.Forms.TabPage TabPage6;
		internal FSFormControls.DBColumn DbColumn25;
		internal FSFormControls.DBColumn DbColumn26;
		internal FSFormControls.DBColumn DbColumn27;
		internal FSFormControls.DBColumn DbColumn28;
		internal FSFormControls.DBColumn DbColumn29;
		internal FSFormControls.DBColumn DbColumn30;
		internal FSFormControls.DBColumn DbColumn31;
		internal FSFormControls.DBColumn DbColumn32;
		internal FSFormControls.DBColumn DbColumn33;
		internal FSFormControls.DBColumn DbColumn34;
		internal FSFormControls.DBColumn DbColumn35;
		internal FSFormControls.DBColumn DbColumn36;
		internal FSFormControls.DBColumn DbColumn37;
		internal FSFormControls.DBColumn DbColumn38;
		internal FSFormControls.DBColumn DbColumn39;
		internal FSFormControls.DBColumn DbColumn40;
		internal FSFormControls.DBColumn DbColumn41;
		internal FSFormControls.DBColumn DbColumn11;
		internal FSFormControls.DBColumn DbColumn12;
		internal FSFormControls.DBColumn DbColumn42;
		internal FSFormControls.DBColumn DbColumn43;
		internal FSFormControls.DBColumn DbColumn44;
		internal FSFormControls.DBRecord DbRecord5;
		internal System.Windows.Forms.TabPage TabPage7;
		internal FSFormControls.DBGrid DbGrid5;
		internal FSFormControls.DBColumn empDbColumn17;
		internal FSFormControls.DBColumn empDbColumn22;
		internal FSFormControls.DBColumn empDbColumn1;
		internal FSFormControls.DBColumn empDbColumn2;
		internal FSFormControls.DBColumn empDbColumn3;
		internal FSFormControls.DBColumn empDbColumn4;
		internal FSFormControls.DBColumn empDbColumn5;
		internal FSFormControls.DBColumn empDbColumn6;
		internal FSFormControls.DBColumn empDbColumn7;
		internal FSFormControls.DBColumn empDbColumn19;
		internal FSFormControls.DBColumn empDbColumn18;
		
		internal FSFormControls.DBColumn empDbColumn11;
		internal FSFormControls.DBColumn empDbColumn12;
		
		
		internal FSFormControls.DBColumn DbColumn4;
		internal FSFormControls.DBControl DbControl7;
		internal FSFormControls.DBControl DbControl6;
		internal FSFormControls.DBControl DbControl4;
		internal FSFormControls.DBControl DbControl5;
		internal System.Windows.Forms.TabPage TabPage1;
		internal FSFormControls.DBGrid DbGrid1;
		internal FSFormControls.DBControl DbControl2;
		internal FSFormControls.DBControl DbControl8;
		internal FSFormControls.DBColumn DbColumn5;
		internal FSFormControls.DBColumn DbColumn6;
		internal FSFormControls.DBImage DbImage1;
		internal System.Windows.Forms.TabPage TabPage2;
		internal System.Windows.Forms.TabPage TabPage3;
		internal FSFormControls.DBColumn DbColumn45;
		internal FSFormControls.DBColumn DbColumn46;
		internal FSFormControls.DBColumn DbColumn47;
		internal FSFormControls.DBColumn DbColumn48;
		internal FSFormControls.DBColumn DbColumn49;
		internal FSFormControls.DBGrid DbGrid4;
		internal FSFormControls.DBControl DbControl10;
		internal FSFormControls.DBColumn DbColumn50;
		internal FSFormControls.DBColumn DbColumn51;
		internal FSFormControls.DBColumn DbColumn52;
		internal FSFormControls.DBColumn DbColumn53;
		internal FSFormControls.DBColumn DbColumn54;
		internal FSFormControls.DBColumn DbColumn55;
		internal FSFormControls.DBColumn DbColumn56;
		internal FSFormControls.DBColumn DbColumn57;
		internal FSFormControls.DBColumn DbColumn58;
		internal FSFormControls.DBColumn DbColumn59;
		internal FSFormControls.DBColumn DbColumn60;
		internal FSFormControls.DBColumn DbColumn61;
		internal FSFormControls.DBColumn DbColumn62;
		internal FSFormControls.DBControl DbControl11;
		internal FSFormControls.DBColumn DbColumn63;
		internal FSFormControls.DBColumn DbColumn64;
		internal FSFormControls.DBColumn DbColumn65;
        private TabPage tabPage4;
        private FSFormControls.DBGridView dbGridView1;
        internal FSFormControls.DBControl dbcServicios;
        internal FSFormControls.DBControl DbControl13;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
            FSFormControls.DBGridViewDisplayLayout dbGridViewDisplayLayout1 = new FSFormControls.DBGridViewDisplayLayout();
            this.empDbColumn17 = new FSFormControls.DBColumn();
            this.empDbColumn22 = new FSFormControls.DBColumn();
            this.DbControl6 = new FSFormControls.DBControl();
            this.empDbColumn1 = new FSFormControls.DBColumn();
            this.empDbColumn2 = new FSFormControls.DBColumn();
            this.empDbColumn3 = new FSFormControls.DBColumn();
            this.empDbColumn4 = new FSFormControls.DBColumn();
            this.empDbColumn5 = new FSFormControls.DBColumn();
            this.empDbColumn6 = new FSFormControls.DBColumn();
            this.empDbColumn7 = new FSFormControls.DBColumn();
            this.empDbColumn11 = new FSFormControls.DBColumn();
            this.DbControl4 = new FSFormControls.DBControl();
            this.empDbColumn12 = new FSFormControls.DBColumn();
            this.DbControl5 = new FSFormControls.DBControl();
            this.empDbColumn19 = new FSFormControls.DBColumn();
            this.DbControl1 = new FSFormControls.DBControl();
            this.empDbColumn18 = new FSFormControls.DBColumn();
            this.DbRecord1 = new FSFormControls.DBRecord();
            this.DbColumn1 = new FSFormControls.DBColumn();
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.DbRecord5 = new FSFormControls.DBRecord();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.DbControl11 = new FSFormControls.DBControl();
            this.DbControl10 = new FSFormControls.DBControl();
            this.DbGrid4 = new FSFormControls.DBGrid();
            this.DbColumn50 = new FSFormControls.DBColumn();
            this.DbColumn51 = new FSFormControls.DBColumn();
            this.DbColumn52 = new FSFormControls.DBColumn();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.dbcServicios = new FSFormControls.DBControl();
            this.DbControl13 = new FSFormControls.DBControl();
            this.DbGrid1 = new FSFormControls.DBGrid();
            this.DbColumn5 = new FSFormControls.DBColumn();
            this.DbColumn6 = new FSFormControls.DBColumn();
            this.DbColumn62 = new FSFormControls.DBColumn();
            this.DbColumn63 = new FSFormControls.DBColumn();
            this.DbColumn64 = new FSFormControls.DBColumn();
            this.DbColumn65 = new FSFormControls.DBColumn();
            this.DbControl2 = new FSFormControls.DBControl();
            this.TabPage7 = new System.Windows.Forms.TabPage();
            this.DbGrid5 = new FSFormControls.DBGrid();
            this.DbColumn4 = new FSFormControls.DBColumn();
            this.DbControl7 = new FSFormControls.DBControl();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.DbImage1 = new FSFormControls.DBImage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dbGridView1 = new FSFormControls.DBGridView();
            this.DbControl8 = new FSFormControls.DBControl();
            this.DbColumn61 = new FSFormControls.DBColumn();
            this.DbColumn53 = new FSFormControls.DBColumn();
            this.DbColumn54 = new FSFormControls.DBColumn();
            this.DbColumn55 = new FSFormControls.DBColumn();
            this.DbColumn56 = new FSFormControls.DBColumn();
            this.DbColumn57 = new FSFormControls.DBColumn();
            this.DbColumn58 = new FSFormControls.DBColumn();
            this.DbColumn59 = new FSFormControls.DBColumn();
            this.DbColumn60 = new FSFormControls.DBColumn();
            this.DbColumn45 = new FSFormControls.DBColumn();
            this.DbColumn46 = new FSFormControls.DBColumn();
            this.DbColumn47 = new FSFormControls.DBColumn();
            this.DbColumn48 = new FSFormControls.DBColumn();
            this.DbColumn49 = new FSFormControls.DBColumn();
            this.DbColumn17 = new FSFormControls.DBColumn();
            this.DbColumn19 = new FSFormControls.DBColumn();
            this.DbColumn20 = new FSFormControls.DBColumn();
            this.DbColumn21 = new FSFormControls.DBColumn();
            this.DbColumn22 = new FSFormControls.DBColumn();
            this.DbColumn23 = new FSFormControls.DBColumn();
            this.DbColumn24 = new FSFormControls.DBColumn();
            this.DbColumn9 = new FSFormControls.DBColumn();
            this.DbColumn10 = new FSFormControls.DBColumn();
            this.DbColumn13 = new FSFormControls.DBColumn();
            this.DbColumn14 = new FSFormControls.DBColumn();
            this.DbColumn15 = new FSFormControls.DBColumn();
            this.DbColumn16 = new FSFormControls.DBColumn();
            this.DbColumn18 = new FSFormControls.DBColumn();
            this.DbColumn42 = new FSFormControls.DBColumn();
            this.DbColumn44 = new FSFormControls.DBColumn();
            this.DbColumn11 = new FSFormControls.DBColumn();
            this.DbColumn12 = new FSFormControls.DBColumn();
            this.DbColumn2 = new FSFormControls.DBColumn();
            this.DbColumn3 = new FSFormControls.DBColumn();
            this.DbColumn7 = new FSFormControls.DBColumn();
            this.DbColumn8 = new FSFormControls.DBColumn();
            this.DbColumn43 = new FSFormControls.DBColumn();
            this.DbColumn37 = new FSFormControls.DBColumn();
            this.DbColumn38 = new FSFormControls.DBColumn();
            this.DbColumn39 = new FSFormControls.DBColumn();
            this.DbColumn40 = new FSFormControls.DBColumn();
            this.DbColumn41 = new FSFormControls.DBColumn();
            this.DbColumn25 = new FSFormControls.DBColumn();
            this.DbColumn26 = new FSFormControls.DBColumn();
            this.DbColumn27 = new FSFormControls.DBColumn();
            this.DbColumn28 = new FSFormControls.DBColumn();
            this.DbColumn29 = new FSFormControls.DBColumn();
            this.DbColumn30 = new FSFormControls.DBColumn();
            this.DbColumn31 = new FSFormControls.DBColumn();
            this.DbColumn32 = new FSFormControls.DBColumn();
            this.DbColumn33 = new FSFormControls.DBColumn();
            this.DbColumn34 = new FSFormControls.DBColumn();
            this.DbColumn35 = new FSFormControls.DBColumn();
            this.DbColumn36 = new FSFormControls.DBColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DbStatusBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbStatusBarPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbStatusBarProgressPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbSBarPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbSBarPanel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbSBarPanel3)).BeginInit();
            this.TabControl1.SuspendLayout();
            this.TabPage6.SuspendLayout();
            this.TabPage3.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.TabPage7.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // DbStatusBar1
            // 
            this.DbStatusBar1.Location = new System.Drawing.Point(0, 446);
            this.DbStatusBar1.Size = new System.Drawing.Size(745, 20);
            // 
            // DbToolBar1
            // 
            this.DbToolBar1.Size = new System.Drawing.Size(745, 138);
            this.DbToolBar1.VisibleScroll = true;
            // 
            // mnuForm
            // 
            this.mnuForm.OwnerDraw = true;
            // 
            // DbSBarPanel1
            // 
            this.DbSBarPanel1.Width = 484;
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
            this.empDbColumn17.FormatString = null;
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
            this.empDbColumn22.ColumnDBControl = this.DbControl6;
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
            this.empDbColumn22.FormatString = null;
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
            // DbControl6
            // 
            this.DbControl6.About = null;
            this.DbControl6.ArrayList = null;
            this.DbControl6.AutoConnect = true;
            this.DbControl6.DataControl = null;
            this.DbControl6.DataSet = null;
            this.DbControl6.DataTable = null;
            this.DbControl6.DataView = null;
            this.DbControl6.DBFieldData = "";
            this.DbControl6.DBPosition = 0;
            this.DbControl6.EraseDBControl = null;
            this.DbControl6.Filter = "";
            this.DbControl6.isEOF = true;
            this.DbControl6.Location = new System.Drawing.Point(372, 72);
            this.DbControl6.LOCK = null;
            this.DbControl6.LOPD = null;
            this.DbControl6.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbControl6.Name = "DbControl6";
            this.DbControl6.Page = 0;
            this.DbControl6.PageSettings = null;
            this.DbControl6.Paging = false;
            this.DbControl6.PagingSize = 0;
            this.DbControl6.ReadOnly = false;
            this.DbControl6.RelationDataControl = null;
            this.DbControl6.RelationDBField = "";
            this.DbControl6.RelationParentDBField = "";
            this.DbControl6.SaveError = false;
            this.DbControl6.SaveOnChangeRecord = false;
            this.DbControl6.Selection = "select * from tipoEstado";
            this.DbControl6.Size = new System.Drawing.Size(116, 40);
            this.DbControl6.StoreInBase64Format = false;
            this.DbControl6.TabIndex = 11;
            this.DbControl6.TableName = "tipoEstado";
            this.DbControl6.TabStop = false;
            this.DbControl6.Text = "SQL: select * from tipoEstado";
            this.DbControl6.Track = false;
            this.DbControl6.TypeDB = FSFormControls.DBControl.DbType.SQLServer;
            this.DbControl6.Versionable = false;
            this.DbControl6.VersionableDateField = "";
            this.DbControl6.VersionableTable = "";
            this.DbControl6.VersionableUserField = "";
            this.DbControl6.VersionableVersionField = "";
            this.DbControl6.Visible = false;
            this.DbControl6.XmlFile = "";
            this.DbControl6.XMLName = "";
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
            this.empDbColumn1.FormatString = null;
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
            this.empDbColumn2.FormatString = null;
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
            this.empDbColumn3.FormatString = null;
            this.empDbColumn3.HeaderCaption = "Teléfono 1";
            this.empDbColumn3.Hidden = false;
            this.empDbColumn3.LastValue = false;
            this.empDbColumn3.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.empDbColumn3.MaskInput = "(##)#######";
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
            this.empDbColumn4.FormatString = null;
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
            this.empDbColumn5.FormatString = null;
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
            this.empDbColumn6.FormatString = null;
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
            this.empDbColumn7.FormatString = null;
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
            this.empDbColumn11.ColumnDBControl = this.DbControl4;
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
            this.empDbColumn11.FormatString = null;
            this.empDbColumn11.HeaderCaption = "Tecnico Responsable";
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
            // DbControl4
            // 
            this.DbControl4.About = null;
            this.DbControl4.ArrayList = null;
            this.DbControl4.AutoConnect = true;
            this.DbControl4.DataControl = null;
            this.DbControl4.DataSet = null;
            this.DbControl4.DataTable = null;
            this.DbControl4.DataView = null;
            this.DbControl4.DBFieldData = "";
            this.DbControl4.DBPosition = 0;
            this.DbControl4.EraseDBControl = null;
            this.DbControl4.Filter = "";
            this.DbControl4.isEOF = true;
            this.DbControl4.Location = new System.Drawing.Point(122, 81);
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
            this.DbControl4.Selection = "select * from Usuarios where tipoUsuario=1";
            this.DbControl4.Size = new System.Drawing.Size(86, 55);
            this.DbControl4.StoreInBase64Format = false;
            this.DbControl4.TabIndex = 12;
            this.DbControl4.TableName = "Usuarios";
            this.DbControl4.TabStop = false;
            this.DbControl4.Text = "SQL: select * from Usuarios where tipoUsuario=1";
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
            // empDbColumn12
            // 
            this.empDbColumn12.ActiveColumnDBButtonOnReadMode = true;
            this.empDbColumn12.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.empDbColumn12.AllowNull = false;
            this.empDbColumn12.AllowRowFiltering = false;
            this.empDbColumn12.AsociatedButtonColumn = -1;
            this.empDbColumn12.AsociatedComboColumn = -1;
            this.empDbColumn12.ColumnBackColor = System.Drawing.Color.Empty;
            this.empDbColumn12.ColumnDBControl = this.DbControl5;
            this.empDbColumn12.ColumnDBFieldData = "";
            this.empDbColumn12.ColumnForeColor = System.Drawing.Color.Empty;
            this.empDbColumn12.ColumnType = FSFormControls.DBColumn.ColumnTypes.ComboColumn;
            this.empDbColumn12.ComboBlankSelection = true;
            this.empDbColumn12.ComboImageList = null;
            this.empDbColumn12.ComboListField = "descripcion";
            this.empDbColumn12.DBGridViewFilters = null;
            this.empDbColumn12.Decimals = 2;
            this.empDbColumn12.DefaultValue = "";
            this.empDbColumn12.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.empDbColumn12.Encrypted = false;
            this.empDbColumn12.Expression = "";
            this.empDbColumn12.FieldDB = "tipoActividad";
            this.empDbColumn12.Font = null;
            this.empDbColumn12.Format = null;
            this.empDbColumn12.FormatString = null;
            this.empDbColumn12.HeaderCaption = "Actividad";
            this.empDbColumn12.Hidden = false;
            this.empDbColumn12.LastValue = false;
            this.empDbColumn12.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.empDbColumn12.MaskInput = null;
            this.empDbColumn12.MaxLength = 0;
            this.empDbColumn12.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.empDbColumn12.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.empDbColumn12.MultiLine = false;
            this.empDbColumn12.NullValue = null;
            this.empDbColumn12.Obligatory = false;
            this.empDbColumn12.PromptChar = '\0';
            this.empDbColumn12.ReadColumn = false;
            this.empDbColumn12.ShowSelectForm = true;
            this.empDbColumn12.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.empDbColumn12.ToolTip = "";
            this.empDbColumn12.Unique = false;
            this.empDbColumn12.Width = 100;
            // 
            // DbControl5
            // 
            this.DbControl5.About = null;
            this.DbControl5.ArrayList = null;
            this.DbControl5.AutoConnect = true;
            this.DbControl5.DataControl = null;
            this.DbControl5.DataSet = null;
            this.DbControl5.DataTable = null;
            this.DbControl5.DataView = null;
            this.DbControl5.DBFieldData = "";
            this.DbControl5.DBPosition = 0;
            this.DbControl5.EraseDBControl = null;
            this.DbControl5.Filter = "";
            this.DbControl5.isEOF = true;
            this.DbControl5.Location = new System.Drawing.Point(472, 104);
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
            this.DbControl5.Selection = "select * from TipoCliente";
            this.DbControl5.Size = new System.Drawing.Size(106, 32);
            this.DbControl5.StoreInBase64Format = false;
            this.DbControl5.TabIndex = 13;
            this.DbControl5.TableName = "TipoCliente";
            this.DbControl5.TabStop = false;
            this.DbControl5.Text = "SQL: select * from TipoCliente";
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
            // empDbColumn19
            // 
            this.empDbColumn19.ActiveColumnDBButtonOnReadMode = true;
            this.empDbColumn19.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.empDbColumn19.AllowNull = false;
            this.empDbColumn19.AllowRowFiltering = false;
            this.empDbColumn19.AsociatedButtonColumn = -1;
            this.empDbColumn19.AsociatedComboColumn = -1;
            this.empDbColumn19.ColumnBackColor = System.Drawing.Color.Empty;
            this.empDbColumn19.ColumnDBControl = null;
            this.empDbColumn19.ColumnDBFieldData = "";
            this.empDbColumn19.ColumnForeColor = System.Drawing.Color.Empty;
            this.empDbColumn19.ColumnType = FSFormControls.DBColumn.ColumnTypes.DateColumn;
            this.empDbColumn19.ComboBlankSelection = true;
            this.empDbColumn19.ComboImageList = null;
            this.empDbColumn19.ComboListField = "";
            this.empDbColumn19.DBGridViewFilters = null;
            this.empDbColumn19.Decimals = 2;
            this.empDbColumn19.DefaultValue = "";
            this.empDbColumn19.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.empDbColumn19.Encrypted = false;
            this.empDbColumn19.Expression = "";
            this.empDbColumn19.FieldDB = "fechaContrato";
            this.empDbColumn19.Font = null;
            this.empDbColumn19.Format = null;
            this.empDbColumn19.FormatString = null;
            this.empDbColumn19.HeaderCaption = "Fecha Contrato";
            this.empDbColumn19.Hidden = false;
            this.empDbColumn19.LastValue = false;
            this.empDbColumn19.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.empDbColumn19.MaskInput = null;
            this.empDbColumn19.MaxLength = 0;
            this.empDbColumn19.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.empDbColumn19.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.empDbColumn19.MultiLine = false;
            this.empDbColumn19.NullValue = null;
            this.empDbColumn19.Obligatory = false;
            this.empDbColumn19.PromptChar = '\0';
            this.empDbColumn19.ReadColumn = false;
            this.empDbColumn19.ShowSelectForm = true;
            this.empDbColumn19.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.empDbColumn19.ToolTip = "";
            this.empDbColumn19.Unique = false;
            this.empDbColumn19.Width = 0;
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
            this.DbControl1.DBFieldData = "";
            this.DbControl1.DBPosition = 0;
            this.DbControl1.EraseDBControl = null;
            this.DbControl1.Filter = "";
            this.DbControl1.isEOF = true;
            this.DbControl1.Location = new System.Drawing.Point(520, 40);
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
            this.DbControl1.Selection = "select * from Clientes where idCliente=?";
            this.DbControl1.Size = new System.Drawing.Size(88, 72);
            this.DbControl1.StoreInBase64Format = false;
            this.DbControl1.TabIndex = 3;
            this.DbControl1.TableName = "Clientes";
            this.DbControl1.TabStop = false;
            this.DbControl1.Text = "SQL: select * from Clientes where idCliente=?";
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
            // empDbColumn18
            // 
            this.empDbColumn18.ActiveColumnDBButtonOnReadMode = true;
            this.empDbColumn18.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.empDbColumn18.AllowNull = false;
            this.empDbColumn18.AllowRowFiltering = false;
            this.empDbColumn18.AsociatedButtonColumn = -1;
            this.empDbColumn18.AsociatedComboColumn = -1;
            this.empDbColumn18.ColumnBackColor = System.Drawing.Color.Empty;
            this.empDbColumn18.ColumnDBControl = null;
            this.empDbColumn18.ColumnDBFieldData = "";
            this.empDbColumn18.ColumnForeColor = System.Drawing.Color.Empty;
            this.empDbColumn18.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.empDbColumn18.ComboBlankSelection = true;
            this.empDbColumn18.ComboImageList = null;
            this.empDbColumn18.ComboListField = "";
            this.empDbColumn18.DBGridViewFilters = null;
            this.empDbColumn18.Decimals = 2;
            this.empDbColumn18.DefaultValue = "";
            this.empDbColumn18.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.empDbColumn18.Encrypted = false;
            this.empDbColumn18.Expression = "";
            this.empDbColumn18.FieldDB = "firmadoPor";
            this.empDbColumn18.Font = null;
            this.empDbColumn18.Format = null;
            this.empDbColumn18.FormatString = null;
            this.empDbColumn18.HeaderCaption = "Firmado por";
            this.empDbColumn18.Hidden = false;
            this.empDbColumn18.LastValue = false;
            this.empDbColumn18.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.empDbColumn18.MaskInput = null;
            this.empDbColumn18.MaxLength = 0;
            this.empDbColumn18.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.empDbColumn18.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.empDbColumn18.MultiLine = false;
            this.empDbColumn18.NullValue = null;
            this.empDbColumn18.Obligatory = false;
            this.empDbColumn18.PromptChar = '\0';
            this.empDbColumn18.ReadColumn = false;
            this.empDbColumn18.ShowSelectForm = true;
            this.empDbColumn18.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.empDbColumn18.ToolTip = "";
            this.empDbColumn18.Unique = false;
            this.empDbColumn18.Width = 0;
            // 
            // DbRecord1
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
            this.DbRecord1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DbRecord1.AutoSize = true;
            this.DbRecord1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DbRecord1.Columns.AddRange(new FSFormControls.DBColumn[] {
            this.DbColumn1});
            this.DbRecord1.DataControl = this.DbControl1;
            this.DbRecord1.DateType = FSFormControls.DBRecord.t_date.Normal;
            this.DbRecord1.DoubleHeightInLargeText = false;
            this.DbRecord1.LabelAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbRecord1.LabelYIncrement = 30;
            this.DbRecord1.Location = new System.Drawing.Point(8, 64);
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
            this.DbRecord1.Size = new System.Drawing.Size(729, 56);
            this.DbRecord1.TabIndex = 2;
            this.DbRecord1.TextBoxShadow = false;
            this.DbRecord1.Track = false;
            // 
            // DbColumn1
            // 
            this.DbColumn1.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn1.AllowNull = false;
            this.DbColumn1.AllowRowFiltering = false;
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
            this.DbColumn1.DBGridViewFilters = null;
            this.DbColumn1.Decimals = 2;
            this.DbColumn1.DefaultValue = "";
            this.DbColumn1.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn1.Encrypted = false;
            this.DbColumn1.Expression = "";
            this.DbColumn1.FieldDB = "nombre";
            this.DbColumn1.Font = null;
            this.DbColumn1.Format = null;
            this.DbColumn1.FormatString = null;
            this.DbColumn1.HeaderCaption = "Nombre Cliente:";
            this.DbColumn1.Hidden = false;
            this.DbColumn1.LastValue = false;
            this.DbColumn1.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn1.MaskInput = null;
            this.DbColumn1.MaxLength = 0;
            this.DbColumn1.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn1.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn1.MultiLine = false;
            this.DbColumn1.NullValue = null;
            this.DbColumn1.Obligatory = false;
            this.DbColumn1.PromptChar = '\0';
            this.DbColumn1.ReadColumn = true;
            this.DbColumn1.ShowSelectForm = true;
            this.DbColumn1.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn1.ToolTip = "";
            this.DbColumn1.Unique = false;
            this.DbColumn1.Width = 0;
            // 
            // TabControl1
            // 
            this.TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl1.Controls.Add(this.TabPage6);
            this.TabControl1.Controls.Add(this.TabPage3);
            this.TabControl1.Controls.Add(this.TabPage1);
            this.TabControl1.Controls.Add(this.TabPage7);
            this.TabControl1.Controls.Add(this.TabPage2);
            this.TabControl1.Controls.Add(this.tabPage4);
            this.TabControl1.Location = new System.Drawing.Point(8, 128);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(729, 305);
            this.TabControl1.TabIndex = 4;
            // 
            // TabPage6
            // 
            this.TabPage6.Controls.Add(this.DbRecord5);
            this.TabPage6.Location = new System.Drawing.Point(4, 22);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Size = new System.Drawing.Size(721, 279);
            this.TabPage6.TabIndex = 5;
            this.TabPage6.Text = "Datos Generales";
            // 
            // DbRecord5
            // 
            this.DbRecord5.About = null;
            this.DbRecord5.AllowAddNew = true;
            this.DbRecord5.AllowCancel = true;
            this.DbRecord5.AllowDelete = true;
            this.DbRecord5.AllowEdit = true;
            this.DbRecord5.AllowFilter = true;
            this.DbRecord5.AllowList = true;
            this.DbRecord5.AllowNavigate = true;
            this.DbRecord5.AllowPrint = true;
            this.DbRecord5.AllowRecord = true;
            this.DbRecord5.AllowSave = true;
            this.DbRecord5.AllowSearch = true;
            this.DbRecord5.AutoSize = true;
            this.DbRecord5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DbRecord5.Columns.AddRange(new FSFormControls.DBColumn[] {
            this.empDbColumn17,
            this.empDbColumn22,
            this.empDbColumn1,
            this.empDbColumn2,
            this.empDbColumn3,
            this.empDbColumn4,
            this.empDbColumn5,
            this.empDbColumn6,
            this.empDbColumn7,
            this.empDbColumn11,
            this.empDbColumn12});
            this.DbRecord5.DataControl = this.DbControl1;
            this.DbRecord5.DateType = FSFormControls.DBRecord.t_date.Normal;
            this.DbRecord5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DbRecord5.DoubleHeightInLargeText = false;
            this.DbRecord5.LabelAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbRecord5.LabelYIncrement = 30;
            this.DbRecord5.Location = new System.Drawing.Point(0, 0);
            this.DbRecord5.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbRecord5.Name = "DbRecord5";
            this.DbRecord5.PosXLabel = 20;
            this.DbRecord5.PosYLabel = 20;
            this.DbRecord5.ShowAddNew = true;
            this.DbRecord5.ShowCancel = true;
            this.DbRecord5.ShowClose = true;
            this.DbRecord5.ShowComboEdit = true;
            this.DbRecord5.ShowDelete = true;
            this.DbRecord5.ShowEdit = true;
            this.DbRecord5.ShowFilter = true;
            this.DbRecord5.ShowList = true;
            this.DbRecord5.ShowMode = FSFormControls.DBRecord.t_showmode.Vertical;
            this.DbRecord5.ShowNavigate = true;
            this.DbRecord5.ShowPrint = true;
            this.DbRecord5.ShowRecord = true;
            this.DbRecord5.ShowSave = true;
            this.DbRecord5.ShowScrollBar = false;
            this.DbRecord5.ShowSearch = true;
            this.DbRecord5.ShowToolBar = false;
            this.DbRecord5.Size = new System.Drawing.Size(721, 279);
            this.DbRecord5.TabIndex = 3;
            this.DbRecord5.TextBoxShadow = false;
            this.DbRecord5.Track = false;
            // 
            // TabPage3
            // 
            this.TabPage3.Controls.Add(this.DbControl11);
            this.TabPage3.Controls.Add(this.DbControl10);
            this.TabPage3.Controls.Add(this.DbGrid4);
            this.TabPage3.Location = new System.Drawing.Point(4, 22);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(721, 279);
            this.TabPage3.TabIndex = 9;
            this.TabPage3.Text = "Incidencias";
            // 
            // DbControl11
            // 
            this.DbControl11.About = null;
            this.DbControl11.ArrayList = null;
            this.DbControl11.AutoConnect = true;
            this.DbControl11.DataControl = null;
            this.DbControl11.DataSet = null;
            this.DbControl11.DataTable = null;
            this.DbControl11.DataView = null;
            this.DbControl11.DBFieldData = "";
            this.DbControl11.DBPosition = 0;
            this.DbControl11.EraseDBControl = null;
            this.DbControl11.Filter = "";
            this.DbControl11.isEOF = true;
            this.DbControl11.Location = new System.Drawing.Point(504, 64);
            this.DbControl11.LOCK = null;
            this.DbControl11.LOPD = null;
            this.DbControl11.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbControl11.Name = "DbControl11";
            this.DbControl11.Page = 0;
            this.DbControl11.PageSettings = null;
            this.DbControl11.Paging = false;
            this.DbControl11.PagingSize = 0;
            this.DbControl11.ReadOnly = false;
            this.DbControl11.RelationDataControl = null;
            this.DbControl11.RelationDBField = "";
            this.DbControl11.RelationParentDBField = "";
            this.DbControl11.SaveError = false;
            this.DbControl11.SaveOnChangeRecord = false;
            this.DbControl11.Selection = "select * from EstadoIncidencia";
            this.DbControl11.Size = new System.Drawing.Size(88, 48);
            this.DbControl11.StoreInBase64Format = false;
            this.DbControl11.TabIndex = 2;
            this.DbControl11.TableName = "EstadoIncidencia";
            this.DbControl11.TabStop = false;
            this.DbControl11.Text = "SQL: select * from EstadoIncidencia";
            this.DbControl11.Track = false;
            this.DbControl11.TypeDB = FSFormControls.DBControl.DbType.SQLServer;
            this.DbControl11.Versionable = false;
            this.DbControl11.VersionableDateField = "";
            this.DbControl11.VersionableTable = "";
            this.DbControl11.VersionableUserField = "";
            this.DbControl11.VersionableVersionField = "";
            this.DbControl11.Visible = false;
            this.DbControl11.XmlFile = "";
            this.DbControl11.XMLName = "";
            // 
            // DbControl10
            // 
            this.DbControl10.About = null;
            this.DbControl10.ArrayList = null;
            this.DbControl10.AutoConnect = true;
            this.DbControl10.DataControl = null;
            this.DbControl10.DataSet = null;
            this.DbControl10.DataTable = null;
            this.DbControl10.DataView = null;
            this.DbControl10.DBFieldData = "";
            this.DbControl10.DBPosition = 0;
            this.DbControl10.EraseDBControl = null;
            this.DbControl10.Filter = "";
            this.DbControl10.isEOF = true;
            this.DbControl10.Location = new System.Drawing.Point(472, 128);
            this.DbControl10.LOCK = null;
            this.DbControl10.LOPD = null;
            this.DbControl10.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbControl10.Name = "DbControl10";
            this.DbControl10.Page = 0;
            this.DbControl10.PageSettings = null;
            this.DbControl10.Paging = false;
            this.DbControl10.PagingSize = 0;
            this.DbControl10.ReadOnly = false;
            this.DbControl10.RelationDataControl = this.DbControl1;
            this.DbControl10.RelationDBField = "idCliente";
            this.DbControl10.RelationParentDBField = "idCliente";
            this.DbControl10.SaveError = false;
            this.DbControl10.SaveOnChangeRecord = false;
            this.DbControl10.Selection = "select * from Incidencias where idCliente=?";
            this.DbControl10.Size = new System.Drawing.Size(104, 64);
            this.DbControl10.StoreInBase64Format = false;
            this.DbControl10.TabIndex = 1;
            this.DbControl10.TableName = "Incidencias";
            this.DbControl10.TabStop = false;
            this.DbControl10.Text = "SQL: select * from Incidencias where idCliente=?";
            this.DbControl10.Track = false;
            this.DbControl10.TypeDB = FSFormControls.DBControl.DbType.SQLServer;
            this.DbControl10.Versionable = false;
            this.DbControl10.VersionableDateField = "";
            this.DbControl10.VersionableTable = "";
            this.DbControl10.VersionableUserField = "";
            this.DbControl10.VersionableVersionField = "";
            this.DbControl10.Visible = false;
            this.DbControl10.XmlFile = "";
            this.DbControl10.XMLName = "";
            // 
            // DbGrid4
            // 
            this.DbGrid4.About = null;
            this.DbGrid4.AllowAddNew = true;
            this.DbGrid4.AllowDelete = true;
            this.DbGrid4.AllowDrop = true;
            this.DbGrid4.AllowSorting = true;
            this.DbGrid4.AlternatingColor = System.Drawing.Color.Empty;
            this.DbGrid4.AutoSave = true;
            this.DbGrid4.AutoSize = true;
            this.DbGrid4.BackGroundColor = System.Drawing.Color.LightGray;
            this.DbGrid4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DbGrid4.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.DbGrid4.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.DbGrid4.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.DbGrid4.CaptionText = null;
            this.DbGrid4.CaptionVisible = true;
            this.DbGrid4.ColumnHeadersVisible = true;
            this.DbGrid4.Columns.AddRange(new FSFormControls.DBColumn[] {
            this.DbColumn50,
            this.DbColumn51,
            this.DbColumn52});
            this.DbGrid4.CurrentRowIndex = -1;
            this.DbGrid4.CustomColumnHeaders = false;
            this.DbGrid4.DataControl = this.DbControl10;
            this.DbGrid4.DefaultDecimals = 2;
            this.DbGrid4.DefaultHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DbGrid4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DbGrid4.Editable = true;
            this.DbGrid4.FlatMode = false;
            this.DbGrid4.GridLineColor = System.Drawing.SystemColors.Control;
            this.DbGrid4.GridLineStyle = System.Windows.Forms.DataGridLineStyle.Solid;
            this.DbGrid4.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.DbGrid4.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DbGrid4.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DbGrid4.LastCol = -1;
            this.DbGrid4.LastRow = -1;
            this.DbGrid4.Location = new System.Drawing.Point(0, 0);
            this.DbGrid4.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbGrid4.Name = "DbGrid4";
            this.DbGrid4.RecordMode = false;
            this.DbGrid4.RowHeadersVisible = true;
            this.DbGrid4.RowHeight = 16;
            this.DbGrid4.RowSel = -1;
            this.DbGrid4.RowsInCaption = 2;
            this.DbGrid4.ShowRecordScrollBar = false;
            this.DbGrid4.ShowTotals = false;
            this.DbGrid4.Size = new System.Drawing.Size(721, 279);
            this.DbGrid4.TabIndex = 0;
            this.DbGrid4.TotalOperation = FSFormControls.DBColumn.OperationTypes.Sum;
            this.DbGrid4.Track = false;
            this.DbGrid4.XMLName = "";
            // 
            // DbColumn50
            // 
            this.DbColumn50.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn50.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn50.AllowNull = false;
            this.DbColumn50.AllowRowFiltering = false;
            this.DbColumn50.AsociatedButtonColumn = -1;
            this.DbColumn50.AsociatedComboColumn = -1;
            this.DbColumn50.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn50.ColumnDBControl = null;
            this.DbColumn50.ColumnDBFieldData = "";
            this.DbColumn50.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn50.ColumnType = FSFormControls.DBColumn.ColumnTypes.DateColumn;
            this.DbColumn50.ComboBlankSelection = true;
            this.DbColumn50.ComboImageList = null;
            this.DbColumn50.ComboListField = "";
            this.DbColumn50.DBGridViewFilters = null;
            this.DbColumn50.Decimals = 2;
            this.DbColumn50.DefaultValue = "";
            this.DbColumn50.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn50.Encrypted = false;
            this.DbColumn50.Expression = "";
            this.DbColumn50.FieldDB = "fecha";
            this.DbColumn50.Font = null;
            this.DbColumn50.Format = null;
            this.DbColumn50.FormatString = null;
            this.DbColumn50.HeaderCaption = "Fecha";
            this.DbColumn50.Hidden = false;
            this.DbColumn50.LastValue = false;
            this.DbColumn50.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn50.MaskInput = null;
            this.DbColumn50.MaxLength = 0;
            this.DbColumn50.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn50.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn50.MultiLine = false;
            this.DbColumn50.NullValue = null;
            this.DbColumn50.Obligatory = false;
            this.DbColumn50.PromptChar = '\0';
            this.DbColumn50.ReadColumn = false;
            this.DbColumn50.ShowSelectForm = true;
            this.DbColumn50.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn50.ToolTip = "";
            this.DbColumn50.Unique = false;
            this.DbColumn50.Width = 200;
            // 
            // DbColumn51
            // 
            this.DbColumn51.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn51.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn51.AllowNull = false;
            this.DbColumn51.AllowRowFiltering = false;
            this.DbColumn51.AsociatedButtonColumn = -1;
            this.DbColumn51.AsociatedComboColumn = -1;
            this.DbColumn51.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn51.ColumnDBControl = null;
            this.DbColumn51.ColumnDBFieldData = "";
            this.DbColumn51.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn51.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn51.ComboBlankSelection = true;
            this.DbColumn51.ComboImageList = null;
            this.DbColumn51.ComboListField = "";
            this.DbColumn51.DBGridViewFilters = null;
            this.DbColumn51.Decimals = 2;
            this.DbColumn51.DefaultValue = "";
            this.DbColumn51.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn51.Encrypted = false;
            this.DbColumn51.Expression = "";
            this.DbColumn51.FieldDB = "descripcion";
            this.DbColumn51.Font = null;
            this.DbColumn51.Format = null;
            this.DbColumn51.FormatString = null;
            this.DbColumn51.HeaderCaption = "Decripción";
            this.DbColumn51.Hidden = false;
            this.DbColumn51.LastValue = false;
            this.DbColumn51.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn51.MaskInput = null;
            this.DbColumn51.MaxLength = 0;
            this.DbColumn51.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn51.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn51.MultiLine = false;
            this.DbColumn51.NullValue = null;
            this.DbColumn51.Obligatory = false;
            this.DbColumn51.PromptChar = '\0';
            this.DbColumn51.ReadColumn = false;
            this.DbColumn51.ShowSelectForm = true;
            this.DbColumn51.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn51.ToolTip = "";
            this.DbColumn51.Unique = false;
            this.DbColumn51.Width = 0;
            // 
            // DbColumn52
            // 
            this.DbColumn52.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn52.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn52.AllowNull = false;
            this.DbColumn52.AllowRowFiltering = false;
            this.DbColumn52.AsociatedButtonColumn = -1;
            this.DbColumn52.AsociatedComboColumn = -1;
            this.DbColumn52.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn52.ColumnDBControl = this.DbControl11;
            this.DbColumn52.ColumnDBFieldData = "id";
            this.DbColumn52.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn52.ColumnType = FSFormControls.DBColumn.ColumnTypes.ComboColumn;
            this.DbColumn52.ComboBlankSelection = true;
            this.DbColumn52.ComboImageList = null;
            this.DbColumn52.ComboListField = "descripcion";
            this.DbColumn52.DBGridViewFilters = null;
            this.DbColumn52.Decimals = 2;
            this.DbColumn52.DefaultValue = "";
            this.DbColumn52.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn52.Encrypted = false;
            this.DbColumn52.Expression = "";
            this.DbColumn52.FieldDB = "estado";
            this.DbColumn52.Font = null;
            this.DbColumn52.Format = null;
            this.DbColumn52.FormatString = null;
            this.DbColumn52.HeaderCaption = "Estado";
            this.DbColumn52.Hidden = false;
            this.DbColumn52.LastValue = false;
            this.DbColumn52.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn52.MaskInput = null;
            this.DbColumn52.MaxLength = 0;
            this.DbColumn52.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn52.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn52.MultiLine = false;
            this.DbColumn52.NullValue = null;
            this.DbColumn52.Obligatory = false;
            this.DbColumn52.PromptChar = '\0';
            this.DbColumn52.ReadColumn = false;
            this.DbColumn52.ShowSelectForm = true;
            this.DbColumn52.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn52.ToolTip = "";
            this.DbColumn52.Unique = false;
            this.DbColumn52.Width = 0;
            // 
            // TabPage1
            // 
            this.TabPage1.Controls.Add(this.dbcServicios);
            this.TabPage1.Controls.Add(this.DbControl13);
            this.TabPage1.Controls.Add(this.DbGrid1);
            this.TabPage1.Location = new System.Drawing.Point(4, 22);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Size = new System.Drawing.Size(721, 279);
            this.TabPage1.TabIndex = 7;
            this.TabPage1.Text = "Servicios cliente";
            // 
            // dbcServicios
            // 
            this.dbcServicios.About = null;
            this.dbcServicios.ArrayList = null;
            this.dbcServicios.AutoConnect = true;
            this.dbcServicios.DataControl = null;
            this.dbcServicios.DataSet = null;
            this.dbcServicios.DataTable = null;
            this.dbcServicios.DataView = null;
            this.dbcServicios.DBFieldData = "";
            this.dbcServicios.DBPosition = 0;
            this.dbcServicios.EraseDBControl = null;
            this.dbcServicios.Filter = "";
            this.dbcServicios.isEOF = true;
            this.dbcServicios.Location = new System.Drawing.Point(320, 126);
            this.dbcServicios.LOCK = null;
            this.dbcServicios.LOPD = null;
            this.dbcServicios.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.dbcServicios.Name = "dbcServicios";
            this.dbcServicios.Page = 0;
            this.dbcServicios.PageSettings = null;
            this.dbcServicios.Paging = false;
            this.dbcServicios.PagingSize = 0;
            this.dbcServicios.ReadOnly = false;
            this.dbcServicios.RelationDataControl = null;
            this.dbcServicios.RelationDBField = "";
            this.dbcServicios.RelationParentDBField = "";
            this.dbcServicios.SaveError = false;
            this.dbcServicios.SaveOnChangeRecord = false;
            this.dbcServicios.Selection = "select * from servicios";
            this.dbcServicios.Size = new System.Drawing.Size(80, 48);
            this.dbcServicios.StoreInBase64Format = false;
            this.dbcServicios.TabIndex = 16;
            this.dbcServicios.TableName = "servicios";
            this.dbcServicios.TabStop = false;
            this.dbcServicios.Text = "SQL: select * from servicios";
            this.dbcServicios.Track = false;
            this.dbcServicios.TypeDB = FSFormControls.DBControl.DbType.SQLServer;
            this.dbcServicios.Versionable = false;
            this.dbcServicios.VersionableDateField = "";
            this.dbcServicios.VersionableTable = "";
            this.dbcServicios.VersionableUserField = "";
            this.dbcServicios.VersionableVersionField = "";
            this.dbcServicios.Visible = false;
            this.dbcServicios.XmlFile = "";
            this.dbcServicios.XMLName = "";
            // 
            // DbControl13
            // 
            this.DbControl13.About = null;
            this.DbControl13.ArrayList = null;
            this.DbControl13.AutoConnect = true;
            this.DbControl13.DataControl = null;
            this.DbControl13.DataSet = null;
            this.DbControl13.DataTable = null;
            this.DbControl13.DataView = null;
            this.DbControl13.DBFieldData = "";
            this.DbControl13.DBPosition = 0;
            this.DbControl13.EraseDBControl = null;
            this.DbControl13.Filter = "";
            this.DbControl13.isEOF = true;
            this.DbControl13.Location = new System.Drawing.Point(360, 112);
            this.DbControl13.LOCK = null;
            this.DbControl13.LOPD = null;
            this.DbControl13.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbControl13.Name = "DbControl13";
            this.DbControl13.Page = 0;
            this.DbControl13.PageSettings = null;
            this.DbControl13.Paging = false;
            this.DbControl13.PagingSize = 0;
            this.DbControl13.ReadOnly = false;
            this.DbControl13.RelationDataControl = null;
            this.DbControl13.RelationDBField = "";
            this.DbControl13.RelationParentDBField = "";
            this.DbControl13.SaveError = false;
            this.DbControl13.SaveOnChangeRecord = false;
            this.DbControl13.Selection = "select * from periodos";
            this.DbControl13.Size = new System.Drawing.Size(96, 40);
            this.DbControl13.StoreInBase64Format = false;
            this.DbControl13.TabIndex = 1;
            this.DbControl13.TableName = "periodos";
            this.DbControl13.TabStop = false;
            this.DbControl13.Text = "SQL: select * from periodos";
            this.DbControl13.Track = false;
            this.DbControl13.TypeDB = FSFormControls.DBControl.DbType.SQLServer;
            this.DbControl13.Versionable = false;
            this.DbControl13.VersionableDateField = "";
            this.DbControl13.VersionableTable = "";
            this.DbControl13.VersionableUserField = "";
            this.DbControl13.VersionableVersionField = "";
            this.DbControl13.Visible = false;
            this.DbControl13.XmlFile = "";
            this.DbControl13.XMLName = "";
            // 
            // DbGrid1
            // 
            this.DbGrid1.About = null;
            this.DbGrid1.AllowAddNew = true;
            this.DbGrid1.AllowDelete = true;
            this.DbGrid1.AllowDrop = true;
            this.DbGrid1.AllowSorting = true;
            this.DbGrid1.AlternatingColor = System.Drawing.Color.Empty;
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
            this.DbColumn5,
            this.DbColumn6,
            this.DbColumn62,
            this.DbColumn63,
            this.DbColumn64,
            this.DbColumn65});
            this.DbGrid1.CurrentRowIndex = -1;
            this.DbGrid1.CustomColumnHeaders = false;
            this.DbGrid1.DataControl = this.DbControl2;
            this.DbGrid1.DefaultDecimals = 2;
            this.DbGrid1.DefaultHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DbGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DbGrid1.Editable = true;
            this.DbGrid1.FlatMode = false;
            this.DbGrid1.GridLineColor = System.Drawing.SystemColors.Control;
            this.DbGrid1.GridLineStyle = System.Windows.Forms.DataGridLineStyle.Solid;
            this.DbGrid1.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.DbGrid1.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DbGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DbGrid1.LastCol = -1;
            this.DbGrid1.LastRow = -1;
            this.DbGrid1.Location = new System.Drawing.Point(0, 0);
            this.DbGrid1.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbGrid1.Name = "DbGrid1";
            this.DbGrid1.RecordMode = false;
            this.DbGrid1.RowHeadersVisible = true;
            this.DbGrid1.RowHeight = 16;
            this.DbGrid1.RowSel = -1;
            this.DbGrid1.RowsInCaption = 2;
            this.DbGrid1.ShowRecordScrollBar = false;
            this.DbGrid1.ShowTotals = false;
            this.DbGrid1.Size = new System.Drawing.Size(721, 279);
            this.DbGrid1.TabIndex = 0;
            this.DbGrid1.TotalOperation = FSFormControls.DBColumn.OperationTypes.Sum;
            this.DbGrid1.Track = false;
            this.DbGrid1.XMLName = "";
            // 
            // DbColumn5
            // 
            this.DbColumn5.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn5.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn5.AllowNull = false;
            this.DbColumn5.AllowRowFiltering = false;
            this.DbColumn5.AsociatedButtonColumn = -1;
            this.DbColumn5.AsociatedComboColumn = -1;
            this.DbColumn5.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn5.ColumnDBControl = this.dbcServicios;
            this.DbColumn5.ColumnDBFieldData = "";
            this.DbColumn5.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn5.ColumnType = FSFormControls.DBColumn.ColumnTypes.ButtonColumn;
            this.DbColumn5.ComboBlankSelection = true;
            this.DbColumn5.ComboImageList = null;
            this.DbColumn5.ComboListField = "";
            this.DbColumn5.DBGridViewFilters = null;
            this.DbColumn5.Decimals = 2;
            this.DbColumn5.DefaultValue = "";
            this.DbColumn5.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn5.Encrypted = false;
            this.DbColumn5.Expression = "";
            this.DbColumn5.FieldDB = "codServicio";
            this.DbColumn5.Font = null;
            this.DbColumn5.Format = null;
            this.DbColumn5.FormatString = null;
            this.DbColumn5.HeaderCaption = "Código";
            this.DbColumn5.Hidden = false;
            this.DbColumn5.LastValue = false;
            this.DbColumn5.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn5.MaskInput = null;
            this.DbColumn5.MaxLength = 0;
            this.DbColumn5.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn5.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn5.MultiLine = false;
            this.DbColumn5.NullValue = null;
            this.DbColumn5.Obligatory = false;
            this.DbColumn5.PromptChar = '\0';
            this.DbColumn5.ReadColumn = false;
            this.DbColumn5.ShowSelectForm = true;
            this.DbColumn5.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn5.ToolTip = "";
            this.DbColumn5.Unique = false;
            this.DbColumn5.Width = 0;
            // 
            // DbColumn6
            // 
            this.DbColumn6.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn6.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn6.AllowNull = false;
            this.DbColumn6.AllowRowFiltering = false;
            this.DbColumn6.AsociatedButtonColumn = 0;
            this.DbColumn6.AsociatedComboColumn = -1;
            this.DbColumn6.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn6.ColumnDBControl = null;
            this.DbColumn6.ColumnDBFieldData = "";
            this.DbColumn6.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn6.ColumnType = FSFormControls.DBColumn.ColumnTypes.DescriptionColumn;
            this.DbColumn6.ComboBlankSelection = true;
            this.DbColumn6.ComboImageList = null;
            this.DbColumn6.ComboListField = "";
            this.DbColumn6.DBGridViewFilters = null;
            this.DbColumn6.Decimals = 2;
            this.DbColumn6.DefaultValue = "";
            this.DbColumn6.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn6.Encrypted = false;
            this.DbColumn6.Expression = "";
            this.DbColumn6.FieldDB = "descripcion";
            this.DbColumn6.Font = null;
            this.DbColumn6.Format = null;
            this.DbColumn6.FormatString = null;
            this.DbColumn6.HeaderCaption = "Descripción";
            this.DbColumn6.Hidden = false;
            this.DbColumn6.LastValue = false;
            this.DbColumn6.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn6.MaskInput = null;
            this.DbColumn6.MaxLength = 0;
            this.DbColumn6.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn6.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn6.MultiLine = false;
            this.DbColumn6.NullValue = null;
            this.DbColumn6.Obligatory = false;
            this.DbColumn6.PromptChar = '\0';
            this.DbColumn6.ReadColumn = false;
            this.DbColumn6.ShowSelectForm = true;
            this.DbColumn6.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn6.ToolTip = "";
            this.DbColumn6.Unique = false;
            this.DbColumn6.Width = 0;
            // 
            // DbColumn62
            // 
            this.DbColumn62.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn62.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn62.AllowNull = false;
            this.DbColumn62.AllowRowFiltering = false;
            this.DbColumn62.AsociatedButtonColumn = -1;
            this.DbColumn62.AsociatedComboColumn = -1;
            this.DbColumn62.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn62.ColumnDBControl = this.DbControl13;
            this.DbColumn62.ColumnDBFieldData = "idPeriodicidad";
            this.DbColumn62.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn62.ColumnType = FSFormControls.DBColumn.ColumnTypes.ComboColumn;
            this.DbColumn62.ComboBlankSelection = true;
            this.DbColumn62.ComboImageList = null;
            this.DbColumn62.ComboListField = "descripcion";
            this.DbColumn62.DBGridViewFilters = null;
            this.DbColumn62.Decimals = 2;
            this.DbColumn62.DefaultValue = "";
            this.DbColumn62.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn62.Encrypted = false;
            this.DbColumn62.Expression = "";
            this.DbColumn62.FieldDB = "Periodicidad";
            this.DbColumn62.Font = null;
            this.DbColumn62.Format = null;
            this.DbColumn62.FormatString = null;
            this.DbColumn62.HeaderCaption = "Periodicidad";
            this.DbColumn62.Hidden = false;
            this.DbColumn62.LastValue = false;
            this.DbColumn62.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn62.MaskInput = null;
            this.DbColumn62.MaxLength = 0;
            this.DbColumn62.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn62.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn62.MultiLine = false;
            this.DbColumn62.NullValue = null;
            this.DbColumn62.Obligatory = false;
            this.DbColumn62.PromptChar = '\0';
            this.DbColumn62.ReadColumn = false;
            this.DbColumn62.ShowSelectForm = true;
            this.DbColumn62.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn62.ToolTip = "";
            this.DbColumn62.Unique = false;
            this.DbColumn62.Width = 0;
            // 
            // DbColumn63
            // 
            this.DbColumn63.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn63.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn63.AllowNull = false;
            this.DbColumn63.AllowRowFiltering = false;
            this.DbColumn63.AsociatedButtonColumn = -1;
            this.DbColumn63.AsociatedComboColumn = -1;
            this.DbColumn63.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn63.ColumnDBControl = null;
            this.DbColumn63.ColumnDBFieldData = "";
            this.DbColumn63.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn63.ColumnType = FSFormControls.DBColumn.ColumnTypes.DateColumn;
            this.DbColumn63.ComboBlankSelection = true;
            this.DbColumn63.ComboImageList = null;
            this.DbColumn63.ComboListField = "";
            this.DbColumn63.DBGridViewFilters = null;
            this.DbColumn63.Decimals = 2;
            this.DbColumn63.DefaultValue = "";
            this.DbColumn63.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn63.Encrypted = false;
            this.DbColumn63.Expression = "";
            this.DbColumn63.FieldDB = "fechaInicio";
            this.DbColumn63.Font = null;
            this.DbColumn63.Format = null;
            this.DbColumn63.FormatString = null;
            this.DbColumn63.HeaderCaption = "Fecha Inicio";
            this.DbColumn63.Hidden = false;
            this.DbColumn63.LastValue = false;
            this.DbColumn63.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn63.MaskInput = null;
            this.DbColumn63.MaxLength = 0;
            this.DbColumn63.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn63.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn63.MultiLine = false;
            this.DbColumn63.NullValue = null;
            this.DbColumn63.Obligatory = false;
            this.DbColumn63.PromptChar = '\0';
            this.DbColumn63.ReadColumn = false;
            this.DbColumn63.ShowSelectForm = true;
            this.DbColumn63.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn63.ToolTip = "";
            this.DbColumn63.Unique = false;
            this.DbColumn63.Width = 0;
            // 
            // DbColumn64
            // 
            this.DbColumn64.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn64.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn64.AllowNull = false;
            this.DbColumn64.AllowRowFiltering = false;
            this.DbColumn64.AsociatedButtonColumn = -1;
            this.DbColumn64.AsociatedComboColumn = -1;
            this.DbColumn64.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn64.ColumnDBControl = null;
            this.DbColumn64.ColumnDBFieldData = "";
            this.DbColumn64.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn64.ColumnType = FSFormControls.DBColumn.ColumnTypes.DateColumn;
            this.DbColumn64.ComboBlankSelection = true;
            this.DbColumn64.ComboImageList = null;
            this.DbColumn64.ComboListField = "";
            this.DbColumn64.DBGridViewFilters = null;
            this.DbColumn64.Decimals = 2;
            this.DbColumn64.DefaultValue = "";
            this.DbColumn64.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn64.Encrypted = false;
            this.DbColumn64.Expression = "";
            this.DbColumn64.FieldDB = "fechaFin";
            this.DbColumn64.Font = null;
            this.DbColumn64.Format = null;
            this.DbColumn64.FormatString = null;
            this.DbColumn64.HeaderCaption = "Fecha Fin";
            this.DbColumn64.Hidden = false;
            this.DbColumn64.LastValue = false;
            this.DbColumn64.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn64.MaskInput = null;
            this.DbColumn64.MaxLength = 0;
            this.DbColumn64.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn64.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn64.MultiLine = false;
            this.DbColumn64.NullValue = null;
            this.DbColumn64.Obligatory = false;
            this.DbColumn64.PromptChar = '\0';
            this.DbColumn64.ReadColumn = false;
            this.DbColumn64.ShowSelectForm = true;
            this.DbColumn64.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn64.ToolTip = "";
            this.DbColumn64.Unique = false;
            this.DbColumn64.Width = 0;
            // 
            // DbColumn65
            // 
            this.DbColumn65.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn65.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn65.AllowNull = false;
            this.DbColumn65.AllowRowFiltering = false;
            this.DbColumn65.AsociatedButtonColumn = 0;
            this.DbColumn65.AsociatedComboColumn = -1;
            this.DbColumn65.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn65.ColumnDBControl = null;
            this.DbColumn65.ColumnDBFieldData = "";
            this.DbColumn65.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn65.ColumnType = FSFormControls.DBColumn.ColumnTypes.DescriptionColumn;
            this.DbColumn65.ComboBlankSelection = true;
            this.DbColumn65.ComboImageList = null;
            this.DbColumn65.ComboListField = "";
            this.DbColumn65.DBGridViewFilters = null;
            this.DbColumn65.Decimals = 2;
            this.DbColumn65.DefaultValue = "";
            this.DbColumn65.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.NumberDescription;
            this.DbColumn65.Encrypted = false;
            this.DbColumn65.Expression = "";
            this.DbColumn65.FieldDB = "precio";
            this.DbColumn65.Font = null;
            this.DbColumn65.Format = null;
            this.DbColumn65.FormatString = null;
            this.DbColumn65.HeaderCaption = "Precio";
            this.DbColumn65.Hidden = false;
            this.DbColumn65.LastValue = false;
            this.DbColumn65.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn65.MaskInput = null;
            this.DbColumn65.MaxLength = 0;
            this.DbColumn65.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn65.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn65.MultiLine = false;
            this.DbColumn65.NullValue = null;
            this.DbColumn65.Obligatory = false;
            this.DbColumn65.PromptChar = '\0';
            this.DbColumn65.ReadColumn = false;
            this.DbColumn65.ShowSelectForm = true;
            this.DbColumn65.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn65.ToolTip = "";
            this.DbColumn65.Unique = false;
            this.DbColumn65.Width = 0;
            // 
            // DbControl2
            // 
            this.DbControl2.About = null;
            this.DbControl2.ArrayList = null;
            this.DbControl2.AutoConnect = true;
            this.DbControl2.DataControl = null;
            this.DbControl2.DataSet = null;
            this.DbControl2.DataTable = null;
            this.DbControl2.DataView = null;
            this.DbControl2.DBFieldData = "";
            this.DbControl2.DBPosition = 0;
            this.DbControl2.EraseDBControl = null;
            this.DbControl2.Filter = "";
            this.DbControl2.isEOF = true;
            this.DbControl2.Location = new System.Drawing.Point(264, 384);
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
            this.DbControl2.RelationDBField = "idCliente";
            this.DbControl2.RelationParentDBField = "idCliente";
            this.DbControl2.SaveError = false;
            this.DbControl2.SaveOnChangeRecord = false;
            this.DbControl2.Selection = "select * from serviciosCliente where idCliente=?";
            this.DbControl2.Size = new System.Drawing.Size(96, 48);
            this.DbControl2.StoreInBase64Format = false;
            this.DbControl2.TabIndex = 14;
            this.DbControl2.TableName = "serviciosCliente";
            this.DbControl2.TabStop = false;
            this.DbControl2.Text = "SQL: select * from serviciosCliente where idCliente=?";
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
            // TabPage7
            // 
            this.TabPage7.Controls.Add(this.DbGrid5);
            this.TabPage7.Location = new System.Drawing.Point(4, 22);
            this.TabPage7.Name = "TabPage7";
            this.TabPage7.Size = new System.Drawing.Size(721, 279);
            this.TabPage7.TabIndex = 6;
            this.TabPage7.Text = "Contratos / Proyectos";
            // 
            // DbGrid5
            // 
            this.DbGrid5.About = null;
            this.DbGrid5.AllowAddNew = true;
            this.DbGrid5.AllowDelete = false;
            this.DbGrid5.AllowDrop = true;
            this.DbGrid5.AllowSorting = true;
            this.DbGrid5.AlternatingColor = System.Drawing.Color.Empty;
            this.DbGrid5.AutoSave = true;
            this.DbGrid5.AutoSize = true;
            this.DbGrid5.BackGroundColor = System.Drawing.Color.LightGray;
            this.DbGrid5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DbGrid5.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.DbGrid5.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.DbGrid5.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.DbGrid5.CaptionText = null;
            this.DbGrid5.CaptionVisible = true;
            this.DbGrid5.ColumnHeadersVisible = true;
            this.DbGrid5.Columns.AddRange(new FSFormControls.DBColumn[] {
            this.DbColumn4,
            this.empDbColumn19,
            this.empDbColumn18});
            this.DbGrid5.CurrentRowIndex = -1;
            this.DbGrid5.CustomColumnHeaders = false;
            this.DbGrid5.DataControl = this.DbControl7;
            this.DbGrid5.DefaultDecimals = 2;
            this.DbGrid5.DefaultHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DbGrid5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DbGrid5.Editable = true;
            this.DbGrid5.FlatMode = false;
            this.DbGrid5.GridLineColor = System.Drawing.SystemColors.Control;
            this.DbGrid5.GridLineStyle = System.Windows.Forms.DataGridLineStyle.Solid;
            this.DbGrid5.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.DbGrid5.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DbGrid5.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DbGrid5.LastCol = -1;
            this.DbGrid5.LastRow = -1;
            this.DbGrid5.Location = new System.Drawing.Point(0, 0);
            this.DbGrid5.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbGrid5.Name = "DbGrid5";
            this.DbGrid5.RecordMode = false;
            this.DbGrid5.RowHeadersVisible = true;
            this.DbGrid5.RowHeight = 16;
            this.DbGrid5.RowSel = -1;
            this.DbGrid5.RowsInCaption = 2;
            this.DbGrid5.ShowRecordScrollBar = false;
            this.DbGrid5.ShowTotals = false;
            this.DbGrid5.Size = new System.Drawing.Size(721, 279);
            this.DbGrid5.TabIndex = 5;
            this.DbGrid5.TotalOperation = FSFormControls.DBColumn.OperationTypes.Sum;
            this.DbGrid5.Track = false;
            this.DbGrid5.XMLName = "";
            this.DbGrid5.DoubleClick += new FSFormControls.DBGrid.DoubleClickEventHandler(this.DbGrid5_DoubleClick);
            // 
            // DbColumn4
            // 
            this.DbColumn4.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn4.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn4.AllowNull = false;
            this.DbColumn4.AllowRowFiltering = false;
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
            this.DbColumn4.DBGridViewFilters = null;
            this.DbColumn4.Decimals = 2;
            this.DbColumn4.DefaultValue = "";
            this.DbColumn4.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn4.Encrypted = false;
            this.DbColumn4.Expression = "";
            this.DbColumn4.FieldDB = "idContrato";
            this.DbColumn4.Font = null;
            this.DbColumn4.Format = null;
            this.DbColumn4.FormatString = null;
            this.DbColumn4.HeaderCaption = "Código";
            this.DbColumn4.Hidden = false;
            this.DbColumn4.LastValue = false;
            this.DbColumn4.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn4.MaskInput = null;
            this.DbColumn4.MaxLength = 0;
            this.DbColumn4.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn4.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn4.MultiLine = false;
            this.DbColumn4.NullValue = null;
            this.DbColumn4.Obligatory = false;
            this.DbColumn4.PromptChar = '\0';
            this.DbColumn4.ReadColumn = true;
            this.DbColumn4.ShowSelectForm = true;
            this.DbColumn4.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn4.ToolTip = "";
            this.DbColumn4.Unique = false;
            this.DbColumn4.Width = 0;
            // 
            // DbControl7
            // 
            this.DbControl7.About = null;
            this.DbControl7.ArrayList = null;
            this.DbControl7.AutoConnect = true;
            this.DbControl7.DataControl = null;
            this.DbControl7.DataSet = null;
            this.DbControl7.DataTable = null;
            this.DbControl7.DataView = null;
            this.DbControl7.DBFieldData = "";
            this.DbControl7.DBPosition = 0;
            this.DbControl7.EraseDBControl = null;
            this.DbControl7.Filter = "";
            this.DbControl7.isEOF = true;
            this.DbControl7.Location = new System.Drawing.Point(432, 384);
            this.DbControl7.LOCK = null;
            this.DbControl7.LOPD = null;
            this.DbControl7.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbControl7.Name = "DbControl7";
            this.DbControl7.Page = 0;
            this.DbControl7.PageSettings = null;
            this.DbControl7.Paging = false;
            this.DbControl7.PagingSize = 0;
            this.DbControl7.ReadOnly = false;
            this.DbControl7.RelationDataControl = this.DbControl1;
            this.DbControl7.RelationDBField = "idCliente";
            this.DbControl7.RelationParentDBField = "idCliente";
            this.DbControl7.SaveError = false;
            this.DbControl7.SaveOnChangeRecord = false;
            this.DbControl7.Selection = "select * from contratos where idCliente=?";
            this.DbControl7.Size = new System.Drawing.Size(112, 40);
            this.DbControl7.StoreInBase64Format = false;
            this.DbControl7.TabIndex = 10;
            this.DbControl7.TableName = "contratos";
            this.DbControl7.TabStop = false;
            this.DbControl7.Text = "SQL: select * from contratos where idCliente=?";
            this.DbControl7.Track = false;
            this.DbControl7.TypeDB = FSFormControls.DBControl.DbType.SQLServer;
            this.DbControl7.Versionable = false;
            this.DbControl7.VersionableDateField = "";
            this.DbControl7.VersionableTable = "";
            this.DbControl7.VersionableUserField = "";
            this.DbControl7.VersionableVersionField = "";
            this.DbControl7.Visible = false;
            this.DbControl7.XmlFile = "";
            this.DbControl7.XMLName = "";
            // 
            // TabPage2
            // 
            this.TabPage2.Controls.Add(this.DbImage1);
            this.TabPage2.Location = new System.Drawing.Point(4, 22);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Size = new System.Drawing.Size(721, 279);
            this.TabPage2.TabIndex = 8;
            this.TabPage2.Text = "Logotipo";
            // 
            // DbImage1
            // 
            this.DbImage1.About = null;
            this.DbImage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DbImage1.DataControl = this.DbControl1;
            this.DbImage1.DBField = "logotipo";
            this.DbImage1.Location = new System.Drawing.Point(24, 24);
            this.DbImage1.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
            this.DbImage1.Name = "DbImage1";
            this.DbImage1.Size = new System.Drawing.Size(145, 141);
            this.DbImage1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.DbImage1.TabIndex = 16;
            this.DbImage1.Track = false;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dbGridView1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(721, 279);
            this.tabPage4.TabIndex = 10;
            this.tabPage4.Text = "Servicios";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dbGridView1
            // 
            this.dbGridView1.About = "";
            this.dbGridView1.ActiveCell = null;
            this.dbGridView1.ActiveRow = null;
            this.dbGridView1.AllowAddNew = true;
            this.dbGridView1.AllowDelete = true;
            this.dbGridView1.AllowDrop = true;
            this.dbGridView1.AlternatingColor = System.Drawing.Color.Empty;
            this.dbGridView1.AutoSave = true;
            this.dbGridView1.AutoSize = true;
            this.dbGridView1.CaptionText = null;
            this.dbGridView1.DataControl = this.dbcServicios;
            this.dbGridView1.DataSource = null;
            this.dbGridView1.DefaultDecimals = 2;
            this.dbGridView1.DefaultHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dbGridViewDisplayLayout1.Appearance = null;
            dbGridViewDisplayLayout1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dbGridViewDisplayLayout1.CaptionVisible = false;
            dbGridViewDisplayLayout1.GroupByBox = null;
            dbGridViewDisplayLayout1.Override = null;
            this.dbGridView1.DisplayLayout = dbGridViewDisplayLayout1;
            this.dbGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbGridView1.Editable = true;
            this.dbGridView1.Location = new System.Drawing.Point(3, 3);
            this.dbGridView1.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.dbGridView1.MultiSelect = false;
            this.dbGridView1.Name = "dbGridView1";
            this.dbGridView1.RecordMode = false;
            this.dbGridView1.RowHeadersWidth = 41;
            this.dbGridView1.RowsInCaption = 2;
            this.dbGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dbGridView1.ShowExpand = false;
            this.dbGridView1.ShowRecordScrollBar = false;
            this.dbGridView1.ShowTotals = false;
            this.dbGridView1.Size = new System.Drawing.Size(715, 273);
            this.dbGridView1.SortedColumns = null;
            this.dbGridView1.TabIndex = 0;
            this.dbGridView1.TotalOperation = FSFormControls.DBColumn.OperationTypes.Sum;
            this.dbGridView1.Track = false;
            // 
            // DbControl8
            // 
            this.DbControl8.About = null;
            this.DbControl8.ArrayList = null;
            this.DbControl8.AutoConnect = true;
            this.DbControl8.DataControl = null;
            this.DbControl8.DataSet = null;
            this.DbControl8.DataTable = null;
            this.DbControl8.DataView = null;
            this.DbControl8.DBFieldData = "";
            this.DbControl8.DBPosition = 0;
            this.DbControl8.EraseDBControl = null;
            this.DbControl8.Filter = "";
            this.DbControl8.isEOF = true;
            this.DbControl8.Location = new System.Drawing.Point(176, 384);
            this.DbControl8.LOCK = null;
            this.DbControl8.LOPD = null;
            this.DbControl8.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbControl8.Name = "DbControl8";
            this.DbControl8.Page = 0;
            this.DbControl8.PageSettings = null;
            this.DbControl8.Paging = false;
            this.DbControl8.PagingSize = 0;
            this.DbControl8.ReadOnly = false;
            this.DbControl8.RelationDataControl = null;
            this.DbControl8.RelationDBField = "";
            this.DbControl8.RelationParentDBField = "";
            this.DbControl8.SaveError = false;
            this.DbControl8.SaveOnChangeRecord = false;
            this.DbControl8.Selection = "select * from servicios";
            this.DbControl8.Size = new System.Drawing.Size(80, 48);
            this.DbControl8.StoreInBase64Format = false;
            this.DbControl8.TabIndex = 15;
            this.DbControl8.TableName = "servicios";
            this.DbControl8.TabStop = false;
            this.DbControl8.Text = "SQL: select * from servicios";
            this.DbControl8.Track = false;
            this.DbControl8.TypeDB = FSFormControls.DBControl.DbType.SQLServer;
            this.DbControl8.Versionable = false;
            this.DbControl8.VersionableDateField = "";
            this.DbControl8.VersionableTable = "";
            this.DbControl8.VersionableUserField = "";
            this.DbControl8.VersionableVersionField = "";
            this.DbControl8.Visible = false;
            this.DbControl8.XmlFile = "";
            this.DbControl8.XMLName = "";
            // 
            // DbColumn61
            // 
            this.DbColumn61.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn61.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn61.AllowNull = false;
            this.DbColumn61.AllowRowFiltering = false;
            this.DbColumn61.AsociatedButtonColumn = -1;
            this.DbColumn61.AsociatedComboColumn = -1;
            this.DbColumn61.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn61.ColumnDBControl = null;
            this.DbColumn61.ColumnDBFieldData = "";
            this.DbColumn61.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn61.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn61.ComboBlankSelection = true;
            this.DbColumn61.ComboImageList = null;
            this.DbColumn61.ComboListField = "";
            this.DbColumn61.DBGridViewFilters = null;
            this.DbColumn61.Decimals = 2;
            this.DbColumn61.DefaultValue = "";
            this.DbColumn61.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn61.Encrypted = false;
            this.DbColumn61.Expression = "";
            this.DbColumn61.FieldDB = "id";
            this.DbColumn61.Font = null;
            this.DbColumn61.Format = null;
            this.DbColumn61.FormatString = null;
            this.DbColumn61.HeaderCaption = "Código";
            this.DbColumn61.Hidden = false;
            this.DbColumn61.LastValue = false;
            this.DbColumn61.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn61.MaskInput = null;
            this.DbColumn61.MaxLength = 0;
            this.DbColumn61.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn61.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn61.MultiLine = false;
            this.DbColumn61.NullValue = null;
            this.DbColumn61.Obligatory = false;
            this.DbColumn61.PromptChar = '\0';
            this.DbColumn61.ReadColumn = false;
            this.DbColumn61.ShowSelectForm = true;
            this.DbColumn61.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn61.ToolTip = "";
            this.DbColumn61.Unique = false;
            this.DbColumn61.Width = 0;
            // 
            // DbColumn53
            // 
            this.DbColumn53.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn53.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn53.AllowNull = false;
            this.DbColumn53.AllowRowFiltering = false;
            this.DbColumn53.AsociatedButtonColumn = -1;
            this.DbColumn53.AsociatedComboColumn = -1;
            this.DbColumn53.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn53.ColumnDBControl = null;
            this.DbColumn53.ColumnDBFieldData = "";
            this.DbColumn53.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn53.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn53.ComboBlankSelection = true;
            this.DbColumn53.ComboImageList = null;
            this.DbColumn53.ComboListField = "";
            this.DbColumn53.DBGridViewFilters = null;
            this.DbColumn53.Decimals = 2;
            this.DbColumn53.DefaultValue = "";
            this.DbColumn53.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn53.Encrypted = false;
            this.DbColumn53.Expression = "";
            this.DbColumn53.FieldDB = "nombre";
            this.DbColumn53.Font = null;
            this.DbColumn53.Format = null;
            this.DbColumn53.FormatString = null;
            this.DbColumn53.HeaderCaption = "Nombre";
            this.DbColumn53.Hidden = false;
            this.DbColumn53.LastValue = false;
            this.DbColumn53.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn53.MaskInput = null;
            this.DbColumn53.MaxLength = 0;
            this.DbColumn53.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn53.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn53.MultiLine = false;
            this.DbColumn53.NullValue = null;
            this.DbColumn53.Obligatory = false;
            this.DbColumn53.PromptChar = '\0';
            this.DbColumn53.ReadColumn = false;
            this.DbColumn53.ShowSelectForm = true;
            this.DbColumn53.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn53.ToolTip = "";
            this.DbColumn53.Unique = false;
            this.DbColumn53.Width = 0;
            // 
            // DbColumn54
            // 
            this.DbColumn54.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn54.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn54.AllowNull = false;
            this.DbColumn54.AllowRowFiltering = false;
            this.DbColumn54.AsociatedButtonColumn = -1;
            this.DbColumn54.AsociatedComboColumn = -1;
            this.DbColumn54.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn54.ColumnDBControl = null;
            this.DbColumn54.ColumnDBFieldData = "";
            this.DbColumn54.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn54.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn54.ComboBlankSelection = true;
            this.DbColumn54.ComboImageList = null;
            this.DbColumn54.ComboListField = "";
            this.DbColumn54.DBGridViewFilters = null;
            this.DbColumn54.Decimals = 2;
            this.DbColumn54.DefaultValue = "";
            this.DbColumn54.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn54.Encrypted = false;
            this.DbColumn54.Expression = "";
            this.DbColumn54.FieldDB = "local";
            this.DbColumn54.Font = null;
            this.DbColumn54.Format = null;
            this.DbColumn54.FormatString = null;
            this.DbColumn54.HeaderCaption = "Local";
            this.DbColumn54.Hidden = false;
            this.DbColumn54.LastValue = false;
            this.DbColumn54.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn54.MaskInput = null;
            this.DbColumn54.MaxLength = 0;
            this.DbColumn54.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn54.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn54.MultiLine = false;
            this.DbColumn54.NullValue = null;
            this.DbColumn54.Obligatory = false;
            this.DbColumn54.PromptChar = '\0';
            this.DbColumn54.ReadColumn = false;
            this.DbColumn54.ShowSelectForm = true;
            this.DbColumn54.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn54.ToolTip = "";
            this.DbColumn54.Unique = false;
            this.DbColumn54.Width = 0;
            // 
            // DbColumn55
            // 
            this.DbColumn55.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn55.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn55.AllowNull = false;
            this.DbColumn55.AllowRowFiltering = false;
            this.DbColumn55.AsociatedButtonColumn = -1;
            this.DbColumn55.AsociatedComboColumn = -1;
            this.DbColumn55.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn55.ColumnDBControl = null;
            this.DbColumn55.ColumnDBFieldData = "id";
            this.DbColumn55.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn55.ColumnType = FSFormControls.DBColumn.ColumnTypes.ComboColumn;
            this.DbColumn55.ComboBlankSelection = true;
            this.DbColumn55.ComboImageList = null;
            this.DbColumn55.ComboListField = "descripcion";
            this.DbColumn55.DBGridViewFilters = null;
            this.DbColumn55.Decimals = 2;
            this.DbColumn55.DefaultValue = "";
            this.DbColumn55.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn55.Encrypted = false;
            this.DbColumn55.Expression = "";
            this.DbColumn55.FieldDB = "tipoUbicacion";
            this.DbColumn55.Font = null;
            this.DbColumn55.Format = null;
            this.DbColumn55.FormatString = null;
            this.DbColumn55.HeaderCaption = "Ubicación";
            this.DbColumn55.Hidden = false;
            this.DbColumn55.LastValue = false;
            this.DbColumn55.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn55.MaskInput = null;
            this.DbColumn55.MaxLength = 0;
            this.DbColumn55.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn55.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn55.MultiLine = false;
            this.DbColumn55.NullValue = null;
            this.DbColumn55.Obligatory = false;
            this.DbColumn55.PromptChar = '\0';
            this.DbColumn55.ReadColumn = false;
            this.DbColumn55.ShowSelectForm = true;
            this.DbColumn55.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn55.ToolTip = "";
            this.DbColumn55.Unique = false;
            this.DbColumn55.Width = 0;
            // 
            // DbColumn56
            // 
            this.DbColumn56.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn56.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn56.AllowNull = false;
            this.DbColumn56.AllowRowFiltering = false;
            this.DbColumn56.AsociatedButtonColumn = -1;
            this.DbColumn56.AsociatedComboColumn = -1;
            this.DbColumn56.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn56.ColumnDBControl = null;
            this.DbColumn56.ColumnDBFieldData = "";
            this.DbColumn56.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn56.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn56.ComboBlankSelection = true;
            this.DbColumn56.ComboImageList = null;
            this.DbColumn56.ComboListField = "";
            this.DbColumn56.DBGridViewFilters = null;
            this.DbColumn56.Decimals = 2;
            this.DbColumn56.DefaultValue = "";
            this.DbColumn56.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn56.Encrypted = false;
            this.DbColumn56.Expression = "";
            this.DbColumn56.FieldDB = "direccionhs";
            this.DbColumn56.Font = null;
            this.DbColumn56.Format = null;
            this.DbColumn56.FormatString = null;
            this.DbColumn56.HeaderCaption = "Dirección";
            this.DbColumn56.Hidden = false;
            this.DbColumn56.LastValue = false;
            this.DbColumn56.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn56.MaskInput = null;
            this.DbColumn56.MaxLength = 0;
            this.DbColumn56.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn56.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn56.MultiLine = false;
            this.DbColumn56.NullValue = null;
            this.DbColumn56.Obligatory = false;
            this.DbColumn56.PromptChar = '\0';
            this.DbColumn56.ReadColumn = false;
            this.DbColumn56.ShowSelectForm = true;
            this.DbColumn56.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn56.ToolTip = "";
            this.DbColumn56.Unique = false;
            this.DbColumn56.Width = 0;
            // 
            // DbColumn57
            // 
            this.DbColumn57.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn57.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn57.AllowNull = false;
            this.DbColumn57.AllowRowFiltering = false;
            this.DbColumn57.AsociatedButtonColumn = -1;
            this.DbColumn57.AsociatedComboColumn = -1;
            this.DbColumn57.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn57.ColumnDBControl = null;
            this.DbColumn57.ColumnDBFieldData = "";
            this.DbColumn57.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn57.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn57.ComboBlankSelection = true;
            this.DbColumn57.ComboImageList = null;
            this.DbColumn57.ComboListField = "";
            this.DbColumn57.DBGridViewFilters = null;
            this.DbColumn57.Decimals = 2;
            this.DbColumn57.DefaultValue = "";
            this.DbColumn57.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn57.Encrypted = false;
            this.DbColumn57.Expression = "";
            this.DbColumn57.FieldDB = "poblacionhs";
            this.DbColumn57.Font = null;
            this.DbColumn57.Format = null;
            this.DbColumn57.FormatString = null;
            this.DbColumn57.HeaderCaption = "Población";
            this.DbColumn57.Hidden = false;
            this.DbColumn57.LastValue = false;
            this.DbColumn57.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn57.MaskInput = null;
            this.DbColumn57.MaxLength = 0;
            this.DbColumn57.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn57.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn57.MultiLine = false;
            this.DbColumn57.NullValue = null;
            this.DbColumn57.Obligatory = false;
            this.DbColumn57.PromptChar = '\0';
            this.DbColumn57.ReadColumn = false;
            this.DbColumn57.ShowSelectForm = true;
            this.DbColumn57.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn57.ToolTip = "";
            this.DbColumn57.Unique = false;
            this.DbColumn57.Width = 0;
            // 
            // DbColumn58
            // 
            this.DbColumn58.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn58.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn58.AllowNull = false;
            this.DbColumn58.AllowRowFiltering = false;
            this.DbColumn58.AsociatedButtonColumn = -1;
            this.DbColumn58.AsociatedComboColumn = -1;
            this.DbColumn58.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn58.ColumnDBControl = null;
            this.DbColumn58.ColumnDBFieldData = "";
            this.DbColumn58.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn58.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn58.ComboBlankSelection = true;
            this.DbColumn58.ComboImageList = null;
            this.DbColumn58.ComboListField = "";
            this.DbColumn58.DBGridViewFilters = null;
            this.DbColumn58.Decimals = 2;
            this.DbColumn58.DefaultValue = "";
            this.DbColumn58.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn58.Encrypted = false;
            this.DbColumn58.Expression = "";
            this.DbColumn58.FieldDB = "provinciahs";
            this.DbColumn58.Font = null;
            this.DbColumn58.Format = null;
            this.DbColumn58.FormatString = null;
            this.DbColumn58.HeaderCaption = "Provincia";
            this.DbColumn58.Hidden = false;
            this.DbColumn58.LastValue = false;
            this.DbColumn58.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn58.MaskInput = null;
            this.DbColumn58.MaxLength = 0;
            this.DbColumn58.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn58.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn58.MultiLine = false;
            this.DbColumn58.NullValue = null;
            this.DbColumn58.Obligatory = false;
            this.DbColumn58.PromptChar = '\0';
            this.DbColumn58.ReadColumn = false;
            this.DbColumn58.ShowSelectForm = true;
            this.DbColumn58.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn58.ToolTip = "";
            this.DbColumn58.Unique = false;
            this.DbColumn58.Width = 0;
            // 
            // DbColumn59
            // 
            this.DbColumn59.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn59.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn59.AllowNull = false;
            this.DbColumn59.AllowRowFiltering = false;
            this.DbColumn59.AsociatedButtonColumn = -1;
            this.DbColumn59.AsociatedComboColumn = -1;
            this.DbColumn59.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn59.ColumnDBControl = null;
            this.DbColumn59.ColumnDBFieldData = "";
            this.DbColumn59.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn59.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn59.ComboBlankSelection = true;
            this.DbColumn59.ComboImageList = null;
            this.DbColumn59.ComboListField = "";
            this.DbColumn59.DBGridViewFilters = null;
            this.DbColumn59.Decimals = 2;
            this.DbColumn59.DefaultValue = "";
            this.DbColumn59.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn59.Encrypted = false;
            this.DbColumn59.Expression = "";
            this.DbColumn59.FieldDB = "cphs";
            this.DbColumn59.Font = null;
            this.DbColumn59.Format = null;
            this.DbColumn59.FormatString = null;
            this.DbColumn59.HeaderCaption = "C.P.";
            this.DbColumn59.Hidden = false;
            this.DbColumn59.LastValue = false;
            this.DbColumn59.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn59.MaskInput = null;
            this.DbColumn59.MaxLength = 0;
            this.DbColumn59.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn59.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn59.MultiLine = false;
            this.DbColumn59.NullValue = null;
            this.DbColumn59.Obligatory = false;
            this.DbColumn59.PromptChar = '\0';
            this.DbColumn59.ReadColumn = false;
            this.DbColumn59.ShowSelectForm = true;
            this.DbColumn59.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn59.ToolTip = "";
            this.DbColumn59.Unique = false;
            this.DbColumn59.Width = 0;
            // 
            // DbColumn60
            // 
            this.DbColumn60.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn60.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn60.AllowNull = false;
            this.DbColumn60.AllowRowFiltering = false;
            this.DbColumn60.AsociatedButtonColumn = -1;
            this.DbColumn60.AsociatedComboColumn = -1;
            this.DbColumn60.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn60.ColumnDBControl = null;
            this.DbColumn60.ColumnDBFieldData = "";
            this.DbColumn60.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn60.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn60.ComboBlankSelection = true;
            this.DbColumn60.ComboImageList = null;
            this.DbColumn60.ComboListField = "";
            this.DbColumn60.DBGridViewFilters = null;
            this.DbColumn60.Decimals = 2;
            this.DbColumn60.DefaultValue = "";
            this.DbColumn60.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn60.Encrypted = false;
            this.DbColumn60.Expression = "";
            this.DbColumn60.FieldDB = "telefono1hs";
            this.DbColumn60.Font = null;
            this.DbColumn60.Format = null;
            this.DbColumn60.FormatString = null;
            this.DbColumn60.HeaderCaption = "Telefono";
            this.DbColumn60.Hidden = false;
            this.DbColumn60.LastValue = false;
            this.DbColumn60.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn60.MaskInput = null;
            this.DbColumn60.MaxLength = 0;
            this.DbColumn60.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn60.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn60.MultiLine = false;
            this.DbColumn60.NullValue = null;
            this.DbColumn60.Obligatory = false;
            this.DbColumn60.PromptChar = '\0';
            this.DbColumn60.ReadColumn = false;
            this.DbColumn60.ShowSelectForm = true;
            this.DbColumn60.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn60.ToolTip = "";
            this.DbColumn60.Unique = false;
            this.DbColumn60.Width = 0;
            // 
            // DbColumn45
            // 
            this.DbColumn45.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn45.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn45.AllowNull = false;
            this.DbColumn45.AllowRowFiltering = false;
            this.DbColumn45.AsociatedButtonColumn = -1;
            this.DbColumn45.AsociatedComboColumn = -1;
            this.DbColumn45.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn45.ColumnDBControl = null;
            this.DbColumn45.ColumnDBFieldData = "";
            this.DbColumn45.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn45.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn45.ComboBlankSelection = true;
            this.DbColumn45.ComboImageList = null;
            this.DbColumn45.ComboListField = "";
            this.DbColumn45.DBGridViewFilters = null;
            this.DbColumn45.Decimals = 2;
            this.DbColumn45.DefaultValue = "";
            this.DbColumn45.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn45.Encrypted = false;
            this.DbColumn45.Expression = "";
            this.DbColumn45.FieldDB = "idUsuario";
            this.DbColumn45.Font = null;
            this.DbColumn45.Format = null;
            this.DbColumn45.FormatString = null;
            this.DbColumn45.HeaderCaption = "Código";
            this.DbColumn45.Hidden = false;
            this.DbColumn45.LastValue = false;
            this.DbColumn45.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn45.MaskInput = null;
            this.DbColumn45.MaxLength = 0;
            this.DbColumn45.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn45.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn45.MultiLine = false;
            this.DbColumn45.NullValue = null;
            this.DbColumn45.Obligatory = false;
            this.DbColumn45.PromptChar = '\0';
            this.DbColumn45.ReadColumn = false;
            this.DbColumn45.ShowSelectForm = true;
            this.DbColumn45.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn45.ToolTip = "";
            this.DbColumn45.Unique = false;
            this.DbColumn45.Width = 0;
            // 
            // DbColumn46
            // 
            this.DbColumn46.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn46.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn46.AllowNull = false;
            this.DbColumn46.AllowRowFiltering = false;
            this.DbColumn46.AsociatedButtonColumn = -1;
            this.DbColumn46.AsociatedComboColumn = -1;
            this.DbColumn46.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn46.ColumnDBControl = null;
            this.DbColumn46.ColumnDBFieldData = "";
            this.DbColumn46.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn46.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn46.ComboBlankSelection = true;
            this.DbColumn46.ComboImageList = null;
            this.DbColumn46.ComboListField = "";
            this.DbColumn46.DBGridViewFilters = null;
            this.DbColumn46.Decimals = 2;
            this.DbColumn46.DefaultValue = "";
            this.DbColumn46.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn46.Encrypted = false;
            this.DbColumn46.Expression = "";
            this.DbColumn46.FieldDB = "nombre";
            this.DbColumn46.Font = null;
            this.DbColumn46.Format = null;
            this.DbColumn46.FormatString = null;
            this.DbColumn46.HeaderCaption = "Nombre";
            this.DbColumn46.Hidden = false;
            this.DbColumn46.LastValue = false;
            this.DbColumn46.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn46.MaskInput = null;
            this.DbColumn46.MaxLength = 0;
            this.DbColumn46.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn46.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn46.MultiLine = false;
            this.DbColumn46.NullValue = null;
            this.DbColumn46.Obligatory = false;
            this.DbColumn46.PromptChar = '\0';
            this.DbColumn46.ReadColumn = false;
            this.DbColumn46.ShowSelectForm = true;
            this.DbColumn46.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn46.ToolTip = "";
            this.DbColumn46.Unique = false;
            this.DbColumn46.Width = 0;
            // 
            // DbColumn47
            // 
            this.DbColumn47.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn47.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn47.AllowNull = false;
            this.DbColumn47.AllowRowFiltering = false;
            this.DbColumn47.AsociatedButtonColumn = -1;
            this.DbColumn47.AsociatedComboColumn = -1;
            this.DbColumn47.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn47.ColumnDBControl = null;
            this.DbColumn47.ColumnDBFieldData = "";
            this.DbColumn47.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn47.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn47.ComboBlankSelection = true;
            this.DbColumn47.ComboImageList = null;
            this.DbColumn47.ComboListField = "";
            this.DbColumn47.DBGridViewFilters = null;
            this.DbColumn47.Decimals = 2;
            this.DbColumn47.DefaultValue = "";
            this.DbColumn47.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn47.Encrypted = false;
            this.DbColumn47.Expression = "";
            this.DbColumn47.FieldDB = "usuario";
            this.DbColumn47.Font = null;
            this.DbColumn47.Format = null;
            this.DbColumn47.FormatString = null;
            this.DbColumn47.HeaderCaption = "Usuario";
            this.DbColumn47.Hidden = false;
            this.DbColumn47.LastValue = false;
            this.DbColumn47.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn47.MaskInput = null;
            this.DbColumn47.MaxLength = 0;
            this.DbColumn47.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn47.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn47.MultiLine = false;
            this.DbColumn47.NullValue = null;
            this.DbColumn47.Obligatory = false;
            this.DbColumn47.PromptChar = '\0';
            this.DbColumn47.ReadColumn = false;
            this.DbColumn47.ShowSelectForm = true;
            this.DbColumn47.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn47.ToolTip = "";
            this.DbColumn47.Unique = false;
            this.DbColumn47.Width = 0;
            // 
            // DbColumn48
            // 
            this.DbColumn48.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn48.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn48.AllowNull = false;
            this.DbColumn48.AllowRowFiltering = false;
            this.DbColumn48.AsociatedButtonColumn = -1;
            this.DbColumn48.AsociatedComboColumn = -1;
            this.DbColumn48.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn48.ColumnDBControl = null;
            this.DbColumn48.ColumnDBFieldData = "";
            this.DbColumn48.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn48.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn48.ComboBlankSelection = true;
            this.DbColumn48.ComboImageList = null;
            this.DbColumn48.ComboListField = "";
            this.DbColumn48.DBGridViewFilters = null;
            this.DbColumn48.Decimals = 2;
            this.DbColumn48.DefaultValue = "";
            this.DbColumn48.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn48.Encrypted = false;
            this.DbColumn48.Expression = "";
            this.DbColumn48.FieldDB = "clave";
            this.DbColumn48.Font = null;
            this.DbColumn48.Format = null;
            this.DbColumn48.FormatString = null;
            this.DbColumn48.HeaderCaption = "Password";
            this.DbColumn48.Hidden = false;
            this.DbColumn48.LastValue = false;
            this.DbColumn48.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn48.MaskInput = null;
            this.DbColumn48.MaxLength = 0;
            this.DbColumn48.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn48.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn48.MultiLine = false;
            this.DbColumn48.NullValue = null;
            this.DbColumn48.Obligatory = false;
            this.DbColumn48.PromptChar = '\0';
            this.DbColumn48.ReadColumn = false;
            this.DbColumn48.ShowSelectForm = true;
            this.DbColumn48.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn48.ToolTip = "";
            this.DbColumn48.Unique = false;
            this.DbColumn48.Width = 0;
            // 
            // DbColumn49
            // 
            this.DbColumn49.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn49.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn49.AllowNull = false;
            this.DbColumn49.AllowRowFiltering = false;
            this.DbColumn49.AsociatedButtonColumn = -1;
            this.DbColumn49.AsociatedComboColumn = -1;
            this.DbColumn49.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn49.ColumnDBControl = null;
            this.DbColumn49.ColumnDBFieldData = "";
            this.DbColumn49.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn49.ColumnType = FSFormControls.DBColumn.ColumnTypes.NumberColumn;
            this.DbColumn49.ComboBlankSelection = true;
            this.DbColumn49.ComboImageList = null;
            this.DbColumn49.ComboListField = "";
            this.DbColumn49.DBGridViewFilters = null;
            this.DbColumn49.Decimals = 0;
            this.DbColumn49.DefaultValue = "";
            this.DbColumn49.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn49.Encrypted = false;
            this.DbColumn49.Expression = "";
            this.DbColumn49.FieldDB = "nivelAcceso";
            this.DbColumn49.Font = null;
            this.DbColumn49.Format = null;
            this.DbColumn49.FormatString = null;
            this.DbColumn49.HeaderCaption = "Nivel Acceso";
            this.DbColumn49.Hidden = false;
            this.DbColumn49.LastValue = false;
            this.DbColumn49.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn49.MaskInput = null;
            this.DbColumn49.MaxLength = 0;
            this.DbColumn49.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn49.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn49.MultiLine = false;
            this.DbColumn49.NullValue = null;
            this.DbColumn49.Obligatory = false;
            this.DbColumn49.PromptChar = '\0';
            this.DbColumn49.ReadColumn = false;
            this.DbColumn49.ShowSelectForm = true;
            this.DbColumn49.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn49.ToolTip = "";
            this.DbColumn49.Unique = false;
            this.DbColumn49.Width = 0;
            // 
            // DbColumn17
            // 
            this.DbColumn17.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn17.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn17.AllowNull = false;
            this.DbColumn17.AllowRowFiltering = false;
            this.DbColumn17.AsociatedButtonColumn = -1;
            this.DbColumn17.AsociatedComboColumn = -1;
            this.DbColumn17.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn17.ColumnDBControl = null;
            this.DbColumn17.ColumnDBFieldData = "";
            this.DbColumn17.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn17.ColumnType = FSFormControls.DBColumn.ColumnTypes.DateColumn;
            this.DbColumn17.ComboBlankSelection = true;
            this.DbColumn17.ComboImageList = null;
            this.DbColumn17.ComboListField = "";
            this.DbColumn17.DBGridViewFilters = null;
            this.DbColumn17.Decimals = 2;
            this.DbColumn17.DefaultValue = "";
            this.DbColumn17.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn17.Encrypted = false;
            this.DbColumn17.Expression = "";
            this.DbColumn17.FieldDB = "FechaEvaluacionRiesgos";
            this.DbColumn17.Font = null;
            this.DbColumn17.Format = null;
            this.DbColumn17.FormatString = null;
            this.DbColumn17.HeaderCaption = "Fecha Evaluación Riesgos";
            this.DbColumn17.Hidden = false;
            this.DbColumn17.LastValue = false;
            this.DbColumn17.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn17.MaskInput = null;
            this.DbColumn17.MaxLength = 0;
            this.DbColumn17.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn17.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn17.MultiLine = false;
            this.DbColumn17.NullValue = null;
            this.DbColumn17.Obligatory = false;
            this.DbColumn17.PromptChar = '\0';
            this.DbColumn17.ReadColumn = false;
            this.DbColumn17.ShowSelectForm = true;
            this.DbColumn17.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn17.ToolTip = "";
            this.DbColumn17.Unique = false;
            this.DbColumn17.Width = 0;
            // 
            // DbColumn19
            // 
            this.DbColumn19.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn19.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn19.AllowNull = false;
            this.DbColumn19.AllowRowFiltering = false;
            this.DbColumn19.AsociatedButtonColumn = -1;
            this.DbColumn19.AsociatedComboColumn = -1;
            this.DbColumn19.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn19.ColumnDBControl = null;
            this.DbColumn19.ColumnDBFieldData = "";
            this.DbColumn19.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn19.ColumnType = FSFormControls.DBColumn.ColumnTypes.CheckColumn;
            this.DbColumn19.ComboBlankSelection = true;
            this.DbColumn19.ComboImageList = null;
            this.DbColumn19.ComboListField = "";
            this.DbColumn19.DBGridViewFilters = null;
            this.DbColumn19.Decimals = 2;
            this.DbColumn19.DefaultValue = "";
            this.DbColumn19.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn19.Encrypted = false;
            this.DbColumn19.Expression = "";
            this.DbColumn19.FieldDB = "planificacionPreventivaAnual";
            this.DbColumn19.Font = null;
            this.DbColumn19.Format = null;
            this.DbColumn19.FormatString = null;
            this.DbColumn19.HeaderCaption = "Planificación Preventiva Anual";
            this.DbColumn19.Hidden = false;
            this.DbColumn19.LastValue = false;
            this.DbColumn19.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn19.MaskInput = null;
            this.DbColumn19.MaxLength = 0;
            this.DbColumn19.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn19.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn19.MultiLine = false;
            this.DbColumn19.NullValue = null;
            this.DbColumn19.Obligatory = false;
            this.DbColumn19.PromptChar = '\0';
            this.DbColumn19.ReadColumn = false;
            this.DbColumn19.ShowSelectForm = true;
            this.DbColumn19.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn19.ToolTip = "";
            this.DbColumn19.Unique = false;
            this.DbColumn19.Width = 0;
            // 
            // DbColumn20
            // 
            this.DbColumn20.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn20.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn20.AllowNull = false;
            this.DbColumn20.AllowRowFiltering = false;
            this.DbColumn20.AsociatedButtonColumn = -1;
            this.DbColumn20.AsociatedComboColumn = -1;
            this.DbColumn20.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn20.ColumnDBControl = null;
            this.DbColumn20.ColumnDBFieldData = "";
            this.DbColumn20.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn20.ColumnType = FSFormControls.DBColumn.ColumnTypes.CheckColumn;
            this.DbColumn20.ComboBlankSelection = true;
            this.DbColumn20.ComboImageList = null;
            this.DbColumn20.ComboListField = "";
            this.DbColumn20.DBGridViewFilters = null;
            this.DbColumn20.Decimals = 2;
            this.DbColumn20.DefaultValue = "";
            this.DbColumn20.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn20.Encrypted = false;
            this.DbColumn20.Expression = "";
            this.DbColumn20.FieldDB = "PlanEmergencia";
            this.DbColumn20.Font = null;
            this.DbColumn20.Format = null;
            this.DbColumn20.FormatString = null;
            this.DbColumn20.HeaderCaption = "Plan Emergencia";
            this.DbColumn20.Hidden = false;
            this.DbColumn20.LastValue = false;
            this.DbColumn20.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn20.MaskInput = null;
            this.DbColumn20.MaxLength = 0;
            this.DbColumn20.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn20.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn20.MultiLine = false;
            this.DbColumn20.NullValue = null;
            this.DbColumn20.Obligatory = false;
            this.DbColumn20.PromptChar = '\0';
            this.DbColumn20.ReadColumn = false;
            this.DbColumn20.ShowSelectForm = true;
            this.DbColumn20.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn20.ToolTip = "";
            this.DbColumn20.Unique = false;
            this.DbColumn20.Width = 0;
            // 
            // DbColumn21
            // 
            this.DbColumn21.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn21.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn21.AllowNull = false;
            this.DbColumn21.AllowRowFiltering = false;
            this.DbColumn21.AsociatedButtonColumn = -1;
            this.DbColumn21.AsociatedComboColumn = -1;
            this.DbColumn21.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn21.ColumnDBControl = null;
            this.DbColumn21.ColumnDBFieldData = "";
            this.DbColumn21.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn21.ColumnType = FSFormControls.DBColumn.ColumnTypes.CheckColumn;
            this.DbColumn21.ComboBlankSelection = true;
            this.DbColumn21.ComboImageList = null;
            this.DbColumn21.ComboListField = "";
            this.DbColumn21.DBGridViewFilters = null;
            this.DbColumn21.Decimals = 2;
            this.DbColumn21.DefaultValue = "";
            this.DbColumn21.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn21.Encrypted = false;
            this.DbColumn21.Expression = "";
            this.DbColumn21.FieldDB = "Cursos";
            this.DbColumn21.Font = null;
            this.DbColumn21.Format = null;
            this.DbColumn21.FormatString = null;
            this.DbColumn21.HeaderCaption = "Cursos";
            this.DbColumn21.Hidden = false;
            this.DbColumn21.LastValue = false;
            this.DbColumn21.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn21.MaskInput = null;
            this.DbColumn21.MaxLength = 0;
            this.DbColumn21.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn21.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn21.MultiLine = false;
            this.DbColumn21.NullValue = null;
            this.DbColumn21.Obligatory = false;
            this.DbColumn21.PromptChar = '\0';
            this.DbColumn21.ReadColumn = false;
            this.DbColumn21.ShowSelectForm = true;
            this.DbColumn21.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn21.ToolTip = "";
            this.DbColumn21.Unique = false;
            this.DbColumn21.Width = 0;
            // 
            // DbColumn22
            // 
            this.DbColumn22.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn22.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn22.AllowNull = false;
            this.DbColumn22.AllowRowFiltering = false;
            this.DbColumn22.AsociatedButtonColumn = -1;
            this.DbColumn22.AsociatedComboColumn = -1;
            this.DbColumn22.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn22.ColumnDBControl = null;
            this.DbColumn22.ColumnDBFieldData = "";
            this.DbColumn22.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn22.ColumnType = FSFormControls.DBColumn.ColumnTypes.CheckColumn;
            this.DbColumn22.ComboBlankSelection = true;
            this.DbColumn22.ComboImageList = null;
            this.DbColumn22.ComboListField = "";
            this.DbColumn22.DBGridViewFilters = null;
            this.DbColumn22.Decimals = 2;
            this.DbColumn22.DefaultValue = "";
            this.DbColumn22.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn22.Encrypted = false;
            this.DbColumn22.Expression = "";
            this.DbColumn22.FieldDB = "Charlas";
            this.DbColumn22.Font = null;
            this.DbColumn22.Format = null;
            this.DbColumn22.FormatString = null;
            this.DbColumn22.HeaderCaption = "Charlas";
            this.DbColumn22.Hidden = false;
            this.DbColumn22.LastValue = false;
            this.DbColumn22.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn22.MaskInput = null;
            this.DbColumn22.MaxLength = 0;
            this.DbColumn22.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn22.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn22.MultiLine = false;
            this.DbColumn22.NullValue = null;
            this.DbColumn22.Obligatory = false;
            this.DbColumn22.PromptChar = '\0';
            this.DbColumn22.ReadColumn = false;
            this.DbColumn22.ShowSelectForm = true;
            this.DbColumn22.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn22.ToolTip = "";
            this.DbColumn22.Unique = false;
            this.DbColumn22.Width = 0;
            // 
            // DbColumn23
            // 
            this.DbColumn23.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn23.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn23.AllowNull = false;
            this.DbColumn23.AllowRowFiltering = false;
            this.DbColumn23.AsociatedButtonColumn = -1;
            this.DbColumn23.AsociatedComboColumn = -1;
            this.DbColumn23.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn23.ColumnDBControl = null;
            this.DbColumn23.ColumnDBFieldData = "";
            this.DbColumn23.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn23.ColumnType = FSFormControls.DBColumn.ColumnTypes.NumberColumn;
            this.DbColumn23.ComboBlankSelection = true;
            this.DbColumn23.ComboImageList = null;
            this.DbColumn23.ComboListField = "";
            this.DbColumn23.DBGridViewFilters = null;
            this.DbColumn23.Decimals = 0;
            this.DbColumn23.DefaultValue = "";
            this.DbColumn23.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn23.Encrypted = false;
            this.DbColumn23.Expression = "";
            this.DbColumn23.FieldDB = "numeroPuestosEvaluados";
            this.DbColumn23.Font = null;
            this.DbColumn23.Format = null;
            this.DbColumn23.FormatString = null;
            this.DbColumn23.HeaderCaption = "Nº Puestos Evaluados";
            this.DbColumn23.Hidden = false;
            this.DbColumn23.LastValue = false;
            this.DbColumn23.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn23.MaskInput = null;
            this.DbColumn23.MaxLength = 0;
            this.DbColumn23.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn23.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn23.MultiLine = false;
            this.DbColumn23.NullValue = null;
            this.DbColumn23.Obligatory = false;
            this.DbColumn23.PromptChar = '\0';
            this.DbColumn23.ReadColumn = false;
            this.DbColumn23.ShowSelectForm = true;
            this.DbColumn23.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn23.ToolTip = "";
            this.DbColumn23.Unique = false;
            this.DbColumn23.Width = 0;
            // 
            // DbColumn24
            // 
            this.DbColumn24.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn24.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn24.AllowNull = false;
            this.DbColumn24.AllowRowFiltering = false;
            this.DbColumn24.AsociatedButtonColumn = -1;
            this.DbColumn24.AsociatedComboColumn = -1;
            this.DbColumn24.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn24.ColumnDBControl = null;
            this.DbColumn24.ColumnDBFieldData = "";
            this.DbColumn24.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn24.ColumnType = FSFormControls.DBColumn.ColumnTypes.NumberColumn;
            this.DbColumn24.ComboBlankSelection = true;
            this.DbColumn24.ComboImageList = null;
            this.DbColumn24.ComboListField = "";
            this.DbColumn24.DBGridViewFilters = null;
            this.DbColumn24.Decimals = 0;
            this.DbColumn24.DefaultValue = "";
            this.DbColumn24.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn24.Encrypted = false;
            this.DbColumn24.Expression = "";
            this.DbColumn24.FieldDB = "numeroAccidentesInvestigados";
            this.DbColumn24.Font = null;
            this.DbColumn24.Format = null;
            this.DbColumn24.FormatString = null;
            this.DbColumn24.HeaderCaption = "Nº Accidentes Investigados";
            this.DbColumn24.Hidden = false;
            this.DbColumn24.LastValue = false;
            this.DbColumn24.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn24.MaskInput = null;
            this.DbColumn24.MaxLength = 0;
            this.DbColumn24.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn24.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn24.MultiLine = false;
            this.DbColumn24.NullValue = null;
            this.DbColumn24.Obligatory = false;
            this.DbColumn24.PromptChar = '\0';
            this.DbColumn24.ReadColumn = false;
            this.DbColumn24.ShowSelectForm = true;
            this.DbColumn24.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn24.ToolTip = "";
            this.DbColumn24.Unique = false;
            this.DbColumn24.Width = 0;
            // 
            // DbColumn9
            // 
            this.DbColumn9.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn9.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn9.AllowNull = false;
            this.DbColumn9.AllowRowFiltering = false;
            this.DbColumn9.AsociatedButtonColumn = -1;
            this.DbColumn9.AsociatedComboColumn = -1;
            this.DbColumn9.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn9.ColumnDBControl = null;
            this.DbColumn9.ColumnDBFieldData = "";
            this.DbColumn9.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn9.ColumnType = FSFormControls.DBColumn.ColumnTypes.DateColumn;
            this.DbColumn9.ComboBlankSelection = true;
            this.DbColumn9.ComboImageList = null;
            this.DbColumn9.ComboListField = "";
            this.DbColumn9.DBGridViewFilters = null;
            this.DbColumn9.Decimals = 2;
            this.DbColumn9.DefaultValue = "";
            this.DbColumn9.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn9.Encrypted = false;
            this.DbColumn9.Expression = "";
            this.DbColumn9.FieldDB = "fechaVisita";
            this.DbColumn9.Font = null;
            this.DbColumn9.Format = null;
            this.DbColumn9.FormatString = null;
            this.DbColumn9.HeaderCaption = "Fecha Visita";
            this.DbColumn9.Hidden = false;
            this.DbColumn9.LastValue = false;
            this.DbColumn9.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn9.MaskInput = null;
            this.DbColumn9.MaxLength = 0;
            this.DbColumn9.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn9.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn9.MultiLine = false;
            this.DbColumn9.NullValue = null;
            this.DbColumn9.Obligatory = false;
            this.DbColumn9.PromptChar = '\0';
            this.DbColumn9.ReadColumn = false;
            this.DbColumn9.ShowSelectForm = true;
            this.DbColumn9.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn9.ToolTip = "";
            this.DbColumn9.Unique = false;
            this.DbColumn9.Width = 0;
            // 
            // DbColumn10
            // 
            this.DbColumn10.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn10.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn10.AllowNull = false;
            this.DbColumn10.AllowRowFiltering = false;
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
            this.DbColumn10.DBGridViewFilters = null;
            this.DbColumn10.Decimals = 2;
            this.DbColumn10.DefaultValue = "";
            this.DbColumn10.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn10.Encrypted = false;
            this.DbColumn10.Expression = "";
            this.DbColumn10.FieldDB = "descripcion";
            this.DbColumn10.Font = null;
            this.DbColumn10.Format = null;
            this.DbColumn10.FormatString = null;
            this.DbColumn10.HeaderCaption = "Descripción";
            this.DbColumn10.Hidden = false;
            this.DbColumn10.LastValue = false;
            this.DbColumn10.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn10.MaskInput = null;
            this.DbColumn10.MaxLength = 0;
            this.DbColumn10.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn10.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn10.MultiLine = false;
            this.DbColumn10.NullValue = null;
            this.DbColumn10.Obligatory = false;
            this.DbColumn10.PromptChar = '\0';
            this.DbColumn10.ReadColumn = false;
            this.DbColumn10.ShowSelectForm = true;
            this.DbColumn10.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn10.ToolTip = "";
            this.DbColumn10.Unique = false;
            this.DbColumn10.Width = 0;
            // 
            // DbColumn13
            // 
            this.DbColumn13.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn13.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn13.AllowNull = false;
            this.DbColumn13.AllowRowFiltering = false;
            this.DbColumn13.AsociatedButtonColumn = -1;
            this.DbColumn13.AsociatedComboColumn = -1;
            this.DbColumn13.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn13.ColumnDBControl = null;
            this.DbColumn13.ColumnDBFieldData = "";
            this.DbColumn13.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn13.ColumnType = FSFormControls.DBColumn.ColumnTypes.DateColumn;
            this.DbColumn13.ComboBlankSelection = true;
            this.DbColumn13.ComboImageList = null;
            this.DbColumn13.ComboListField = "";
            this.DbColumn13.DBGridViewFilters = null;
            this.DbColumn13.Decimals = 2;
            this.DbColumn13.DefaultValue = "";
            this.DbColumn13.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn13.Encrypted = false;
            this.DbColumn13.Expression = "";
            this.DbColumn13.FieldDB = "FechaRealizacion";
            this.DbColumn13.Font = null;
            this.DbColumn13.Format = null;
            this.DbColumn13.FormatString = null;
            this.DbColumn13.HeaderCaption = "Fecha de Realización";
            this.DbColumn13.Hidden = false;
            this.DbColumn13.LastValue = false;
            this.DbColumn13.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn13.MaskInput = null;
            this.DbColumn13.MaxLength = 0;
            this.DbColumn13.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn13.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn13.MultiLine = false;
            this.DbColumn13.NullValue = null;
            this.DbColumn13.Obligatory = false;
            this.DbColumn13.PromptChar = '\0';
            this.DbColumn13.ReadColumn = false;
            this.DbColumn13.ShowSelectForm = true;
            this.DbColumn13.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn13.ToolTip = "";
            this.DbColumn13.Unique = false;
            this.DbColumn13.Width = 0;
            // 
            // DbColumn14
            // 
            this.DbColumn14.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn14.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn14.AllowNull = false;
            this.DbColumn14.AllowRowFiltering = false;
            this.DbColumn14.AsociatedButtonColumn = -1;
            this.DbColumn14.AsociatedComboColumn = -1;
            this.DbColumn14.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn14.ColumnDBControl = null;
            this.DbColumn14.ColumnDBFieldData = "";
            this.DbColumn14.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn14.ColumnType = FSFormControls.DBColumn.ColumnTypes.NumberColumn;
            this.DbColumn14.ComboBlankSelection = true;
            this.DbColumn14.ComboImageList = null;
            this.DbColumn14.ComboListField = "";
            this.DbColumn14.DBGridViewFilters = null;
            this.DbColumn14.Decimals = 0;
            this.DbColumn14.DefaultValue = "";
            this.DbColumn14.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn14.Encrypted = false;
            this.DbColumn14.Expression = "";
            this.DbColumn14.FieldDB = "NumeroRMRealizados";
            this.DbColumn14.Font = null;
            this.DbColumn14.Format = null;
            this.DbColumn14.FormatString = null;
            this.DbColumn14.HeaderCaption = "Nº RM Realizados";
            this.DbColumn14.Hidden = false;
            this.DbColumn14.LastValue = false;
            this.DbColumn14.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn14.MaskInput = null;
            this.DbColumn14.MaxLength = 0;
            this.DbColumn14.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn14.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn14.MultiLine = false;
            this.DbColumn14.NullValue = null;
            this.DbColumn14.Obligatory = false;
            this.DbColumn14.PromptChar = '\0';
            this.DbColumn14.ReadColumn = false;
            this.DbColumn14.ShowSelectForm = true;
            this.DbColumn14.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn14.ToolTip = "";
            this.DbColumn14.Unique = false;
            this.DbColumn14.Width = 0;
            // 
            // DbColumn15
            // 
            this.DbColumn15.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn15.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn15.AllowNull = false;
            this.DbColumn15.AllowRowFiltering = false;
            this.DbColumn15.AsociatedButtonColumn = -1;
            this.DbColumn15.AsociatedComboColumn = -1;
            this.DbColumn15.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn15.ColumnDBControl = null;
            this.DbColumn15.ColumnDBFieldData = "";
            this.DbColumn15.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn15.ColumnType = FSFormControls.DBColumn.ColumnTypes.CheckColumn;
            this.DbColumn15.ComboBlankSelection = true;
            this.DbColumn15.ComboImageList = null;
            this.DbColumn15.ComboListField = "";
            this.DbColumn15.DBGridViewFilters = null;
            this.DbColumn15.Decimals = 2;
            this.DbColumn15.DefaultValue = "";
            this.DbColumn15.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn15.Encrypted = false;
            this.DbColumn15.Expression = "";
            this.DbColumn15.FieldDB = "primerosAuxilios";
            this.DbColumn15.Font = null;
            this.DbColumn15.Format = null;
            this.DbColumn15.FormatString = null;
            this.DbColumn15.HeaderCaption = "Primeros Auxilios";
            this.DbColumn15.Hidden = false;
            this.DbColumn15.LastValue = false;
            this.DbColumn15.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn15.MaskInput = null;
            this.DbColumn15.MaxLength = 0;
            this.DbColumn15.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn15.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn15.MultiLine = false;
            this.DbColumn15.NullValue = null;
            this.DbColumn15.Obligatory = false;
            this.DbColumn15.PromptChar = '\0';
            this.DbColumn15.ReadColumn = false;
            this.DbColumn15.ShowSelectForm = true;
            this.DbColumn15.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn15.ToolTip = "";
            this.DbColumn15.Unique = false;
            this.DbColumn15.Width = 0;
            // 
            // DbColumn16
            // 
            this.DbColumn16.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn16.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn16.AllowNull = false;
            this.DbColumn16.AllowRowFiltering = false;
            this.DbColumn16.AsociatedButtonColumn = -1;
            this.DbColumn16.AsociatedComboColumn = -1;
            this.DbColumn16.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn16.ColumnDBControl = null;
            this.DbColumn16.ColumnDBFieldData = "";
            this.DbColumn16.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn16.ColumnType = FSFormControls.DBColumn.ColumnTypes.CheckColumn;
            this.DbColumn16.ComboBlankSelection = true;
            this.DbColumn16.ComboImageList = null;
            this.DbColumn16.ComboListField = "";
            this.DbColumn16.DBGridViewFilters = null;
            this.DbColumn16.Decimals = 2;
            this.DbColumn16.DefaultValue = "";
            this.DbColumn16.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn16.Encrypted = false;
            this.DbColumn16.Expression = "";
            this.DbColumn16.FieldDB = "evaluacion";
            this.DbColumn16.Font = null;
            this.DbColumn16.Format = null;
            this.DbColumn16.FormatString = null;
            this.DbColumn16.HeaderCaption = "Evaluación";
            this.DbColumn16.Hidden = false;
            this.DbColumn16.LastValue = false;
            this.DbColumn16.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn16.MaskInput = null;
            this.DbColumn16.MaxLength = 0;
            this.DbColumn16.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn16.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn16.MultiLine = false;
            this.DbColumn16.NullValue = null;
            this.DbColumn16.Obligatory = false;
            this.DbColumn16.PromptChar = '\0';
            this.DbColumn16.ReadColumn = false;
            this.DbColumn16.ShowSelectForm = true;
            this.DbColumn16.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn16.ToolTip = "";
            this.DbColumn16.Unique = false;
            this.DbColumn16.Width = 0;
            // 
            // DbColumn18
            // 
            this.DbColumn18.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn18.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn18.AllowNull = false;
            this.DbColumn18.AllowRowFiltering = false;
            this.DbColumn18.AsociatedButtonColumn = -1;
            this.DbColumn18.AsociatedComboColumn = -1;
            this.DbColumn18.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn18.ColumnDBControl = null;
            this.DbColumn18.ColumnDBFieldData = "id";
            this.DbColumn18.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn18.ColumnType = FSFormControls.DBColumn.ColumnTypes.ComboColumn;
            this.DbColumn18.ComboBlankSelection = true;
            this.DbColumn18.ComboImageList = null;
            this.DbColumn18.ComboListField = "descripcion";
            this.DbColumn18.DBGridViewFilters = null;
            this.DbColumn18.Decimals = 2;
            this.DbColumn18.DefaultValue = "";
            this.DbColumn18.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn18.Encrypted = false;
            this.DbColumn18.Expression = "";
            this.DbColumn18.FieldDB = "tipoEvaluacion";
            this.DbColumn18.Font = null;
            this.DbColumn18.Format = null;
            this.DbColumn18.FormatString = null;
            this.DbColumn18.HeaderCaption = "Tipo Evaluación";
            this.DbColumn18.Hidden = false;
            this.DbColumn18.LastValue = false;
            this.DbColumn18.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn18.MaskInput = null;
            this.DbColumn18.MaxLength = 0;
            this.DbColumn18.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn18.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn18.MultiLine = false;
            this.DbColumn18.NullValue = null;
            this.DbColumn18.Obligatory = false;
            this.DbColumn18.PromptChar = '\0';
            this.DbColumn18.ReadColumn = false;
            this.DbColumn18.ShowSelectForm = true;
            this.DbColumn18.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn18.ToolTip = "";
            this.DbColumn18.Unique = false;
            this.DbColumn18.Width = 0;
            // 
            // DbColumn42
            // 
            this.DbColumn42.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn42.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn42.AllowNull = false;
            this.DbColumn42.AllowRowFiltering = false;
            this.DbColumn42.AsociatedButtonColumn = -1;
            this.DbColumn42.AsociatedComboColumn = -1;
            this.DbColumn42.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn42.ColumnDBControl = null;
            this.DbColumn42.ColumnDBFieldData = "";
            this.DbColumn42.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn42.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn42.ComboBlankSelection = true;
            this.DbColumn42.ComboImageList = null;
            this.DbColumn42.ComboListField = "";
            this.DbColumn42.DBGridViewFilters = null;
            this.DbColumn42.Decimals = 2;
            this.DbColumn42.DefaultValue = "";
            this.DbColumn42.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn42.Encrypted = false;
            this.DbColumn42.Expression = "";
            this.DbColumn42.FieldDB = "protocolosAplicados";
            this.DbColumn42.Font = null;
            this.DbColumn42.Format = null;
            this.DbColumn42.FormatString = null;
            this.DbColumn42.HeaderCaption = "Protocolos Aplicados";
            this.DbColumn42.Hidden = false;
            this.DbColumn42.LastValue = false;
            this.DbColumn42.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn42.MaskInput = null;
            this.DbColumn42.MaxLength = 0;
            this.DbColumn42.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn42.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn42.MultiLine = false;
            this.DbColumn42.NullValue = null;
            this.DbColumn42.Obligatory = false;
            this.DbColumn42.PromptChar = '\0';
            this.DbColumn42.ReadColumn = false;
            this.DbColumn42.ShowSelectForm = true;
            this.DbColumn42.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn42.ToolTip = "";
            this.DbColumn42.Unique = false;
            this.DbColumn42.Width = 0;
            // 
            // DbColumn44
            // 
            this.DbColumn44.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn44.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn44.AllowNull = false;
            this.DbColumn44.AllowRowFiltering = false;
            this.DbColumn44.AsociatedButtonColumn = -1;
            this.DbColumn44.AsociatedComboColumn = -1;
            this.DbColumn44.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn44.ColumnDBControl = null;
            this.DbColumn44.ColumnDBFieldData = "";
            this.DbColumn44.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn44.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn44.ComboBlankSelection = true;
            this.DbColumn44.ComboImageList = null;
            this.DbColumn44.ComboListField = "";
            this.DbColumn44.DBGridViewFilters = null;
            this.DbColumn44.Decimals = 2;
            this.DbColumn44.DefaultValue = "";
            this.DbColumn44.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn44.Encrypted = false;
            this.DbColumn44.Expression = "";
            this.DbColumn44.FieldDB = "lugarRealizacion";
            this.DbColumn44.Font = null;
            this.DbColumn44.Format = null;
            this.DbColumn44.FormatString = null;
            this.DbColumn44.HeaderCaption = "Lugar Realización";
            this.DbColumn44.Hidden = false;
            this.DbColumn44.LastValue = false;
            this.DbColumn44.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn44.MaskInput = null;
            this.DbColumn44.MaxLength = 0;
            this.DbColumn44.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn44.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn44.MultiLine = false;
            this.DbColumn44.NullValue = null;
            this.DbColumn44.Obligatory = false;
            this.DbColumn44.PromptChar = '\0';
            this.DbColumn44.ReadColumn = false;
            this.DbColumn44.ShowSelectForm = true;
            this.DbColumn44.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn44.ToolTip = "";
            this.DbColumn44.Unique = false;
            this.DbColumn44.Width = 0;
            // 
            // DbColumn11
            // 
            this.DbColumn11.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn11.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn11.AllowNull = false;
            this.DbColumn11.AllowRowFiltering = false;
            this.DbColumn11.AsociatedButtonColumn = -1;
            this.DbColumn11.AsociatedComboColumn = -1;
            this.DbColumn11.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn11.ColumnDBControl = null;
            this.DbColumn11.ColumnDBFieldData = "";
            this.DbColumn11.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn11.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn11.ComboBlankSelection = true;
            this.DbColumn11.ComboImageList = null;
            this.DbColumn11.ComboListField = "";
            this.DbColumn11.DBGridViewFilters = null;
            this.DbColumn11.Decimals = 2;
            this.DbColumn11.DefaultValue = "";
            this.DbColumn11.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn11.Encrypted = false;
            this.DbColumn11.Expression = "";
            this.DbColumn11.FieldDB = "personaContactoFac";
            this.DbColumn11.Font = null;
            this.DbColumn11.Format = null;
            this.DbColumn11.FormatString = null;
            this.DbColumn11.HeaderCaption = "Persona de Contacto";
            this.DbColumn11.Hidden = false;
            this.DbColumn11.LastValue = false;
            this.DbColumn11.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn11.MaskInput = null;
            this.DbColumn11.MaxLength = 0;
            this.DbColumn11.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn11.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn11.MultiLine = false;
            this.DbColumn11.NullValue = null;
            this.DbColumn11.Obligatory = false;
            this.DbColumn11.PromptChar = '\0';
            this.DbColumn11.ReadColumn = false;
            this.DbColumn11.ShowSelectForm = true;
            this.DbColumn11.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn11.ToolTip = "";
            this.DbColumn11.Unique = false;
            this.DbColumn11.Width = 0;
            // 
            // DbColumn12
            // 
            this.DbColumn12.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn12.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn12.AllowNull = false;
            this.DbColumn12.AllowRowFiltering = false;
            this.DbColumn12.AsociatedButtonColumn = -1;
            this.DbColumn12.AsociatedComboColumn = -1;
            this.DbColumn12.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn12.ColumnDBControl = null;
            this.DbColumn12.ColumnDBFieldData = "";
            this.DbColumn12.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn12.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn12.ComboBlankSelection = true;
            this.DbColumn12.ComboImageList = null;
            this.DbColumn12.ComboListField = "";
            this.DbColumn12.DBGridViewFilters = null;
            this.DbColumn12.Decimals = 2;
            this.DbColumn12.DefaultValue = "";
            this.DbColumn12.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn12.Encrypted = false;
            this.DbColumn12.Expression = "";
            this.DbColumn12.FieldDB = "telefono1Fac";
            this.DbColumn12.Font = null;
            this.DbColumn12.Format = null;
            this.DbColumn12.FormatString = null;
            this.DbColumn12.HeaderCaption = "Teléfono";
            this.DbColumn12.Hidden = false;
            this.DbColumn12.LastValue = false;
            this.DbColumn12.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn12.MaskInput = null;
            this.DbColumn12.MaxLength = 0;
            this.DbColumn12.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn12.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn12.MultiLine = false;
            this.DbColumn12.NullValue = null;
            this.DbColumn12.Obligatory = false;
            this.DbColumn12.PromptChar = '\0';
            this.DbColumn12.ReadColumn = false;
            this.DbColumn12.ShowSelectForm = true;
            this.DbColumn12.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn12.ToolTip = "";
            this.DbColumn12.Unique = false;
            this.DbColumn12.Width = 0;
            // 
            // DbColumn2
            // 
            this.DbColumn2.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn2.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn2.AllowNull = false;
            this.DbColumn2.AllowRowFiltering = false;
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
            this.DbColumn2.DBGridViewFilters = null;
            this.DbColumn2.Decimals = 2;
            this.DbColumn2.DefaultValue = "";
            this.DbColumn2.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn2.Encrypted = false;
            this.DbColumn2.Expression = "";
            this.DbColumn2.FieldDB = "formaDePago";
            this.DbColumn2.Font = null;
            this.DbColumn2.Format = null;
            this.DbColumn2.FormatString = null;
            this.DbColumn2.HeaderCaption = "Forma de Pago";
            this.DbColumn2.Hidden = false;
            this.DbColumn2.LastValue = false;
            this.DbColumn2.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn2.MaskInput = null;
            this.DbColumn2.MaxLength = 0;
            this.DbColumn2.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn2.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn2.MultiLine = false;
            this.DbColumn2.NullValue = null;
            this.DbColumn2.Obligatory = false;
            this.DbColumn2.PromptChar = '\0';
            this.DbColumn2.ReadColumn = false;
            this.DbColumn2.ShowSelectForm = true;
            this.DbColumn2.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn2.ToolTip = "";
            this.DbColumn2.Unique = false;
            this.DbColumn2.Width = 0;
            // 
            // DbColumn3
            // 
            this.DbColumn3.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn3.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn3.AllowNull = false;
            this.DbColumn3.AllowRowFiltering = false;
            this.DbColumn3.AsociatedButtonColumn = -1;
            this.DbColumn3.AsociatedComboColumn = -1;
            this.DbColumn3.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn3.ColumnDBControl = null;
            this.DbColumn3.ColumnDBFieldData = "";
            this.DbColumn3.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn3.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn3.ComboBlankSelection = true;
            this.DbColumn3.ComboImageList = null;
            this.DbColumn3.ComboListField = "";
            this.DbColumn3.DBGridViewFilters = null;
            this.DbColumn3.Decimals = 2;
            this.DbColumn3.DefaultValue = "";
            this.DbColumn3.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn3.Encrypted = false;
            this.DbColumn3.Expression = "";
            this.DbColumn3.FieldDB = "numeroCuenta";
            this.DbColumn3.Font = null;
            this.DbColumn3.Format = null;
            this.DbColumn3.FormatString = null;
            this.DbColumn3.HeaderCaption = "Número de Cuenta";
            this.DbColumn3.Hidden = false;
            this.DbColumn3.LastValue = false;
            this.DbColumn3.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn3.MaskInput = null;
            this.DbColumn3.MaxLength = 0;
            this.DbColumn3.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn3.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn3.MultiLine = false;
            this.DbColumn3.NullValue = null;
            this.DbColumn3.Obligatory = false;
            this.DbColumn3.PromptChar = '\0';
            this.DbColumn3.ReadColumn = false;
            this.DbColumn3.ShowSelectForm = true;
            this.DbColumn3.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn3.ToolTip = "";
            this.DbColumn3.Unique = false;
            this.DbColumn3.Width = 0;
            // 
            // DbColumn7
            // 
            this.DbColumn7.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn7.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn7.AllowNull = false;
            this.DbColumn7.AllowRowFiltering = false;
            this.DbColumn7.AsociatedButtonColumn = -1;
            this.DbColumn7.AsociatedComboColumn = -1;
            this.DbColumn7.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn7.ColumnDBControl = null;
            this.DbColumn7.ColumnDBFieldData = "";
            this.DbColumn7.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn7.ColumnType = FSFormControls.DBColumn.ColumnTypes.CheckColumn;
            this.DbColumn7.ComboBlankSelection = true;
            this.DbColumn7.ComboImageList = null;
            this.DbColumn7.ComboListField = "";
            this.DbColumn7.DBGridViewFilters = null;
            this.DbColumn7.Decimals = 2;
            this.DbColumn7.DefaultValue = "";
            this.DbColumn7.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn7.Encrypted = false;
            this.DbColumn7.Expression = "";
            this.DbColumn7.FieldDB = "abonado";
            this.DbColumn7.Font = null;
            this.DbColumn7.Format = null;
            this.DbColumn7.FormatString = null;
            this.DbColumn7.HeaderCaption = "Abonado";
            this.DbColumn7.Hidden = false;
            this.DbColumn7.LastValue = false;
            this.DbColumn7.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn7.MaskInput = null;
            this.DbColumn7.MaxLength = 0;
            this.DbColumn7.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn7.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn7.MultiLine = false;
            this.DbColumn7.NullValue = null;
            this.DbColumn7.Obligatory = false;
            this.DbColumn7.PromptChar = '\0';
            this.DbColumn7.ReadColumn = false;
            this.DbColumn7.ShowSelectForm = true;
            this.DbColumn7.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn7.ToolTip = "";
            this.DbColumn7.Unique = false;
            this.DbColumn7.Width = 0;
            // 
            // DbColumn8
            // 
            this.DbColumn8.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn8.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn8.AllowNull = false;
            this.DbColumn8.AllowRowFiltering = false;
            this.DbColumn8.AsociatedButtonColumn = -1;
            this.DbColumn8.AsociatedComboColumn = -1;
            this.DbColumn8.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn8.ColumnDBControl = null;
            this.DbColumn8.ColumnDBFieldData = "";
            this.DbColumn8.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn8.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn8.ComboBlankSelection = true;
            this.DbColumn8.ComboImageList = null;
            this.DbColumn8.ComboListField = "";
            this.DbColumn8.DBGridViewFilters = null;
            this.DbColumn8.Decimals = 2;
            this.DbColumn8.DefaultValue = "";
            this.DbColumn8.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn8.Encrypted = false;
            this.DbColumn8.Expression = "";
            this.DbColumn8.FieldDB = "observaciones";
            this.DbColumn8.Font = null;
            this.DbColumn8.Format = null;
            this.DbColumn8.FormatString = null;
            this.DbColumn8.HeaderCaption = "Observaciones";
            this.DbColumn8.Hidden = false;
            this.DbColumn8.LastValue = false;
            this.DbColumn8.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn8.MaskInput = null;
            this.DbColumn8.MaxLength = 0;
            this.DbColumn8.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn8.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn8.MultiLine = false;
            this.DbColumn8.NullValue = null;
            this.DbColumn8.Obligatory = false;
            this.DbColumn8.PromptChar = '\0';
            this.DbColumn8.ReadColumn = false;
            this.DbColumn8.ShowSelectForm = true;
            this.DbColumn8.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn8.ToolTip = "";
            this.DbColumn8.Unique = false;
            this.DbColumn8.Width = 0;
            // 
            // DbColumn43
            // 
            this.DbColumn43.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn43.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn43.AllowNull = false;
            this.DbColumn43.AllowRowFiltering = false;
            this.DbColumn43.AsociatedButtonColumn = -1;
            this.DbColumn43.AsociatedComboColumn = -1;
            this.DbColumn43.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn43.ColumnDBControl = null;
            this.DbColumn43.ColumnDBFieldData = "";
            this.DbColumn43.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn43.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn43.ComboBlankSelection = true;
            this.DbColumn43.ComboImageList = null;
            this.DbColumn43.ComboListField = "";
            this.DbColumn43.DBGridViewFilters = null;
            this.DbColumn43.Decimals = 2;
            this.DbColumn43.DefaultValue = "";
            this.DbColumn43.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn43.Encrypted = false;
            this.DbColumn43.Expression = "";
            this.DbColumn43.FieldDB = "estadoActual";
            this.DbColumn43.Font = null;
            this.DbColumn43.Format = null;
            this.DbColumn43.FormatString = null;
            this.DbColumn43.HeaderCaption = "Estado Actual";
            this.DbColumn43.Hidden = false;
            this.DbColumn43.LastValue = false;
            this.DbColumn43.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn43.MaskInput = null;
            this.DbColumn43.MaxLength = 0;
            this.DbColumn43.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn43.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn43.MultiLine = false;
            this.DbColumn43.NullValue = null;
            this.DbColumn43.Obligatory = false;
            this.DbColumn43.PromptChar = '\0';
            this.DbColumn43.ReadColumn = false;
            this.DbColumn43.ShowSelectForm = true;
            this.DbColumn43.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn43.ToolTip = "";
            this.DbColumn43.Unique = false;
            this.DbColumn43.Width = 0;
            // 
            // DbColumn37
            // 
            this.DbColumn37.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn37.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn37.AllowNull = false;
            this.DbColumn37.AllowRowFiltering = false;
            this.DbColumn37.AsociatedButtonColumn = -1;
            this.DbColumn37.AsociatedComboColumn = -1;
            this.DbColumn37.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn37.ColumnDBControl = null;
            this.DbColumn37.ColumnDBFieldData = "";
            this.DbColumn37.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn37.ColumnType = FSFormControls.DBColumn.ColumnTypes.DateColumn;
            this.DbColumn37.ComboBlankSelection = true;
            this.DbColumn37.ComboImageList = null;
            this.DbColumn37.ComboListField = "";
            this.DbColumn37.DBGridViewFilters = null;
            this.DbColumn37.Decimals = 2;
            this.DbColumn37.DefaultValue = "";
            this.DbColumn37.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn37.Encrypted = false;
            this.DbColumn37.Expression = "";
            this.DbColumn37.FieldDB = "fechaVisita";
            this.DbColumn37.Font = null;
            this.DbColumn37.Format = null;
            this.DbColumn37.FormatString = null;
            this.DbColumn37.HeaderCaption = "Fecha Visita";
            this.DbColumn37.Hidden = false;
            this.DbColumn37.LastValue = false;
            this.DbColumn37.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn37.MaskInput = null;
            this.DbColumn37.MaxLength = 0;
            this.DbColumn37.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn37.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn37.MultiLine = false;
            this.DbColumn37.NullValue = null;
            this.DbColumn37.Obligatory = false;
            this.DbColumn37.PromptChar = '\0';
            this.DbColumn37.ReadColumn = false;
            this.DbColumn37.ShowSelectForm = true;
            this.DbColumn37.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn37.ToolTip = "";
            this.DbColumn37.Unique = false;
            this.DbColumn37.Width = 0;
            // 
            // DbColumn38
            // 
            this.DbColumn38.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn38.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn38.AllowNull = false;
            this.DbColumn38.AllowRowFiltering = false;
            this.DbColumn38.AsociatedButtonColumn = -1;
            this.DbColumn38.AsociatedComboColumn = -1;
            this.DbColumn38.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn38.ColumnDBControl = null;
            this.DbColumn38.ColumnDBFieldData = "";
            this.DbColumn38.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn38.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn38.ComboBlankSelection = true;
            this.DbColumn38.ComboImageList = null;
            this.DbColumn38.ComboListField = "";
            this.DbColumn38.DBGridViewFilters = null;
            this.DbColumn38.Decimals = 2;
            this.DbColumn38.DefaultValue = "";
            this.DbColumn38.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn38.Encrypted = false;
            this.DbColumn38.Expression = "";
            this.DbColumn38.FieldDB = "descripcion";
            this.DbColumn38.Font = null;
            this.DbColumn38.Format = null;
            this.DbColumn38.FormatString = null;
            this.DbColumn38.HeaderCaption = "Descripción";
            this.DbColumn38.Hidden = false;
            this.DbColumn38.LastValue = false;
            this.DbColumn38.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn38.MaskInput = null;
            this.DbColumn38.MaxLength = 0;
            this.DbColumn38.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn38.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn38.MultiLine = false;
            this.DbColumn38.NullValue = null;
            this.DbColumn38.Obligatory = false;
            this.DbColumn38.PromptChar = '\0';
            this.DbColumn38.ReadColumn = false;
            this.DbColumn38.ShowSelectForm = true;
            this.DbColumn38.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn38.ToolTip = "";
            this.DbColumn38.Unique = false;
            this.DbColumn38.Width = 0;
            // 
            // DbColumn39
            // 
            this.DbColumn39.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn39.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn39.AllowNull = false;
            this.DbColumn39.AllowRowFiltering = false;
            this.DbColumn39.AsociatedButtonColumn = -1;
            this.DbColumn39.AsociatedComboColumn = -1;
            this.DbColumn39.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn39.ColumnDBControl = null;
            this.DbColumn39.ColumnDBFieldData = "";
            this.DbColumn39.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn39.ColumnType = FSFormControls.DBColumn.ColumnTypes.DateColumn;
            this.DbColumn39.ComboBlankSelection = true;
            this.DbColumn39.ComboImageList = null;
            this.DbColumn39.ComboListField = "";
            this.DbColumn39.DBGridViewFilters = null;
            this.DbColumn39.Decimals = 2;
            this.DbColumn39.DefaultValue = "";
            this.DbColumn39.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn39.Encrypted = false;
            this.DbColumn39.Expression = "";
            this.DbColumn39.FieldDB = "fechaUltimaVisita";
            this.DbColumn39.Font = null;
            this.DbColumn39.Format = null;
            this.DbColumn39.FormatString = null;
            this.DbColumn39.HeaderCaption = "Fecha Últ. Visita";
            this.DbColumn39.Hidden = false;
            this.DbColumn39.LastValue = false;
            this.DbColumn39.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn39.MaskInput = null;
            this.DbColumn39.MaxLength = 0;
            this.DbColumn39.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn39.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn39.MultiLine = false;
            this.DbColumn39.NullValue = null;
            this.DbColumn39.Obligatory = false;
            this.DbColumn39.PromptChar = '\0';
            this.DbColumn39.ReadColumn = false;
            this.DbColumn39.ShowSelectForm = true;
            this.DbColumn39.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn39.ToolTip = "";
            this.DbColumn39.Unique = false;
            this.DbColumn39.Width = 0;
            // 
            // DbColumn40
            // 
            this.DbColumn40.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn40.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn40.AllowNull = false;
            this.DbColumn40.AllowRowFiltering = false;
            this.DbColumn40.AsociatedButtonColumn = -1;
            this.DbColumn40.AsociatedComboColumn = -1;
            this.DbColumn40.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn40.ColumnDBControl = null;
            this.DbColumn40.ColumnDBFieldData = "";
            this.DbColumn40.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn40.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn40.ComboBlankSelection = true;
            this.DbColumn40.ComboImageList = null;
            this.DbColumn40.ComboListField = "";
            this.DbColumn40.DBGridViewFilters = null;
            this.DbColumn40.Decimals = 2;
            this.DbColumn40.DefaultValue = "";
            this.DbColumn40.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn40.Encrypted = false;
            this.DbColumn40.Expression = "";
            this.DbColumn40.FieldDB = "numeroVisitasAñosAnteriores";
            this.DbColumn40.Font = null;
            this.DbColumn40.Format = null;
            this.DbColumn40.FormatString = null;
            this.DbColumn40.HeaderCaption = "Nº Visitas años Anteriores";
            this.DbColumn40.Hidden = false;
            this.DbColumn40.LastValue = false;
            this.DbColumn40.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn40.MaskInput = null;
            this.DbColumn40.MaxLength = 0;
            this.DbColumn40.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn40.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn40.MultiLine = false;
            this.DbColumn40.NullValue = null;
            this.DbColumn40.Obligatory = false;
            this.DbColumn40.PromptChar = '\0';
            this.DbColumn40.ReadColumn = false;
            this.DbColumn40.ShowSelectForm = true;
            this.DbColumn40.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn40.ToolTip = "";
            this.DbColumn40.Unique = false;
            this.DbColumn40.Width = 0;
            // 
            // DbColumn41
            // 
            this.DbColumn41.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn41.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn41.AllowNull = false;
            this.DbColumn41.AllowRowFiltering = false;
            this.DbColumn41.AsociatedButtonColumn = -1;
            this.DbColumn41.AsociatedComboColumn = -1;
            this.DbColumn41.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn41.ColumnDBControl = null;
            this.DbColumn41.ColumnDBFieldData = "";
            this.DbColumn41.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn41.ColumnType = FSFormControls.DBColumn.ColumnTypes.NumberColumn;
            this.DbColumn41.ComboBlankSelection = true;
            this.DbColumn41.ComboImageList = null;
            this.DbColumn41.ComboListField = "";
            this.DbColumn41.DBGridViewFilters = null;
            this.DbColumn41.Decimals = 0;
            this.DbColumn41.DefaultValue = "";
            this.DbColumn41.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn41.Encrypted = false;
            this.DbColumn41.Expression = "";
            this.DbColumn41.FieldDB = "idConstruccion";
            this.DbColumn41.Font = null;
            this.DbColumn41.Format = null;
            this.DbColumn41.FormatString = null;
            this.DbColumn41.HeaderCaption = "Id";
            this.DbColumn41.Hidden = false;
            this.DbColumn41.LastValue = false;
            this.DbColumn41.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn41.MaskInput = null;
            this.DbColumn41.MaxLength = 0;
            this.DbColumn41.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn41.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn41.MultiLine = false;
            this.DbColumn41.NullValue = null;
            this.DbColumn41.Obligatory = false;
            this.DbColumn41.PromptChar = '\0';
            this.DbColumn41.ReadColumn = false;
            this.DbColumn41.ShowSelectForm = true;
            this.DbColumn41.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn41.ToolTip = "";
            this.DbColumn41.Unique = false;
            this.DbColumn41.Width = 0;
            // 
            // DbColumn25
            // 
            this.DbColumn25.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn25.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn25.AllowNull = false;
            this.DbColumn25.AllowRowFiltering = false;
            this.DbColumn25.AsociatedButtonColumn = -1;
            this.DbColumn25.AsociatedComboColumn = -1;
            this.DbColumn25.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn25.ColumnDBControl = null;
            this.DbColumn25.ColumnDBFieldData = "";
            this.DbColumn25.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn25.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn25.ComboBlankSelection = true;
            this.DbColumn25.ComboImageList = null;
            this.DbColumn25.ComboListField = "";
            this.DbColumn25.DBGridViewFilters = null;
            this.DbColumn25.Decimals = 2;
            this.DbColumn25.DefaultValue = "";
            this.DbColumn25.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn25.Encrypted = false;
            this.DbColumn25.Expression = "";
            this.DbColumn25.FieldDB = "Obra";
            this.DbColumn25.Font = null;
            this.DbColumn25.Format = null;
            this.DbColumn25.FormatString = null;
            this.DbColumn25.HeaderCaption = "Descripción";
            this.DbColumn25.Hidden = false;
            this.DbColumn25.LastValue = false;
            this.DbColumn25.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn25.MaskInput = null;
            this.DbColumn25.MaxLength = 0;
            this.DbColumn25.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn25.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn25.MultiLine = false;
            this.DbColumn25.NullValue = null;
            this.DbColumn25.Obligatory = false;
            this.DbColumn25.PromptChar = '\0';
            this.DbColumn25.ReadColumn = false;
            this.DbColumn25.ShowSelectForm = true;
            this.DbColumn25.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn25.ToolTip = "";
            this.DbColumn25.Unique = false;
            this.DbColumn25.Width = 0;
            // 
            // DbColumn26
            // 
            this.DbColumn26.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn26.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn26.AllowNull = false;
            this.DbColumn26.AllowRowFiltering = false;
            this.DbColumn26.AsociatedButtonColumn = -1;
            this.DbColumn26.AsociatedComboColumn = -1;
            this.DbColumn26.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn26.ColumnDBControl = null;
            this.DbColumn26.ColumnDBFieldData = "";
            this.DbColumn26.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn26.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn26.ComboBlankSelection = true;
            this.DbColumn26.ComboImageList = null;
            this.DbColumn26.ComboListField = "";
            this.DbColumn26.DBGridViewFilters = null;
            this.DbColumn26.Decimals = 2;
            this.DbColumn26.DefaultValue = "";
            this.DbColumn26.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn26.Encrypted = false;
            this.DbColumn26.Expression = "";
            this.DbColumn26.FieldDB = "direccion";
            this.DbColumn26.Font = null;
            this.DbColumn26.Format = null;
            this.DbColumn26.FormatString = null;
            this.DbColumn26.HeaderCaption = "Dirección";
            this.DbColumn26.Hidden = false;
            this.DbColumn26.LastValue = false;
            this.DbColumn26.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn26.MaskInput = null;
            this.DbColumn26.MaxLength = 0;
            this.DbColumn26.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn26.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn26.MultiLine = false;
            this.DbColumn26.NullValue = null;
            this.DbColumn26.Obligatory = false;
            this.DbColumn26.PromptChar = '\0';
            this.DbColumn26.ReadColumn = false;
            this.DbColumn26.ShowSelectForm = true;
            this.DbColumn26.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn26.ToolTip = "";
            this.DbColumn26.Unique = false;
            this.DbColumn26.Width = 0;
            // 
            // DbColumn27
            // 
            this.DbColumn27.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn27.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn27.AllowNull = false;
            this.DbColumn27.AllowRowFiltering = false;
            this.DbColumn27.AsociatedButtonColumn = -1;
            this.DbColumn27.AsociatedComboColumn = -1;
            this.DbColumn27.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn27.ColumnDBControl = null;
            this.DbColumn27.ColumnDBFieldData = "";
            this.DbColumn27.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn27.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn27.ComboBlankSelection = true;
            this.DbColumn27.ComboImageList = null;
            this.DbColumn27.ComboListField = "";
            this.DbColumn27.DBGridViewFilters = null;
            this.DbColumn27.Decimals = 2;
            this.DbColumn27.DefaultValue = "";
            this.DbColumn27.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn27.Encrypted = false;
            this.DbColumn27.Expression = "";
            this.DbColumn27.FieldDB = "telefono1";
            this.DbColumn27.Font = null;
            this.DbColumn27.Format = null;
            this.DbColumn27.FormatString = null;
            this.DbColumn27.HeaderCaption = "Telefono 1";
            this.DbColumn27.Hidden = false;
            this.DbColumn27.LastValue = false;
            this.DbColumn27.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn27.MaskInput = null;
            this.DbColumn27.MaxLength = 0;
            this.DbColumn27.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn27.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn27.MultiLine = false;
            this.DbColumn27.NullValue = null;
            this.DbColumn27.Obligatory = false;
            this.DbColumn27.PromptChar = '\0';
            this.DbColumn27.ReadColumn = false;
            this.DbColumn27.ShowSelectForm = true;
            this.DbColumn27.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn27.ToolTip = "";
            this.DbColumn27.Unique = false;
            this.DbColumn27.Width = 0;
            // 
            // DbColumn28
            // 
            this.DbColumn28.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn28.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn28.AllowNull = false;
            this.DbColumn28.AllowRowFiltering = false;
            this.DbColumn28.AsociatedButtonColumn = -1;
            this.DbColumn28.AsociatedComboColumn = -1;
            this.DbColumn28.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn28.ColumnDBControl = null;
            this.DbColumn28.ColumnDBFieldData = "";
            this.DbColumn28.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn28.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn28.ComboBlankSelection = true;
            this.DbColumn28.ComboImageList = null;
            this.DbColumn28.ComboListField = "";
            this.DbColumn28.DBGridViewFilters = null;
            this.DbColumn28.Decimals = 2;
            this.DbColumn28.DefaultValue = "";
            this.DbColumn28.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn28.Encrypted = false;
            this.DbColumn28.Expression = "";
            this.DbColumn28.FieldDB = "telefono2";
            this.DbColumn28.Font = null;
            this.DbColumn28.Format = null;
            this.DbColumn28.FormatString = null;
            this.DbColumn28.HeaderCaption = "Telefono 2";
            this.DbColumn28.Hidden = false;
            this.DbColumn28.LastValue = false;
            this.DbColumn28.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn28.MaskInput = null;
            this.DbColumn28.MaxLength = 0;
            this.DbColumn28.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn28.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn28.MultiLine = false;
            this.DbColumn28.NullValue = null;
            this.DbColumn28.Obligatory = false;
            this.DbColumn28.PromptChar = '\0';
            this.DbColumn28.ReadColumn = false;
            this.DbColumn28.ShowSelectForm = true;
            this.DbColumn28.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn28.ToolTip = "";
            this.DbColumn28.Unique = false;
            this.DbColumn28.Width = 0;
            // 
            // DbColumn29
            // 
            this.DbColumn29.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn29.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn29.AllowNull = false;
            this.DbColumn29.AllowRowFiltering = false;
            this.DbColumn29.AsociatedButtonColumn = -1;
            this.DbColumn29.AsociatedComboColumn = -1;
            this.DbColumn29.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn29.ColumnDBControl = null;
            this.DbColumn29.ColumnDBFieldData = "";
            this.DbColumn29.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn29.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn29.ComboBlankSelection = true;
            this.DbColumn29.ComboImageList = null;
            this.DbColumn29.ComboListField = "";
            this.DbColumn29.DBGridViewFilters = null;
            this.DbColumn29.Decimals = 2;
            this.DbColumn29.DefaultValue = "";
            this.DbColumn29.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn29.Encrypted = false;
            this.DbColumn29.Expression = "";
            this.DbColumn29.FieldDB = "personaContacto";
            this.DbColumn29.Font = null;
            this.DbColumn29.Format = null;
            this.DbColumn29.FormatString = null;
            this.DbColumn29.HeaderCaption = "Contacto";
            this.DbColumn29.Hidden = false;
            this.DbColumn29.LastValue = false;
            this.DbColumn29.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn29.MaskInput = null;
            this.DbColumn29.MaxLength = 0;
            this.DbColumn29.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn29.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn29.MultiLine = false;
            this.DbColumn29.NullValue = null;
            this.DbColumn29.Obligatory = false;
            this.DbColumn29.PromptChar = '\0';
            this.DbColumn29.ReadColumn = false;
            this.DbColumn29.ShowSelectForm = true;
            this.DbColumn29.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn29.ToolTip = "";
            this.DbColumn29.Unique = false;
            this.DbColumn29.Width = 0;
            // 
            // DbColumn30
            // 
            this.DbColumn30.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn30.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn30.AllowNull = false;
            this.DbColumn30.AllowRowFiltering = false;
            this.DbColumn30.AsociatedButtonColumn = -1;
            this.DbColumn30.AsociatedComboColumn = -1;
            this.DbColumn30.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn30.ColumnDBControl = null;
            this.DbColumn30.ColumnDBFieldData = "";
            this.DbColumn30.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn30.ColumnType = FSFormControls.DBColumn.ColumnTypes.CheckColumn;
            this.DbColumn30.ComboBlankSelection = true;
            this.DbColumn30.ComboImageList = null;
            this.DbColumn30.ComboListField = "";
            this.DbColumn30.DBGridViewFilters = null;
            this.DbColumn30.Decimals = 2;
            this.DbColumn30.DefaultValue = "";
            this.DbColumn30.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn30.Encrypted = false;
            this.DbColumn30.Expression = "";
            this.DbColumn30.FieldDB = "contratadoPS";
            this.DbColumn30.Font = null;
            this.DbColumn30.Format = null;
            this.DbColumn30.FormatString = null;
            this.DbColumn30.HeaderCaption = "PS";
            this.DbColumn30.Hidden = false;
            this.DbColumn30.LastValue = false;
            this.DbColumn30.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn30.MaskInput = null;
            this.DbColumn30.MaxLength = 0;
            this.DbColumn30.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn30.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn30.MultiLine = false;
            this.DbColumn30.NullValue = null;
            this.DbColumn30.Obligatory = false;
            this.DbColumn30.PromptChar = '\0';
            this.DbColumn30.ReadColumn = false;
            this.DbColumn30.ShowSelectForm = true;
            this.DbColumn30.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn30.ToolTip = "";
            this.DbColumn30.Unique = false;
            this.DbColumn30.Width = 0;
            // 
            // DbColumn31
            // 
            this.DbColumn31.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn31.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn31.AllowNull = false;
            this.DbColumn31.AllowRowFiltering = false;
            this.DbColumn31.AsociatedButtonColumn = -1;
            this.DbColumn31.AsociatedComboColumn = -1;
            this.DbColumn31.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn31.ColumnDBControl = null;
            this.DbColumn31.ColumnDBFieldData = "";
            this.DbColumn31.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn31.ColumnType = FSFormControls.DBColumn.ColumnTypes.NumberColumn;
            this.DbColumn31.ComboBlankSelection = true;
            this.DbColumn31.ComboImageList = null;
            this.DbColumn31.ComboListField = "";
            this.DbColumn31.DBGridViewFilters = null;
            this.DbColumn31.Decimals = 0;
            this.DbColumn31.DefaultValue = "";
            this.DbColumn31.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn31.Encrypted = false;
            this.DbColumn31.Expression = "";
            this.DbColumn31.FieldDB = "numeroInspeccion";
            this.DbColumn31.Font = null;
            this.DbColumn31.Format = null;
            this.DbColumn31.FormatString = null;
            this.DbColumn31.HeaderCaption = "Nº Inspección";
            this.DbColumn31.Hidden = false;
            this.DbColumn31.LastValue = false;
            this.DbColumn31.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn31.MaskInput = null;
            this.DbColumn31.MaxLength = 0;
            this.DbColumn31.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn31.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn31.MultiLine = false;
            this.DbColumn31.NullValue = null;
            this.DbColumn31.Obligatory = false;
            this.DbColumn31.PromptChar = '\0';
            this.DbColumn31.ReadColumn = false;
            this.DbColumn31.ShowSelectForm = true;
            this.DbColumn31.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn31.ToolTip = "";
            this.DbColumn31.Unique = false;
            this.DbColumn31.Width = 0;
            // 
            // DbColumn32
            // 
            this.DbColumn32.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn32.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn32.AllowNull = false;
            this.DbColumn32.AllowRowFiltering = false;
            this.DbColumn32.AsociatedButtonColumn = -1;
            this.DbColumn32.AsociatedComboColumn = -1;
            this.DbColumn32.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn32.ColumnDBControl = null;
            this.DbColumn32.ColumnDBFieldData = "";
            this.DbColumn32.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn32.ColumnType = FSFormControls.DBColumn.ColumnTypes.CheckColumn;
            this.DbColumn32.ComboBlankSelection = true;
            this.DbColumn32.ComboImageList = null;
            this.DbColumn32.ComboListField = "";
            this.DbColumn32.DBGridViewFilters = null;
            this.DbColumn32.Decimals = 2;
            this.DbColumn32.DefaultValue = "";
            this.DbColumn32.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn32.Encrypted = false;
            this.DbColumn32.Expression = "";
            this.DbColumn32.FieldDB = "coordinacion";
            this.DbColumn32.Font = null;
            this.DbColumn32.Format = null;
            this.DbColumn32.FormatString = null;
            this.DbColumn32.HeaderCaption = "Coodinación";
            this.DbColumn32.Hidden = false;
            this.DbColumn32.LastValue = false;
            this.DbColumn32.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn32.MaskInput = null;
            this.DbColumn32.MaxLength = 0;
            this.DbColumn32.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn32.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn32.MultiLine = false;
            this.DbColumn32.NullValue = null;
            this.DbColumn32.Obligatory = false;
            this.DbColumn32.PromptChar = '\0';
            this.DbColumn32.ReadColumn = false;
            this.DbColumn32.ShowSelectForm = true;
            this.DbColumn32.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn32.ToolTip = "";
            this.DbColumn32.Unique = false;
            this.DbColumn32.Width = 0;
            // 
            // DbColumn33
            // 
            this.DbColumn33.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn33.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn33.AllowNull = false;
            this.DbColumn33.AllowRowFiltering = false;
            this.DbColumn33.AsociatedButtonColumn = -1;
            this.DbColumn33.AsociatedComboColumn = -1;
            this.DbColumn33.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn33.ColumnDBControl = null;
            this.DbColumn33.ColumnDBFieldData = "";
            this.DbColumn33.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn33.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn33.ComboBlankSelection = true;
            this.DbColumn33.ComboImageList = null;
            this.DbColumn33.ComboListField = "";
            this.DbColumn33.DBGridViewFilters = null;
            this.DbColumn33.Decimals = 2;
            this.DbColumn33.DefaultValue = "";
            this.DbColumn33.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn33.Encrypted = false;
            this.DbColumn33.Expression = "";
            this.DbColumn33.FieldDB = "estudioSeguridad";
            this.DbColumn33.Font = null;
            this.DbColumn33.Format = null;
            this.DbColumn33.FormatString = null;
            this.DbColumn33.HeaderCaption = "Estudio Seguridad";
            this.DbColumn33.Hidden = false;
            this.DbColumn33.LastValue = false;
            this.DbColumn33.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn33.MaskInput = null;
            this.DbColumn33.MaxLength = 0;
            this.DbColumn33.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn33.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn33.MultiLine = false;
            this.DbColumn33.NullValue = null;
            this.DbColumn33.Obligatory = false;
            this.DbColumn33.PromptChar = '\0';
            this.DbColumn33.ReadColumn = false;
            this.DbColumn33.ShowSelectForm = true;
            this.DbColumn33.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn33.ToolTip = "";
            this.DbColumn33.Unique = false;
            this.DbColumn33.Width = 0;
            // 
            // DbColumn34
            // 
            this.DbColumn34.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn34.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn34.AllowNull = false;
            this.DbColumn34.AllowRowFiltering = false;
            this.DbColumn34.AsociatedButtonColumn = -1;
            this.DbColumn34.AsociatedComboColumn = -1;
            this.DbColumn34.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn34.ColumnDBControl = null;
            this.DbColumn34.ColumnDBFieldData = "";
            this.DbColumn34.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn34.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn34.ComboBlankSelection = true;
            this.DbColumn34.ComboImageList = null;
            this.DbColumn34.ComboListField = "";
            this.DbColumn34.DBGridViewFilters = null;
            this.DbColumn34.Decimals = 2;
            this.DbColumn34.DefaultValue = "";
            this.DbColumn34.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn34.Encrypted = false;
            this.DbColumn34.Expression = "";
            this.DbColumn34.FieldDB = "entregaPS";
            this.DbColumn34.Font = null;
            this.DbColumn34.Format = null;
            this.DbColumn34.FormatString = null;
            this.DbColumn34.HeaderCaption = "Entraga PS";
            this.DbColumn34.Hidden = false;
            this.DbColumn34.LastValue = false;
            this.DbColumn34.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn34.MaskInput = null;
            this.DbColumn34.MaxLength = 0;
            this.DbColumn34.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn34.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn34.MultiLine = false;
            this.DbColumn34.NullValue = null;
            this.DbColumn34.Obligatory = false;
            this.DbColumn34.PromptChar = '\0';
            this.DbColumn34.ReadColumn = false;
            this.DbColumn34.ShowSelectForm = true;
            this.DbColumn34.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn34.ToolTip = "";
            this.DbColumn34.Unique = false;
            this.DbColumn34.Width = 0;
            // 
            // DbColumn35
            // 
            this.DbColumn35.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn35.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn35.AllowNull = false;
            this.DbColumn35.AllowRowFiltering = false;
            this.DbColumn35.AsociatedButtonColumn = -1;
            this.DbColumn35.AsociatedComboColumn = -1;
            this.DbColumn35.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn35.ColumnDBControl = null;
            this.DbColumn35.ColumnDBFieldData = "";
            this.DbColumn35.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn35.ColumnType = FSFormControls.DBColumn.ColumnTypes.DateColumn;
            this.DbColumn35.ComboBlankSelection = true;
            this.DbColumn35.ComboImageList = null;
            this.DbColumn35.ComboListField = "";
            this.DbColumn35.DBGridViewFilters = null;
            this.DbColumn35.Decimals = 2;
            this.DbColumn35.DefaultValue = "";
            this.DbColumn35.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn35.Encrypted = false;
            this.DbColumn35.Expression = "";
            this.DbColumn35.FieldDB = "fechaUltimaInspeccion";
            this.DbColumn35.Font = null;
            this.DbColumn35.Format = null;
            this.DbColumn35.FormatString = null;
            this.DbColumn35.HeaderCaption = "Fecha Últ. Inspección";
            this.DbColumn35.Hidden = false;
            this.DbColumn35.LastValue = false;
            this.DbColumn35.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn35.MaskInput = null;
            this.DbColumn35.MaxLength = 0;
            this.DbColumn35.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn35.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn35.MultiLine = false;
            this.DbColumn35.NullValue = null;
            this.DbColumn35.Obligatory = false;
            this.DbColumn35.PromptChar = '\0';
            this.DbColumn35.ReadColumn = false;
            this.DbColumn35.ShowSelectForm = true;
            this.DbColumn35.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn35.ToolTip = "";
            this.DbColumn35.Unique = false;
            this.DbColumn35.Width = 0;
            // 
            // DbColumn36
            // 
            this.DbColumn36.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn36.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn36.AllowNull = false;
            this.DbColumn36.AllowRowFiltering = false;
            this.DbColumn36.AsociatedButtonColumn = -1;
            this.DbColumn36.AsociatedComboColumn = -1;
            this.DbColumn36.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn36.ColumnDBControl = null;
            this.DbColumn36.ColumnDBFieldData = "";
            this.DbColumn36.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn36.ColumnType = FSFormControls.DBColumn.ColumnTypes.NumberColumn;
            this.DbColumn36.ComboBlankSelection = true;
            this.DbColumn36.ComboImageList = null;
            this.DbColumn36.ComboListField = "";
            this.DbColumn36.DBGridViewFilters = null;
            this.DbColumn36.Decimals = 2;
            this.DbColumn36.DefaultValue = "";
            this.DbColumn36.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn36.Encrypted = false;
            this.DbColumn36.Expression = "";
            this.DbColumn36.FieldDB = "visitasRealizadas";
            this.DbColumn36.Font = null;
            this.DbColumn36.Format = null;
            this.DbColumn36.FormatString = null;
            this.DbColumn36.HeaderCaption = "Visitas Realizadas";
            this.DbColumn36.Hidden = false;
            this.DbColumn36.LastValue = false;
            this.DbColumn36.LogicalOperator = FSFormControls.DBColumn.LogicalOperatorEnum.Or;
            this.DbColumn36.MaskInput = null;
            this.DbColumn36.MaxLength = 0;
            this.DbColumn36.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.DbColumn36.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.DbColumn36.MultiLine = false;
            this.DbColumn36.NullValue = null;
            this.DbColumn36.Obligatory = false;
            this.DbColumn36.PromptChar = '\0';
            this.DbColumn36.ReadColumn = false;
            this.DbColumn36.ShowSelectForm = true;
            this.DbColumn36.SortIndicator = FSFormControls.DBColumn.SortIndicatorEnum.Ascending;
            this.DbColumn36.ToolTip = "";
            this.DbColumn36.Unique = false;
            this.DbColumn36.Width = 0;
            // 
            // frmDetalleCliente
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(745, 466);
            this.Controls.Add(this.DbControl8);
            this.Controls.Add(this.DbControl2);
            this.Controls.Add(this.DbControl5);
            this.Controls.Add(this.DbControl4);
            this.Controls.Add(this.DbControl6);
            this.Controls.Add(this.DbControl7);
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.DbControl1);
            this.Controls.Add(this.DbRecord1);
            this.DataControl = this.DbControl1;
            this.Name = "frmDetalleCliente";
            this.Text = "Detalle Cliente";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmDetalleCliente_Load);
            this.Controls.SetChildIndex(this.DbToolBar1, 0);
            this.Controls.SetChildIndex(this.DbStatusBar1, 0);
            this.Controls.SetChildIndex(this.DbRecord1, 0);
            this.Controls.SetChildIndex(this.DbControl1, 0);
            this.Controls.SetChildIndex(this.TabControl1, 0);
            this.Controls.SetChildIndex(this.DbControl7, 0);
            this.Controls.SetChildIndex(this.DbControl6, 0);
            this.Controls.SetChildIndex(this.DbControl4, 0);
            this.Controls.SetChildIndex(this.DbControl5, 0);
            this.Controls.SetChildIndex(this.DbControl2, 0);
            this.Controls.SetChildIndex(this.DbControl8, 0);
            ((System.ComponentModel.ISupportInitialize)(this.DbStatusBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbStatusBarPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbStatusBarProgressPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbSBarPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbSBarPanel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DbSBarPanel3)).EndInit();
            this.TabControl1.ResumeLayout(false);
            this.TabPage6.ResumeLayout(false);
            this.TabPage6.PerformLayout();
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            this.TabPage7.ResumeLayout(false);
            this.TabPage7.PerformLayout();
            this.TabPage2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		
#endregion
		
		private void frmDetalleCliente_Load(System.Object sender, System.EventArgs e)
		{
			Global.AplicaSeguridad(this);
			Global.AplicaToolbar(this);
		}
		
		private void DbGrid5_DoubleClick(System.Object sender, System.EventArgs e)
		{
			Global.MuestraContrato(this.DbGrid5.get_RowValue(0) + "");
		}
	}
	
}
