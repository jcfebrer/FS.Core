<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pop_up_topic_admin.aspx.vb" Inherits="pop_up_topic_admin" %>

<%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %>

<common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting
'Response.Buffer = True


    'Dimension variables
    Dim rsSelectForum As Data.Common.DbDataReader     'Holds the db recordset
    Dim lngTopicID As Long      'Holds the topic ID number to return to
    Dim intForumID As Integer       'Holds the forum ID number
    Dim intNewForumID As Integer    'Holds the new forum ID if the topic is to be moved
    Dim strSubject As String        'Holds the topic subject
    Dim intTopicPriority As Integer 'Holds the priority of the topic
    Dim blnLockedStatus As Boolean  'Holds the lock status of the topic
    Dim strCatName As String        'Holds the name of the category
    Dim intCatID As Integer     'Holds the ID number of the category
    Dim strForumName As String  'Holds the name of the forum to jump to
    Dim lngFID As Long      'Holds the forum id to jump to
    Dim lngPollID As Long       'Holds the poll ID
    Dim lngMovedNum As Long     'Holds the moved number if topic has been moved
    Dim blnMoved As Boolean     'Set to true if moved icon is to be shown


    'Read in the topic ID number
    lngTopicID = CLng(func.Numero(Request("TID")))


'If the person is not an admin or a moderator then send them away
    If lngTopicID = 0 Then
        'Clean up
        'rsCommon = Nothing
        'adoCon.Close()
        'adoCon = Nothing
        
        'Redirect
        Response.Redirect("default.aspx")
    End If



'Initliase the SQL query to get the topic details from the database
    Dim strSQL As String = "SELECT " & portal.variablesForum.strDbTable & "Topic.* "
    strSQL = strSQL & "FROM " & portal.variablesForum.strDbTable & "Topic "
    strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Topic.Topic_ID = " & lngTopicID & ";"


    'Set the cursor	type property of the record set	to Dynamic so we can navigate through the record set
    'rsCommon.CursorType = 2

    'Set the Lock Type for the records so that the record set is only locked when it is updated
    'rsCommon.LockType = 3

    'Query the database
    Dim rsCommon As Data.Common.DbDataReader
    rsCommon = db.execute(strSQL)
    rsCommon.Read()
    
    'If there is a record returened read in the forum ID
    If rsCommon.HasRows Then
        portal.variablesForum.intForumID = CInt(rsCommon("Forum_ID"))
    End If


    'Call the moderator function and see if the user is a moderator
    If portal.variablesForum.blnAdmin = False Then portal.variablesForum.blnModerator = common.isModerator(portal.variablesForum.intForumID, portal.variablesForum.intGroupID)

    'If this is a post back then  update the database
    If (func.ValorBool(Request.Form("postBack"))) And (portal.variablesForum.blnAdmin Or portal.variablesForum.blnModerator) Then

        strSubject = Trim(Mid(Request.Form("subject"), 1, 41))
        intTopicPriority = CInt(Request.Form("priority"))
        blnLockedStatus = CBool(Request.Form("locked"))
        intNewForumID = CInt(Request.Form("forum"))
        blnMoved = CBool(Request.Form("moveIco"))

        'Get rid of scripting tags in the subject
        strSubject = funcForum.removeAllTags(strSubject)
        strSubject = func.formatInput(strSubject)

        'Update the recordset
        With rsCommon

            'Update	the recorset
            If intNewForumID <> 0 Then .Fields("Forum_ID") = intNewForumID
            If blnMoved = False Then
                .Fields("Moved_ID") = 0
            ElseIf intNewForumID <> 0 And intNewForumID <> portal.variablesForum.intForumID And blnMoved Then
                .Fields("Moved_ID") = portal.variablesForum.intForumID
            End If
            .Fields("Subject") = strSubject
            .Fields("Priority") = intTopicPriority
            .Fields("Locked") = blnLockedStatus

            'Update db
            .Update()

            'Re-run the query as access needs time to catch up
            .ReQuery()
		
            'Update topic and post count
            updateTopicPostCount(portal.variablesForum.intForumID)

        End With

    End If

%>
<html>
<head>

<title>Topic Admin</title>

<script language="javascript">

