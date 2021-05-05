
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;



namespace FSGestion
{
	public class frmDetalleContrato : FSFormControls.DBForm
	{
		
#region  Código generado por el Diseñador de Windows Forms
		
		public frmDetalleContrato()
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
		internal System.Windows.Forms.TabPage TabPage1;
		internal System.Windows.Forms.TabPage TabPage2;
		internal System.Windows.Forms.TabPage TabPage4;
		internal FSFormControls.DBColumn DbColumn1;
		internal FSFormControls.DBColumn DbColumn2;
		internal FSFormControls.DBColumn DbColumn3;
		internal FSFormControls.DBColumn DbColumn7;
		internal FSFormControls.DBColumn DbColumn8;
		internal FSFormControls.DBGrid DbGrid1;
		internal FSFormControls.DBColumn DbColumn9;
		internal FSFormControls.DBColumn DbColumn10;
		internal FSFormControls.DBControl DbControl2;
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
		internal FSFormControls.DBColumn DbColumn41;
		internal FSFormControls.DBColumn DbColumn11;
		internal FSFormControls.DBColumn DbColumn12;
		internal FSFormControls.DBColumn DbColumn42;
		internal FSFormControls.DBColumn DbColumn43;
		internal FSFormControls.DBColumn DbColumn44;
		internal FSFormControls.DBRecord DbRecord5;
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
		internal FSFormControls.DBColumn idContrato;
		internal FSFormControls.DBControl DbControl7;
		internal FSFormControls.DBColumn DbColumn39;
		internal FSFormControls.DBColumn DbColumn46;
		internal FSFormControls.DBColumn DbColumn47;
		internal FSFormControls.DBControl DbControl8;
		internal FSFormControls.DBControl DbControl3;
		internal FSFormControls.DBFile DbFile1;
        private SplitContainer splitContainer1;
        internal FSFormControls.DBRecord DbRecord2;
        internal FSFormControls.DBGrid DbGrid5;
        internal FSFormControls.DBColumn DbColumn4;
		
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
            this.empDbColumn19 = new FSFormControls.DBColumn();
            this.DbControl1 = new FSFormControls.DBControl();
            this.empDbColumn18 = new FSFormControls.DBColumn();
            this.DbRecord1 = new FSFormControls.DBRecord();
            this.DbColumn1 = new FSFormControls.DBColumn();
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.DbRecord5 = new FSFormControls.DBRecord();
            this.idContrato = new FSFormControls.DBColumn();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.DbColumn46 = new FSFormControls.DBColumn();
            this.DbColumn39 = new FSFormControls.DBColumn();
            this.DbColumn47 = new FSFormControls.DBColumn();
            this.DbColumn4 = new FSFormControls.DBColumn();
            this.DbControl8 = new FSFormControls.DBControl();
            this.DbColumn11 = new FSFormControls.DBColumn();
            this.DbColumn12 = new FSFormControls.DBColumn();
            this.DbColumn2 = new FSFormControls.DBColumn();
            this.DbColumn3 = new FSFormControls.DBColumn();
            this.DbColumn7 = new FSFormControls.DBColumn();
            this.DbColumn8 = new FSFormControls.DBColumn();
            this.DbColumn43 = new FSFormControls.DBColumn();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.DbControl3 = new FSFormControls.DBControl();
            this.DbFile1 = new FSFormControls.DBFile();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.DbGrid1 = new FSFormControls.DBGrid();
            this.DbColumn9 = new FSFormControls.DBColumn();
            this.DbColumn10 = new FSFormControls.DBColumn();
            this.DbControl2 = new FSFormControls.DBControl();
            this.DbColumn17 = new FSFormControls.DBColumn();
            this.DbColumn19 = new FSFormControls.DBColumn();
            this.DbColumn20 = new FSFormControls.DBColumn();
            this.DbColumn21 = new FSFormControls.DBColumn();
            this.DbColumn22 = new FSFormControls.DBColumn();
            this.DbColumn23 = new FSFormControls.DBColumn();
            this.DbColumn24 = new FSFormControls.DBColumn();
            this.DbColumn13 = new FSFormControls.DBColumn();
            this.DbColumn14 = new FSFormControls.DBColumn();
            this.DbColumn15 = new FSFormControls.DBColumn();
            this.DbColumn16 = new FSFormControls.DBColumn();
            this.DbColumn18 = new FSFormControls.DBColumn();
            this.DbColumn42 = new FSFormControls.DBColumn();
            this.DbColumn44 = new FSFormControls.DBColumn();
            this.DbColumn37 = new FSFormControls.DBColumn();
            this.DbColumn38 = new FSFormControls.DBColumn();
            this.DbColumn41 = new FSFormControls.DBColumn();
            this.DbColumn25 = new FSFormControls.DBColumn();
            this.DbColumn26 = new FSFormControls.DBColumn();
            this.DbColumn27 = new FSFormControls.DBColumn();
            this.DbColumn28 = new FSFormControls.DBColumn();
            this.DbColumn29 = new FSFormControls.DBColumn();
            this.DbColumn32 = new FSFormControls.DBColumn();
            this.DbColumn33 = new FSFormControls.DBColumn();
            this.DbColumn30 = new FSFormControls.DBColumn();
            this.DbColumn31 = new FSFormControls.DBColumn();
            this.DbColumn34 = new FSFormControls.DBColumn();
            this.DbColumn35 = new FSFormControls.DBColumn();
            this.DbColumn36 = new FSFormControls.DBColumn();
            this.DbControl7 = new FSFormControls.DBControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.DbRecord2 = new FSFormControls.DBRecord();
            this.DbGrid5 = new FSFormControls.DBGrid();
            this.TabControl1.SuspendLayout();
            this.TabPage6.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.TabPage4.SuspendLayout();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // empDbColumn19
            // 
            this.empDbColumn19.ActiveColumnDBButtonOnReadMode = true;
            this.empDbColumn19.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.empDbColumn19.Decimals = 2;
            this.empDbColumn19.DefaultValue = "";
            this.empDbColumn19.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.empDbColumn19.Encrypted = false;
            this.empDbColumn19.Expression = "";
            this.empDbColumn19.FieldDB = "fechaContrato";
            this.empDbColumn19.Font = null;
            this.empDbColumn19.FormatString = null;
            this.empDbColumn19.HeaderCaption = "Fecha Contrato";
            this.empDbColumn19.Hidden = false;
            this.empDbColumn19.LastValue = false;
            this.empDbColumn19.MaskInput = null;
            this.empDbColumn19.MaxLength = 0;
            this.empDbColumn19.MaxValue = decimal.MaxValue;
            this.empDbColumn19.Obligatory = false;
            this.empDbColumn19.ReadColumn = false;
            this.empDbColumn19.ShowSelectForm = true;
            this.empDbColumn19.Width = 0;
            this.empDbColumn19.ToolTip = "";
            this.empDbColumn19.Unique = false;
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
            this.DbControl1.Selection = "select * from Contratos where idContrato=?";
            this.DbControl1.Size = new System.Drawing.Size(88, 72);
            this.DbControl1.TabIndex = 3;
            this.DbControl1.TableName = "Contratos";
            this.DbControl1.TabStop = false;
            this.DbControl1.Text = "SQL: select * from Contratos where idContrato=?";
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
            this.empDbColumn18.Decimals = 2;
            this.empDbColumn18.DefaultValue = "";
            this.empDbColumn18.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.empDbColumn18.Encrypted = false;
            this.empDbColumn18.Expression = "";
            this.empDbColumn18.FieldDB = "firmadoPor";
            this.empDbColumn18.Font = null;
            this.empDbColumn18.FormatString = null;
            this.empDbColumn18.HeaderCaption = "Firmado por:";
            this.empDbColumn18.Hidden = false;
            this.empDbColumn18.LastValue = false;
            this.empDbColumn18.MaskInput = null;
            this.empDbColumn18.MaxLength = 0;
            this.empDbColumn18.MaxValue = decimal.MaxValue;
            this.empDbColumn18.Obligatory = false;
            this.empDbColumn18.ReadColumn = false;
            this.empDbColumn18.ShowSelectForm = true;
            this.empDbColumn18.Width = 0;
            this.empDbColumn18.ToolTip = "";
            this.empDbColumn18.Unique = false;
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
            this.DbRecord1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DbRecord1.Columns.AddRange(new FSFormControls.DBColumn[] {
            this.DbColumn1});
            this.DbRecord1.DataControl = this.DbControl1;
            this.DbRecord1.DateType = FSFormControls.DBRecord.t_date.Normal;
            this.DbRecord1.DoubleHeightInLargeText = false;
            this.DbRecord1.LabelAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbRecord1.LabelYIncrement = 30;
            this.DbRecord1.Location = new System.Drawing.Point(8, 56);
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
            this.DbRecord1.Size = new System.Drawing.Size(752, 56);
            this.DbRecord1.TabIndex = 2;
            this.DbRecord1.TextBoxShadow = false;
            this.DbRecord1.Track = false;
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
            this.DbColumn1.ColumnType = FSFormControls.DBColumn.ColumnTypes.DateColumn;
            this.DbColumn1.ComboBlankSelection = true;
            this.DbColumn1.ComboImageList = null;
            this.DbColumn1.ComboListField = "";
            this.DbColumn1.Decimals = 2;
            this.DbColumn1.DefaultValue = "";
            this.DbColumn1.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn1.Encrypted = false;
            this.DbColumn1.Expression = "";
            this.DbColumn1.FieldDB = "fechaContrato";
            this.DbColumn1.Font = null;
            this.DbColumn1.FormatString = null;
            this.DbColumn1.HeaderCaption = "Fecha Contrato:";
            this.DbColumn1.Hidden = false;
            this.DbColumn1.LastValue = false;
            this.DbColumn1.MaskInput = null;
            this.DbColumn1.MaxLength = 0;
            this.DbColumn1.MaxValue = decimal.MaxValue;
            this.DbColumn1.Obligatory = false;
            this.DbColumn1.ReadColumn = true;
            this.DbColumn1.ShowSelectForm = true;
            this.DbColumn1.Width = 0;
            this.DbColumn1.ToolTip = "";
            this.DbColumn1.Unique = false;
            // 
            // TabControl1
            // 
            this.TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl1.Controls.Add(this.TabPage6);
            this.TabControl1.Controls.Add(this.TabPage1);
            this.TabControl1.Controls.Add(this.TabPage4);
            this.TabControl1.Controls.Add(this.TabPage2);
            this.TabControl1.Location = new System.Drawing.Point(8, 120);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(752, 254);
            this.TabControl1.TabIndex = 4;
            // 
            // TabPage6
            // 
            this.TabPage6.Controls.Add(this.DbRecord5);
            this.TabPage6.Location = new System.Drawing.Point(4, 22);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Size = new System.Drawing.Size(744, 228);
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
            this.DbRecord5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DbRecord5.Columns.AddRange(new FSFormControls.DBColumn[] {
            this.idContrato,
            this.empDbColumn19,
            this.empDbColumn18});
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
            this.DbRecord5.ShowComboEdit = false;
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
            this.DbRecord5.Size = new System.Drawing.Size(744, 228);
            this.DbRecord5.TabIndex = 3;
            this.DbRecord5.TextBoxShadow = false;
            this.DbRecord5.Track = false;
            // 
            // idContrato
            // 
            this.idContrato.ActiveColumnDBButtonOnReadMode = true;
            this.idContrato.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.idContrato.AsociatedButtonColumn = -1;
            this.idContrato.AsociatedComboColumn = -1;
            this.idContrato.ColumnBackColor = System.Drawing.Color.Empty;
            this.idContrato.ColumnDBControl = null;
            this.idContrato.ColumnDBFieldData = "";
            this.idContrato.ColumnForeColor = System.Drawing.Color.Empty;
            this.idContrato.ColumnType = FSFormControls.DBColumn.ColumnTypes.NumberColumn;
            this.idContrato.ComboBlankSelection = true;
            this.idContrato.ComboImageList = null;
            this.idContrato.ComboListField = "";
            this.idContrato.Decimals = 0;
            this.idContrato.DefaultValue = "";
            this.idContrato.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.idContrato.Encrypted = false;
            this.idContrato.Expression = "";
            this.idContrato.FieldDB = "idContrato";
            this.idContrato.Font = null;
            this.idContrato.FormatString = null;
            this.idContrato.HeaderCaption = "Código";
            this.idContrato.Hidden = false;
            this.idContrato.LastValue = false;
            this.idContrato.MaskInput = null;
            this.idContrato.MaxLength = 0;
            this.idContrato.MaxValue = decimal.MaxValue;
            this.idContrato.Obligatory = false;
            this.idContrato.ReadColumn = true;
            this.idContrato.ShowSelectForm = true;
            this.idContrato.Width = 0;
            this.idContrato.ToolTip = "";
            this.idContrato.Unique = false;
            // 
            // TabPage1
            // 
            this.TabPage1.Controls.Add(this.splitContainer1);
            this.TabPage1.Location = new System.Drawing.Point(4, 22);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Size = new System.Drawing.Size(744, 228);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Facturación";
            // 
            // DbColumn46
            // 
            this.DbColumn46.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn46.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn46.AsociatedButtonColumn = -1;
            this.DbColumn46.AsociatedComboColumn = -1;
            this.DbColumn46.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn46.ColumnDBControl = null;
            this.DbColumn46.ColumnDBFieldData = "";
            this.DbColumn46.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn46.ColumnType = FSFormControls.DBColumn.ColumnTypes.NumberColumn;
            this.DbColumn46.ComboBlankSelection = true;
            this.DbColumn46.ComboImageList = null;
            this.DbColumn46.ComboListField = "";
            this.DbColumn46.Decimals = 2;
            this.DbColumn46.DefaultValue = "";
            this.DbColumn46.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.NumberDescription;
            this.DbColumn46.Encrypted = false;
            this.DbColumn46.Expression = "";
            this.DbColumn46.FieldDB = "codigo";
            this.DbColumn46.Font = null;
            this.DbColumn46.FormatString = null;
            this.DbColumn46.HeaderCaption = "Código";
            this.DbColumn46.Hidden = false;
            this.DbColumn46.LastValue = false;
            this.DbColumn46.MaskInput = null;
            this.DbColumn46.MaxLength = 0;
            this.DbColumn46.MaxValue = decimal.MaxValue;
            this.DbColumn46.Obligatory = false;
            this.DbColumn46.ReadColumn = true;
            this.DbColumn46.ShowSelectForm = true;
            this.DbColumn46.Width = 0;
            this.DbColumn46.ToolTip = "";
            this.DbColumn46.Unique = false;
            // 
            // DbColumn39
            // 
            this.DbColumn39.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn39.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn39.Decimals = 2;
            this.DbColumn39.DefaultValue = "";
            this.DbColumn39.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn39.Encrypted = false;
            this.DbColumn39.Expression = "";
            this.DbColumn39.FieldDB = "fecha";
            this.DbColumn39.Font = null;
            this.DbColumn39.FormatString = null;
            this.DbColumn39.HeaderCaption = "Fecha";
            this.DbColumn39.Hidden = false;
            this.DbColumn39.LastValue = false;
            this.DbColumn39.MaskInput = null;
            this.DbColumn39.MaxLength = 0;
            this.DbColumn39.MaxValue = decimal.MaxValue;
            this.DbColumn39.Obligatory = false;
            this.DbColumn39.ReadColumn = false;
            this.DbColumn39.ShowSelectForm = true;
            this.DbColumn39.Width = 0;
            this.DbColumn39.ToolTip = "";
            this.DbColumn39.Unique = false;
            // 
            // DbColumn47
            // 
            this.DbColumn47.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn47.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn47.Decimals = 2;
            this.DbColumn47.DefaultValue = "";
            this.DbColumn47.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn47.Encrypted = false;
            this.DbColumn47.Expression = "";
            this.DbColumn47.FieldDB = "formapago";
            this.DbColumn47.Font = null;
            this.DbColumn47.FormatString = null;
            this.DbColumn47.HeaderCaption = "Forma de Pago";
            this.DbColumn47.Hidden = false;
            this.DbColumn47.LastValue = false;
            this.DbColumn47.MaskInput = null;
            this.DbColumn47.MaxLength = 0;
            this.DbColumn47.MaxValue = decimal.MaxValue;
            this.DbColumn47.Obligatory = false;
            this.DbColumn47.ReadColumn = false;
            this.DbColumn47.ShowSelectForm = true;
            this.DbColumn47.Width = 0;
            this.DbColumn47.ToolTip = "";
            this.DbColumn47.Unique = false;
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
            this.DbColumn4.ColumnType = FSFormControls.DBColumn.ColumnTypes.AutoNumericColumn;
            this.DbColumn4.ComboBlankSelection = true;
            this.DbColumn4.ComboImageList = null;
            this.DbColumn4.ComboListField = "";
            this.DbColumn4.Decimals = 2;
            this.DbColumn4.DefaultValue = "";
            this.DbColumn4.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn4.Encrypted = false;
            this.DbColumn4.Expression = "";
            this.DbColumn4.FieldDB = "nroFactura";
            this.DbColumn4.Font = null;
            this.DbColumn4.FormatString = null;
            this.DbColumn4.HeaderCaption = "Nº Factura";
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
            // DbControl8
            // 
            this.DbControl8.About = null;
            this.DbControl8.AutoConnect = true;
            this.DbControl8.DataControl = null;
            this.DbControl8.DataTable = null;
            //this.DbControl8.DBConnection = null;
            this.DbControl8.DBFieldData = "";
            this.DbControl8.DBPosition = 0;
            this.DbControl8.EraseDBControl = null;
            this.DbControl8.Filter = "";
            this.DbControl8.isEOF = true;
            this.DbControl8.Location = new System.Drawing.Point(240, 376);
            this.DbControl8.LOCK = null;
            this.DbControl8.LOPD = null;
            this.DbControl8.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbControl8.Name = "DbControl8";
            this.DbControl8.Page = 0;
            this.DbControl8.PageSettings = null;
            this.DbControl8.Paging = false;
            this.DbControl8.PagingSize = 0;
            this.DbControl8.ReadOnly = false;
            this.DbControl8.RelationDataControl = this.DbControl1;
            this.DbControl8.RelationDBField = "idContrato";
            this.DbControl8.RelationParentDBField = "idContrato";
            this.DbControl8.SaveError = false;
            this.DbControl8.SaveOnChangeRecord = false;
            this.DbControl8.Selection = "select * from cabeceraPedido where idContrato=?";
            this.DbControl8.Size = new System.Drawing.Size(112, 48);
            this.DbControl8.TabIndex = 11;
            this.DbControl8.TableName = "cabeceraPedido";
            this.DbControl8.TabStop = false;
            this.DbControl8.Text = "SQL: select * from cabeceraPedido where idContrato=?";
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
            // DbColumn11
            // 
            this.DbColumn11.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn11.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn11.Decimals = 2;
            this.DbColumn11.DefaultValue = "";
            this.DbColumn11.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn11.Encrypted = false;
            this.DbColumn11.Expression = "";
            this.DbColumn11.FieldDB = "personaContactoFac";
            this.DbColumn11.Font = null;
            this.DbColumn11.FormatString = null;
            this.DbColumn11.HeaderCaption = "Persona de Contacto";
            this.DbColumn11.Hidden = false;
            this.DbColumn11.LastValue = false;
            this.DbColumn11.MaskInput = null;
            this.DbColumn11.MaxLength = 0;
            this.DbColumn11.MaxValue = decimal.MaxValue;
            this.DbColumn11.Obligatory = false;
            this.DbColumn11.ReadColumn = false;
            this.DbColumn11.ShowSelectForm = true;
            this.DbColumn11.Width = 0;
            this.DbColumn11.ToolTip = "";
            this.DbColumn11.Unique = false;
            // 
            // DbColumn12
            // 
            this.DbColumn12.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn12.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn12.Decimals = 2;
            this.DbColumn12.DefaultValue = "";
            this.DbColumn12.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn12.Encrypted = false;
            this.DbColumn12.Expression = "";
            this.DbColumn12.FieldDB = "telefono1Fac";
            this.DbColumn12.Font = null;
            this.DbColumn12.FormatString = null;
            this.DbColumn12.HeaderCaption = "Teléfono";
            this.DbColumn12.Hidden = false;
            this.DbColumn12.LastValue = false;
            this.DbColumn12.MaskInput = null;
            this.DbColumn12.MaxLength = 0;
            this.DbColumn12.MaxValue = decimal.MaxValue;
            this.DbColumn12.Obligatory = false;
            this.DbColumn12.ReadColumn = false;
            this.DbColumn12.ShowSelectForm = true;
            this.DbColumn12.Width = 0;
            this.DbColumn12.ToolTip = "";
            this.DbColumn12.Unique = false;
            // 
            // DbColumn2
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
            this.DbColumn2.FieldDB = "formaDePago";
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
            // DbColumn3
            // 
            this.DbColumn3.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn3.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn3.Decimals = 2;
            this.DbColumn3.DefaultValue = "";
            this.DbColumn3.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn3.Encrypted = false;
            this.DbColumn3.Expression = "";
            this.DbColumn3.FieldDB = "numeroCuenta";
            this.DbColumn3.Font = null;
            this.DbColumn3.FormatString = null;
            this.DbColumn3.HeaderCaption = "Número de Cuenta";
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
            this.DbColumn7.FieldDB = "factura";
            this.DbColumn7.Font = null;
            this.DbColumn7.FormatString = null;
            this.DbColumn7.HeaderCaption = "Factura";
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
            // DbColumn8
            // 
            this.DbColumn8.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn8.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn8.Decimals = 2;
            this.DbColumn8.DefaultValue = "";
            this.DbColumn8.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn8.Encrypted = false;
            this.DbColumn8.Expression = "";
            this.DbColumn8.FieldDB = "observaciones";
            this.DbColumn8.Font = null;
            this.DbColumn8.FormatString = null;
            this.DbColumn8.HeaderCaption = "Observaciones";
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
            // DbColumn43
            // 
            this.DbColumn43.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn43.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn43.Decimals = 2;
            this.DbColumn43.DefaultValue = "";
            this.DbColumn43.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn43.Encrypted = false;
            this.DbColumn43.Expression = "";
            this.DbColumn43.FieldDB = "estadoActual";
            this.DbColumn43.Font = null;
            this.DbColumn43.FormatString = null;
            this.DbColumn43.HeaderCaption = "Estado Actual";
            this.DbColumn43.Hidden = false;
            this.DbColumn43.LastValue = false;
            this.DbColumn43.MaskInput = null;
            this.DbColumn43.MaxLength = 0;
            this.DbColumn43.MaxValue = decimal.MaxValue;
            this.DbColumn43.Obligatory = false;
            this.DbColumn43.ReadColumn = false;
            this.DbColumn43.ShowSelectForm = true;
            this.DbColumn43.Width = 0;
            this.DbColumn43.ToolTip = "";
            this.DbColumn43.Unique = false;
            // 
            // TabPage4
            // 
            this.TabPage4.Controls.Add(this.DbControl3);
            this.TabPage4.Controls.Add(this.DbFile1);
            this.TabPage4.Location = new System.Drawing.Point(4, 22);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(744, 228);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Documentación";
            // 
            // DbControl3
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
            this.DbControl3.Location = new System.Drawing.Point(436, 103);
            this.DbControl3.LOCK = null;
            this.DbControl3.LOPD = null;
            this.DbControl3.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbControl3.Name = "DbControl3";
            this.DbControl3.Page = 0;
            this.DbControl3.PageSettings = null;
            this.DbControl3.Paging = false;
            this.DbControl3.PagingSize = 0;
            this.DbControl3.ReadOnly = false;
            this.DbControl3.RelationDataControl = this.DbControl1;
            this.DbControl3.RelationDBField = "idContrato";
            this.DbControl3.RelationParentDBField = "idContrato";
            this.DbControl3.SaveError = false;
            this.DbControl3.SaveOnChangeRecord = false;
            this.DbControl3.Selection = "select * from documentos where idContrato=?";
            this.DbControl3.Size = new System.Drawing.Size(112, 64);
            this.DbControl3.TabIndex = 7;
            this.DbControl3.TableName = "documentos";
            this.DbControl3.TabStop = false;
            this.DbControl3.Text = "SQL: select * from documentos where idContrato=?";
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
            // DbFile1
            // 
            this.DbFile1.About = null;
            this.DbFile1.Data = null;
            this.DbFile1.DataControl = this.DbControl3;
            this.DbFile1.DBField = "fichero";
            this.DbFile1.FieldDateTime = "fecha";
            this.DbFile1.FieldFileName = "nombre";
            this.DbFile1.Location = new System.Drawing.Point(144, 56);
            this.DbFile1.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
            this.DbFile1.Name = "DbFile1";
            this.DbFile1.ShowText = true;
            this.DbFile1.Size = new System.Drawing.Size(288, 20);
            this.DbFile1.TabIndex = 6;
            this.DbFile1.Text = "DBFile1";
            this.DbFile1.Track = false;
            // 
            // TabPage2
            // 
            this.TabPage2.Controls.Add(this.DbGrid1);
            this.TabPage2.Location = new System.Drawing.Point(4, 22);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Size = new System.Drawing.Size(744, 228);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Visitas";
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
            this.DbColumn9,
            this.DbColumn10});
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
            this.DbGrid1.Size = new System.Drawing.Size(744, 228);
            this.DbGrid1.TabIndex = 0;
            this.DbGrid1.TotalOperation = FSFormControls.DBColumn.OperationTypes.Sum;
            this.DbGrid1.Track = false;
            this.DbGrid1.XMLName = "";
            // 
            // DbColumn9
            // 
            this.DbColumn9.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn9.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn9.Decimals = 2;
            this.DbColumn9.DefaultValue = "";
            this.DbColumn9.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn9.Encrypted = false;
            this.DbColumn9.Expression = "";
            this.DbColumn9.FieldDB = "fechaVisita";
            this.DbColumn9.Font = null;
            this.DbColumn9.FormatString = null;
            this.DbColumn9.HeaderCaption = "Fecha Visita";
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
            this.DbColumn10.FieldDB = "descripcion";
            this.DbColumn10.Font = null;
            this.DbColumn10.FormatString = null;
            this.DbColumn10.HeaderCaption = "Descripción";
            this.DbColumn10.Hidden = false;
            this.DbColumn10.LastValue = false;
            this.DbColumn10.MaskInput = null;
            this.DbColumn10.MaxLength = 0;
            this.DbColumn10.MaxValue = decimal.MaxValue;
            this.DbColumn10.Obligatory = false;
            this.DbColumn10.ReadColumn = false;
            this.DbColumn10.ShowSelectForm = true;
            this.DbColumn10.Width = 0;
            this.DbColumn10.ToolTip = "";
            this.DbColumn10.Unique = false;
            // 
            // DbControl2
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
            this.DbControl2.Location = new System.Drawing.Point(352, 32);
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
            this.DbControl2.RelationDBField = "idContrato";
            this.DbControl2.RelationParentDBField = "idContrato";
            this.DbControl2.SaveError = false;
            this.DbControl2.SaveOnChangeRecord = false;
            this.DbControl2.Selection = "select * from visitas where idContrato=?";
            this.DbControl2.Size = new System.Drawing.Size(72, 56);
            this.DbControl2.TabIndex = 5;
            this.DbControl2.TableName = "visitas";
            this.DbControl2.TabStop = false;
            this.DbControl2.Text = "SQL: select * from visitas where idContrato=?";
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
            // DbColumn17
            // 
            this.DbColumn17.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn17.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn17.Decimals = 2;
            this.DbColumn17.DefaultValue = "";
            this.DbColumn17.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn17.Encrypted = false;
            this.DbColumn17.Expression = "";
            this.DbColumn17.FieldDB = "FechaEvaluacionRiesgos";
            this.DbColumn17.Font = null;
            this.DbColumn17.FormatString = null;
            this.DbColumn17.HeaderCaption = "Fecha Evaluación Riesgos";
            this.DbColumn17.Hidden = false;
            this.DbColumn17.LastValue = false;
            this.DbColumn17.MaskInput = null;
            this.DbColumn17.MaxLength = 0;
            this.DbColumn17.MaxValue = decimal.MaxValue;
            this.DbColumn17.Obligatory = false;
            this.DbColumn17.ReadColumn = false;
            this.DbColumn17.ShowSelectForm = true;
            this.DbColumn17.Width = 0;
            this.DbColumn17.ToolTip = "";
            this.DbColumn17.Unique = false;
            // 
            // DbColumn19
            // 
            this.DbColumn19.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn19.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn19.Decimals = 2;
            this.DbColumn19.DefaultValue = "";
            this.DbColumn19.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn19.Encrypted = false;
            this.DbColumn19.Expression = "";
            this.DbColumn19.FieldDB = "planificacionPreventivaAnual";
            this.DbColumn19.Font = null;
            this.DbColumn19.FormatString = null;
            this.DbColumn19.HeaderCaption = "Planificación Preventiva Anual";
            this.DbColumn19.Hidden = false;
            this.DbColumn19.LastValue = false;
            this.DbColumn19.MaskInput = null;
            this.DbColumn19.MaxLength = 0;
            this.DbColumn19.MaxValue = decimal.MaxValue;
            this.DbColumn19.Obligatory = false;
            this.DbColumn19.ReadColumn = false;
            this.DbColumn19.ShowSelectForm = true;
            this.DbColumn19.Width = 0;
            this.DbColumn19.ToolTip = "";
            this.DbColumn19.Unique = false;
            // 
            // DbColumn20
            // 
            this.DbColumn20.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn20.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn20.Decimals = 2;
            this.DbColumn20.DefaultValue = "";
            this.DbColumn20.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn20.Encrypted = false;
            this.DbColumn20.Expression = "";
            this.DbColumn20.FieldDB = "PlanEmergencia";
            this.DbColumn20.Font = null;
            this.DbColumn20.FormatString = null;
            this.DbColumn20.HeaderCaption = "Plan Emergencia";
            this.DbColumn20.Hidden = false;
            this.DbColumn20.LastValue = false;
            this.DbColumn20.MaskInput = null;
            this.DbColumn20.MaxLength = 0;
            this.DbColumn20.MaxValue = decimal.MaxValue;
            this.DbColumn20.Obligatory = false;
            this.DbColumn20.ReadColumn = false;
            this.DbColumn20.ShowSelectForm = true;
            this.DbColumn20.Width = 0;
            this.DbColumn20.ToolTip = "";
            this.DbColumn20.Unique = false;
            // 
            // DbColumn21
            // 
            this.DbColumn21.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn21.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn21.Decimals = 2;
            this.DbColumn21.DefaultValue = "";
            this.DbColumn21.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn21.Encrypted = false;
            this.DbColumn21.Expression = "";
            this.DbColumn21.FieldDB = "Cursos";
            this.DbColumn21.Font = null;
            this.DbColumn21.FormatString = null;
            this.DbColumn21.HeaderCaption = "Cursos";
            this.DbColumn21.Hidden = false;
            this.DbColumn21.LastValue = false;
            this.DbColumn21.MaskInput = null;
            this.DbColumn21.MaxLength = 0;
            this.DbColumn21.MaxValue = decimal.MaxValue;
            this.DbColumn21.Obligatory = false;
            this.DbColumn21.ReadColumn = false;
            this.DbColumn21.ShowSelectForm = true;
            this.DbColumn21.Width = 0;
            this.DbColumn21.ToolTip = "";
            this.DbColumn21.Unique = false;
            // 
            // DbColumn22
            // 
            this.DbColumn22.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn22.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn22.Decimals = 2;
            this.DbColumn22.DefaultValue = "";
            this.DbColumn22.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn22.Encrypted = false;
            this.DbColumn22.Expression = "";
            this.DbColumn22.FieldDB = "Charlas";
            this.DbColumn22.Font = null;
            this.DbColumn22.FormatString = null;
            this.DbColumn22.HeaderCaption = "Charlas";
            this.DbColumn22.Hidden = false;
            this.DbColumn22.LastValue = false;
            this.DbColumn22.MaskInput = null;
            this.DbColumn22.MaxLength = 0;
            this.DbColumn22.MaxValue = decimal.MaxValue;
            this.DbColumn22.Obligatory = false;
            this.DbColumn22.ReadColumn = false;
            this.DbColumn22.ShowSelectForm = true;
            this.DbColumn22.Width = 0;
            this.DbColumn22.ToolTip = "";
            this.DbColumn22.Unique = false;
            // 
            // DbColumn23
            // 
            this.DbColumn23.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn23.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn23.Decimals = 0;
            this.DbColumn23.DefaultValue = "";
            this.DbColumn23.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn23.Encrypted = false;
            this.DbColumn23.Expression = "";
            this.DbColumn23.FieldDB = "numeroPuestosEvaluados";
            this.DbColumn23.Font = null;
            this.DbColumn23.FormatString = null;
            this.DbColumn23.HeaderCaption = "Nº Puestos Evaluados";
            this.DbColumn23.Hidden = false;
            this.DbColumn23.LastValue = false;
            this.DbColumn23.MaskInput = null;
            this.DbColumn23.MaxLength = 0;
            this.DbColumn23.MaxValue = decimal.MaxValue;
            this.DbColumn23.Obligatory = false;
            this.DbColumn23.ReadColumn = false;
            this.DbColumn23.ShowSelectForm = true;
            this.DbColumn23.Width = 0;
            this.DbColumn23.ToolTip = "";
            this.DbColumn23.Unique = false;
            // 
            // DbColumn24
            // 
            this.DbColumn24.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn24.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn24.Decimals = 0;
            this.DbColumn24.DefaultValue = "";
            this.DbColumn24.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn24.Encrypted = false;
            this.DbColumn24.Expression = "";
            this.DbColumn24.FieldDB = "numeroAccidentesInvestigados";
            this.DbColumn24.Font = null;
            this.DbColumn24.FormatString = null;
            this.DbColumn24.HeaderCaption = "Nº Accidentes Investigados";
            this.DbColumn24.Hidden = false;
            this.DbColumn24.LastValue = false;
            this.DbColumn24.MaskInput = null;
            this.DbColumn24.MaxLength = 0;
            this.DbColumn24.MaxValue = decimal.MaxValue;
            this.DbColumn24.Obligatory = false;
            this.DbColumn24.ReadColumn = false;
            this.DbColumn24.ShowSelectForm = true;
            this.DbColumn24.Width = 0;
            this.DbColumn24.ToolTip = "";
            this.DbColumn24.Unique = false;
            // 
            // DbColumn13
            // 
            this.DbColumn13.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn13.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn13.Decimals = 2;
            this.DbColumn13.DefaultValue = "";
            this.DbColumn13.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn13.Encrypted = false;
            this.DbColumn13.Expression = "";
            this.DbColumn13.FieldDB = "FechaRealizacion";
            this.DbColumn13.Font = null;
            this.DbColumn13.FormatString = null;
            this.DbColumn13.HeaderCaption = "Fecha de Realización";
            this.DbColumn13.Hidden = false;
            this.DbColumn13.LastValue = false;
            this.DbColumn13.MaskInput = null;
            this.DbColumn13.MaxLength = 0;
            this.DbColumn13.MaxValue = decimal.MaxValue;
            this.DbColumn13.Obligatory = false;
            this.DbColumn13.ReadColumn = false;
            this.DbColumn13.ShowSelectForm = true;
            this.DbColumn13.Width = 0;
            this.DbColumn13.ToolTip = "";
            this.DbColumn13.Unique = false;
            // 
            // DbColumn14
            // 
            this.DbColumn14.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn14.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn14.Decimals = 0;
            this.DbColumn14.DefaultValue = "";
            this.DbColumn14.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn14.Encrypted = false;
            this.DbColumn14.Expression = "";
            this.DbColumn14.FieldDB = "NumeroRMRealizados";
            this.DbColumn14.Font = null;
            this.DbColumn14.FormatString = null;
            this.DbColumn14.HeaderCaption = "Nº RM Realizados";
            this.DbColumn14.Hidden = false;
            this.DbColumn14.LastValue = false;
            this.DbColumn14.MaskInput = null;
            this.DbColumn14.MaxLength = 0;
            this.DbColumn14.MaxValue = decimal.MaxValue;
            this.DbColumn14.Obligatory = false;
            this.DbColumn14.ReadColumn = false;
            this.DbColumn14.ShowSelectForm = true;
            this.DbColumn14.Width = 0;
            this.DbColumn14.ToolTip = "";
            this.DbColumn14.Unique = false;
            // 
            // DbColumn15
            // 
            this.DbColumn15.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn15.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn15.Decimals = 2;
            this.DbColumn15.DefaultValue = "";
            this.DbColumn15.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn15.Encrypted = false;
            this.DbColumn15.Expression = "";
            this.DbColumn15.FieldDB = "primerosAuxilios";
            this.DbColumn15.Font = null;
            this.DbColumn15.FormatString = null;
            this.DbColumn15.HeaderCaption = "Primeros Auxilios";
            this.DbColumn15.Hidden = false;
            this.DbColumn15.LastValue = false;
            this.DbColumn15.MaskInput = null;
            this.DbColumn15.MaxLength = 0;
            this.DbColumn15.MaxValue = decimal.MaxValue;
            this.DbColumn15.Obligatory = false;
            this.DbColumn15.ReadColumn = false;
            this.DbColumn15.ShowSelectForm = true;
            this.DbColumn15.Width = 0;
            this.DbColumn15.ToolTip = "";
            this.DbColumn15.Unique = false;
            // 
            // DbColumn16
            // 
            this.DbColumn16.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn16.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn16.Decimals = 2;
            this.DbColumn16.DefaultValue = "";
            this.DbColumn16.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn16.Encrypted = false;
            this.DbColumn16.Expression = "";
            this.DbColumn16.FieldDB = "evaluacion";
            this.DbColumn16.Font = null;
            this.DbColumn16.FormatString = null;
            this.DbColumn16.HeaderCaption = "Evaluación";
            this.DbColumn16.Hidden = false;
            this.DbColumn16.LastValue = false;
            this.DbColumn16.MaskInput = null;
            this.DbColumn16.MaxLength = 0;
            this.DbColumn16.MaxValue = decimal.MaxValue;
            this.DbColumn16.Obligatory = false;
            this.DbColumn16.ReadColumn = false;
            this.DbColumn16.ShowSelectForm = true;
            this.DbColumn16.Width = 0;
            this.DbColumn16.ToolTip = "";
            this.DbColumn16.Unique = false;
            // 
            // DbColumn18
            // 
            this.DbColumn18.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn18.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn18.Decimals = 2;
            this.DbColumn18.DefaultValue = "";
            this.DbColumn18.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn18.Encrypted = false;
            this.DbColumn18.Expression = "";
            this.DbColumn18.FieldDB = "tipoEvaluacion";
            this.DbColumn18.Font = null;
            this.DbColumn18.FormatString = null;
            this.DbColumn18.HeaderCaption = "Tipo Evaluación";
            this.DbColumn18.Hidden = false;
            this.DbColumn18.LastValue = false;
            this.DbColumn18.MaskInput = null;
            this.DbColumn18.MaxLength = 0;
            this.DbColumn18.MaxValue = decimal.MaxValue;
            this.DbColumn18.Obligatory = false;
            this.DbColumn18.ReadColumn = false;
            this.DbColumn18.ShowSelectForm = true;
            this.DbColumn18.Width = 0;
            this.DbColumn18.ToolTip = "";
            this.DbColumn18.Unique = false;
            // 
            // DbColumn42
            // 
            this.DbColumn42.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn42.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn42.Decimals = 2;
            this.DbColumn42.DefaultValue = "";
            this.DbColumn42.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn42.Encrypted = false;
            this.DbColumn42.Expression = "";
            this.DbColumn42.FieldDB = "protocolosAplicados";
            this.DbColumn42.Font = null;
            this.DbColumn42.FormatString = null;
            this.DbColumn42.HeaderCaption = "Protocolos Aplicados";
            this.DbColumn42.Hidden = false;
            this.DbColumn42.LastValue = false;
            this.DbColumn42.MaskInput = null;
            this.DbColumn42.MaxLength = 0;
            this.DbColumn42.MaxValue = decimal.MaxValue;
            this.DbColumn42.Obligatory = false;
            this.DbColumn42.ReadColumn = false;
            this.DbColumn42.ShowSelectForm = true;
            this.DbColumn42.Width = 0;
            this.DbColumn42.ToolTip = "";
            this.DbColumn42.Unique = false;
            // 
            // DbColumn44
            // 
            this.DbColumn44.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn44.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn44.Decimals = 2;
            this.DbColumn44.DefaultValue = "";
            this.DbColumn44.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn44.Encrypted = false;
            this.DbColumn44.Expression = "";
            this.DbColumn44.FieldDB = "lugarRealizacion";
            this.DbColumn44.Font = null;
            this.DbColumn44.FormatString = null;
            this.DbColumn44.HeaderCaption = "Lugar Realización";
            this.DbColumn44.Hidden = false;
            this.DbColumn44.LastValue = false;
            this.DbColumn44.MaskInput = null;
            this.DbColumn44.MaxLength = 0;
            this.DbColumn44.MaxValue = decimal.MaxValue;
            this.DbColumn44.Obligatory = false;
            this.DbColumn44.ReadColumn = false;
            this.DbColumn44.ShowSelectForm = true;
            this.DbColumn44.Width = 0;
            this.DbColumn44.ToolTip = "";
            this.DbColumn44.Unique = false;
            // 
            // DbColumn37
            // 
            this.DbColumn37.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn37.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn37.Decimals = 2;
            this.DbColumn37.DefaultValue = "";
            this.DbColumn37.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn37.Encrypted = false;
            this.DbColumn37.Expression = "";
            this.DbColumn37.FieldDB = "fechaVisita";
            this.DbColumn37.Font = null;
            this.DbColumn37.FormatString = null;
            this.DbColumn37.HeaderCaption = "Fecha Visita";
            this.DbColumn37.Hidden = false;
            this.DbColumn37.LastValue = false;
            this.DbColumn37.MaskInput = null;
            this.DbColumn37.MaxLength = 0;
            this.DbColumn37.MaxValue = decimal.MaxValue;
            this.DbColumn37.Obligatory = false;
            this.DbColumn37.ReadColumn = false;
            this.DbColumn37.ShowSelectForm = true;
            this.DbColumn37.Width = 0;
            this.DbColumn37.ToolTip = "";
            this.DbColumn37.Unique = false;
            // 
            // DbColumn38
            // 
            this.DbColumn38.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn38.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn38.Decimals = 2;
            this.DbColumn38.DefaultValue = "";
            this.DbColumn38.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn38.Encrypted = false;
            this.DbColumn38.Expression = "";
            this.DbColumn38.FieldDB = "descripcion";
            this.DbColumn38.Font = null;
            this.DbColumn38.FormatString = null;
            this.DbColumn38.HeaderCaption = "Descripción";
            this.DbColumn38.Hidden = false;
            this.DbColumn38.LastValue = false;
            this.DbColumn38.MaskInput = null;
            this.DbColumn38.MaxLength = 0;
            this.DbColumn38.MaxValue = decimal.MaxValue;
            this.DbColumn38.Obligatory = false;
            this.DbColumn38.ReadColumn = false;
            this.DbColumn38.ShowSelectForm = true;
            this.DbColumn38.Width = 0;
            this.DbColumn38.ToolTip = "";
            this.DbColumn38.Unique = false;
            // 
            // DbColumn41
            // 
            this.DbColumn41.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn41.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn41.Decimals = 0;
            this.DbColumn41.DefaultValue = "";
            this.DbColumn41.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn41.Encrypted = false;
            this.DbColumn41.Expression = "";
            this.DbColumn41.FieldDB = "idConstruccion";
            this.DbColumn41.Font = null;
            this.DbColumn41.FormatString = null;
            this.DbColumn41.HeaderCaption = "Código";
            this.DbColumn41.Hidden = false;
            this.DbColumn41.LastValue = false;
            this.DbColumn41.MaskInput = null;
            this.DbColumn41.MaxLength = 0;
            this.DbColumn41.MaxValue = decimal.MaxValue;
            this.DbColumn41.Obligatory = false;
            this.DbColumn41.ReadColumn = false;
            this.DbColumn41.ShowSelectForm = true;
            this.DbColumn41.Width = 0;
            this.DbColumn41.ToolTip = "";
            this.DbColumn41.Unique = false;
            // 
            // DbColumn25
            // 
            this.DbColumn25.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn25.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn25.Decimals = 2;
            this.DbColumn25.DefaultValue = "";
            this.DbColumn25.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn25.Encrypted = false;
            this.DbColumn25.Expression = "";
            this.DbColumn25.FieldDB = "Obra";
            this.DbColumn25.Font = null;
            this.DbColumn25.FormatString = null;
            this.DbColumn25.HeaderCaption = "Descripción";
            this.DbColumn25.Hidden = false;
            this.DbColumn25.LastValue = false;
            this.DbColumn25.MaskInput = null;
            this.DbColumn25.MaxLength = 0;
            this.DbColumn25.MaxValue = decimal.MaxValue;
            this.DbColumn25.Obligatory = false;
            this.DbColumn25.ReadColumn = false;
            this.DbColumn25.ShowSelectForm = true;
            this.DbColumn25.Width = 0;
            this.DbColumn25.ToolTip = "";
            this.DbColumn25.Unique = false;
            // 
            // DbColumn26
            // 
            this.DbColumn26.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn26.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn26.Decimals = 2;
            this.DbColumn26.DefaultValue = "";
            this.DbColumn26.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn26.Encrypted = false;
            this.DbColumn26.Expression = "";
            this.DbColumn26.FieldDB = "direccion";
            this.DbColumn26.Font = null;
            this.DbColumn26.FormatString = null;
            this.DbColumn26.HeaderCaption = "Dirección";
            this.DbColumn26.Hidden = false;
            this.DbColumn26.LastValue = false;
            this.DbColumn26.MaskInput = null;
            this.DbColumn26.MaxLength = 0;
            this.DbColumn26.MaxValue = decimal.MaxValue;
            this.DbColumn26.Obligatory = false;
            this.DbColumn26.ReadColumn = false;
            this.DbColumn26.ShowSelectForm = true;
            this.DbColumn26.Width = 0;
            this.DbColumn26.ToolTip = "";
            this.DbColumn26.Unique = false;
            // 
            // DbColumn27
            // 
            this.DbColumn27.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn27.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn27.Decimals = 2;
            this.DbColumn27.DefaultValue = "";
            this.DbColumn27.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn27.Encrypted = false;
            this.DbColumn27.Expression = "";
            this.DbColumn27.FieldDB = "telefono1";
            this.DbColumn27.Font = null;
            this.DbColumn27.FormatString = null;
            this.DbColumn27.HeaderCaption = "Telefono 1";
            this.DbColumn27.Hidden = false;
            this.DbColumn27.LastValue = false;
            this.DbColumn27.MaskInput = null;
            this.DbColumn27.MaxLength = 0;
            this.DbColumn27.MaxValue = decimal.MaxValue;
            this.DbColumn27.Obligatory = false;
            this.DbColumn27.ReadColumn = false;
            this.DbColumn27.ShowSelectForm = true;
            this.DbColumn27.Width = 0;
            this.DbColumn27.ToolTip = "";
            this.DbColumn27.Unique = false;
            // 
            // DbColumn28
            // 
            this.DbColumn28.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn28.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn28.Decimals = 2;
            this.DbColumn28.DefaultValue = "";
            this.DbColumn28.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn28.Encrypted = false;
            this.DbColumn28.Expression = "";
            this.DbColumn28.FieldDB = "telefono2";
            this.DbColumn28.Font = null;
            this.DbColumn28.FormatString = null;
            this.DbColumn28.HeaderCaption = "Telefono 2";
            this.DbColumn28.Hidden = false;
            this.DbColumn28.LastValue = false;
            this.DbColumn28.MaskInput = null;
            this.DbColumn28.MaxLength = 0;
            this.DbColumn28.MaxValue = decimal.MaxValue;
            this.DbColumn28.Obligatory = false;
            this.DbColumn28.ReadColumn = false;
            this.DbColumn28.ShowSelectForm = true;
            this.DbColumn28.Width = 0;
            this.DbColumn28.ToolTip = "";
            this.DbColumn28.Unique = false;
            // 
            // DbColumn29
            // 
            this.DbColumn29.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn29.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn29.Decimals = 2;
            this.DbColumn29.DefaultValue = "";
            this.DbColumn29.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn29.Encrypted = false;
            this.DbColumn29.Expression = "";
            this.DbColumn29.FieldDB = "personaContacto";
            this.DbColumn29.Font = null;
            this.DbColumn29.FormatString = null;
            this.DbColumn29.HeaderCaption = "Contacto";
            this.DbColumn29.Hidden = false;
            this.DbColumn29.LastValue = false;
            this.DbColumn29.MaskInput = null;
            this.DbColumn29.MaxLength = 0;
            this.DbColumn29.MaxValue = decimal.MaxValue;
            this.DbColumn29.Obligatory = false;
            this.DbColumn29.ReadColumn = false;
            this.DbColumn29.ShowSelectForm = true;
            this.DbColumn29.Width = 0;
            this.DbColumn29.ToolTip = "";
            this.DbColumn29.Unique = false;
            // 
            // DbColumn32
            // 
            this.DbColumn32.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn32.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn32.Decimals = 2;
            this.DbColumn32.DefaultValue = "";
            this.DbColumn32.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn32.Encrypted = false;
            this.DbColumn32.Expression = "";
            this.DbColumn32.FieldDB = "coordinacion";
            this.DbColumn32.Font = null;
            this.DbColumn32.FormatString = null;
            this.DbColumn32.HeaderCaption = "Coodinación";
            this.DbColumn32.Hidden = false;
            this.DbColumn32.LastValue = false;
            this.DbColumn32.MaskInput = null;
            this.DbColumn32.MaxLength = 0;
            this.DbColumn32.MaxValue = decimal.MaxValue;
            this.DbColumn32.Obligatory = false;
            this.DbColumn32.ReadColumn = false;
            this.DbColumn32.ShowSelectForm = true;
            this.DbColumn32.Width = 0;
            this.DbColumn32.ToolTip = "";
            this.DbColumn32.Unique = false;
            // 
            // DbColumn33
            // 
            this.DbColumn33.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn33.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn33.Decimals = 2;
            this.DbColumn33.DefaultValue = "";
            this.DbColumn33.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn33.Encrypted = false;
            this.DbColumn33.Expression = "";
            this.DbColumn33.FieldDB = "estudioSeguridad";
            this.DbColumn33.Font = null;
            this.DbColumn33.FormatString = null;
            this.DbColumn33.HeaderCaption = "Estudio Seguridad";
            this.DbColumn33.Hidden = false;
            this.DbColumn33.LastValue = false;
            this.DbColumn33.MaskInput = null;
            this.DbColumn33.MaxLength = 0;
            this.DbColumn33.MaxValue = decimal.MaxValue;
            this.DbColumn33.Obligatory = false;
            this.DbColumn33.ReadColumn = false;
            this.DbColumn33.ShowSelectForm = true;
            this.DbColumn33.Width = 0;
            this.DbColumn33.ToolTip = "";
            this.DbColumn33.Unique = false;
            // 
            // DbColumn30
            // 
            this.DbColumn30.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn30.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbColumn30.AsociatedButtonColumn = -1;
            this.DbColumn30.AsociatedComboColumn = -1;
            this.DbColumn30.ColumnBackColor = System.Drawing.Color.Empty;
            this.DbColumn30.ColumnDBControl = null;
            this.DbColumn30.ColumnDBFieldData = "";
            this.DbColumn30.ColumnForeColor = System.Drawing.Color.Empty;
            this.DbColumn30.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
            this.DbColumn30.ComboBlankSelection = true;
            this.DbColumn30.ComboImageList = null;
            this.DbColumn30.ComboListField = "";
            this.DbColumn30.Decimals = 2;
            this.DbColumn30.DefaultValue = "";
            this.DbColumn30.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn30.Encrypted = false;
            this.DbColumn30.Expression = "";
            this.DbColumn30.FieldDB = "contratadoPS";
            this.DbColumn30.Font = null;
            this.DbColumn30.FormatString = null;
            this.DbColumn30.HeaderCaption = "PS";
            this.DbColumn30.Hidden = false;
            this.DbColumn30.LastValue = false;
            this.DbColumn30.MaskInput = null;
            this.DbColumn30.MaxLength = 0;
            this.DbColumn30.MaxValue = decimal.MaxValue;
            this.DbColumn30.Obligatory = false;
            this.DbColumn30.ReadColumn = false;
            this.DbColumn30.ShowSelectForm = true;
            this.DbColumn30.Width = 0;
            this.DbColumn30.ToolTip = "";
            this.DbColumn30.Unique = false;
            // 
            // DbColumn31
            // 
            this.DbColumn31.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn31.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn31.Decimals = 0;
            this.DbColumn31.DefaultValue = "";
            this.DbColumn31.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn31.Encrypted = false;
            this.DbColumn31.Expression = "";
            this.DbColumn31.FieldDB = "numeroInspeccion";
            this.DbColumn31.Font = null;
            this.DbColumn31.FormatString = null;
            this.DbColumn31.HeaderCaption = "Nº Inspección";
            this.DbColumn31.Hidden = false;
            this.DbColumn31.LastValue = false;
            this.DbColumn31.MaskInput = null;
            this.DbColumn31.MaxLength = 0;
            this.DbColumn31.MaxValue = decimal.MaxValue;
            this.DbColumn31.Obligatory = false;
            this.DbColumn31.ReadColumn = false;
            this.DbColumn31.ShowSelectForm = true;
            this.DbColumn31.Width = 0;
            this.DbColumn31.ToolTip = "";
            this.DbColumn31.Unique = false;
            // 
            // DbColumn34
            // 
            this.DbColumn34.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn34.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn34.Decimals = 2;
            this.DbColumn34.DefaultValue = "";
            this.DbColumn34.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn34.Encrypted = false;
            this.DbColumn34.Expression = "";
            this.DbColumn34.FieldDB = "entregaPS";
            this.DbColumn34.Font = null;
            this.DbColumn34.FormatString = null;
            this.DbColumn34.HeaderCaption = "Entrega PS";
            this.DbColumn34.Hidden = false;
            this.DbColumn34.LastValue = false;
            this.DbColumn34.MaskInput = null;
            this.DbColumn34.MaxLength = 0;
            this.DbColumn34.MaxValue = decimal.MaxValue;
            this.DbColumn34.Obligatory = false;
            this.DbColumn34.ReadColumn = false;
            this.DbColumn34.ShowSelectForm = true;
            this.DbColumn34.Width = 0;
            this.DbColumn34.ToolTip = "";
            this.DbColumn34.Unique = false;
            // 
            // DbColumn35
            // 
            this.DbColumn35.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn35.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn35.Decimals = 2;
            this.DbColumn35.DefaultValue = "";
            this.DbColumn35.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn35.Encrypted = false;
            this.DbColumn35.Expression = "";
            this.DbColumn35.FieldDB = "fechaUltimaInspeccion";
            this.DbColumn35.Font = null;
            this.DbColumn35.FormatString = null;
            this.DbColumn35.HeaderCaption = "Fecha Últ. Inspección";
            this.DbColumn35.Hidden = false;
            this.DbColumn35.LastValue = false;
            this.DbColumn35.MaskInput = null;
            this.DbColumn35.MaxLength = 0;
            this.DbColumn35.MaxValue = decimal.MaxValue;
            this.DbColumn35.Obligatory = false;
            this.DbColumn35.ReadColumn = false;
            this.DbColumn35.ShowSelectForm = true;
            this.DbColumn35.Width = 0;
            this.DbColumn35.ToolTip = "";
            this.DbColumn35.Unique = false;
            // 
            // DbColumn36
            // 
            this.DbColumn36.ActiveColumnDBButtonOnReadMode = true;
            this.DbColumn36.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.DbColumn36.Decimals = 2;
            this.DbColumn36.DefaultValue = "";
            this.DbColumn36.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
            this.DbColumn36.Encrypted = false;
            this.DbColumn36.Expression = "";
            this.DbColumn36.FieldDB = "visitasRealizadas";
            this.DbColumn36.Font = null;
            this.DbColumn36.FormatString = null;
            this.DbColumn36.HeaderCaption = "Visitas Realizadas";
            this.DbColumn36.Hidden = false;
            this.DbColumn36.LastValue = false;
            this.DbColumn36.MaskInput = null;
            this.DbColumn36.MaxLength = 0;
            this.DbColumn36.MaxValue = decimal.MaxValue;
            this.DbColumn36.Obligatory = false;
            this.DbColumn36.ReadColumn = false;
            this.DbColumn36.ShowSelectForm = true;
            this.DbColumn36.Width = 0;
            this.DbColumn36.ToolTip = "";
            this.DbColumn36.Unique = false;
            // 
            // DbControl7
            // 
            this.DbControl7.About = null;
            this.DbControl7.AutoConnect = true;
            this.DbControl7.DataControl = null;
            this.DbControl7.DataTable = null;
            //this.DbControl7.DBConnection = null;
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
            this.DbControl7.Selection = "select * from Clientes where idCliente=?";
            this.DbControl7.Size = new System.Drawing.Size(112, 40);
            this.DbControl7.TabIndex = 10;
            this.DbControl7.TableName = "Clientes";
            this.DbControl7.TabStop = false;
            this.DbControl7.Text = "SQL: select * from Clientes where idCliente=?";
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
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DbRecord2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.DbGrid5);
            this.splitContainer1.Size = new System.Drawing.Size(744, 228);
            this.splitContainer1.SplitterDistance = 117;
            this.splitContainer1.TabIndex = 3;
            // 
            // DbRecord2
            // 
            this.DbRecord2.About = null;
            this.DbRecord2.AllowAddNew = true;
            this.DbRecord2.AllowCancel = true;
            this.DbRecord2.AllowDelete = true;
            this.DbRecord2.AllowEdit = true;
            this.DbRecord2.AllowFilter = true;
            this.DbRecord2.AllowList = true;
            this.DbRecord2.AllowNavigate = true;
            this.DbRecord2.AllowPrint = true;
            this.DbRecord2.AllowRecord = true;
            this.DbRecord2.AllowSave = true;
            this.DbRecord2.AllowSearch = true;
            this.DbRecord2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DbRecord2.Columns.AddRange(new FSFormControls.DBColumn[] {
            this.DbColumn11,
            this.DbColumn12,
            this.DbColumn2,
            this.DbColumn3,
            this.DbColumn7,
            this.DbColumn8,
            this.DbColumn43});
            this.DbRecord2.DataControl = this.DbControl1;
            this.DbRecord2.DateType = FSFormControls.DBRecord.t_date.Normal;
            this.DbRecord2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DbRecord2.DoubleHeightInLargeText = false;
            this.DbRecord2.LabelAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbRecord2.LabelYIncrement = 30;
            this.DbRecord2.Location = new System.Drawing.Point(0, 0);
            this.DbRecord2.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbRecord2.Name = "DbRecord2";
            this.DbRecord2.PosXLabel = 20;
            this.DbRecord2.PosYLabel = 20;
            this.DbRecord2.ShowAddNew = true;
            this.DbRecord2.ShowCancel = true;
            this.DbRecord2.ShowClose = true;
            this.DbRecord2.ShowComboEdit = false;
            this.DbRecord2.ShowDelete = true;
            this.DbRecord2.ShowEdit = true;
            this.DbRecord2.ShowFilter = true;
            this.DbRecord2.ShowList = true;
            this.DbRecord2.ShowMode = FSFormControls.DBRecord.t_showmode.Vertical;
            this.DbRecord2.ShowNavigate = true;
            this.DbRecord2.ShowPrint = true;
            this.DbRecord2.ShowRecord = true;
            this.DbRecord2.ShowSave = true;
            this.DbRecord2.ShowScrollBar = false;
            this.DbRecord2.ShowSearch = true;
            this.DbRecord2.ShowToolBar = false;
            this.DbRecord2.Size = new System.Drawing.Size(744, 117);
            this.DbRecord2.TabIndex = 1;
            this.DbRecord2.TextBoxShadow = false;
            this.DbRecord2.Track = false;
            // 
            // DbGrid5
            // 
            this.DbGrid5.About = null;
            this.DbGrid5.AllowAddNew = true;
            this.DbGrid5.AllowDelete = true;
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
            this.DbColumn46,
            this.DbColumn39,
            this.DbColumn47,
            this.DbColumn4});
            this.DbGrid5.CurrentRowIndex = -1;
            this.DbGrid5.CustomColumnHeaders = false;
            this.DbGrid5.DataControl = this.DbControl8;
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
            this.DbGrid5.Size = new System.Drawing.Size(744, 107);
            this.DbGrid5.TabIndex = 2;
            this.DbGrid5.TotalOperation = FSFormControls.DBColumn.OperationTypes.Sum;
            this.DbGrid5.Track = false;
            this.DbGrid5.XMLName = "";
            // 
            // frmDetalleContrato
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(768, 407);
            this.Controls.Add(this.DbControl8);
            this.Controls.Add(this.DbControl7);
            this.Controls.Add(this.DbControl2);
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.DbControl1);
            this.Controls.Add(this.DbRecord1);
            this.DataControl = this.DbControl1;
            this.Name = "frmDetalleContrato";
            this.Text = "Detalle Contrato";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmDetalleCliente_Load);
            this.Controls.SetChildIndex(this.DbRecord1, 0);
            this.Controls.SetChildIndex(this.DbControl1, 0);
            this.Controls.SetChildIndex(this.TabControl1, 0);
            this.Controls.SetChildIndex(this.DbControl2, 0);
            this.Controls.SetChildIndex(this.DbControl7, 0);
            this.Controls.SetChildIndex(this.DbControl8, 0);
            this.TabControl1.ResumeLayout(false);
            this.TabPage6.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage4.ResumeLayout(false);
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		
#endregion
		
		
		private void frmDetalleCliente_Load(System.Object sender, System.EventArgs e)
		{
			Global.AplicaSeguridad(this);
			Global.AplicaToolbar(this);
			
			this.TabControl1.SelectedTab = this.TabControl1.TabPages[0];
		}
		
		private void DbGrid5_DoubleClick(System.Object sender, System.EventArgs e)
		{
			Global.MuestraFactura(DbGrid5.get_RowValue(0).ToString());
		}
	}
	
}
