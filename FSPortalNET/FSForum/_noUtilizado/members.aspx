

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />

<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True

'Make sure this page is not cached
Response.Expires = -1
Response.ExpiresAbsolute = Now() - 2
Response.AddHeader "pragma","no-cache"
Response.AddHeader "cache-control","private"
Response.CacheControl = "No-Store"

'Dimension variables
Dim strusuario			'Holds the users usuario
Dim strHomepage			'Holds the users homepage if they have one
Dim strEmail			'Holds the users e-mail address
Dim blnShowEmail		'Boolean set to true if the user wishes there e-mail address to be shown
Dim lngUserID			'Holds the new users ID number
Dim lngNumOfPosts		'Holds the number of posts the user has made
Dim intMemberGroupID		'Holds the users interger group ID
Dim strMemberGroupName		'Holds the umembers group name
Dim intRankStars		'holds the number of rank stars the user holds
Dim dtmRegisteredDate		'Holds the date the usre registered
Dim intTotalNumMembersPages	'Holds the total number of pages
Dim intTotalNumMembers		'Holds the total number of forum members
Dim intRecordPositionPageNum	'Holds the page number we are on
Dim intRecordLoopCounter	'Recordset loop counter
Dim dtmLastPostDate		'Holds the date of the users las post
Dim intLinkPageNum		'Holds the page number to link to
Dim strSearchCriteria		'Holds the search critiria
Dim strSortBy			'Holds the way the records are sorted
Dim intSortSelectField		'Holds the sort selection to be shown in the sort list box
Dim intForumID			'Holds the forum ID if within a forum
Dim intGetGroupID		'Holds the group ID
Dim strRankCustomStars		'Holds custom stars for the user group

'Initalise variables
blnShowEmail = False
intGetGroupID = CInt(Request.QueryString("GID"))


'If this is the first time the page is displayed then the members record position is set to page 1
If Request.QueryString("MemPN") = "" Then
	intRecordPositionPageNum = 1

'Else the page has been displayed before so the members page record postion is set to the Record Position number
Else
	intRecordPositionPageNum = CInt(Request.QueryString("MemPN"))
End If


'Get the search critiria for the members to display
If NOT Request.QueryString("SF") = "" Then
	strSearchCriteria = Trim(Mid(Request.QueryString("SF"), 1, 15))
End If


'Take out parts of the usuario that are not permitted
strSearchCriteria = disallowedMemberNames(strSearchCriteria)

'Get rid of milisous code
strSearchCriteria = formatSQLInput(strSearchCriteria)

'Get the sort critiria
Select Case Request.QueryString("SO")
	Case "PT"
		strSortBy = "" & "Usuarios.No_of_posts DESC"
		intSortSelectField = 1
	Case "LU"
		strSortBy = "" & "Usuarios.FechaCreacion DESC"
		intSortSelectField = 2
	Case "OU"
		strSortBy = "" & "Usuarios.FechaCreacion ASC"
		intSortSelectField = 3
	Case "GP"
		strSortBy = "" & portal.variablesForum.strDbTable & "Group.Name ASC"
		intSortSelectField = 4
	Case "SR"
		strSortBy = "" & portal.variablesForum.strDbTable & "Group.Stars ASC"
		intSortSelectField = 5
	Case Else
		strSortBy = "" & "Usuarios.usuario ASC"
		intSortSelectField = 0
End Select


%>
<html>
<head>

<title><% = strMainForumName %> Members</title>


     	

<script  language="javascript">

//Function to check form is filled in correctly before submitting
function CheckForm () {

	//Check for a somthing to search for
	if (document.frmMemberSearch.SF.value==""){

		msg = "<% = portal.variablesForum.strTxtErrorDisplayLine %>\n\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine1 %>\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine2 %>\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine %>\n\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine3 %>\n";

		alert(msg + "\n\t<% = portal.variablesForum.strTxtErrorMemberSerach %>\n\n");
		document.frmMemberSearch.SF.focus();
		return false;
	}

	return true;
}

//Function to choose how the members list is sorted
function MembersSort(SelectSort){

   	if (SelectSort != "") self.location.href = "membem_rs.aspx?SF=<% = Server.URLEncode(Request.QueryString("SF")) %>&amp;gID=<% = intGetGroupID %>&amp;sO=" + SelectSort.options[SelectSort.selectedIndex].value;
	return true;
}

