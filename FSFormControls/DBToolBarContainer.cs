using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSFormControls
{
    public class DBToolBarContainer : ToolStripContainer
    {
        public DockStyle DockedPosition
        {
            get { return this.Dock; }
            set { this.Dock = value; }
        }

        public DBToolBarManager ToolbarsManager { get; set; }
    }
}
