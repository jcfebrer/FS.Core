using FSGoogleFirebase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSGestion
{
    public partial class frmGoogleFirebase : Form
    {
        public frmGoogleFirebase()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            FSGoogleFirebase.Database firebase = new FSGoogleFirebase.Database("https://truckcontrol-afda7.firebaseio.com", "YcW8IKesXDDPi4gQn6GSNzAKasy2B2nA1bvpiTTD");
            Register register = firebase.Get("Usuarios");

            //FSGoogleFirebase.Push push = new Push("AIzaSyAyEXEfYrstjBYtMDb9SHwhYRLOYm-PoXk", "67558770893");
            //push.SendMenssage("codigoRegistroMovil", "mensaje");
        }
    }
}
