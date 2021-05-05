

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include file="language_files/pm_language_file_inc.aspx" -->

<%
'Set the buffer to true
Response.Buffer = True

'Declare variables
Dim intRowColourNumber	'Holds the number to calculate the table row colour	
Dim blnIsUserOnline	'Set to true if the user is online
Dim intForumID

'Initialise variable
intRowColourNumber = 0


'If the user is user is using a banned IP redirect to an error page
If bannedIP() Then
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	Response.Redirect("insufficient_permission.aspx?M=IP")

End If



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


%>
<html>
<head>

<title>Private Messenger: Buddy List</title>


     	

<script  language="javascript">

//Function to check form is filled in correctly before submitting
function CheckForm () {
	
	var errorMsg = "";
	
	//Check for a buddy
	if (document.frmBuddy.usuario.value==""){
		errorMsg += "\n\t<% = portal.variablesForum.strTxtNoBuddyErrorMsg %>";
	}
	
	//If there is aproblem with the form then display an error
	if (errorMsg != ""){
		msg = "<% = portal.variablesForum.strTxtErrorDisplayLine %>\n\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine1 %>\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine2 %>\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine %>\n\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine3 %>\n";
		
		errorMsg += alert(msg + errorMsg + "\n\n");
		return false;
	}
	
	return true;
}
</script>
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
  <td width="60%"><span class="lgText"><img src="<% = portal.variablesForum.strImagePath %>subject_folder.gif" width="26" height="26" alt="<% = portal.variablesForum.strTxtSubjectFolder %>" align="middle"> <% = portal.variablesForum.strTxtPrivateMessenger & ": " & portal.variablesForum.strTxtBuddyList %></span></td>
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
     <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="19%" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtBuddy %></td>
     <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="40%" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtDescription %></td>
     <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="35%" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtContactStatus %></td><%
If portal.variablesForum.blnActiveUsers Then %>
     <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="6%" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtOnLine2 %></td><%
End If %>
     <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="6%" align="center" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtDelete %></td>
    </tr><%
    
'Get the users buddy detals from the db
	
'Initlise the sql statement
strSQL = "SELECT " & portal.variablesForum.strDbTable & "BuddyList.*, " & "Usuarios.usuario, " & "Usuarios.UsuarioID "
strSQL = strSQL & "FROM " & "Usuarios, " & portal.variablesForum.strDbTable & "BuddyList "
strSQL = strSQL & "WHERE " & "Usuarios.UsuarioID = " & portal.variablesForum.strDbTable & "BuddyList.Buddy_ID AND " & portal.variablesForum.strDbTable & "BuddyList.UsuarioID=" & portal.variablesForum.lngLoggedInUserID & " AND " & portal.variablesForum.strDbTable & "BuddyList.Buddy_ID <> 2 "
strSQL = strSQL & "ORDER BY " & portal.variablesForum.strDbTable & "BuddyList.Block ASC, " & "Usuarios.usuario ASC;" 
	

'Query the database
rsCommon=db.execute(strSQL)
    
'Check there are PM messages to display
If rsCommon.EOF Then

	'If there are no pm messages to display then display the appropriate error message
	Response.Write vbCrLf & "<td bgcolor=""" & portal.variablesForum.strTableColour & """ background=""" & portal.variablesForum.strTableBgImage & """ colspan=""5"" class=""text"">" & portal.variablesForum.strTxtNoBuddysInList & "</td>"

'Else there the are topic's so write the HTML to display the topic names and a discription
Else 	
	
	'Loop round to read in all the Topics in the database
	Do while NOT rsCommon.EOF 
		
		'Get the row number
		intRowColourNumber = intRowColourNumber + 1
	%>
    <tr> 
     <td bgcolor="<% If (intRowColourNumber MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" width="19%" class="text"><a href="JavaScript:openWin('pop_up_profile.aspx?PF=<% = rsCommon("Buddy_ID") %>','profile','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=590,height=425')"><% = rsCommon("usuario") %></a></td>
     <td bgcolor="<% If (intRowColourNumber MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" width="40%" class="text"><% = rsCommon("Description") %>&nbsp;</td>
     <td bgcolor="<% If (intRowColourNumber MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" width="35%" class="text"><%
     		'Get the contact status
     		If rsCommon("Block") = True Then
     			Response.Write(portal.variablesForum.strTxtThisPersonCanNotMessageYou)
     		Else
     			Response.Write(portal.variablesForum.strTxtThisPersonCanMessageYou)
     		End If
     %></td><%
		'If active users is enabled see if any buddies are online
		If portal.variablesForum.blnActiveUsers Then 
			
			'Initilase variable
			blnIsUserOnline = False
			
			'Get the users online status
			For intArrayPass = 1 To UBound(saryActiveUsers, 2)
				If saryActiveUsers(1, intArrayPass) = CLng(rsCommon("Buddy_ID")) Then blnIsUserOnline = True
			Next
			
			%>
     <td bgcolor="<% If (intRowColourNumber MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" width="6%" class="text" align="center"><% If blnIsUserOnline Then Response.Write("<img src=""" & portal.variablesForum.strImagePath & "yes.gif""/>") Else Response.Write("<img src=""" & portal.variablesForum.strImagePath & "no.gif""/>") %></td><%
		 	
		End If 
		
		%>
     <td bgcolor="<% If (intRowColourNumber MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" width="6%" class="text" align="center"><a href="pm_delete_buddy.aspx?pm_id=<% = rsCommon("Address_ID") %>" OnClick="return confirm('<% = portal.variablesForum.strTxtDeleteBuddyAlert %>')"><img src="<% = portal.variablesForum.strImagePath %>delete_icon.gif" width="15" height="16" alt="<% = portal.variablesForum.strTxtDelete %>" border="0"></a></td>
    </tr><%

		
		'Move to the next recordset
		rsCommon.MoveNext
	Loop
