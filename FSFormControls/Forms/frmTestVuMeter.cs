using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace FSFormControls.Forms
{
    public partial class frmTestVuMeter : Form
    {
        public frmTestVuMeter()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            vuMeter1.Level = trackBar1.Value;
            vuMeter2.Level = trackBar1.Value;
            vuMeter3.Level = trackBar1.Value;
            vuMeter4.Level = trackBar1.Value;
            vuMeter5.Level = trackBar1.Value;
            vuMeter6.Level = trackBar1.Value;
            vuMeter7.Level = trackBar1.Value;
            vuMeter8.Level = trackBar1.Value;
            vuMeter9.Level = trackBar1.Value;
            vuMeter10.Level = trackBar1.Value;
            vuMeter10.VuText = trackBar1.Value.ToString();
            vuMeter11.Level = trackBar1.Value;
            vuMeter12.Level = trackBar1.Value;
            vuMeter13.Level = trackBar1.Value;
        }

    }
}