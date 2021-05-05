

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True

'Dimension variables
Dim strPofileusuario		'Holds the users usuario
Dim lngUserID			'Holds the new users ID number
Dim lngNumOfPosts		'Holds the number of posts the user has made
Dim strGroupName		'Holds the group name
Dim dtmRegisteredDate		'Holds the date the usre registered
Dim intTotalNumMembersPages	'Holds the total number of pages
Dim intTotalNumMembers		'Holds the total number of forum members
Dim intRecordPositionPageNum	'Holds the page number we are on
Dim intRecordLoopCounter	'Recordset loop counter
Dim dtmLastVisit		'Holds the date of the users las post
Dim intLinkPageNum		'Holds the page number to link to
Dim strSearchCriteria		'Holds the search critiria
Dim strSortBy			'Holds the way the records are sorted
Dim intSortSelectField		'Holds the sort selection to be shown in the sort list box

'Initalise variables
strSearchCriteria = "%"


'If this is the first time the page is displayed then the members record position is set to page 1
If Request.QueryString("MemPN") = "" Then
	intRecordPositionPageNum = 1

'Else the page has been displayed before so the members page record postion is set to the Record Position number
Else
	intRecordPositionPageNum = CInt(Request.QueryString("MemPN"))
End If


'Get the search critiria for the members to display
If NOT Request.QueryString("find") = "" Then
	strSearchCriteria = Request.QueryString("find") & "%"
End If

'Get rid of milisous code
strSearchCriteria = formatSQLInput(strSearchCriteria)

'Get the sort critiria
Select Case Request.QueryString("Sort")
	Case "post"
		strSortBy = "No_of_posts DESC"
		intSortSelectField = 1
	Case "latestUsers"
		strSortBy = "FechaCreacion DESC"
		intSortSelectField = 2
	Case "oldestUsers"
		strSortBy = "FechaCreacion ASC"
		intSortSelectField = 3
	Case "location"
		strSortBy = "Location ASC"
		intSortSelectField = 4
	Case Else
		strSortBy = "usuario ASC"
		intSortSelectField = 0
End Select


%>
<html>
<head>
<title>Forum Member Adminstration</title>



     	

<script  language="javascript">
<!-- Hide from older browsem_rs...

//Function to check form is filled in correctly before submitting
function CheckForm () {

	//Check for a somthing to search for
	if (document.frmMemberSearch.find.value==""){
		alert("Please enter a member to Search for");
		return false;
	}

	return true;
}

//Function to choose how the members list is sorted
function MembersSort(SelectSort){

   	if (SelectSort != "") self.location.href = "select_membem_rs.aspx?find=<% = Server.URLEncode(Request.QueryString("find")) %>&amp;sort=" + SelectSort.options[SelectSort.selectedIndex].value;
	return true;
}

//Function to move to another page of members
function MembersPage(SelectPage){

   	if (SelectPage != -1) self.location.href = "select_membem_rs.aspx?find=<% = Request.QueryString("find") %>&amp;sort=<% = Request.QueryString("sort") %>&amp;memPN=" + SelectPage.options[SelectPage.selectedIndex].value;
	return true;
}

// -->
</script>

