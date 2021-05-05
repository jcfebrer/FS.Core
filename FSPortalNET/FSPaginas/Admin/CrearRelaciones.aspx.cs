// <fileheader>
// <copyright file="crearRelaciones.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: admin\crearRelaciones.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using System.Text;
using FSPortal;
using FSDatabase;

namespace FSPaginas.Admin
{
    /// <summary>
    ///     Crea las relaciones de la base de datos OLEDB en una tabla llamada relaciones
    /// </summary>
    public class CrearRelaciones : BasePage
    {
        /// <summary>
        ///     Carga de la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                if (Utils.BDType == Utils.TypeBd.Oledb)
                {
                    DataTable dtSchemaFk = db.GetSchemaForeignKeys();

                    if (dtSchemaFk.Rows.Count > 0)
                    {
                        db.ExecuteNonQuery("delete * from " + Variables.App.prefijoTablas + "relaciones");

                        foreach (DataRow row in dtSchemaFk.Rows)
                        {
                        	Register reg = new Register();
                            
                        	reg.Add(new Field("FK_TABLE_NAME", row["FK_TABLE_NAME"].ToString(), typeof (String)));
                        	reg.Add(new Field("FK_Column_Name", row["FK_Column_Name"].ToString(), typeof (String)));
                        	reg.Add(new Field("PK_TABLE_NAME", row["PK_TABLE_NAME"].ToString(), typeof (String)));
                        	reg.Add(new Field("PK_Column_Name", row["PK_Column_Name"].ToString(), typeof (String)));
                            

                            if (db.ExecuteNonQuery(db.InsertSql("relaciones", reg, Variables.User.UsuarioId)) != 0)
                            {
                                sb.Append("Relación añadida: " + row["FK_TABLE_NAME"] + "-" + row["FK_Column_Name"] +
                                          Ui.Lf());
                            }
                            else
                            {
                                sb.Append("ERROR al añadir: " + row["FK_TABLE_NAME"] + "-" + row["FK_Column_Name"] +
                                          Ui.Lf());
                            }
                        }
                    }
                    else
                    {
                        sb.Append("No hay relaciones definidas en la base de datos. GetSchemaForeignKeys.rows=0");
                    }
                }
                else
                {
                    sb.Append("La conexión a la base de datos no esta definida como OLEDB. ProviderName: " +
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