//Function to check form is filled in correctly before submitting
function CheckForm () {

	var errorMsg = "";

	//Check for a subject
	if (document.frmTopicAdmin.subject.value==""){
		errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorTopicSubject %>";
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

<!--#include file="includes/skin_file.aspx" -->

</head>
<body bgcolor="<% = strBgColour %>" text="<% = strTextColour %>" background="<% = strBgImage %>" marginheight="0" marginwidth="0" topmargin="0" leftmargin="0" OnLoad="self.focus();">
<div align="center" class="heading"><% = portal.variablesForum.strTxtTopicAdmin %></div>
    <br /><%

'If there is no topic info returned by the rs then display an error message
If rsCommon.EOF OR (portal.variablesForum.blnAdmin = False AND portal.variablesForum.blnModerator = False) OR bannedIP() OR blnActiveMember = False Then

	'Close the rs
	rsCommon.Close

	Response.Write("<div align=""center"">")
	Response.Write("<span class=""lgText"">" & portal.variablesForum.strTxtTopicNotFoundOrAccessDenied & "</span><br /><br /><br />")
	Response.Write("</div>")

'Else display a form to allow updating of the topic
Else

	'Read in the topic details
	portal.variablesForum.intForumID = CInt(rsCommon("Forum_ID"))
	strSubject = rsCommon("Subject")
	intTopicPriority = CInt(rsCommon("Priority"))
	blnLockedStatus = CBool(rsCommon("Locked"))
	lngPollID = CLng(rsCommon("Poll_ID"))
	lngMovedNum = CLng(rsCommon("Moved_ID"))


	'Close the rs
	rsCommon.Close
%>
<form name="frmTopicAdmin" method="post" action="pop_up_topic_admin.aspx" onSubmit="return CheckForm();" onReset="return confirm('<% = strResetFormConfirm %>');">
 <table width="450" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr>
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableBgColour %>">
   <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
   <td>
    <table border="0" align="center" cellpadding="4" cellspacing="1" width="450">
     <tr align="left" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableTitleBgImage %>">
      <td colspan="2" bgcolor="<% = portal.variablesForum.strTableTitleColour %>" background="<% = portal.variablesForum.strTableTitleBgImage %>" class="text">*<% = portal.variablesForum.strTxtRequiredFields %></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
      <td align="right" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"><% = portal.variablesForum.strTxtSubjectFolder %>*:</td>
      <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>"><input type='text' name="subject" size="30" maxlength="41" value="<% = strSubject %>"/></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
      <td align="right" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"><% = portal.variablesForum.strTxtPriority %>:</td>
      <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>"><select name="priority">
           <option value="0"<% If intTopicPriority = 0 Then Response.Write(" selected") %>><% = portal.variablesForum.strTxtNormal %></option>
           <option value="1"<% If intTopicPriority = 1 Then Response.Write(" selected") %>><% = portal.variablesForum.strTxtPinnedTopic %></option>
           <option value="2"<% If intTopicPriority = 2 Then Response.Write(" selected") %>><% = portal.variablesForum.strTopThisForum %></option><%

         	'If this is the forum admin let them post a priority post to all forums
         	If portal.variablesForum.blnAdmin = True Then %>

           <option value="3"<% If intTopicPriority = 3 Then Response.Write(" selected") %>><% = portal.variablesForum.strTxtTopAllForums %></option><%

		End If
        	%>
          </select></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
      <td align="right" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"><% = portal.variablesForum.strTxtLockedTopic %>:</td>
      <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>"><input type='checkbox' name="locked" value="true" <% If blnLockedStatus = True Then Response.Write(" checked") %> /></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
      <td align="right" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text" valign="top"><% = portal.variablesForum.strTxtMoveTopic %>:</td>
      <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"><select name="forum"><%

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
	Response.Write vbCrLf & "		<option value=""0"">" & strCatName & "</option>"

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
Set rsSelectForum = Nothing
%>
          </select><br />
          <input type='checkbox' name="moveIco" value="true" checked /><% = portal.variablesForum.strTxtShowMovedIconInLastForum %></td>
     </tr><%

'If there is a poll then let the admin moderator edit or delete the poll
If lngPollID <> 0 Then

	%>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
      <td align="right" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"><% = portal.variablesForum.strTxtPoll %></td>
      <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>"><a href="delete_poll.aspx?TID=<% = lngTopicID %>" onClick="return confirm('<% = portal.variablesForum.strTxtAreYouSureYouWantToDeleteThisPoll %>');"><% = portal.variablesForum.strTxtDeletePoll %></a></td>
     </tr><%
End If

%>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" align="center" />
      <td valign="top" colspan="2" background="<% = portal.variablesForum.strTableBgImage %>" />
        <input type="hidden" name="TID" value="<% = lngTopicID %>" />
        <input type="hidden" name="postBack" value="true" />
        <input type='submit' name="Submit" value="<% = portal.variablesForum.strTxtUpdateTopic %>" />
        <input type="reset" name="Reset" value="<% = portal.variablesForum.strTxtResetForm %>" />
      </td>
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
</form>
<form name="frmDeleteTopic" method="post" action="delete_topic.aspx" OnSubmit="return confirm('<% = portal.variablesForum.strTxtDeleteTopicAlert %>')">
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
    <tr>
      <td align="center">
        <input type="hidden" name="TID" value="<% = lngTopicID %>" />
        <input type='submit' name="Submit" value="<% = portal.variablesForum.strTxtDeleteTopic %>" />
      </td>
    </tr>
  </table>
   </form><br /><%

End If

%>
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
    <tr>
      <td align="center">
        <a href="JavaScript:onClick=window.opener.location.href = window.opener.location.href; window.close();"><% = portal.variablesForum.strTxtCloseWindow %></a>
      </td>
    </tr>
  </table>
<br /><br />
<div align="center">
<%

'Clean up
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing


%>
</div>
</body>
</html>
