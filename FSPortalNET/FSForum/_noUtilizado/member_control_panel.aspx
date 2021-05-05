

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include file="language_files/cp_language_file_inc.aspx" -->
<%
'Set the buffer to true
Response.Buffer = True

'Declare variables
Dim intForumID
Dim lngUserProfileID            'Holds the users ID of the profile to get
Dim strMode			'Holds the mode of the page



'If the user his not activated their mem
If blnActiveMember = False Then

        'clean up before redirecting
        Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

        'redirect to insufficient permissions page
        Response.Redirect("insufficient_permission.aspx?M=ACT")
End If

'If the user has not logged in then redirect them to the insufficient permissions page
If portal.variablesForum.intGroupID = 2 Then

        'clean up before redirecting
        Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

        'redirect to insufficient permissions page
        Response.Redirect("insufficient_permission.aspx")
End If



'Read in the mode of the page
strMode = Trim(Mid(Request.QueryString("M"), 1, 1))


'If this is not an admin but in admin mode then see if the user is a moderator
If portal.variablesForum.blnAdmin = False AND strMode = "A" Then
	
	'Initalise the strSQL variable with an SQL statement to query the database
        strSQL = "SELECT " & portal.variablesForum.strDbTable & "Permissions.Moderate "
        strSQL = strSQL & "FROM " & portal.variablesForum.strDbTable & "Permissions "
        If portal.variablesForum.strDatabaseType = "SQLServer" Then
                strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Permissions.Group_ID = " & portal.variablesForum.intGroupID & " AND  " & portal.variablesForum.strDbTable & "Permissions.Moderate = 1;"
        Else
                strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Permissions.Group_ID = " & portal.variablesForum.intGroupID & " AND  " & portal.variablesForum.strDbTable & "Permissions.Moderate = True;"
        End If
               

        'Query the database
         rsCommon=db.execute(strSQL)

        'If a record is returned then the user is a moderator in one of the forums
        If NOT rsCommon.EOF Then portal.variablesForum.blnModerator = True

        'Clean up
        rsCommon.Close
End If


'Get the user ID of the member being edited if in admin mode
If (portal.variablesForum.blnAdmin OR (portal.variablesForum.blnModerator AND CLng(Request.QueryString("PF")) > 2)) AND strMode = "A" AND CLng(Request.QueryString("PF")) <> 2 Then
	
	lngUserProfileID = CLng(Request.QueryString("PF"))

'Get the logged in ID number
Else
	lngUserProfileID = portal.variablesForum.lngLoggedInUserID
End If
%>
<html>
<head>

<title>Member Control Panel Menu</title>


     	
     	
<!-- #include file="includes/header.aspx" -->
<navigation:navigation ID="common1" runat="server" />
<a name="top"></a>
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="3" align="center">
 <tr> 
  <td class="heading"><% = portal.variablesForum.strTxtMemberCPMenu %></td>
