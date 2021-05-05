using FSException;
using FSTrace;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UHPShared;

namespace FSSyscat
{
    class Chat
    {
        public static List<frmChat> FrmChats = new List<frmChat>();
        public static UHPShared.ClientUtil clientUHP;
        public static bool Connected = false;

        public static void StartUhp()
        {
            try
            {
                clientUHP = new UHPShared.ClientUtil(ConfigurationManager.AppSettings["UHPServer"], ConfigurationManager.AppSettings["UHPPort"], false, "SYSCAT en: " + System.Environment.MachineName);
                Log.TraceInfo("Conectando con servidor UHP: " + clientUHP.ServerEndpoint.ToString() + ". Puerto: " + clientUHP.ServerEndpoint.Port.ToString());

                clientUHP.OnServerConnect += Client_OnServerConnect;
                clientUHP.OnServerDisconnect += Client_OnServerDisconnect;
                clientUHP.OnClientAdded += Client_OnClientAdded;
                clientUHP.OnClientUpdated += Client_OnClientUpdated;
                clientUHP.OnClientRemoved += Client_OnClientRemoved;
                clientUHP.OnClientConnection += Client_OnClientConnection;
                clientUHP.OnMessageReceived += Client_OnMessageReceived;

                clientUHP.UPnPEnabled = false;

                clientUHP.Connect();
            }
            catch (System.Exception ex)
            {
                Log.TraceError(ex);
            }
        }

        public static void Client_OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            frmChat chat = FrmChats.FirstOrDefault(C => C.RemoteEP.Equals((IPEndPoint)sender));

            if (chat == null)
            {
                chat = new frmChat(clientUHP, e.clientInfo.Name, e.EstablishedEP, e.clientInfo.ID);
                FrmChats.Add(chat);
                chat.Closed += delegate { FrmChats.Remove(chat); };

                Program.SynchronizationContext.Post((state) =>
                {
                    if (!Program.frmMain.IsDisposed)
                    {
                        chat.Show();
                    }
                }, null);
            }

            Program.SynchronizationContext.Post((state) =>
            {
                if (!Program.frmMain.IsDisposed)
                {
                    chat.Focus();
                    chat.ReceiveMessage(e.message);
                }
            }, null);
        }

        public static void Client_OnClientConnection(object sender, IPEndPoint e)
        {
            Log.TraceInfo("Cliente conectado desde: " + e.ToString());

            frmChat chat = FrmChats.FirstOrDefault(C => C.RemoteEP.Equals(e));

            if (chat == null)
            {
                chat = new frmChat(clientUHP, ((UhpMessage)sender).Name, e, ((UhpMessage)sender).ID);
                FrmChats.Add(chat);
                chat.Closed += delegate { FrmChats.Remove(chat); };

                Program.SynchronizationContext.Post((state) =>
                {
                    if (!Program.frmMain.IsDisposed)
                    {
                        chat.Show();
                    }
                }, null);
            }

            Program.SynchronizationContext.Post((state) =>
            {
                if (!Program.frmMain.IsDisposed)
                {
                    chat.Focus();
                    chat.txtMessage.Focus();
                }
            }, null);
        }

        public static void Client_OnClientRemoved(object sender, UhpMessage e)
        {
            int i = -1;
            frmChat chat = null;

            foreach (ListViewItem lvi in Program.frmMain.ListViewClients.Items)
            {
                UhpMessage CI = lvi.Tag as UhpMessage;
                CI.ClassType = UhpType.ClientInfo;

                if (CI.ID == e.ID)
                    i = Program.frmMain.ListViewClients.Items.IndexOf(lvi);
            }

            foreach (frmChat CH in FrmChats)
                if (CH.ID == e.ID)
                    chat = CH;

            if (i != -1)
                Program.frmMain.ListViewClients.Items.RemoveAt(i);

            if (chat != null)
                chat.Close();
        }

        public static void Client_OnClientUpdated(object sender, UhpMessage e)
        {
            Program.SynchronizationContext.Post((state) =>
            {
                if (!Program.frmMain.IsDisposed)
                {
                    foreach (ListViewItem lvi in Program.frmMain.ListViewClients.Items)
                    {
                        UhpMessage CI = lvi.Tag as UhpMessage;
                        CI.ClassType = UhpType.ClientInfo;

                        if (CI.ID == e.ID)
                            CI.Update(e);
                    }
                }
            }, null);
        }

        public static void Client_OnClientAdded(object sender, UhpMessage e)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = e.Name;
            lvi.Tag = e;

            Program.SynchronizationContext.Post((state) =>
            {
                if (!Program.frmMain.IsDisposed)
                {
                    Program.frmMain.ListViewClients.Items.Add(lvi);
                }
            }, null);
        }

        public static void Client_OnServerDisconnect(object sender, EventArgs e)
        {
            Program.frmMain.ListViewClients.Items.Clear();
            Log.TraceInfo("Desconectado del servidor.");
        }

        public static void Client_OnServerConnect(object sender, EventArgs e)
        {
            Connected = true;
            Log.TraceInfo("Conectado con el servidor.");
        }
    }
}
