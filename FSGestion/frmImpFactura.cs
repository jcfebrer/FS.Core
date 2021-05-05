
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;



namespace FSGestion
{
	public class frmImpFactura : System.Windows.Forms.Form
	{
		
#region  Código generado por el Diseñador de Windows Forms
		
		public frmImpFactura()
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
		internal FSFormControls.FSReport DbReport1;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.DbReport1 = new FSFormControls.FSReport();
			this.SuspendLayout();
			//
			//DbReport1
			//
			this.DbReport1.About = null;
			this.DbReport1.Database = null;
			this.DbReport1.DataSet = null;
			this.DbReport1.DisplayToolbar = true;
			this.DbReport1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DbReport1.Location = new System.Drawing.Point(0, 0);
			this.DbReport1.Name = "DbReport1";
			this.DbReport1.Password = null;
			this.DbReport1.ReportFile = null;
			this.DbReport1.Selection = null;
			this.DbReport1.Server = null;
			this.DbReport1.ShowCloseButton = false;
			this.DbReport1.ShowExportButton = true;
			this.DbReport1.ShowGotoPageButton = true;
			this.DbReport1.ShowGroupTreeButton = true;
			this.DbReport1.Size = new System.Drawing.Size(720, 470);
			this.DbReport1.TabIndex = 0;
			this.DbReport1.UserName = null;
			//
			//frmImpFactura
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(720, 470);
			this.Controls.Add(this.DbReport1);
			this.Name = "frmImpFactura";
			this.Text = "Factura";
			this.ResumeLayout(false);
			
		}
		
#endregion
		
	}
	
}
