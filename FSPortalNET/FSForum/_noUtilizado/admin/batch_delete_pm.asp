
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
Session.Timeout = 90

'Set the response buffer to true as we maybe redirecting
Response.Buffer = True

'Dimension variables
Dim intNoOfDays			'Holds the number of days to delete posts from


'get teh number of days to delte from
intNoOfDays = CInt(Request.Form("days"))



'Initalise the strSQL variable with an SQL statement to delete the private messages from the database
strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "PMMessage "
strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "PMMessage.PM_Message_Date < " & strDatabaseDateFunction & " - " & intNoOfDays  & ";"

'Delete the topics
db.execute(strSQL)

'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

%>
<html>
<head>

<title>Batch Delete Private Messages</title>

<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"> 
 <p class="text"><span class="heading">Batch Delete Private Messages</span><br />
  <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
  <br />
  <br />
  <br />
  <br />
  <br />
  Private Messages have been Deleted.<br />
 </p>
</div>
</body>
</html>
