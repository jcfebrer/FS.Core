

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
Session.Timeout =  1000

'Set the response buffer to true as we maybe redirecting
Response.Buffer = True 

'Declare veraibles
Dim intNoOfDays
Dim intForumID
Dim blnClose
Dim intPriority	


'get teh number of days to delte from
intNoOfDays = CInt(Request.Form("days"))
portal.variablesForum.intForumID = CInt(Request.Form("FID"))
blnClose = CBool(Request.Form("closeTopic"))
intPriority = CInt(Request.Form("priority"))


		
'Initalise the strSQL variable with an SQL statement to get the topic from the database
If portal.variablesForum.strDatabaseType = "SQLServer" AND blnClose Then
	strSQL = "UPDATE " & portal.variablesForum.strDbTable & "Topic SET " & portal.variablesForum.strDbTable & "Topic.Locked=1 "
ElseIf portal.variablesForum.strDatabaseType = "Access" AND blnClose Then
	strSQL = "UPDATE " & portal.variablesForum.strDbTable & "Topic SET " & portal.variablesForum.strDbTable & "Topic.Locked=True "

ElseIf portal.variablesForum.strDatabaseType = "SQLServer" AND blnClose = false Then
	strSQL = "UPDATE " & portal.variablesForum.strDbTable & "Topic SET " & portal.variablesForum.strDbTable & "Topic.Locked=0 "
ElseIf portal.variablesForum.strDatabaseType = "Access" AND blnClose = false Then	
	strSQL = "UPDATE " & portal.variablesForum.strDbTable & "Topic SET " & portal.variablesForum.strDbTable & "Topic.Locked=False "
End If

If portal.variablesForum.intForumID = 0 Then
	strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Topic.Last_entry_date < " & strDatabaseDateFunction & " - " & intNoOfDays  & " "
Else
	strSQL = strSQL & "WHERE (" & portal.variablesForum.strDbTable & "Topic.Last_entry_date < " & strDatabaseDateFunction & " - " & intNoOfDays  & ") AND (" & portal.variablesForum.strDbTable & "Topic.Forum_ID=" & portal.variablesForum.intForumID & ") "
End If

If intPriority <> 4 Then strSQL = strSQL & " AND (" & portal.variablesForum.strDbTable & "Topic.Priority=" & intPriority & ")"
strSQL = strSQL & ";"



'Delete the topics
db.execute(strSQL)	
	
'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

%>
<html>
<head>

<title>Close Forum Topics</title>

<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center">
 <p class="text"><span class="heading">Close Forum Topics</span><br />
  <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
  <br />
  Topics have been Closed<br />
 </p>
</div>
</body>
</html>
