
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;



namespace FSGestion
{
	public class frmViewForm : System.Windows.Forms.Form
	{
		
		private static frmViewForm mInstance = null;
		
#region  Windows Form Designer generated code
		
		public frmViewForm()
		{
			
			//This call is required by the Windows Form Designer.
			InitializeComponent();
			
			//Add any initialization after the InitializeComponent() call
			
		}
		
		//Form overrides dispose to clean up the component list.
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
		
		//Required by the Windows Form Designer
		private System.ComponentModel.Container components = null;
		
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		internal System.Windows.Forms.TextBox txtText;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.txtText = new System.Windows.Forms.TextBox();
			base.Load += new System.EventHandler(ViewForm_Load);
			this.SuspendLayout();
			//
			//txtText
			//
			this.txtText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtText.Multiline = true;
			this.txtText.Name = "txtText";
			this.txtText.ReadOnly = true;
			this.txtText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtText.Size = new System.Drawing.Size(292, 273);
			this.txtText.TabIndex = 0;
			this.txtText.Text = "";
			//
			//ViewForm
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {this.txtText});
			this.Name = "ViewForm";
			this.Text = "ViewForm";
			this.ResumeLayout(false);
			
		}
		
#endregion
		
		private void ViewForm_Load(System.Object sender, System.EventArgs e)
		{
			
		}
		
		public static void ShowText(string text)
		{
			if (mInstance == null)
			{
				mInstance = new frmViewForm();
			}
			mInstance.Show();
			mInstance.Focus();
			mInstance.txtText.Text = text;
		}
		
		protected override void OnClosed(System.EventArgs e)
		{
			mInstance = null;
		}
	}
	
	
	
}
