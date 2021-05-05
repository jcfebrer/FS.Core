

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include file="language_files/admin_language_file_inc.aspx" -->
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True


'Dimension variables
Dim strTopicSubject		'Holds the name of the topic
Dim lngTopicID			'Holds the ID number of the category
Dim lngMoveToForumID		'Holds the forum id to jump to
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
lngPostID = CLng(Request.Form("PID"))

'Read in the forum ID that the user wants to move the post to
lngMoveToForumID = CLng(Request.Form("forum"))


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
	adoCon.Close
	Set adoCon = Nothing

	'Redirect
	Response.Redirect("default.aspx")
End If
%>
<html>
<head>

<title>Discussion Forum Move Post</title>

<!--#include file="includes/skin_file.aspx" -->

</head>
<body bgcolor="<% = strBgColour %>" text="<% = strTextColour %>" background="<% = strBgImage %>" marginheight="0" marginwidth="0" topmargin="0" leftmargin="0" OnLoad="self.focus();">
<div align="center" class="heading"><% = portal.variablesForum.strTxtMovePost %></div>
<div align="center" class="text"><br /><% = portal.variablesForum.strTxtSelectTopicToMovePostTo %></div>
   <form method="post" name="frmMovePost" action="move_post.aspx">
    <table width="400" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" height="8">
     <tr>
      <td height="24" width="680">
       <table width="100%" border="0" align="center"  height="8" cellpadding="4" cellspacing="1">
        <tr >
         <td align="left" width="57%" height="2"  bgcolor="<% = portal.variablesForum.strTableTitleColour %>" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtSelectTheTopicYouWouldLikeThisPostToBeIn %></td>
        </tr>
        <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td align="left" width="57%"  height="12" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
          <select name="topicSelect"><%


'Read in the category name from the database
'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT TOP 400 " & portal.variablesForum.strDbTable & "Topic.Topic_ID, " & portal.variablesForum.strDbTable & "Topic.Subject FROM " & portal.variablesForum.strDbTable & "Topic WHERE " & portal.variablesForum.strDbTable & "Topic.Forum_ID = " & lngMoveToForumID & " ORDER BY " & portal.variablesForum.strDbTable & "Topic.Last_entry_date DESC;"

'Query the database
rsCommon=db.execute(strSQL)

'Loop through all the categories in the database
Do while NOT rsCommon.EOF

	'Read in the deatils for the category
	strTopicSubject = rsCommon("Subject")
	lngTopicID = Cint(rsCommon("Topic_ID"))

	'Display a link in the link list to the forum
	Response.Write vbCrLf & "		<option value=""" & lngTopicID & """>" & strTopicSubject & "</option>"

	'Move to the next record in the recordset
	rsCommon.MoveNext
Loop

'Reset Server Objects
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
          </select>
          <input type="hidden" name="PID" value="<% = lngPostID %>">
          <input type="hidden" name="toFID" value="<% = lngMoveToForumID %>">
         </td>
        </tr>
       </table>
      </td>
    </tr>
  </table>
    <br />
    <table width="400" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" height="8">
     <tr>
      <td height="24" width="680">
       <table width="100%" border="0" align="center"  height="8" cellpadding="4" cellspacing="1">
        <tr >
         <td align="left" width="57%" height="2"  bgcolor="<% = portal.variablesForum.strTableTitleColour %>" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtOrTypeTheSubjectOfANewTopic %></td>
        </tr>
        <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td align="left" width="57%"  height="12" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
          <input type='text' name="subject" size="30" maxlength="41">
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
       <input type='submit' name="Submit" value="<% = portal.variablesForum.strTxtMovePost %>">
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