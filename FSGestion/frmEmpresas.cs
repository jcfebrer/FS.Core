
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;



namespace FSGestion
{
	public class frmEmpresas : FSFormControls.DBForm
	{
		
#region  Código generado por el Diseñador de Windows Forms
		
		public frmEmpresas()
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
		internal FSFormControls.DBGrid DbGrid1;
		internal FSFormControls.DBControl DbControl1;
		internal FSFormControls.DBColumn DbColumn1;
		internal FSFormControls.DBColumn DbColumn2;
		internal FSFormControls.DBColumn DbColumn3;
		internal FSFormControls.DBColumn DbColumn4;
		internal FSFormControls.DBColumn DbColumn5;
		internal FSFormControls.DBColumn DbColumn6;
		internal FSFormControls.DBColumn DbColumn7;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.DbGrid1 = new FSFormControls.DBGrid();
			this.DbColumn1 = new FSFormControls.DBColumn();
			this.DbColumn2 = new FSFormControls.DBColumn();
			this.DbColumn3 = new FSFormControls.DBColumn();
			this.DbColumn4 = new FSFormControls.DBColumn();
			this.DbColumn5 = new FSFormControls.DBColumn();
			this.DbColumn6 = new FSFormControls.DBColumn();
			this.DbColumn7 = new FSFormControls.DBColumn();
			this.DbControl1 = new FSFormControls.DBControl();
			this.SuspendLayout();
			//
			//mnuForm
			//
			this.mnuForm.OwnerDraw = true;
			//
			//DbGrid1
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
			this.DbGrid1.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Bold);
			this.DbGrid1.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.DbGrid1.CaptionText = null;
			this.DbGrid1.CaptionVisible = true;
			this.DbGrid1.ColumnHeadersVisible = true;
			this.DbGrid1.Columns.AddRange(new FSFormControls.DBColumn[] {this.DbColumn1, this.DbColumn2, this.DbColumn3, this.DbColumn4, this.DbColumn5, this.DbColumn6, this.DbColumn7});
			this.DbGrid1.CurrentRowIndex = -1;
			this.DbGrid1.CustomColumnHeaders = false;
			this.DbGrid1.DataControl = this.DbControl1;
			this.DbGrid1.DefaultDecimals = 2;
			this.DbGrid1.DefaultHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.DbGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DbGrid1.Editable = true;
			this.DbGrid1.FlatMode = false;
			this.DbGrid1.GridLineColor = System.Drawing.SystemColors.Control;
			this.DbGrid1.GridLineStyle = System.Windows.Forms.DataGridLineStyle.Solid;
			this.DbGrid1.HeaderBackColor = System.Drawing.SystemColors.Control;
			this.DbGrid1.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.DbGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.DbGrid1.LastCol = -1;
			this.DbGrid1.LastRow = -1;
			this.DbGrid1.Location = new System.Drawing.Point(0, 138);
			this.DbGrid1.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbGrid1.Name = "DbGrid1";
			this.DbGrid1.RecordMode = false;
			this.DbGrid1.RowHeadersVisible = true;
			this.DbGrid1.RowHeight = 16;
			this.DbGrid1.RowSel = -1;
			this.DbGrid1.RowsInCaption = 2;
			this.DbGrid1.ShowRecordScrollBar = true;
			this.DbGrid1.ShowTotals = false;
			this.DbGrid1.Size = new System.Drawing.Size(907, 279);
			this.DbGrid1.TabIndex = 3;
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
			this.DbColumn1.FieldDB = "nombre";
			this.DbColumn1.Font = null;
			this.DbColumn1.FormatString = null;
			this.DbColumn1.HeaderCaption = "Nombre";
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
			this.DbColumn2.FieldDB = "direccion";
			this.DbColumn2.Font = null;
			this.DbColumn2.FormatString = null;
			this.DbColumn2.HeaderCaption = "Dirección";
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
			this.DbColumn3.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
			this.DbColumn3.ComboBlankSelection = true;
			this.DbColumn3.ComboImageList = null;
			this.DbColumn3.ComboListField = "";
			this.DbColumn3.Decimals = 2;
			this.DbColumn3.DefaultValue = "";
			this.DbColumn3.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn3.Encrypted = false;
			this.DbColumn3.Expression = "";
			this.DbColumn3.FieldDB = "codigopostal";
			this.DbColumn3.Font = null;
			this.DbColumn3.FormatString = null;
			this.DbColumn3.HeaderCaption = "C.P.";
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
			this.DbColumn4.FieldDB = "poblacion";
			this.DbColumn4.Font = null;
			this.DbColumn4.FormatString = null;
			this.DbColumn4.HeaderCaption = "Población";
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
			this.DbColumn5.ColumnType = FSFormControls.DBColumn.ColumnTypes.TextColumn;
			this.DbColumn5.ComboBlankSelection = true;
			this.DbColumn5.ComboImageList = null;
			this.DbColumn5.ComboListField = "";
			this.DbColumn5.Decimals = 2;
			this.DbColumn5.DefaultValue = "";
			this.DbColumn5.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn5.Encrypted = false;
			this.DbColumn5.Expression = "";
			this.DbColumn5.FieldDB = "provincia";
			this.DbColumn5.Font = null;
			this.DbColumn5.FormatString = null;
			this.DbColumn5.HeaderCaption = "Provincia";
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
			this.DbColumn6.ColumnType = FSFormControls.DBColumn.ColumnTypes.NumberColumn;
			this.DbColumn6.ComboBlankSelection = true;
			this.DbColumn6.ComboImageList = null;
			this.DbColumn6.ComboListField = "";
			this.DbColumn6.Decimals = 2;
			this.DbColumn6.DefaultValue = "";
			this.DbColumn6.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn6.Encrypted = false;
			this.DbColumn6.Expression = "";
			this.DbColumn6.FieldDB = "irpf";
			this.DbColumn6.Font = null;
			this.DbColumn6.FormatString = null;
			this.DbColumn6.HeaderCaption = "I.R.P.F.";
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
			this.DbColumn7.ColumnType = FSFormControls.DBColumn.ColumnTypes.PictureColumn;
			this.DbColumn7.ComboBlankSelection = true;
			this.DbColumn7.ComboImageList = null;
			this.DbColumn7.ComboListField = "";
			this.DbColumn7.Decimals = 2;
			this.DbColumn7.DefaultValue = "";
			this.DbColumn7.DescriptionType = FSFormControls.DBColumn.DescriptionTypes.TextDescription;
			this.DbColumn7.Encrypted = false;
			this.DbColumn7.Expression = "";
			this.DbColumn7.FieldDB = "logotipo";
			this.DbColumn7.Font = null;
			this.DbColumn7.FormatString = null;
			this.DbColumn7.HeaderCaption = "Logotipo";
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
			this.DbControl1.Location = new System.Drawing.Point(304, 256);
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
			this.DbControl1.Selection = "select * from empresas";
			this.DbControl1.Size = new System.Drawing.Size(120, 80);
			this.DbControl1.TabIndex = 5;
			this.DbControl1.TableName = "empresas";
			this.DbControl1.TabStop = false;
			this.DbControl1.Text = "SQL: select * from empresas";
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
			//frmEmpresas
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(907, 437);
			this.Controls.Add(this.DbControl1);
			this.Controls.Add(this.DbGrid1);
			this.Name = "frmEmpresas";
			this.ShowScrollBar = false;
			this.Text = "Empresas";
			this.Controls.SetChildIndex(this.DbGrid1, 0);
			this.Controls.SetChildIndex(this.DbControl1, 0);
			this.ResumeLayout(false);
			this.PerformLayout();
			
		}
		
#endregion
		
	}
	
}
