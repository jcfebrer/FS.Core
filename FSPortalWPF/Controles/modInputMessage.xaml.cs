using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FSPortalWPF
{
    public partial class modInputMessage : System.Windows.Window
    {
        public string mensaje
        {
            get { return (string)this.lblMensaje.Content; }
            set { this.lblMensaje.Content = value; }
        }

        public string titulo
        {
            get { return this.Title; }
            set { this.Title = value; }
        }

        public string respuesta
        {
            get { return this.txtRespuesta.Text; }
        }


        public modInputMessage(string mensaje)
        {
            InitializeComponent();
            this.lblMensaje.Content = mensaje;
            
        }

        public modInputMessage(string mensaje, string titulo)
        {
            InitializeComponent();
            this.Title = titulo;
            this.lblMensaje.Content = mensaje;
            
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

    }
}