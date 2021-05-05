

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />

<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True

Dim lngProfileNum		'Holds the profile number of the user we are getting the profile for
Dim strusuario			'Holds the users usuario
Dim intUsersGroupID		'Holds the users group ID
Dim strEmail			'Holds the new users e-mail address
Dim blnShowEmail		'Boolean set to true if the user wishes there e-mail address to be shown
Dim strLocation			'Holds the new users location
Dim strHomepage			'Holds the new users homepage if they have one
Dim strAvatar			'Holds the avatar image
Dim intForumID			'Holds the forum ID if within a forum
Dim strICQNum			'Holds the users ICQ Number
Dim strAIMAddress		'Holds the users AIM address
Dim strMSNAddress		'Holds the users MSN address
Dim strYahooAddress		'Holds the users Yahoo Address
Dim strOccupation		'Holds the users Occupation
Dim strInterests		'Holds the users Interests
Dim dtmJoined			'Holds the joined date
Dim lngNumOfPosts		'Holds the number of posts the user has made
Dim dtmDateOfBirth		'Holds the users Date Of Birth
Dim dtmLastVisit		'Holds the date the user last came to the forum
Dim strGroupName		'Holds the group name
Dim intRankStars 		'Holds the rank stars
Dim strRankCustomStars		'Holds the custom stars image if there is one
Dim blnProfileReturned		'Boolean set to false if the user's profile is not found in the database
Dim blnGuestUser		'Set to True if the user is a guest or not logged in
Dim blnActive			'Set to true of the users account is active
Dim strRealName			'Holds the persons real name
Dim strMemberTitle		'Holds the members title
Dim blnIsUserOnline		'Set to true if the user is online


'Initalise variables
blnProfileReturned = True
blnGuestUser = False
blnShowEmail = False
portal.variablesForum.blnModerator = False
blnIsUserOnline = False
lngNumOfPosts = 0

'If the user is using a banned IP address then don't let the view a profile
If bannedIP() Then blnActiveMember = False

'Read in the profile number to get the details on
lngProfileNum = CLng(Request.QueryString("PF"))

'If the user has logged in then the Logged In User ID number will be more than 0
If portal.variablesForum.intGroupID <> 2 Then


	'First see if the user is a in a moderator group for any forum
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


	'Read the various forums from the database
	'Initalise the strSQL variable with an SQL statement to query the database
	strSQL = "SELECT " & "Usuarios.*, " & portal.variablesForum.strDbTable & "Group.Name, " & portal.variablesForum.strDbTable & "Group.Stars, " & portal.variablesForum.strDbTable & "Group.Custom_stars "
	strSQL = strSQL & "FROM " & "Usuarios, " & portal.variablesForum.strDbTable & "Group "
	strSQL = strSQL & "WHERE " & "Usuarios.Group_ID = " & portal.variablesForum.strDbTable & "Group.Group_ID AND " & "Usuarios.UsuarioID = " & lngProfileNum

	'Query the database
	rsCommon=db.execute(strSQL)

	'Read in the details if a profile is returned
	If NOT rsCommon.EOF Then

		'Read in the new user's profile from the recordset
		strusuario = rsCommon("usuario")
		strRealName = rsCommon("nombre")
		intUsersGroupID = CInt(rsCommon("Group_ID"))
		strEmail = rsCommon("email")
		blnShowEmail = CBool(rsCommon("Show_email"))
		strHomepage = rsCommon("Homepage")
		strLocation = rsCommon("Location")
		strAvatar = rsCommon("Avatar")
		strMemberTitle = rsCommon("Avatar_title")
		strICQNum = rsCommon("ICQ")
		strAIMAddress = rsCommon("AIM")
		strMSNAddress = rsCommon("MSN")
		strYahooAddress = rsCommon("Yahoo")
		strOccupation = rsCommon("Occupation")
		strInterests = rsCommon("Interests")
		If isDate(rsCommon("DOB")) Then dtmDateOfBirth = CDate(rsCommon("DOB"))
		dtmJoined = CDate(rsCommon("FechaCreacion"))
		lngNumOfPosts = CLng(rsCommon("No_of_posts"))
		dtmLastVisit = rsCommon("UltimaConexion")
		strGroupName = rsCommon("Name")
		intRankStars = CInt(rsCommon("Stars"))
		strRankCustomStars = rsCommon("Custom_stars")
		blnActive = CBool(rsCommon("Active"))

	'Else no profile is returned so set an error variable
	Else
		blnProfileReturned = False

	End If

	'Reset Server Objects
	rsCommon.Close
	
	'Clean up email link
	If strEmail <> "" Then
		strEmail = formatLink(strEmail)
		strEmail = func.formatInput(strEmail)
	End If
	
	
	If portal.variablesForum.blnActiveUsers Then
		'Get the users online status
		For intArrayPass = 1 To UBound(saryActiveUsers, 2)
			If saryActiveUsers(1, intArrayPass) = lngProfileNum Then blnIsUserOnline = True
		Next
	End If


