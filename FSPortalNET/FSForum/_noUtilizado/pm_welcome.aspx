

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include file="language_files/pm_language_file_inc.aspx" -->

<%
'Set the buffer to true
Response.Buffer = True

'Declare variables
Dim rsPmMessage		'db recordset holding any new pm's since last vist
Dim intForumID

'If Priavte messages are not on then send them away
If blnPrivateMessages = False Then 
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	Response.Redirect("default.aspx")
End If


'If the user is not allowed then send them away
If portal.variablesForum.intGroupID = 2 OR blnActiveMember = False Then 
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	Response.Redirect("insufficient_permission.aspx")
End If


'Intialise the ADO recordset object
Set rsPmMessage = Server.CreateObject("ADODB.Recordset")
			
'Initlise the sql statement
If portal.variablesForum.strDatabaseType = "SQLServer" Then
	strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "CountOfPMs @portal.variablesForum.lngLoggedInUserID = " & portal.variablesForum.lngLoggedInUserID
Else
	strSQL = "SELECT Count(" & portal.variablesForum.strDbTable & "PMMessage.PM_ID) AS CountOfPM FROM " & portal.variablesForum.strDbTable & "PMMessage "
	strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "PMMessage.Read_Post = 0 AND " & portal.variablesForum.strDbTable & "PMMessage.UsuarioID = " & portal.variablesForum.lngLoggedInUserID & ";"
End If
	
'Query the database
rsPmMessage=db.execute(strSQL)


'Get the number of new pm's this user has
intNumOfNewPM = CInt(rsPmMessage("CountOfPM"))

'Close the recordset
rsPmMessage.Close

'Now get the date of the last PM
strSQL = "SELECT TOP 1 " & portal.variablesForum.strDbTable & "PMMessage.PM_Message_Date, " & "Usuarios.usuario "
strSQL = strSQL & "FROM " & "Usuarios INNER JOIN " & portal.variablesForum.strDbTable & "PMMessage ON " & "Usuarios.UsuarioID = " & portal.variablesForum.strDbTable & "PMMessage.From_ID "
strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "PMMessage.Read_Post = 0 AND " & portal.variablesForum.strDbTable & "PMMessage.UsuarioID = " & portal.variablesForum.lngLoggedInUserID & " "
strSQL = strSQL & "ORDER BY " & portal.variablesForum.strDbTable & "PMMessage.PM_Message_Date DESC;"
		
'Query the database
rsPmMessage=db.execute(strSQL)
%>
<html>
<head>

<title>Private Messenger : Welcome</title>


     	
     	
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
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="4" align="center">
 <tr> 
  <td width="60%"><span class="lgText"><img src="<% = portal.variablesForum.strImagePath %>subject_folder.gif" alt="<% = portal.variablesForum.strTxtSubjectFolder %>" align="middle"> <% = portal.variablesForum.strTxtPrivateMessenger & ": " & portal.variablesForum.strTxtWelcome %></span></td>
  <td align="right" width="40%" nowrap="nowrap"><a href="pm_inbox.aspx" target="_self"><img src="<% = portal.variablesForum.strImagePath %>inbox.gif" alt="<% = portal.variablesForum.strTxtPrivateMessenger & " " & portal.variablesForum.strTxtInbox %>" border="0"></a>&nbsp;<a href="pm_outbox.aspx" target="_self"><img src="<% = portal.variablesForum.strImagePath %>outbox.gif" alt="<% = portal.variablesForum.strTxtPrivateMessenger & " " & portal.variablesForum.strTxtOutbox %>" border="0"></a>&nbsp;<a href="pm_buddy_list.aspx" target="_self"><img src="<% = portal.variablesForum.strImagePath %>buddy_list.gif" alt="<% = portal.variablesForum.strTxtPrivateMessenger & " " & portal.variablesForum.strTxtBuddyList %>" border="0"></a>&nbsp;<a href="pm_new_message_form.aspx" target="_self"><img src="<% = portal.variablesForum.strImagePath %>new_private_message.gif" alt="<% = portal.variablesForum.strTxtNewPrivateMessage %>" border="0"></a></td>
 </tr>
