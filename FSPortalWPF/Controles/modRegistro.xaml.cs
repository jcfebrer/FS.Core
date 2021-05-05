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
    public partial class modRegistro
    {
        FSPortal.Portal portal = new FSPortal.Portal();
        //@"Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=False;Data Source=k:\wwwroot\data\fsfebrer\portalnet.mdb", "System.Data.OleDb");

        public modRegistro()
        {

            this.InitializeComponent();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                portal.AddUser(txtUsuario.Text, txtEmail.Text, txtNombre.Text, txtApellido1.Text, "", 0, 0, 0, true, true, 0, txtPassword.Password);
                lblMensaje.Content = "Registro realizado correctamente.";
            }
            catch (Exception ex)
            {
                lblMensaje.Content = ex.Message;
            }

        }
    }
}