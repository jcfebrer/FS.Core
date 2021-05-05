
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System;
using System.Collections;
using System.Windows.Forms;



namespace FSGestion
{
	public partial class frmDemoWebBrowser
	{
		public frmDemoWebBrowser()
		{
			InitializeComponent();
		}
		
		public void DbButton1_Click(System.Object sender, System.EventArgs e)
		{
			this.DbWebBrowser1.Navigate("http://www.febrersoftware.com");
			MessageBox.Show("Hecho!!");
		}
		
		public void DbButton2_Click(System.Object sender, System.EventArgs e)
		{
			this.DbWebBrowser1.ScreenShot("c:\\", "prueba.jpg");
			MessageBox.Show("Hecho!!");
		}
		
		public void DbButton3_Click(System.Object sender, System.EventArgs e)
		{
			HtmlElement buttonToClick = this.DbWebBrowser1.FindControlByName("cmdSend");
			
			this.DbWebBrowser1.ClickButton(buttonToClick);
			MessageBox.Show("Hecho!!");
		}
		
		public void DbButton4_Click(System.Object sender, System.EventArgs e)
		{
			HtmlElement textb = this.DbWebBrowser1.FindControlByName("txtUsuario");
			
			this.DbWebBrowser1.FillTextBox(textb, "esto es una prueba");
			MessageBox.Show("Hecho!!");
		}
		
		public void DbButton5_Click(System.Object sender, System.EventArgs e)
		{
			HtmlElement link = this.DbWebBrowser1.FindControlByTag("A");
			
			this.DbWebBrowser1.ClickLink(link);
			MessageBox.Show("Hecho!!");
		}
		
		public void DbButton6_Click(System.Object sender, System.EventArgs e)
		{
			HtmlElement radio = this.DbWebBrowser1.FindControlByName("radio");
			
			this.DbWebBrowser1.SelectRadioButton(radio);
			MessageBox.Show("Hecho!!");
		}
	}
	
}
