<%@ Page Language="VB" AutoEventWireup="false" CodeFile="condiciones.aspx.vb" Inherits="_condiciones" %>
<%
'Set the timeout of the forum
Server.ScriptTimeout = 90

'Set the date time format to your own if you are getting a CDATE error
'Session.LCID = 1033

Dim adoCon 			'Database session("conn")ection Variable Object
Dim strCon			'Holds the string to session("conn")ect to the db
Dim rsCommon			'Database recordset
Dim lngLoggedInUserID		'Holds the logginned in user ID
Dim strusuario			'Holds the users usuario
Dim strclave			'Holds the usres clave
Dim strUserCode			'Holds the users ID code
Dim strLoggedInUserCode		'Holds the loggin in user ID
Dim strSQL			'Holds the SQL query
Dim strSalt			'Holds salt values
Dim strCode			'Holds the page code
Dim strCode2			'Holds the page code
Dim strDatabaseDateFunction	'Holds a different date function for Access or SQL server
Dim strDatabaseType		'Holds the type of database used
Dim strDbPathAndName		'Holds the path and name of the database
Dim intGroupID			'Holds the group ID of the user


'Intialise variables
Const strVersion = "7.9"
strSalt = "5CB237B1D85"
Const strCodeField = "&#076;_&#099;&#111;&#100;&#101;"
portal.variablesForum.lngLoggedInUserID = 0
portal.variablesForum.intGroupID = 0
Const blnMassMailier = True




'Database Type
portal.variablesForum.strDatabaseType = "Access"
'portal.variablesForum.strDatabaseType = "SQLServer"

'Set up the database table name prefix and stored procedure prefix
'(This is useful if you are running multiple forums from one database)
' - make sure you also change this in the msSQL_server_setup.aspx file if setting up an ms SQL server database)
Const portal.variablesForum.strDbTable = "Forum"
Const portal.variablesForum.strDbProc = "wwfSp"


'Set up the forum cookie name
'(This is useful if you run multiple copies of Web Wiz Forums on the same site so that cookies don't interfer with each other)
'Const portal.variables.strCookieName = "WWF"


'Encrypted claves
'This will make your forum unsecure from hackers if you disable this!!!!!
'This can NOT be changed once your forum is in use!!!
'You will also need to directly edit the database to type in the admin clave to the clave field in the tblUsuarios table at record position 1
'also edit both common.aspx files to change this variable
Const blnEncryptedclaves = true




'Create database session("conn")ection
'Create a session("conn")ection odject
Set adoCon = Server.CreateObject("ADODB.Connection")

'If this is access set the access driver
If portal.variablesForum.strDatabaseType = "Access" Then



	'--------------------- Set the path and name of the database --------------------------------------------------------------------------------
	
	'Virtual path to database
	strDbPathAndName = "D:\Domains\portal.com\db\portal.mdb"  'This is the path of the database from this files location on the server
	
	'Physical path to database
	'strDbPathAndName = "" 'Use this if you use the physical server path, eg:- C:\Inetpub\private\WebWizForum.mdb
	
	
	'BRINKSTER USEm_rs(Web Wiz Forums only works with free Brinkster accounts, not for the paid accounts)
	'Brinkster users remove the ' single quote mark from infront of the line below and replace usuario with your Brinkster uersname
	
	'strDbPathAndName = Server.MapPath("/usuario/db/wwForum.mdb")
	
	'PLEASE NOTE: - For extra security it is highly recommended you change the name of the database, wwForum.mdb, to another name and then 
	'replace the wwForum.mdb found above with the name you changed the forum database to.
	
	'---------------------------------------------------------------------------------------------------------------------------------------------
	
	
	
	
	'------------- If you are having problems with the script then try using a diffrent driver or DSN by editing the lines below --------------
				 
	'Database session("conn")ection info and driver (if this driver does not work then comment it out and use one of the alternative drivers)
	'strCon = "DRIVER={Microsoft Access Driver (*.mdb)}; DBQ=" & strDbPathAndName
	
	'Alternative drivers faster than the basic one above
	'strCon = "Provider=Microsoft.Jet.OLEDB.3.51; Data Source=" & strDbPathAndName 'This one is if you convert the database to Access 97
	'strCon = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & strDbPathAndName  'This one is for Access 2000/2002
	strCon = portal.variables.ConnPortal
	
	'If you wish to use DSN then comment out the driver above and uncomment the line below (DSN is slower than the above drivers)
	'strCon = "DSN=DSN_NAME" 'Place the DSN where you see DSN_NAME
	
	'---------------------------------------------------------------------------------------------------------------------------------------------




	
	'The now() function is used in Access for dates
	strDatabaseDateFunction = "Now()"


'Else set the MS SQL server stuff
Else 	
	
	%><!--#include file="SQL_server_session("conn")ection.aspx" --><%
	
	'The GetDate() function is used in SQL Server
	strDatabaseDateFunction = "GetDate()"
End If


'Set the session("conn")ection string to the database
adoCon.connectionstring = strCon

'Set an active session("conn")ection to the session("conn")ection object
adoCon.Open






'Read in the users details from the form if user is logging in
strusuario = Trim(Mid(Request.Form("name"), 1, 15))
strclave = LCase(Trim(Mid(Request.Form("clave"), 1, 15)))


