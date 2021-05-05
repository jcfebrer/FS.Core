// <fileheader>
// <copyright file="buscar.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: buscar.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using System.Text;
using FSPortal;
using FSLibrary;
using FSQueryBuilder;
using FSDatabase;
using FSNetwork;

namespace FSPaginas
{
    public class Buscar : BasePage
    {
        private readonly Modulos modulos = new Modulos();
        public bool bNoTemaId;
        public int iProject;

        public int iResultCount;
        public int iSearchType;
        public string sSearch;

        public string sTableDotField;


        /// <summary>
        ///     Carga de la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        public string Inicio()
        {
            StringBuilder sb = new StringBuilder("");

            sb.Append(Ui.Lf());
            sb.Append("<p class='cabepeque'>");
            sb.Append("/ :: ");

            sb.Append(FuncionesWeb.Idioma(103));
            sb.Append("</p>");

            InitSearch();

            if (Request.QueryString["cmdSearch"] == "" || sSearch.Length == 0)
            {
                sb.Append(ShowSearchForm());
            }
            else
            {
                sb.Append(ShowResults());
                sb.Append(ShowSummary());
                sb.Append(ShowSearchForm());
                sb.Append(LogSearch());
            }

            sb.Append(Ui.Lf() + Ui.Lf());

            return sb.ToString();
        }

        public string ShowSearchForm()
        {
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            StringBuilder sb = new StringBuilder("");

            sb.Append("\r\n" + @"<form action=""buscar.aspx?cmdSearch=1"" method=""post"">");
            sb.Append("\r\n" + @"<table border=""0"" cellpadding=""2"" cellspacing=""2"">");
            sb.Append("\r\n" + "<tr><td>");
            sb.Append("\r\n" + FuncionesWeb.Idioma(322));
            sb.Append("\r\n" + "</td></tr>");
            sb.Append("\r\n" + "<tr><td>");
            sb.Append("\r\n" + @"<input type=""text"" name=""txtSearch"" value=""" + sSearch +
                      @""" size=""50"" maxlength=""250"" class=""textboxplano"" />");
            sb.Append("\r\n" + @"<input type=""submit"" name=""cmdSearch"" value=""" + FuncionesWeb.Idioma(103) +
                      @""" class=""botonplano"" />");
            sb.Append("\r\n" + "</td></tr><tr><td>");
            sb.Append("\r\n" + @"<table border=""0"">");
            sb.Append("\r\n" + "<tr><td>");
            sb.Append("\r\n" + FuncionesWeb.Idioma(323) + ":");
            sb.Append("\r\n" + "</td><td>");

            string[] aTypes = new string[4];
            aTypes[1] = FuncionesWeb.Idioma(324);
            aTypes[2] = FuncionesWeb.Idioma(325);
            aTypes[3] = FuncionesWeb.Idioma(326);


            sb.Append("\r\n" + @"<select name=""cboType"" class=""textboxplano"">");
            
            for (int iType = 1; iType <= 3; iType++)
            {
                sb.Append("\r\n" + @"<option value=""" + iType + @"""");
                if (iSearchType == iType)
                {
                    sb.Append(@" selected=""selected""");
                }
                sb.Append(">" + aTypes[iType] + "</option>");
            }

            sb.Append("\r\n" + "</select>");
            sb.Append("\r\n" + "</td></tr>");

            if (modulos.ModuloActivo("modTemas"))
            {
                sb.Append("\r\n" + "<tr><td>");
                sb.Append("\r\n" + "Buscar en");
                sb.Append("\r\n" + "</td><td>");
                sb.Append("\r\n" + "<select name='cboProject' class='textboxplano'>");
                sb.Append("\r\n" + "<option value='0'>Todos los temas</option>");

                SelectQueryBuilder sqB = new SelectQueryBuilder();
                sqB.Columns.SelectColumns("temaid", "nombre");
                sqB.TableSource = Variables.App.prefijoTablas + "Temas";

                DataTable dt = db.Execute(sqB.BuildQuery());
                foreach (DataRow row in dt.Rows)
                {
                    sb.Append("\r\n" + @"<option value=""" + row["temaid"] + @"""");
                    if (iProject == Convert.ToInt32(row["temaid"]))
                    {
                        sb.Append(@" selected=""selected""");
                    }
                    sb.Append(">" + row["nombre"] + "</option>");
                }