End If

'clear up
rsCommon.Close
%>
   </table>
  </td>
 </tr>
</table>
</td>
 </tr>
</table>
<br />
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr><form method="post" name="frmBuddy" action="pm_add_buddy.aspx" onSubmit="return CheckForm();" onReset="return ResetForm();">
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableBgColour %>">
   <table width="100%" border="0" cellspacing="1" cellpadding="3" height="14" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtAddNewBuddyToList %></td>
    </tr>
    <tr> 
     <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>"> 
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
       <tr>
         <td width="23%" class="text"><% = portal.variablesForum.strTxtMemberName %></td>
        <td width="32%" class="text"><% = portal.variablesForum.strTxtDescription %></td>
        <td width="24%" class="text"><% = portal.variablesForum.strTxtAllowThisMemberTo %></td>
        <td width="21%" class="text">&nbsp;</td>
       </tr>
       <tr>
        <td width="23%"> 
         <input type='text' name="usuario" size="15" maxlength="15" value="<% If CInt(Request.QueryString("code")) <> 2 Then Response.Write(Server.HTMLEncode(Request.QueryString("name"))) %>">
          <a href="JavaScript:openWin('pop_up_member_search.aspx?RP=BUD','profile','toolbar=0,location=0,status=0,menubar=0,scrollbars=0,resizable=1,width=440,height=255')"><img src="<% = portal.variablesForum.strImagePath %>search.gif" alt="<% = portal.variablesForum.strTxtMemberSearch %>" border="0" align="middle"></a> 
         </td>
        <td width="32%"> 
         <input type='text' name="description" size="25" maxlength="30" value="<% If CInt(Request.QueryString("code")) <> 2 Then Response.Write(Server.HTMLEncode(Request.QueryString("desc"))) %>">
        </td>
        <td width="24%"> 
         <select name="blocked">
          <option value="False" selected><% = portal.variablesForum.strTxtMessageMe %></option>
          <option value="True"><% = portal.variablesForum.strTxtNotMessageMe %></option>
         </select>
        </td>
        <td width="21%" align="right"><input type='submit' name="Submit" value="<% = portal.variablesForum.strTxtAddToBuddy %>"></td>
       </tr>
      </table>
     </td>
    </tr>
   </table>
  </td>
 </tr>
</table>
</td>
</form></tr>
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
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing


'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"

%>
</div>
<%
'Display a msg letting the user know any add or delete details to the buddy list
Select Case Request.QueryString("ER")
	Case "1"
		Response.Write("<script  language=""JavaScript"">")
		Response.Write("alert('" & Replace(Server.HTMLEncode(Request.QueryString("name")), "'", "\'", 1, -1, 1) & " " & portal.variablesForum.strTxtIsAlreadyInYourBuddyList & ".');")
		Response.Write("</script>")
	Case "2"
		Response.Write("<script  language=""JavaScript"">")
		Response.Write("alert('" & Replace(Server.HTMLEncode(Request.QueryString("name")), "'", "\'", 1, -1, 1) & ", " & portal.variablesForum.strTxtUserCanNotBeFoundInDatabase & ".');")
		Response.Write("</script>")
End Select
%>

