using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NudeDetect
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void cmdDetect_Click(object sender, EventArgs e)
        {
            NudeDetectLib.Nude nd = new NudeDetectLib.Nude();

            bool result = nd.DetectNude(this.picImage.ImageLocation);

            lblResult.Text = result.ToString();
        }
    }
}
