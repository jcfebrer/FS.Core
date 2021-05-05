// <fileheader>
// <copyright file="portalFlash.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: portalFlash.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using System.Xml;
using com.TheSilentGroup.Fluorine;
using FSPaginas.Admin;
using FSPortal;
using FSLibrary;
using FSQueryBuilder;
using FSQueryBuilder.Enums;
using FSQueryBuilder.QueryParts.Where;
using FSDatabase;
using FSNetwork;
using FSMail;

namespace FSPaginas
{
    /// <summary>
    ///     Clase para enlazar con Macromedia Flash mediante Flourine
    /// </summary>
    [RemotingService("ServicioFlash")]
    public class PortalFlash : BasePage
    {
        private readonly Portal portal = new Portal();

        /// <summary>
        ///     Grid1
        /// </summary>
        protected GridView GridView1;

        /// <summary>
        ///     Grid2
        /// </summary>
        protected GridView GridView2;

        /// <summary>
        ///     Textbox1
        /// </summary>
        protected TextBox TextBox1;

        private DataTable dtArbol;
        private int sp = 1;

        /// <summary>
        ///     Permite el acceso al portal
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Login(string username, string password)
        {
            try
            {
                portal.Login(username, password);
            }
            catch (System.Exception e)
            {
                return e.Message;
            }

            return "OK";
        }

        /// <summary>
        ///     Devuelve los datos del usuario en un datatable
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public DataTable UserData(string username, string password)
        {
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            SelectQueryBuilder sqB = new SelectQueryBuilder();
            sqB.Columns.SelectColumns("*");
            sqB.TableSource = Variables.App.prefijoTablas + "Usuarios";

			FSCrypto.Crypto crypt = new FSCrypto.Crypto();

            AndWhere aw = new AndWhere();
            aw.Add(new SimpleWhere(sqB.TableSource, "usuario", Comparison.Equals, username));
            aw.Add(new SimpleWhere(sqB.TableSource, "clave", Comparison.Equals, crypt.Md5(password)));

            sqB.Where = aw;

            DataTable dt = db.Execute(sqB.BuildQuery());

            return dt;
        }


        /// <summary>
        ///     Devuelve las noticias en un DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable Noticias()
        {
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            string ssql = "SELECT titulo,textoCorto,fechaInicio,imagen FROM " + Variables.App.prefijoTablas +
                          "Noticias Where publicar=true and fechaInicio<=" +
				Utils.FormatShortDate(System.DateTime.Now) + " and fechaFin>=" +
                Utils.FormatShortDate(System.DateTime.Now) + " ORDER BY FechaInicio DESC";
            return Selection(ssql);
        }


        /// <summary>
        ///     Método que permite Añadir un usuario al portal
        /// </summary>
        /// <param name="sUsuario"></param>
        /// <param name="sEMail"></param>
        /// <param name="sNombre"></param>
        /// <param name="sApellido1"></param>
        /// <param name="sApellido2"></param>
        /// <param name="sBirthMonth"></param>
        /// <param name="sBirthDay"></param>
        /// <param name="sBirthYear"></param>
        /// <param name="sNotifications"></param>
        /// <param name="sNewsletter"></param>
        /// <param name="sCountryCode"></param>
        /// <param name="sRemember"></param>
        /// <param name="sGenderMale"></param>
        /// <param name="sGenderFemale"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public string AddUser(string sUsuario, string sEMail, string sNombre, string sApellido1, string sApellido2,
            int sBirthMonth, int sBirthDay, int sBirthYear, bool sNotifications, bool sNewsletter, int sCountryCode,
            bool sRemember, bool sGenderMale, bool sGenderFemale, string sPassword)
        {
            try
            {
                portal.AddUser(sUsuario, sEMail, sNombre, sApellido1, sApellido2, sBirthMonth, sBirthDay, sBirthYear,
                    sNotifications, sNewsletter, sCountryCode, sPassword);
            }
            catch (System.Exception e)
            {
                return e.Message;
            }

            return "OK";
        }

