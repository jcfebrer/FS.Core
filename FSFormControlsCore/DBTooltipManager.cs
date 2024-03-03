using System.ComponentModel;
using System.Windows.Forms;

namespace FSFormControlsCore
{
    public class DBTooltipManager
    {
        private IContainer components;

        public DBTooltipManager(IContainer components)
        {
            this.components = components;
        }

        public int AutoPopDelay { get; set; }
        public bool Enabled { get; set; }
        public Control ContainingControl { get; set; }

        public void ShowToolTip(Control control)
        {
            //throw new NotImplementedException();
        }

        public void SetToolTip(Control control, DBTooltip tooltip)
        {
            //throw new NotImplementedException();
        }

        public DBTooltip GetToolTip(Control control)
        {
            //throw new NotImplementedException();
            return new DBTooltip("Revisar ToolTip");
        }
    }
}