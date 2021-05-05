using FSDatabase;
using FSException;
using FSPortal;
using System;
using System.Data;
using System.Web.Services;

namespace FSPaginas.Xml
{
    /// <summary>
    /// Descripción breve de WebService1
    /// </summary>
    [WebService(Namespace = "http://febrersoftware.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class AdminWS : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public string Tablas()
        {
            try
            {
                if (Variables.User.Administrador)
                {
                    if (Variables.App.UseXML)
                    {
                        XML xml = new XML(Variables.App.directorioWeb + "data");
                        return FSDatabase.Json.DataTableToJson(xml.GetSchemaTables());
                    }
                    else
                    {
                        BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
                        return FSDatabase.Json.DataTableToJson(db.GetSchemaTables(true, false));
                    }
                }
                else
                {
                    return "Error: No dispones de los permisos necesarios.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        [WebMethod(EnableSession = true)]
        public string EsquemaTabla(string nombreTabla)
        {
            try
            {
                if (Variables.User.Administrador)
                {
                    if (Variables.App.UseXML)
                    {
                        XML xml = new XML(Variables.App.directorioWeb + "data");
                        return FSDatabase.Json.DataTableToJson(xml.GetSchema(nombreTabla));
                    }
                    else
                    {
                        BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
                        return FSDatabase.Json.DataTableToJson(db.GetSchemaTable(nombreTabla));
                    }
                }
                else
                {
                    return "Error: No dispones de los permisos necesarios.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        [WebMethod(EnableSession = true)]
        public string Tabla(string nombreTabla, int page)
        {
            try
            {
                if (Variables.User.Administrador)
                {
                    if (Variables.App.UseXML)
                    {
                        XML xml = new XML(Variables.App.directorioWeb + "data");
                        xml.Load(nombreTabla + ".xml");
                        return FSDatabase.Json.DataTableToJson(Utils.SplitDatatable(xml.DataTable, page, Variables.App.registrosPorPagina));
                    }
                    else
                    {
                        BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
                        return FSDatabase.Json.DataTableToJson(db.Execute("select * from " + Variables.App.prefijoTablas + nombreTabla, page, Variables.App.registrosPorPagina));
                    }
                }
                else
                {
                    return "Error: No dispones de los permisos necesarios.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        [WebMethod(EnableSession = true)]
        public string Selection(string sql, int page)
        {
            try
            {
                if (Variables.User.Administrador)
                {
                    if (Variables.App.UseXML)
                    {
                        XML xml = new XML(Variables.App.directorioWeb + "data");
                        xml.Load(Utils.GetTableName(sql) + ".xml");
                        return FSDatabase.Json.DataTableToJson(Utils.SplitDatatable(xml.Select(sql), page, Variables.App.registrosPorPagina));
                    }
                    else
                    {
                        BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
                        return FSDatabase.Json.DataTableToJson(db.Execute(sql, page, Variables.App.registrosPorPagina));
                    }
                }
                else
                {
                    return "Error: No dispones de los permisos necesarios.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        [WebMethod(EnableSession = true)]
        public string Usuarios()
        {
            try
            {
                if (Variables.User.Administrador)
                {
                    if (Variables.App.UseXML)
                    {
                        XML xml = new XML(Variables.App.directorioWeb + "data");
                        xml.Load("usuarios.xml");
                        return FSDatabase.Json.DataTableToJson(xml.DataTable);
                    }
                    else
                    {
                        BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
                        return FSDatabase.Json.DataTableToJson(db.Execute("select * from " + Variables.App.prefijoTablas + "usuarios"));
                    }
                }
                else
                {
                    return "Error: No dispones de los permisos necesarios.";
                }
            }
            catch(Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        [WebMethod(EnableSession = true)]
        public string Articulos(int page)
        {
            try
            {
                if (Variables.User.Administrador)
                {
                    if (Variables.App.UseXML)
                    {
                        XML xml = new XML(Variables.App.directorioWeb + "data");
                        xml.Load("articulos.xml");
                        return FSDatabase.Json.DataTableToJson(Utils.SplitDatatable(xml.DataTable, page, Variables.App.registrosPorPagina));
                    }
                    else
                    {
                        BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
                        return FSDatabase.Json.DataTableToJson(db.Execute("select * from " + Variables.App.prefijoTablas + "articulos", page, Variables.App.registrosPorPagina));
                    }
                }
                else
                {
                    return "Error: No dispones de los permisos necesarios.";
                }
            }
            catch(Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
