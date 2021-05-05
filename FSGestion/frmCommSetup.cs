
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System;
using System.Collections;
using System.Windows.Forms;



namespace FSGestion
{
	public class Options : System.Windows.Forms.Form
	{
		
#region  Windows Form Designer generated code
		
		public Options()
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
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.ComboBox ComboBox1;
		internal System.Windows.Forms.ComboBox ComboBox2;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.ComboBox ComboBox3;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.ComboBox ComboBox4;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.ComboBox ComboBox5;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.Button Button1;
		internal System.Windows.Forms.Button Button2;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
            this.Label1 = new System.Windows.Forms.Label();
            this.ComboBox1 = new System.Windows.Forms.ComboBox();
            this.ComboBox2 = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.ComboBox3 = new System.Windows.Forms.ComboBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.ComboBox4 = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.ComboBox5 = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(7, 8);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(56, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "&COM Port:";
            // 
            // ComboBox1
            // 
            this.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox1.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4"});
            this.ComboBox1.Location = new System.Drawing.Point(92, 5);
            this.ComboBox1.Name = "ComboBox1";
            this.ComboBox1.Size = new System.Drawing.Size(74, 21);
            this.ComboBox1.TabIndex = 1;
            // 
            // ComboBox2
            // 
            this.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox2.Items.AddRange(new object[] {
            "110",
            "300",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200",
            "460800",
            "921600"});
            this.ComboBox2.Location = new System.Drawing.Point(92, 40);
            this.ComboBox2.Name = "ComboBox2";
            this.ComboBox2.Size = new System.Drawing.Size(74, 21);
            this.ComboBox2.TabIndex = 3;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(7, 43);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(56, 13);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "&Baud rate:";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Location = new System.Drawing.Point(1, 29);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(266, 8);
            this.GroupBox1.TabIndex = 4;
            this.GroupBox1.TabStop = false;
            // 
            // ComboBox3
            // 
            this.ComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox3.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.ComboBox3.Location = new System.Drawing.Point(92, 91);
            this.ComboBox3.Name = "ComboBox3";
            this.ComboBox3.Size = new System.Drawing.Size(74, 21);
            this.ComboBox3.TabIndex = 8;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(7, 68);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(36, 13);
            this.Label3.TabIndex = 5;
            this.Label3.Text = "&Parity:";
            // 
            // ComboBox4
            // 
            this.ComboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox4.Items.AddRange(new object[] {
            "Even",
            "Odd",
            "None",
            "Mark"});
            this.ComboBox4.Location = new System.Drawing.Point(92, 65);
            this.ComboBox4.Name = "ComboBox4";
            this.ComboBox4.Size = new System.Drawing.Size(74, 21);
            this.ComboBox4.TabIndex = 6;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(7, 94);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(52, 13);
            this.Label4.TabIndex = 7;
            this.Label4.Text = "&Data bits:";
            // 
            // ComboBox5
            // 
            this.ComboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox5.Items.AddRange(new object[] {
            "1",
            "2"});
            this.ComboBox5.Location = new System.Drawing.Point(92, 116);
            this.ComboBox5.Name = "ComboBox5";
            this.ComboBox5.Size = new System.Drawing.Size(74, 21);
            this.ComboBox5.TabIndex = 10;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(7, 119);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(51, 13);
            this.Label5.TabIndex = 9;
            this.Label5.Text = "&Stop bits:";
            // 
            // Button1
            // 
            this.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button1.Location = new System.Drawing.Point(175, 46);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(79, 39);
            this.Button1.TabIndex = 11;
            this.Button1.Text = "&Aceptar";
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Button2
            // 
            this.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button2.Location = new System.Drawing.Point(175, 97);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(79, 39);
            this.Button2.TabIndex = 12;
            this.Button2.Text = "Cancelar";
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Options
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.Button2;
            this.ClientSize = new System.Drawing.Size(259, 144);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.ComboBox5);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.ComboBox4);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.ComboBox3);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.ComboBox2);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.ComboBox1);
            this.Controls.Add(this.Label1);
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuración del puerto";
            this.Load += new System.EventHandler(this.Options_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		
#endregion
		
		public bool bGo;
		
		private void Options_Load(System.Object sender, System.EventArgs e)
		{
			int i = default(int);
			
			ComboBox1.Items.Clear();
			for (i = 0; i <= 3; i++)
			{
				if (Global.mComs[i])
				{
					ComboBox1.Items.Add("COM" + (i + 1).ToString());
				}
			}
			
			ComboBox1.SelectedIndex = 0;
			ComboBox2.SelectedIndex = 5;
			ComboBox3.SelectedIndex = 3;
			ComboBox4.SelectedIndex = 2;
			ComboBox5.SelectedIndex = 0;
		}
		
		private void Button2_Click(System.Object sender, System.EventArgs e)
		{
			bGo = false;
			this.Hide();
		}
		
		private void Button1_Click(System.Object sender, System.EventArgs e)
		{
			if (ComboBox1.SelectedIndex == -1)
			{
				MessageBox.Show("Invalid comm port!", "RS232 tester...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				ComboBox1.Focus();
				return;
			}
			if (ComboBox2.SelectedIndex == -1)
			{
				MessageBox.Show("Invalid baud rate!", "RS232 tester...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				ComboBox2.Focus();
				return;
			}
			if (ComboBox3.SelectedIndex == -1)
			{
                MessageBox.Show("Invalid parity!", "RS232 tester...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				ComboBox3.Focus();
				return;
			}
			if (ComboBox4.SelectedIndex == -1)
			{
                MessageBox.Show("Invalid data bits!", "RS232 tester...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				ComboBox4.Focus();
				return;
			}
			if (ComboBox5.SelectedIndex == -1)
			{
                MessageBox.Show("Invalid stop bits!", "RS232 tester...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ComboBox5.Focus();
				return;
			}
			bGo = true;
			this.Hide();
		}
	}
	
}
