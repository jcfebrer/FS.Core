using System;
using FSPortal;
using System.Text;
using FSDatabase;

namespace FSPaginas.Varios
{
	public class AddFavorite : BasePage
	{
	    protected void Page_Load(object sender, EventArgs e)
	    {
            contenido = Inicio();
	    }

        string Inicio()
        {
            StringBuilder sb = new StringBuilder();
            string url = "";
            string group = "";
            string desc = "";

            if (Request["url"] != null) url = Request["url"].ToString();
            if (Request["group"] != null) group = Request["group"].ToString();
            if (Request["desc"] != null) desc = Request["desc"].ToString();

            if (url != "" && group != "" && desc != "")
            {
                BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
                db.Execute(string.Format("insert into favoritos (idgrupo,favorito,descripcion) values ('{0}','{1}','{2}')", group, url, desc));
                sb.AppendLine(@"Favorito """ + desc + @""" añadido a la lista.");
            }
            else
            {
                sb.AppendLine(String.Format("NO OK: {0},{1},{2}", url, group, desc));
            }

            return sb.ToString();
        }
	}
}