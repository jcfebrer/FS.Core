using System;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using DataTable = System.Data.DataTable;

namespace FSExcelCore
{
    public class Excel
    {
        public Application m_Excel;
        public Microsoft.Office.Interop.Excel.Range m_Rng;
        public Worksheet m_Sht;
        public Workbook m_WrkBk;

        public Excel()
        {
            m_Excel = new Application();
            m_WrkBk = null;
            m_Sht = null;
            m_Rng = null;
        }

        public void Open(string fileName)
        {
            m_WrkBk = m_Excel.Workbooks.Open(fileName);
            m_Sht = (Worksheet) m_WrkBk.Worksheets[1];
        }


        public void SetSheet(int number)
        {
            m_Sht = (Worksheet) m_WrkBk.Worksheets[number];
        }


        public string get_Range(string cell)
        {
            m_Rng = m_Sht.get_Range(cell);
            return Convert.ToString(m_Rng.get_Value());
        }

        public void set_Range(string cell, string value)
        {
            m_Rng = m_Sht.get_Range(cell);
            m_Rng.set_Value(null, value);
        }


        public void Show()
        {
            m_Excel.Visible = true;
        }


        public void Hide()
        {
            m_Excel.Visible = false;
        }


        public long ConvertHorizontalAlignmentToExcelAlign(HorizontalAlignment ha)
        {
            switch (ha)
            {
                case HorizontalAlignment.Center:
                    return Convert.ToInt64(XlVAlign.xlVAlignCenter);
                case HorizontalAlignment.Left:
                    return Convert.ToInt64(XlVAlign.xlVAlignDistributed);
                case HorizontalAlignment.Right:
                    return Convert.ToInt64(XlVAlign.xlVAlignJustify);
            }

            return 0;
        }

        public void Export(DataTable dataTable)
        {
            Export(dataTable, null);
        }

        public void Export(DataTable dataTable, string fileName)
        {
            var xlsApp = new Application();
            Workbook xlsWB = null;
            Worksheet xlsSheet = null;
            Microsoft.Office.Interop.Excel.Range xlsRng = null;
            int f = 0, g = 0;

            xlsApp.Visible = false;

            xlsWB = xlsApp.Workbooks.Add();
            xlsSheet = (Worksheet) xlsWB.ActiveSheet;

            for (f = 0; f <= dataTable.Columns.Count - 1; f++)
                xlsSheet.Cells[1, f + 1] = dataTable.Columns[f]
                    .ColumnName
                    .ToUpper();

            var ra = xlsSheet.get_Range("A1", "IV1");

            ra.Font.Bold = true;
            ra.VerticalAlignment = XlVAlign.xlVAlignCenter;


            for (f = 0; f <= dataTable.Rows.Count - 1; f++)
            for (g = 0; g <= dataTable.Columns.Count - 1; g++)
                xlsSheet.Cells[f + 3, g + 1] = dataTable.Rows[f][g].ToString();

            xlsRng = xlsSheet.get_Range("A1", "IV1");
            xlsRng.EntireColumn.AutoFit();

            xlsApp.Visible = true;
            xlsApp.UserControl = true;

            if (fileName != null)
                xlsSheet.SaveAs(fileName);

            xlsRng = null;
            xlsSheet = null;
            xlsWB = null;
            xlsApp = null;
        }

        //     public void ExportDBGrid(DBGrid grid)
        //     {
        //         Microsoft.Office.Interop.Excel.Application oXL = new Microsoft.Office.Interop.Excel.Application();
        //         Workbook oWB = null;
        //         Worksheet oSheet = null;
        //         Range oRng = null;
        //         int f = 0, g = 0;

        //         //oXL = ((Microsoft.Office.Interop.Excel.Application)(FSLibrary.Functions.CreateObject("Excel.Application", "")));
        //         oXL.Visible = false;

        //         oWB = oXL.Workbooks.Add(null);
        //         oSheet = ((Worksheet)(oWB.ActiveSheet));

        //         oSheet.Cells[1, 1] = grid.CaptionText; //.Value
        //         //oSheet.Cells[ 1, 1 ].Font.Bold = true; 
        //         //oSheet.Cells[ 1, 1 ].Font.Size = 12; 
        //         //oSheet.Cells[ 1, 1 ].BorderAround(); 

        //         for (f = 0; f <= grid.Columns.Count - 1; f++)
        //         {
        //             oSheet.Cells[3, f + 2] = grid.Columns[f].HeaderCaption; // .Value
        //             //oSheet.Cells[ 3, f + 2 ].VerticalAlignment = DBGlobal.func.ConvertHorizontalAlignmentToExcelAlign( Columns[ f ].Alignment ); 
        //         }

