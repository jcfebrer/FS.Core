

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
Dim rsPmMessage			'ADO recordset object holding the users private messages
Dim lngPmMessageID		'Private message id
Dim strPmSubject 		'Holds the subject of the private message
Dim strMessage			'Holds the message body of the thread
Dim lngMessageID		'Holds the message ID number
Dim lngFromUserID		'Holds the from user ID
Dim lngToUserID			'Holds the to user ID
Dim dtmTopicDate		'Holds the date the thread was made
Dim strusuario 		'Holds the usuario of the thread
Dim strUsuariosHomepage		'Holds the homepage of the usuario if it is given
Dim strUsuariosLocation		'Holds the location of the user if given
Dim strUsuariosAvatar		'Holds the Usuarioss avatar	
Dim lngUsuariosNumOfPosts		'Holds the number of posts the user has made to the forum
Dim dtmUsuariosRegistration	'Holds the registration date of the user
Dim intRecordLoopCounter	'Holds the loop counter numeber
Dim intTopicPageLoopCounter	'Holds the number of pages there are of pm messages
Dim strEmailBody		'Holds the body of the e-mail message
Dim strEmailSubject		'Holds the subject of the e-mail
Dim blnEmailSent		'set to true if an e-mail is sent
Dim intForumID			'Holds the forum ID
Dim strGroupName		'Holds the Usuarioss group name
Dim intRankStars		'Holds the number of stars for the group
Dim strMemberTitle		'Holds the members title
Dim strRankCustomStars		'Holds custom stars for the user group

'Raed in the pm mesage number to display
lngPmMessageID = CLng(Request.QueryString("ID"))

'If Priavte messages are not on then send them away
If blnPrivateMessages = False Then Response.Redirect("default.aspx")

'If the user is not allowed then send them away
If portal.variablesForum.intGroupID = 2 OR blnActiveMember = False Then Response.Redirect("insufficient_permission.aspx")



'Intialise the ADO recordset object
Set rsPmMessage = Server.CreateObject("ADODB.Recordset")
	
'Initlise the sql statement
strSQL = "SELECT " & portal.variablesForum.strDbTable & "PMMessage.*, " & "Usuarios.usuario, " & "Usuarios.Homepage, " & "Usuarios.Location, " & "Usuarios.email, " & "Usuarios.No_of_posts, " & "Usuarios.FechaCreacion, " & "Usuarios.Signature, " & "Usuarios.Active, " & "Usuarios.Avatar, " & "Usuarios.Avatar_title, " & portal.variablesForum.strDbTable & "Group.Name, " & portal.variablesForum.strDbTable & "Group.Stars, " & portal.variablesForum.strDbTable & "Group.Custom_stars "
strSQL = strSQL & "FROM " & "Usuarios, " & portal.variablesForum.strDbTable & "PMMessage, " & portal.variablesForum.strDbTable & "Group "
strSQL = strSQL & "WHERE " & "Usuarios.UsuarioID = " & portal.variablesForum.strDbTable & "PMMessage.From_ID AND " & "Usuarios.Group_ID = " & portal.variablesForum.strDbTable & "Group.Group_ID AND " & portal.variablesForum.strDbTable & "PMMessage.PM_ID=" & lngPmMessageID & " "
'If this is a link from the out box then check the from Usuarios ID to check the user can view the message
If Request.QueryString("M") = "OB" Then
	strSQL = strSQL & " AND " & portal.variablesForum.strDbTable & "PMMessage.From_ID=" & portal.variablesForum.lngLoggedInUserID & ";"
'Else use the to Usuarios ID to check the user can view the message
Else
	strSQL = strSQL & " AND " & portal.variablesForum.strDbTable & "PMMessage.UsuarioID=" & portal.variablesForum.lngLoggedInUserID & ";"
End If

'Query the database
rsPmMessage=db.execute(strSQL)


'If a mesage is found then send a mail if the sender wants notifying
If NOT rsPmMessage.EOF Then 
	
	'Read in some of the details
	strPmSubject = rsPmMessage("PM_Tittle")
	strusuario = rsPmMessage("usuario")
	
	'If the sender wants notifying then send a mail as long as e-mail notify is on and the message hasn't already been read
	If CBool(rsPmMessage("Email_notify")) AND rsPmMessage("email") <> "" AND portal.variablesForum.blnEmail AND CBool(rsPmMessage("Read_Post")) = False AND Request.QueryString("M") <> "OB" Then
		
		'Set the subject
		strEmailSubject = strMainForumName & " " & portal.variablesForum.strTxtNotificationPM
	
		'Initailise the e-mail body variable with the body of the e-mail
		strEmailBody = portal.variablesForum.strTxtHi & " " & decodeString(strusuario) & ","
		strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtThisIsToNotifyYouThat & " " & strLoggedInusuario & " " & portal.variablesForum.strTxtHasReadPM & ", '" & decodeString(strPmSubject) & "', " & portal.variablesForum.strTxtYouSentToThemOn & " " & strMainForumName & "."
		
		'Call the function to send the e-mail
		portal.variablesForum.blnEmailSent = funcMail.SendMailForo(strEmailBody, decodeString(strusuario), decodeString(rsPmMessage("email")), strMainForumName, decodeString(strForumEmailAddress), strEmailSubject, strMailComponent, false)
	
	End If
