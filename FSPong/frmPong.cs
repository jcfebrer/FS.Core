using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSPong
{
    public partial class frmPong : Form
    {
        //public int paddleLeft = 512;
        //public int paddleRight = 512;
        int paddleMove = 24;
        int maxHeightPong = 1024;

        public frmPong()
        {
            this.KeyPreview = true;

            InitializeComponent();

            this.KeyDown += new KeyEventHandler(pong_KeyDown);
            panelPong.MouseMove += new MouseEventHandler(pong_MouseMove);
            panelPong.MouseLeave += PanelPong_MouseLeave;
            panelPong.MouseEnter += PanelPong_MouseEnter;

            Start();
        }

        private void PanelPong_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Hide();
        }

        private void PanelPong_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
        }

        private void pong_MouseMove(object sender, MouseEventArgs e)
        {

            int pos = (e.Location.Y * maxHeightPong) / panelPong.Height;

            //if (e.X > panelPong.pong.fieldWidth)
            panelPong.pong.paddleLeft = pos;
            //else
            panelPong.pong.paddleRight = pos;

            //if (e.Y < 240 && e.Y > 0)
            //    paddleLeft = e.Y;

            //if (e.Delta < 240 && e.Delta > 0)
            //    paddleRight = e.Delta;

            //paddleRight = e.Delta;
        }

        private void pong_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Q:
                    if (panelPong.pong.paddleLeft > 0) panelPong.pong.paddleLeft -= paddleMove;
                    break;
                case Keys.A:
                    if (panelPong.pong.paddleLeft < maxHeightPong) panelPong.pong.paddleLeft += paddleMove;
                    break;
                case Keys.P:
                    if (panelPong.pong.paddleRight > 0) panelPong.pong.paddleRight -= paddleMove;
                    break;
                case Keys.L:
                    if (panelPong.pong.paddleRight < maxHeightPong) panelPong.pong.paddleRight += paddleMove;
                    break;
            }
            e.SuppressKeyPress = true;
        }


        private void cmdReset_Click(object sender, EventArgs e)
        {
            panelPong.reset();
        }

        private void cmdPlay_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void Start()
        {
            panelPong.ScaleValue = trackBar1.Value;
            panelPong.Start();
        }


        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            panelPong.ScaleValue = trackBar1.Value;
        }

        private void optTennis_CheckedChanged(object sender, EventArgs e)
        {
            if (optTennis.Checked)
                panelPong.pong.gameNumber = 1;
        }

        private void optFootball_CheckedChanged(object sender, EventArgs e)
        {
            if (optFootball.Checked)
                panelPong.pong.gameNumber = 2;
        }

        private void optSquash_CheckedChanged(object sender, EventArgs e)
        {
            if (optSquash.Checked)
                panelPong.pong.gameNumber = 3;
        }

        private void optSolo_CheckedChanged(object sender, EventArgs e)
        {
            if (optSolo.Checked)
                panelPong.pong.gameNumber = 4;
        }


        private void chkSize_CheckedChanged(object sender, EventArgs e)
        {
            if(chkSize.Checked)
                panelPong.pong.paddleHeight = 7;
            else
                panelPong.pong.paddleHeight = 14;
        }

        private void chkSpeed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSpeed.Checked)
                panelPong.pong.doubleSpeed = true;
            else
                panelPong.pong.doubleSpeed = false;
        }

        private void chkAngle_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAngle.Checked)
                panelPong.pong.bigAngles = 1;
            else
                panelPong.pong.bigAngles = 0;
        }
    }
}
