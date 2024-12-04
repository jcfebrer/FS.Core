using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FSFormControlsCore
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControlsCore.Resources.DBToolBar.bmp")]
    [ToolboxItem(true)]
    public class DBToolBar : ToolStrip, ISupportInitialize
    {
        public enum DBRuntimeCustomizationOptions
        {
            None = 0,
            AllowCustomizeDialog = 1,
            AllowAltClickToolDragging = 2,
            AllowToolbarLocking = 4,
            AllowImageEditing = 8,
            All = -1
        }

        public DBToolBar()
        {
            Dock = DockStyle.Top;
        }

        public int DesignerFlags { get; set; }
        public object DockWithinContainer { get; set; }
        public bool LockToolbars { get; set; }
        public bool MdiMergeable { get; set; }
        public DBRuntimeCustomizationOptions RuntimeCustomizationOptions { get; set; }
        public bool ShowShortcutsInToolTips { get; set; }
        
        public ToolStripItemCollection Tools
        {
            get { return this.Items; }
        }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
    }
}