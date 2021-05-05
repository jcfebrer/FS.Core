#region

using System;
using System.Windows.Forms;
using WiworBrowser.Objects;

#endregion

namespace WiworBrowser.UI.Controls
{
    public partial class AddFavorites : Form
    {
        private readonly string group;
        private readonly string url;
        public string favFile;
        public string favName;
        public string favUrl;

        public AddFavorites(string url, string group)
        {
            this.url = url;
            this.group = group;
            InitializeComponent();
        }

        private void AddFavorits_Load(object sender, EventArgs e)
        {
            txtUrl.Text = url;
            txtName.Text = url;

            LoadGroupCombo();
        }

        private void LoadGroupCombo()
        {
            if (Common.Configuration.FavoritesGroups.Count == 0)
                cmbGroup.Items.Add("Links");
            else
            {
                foreach (Group fg in Common.Configuration.FavoritesGroups)
                {
                    cmbGroup.Items.Add(fg.Name);
                }
            }

            if(group != "")
                cmbGroup.Text = group;
            else
                cmbGroup.Text = cmbGroup.Items[0].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            favName = txtName.Text;
            favFile = cmbGroup.Text;
            favUrl = txtUrl.Text;
        }
    }
}