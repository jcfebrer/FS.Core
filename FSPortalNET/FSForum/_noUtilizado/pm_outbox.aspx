

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include file="language_files/pm_language_file_inc.aspx" -->

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
Dim intRecordPositionPageNum	'Holds the recorset page number to show the other pm message
Dim intTotalNumOfPages		'Holds the total number of pages in the recordset
Dim intRecordLoopCounter	'Holds the loop counter numeber
Dim intPageLoopCounter		'Holds the number of pages there are of pm messages
Dim intNumOfPMs			'Holds the number of private messages the user has
Dim intForumID

'Initilise varaibles
intNumOfPMs = 0

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


'If this is the first time the page is displayed then the pm message record position is set to page 1
If Request.QueryString("PN") = 0 Then
	intRecordPositionPageNum = 1

'Else the page has been displayed before so the pm message record postion is set to the Record Position number
Else
	intRecordPositionPageNum = CInt(Request.QueryString("PN"))
End If	


'Intialise the ADO recordset object
Set rsPmMessage = Server.CreateObject("ADODB.Recordset")
	
'Initlise the sql statement
strSQL = "SELECT " & portal.variablesForum.strDbTable & "PMMessage.*, " & "Usuarios.usuario "
strSQL = strSQL & "FROM " & "Usuarios INNER JOIN " & portal.variablesForum.strDbTable & "PMMessage ON " & "Usuarios.UsuarioID = " & portal.variablesForum.strDbTable & "PMMessage.UsuarioID "
strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "PMMessage.From_ID=" & portal.variablesForum.lngLoggedInUserID & " "
strSQL = strSQL & "ORDER BY " & portal.variablesForum.strDbTable & "PMMessage.PM_Message_Date DESC;"
	
'Set the cursor type property of the record set to dynamic so we can naviagate through the record set
rsPmMessage.CursorType = 1

'Query the database
rsPmMessage=db.execute(strSQL)

'Set the number of records to display on each page
rsPmMessage.PageSize = 10

'Get the record poistion to display from
If NOT rsPmMessage.EOF Then rsPmMessage.AbsolutePage = intRecordPositionPageNum

