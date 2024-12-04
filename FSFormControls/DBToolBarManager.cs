using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBToolBar.bmp")]
    [ToolboxItem(true)]
    public class DBToolBarManager
    {
        public DBToolBarManager()
        {
        }

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
    }
}