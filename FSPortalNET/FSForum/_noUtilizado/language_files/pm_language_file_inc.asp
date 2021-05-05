<%
'****************************************************************************************
'**  Copyright Notice
'**
'**  Web Wiz Guide - Web Wiz Forums
'**
'**  Copyright 2001-2004 Bruce Corkhill All Rights Reserved.
'**
'**  This program is free software; you can modify (at your own risk) any part of it
'**  under the terms of the License that accompanies this software and use it both
'**  privately and commercially.
'**
'**  All copyright notices must remain in tacked in the scripts and the
'**  outputted HTML.
'**
'**  You may use parts of this program in your own private work, but you may NOT
'**  redistribute, repackage, or sell the whole or any part of this program even
'**  if it is modified or reverse engineered in whole or in part without express
'**  permission from the Usuarios.
'**
'**  You may not pass the whole or any part of this application off as your own work.
'**
'**  All links to Web Wiz Guide and powered by logo's must remain unchanged and in place
'**  and must remain visible when the pages are viewed unless permission is first granted
'**  by the copyright holder.
'**
'**  This program is distributed in the hope that it will be useful,
'**  but WITHOUT ANY WARRANTY; without even the implied warranty of
'**  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE OR ANY OTHER
'**  WARRANTIES WHETHER EXPRESSED OR IMPLIED.
'**
'**  You should have received a copy of the License along with this program;
'**  if not, write to:- Web Wiz Guide, PO Box 4982, Bournemouth, BH8 8XP, United Kingdom.
'**
'**
'**  No official support is available for this program but you may post support questions at: -
'**  http://www.webwizguide.info/forum
'**
'**  Support questions are NOT answered by email ever!
'**
'**  For correspondence or non support questions contact: -
'**  info@webwizguide.info
'**
'**  or at: -
'**
'**  Web Wiz Guide, PO Box 4982, Bournemouth, BH8 8XP, United Kingdom
'**
'****************************************************************************************


'pm_welcome.aspx
'---------------------------------------------------------------------------------
Const portal.variablesForum.strTxtToYourPrivateMessenger = "a su Mensajería Privada"
Const portal.variablesForum.strTxtPmIntroduction = "Desde su Mensajería Privada puede enviar y recibir Mensajes Privados entre Ud. y otros miembros del Foro, sabiendo que sus mensajes no serán vistos por otros miembros del Foro."
Const portal.variablesForum.strTxtInboxStatus = "Estado de la Bandeja de Entrada"
Const portal.variablesForum.strTxtGoToYourInbox = "Ir a su Bandeja de Entrada"
Const portal.variablesForum.strTxtNoNewMsgsInYourInbox = "No tiene nuevos mensajes en su Bandeja de Entrada."
Const portal.variablesForum.strTxtYourLatestPrivateMessageIsFrom = "Su ultimo mensaje privado es de"
Const portal.variablesForum.strTxtSentOn = "enviado el"
Const portal.variablesForum.strTxtPrivateMessengerOverview = "Descripción de la Mensajerí Privada"
Const portal.variablesForum.strTxtInboxOverview = "Este es donde son almacenados todos sus Mensajes Privados entrantes y desde donde Ud. puede ver o borrar cualquier Mensaje Privado que reciba. Este funciona muy similar la Bandeja de Entrada de su e-mail."
Const portal.variablesForum.strTxtOutboxOverview = "Aquí es donde se almacenan los Mensajes Privados salientes y desde donde Ud. puede ver los Mensajes Privados que ha enviado. Los Mensajes Privados permanecen en su Bandeja de Salida hasta que el destinatario los borra."
Const portal.variablesForum.strTxtBuddyListOverview = "Este es como una libreta de direcciones. Ud. puede añadir o borrar a miembros del foro de su Lista de Amigos para rápida referencia. Ud. también puede bloquear a esos miembros de los cuales no desea recibir Mensajes Privados."
Const portal.variablesForum.strTxtNewMsgOverview = "Este es donde Ud. puede crear nuevos Mensajes Privados y enviarlos a otros miembros del foro. "

