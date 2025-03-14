#region

using System.ComponentModel;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class DBGroupBox : GroupBox, ISupportInitialize
    {
        public DBGroupBox()
        {
        }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Caption
        {
            get { return Text; }
            set { Text = value; }
        }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DBAppearance HeaderAppearance { get; set; }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public BorderStyle BorderStyle { get; set; }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
    }
}