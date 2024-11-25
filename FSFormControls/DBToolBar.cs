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

        public int DesignerFlags { get; set; }
        public object DockWithinContainer { get; set; }
        public bool LockToolbars { get; set; }
        public bool MdiMergeable { get; set; }
        public object RuntimeCustomizationOptions { get; set; }
        public bool ShowShortcutsInToolTips { get; set; }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
    }
}