'pm_inbox.aspx
'---------------------------------------------------------------------------------

Const portal.variablesForum.strTxtInbox = "Bandeja de Entrada"
Const portal.variablesForum.strTxtBuddyList = "Lista de Amigos"
Const portal.variablesForum.strTxtNewPrivateMessage = "Nuevos Mensajes Privados"
Const portal.variablesForum.strTxtNoPrivateMessages = "Ud. no tiene Mensajes Privados en su"
Const portal.variablesForum.strTxtRead = "Leer"
Const portal.variablesForum.strTxtMessageTitle = "Título del Mensaje"
Const portal.variablesForum.strTxtMessageFrom = "Mensaje de"
Const portal.variablesForum.strTxtDate = "Fecha"
Const portal.variablesForum.strTxtBlock = "bloquear"
Const portal.variablesForum.strTxtSentBy = "Enviar por"
Const portal.variablesForum.strTxtDeletePrivateMessageAlert = "¿Está seguro que desea borrar los Mensajes Privados seleccionados?"
Const portal.variablesForum.strTxtPrivateMessagesYouCanReceiveAnother = "Mensajes Privados, Ud. puede recibir otros"
Const portal.variablesForum.strTxtOutOf = "de"
Const portal.variablesForum.strTxtPreviousPrivateMessage = "Mensajes Privados Anteriores"
Const portal.variablesForum.strTxtMeassageDeleted = "Los Mensajes Privados han sido borrados de su Bandeja de Entrada"

'pm_show_message.aspx
'---------------------------------------------------------------------------------
Const portal.variablesForum.strTxtSorryYouDontHavePermissionsPM = "Lo sentimos, no tiene permiso para ver este Mensaje Privado."
Const portal.variablesForum.strTxtYouDoNotHavePermissionViewPM = "No tiene permiso para ver este Mensaje Privado."
Const portal.variablesForum.strTxtNotificationReadPM = "Leer la notificación del Mensaje Privado"
Const portal.variablesForum.strTxtReplyToPrivateMessage = "Contestar al Mensaje Privado"
Const portal.variablesForum.strTxtAddToBuddy = "Agregar"
Const portal.variablesForum.strTxtThisIsToNotifyYouThat = "Esto es para notificarle que"
Const portal.variablesForum.strTxtHasReadPM = "ha leído el Mensaje Privado"
Const portal.variablesForum.strTxtYouSentToThemOn = "enviaste a ellos en"


'pm_new_message_form.aspx
'---------------------------------------------------------------------------------
Const portal.variablesForum.strTxtSendNewMessage = "Enviar nuevo mensaje"
Const portal.variablesForum.strTxtPostMessage = "Colocar mensaje"
Const portal.variablesForum.strTxtEmailNotifyWhenPMIsRead = "Avisarme vía e-mail cuando el mensaje sea leído"
Const portal.variablesForum.strTxtTousuario = "Para"
Const strSelectFormBuddyList = "o elija de la Lista de Amigos"
Const portal.variablesForum.strTxtNoPMSubjectErrorMsg = "Asunto \t\t- escriba un asunto para su nuevo Mensaje Privado"
Const portal.variablesForum.strTxtNoTousuarioErrorMsg = "Para \t- escriba o selecciona un Usuario para enviar su Mensaje Privado"
Const portal.variablesForum.strTxtNoPMErrorMsg = "Mensaje \t\t- escriba su Mensaje Privado para enviar"
Const portal.variablesForum.strTxtSent = "Enviar"