        /// <summary>
        ///     Permite realizar una consulta SQL al portal, y devolverla como un DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable Selection(string sql)
        {
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            DataTable dt = db.Execute(sql);

            return dt;
        }


        /// <summary>
        ///     Devuelve un datatable con la estructura de la tabla indicada para realizar un INSERT
        /// </summary>
        /// <param name="tabla"></param>
        /// <returns></returns>
        public DataTable InsertRegister(string tabla)
        {
            DataTable dt = new DataTable();
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            DataColumn dtc = null;
            dtc = new DataColumn("campo", Type.GetType("System.String"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn("valor", Type.GetType("System.String"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn("tipo", Type.GetType("System.String"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn("size", Type.GetType("System.Int32"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn("tabla", Type.GetType("System.String"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn("autoNumerico", Type.GetType("System.Boolean"));
            dt.Columns.Add(dtc);

            DataTable dtTableColumns = db.GetSchemaTable(tabla);

            DataRow dtr = null;


            foreach (DataRow fld in dtTableColumns.Rows)
            {
                dtr = dt.NewRow();

                dtr["campo"] = fld["ColumnName"];
                dtr["valor"] = "";
                dtr["tipo"] = fld["DataType"];
                dtr["size"] = fld["ColumnSize"];
                dtr["tabla"] = tabla;
                dtr["autoNumerico"] = fld["IsAutoIncrement"];

                dt.Rows.Add(dtr);
            }

            return dt;
        }


        /// <summary>
        ///     Devuelve un DataTable con las estructura de la tabla de la SQL con los valores seleccionados
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable SelectionRegister(string sql)
        {
            string tabla = sql.Split(char.Parse(" "))[3]; //cogemos la tabla de la sentencia SQL: Select * from TABLA

			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            DataTable dt = new DataTable();

            DataColumn dtc = null;
            dtc = new DataColumn("campo", Type.GetType("System.String"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn("valor", Type.GetType("System.String"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn("tipo", Type.GetType("System.String"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn("size", Type.GetType("System.Int32"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn("tabla", Type.GetType("System.String"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn("autoNumerico", Type.GetType("System.Boolean"));
            dt.Columns.Add(dtc);

            DataTable dtTableColumns = db.GetSchemaTable(tabla);

            DataTable dtT = db.Execute(sql);

            DataRow dtr;

            foreach (DataRow fld in dtTableColumns.Rows)
            {
                dtr = dt.NewRow();

                dtr["campo"] = fld["ColumnName"];
                dtr["valor"] = Functions.Valor(dtT.Rows[0][fld["ColumnName"].ToString()]);
                dtr["tipo"] = fld["DataType"];
                dtr["size"] = fld["ColumnSize"];
                dtr["tabla"] = tabla;
                dtr["autoNumerico"] = fld["IsAutoIncrement"];

                dt.Rows.Add(dtr);
            }

            return dt;
        }


        /// <summary>
        ///     Permite la excución de sentencias SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string Execute(string sql)
        {
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            try
            {
                db.ExecuteNonQuery(sql);
            }
            catch (System.Exception e)
            {
                return e.Message;
            }

            return "OK";
        }

        /// <summary>
        ///     Devuelve el nombre completo del usuario que ha realizado Login.
        /// </summary>
        /// <returns></returns>
        public string NombreCompleto()
        {
            return Variables.User.NombreCompleto;
        }


        /// <summary>
        ///     Devuelve el ID del usuario que ha realizado Login.
        /// </summary>
        /// <returns></returns>
        public int UsuarioID()
        {
            return Variables.User.UsuarioId;
        }


        /// <summary>
        ///     Permite el envio de un correo
        /// </summary>
        /// <param name="sTo"></param>
        /// <param name="sSubject"></param>
        /// <param name="sBody"></param>
        /// <param name="sFrom"></param>
        /// <param name="sFromName"></param>
        /// <returns></returns>
        public string SendMail(string sTo, string sSubject, string sBody, string sFrom, string sFromName)
        {
            try
            {
				new SendMail().SendMailAsync(sTo, Variables.App.correoPrueba, Variables.App.correoCopia, sSubject, sBody, sFrom, sFromName, Variables.App.plantillaCorreo);
            }
            catch (System.Exception e)
            {
                return e.Message;
            }

            return "OK";
        }


        /// <summary>
        ///     Para probar que funciona la conexión con Flourine
        /// </summary>
        /// <returns></returns>
        public string HolaMundo()
        {
            return "Hola Mundo!!";
        }

        /// <summary>
        ///     Devuelve un DataTable con la tabla Artículos
        /// </summary>
        /// <returns></returns>
        public DataTable Articulos()
        {
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            SelectQueryBuilder sqB = new SelectQueryBuilder();
            sqB.Columns.SelectColumns("*");
            sqB.TableSource = Variables.App.prefijoTablas + "Articulos";

            DataTable dt = db.Execute(sqB.BuildQuery());

            return dt;
        }


        /// <summary>
        ///     Devuelve un DataTable con la tabla Categorias
        /// </summary>
        /// <returns></returns>
        public DataTable Categorias(int padre)
        {
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            DataTable dt = null;

            if (padre != -1)
            {
                dt =
                    (db.Execute(
                        "SELECT codigoWebCatalogo,codigoPadre,tipo,tipo1,titulo,subtitulo,imagen1,imagen2,texto1 from conWebCatalogos WHERE relacion=1 and codigoPadre=" +
                        padre + " order by orden")); //and (titulo is null or titulo='')
            }
            else
            {
                dt =
                    (db.Execute(
                        "SELECT codigoWebCatalogo,codigoPadre,tipo,tipo1,titulo,subtitulo,imagen1,imagen2,texto1 from conWebCatalogos WHERE relacion=1 and codigoPadre=1 order by orden"));
                    //and (titulo is null or titulo='')
            }

            return dt;
        }

        /// <summary>
        ///     Devuelve un DataTable con la tabla Categorias raiz
        /// </summary>
        /// <returns></returns>
        public DataTable Categorias()
        {
            return Categorias(-1);
        }


        /// <summary>
        ///     Devuelve un DataTable con la tabla Producto, indicando el catálogo
        /// </summary>
        /// <returns></returns>
        public DataTable Producto(int codigoCatalogo)
        {
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            DataTable dt = null;

            dt = db.Execute("SELECT * from WebCatalogos WHERE codigoWebCatalogo=" + codigoCatalogo);

            return dt;
        }


        /// <summary>
        ///     Permite la búsqueda de productos en la tabla WebCatalogos
        /// </summary>
        /// <returns></returns>
        public DataTable BuscarProducto(string cadena)
        {
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            DataTable dt = null;

            dt =
                db.Execute("SELECT * from WebCatalogos WHERE (titulo like '%" + cadena + "%' or tipo1 like '%" + cadena +
                           "%' or tipo like '%" + cadena + "%' or texto1 like '%" + cadena + "%' or texto2 like '%" +
                           cadena + "%' or texto3 like '%" + cadena + "%') and not (titulo is null or titulo='')");

            return dt;
        }


        /// <summary>
        ///     Devuelve un DataTable con la tabla Categorias pertenecientes a un 'padre'
        /// </summary>
        /// <returns></returns>
        public DataTable BuscarCategorias(int padre)
        {
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            DataTable dt = null;

            dt =
                (db.Execute("SELECT * from conWebCatalogos WHERE relacion=1 and codigoPadre=" + padre +
                            " and not (titulo is null or titulo='') order by orden"));

            return dt;
        }


        /// <summary>
        ///     Devuelve un DataTable con la tabla Categorias pertenecientes a un 'padre' versión 2
        /// </summary>
        /// <returns></returns>
        public DataTable BuscarCategorias2(int padre)
        {
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            DataTable dt = null;

            dt =
                (db.Execute("SELECT * from conWebCatalogos WHERE relacion=1 and codigoPadre=" + padre +
                            " and (titulo is null or titulo='') order by orden"));

            return dt;
        }


        /// <summary>
        ///     Devuelve un DataTable con la tabla Categorias pertenecientes a un tipo
        /// </summary>
        /// <returns></returns>
        public DataTable BuscarCategoriasTipo(string tipo)
        {
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            DataTable dt = null;

            dt =
                (db.Execute("SELECT * from conWebCatalogos WHERE tipo='" + tipo +
                            "' and not (titulo is null or titulo='') order by tipo1"));

            return dt;
        }

        /// <summary>
        ///     Arbol de categorías
        /// </summary>
        /// <returns></returns>
        public string ArbolCategorias()
        {
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            MemoryStream memory_stream = new MemoryStream();
            string arb = null;
            XmlTextWriter doc = new XmlTextWriter(memory_stream, Encoding.UTF8);


            doc.Indentation = 4;
            doc.IndentChar = char.Parse(" ");
            doc.Formatting = ((Formatting) (doc.Indentation));

            doc.WriteStartDocument();

            doc.WriteStartElement("root");
            doc.WriteAttributeString("label", "Raiz");
            doc.WriteAttributeString("data", "1");
            doc.WriteAttributeString("isBranch", "true");


            dtArbol =
                db.Execute(
                    "SELECT codigoWebCatalogo,codigoPadre,tipo,tipo1,titulo,orden from conWebCatalogos WHERE relacion=1 order by orden");

            ArbolCategoriasAdd(ref doc, 1);

            doc.WriteEndDocument();
            doc.Flush();

            StreamReader stream_reader = new StreamReader(memory_stream);
            memory_stream.Seek(0, SeekOrigin.Begin);
            arb = stream_reader.ReadToEnd();

            doc.Close();

            return arb;
        }


        /// <summary>
        ///     Devuelve el 'dato' tabulado en una posición más, cada vez que se realiza una llamada a esta función.
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>
        private string Valor(object dato)
        {
            sp = sp + 1;
            if (dato == null)
            {
                return TextUtil.Substring("                          ", 0, sp);
            }
            if (Convert.ToString(dato).Trim() == "")
            {
                return TextUtil.Substring("                          ", 0, sp);
            }
            return Convert.ToString(dato);
        }


        /// <summary>
        ///     Devuelve un DataTable con la tabla Precios y fechas pertenecientes a un catálogo
        /// </summary>
        /// <returns></returns>
        public DataTable DetalleProducto(int codigoCatalogo)
        {
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            DataTable dt = new DataTable();
            DataRow r = null;
            DataTable dtPrecios = db.Execute("SELECT * from WebPrecios WHERE codigoWebPrecios=" + codigoCatalogo);

            if (dtPrecios.Rows.Count == 0)
            {
                return null;
            }

            r = dtPrecios.Rows[0];

            DataRow dtr = null;
            DataColumn dtc = null;
            dtc = new DataColumn(" ", Type.GetType("System.String"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn(Valor(r["FechaA"]), Type.GetType("System.String"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn(Valor(r["FechaB"]), Type.GetType("System.String"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn(Valor(r["FechaC"]), Type.GetType("System.String"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn(Valor(r["FechaD"]), Type.GetType("System.String"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn(Valor(r["FechaE"]), Type.GetType("System.String"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn(Valor(r["FechaF"]), Type.GetType("System.String"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn(Valor(r["FechaG"]), Type.GetType("System.String"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn(Valor(r["FechaH"]), Type.GetType("System.String"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn(Valor(r["FechaI"]), Type.GetType("System.String"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn(Valor(r["FechaJ"]), Type.GetType("System.String"));
            dt.Columns.Add(dtc);

            int f = 0;
            for (f = 1; f <= 6; f++)
            {
                dtr = dt.NewRow();

                dtr[0] = r["Pax" + f];
                dtr[1] = r["PrA" + f];
                dtr[2] = r["PrB" + f];
                dtr[3] = r["PrC" + f];
                dtr[4] = r["PrD" + f];
                dtr[5] = r["PrE" + f];
                dtr[6] = r["PrF" + f];
                dtr[7] = r["PrG" + f];
                dtr[8] = r["PrH" + f];
                dtr[9] = r["PrI" + f];
                dtr[10] = r["PrJ" + f];

                dt.Rows.Add(dtr);
            }

            return dt;
        }


        /// <summary>
        ///     Devuelve un DataTable con la tabla Precios pertenecientes a un catálogo
        /// </summary>
        /// <returns></returns>
        public DataTable Precios(int codigoCatalogo)
        {
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            DataTable dt = db.Execute("SELECT * from WebPrecios WHERE codigoWebPrecios=" + codigoCatalogo);

            return dt;
        }


        /// <summary>
        ///     Devuelve un DataTable con la tabla Paises
        /// </summary>
        /// <returns></returns>
        public DataTable ComboPaises()
        {
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            DataTable dt = db.Execute("SELECT * from " + Variables.App.prefijoTablas + "Paises order by nombre");

            return dt;
        }


        /// <summary>
        ///     Devuelve un DataTable con la tabla Provincias
        /// </summary>
        /// <returns></returns>
        public DataTable ComboProvincias()
        {
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            DataTable dt = db.Execute("SELECT * from " + Variables.App.prefijoTablas + "Provincias order by provincia");

            return dt;
        }


        /// <summary>
        ///     Devuelve un DataTable con las reservas que se pueden realizar
        /// </summary>
        /// <returns></returns>
        public DataTable ComboReservas(int codigoCatalogo)
        {
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            DataTable dt = new DataTable();
            DataRow r = null;
            DataTable dtPrecios = db.Execute("SELECT * from WebPrecios WHERE codigoWebPrecios=" + codigoCatalogo);

            if (dtPrecios.Rows.Count == 0)
            {
                return null;
            }
            r = dtPrecios.Rows[0];

            DataRow dtr = null;
            DataColumn dtc = null;
            dtc = new DataColumn("Id", Type.GetType("System.Int32"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn("Fecha", Type.GetType("System.String"));
            dt.Columns.Add(dtc);

            int f = 0;
            string fec = null;
            for (f = 0; f <= 9; f++)
            {
                fec = Functions.Valor(r["Fecha" + Convert.ToChar(65 + f)]);

                if (fec != "")
                {
                    dtr = dt.NewRow();

                    dtr[0] = f + 1;
                    string tt = TextUtil.Replace(fec, "\r\n", ".");
                    dtr[1] = TextUtil.Replace(tt, "\r", ".");

                    dt.Rows.Add(dtr);
                }
            }

            return dt;
        }


        /// <summary>
        ///     Devuelve un DataTable con los alojamientos pertenecientes a un catálogo
        /// </summary>
        /// <returns></returns>
        public DataTable ComboAlojamiento(int codigoCatalogo)
        {
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            DataTable dt = new DataTable();
            DataRow r = null;
            DataTable dtPrecios = db.Execute("SELECT * from WebPrecios WHERE codigoWebPrecios=" + codigoCatalogo);

            if (dtPrecios.Rows.Count == 0)
            {
                return null;
            }
            r = dtPrecios.Rows[0];

            DataRow dtr = null;
            DataColumn dtc = null;
            dtc = new DataColumn("Id", Type.GetType("System.Int32"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn("Descripcion", Type.GetType("System.String"));
            dt.Columns.Add(dtc);

            int f = 0;
            string pax = null;
            for (f = 0; f <= 5; f++)
            {
                pax = Functions.Valor(r["Pax" + (f + 1)]);

                if (pax != "")
                {
                    dtr = dt.NewRow();

                    dtr[0] = f + 1;
                    dtr[1] = pax;

                    dt.Rows.Add(dtr);
                }
            }

            return dt;
        }


        /// <summary>
        ///     Devuelve un DataTable con las páginas exitentes en el portal.
        /// </summary>
        /// <returns></returns>
        public DataTable Paginas()
        {
            DataTable dt = new DataTable();
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            DataTable dtPaginas =
                db.Execute("SELECT idPagina,titulo,enlace FROM " + Variables.App.prefijoTablas +
                           "Paginas order by fechaCreacion DESC,titulo");

            DataColumn dtc;
            dtc = new DataColumn("idPagina", Type.GetType("System.Int32"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn("titulo", Type.GetType("System.String"));
            dt.Columns.Add(dtc);
            dtc = new DataColumn("enlace", Type.GetType("System.String"));
            dt.Columns.Add(dtc);

            DataRow dtr;
            foreach (DataRow row in dtPaginas.Rows)
            {
                dtr = dt.NewRow();

                dtr["idPagina"] = NumberUtils.NumberInt(row["idPagina"]);
                dtr["titulo"] = Functions.Valor(row["titulo"]);
                dtr["enlace"] = Functions.Valor(row["enlace"]);

                dt.Rows.Add(dtr);
            }

            return dt;
        }


        /// <summary>
        ///     Devuelve el código HTML de la página indicada por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Pagina(int id)
        {
            return Ui.MuestraPagina(id);
        }

        /// <summary>
        ///     Devuelve el código HTML de la página col el título dado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Pagina(string titulo)
        {
            return Ui.MuestraPagina(titulo);
        }


        /// <summary>
        ///     Devuelve el código HTML del módulo indicado por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Modulo(int id)
        {
            Modulos modulos = new Modulos();
            return modulos.MuestraModulo(id, false);
        }


        ///// <summary>
        ///// Devuelve el código HTML de la cabecera
        ///// </summary>
        ///// <returns></returns>
        //public string Cabecera()
        //{
        //    FSPortal.Modulos modulos = new FSPortal.Modulos();
        //    return modulos.MuestraCabecera();
        //}


        ///// <summary>
        ///// Devuelve el código HTML del píe
        ///// </summary>
        ///// <returns></returns>
        //public string Pie()
        //{
        //    FSPortal.Modulos modulos = new FSPortal.Modulos();
        //    return modulos.MuestraPie();
        //}


        /// <summary>
        ///     Función recursiva para la creación del arbol de navegación
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="id"></param>
        private void ArbolCategoriasAdd(ref XmlTextWriter doc, int id)
        {
            DataTable dt = Utils.SelectDataTable(dtArbol, "codigoPadre=" + id, "orden");

            foreach (DataRow row in dt.Rows)
            {
                doc.WriteStartElement("element");
                string tit = Functions.Valor(row["titulo"]);
                if (tit == "")
                {
                    tit = Functions.Valor(row["tipo1"]);
                }
                if (tit == "")
                {
                    tit = Functions.Valor(row["tipo"]);
                }

                doc.WriteAttributeString("label", tit);
                doc.WriteAttributeString("data", Functions.Valor(row["codigoWebCatalogo"]));
                doc.WriteAttributeString("isBranch",
                    tieneSubCat(ref doc, NumberUtils.NumberInt(row["codigoWebCatalogo"])).ToString().ToLower());

                ArbolCategoriasAdd(ref doc, NumberUtils.NumberInt(row["codigoWebCatalogo"]));

                doc.WriteEndElement();
            }
        }


        /// <summary>
        ///     Devuelve true o false si la categoría id tiene subcategorías
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool tieneSubCat(ref XmlTextWriter doc, int id)
        {
            DataTable dt = Utils.SelectDataTable(dtArbol, "codigoPadre=" + id, "orden");

            if (dt.Rows.Count == 0)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        ///     Devuelve código HTML con información del portal.
        /// </summary>
        /// <returns></returns>
        public string Comprobar()
        {
            Comprobar c = new Comprobar();
            return c.Inicio();
        }

        /// <summary>
        ///     Carga de la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataSource = BuscarCategoriasTipo("Nieve");
            GridView1.DataBind();


            GridView2.DataSource = ComboAlojamiento(72);
            GridView2.DataBind();

            TextBox1.Text = ArbolCategorias();
        }
    }
}