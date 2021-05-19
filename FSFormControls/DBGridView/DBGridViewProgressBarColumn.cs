using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FSFormControls.UserControls.DBGridView;

namespace FSFormControls.UserControls.DBGridView
{
    public class DBGridViewProgressBarColumn : DataGridViewImageColumn
    {
        public DBGridViewProgressBarColumn()
        {
            CellTemplate = new DBGridViewProgressBarCell();
        }
    }
}