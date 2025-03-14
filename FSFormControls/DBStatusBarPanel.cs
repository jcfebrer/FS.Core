using System.ComponentModel;
using System.Windows.Forms;

namespace FSFormControls
{
    public class DBStatusBarPanel : ToolStripStatusLabel, ISupportInitialize
    {
        public enum SizingModeEnum
        {
            Default,
            Spring
        }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Key
        {
            get { return Name; }
            set { Name = value; }
        }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public SizingModeEnum SizingMode { get; set; }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ProgressBar ProgressBarInfo { get; set; }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new bool Visible { get; set; }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
    }
}