using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBToolBar.bmp")]
    [ToolboxItem(true)]
    public class DBToolBar : ToolBar, ISupportInitialize
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