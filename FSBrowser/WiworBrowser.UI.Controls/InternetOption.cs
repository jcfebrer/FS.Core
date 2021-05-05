#region

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

#endregion

namespace WiworBrowser.UI.Controls
{
    public partial class InternetOption : Form
    {
        public Color backcolor;
        public Font font;
        public Color forecolor;
        private string currentPage;

        public InternetOption(string currentPage)
        {
            InitializeComponent();

            chkAllowFileDownload.Checked = Common.Configuration.Settings.AllowFileDownload;
            chkAllowIFrames.Checked = Common.Configuration.Settings.AllowIFrames;
            chkAllowImageExternalLinks.Checked = Common.Configuration.Settings.AllowImageExternalLinks;
            chkAllowNewWindow.Checked = Common.Configuration.Settings.AllowNewWindow;
            chkCheckIsChildValidPage.Checked = Common.Configuration.Settings.CheckIsChildValidPage;
            chkRemoveContextMenu.Checked = Common.Configuration.Settings.RemoveContextMenu;
            chkScriptErrorsSuppressed.Checked = Common.Configuration.Settings.ScriptErrorsSuppressed;
            chkWebBrowserShortcutsEnabled.Checked = Common.Configuration.Settings.WebBrowserShortcutsEnabled;
            chkCheckContent.Checked = Common.Configuration.Settings.CheckContent;
            chkPasswordOnExit.Checked = Common.Configuration.Settings.PasswordOnExit;

            txtPassword.Text = Common.Configuration.Settings.SettingsPassword;
            homepage.Text = Common.Configuration.Settings.HomePage;

            lstBadWords.Items.AddRange(Common.Configuration.Settings.BadWords.ToArray());
            //lstBlackList.Items.AddRange(Common.Configuration.Settings.BlackList);

            num.Value = Common.Configuration.Settings.DropDown;

            this.currentPage = currentPage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            homepage.Text = "about:blank";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            homepage.Text = currentPage;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
                font = dlg.Font;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
                forecolor = c.Color;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
                backcolor = c.Color;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Common.Configuration.Settings.AllowFileDownload = chkAllowFileDownload.Checked;
            Common.Configuration.Settings.AllowIFrames = chkAllowIFrames.Checked;
            Common.Configuration.Settings.AllowImageExternalLinks = chkAllowImageExternalLinks.Checked;
            Common.Configuration.Settings.AllowNewWindow = chkAllowNewWindow.Checked;
            Common.Configuration.Settings.CheckIsChildValidPage = chkCheckIsChildValidPage.Checked;
            Common.Configuration.Settings.RemoveContextMenu = chkRemoveContextMenu.Checked;
            Common.Configuration.Settings.IsWebBrowserContextMenuEnabled = chkRemoveHistory.Checked;
            Common.Configuration.Settings.ScriptErrorsSuppressed = chkScriptErrorsSuppressed.Checked;
            Common.Configuration.Settings.WebBrowserShortcutsEnabled = chkWebBrowserShortcutsEnabled.Checked;
            Common.Configuration.Settings.CheckContent = chkCheckContent.Checked;
            Common.Configuration.Settings.SettingsPassword = txtPassword.Text;
            Common.Configuration.Settings.PasswordOnExit = chkPasswordOnExit.Checked;

            Common.Configuration.Settings.BadWords = ToArrayString(lstBadWords);

            if (homepage.Text != "")
                Common.Configuration.Settings.HomePage = homepage.Text;
        }

        private List<string> ToArrayString(ListBox listBox)
        {
            List<string> list = new List<string>();

            for (int f = 0; f < listBox.Items.Count; f++ )
            {
                list.Add(listBox.Items[f].ToString());
            }

            return list;
        }

        private void btnDefaultValues_Click(object sender, EventArgs e)
        {
            chkAllowFileDownload.Checked = false;
            chkAllowIFrames.Checked = false;
            chkAllowImageExternalLinks.Checked = false;
            chkAllowNewWindow.Checked = false;
            chkCheckIsChildValidPage.Checked = true;
            chkRemoveHistory.Checked = false;
            chkRemoveContextMenu.Checked = true;
            chkScriptErrorsSuppressed.Checked = true;
            chkWebBrowserShortcutsEnabled.Checked = false;
            chkPasswordOnExit.Checked = false;
        }

        private void cmdNewBadWord_Click(object sender, EventArgs e)
        {
            if (txtNewWord.Text != "")
            {
                lstBadWords.Items.Add(txtNewWord.Text);
            }           
        }

        private void cmdDeleteBadWord_Click(object sender, EventArgs e)
        {
            if (lstBadWords.SelectedItem != null)
            {
                if (MessageBox.Show(string.Format("¿Estás seguro de querer eliminar de la lista la palabra: '{0}'?", lstBadWords.SelectedItem.ToString()),"Borrar",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lstBadWords.Items.Remove(lstBadWords.SelectedItem);
                }
            }
        }

        private void cmdNewUrl_Click(object sender, EventArgs e)
        {
            if (txtNewUrl.Text != "")
            {
                if(WiworBrowser.Controls.DBFunctions.IsUrl(txtNewUrl.Text))
                {
                    lstBlackList.Items.Add(txtNewUrl.Text);
                }
                else
                {
                    MessageBox.Show("No es una dirección URL válida.", "No válida", MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                }
            }  
        }

        private void cmdDeleteUrl_Click(object sender, EventArgs e)
        {
            if (lstBlackList.SelectedItem != null)
            {
                if (MessageBox.Show(string.Format("¿Estás seguro de querer eliminar de la lista la URL: '{0}'?", lstBlackList.SelectedItem.ToString()), "Borrar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lstBlackList.Items.Remove(lstBlackList.SelectedItem);
                }
            }
        }

        private void cmdCheckVersion_Click(object sender, EventArgs e)
        {
            (new AutoUpdate(false)).Show();
        }

    }
}