End If

'If this is not from the outbox then update the read field
If Request.QueryString("M") <> "OB" Then
	'Inittilise the sql veriable to update the database
	strSQL = "UPDATE " & portal.variablesForum.strDbTable & "PMMessage SET " & portal.variablesForum.strDbTable & "PMMessage.Read_Post = 1 WHERE " & portal.variablesForum.strDbTable & "PMMessage.PM_ID=" & lngPmMessageID & ";"

	'Execute the sql statement to set the pm to read
	db.execute(strSQL)
End If

%>
<html>
<head>

<title>Private Messenger: <% = strPmSubject %></title>


     	

<!-- #include file="includes/header.aspx" -->
<navigation:navigation ID="common1" runat="server" />
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="3" align="center">
 <tr> 
  <td align="left" class="heading"><% = portal.variablesForum.strTxtPrivateMessenger %></td>
</tr>
 <tr> 
  <td align="left" width="71%" class="bold"><img src="<% = portal.variablesForum.strImagePath %>open_folder_icon.gif" border="0" align="middle">&nbsp;<a href="default.aspx" target="_self" class="boldLink"><% = strMainForumName %></a><% = strNavSpacer %><% = portal.variablesForum.strTxtPrivateMessenger %><br /></td>
  </tr>
</table>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="2" align="center">
 <tr> 
  <td width="60%"><span class="lgText"><img src="<% = portal.variablesForum.strImagePath %>subject_folder.gif" alt="<% = portal.variablesForum.strTxtSubjectFolder %>" align="middle"> <% = portal.variablesForum.strTxtPrivateMessenger %></span></td>
  <td align="right" width="40%" nowrap="nowrap"><a href="pm_inbox.aspx" target="_self"><img src="<% = portal.variablesForum.strImagePath %>inbox.gif" alt="<% = portal.variablesForum.strTxtPrivateMessenger & " " & portal.variablesForum.strTxtInbox %>" border="0"></a><a href="pm_outbox.aspx" target="_self"><img src="<% = portal.variablesForum.strImagePath %>outbox.gif" alt="<% = portal.variablesForum.strTxtPrivateMessenger & " " & portal.variablesForum.strTxtOutbox %>" border="0"></a><a href="pm_buddy_list.aspx" target="_self"><img src="<% = portal.variablesForum.strImagePath %>buddy_list.gif" alt="<% = portal.variablesForum.strTxtPrivateMessenger & " " & portal.variablesForum.strTxtBuddyList %>" border="0"></a><a href="pm_new_message_form.aspx" target="_self"><img src="<% = portal.variablesForum.strImagePath %>new_private_message.gif" alt="<% = portal.variablesForum.strTxtNewPrivateMessage %>" border="0"></a></td>
 </tr>
 <tr> 
  <td colspan="2"><span class="lgText"><% = portal.variablesForum.strTxtSubjectFolder & ": " & strPmSubject %></span></td>
 </tr>
</table><%

'If no private message display an error
IF rsPmMessage.EOF Then
%>
<table width="100%" border="0" cellspacing="0" cellpadding="0" height="166">
 <tr>
  <td align="center" class="heading" height="120"><% = portal.variablesForum.strTxtYouDoNotHavePermissionViewPM %></td>
 </tr>
</table><%

'Else display the message
Else
	'Read in threads details for the topic from the database
	strMessage = rsPmMessage("PM_Message")
	lngFromUserID = CLng(rsPmMessage("From_ID"))
	lngToUserID = CLng(rsPmMessage("UsuarioID"))
	dtmTopicDate = CDate(rsPmMessage("PM_Message_Date")) 
	strUsuariosHomepage = rsPmMessage("Homepage")
	strUsuariosLocation = rsPmMessage("Location")
	dtmUsuariosRegistration = CDate(rsPmMessage("FechaCreacion"))
	lngUsuariosNumOfPosts = CLng(rsPmMessage("No_of_posts"))
	strUsuariosAvatar = rsPmMessage("Avatar")
	strGroupName = rsPmMessage("Name")
	intRankStars = CInt(rsPmMessage("Stars"))
	strMemberTitle = rsPmMessage("Avatar_title")
	strRankCustomStars = rsPmMessage("Custom_stars")
	
	
	'If the pm contains a quote or code block then format it
	If InStr(1, strMessage, "[QUOTE=", 1) > 0 AND InStr(1, strMessage, "[/QUOTE]", 1) > 0 Then strMessage = formatUserQuote(strMessage)
	If InStr(1, strMessage, "[QUOTE]", 1) > 0 AND InStr(1, strMessage, "[/QUOTE]", 1) > 0 Then strMessage = formatQuote(strMessage)
	If InStr(1, strMessage, "[CODE]", 1) > 0 AND InStr(1, strMessage, "[/CODE]", 1) > 0 Then strMessage = formatCode(strMessage)


	'If the pm contains a flash link then format it
	If blnFlashFiles Then
		If InStr(1, strMessage, "[FLASH", 1) > 0 AND InStr(1, strMessage, "[/FLASH]", 1) > 0 Then strMessage = formatFlash(strMessage)
	End If

