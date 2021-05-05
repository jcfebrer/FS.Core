#region

using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class frmMessage : Form
    {
        public void Show(string Message)
        {
            Label1.Text = Message;
            Show();
            Application.DoEvents();
        }

        public void ShowDialog(string Message)
        {
            Label1.Text = Message;
            ShowDialog();
            Application.DoEvents();
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        internal Label Label1;

        public frmMessage()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();
            base.Dispose(disposing);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            Label1 = new Label();
            SuspendLayout();
            // 
            // Label1
            // 
            Label1.Dock = DockStyle.Fill;
            Label1.Location = new Point(0, 0);
            Label1.Name = "Label1";
            Label1.Size = new Size(438, 63);
            Label1.TabIndex = 0;
            Label1.Text = "Message";
            Label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmMessage
            // 
            AutoScaleMode = AutoScaleMode.None;
            CausesValidation = false;
            ClientSize = new Size(438, 63);
            ControlBox = false;
            Controls.Add(Label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmMessage";
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Status";
            ResumeLayout(false);
        }

        #endregion
    }
}