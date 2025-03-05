using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static FSFormControlsCore.DBToolBar;
using static System.Windows.Forms.ToolStrip;

namespace FSFormControlsCore
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControlsCore.Resources.DBToolBar.bmp")]
    [ToolboxItem(true)]
    public class DBToolBarManager
    {
        public DBToolBarManager()
        {
            DBToolBar toolbar = new DBToolBar();
            toolbar.Click += Toolbar_ButtonClick;
            
            toolbars.Add(toolbar);
        }

        private void Toolbar_ButtonClick(object sender, EventArgs e)
        {
            if (this.ButtonClick != null)
            {
                this.ButtonClick(sender, e);
            }
        }

        public enum DBRuntimeCustomizationOptions
        {
            None = 0,
            AllowCustomizeDialog = 1,
            AllowAltClickToolDragging = 2,
            AllowToolbarLocking = 4,
            AllowImageEditing = 8,
            All = -1
        }

        public int DesignerFlags { get; set; }
        public object DockWithinContainer { get; set; }
        public bool LockToolbars { get; set; }
        public bool MdiMergeable { get; set; }
        public DBRuntimeCustomizationOptions RuntimeCustomizationOptions { get; set; }
        public bool ShowShortcutsInToolTips { get; set; }
        List<DBToolBar> toolbars = new List<DBToolBar>();

        /// <summary>
        /// Esta propiedad la utiliza infragistics para manejar multiples Toolbar.
        /// </summary>
        public List<DBToolBar> Toolbars
        {
            get { return toolbars; }
            set { toolbars = value; }
        }

        public bool Exists(string name)
        {
            if (toolbars.Exists(n => n.Name == name))
                return true;
            return false;
        }

        public ToolStripItemCollection Tools
        {
            get { return toolbars[0].Items; }
        }

        public ToolStripItemCollection Buttons
        {
            get { return toolbars[0].Items; }
        }
        
        public bool Visible {
            get { return toolbars[0].Visible; }
            set { toolbars[0].Visible = value; }
        }

        public event ToolBarButtonClickEventHandler ButtonClick;
        public delegate void ToolBarButtonClickEventHandler(object sender, EventArgs e);
    }
}