</table>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr>
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableBgColour %>">
   <table width="100%" border="0" cellspacing="1" cellpadding="4" height="14" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtWelcome & " " & strLoggedInusuario & " " & portal.variablesForum.strTxtToYourPrivateMessenger %></td>
    </tr>
    <tr> 
     <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text">
      <table width="100%" border="0" cellspacing="0" cellpadding="2" class="text">
       <tr> 
        <td class="text"><% = portal.variablesForum.strTxtPmIntroduction %></td>
       </tr>
       <tr> 
        <td>&nbsp;</td>
       </tr>
       <tr> 
        <td><span class="tiHeading"><% = portal.variablesForum.strTxtInboxStatus %><br />
         </span> 
         <table width="100%" border="0" cellspacing="0" cellpadding="4">
          <tr> 
           <td width="3%"><%
'If there are pm's display full inbox icon
If NOT rsPmMessage.EOF Then
	Response.Write("<img src=""" & portal.variablesForum.strImagePath & "inbox_full.gif""/>")
Else
	Response.Write("<img src=""" & portal.variablesForum.strImagePath & "inbox_empty.gif""/>")
End If
         %></td>
           <td width="97%" class="text"><%
'If there are pm's display the last pm details
If NOT rsPmMessage.EOF Then
	Response.Write("<span class=""bold"">" & portal.variablesForum.strTxtYouHave & " " & intNumOfNewPM & " " & portal.variablesForum.strTxtNewMsgsInYourInbox & "</span> <a href=""pm_inbox.aspx"" target=""_self"">" & portal.variablesForum.strTxtGoToYourInbox & "</a>.<br />")
	Response.Write(portal.variablesForum.strTxtYourLatestPrivateMessageIsFrom & " " & rsPmMessage("usuario") & " " & portal.variablesForum.strTxtSentOn & " " & funcFecha.DateFormat(rsPmMessage("PM_Message_Date"), funcFecha.saryDateTimeData) & " " & portal.variablesForum.strTxtAt & " " & funcFecha.TimeFormat(rsPmMessage("PM_Message_Date"), funcFecha.saryDateTimeData) & "<br />")
Else	
	Response.Write(portal.variablesForum.strTxtNoNewMsgsInYourInbox & " <a href=""pm_inbox.aspx"" target=""_self"">" & portal.variablesForum.strTxtGoToYourInbox & "</a>")
End If
         %></td>
         </tr>
         </table>
        </td>
       </tr>
       <tr>
        <td>&nbsp;</td>
       </tr>
       <tr> 
        <td><span class="tiHeading"><% = portal.variablesForum.strTxtPrivateMessengerOverview %></span></td>
       </tr>
       <tr> 
        <td class="text"><span class="bold"><% = portal.variablesForum.strTxtInbox %>: </span><% = portal.variablesForum.strTxtInboxOverview %></td>
       </tr>
       <tr> 
        <td class="text"><span class="bold"><% = portal.variablesForum.strTxtOutbox %>: </span><% = portal.variablesForum.strTxtOutboxOverview %></td>
       </tr>
       <tr> 
        <td class="text"><span class="bold"><% = portal.variablesForum.strTxtBuddyList %>: </span><% = portal.variablesForum.strTxtBuddyListOverview %></td>
       </tr>
       <tr> 
        <td class="text"><span class="bold"><% = portal.variablesForum.strTxtNewPrivateMessage %>:</span> <% = portal.variablesForum.strTxtNewMsgOverview %></td>
       </tr>
      </table>
     </td>
    </tr>
   </table>
  </td>
 </tr>
</table>
</td>
 </tr>
</table>
<br />
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="4" align="center">
 <tr>
  <form>
   <td>
    <!-- #include file="includes/forum_jump_inc.aspx" -->
   </td>
  </form>
 </tr>
</table>
<div align="center"><br />
<% 

'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"
%>
</div>

<%
'Clean up
rsPmMessage.Close
Set rsPmMessage = Nothing
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>