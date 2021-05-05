
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System;
using System.Collections;
using System.Windows.Forms;



namespace FSGestion
{
	public partial class frmAsistente : FSFormControls.frmWizard
	{
		
		//Form overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
		
		//Required by the Windows Form Designer
		private System.ComponentModel.Container components = null;
		
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAsistente));
			this.TabPage1 = new System.Windows.Forms.TabPage();
			base.ValidatePage += new FSFormControls.frmWizard.ValidatePageEventHandler(frmAsistente_ValidatePage);
			this.Label4 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.PictureBox2 = new System.Windows.Forms.PictureBox();
			this.Button1 = new System.Windows.Forms.Button();
			this.Label1 = new System.Windows.Forms.Label();
			this.TabPage2 = new System.Windows.Forms.TabPage();
			this.TreeView1 = new System.Windows.Forms.TreeView();
			this.TabPage3 = new System.Windows.Forms.TabPage();
			this.TabPage4 = new System.Windows.Forms.TabPage();
			this.TabPage5 = new System.Windows.Forms.TabPage();
			this.panelStep.SuspendLayout();
			this.TabControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.picWizard).BeginInit();
			this.wizardTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.PictureBox1).BeginInit();
			this.TabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.PictureBox2).BeginInit();
			this.TabPage2.SuspendLayout();
			this.SuspendLayout();
			//
			//panelStep
			//
			this.panelStep.Location = new System.Drawing.Point(164, 92);
			this.panelStep.Size = new System.Drawing.Size(589, 225);
			this.panelStep.TabIndex = 13;
			//
			//TabControl1
			//
			this.TabControl1.Controls.Add(this.TabPage1);
			this.TabControl1.Controls.Add(this.TabPage2);
			this.TabControl1.Controls.Add(this.TabPage3);
			this.TabControl1.Controls.Add(this.TabPage4);
			this.TabControl1.Controls.Add(this.TabPage5);
			this.TabControl1.Size = new System.Drawing.Size(573, 209);
			this.TabControl1.TabIndex = 14;
			//
			//picWizard
			//
			this.picWizard.Size = new System.Drawing.Size(164, 225);
			this.picWizard.TabIndex = 12;
			//
			//wizardTop
			//
			this.wizardTop.Location = new System.Drawing.Point(0, 28);
			this.wizardTop.Size = new System.Drawing.Size(753, 64);
			this.wizardTop.TabIndex = 7;
			//
			//PictureBox1
			//
			this.PictureBox1.Location = new System.Drawing.Point(1815, 0);
			this.PictureBox1.TabIndex = 8;
			//
			//LinkLabel1
			//
			this.LinkLabel1.TabIndex = 20;
			this.LinkLabel1.TabStop = true;
			this.LinkLabel1.Text = "Febrer Software 2010(c)";
			//
			//TabPage1
			//
			this.TabPage1.Controls.Add(this.Label4);
			this.TabPage1.Controls.Add(this.Label3);
			this.TabPage1.Controls.Add(this.Label2);
			this.TabPage1.Controls.Add(this.PictureBox2);
			this.TabPage1.Controls.Add(this.Button1);
			this.TabPage1.Controls.Add(this.Label1);
			this.TabPage1.Location = new System.Drawing.Point(4, 22);
			this.TabPage1.Name = "TabPage1";
			this.TabPage1.Size = new System.Drawing.Size(565, 183);
			this.TabPage1.TabIndex = 0;
			this.TabPage1.Tag = "contenido del tag";
			this.TabPage1.Text = "TabPage1";
			this.TabPage1.UseVisualStyleBackColor = true;
			//
			//Label4
			//
			this.Label4.AutoSize = true;
			this.Label4.Location = new System.Drawing.Point(46, 8);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(385, 13);
			this.Label4.TabIndex = 5;
			this.Label4.Text = "Bienvenido al asistente de configuración del sistema de vigilancia \'SecurInstant\'" + ".";
			//
			//Label3
			//
			this.Label3.Location = new System.Drawing.Point(185, 133);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(317, 40);
			this.Label3.TabIndex = 4;
			this.Label3.Text = "Debe conectar la cámara a la red electrica para que esta pueda ser detectada corr" + "ectamente. Cuando estre preparado, pulse en \'Detectar Cámara\'.";
			//
			//Label2
			//
			this.Label2.Location = new System.Drawing.Point(185, 87);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(317, 36);
			this.Label2.TabIndex = 3;
			this.Label2.Text = "Encontrará un cable como el de la imagen, que debe ser conectado entre la cámara " + "y el PC o portatíl.";
			//
			//PictureBox2
			//
			this.PictureBox2.Image = (System.Drawing.Image) (resources.GetObject("PictureBox2.Image"));
			this.PictureBox2.Location = new System.Drawing.Point(22, 87);
			this.PictureBox2.Name = "PictureBox2";
			this.PictureBox2.Size = new System.Drawing.Size(130, 98);
			this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.PictureBox2.TabIndex = 2;
			this.PictureBox2.TabStop = false;
			//
			//Button1
			//
			this.Button1.Location = new System.Drawing.Point(286, 191);
			this.Button1.Name = "Button1";
			this.Button1.Size = new System.Drawing.Size(160, 27);
			this.Button1.TabIndex = 1;
			this.Button1.Text = "Detectar Cámara";
			this.Button1.UseVisualStyleBackColor = true;
			//
			//Label1
			//
			this.Label1.Location = new System.Drawing.Point(46, 39);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(456, 34);
			this.Label1.TabIndex = 0;
			this.Label1.Text = "Para continuar, debe conectar la cámara directamente a su PC. Una vez este listo," + " pulse en \'Detectar Cámara\'.";
			//
			//TabPage2
			//
			this.TabPage2.Controls.Add(this.TreeView1);
			this.TabPage2.Location = new System.Drawing.Point(4, 22);
			this.TabPage2.Name = "TabPage2";
			this.TabPage2.Size = new System.Drawing.Size(504, 0);
			this.TabPage2.TabIndex = 1;
			this.TabPage2.Text = "TabPage2";
			this.TabPage2.UseVisualStyleBackColor = true;
			//
			//TreeView1
			//
			this.TreeView1.Location = new System.Drawing.Point(27, 28);
			this.TreeView1.Name = "TreeView1";
			this.TreeView1.Size = new System.Drawing.Size(411, 188);
			this.TreeView1.TabIndex = 0;
			//
			//TabPage3
			//
			this.TabPage3.Location = new System.Drawing.Point(4, 22);
			this.TabPage3.Name = "TabPage3";
			this.TabPage3.Size = new System.Drawing.Size(504, 0);
			this.TabPage3.TabIndex = 2;
			this.TabPage3.Text = "TabPage3";
			this.TabPage3.UseVisualStyleBackColor = true;
			//
			//TabPage4
			//
			this.TabPage4.Location = new System.Drawing.Point(4, 22);
			this.TabPage4.Name = "TabPage4";
			this.TabPage4.Size = new System.Drawing.Size(504, 0);
			this.TabPage4.TabIndex = 3;
			this.TabPage4.Text = "TabPage4";
			this.TabPage4.UseVisualStyleBackColor = true;
			//
			//TabPage5
			//
			this.TabPage5.Location = new System.Drawing.Point(4, 22);
			this.TabPage5.Name = "TabPage5";
			this.TabPage5.Size = new System.Drawing.Size(504, 0);
			this.TabPage5.TabIndex = 4;
			this.TabPage5.Text = "TabPage5";
			this.TabPage5.UseVisualStyleBackColor = true;
			//
			//frmAsistente
			//
			this.ClientSize = new System.Drawing.Size(753, 377);
			this.Name = "frmAsistente";
            this.ToolbarType = FSFormControls.DBToolBarEx.tToolbar.ToolbarOffice;
			this.panelStep.ResumeLayout(false);
			this.TabControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) this.picWizard).EndInit();
			this.wizardTop.ResumeLayout(false);
			this.wizardTop.PerformLayout();
			((System.ComponentModel.ISupportInitialize) this.PictureBox1).EndInit();
			this.TabPage1.ResumeLayout(false);
			this.TabPage1.PerformLayout();
			((System.ComponentModel.ISupportInitialize) this.PictureBox2).EndInit();
			this.TabPage2.ResumeLayout(false);
			this.ResumeLayout(false);
			
		}
		internal System.Windows.Forms.TabPage TabPage1;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Button Button1;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.PictureBox PictureBox2;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.TabPage TabPage2;
		internal System.Windows.Forms.TabPage TabPage3;
		internal System.Windows.Forms.TabPage TabPage4;
		internal System.Windows.Forms.TabPage TabPage5;
		internal System.Windows.Forms.TreeView TreeView1;
		
	}
	
}