'pm_post_message.aspx
'---------------------------------------------------------------------------------
Const portal.variablesForum.strTxtAPrivateMessageHasBeenPosted = "Un Mensaje Privado ha sido colocado para Ud. en"
Const portal.variablesForum.strTxtClickOnLinkBelowForPM = "Presione en la liga de abajo para ver el Mensaje Privado"
Const portal.variablesForum.strTxtNotificationPM = "Notificación de Mensaje Privado"
Const portal.variablesForum.strTxtTheusuarioCannotBeFound = "El usuario que ha proporcionado no ha sido encontrado."
Const portal.variablesForum.strTxtYourPrivateMessage = "¡Su Mensaje Privado"
Const portal.variablesForum.strTxtHasNotBeenSent = "no ha sido enviado!"
Const portal.variablesForum.strTxtAmendYourPrivateMessage = "Enmiende su Mensaje Privado"
Const portal.variablesForum.strTxtReturnToYourPrivateMessenger = "Regresar a su Mensajería Privada"
Const portal.variablesForum.strTxtYouAreBlockedFromSendingPMsTo = "Ha sido bloqueado para enviar Mensajes Privados a"
Const portal.variablesForum.strTxtHasExceededMaxNumPPMs = "ha excedido el número máximo de Mensajes Privados que se te permiten recibir"
Const portal.variablesForum.strTxtHasSentYouPM = "ha enviado un Mensaje Privado con el siguiente asunto"
Const portal.variablesForum.strTxtToViewThePrivateMessage = "Para ver el Mensaje Privado"


'pm_buddy_list.aspx
'---------------------------------------------------------------------------------
Const portal.variablesForum.strTxtNoBuddysInList = "No tiene amigos en su Lista de Amigos"
Const portal.variablesForum.strTxtDeleteBuddyAlert = "¿Esta seguro de borrar este amigo de su Lista de Amigos?"
Const portal.variablesForum.strTxtNoBuddyErrorMsg = "Nombre del miembro \t- proporcione un miembro del Foro para añadir a tu Lista de Amigos"
Const portal.variablesForum.strTxtBuddy = "Amigo"
Const portal.variablesForum.strTxtDescription = "Descripción"
Const portal.variablesForum.strTxtContactStatus = "Estado del contacto"
Const portal.variablesForum.strTxtThisPersonCanNotMessageYou = "Esta persona no puede mensajearle"
Const portal.variablesForum.strTxtThisPersonCanMessageYou = "Esta persona puede mensajearle"
Const portal.variablesForum.strTxtAddNewBuddyToList = "Añadir nuevo Amigo a la lista"
Const portal.variablesForum.strTxtMemberName = "Nombre del Miembro"
Const portal.variablesForum.strTxtAllowThisMemberTo = "Permitir a este miembro"
Const portal.variablesForum.strTxtMessageMe = "Mensajearme"
Const portal.variablesForum.strTxtNotMessageMe = "No mensajearme"
Const portal.variablesForum.strTxtHasNowBeenAddedToYourBuddyList = "ahora ha sido añadido a su Lista de Amigos"
Const portal.variablesForum.strTxtIsAlreadyInYourBuddyList = "esta ya en su Lista de Amigos"
Const portal.variablesForum.strTxtUserCanNotBeFoundInDatabase = "no puede ser encontrado en la base de datos.\n\nVerifique que no ha escrito mal el nombre de usuario"



Const portal.variablesForum.strTxtOutbox = "Bandeja de Salida"
Const portal.variablesForum.strTxtMessageTo = "Mensaje para"
Const portal.variablesForum.strTxtMessagesInOutBox = "El mensaje permanece en su Bandeja de Salida hasta que el destinatario borre el mensaje"

'New from version 7.02
'---------------------------------------------------------------------------------
Const portal.variablesForum.strTxtYourInboxIs = "Su bandeja de entrada es"
Const portal.variablesForum.strTxtFull = "completo"
Const portal.variablesForum.strTxtEmailThisPMToMe = "Enviarme este mensaje privado"
Const portal.variablesForum.strTxtEmailBelowPrivateEmailThatYouRequested = "Debajo está la copia del mensaje privado que usted solicitó"
Const portal.variablesForum.strTxtAnEmailWithPM = "El correo que contiene el mensaje privado tiene"
Const portal.variablesForum.strTxtBeenSent = "enviándose a su correo privado"
Const portal.variablesForum.strTxtNotBeenSent = "no se puede enviar ha ocurrido un error"
Const portal.variablesForum.strTxtSelected = "Seleccionado"

%>