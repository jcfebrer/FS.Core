#region

using System.ComponentModel;
using System.Windows.Forms;

#endregion

namespace FSFormControlsCore
{
    public class DBGroupBox : GroupBox, ISupportInitialize
    {
        public DBGroupBox()
        {
        }

        public string Caption
        {
            get { return Text; }
            set { Text = value; }
        }

        public DBAppearance HeaderAppearance { get; set; }

        public BorderStyle BorderStyle { get; set; }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
    }
}