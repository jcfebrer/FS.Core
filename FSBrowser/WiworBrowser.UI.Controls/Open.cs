#region

using System;
using System.Windows.Forms;

#endregion

namespace WiworBrowser.UI.Controls
{
    public partial class Open : Form
    {
        private readonly WiworBrowser.Controls.DBWebBrowser wb;

        public Open(WiworBrowser.Controls.DBWebBrowser wb)
        {
            this.wb = wb;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wb.Navigate(textBox1.Text);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Text Files(*.txt)|*.txt|Html file(*.html)|*.html|AllFiles|*.*";
            if (o.ShowDialog() == DialogResult.OK)
                textBox1.Text = o.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                wb.Navigate(textBox1.Text);
                Close();
            }
        }
    }
}