        //         Range cell1 = oSheet.get_Range("B3", "IV3");
        //cell1.Font.Bold = true;
        //cell1.VerticalAlignment = XlVAlign.xlVAlignCenter;


        //         for (f = 0; f <= grid.DataControl.DataTable.Rows.Count - 1; f++)
        //         {
        //             for (g = 0; g <= grid.Columns.Count - 1; g++)
        //             {
        //                 if (grid.Columns[g].ColumnType == FSFormControls.DBColumn.ColumnTypes.ComboColumn)
        //                 {
        //                     oSheet.Cells[f + 4, g + 2] = grid.Columns[g].ColumnDBControl.Find(grid.Columns[g].ColumnDBFieldData,
        //                                                                                  Convert.ToString(
        //                                                                                      grid.get_RowValue(
        //                                                                                          grid.Columns[g].FieldDB, -1)),
        //                                                                                  grid.Columns[g].ComboListField);
        //                     //.Value
        //                 }
        //                 else
        //                 {
        //                     oSheet.Cells[f + 4, g + 2] = grid.get_RowValue(grid.Columns[g].FieldDB, f); //.Value
        //                 }
        //             }
        //         }

        //         oRng = oSheet.get_Range("B3", "IV3");
        //         oRng.EntireColumn.AutoFit();

        //         oXL.Visible = true;
        //         oXL.UserControl = true;

        //         oRng = null;
        //         oSheet = null;
        //         oWB = null;
        //         oXL = null;
        //     }


        //     public void ExportDBGridView(DBGridView grid)
        //     {
        //         Microsoft.Office.Interop.Excel.Application oXL = new Microsoft.Office.Interop.Excel.Application();
        //         Workbook oWB = null;
        //         Worksheet oSheet = null;
        //         Range oRng = null;
        //         int f = 0, g = 0;

        //         //oXL = ((Microsoft.Office.Interop.Excel.Application)(FSLibrary.Functions.CreateObject("Excel.Application", "")));
        //         oXL.Visible = false;

        //         oWB = oXL.Workbooks.Add(null);
        //         oSheet = ((Worksheet)(oWB.ActiveSheet));

        //         oSheet.Cells[1, 1] = grid.CaptionText; //.Value
        //         //oSheet.Cells[ 1, 1 ].Font.Bold = true; 
        //         //oSheet.Cells[ 1, 1 ].Font.Size = 12; 
        //         //oSheet.Cells[ 1, 1 ].BorderAround(); 

        //         for (f = 0; f <= grid.Columns.Count - 1; f++)
        //         {
        //             oSheet.Cells[3, f + 2] = grid.Columns[f].HeaderCaption; // .Value
        //             //oSheet.Cells[ 3, f + 2 ].VerticalAlignment = DBGlobal.func.ConvertHorizontalAlignmentToExcelAlign( Columns[ f ].Alignment ); 
        //         }

        //         Range cell1 = oSheet.get_Range("B3", "IV3");
        //         cell1.Font.Bold = true;
        //         cell1.VerticalAlignment = XlVAlign.xlVAlignCenter;


        //         for (f = 0; f <= grid.DataControl.DataTable.Rows.Count - 1; f++)
        //         {
        //             for (g = 0; g <= grid.Columns.Count - 1; g++)
        //             {
        //                 if (grid.Columns[g].ColumnType == FSFormControls.DBColumn.ColumnTypes.ComboColumn)
        //                 {
        //                     oSheet.Cells[f + 4, g + 2] = grid.Columns[g].ColumnDBControl.Find(grid.Columns[g].ColumnDBFieldData,
        //                                                                                  Convert.ToString(
        //                                                                                      grid.get_RowValue(
        //                                                                                          grid.Columns[g].FieldDB, -1)),
        //                                                                                  grid.Columns[g].ComboListField);
        //                     //.Value
        //                 }
        //                 else
        //                 {
        //                     oSheet.Cells[f + 4, g + 2] = grid.get_RowValue(grid.Columns[g].FieldDB, f); //.Value
        //                 }
        //             }
        //         }

        //         oRng = oSheet.get_Range("B3", "IV3");
        //         oRng.EntireColumn.AutoFit();

        //         oXL.Visible = true;
        //         oXL.UserControl = true;

        //         oRng = null;
        //         oSheet = null;
        //         oWB = null;
        //         oXL = null;
        //     }
    }
}