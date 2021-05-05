#region

using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

#endregion

namespace WiworBrowser.UI.Controls
{
    public partial class About : Form
    {
        private readonly bool splash;

        public About(bool splash)
        {
            this.splash = splash;
            InitializeComponent();

            Text = String.Format("Sobre {0}", Common.AssemblyFunctions.AssemblyTitle);
            labelProductName.Text = Common.AssemblyFunctions.AssemblyProduct;
            labelVersion.Text = String.Format("Version {0}", Common.AssemblyFunctions.AssemblyVersion);
            labelCopyright.Text = Common.AssemblyFunctions.AssemblyCopyright;
            labelCompanyName.Text = Common.AssemblyFunctions.AssemblyCompany;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void About_Load(object sender, EventArgs e)
        {
            if (splash)
            {
                Timer t = new Timer();
                t.Tick += t_Tick;
                t.Interval = 4000;
                t.Start();

                //FormBorderStyle = FormBorderStyle.FixedToolWindow;
                okButton.Visible = false;
            }
        }

        private void t_Tick(object sender, EventArgs e)
        {
            Close();
        }
    }
}