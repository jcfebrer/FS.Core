
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;


//using System.Diagnostics.Process;


namespace FSGestion
{
	public class frmSpash : System.Windows.Forms.Form
	{
		
#region  Código generado por el Diseñador de Windows Forms
		
		public frmSpash()
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
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.LinkLabel LinkLabel1;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.Timer Timer1;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.PictureBox PictureBox2;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmSpash));
			this.Label2 = new System.Windows.Forms.Label();
			this.LinkLabel1 = new System.Windows.Forms.LinkLabel();
			this.LinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
			this.Label3 = new System.Windows.Forms.Label();
			this.Timer1 = new System.Windows.Forms.Timer(this.components);
			this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
			this.Label1 = new System.Windows.Forms.Label();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label5 = new System.Windows.Forms.Label();
			this.PictureBox2 = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			//
			//Label2
			//
			this.Label2.Anchor = (System.Windows.Forms.AnchorStyles) (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.Label2.AutoSize = true;
			this.Label2.BackColor = System.Drawing.Color.White;
			this.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Label2.Location = new System.Drawing.Point(128, 160);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(131, 16);
			this.Label2.TabIndex = 11;
			this.Label2.Text = "info@febrersoftware.com";
			//
			//LinkLabel1
			//
			this.LinkLabel1.Anchor = (System.Windows.Forms.AnchorStyles) (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.LinkLabel1.AutoSize = true;
			this.LinkLabel1.BackColor = System.Drawing.Color.White;
			this.LinkLabel1.Location = new System.Drawing.Point(112, 144);
			this.LinkLabel1.Name = "LinkLabel1";
			this.LinkLabel1.Size = new System.Drawing.Size(157, 16);
			this.LinkLabel1.TabIndex = 9;
			this.LinkLabel1.TabStop = true;
			this.LinkLabel1.Text = "http://www.febrersoftware.com";
			//
			//Label3
			//
			this.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Label3.Location = new System.Drawing.Point(0, 0);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(368, 238);
			this.Label3.TabIndex = 13;
			//
			//Timer1
			//
			this.Timer1.Enabled = true;
			this.Timer1.Interval = 5000;
			//
			//Label1
			//
			this.Label1.AutoSize = true;
			this.Label1.Location = new System.Drawing.Point(128, 192);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(115, 16);
			this.Label1.TabIndex = 14;
			this.Label1.Text = "Bº Marusas Nº5 Lonja";
			//
			//Label4
			//
			this.Label4.AutoSize = true;
			this.Label4.Location = new System.Drawing.Point(120, 208);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(131, 16);
			this.Label4.TabIndex = 15;
			this.Label4.Text = "48610 - Urduliz (Vizcaya)";
			//
			//Label5
			//
			this.Label5.AutoSize = true;
			this.Label5.Location = new System.Drawing.Point(136, 176);
			this.Label5.Name = "Label5";
			this.Label5.Size = new System.Drawing.Size(97, 16);
			this.Label5.TabIndex = 16;
			this.Label5.Text = "D.N.I.: 16055459X";
			//
			//PictureBox2
			//
			this.PictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PictureBox2.Enabled = false;
			this.PictureBox2.ForeColor = System.Drawing.Color.Black;
			this.PictureBox2.Image = (System.Drawing.Image) (resources.GetObject("PictureBox2.Image"));
			this.PictureBox2.Location = new System.Drawing.Point(72, 56);
			this.PictureBox2.Name = "PictureBox2";
			this.PictureBox2.Size = new System.Drawing.Size(240, 63);
			this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.PictureBox2.TabIndex = 17;
			this.PictureBox2.TabStop = false;
			//
			//frmSpash
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(368, 238);
			this.ControlBox = false;
			this.Controls.Add(this.PictureBox2);
			this.Controls.Add(this.Label5);
			this.Controls.Add(this.Label4);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.Label2);
			this.Controls.Add(this.LinkLabel1);
			this.Controls.Add(this.Label3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "frmSpash";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Febrer Software";
			this.ResumeLayout(false);
			
		}
		
#endregion
		
		
		private void LinkLabel1_LinkClicked(System.Object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.febrersoftware.com");
		}
		
		private void Timer1_Tick(System.Object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
	
}
