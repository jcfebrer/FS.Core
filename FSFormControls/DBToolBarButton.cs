using System.Windows.Forms;

namespace FSFormControls
{
    public class DBToolBarButton : ToolBarButton
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