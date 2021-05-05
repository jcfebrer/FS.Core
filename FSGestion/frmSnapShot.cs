
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;



namespace FSGestion
{
	public partial class frmSnapShot
	{
		public frmSnapShot()
		{
			InitializeComponent();
		}
		
		
		public void Button1_Click(System.Object sender, System.EventArgs e)
		{
			string url = FSFormControls.InputBox.Show("URL:").Text;
			
			WebBrowser1.Navigate(url);
		}
		
		public void Completado(System.Object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
		{
			Bitmap docImg = new Bitmap(this.Width, this.Height);
			Control ctrl = WebBrowser1;
			ctrl.DrawToBitmap(docImg, new Rectangle(WebBrowser1.Location.X, WebBrowser1.Location.Y, WebBrowser1.Width, WebBrowser1.Height));
			
			this.PictureBox1.Image = System.Drawing.Image.FromHbitmap(docImg.GetHbitmap());
		}
	}
}
