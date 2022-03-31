using System;
using System.Threading;
using System.Windows.Forms;

namespace FSFormControls
{
    public class FunctionsForms
    {
        public static DBColumn.ColumnTypes ConvertFieldTypeToColumnType(Type fieldType)
        {
            switch (fieldType.ToString().ToLower())
            {
                case "system.int16":
                case "system.int32":
                case "system.int64":
                case "system.double":
                case "system.single":
                case "system.byte":
                case "system.decimal":
                    return DBColumn.ColumnTypes.NumberColumn;
                case "system.datetime":
                    return DBColumn.ColumnTypes.DateColumn;
                case "system.char":
                case "system.string":
                    return DBColumn.ColumnTypes.TextColumn;
                case "system.boolean":
                    return DBColumn.ColumnTypes.CheckColumn;
            }

            return DBColumn.ColumnTypes.TextColumn;
        }

        public static DBColumn.ColumnTypes ConvertFieldTypeToColumnType(FSDatabase.Utils.FieldTypeEnum fieldType)
        {
            switch (fieldType)
            {
                case FSDatabase.Utils.FieldTypeEnum.Number:
                    return DBColumn.ColumnTypes.NumberColumn;
                case FSDatabase.Utils.FieldTypeEnum.DateTime:
                    return DBColumn.ColumnTypes.DateColumn;
                case FSDatabase.Utils.FieldTypeEnum.String:
                    return DBColumn.ColumnTypes.TextColumn;
                case FSDatabase.Utils.FieldTypeEnum.Boolean:
                    return DBColumn.ColumnTypes.CheckColumn;
            }

            return DBColumn.ColumnTypes.TextColumn;
        }

        public static void TabpageHide(TabControl tabcontrol, int pageNumber)
        {
            if (tabcontrol.Controls.Contains(tabcontrol.TabPages[pageNumber]))
                tabcontrol.Controls.Remove(tabcontrol.TabPages[pageNumber]);
        }


        public static void TabpageHide(TabControl tabcontrol, TabPage tabPage)
        {
            if (tabcontrol.Controls.Contains(tabPage)) tabcontrol.Controls.Remove(tabPage);
        }


        public static void TabpageShow(TabControl tabcontrol, TabPage tabpage)
        {
            if (!tabcontrol.Controls.Contains(tabpage))
            {
                tabcontrol.Controls.Add(tabpage);

                tabpage.BindingContext = tabcontrol.BindingContext;

                foreach (Control c in tabpage.Controls) c.BindingContext = tabcontrol.BindingContext;
            }
        }


        public static void TabpageShow(TabControl tabcontrol, int pageNumber)
        {
            if (!tabcontrol.Controls.Contains(tabcontrol.TabPages[pageNumber]))
            {
                tabcontrol.Controls.Add(tabcontrol.TabPages[pageNumber]);
                tabcontrol.TabPages[pageNumber].BindingContext = tabcontrol.BindingContext;

                foreach (Control c in tabcontrol.TabPages[pageNumber].Controls)
                    c.BindingContext = tabcontrol.BindingContext;
            }
        }


        public static bool TabPageExist(TabControl tabcontrol, TabPage tabPage)
        {
            if (tabcontrol.Controls.Contains(tabPage))
                return true;
            return false;
        }


        public static bool TabPageExist(TabControl tabcontrol, int pageNumber)
        {
            if (tabcontrol.Controls.Contains(tabcontrol.TabPages[pageNumber]))
                return true;
            return false;
        }


        public static void WaitCursor()
        {
            Cursor.Current = Cursors.WaitCursor;
        }


        public static void NormalCursor()
        {
            Cursor.Current = Cursors.Default;
        }


        public static DBTextBox.TypeData ConvertDataTypeToColumnType(DBColumn.ColumnTypes columtype)
        {
            switch (columtype)
            {
                case DBColumn.ColumnTypes.DateColumn:
                    return DBTextBox.TypeData.Date;
                case DBColumn.ColumnTypes.FormulaColumn:
                    return DBTextBox.TypeData.Numeric;
                case DBColumn.ColumnTypes.MoneyColumn:
                    return DBTextBox.TypeData.Money;
                case DBColumn.ColumnTypes.NumberColumn:
                    return DBTextBox.TypeData.Numeric;
                case DBColumn.ColumnTypes.PercentColumn:
                    return DBTextBox.TypeData.Pecentage;
                case DBColumn.ColumnTypes.TimeColumn:
                    return DBTextBox.TypeData.Time;
                default:
                    return DBTextBox.TypeData.All;
            }
        }


        public static void Center(Form frm)
        {
            frm.Left = Convert.ToInt32((Screen.PrimaryScreen.Bounds.Width - frm.Width) / 2);
            frm.Top = Convert.ToInt32((Screen.PrimaryScreen.Bounds.Height - frm.Height) / 2);
        }