<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"> 
 <p class="text"><span class="heading">Forum Member Administration</span><br />
  <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
  <br />
  Click on the members name to administer their forum membership account, <br />
  from where you can, change their details, member group, reset clave, suspend, delete, etc. from the Forum.<br />
 </p>
 <form name="frmMemberSearch" method="get" action="select_membem_rs.aspx" onSubmit="return CheckForm();">
  <table width="490" border="0" cellspacing="0" cellpadding="1" height="24" align="center" bgcolor="#000000">
   <tr>
    <td align="center" height="2"> <table width="100%" border="0" cellspacing="0" cellpadding="4" bgcolor="#FFFFFF">
      <tr>
       <td width="100%" height="20" align="center" bgcolor="#F5F5FA" class="text"><span class="text">Search:</span> 
        <input type='text' name="find" size="15" maxlength="15"> <input type='submit' name="Submit" value="Search">
       </td>
      </tr>
      <tr>
       <td align="center" bgcolor="#F5F5FA" width="100%"><a href="select_membem_rs.aspx" target="_self">All</a> <a href="select_membem_rs.aspx?find=A" target="_self">A</a> <a href="select_membem_rs.aspx?find=B" target="_self">B</a> 
        <a href="select_membem_rs.aspx?find=C" target="_self">C</a> <a href="select_membem_rs.aspx?find=D" target="_self">D</a> <a href="select_membem_rs.aspx?find=E" target="_self">E</a> <a href="select_membem_rs.aspx?find=F" target="_self">F</a> 
        <a href="select_membem_rs.aspx?find=G" target="_self">G</a> <a href="select_membem_rs.aspx?find=H" target="_self">H</a> <a href="select_membem_rs.aspx?find=I" target="_self">I</a> <a href="select_membem_rs.aspx?find=J" target="_self">J</a> 
        <a href="select_membem_rs.aspx?find=K" target="_self">K</a> <a href="select_membem_rs.aspx?find=L" target="_self">L</a> <a href="select_membem_rs.aspx?find=M" target="_self">M</a> <a href="select_membem_rs.aspx?find=N" target="_self">N</a> 
        <a href="select_membem_rs.aspx?find=O" target="_self">O</a> <a href="select_membem_rs.aspx?find=P" target="_self">P</a> <a href="select_membem_rs.aspx?find=Q" target="_self">Q</a> <a href="select_membem_rs.aspx?find=R" target="_self">R</a> 
        <a href="select_membem_rs.aspx?find=S" target="_self">S</a> <a href="select_membem_rs.aspx?find=T" target="_self">T</a> <a href="select_membem_rs.aspx?find=U" target="_self">U</a> <a href="select_membem_rs.aspx?find=V" target="_self">V</a> 
        <a href="select_membem_rs.aspx?find=W" target="_self">W</a> <a href="select_membem_rs.aspx?find=X" target="_self">X</a> <a href="select_membem_rs.aspx?find=Y" target="_self">Y</a> <a href="select_membem_rs.aspx?find=Z" target="_self">Z</a></td>
      </tr>
     </table></td>
   </tr>
  </table>
 </form>
</div>
<div align="center"><%


'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & "Usuarios.*, " & portal.variablesForum.strDbTable & "Group.Name "
strSQL = strSQL & "FROM " & "Usuarios, " & portal.variablesForum.strDbTable & "Group "
strSQL = strSQL & "WHERE " & "Usuarios.Group_ID = " & portal.variablesForum.strDbTable & "Group.Group_ID AND " & "Usuarios.usuario Like '" & strSearchCriteria & "' "
strSQL = strSQL & "ORDER BY " & "Usuarios." & strSortBy & ";"

'Set the cursor type property of the record set to dynamic so we can naviagate through the record set
rsCommon.CursorType = 1

'Query the database
rsCommon=db.execute(strSQL)

'Set the number of records to display on each page
rsCommon.PageSize = 33


