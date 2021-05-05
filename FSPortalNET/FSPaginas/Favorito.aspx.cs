// <fileheader>
// <copyright file="favorito.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: favorito.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Text;
using FSPortal;
using FSDatabase;
using FSNetwork;

namespace FSPaginas
{
    public class Favorito : BasePage
    {
        public bool bErr;
        public string sComments = "";
        public string sErr = "";
        public string sTitulo = "";


        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            StringBuilder sb = new StringBuilder("");
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            bErr = false;
            if (Web.Request("cmdSend") != "")
            {
                sTitulo = Web.Request("txtTitulo");
                sComments = Web.Request("txtComments");
                if (sTitulo == "")
                {
                    sErr = "Debes introducir un título.";
                    bErr = true;
                }
                if (!(bErr))
                {
                    sErr = db.ExecuteNonQuery("insert into favoritos (titulo,url,comentarios,usuarioid) values ('" +
                                              sTitulo + "','" + Web.Request("comeback") + "','" + sComments + "'," +
                                              Variables.User.UsuarioId + ")") != 0 ? "Registro añadido." : "Problemas al añadir.";
                    sb.Append(Formulario());
                }
                else
                {
                    sb.Append(Formulario());
                }
            }
            else
            {
                sb.Append(Formulario());
            }

            return sb.ToString();
        }


        public string Formulario()
        {
            StringBuilder sb = new StringBuilder("");

            sb.Append("\r\n" + Ui.Lf());
            sb.Append("\r\n" + "<p class='cabepeque'>");
            sb.Append("\r\n" + "Añadir favorito");
            sb.Append("\r\n" + "</p>");
            sb.Append("\r\n" + "Añadir Página a tu lista de favoritos");

            sb.Append("\r\n" + Ui.Lf() + Ui.Lf() + Ui.Lf() + "Página recomendada: " + Web.Request("comeback"));
            sb.Append("\r\n" + @"<p class=""error"">");
            sb.Append("\r\n" + sErr);
            sb.Append("\r\n" + "</p>");
            sb.Append("\r\n" + @"<form action=""favorito.aspx?send=1"" method=""post"">");
            sb.Append("\r\n" + @"<input type=""hidden"" name=""comeback"" value=""" + Web.Request("comeback") +
                      @"""/>");
            sb.Append("\r\n" + @"<table border=""0"">");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>Titulo</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtTitulo"" class='textboxplano' value=""" + sTitulo +
                      @"""></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(288) + "</td>");
            sb.Append("\r\n" + @"<td><textarea cols=40 rows=5 name=""txtComments"" class='textboxplano'>" + sComments +
                      "</textarea></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td>&nbsp;</td>");
            sb.Append("\r\n" +
                      @"<td><input type='submit' name=""cmdSend"" value="" Guardar favorito "" class='botonplano' /></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "</table>");
            sb.Append("\r\n" + "</form>");
            sb.Append("\r\n" + "</p>");
            sb.Append("\r\n" + Ui.Lf() + Ui.Lf());

            return sb.ToString();
        }
    }
}