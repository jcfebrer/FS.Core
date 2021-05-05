/*
 * Created by SharpDevelop.
 * User: jcfeb
 * Date: 04/07/2016
 * Time: 21:46
*/
using FSFile;
using FSLibrary;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FileNormalization
{
	/// <summary>
	/// Description of frmFilmDetail.
	/// </summary>
	public partial class frmFileDetail : Form
	{
		File _pel;
		
		public frmFileDetail(File pel)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			_pel = pel;
			
			txtNombre.Text = pel.Nombre;
			txtNombreNormalizado.Text = pel.NombreNormalizado;
			txtLabel.Text = pel.Label;
			txtColor.BackColor = pel.ColorFondo;
			
			_pel = pel;
		}
		
		void CmdColorClick(object sender, EventArgs e)
		{
			colorDialog1.ShowDialog();
			txtColor.BackColor = colorDialog1.Color;
		}
		void CmdAceptarClick(object sender, EventArgs e)
		{
			_pel.Nombre = txtNombre.Text;
			_pel.NombreNormalizado = txtNombreNormalizado.Text;
			_pel.Label = txtLabel.Text;
			_pel.ColorFondo = txtColor.BackColor;
			
			Close();
		}
	}
}
