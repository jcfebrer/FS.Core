using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace FSPortalWPF
{
	public partial class modPaginas
	{
        FSPortal.Portal portal = new FSPortal.Portal();
        //@"Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=False;Data Source=k:\wwwroot\data\fsfebrer\portalnet.mdb", "System.Data.OleDb");

		public modPaginas()
		{
			InitializeComponent();

            FSPortalWPF.clases.funciones f = new FSPortalWPF.clases.funciones();

            System.Windows.Controls.GridView gridview = new System.Windows.Controls.GridView();
            
            System.Windows.Controls.GridViewColumn gvcolumn = new GridViewColumn();
            gvcolumn.Header = "Título";
            gvcolumn.DisplayMemberBinding = new Binding("titulo");

            gridview.Columns.Add(gvcolumn);

            f.FillListView(this.grdPaginas, gridview, "select titulo from paginas");
		}

	}
}