'Intialise the ADO recordset object
Set rsCommon = Server.CreateObject("ADODB.Recordset")


'If a usuario has been entered check that the clave is correct
If strusuario <> "" AND Session("lngSecurityCode") = Trim(Mid(Request.Form("securityCode"), 1, 6)) Then
	
	
	'Check the users session ID for security from hackers
	Call checkSessionID(Request.Form("sessionID"))
	
	
	'Take out parts of the usuario that are not permitted
	strusuario = Replace(strusuario, "clave", "", 1, -1, 1)
	strusuario = Replace(strusuario, "salt", "", 1, -1, 1)
	strusuario = Replace(strusuario, "Usuarios", "", 1, -1, 1)
	strusuario = Replace(strusuario, "code", "", 1, -1, 1)
	strusuario = Replace(strusuario, "usuario", "", 1, -1, 1)
	
	'Replace harmful SQL quotation marks with doubles
	strusuario = formatSQLInput(strusuario)
	
	'Read the various forums from the database
	'Initalise the strSQL variable with an SQL statement to query the database
	strSQL = "SELECT " & "Usuarios.usuario, " & "Usuarios.clave, " & "Usuarios.Salt, " & "Usuarios.Group_ID, " & "Usuarios.UsuarioID, " & "Usuarios.User_code "
	strSQL = strSQL & "FROM " & "Usuarios "
	strSQL = strSQL & "WHERE " & "Usuarios.usuario = '" & strusuario & "';"
	
	'Query the database
	rsCommon=db.execute(strSQL)
	
	'If the query has returned a value to the recordset then check the clave is correct
	If NOT rsCommon.EOF Then
		
		
		'Only encrypt clave if this is enabled
		If blnEncryptedclaves Then
			'Encrypt clave so we can check it against the encypted clave in the database
			'Read in the salt
			strclave = strclave & rsCommon("Salt")
	
			'Encrypt the entered clave
			strclave = HashEncode(strclave)
		End If
		
	
		'Check the encrypted clave is correct, if it is get the user ID and set a cookie
		If strclave = rsCommon("clave") Then
			
			'Read in the users ID number and whether they want to be automactically logged in when they return to the forum
			strusuario = rsCommon("usuario")
			portal.variablesForum.lngLoggedInUserID = CLng(rsCommon("UsuarioID"))
			strUserCode = rsCommon("User_code")
			portal.variablesForum.intGroupID = CInt(rsCommon("Group_ID"))
			
			
			'Write a cookie with the User ID number so the user logged in throughout the forum	
			'Write the cookie with the name Forum containing the value UserID number
			Response.Cookies(portal.variables.strCookieName)("UID") = strUserCode
		End If
	End If
	
	'Reset Server Objects
	rsCommon.Close
End If
	



'Read in users ID number from the cookie
strLoggedInUserCode = Trim(Mid(func.ValorCookie(Request.Cookies(portal.variables.strCookieName),"UID"), 1, 44))


'If a cookie exsists on the users system then read in there usuario from the database
If strLoggedInUserCode <> "" Then
	
	'Make the usercode SQL safe
	strLoggedInUserCode = formatSQLInput(strLoggedInUserCode)
	
	'Initalise the strSQL variable with an SQL statement to query the database
	strSQL = "SELECT " & "Usuarios.UsuarioID, " & "Usuarios.usuario, " & "Usuarios.Group_ID "
	strSQL = strSQL & "FROM " & "Usuarios "
	strSQL = strSQL & "WHERE User_code = '" & strLoggedInUserCode & "';"
		
	'Query the database
	rsCommon=db.execute(strSQL)
	
	'If there is a user with the ID number read in from the cookie then
	If NOT rsCommon.EOF Then
	
		'Read in the users details from the recordset
		strusuario = rsCommon("usuario")
		portal.variablesForum.lngLoggedInUserID = CLng(rsCommon("UsuarioID"))
		portal.variablesForum.intGroupID = CInt(rsCommon("Group_ID"))
	
	'Otherwise the usuario is not correct or the user has been barred so set there User ID to 0
	Else
		portal.variablesForum.lngLoggedInUserID = 0
		portal.variablesForum.intGroupID = 0
	End If
	
	'Reset server objects
	rsCommon.Close

End If


'If the user is not the admin or not logged in send them away
If portal.variablesForum.intGroupID <> 1 Then 
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	response.redirect("../insufficient_permission.aspx"
End If




'Check license is agreed to
'Initialise the SQL variable with an SQL statement to get the configuration details from the database
If portal.variablesForum.strDatabaseType = "SQLServer" Then
	strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "SelectConfiguration"
Else
	strSQL = "SELECT TOP 1 " & portal.variablesForum.strDbTable & "Configuration.* From " & portal.variablesForum.strDbTable & "Configuration;"
End If
	
'Query the database
rsCommon=db.execute(strSQL)

'If the license is not agreed to the  redirect
If Cbool(rsCommon("L_Code")) = true and Session("blnLicense") = false Then
	
	'Clean up
	rsCommon.Close
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	response.redirect("webwizforums_license.aspx"
End If

'Clean up
rsCommon.Close
%>
<!--#include file="functions/functions_filtem_rs.aspx" -->
<!--#include file="functions/functions_common.aspx" -->
<!--#include file="functions/functions_hash1way.aspx" -->