'If there are no records on this page and it's above the frist page then set the page position to 1
If rsPmMessage.EOF AND intRecordPositionPageNum > 1 Then response.redirect("pm_outbox.aspx?PN=1"

%>
<html>
<head>

<title>Private Messenger: Outbox</title>


     	

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
<table width="100%" border="0" cellspacing="0" cellpadding="0">
 <tr>
 <td>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="4" align="center">
 <tr> 
  <td width="60%"><span class="lgText"><img src="<% = portal.variablesForum.strImagePath %>subject_folder.gif" alt="<% = portal.variablesForum.strTxtSubjectFolder %>" align="middle"> <% = portal.variablesForum.strTxtPrivateMessenger & ": " & portal.variablesForum.strTxtOutbox %></span></td>
  <td align="right" width="40%" nowrap="nowrap"><a href="pm_inbox.aspx" target="_self"><img src="<% = portal.variablesForum.strImagePath %>inbox.gif" alt="<% = portal.variablesForum.strTxtPrivateMessenger & " " & portal.variablesForum.strTxtInbox %>" border="0"></a><a href="pm_outbox.aspx" target="_self"><img src="<% = portal.variablesForum.strImagePath %>outbox.gif" alt="<% = portal.variablesForum.strTxtPrivateMessenger & " " & portal.variablesForum.strTxtOutbox %>" border="0"></a><a href="pm_buddy_list.aspx" target="_self"><img src="<% = portal.variablesForum.strImagePath %>buddy_list.gif" alt="<% = portal.variablesForum.strTxtPrivateMessenger & " " & portal.variablesForum.strTxtBuddyList %>" border="0"></a><a href="pm_new_message_form.aspx" target="_self"><img src="<% = portal.variablesForum.strImagePath %>new_private_message.gif" alt="<% = portal.variablesForum.strTxtNewPrivateMessage %>" border="0"></a></td>
 </tr>
</table>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr>
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableBgColour %>">
   <table width="100%" border="0" cellspacing="1" cellpadding="3" height="14" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="3%" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>" align="center"><% = portal.variablesForum.strTxtRead %></td>
     <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="39%" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtMessageTitle %></td>
     <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="22%" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtMessageTo %></td>
     <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="31%" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtDate %></td>
    </tr><%
    
'Check there are PM messages to display
If rsPmMessage.EOF Then

	'If there are no pm messages to display then display the appropriate error message
	Response.Write vbCrLf & "<td bgcolor=""" & portal.variablesForum.strTableColour & """ background=""" & portal.variablesForum.strTableBgImage & """ colspan=""5"" class=""text"">" & portal.variablesForum.strTxtNoPrivateMessages & " " & portal.variablesForum.strTxtOutbox & "<input type=""hidden"" name=""chkDelete"" value=""-1""></td>"

'Else there the are topic's so write the HTML to display the topic names and a discription
Else 
	'Get the total number of pm's this user has
	intNumOfPMs = rsPmMessage.RecordCount
	
	'Count the number of pages there are in the recordset calculated by the PageSize attribute set above
	intTotalNumOfPages = rsPmMessage.PageCount

	
	'Loop round to read in all the Topics in the database
	For intRecordLoopCounter = 1 to 10 
	
		'Exit loop if run out of records
		If rsPmMessage.EOF Then Exit For
	%>
    <tr> 
     <td bgcolor="<% If (intRecordLoopCounter MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" width="3%" class="text" align="center"><% 
      
     		If CBool(rsPmMessage("Read_Post")) = False Then
     			Response.Write("<img src=""" & portal.variablesForum.strImagePath & "unread_private_message.gif"" alt=""" & portal.variablesForum.strTxtUnreadMessage & """>")
     		Else
     			Response.Write("<img src=""" & portal.variablesForum.strImagePath & "read_private_message.gif"" alt=""" & portal.variablesForum.strTxtReadMessage & """>")
     		End If
     
     %>
     </td>
     <td bgcolor="<% If (intRecordLoopCounter MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" width="39%" class="text"><% Response.Write("<a href=""pm_show_message.aspx?ID=" & rsPmMessage("PM_ID") & "&amp;m=OB"" target=""_self"">" & rsPmMessage("PM_Tittle") & "</a>") %></td>
     <td bgcolor="<% If (intRecordLoopCounter MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" width="22%" class="text"><a href="JavaScript:openWin('pop_up_profile.aspx?PF=<% = rsPmMessage("UsuarioID") %>','profile','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=590,height=425')"><% = rsPmMessage("usuario") %></a></td>
     <td bgcolor="<% If (intRecordLoopCounter MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" width="31%" class="smText" nowrap="nowrap"><% Response.Write(funcFecha.DateFormat(rsPmMessage("PM_Message_Date"), funcFecha.saryDateTimeData) & " " & portal.variablesForum.strTxtAt & " " & funcFecha.TimeFormat(rsPmMessage("PM_Message_Date"), funcFecha.saryDateTimeData)) %></td>
    </tr><%
		
		'Move to the next recordset
		rsPmMessage.MoveNext
	Next
End If
%>
   </table>
  </td>
 </tr>
</table>
</td>
 </tr>
</table>
    <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="2" align="center">
     <tr>
      <td class="text"><% Response.Write(portal.variablesForum.strTxtMessagesInOutBox) %></td>
     </tr>
    </table>
    <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="2" align="center">
     <tr> 
      <td class="text"><img src="<% = portal.variablesForum.strImagePath %>unread_private_message.gif" alt="<% = portal.variablesForum.strTxtUnreadMessage %>"> <% = portal.variablesForum.strTxtUnreadMessage %>&nbsp;&nbsp;<img src="<% = portal.variablesForum.strImagePath %>read_private_message.gif" alt="<% = portal.variablesForum.strTxtReadMessage %>"> <% = portal.variablesForum.strTxtReadMessage %></td>
      <%

'If there is more than 1 page of topics then dispaly drop down list to the other topics
If intTotalNumOfPages > 1 Then   

	
	'Display an drop down list to the other members in list
	Response.Write (vbCrLf & "		<td align=""right"" class=""text"">")
		
	'Display a prev link if previous pages are available
	If intRecordPositionPageNum > 1 Then Response.Write("<a href=""pm_outbox.aspx?PN=" & intRecordPositionPageNum - 1 & """>&lt;&amp;lt&nbsp;" & portal.variablesForum.strTxtPrevious & "</a>&nbsp;")
		
	Response.Write (portal.variablesForum.strTxtPage & " " & _
	vbCrLf & "		 <select onChange=""ForumJump(this)"" name=""SelectTopicPage"">")
	
	'Loop round to display links to all the other pages
	For intPageLoopCounter = 1 to intTotalNumOfPages  
	
		'Display a link in the link list to the another topic page
		Response.Write (vbCrLf & "		  <option value=""pm_outbox.aspx?PN=" & intPageLoopCounter & """")
		
		'If this page number to display is the same as the page being displayed then make sure it's selected
		If intPageLoopCounter = intRecordPositionPageNum Then
			Response.Write (" selected")
		End If
		
		'Display the link page number
		Response.Write (">" & intPageLoopCounter & "</option>")
	
	Next
	
	'End the drop down list
	Response.Write (vbCrLf & "		</select> " & portal.variablesForum.strTxtOf & " " & intTotalNumOfPages)
		
	'Display a next link if needed
	If intRecordPositionPageNum <> intTotalNumOfPages Then Response.Write("&nbsp;<a href=""pm_outbox.aspx?PN=" & intRecordPositionPageNum + 1 & """>" & portal.variablesForum.strTxtNext & "&nbsp;&gt;&gt;</a>")
		
	Response.Write("</td>")
End If
%>
     </tr>
    </table>
</td>
</tr>
</table>
<br />
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="4" align="center">
  <tr><form>
   <td><!-- #include file="includes/forum_jump_inc.aspx" --></td>
   </form>
  </tr>
 </table>
<div align="center"><br />
 <%
'Clear server objects
rsPmMessage.Close
Set rsPmMessage = Nothing
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing


'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"
%>
 
