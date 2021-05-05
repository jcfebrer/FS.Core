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
    public partial class frmGoogleMaps : Form
    {
        public frmGoogleMaps()
        {
            InitializeComponent();
        }

        private void btnDistancia_Click(object sender, EventArgs e)
        {
            txtKMS.Text = FSGoogleMaps.Library.GetDistance(txtOrigen.Text, txtDestino.Text).ToString();
        }
    }
}
