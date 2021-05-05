using FSDatabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSPortalWPF.clases
{
    class funciones
    {
        public void FillListView(System.Windows.Controls.ListView lv, System.Windows.Controls.GridView gv, string sql)
        {
            BdUtils db = new BdUtils("FSConnection");
            System.Windows.Data.Binding bind = new System.Windows.Data.Binding();
           
            lv.View = gv;

            lv.DataContext = db.Execute(sql);
            lv.SetBinding(System.Windows.Controls.ListView.ItemsSourceProperty, bind);
        }
    }
}