%>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = strTablePostsBorderColour %>" align="center">
 <tr> 
  <td> <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = strTablePMBgColour %>">
    <tr> 
     <td bgcolor="<% = strTablePMBgColour %>"> <table width="100%" border="0" cellspacing="1" cellpadding="3" height="14">
       <tr> 
        <td bgcolor="<% = strTablePMTitleColour %>" width="145" class="tHeading" background="<% = strTablePMTitleBgImage %>" nowrap="nowrap"><% = portal.variablesForum.strTxtUsuarios %></td>
        <td bgcolor="<% = strTablePMTitleColour %>" width="82%" class="tHeading" background="<% = strTablePMTitleBgImage %>" nowrap="nowrap"><% = portal.variablesForum.strTxtMessage %></td>
       </tr>
       <tr> 
        <td valign="top" background="<% = strTablePMBgImage %>" bgcolor="<% = strTablePMBoxSideBgColour %>" class="smText"> 
         <span class="bold"><a name="<% = lngMessageID %>"></a><% = strusuario %></span><br /> <span class="smText"><% = strGroupName %><br /><%
         	
         	Response.Write(vbCrLf & "         <img src=""")
		If strRankCustomStars <> "" Then Response.Write(strRankCustomStars) Else Response.Write(portal.variablesForum.strImagePath & intRankStars & "_star_rating.gif")
		Response.Write(""" alt=""" & strGroupName & """><br />")
         
         
	        'If the user has an avatar then display it
	        If blnAvatar = True AND strUsuariosAvatar <> "" Then 
	        	
	        	Response.Write("<img src=""" & strUsuariosAvatar & """ width=""" & intAvatarWidth & """ height=""" & intAvatarHeight & """ alt=""" & portal.variablesForum.strTxtAvatar & """ OnError=""this.src='avatars/blank.gif', height='0';"">")
	       	End If
	       	
	       	'If there is a title for this member then display it
	       	If strMemberTitle <> "" Then Response.Write(vbCrLf & "<br />" & strMemberTitle)
	       	
          		Response.Write("<br /><br />")
          		Response.Write(portal.variablesForum.strTxtJoined & ": " & funcFecha.DateFormat(dtmUsuariosRegistration, funcFecha.saryDateTimeData))
          		Response.Write("<br />")
          		Response.Write(portal.variablesForum.strTxtPosts & ": " & lngUsuariosNumOfPosts) 
         
         	'If the is a location display it
         	If strUsuariosLocation <> "" Then
         		
         		Response.Write("<br />" & portal.variablesForum.strTxtLocation & ": " & strUsuariosLocation)
         	End If
         	
         	%></span></td>
        <td valign="top" background="<% = strTablePMBgImage %>" bgcolor="<% = strTablePMBoxBgColour %>" class="text"> 
         <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr> 
           <td width="60%" class="smText"><% Response.Write(portal.variablesForum.strTxtSent & ": " & funcFecha.DateFormat(dtmTopicDate, funcFecha.saryDateTimeData) & " " & portal.variablesForum.strTxtAt & " " & funcFecha.TimeFormat(dtmTopicDate, funcFecha.saryDateTimeData)) %></td>
           <td width="30%" align="right"><%
           	
           	'If the person reading the pm is the recepient disply delete and reply buttons
      		If lngToUserID = portal.variablesForum.lngLoggedInUserID Then
      			
      			%>
           <a href="pm_delete_message.aspx?pm_id=<% = rsPmMessage("PM_ID") %>" OnClick="return confirm('<% = portal.variablesForum.strTxtDeletePrivateMessageAlert %>')"><img src="<% = portal.variablesForum.strImagePath %>delete_sm.gif" border="0" align="middle" alt="<% = portal.variablesForum.strTxtDelete %>"></a> 
            <a href="pm_new_message_form.aspx?code=reply&amp;pm=<% = rsPmMessage("PM_ID") %>"><img src="<% = portal.variablesForum.strImagePath %>pm_reply.gif" align="middle" border="0" alt="<% = portal.variablesForum.strTxtReplyToPrivateMessage %>"></a><%
            
        	End If
            %></td>
          </tr>
          <tr> 
           <td colspan="2"><hr /></td>
          </tr>
         </table>
         <!-- Message body -->
         <% = strMessage %>
         <!-- Message body ''"" -->
        </td>
       </tr>
       <tr> 
        <td bgcolor="<% = strTablePMBoxSideBgColour %>" background="<% = strTablePMBgImage %>">&nbsp;</td>
        <td bgcolor="<% = strTablePMBoxBgColour %>" background="<% = strTablePMBgImage %>" class="text"><a href="JavaScript:openWin('pop_up_profile.aspx?PF=<% = lngFromUserID %>&amp;fID=<% = portal.variablesForum.intForumID %>','profile','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=590,height=425')"><img src="<% = portal.variablesForum.strImagePath %>profile_icon.gif" border="0" alt="<% = portal.variablesForum.strTxtView %>&nbsp;<% = strusuario %>'s&nbsp;<% = portal.variablesForum.strTxtProfile %>" align="middle"></a> 
         <a href="search_form.aspx?KW=<% = Server.URLEncode(strusuario) %>&amp;sI=AR&amp;fID=0"><img src="<% = portal.variablesForum.strImagePath %>search_sm.gif" border="0" alt="<% = portal.variablesForum.strTxtSearchForPosts %>&nbsp;<% = strusuario %>" align="middle"></a><% 
         
            	'If the user has a hompeage put in a link button
      		If strUsuariosHomepage <> "" Then
	%>
         <a href="<% = strUsuariosHomepage %>" target="_blank"><img src="<% = portal.variablesForum.strImagePath %>home_icon.gif" border="0" alt="<% = portal.variablesForum.strTxtVisit & " " & strusuario & "'s " & portal.variablesForum.strTxtHomepage %>" align="middle"></a> 
         <%
      		
      		End If
      
            	'If the private msg's are on then display a link to enable use to send them a msg 
            	If blnPrivateMessages = True Then 
        
         %>
         <a href="pm_buddy_list.aspx?name=<% = Server.URLEncode(strusuario) %>" target="_self"><img src="<% = portal.variablesForum.strImagePath %>add_buddy_sm.gif" align="middle" border="0" alt="<% = portal.variablesForum.strTxtAddToBuddyList %>"></a> 
         <% 
      		
      		End If
      		
      		'If the person reading the pm is the recepient disply delete and reply buttons
      		If lngToUserID = portal.variablesForum.lngLoggedInUserID Then
%>
         <a href="pm_delete_message.aspx?pm_id=<% = rsPmMessage("PM_ID") %>" OnClick="return confirm('<% = portal.variablesForum.strTxtDeletePrivateMessageAlert %>')"><img src="<% = portal.variablesForum.strImagePath %>delete_sm.gif" border="0" alt="<% = portal.variablesForum.strTxtDelete %>" align="middle"></a>
	 <a href="pm_new_message_form.aspx?code=reply&amp;pm=<% = rsPmMessage("PM_ID") %>"><img src="<% = portal.variablesForum.strImagePath %>pm_reply.gif" align="middle" border="0" alt="<% = portal.variablesForum.strTxtReplyToPrivateMessage %>"></a><%
	 
		End If
	 %></td>
       </tr>
      </table></td>
    </tr>
   </table></td>
 </tr>
</table><%

	'If the user has an email address and emailing is enabled then allow user to receive this pm by email
	If portal.variablesForum.blnLoggedInUserEmail AND portal.variablesForum.blnEmail Then
	
%>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="3" align="center">
 <tr> 
  <td><a href="pm_email_pm.aspx?ID=<% = lngPmMessageID %><% If Request.QueryString("M") = "OB" Then Response.Write("&amp;m=OB")%>"><% = portal.variablesForum.strTxtEmailThisPMToMe %></a></td>
</tr>
</table><%

	End If

Response.Write("<br />")


End If

Response.Write(vbCrLf & "<div align=""center""><br />")

'Clear server objects
rsPmMessage.Close
Set rsPmMessage = Nothing
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing


'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"
%>
 <br />
</div>
<%
'Display a msg letting the user know they have been emailed a private message
If Request.QueryString("ES") = "True" Then
	Response.Write("<script  language=""JavaScript"">")
	Response.Write("alert('" & portal.variablesForum.strTxtAnEmailWithPM & " " & portal.variablesForum.strTxtBeenSent & ".');")
	Response.Write("</script>")
ElseIf Request.QueryString("ES") = "False" Then
	Response.Write("<script  language=""JavaScript"">")
	Response.Write("alert('" & portal.variablesForum.strTxtAnEmailWithPM & " " & portal.variablesForum.strTxtNotBeenSent & ".');")
	Response.Write("</script>")
End If
%>

