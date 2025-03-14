using System.ComponentModel;
using System.Windows.Forms;

namespace FSFormControls
{
    public class DBRadioButton : RadioButton
    {
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string DisplayText
        {
            get { return Text; }
            set { Text = value; }
        }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public object DataValue { get; set; }
    }
}