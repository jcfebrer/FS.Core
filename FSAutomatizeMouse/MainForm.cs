using System;
using System.Windows.Forms;
using FSLibrary;
using System.Diagnostics;
using FSMouseKeyboardLibrary;

namespace FSAutomatizeMouse
{
    public partial class MainForm : Form
    { 
        private KeyboardHook HookerKeyboard;
        private MouseHook HookerMouse;
        private bool capturing = false;
        private int lastTimeRecorded = 0;

        public MainForm()
        {
            InitializeComponent();

            InitHook();
        }

        #region Private Methods

        private void InitHook()
        {
            if (HookerKeyboard == null)
            {
                HookerKeyboard = new FSMouseKeyboardLibrary.KeyboardHook();
                HookerKeyboard.KeyDown += new KeyEventHandler(HookerKeyDown);
                HookerKeyboard.KeyPress += new KeyPressEventHandler(HookerKeyPress);
                HookerKeyboard.KeyUp += new KeyEventHandler(HookerKeyUp);
            }

            if (HookerMouse == null)
            {
                HookerMouse = new FSMouseKeyboardLibrary.MouseHook();
                HookerMouse.MouseUp += new MouseEventHandler(MouseUp);
                HookerMouse.MouseDown += new MouseEventHandler(MouseDown);
                HookerMouse.MouseMove += new MouseEventHandler(MouseMove);
                HookerMouse.MouseWheel += new MouseEventHandler(MouseWheel);
            }
        }

        public void HookerKeyUp(object sender, KeyEventArgs e)
        {
            AddEntryKey(e, MouseActionEntry.EventType.KeyUp);
        }

        public void HookerKeyPress(object sender, KeyPressEventArgs e)
        {
            //AddEntryKey(e, MouseActionEntry.EventType.KeyPress);
        }

        public void HookerKeyDown(object sender, KeyEventArgs e)
        {
            AddEntryKey(e, MouseActionEntry.EventType.KeyDown);
        }

        public new void MouseMove(object sender, MouseEventArgs e)
        {
            AddEntryMouse(e, MouseActionEntry.EventType.MouseMove);
        }

        public new void MouseDown(object sender, MouseEventArgs e)
        {
            AddEntryMouse(e, MouseActionEntry.EventType.MouseDown);
        }

        public new void MouseUp(object sender, MouseEventArgs e)
        {
            AddEntryMouse(e, MouseActionEntry.EventType.MouseUp);
        }
        public new void MouseWheel(object sender, MouseEventArgs e)
        {
            AddEntryMouse(e, MouseActionEntry.EventType.MouseWheel);
        }

        private void AddEntryMouse(MouseEventArgs e, MouseActionEntry.EventType eventType)
        {
            int x = e.X;
            int y = e.Y;

            string point = x.ToString() + "," + y.ToString();

            //Obtenemos el proceso acctivo
            Process p = Process.GetProcessById(Win32API.GetWindowProcessID(Win32API.GetForegroundWindow()));

            ListViewItem lvi = new ListViewItem(new string[] { point, eventType.ToString(), e.Button.ToString(), (Environment.TickCount - lastTimeRecorded).ToString(), "None", p.MainWindowTitle });
            MouseActionEntry action = new MouseActionEntry(x, y, Environment.TickCount - lastTimeRecorded, eventType, p.MainWindowTitle, e.Button, Keys.None);
            lvi.Tag = action;
            lvActions.Items.Add(lvi);

            this.toolStripStatusPosX.Text = e.X.ToString();
            this.toolStripStatusPosY.Text = e.Y.ToString();

            lastTimeRecorded = Environment.TickCount;
        }

        private void AddEntryKey(KeyEventArgs e, MouseActionEntry.EventType eventType)
        {
            //Obtenemos el proceso acctivo
            Process p = Process.GetProcessById(Win32API.GetWindowProcessID(Win32API.GetForegroundWindow()));

            ListViewItem lvi = new ListViewItem(new string[] { "0,0", eventType.ToString(),"None", (Environment.TickCount - lastTimeRecorded).ToString(), e.KeyCode.ToString(), p.MainWindowTitle });
            MouseActionEntry action = new MouseActionEntry(0, 0, Environment.TickCount - lastTimeRecorded, eventType, p.MainWindowTitle, MouseButtons.None, e.KeyCode);
            lvi.Tag = action;
            lvActions.Items.Add(lvi);

            lastTimeRecorded = Environment.TickCount;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (lvActions.Items.Count == 0) return;

            WindowState = FormWindowState.Minimized;

            MouseActionsEntry actions = new MouseActionsEntry();


            foreach (ListViewItem lvi in lvActions.Items)
            {
                actions.Add(lvi.Tag as MouseActionEntry);
            }

            ProcessActions.OnEntryProcess += new ProcessActions.ActionEntryEventHandler(this.OnEntryProcess);

            ProcessActions.Do(actions, chkRepeat.Checked, (int)numericUpDown1.Value);

            WindowState = FormWindowState.Normal;
        }


