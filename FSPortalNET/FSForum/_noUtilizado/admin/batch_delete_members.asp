
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the timeout higher so that it doesn't timeout half way through
Session.Timeout =  1000

'Set the response buffer to true as we maybe redirecting
Response.Buffer = True

'Dimension variables
Dim lngDelUsuariosID		'Holds the Usuarioss ID to be deleted
Dim intNoOfDays			'Holds the number of days to delete posts from
Dim lngNumberOfMembers		'Holds the number of members that are deleted
Dim rsThread			'Holds the threads recordset
Dim blnUnActive			'Set to true if deleting non active accounts only

'Initilise variables
lngNumberOfMembers = 0

'get teh number of days to delte from
intNoOfDays = CInt(Request.Form("days"))
blnUnActive = CBool(Request.Form("unactive"))



'Get all the Topics from the database to be deleted

'Initalise the strSQL variable with an SQL statement to get the topic from the database
strSQL = "SELECT " & "Usuarios.UsuarioID FROM " & "Usuarios "
strSQL = strSQL & "WHERE (" & "Usuarios.FechaCreacion < " & strDatabaseDateFunction & " - " & intNoOfDays  & " AND " & "Usuarios.No_of_posts=0) AND " & "Usuarios.UsuarioID > 2"
If blnUnActive = True Then 
	If portal.variablesForum.strDatabaseType = "SQLServer" Then
		strSQL = strSQL & " AND " & "Usuarios.Active = 0"
	Else
		strSQL = strSQL & " AND " & "Usuarios.Active = False"
	End If
End If
strSQL = strSQL & ";"


'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
rsCommon.CursorType = 2

'Set set the lock type of the recordset to optomistic while the record is deleted
rsCommon.LockType = 3


'Query the database
rsCommon=db.execute(strSQL)


'Create a record set object to the Threads held in the database
Set rsThread = Server.CreateObject("ADODB.Recordset")


'Loop through all all the members to delete
Do While NOT rsCommon.EOF

	'Get the Usuarios ID
	lngDelUsuariosID = CLng(rsCommon("UsuarioID"))


	'Check to make sure that there isn't any posts by the member
	strSQL = "SELECT TOP 1 " & portal.variablesForum.strDbTable & "Thread.Thread_ID FROM " & portal.variablesForum.strDbTable & "Thread WHERE " & portal.variablesForum.strDbTable & "Thread.UsuarioID=" & lngDelUsuariosID & ";"

	'Query the database
	rsThread=db.execute(strSQL)
	
	'If there are no posts start deleting
	If rsThread.EOF Then
		
		'Delete the members buddy list
		'Initalise the strSQL variable with an SQL statement
		strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "BuddyList WHERE (UsuarioID ="  & lngDelUsuariosID & ") OR (Buddy_ID ="  & lngDelUsuariosID & ")"
		
		'Write to database
		db.execute(strSQL)	
		
		
		'Delete the members private msg's
		strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "PMMessage WHERE (UsuarioID ="  & lngDelUsuariosID & ")"
			
		'Write to database
		db.execute(strSQL)	
		
		
		'Delete the members private msg's
		strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "PMMessage WHERE (From_ID ="  & lngDelUsuariosID & ")"
			
		'Write to database
		db.execute(strSQL)
		
		
		'Set all the users private messages to Guest account
		strSQL = "UPDATE " & portal.variablesForum.strDbTable & "PMMessage SET From_ID=2 WHERE (From_ID ="  & lngDelUsuariosID & ")"
			
		'Write to database
		db.execute(strSQL)
		
		
		'Set all the users posts to the Guest account
		strSQL = "UPDATE " & portal.variablesForum.strDbTable & "Thread SET UsuarioID=2 WHERE (UsuarioID ="  & lngDelUsuariosID & ")"
			
		'Write to database
		db.execute(strSQL)
				
		
		'Delete the user from the email notify table
		strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "EmailNotify WHERE (UsuarioID ="  & lngDelUsuariosID & ")"
			
		'Write to database
		db.execute(strSQL)
		
		
		'Delete the user from forum permissions table
		strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "Permissions WHERE (UsuarioID ="  & lngDelUsuariosID & ")"
			
		'Write to database
		db.execute(strSQL)
		
		
		'Delete the record set
		rsCommon.Delete
		
		
		'Total up the number of members deleted
		lngNumberOfMembers = lngNumberOfMembers + 1
	End If
	
	'Close the recordset
	rsThread.Close

	'Move to the next record
	rsCommon.MoveNext
Loop



'Reset Server Objects
Set rsThread = Nothing
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

%>
<html>
<head>

<title>Batch Delete Members</title>

<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"> 
 <p class="text"><span class="heading">Batch Delete Members </span><br />
  <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
  <br />
  <br />
  <br />
  <br />
  <br />
  <% = lngNumberOfMembers %> Members have been Deleted.<br />
 </p>
</div>
</body>
</html>