</tr>
 <tr> 
  <td width="60%" class="bold"><img src="<% = portal.variablesForum.strImagePath %>open_folder_icon.gif" border="0" align="middle">&nbsp;<a href="default.aspx" target="_self" class="boldLink"><% = strMainForumName %></a><% = strNavSpacer %><% = portal.variablesForum.strTxtMemberCPMenu %><br /><%

 
	Response.Write(vbCrlf & "  <td width=""40%"" align=""right"" nowrap=""nowrap""><a href=""member_control_panel.aspx")
	'If this is in admin mode then add the profile to be edited
	If strMode = "A" AND (portal.variablesForum.blnAdmin OR portal.variablesForum.blnModerator) Then Response.Write("?PF=" & lngUserProfileID & "&amp;m=A")
	Response.Write(""" target=""_self""><img src=""" & portal.variablesForum.strImagePath & "cp_menu.gif"" border=""0"" alt=""" & portal.variablesForum.strTxtMemberCPMenu & """></a>")

	
	Response.Write("<a href=""register.aspx")
	'If this is in admin mode then add the profile to be edited
	If strMode = "A" AND (portal.variablesForum.blnAdmin OR portal.variablesForum.blnModerator) Then Response.Write("?PF=" & lngUserProfileID & "&amp;m=A")
	Response.Write(""" target=""_self""><img src=""" & portal.variablesForum.strImagePath & "profile.gif"" border=""0"" alt=""" & portal.variablesForum.strTxtEditProfile & """></a>")

'email notify is on show link to email subcriptions
If portal.variablesForum.blnEmail Then
		
	Response.Write("<a href=""email_notify_subscriptions.aspx")	
	'If this is in admin mode then allow the admin or modertor change this users email subscriptions
	If strMode = "A" AND (portal.variablesForum.blnAdmin OR portal.variablesForum.blnModerator) Then Response.Write("?PF=" & lngUserProfileID & "&amp;m=A")
	Response.Write(""" target""_self""><img src=""" & portal.variablesForum.strImagePath & "email_notify.gif"" border=""0"" alt=""" & portal.variablesForum.strTxtEmailNotificationSubscriptions & """></a>")

End If

%></td>
  </tr>
</table>
<br />
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr>
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableBgColour %>">
   <table width="100%" border="0" cellspacing="1" cellpadding="4" height="14" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtMemberCPMenu %></td>
    </tr>
    <tr> 
     <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text">
      <table width="100%" border="0" cellspacing="0" cellpadding="0" class="text">
          <tr>
           <td><a href="register.aspx<% If strMode = "A" AND (portal.variablesForum.blnAdmin OR portal.variablesForum.blnModerator) Then Response.Write("?PF=" & lngUserProfileID & "&amp;m=A") %>"><% = portal.variablesForum.strTxtEditProfile %></a><br /><% = portal.variablesForum.strTxtChangeProfile %><br /></td>
          </tr>
          <tr>
           <td><br /><a href="register.aspx?FPN=1<% If strMode = "A" AND (portal.variablesForum.blnAdmin OR portal.variablesForum.blnModerator) Then Response.Write("&amp;pF=" & lngUserProfileID & "&amp;m=A") %>"><% = portal.variablesForum.strTxtRegistrationDetails %></a><br /><% = portal.variablesForum.strTxtChangePassAndEmail %><br /></td>
          </tr>
          </tr>
          <tr>
           <td><br /><a href="register.aspx?FPN=2<% If strMode = "A" AND (portal.variablesForum.blnAdmin OR portal.variablesForum.blnModerator) Then Response.Write("&amp;pF=" & lngUserProfileID & "&amp;m=A") %>"><% = portal.variablesForum.strTxtProfileInfo %></a><br /><% = portal.variablesForum.strTxtChangeProfileInfo %><br /></td>
          </tr>
          </tr>
          <tr>
           <td><br /><a href="register.aspx?FPN=3<% If strMode = "A" AND (portal.variablesForum.blnAdmin OR portal.variablesForum.blnModerator) Then Response.Write("&amp;pF=" & lngUserProfileID & "&amp;m=A") %>"><% = portal.variablesForum.strTxtForumPreferences %></a><br /><% = portal.variablesForum.strTxtChangeForumPreferences %><br /></td>
          </tr><%
          
'email notify is on show link to email subcriptions
If portal.variablesForum.blnEmail Then
%>	
          <tr>
           <td><br /><a href="email_notify_subscriptions.aspx<% If strMode = "A" AND (portal.variablesForum.blnAdmin OR portal.variablesForum.blnModerator) Then Response.Write("?PF=" & lngUserProfileID & "&amp;m=A") %>"><% = portal.variablesForum.strTxtEmailNotificationSubscriptions %></a><br /><% = portal.variablesForum.strTxtAlterEmailSubscriptions %><br /></td>
          </tr><%
End If
         
'If PM is on then show links to PM functions
If blnPrivateMessages AND strMode <> "A" Then
	%>
          <tr>
           <td><br /><a href="pm_welcome.aspx"><% = portal.variablesForum.strTxtPrivateMessenger %></a><br /><% = portal.variablesForum.strTxtReadandSendPMs %><br /></td>
          </tr>
          <tr>
           <td><br /><a href="pm_buddy_list.aspx"><% = portal.variablesForum.strTxtBuddyList %></a><br /><% = portal.variablesForum.strTxtListOfYourForumBuddies %><br /></td>
          </tr><%
End If

'If admin mod mode have link to admin functions
If strMode = "A" AND (portal.variablesForum.blnAdmin OR portal.variablesForum.blnModerator) Then
%>
          <tr>
           <td><br /><a href="register.aspx?PF=<% = lngUserProfileID %>&amp;m=A#admin"><% = portal.variablesForum.strTxtAdminAndModFunc %></a><br /><% = portal.variablesForum.strTxtAdminFunctionsTo %><br /></td>
          </tr><%
End If

%>
          <tr>
           <td><br /><a href="help.aspx"><% = portal.variablesForum.strTxtForumHelp %></a><br /><% = portal.variablesForum.strTxtForumHelpFilesandFAQsToHelpYou %><br /></td>
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
<div align="center"><br /><%

'Clean up
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

	


'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"
%>
</div>

