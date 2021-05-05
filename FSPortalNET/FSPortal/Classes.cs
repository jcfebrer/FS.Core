using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSPortal
{
    class Classes
    {
        public class ___Configuracion
        {

            public System.Int32 idConfiguracion { get; set; }
            public System.String NombreWeb { get; set; }
            public System.String descripcionWeb { get; set; }
            public System.String palabrasClave { get; set; }
            public System.Double? euro { get; set; }
            public System.String moneda { get; set; }
            public System.Double? iva { get; set; }
            public System.String NoticiasRSS { get; set; }
            public System.Boolean? AllowSelect { get; set; }
            public System.Boolean? AllowContextMenu { get; set; }
            public System.Boolean? AllowDragStart { get; set; }
            public System.Boolean? ShowStatusBar { get; set; }
            public System.String StatusMensaje { get; set; }
            public System.Int32? encuesta { get; set; }
            public System.Int32? registrosPorPagina { get; set; }
            public System.String direccionFisica { get; set; }
            public System.String codigoPostal { get; set; }
            public System.String provincia { get; set; }
            public System.String correoInfo { get; set; }
            public System.String servidorCorreo { get; set; }
            public System.String correoUsuario { get; set; }
            public System.String correoPassword { get; set; }
            public System.Int16? correoPuerto { get; set; }
            public System.Boolean? correoActivarSSL { get; set; }
            public System.Boolean? multiCestas { get; set; }
            public System.String uploadPath { get; set; }
            public System.String explorerPath { get; set; }
            public System.String strCookieName { get; set; }
            public System.Int32? estado { get; set; }
            public System.Boolean? modoPrueba { get; set; }
            public System.String correoPrueba { get; set; }
            public System.String correoCopia { get; set; }
            public System.String idioma { get; set; }
            public System.String payPalURL { get; set; }
            public System.String payPalEmail { get; set; }
            public System.String cuentaPago { get; set; }
            public System.String payPalNotifyURL { get; set; }
            public System.String codigoTienda4B { get; set; }
            public System.String url4B { get; set; }
            public System.String bbvaURL { get; set; }
            public System.String bbvaComercio { get; set; }
            public System.String bbvaTerminal { get; set; }
            public System.String bbvaPalClave { get; set; }
            public System.String bbvaClaveXOR { get; set; }
            public System.String laCaixaNroComercio { get; set; }
            public System.String laCaixaClaveEnc { get; set; }
            public System.Int32? laCaixaTerminal { get; set; }
            public System.String laCaixaURLTpvVirtual { get; set; }
            public System.String tiposImagenes { get; set; }
            public System.Int32? maxTamImagen { get; set; }
            public System.Boolean? mostrarControlesModulos { get; set; }
            public System.Boolean? mostrarCabeceraModulos { get; set; }
            public System.Boolean? tallasYColores { get; set; }
            public System.Boolean? disponibilidad { get; set; }
            public System.Int32? altoPopUp { get; set; }
            public System.Int32? anchoPopUp { get; set; }
            public System.String version { get; set; }
            public System.Boolean? mostrarTitulosModulos { get; set; }
            public System.Boolean? mostrarTitulosPaginas { get; set; }
            public System.Boolean? mostrarNavegacionPaginas { get; set; }
            public System.Boolean? mostrarTiempoCarga { get; set; }
            public System.Boolean? mostrarIP { get; set; }
            public System.Boolean? mostrarImprimir { get; set; }
            public System.Boolean? mostrarEnviarAmigo { get; set; }
            public System.Boolean? mostrarAnadirFavoritos { get; set; }
            public System.Boolean? mostrarRegistro { get; set; }
            public System.Boolean? mostrarOpcionesPagina { get; set; }
            public System.Int32? correoBienvenida { get; set; }
            public System.String paginaRegistro { get; set; }
            public System.String paginaLogin { get; set; }
            public System.String paginaPerfil { get; set; }
            public System.String paginaRecordar { get; set; }
            public System.Boolean? estadisticas { get; set; }
            public System.String jqueryTema { get; set; }
            public System.String plantillaCorreo { get; set; }
            public System.String plantillaModulos { get; set; }
            public System.DateTime? fechaModificacion { get; set; }
            public System.DateTime? fechaCreacion { get; set; }
            public System.Int32? usuarioModificacion { get; set; }
            public System.Int32? usuarioCreacion { get; set; }
        }

        public class Banners
        {

            public System.Int32 idBanner { get; set; }
            public System.String link { get; set; }
            public System.String imagen { get; set; }
            public System.String mensaje { get; set; }
            public System.Int32? vecesMostrado { get; set; }
            public System.Int32? vecesPulsado { get; set; }
            public System.Int32? vecesQueMostrar { get; set; }
            public System.Int32? vecesQuePulsar { get; set; }
            public System.Boolean? activo { get; set; }
            public System.Boolean? horizontal { get; set; }
            public System.Boolean? nuevaVentana { get; set; }
            public System.DateTime? fechaModificacion { get; set; }
            public System.DateTime? fechaCreacion { get; set; }
            public System.Int32? usuarioModificacion { get; set; }
            public System.Int32? usuarioCreacion { get; set; }
        }

        public class Busquedas
        {

            public System.Int32 BusquedaID { get; set; }
            public System.String TextoBusqueda { get; set; }
            public System.Int32? Tipo { get; set; }
            public System.Int32? TemaID { get; set; }
            public System.DateTime? Fecha { get; set; }
            public System.DateTime? Hora { get; set; }
            public System.Int32? Resultados { get; set; }
            public System.String Usuario { get; set; }
            public System.DateTime? fechaModificacion { get; set; }
            public System.DateTime? fechaCreacion { get; set; }
            public System.Int32? usuarioModificacion { get; set; }
            public System.Int32? usuarioCreacion { get; set; }
        }

        public class Categorias_Paginas
        {

            public System.Int32 idCategoriaPagina { get; set; }
            public System.Int32? idPagina { get; set; }
            public System.String categoria { get; set; }
            public System.DateTime? fechaModificacion { get; set; }
            public System.DateTime? fechaCreacion { get; set; }
            public System.Int32? usuarioModificacion { get; set; }
            public System.Int32? usuarioCreacion { get; set; }
        }

        public class Comentarios
        {

            public System.Int32 idComentario { get; set; }
            public System.Int32? idPagina { get; set; }
            public System.String comentario { get; set; }
            public System.DateTime? fecha { get; set; }
            public System.String enviadoPor { get; set; }
            public System.DateTime? fechaModificacion { get; set; }
            public System.DateTime? fechaCreacion { get; set; }
            public System.Int32? usuarioModificacion { get; set; }
            public System.Int32? usuarioCreacion { get; set; }
        }

        public class ConfiguracionModulos
        {

            public System.Int32 idConfModulo { get; set; }
            public System.Int32? idModulo { get; set; }
            public System.String propiedad { get; set; }
            public System.String valor { get; set; }
            public System.DateTime? fechaModificacion { get; set; }
            public System.DateTime? fechaCreacion { get; set; }
            public System.Int32? usuarioModificacion { get; set; }
            public System.Int32? usuarioCreacion { get; set; }
        }

        public class Correo
        {

            public System.Int32 MessageID { get; set; }
            public System.String From { get; set; }
            public System.String To { get; set; }
            public System.String Subject { get; set; }
            public System.String Body { get; set; }
            public System.DateTime? Date { get; set; }
            public System.DateTime? Time { get; set; }
            public System.Boolean? Leido { get; set; }
            public System.Boolean? Contestado { get; set; }
            public System.DateTime? fechaModificacion { get; set; }
            public System.DateTime? fechaCreacion { get; set; }
            public System.Int32? usuarioModificacion { get; set; }
            public System.Int32? usuarioCreacion { get; set; }
        }

        public class Encuestas
        {

            public System.Int32 EncuestaID { get; set; }
            public System.String EncuestaName { get; set; }
            public System.String EncuestaQuestion { get; set; }
            public System.DateTime? StartDate { get; set; }
            public System.DateTime? FinishDate { get; set; }
            public System.Boolean? Active { get; set; }
            public System.DateTime? fechaModificacion { get; set; }
            public System.DateTime? fechaCreacion { get; set; }
            public System.Int32? usuarioModificacion { get; set; }
            public System.Int32? usuarioCreacion { get; set; }
        }

        public class Errores
        {

            public System.Int32 idError { get; set; }
            public System.DateTime? Fecha { get; set; }
            public System.String Mensaje { get; set; }
            public System.Int16? Tipo { get; set; }
        }

        public class EstadoPortal
        {

            public System.Int32 idEstado { get; set; }
            public System.String Estado { get; set; }
        }

        public class Estados
        {

            public System.Int32 Id { get; set; }
            public System.Int32? idEstado { get; set; }
            public System.String descripcion { get; set; }
        }

        public class FormularioCampos
        {

            public System.Int32 idCamposFormulario { get; set; }
            public System.Int32? idFormulario { get; set; }
            public System.String nombre { get; set; }
            public System.Int32? tipo { get; set; }
            public System.Int32? tamano { get; set; }
            public System.String descripcion { get; set; }
            public System.DateTime? fechaModificacion { get; set; }
            public System.DateTime? fechaCreacion { get; set; }
            public System.Int32? usuarioModificacion { get; set; }
            public System.Int32? usuarioCreacion { get; set; }
        }

        public class Formularios
        {

            public System.Int32 idFormulario { get; set; }
            public System.String nombre { get; set; }
            public System.Boolean? activo { get; set; }
            public System.String textoEnviar { get; set; }
            public System.String mensajeEnviar { get; set; }
            public System.DateTime? fechaModificacion { get; set; }
            public System.DateTime? fechaCreacion { get; set; }
            public System.Int32? usuarioModificacion { get; set; }
            public System.Int32? usuarioCreacion { get; set; }
        }

        public class FormulariosTipoCampos
        {

            public System.Int32 idTipoCampo { get; set; }
            public System.String descripcion { get; set; }
        }

        public class Grupos
        {

            public System.Int32 GrupoId { get; set; }
            public System.String Nombre { get; set; }
            public System.Boolean? editarContenido { get; set; }
            public System.Boolean? guardarContenido { get; set; }
            public System.Boolean? borrarContenido { get; set; }
            public System.Boolean? administrar { get; set; }
            public System.DateTime? fechaModificacion { get; set; }
            public System.DateTime? fechaCreacion { get; set; }
            public System.Int32? usuarioModificacion { get; set; }
            public System.Int32? usuarioCreacion { get; set; }
        }

        public class Idiomas
        {

            public System.Int32 idIdioma { get; set; }
            public System.String castellano { get; set; }
            public System.String ingles { get; set; }
            public System.String frances { get; set; }
            public System.String aleman { get; set; }
            public System.String euskera { get; set; }
            public System.String catalan { get; set; }
        }

        public class Modulos
        {

            public System.Int32 idModulo { get; set; }
            public System.String Nombre { get; set; }
            public System.Int32? Posicion { get; set; }
            public System.Int32? Orden { get; set; }
            public System.String Titulo { get; set; }
            public System.Boolean? Activo { get; set; }
            public System.Boolean? mostrarRecuadro { get; set; }
            public System.Boolean? scroll { get; set; }
            public System.Int32? altoModulo { get; set; }
            public System.DateTime? fechaModificacion { get; set; }
            public System.DateTime? fechaCreacion { get; set; }
            public System.Int32? usuarioModificacion { get; set; }
            public System.Int32? usuarioCreacion { get; set; }
        }

        public class Noticias
        {

            public System.Int32 NoticiaID { get; set; }
            public System.String Titulo { get; set; }
            public System.String Autor { get; set; }
            public System.String TextoCorto { get; set; }
            public System.String TextoLargo { get; set; }
            public System.String Imagen { get; set; }
            public System.Boolean? Publicar { get; set; }
            public System.DateTime? FechaInicio { get; set; }
            public System.DateTime? FechaFin { get; set; }
            public System.DateTime? fechaModificacion { get; set; }
            public System.DateTime? fechaCreacion { get; set; }
            public System.Int32? usuarioModificacion { get; set; }
            public System.Int32? usuarioCreacion { get; set; }
        }

        public class OpcionesEncuesta
        {

            public System.Int32 OpcionID { get; set; }
            public System.Int32? EncuestaID { get; set; }
            public System.String OpcionText { get; set; }
            public System.String AdditionalInformation { get; set; }
        }

        public class Paginas
        {

            public System.Int32 idPagina { get; set; }
            public System.String Titulo { get; set; }
            public System.String Enlace { get; set; }
            public System.String Contenido { get; set; }
            public System.String contenidoMovil { get; set; }
            public System.Boolean? noContar { get; set; }
            public System.Boolean? soloAdmin { get; set; }
            public System.Boolean? nuevaPagina { get; set; }
            public System.Boolean? requiereLogin { get; set; }
            public System.Int32? Padre { get; set; }
            public System.Int32? Accesos { get; set; }
            public System.Int32? orden { get; set; }
            public System.String teclaDeAcceso { get; set; }
            public System.Boolean? comentarios { get; set; }
            public System.String tags { get; set; }
            public System.Int32? idCategoria { get; set; }
            public System.String plantilla { get; set; }
            public System.Boolean? checkURL { get; set; }
            public System.DateTime? fechaModificacion { get; set; }
            public System.DateTime? fechaCreacion { get; set; }
            public System.Int32? usuarioModificacion { get; set; }
            public System.Int32? usuarioCreacion { get; set; }
        }

        public class Paises
        {

            public System.Int32 idPais { get; set; }
            public System.String Nombre { get; set; }
            public System.String CodigoPais { get; set; }
        }

        public class Plantillas
        {

            public System.String plantilla { get; set; }
            public System.String titulo { get; set; }
            public System.String contenido { get; set; }
            public System.String contenidoMovil { get; set; }
            public System.Boolean? porDefecto { get; set; }
            public System.DateTime? fechaModificacion { get; set; }
            public System.DateTime? fechaCreacion { get; set; }
            public System.Int32? usuarioModificacion { get; set; }
            public System.Int32? usuarioCreacion { get; set; }
        }

        public class Provincias
        {

            public System.Int32 IdProvincia { get; set; }
            public System.String Provincia { get; set; }
            public System.String Capital { get; set; }
            public System.String CodigoPostal { get; set; }
            public System.Int32? Zona { get; set; }
        }

        public class Relaciones
        {

            public System.Int32 idRelacion { get; set; }
            public System.String FK_TABLE_NAME { get; set; }
            public System.String FK_Column_Name { get; set; }
            public System.String PK_TABLE_NAME { get; set; }
            public System.String PK_Column_Name { get; set; }
        }

        public class RespuestasEncuestas
        {

            public System.Int32 RespuestaID { get; set; }
            public System.Int32? EncuestaID { get; set; }
            public System.Int32? OpcionID { get; set; }
            public System.String IP { get; set; }
            public System.DateTime? fechaModificacion { get; set; }
            public System.DateTime? fechaCreacion { get; set; }
            public System.Int32? usuarioModificacion { get; set; }
            public System.Int32? usuarioCreacion { get; set; }
        }

        public class Sexo
        {

            public System.Int32 idSexo { get; set; }
            public System.String sexo { get; set; }
        }

        public class Usuarios
        {

            public System.Int32 UsuarioID { get; set; }
            public System.String Usuario { get; set; }
            public System.String Clave { get; set; }
            public System.String Email { get; set; }
            public System.String Nombre { get; set; }
            public System.String Apellido1 { get; set; }
            public System.String Apellido2 { get; set; }
            public System.Int32? Sexo { get; set; }
            public System.DateTime? FechaNacimiento { get; set; }
            public System.Boolean? EnvioCorreos { get; set; }
            public System.Boolean? EnvioNotificaciones { get; set; }
            public System.Int32? Pais { get; set; }
            public System.DateTime? UltimaConexion { get; set; }
            public System.Int32? Edad { get; set; }
            public System.Int32? Dto { get; set; }
            public System.String TipoDomicilio { get; set; }
            public System.String Domicilio { get; set; }
            public System.String Portal { get; set; }
            public System.String Piso { get; set; }
            public System.String Poblacion { get; set; }
            public System.Int32? Provincia { get; set; }
            public System.String CodigoPostal { get; set; }
            public System.String DNI { get; set; }
            public System.String Telefono1 { get; set; }
            public System.String Telefono2 { get; set; }
            public System.Boolean? NotificarCorreo { get; set; }
            public System.Boolean? NoRecibirCorreo { get; set; }
            public System.Int32? Grupo { get; set; }
            public System.Boolean? Active { get; set; }
            public System.String Date_format { get; set; }
            public System.Int32? VecesConectado { get; set; }
            public System.Int32? PaginaInicio { get; set; }
            public System.Int32? Comercial { get; set; }
            public System.String Ip { get; set; }
            public System.String PrecioaMostrar { get; set; }
            public System.String Idioma { get; set; }
            public System.String Campo1 { get; set; }
            public System.String Campo2 { get; set; }
            public System.String Campo3 { get; set; }
            public System.String Campo4 { get; set; }
            public System.DateTime? FechaModificacion { get; set; }
            public System.DateTime? FechaCreacion { get; set; }
            public System.Int32? UsuarioModificacion { get; set; }
            public System.Int32? UsuarioCreacion { get; set; }
        }

        public class UsuariosForum
        {

            public System.String AIM { get; set; }
            public System.Boolean? Attach_signature { get; set; }
            public System.String Avatar { get; set; }
            public System.String Avatar_title { get; set; }
            public System.Int32? comercial { get; set; }
            public System.String Date_format { get; set; }
            public System.DateTime? DOB { get; set; }
            public System.Boolean? EnvioCorreos { get; set; }
            public System.Boolean? EnvioNotificaciones { get; set; }
            public System.String Homepage { get; set; }
            public System.String ICQ { get; set; }
            public System.String idioma { get; set; }
            public System.String Interests { get; set; }
            public System.String Location { get; set; }
            public System.String MSN { get; set; }
            public System.Int32? No_of_posts { get; set; }
            public System.Boolean? NoRecibirCorreo { get; set; }
            public System.Boolean? NotificarCorreo { get; set; }
            public System.String Occupation { get; set; }
            public System.Int32? paginaInicio { get; set; }
            public System.Boolean? PM_notify { get; set; }
            public System.String precioaMostrar { get; set; }
            public System.Boolean? Reply_notify { get; set; }
            public System.Boolean? Rich_editor { get; set; }
            public System.String Salt { get; set; }
            public System.Boolean? Show_email { get; set; }
            public System.String Signature { get; set; }
            public System.String Telefono2 { get; set; }
            public System.Int16? Time_offset_hours { get; set; }
            public System.String TipoDomicilio { get; set; }
            public System.String User_code { get; set; }
            public System.Int32 UsuarioID { get; set; }
            public System.String Yahoo { get; set; }
        }
    }
}