        void OnEntryProcess(MouseActionEntry action, int position)
        {
            lvActions.Items[position].Selected = true;
            lvActions.Refresh();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cout = lvActions.Items.Count;
            int coutselect = lvActions.SelectedItems.Count;

            for (int i = coutselect - 1; i >= 0; --i)
            {
                int index = lvActions.SelectedItems[i].Index;
                lvActions.Items[index].Remove();
            }
        }

        private void lvActions_MouseDown(object sender, MouseEventArgs e)
        {
            int coutselect = lvActions.SelectedItems.Count;
            deleteToolStripMenuItem.Available = coutselect > 0;
            editToolStripMenuItem.Available = coutselect == 1;
            borrarScriptToolStripMenuItem.Available = coutselect > 0;
        }

        private void OpenFileXml(string file)
        {
            MouseActionsEntry entries = ProcessActions.OpenFileXml(file);

            lvActions.Items.Clear();
            foreach (MouseActionEntry ae in entries)
            {
                string point = ae.X.ToString() + "," + ae.Y.ToString();
                string interval = ae.Interval.ToString();
                ListViewItem lvi = new ListViewItem(new string[] { point, ae.Type.ToString(), interval, ae.KeyCode.ToString(), ae.Process });
                MouseActionEntry acion = new MouseActionEntry(ae.X, ae.Y, ae.Interval, ae.Type, ae.Process, ae.Button, ae.KeyCode);
                lvi.Tag = acion;
                lvActions.Items.Add(lvi);
            }
        }
       
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MouseActionEntry action = lvActions.SelectedItems[0].Tag as MouseActionEntry;
            frmEditar frm = new frmEditar(action);
            frm.Actionentry = action;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                action = frm.Actionentry;
                lvActions.SelectedItems[0].Tag = action;
                lvActions.SelectedItems[0].Text = action.X + "," + action.Y;
                lvActions.SelectedItems[0].SubItems[1].Text = action.Type.ToString();
                lvActions.SelectedItems[0].SubItems[2].Text = action.Button.ToString();
                lvActions.SelectedItems[0].SubItems[3].Text = action.Interval.ToString();
                lvActions.SelectedItems[0].SubItems[4].Text = action.KeyCode.ToString();
                lvActions.SelectedItems[0].SubItems[5].Text = action.Process;
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (capturing)
            {
                DetenerCaptura();
            }
            else
            {
                lastTimeRecorded = Environment.TickCount;

                Capturar();
            }
        }

        private void Capturar()
        {
            if (HookerMouse != null)
            {
                HookerMouse.Start();
            }
            if (HookerKeyboard != null)
            {
                HookerKeyboard.Start();
            }

            if (HookerKeyboard.IsStarted == false || HookerMouse.IsStarted == false)
            {
                MessageBox.Show("Imposible iniciar Hooker de teclado/ratón", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                WindowState = FormWindowState.Minimized;

                capturing = true;
                btnCapture.Text = "Parar captura";
            }    
        }

        private void DetenerCaptura()
        {
            if (HookerKeyboard != null)
                HookerKeyboard.Stop();
            if (HookerMouse != null)
                HookerMouse.Stop();

            capturing = false;
            btnCapture.Text = "CAPTURAR!";
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            DetenerCaptura();
        }

        private void borrarScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvActions.Items.Clear();
        }

        private void abrirScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenScript();
        }

        private void OpenScript()
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "XML File |*.xml";
            file.Multiselect = false;
            if (file.ShowDialog() == DialogResult.OK)
            {
                OpenFileXml(file.FileName);
                string name = file.SafeFileName;
                this.Text = name.Substring(0, name.Length - 4);
            }
        }

        private void guardarScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveScript();
        }

        private void SaveScript()
        {
            SaveFileDialog file = new SaveFileDialog();
            file.Filter = "XML File |*.xml";
            if (file.ShowDialog() == DialogResult.OK)
            { 
                MouseActionsEntry actionsEntry = new MouseActionsEntry();
                foreach (ListViewItem lvi in lvActions.Items)
                {
                    MouseActionEntry actionEntry = lvi.Tag as MouseActionEntry;
                    actionsEntry.Add(actionEntry);
                }

                if (ProcessActions.SaveFileXml(file.FileName, actionsEntry))
                    MessageBox.Show("Fichero guardado correctamente", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Problemas al guardar el fichero", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvActions_DoubleClick(object sender, EventArgs e)
        {
            if (editToolStripMenuItem.Available)
            {
                editToolStripMenuItem.PerformClick();
            }
        }
        #endregion

        private void btnClear_Click(object sender, EventArgs e)
        {
            lvActions.Items.Clear();
        }
    }
}
