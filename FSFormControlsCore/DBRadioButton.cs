using System.Windows.Forms;

namespace FSFormControlsCore
{
    public class DBRadioButton : RadioButton
    {
        public string DisplayText
        {
            get { return Text; }
            set { Text = value; }
        }

        public object DataValue { get; set; }
    }
}