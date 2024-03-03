using System.Windows.Forms;

namespace FSFormControlsCore.UserControls.DBGridView
{
    public class DBGridViewDisplayLayout
    {
        public DBAppearance Appearance { get; set; }
        public BorderStyle BorderStyle { get; set; }
        public DBGridViewDisplayLayout GroupByBox { get; set; }
        public DBGridViewDisplayLayout Override { get; set; }
        public bool CaptionVisible { get; set; }
    }
}