

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include virtual="/fsportalnet/includes/funcionesMail.aspx" -->
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

'Set the response buffer to false as we may need to puase while the e-mails are being sent
Response.Buffer = False

'Set the script timeout high enough for all mail to be sent
Server.ScriptTimeout =  5000


'Dimension variables
Dim intSelGroupID		'Holds the group ID
Dim strEmailBody		'Holds the body of the e-mail
Dim strSubject			'Holds the subject of the e-mail
Dim strEmailAddress		'Holds the users email address
Dim strMemusuario		'Holds the usuario of the person we are sending the mail to
Dim lngNumberOfMembers		'Holds the number of members to send emails to
Dim blnLCode			'Holds if there is a link appened to message
Dim strMainForumName		'Holds the forum name
Dim strForumEmailAddress	'Holds the forum email address
Dim intEmailSentLoopCounter	'Holds the loop counter for the sent mails
Dim strMailComponent		'Holds the mail component to use
Dim strOutgoingMailServer	'Holds the mail server
Dim blnHTMLEmailFormat		'Set to true if the email is to be sent in HTML format


'Initialise the SQL variable with an SQL statement to get the configuration details from the database
If portal.variablesForum.strDatabaseType = "SQLServer" Then
	strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "SelectConfiguration"
Else
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Configuration.* From " & portal.variablesForum.strDbTable & "Configuration;"
End If

'Query the database
rsCommon=db.execute(strSQL)

'Read in page setup from the config table
blnLCode = CBool(rsCommon("L_code"))
strMainForumName = rsCommon("forum_name")
strForumEmailAddress = rsCommon("forum_email_address")
strMailComponent = rsCommon("mail_component")
strOutgoingMailServer = rsCommon("mail_server")



'If the mass mailier is not active then redirect
If blnMassMailier = False Then

	'Release Server Objects
	rsCommon.Close
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect to admin menu
	Response.Redirect("admin_menu.aspx")
End If

'Close the rs
rsCommon.Close



'Read in the body of the e-mail
intSelGroupID = CInt(Request.Form("group"))
strEmailBody = Request.Form("message")
strSubject = Request.Form("subject")
blnHTMLEmailFormat = CBool(Request.Form("mailFormat"))


'If there is no user group get all email address from the db
If intSelGroupID = 0 Then
	strSQL = "SELECT " & "Usuarios.usuario, " & "Usuarios.email  "
	strSQL = strSQL & "From " & "Usuarios "
	strSQL = strSQL & "WHERE " & "Usuarios.email <> '';"

'Only select those that are part of the usergroup
Else
	strSQL = "SELECT " & "Usuarios.usuario, " & "Usuarios.email  "
	strSQL = strSQL & "From " & "Usuarios "
	strSQL = strSQL & "WHERE " & "Usuarios.Group_ID = " & intSelGroupID & " AND " & "Usuarios.email <> '';"
End If

'Set the cursor type so we can do a record count
rsCommon.CursorType = 3

'Query the database
rsCommon=db.execute(strSQL)


%>
<html>
<head>
<title>Mass Mailier</title>



     	

<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"><span class="heading">Member Email Messenger</span><br />
 <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
 <br />
 <span class="text">From here you can send email's to all your forum members or those that are part of a specific User Group, that have entered valid email addresses in their profiles.</span><br />
 <br />
 <table width="97%" border="0" cellspacing="1" cellpadding="4" bgcolor="#000000">
  <tr> 
   <td bgcolor="#F5F5FA" class="text"> 
    <%
   
   	'Get the number of mailing list members
	lngNumberOfMembers = rsCommon.RecordCount
	
	
	'Display the HTML for sending the mail
	'Display a message on the screen incase the user thinks nothing is happening and hits refresh sending the email's twice
	Response.Write("<span class=""lgText"">The email's are being sent<br />Do not Hit Refresh or some members will receive the email twice!</span><br /><br />This may take some time depending on the speed of the mail server and how many email's there are to send.")
	
	'Display the number of e-mails sent and how many left to send
	Response.Write("<form name=""frmSent"">There are <input type=""text"" size=""4"" name=""sent"" value=""0""> email's sent out of a total of " & lngNumberOfMembers & "</form>")


	'Loop through the recordset and send the e-mail to everyone in the mailing list
	Do While NOT rsCommon.EOF
	
		'loop counter to count how many e-mails have been sent
		intEmailSentLoopCounter = intEmailSentLoopCounter + 1
		
		'Read in who the email is to
		strEmailAddress	= rsCommon("usuario")
		strMemusuario = rsCommon("email")
	
	
		'Update the text box displaying the number of e-mails sent
		Response.Write(vbCrLf & "<script langauge=""JavaScript"">document.frmSent.sent.value = " & intEmailSentLoopCounter & ";</script>")
		
		
		'Send the e-mail using the Send Mail function created on the send_mail_function.inc file
		Call funcMail.SendMailForo(strEmailBody, decodeString(strEmailAddress), decodeString(strMemusuario), strMainForumName, decodeString(strForumEmailAddress), strSubject, strMailComponent, blnHTMLEmailFormat)
		
		
		'Move to the next record in the recordset
		rsCommon.MoveNext
	Loop
	
	'Write a message saying that all the e-mails have been sent
	Response.Write(vbCrLf & "<br /><span class=""lgText"">Your email's have now been sent.</span>")


'Release Server Objects
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing   
   %>
   </td>
  </tr>
 </table>
</div>
<br />
</body>
</html>