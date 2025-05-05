using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using static FSFormControls.DBGridView;

namespace FSFormControls
{
    public class DBGridViewRowCollection : DataGridViewRowCollection
    {
        public DBGridViewRowCollection(DataGridView dataGridView) : base(dataGridView)
        {
        }

        public new DBGridViewRow this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), "Index out of range");
                }
                return (DBGridViewRow)base[index];
            }
        }

        public List<DBGridViewFilter> ColumnFilters { get; set; }
    }
}