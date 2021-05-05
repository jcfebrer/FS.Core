using System;
using System.Net;
using System.Windows.Forms;
using UHPShared;

namespace FSSyscat
{
    public partial class frmChat : Form
    {
        private ClientUtil client;
        private new string Name;
        public IPEndPoint RemoteEP;
        public long ID;

        public frmChat()
        {
            InitializeComponent();
        }

        public frmChat(ClientUtil _client, string _Name, IPEndPoint _RemoteEP, long _ID)
        {
            InitializeComponent();

            client = _client;
            Name = _Name;
            RemoteEP = _RemoteEP;
            ID = _ID;

            Text = Name + " via " + RemoteEP;
        }

        public void ReceiveMessage(UHPShared.UhpMessage M)
        {
            txtConversation.Text += M.From + ": " + M.Content + Environment.NewLine;
            txtConversation.SelectionStart = txtConversation.Text.Length;
            txtConversation.ScrollToCaret();

            txtMessage.Focus();
        }

        private void SendMessage()
        {
            UHPShared.UhpMessage M = new UHPShared.UhpMessage(client.LocalClientInfo.Name, Name, txtMessage.Text);
            client.SendMessageUDP(M, RemoteEP);
            txtConversation.Text += client.LocalClientInfo.Name + ": " + txtMessage.Text + Environment.NewLine;

            txtConversation.SelectionStart = txtConversation.Text.Length;
            txtConversation.ScrollToCaret();

            txtMessage.Text = string.Empty;
            txtMessage.Focus();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                SendMessage();
        }
    }
}
