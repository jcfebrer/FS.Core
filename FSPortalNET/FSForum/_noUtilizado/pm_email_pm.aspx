

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include file="language_files/pm_language_file_inc.aspx" -->

<!--#include virtual="/fsportalnet/includes/funcionesMail.aspx" -->
<!--#include file="functions/functions_format_post.aspx" -->
<%
'Set the buffer to true
Response.Buffer = True

'Make sure this page is not cached
Response.Expires = -1
Response.ExpiresAbsolute = Now() - 2
Response.AddHeader "pragma","no-cache"
Response.AddHeader "cache-control","private"
Response.CacheControl = "No-Store"

'Declare variables
Dim lngPmMessageID		'Private message id
Dim strPmSubject 		'Holds the subject of the private message
Dim strusuario 		'Holds the usuario of the thread
Dim strEmailBody		'Holds the body of the e-mail message
Dim blnEmailSent		'set to true if an e-mail is sent
Dim intForumID			'Holds the forum ID
Dim strPrivateMessage		'Holds the private message


'Raed in the pm mesage number to display
lngPmMessageID = CLng(Request.QueryString("ID"))

'If Priavte messages are not on then send them away
If blnPrivateMessages = False Then Response.Redirect("default.aspx")

'If the user is not allowed then send them away
If portal.variablesForum.intGroupID = 2 OR blnActiveMember = False Then Response.Redirect("insufficient_permission.aspx")




	
'Initlise the sql statement
strSQL = "SELECT " & portal.variablesForum.strDbTable & "PMMessage.*, " & "Usuarios.usuario "
strSQL = strSQL & "FROM " & "Usuarios, " & portal.variablesForum.strDbTable & "PMMessage "
strSQL = strSQL & "WHERE " & "Usuarios.UsuarioID = " & portal.variablesForum.strDbTable & "PMMessage.From_ID AND " & portal.variablesForum.strDbTable & "PMMessage.PM_ID=" & lngPmMessageID & " "
'If this is a link from the out box then check the from Usuarios ID to check the user can view the message
If Request.QueryString("M") = "OB" Then
	strSQL = strSQL & " AND " & portal.variablesForum.strDbTable & "PMMessage.From_ID=" & portal.variablesForum.lngLoggedInUserID & ";"
'Else use the to Usuarios ID to check the user can view the message
Else
	strSQL = strSQL & " AND " & portal.variablesForum.strDbTable & "PMMessage.UsuarioID=" & portal.variablesForum.lngLoggedInUserID & ";"
End If

'Query the database
rsCommon=db.execute(strSQL)



'If a mesage is found then send a mail
If portal.variablesForum.blnLoggedInUserEmail AND portal.variablesForum.blnEmail AND NOT rsCommon.EOF Then 
	
	'Read in some of the details
	strPmSubject = rsCommon("PM_Tittle")
	strusuario = rsCommon("usuario")
	strPrivateMessage = rsCommon("PM_Message")
	
	'Change	the path to the	emotion	symbols	to include the path to the images
	strPrivateMessage = Replace(strPrivateMessage, "src=""smileys/smiley", "src=""" & strForumPath & "/smileys/smiley", 1, -1, 1)
	
	'Initailise the e-mail body variable with the body of the e-mail
	strEmailBody = portal.variablesForum.strTxtHi & " " & decodeString(strLoggedInusuario) & ","
	strEmailBody = strEmailBody & "<br /><br />" & portal.variablesForum.strTxtEmailBelowPrivateEmailThatYouRequested & ":-"
	strEmailBody = strEmailBody & "<br /><br /><hr />"
	strEmailBody = strEmailBody & "<br /><b>" & portal.variablesForum.strTxtPrivateMessage & " :</b> " & strPmSubject
	strEmailBody = strEmailBody & "<br /><b>" & portal.variablesForum.strTxtSentBy & " :</b> " & decodeString(strusuario) 
	strEmailBody = strEmailBody & "<br /><b>" & portal.variablesForum.strTxtSent & " :</b> " & funcFecha.DateFormat(rsCommon("PM_Message_Date"), funcFecha.saryDateTimeData) & " at " & funcFecha.TimeFormat(rsCommon("PM_Message_Date"), funcFecha.saryDateTimeData) & "<br /><br />"
	strEmailBody = strEmailBody & strPrivateMessage
		
		
	'Call the function to send the e-mail
	portal.variablesForum.blnEmailSent = funcMail.SendMailForo(strEmailBody, decodeString(strLoggedInusuario), decodeString(strLoggedInUserEmail), decodeString(strLoggedInusuario), decodeString(strLoggedInUserEmail), decodeString(strPmSubject), strMailComponent, true)
End If



'Clear server objects
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

Response.Redirect("pm_show_message.aspx?ES=" & portal.variablesForum.blnEmailSent & "&" & Request.QueryString)
%>