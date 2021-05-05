#region

using System;
using System.Windows.Forms;

#endregion

namespace WiworBrowser.UI.Controls
{
    public partial class RenameLink : Form
    {
        private readonly string oldName;

        public RenameLink(string oldName)
        {
            this.oldName = oldName;
            InitializeComponent();
        }

        private void RenameLink_Load(object sender, EventArgs e)
        {
            newName.Text = oldName;
        }
    }
}