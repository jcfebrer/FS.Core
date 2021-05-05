

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include file="functions/functions_common.aspx" -->
<%
'Set the response buffer to true
Response.Buffer = True



'Dimension variables
Dim strNewusuario                 'Holds the users usuario
Dim strMemberName	'Holds the member name to lookup
Dim intErrorCode	'Holds the error code if user not found
Dim lngMemberID		'Holds the ID number of the member
Dim blnMemNotFound	'Holds the error code if user not found
Dim blnusuarioOK	'Holds if the usuario is OK
Dim blnMemNameUpdated	'Set to true if the member name is updated

'Initiliase variables
blnMemNotFound = false
blnMemNameUpdated = false
blnusuarioOK = true


'If this is a postback check for the user exsisting in the db before redirecting
If Request.Form("postBack") Then
	
		
	
	'Read in the members name to lookup
	strMemberName = Trim(Mid(Request.Form("member"), 1, 20))
	strNewusuario = Trim(Mid(Request.Form("newMember"), 1, 15))
	
	'Take out parts of the usuario that are not permitted
	strMemberName = disallowedMemberNames(strMemberName)
	
	'Make sure the user has not entered disallowed usuarios
        If InStr(1, strNewusuario, "admin", vbTextCompare) Then blnusuarioOK = False
        If InStr(1, strNewusuario, "clave", vbTextCompare) Then blnusuarioOK = False
        If InStr(1, strNewusuario, "salt", vbTextCompare) Then blnusuarioOK = False
        If InStr(1, strNewusuario, "Usuarios", vbTextCompare) Then blnusuarioOK = False
        If InStr(1, strNewusuario, "code", vbTextCompare) Then blnusuarioOK = False
        If InStr(1, strNewusuario, "usuario", vbTextCompare) Then blnusuarioOK = False
        If InStr(1, strNewusuario, "N0act", vbTextCompare) Then blnusuarioOK = False
	
	'Get rid of milisous code
	strMemberName = formatSQLInput(strMemberName)
	strNewusuario = formatSQLInput(strNewusuario)
	
	
	'Check new usuario isn't already taken
	'Initalise the strSQL variable with an SQL statement to query the database
	strSQL = "SELECT " & "Usuarios.UsuarioID From " & "Usuarios WHERE " & "Usuarios.usuario='" & strNewusuario & "';"
	
	'Query the database
	rsCommon=db.execute(strSQL)
	
	'See if a user with that name is returned by the database
	If NOT rsCommon.EOF Then blnusuarioOK = false
	
	'Reset Server Objects
	rsCommon.Close

	'If the usuario is OK then get the user to change
	If blnusuarioOK Then
		
		'Initalise the strSQL variable with an SQL statement to query the database
		strSQL = "SELECT " & "Usuarios.usuario From " & "Usuarios WHERE " & "Usuarios.usuario='" & strMemberName & "';"
		
		'Set the Lock Type for the records so that the record set is only locked when it is updated
		rsCommon.LockType = 3

		'Set the Cursor Type to dynamic
		rsCommon.CursorType = 2
		
		'Query the database
		rsCommon=db.execute(strSQL)
		
		'See if a user with that name is returned by the database
		If NOT rsCommon.EOF Then
			
			'Update the recordset
			rsCommon.Fields("usuario") = strNewusuario

			'Update the database with the new user's details
			rsCommon.Update
		
			'Set to true if the member name is updated
			blnMemNameUpdated = true
		
		'Else there is no user with that name returned so set an error code
		Else
		
			blnMemNotFound = true	
			
		End If
		
		'Reset Server Objects
		rsCommon.Close
	End If


End If


'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
<html>
<head>

<title>Change usuario </title>


     	

<!-- Check the from is filled in correctly before submitting -->
<script  language="javascript">
<!-- Hide from older browsem_rs...

//Function to check form is filled in correctly before submitting
function CheckForm () {

	//Check for a group
	if (document.frmAddMessage.member.value==""){
		alert("Please enter a members usuario");
		return false;
	}
	
	return true
}


//Function to open pop up window
function openWin(theURL,winName,features) {
  	window.open(theURL,winName,features);
}
// -->
</script>
<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center" class="text"><span class="heading">Change usuario </span><br />
 <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
 <br />
 From here you can change the usuario of your forum membem_rs.
 <p class="text">Select the Forum Member that you would like to change the usuario for. </p>
 <p></p>
</div>
<form action="change_usuario.aspx" method="post" name="frmAddMessage" target="_self" id="frmAddMessage" onSubmit="return CheckForm();">
 <tr>
  <td width="500"><br /> 
   <table width="500" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000">
    <tr>
   <td width="500"> <table width="100%" border="0" align="center" class="normal" cellpadding="4" cellspacing="1">
       <tr bgcolor="#CCCEE6"> 
        <td class="tHeading"><b> Select a Member</b></td>
       </tr>
       <tr bgcolor="#FFFFFF">
        <td bgcolor="#F5F5FA" class="text">Old usuario&nbsp;         <input name="member" type='text' id="member" size="20" maxlength="20" value="<% If blnMemNameUpdated = false Then Response.Write(strMemberName) %>">
         <input type="button" name="Button" value="Search for Member" onClick="openWin('../pop_up_member_search.aspx','profile','toolbar=0,location=0,status=0,menubar=0,scrollbars=0,resizable=1,width=440,height=255')"></td>
       </tr>
       <tr bgcolor="#FFFFFF">
        <td bgcolor="#CCCEE6" class="tHeading">Enter new usuario for Member</td>
       </tr>
       <tr bgcolor="#FFFFFF">
        <td bgcolor="#F5F5FA" class="text">New usuario 
        <input name="newMember" type='text' id="newMember" size="20" maxlength="15" value="<% If blnMemNameUpdated = false Then Response.Write(strNewusuario) %>"></td>
       </tr>
      </table></td>
  </tr>
 </table>
 <div align="center"><span class="text"><br />
  <input type="hidden" name="postBack" value="true" />
  <input type='submit' name="Submit" value="Change usuario">
 </span><br />
    <br />
 </div>
</form><%

'If the usuario can not be found display an error message pop-up
If blnMemNotFound  Then
        Response.Write("<script  language=""JavaScript"">")
        Response.Write("alert('The usuario entered could not be found.\n\nPlease check your spelling and try again.');")
        Response.Write("</script>")

End If 

'If the usuario is already gone display an error message pop-up
If blnusuarioOK = false  Then
        Response.Write("<script  language=""JavaScript"">")
        Response.Write("alert('Sorry, the usuario you requested is already taken.\n\nPlease choose another usuario.');")
        Response.Write("</script>")

End If 

'If the usuario is already gone display an error message pop-up
If blnMemNameUpdated Then
        Response.Write("<script  language=""JavaScript"">")
        Response.Write("alert('The member \'" & strMemberName & "\' has had their usuario changed to \'" & strNewusuario & "\'.');")
        Response.Write("</script>")

End If 

%>
</body>
</html>