'Else the user is not logged in
Else
	'Set the Guest User boolean to true as the user must be a guest
	blnGuestUser = True

End If


%>
<html>
<head>
<title>Users Profile</title>



     	

<script language="javascript">

//Function to open link in main window
function openInMainWin(winLocation){
	window.opener.location.href = winLocation
	window.opener.focus();
	window.close();
}
</script>

<!--include file="includes/skin_file.aspx" -->

</head>
<body bgcolor="<% = strBgColour %>" text="<% = strTextColour %>" background="<% = strBgImage %>" marginheight="0" marginwidth="0" topmargin="0" leftmargin="0" OnLoad="self.focus();">
<div align="center" class="heading"><% = portal.variablesForum.strTxtProfile %></div><br />
<div align="center"><%

'If no profile can be found then display the appropriate message
If blnProfileReturned = False Then

	Response.Write (vbCrLf & "<span class=""text"">" & portal.variablesForum.strTxtNoUserProfileFound & "</span>")

'If the user is a guest then tell them they must register or login before they can view other users profiles
ElseIf blnGuestUser = True OR blnActiveMember = False Then

	Response.Write (vbCrLf & "<span class=""text"">" & portal.variablesForum.strTxtRegisteredToViewProfile)
	
	'If mem suspended display message
	If blnActiveMember = false and InStr(1, strLoggedInUserCode, "N0act", vbTextCompare) Then
		Response.Write("<span class=""bold""><br /><br />" & portal.variablesForum.strTxtForumMemberSuspended & "</span>")
	'Else account not yet active
	ElseIf blnActiveMember = false Then
		Response.Write("<span class=""bold""><br /><br />" & portal.variablesForum.strTxtForumMembershipNotAct & "</span><br /><br />" & portal.variablesForum.strTxtToActivateYourForumMem)
	End If
	
	Response.Write("</span><br /><br />")
Else

