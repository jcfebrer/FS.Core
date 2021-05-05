#region

using System;
using System.Windows.Forms;

#endregion

namespace WiworBrowser.UI.Controls
{
    public partial class NewGroup : Form
    {
        private readonly string name;

        public NewGroup(string name)
        {
            this.name = name;
            InitializeComponent();
        }

        private void NewGroup_Load(object sender, EventArgs e)
        {
            groupName.Text = name;
        }
    }
}