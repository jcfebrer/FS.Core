
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;



namespace FSGestion
{
	public class frmDetalleArticulo : FSFormControls.DBForm
	{
		
#region  Código generado por el Diseñador de Windows Forms
		
		public frmDetalleArticulo()
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
		internal System.Windows.Forms.TabControl TabControl1;
		internal System.Windows.Forms.TabPage TabPage1;
		internal FSFormControls.DBRecord DbRecord1;
		internal FSFormControls.DBControl DbControl1;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.TabControl1 = new System.Windows.Forms.TabControl();
			this.TabPage1 = new System.Windows.Forms.TabPage();
			this.DbControl1 = new FSFormControls.DBControl();
			this.DbRecord1 = new FSFormControls.DBRecord();
			this.TabControl1.SuspendLayout();
			this.TabPage1.SuspendLayout();
			this.SuspendLayout();
			//
			//mnuForm
			//
			this.mnuForm.OwnerDraw = true;
			//
			//TabControl1
			//
			this.TabControl1.Controls.Add(this.TabPage1);
			this.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TabControl1.Location = new System.Drawing.Point(0, 138);
			this.TabControl1.Name = "TabControl1";
			this.TabControl1.SelectedIndex = 0;
			this.TabControl1.Size = new System.Drawing.Size(728, 190);
			this.TabControl1.TabIndex = 2;
			//
			//TabPage1
			//
			this.TabPage1.Controls.Add(this.DbControl1);
			this.TabPage1.Controls.Add(this.DbRecord1);
			this.TabPage1.Location = new System.Drawing.Point(4, 22);
			this.TabPage1.Name = "TabPage1";
			this.TabPage1.Size = new System.Drawing.Size(720, 164);
			this.TabPage1.TabIndex = 0;
			this.TabPage1.Text = "Detalles";
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
			this.DbControl1.Location = new System.Drawing.Point(376, 40);
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
			this.DbControl1.Selection = "select * from articulos where codArticulo=?";
			this.DbControl1.Size = new System.Drawing.Size(96, 64);
			this.DbControl1.TabIndex = 1;
			this.DbControl1.TableName = "articulos";
			this.DbControl1.TabStop = false;
			this.DbControl1.Text = "SQL: select * from articulos where codArticulo=?";
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
			this.DbRecord1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.DbRecord1.DataControl = this.DbControl1;
			this.DbRecord1.DateType = FSFormControls.DBRecord.t_date.Normal;
			this.DbRecord1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DbRecord1.DoubleHeightInLargeText = true;
			this.DbRecord1.LabelAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.DbRecord1.LabelYIncrement = 30;
			this.DbRecord1.Location = new System.Drawing.Point(0, 0);
			this.DbRecord1.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
			this.DbRecord1.Name = "DbRecord1";
			this.DbRecord1.PosXLabel = 20;
			this.DbRecord1.PosYLabel = 20;
			this.DbRecord1.ShowAddNew = true;
			this.DbRecord1.ShowCancel = true;
			this.DbRecord1.ShowClose = true;
			this.DbRecord1.ShowComboEdit = true;
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
			this.DbRecord1.Size = new System.Drawing.Size(720, 164);
			this.DbRecord1.TabIndex = 0;
			this.DbRecord1.TextBoxShadow = false;
			this.DbRecord1.Track = false;
			//
			//frmDetalleArticulo
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(728, 348);
			this.Controls.Add(this.TabControl1);
			this.Name = "frmDetalleArticulo";
			this.Text = "Detalle Artículo";
			this.Controls.SetChildIndex(this.TabControl1, 0);
			this.TabControl1.ResumeLayout(false);
			this.TabPage1.ResumeLayout(false);
			this.ResumeLayout(false);
			
		}
		
#endregion
		
	}
	
}
