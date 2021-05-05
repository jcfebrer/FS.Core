using FSMouseKeyboardLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FSAutomatizeMouse
{
    public partial class frmEditar : Form
    {
        #region Fields
        private MouseActionEntry action;
        #endregion

        #region Construction
        public frmEditar(MouseActionEntry action)
        {
            InitializeComponent();
            this.action = action;


            cbType.DataSource = Enum.GetValues(typeof(MouseActionEntry.EventType));
        }
        #endregion

        #region Private Methods
        private void EditWin_Load(object sender, EventArgs e)
        {
            if (action != null)
            {
                txbX.Text = action.X.ToString();
                txbY.Text = action.Y.ToString();
                txbData.Text = action.KeyCode.ToString();
                nWait.Value = action.Interval;
                cbType.SelectedItem = action.Type;
                txtProcess.Text = action.Process;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            action.X = Int32.Parse(txbX.Text);
            action.Y =  Int32.Parse(txbY.Text);
            action.KeyCode = txbData.Enabled ? (Keys)Enum.Parse(typeof(Keys), txbData.Text, true) : Keys.None;
            action.Interval = (int)nWait.Value;
            action.Type = (MouseActionEntry.EventType)cbType.SelectedIndex;
            action.Process = txtProcess.Text;
            this.DialogResult = DialogResult.OK;
        }
        #endregion

        #region Public methods
        public MouseActionEntry Actionentry
        {
            set { action = value; }
            get { return action; }
        }
        #endregion

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbData.Enabled = cbType.SelectedIndex.Equals(3);
        }

        

        

        
    }
}
