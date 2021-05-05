

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
Session.Timeout =  1000

'Set the response buffer to true as we maybe redirecting
Response.Buffer = True 

'Declare veraibles
Dim intNoOfDays
Dim intFromForumID
Dim intToForumID
Dim intPriority


'get teh number of days to delte from
intNoOfDays = CInt(Request.Form("days"))
intFromForumID = CInt(Request.Form("FFID"))
intToForumID = CInt(Request.Form("TFID"))
intPriority = CInt(Request.Form("priority"))
		
'Initalise the strSQL variable with an SQL statement to get the topic from the database
strSQL = "UPDATE " & portal.variablesForum.strDbTable & "Topic SET " & portal.variablesForum.strDbTable & "Topic.Forum_ID=" & intToForumID & " "
If intFromForumID = 0 Then
	strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Topic.Last_entry_date < " & strDatabaseDateFunction & " - " & intNoOfDays
Else
	strSQL = strSQL & "WHERE (" & portal.variablesForum.strDbTable & "Topic.Last_entry_date < " & strDatabaseDateFunction & " - " & intNoOfDays  & ") AND (" & portal.variablesForum.strDbTable & "Topic.Forum_ID=" & intFromForumID & ")"
End If
If intPriority <> 4 Then strSQL = strSQL & " AND (" & portal.variablesForum.strDbTable & "Topic.Priority=" & intPriority & ")"
strSQL = strSQL & ";"


'Delete the topics
db.execute(strSQL)	



'Update post count
updateTopicPostCount(intFromForumID)
updateTopicPostCount(intToForumID)
	
'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

%>
<html>
<head>

<title>Batch Move Forum Topics</title>

<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"> 
 <p class="text"><span class="heading">Close Forum Topics</span><br />
  <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
  <br />
  <br />
  <br />
  <br />
  <br />
  <span class="bold">Topics have been Moved</span><br />
  <br />
  <br />
  <br />
  <br />
  <a href="resync_forum_post_count.aspx">Click here to re-sync Post and Topic Counts for the Forums</a></p>
</div>
</body>
</html>
