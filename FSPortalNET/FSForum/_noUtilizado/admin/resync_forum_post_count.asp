

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true
Response.Buffer = True 


	
'Initalise the strSQL variable with an SQL statement to get the all the forum id's
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.Forum_ID FROM " & portal.variablesForum.strDbTable & "Forum;"
	
'Query the database
rsCommon=db.execute(strSQL)

'Loop through all the forums to re-sync them
Do While NOT rsCommon.Eof

	'Update topic and post count
	updateTopicPostCount(CInt(rsCommon("Forum_ID")))
	
	'Move to the next record
	rsCommon.MoveNext
Loop


'Reset server objects
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>  
<html>
<head>
<title>Re-sync the Topic and Post Count</title>



     	
     	
<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"> 
 <p class="text"><span class="heading">Re-sync the Topic and Post Count</span><br />
  <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
 </p>
 <br />
 <span class="lgText">The Topic and Post Count for all Forums have been re-synchronised.</span></div>
</body>
</html>