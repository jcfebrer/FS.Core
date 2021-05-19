#region

using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class frmCalculator : Form
    {
        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        internal DBCalculator DbCalculator1;

        public frmCalculator()
        {
            InitializeComponent();

            DbCalculator1.Initialize();
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
            DbCalculator1 = new DBCalculator();
            SuspendLayout();
            // 
            // DbCalculator1
            // 
            DbCalculator1.About = null;
            DbCalculator1.BackColor = Color.Silver;
            DbCalculator1.ButtonColor = Color.FromArgb(224, 224, 224);
            DbCalculator1.ButtonSeparation = 10;
            DbCalculator1.Dock = DockStyle.Fill;
            DbCalculator1.EurValue = 166.386D;
            DbCalculator1.Location = new Point(0, 0);
            DbCalculator1.Name = "DbCalculator1";
            DbCalculator1.Size = new Size(160, 272);
            DbCalculator1.TabIndex = 0;
            DbCalculator1.TextColor = Color.Black;
            DbCalculator1.Track = false;
            // 
            // frmCalculator
            // 
            AutoScaleBaseSize = new Size(5, 13);
            ClientSize = new Size(160, 272);
            Controls.Add(DbCalculator1);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "frmCalculator";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Calculadora";
            ResumeLayout(false);
        }

        #endregion
    }
}