//Function to move to another page of members
function MembersPage(SelectPage){

   	if (SelectPage != -1) self.location.href = "membem_rs.aspx?SF=<% = Server.URLEncode(Request.QueryString("SF")) %>&amp;gID=<% = intGetGroupID %>&amp;sO=<% = Trim(Mid(Request.QueryString("SO"),1,2)) %>&amp;memPN=" + SelectPage.options[SelectPage.selectedIndex].value;
	return true;
}
</script>

<!-- #include file="includes/header.aspx" -->
<navigation:navigation ID="common1" runat="server" />
<!--<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="3" align="center">
  <tr>
  <td align="left" class="heading"><% = portal.variablesForum.strTxtForumMembers %></td>
</tr>
 <tr>
  <td align="left" width="71%" class="bold"><img src="<% = portal.variablesForum.strImagePath %>open_folder_icon.gif" border="0" align="middle">&nbsp;<a href="default.aspx" target="_self" class="boldLink"><% = strMainForumName %></a><% = strNavSpacer %><% = portal.variablesForum.strTxtForumMembers %></td>
 </tr>
</table>-->
    <br />
    <form name="frmMemberSearch" method="get" action="membem_rs.aspx" onSubmit="return CheckForm();">
     <table width="490" border="0" cellspacing="0" cellpadding="1" height="24" align="center" bgcolor="<% = portal.variablesForum.strTableBorderColour %>">
      <tr>
       <td align="center" height="2">
        <table width="100%" border="0" cellspacing="0" cellpadding="4" bgcolor="<% = portal.variablesForum.strTableBorderColour %>">
         <tr>
          <td align="center" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" width="100%" height="20"><span class="text"><% = portal.variablesForum.strTxtMemberSearch %>:</span>
           <input type='text' name="SF" size="15" maxlength="15" value="<% = Server.HTMLEncode(Request.QueryString("SF")) %>">
           <input type='submit' name="Submit" value="<% = portal.variablesForum.strTxtSearch %>">
          </td>
         </tr>
         <tr>
          <td align="center" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" width="100%"><a href="membem_rs.aspx" target="_self"><% = portal.variablesForum.strTxtAll %></a> <a href="membem_rs.aspx?SF=A" target="_self">A</a> <a href="membem_rs.aspx?SF=B" target="_self">B</a> <a href="membem_rs.aspx?SF=C" target="_self">C</a>
           <a href="membem_rs.aspx?SF=D" target="_self">D</a> <a href="membem_rs.aspx?SF=E" target="_self">E</a> <a href="membem_rs.aspx?SF=F" target="_self">F</a>
           <a href="membem_rs.aspx?SF=G" target="_self">G</a> <a href="membem_rs.aspx?SF=H" target="_self">H</a> <a href="membem_rs.aspx?SF=I" target="_self">I</a>
           <a href="membem_rs.aspx?SF=J" target="_self">J</a> <a href="membem_rs.aspx?SF=K" target="_self">K</a> <a href="membem_rs.aspx?SF=L" target="_self">L</a>
           <a href="membem_rs.aspx?SF=M" target="_self">M</a> <a href="membem_rs.aspx?SF=N" target="_self">N</a> <a href="membem_rs.aspx?SF=O" target="_self">O</a>
           <a href="membem_rs.aspx?SF=P" target="_self">P</a> <a href="membem_rs.aspx?SF=Q" target="_self">Q</a> <a href="membem_rs.aspx?SF=R" target="_self">R</a>
           <a href="membem_rs.aspx?SF=S" target="_self">S</a> <a href="membem_rs.aspx?SF=T" target="_self">T</a> <a href="membem_rs.aspx?SF=U" target="_self">U</a>
           <a href="membem_rs.aspx?SF=V" target="_self">V</a> <a href="membem_rs.aspx?SF=W" target="_self">W</a> <a href="membem_rs.aspx?SF=X" target="_self">X</a>
           <a href="membem_rs.aspx?SF=Y" target="_self">Y</a> <a href="membem_rs.aspx?SF=Z" target="_self">Z</a></td>
         </tr>
        </table>
       </td>
      </tr>
     </table>
    </form>
   </div>
   <div align="center"><%
'If the users account is suspended then let them know
If blnActiveMember = False Then
		
	Response.Write (vbCrLf & "<span class=""text"">")
	
	'If mem suspended display message
	If  InStr(1, strLoggedInUserCode, "N0act", vbTextCompare) Then
		Response.Write(portal.variablesForum.strTxtForumMemberSuspended)
	'Else account not yet active
	Else
		Response.Write("<span class=""lgText"">" & portal.variablesForum.strTxtInsufficientPermison & "<br /><br />" & portal.variablesForum.strTxtForumMembershipNotAct & "</span><br /><br />" & portal.variablesForum.strTxtToActivateYourForumMem)
	End If
	'If email is on then place a re-send activation email link
	If InStr(1, strLoggedInUserCode, "N0act", vbTextCompare) = False AND portal.variablesForum.blnEmailActivation AND portal.variablesForum.blnLoggedInUserEmail Then Response.Write("<br /><br /><a href=""JavaScript:openWin('resend_email_activation.aspx','actMail','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=475,height=200')"">" & portal.variablesForum.strTxtResendActivationEmail & "</a>")
	
	Response.Write("</span><br /><br /><br /><br />")
	
