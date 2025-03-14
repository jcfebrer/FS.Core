using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBControl.bmp")]
    [ToolboxItem(true)]
    public partial class DBOptionSet : DBUserControl, ISupportInitialize
    {
        public DBOptionSet()
        {
            InitializeComponent();
        }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DBRadioButtonCollection Items { get; set; }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new BorderStyle BorderStyle
        {
            get { return dbGroupBox1.BorderStyle; }
            set { dbGroupBox1.BorderStyle = value; }
        }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ItemSpacingVertical { get; set; }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int TextIndentation { get; set; }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Point ItemOrigin { get; set; }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int CheckedIndex { get; set; }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
    }
}