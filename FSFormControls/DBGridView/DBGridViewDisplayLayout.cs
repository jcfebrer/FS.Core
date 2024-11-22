using System.Windows.Forms;

namespace FSFormControls.UserControls.DBGridView
{
    public class DBGridViewDisplayLayout
    {
        public enum DBCellClickAction
        {
            Default,
            Edit,
            RowSelect,
            CellSelect,
            EditAndSelectText
        }
        public enum DBHeaderClickAction
        {
            Default,
            Select,
            SortSingle,
            SortMulti,
            ExternalSortSingle,
            ExternalSortMulti
        }
        public enum DBBorderStyle
        {
            None,
            FixedSingle,
            Fixed3D
        }
        public enum DBHeaderStyle
        {
            Default,
            Standard,
            WindowsXPCommand,
            XPThemed,
            WindowsVista
        }
        public enum DBScrollBounds
        {
            ScrollToFill,
            ScrollToLastItem
        }
        public enum DBScrollStyle
        {
            Deferred,
            Immediate
        }
        public enum DBViewStyleBand
        {
            Vertical,
            Horizontal,
            OutlookGroupBy
        }

        public DBAppearance Appearance { get; set; }
        public BorderStyle BorderStyle { get; set; }
        public DBGridViewDisplayLayout GroupByBox { get; set; }
        public DBGridViewDisplayLayout Override { get; set; }
        public bool CaptionVisible { get; set; }
        public bool Hidden { get; set; }
        public bool RowSelectors { get; set; }
        public int MinRowHeight { get; set; }
        public int CellPadding { get; set; }
        public int MaxColScrollRegions { get; set; }
        public int MaxRowScrollRegions { get; set; }

        public DBAppearance ActiveRowAppearance { get; set; }
        public DBAppearance ActiveCellAppearance { get; set; }
        public DBAppearance BandLabelAppearance { get; set; }
        public DBAppearance CardAreaAppearance { get; set; }
        public DBAppearance CellAppearance { get; set; }
        public DBAppearance GroupByRowAppearance { get; set; }
        public DBAppearance HeaderAppearance { get; set; }
        public DBAppearance PromptAppearance { get;set; }
        public DBAppearance RowAppearance { get; set; }
        public DBAppearance TemplateAddRowAppearance { get; set; }

        public DBScrollBounds ScrollBounds { get; set; }
        public DBScrollStyle ScrollStyle { get; set; }
        public DBViewStyleBand ViewStyleBand { get; set; }
        public DBHeaderStyle HeaderStyle { get; set; }
        public DBHeaderClickAction HeaderClickAction { get; set; }
        public DBCellClickAction CellClickAction { get; set; }
        public DBBorderStyle BorderStyleCell { get; set; }
        public DBBorderStyle BorderStyleRow { get; set; }
    }
}