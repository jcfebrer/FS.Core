

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include file="language_files/admin_language_file_inc.aspx" -->
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True


'Dimension variables
Dim rsSelectForum		'Holds the recordset for the forum
Dim strCatName			'Holds the name of the category
Dim intCatID			'Holds the ID number of the category
Dim strForumName		'Holds the name of the forum to jump to
Dim lngFID			'Holds the forum id to jump to
Dim intForumID			'Holds the forum ID
Dim lngPostID			'Holds the post ID


'If the user is user is using a banned IP redirect to an error page
If bannedIP() Then
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

	'Redirect
	Response.Redirect("insufficient_permission.aspx?M=IP")
End If



'Read in the post ID
lngPostID = CLng(Request.QueryString("PID"))



'Query the datbase to get the forum ID for this post
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Topic.Forum_ID "
strSQL = strSQL & "FROM " & portal.variablesForum.strDbTable & "Topic, " & portal.variablesForum.strDbTable & "Thread "
strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Topic.Topic_ID = " & portal.variablesForum.strDbTable & "Thread.Topic_ID AND " & portal.variablesForum.strDbTable & "Thread.Thread_ID = " & lngPostID & ";"

'Query the database
rsCommon=db.execute(strSQL)


'If there is a record returened read in the forum ID
If NOT rsCommon.EOF Then
	portal.variablesForum.intForumID = CInt(rsCommon("Forum_ID"))
End If

'Clean up
rsCommon.Close


'Call the moderator function and see if the user is a moderator
If portal.variablesForum.blnAdmin = False Then portal.variablesForum.blnModerator = isModerator(portal.variablesForum.intForumID, portal.variablesForum.intGroupID)


'If the person is not a moderator or admin then send them away
If portal.variablesForum.blnAdmin = False AND portal.variablesForum.blnModerator = False Then
	'Reset Server Objects
	Set rsCommon = Nothing
	Set adoCon = Nothing
	Set adoCon = Nothing

	'Redirect
	Response.Redirect("default.aspx")
End If
%>
<html>
<head>

<title>Discussion Forum Move Post</title>

<!-- Check the from is filled in correctly before submitting -->
<script language="javascript">

//Function to check form is filled in correctly before submitting
function CheckForm () {

	//Check for a forum to move Post to
	if (document.frmMovePost.forum.value==""){

		msg = "<% = portal.variablesForum.strTxtErrorDisplayLine %>\n\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine1 %>\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine2 %>\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine %>\n\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine3 %>\n";

		alert(msg + "\n\t<% = portal.variablesForum.strTxtMovePostErrorMsg %>\n\n");
		return false;
	}

	return true
}
</script>
<!--#include file="includes/skin_file.aspx" -->

</head>
<body bgcolor="<% = strBgColour %>" text="<% = strTextColour %>" background="<% = strBgImage %>" marginheight="0" marginwidth="0" topmargin="0" leftmargin="0" OnLoad="self.focus();">
<div align="center" class="heading"><% = portal.variablesForum.strTxtMovePost %></div>
<div align="center" class="text"><br /><% = portal.variablesForum.strTxtSelectForumClickNext %></div>
   <form method="post" name="frmMovePost" action="move_post_form_to.aspx" onSubmit="return CheckForm();">
    <table width="400" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" height="8">
     <tr>
      <td height="24" width="680">
       <table width="100%" border="0" align="center"  height="8" cellpadding="4" cellspacing="1">
        <tr >
         <td align="left" width="57%" height="2"  bgcolor="<% = portal.variablesForum.strTableTitleColour %>" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtSelectTheForumYouWouldLikePostIn %></td>
        </tr>
        <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td align="left" width="57%"  height="12" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
          <select name="forum"><%


'Create a recordset to hold the forum name and id number
Set rsSelectForum = Server.CreateObject("ADODB.Recordset")


'Read in the category name from the database
'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Category.Cat_name, " & portal.variablesForum.strDbTable & "Category.Cat_ID FROM " & portal.variablesForum.strDbTable & "Category ORDER BY " & portal.variablesForum.strDbTable & "Category.Cat_order ASC;"

'Query the database
rsCommon=db.execute(strSQL)

'Loop through all the categories in the database
Do while NOT rsCommon.EOF

	'Read in the deatils for the category
	strCatName = rsCommon("Cat_name")
	intCatID = Cint(rsCommon("Cat_ID"))

	'Display a link in the link list to the forum
	Response.Write vbCrLf & "		<option value="""">" & strCatName & "</option>"

	'Read in the forum name from the database
	'Initalise the strSQL variable with an SQL statement to query the database
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.Forum_name, " & portal.variablesForum.strDbTable & "Forum.Forum_ID FROM " & portal.variablesForum.strDbTable & "Forum WHERE " & portal.variablesForum.strDbTable & "Forum.Cat_ID = " & intCatID & " ORDER BY " & portal.variablesForum.strDbTable & "Forum.Forum_Order ASC;"

	'Query the database
	rsSelectForum=db.execute(strSQL)

	'Loop through all the froum in the database
	Do while NOT rsSelectForum.EOF

		'Read in the forum details from the recordset
		strForumName = rsSelectForum("Forum_name")
		lngFID = CLng(rsSelectForum("Forum_ID"))


		'Display a link in the link list to the forum
		Response.Write vbCrLf & "		<option value=""" & lngFID & """"
		If CInt(Request.QueryString("FID")) = lngFID OR portal.variablesForum.intForumID = lngFID Then response.write(" selected"
		response.write(">&nbsp;&nbsp;-&nbsp;" & strForumName & "</option>"


		'Move to the next record in the recordset
		rsSelectForum.MoveNext
	Loop

	'Close the forum recordset so another can be opened
	rsSelectForum.Close

	'Move to the next record in the recordset
	rsCommon.MoveNext
Loop

'Reset Server Objects
rsCommon.Close
Set rsCommon = Nothing
Set rsSelectForum = Nothing
adoCon.Close
Set adoCon = Nothing
%>
          </select>
          <input type="hidden" name="PID" value="<% = lngPostID %>">
         </td>
        </tr>
       </table>
      </td>
    </tr>
  </table>
  <br />
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
    <tr>
      <td align="center">
       <input type='submit' name="Submit" value="<% = portal.variablesForum.strTxtNext %> &gt;&gt;">
      </td>
    </tr>
  </table>
</form>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
    <tr>
      <td align="center">
        <a href="JavaScript:window.close();"><% = portal.variablesForum.strTxtCloseWindow %></a>
      </td>
    </tr>
  </table>
  <br />
<div align="center">
<%

%>
</div>
</body>
</html>