'If the user has not logged in dispaly an error message
ElseIf portal.variablesForum.intGroupID = 2 Then

	Response.Write (vbCrLf & "<span class=""bold"">" & portal.variablesForum.strTxtMustBeRegistered & "</span><br /><br />")
	Response.Write (vbCrLf & "<a href=""registration_rules.aspx"" target=""_self""><img src=""" & portal.variablesForum.strImagePath & "register.gif""  alt=""" & portal.variablesForum.strTxtRegister & """ border=""0"" align=""middle"" /></a>&nbsp;&nbsp;<a href=""login_user.aspx"" target=""_self""><img src=""" & portal.variablesForum.strImagePath & "login.gif""  alt=""" & portal.variablesForum.strTxtLogin & """ border=""0"" align=""middle"" /></a><br /><br /><br /><br /><br /><br />")

'If the user has logged in then read in the members from the database and dispaly them
Else

	'If this is to show a group the query the database for the members of the group
	If intGetGroupID <> 0 Then
		'Initalise the strSQL variable with an SQL statement to query the database
		strSQL = "SELECT " & "Usuarios.UsuarioID, " & "Usuarios.usuario, " & "Usuarios.Group_ID, " & "Usuarios.Homepage, " & "Usuarios.No_of_posts, " & "Usuarios.FechaCreacion, " & "Usuarios.Active, " & portal.variablesForum.strDbTable & "Group.Name, " & portal.variablesForum.strDbTable & "Group.Stars, " & portal.variablesForum.strDbTable & "Group.Custom_stars "
		strSQL = strSQL & "FROM " & "Usuarios, " & portal.variablesForum.strDbTable & "Group "
		strSQL = strSQL & "WHERE " & "Usuarios.Group_ID = " & portal.variablesForum.strDbTable & "Group.Group_ID AND " & "Usuarios.Group_ID=" & intGetGroupID & " "
		strSQL = strSQL & "ORDER BY " & strSortBy & ";"

	'Else get all the members from the database
	Else
		'Initalise the strSQL variable with an SQL statement to query the database
		strSQL = "SELECT " & "Usuarios.UsuarioID, " & "Usuarios.usuario, " & "Usuarios.Group_ID, " & "Usuarios.Homepage, " & "Usuarios.No_of_posts, " & "Usuarios.FechaCreacion, " & "Usuarios.Active, " & portal.variablesForum.strDbTable & "Group.Name, " & portal.variablesForum.strDbTable & "Group.Stars, " & portal.variablesForum.strDbTable & "Group.Custom_stars "
		strSQL = strSQL & "FROM " & "Usuarios, " & portal.variablesForum.strDbTable & "Group "
		strSQL = strSQL & "WHERE " & "Usuarios.Group_ID = " & portal.variablesForum.strDbTable & "Group.Group_ID AND " & "Usuarios.usuario Like '" & strSearchCriteria & "%' "
		strSQL = strSQL & "ORDER BY " & strSortBy & ";"
	End If

	'Set the cursor type property of the record set to dynamic so we can naviagate through the record set
	rsCommon.CursorType = 1

	'Query the database
	rsCommon=db.execute(strSQL)

	'Set the number of records to display on each page
	rsCommon.PageSize = 25
	
	


	'If there are no memebers to display then show an error message
	If rsCommon.EOF Then
		response.write("<span class=""bold"">" & portal.variablesForum.strTxtSorryYourSearchFoundNoMembers & "</span><br /><br /><br />"

	'If there is a recordset returned by the query then read in the details
	Else
		'Set the page number to display records for
		rsCommon.AbsolutePage = intRecordPositionPageNum


		'Count the number of members there are in the database by returning the number of records in the recordset
		intTotalNumMembers = rsCommon.RecordCount

		'Count the number of pages there are in the database calculated by the PageSize attribute set above
		intTotalNumMembersPages = rsCommon.PageCount


		'Display the HTML for the total number of pages and total number of records in the database for the users
		Response.Write vbCrLf & "	<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" align=""center"">"
		Response.Write vbCrLf & " 	  <tr>"
		Response.Write vbCrLf & " 	    <td align=""center"" class=""bold"">"

		'If we are showing all the forum memebers then display how many members there are
		If Request.QueryString("SF") = "" Then
			Response.Write vbCrLf & "	      " & portal.variablesForum.strTxtThereAre & " " & intTotalNumMembers & " " & portal.variablesForum.strTxtForumMembersOn & " " & intTotalNumMembersPages & " " & portal.variablesForum.strTxtPageYouAerOnPage & " " & intRecordPositionPageNum
		'Else display how many results were fround from the search
		Else
			Response.Write vbCrLf & "	      " & portal.variablesForum.strTxtYourSearchMembersFound & " " & intTotalNumMembers & " " & portal.variablesForum.strTxtMatches
		End If

		Response.Write vbCrLf & "	    </td>"
		Response.Write vbCrLf & "	  </tr>"
		Response.Write vbCrLf & "	</table>"
		Response.Write vbCrLf & "	<br />"

%>  <form>
     <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="0" align="center" height="32">
      <tr>
       <td align="right" height="28" valign="top"><span class="text"><% = portal.variablesForum.strTxtSortResultsBy %></span>
        <select name="SelectSort" onChange="MembersSort(this)">
         <option value="UN" <% If intSortSelectField = 0 Then response.write("selected" %>><% = portal.variablesForum.strTxtusuarioAlphabetically %></option>
         <option value="PT" <% If intSortSelectField = 1 Then response.write("selected" %>><% = portal.variablesForum.strTxtPosts %></option>
         <option value="LU" <% If intSortSelectField = 2 Then response.write("selected" %>><% = portal.variablesForum.strTxtNewForumMembersFirst %></option>
         <option value="OU" <% If intSortSelectField = 3 Then response.write("selected" %>><% = portal.variablesForum.strTxtOldForumMembersFirst %></option>
         <option value="GP" <% If intSortSelectField = 4 Then response.write("selected" %>><% = portal.variablesForum.strTxtType %></option>
         <option value="SR" <% If intSortSelectField = 5 Then response.write("selected" %>><% = portal.variablesForum.strTxtNoOfStars %></option>
        </select>
       </td>
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
          <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" height="2" width="116" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><a href="http://www.webwizguide.info"></a><% = portal.variablesForum.strTxtusuario %></td>
          <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="113" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtType %></td>
          <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="116" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtRegistered %></td>
          <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="41" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>" align="center"><% = portal.variablesForum.strTxtPosts %></td>
          <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="59" align="center" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtHomepage %></td>
          <% If blnPrivateMessages = True Then %>
          <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="64" align="center" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtAddBuddy %></td>
          <% End If %>
          <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="57" height="2" align="center" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtSearch %></td>
         </tr><%

		'For....Next Loop to loop through the recorset to display the forum members
		For intRecordLoopCounter = 1 to 25

			'If there are no member's records left to display then exit loop
			If rsCommon.EOF Then Exit For

			'Initialise varibles
			dtmLastPostDate = ""

			'Read in the profile from the recordset
			lngUserID = CLng(rsCommon("UsuarioID"))
			strusuario = rsCommon("usuario")
			'strEmail = rsCommon("email")
			'blnShowEmail = CBool(rsCommon("Show_email"))
			strHomepage = rsCommon("Homepage")
			lngNumOfPosts = CLng(rsCommon("No_of_posts"))
			dtmRegisteredDate = CDate(rsCommon("FechaCreacion"))
			intMemberGroupID = CInt(rsCommon("Group_ID"))
			strMemberGroupName = rsCommon("Name")
			intRankStars = CInt(rsCommon("Stars"))
			strRankCustomStars = rsCommon("Custom_stars")
			

			'If the users account is not active make there account level guest
			If CBool(rsCommon("Active")) = False Then intMemberGroupID = 0

			'Write the HTML of the Topic descriptions as hyperlinks to the Topic details and message
			%>
         <tr>
          <td bgcolor="<% If (intRecordLoopCounter MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" width="116" height="24"><a href="JavaScript:openWin('pop_up_profile.aspx?PF=<% = lngUserID %>','profile','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=590,height=425')"><% = strusuario %></a></td>
          <td bgcolor="<% If (intRecordLoopCounter MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" width="113" height="24" class="smText"><% = strMemberGroupName %><br /><img src="<% If strRankCustomStars <> "" Then Response.Write(strRankCustomStars) Else Response.Write(portal.variablesForum.strImagePath & intRankStars & "_star_rating.gif") %>" alt="<% = strMemberGroupName %>"></td>
          <td bgcolor="<% If (intRecordLoopCounter MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" width="116" height="24" class="smText"><% = funcFecha.DateFormat(dtmRegisteredDate, funcFecha.saryDateTimeData) %></td>
          <td bgcolor="<% If (intRecordLoopCounter MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" width="41" align="center" height="24" class="text"><% = lngNumOfPosts %></td>
          <td bgcolor="<% If (intRecordLoopCounter MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" width="59" align="center" height="24" class="text"><% If NOT strHomepage = "" Then Response.Write("<a href=""" & strHomepage & """ target=""_blank""><img src=""" & portal.variablesForum.strImagePath & "home_icon.gif"" border=""0"" alt=""" & portal.variablesForum.strTxtVisit & " " & strusuario & "'s " & portal.variablesForum.strTxtHomepage & """></a>") %>&nbsp;</td>
          <% If blnPrivateMessages = True Then %>
          <td bgcolor="<% If (intRecordLoopCounter MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" width="64" align="center" height="24" class="text"><a href="pm_buddy_list.aspx?name=<% = Server.URLEncode(strusuario) %>" target="_self"><img src="<% = portal.variablesForum.strImagePath %>add_buddy_sm.gif" align="middle" border="0" alt="<% = portal.variablesForum.strTxtAddToBuddyList %>"></a></td>
          <% End If %>
          <td bgcolor="<% If (intRecordLoopCounter MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" width="57" align="center" height="24"><a href="search_form.aspx?KW=<% = Server.URLEncode(strusuario) %>&amp;sI=AR"><img src="<% = portal.variablesForum.strImagePath %>search_sm.gif" border="0" alt="<% = portal.variablesForum.strTxtSearchForPosts %>&nbsp;<% = strusuario %>" align="middle"></a></td>
         </tr><%

			'Move to the next record in the database
	   		rsCommon.MoveNext

		'Loop back round
		Next
	End If


%>
        </table>
      </tr>
     </table>
     </td>
    </tr>
    </table>
     <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="0" align="center" height="32">
      <tr><%

   'If there is more than 1 page of members then dispaly drop down list to the other members
	If intTotalNumMembersPages > 1 Then

		'Display an drop down list to the other members in list
		Response.Write (vbCrLf & "		<td align=""right"" class=""text"">")
		
		'Display a prev link if previous pages are available
		If intRecordPositionPageNum > 1 Then Response.Write("<a href=""membem_rs.aspx?SF=" & Server.URLEncode(Request.QueryString("SF")) & "&amp;gID=" & intGetGroupID & "&amp;sO=" & Trim(Mid(Request.QueryString("SO"),1,2)) & "&amp;memPN=" & intRecordPositionPageNum - 1 & """>&lt;&amp;lt&nbsp;" & portal.variablesForum.strTxtPrevious & "</a>&nbsp;")
		
		Response.Write (portal.variablesForum.strTxtPage & " " & _
		vbCrLf & "		 <select onChange=""MembersPage(this)"" name=""SelectPage"">")

		Dim intTopicPageLoopCounter

		'Loop round to display links to all the other pages
		For intTopicPageLoopCounter = 1 to intTotalNumMembersPages

			'Display a link in the link list to the another members page
			Response.Write (vbCrLf & "		  <option value=""" & intTopicPageLoopCounter & """")

			'If this page number to display is the same as the page being displayed then make sure it's selected
			If intTopicPageLoopCounter = intRecordPositionPageNum Then
				Response.Write (" selected")
			End If

			'Display the link page number
			Response.Write (">" & intTopicPageLoopCounter & "</option>")

		Next

		'End the drop down list
		Response.Write (vbCrLf & "		</select> " & portal.variablesForum.strTxtOf & " " & intTotalNumMembersPages)
		
		'Display a next link if needed
		If intRecordPositionPageNum <> intTotalNumMembersPages Then Response.Write("&nbsp;<a href=""membem_rs.aspx?SF=" & Server.URLEncode(Request.QueryString("SF")) & "&amp;gID=" & intGetGroupID & "&amp;sO=" & Trim(Mid(Request.QueryString("SO"),1,2)) & "&amp;memPN=" & intRecordPositionPageNum + 1 & """>" & portal.variablesForum.strTxtNext & "&nbsp;&gt;&gt;</a>")
		
		Response.Write("</td>")
	End If
%>	</tr>
     </table>
     <%
	'Reset Server Variables
	rsCommon.Close
End If

'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing


     
Response.Write(vbCrLf & "    </form>" & _
vbCrLf & "    <div align=""center"">")
 
 


'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"
%>
    </div>
   </div>
   
