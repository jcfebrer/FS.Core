// // <fileheader>
// // <copyright file="Variables.App.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSPortal
// //     Solution: FSPortalNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System.Collections;
using FSPlugin;

#endregion

namespace FSPortal
{
    /// <summary>
    ///     Clase pública con las variables globales del portal.
    /// </summary>
    public class VariablesAplicacion
    {
//#pragma warning disable 1591

        //
        // variables globales
        //
        public string version = "2.9";
        public bool modoLite;
        public string nombreWeb;
        public string HTTP_HOST;
        public string directorioPortal;
        public string directorioWeb;
        public string webHttp;
        public string portal;
        public string plantillaModulos;
        public int estado = 0;
        public bool estadisticas = true;
        //public string simbDate = "#";
        //public string dateSep = "/";
        //public string dateFormat = "ddmmyyyy";
        public string plantillaCorreo;
        public string paginaRegistro = "";
        public string paginaLogin = "";
        public string paginaPerfil = "";
        public string paginaRecordar = "";
        public ArrayList modulosActivos = new ArrayList();

        //database variables
        public string prefijoTablas = "";
        public string defaultEntry = "FSConnection";
        public string connectionString = "";
        public string providerName = "";

        public string moneda;
        public string ExtraCSS;
        public string descripcionWeb;
        public bool tallasYColores;
        public string palabrasClave;
        //public bool AllowSelect;
        //public bool AllowContextMenu;
        //public bool AllowDragStart;
        //public bool ShowStatusBar;
        //public string StatusMensaje;
        public int registrosPorPagina;
        public string direccionFisica;
        public string codigoPostal;
        public string provincia;
        public bool multiCestas;
        public string uploadPath;
        public string explorerPath;
        public string strCookieName;
        public string payPalURL;
        public string payPalEmail;
        public string payPalNotifyURL;
        public string cuentaPago;
        public string jqueryTema;
        public int invitado = 2;
        //pasarela 4b
        public string codigoTienda4b;
        public string url4b;
        //pasarela bbva
        public string bbvaURL;
        public string bbvaComercio;
        public string bbvaTerminal;
        public string bbvaPalClave;
        public string bbvaClaveXOR;
        //pasarela laCaixa
        public string laCaixaNroComercio;
        public string laCaixaClaveEnc;
        public int laCaixaTerminal;
        public string laCaixaURLTpvVirtual;

        //public bool cachearCabecera;
        //public bool cachearPie;
        //public bool cachearColumnaIzquierda;
        //public bool cachearColumnaDerecha;
        //public bool cachearColumnaCentro;
        public string anchoPopUp;
        public string altoPopUp;
        //public bool mostrarCabeceraModulos;
        //public bool mostrarControlesModulos;
        //public bool mostrarTitulosModulos;
        //public bool mostrarTitulosPaginas;
        //public bool mostrarNavegacionPaginas;
        //public bool mostrarTiempoCarga;
        //public bool mostrarIP;
        //public bool mostrarImprimir;
        //public bool mostrarEnviarAmigo;
        //public bool mostrarAnadirFavoritos;
        //public bool mostrarRegistro;
        //public bool mostrarOpcionesPagina;
        //public int usuariosTotales;
        public string encuesta;
        //public string noticiasRSS;
        public int correoBienvenida;

        public string idioma;
        public double Iva;
        public bool disponibilidad;
        //public string ConnPortal;
        //public string ProviderName;
        public string correoInfo;
        public string servidorCorreo;
        public int correoPuerto;
        public bool correoActivarSSL;
        public string correoPrueba;
        public string correoCopia;
        public string correoUsuario;
        public string correoPassword;
        public bool ModoPrueba;
        public bool UseXML;
        public bool MobileMode;
        public bool MostrarPaginasNoRegistradas;

        public Variables.HtmlEditorType htmlEditor = Variables.HtmlEditorType.TinyMce;

        //estadísticas
        public string FilterIps;
        public bool ShowLinks;
        public bool RefThisServer;
        public bool StripPathParameters;
        public bool StripPathProtocol;
        public bool StripRefParameters;
        public bool StripRefProtocol;
        public bool StripRefFile;

        //plugins
        public PluginCollection Plugins = null;

        //pagina cargada
        public BasePage Page;

        ////variable de sesión
        //public HttpSessionState Session;

        //cache
        //public DataTable cacheIdiomas;
        //public DataTable cacheSchemaTable;
        //public DataTable cacheSchemaForeignKeys;
        //public DataTable cacheConfiguracion;

      
        //#pragma warning restore 1591
    }
}