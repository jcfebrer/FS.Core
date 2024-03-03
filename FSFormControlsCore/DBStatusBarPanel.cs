using System.Windows.Forms;

namespace FSFormControlsCore
{
    public class DBStatusBarPanel : ToolStripStatusLabel
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

        public bool Visible { get; set; }
    }
}