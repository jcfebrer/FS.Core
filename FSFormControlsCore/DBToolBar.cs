using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FSFormControlsCore
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControlsCore.Resources.DBToolBar.bmp")]
    [ToolboxItem(true)]
    public class DBToolBar : ToolStrip, ISupportInitialize
    {
        public DBToolBar()
        {
            Dock = DockStyle.Top;
        }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
    }
}