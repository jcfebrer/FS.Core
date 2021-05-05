// <fileheader>
// <copyright file="compactar.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: admin\compactar.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Text;
using FSPortal;
using FSDatabase;

namespace FSPaginas.Admin
{
    public class Compactar : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            StringBuilder s = new StringBuilder("");

            s.Append(Ui.Lf() + "<p class='cabepeque'>Compactar y reparar</p>");

            Response.Buffer = false;
            if (Utils.BDType == Utils.TypeBd.SQLServer || Utils.BDType == Utils.TypeBd.MySQL)
            {
                s.Append("El servidor es SQL, no hace falta compactar ni reparar." + Ui.Lf());
            }
            else
            {
                s.Append("Compactando y reparando, por favor espere ..." + Ui.Lf());

				string err = "Función CompactJetDatabase inexistente.";
                //string err = db.CompactJetDatabase(db.PathDb());
                if (err != "")
                {
                    s.Append(Ui.Lf() + "Finalizado correctamente.");
                }
                else
                {
                    s.Append(Ui.Lf() + "Problemas al realizar la operación. Error: " + err);
                }
            }

            s.Append(Ui.Lf() + Ui.Lf());

            return s.ToString();
        }
    }
}