'If there are no memebers to display then show an error message
If rsCommon.EOF Then
	response.write("<span class=""text"">Sorry, your search found no forum members that match your criteria</span>"

'If there is a recorset returned by the query then read in the details
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
	Response.Write vbCrLf & " 	    <td align=""center""><span class=""text"">"

	'If we are showing all the forum memebers then display how many members there are
	If Request.QueryString("find") = "" Then
		Response.Write vbCrLf & "	      Thre are " & intTotalNumMembers & " forum members on " & intTotalNumMembersPages & " pages and you are on page number " & intRecordPositionPageNum
	'Else display how many results were fround from the search
	Else
		Response.Write vbCrLf & "	      Your search of the forum members found " & intTotalNumMembers & " matches"
	End If

	Response.Write vbCrLf & "	    </span></td>"
	Response.Write vbCrLf & "	  </tr>"
	Response.Write vbCrLf & "	</table>"
	Response.Write vbCrLf & "	<br />"
%>
 <form>
  <table width="98%" border="0" cellspacing="0" cellpadding="0" align="center" height="32">
   <tr>
    <td align="right" height="28" valign="top"><span class="text">Sort Results By</span> <select name="SelectSort" onChange="MembersSort(this)">
      <option value="usuario" <% If intSortSelectField = 0 Then response.write("selected" %>>usuario Alphabetically</option>
      <option value="post" <% If intSortSelectField = 1 Then response.write("selected" %>>No of Posts</option>
      <option value="latestUsers" <% If intSortSelectField = 2 Then response.write("selected" %>>New Forum Members First</option>
      <option value="oldestUsers" <% If intSortSelectField = 3 Then response.write("selected" %>>Old Forum Members First</option>
      <option value="location" <% If intSortSelectField = 4 Then response.write("selected" %>>Location Alphabetically</option>
     </select> </td>
   </tr>
  </table>
  <table width="98%" border="0" cellspacing="0" cellpadding="0" bgcolor="#000000" align="center">
   <tr>
    <td width="983" height="46"> <table border="0" cellspacing="1" cellpadding="3" bgcolor="#000000" width="100%">
      <tr bgcolor="#CCCEE6" class="tHeading">
       <td width="26%" height="2"><b>usuario</b></td>
       <td width="16%" height="2"><b>Group</b></td>
       <td width="10%" height="2" align="center"><b>Posts</b></td>
       <td width="23%" height="2" align="left"><b>Last Visit</b></td>
       <td width="25%" height="2" align="left"><b>Registered</b></td>
      </tr><%

	'For....Next Loop to loop through the recorset to display the forum members
	For intRecordLoopCounter = 1 to 33

		'If there are no member's records left to display then exit loop
		If rsCommon.EOF Then Exit For

		'Initialise varibles
		dtmLastVisit = ""

		'Read in the profile from the recordset
		lngUserID = CLng(rsCommon("UsuarioID"))
		strPofileusuario = rsCommon("usuario")
		lngNumOfPosts = CLng(rsCommon("No_of_posts"))
		dtmRegisteredDate = CDate(rsCommon("FechaCreacion"))
		If NOT isNull(rsCommon("UltimaConexion")) Then dtmLastVisit = CDate(rsCommon("UltimaConexion"))
		strGroupName = rsCommon("Name")


		'Write the HTML of the Topic descriptions as hyperlinks to the Topic details and message
		%>
      <tr bgcolor="#F5F5FA"> 
       <td width="26%" height="30" class="text"><a href="../register.aspx?PF=<% = lngUserID %>&amp;m=A" target="_top"> 
        <% = strPofileusuario %>
        </a></td>
       <td width="16%" height="30" class="text"> 
        <% = strGroupName %>
       </td>
       <td width="10%" height="30" align="center" class="text"> 
        <% = lngNumOfPosts %>
       </td>
       <td width="23%" height="30" align="left" class="text"> 
        <%

                	If dtmLastVisit = "" Then
             			response.write("&nbsp;"
             	 	Else
             	 		Response.Write FormatDateTime(dtmLastVisit, VbLongDate)
             	 	End If
             	 %>
       </td>
       <td width="25%" height="30" align="left" class="text"> 
        <% = FormatDateTime(dtmRegisteredDate, VbLongDate) %>
       </td>
      </tr><%

		'Move to the next record in the database
   		rsCommon.MoveNext

	'Loop back round
	Next
End If


%>
     </table></tr>
  </table>
  <table width="98%" border="0" cellspacing="0" cellpadding="0" align="center" height="32">
   <tr><%

   'If there is more than 1 page of members then dispaly drop down list to the other members
	If intTotalNumMembersPages > 1 Then

		'Display an drop down list to the other members in list
		Response.Write vbCrLf & "		<td align=""right"" height=""28"" valign=""bottom""><span class=""text"">Page"
		Response.Write vbCrLf & "		 <select onChange=""MembersPage(this)"" name=""SelectPage"">"

		Dim intTopicPageLoopCounter

		'Loop round to display links to all the other pages
		For intTopicPageLoopCounter = 1 to intTotalNumMembersPages

			'Display a link in the link list to the another members page
			Response.Write vbCrLf & "		  <option value=""" & intTopicPageLoopCounter & """"

			'If this page number to display is the same as the page being displayed then make sure it's selected
			If intTopicPageLoopCounter = intRecordPositionPageNum Then
				response.write(" selected"
			End If

			'Display the link page number
			response.write(">" & intTopicPageLoopCounter & "</option>"

		Next

		'End the drop down list
		Response.Write vbCrLf & "		</select> of " & intTotalNumMembersPages & "</span></td>"
End If
%>
   </tr>
  </table><%

'Reset Server Variables
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
  <br />
 </form><br />
</div>
<br />
</body>
</html>