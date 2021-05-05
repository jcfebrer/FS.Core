using FSDatabase;
using FSNetwork;
using FSLibrary;
using FSPlugin;
using FSPortal;
using System.Data;

namespace FSCustomPlugin
{
    public class ModOfertasTrabajo : IPlugin
    {
        public string Execute(params string[] p)
        {
            return OfertasTrabajo();
        }

        public string Name
        {
            get { return "ModOfertasTrabajo"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string OfertasTrabajo()
        {
            string modOfertasTrabajoReturn = "";

            string sWhere = "";
            if (Web.Request("cat") != "Selecciona ..." & Web.Request("cat") != "")
            {
                sWhere = " and categoria=" + Web.Request("cat");
            }
            if (Web.Request("prov") != "Selecciona ..." & Web.Request("prov") != "")
            {
                sWhere = sWhere + " and provincia=" + Web.Request("prov");
            }
            if (Web.Request("pal") != "")
            {
                sWhere = sWhere + " and (titulo like '%" + Web.Request("pal") + "%' or textoCorto like '%" +
                         Web.Request("pal") + "%' or textoLargo like '%" + Web.Request("pal") +
                         "%' or descripcion like '%" + Web.Request("pal") + "%')";
            }

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            string ssql = "SELECT ofertaID,titulo,fechaInicio,textoCorto,textoLargo,prov,descripcion FROM " +
                          Variables.App.prefijoTablas + "v_OfertasTrabajo Where publicar=true and fechaInicio<=" + Utils.FormatShortDate(System.DateTime.Now) + " and fechaFin>=" +
                          Utils.FormatShortDate(System.DateTime.Now) + " " + sWhere + " ORDER BY FechaInicio DESC";


            DataTable dtOfertas = db.Execute(ssql);

            if (dtOfertas.Rows.Count == 0)
            {
                modOfertasTrabajoReturn = modOfertasTrabajoReturn + "No hay ofertas de trabajo.";
            }
            else
            {
                modOfertasTrabajoReturn = modOfertasTrabajoReturn +
                                          @"<form name=""frmOfertas"" method=""post"" action=""""><table><tr><td>";
                modOfertasTrabajoReturn = modOfertasTrabajoReturn +
                                          @"Cadena de búsqueda:</td><td><input type=""text"" name=""pal"" size=""35""></td></tr>";
                modOfertasTrabajoReturn = modOfertasTrabajoReturn +
                                          "<tr><td>Provincia:</td><td>{frmCombo(provincias,provincia,idprovincia,prov,prov)}</td></tr>";
                modOfertasTrabajoReturn = modOfertasTrabajoReturn +
                                          "<tr><td>Categoria:</td><td>{frmCombo(categorias_ofertastrabajo,descripcion,idCategoriasOfertas,cat,cat)}</td></tr>";
                modOfertasTrabajoReturn = modOfertasTrabajoReturn +
                                          @"<tr><td></td>&nbsp;<td><input type=""submit"" value=""Buscar""></td></tr></table></form>" +
                                          Ui.Lf();
            }

            foreach (DataRow row in dtOfertas.Rows)
            {
                modOfertasTrabajoReturn = modOfertasTrabajoReturn +
                                          @"<table cellpadding=""2"" cellspacing=""0"" width=""100%"" border=""0""><tr>";
                modOfertasTrabajoReturn = modOfertasTrabajoReturn + @"<td class=""textopeque"">";
                modOfertasTrabajoReturn = modOfertasTrabajoReturn + @"<img border=""0"" src='" + Variables.App.directorioPortal +
                                          "imagenes/bullet.gif' alt='' />";

                modOfertasTrabajoReturn = modOfertasTrabajoReturn + "&nbsp;" +
                                          Ui.EditPage("OfertasTrabajo", "OfertaID", row["OfertaID"].ToString(),
                                              "Editar Oferta", "Borrar Oferta");

                if (Functions.Valor(row["textoLargo"]) != "")
                {
                    modOfertasTrabajoReturn = modOfertasTrabajoReturn + @"&nbsp;<b><a href=""" + Variables.App.directorioPortal +
                                              "p_detalle Oferta.aspx?id=" + row["ofertaID"] + @""">" + row["Titulo"] +
                                              " - " + row["descripcion"] + "</a></b>";
                }
                else
                {
                    modOfertasTrabajoReturn = modOfertasTrabajoReturn + "&nbsp;<b>" + row["Titulo"] + " - " +
                                              row["descripcion"] + "</b>";
                }

                modOfertasTrabajoReturn = modOfertasTrabajoReturn + @"</td><td align=""right"">";
                modOfertasTrabajoReturn = modOfertasTrabajoReturn +
                                          FSLibrary.DateTimeUtil.ShortDate(DateTimeUtil.ValorFecha(row["FechaInicio"].ToString()));
                modOfertasTrabajoReturn = modOfertasTrabajoReturn +
                                          @"</td></tr><tr><td class=""textomaspeque"" colspan=""2"">";
                modOfertasTrabajoReturn = modOfertasTrabajoReturn + row["TextoCorto"] + Ui.Lf() + row["prov"] + Ui.Lf() +
                                          "<hr />";
                modOfertasTrabajoReturn = modOfertasTrabajoReturn + "</td></tr></table>";
                modOfertasTrabajoReturn = modOfertasTrabajoReturn + Ui.Lf();
            }

            if (Variables.User.Administrador)
            {
                modOfertasTrabajoReturn = modOfertasTrabajoReturn + Ui.Lf() + Ui.Lf() +
                                          Ui.Link("Añadir oferta de trabajo",
                                              Variables.App.directorioPortal +
                                              "admin/editor/showrecord.aspx?tablename=OfertasTrabajo&amp;add=1&amp;page=1");
            }

            return modOfertasTrabajoReturn;
        }
    }
}