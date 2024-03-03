using System.Windows.Forms;

namespace FSFormControlsCore
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

        public string Caption
        {
            get { return Text; }
            set { Text = value; }
        }

        public Shortcut Shortcut { get; set; }

        public bool Checked { get; set; }

        public string Key
        {
            get { return Name; }
            set { Name = value; }
        }
    }
}