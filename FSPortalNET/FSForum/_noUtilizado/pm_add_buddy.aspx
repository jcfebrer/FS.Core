

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True

'Declare variables
Dim rsAddBuddyList	'Db recorset to add the new buddy
Dim strusuario		'Holds the usrename of the new buddy
Dim strDescription	'Holds a short description of the buddy
Dim blnBlocked		'Set to true if the users is blocked from messaging
Dim intCode		'Return page code
Dim intErrorNum		'Holds the error number
Dim lngUsuariosID		'Holds the Usuarioss user ID

'Set the return page code
intCode = 1

'If Priavte messages are not on then send them away
If blnPrivateMessages = False Then 
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	Response.Redirect("default.aspx")
End If


'If the user is not allowed then send them away
If portal.variablesForum.intGroupID = 2 OR blnActiveMember = False Then 
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	Response.Redirect("insufficient_permission.aspx")
End If



'Read in the details from the form
strusuario = Trim(Mid(Request.Form("usuario"), 1, 15))
strDescription = Trim(Mid(Request.Form("description"), 1, 30))
blnBlocked = CBool(Request.Form("blocked"))


'Take out parts of the usuario that are not permitted
strusuario = disallowedMemberNames(strusuario)


'Clean up user input
strusuario = formatSQLInput(strusuario)
strDescription = func.formatInput(strDescription)



'Check that the new buddy exsists
	
'Initalise the SQL string to query the database to see if the uername exists
strSQL = "SELECT " & "Usuarios.UsuarioID FROM " & "Usuarios "
strSQL = strSQL & "WHERE " & "Usuarios.usuario = '" & strusuario & "';"

'Open the recordset
rsCommon=db.execute(strSQL)



'If the user exsist check there not in the list and then add them
If NOT rsCommon.EOF Then
	
	'Get the Usuarios ID
	lngUsuariosID = CLng(rsCommon("UsuarioID"))
	
	'Intialise the ADO recordset object
	Set rsAddBuddyList = Server.CreateObject("ADODB.Recordset")
		
	'Initalise the SQL string with a query to check to see if user is already in list
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "BuddyList.* FROM " & portal.variablesForum.strDbTable & "BuddyList "
	strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "BuddyList.Buddy_ID = " & lngUsuariosID & " AND " & portal.variablesForum.strDbTable & "BuddyList.UsuarioID = " & portal.variablesForum.lngLoggedInUserID & ";"
	
	'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
	rsAddBuddyList.CursorType = 2
	
	'Set the Lock Type for the records so that the record set is only locked when it is updated
	rsAddBuddyList.LockType = 3
	
	'Open the recordset
	rsAddBuddyList=db.execute(strSQL)
	
	'If no record is returned the buddy is not already in the buddy list so eneter them
	If rsAddBuddyList.EOF Then
		
		'Add the new buddy
		rsAddBuddyList.AddNew
		rsAddBuddyList.Fields("UsuarioID") = portal.variablesForum.lngLoggedInUserID
		rsAddBuddyList.Fields("Buddy_ID") = lngUsuariosID
		rsAddBuddyList.Fields("Description") = strDescription
		rsAddBuddyList.Fields("Block") = blnBlocked
		rsAddBuddyList.Update
		
		'Set the msg varaible to let the user know the buddy has been added
		intCode = 2
		
	'Else the buddy is alreay entered so set the msg varaiable to tell the user
	Else
		intErrorNum = 1
	End If

	'Clear up
	rsAddBuddyList.Close
	Set rsAddBuddyList = Nothing

Else
	'Tell the next page to display an error msg as user is not found
	intErrorNum = 2
End If

'Clear up
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

'Remove anti SQL injection code
strusuario = Replace(strusuario, "''", "'", 1, -1, 1)

'Return to the page showing the threads
response.redirect("pm_buddy_list.aspx?name=" & Server.URLEncode(strusuario) & "&amp;desc=" & Server.URLEncode(strDescription) & "&amp;code=" & intCode & "&amp;eR=" & intErrorNum
%>