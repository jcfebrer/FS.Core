<%@ Page Language="VB" AutoEventWireup="false" CodeFile="active_users.aspx.vb" Inherits="active_users" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />

<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True 





'If active users is off redirect back to the homepage
If portal.variablesForum.blnActiveUsers = False Then
	
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	Response.Redirect("default.aspx")
End If




'Make sure this page is not cached
Response.Expires = -1
Response.ExpiresAbsolute = Now() - 2
Response.AddHeader "pragma","no-cache"
Response.AddHeader "cache-control","private"
Response.CacheControl = "No-Store"



'Dimension variables
Dim lngUserID			'Holds the active users ID
Dim strusuario			'Holds the active users usuario
Dim strForumName 		'Holds the forum name
Dim intGuestNumber		'Holds the Guest Number
Dim dtmLoggedIn			'Holds the date/time the user logged in
Dim dtmLastActive		'Holds the date/time the user was last active
Dim intActiveUsers		'Holds the number of active users
Dim intActiveGuests		'Holds the number of active guests
Dim intActiveMembers		'Holds the number of logged in active members
Dim intForumColourNumber	'Holds the number to calculate the table row colour	
Dim intForumID			'Holds the forum ID number if there is one

'Initilise variables
intActiveMembers = 0
intActiveGuests = 0
intActiveUsers = 0
intGuestNumber = 0
intForumColourNumber = 0


'Sort the active users array
Call SortActiveUsersList(saryActiveUsers)

%>
<html> 
<head>
<title>Discussion Forum Active Users</title>

<!-- #include file="includes/header.aspx" -->
<navigation:navigation ID="common1" runat="server" />
<!--  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="3" align="center">
 <tr> 
  <td align="left" class="heading"><% = portal.variablesForum.strTxtActiveForumUsers %></td>
</tr>
 <tr> 
  <td align="left" width="71%" class="bold"><img src="<% = portal.variablesForum.strImagePath %>open_folder_icon.gif" border="0" align="middle">&nbsp;<a href="default.aspx" target="_self" class="boldLink"><% = strMainForumName %></a><% = strNavSpacer %><% = portal.variablesForum.strTxtActiveForumUsers %><br /></td>
  </tr>
</table>-->
   <div align="center"> <br /><%

'Clean up
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing


'Get the number of active users
'Get the active users online
For intArrayPass = 1 To UBound(saryActiveUsers, 2)
	
	'If this is a guest user then increment the number of active guests veriable
	If saryActiveUsers(1, intArrayPass) = 2 Then 	
			
		intActiveGuests = intActiveGuests + 1
	End If
		
Next 

'Calculate the number of members online and total people online
intActiveUsers = UBound(saryActiveUsers, 2)
intActiveMembers = intActiveUsers - intActiveGuests

Response.Write("    	<span class=""text"">" & portal.variablesForum.strTxtThereAreCurrently & " " & intActiveUsers & " " & portal.variablesForum.strTxtActiveUsers & " " & portal.variablesForum.strTxtOnLine & ", "  & intActiveGuests & " " & portal.variablesForum.strTxtGuestsAnd & " " & intActiveMembers & " " & portal.variablesForum.strTxtMembers & "</span><br />")
%>    
    <br />
    <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr>
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableBgColour %>">
   <table width="100%" border="0" cellspacing="1" cellpadding="3" height="14" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
         <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="93" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtusuario %></td>
         <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="120" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtLoggedIn %></td>
         <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="117" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtLastActive %></td>
         <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="66" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtActive %></td>
         <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="95" align="left" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtOS %></td>
         <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="96" align="left" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtBrowser %></td>
        </tr>
        <%  


        		
'display the active users
For intArrayPass = 1 To UBound(saryActiveUsers, 2)

	intForumColourNumber = intForumColourNumber + 1

	'Read in the details from the rs
	lngUserID = saryActiveUsers(1, intArrayPass)
	strusuario = saryActiveUsers(2, intArrayPass)
	dtmLoggedIn = saryActiveUsers(3, intArrayPass)
	dtmLastActive = saryActiveUsers(4, intArrayPass)
	strOS = saryActiveUsers(5, intArrayPass)
	strBrowserUserType = saryActiveUsers(6, intArrayPass)
	blnHideActiveUser = CBool(saryActiveUsers(7, intArrayPass))
	
			
			'Write the HTML of the Topic descriptions as hyperlinks to the Topic details and message
			%>
        <tr> 
         <td bgcolor="<% If (intForumColourNumber MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" width="93" height="24" class="text"><% 
          
         'If the user is a Guest then display them as a Guest
         If lngUserID = 2 Then
         
         	'Add 1 to the Guest number
         	intGuestNumber = intGuestNumber + 1
         	
         	'Display the User as Guest
         	Response.Write(portal.variablesForum.strTxtGuest & " "& intGuestNumber)
         
         'If the user wants to hide there ID then do so 
         ElseIf blnHideActiveUser Then
         	
         	'Display the user as an annoy
         	Response.Write(portal.variablesForum.strTxtAnnoymous)
         
         'Else display the users name
         Else %>
          <a href="JavaScript:openWin('pop_up_profile.aspx?PF=<% = lngUserID %>','profile','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=590,height=425')"><% = strusuario %></a> 
          <% 
        End If
        %>
         </td>
         <td bgcolor="<% If (intForumColourNumber MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" class="smText"><% Response.Write(funcFecha.DateFormat(dtmLoggedIn, funcFecha.saryDateTimeData) & " " & portal.variablesForum.strTxtAt & "&nbsp;" & funcFecha.TimeFormat(dtmLoggedIn, funcFecha.saryDateTimeData))  %></td>
         <td bgcolor="<% If (intForumColourNumber MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" class="smText"><% Response.Write(funcFecha.DateFormat(dtmLastActive, funcFecha.saryDateTimeData) & " " & portal.variablesForum.strTxtAt & "&nbsp;" & funcFecha.TimeFormat(dtmLastActive, funcFecha.saryDateTimeData)) %></td>
         <td bgcolor="<% If (intForumColourNumber MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"><% = DateDiff("n", dtmLoggedIn, dtmLastActive) %>&nbsp;<% = portal.variablesForum.strTxtMinutes %></td>
         <td bgcolor="<% If (intForumColourNumber MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text" nowrap="nowrap"><% = strOS %></td>
         <td bgcolor="<% If (intForumColourNumber MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text" nowrap="nowrap"><% = strBrowserUserType %></td>
        </tr>
        <%
		
	   		
Next
	


%>
       </table>
     </tr>
    </table>
    </td>
 </tr>
</table>
    <br /><span class="text"><% = portal.variablesForum.strTxtDataBasedOnActiveUsersInTheLastXMinutes %></span><br /><br /><% 
    

'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"
%>
   </div>

