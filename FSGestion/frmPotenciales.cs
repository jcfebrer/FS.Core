
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;



namespace FSGestion
{
	public class frmPotenciales : FSFormControls.DBForm
	{
		
#region  Código generado por el Diseñador de Windows Forms
		
		public frmPotenciales()
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
		internal FSFormControls.DBControl DbControl1;
		internal FSFormControls.DBGrid DbGrid1;
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal FSFormControls.DBLabel DbLabel2;
		internal FSFormControls.DBButton dbbLocalizar;
		internal FSFormControls.DBTextBox txtNombreCliente;
		internal FSFormControls.DBButton dbbTodas;
		internal FSFormControls.DBColumn empDbColumn17;
		internal FSFormControls.DBColumn empDbColumn1;
		internal FSFormControls.DBColumn empDbColumn2;
		internal FSFormControls.DBColumn empDbColumn3;
		internal FSFormControls.DBColumn empDbColumn4;
		internal FSFormControls.DBColumn empDbColumn5;
		internal FSFormControls.DBColumn empDbColumn6;
		internal FSFormControls.DBColumn empDbColumn111;
		internal FSFormControls.DBTextBox txtActividad;
		internal FSFormControls.DBLabel DbLabel1;
		internal FSFormControls.DBColumn DbColumn1;
		internal FSFormControls.DBColumn empDbColumn7;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.DbControl1 = new FSFormControls.DBControl();
			base.Load += new System.EventHandler(frmClientes_Load);
			this.DbGrid1 = new FSFormControls.DBGrid();
			this.empDbColumn17 = new FSFormControls.DBColumn();
			this.empDbColumn1 = new FSFormControls.DBColumn();
			this.empDbColumn2 = new FSFormControls.DBColumn();
			this.empDbColumn3 = new FSFormControls.DBColumn();
			this.empDbColumn4 = new FSFormControls.DBColumn();
			this.empDbColumn5 = new FSFormControls.DBColumn();
			this.empDbColumn6 = new FSFormControls.DBColumn();
			this.empDbColumn7 = new FSFormControls.DBColumn();
			this.DbColumn1 = new FSFormControls.DBColumn();
			this.empDbColumn111 = new FSFormControls.DBColumn();
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			this.txtActividad = new FSFormControls.DBTextBox();
			this.DbLabel1 = new FSFormControls.DBLabel();
			this.dbbTodas = new FSFormControls.DBButton();
			this.dbbTodas.Click += new System.EventHandler(this.dbbTodas_Click);
			this.dbbLocalizar = new FSFormControls.DBButton();
			this.dbbLocalizar.Click += new System.EventHandler(this.dbbLocalizar_Click);
			this.txtNombreCliente = new FSFormControls.DBTextBox();
			this.DbLabel2 = new FSFormControls.DBLabel();
			this.GroupBox1.SuspendLayout();
			this.SuspendLayout();
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
			this.DbControl1.Location = new System.Drawing.Point(578, 80);
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
			this.DbControl1.Selection = "select * from potenciales order by nombre";
			this.DbControl1.Size = new System.Drawing.Size(126, 62);
			this.DbControl1.TabIndex = 3;
			this.DbControl1.TableName = "potenciales";
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
			//DbGrid1
			//
			this.DbGrid1.About = null;
			this.DbGrid1.AllowAddNew = true;
			this.DbGrid1.AllowDelete = false;
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
			this.DbGrid1.CaptionText = "Listado de clientes potenciales";
			this.DbGrid1.CaptionVisible = true;
			this.DbGrid1.ColumnHeadersVisible = true;
			this.DbGrid1.Columns.AddRange(new FSFormControls.DBColumn[] {this.empDbColumn17, this.empDbColumn1, this.empDbColumn2, this.empDbColumn3, this.empDbColumn4, this.empDbColumn5, this.empDbColumn6, this.empDbColumn7, this.DbColumn1, this.empDbColumn111});
			this.DbGrid1.CurrentRowIndex = -1;
			this.DbGrid1.CustomColumnHeaders = false;
			this.DbGrid1.DataControl = this.DbControl1;
			this.DbGrid1.DefaultDecimals = 2;
			this.DbGrid1.DefaultHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.DbGrid1.Editable = true;
			this.DbGrid1.FlatMode = System.Convert.ToBoolean(System.Windows.Forms.FlatStyle.Flat);
			this.DbGrid1.GridLineColor = System.Drawing.SystemColors.Control;
			this.DbGrid1.GridLineStyle = System.Windows.Forms.DataGridLineStyle.Solid;
			this.DbGrid1.HeaderBackColor = System.Drawing.SystemColors.Control;
			this.DbGrid1.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
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
			this.DbGrid1.Size = new System.Drawing.Size(696, 424);
			this.DbGrid1.TabIndex = 4;
			this.DbGrid1.Track = false;
			this.DbGrid1.XMLName = "";
			//
			//empDbColumn17
			//
			this.empDbColumn17.ActiveColumnDBButtonOnReadMode = true;
			this.empDbColumn17.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.empDbColumn17.AsociatedButtonColumn = -1;
			this.empDbColumn17.AsociatedComboColumn = -1;
			this.empDbColumn17.ColumnBackColor = System.Drawing.Color.Empty;
			this.empDbColumn17.ColumnDBControl = null;
			this.empDbColumn17.ColumnForeColor = System.Drawing.Color.Empty;
			this.empDbColumn17.ColumnType = FSFormControls.DBColumn.ColumnTypes.NumberColumn;
			this.empDbColumn17.ComboBlankSelection = true;
			this.empDbColumn17.ColumnDBFieldData = "";
			this.empDbColumn17.ComboListField = "";
			this.empDbColumn17.Decimals = 0;
			this.empDbColumn17.DefaultValue = "";
			this.empDbColumn17.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.empDbColumn17.Encrypted = false;
			this.empDbColumn17.Expression = "";
			this.empDbColumn17.FieldDB = "idCliente";
			this.empDbColumn17.Font = null;
			this.empDbColumn17.FormatString = "";
			this.empDbColumn17.HeaderCaption = "Código";
			this.empDbColumn17.Hidden = false;
			this.empDbColumn17.LastValue = false;
			this.empDbColumn17.MaskInput = null;
			this.empDbColumn17.MaxLength = 0;
			this.empDbColumn17.MaxValue = decimal.MaxValue;
			this.empDbColumn17.Obligatory = false;
			this.empDbColumn17.ReadColumn = true;
			this.empDbColumn17.ShowSelectForm = true;
			this.empDbColumn17.Width = 0;
			this.empDbColumn17.ToolTip = "";
			this.empDbColumn17.Unique = false;
			//
			//empDbColumn1
			//
			this.empDbColumn1.ActiveColumnDBButtonOnReadMode = true;
			this.empDbColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.empDbColumn1.AsociatedButtonColumn = -1;
			this.empDbColumn1.AsociatedComboColumn = -1;
			this.empDbColumn1.ColumnBackColor = System.Drawing.Color.Empty;
			this.empDbColumn1.ColumnDBControl = null;
			this.empDbColumn1.ColumnForeColor = System.Drawing.Color.Empty;
			this.empDbColumn1.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
			this.empDbColumn1.ComboBlankSelection = true;
			this.empDbColumn1.ColumnDBFieldData = "";
			this.empDbColumn1.ComboListField = "";
			this.empDbColumn1.Decimals = 2;
			this.empDbColumn1.DefaultValue = "";
			this.empDbColumn1.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.empDbColumn1.Encrypted = false;
			this.empDbColumn1.Expression = "";
			this.empDbColumn1.FieldDB = "nombre";
			this.empDbColumn1.Font = null;
			this.empDbColumn1.FormatString = "";
			this.empDbColumn1.HeaderCaption = "Nombre";
			this.empDbColumn1.Hidden = false;
			this.empDbColumn1.LastValue = false;
			this.empDbColumn1.MaskInput = null;
			this.empDbColumn1.MaxLength = 0;
			this.empDbColumn1.MaxValue = decimal.MaxValue;
			this.empDbColumn1.Obligatory = false;
			this.empDbColumn1.ReadColumn = false;
			this.empDbColumn1.ShowSelectForm = true;
			this.empDbColumn1.Width = 0;
			this.empDbColumn1.ToolTip = "";
			this.empDbColumn1.Unique = false;
			//
			//empDbColumn2
			//
			this.empDbColumn2.ActiveColumnDBButtonOnReadMode = true;
			this.empDbColumn2.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.empDbColumn2.AsociatedButtonColumn = -1;
			this.empDbColumn2.AsociatedComboColumn = -1;
			this.empDbColumn2.ColumnBackColor = System.Drawing.Color.Empty;
			this.empDbColumn2.ColumnDBControl = null;
			this.empDbColumn2.ColumnForeColor = System.Drawing.Color.Empty;
			this.empDbColumn2.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
			this.empDbColumn2.ComboBlankSelection = true;
			this.empDbColumn2.ColumnDBFieldData = "";
			this.empDbColumn2.ComboListField = "";
			this.empDbColumn2.Decimals = 2;
			this.empDbColumn2.DefaultValue = "";
			this.empDbColumn2.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.empDbColumn2.Encrypted = false;
			this.empDbColumn2.Expression = "";
			this.empDbColumn2.FieldDB = "direccion";
			this.empDbColumn2.Font = null;
			this.empDbColumn2.FormatString = "";
			this.empDbColumn2.HeaderCaption = "Dirección";
			this.empDbColumn2.Hidden = false;
			this.empDbColumn2.LastValue = false;
			this.empDbColumn2.MaskInput = null;
			this.empDbColumn2.MaxLength = 0;
			this.empDbColumn2.MaxValue = decimal.MaxValue;
			this.empDbColumn2.Obligatory = false;
			this.empDbColumn2.ReadColumn = false;
			this.empDbColumn2.ShowSelectForm = true;
			this.empDbColumn2.Width = 0;
			this.empDbColumn2.ToolTip = "";
			this.empDbColumn2.Unique = false;
			//
			//empDbColumn3
			//
			this.empDbColumn3.ActiveColumnDBButtonOnReadMode = true;
			this.empDbColumn3.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.empDbColumn3.AsociatedButtonColumn = -1;
			this.empDbColumn3.AsociatedComboColumn = -1;
			this.empDbColumn3.ColumnBackColor = System.Drawing.Color.Empty;
			this.empDbColumn3.ColumnDBControl = null;
			this.empDbColumn3.ColumnForeColor = System.Drawing.Color.Empty;
			this.empDbColumn3.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
			this.empDbColumn3.ComboBlankSelection = true;
			this.empDbColumn3.ColumnDBFieldData = "";
			this.empDbColumn3.ComboListField = "";
			this.empDbColumn3.Decimals = 2;
			this.empDbColumn3.DefaultValue = "";
			this.empDbColumn3.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.empDbColumn3.Encrypted = false;
			this.empDbColumn3.Expression = "";
			this.empDbColumn3.FieldDB = "telefono1";
			this.empDbColumn3.Font = null;
			this.empDbColumn3.FormatString = "";
			this.empDbColumn3.HeaderCaption = "Teléfono 1";
			this.empDbColumn3.Hidden = false;
			this.empDbColumn3.LastValue = false;
			this.empDbColumn3.MaskInput = null;
			this.empDbColumn3.MaxLength = 0;
			this.empDbColumn3.MaxValue = decimal.MaxValue;
			this.empDbColumn3.Obligatory = false;
			this.empDbColumn3.ReadColumn = false;
			this.empDbColumn3.ShowSelectForm = true;
			this.empDbColumn3.Width = 0;
			this.empDbColumn3.ToolTip = "";
			this.empDbColumn3.Unique = false;
			//
			//empDbColumn4
			//
			this.empDbColumn4.ActiveColumnDBButtonOnReadMode = true;
			this.empDbColumn4.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.empDbColumn4.AsociatedButtonColumn = -1;
			this.empDbColumn4.AsociatedComboColumn = -1;
			this.empDbColumn4.ColumnBackColor = System.Drawing.Color.Empty;
			this.empDbColumn4.ColumnDBControl = null;
			this.empDbColumn4.ColumnForeColor = System.Drawing.Color.Empty;
			this.empDbColumn4.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
			this.empDbColumn4.ComboBlankSelection = true;
			this.empDbColumn4.ColumnDBFieldData = "";
			this.empDbColumn4.ComboListField = "";
			this.empDbColumn4.Decimals = 2;
			this.empDbColumn4.DefaultValue = "";
			this.empDbColumn4.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.empDbColumn4.Encrypted = false;
			this.empDbColumn4.Expression = "";
			this.empDbColumn4.FieldDB = "telefono2";
			this.empDbColumn4.Font = null;
			this.empDbColumn4.FormatString = "";
			this.empDbColumn4.HeaderCaption = "Teléfono 2";
			this.empDbColumn4.Hidden = false;
			this.empDbColumn4.LastValue = false;
			this.empDbColumn4.MaskInput = null;
			this.empDbColumn4.MaxLength = 0;
			this.empDbColumn4.MaxValue = decimal.MaxValue;
			this.empDbColumn4.Obligatory = false;
			this.empDbColumn4.ReadColumn = false;
			this.empDbColumn4.ShowSelectForm = true;
			this.empDbColumn4.Width = 0;
			this.empDbColumn4.ToolTip = "";
			this.empDbColumn4.Unique = false;
			//
			//empDbColumn5
			//
			this.empDbColumn5.ActiveColumnDBButtonOnReadMode = true;
			this.empDbColumn5.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.empDbColumn5.AsociatedButtonColumn = -1;
			this.empDbColumn5.AsociatedComboColumn = -1;
			this.empDbColumn5.ColumnBackColor = System.Drawing.Color.Empty;
			this.empDbColumn5.ColumnDBControl = null;
			this.empDbColumn5.ColumnForeColor = System.Drawing.Color.Empty;
			this.empDbColumn5.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
			this.empDbColumn5.ComboBlankSelection = true;
			this.empDbColumn5.ColumnDBFieldData = "";
			this.empDbColumn5.ComboListField = "";
			this.empDbColumn5.Decimals = 2;
			this.empDbColumn5.DefaultValue = "";
			this.empDbColumn5.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.empDbColumn5.Encrypted = false;
			this.empDbColumn5.Expression = "";
			this.empDbColumn5.FieldDB = "fax";
			this.empDbColumn5.Font = null;
			this.empDbColumn5.FormatString = "";
			this.empDbColumn5.HeaderCaption = "Fax";
			this.empDbColumn5.Hidden = false;
			this.empDbColumn5.LastValue = false;
			this.empDbColumn5.MaskInput = null;
			this.empDbColumn5.MaxLength = 0;
			this.empDbColumn5.MaxValue = decimal.MaxValue;
			this.empDbColumn5.Obligatory = false;
			this.empDbColumn5.ReadColumn = false;
			this.empDbColumn5.ShowSelectForm = true;
			this.empDbColumn5.Width = 0;
			this.empDbColumn5.ToolTip = "";
			this.empDbColumn5.Unique = false;
			//
			//empDbColumn6
			//
			this.empDbColumn6.ActiveColumnDBButtonOnReadMode = true;
			this.empDbColumn6.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.empDbColumn6.AsociatedButtonColumn = -1;
			this.empDbColumn6.AsociatedComboColumn = -1;
			this.empDbColumn6.ColumnBackColor = System.Drawing.Color.Empty;
			this.empDbColumn6.ColumnDBControl = null;
			this.empDbColumn6.ColumnForeColor = System.Drawing.Color.Empty;
			this.empDbColumn6.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
			this.empDbColumn6.ComboBlankSelection = true;
			this.empDbColumn6.ColumnDBFieldData = "";
			this.empDbColumn6.ComboListField = "";
			this.empDbColumn6.Decimals = 2;
			this.empDbColumn6.DefaultValue = "";
			this.empDbColumn6.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.empDbColumn6.Encrypted = false;
			this.empDbColumn6.Expression = "";
			this.empDbColumn6.FieldDB = "personaContacto";
			this.empDbColumn6.Font = null;
			this.empDbColumn6.FormatString = "";
			this.empDbColumn6.HeaderCaption = "Persona de Contacto";
			this.empDbColumn6.Hidden = false;
			this.empDbColumn6.LastValue = false;
			this.empDbColumn6.MaskInput = null;
			this.empDbColumn6.MaxLength = 0;
			this.empDbColumn6.MaxValue = decimal.MaxValue;
			this.empDbColumn6.Obligatory = false;
			this.empDbColumn6.ReadColumn = false;
			this.empDbColumn6.ShowSelectForm = true;
			this.empDbColumn6.Width = 0;
			this.empDbColumn6.ToolTip = "";
			this.empDbColumn6.Unique = false;
			//
			//empDbColumn7
			//
			this.empDbColumn7.ActiveColumnDBButtonOnReadMode = true;
			this.empDbColumn7.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.empDbColumn7.AsociatedButtonColumn = -1;
			this.empDbColumn7.AsociatedComboColumn = -1;
			this.empDbColumn7.ColumnBackColor = System.Drawing.Color.Empty;
			this.empDbColumn7.ColumnDBControl = null;
			this.empDbColumn7.ColumnForeColor = System.Drawing.Color.Empty;
			this.empDbColumn7.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
			this.empDbColumn7.ComboBlankSelection = true;
			this.empDbColumn7.ColumnDBFieldData = "";
			this.empDbColumn7.ComboListField = "";
			this.empDbColumn7.Decimals = 2;
			this.empDbColumn7.DefaultValue = "";
			this.empDbColumn7.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.empDbColumn7.Encrypted = false;
			this.empDbColumn7.Expression = "";
			this.empDbColumn7.FieldDB = "cif";
			this.empDbColumn7.Font = null;
			this.empDbColumn7.FormatString = "";
			this.empDbColumn7.HeaderCaption = "C.I.F.";
			this.empDbColumn7.Hidden = false;
			this.empDbColumn7.LastValue = false;
			this.empDbColumn7.MaskInput = null;
			this.empDbColumn7.MaxLength = 0;
			this.empDbColumn7.MaxValue = decimal.MaxValue;
			this.empDbColumn7.Obligatory = false;
			this.empDbColumn7.ReadColumn = false;
			this.empDbColumn7.ShowSelectForm = true;
			this.empDbColumn7.Width = 0;
			this.empDbColumn7.ToolTip = "";
			this.empDbColumn7.Unique = false;
			//
			//DbColumn1
			//
			this.DbColumn1.ActiveColumnDBButtonOnReadMode = true;
			this.DbColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbColumn1.AsociatedButtonColumn = -1;
			this.DbColumn1.AsociatedComboColumn = -1;
			this.DbColumn1.ColumnBackColor = System.Drawing.Color.Empty;
			this.DbColumn1.ColumnDBControl = null;
			this.DbColumn1.ColumnForeColor = System.Drawing.Color.Empty;
			this.DbColumn1.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
			this.DbColumn1.ComboBlankSelection = true;
			this.DbColumn1.ColumnDBFieldData = "";
			this.DbColumn1.ComboListField = "";
			this.DbColumn1.Decimals = 2;
			this.DbColumn1.DefaultValue = "";
			this.DbColumn1.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn1.Encrypted = false;
			this.DbColumn1.Expression = "";
			this.DbColumn1.FieldDB = "email";
			this.DbColumn1.Font = null;
			this.DbColumn1.FormatString = null;
			this.DbColumn1.HeaderCaption = "Email";
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
			//empDbColumn111
			//
			this.empDbColumn111.ActiveColumnDBButtonOnReadMode = true;
			this.empDbColumn111.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.empDbColumn111.AsociatedButtonColumn = -1;
			this.empDbColumn111.AsociatedComboColumn = -1;
			this.empDbColumn111.ColumnBackColor = System.Drawing.Color.Empty;
			this.empDbColumn111.ColumnDBControl = null;
			this.empDbColumn111.ColumnForeColor = System.Drawing.Color.Empty;
			this.empDbColumn111.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
			this.empDbColumn111.ComboBlankSelection = true;
			this.empDbColumn111.ColumnDBFieldData = "";
			this.empDbColumn111.ComboListField = "";
			this.empDbColumn111.Decimals = 2;
			this.empDbColumn111.DefaultValue = "";
			this.empDbColumn111.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.empDbColumn111.Encrypted = false;
			this.empDbColumn111.Expression = "";
			this.empDbColumn111.FieldDB = "act";
			this.empDbColumn111.Font = null;
			this.empDbColumn111.FormatString = null;
			this.empDbColumn111.HeaderCaption = "Actividad";
			this.empDbColumn111.Hidden = false;
			this.empDbColumn111.LastValue = false;
			this.empDbColumn111.MaskInput = null;
			this.empDbColumn111.MaxLength = 0;
			this.empDbColumn111.MaxValue = decimal.MaxValue;
			this.empDbColumn111.Obligatory = false;
			this.empDbColumn111.ReadColumn = false;
			this.empDbColumn111.ShowSelectForm = true;
			this.empDbColumn111.Width = 0;
			this.empDbColumn111.ToolTip = "";
			this.empDbColumn111.Unique = false;
			//
			//GroupBox1
			//
			this.GroupBox1.Controls.Add(this.txtActividad);
			this.GroupBox1.Controls.Add(this.DbLabel1);
			this.GroupBox1.Controls.Add(this.dbbTodas);
			this.GroupBox1.Controls.Add(this.dbbLocalizar);
			this.GroupBox1.Controls.Add(this.txtNombreCliente);
			this.GroupBox1.Controls.Add(this.DbLabel2);
			this.GroupBox1.Location = new System.Drawing.Point(8, 56);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(544, 94);
			this.GroupBox1.TabIndex = 5;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "Selección:";
			//
			//txtActividad
			//
			this.txtActividad.About = null;
			this.txtActividad.AsociatedCombo = null;
			this.txtActividad.AsociatedDBFindTextBox = null;
			this.txtActividad.BackColorRead = System.Drawing.Color.WhiteSmoke;
			this.txtActividad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.txtActividad.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
			this.txtActividad.DataControl = null;
			this.txtActividad.DataType = FSFormControls.DBTextBox.TypeData.All;
			this.txtActividad.DateFormat = "dd/mm/yyyy";
			this.txtActividad.DBField = null;
			this.txtActividad.DBFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.txtActividad.Decimals = 2;
			this.txtActividad.DefaultValue = null;
			this.txtActividad.DotNumber = false;
			this.txtActividad.Editable = true;
			this.txtActividad.Encrypted = false;
			this.txtActividad.Expression = "";
			this.txtActividad.FormatString = "";
			this.txtActividad.GridOperation = FSFormControls.DBColumn.OperationTypes.Sum;
			this.txtActividad.Location = new System.Drawing.Point(176, 46);
			this.txtActividad.MaskInput = null;
			this.txtActividad.MaxLength = (int) ((long) (32767));
			this.txtActividad.MaxValue = decimal.MaxValue;
			this.txtActividad.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
			this.txtActividad.Multiline = false;
			this.txtActividad.Name = "txtActividad";
			this.txtActividad.Obligatory = false;
			this.txtActividad.PasswordChar = (char)0;
            this.txtActividad.ReadOnly = false;
			this.txtActividad.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.txtActividad.Shadow = false;
			this.txtActividad.ShadowColor = System.Drawing.Color.Gray;
			this.txtActividad.ShadowSize = 4;
			this.txtActividad.ShowKeyboard = false;
			this.txtActividad.Size = new System.Drawing.Size(136, 20);
			this.txtActividad.TabIndex = 11;
			this.txtActividad.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtActividad.ToolTip = "";
			this.txtActividad.Track = false;
			this.txtActividad.XMLName = null;
			//
			//DbLabel1
			//
			this.DbLabel1.About = null;
			this.DbLabel1.Angle = (float) (0.0F);
			this.DbLabel1.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
			this.DbLabel1.DataControl = null;
			this.DbLabel1.DataType = FSFormControls.DBTextBox.TypeData.All;
			this.DbLabel1.DateFormat = "dd/MM/yyyy";
			this.DbLabel1.Decimals = 2;
			this.DbLabel1.EndColor = System.Drawing.Color.LightSkyBlue;
			this.DbLabel1.Location = new System.Drawing.Point(56, 46);
			this.DbLabel1.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbLabel1.Name = "DbLabel1";
			this.DbLabel1.ShadowColor = System.Drawing.Color.Black;
			this.DbLabel1.Size = new System.Drawing.Size(96, 16);
			this.DbLabel1.StartColor = System.Drawing.Color.White;
			this.DbLabel1.TabIndex = 10;
			this.DbLabel1.TabStop = false;
			this.DbLabel1.Text = "Actividad:";
			this.DbLabel1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.DbLabel1.Track = false;
			this.DbLabel1.XOffset = (float) (1.0F);
			this.DbLabel1.YOffset = (float) (1.0F);
			//
			//dbbTodas
			//
			this.dbbTodas.About = null;
			this.dbbTodas.ButtonStyle = FSFormControls.DBButton.ButtonStyleType.Normal;
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
			this.dbbTodas.Location = new System.Drawing.Point(328, 24);
			this.dbbTodas.Name = "dbbTodas";
			this.dbbTodas.Size = new System.Drawing.Size(192, 24);
			this.dbbTodas.TabIndex = 9;
			this.dbbTodas.Text = "Mostrar Todas";
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
			this.dbbLocalizar.Location = new System.Drawing.Point(328, 54);
			this.dbbLocalizar.Name = "dbbLocalizar";
			this.dbbLocalizar.Size = new System.Drawing.Size(192, 24);
			this.dbbLocalizar.TabIndex = 4;
			this.dbbLocalizar.Text = "Localizar";
			this.dbbLocalizar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.dbbLocalizar.TextColorEnd = System.Drawing.Color.Black;
			this.dbbLocalizar.TextColorStart = System.Drawing.Color.Blue;
			this.dbbLocalizar.TextFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.dbbLocalizar.ToolTip = "";
			this.dbbLocalizar.Track = false;
			//
			//txtNombreCliente
			//
			this.txtNombreCliente.About = null;
			this.txtNombreCliente.AsociatedCombo = null;
			this.txtNombreCliente.AsociatedDBFindTextBox = null;
			this.txtNombreCliente.BackColorRead = System.Drawing.Color.WhiteSmoke;
			this.txtNombreCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.txtNombreCliente.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
			this.txtNombreCliente.DataControl = null;
			this.txtNombreCliente.DataType = FSFormControls.DBTextBox.TypeData.All;
			this.txtNombreCliente.DateFormat = "dd/mm/yyyy";
			this.txtNombreCliente.DBField = null;
			this.txtNombreCliente.DBFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.txtNombreCliente.Decimals = 2;
			this.txtNombreCliente.DefaultValue = null;
			this.txtNombreCliente.DotNumber = false;
			this.txtNombreCliente.Editable = true;
			this.txtNombreCliente.Encrypted = false;
			this.txtNombreCliente.Expression = "";
			this.txtNombreCliente.FormatString = "";
			this.txtNombreCliente.GridOperation = FSFormControls.DBColumn.OperationTypes.Sum;
			this.txtNombreCliente.Location = new System.Drawing.Point(176, 24);
			this.txtNombreCliente.MaskInput = null;
			this.txtNombreCliente.MaxLength = (int) ((long) (32767));
			this.txtNombreCliente.MaxValue = decimal.MaxValue;
			this.txtNombreCliente.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
			this.txtNombreCliente.Multiline = false;
			this.txtNombreCliente.Name = "txtNombreCliente";
			this.txtNombreCliente.Obligatory = false;
			this.txtNombreCliente.PasswordChar = (char)0;
            this.txtNombreCliente.ReadOnly = false;
			this.txtNombreCliente.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.txtNombreCliente.Shadow = false;
			this.txtNombreCliente.ShadowColor = System.Drawing.Color.Gray;
			this.txtNombreCliente.ShadowSize = 4;
			this.txtNombreCliente.ShowKeyboard = false;
			this.txtNombreCliente.Size = new System.Drawing.Size(136, 20);
			this.txtNombreCliente.TabIndex = 3;
			this.txtNombreCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtNombreCliente.ToolTip = "";
			this.txtNombreCliente.Track = false;
			this.txtNombreCliente.XMLName = null;
			//
			//DbLabel2
			//
			this.DbLabel2.About = null;
			this.DbLabel2.Angle = (float) (0.0F);
			this.DbLabel2.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
			this.DbLabel2.DataControl = null;
			this.DbLabel2.DataType = FSFormControls.DBTextBox.TypeData.All;
			this.DbLabel2.DateFormat = "dd/MM/yyyy";
			this.DbLabel2.Decimals = 2;
			this.DbLabel2.EndColor = System.Drawing.Color.LightSkyBlue;
			this.DbLabel2.Location = new System.Drawing.Point(56, 24);
			this.DbLabel2.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbLabel2.Name = "DbLabel2";
			this.DbLabel2.ShadowColor = System.Drawing.Color.Black;
			this.DbLabel2.Size = new System.Drawing.Size(96, 16);
			this.DbLabel2.StartColor = System.Drawing.Color.White;
			this.DbLabel2.TabIndex = 1;
			this.DbLabel2.TabStop = false;
			this.DbLabel2.Text = "Nombre Cliente:";
			this.DbLabel2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.DbLabel2.Track = false;
			this.DbLabel2.XOffset = (float) (1.0F);
			this.DbLabel2.YOffset = (float) (1.0F);
			//
			//frmPotenciales
			//
			this.AllowAddNew = false;
			this.AllowDelete = false;
			this.AllowFilter = false;
			this.AllowList = false;
			this.AllowNavigate = false;
			this.AllowPrint = false;
			this.AllowRecord = false;
			this.AllowSearch = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(712, 615);
			this.Controls.Add(this.GroupBox1);
			this.Controls.Add(this.DbControl1);
			this.Controls.Add(this.DbGrid1);
			this.DataControl = this.DbControl1;
			this.Name = "frmPotenciales";
			this.ShowScrollBar = false;
			this.Text = "Listado de Clientes Potenciales";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Controls.SetChildIndex(this.DbGrid1, 0);
			this.Controls.SetChildIndex(this.DbControl1, 0);
			this.Controls.SetChildIndex(this.GroupBox1, 0);
			this.GroupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
			
		}
		
#endregion
		
		
		private void dbbLocalizar_Click(System.Object sender, System.EventArgs e)
		{
			string filtro = default(string);
			filtro = "";
			
			if (this.txtNombreCliente.Text != "")
			{
				filtro = "Nombre like \'%" + this.txtNombreCliente.Text + "%\'";
			}
			
			if (this.txtActividad.Text != "")
			{
				if (filtro != "")
				{
					filtro = filtro + " and ";
				}
				filtro = "(Act like \'%" + this.txtActividad.Text + "%\' or actDes like \'%" + this.txtActividad.Text + "%\')";
			}
			
			this.DbControl1.Filter = filtro;
		}
		
		private void dbbTodas_Click(System.Object sender, System.EventArgs e)
		{
			this.DbControl1.Filter = "";
		}
		
		private void frmClientes_Load(System.Object sender, System.EventArgs e)
		{
			Global.AplicaSeguridad(this);
			Global.AplicaToolbar(this);
		}
	}
	
}
