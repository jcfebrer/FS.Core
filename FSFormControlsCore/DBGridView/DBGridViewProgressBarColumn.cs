using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FSFormControlsCore.UserControls.DBGridView;

namespace FSFormControlsCore.UserControls.DBGridView
{
    public class DBGridViewProgressBarColumn : DataGridViewImageColumn
    {
        public DBGridViewProgressBarColumn()
        {
            CellTemplate = new DBGridViewProgressBarCell();
        }
    }
}