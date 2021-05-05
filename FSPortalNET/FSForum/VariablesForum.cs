// <fileheader>
// <copyright file="Variables.Forum.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: clsVariables.Forum.cs
//     Copyright (c) 2003-2007 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FSForum
{
	/// <summary>
	/// Clase pública para almacenar las variables del foro.
	/// </summary>
    public class VariablesForum
    {

#pragma warning disable 1591

        public string strCon;
        public DataTable dtCommon;
        public string strSQL;
        public long lngLoggedInUserID;
        public string strLoggedInusuario;
        //public int intGroupID;
        public string strWebsiteName;
        public string strMainForumName;
        public string strForumPath;
        public string strForumEmailAddress;
        public bool blnTextLinks;
        public bool blnRTEEditor;
        public bool blnEmail;
        public long lngPollID;
        public string strMailComponent;
        public long lngTopicID;
        public string strOutgoingMailServer;
        public bool blnForumLocked;
        public string strLoggedInUserCode;
        public bool blnTopicLocked;
        public int intTopicPageNumber;
        public int intRecordPositionPageNum;
        public bool blnLCode;
        //public bool blnAdmin;
        public bool blnModerator;
        public bool blnGuest;
        public bool blnActiveMember;
        public bool blnLoggedInUserEmail;
        public bool blnLoggedInUserSignature;
        public int intTopicPerPage;
        public string strTitleImage;
        public bool blnEmoticons;
        public string strDatabaseDateFunction;
        //public string strDatabaseType;
        public bool blnGuestPost;
        public bool blnAvatar;
        public bool blnEmailActivation;
        public bool blnSendPost;
        public int intNumHotViews;
        public int intNumHotReplies;
        public bool blnPrivateMessages;
        public int intNumPrivateMessages;
        public int intThreadsPerPage;
        public string strDbPathAndName;
        public int intSpamTimeLimitSeconds;
        public int intSpamTimeLimitMinutes;
        public string strDateFormat;
        public string strTimeOffSet;
        public int intTimeOffSet;
        public bool blnReplyNotify;
        public bool blnAttachSignature;
        public bool blnWYSIWYGEditor;
        public int intMaxPollChoices;
        public bool blnEmailMessenger;
        public bool blnActiveUsers;
        public bool blnForumClosed;
        public bool blnShowEditUser;
        public bool blnShowProcessTime;
        public double dblStartTime;
        public bool blnClosedForumPage;
        public bool blnFlashFiles;
        public string strWebsiteURL;
        public bool blnShowMod;
        public bool blnAvatarUploadEnabled;
        public bool blnRegistrationSuspeneded;
        public string strLoggedInUserEmail;
        public string strImageTypes;
        public bool blnLongRegForm;
        public bool blnLongSecurityCode;

        public bool blnRead;
        public bool blnPost;
        public bool blnReply;
        public bool blnEdit;
        public bool blnDelete;
        public bool blnPriority;
        public bool blnPollCreate;
        public bool blnVote;
        public bool blnAttachments;
        public bool blnImageUpload;


        public string strVersion = "8";
        public string strRTEversion = "1.2c";

        public string strDbTable = "Forum";  //nombre pre tabla
        public string strDbProc = "wwfSp";



        public bool blnEncryptedclaves = false;

        public int intForumID;

        public string strBgColour = "#FFFFFF";
        public string strBgImage = "";
        public string strTextColour = "#000000";
        public string strNavSpacer = " : ";

        public int intAvatarHeight = 64;
        public int intAvatarWidth = 64;



        public string strTableColour = "#fdf5e6";
        public string strTableBgImage = "";
        public string strTableBgColour = "#FFFFFF";
        public string strTableBorderColour = "#999999";
        public string strTableVariableWidth = "98%";

        public string strTableTitleColour = "#FFCC99";
        public string strTableTitleBgImage = "";

        public string strTableTitleColour2 = "#CCCCAA";
        public string strTableTitleBgImage2 = "";

        public string strTableEvenRowColour = "#fdf5e6";
        public string strTableOddRowColour = "#F8F8FC";

        public string strTableBottomRowColour = "#CCCCAA";


        public string strTablePostsColour = "#FBFBF6";
        public string strTablePostsBgImage = "";
        public string strTablePostsBgColour = "#FFFFFF";
        public string strTablePostsBorderColour = "#999999";
        public string strTablePostsVariableWidth = "98%";

        public string strTablePostsTitleColour = "#FFCC99";
        public string strTablePostsTitleBgImage = "";

        public string strTablePostsEvenRowColour = "#fdf5e6";
        public string strTablePostsOddRowColour = "#F8F8FC";
        public string strTablePostsSideEvenRowColour = "#fdf5e6";
        public string strTablePostsSideOddRowColour = "#F8F8FC";

        public string strTablePostsSeporatorColour = "#E1E2F0";

        public string strTableQuoteBorderColour = "#999999";
        public string strTableQuoteColour = "#FFFFFF";

        public string strTablePollColour = "#FBFBF6";
        public string strTablePollBgImage = "";
        public string strTablePollBgColour = "#FFFFFF";
        public string strTablePollBorderColour = "#999999";
        public string strTablePollVariableWidth = "98%";

        public string strTablePollTitleColour = "#FFCC99";
        public string strTablePollTitleBgImage = "";

        public string strTablePollColumnHeadingColour = "#CCCCAA";
        public string strTablePollColumnHeadingBgImage = "";

        public string strTablePollEvenRowColour = "#fdf5e6";
        public string strTablePollOddRowColour = "#F8F8FC";

        public string strTablePollBottomRowColour = "#CCCCAA";

        public string strTablePMBgColour = "#FFFFFF";
        public string strTablePMBgImage = "";
        public string strTablePMBoxBgColour = "#fdf5e6";
        public string strTablePMBoxSideBgColour = "#fdf5e6";

        public string strTablePMTitleColour = "#FFCC99";
        public string strTablePMTitleBgImage = "";

        public string strTableProfileColour = "#fdf5e6";
        public string strTableProfileBgImage = "";
        public string strTableProfileBgColour = "#FFFFFF";
        public string strTableProfileBorderColour = "#999999";
        public string strTableRowProfileColour = "#fdf5e6";

        public string strTableProfileTitleColour = "#FFCC99";
        public string strTableProfileTitleBgImage = "";
        public string strIETextBoxColour = "#FFFFFF";
        public string strImagePath = "forum_images/";





        public string strTxtWelcome = "Bienvenido";
        public string strTxtAllForums = "Todos los foros";
        public string strTxtTopics = "Temas";
        public string strTxtPosts = "Mensajes";
        public string strTxtLastPost = "Último mensaje";
        public string strTxtPostPreview = "Vista previa";
        public string strTxtAt = "a las";
        public string strTxtBy = "Por";
        public string strTxtOn = "en";
        public string strTxtProfile = "Perfil";
        public string strTxtSearch = "Buscar";
        public string strTxtQuote = "Citar";
        public string strTxtVisit = "Visita";
        public string strTxtView = "Ver";
        public string strTxtHome = "Home";
        public string strTxtHomepage = "Página principal";
        public string strTxtEdit = "Editar";
        public string strTxtDelete = "Borrar";
        public string strTxtEditProfile = "Editar perfil";
        public string strTxtLogOff = "Cerrar sesión";
        public string strTxtRegister = "Registrar";
        public string strTxtLogin = "Iniciar sesión";
        public string strTxtMembersList = "Mostrar la lista de miembros del foro";
        public string strTxtForumLocked = "Foro cerrado";
        public string strTxtSearchTheForum = "Buscar en el foro";
        public string strTxtPostReply = "Responder mensaje";
        public string strTxtNewTopic = "Escribir nuevo tema";
        public string strTxtCloseWindow = "Cerrar ventana";
        public string strTxtNoForums = "Aquí no hay foros";
        public string strTxtReturnToDiscussionForum = "Volver al foro de discusión";
        public string strTxtMustBeRegistered = "Debe estar registrado para poder usar el foro.";
        public string strClickHereIfNotRegistered = "Click aquí si no es un usuario registrado";
        public string strTxtResetForm = "Resetear";
        public string strTxtClearForm = "Borrar";
        public string strTxtYes = "Si";
        public string strTxtNo = "No";
        public string strTxtForumLockedByAdmim = "Disculpe, esta función a sido deshabilitada.<br />Este foro a sido bloqueado por el Administrador.";
        public string strTxtRequiredFields = "Los campos indicados son requeridos";

        public string strTxtForumJump = "Ir al foro";
        public string strTxtSelectForum = "Seleccionar el foro";

        public string strTxtErrorDisplayLine = "_______________________________________________________________";
        public string strTxtErrorDisplayLine1 = "El formulario no a sido enviado porque hubo algún problema con el mismo.";
        public string strTxtErrorDisplayLine2 = "Por favor solucione el problema y reenvié el formulario.";
        public string strTxtErrorDisplayLine3 = "Los siguientes campo(s) necesitan ser corregidos: -";
        public string strResetFormConfirm = "¿Está seguro que quiere resetear el formulario?";

        public string strTxtCookies = "Cookies y JavaScript deben estar activos para poder usar el foro";
        public string strTxtForum = "Foro";
        public string strTxtLatestForumPosts = "Últimos mensajes en el foro";
        public string strTxtForumStatistics = "Estadísticas del foro";
        public string strTxtNoForumPostMade = "No hay mensajes en el foro";
        public string strTxtThereAre = "Hay";
        public string strTxtPostsIn = "Mensajes en";
        public string strTxtTopicsIn = "Temas en";
        public string strTxtLastPostOn = "Último mensaje el";
        public string strTxtLastPostBy = "Último mensaje por";
        public string strTxtForumMembers = "Miembros del foro";
        public string strTxtTheNewestForumMember = "El Último miembro registrado es";

        public string strTxtThreadStarter = "Autor";
        public string strTxtReplies = "Respuestas";
        public string strTxtViews = "Vistas";
        public string strTxtDeleteTopicAlert = "¿Está seguro que quiere eliminar este tema?";
        public string strTxtDeleteTopic = "Borrar tema";
        public string strTxtNextTopic = "Tema siguiente";
        public string strTxtLastTopic = "Último tema";
        public string strTxtShowTopics = "Mostrar temas";
        public string strTxtNoTopicsToDisplay = "No hay mensajes escritos en el foro en el Último tiempo";

        public string strTxtAll = "Todo";
        public string strTxtLastWeek = "desde la Última semana";
        public string strTxtLastTwoWeeks = "desde hace dos semanas";
        public string strTxtLastMonth = "desde hace un mes";
        public string strTxtLastTwoMonths = "desde hace dos meses";
        public string strTxtLastSixMonths = "desde hace seis meses";
        public string strTxtLastYear = "desde hace un año";

        public string strTxtLocation = "Lugar";
        public string strTxtJoined = "Ingresado";
        public string strTxtForumAdministrator = "Administrador";
        public string strTxtForumModerator = "Moderador";
        public string strTxtDeletePostAlert = "¿Está seguro que quiere borrar este mensaje?";
        public string strTxtEditPost = "Editar mensaje";
        public string strTxtDeletePost = "Borrar mensaje";
        public string strTxtSearchForPosts = "Buscar otros mensajes por";
        public string strTxtSubjectFolder = "Asunto";
        public string strTxtPrintVersion = "Versión imprimible";
        public string strTxtEmailTopic = "Enviar por email";
        public string strTxtSorryNoReply = "Disculpe, Ud. no puede escribir aquí una respuesta.";
        public string strTxtThisForumIsLocked = "Este foro ha sido cerrado por el Administrador.";
        public string strTxtPostAReplyRegister = "Si desea responder a un mensaje en este tema debe primero";
        public string strTxtNeedToRegister = "Si Ud. no esta registrado debe primero";
        public string strTxtSmRegister = "registrarse";
        public string strTxtNoThreads = "No hay mensajes en la base de datos relacionados con el tema";
        public string strTxtNotGiven = "No dado";
        public string strTxtNoMessageError = @"Mensaje \t\t- Ingrese el mensaje a escribir";

        public string strTxtSearchFormError = @"Buscar por\t- Ingrese algo para la búsqueda";

        public string strTxtSearchResults = "Resultados de la búsqueda";
        public string strTxtYourSearchFor = "Su búsqueda por";
        public string strTxtHasFound = "- Han sido encontrados";
        public string strTxtResults = "resultados";
        public string strTxtNoSearchResults = "Disculpe, su búsqueda no dio resultados";
        public string strTxtClickHereToRefineSearch = "Click aqui para redefinir su búsqueda";
        public string strTxtSearchFor = "Buscar por";
        public string strTxtSearchIn = "Buscar dentro";
        public string strTxtSearchOn = "Buscar en";
        public string strTxtAllWords = "Todas las palabras";
        public string strTxtAnyWords = "Cualquier palabra";
        public string strTxtPhrase = "Frase";
        public string strTxtTopicSubject = "Temas con asunto";
        public string strTxtMessageBody = "Cuerpo del mensaje";
        public string strTxtUsuarios = "Autor";
        public string strTxtSearchForum = "Buscar en el foro";
        public string strTxtSortResultsBy = "Mostrar resultados por";
        public string strTxtLastPostTime = "Último mensaje";
        public string strTxtTopicStartDate = "Tema empezado el día";
        public string strTxtSubjectAlphabetically = "Por asunto alfabéticamente";
        public string strTxtNumberViews = "Número de visitas";
        public string strTxtStartSearch = "Comenzar la búsqueda";

        public string strTxtPrintPage = "Imprimir Página";
        public string strTxtPrintedFrom = "Impreso por";
        public string strTxtForumName = "Nombre del formulario";
        public string strTxtForumDiscription = "Descripción del formulario";
        public string strTxtURL = "URL";
        public string strTxtPrintedDate = "Impreso el día";
        public string strTxtTopic = "Tema";
        public string strTxtPostedBy = "Escrito por";
        public string strTxtDatePosted = "Escrito el día";

        public string strTxtEmoticonSmilies = "Caritas";
        public string strTxtClickOnEmoticonToAdd = "Click acá para agregar mas caritas a su mensaje.";

        public string strTxtSorryusuarioclaveIncorrect = "Disculpe el nombre de usuario o contraseña ingresados no son correctos.";
        public string strTxtPleaseTryAgain = "Intente nuevamente por favor.";
        public string strTxtusuario = "Nombre de usuario";
        public string strTxtclave = "Contraseña";
        public string strTxtLoginUser = "Iniciar sesión en el foro";
        public string strTxtClickHereForgottenPass = "¿Perdió su contraseña?";
        public string strTxtErrorusuario = @"Nombre de usuario \t- Ingrese el nombre de usuario para este foro";
        public string strTxtErrorclave = @"Contraseña \t- Ingrese su contraseña para este foro";

        public string strTxtForgottenclave = "Olvidó la contraseña";
        public string strTxtNoRecordOfusuario = "Disculpe, la dirección de email que ingresó no corresponde con la del usuario.";
        public string strTxtNoEmailAddressInProfile = "Disculpe, su perfil no contiene una dirección de email.<br />Su nueva Contraseña no podrá ser enviada.";
        public string strTxtReregisterForForum = "Ud. necesitará re-registrarte en el foro.";
        public string strTxtclaveEmailToYou = "Su nueva contraseña le a sido enviada.";
        public string strTxtPleaseEnterYourusuario = "Por favor ingrese el nombre de usuario y la dirección de email en la casilla de abajo.<br />Su nueva contrase?a, ser? enviada a la direcci?n de correo de su perfil.";
        public string strTxtValidEmailRequired = "Si su perfil en el foro no contiene una dirección de email valida, necesitar? re-registrarte en el foro.";
        public string strTxtEmailclave = "Enviar por email la contraseña";

        public string strTxtEmailclaveRequest = "En respuesta a la solicitud que nos envió, procedemos al env?o de su contrase?a para acceder al foro de, ";
        public string strTxtEmailclaveRequest2 = "Su contraseña es: -";
        public string strTxtEmailclaveRequest3 = "Para ir al foro pinche en el link siguiente: -";

        public string strTxtForumLogin = "Introducir Contraseña";
        public string strTxtErrorEnterclave = @"Contraseña \t- Introduzca la contraseña";
        public string strTxtclaveRequiredForForum = "Es necesario que inserte la contraseña para poder visualizar los mensajes.";
        public string strTxtForumclaveIncorrect = "Disculpe, la contraseña introducida es incorrecta.";
        public string strTxtAutoLogin = "Iniciar Sesión Automáticamente";
        public string strTxtLoginToForum = "Acceder al foro";

        public string strTxtNoUserProfileFound = "No existe ningún perfil disponible para este usuario";
        public string strTxtRegisteredToViewProfile = "Necesita ser miembro del foro para poder visualizar perfiles.";
        public string strTxtMemberNo = "Miembro No.";
        public string strTxtEmail = "Email";
        public string strTxtPrivate = "Privado";

        public string strTxtPostNewTopic = "Publicar Nuevo Tema";
        public string strTxtErrorTopicSubject = @"Asunto \t- Es necesario insertar un asunto";
        public string strTxtForumMemberSuspended = "Disculpe, la función no está activada porque su cuenta no está activa";

        public string strTxtNoPermissionToEditPost = "Disculpe Ud. no tiene permiso para editar este mensaje";
        public string strTxtReturnForumTopic = "Volver al tema en el foro";

        public string strTxtEmailTopicToFriend = "Enviar Tema a un amigo";
        public string strTxtFriendSentEmail = "El email ha sido enviado";
        public string strTxtFriendsName = "Nombre del amigo";
        public string strTxtFriendsEmail = "Email del amigo";
        public string strTxtYourName = "Su nombre";
        public string strTxtYourEmail = "Su email";
        public string strTxtSendEmail = "Mandar email";
        public string strTxtMessage = "Mensaje";

        public string strTxtEmailFriendMessage = "Pienso que podrías estar interesado en el mensaje que hay publicado en";
        public string strTxtFrom = "de";

        public string strTxtErrorFrinedsName = @"Nombre del amigo \t- Introduzca el nombre de su amigo";
        public string strTxtErrorFriendsEmail = @"Email del amigo \t- Introduzca una dirección de email v?lida";
        public string strTxtErrorYourName = @"Su nombre \t- Introduzca su nombre";
        public string strTxtErrorYourEmail = @"Su Email \t- Introduzca una dirección email válida";
        public string strTxtErrorEmailMessage = @"Mensaje \t- Introduzca un mensaje a su amigo";

        public string strTxtForumMembersList = "Lista de Miembros";
        public string strTxtMemberSearch = "Buscar Miembro";
        public string strTxtForumMembersOn = "miembros en";
        public string strTxtPageYouAerOnPage = "páginas, Ud. está en la Página";
        public string strTxtYourSearchMembersFound = "Según las opciones de su búsqueda, ha/han sido encontrada/s";
        public string strTxtMatches = "coincidencia/s";

        public string strTxtusuarioAlphabetically = "Nombre de usuarios alfabéticamente";
        public string strTxtNewForumMembersFirst = "Nuevos miembros primero";
        public string strTxtOldForumMembersFirst = "Viejos miembros primero";
        public string strTxtLocationAlphabetically = "Situación geográfica alfabética";

        public string strTxtRegistered = "Registrado el";
        public string strTxtSend = "Enviar a";
        public string strTxtNext = "Siguiente";
        public string strTxtPrevious = "Anterior";
        public string strTxtPage = "Página";

        public string strTxtErrorMemberSerach = @"Buscar Miembros\t- Inserte el nombre de miembro a buscar por";

        public string strTxtTextFormat = "Formato del texto";
        public string strTxtPreviewPost = "Vista previa";
        public string strTxtMode = "Modo";
        public string strTxtPrompt = "Prompt";
        public string strTxtBasic = "Básico";
        public string strTxtAddEmailLink = "Agregar link de email";
        public string strTxtList = "Lista";
        public string strTxtCentre = "Centrar";

        public string strTxtEnterBoldText = "Inserte el texto al que quiera dar formato en negrita";
        public string strTxtEnterItalicText = "Inserte el texto al que quiera dar formato en cursiva";
        public string strTxtEnterUnderlineText = "Inserte el texto al que quiera subrayar";
        public string strTxtEnterCentredText = "Inserte el texto al que quiera centrar";
        public string strTxtEnterHyperlinkText = "Inserte el texto a vincular";
        public string strTxtEnterHeperlinkURL = "Inserte la URL para crear un hipervínculo a";
        public string strTxtEnterEmailText = "Inserte el texto que se vinculará a la dirección email";
        public string strTxtEnterEmailMailto = "Inserte la dirección email para vincular a";
        public string strTxtEnterImageURL = "Inserte la URL de la imagen";
        public string strTxtEnterTypeOfList = "Tipo de lista";
        public string strTxtEnterEnter = "Entrar";
        public string strTxtEnterNumOrBlankList = "para numerar o dejar en blanco";
        public string strTxtEnterListError = "ERROR! Por favor, ingrese";
        public string strEnterLeaveBlankForEndList = "Dejar en blanco el ítem para ir al final de la lista";

        public string strTxtCut = "Cortar";
        public string strTxtCopy = "Copiar";
        public string strTxtPaste = "Pegar";
        public string strTxtBold = "Negrita";
        public string strTxtItalic = "Cursiva";
        public string strTxtUnderline = "Subrayar";
        public string strTxtLeftJustify = "Alinear a la Izquierda";
        public string strTxtCentrejustify = "Centrar";
        public string strTxtRightJustify = "Alinear a la derecha";
        public string strTxtUnorderedList = "Listar";
        public string strTxtOutdent = "Mover a la Izquierda";
        public string strTxtIndent = "Mover a la derecha";
        public string strTxtAddHyperlink = "Agregar Hipervínculo";
        public string strTxtAddImage = "Agregar Imagen";
        public string strTxtJavaScriptEnabled = "JavaScript debe estar activado en tu navegador para publicar un mensaje";
        public string strTxtShowSignature = "Mostrar Firma";
        public string strTxtEmailNotify = "Notificarme por email cuando exista una respuesta";
        public string strTxtUpdatePost = "Actualizar Mensaje";
        public string strTxtFontColour = "Color";

        public string strTxtRegisterNewUser = "Registrarse como un nuevo usuario";

        public string strTxtProfileusuarioLong = "Nombre con el que será conocido por el resto de los usuarios";
        public string strTxtRetypeclave = "Reescriba la contraseña";
        public string strTxtProfileEmailLong = "No es requerido, pero es necesario si desea ser informado cuando exista una respuesta o pierda su contraseña";
        public string strTxtShowHideEmail = "Mostrar mi email";
        public string strTxtShowHideEmailLong = "Oculta tu dirección de correo si quieres que otros usuarios no lo vean";
        public string strTxtSelectCountry = "Seleccionar País";
        public string strTxtProfileAutoLogin = "Iniciar Sesión automáticamente cuando regrese a la comunidad/foro";
        public string strTxtSignature = "Firma";
        public string strTxtSignatureLong = "Inserta una firma que quieras mostrar al final de tus mensajes";

        public string strTxtErrorusuarioChar = @"Nombre de Usuario \t- Debe contener más de 4 caracteres";
        public string strTxtErrorclaveChar = @"Contrase?a \t- Debe contener más de 4 caracteres";
        public string strTxtErrorclaveNoMatch = @"Error de Contraseña\t- La contraseña no coincide";
        public string strTxtErrorValidEmail = @"Email\t\t- Introduzca un email válido";
        public string strTxtErrorValidEmailLong = "Si no quiere insertar su email deje el cuadro vacío";
        public string strTxtErrorNoEmailToShow = "No puede mostrar su email si no lo ha proporcionado!";
        public string strTxtErrorSignatureToLong = @"Firma \t- Su firma tiene demasiados caracteres";
        public string strTxtUpdateProfile = "Actualizar perfil";

        public string strTxtUsrenameGone = @"Disculpe, el nombre de usuario está ocupado.\n\nPor favor, introduzca otro diferente.";
        public string strTxtEmailThankYouForRegistering = "Gracias por tomarse el tiempo en registrarse para usar el";
        public string strTxtEmailYouCanNowUseTheForumAt = "Sus datos de inicio de sesión se muestran a continuación. Ya puede comenzar a usar el foro de";
        public string strTxtEmailForumAt = "Foro en";
        public string strTxtEmailToThe = "al";

        public string strTxtEmailAMeesageHasBeenPosted = "Un mensaje ha sido publicado en el foro de";
        public string strTxtEmailClickOnLinkBelowToView = "Para ver el mensaje y/o responderlo, haga click en el link siguiente";
        public string strTxtEmailAMeesageHasBeenPostedOnForumNum = "Un mensaje ha sido publicado en el foro número";

        public string strTxtForumRulesAndPolicies = "Condiciones de Uso y Privacidad";
        public string srtTxtAccept = "Aceptar";
        public string strTxtCancel = "Cancelar";

        public string strTxtHi = "Hola";
        public string strTxtInterestingForumPostOn = "Mensaje interesante publicado en";
        public string strTxtForumLostclaveRequest = "Requerimiento de pérdida de contraseña";
        public string strTxtLockForum = "Foro cerrado";
        public string strTxtLockedTopic = "Tema bloqueado";
        public string strTxtUnLockTopic = "Tema des-bloqueado";
        public string strTxtTopicLocked = "Tema cerrado";
        public string strTxtUnForumLocked = "Foro des-bloqueado";
        public string strTxtThisTopicIsLocked = "Este tema esta cerrado.";
        public string strTxtThatYouAskedKeepAnEyeOn = "que solicitó que le avisáramos.";
        public string strTxtTheTopicIsNowDeleted = "El tema a sido borrado.";
        public string strTxtOf = "de";
        public string strTxtTheTimeNowIs = "La fecha actual es";
        public string strTxtYouLastVisitedOn = "Su Última visita fue el";
        public string strTxtSendMsg = "Enviar mensaje personal";
        public string strTxtSendPrivateMessage = "Enviar un Mensaje Privado";
        public string strTxtActiveUsers = "Usuarios activos";
        public string strTxtGuestsAnd = "Invitado(s) y";
        public string strTxtMembers = "Miembro(s)";
        public string strTxtPreview = "Vista previa";
        public string strTxtThereIsNothingToPreview = "No hay nada que previsualizar";
        public string strTxtEnterTextYouWouldLikeIn = "Introduzca el texto que le gustaría en";
        public string strTxtEmailAddressAlreadyUsed = "Lo sentimos, la dirección email introducida está siendo utilizada por otro usuario.";
        public string strTxtIP = "IP";
        public string strTxtIPLogged = "IP registrada";
        public string strTxtPages = "Páginas";
        public string strTxtCharacterCount = "Contador de Caracteres";
        public string strTxtAdmin = "Administración";

        public string strTxtType = "Grupo";
        public string strTxtActive = "Activo";
        public string strTxtGuest = "Invitado";
        public string strTxtAccountStatus = "Estado de cuenta";
        public string strTxtNotActive = "No Activo";

        public string strTxtEmailRequiredForActvation = "Requerido para recibir el email de activación";
        public string strTxtToActivateYourMembershipFor = "Para activar su cuenta de usuario de";
        public string strTxtForumClickOnTheLinkBelow = "Presione en el link siguiente";
        public string strTxtForumAdmin = "Administrador";
        public string strTxtViewLastPost = "Ver Último mensaje";
        public string strTxtSelectAvatar = "Seleccionar Avatar";
        public string strTxtAvatar = "Avatar";
        public string strTxtSelectAvatarDetails = "Pequeño icono que aparecerá en sus mensajes. Seleccione uno de la lista o introduzca la ruta de uno exclusivo (32 x 32 pixeles).";
        public string strTxtPixels = " pixels).";
        public string strTxtForumCodesInSignature = "puede ser usado en su firma";

        public string strTxtHighPriorityPost = "Anuncio";
        public string strTxtHighPriorityPostLocked = "Anuncio cerrado";
        public string strTxtHotTopicNewReplies = "Tema caliente con nuevas resp.";
        public string strTxtHotTopic = "Tema caliente sin nuevas resp.";
        public string strTxtOpenTopic = "Tema abierto sin nuevas resp.";
        public string strTxtOpenTopicNewReplies = "Tema abierto con nuevas resp.";
        public string strTxtPinnedTopic = "Tema preferente";

        public string strTxtOpenForum = "Foro abierto sin nuevas resp.";
        public string strTxtOpenForumNewReplies = "Foro abierto con nuevas resp.";
        public string strTxtReadOnly = "Sólo lectura sin nuevas resp.";
        public string strTxtReadOnlyNewReplies = "Sólo lectura con nuevas resp.";
        public string strTxtclaveRequired = "Requiere Contraseña";
        public string strTxtNoAccess = "Sin acceso";

        public string strTxtFont = "Fuente";
        public string strTxtSize = "Tamaño";
        public string strTxtForumCodes = "Los códigos del foro";

        public string strTxtPriority = "Preferente";
        public string strTxtNormal = "Normal";
        public string strTxtTopAllForums = "Anuncio (en todos los foros)";
        public string strTopThisForum = "Anuncio (en este foro)";

        public string strTxtMarkAllPostsAsRead = "Marcar todos los mensajes como leídos";
        public string strTxtDeleteCookiesSetByThisForum = "Eliminar cookies creados por este foro";

        public string strTxtYouCanUseForumCodesToFormatText = "Puede usar los siguientes códigos de formato de texto para usar en el foro";
        public string strTxtTypedForumCode = "Tipos de código para el foro";
        public string strTxtConvetedCode = "Código convertido";
        public string strTxtTextFormating = "Formato del texto";
        public string strTxtImagesAndLinks = "Imágenes y Links";
        public string strTxtFontTypes = "Tipos de fuente";
        public string strTxtFontSizes = "Tamaños de fuente";
        public string strTxtFontColours = "Colores de fuente";
        public string strTxtEmoticons = "Iconos de Emoción";
        public string strTxtFontSize = "Tamaño de fuente";
        public string strTxtMyLink = "Mi link";
        public string strTxtMyEmail = "Mi email";

        public string strTxtAccessDenied = "Acceso Denegado";
        public string strTxtInsufficientPermison = "Sólo miembros con un nivel suficiente de privilegios pueden acceder a esta página.";

        public string strTxtYourForumMemIsNowActive = @"Gracias por registrarse.<br /><br /><span class As String =""lgText"">Su cuenta personal está ahora activa.</span>";
        public string strTxtErrorWithActvation = "Se han detectado algunos problemas a la hora de activar tu cuenta personal.<br /><br />Por favor contacte con el administrador del foro";

        public string strTxtYouShouldReceiveAnEmail = "Debería recibir un email en los pr?ximos 15 minutos.<br />No olvide presionar en el link que aparecer? en el email para activar su cuenta personal.";
        public string strTxtThankYouForRegistering = "Gracias por registrarse";
        public string strTxtIfErrorActvatingMembership = "Si tiene algún problema en activar su cuenta por favor cont?ctenos";

        public string strTxtActiveForumUsers = "Miembros Activos";
        public string strTxtAddMToActiveUsersList = "Agregarme a la lista de usuarios conectados";
        public string strTxtLoggedIn = "Iniciada la sesión";
        public string strTxtLastActive = "Última actividad";
        public string strTxtBrowser = "Navegador";
        public string strTxtOS = "SO";
        public string strTxtMinutes = "minutos";
        public string strTxtAnnoymous = "Anónimos";

        public string strTxtMessageNotPosted = "El Mensaje no ha sido publicado";
        public string strTxtDoublePostingIsNotPermitted = "La duplicación no está permitida; su mensaje ya ha sido publicado anteriormente.";
        public string strTxtSpammingIsNotPermitted = "?Practicar Spam no está permitido!";
        public string strTxtYouHaveExceededNumOfPostAllowed = "Ha excedido la cantidad de Publicaciones permitidas en un periodo de tiempo determinado.<br /><br />Espere unos minutos para volver a publicar el próximo mensaje.";
        public string strTxtYourMessageNoValidSubjectHeading = "Su mensaje no contiene un asunto.";

        public string strTxtActiveTopics = "Temas Activos";
        public string strTxtLastVisitOn = "Última visita el";
        public string strTxtLastFifteenMinutes = "Últimos 15 minutos";
        public string strTxtLastThirtyMinutes = "Últimos 30 minutos";
        public string strTxtLastFortyFiveMinutes = "Últimos 45 minutos";
        public string strTxtLastHour = "Última hora";
        public string strTxtLastTwoHours = "Últimas 2 horas";
        public string strTxtYesterday = "ayer";
        public string strTxtShowActiveTopicsSince = "Mostrar temas activos desde";
        public string strTxtNoActiveTopicsSince = "No hay temas activos desde";
        public string strTxtToDisplay = "a mostrar";
        public string strTxtThereAreCurrently = "Existen en estos momentos";

        public string strTxtNewPMsClickToGoNowToPM = @"Nuevo(s) mensaje(s) privado(s).\n\nPresione aceptar para ir a su centro de mensajes privados.";

        public string strTxtFewYears = "algunos años";
        public string strTxtWeek = "semana";
        public string strTxtTwoWeeks = "dos semanas";
        public string strTxtMonth = "mes";
        public string strTxtTwoMonths = "dos meses";
        public string strTxtSixMonths = "6 meses";
        public string strTxtYear = "año";

        public string strTxtBlack = "Negro";
        public string strTxtWhite = "Blanco";
        public string strTxtBlue = "Azul";
        public string strTxtRed = "Rojo";
        public string strTxtGreen = "Verde";
        public string strTxtYellow = "Amarillo";
        public string strTxtOrange = "Naranja";
        public string strTxtBrown = "Marrón";
        public string strTxtMagenta = "Magenta";
        public string strTxtCyan = "Cyan";
        public string strTxtLimeGreen = "Verde Lima";

        public string strTxtHasBeenSentTo = "ha sido enviado a";
        public string strTxtCharactersInYourSignatureToLong = "caracteres en su firma, Su firma no debe contener más de 200 caracteres";
        public string strTxtSorryYourSearchFoundNoMembers = "Lo sentimos, su búsqueda no ha encontrado ningún miembro";
        public string strTxtCahngeOfEmailReactivateAccount = "Si cambia su dirección de email le mandaremos un correo de activación otra vez";
        public string strTxtAddToBuddyList = "Agregar";

        public string strTxtYourEmailAddressHasBeenChanged = "Su dirección de correo ha cambiado, <br />tendrá que re-activar Su cuenta del foro antes de poder poner mensajes.";
        public string strTxtYouShouldReceiveAReactivateEmail = "debería recibir un correo dentro 15 minutos.<bar>Piche en el hipervínculo de este correo para re-activar tu cuenta del foro.";

        public string strTxtSignaturePreview = "Previsualizar firma";
        public string strTxtPostedMessage = "Mensaje enviado";

        public string strTxtMemberlist = "Lista de miembros";
        public string strTxtForums = "Foro(s)";
        public string strTxtOurUserHavePosted = "Nuestros miembros han escrito";
        public string strTxtInTotalThereAre = "En total tenemos";
        public string strTxtOnLine = "conectados";
        public string strTxtWeHave = "Nosotros tenemos";
        public string strTxtActivateAccount = "Cuentas activas";
        public string strTxtSorryYouDoNotHavePermissionToPostInTisForum = "Perdón pero Ud. no tiene permiso para poner nuevos temas en este foro";
        public string strTxtSorryYouDoNotHavePerimssionToReplyToPostsInThisForum = "Perdón pero Ud. no tiene permiso para responder temas en este foro";
        public string strTxtSorryYouDoNotHavePerimssionToReplyIPBanned = "Perdón pero Ud. no puede responder a temas en este foro, su dirección IP o en su defecto el rango no esta permitido.<br />Por favor p?ngase en contacto con el Administrador para verificar este error.";
        public string strTxtLoginSm = "Iniciar sesión";
        public string strTxtYourProfileHasBeenUpdated = "Su perfil fue actualizado.";
        public string strTxtPosted = "Escrito el:";
        public string strTxtBackToTop = "Volver al comienzo";
        public string strTxtNewclave = "Nueva contraseña";
        public string strTxtRetypeNewclave = "Repetir la nueva contraseña";
        public string strTxtRegards = "Saludos";
        public string strTxtClickTheLinkBelowToUnsubscribe = "Para des-suscribirse a la notificación por email en este foro haga click debajo ";
        public string strTxtPostsPerDay = "mensajes por día";
        public string strTxtGroup = "Grupo";
        public string strTxtLastVisit = "Última visita";
        public string strTxtPrivateMessage = "Mensaje privado";
        public string strTxtSorryFunctionNotPermiitedIPBanned = "Perdón, pero esta función no esta disponible, su dirección IP o en su defecto el rango no esta permitido.<br />Por favor póngase en contacto con el Administrador para verificar este error.";
        public string strTxtEmailAddressBlocked = "Perdón, pero la dirección de email o el dominio ingresado están bloqueados en este foro";
        public string strTxtTopicAdmin = "Administrador de Temas";
        public string strTxtMovePost = "Mover mensaje";
        public string strTxtPrevTopic = "Tema anterior";
        public string strTxtTheMemberHasBeenDleted = "El miembro a sido borrado.";
        public string strTxtThisPageWasGeneratedIn = "Esta pagina fue generada en";
        public string strTxtSeconds = "segundos.";
        public string strTxtEditBy = "Editado por";
        public string strTxtWrote = "escribió";
        public string strTxtEnable = "Habilitado";
        public string strTxtToFormatPosts = "para el formato del mensaje";
        public string strTxtFlashFilesImages = "Archivos/Imagenes Flash";
        public string strTxtSessionIDErrorCheckCookiesAreEnabled = "Un error de seguridad ha ocurrido con la autenticación.<br />Aseg?rese que tiene habilitada la opci?n de aceptar cookies, o que no esta visitando la pagina, detr?s de un proxy o de manera local.";
        public string strTxtName = "Nombre";
        public string strTxtModerators = "Moderadores";
        public string strTxtMore = "mas...";
        public string strTxtNewRegSuspendedCheckBackLater = "Disculpe pero el registro esta suspendido temporalmente, regrese pronto para verificar si esta habilitado, gracias.";
        public string strTxtMoved = "Movido: ";
        public string strTxtNoNameError = @"Nombre \t\t- Ingrese su nombre";
        public string strTxtHelp = "Ayuda";

        public string strTxtPrivateMessenger = "Mensajería Privada";
        public string strTxtUnreadMessage = "Mensaje no leído";
        public string strTxtReadMessage = "Leer Mensaje";
        public string strTxtNew = "nuevo";
        public string strTxtYouHave = "Ud. tiene";
        public string strTxtNewMsgsInYourInbox = "nuevos mensaje(s)en su bandeja de entrada!";
        public string strTxtNoneSelected = "No hay nada seleccionado";
        public string strTxtAddBuddy = "Agregar a contactos";

        public string strTxtSelectMember = "Seleccionar miembro";
        public string strTxtSelect = "Seleccionar";
        public string strTxtNoMatchesFound = "No se encontraron resultados";

        public string strTxtLastFourHours = "Últimas 4 horas";
        public string strTxtLastSixHours = "Últimas 6 horas";
        public string strTxtLastEightHours = "Últimas 8 horas";
        public string strTxtLastTwelveHours = "Últimas 12 horas";
        public string strTxtLastSixteenHours = "Últimas 16 horas";

        public string strTxtYou = "Ud.";
        public string strTxtCan = "puede";
        public string strTxtCannot = "no puede";
        public string strTxtpostNewTopicsInThisForum = "publicar nuevos temas";
        public string strTxtReplyToTopicsInThisForum = "responder a temas";
        public string strTxtEditYourPostsInThisForum = "editar sus respuestas";
        public string strTxtDeleteYourPostsInThisForum = "borrar sus respuestas";
        public string strTxtCreatePollsInThisForum = "crear encuestas";
        public string strTxtVoteInPOllsInThisForum = "votar en las encuestas";

        public string strTxtRegistrationDetails = "Detalles del registro";
        public string strTxtProfileInformation = "Información del perfil";
        public string strTxtForumPreferences = "Preferencias del foro";
        public string strTxtICQNumber = "UIN ICQ Messenger";
        public string strTxtAIMAddress = "Dirección AIM Messenger";
        public string strTxtMSNMessenger = "Dirección MSN Messenger";
        public string strTxtYahooMessenger = "Dirección Yahoo Messenger";
        public string strTxtOccupation = "Ocupación";
        public string strTxtInterests = "Intereses";
        public string strTxtDateOfBirth = "Fecha de nacimiento";
        public string strTxtNotifyMeOfReplies = "Notificarme de las respuestas en mis mensajes";
        public string strTxtSendsAnEmailWhenSomeoneRepliesToATopicYouHavePostedIn = "Enviarme un email cuando alguien haya respondido a un mensaje dejado por mi. Esto lo puede cambiar.";
        public string strTxtNotifyMeOfPrivateMessages = "Notificarme vía email cuando reciba un mensaje privado.";
        public string strTxtAlwaysAttachMySignature = "Siempre agregar mi firma en todos los mensajes.";
        public string strTxtEnableTheWindowsIEWYSIWYGPostEditor = "Habilitar el editor de Windows IE 5 + WYSIWYG";
        public string strTxtTimezone = "Setear la diferencia horaria.";
        public string strTxtPresentServerTimeIs = "La hora en el servidor del foro es: ";
        public string strTxtDateFormat = "Formato de Fecha";
        public string strTxtDayMonthYear = "Dia/Mes/Año";
        public string strTxtMonthDayYear = "Mes/Dia/Año";
        public string strTxtYearMonthDay = "Año/Mes/Dia";
        public string strTxtYearDayMonth = "Año/Dia/Mes";
        public string strTxtHours = "horas";
        public string strTxtDay = "Día";
        public string strTxtCMonth = "Mes";
        public string strTxtCYear = "Año";
        public string strTxtRealName = "Nombre real";
        public string strTxtMemberTitle = "Su titulo";

        public string strTxtCreateNewPoll = "Crear nueva encuesta";
        public string strTxtPollQuestion = "Pregunta de la encuesta";
        public string strTxtPollChoice = "Opciones de la encuesta";
        public string strTxtErrorPollQuestion = @"Pregunta de la encuesta \t- Ingrese la pregunta de la encuesta";
        public string strTxtErrorPollChoice = @"Opciones de la encuesta \t- Ingrese al menos dos opciones para esta encuesta";
        public string strTxtSorryYouDoNotHavePermissionToCreatePollsForum = "Perdón, pero Ud. no tiene permiso de crear encuestas en este foro";
        public string strTxtAllowMultipleVotes = "¿Permitir múltiples votos en la encuesta?";
        public string strTxtMakePollOnlyNoReplies = "Hacer solo una encuesta (no permitir respuestas)";
        public string strTxtYourNoValidPoll = "Su encuesta no contiene una pregunta u opciones válidas.";
        public string strTxtPoll = "Encuesta:";
        public string strTxtVote = "Votar";
        public string strTxtVotes = "Votos";
        public string strTxtCastMyVote = "Votar";
        public string strTxtPollStatistics = "Estadísticas de la encuesta";
        public string strTxtThisTopicIsClosedNoNewVotesAccepted = "Este tema esta cerrado, no acepta nuevos votos";
        public string strTxtYouHaveAlreadyVotedInThisPoll = "Ud. ya a votado en esta encuesta";
        public string strTxtThankYouForCastingYourVote = "Gracias por votar.";
        public string strsTxYouCanNotNotVoteInThisPoll = "Ud. no puede votar en esta encuesta";
        public string strTxtYouDidNotSelectAChoiceForYourVote = @"Perdón pero su voto no puede ser sumado.\n\nUd. no selecciono ninguna opción.";
        public string strTxtThisIsAPollOnlyYouCanNotReply = "Esto es una encuestas solamente, Ud. no puede publicar mensajes.";

        public string strTxtWatchThisTopic = "Seguir este tema con futuras respuestas";
        public string strTxtUn = "No-";
        public string strTxtWatchThisForum = "Seguir este foro con futuros temas";
        public string strTxtYouAreNowBeNotifiedOfPostsInThisForum = @"Ud. ahora será notificado de todas las respuestas.\n\nPara sacar el seguimiento hacer click en el link \'Dar de baja seguimiento a nuevas respuestas\' en la parte inferior de la pagina.";
        public string strTxtYouAreNowNOTBeNotifiedOfPostsInThisForum = @"Ud. ahora no será notificado de los mensajes en este tema.\n\nPara re-activar el seguimiento en este foro hacer click en en el link\'Seguir este foro con futuros temas\' en la parte inferior de la pagina.";
        public string strTxtYouWillNowBeNotifiedOfAllReplies = @"Ud. ahora será notificado por email de las respuestas en este tema.\n\nPara sacar el seguimiento en este tema hacer click en el link\'Dar de baja seguimiento a nuevas respuestas\' en la parte inferior de la pagina.";
        public string strTxtYouWillNowNOTBeNotifiedOfAllReplies = @"Ud. ahora no será notificado por email de las respuestas en este tema.\n\nPara re-activar el seguimiento en este tema hacer click en el link \'Seguir este tema con futuras respuestas\' en la parte inferior de la pagina.";

        public string strTxtEmailMessenger = "Email Messenger";
        public string strTxtRecipient = "Destinatario/a";
        public string strTxtNoHTMLorForumCodeInEmailBody = "Por favor asegúrese de que el email es enviado en formato plano de texto (no en HTML o con c?digos del foro).<br /><br />La direcci?n del email de respuesta le ser? asignado a usted.";
        public string strTxtYourEmailHasBeenSentTo = "Su email a sido enviado a";
        public string strTxtYouCanNotEmail = "Ud. no puede enviar un email";
        public string strTxtYouDontHaveAValidEmailAddr = "Ud. no tiene una dirección de email valida.";
        public string strTxtTheyHaveChoosenToHideThierEmailAddr = "Ha elegido ocultar su dirección de e-mail.";
        public string strTxtTheyDontHaveAValidEmailAddr = "No tienen una dirección de email válida en su perfil.";
        public string strTxtSendACopyOfThisEmailToMyself = "Enviarme una copia de este email";
        public string strTxtTheFollowingEmailHasBeenSentToYouBy = "El siguiente email a sido enviado a ud. por";
        public string strTxtFromYourAccountOnThe = "Desde su cuenta";
        public string strTxtIfThisMessageIsAbusive = "Si este mensaje es un correo basura o lo encuentra ofensivo por favor contacte con el Administrador del foro a la siguiente dirección";
        public string strTxtIncludeThisEmailAndTheFollowing = "Incluir este mail en el seguimiento";
        public string strTxtReplyToEmailSetTo = "Por favor asegúrese de que la dirección de respuesta de este correo ha sido asignada a";
        public string strTxtMessageSent = "Mensaje enviado";

        public string strTxtImageUpload = "Subir Imagen";
        public string strTxtFileUpload = "Subir Archivo";
        public string strTxtAvatarUpload = "Subir Avatar";
        public string strTxtUpload = "Subir";

        public string strTxtImage = "Imagen";
        public string strTxtImagesMustBeOfTheType = "La imagen debe ser del tipo";
        public string strTxtAndHaveMaximumFileSizeOf = "y tener un tamaño máximo de";
        public string strTxtImageOfTheWrongFileType = "No es correcto el tipo de imagen subido";
        public string strTxtImageFileSizeToLarge = "El tamaño de la imagen es muy grande";
        public string strTxtMaximumFileSizeMustBe = "El tamaño de la imagen es muy grande en";

        public string strTxtFile = "Fichero";
        public string strTxtFilesMustBeOfTheType = "Los Ficheros deben ser del tipo";
        public string strTxtFileOfTheWrongFileType = "El fichero subido es de tipo incorrecto";
        public string strTxtFileSizeToLarge = "El tamaño del fichero excede en";

        public string strTxtPleaseWaitWhileFileIsUploaded = "Por favor aguarde mientras el archivo es subido al servidor.";
        public string strTxtPleaseWaitWhileImageIsUploaded = "Por favor aguarde mientras la imagen es subida al servidor.";

        public string strTxtForumClosed = "Foro Cerrado";
        public string strTxtSorryTheForumsAreClosedForMaintenance = "Disculpe, pero el foro esta actualmente cerrado por mantenimiento.<br />Por favor intente nuevamente luego.";

        public string strTxtReportPost = "Reportar mensaje(s)";
        public string strTxtSendReport = "Enviar reporte";
        public string strTxtProblemWithPost = "Problema con el mensaje";
        public string strTxtPleaseStateProblemWithPost = "Por favor afirme o consigne el texto debajo, una copia del mensaje será enviada a los administradores y/o moderadores del foro, ellos podrán darle un trato apropiado.";
        public string strTxtTheFollowingReportSubmittedBy = "El siguiente reporte a sido enviado por";
        public string strTxtWhoHasTheFollowingIssue = "quien tuvo el siguiente tema con este mensaje";
        public string strTxtToViewThePostClickTheLink = "Para ver el mensaje, haga click en el link de abajo";
        public string strTxtIssueWithPostOn = "Tema con mensaje en";
        public string strTxtYourReportEmailHasBeenSent = "Su email fue enviado a un moderador del foro y/o un administrador, ellos podrán analizar el caso.";

        public string strTxtImportantTopics = "Temas importantes";
        public string strTxtQuickLogin = "Inicio Rápido";
        public string strTxtThisTopicWasStarted = "Este tema se ha iniciado: ";
        public string strTxtResendActivationEmail = "Reenviar el correo de activación";
        public string strTxtNoOfStars = "N?mero de estrellas";
        public string strTxtOnLine2 = "Conectados";
        public string strTxtIeSpellNotDetected = "ieSpell no detectado. Haga un click en OK para ir a la Página de descarga.";
        public string strTxtstrTxtOrderedList = "Lista ordenada";
        public string strTxtTextColour = "Color de texto";
        public string strTxtstrSpellCheck = "Chequear Spell";
        public string strTxtCode = "Clave";
        public string strTxtCodeandFixedWidthData = "Code and Fixed Width Data";
        public string strTxtQuoting = "Citado";
        public string strTxtMyCodeData = "My Code or Fixed Width Data";
        public string strTxtQuotedMessage = "Mensaje citado";
        public string strTxtWithusuario = "With usuario";
        public string strTxtOK = "OK";
        public string strTxtGo = "Ir";
        public string strTxtDataBasedOnActiveUsersInTheLastXMinutes = "Estos datos proceden de usuarios conectados en los Últimos diez minutos";
        public string strTxtSoftwareVersion = "Versión del Software";
        public string strTxtForumMembershipNotAct = "Su acceso al foro no ha sido aun activado!";
        public string strTxtMustBeRegisteredToPost = "Para poner mensajes en este foro debe ser un usuario registrado.";
        public string strTxtSettings = "Panel de Control";
        public string strTxtMemberCPMenu = "Panel de Control del Usuario";
        public string strTxtYouCanAccessCP = "Usted puede acceder a las características del foro y cambiar su configuraci?n personal a trav?s del foro";
        public string strTxtEditMembersSettings = "Editar los ajustes del foro para estos miembros";
        public string strTxtSecurityCodeConfirmation = "Confirmación del código de seguridad (obligatorio)";
        public string strTxtUniqueSecurityCode = "Código único de seguridad";
        public string strTxtConfirmSecurityCode = "Confirmar el código de seguridad* ";
        public string strTxtEnter6DigitCode = "Por favor, introduzca el código de 6 dígitos en el formato mostrado en la imagen superior.<br />Solo se permiten números, '0' es el número cero.";
        public string strTxtErrorSecurityCode = @"Código de seguridad \t- Debe introducir el código de seguridad de 6 dígitos";
        public string strTxtSecurityCodeDidNotMatch = @"Lo sentimos, el código de seguridad introducido no concuerda con el mostrado.\n\nUn nuevo código de seguridad ha sido generado.\n\nPor favor asegúrese de tener activadas las cookies en su navegador.";
        public string strTxtCookiesMustBeEnabled = "Para poder ver la imágenes, su navegador debe tener activadas las cookies.";

        public string strTxtSuccessfulLogin = "Ingreso Correcto";
        public string strTxtSuccessfulLoginReturnToForum = "Su ingreso ha sido correcto, por favor espere mientras es devuelto al foro";
        public string strTxtUnSuccessfulLoginText = "Su ingreso no fue correcto debido a un problema con la cookie. <br /><br />Por favor asegúrese de que todas las cookies están activas en su navegador para este sitio web.";
        public string strTxtUnSuccessfulLoginReTry = "Haga un click aquí para reintentar conectar al foro.";
        public string strTxtToActivateYourForumMem = "Para darse de alta como miembro del foro debe hacer un click sobre el enlace en el correo de activación que debe haber recibido a continuaci?n de haberse registrado.";

        public string strTxtEmailNotificationSubscriptions = "Correo de aviso de suscripciones";
        public string strTxtSelectForumErrorMsg = @"Seleccione foro\t- Seleccione un foro para suscribirse a el";
        public string strTxtYouHaveNoSubToEmailNotify = "No tiene suscripciones para correo de aviso";
        public string strTxtThatYouHaveSubscribedTo = "Se ha suscrito para correo de notificación";
        public string strTxtUnsusbribe = "Desuscribirse";
        public string strTxtAreYouWantToUnsubscribe = "¿Esta usted seguro de que quiere desuscribirse de estos?";

        public string strTxtFontStyle = "Estilo de Fuente";
        public string strTxtBackgroundColour = "Color de Fondo";
        public string strTxtUndo = "Deshacer";
        public string strTxtRedo = "Rehacer";
        public string strTxtJustify = "Justificar";
        public string strTxtToggleHTMLView = "Botón de vista HTML";
        public string strTxtAboutRichTextEditor = "Acerca de Rich Text Editor";
        public string strTxtImageURL = "Imagen URL";
        public string strTxtAlternativeText = "Texto Alternativo";
        public string strTxtLayout = "Trazado";
        public string strTxtAlignment = "Alineamiento";
        public string strTxtBorder = "Borde";
        public string strTxtSpacing = "Espaciado";
        public string strTxtHorizontal = "Horizontal";
        public string strTxtVertical = "Vertical";
        public string strTxtRows = "Filas";
        public string strTxtColumns = "Columnas";
        public string strTxtWidth = "Ancho";
        public string strTxtCellPad = "Separación de contenido";
        public string strTxtCellSpace = "Separación entre celdas";
        public string strTxtInsertTable = "Insertar Tabla";
        public string strTxtOrderedList = "Lista ordenada";

        public string strTxtSubscribeToForum = "Suscríbase a la notificación vía email de nuevos correos de terceros";
        public string strTxtSelectForumToSubscribeTo = "Seleccione un foro para suscribirse a el";

        public string strTxtOnlineStatus = "Conectado";
        public string strTxtOffLine = "Desconectado";

        public string strTxtConfirmOldPass = "Confirme su Contraseña anterior";
        public string strTxtConformOldPassNotMatching = @"La confirmación de su contraseña no coinside con la de su expediente .\n\nSi quiere cambiar su contraseña, porfavor ingrese su antigua contraseña correctamente.";

        public string strTxtSub = "Sub";
        public string strTxtHidden = "Oculto";
        public string strTxtShowPost = "Mostrar Mensajes";
        public string strTxtHidePost = "Ocultar Mensaje";
        public string strTxtAreYouSureYouWantToHidePost = "¿Está seguro de que quiere ocultar este mensaje?";
        public string strTxtModeratedPost = "Mensaje Moderado";
        public string strTxtYouArePostingModeratedForum = "Ud. ha posteado en un foro mdoerado.";
        public string strTxtBeforePostDisplayedUsuariosised = "Antes de que su mensaje sea exhibido en el foro primero necesitará ser autorizado por un administrador o un asesor del foro.";
        public string strTxtHiddenTopics = "Mensajes Ocultos";
        public string strTxtVerifiedBy = "Verificado Por";

        public string[,] saryEmoticons = new string[37, 4];

        public string strTxtToYourPrivateMessenger = "a su Mensajería Privada";
        public string strTxtPmIntroduction = "Desde su Mensajería Privada puede enviar y recibir Mensajes Privados entre Ud. y otros miembros del Foro, sabiendo que sus mensajes no ser?n vistos por otros miembros del Foro.";
        public string strTxtInboxStatus = "Estado de la Bandeja de Entrada";
        public string strTxtGoToYourInbox = "Ir a su Bandeja de Entrada";
        public string strTxtNoNewMsgsInYourInbox = "No tiene nuevos mensajes en su Bandeja de Entrada.";
        public string strTxtYourLatestPrivateMessageIsFrom = "Su ultimo mensaje privado es de";
        public string strTxtSentOn = "enviado el";
        public string strTxtPrivateMessengerOverview = "Descripción de la Mensajeriá Privada";
        public string strTxtInboxOverview = "Este es donde son almacenados todos sus Mensajes Privados entrantes y desde donde Ud. puede ver o borrar cualquier Mensaje Privado que reciba. Este funciona muy similar la Bandeja de Entrada de su e-mail.";
        public string strTxtOutboxOverview = "Aquí es donde se almacenan los Mensajes Privados salientes y desde donde Ud. puede ver los Mensajes Privados que ha enviado. Los Mensajes Privados permanecen en su Bandeja de Salida hasta que el destinatario los borra.";
        public string strTxtBuddyListOverview = "Este es como una libreta de direcciones. Ud. puede añadir o borrar a miembros del foro de su Lista de Amigos para rápida referencia. Ud. también puede bloquear a esos miembros de los cuales no desea recibir Mensajes Privados.";
        public string strTxtNewMsgOverview = "Este es donde Ud. puede crear nuevos Mensajes Privados y enviarlos a otros miembros del foro. ";


        public string strTxtInbox = "Bandeja de Entrada";
        public string strTxtBuddyList = "Lista de Amigos";
        public string strTxtNewPrivateMessage = "Nuevos Mensajes Privados";
        public string strTxtNoPrivateMessages = "Ud. no tiene Mensajes Privados en su";
        public string strTxtRead = "Leer";
        public string strTxtMessageTitle = "Título del Mensaje";
        public string strTxtMessageFrom = "Mensaje de";
        public string strTxtDate = "Fecha";
        public string strTxtBlock = "bloquear";
        public string strTxtSentBy = "Enviar por";
        public string strTxtDeletePrivateMessageAlert = "¿Está seguro que desea borrar los Mensajes Privados seleccionados?";
        public string strTxtPrivateMessagesYouCanReceiveAnother = "Mensajes Privados, Ud. puede recibir otros";
        public string strTxtOutOf = "de";
        public string strTxtPreviousPrivateMessage = "Mensajes Privados Anteriores";
        public string strTxtMeassageDeleted = "Los Mensajes Privados han sido borrados de su Bandeja de Entrada";

        public string strTxtSorryYouDontHavePermissionsPM = "Lo sentimos, no tiene permiso para ver este Mensaje Privado.";
        public string strTxtYouDoNotHavePermissionViewPM = "No tiene permiso para ver este Mensaje Privado.";
        public string strTxtNotificationReadPM = "Leer la notificación del Mensaje Privado";
        public string strTxtReplyToPrivateMessage = "Contestar al Mensaje Privado";
        public string strTxtAddToBuddy = "Agregar";
        public string strTxtThisIsToNotifyYouThat = "Esto es para notificarle que";
        public string strTxtHasReadPM = "ha leído el Mensaje Privado";
        public string strTxtYouSentToThemOn = "enviaste a ellos en";


        public string strTxtSendNewMessage = "Enviar nuevo mensaje";
        public string strTxtPostMessage = "Colocar mensaje";
        public string strTxtEmailNotifyWhenPMIsRead = "Avisarme vía e-mail cuando el mensaje sea leído";
        public string strTxtTousuario = "Para";
        public string strSelectFormBuddyList = "o elija de la Lista de Amigos";
        public string strTxtNoPMSubjectErrorMsg = @"Asunto \t\t- escriba un asunto para su nuevo Mensaje Privado";
        public string strTxtNoTousuarioErrorMsg = @"Para \t- escriba o selecciona un Usuario para enviar su Mensaje Privado";
        public string strTxtNoPMErrorMsg = @"Mensaje \t\t- escriba su Mensaje Privado para enviar";
        public string strTxtSent = "Enviar";

        public string strTxtAPrivateMessageHasBeenPosted = "Un Mensaje Privado ha sido colocado para Ud. en";
        public string strTxtClickOnLinkBelowForPM = "Presione en la liga de abajo para ver el Mensaje Privado";
        public string strTxtNotificationPM = "Notificación de Mensaje Privado";
        public string strTxtTheusuarioCannotBeFound = "El usuario que ha proporcionado no ha sido encontrado.";
        public string strTxtYourPrivateMessage = "Su Mensaje Privado";
        public string strTxtHasNotBeenSent = "no ha sido enviado!";
        public string strTxtAmendYourPrivateMessage = "Enmiende su Mensaje Privado";
        public string strTxtReturnToYourPrivateMessenger = "Regresar a su Mensajería Privada";
        public string strTxtYouAreBlockedFromSendingPMsTo = "Ha sido bloqueado para enviar Mensajes Privados a";
        public string strTxtHasExceededMaxNumPPMs = "ha excedido el número máximo de Mensajes Privados que se te permiten recibir";
        public string strTxtHasSentYouPM = "ha enviado un Mensaje Privado con el siguiente asunto";
        public string strTxtToViewThePrivateMessage = "Para ver el Mensaje Privado";


        public string strTxtNoBuddysInList = "No tiene amigos en su Lista de Amigos";
        public string strTxtDeleteBuddyAlert = "¿Esta seguro de borrar este amigo de su Lista de Amigos?";
        public string strTxtNoBuddyErrorMsg = @"Nombre del miembro \t- proporcione un miembro del Foro para añadir a tu Lista de Amigos";
        public string strTxtBuddy = "Amigo";
        public string strTxtDescription = "Descripción";
        public string strTxtContactStatus = "Estado del contacto";
        public string strTxtThisPersonCanNotMessageYou = "Esta persona no puede mensajearle";
        public string strTxtThisPersonCanMessageYou = "Esta persona puede mensajearle";
        public string strTxtAddNewBuddyToList = "Añadir nuevo Amigo a la lista";
        public string strTxtMemberName = "Nombre del Miembro";
        public string strTxtAllowThisMemberTo = "Permitir a este miembro";
        public string strTxtMessageMe = "Mensajearme";
        public string strTxtNotMessageMe = "No mensajearme";
        public string strTxtHasNowBeenAddedToYourBuddyList = "ahora ha sido añadido a su Lista de Amigos";
        public string strTxtIsAlreadyInYourBuddyList = "esta ya en su Lista de Amigos";
        public string strTxtUserCanNotBeFoundInDatabase = @"no puede ser encontrado en la base de datos.\n\nVerifique que no ha escrito mal el nombre de usuario";



        public string strTxtOutbox = "Bandeja de Salida";
        public string strTxtMessageTo = "Mensaje para";
        public string strTxtMessagesInOutBox = "El mensaje permanece en su Bandeja de Salida hasta que el destinatario borre el mensaje";

        public string strTxtYourInboxIs = "Su bandeja de entrada es";
        public string strTxtFull = "completo";
        public string strTxtEmailThisPMToMe = "Enviarme este mensaje privado";
        public string strTxtEmailBelowPrivateEmailThatYouRequested = "Debajo está la copia del mensaje privado que usted solicitó";
        public string strTxtAnEmailWithPM = "El correo que contiene el mensaje privado tiene";
        public string strTxtBeenSent = "enviándose a su correo privado";
        public string strTxtNotBeenSent = "no se puede enviar ha ocurrido un error";
        public string strTxtSelected = "Seleccionado";

        public string strTxtUpdateTopic = "Actualizar Tema";
        public string strTxtTopicNotFoundOrAccessDenied = "Ninguno de los temas ha sido encontrado, o bien usted no tiene permiso para acceder a esta Página";
        public string strTxtMoveTopic = "Mover Tema";
        public string strTxtDeletePoll = "Eliminar Votación";
        public string strTxtAreYouSureYouWantToDeleteThisPoll = "Esta usted seguro de que quiere eliminar esta votación?";

        public string strTxtSelectTheForumYouWouldLikePostIn = "Seleccione el foro en el que desea que está incluido este mensaje";
        public string strTxtMovePostErrorMsg = @"Foro\t- Seleccione el foro hacia donde quiere mover este mensaje";
        public string strTxtSelectForumClickNext = "Paso 1: - Seleccione el foro hacia donde quiere mover este mensaje.<br />A continuación haga click en el bot?n siguiente.";
        public string strTxtSelectTopicToMovePostTo = "Paso 2: - Seleccione el tema hacia donde quiere mover el mensaje desde los Últimos 400 temas<br />foro. sino escriba el nombre del nuevo tema en la caja de texto en<br />el pie para mover el mensaje en un nuevo tema.";
        public string strTxtSelectTheTopicYouWouldLikeThisPostToBeIn = "Seleccione el tema donde desee incluir este mensaje";
        public string strTxtOrTypeTheSubjectOfANewTopic = "O escriba el asunto del nuevo tema";

        public string strTxtIPBlocking = "Bloqueando IP";
        public string strTxtBlockedIPList = "IP Bloqueada Lista de direcciones";
        public string strTxtYouHaveNoBlockedIpAddesses = "Usted no tiene bloqueda su IP";
        public string strTxtRemoveIP = "Borrar IP";
        public string strTxtBlockIPAddressOrRange = "Bloquear dirección IP o rango";
        public string strTxtIpAddressRange = "IP Dirección/Rango";
        public string strTxtBlockIPAddressRange = "Bloquear IP Dirección/rango";
        public string strTxtBlockIPRangeWhildcardDescription = "El caracter * puede ser utilizado para bloquear rangos de ip. <br />ej. Para bloquear el rango '200.200.200.0 - 255' debe usar '200.200.200.*'";
        public string strTxtErrorIPEmpty = @"IP Dirección/rango \t- Introduzca una dirección ip o un rango para bloquear";

        public string strTxtAdminModeratorFunctions = "funciones de administrador y moderador";
        public string strTxtUserIsActive = "El usuario esta activo";
        public string strTxtDeleteThisUser = "Eliminar este miembro?";
        public string strTxtCheckThisBoxToDleteMember = "Chequee esta caja para eliminar a este miembro. No podrá deshacer los cambios.";
        public string strTxtNumberOfPosts = "Número de mensajes";
        public string strTxtNonRankGroup = "No en el grupo principal";
        public string strTxtRankGroupMinPosts = "Grupo principal - Min. Mensajes";

        public string strTxtUpdateForum = "Actualizar foro";
        public string strTxtForumNotFoundOrAccessDenied = "Ninguno de los foros ha sido encontrado o usted no tiene permisos para acceder a esta Página";
        public string strTxtErrorForumName = @"Nombre del foro \t- Introduzca un nombre para el foro";
        public string strTxtErrorForumDescription = @"Descripción del foro \t- Introduzca una descripción para el foro";
        public string strTxtResyncTopicPostCount = "Actualizar el tema de foro y el contador de mensajes";

        public string strTxtShowMovedIconInLastForum = "Mostar el icono movido en el Último foro";

        public string strTxtHideTopic = "Ocultar Mensaje";
        public string strTxtIfYouAreShowingTopic = "If you are displaying the topic make sure at least 1 post is showing in the topic";
        public string strTxtShowHiddenTopicsSince = "Mostrar Mensajes Ocultos";
        public string strTxtHiddenTopicsPosts = "Mensajes Ocultos";
        public string strTxtNoHiddenTopicsPostsSince = "No hay Mensajes Ocultos";

#pragma warning restore 1591

    }

}
