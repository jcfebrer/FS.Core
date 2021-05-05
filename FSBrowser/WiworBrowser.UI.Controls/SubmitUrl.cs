#region

using System.Windows.Forms;

#endregion

namespace WiworBrowser.UI.Controls
{
    public partial class SubmitUrl : Form
    {
        private string url;
        public SubmitUrl(string url)
        {
            this.url = url;
            InitializeComponent();
            this.Load += new System.EventHandler(SubmitUrl_Load);
        }

        void SubmitUrl_Load(object sender, System.EventArgs e)
        {
            lblUrl.Text = url;
        }
    }
}