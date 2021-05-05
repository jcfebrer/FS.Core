using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace FSPortalWPF
{
	public partial class modAcceso
	{
        FSPortal.Portal portal = new FSPortal.Portal();
        //@"Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=False;Data Source=k:\wwwroot\data\fsfebrer\portalnet.mdb", "System.Data.OleDb");

		public modAcceso()
		{
			InitializeComponent();
		}

        private void OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                portal.Login(txtUsuario.Text, txtPassword.Password);

                string s = "Validación correcta.";
                lblMensaje.Content = s;
                lblMensaje.ToolTip = s;
            }
            catch (System.Exception ex)
            {
                lblMensaje.Content = ex.Message;
            }
        }
	}
}