        public static TreeNode[] ConvertStringArrToNodeArr(string[] str)
        {
            TreeNode[] n = null;
            var f = 0;

            if (str == null) return null;

            n = new TreeNode[str.Length - 1];

            foreach (var s in str)
            {
                n[f] = new TreeNode(s);
                f = f + 1;
            }

            return n;
        }


        public static int SearchNode(TreeNodeCollection nodes, string text)
        {
            var pos = -1;
            foreach (TreeNode n in nodes)
                if (n.Text == text)
                {
                    pos = n.Index;
                    break;
                }

            return pos;
        }


        public static object FindControlType(Control.ControlCollection frm, string strType)
        {
            string typ = null;

            if (frm == null) return null;

            foreach (Control ctr in frm)
            {
                typ = ctr.GetType().ToString();
                if (ctr is DBPanel | ctr is Panel | ctr is TabControl | ctr is TabPage | ctr is DBTabControl |
                    ctr is DBTabPage |
                    ctr is GroupBox)
                    return FindControlType(ctr.Controls, strType);

                if (typ == strType) return ctr;
            }

            return null;
        }


        public static object GetControlByName(Control.ControlCollection frm, string name)
        {
            string nam = null;

            if (frm == null) return null;

            foreach (Control ctr in frm)
            {
                nam = ctr.Name.ToLower();
                if (ctr is DBPanel | ctr is Panel | ctr is DBRecord | ctr is TabControl | ctr is TabPage |
                    ctr is DBTabControl |
                    ctr is DBTabPage | ctr is GroupBox)
                    return GetControlByName(ctr.Controls, name);

                if (nam == name.ToLower()) return ctr;
            }

            return null;
        }

        public static object GetControlByDBField(Control.ControlCollection frm, string dbfield)
        {
            if (frm == null) return null;

            foreach (Control ctr in frm)
                if (IsContainer(ctr))
                {
                    return GetControlByDBField(ctr.Controls, dbfield);
                }
                else
                {
                    if (ctr is DBTextBox && ((DBTextBox)ctr).DBField.ToLower() == dbfield.ToLower())
                        return ctr;
                    if (ctr is DBCombo && ((DBCombo)ctr).DBField.ToLower() == dbfield.ToLower())
                        return ctr;
                    if (ctr is DBCheckBox && ((DBCheckBox)ctr).DBField.ToLower() == dbfield.ToLower())
                        return ctr;
                    if (ctr is DBDate && ((DBDate)ctr).DBField.ToLower() == dbfield.ToLower())
                        return ctr;
                    if (ctr is DBFindTextBox && ((DBFindTextBox)ctr).DBField.ToLower() == dbfield.ToLower())
                        return ctr;
                    if (ctr is DBFile && ((DBFile)ctr).DBField.ToLower() == dbfield.ToLower())
                        return ctr;
                }

            return null;
        }

        public static void showSplash()
        {
            var SplashScreen = new SplashScreen();
            SplashScreen.ShowSplashScreen();
            SplashScreen.SetStatus("Loading module 1");
            Thread.Sleep(500);
            SplashScreen.SetStatus("Loading module 2");
            Thread.Sleep(300);
            SplashScreen.SetStatus("Loading module 3");
            Thread.Sleep(900);
            SplashScreen.SetStatus("Loading module 4");
            Thread.Sleep(100);
            SplashScreen.SetStatus("Loading module 5");
            Thread.Sleep(400);
            SplashScreen.SetStatus("Loading module 6");
            Thread.Sleep(50);
            SplashScreen.SetStatus("Loading module 7");
            Thread.Sleep(240);
            SplashScreen.SetStatus("Loading module 8");
            Thread.Sleep(900);
            SplashScreen.SetStatus("Loading module 9");
            Thread.Sleep(240);
            SplashScreen.SetStatus("Loading module 10");
            Thread.Sleep(90);
            SplashScreen.SetStatus("Loading module 11");
            Thread.Sleep(1000);
            SplashScreen.SetStatus("Loading module 12");
            Thread.Sleep(100);
            SplashScreen.SetStatus("Loading module 13");
            Thread.Sleep(500);
            SplashScreen.SetStatus("Loading module 14");
            Thread.Sleep(1000);
            SplashScreen.SetStatus("Loading module 15");
            Thread.Sleep(20);
            SplashScreen.SetStatus("Loading module 16");
            Thread.Sleep(450);
            SplashScreen.SetStatus("Loading module 17");
            Thread.Sleep(240);
            SplashScreen.SetStatus("Loading module 18");
            Thread.Sleep(90);
        }

        public static bool IsContainer(object ctr)
        {
            if (ctr is DBPanel | ctr is Panel | ctr is DBRecord | ctr is TabControl | ctr is TabPage |
                ctr is DBTabControl |
                ctr is DBTabPage | ctr is GroupBox | ctr is DBGroupBoxXP | ctr is DBGroupBoxXPList |
                ctr is SplitContainer | ctr is SplitterPanel | ctr is DBFrame)
                return true;
            return false;
        }
    }
}