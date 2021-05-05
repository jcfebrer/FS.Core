
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System;
using System.Collections;
using System.Windows.Forms;



namespace FSGestion
{
	public partial class frmDemoWebBrowser : FSFormControls.DBForm
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
			this.DbWebBrowser1 = new FSFormControls.FSWebBrowser();
			this.DbButton1 = new FSFormControls.DBButton();
			this.DbButton1.Click += new System.EventHandler(this.DbButton1_Click);
			this.DbButton2 = new FSFormControls.DBButton();
			this.DbButton2.Click += new System.EventHandler(this.DbButton2_Click);
			this.DbButton3 = new FSFormControls.DBButton();
			this.DbButton3.Click += new System.EventHandler(this.DbButton3_Click);
			this.DbButton4 = new FSFormControls.DBButton();
			this.DbButton4.Click += new System.EventHandler(this.DbButton4_Click);
			this.DbButton5 = new FSFormControls.DBButton();
			this.DbButton5.Click += new System.EventHandler(this.DbButton5_Click);
			this.DbButton6 = new FSFormControls.DBButton();
			this.DbButton6.Click += new System.EventHandler(this.DbButton6_Click);
			this.DbButton7 = new FSFormControls.DBButton();
			this.SuspendLayout();
			//
			//DbWebBrowser1
			//
			this.DbWebBrowser1.About = null;
			this.DbWebBrowser1.Location = new System.Drawing.Point(273, 134);
			this.DbWebBrowser1.Name = "DbWebBrowser1";
			this.DbWebBrowser1.Size = new System.Drawing.Size(411, 267);
			this.DbWebBrowser1.TabIndex = 8;
			this.DbWebBrowser1.Track = false;
			//
			//DbButton1
			//
			this.DbButton1.About = null;
			this.DbButton1.ButtonStyle = FSFormControls.DBButton.ButtonStyleType.Normal;
			this.DbButton1.DropDownMenu = null;
			this.DbButton1.FillColorEnd = System.Drawing.Color.White;
			this.DbButton1.FillColorStart = System.Drawing.Color.LightGray;
			this.DbButton1.FillHoverColorEnd = System.Drawing.Color.Beige;
			this.DbButton1.FillHoverColorStart = System.Drawing.Color.Beige;
			this.DbButton1.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.DbButton1.Gradient = false;
			this.DbButton1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			this.DbButton1.Image = null;
			this.DbButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.DbButton1.Location = new System.Drawing.Point(23, 149);
			this.DbButton1.Name = "DbButton1";
			this.DbButton1.Size = new System.Drawing.Size(235, 19);
			this.DbButton1.TabIndex = 9;
			this.DbButton1.Text = "Ir a página";
			this.DbButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.DbButton1.TextColorEnd = System.Drawing.Color.Black;
			this.DbButton1.TextColorStart = System.Drawing.Color.Blue;
			this.DbButton1.TextFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.DbButton1.ToolTip = "";
			this.DbButton1.Track = false;
			//
			//DbButton2
			//
			this.DbButton2.About = null;
			this.DbButton2.ButtonStyle = FSFormControls.DBButton.ButtonStyleType.Normal;
			this.DbButton2.DropDownMenu = null;
			this.DbButton2.FillColorEnd = System.Drawing.Color.White;
			this.DbButton2.FillColorStart = System.Drawing.Color.LightGray;
			this.DbButton2.FillHoverColorEnd = System.Drawing.Color.Beige;
			this.DbButton2.FillHoverColorStart = System.Drawing.Color.Beige;
			this.DbButton2.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.DbButton2.Gradient = false;
			this.DbButton2.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			this.DbButton2.Image = null;
			this.DbButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.DbButton2.Location = new System.Drawing.Point(23, 174);
			this.DbButton2.Name = "DbButton2";
			this.DbButton2.Size = new System.Drawing.Size(235, 19);
			this.DbButton2.TabIndex = 10;
			this.DbButton2.Text = "Guardar como imagen";
			this.DbButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.DbButton2.TextColorEnd = System.Drawing.Color.Black;
			this.DbButton2.TextColorStart = System.Drawing.Color.Blue;
			this.DbButton2.TextFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.DbButton2.ToolTip = "";
			this.DbButton2.Track = false;
			//
			//DbButton3
			//
			this.DbButton3.About = null;
			this.DbButton3.ButtonStyle = FSFormControls.DBButton.ButtonStyleType.Normal;
			this.DbButton3.DropDownMenu = null;
			this.DbButton3.FillColorEnd = System.Drawing.Color.White;
			this.DbButton3.FillColorStart = System.Drawing.Color.LightGray;
			this.DbButton3.FillHoverColorEnd = System.Drawing.Color.Beige;
			this.DbButton3.FillHoverColorStart = System.Drawing.Color.Beige;
			this.DbButton3.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.DbButton3.Gradient = false;
			this.DbButton3.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			this.DbButton3.Image = null;
			this.DbButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.DbButton3.Location = new System.Drawing.Point(23, 199);
			this.DbButton3.Name = "DbButton3";
			this.DbButton3.Size = new System.Drawing.Size(235, 19);
			this.DbButton3.TabIndex = 11;
			this.DbButton3.Text = "Pulsar un boton";
			this.DbButton3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.DbButton3.TextColorEnd = System.Drawing.Color.Black;
			this.DbButton3.TextColorStart = System.Drawing.Color.Blue;
			this.DbButton3.TextFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.DbButton3.ToolTip = "";
			this.DbButton3.Track = false;
			//
			//DbButton4
			//
			this.DbButton4.About = null;
			this.DbButton4.ButtonStyle = FSFormControls.DBButton.ButtonStyleType.Normal;
			this.DbButton4.DropDownMenu = null;
			this.DbButton4.FillColorEnd = System.Drawing.Color.White;
			this.DbButton4.FillColorStart = System.Drawing.Color.LightGray;
			this.DbButton4.FillHoverColorEnd = System.Drawing.Color.Beige;
			this.DbButton4.FillHoverColorStart = System.Drawing.Color.Beige;
			this.DbButton4.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.DbButton4.Gradient = false;
			this.DbButton4.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			this.DbButton4.Image = null;
			this.DbButton4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.DbButton4.Location = new System.Drawing.Point(23, 224);
			this.DbButton4.Name = "DbButton4";
			this.DbButton4.Size = new System.Drawing.Size(235, 19);
			this.DbButton4.TabIndex = 12;
			this.DbButton4.Text = "Rellenar un textbox";
			this.DbButton4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.DbButton4.TextColorEnd = System.Drawing.Color.Black;
			this.DbButton4.TextColorStart = System.Drawing.Color.Blue;
			this.DbButton4.TextFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.DbButton4.ToolTip = "";
			this.DbButton4.Track = false;
			//
			//DbButton5
			//
			this.DbButton5.About = null;
			this.DbButton5.ButtonStyle = FSFormControls.DBButton.ButtonStyleType.Normal;
			this.DbButton5.DropDownMenu = null;
			this.DbButton5.FillColorEnd = System.Drawing.Color.White;
			this.DbButton5.FillColorStart = System.Drawing.Color.LightGray;
			this.DbButton5.FillHoverColorEnd = System.Drawing.Color.Beige;
			this.DbButton5.FillHoverColorStart = System.Drawing.Color.Beige;
			this.DbButton5.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.DbButton5.Gradient = false;
			this.DbButton5.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			this.DbButton5.Image = null;
			this.DbButton5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.DbButton5.Location = new System.Drawing.Point(23, 249);
			this.DbButton5.Name = "DbButton5";
			this.DbButton5.Size = new System.Drawing.Size(235, 19);
			this.DbButton5.TabIndex = 13;
			this.DbButton5.Text = "Pulsar un Link";
			this.DbButton5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.DbButton5.TextColorEnd = System.Drawing.Color.Black;
			this.DbButton5.TextColorStart = System.Drawing.Color.Blue;
			this.DbButton5.TextFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.DbButton5.ToolTip = "";
			this.DbButton5.Track = false;
			//
			//DbButton6
			//
			this.DbButton6.About = null;
			this.DbButton6.ButtonStyle = FSFormControls.DBButton.ButtonStyleType.Normal;
			this.DbButton6.DropDownMenu = null;
			this.DbButton6.FillColorEnd = System.Drawing.Color.White;
			this.DbButton6.FillColorStart = System.Drawing.Color.LightGray;
			this.DbButton6.FillHoverColorEnd = System.Drawing.Color.Beige;
			this.DbButton6.FillHoverColorStart = System.Drawing.Color.Beige;
			this.DbButton6.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.DbButton6.Gradient = false;
			this.DbButton6.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			this.DbButton6.Image = null;
			this.DbButton6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.DbButton6.Location = new System.Drawing.Point(23, 274);
			this.DbButton6.Name = "DbButton6";
			this.DbButton6.Size = new System.Drawing.Size(235, 19);
			this.DbButton6.TabIndex = 14;
			this.DbButton6.Text = "Seleccionar un radio button";
			this.DbButton6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.DbButton6.TextColorEnd = System.Drawing.Color.Black;
			this.DbButton6.TextColorStart = System.Drawing.Color.Blue;
			this.DbButton6.TextFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.DbButton6.ToolTip = "";
			this.DbButton6.Track = false;
			//
			//DbButton7
			//
			this.DbButton7.About = null;
			this.DbButton7.ButtonStyle = FSFormControls.DBButton.ButtonStyleType.Normal;
			this.DbButton7.DropDownMenu = null;
			this.DbButton7.FillColorEnd = System.Drawing.Color.White;
			this.DbButton7.FillColorStart = System.Drawing.Color.LightGray;
			this.DbButton7.FillHoverColorEnd = System.Drawing.Color.Beige;
			this.DbButton7.FillHoverColorStart = System.Drawing.Color.Beige;
			this.DbButton7.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.DbButton7.Gradient = false;
			this.DbButton7.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			this.DbButton7.Image = null;
			this.DbButton7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.DbButton7.Location = new System.Drawing.Point(23, 299);
			this.DbButton7.Name = "DbButton7";
			this.DbButton7.Size = new System.Drawing.Size(235, 19);
			this.DbButton7.TabIndex = 15;
			this.DbButton7.Text = "Seleccionar un valor en un listbox";
			this.DbButton7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.DbButton7.TextColorEnd = System.Drawing.Color.Black;
			this.DbButton7.TextColorStart = System.Drawing.Color.Blue;
			this.DbButton7.TextFont = new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.DbButton7.ToolTip = "";
			this.DbButton7.Track = false;
			//
			//frmDemoWebBrowser
			//
			this.ClientSize = new System.Drawing.Size(718, 436);
			this.Controls.Add(this.DbButton7);
			this.Controls.Add(this.DbButton6);
			this.Controls.Add(this.DbButton5);
			this.Controls.Add(this.DbButton4);
			this.Controls.Add(this.DbButton3);
			this.Controls.Add(this.DbButton2);
			this.Controls.Add(this.DbButton1);
			this.Controls.Add(this.DbWebBrowser1);
			this.Name = "frmDemoWebBrowser";
			this.Controls.SetChildIndex(this.DbWebBrowser1, 0);
			this.Controls.SetChildIndex(this.DbButton1, 0);
			this.Controls.SetChildIndex(this.DbButton2, 0);
			this.Controls.SetChildIndex(this.DbButton3, 0);
			this.Controls.SetChildIndex(this.DbButton4, 0);
			this.Controls.SetChildIndex(this.DbButton5, 0);
			this.Controls.SetChildIndex(this.DbButton6, 0);
			this.Controls.SetChildIndex(this.DbButton7, 0);
			this.ResumeLayout(false);
			
		}
		internal FSFormControls.FSWebBrowser DbWebBrowser1;
		internal FSFormControls.DBButton DbButton1;
		internal FSFormControls.DBButton DbButton2;
		internal FSFormControls.DBButton DbButton3;
		internal FSFormControls.DBButton DbButton4;
		internal FSFormControls.DBButton DbButton5;
		internal FSFormControls.DBButton DbButton6;
		internal FSFormControls.DBButton DbButton7;
		
	}
	
}
