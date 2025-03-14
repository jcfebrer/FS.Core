using System.ComponentModel;
using System.Windows.Forms;

namespace FSFormControls
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

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ToolTipText { get; set; }
    }
}