                sb.Append("\r\n" + "</select>");
                sb.Append("\r\n" + "</td></tr>");
            }

            sb.Append("\r\n" + "</table>");
            sb.Append("\r\n" + "</td></tr>");
            sb.Append("\r\n" + "</table>");
            sb.Append("\r\n" + "</form>");

            return sb.ToString();
        }


        public string ShowResults()
        {
            StringBuilder sb = new StringBuilder("");
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            if (modulos.ModuloActivo("modTemas"))
            {
                sb.Append("\r\n" + Show("Temas", "Nombre,Descripcion", "temaid", "temas/tema.aspx?pid=", ""));
                sb.Append("\r\n" + ShowDocuments());
                sb.Append("\r\n" + ShowFeedbacks());
            }

            if (modulos.ModuloActivo("modTienda"))
            {
                sb.Append("\r\n" +
                          Show("Articulos", "titulo,Descripcion", "idArticulo", "tienda/detalleArticulo2.aspx?id=", ""));
            }

            if (modulos.ModuloActivo("modNoticias"))
            {
                sb.Append("\r\n" +
                          Show("Noticias", "titulo,textocorto,textolargo", "noticiaId", "detalleNoticia.aspx?id=",
                              " and publicar=true order by FechaInicio DESC"));
            }

            if (modulos.ModuloActivo("modOfertasTrabajo"))
            {
                sb.Append("\r\n" + ShowOfertasTrabajo());
            }

            if (modulos.ModuloActivo("modEventosRecientes") | modulos.ModuloActivo("modProximosEventos"))
            {
                sb.Append("\r\n" +
                          Show("Eventos", "titulo,descripcion,fecha", "ideventos", "eventos/verEvento.aspx?id=",
                              "order by fecha"));
            }

            if (modulos.ModuloActivo("modEventosDeportivosRecientes") |
                modulos.ModuloActivo("modProximosEventosDeportivos"))
            {
                sb.Append("\r\n" +
                          Show("EventosDeportivos", "titulo,descripcion,lugar,fecha", "idEventos",
                              "eventos/verEventoDeportivo.aspx?id=", "order by fecha"));
            }

            if (db.TableExists(Variables.App.prefijoTablas + "Contactos"))
            {
                sb.Append("\r\n" +
                          Show("Contactos", "nombre,apellido1,apellido2,descripcion", "idContacto",
                              "p_detalle contacto.aspx?idc=", "order by fechaAlta"));
            }

            if (db.TableExists(Variables.App.prefijoTablas + "Clubs"))
            {
                sb.Append("\r\n" +
                          Show("Clubs", "nombre,descripcion", "idClub", "p_detalle club.aspx?idc=", "order by fechaAlta"));
            }

            sb.Append("\r\n" + ShowPages());
            return sb.ToString();
        }


        public string ShowHeader(string sHeaderText)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append("\r\n" + Ui.Lf());
            sb.Append("\r\n" + @"<table border=""0"" cellpadding=""0"" cellspacing=""2"" width=""100%"">");
            sb.Append("\r\n" + @"<tr><td class=""cabemaspeque"" bgcolor=""#fdf5e6"">" + sHeaderText + "</td></tr>");
            return sb.ToString();
        }


        public string ShowFooter()
        {
            return "</table>";
        }


        public string ShowItem(string sUrl, string sTitle, string editPage)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append("\r\n" + @"<tr><td class=""textomaspeque""><img border=""0"" src='" + Variables.App.directorioPortal +
                      "imagenes/bullet.gif' alt='' />&nbsp;" + editPage + Ui.Link(sTitle, sUrl) + "</td></tr>");
            return sb.ToString();
        }


        public string ConstructWhere(string sFields)
        {
            string sWhere = "";
            bool bFieldBegin = false;
            string sExpr = "";

            switch (iSearchType)
            {
                case 1:
                    sWhere = ConstructWhereByFields(sFields, " AND ");
                    break;
                case 2:
                    sWhere = ConstructWhereByFields(sFields, " OR ");
                    break;
                case 3:
                    string[] aFields = sFields.Split(',');
                    
                    for (int iField = 0; iField <= aFields.GetUpperBound(0); iField++)
                    {
                        if (bFieldBegin)
                        {
                            sExpr = " OR ";
                        }
                        sWhere = sWhere + sExpr + aFields[iField] + " LIKE '%" + sSearch + "%'";
                        bFieldBegin = true;
                    }
                    break;
            }

            sWhere = "(" + sWhere + ")";
            if ((iProject > 0 & bNoTemaId == false))
            {
                if (sTableDotField != "")
                {
                    sWhere = sWhere + " AND (" + sTableDotField + " = " + iProject + ")";
                }
                else
                {
                    sWhere = sWhere + " AND (TemaID = " + iProject + ")";
                }
            }
            
            return sWhere;
        }


        public string ConstructWhereByFields(string sFields, string sWordExpr)
        {
            string sWhere = "";
            string sExpr = "";

            string[] aFields = sFields.Split(',');
            string[] aWords = sSearch.Split(' ');

            bool bFieldBegin = false;
            
            for (int iField = 0; iField <= aFields.GetUpperBound(0); iField++)
            {
                if (bFieldBegin)
                {
                    sExpr = " OR ";
                }
                bool bWordBegin = false;
                
                for (int iWord = 0; iWord <= aWords.GetUpperBound(0); iWord++)
                {
                    if (bWordBegin == false)
                    {
                        sWhere = sWhere + sExpr + "(" + aFields[iField] + " LIKE '%" + aWords[iWord] + "%'";
                    }
                    else
                    {
                        sWhere = sWhere + sWordExpr + aFields[iField] + " LIKE '%" + aWords[iWord] + "%'";
                    }

                    bWordBegin = true;
                }
                bFieldBegin = true;
                sWhere = sWhere + ")";
            }
            
            return sWhere;
        }


        public string Show(string name, string fields, string idname, string linkUrl, string orderby)
        {
            StringBuilder sb = new StringBuilder("");


            string sFields = fields;
            string sWhere = ConstructWhere(sFields);

            string sSql = "SELECT " + fields + "," + idname + " FROM " + Variables.App.prefijoTablas + name + " WHERE " + sWhere +
                          " " + orderby;
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);


            DataTable dt = db.Execute(sSql);

            if (dt.Rows.Count > 0)
            {
                sb.Append("\r\n" + ShowHeader("@@@ :: " + name));
                sb.Append("\r\n" + @"<tr><td class=""textomaspeque"">");
                foreach (DataRow row in dt.Rows)
                {
                    string sUrl = linkUrl + row[idname];
                    string editPage = Ui.EditPage(name, idname, row[idname].ToString(), "Editar " + name, "Borrar " + name);
                    string sTitle = row[0].ToString();

                    sb.Append("\r\n" + ShowItem(sUrl, sTitle, editPage));
                    iResultCount = iResultCount + 1;
                }
                sb.Append("\r\n" + "</td></tr>");
                sb.Append("\r\n" + ShowFooter());
            }
            return sb.ToString();
        }


        public string ShowDocuments()
        {
            StringBuilder sb = new StringBuilder("");

            string sFields = Variables.App.prefijoTablas + "Documentos.Nombre, Cuerpo";
            sTableDotField = Variables.App.prefijoTablas + "Temas.TemaID";
            string sWhere = ConstructWhere(sFields);
            sTableDotField = "";

            string sSql = "SELECT " + Variables.App.prefijoTablas + "documentos.documentoid as documentoid," + Variables.App.prefijoTablas +
                          "temas.temaid as temaid, " + Variables.App.prefijoTablas + "temas.nombre as temasNombre," +
                          Variables.App.prefijoTablas + "documentos.nombre as docnombre FROM " + Variables.App.prefijoTablas +
                          "Documentos LEFT JOIN " + Variables.App.prefijoTablas + "Temas ON (" + Variables.App.prefijoTablas +
                          "Documentos.TemaID = " + Variables.App.prefijoTablas + "Temas.TemaID) WHERE " + sWhere;
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            DataTable dt = db.Execute(sSql);

            if (dt.Rows.Count > 0)
            {
                sb.Append("\r\n" + ShowHeader(" :: lecturas"));
                foreach (DataRow row in dt.Rows)
                {
                    string sUrl = "temas/leer.aspx?pid=" + row["temaid"] + "&amp;docid=" + row["DocumentoID"];
                    string editPage = Ui.EditPage("documentos", "documentoID", row["documentoID"].ToString(),
                        "Editar Documento", "Borrar Documento");
                    string sTitle = row["temasnombre"] + " :: " + row["docnombre"];

                    sb.Append("\r\n" + ShowItem(sUrl, sTitle, editPage));
                    iResultCount = iResultCount + 1;
                }
                sb.Append("\r\n" + ShowFooter());
            }
            return sb.ToString();
        }


        public string ShowFeedbacks()
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append("\r\n" + ShowBugs());
            sb.Append("\r\n" + ShowRecommends());
            sb.Append("\r\n" + ShowQuestions());
            sb.Append("\r\n" +
                      Show("Enlaces", "Titulo,Descripcion,URL", "enlaceId", "temas/enlaces.aspx?pid=",
                          "order by titulo desc"));
            return sb.ToString();
        }


        public string ShowBugs()
        {
            StringBuilder sb = new StringBuilder("");


            const string sFields = "Titulo,Descripcion";
            string sWhere = ConstructWhere(sFields);
            string sSql = "SELECT temaid,errorid,titulo FROM " + Variables.App.prefijoTablas + "Errores WHERE " + sWhere +
                          " ORDER BY FechaEnvio DESC";
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            DataTable dt = db.Execute(sSql);

            if (dt.Rows.Count > 0)
            {
                sb.Append("\r\n" + ShowHeader(" :: errores"));
                foreach (DataRow row in dt.Rows)
                {
                    string sUrl = "temas/errores.aspx?pid=" + row["temaid"] + "#" + row["ErrorID"];
                    string editPage = Ui.EditPage("Errores", "errorId", row["errorID"].ToString(), "Editar Error",
                        "Borrar Error");
                    string sTitle = row["Titulo"].ToString();

                    sb.Append("\r\n" + ShowItem(sUrl, sTitle, editPage));
                    iResultCount = iResultCount + 1;
                }
                sb.Append("\r\n" + ShowFooter());
            }
            return sb.ToString();
        }


        public string ShowRecommends()
        {
            StringBuilder sb = new StringBuilder("");


            const string sFields = "Titulo,Descripcion";
            string sWhere = ConstructWhere(sFields);

            string sSql = "SELECT temaid,recomendacionid,titulo FROM " + Variables.App.prefijoTablas + "Recomendaciones WHERE " +
                          sWhere + " ORDER BY FechaRecomendacion DESC";
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            DataTable dt = db.Execute(sSql);

            if (dt.Rows.Count > 0)
            {
                sb.Append("\r\n" + ShowHeader(" :: mejoras recomendadas"));
                foreach (DataRow row in dt.Rows)
                {
                    string sUrl = "temas/recomendaciones.aspx?pid=" + row["temaid"] + "#" + row["RecomendacionID"];
                    string editPage = Ui.EditPage("Recomendaciones", "recomendacionid", row["recomendacionid"].ToString(),
                        "Editar Recomendación", "Borrar Recomendación");
                    string sTitle = row["Titulo"].ToString();

                    sb.Append("\r\n" + ShowItem(sUrl, sTitle, editPage));
                    iResultCount = iResultCount + 1;
                }
                sb.Append("\r\n" + ShowFooter());
            }
            return sb.ToString();
        }


        public string ShowQuestions()
        {
            StringBuilder sb = new StringBuilder("");


            const string sFields = "Titulo,Descripcion";
            string sWhere = ConstructWhere(sFields);

            string sSql = "SELECT temaid,preguntaid,titulo FROM " + Variables.App.prefijoTablas + "Preguntas WHERE " + sWhere +
                          " ORDER BY FechaPregunta DESC";
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            DataTable dt = db.Execute(sSql);

            if (dt.Rows.Count > 0)
            {
                sb.Append("\r\n" + ShowHeader(" :: preguntas y respuestas"));
                foreach (DataRow row in dt.Rows)
                {
                    string sUrl = "temas/respuestas.aspx?pid=" + row["temaid"] + "&amp;qid=" + row["PreguntaID"];
                    string editPage = Ui.EditPage("Preguntas", "preguntaId", row["preguntaid"].ToString(), "Editar Pregunta",
                        "Borrar Pregunta");
                    string sTitle = row["Titulo"].ToString();

                    sb.Append("\r\n" + ShowItem(sUrl, sTitle, editPage));
                    iResultCount = iResultCount + 1;
                }
                sb.Append("\r\n" + ShowFooter());
            }
            return sb.ToString();
        }


        public string ShowOfertasTrabajo()
        {
            StringBuilder sb = new StringBuilder("");


            const string sFields = "titulo,textocorto,textolargo";
            bNoTemaId = true;
            string sWhere = ConstructWhere(sFields);
            bNoTemaId = false;

            string sSql = "SELECT ofertaid,titulo,fechaInicio,textoCorto FROM " + Variables.App.prefijoTablas +
                          "ofertasTrabajo WHERE " + sWhere + " and publicar=true order by FechaInicio DESC";
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            DataTable dt = db.Execute(sSql);

            if (dt.Rows.Count > 0)
            {
                sb.Append("\r\n" + ShowHeader(" :: ofertas de trabajo"));
                foreach (DataRow row in dt.Rows)
                {
                    string sUrl = "detalleOferta.aspx?id=" + row["ofertaId"];
                    string editPage = Ui.EditPage("OfertasTrabajo", "OfertaID", row["OfertaID"].ToString(), "Editar Oferta",
                        "Borrar Oferta");
                    string sTitle = row["fechaInicio"] + "-" + row["titulo"] + Ui.Lf() + row["textocorto"];

                    sb.Append("\r\n" + ShowItem(sUrl, sTitle, editPage));
                    iResultCount = iResultCount + 1;
                }
                sb.Append("\r\n" + ShowFooter());
            }
            return sb.ToString();
        }


        public string ShowPages()
        {
            StringBuilder sb = new StringBuilder("");


            const string sFields = "contenido,titulo";
            bNoTemaId = true;
            string sWhere = ConstructWhere(sFields);
            bNoTemaId = false;

            string sSql = "SELECT enlace,idPagina,titulo FROM " + Variables.App.prefijoTablas + "Paginas WHERE " + sWhere;
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            DataTable dt = db.Execute(sSql);

            if (dt.Rows.Count > 0)
            {
                sb.Append("\r\n" + ShowHeader(" :: páginas"));
                foreach (DataRow row in dt.Rows)
                {
                    string sUrl;
                    if (row["enlace"].ToString() != "")
                    {
                        sUrl = Functions.Valor(row["enlace"]);
                    }
                    else
                    {
                        sUrl = "pagina.aspx?id=" + Functions.Valor(row["idPagina"]);
                    }

                    string editPage = Ui.EditPage("Paginas", "idPagina", Functions.Valor(row["idPagina"]), "Editar Página",
                        "Borrar Página");
                    string sTitle = Functions.Valor(row["titulo"]);

                    sb.Append("\r\n" + ShowItem(sUrl, sTitle, editPage));
                    iResultCount = iResultCount + 1;
                }
                sb.Append("\r\n" + ShowFooter());
            }
            return sb.ToString();
        }


        public string ShowSummary()
        {
            StringBuilder sb = new StringBuilder("");
            if (iResultCount > 0)
            {
                sb.Append("\r\n" + @"<table border=""0"" width=""100%""><tr><td bgcolor=""#fdf5e6"">" +
                          "Encontrados <strong>" + iResultCount + "</strong> resultados." + "</td></tr></table>");
            }
            else
            {
                sb.Append("\r\n" + @"<table border=""0"" width=""100%""><tr><td bgcolor=""#fdf5e6"">" +
                          "Ningún registro encontrado." + "</td></tr></table>");
            }
            return sb.ToString();
        }


        public string LogSearch()
        {
            string sSql = "INSERT into " + Variables.App.prefijoTablas +
                          "Busquedas (textoBusqueda,tipo,temaid,fecha,hora,resultados,usuario) VALUES ('" + sSearch +
				"', '" + iSearchType + "'," + iProject + ",'" + FSLibrary.DateTimeUtil.ShortDate(System.DateTime.Now) + "','" +
				System.DateTime.Now.ToShortTimeString() + "', " + iResultCount + ",'" + Variables.User.Usuario + "')";
            StringBuilder sb = new StringBuilder("");

			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            if (db.ExecuteNonQuery(sSql) == 0)
            {
                sb.Append("\r\n" + "Imposible guardar LOG de búsquedas.");
            }

            return sb.ToString();
        }


        public void InitSearch()
        {
            sSearch = Web.Request("txtSearch");
            iSearchType = Web.RequestInt("cboType");
            iProject = Web.RequestInt("cboProject");

            if (iSearchType == 0)
            {
                iSearchType = 1;
            }
        }
    }
}