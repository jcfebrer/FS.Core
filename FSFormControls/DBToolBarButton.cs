using System.ComponentModel;
using System.Windows.Forms;

namespace FSFormControls
{
    public class DBToolBarButton : ToolStripButton
    {
        public DBToolBarButton()
        {
        }

        public DBToolBarButton(string name)
        {
            Name = name;
        }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Caption
        {
            get { return Text; }
            set { Text = value; }
        }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Shortcut Shortcut { get; set; }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new bool Checked { get; set; }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Key
        {
            get { return Name; }
            set { Name = value; }
        }

        public class SharedDef
        {
            public Shortcut Shortcut;
            public string ToolTipText;
        }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public SharedDef SharedProps { get; set; }
    }
}