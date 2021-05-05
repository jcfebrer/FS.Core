// <fileheader>
// <copyright file="limpiarLog.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: admin\limpiarLog.aspx.cs
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
    public class LimpiarLog : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            StringBuilder sb = new StringBuilder("");

            try
            {
                if (Utils.BDType == Utils.TypeBd.SQLServer)
                {
                    sb.Append(db.ClearLog()
                        ? "Registro de transacciones inicializado."
                        : "Problemas al inicializar el registro de transacciones.");
                }
                else
                {
                    sb.Append("La conexión a la base de datos no esta definida como SQLServer. ProviderName: " +
                              db.ProviderName);
                }
            }
            catch (System.Exception e)
            {
                sb.Append(e);
            }

            return sb.ToString();
        }
    }
}