%><table width="550" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = strTableProfileBorderColour %>" align="center">
  <tr>
    <td>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = strTableProfileBgColour %>">
      <tr>
       <td bgcolor="<% = strTableProfileBgColour %>"> <table width="100%" border="0" cellspacing="1" cellpadding="4" bgcolor="<% = strTableProfileBgColour %>">
        <tr>
         <td colspan="2" background="<% = strTableProfileTitleBgImage %>" bgcolor="<% = strTableProfileTitleColour %>" class="tHeading"><% Response.Write(portal.variablesForum.strTxtProfile & ": " & strusuario) %></td>
        </tr>
        <tr>
         <td width="25%" background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>"  align="right" class="text"><% = portal.variablesForum.strTxtusuario %>:</td>
         <td width="75%" background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" class="text"><% = strusuario %></td>
        </tr><%

        'If the user has an avatar then display it
        If blnAvatar = True AND NOT strAvatar = "" Then
        	%>
        <tr>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" align="right" class="text" valign="top"><% = portal.variablesForum.strTxtAvatar %>:</td>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" class="text"><% Response.Write("<img src=""" & strAvatar & """ width=""" & intAvatarWidth & """ height=""" & intAvatarHeight & """ alt=""" & portal.variablesForum.strTxtAvatar & """ OnError=""this.src='avatars/blank.gif', height='0';"">") %>&nbsp;</td>
        </tr><%
	End If
	
	'If there is a member title display it
	If strMemberTitle <> "" Then
	%>
	<tr>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" align="right" class="text"><% = portal.variablesForum.strTxtMemberTitle %>:</td>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" class="text"><% = strMemberTitle %></td>
        </tr><%
        
	End If
	%>
	<tr>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" align="right" class="text"><% = portal.variablesForum.strTxtGroup %>:</td>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" class="text"><% = strGroupName %> <img src="<% 
         	If strRankCustomStars <> "" Then Response.Write(strRankCustomStars) Else Response.Write(portal.variablesForum.strImagePath & intRankStars & "_star_rating.gif") 
		Response.Write(""" alt=""" & strGroupName & """ align=""middle"" />") %></td>
        </tr>
         <tr>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" align="right" class="text"><% = portal.variablesForum.strTxtAccountStatus %>:</td>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" class="text"><% If blnActive = True Then Response.Write(portal.variablesForum.strTxtActive) Else Response.Write(portal.variablesForum.strTxtNotActive)%></td>
        </tr>
        </tr><%
        
        'If active users are enabled display if they are online or not
        If portal.variablesForum.blnActiveUsers Then
        	%>
         <tr>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" align="right" class="text"><% = portal.variablesForum.strTxtOnlineStatus %>:</td>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" class="text"><% If blnIsUserOnline = True Then Response.Write(portal.variablesForum.strTxtOnLine2) Else Response.Write(portal.variablesForum.strTxtOffLine)%></td>
        </tr><%
	End If
%>
        <tr>
         <td width="25%" background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>"  align="right" class="text"><% = portal.variablesForum.strTxtRealName %>:</td>
         <td width="75%" background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" class="text"><% If strRealName <> "" Then Response.Write(strRealName) Else Response.Write(portal.variablesForum.strTxtNotGiven) %></td>
        </tr>
        <tr>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" align="right" class="text"><% = portal.variablesForum.strTxtJoined %>:</td>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" class="text"><% = funcFecha.DateFormat(dtmJoined, funcFecha.saryDateTimeData) %></td>
        </tr>
        <tr>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" align="right" class="text"><% = portal.variablesForum.strTxtLastVisit %>:</td>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" class="text"><% If isDate(dtmLastVisit) Then Response.Write(funcFecha.DateFormat(dtmLastVisit, funcFecha.saryDateTimeData)) %></td>
        </tr>
        <tr>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" align="right" class="text"><% = portal.variablesForum.strTxtPosts %>:</td>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" class="text"><% = lngNumOfPosts %> <span class="smText"><% If lngNumOfPosts > 0 AND DateDiff("d", dtmJoined, Now()) > 0 Then Response.Write("[" & FormatNumber(lngNumOfPosts / DateDiff("d", dtmJoined, Now()), 2) & " " & portal.variablesForum.strTxtPostsPerDay) & "]" %></span></td>
        </tr>
        <tr>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" align="right" class="text"><% = portal.variablesForum.strTxtLocation %>:</td>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" class="text"><% If strLocation = "" Or isNull(strLocation) Then Response.Write(portal.variablesForum.strTxtNotGiven) Else Response.Write(strLocation) %></td>
        </tr>
         <tr>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" align="right" class="text"><% = portal.variablesForum.strTxtDateOfBirth %>:</td>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" class="text"><% 
         
         'If there is a Date of Birth display it
         If isDate(dtmDateOfBirth) Then 
         	
        	 'As formatting the date also will add a time off set, make sure the date is correct
		If portal.variablesForum.strTimeOffSet = "-" Then
			dtmDateOfBirth = DateAdd("h", + portal.variablesForum.intTimeOffSet, dtmDateOfBirth)
		ElseIf portal.variablesForum.strTimeOffSet = "+" Then
			dtmDateOfBirth = DateAdd("h", - portal.variablesForum.intTimeOffSet, dtmDateOfBirth)
		End If
         	
         	'Display the persons Date of Birth
         	Response.Write(funcFecha.DateFormat(dtmDateOfBirth, funcFecha.saryDateTimeData)) 
         	
         Else 	
         	'Display that a Date of Birth was not given
         	Response.Write(portal.variablesForum.strTxtNotGiven) 
         	
        End If%></td>
        </tr>
        <tr>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" align="right" class="text"><% = portal.variablesForum.strTxtHomepage %>:</td>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" class="text"><% If strHomepage = "" OR IsNull(strHomepage) Then Response.Write(portal.variablesForum.strTxtNotGiven) Else Response.Write("<a href=""" & strHomepage & """ target=""_blank"">" & strHomepage & "</a>") %></td>
        </tr>
        <tr>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" align="right" class="text"><% = portal.variablesForum.strTxtOccupation %>:</td>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" class="text"><% If strOccupation = "" OR IsNull(strOccupation) Then Response.Write(portal.variablesForum.strTxtNotGiven) Else Response.Write(strOccupation) %></td>
        </tr>
        <tr>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" align="right" class="text"><% = portal.variablesForum.strTxtInterests %>:</td>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" class="text"><% If strInterests = "" OR IsNull(strInterests) Then Response.Write(portal.variablesForum.strTxtNotGiven) Else Response.Write(strInterests) %></td>
        </tr>
        <tr>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" align="right" class="text"><% = portal.variablesForum.strTxtEmail %>:</td>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" class="text"><%

        'If the user has choosen not to display there e-mail then this field will show private
	If blnShowEmail = False AND portal.variablesForum.blnAdmin = False AND strEmail <> "" Then
        	Response.Write(portal.variablesForum.strTxtPrivate)

        'If no clave then display not given
        ElseIf strEmail = "" OR isNull(strEmail) Then
            	Response.Write(portal.variablesForum.strTxtNotGiven)

        'If email address is shown and the email messenger of the forum is enabled show link button
        ElseIf portal.variablesForum.blnEmailMessenger AND portal.variablesForum.blnEmail Then

        	Response.Write("<a href=""javascript:openInMainWin('email_messenger.aspx?SEID=" & lngProfileNum & "')""><img src=""" & portal.variablesForum.strImagePath & "email_button.gif""  border=""0"" align=""middle"" alt=""" & portal.variablesForum.strTxtSendEmail & """></a>")

        'Else the user allows there e-mail address to be shown so show there e-mail address
        Else
            	Response.Write("<a href=""mailto:" & strEmail & """>" & strEmail & "</a>")
        End If


    %></td><%

    	'If the private messager is on show PM link
    	If blnPrivateMessages Then

    		%>
        <tr>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" align="right" class="text"><% = portal.variablesForum.strTxtPrivateMessage %>:</td>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" class="text"><% Response.Write("<a href=""javascript:openInMainWin('pm_new_message_form.aspx?name=" & Server.URLEncode(Replace(strusuario, "'", "\'",  1, -1, 1)) & "')""><img src=""" & portal.variablesForum.strImagePath & "pm_icon.gif""  border=""0"" align=""middle"" alt=""" & portal.variablesForum.strTxtSendPrivateMessage & """></a>") %></td>
        </tr><%

	End If

%>
    	<tr>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" align="right" class="text"><% = portal.variablesForum.strTxtMSNMessenger %>:</td>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" class="text"><% If strMSNAddress <> "" Then Response.Write(strMSNAddress) Else Response.Write(portal.variablesForum.strTxtNotGiven) %></td>
        </tr>
        <tr>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" align="right" class="text"><% = portal.variablesForum.strTxtAIMAddress %>:</td>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" class="text"><% If strAIMAddress <> "" Then Response.Write("<a href=""javascript:openInMainWin('aim:goim?screenname=" & Server.URLEncode(strAIMAddress) & "&amp;message=Hello+Are+you+there?')""><img src=""" & portal.variablesForum.strImagePath & "aol.gif""  border=""0"" align=""middle"" alt=""" & portal.variablesForum.strTxtAIMAddress & """></a>") Else Response.Write(portal.variablesForum.strTxtNotGiven) %></td>
        </tr>
        <tr>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" align="right" class="text"><% = portal.variablesForum.strTxtYahooMessenger %>:</td>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" class="text"><% If strYahooAddress <> "" Then Response.Write("<a href=""javascript:openInMainWin('http://edit.yahoo.com/config/send_webmesg?.target=" & Server.URLEncode(strYahooAddress) & "&amp;.src=pg')""><img src=""" & portal.variablesForum.strImagePath & "yim.gif""  border=""0"" align=""middle"" alt=""" & portal.variablesForum.strTxtYahooMessenger & """></a>") Else Response.Write(portal.variablesForum.strTxtNotGiven) %></td>
        </tr>
    	<tr>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" align="right" class="text"><% = portal.variablesForum.strTxtICQNumber %>:</td>
         <td background="<% = strTableProfileBgImage %>" bgcolor="<% =(strTableRowProfileColour) %>" class="text"><% If strICQNum <> "" Then Response.Write("<a href=""javascript:openInMainWin('http://wwp.icq.com/scripts/search.dll?to=" & strICQNum & "')""><img src=""" & portal.variablesForum.strImagePath & "icq.gif""  border=""0"" align=""middle"" alt=""" & portal.variablesForum.strTxtICQNumber & """></a>") Else Response.Write(portal.variablesForum.strTxtNotGiven) %></td>
        </tr>
       </table></td>
      </tr>
     </table></td>
  </tr>
 </table><%

	'If the user is an admin or a moderator give them the chance to edit the profile unless it's the main admin account of the guest account
	If portal.variablesForum.blnAdmin OR portal.variablesForum.blnModerator Then

%><br />
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
   <form>
    <tr>
      <td align="center"><input type='submit' name="Submit" value="<% = portal.variablesForum.strTxtEditMembersSettings %>" onClick="openInMainWin('member_control_panel.aspx?PF=<% = lngProfileNum %>&amp;m=A');" /></td>
    </tr>
    </form>
  </table><%
	End If

End If


'Clean up
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
  <br />
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
  <tr>
  <td align="center" height="34">
   <a href="JavaScript:onClick=window.close()"><% = portal.variablesForum.strTxtCloseWindow %></a>
   <br /><br /><%
   

%>
</td>
 </tr>
</table>
</div>
</body>
</html>