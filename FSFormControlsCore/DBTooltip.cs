using System.Windows.Forms;

namespace FSFormControlsCore
{
    public class DBTooltip : ToolTip
    {
        public DBTooltip()
        {
        }

        public DBTooltip(string text)
        {
            ToolTipText = text;
        }

        public string ToolTipText { get; set; }
    }
}