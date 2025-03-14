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
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DockStyle DockedPosition
        {
            get { return this.Dock; }
            set { this.Dock = value; }
        }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DBToolBarManager ToolbarsManager { get; set; }
    }
}
