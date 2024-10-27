using System.ComponentModel;
using System.Windows.Forms;

namespace FSFormControlsCore
{
    public class DBStatusBarPanel : ToolStripStatusLabel, ISupportInitialize
    {
        public enum SizingModeEnum
        {
            Default,
            Spring
        }

        public string Key
        {
            get { return Name; }
            set { Name = value; }
        }

        public SizingModeEnum SizingMode { get; set; }

        public ProgressBar ProgressBarInfo { get; set; }

        public new bool Visible { get; set; }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
    }
}