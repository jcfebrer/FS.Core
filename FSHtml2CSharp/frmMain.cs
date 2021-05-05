// VBConversions Note: VB project level imports
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System;
// End of VB project level imports

using Html2CSharp;

namespace Html2CSharp
{
	public partial class frmMain
	{
		public frmMain()
		{
			InitializeComponent();
		}
		
		
		public void Button2_Click(System.Object sender, System.EventArgs e)
		{
			
			txtData.Text = "";
			
		}
		
		public void Button3_Click(System.Object sender, System.EventArgs e)
		{
			txtResult.Text = "";
		}
		
		public short KeyAscii(KeyPressEventArgs UserKeyArgument)
		{
			short returnValue = 0;
			returnValue = Convert.ToInt16(FSLibrary.TextUtil.Ascii(UserKeyArgument.KeyChar.ToString()));
			return returnValue;
		}
		
		public void TextBox1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			
			if (e.KeyChar == char.Parse(FSLibrary.TextUtil.ControlChars.Cr.ToString()))
			{
				txtData.Text = txtData.Text + FSLibrary.TextUtil.ControlChars.Cr;
				txtData.SelectionStart = txtData.Text.Length;
			}
			
			
		}
		
		public void Form1_Load(object sender, System.EventArgs e)
		{
			this.Text = this.Text + " " + Application.ProductVersion;
		}
		
		public void Button4_Click(System.Object sender, System.EventArgs e)
		{
            ConvertCS();
		}

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            ofDialog.Filter = "Ficheros Html/Aspx|*.aspx;*.ascx;*.html;*.htm";
            if (ofDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new
         System.IO.StreamReader(ofDialog.FileName);
                txtData.Text = sr.ReadToEnd();
                sr.Close();

                ConvertCS();
            }
        }

        private void ConvertCS()
        {
            if (txtData.Text != "")
            {
                txtResult.Text = StartupModule.ConvertToCS(txtData.Text);
            }
        }
    }
	
}
