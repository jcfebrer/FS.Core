

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true
Response.Buffer = True


'Dimension variables
Dim rsAdminDetails	'recordset holding the admin details
Dim strMode		'holds the mode of the page, set to true if changes are to be made to the database
Dim strEncyptedclave	'Holds the new clave
Dim blnusuarioOK	'Set to ture if the Name is not already in the database
Dim strCheckusuario	'Holds the Name from the database that we are checking against
Dim blnUpdated		'Set to true if the usuario and clave are updated


'Initialise variables
blnusuarioOK = True
blnUpdated = False

'Redirect if this is not the main forum account
If portal.variablesForum.lngLoggedInUserID <> 1 Then

	'Reset Server Objects
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

	Response.Redirect("admin_menu.aspx")
End If




'Read in the users details from the form
strMode = Request.Form("mode")


'If the user is changing there usuario and clave then update the database
If strMode = "postBack" Then

	'Read in the usuario and clave from the form
	strusuario = Trim(Mid(Request.Form("usuario"), 1, 15))
	strclave = LCase(Trim(Mid(Request.Form("clave"), 1, 15)))

	'If there is no usuario entered then don't save
	If strusuario = "" Then blnusuarioOK = False

	'Make sure the user has not entered disallowed usuarios
	If InStr(1, strusuario, "clave", vbTextCompare) Then blnusuarioOK = False
	If InStr(1, strusuario, "Usuarios", vbTextCompare) Then blnusuarioOK = False
	If InStr(1, strusuario, "code", vbTextCompare) Then blnusuarioOK = False
	If InStr(1, strusuario, "usuario", vbTextCompare) Then blnusuarioOK = False
	If InStr(1, strusuario, "N0act", vbTextCompare) Then blnusuarioOK = False

	'Clean up user input
        strusuario = formatSQLInput(strusuario)

	'Intialise the ADO recordset object
	Set rsCommon = Server.CreateObject("ADODB.Recordset")

	'Read in the usuarios from the database to check the usuario does not alreday exsist
	'Initalise the strSQL variable with an SQL statement to query the database
	strSQL = "SELECT " & "Usuarios.usuario FROM " & "Usuarios WHERE " & "Usuarios.usuario = '" & strusuario & "' AND NOT " & "Usuarios.UsuarioID = 1;"

	'Query the database
	rsCommon=db.execute(strSQL)

	'If there is a record returned then the usuario is already in use
	If NOT rsCommon.EOF Then blnusuarioOK = False

	'Remove SQL safe single quote double up set in the format SQL function
        strusuario = Replace(strusuario, "''", "'", 1, -1, 1)


	'Clean up
	rsCommon.Close

	'If the usuario dose not already exsists then save the users details to the database
	If blnusuarioOK Then


		'Only encrypt clave if this is enabled
		If blnEncryptedclaves Then
			
			'Generate new salt
	                strSalt = getSalt(Len(strclave))
	
	                'Concatenate salt value to the clave
	                strEncyptedclave = strclave & strSalt
	
	                'Re-Genreate encypted clave with new salt value
	                strEncyptedclave = HashEncode(strEncyptedclave)
		
		'Else the clave is not set to be encrypted so place the un-encrypted clave into the strEncyptedclave variable
		Else
			strEncyptedclave = strclave
		End If


		'Intialise the strSQL variable with an SQL string to open a record set for the Usuarios table
		strSQL = "SELECT " & "Usuarios.usuario,  " & "Usuarios.clave, " & "Usuarios.Salt, " & "Usuarios.User_code "
		strSQL = strSQL & "From " & "Usuarios "
		strSQL = strSQL & "WHERE " & "Usuarios.UsuarioID=1;"


		'Set the Lock Type for the records so that the record set is only locked when it is updated
		rsCommon.LockType = 3

		'Set the Cursor Type to dynamic
		rsCommon.CursorType = 2

		'Open the Usuarios table
		rsCommon=db.execute(strSQL)

		'Randomise the system timer
		Randomize Timer

		'Calculate a code for the user
                strUserCode = func.userCode(strusuario)

		With rsCommon
			'Update the recordset
			.Fields("usuario") = strusuario
			.Fields("clave") = strEncyptedclave
			.Fields("Salt") = strSalt
			.Fields("User_code") = strUserCode

			'Update the database with the new user's details
			.Update

			'Re-run the NewUser query to read in the updated recordset from the database
			.Requery

			'Write a cookie with the User ID number so the user logged in throughout the forum
			'Write the cookie with the Name Forum containing the value UserID number
			Response.Cookies(portal.variables.strCookieName)("UID") = strUserCode

			'Read back in the new usuario
			strusuario = rsCommon("usuario")

			'Clean up
			.Close
		End With
		
		'Set the update field to true
		blnUpdated = True

	End If
End If

'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
<html>
<head>

<title>Change Admin usuario &amp; clave</title>


     	

<!-- Check the from is filled in correctly before submitting -->
<script  language="javascript">
<!-- Hide from older browsem_rs...

//Function to check form is filled in correctly before submitting
function CheckForm () {

	//Initialise variables
        var errorMsg = "";
        var errorMsgLong = "";

	//Check for a usuario
        if (document.frmChangeclave.usuario.value.length <= 3){
                errorMsg += "\n\tusuario \t- Your usuario must be at least 4 characters";
        }

        //Check for a clave
        if (document.frmChangeclave.clave.value.length <= 3){
                errorMsg += "\n\tclave \t- Your clave must be at least 4 characters";
        }

        //Check both claves are the same
        if ((document.frmChangeclave.clave.value) != (document.frmChangeclave.clave2.value)){
                errorMsg += "\n\tclave Error\t- The claves entered do not match";
                document.frmChangeclave.clave.value = "";
                document.frmChangeclave.clave2.value = "";
        }

        //If there is a problem with the form then display an error
	if ((errorMsg != "") || (errorMsgLong != "")){
		msg = "_________________________________________________________________\n\n";
		msg += "The form has not been submitted because there are problem(s) with the form.\n";
		msg += "Please correct the problem(s) and re-submit the form.\n";
		msg += "_________________________________________________________________\n\n";
		msg += "The following field(s) need to be corrected: -\n";

		errorMsg += alert(msg + errorMsg + "\n" + errorMsgLong);
		return false;
	}

	return true;
}
// -->
</script>
<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center">
 <p class="text"><span class="heading">Change Admin usuario &amp; clave</span><br />
  <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
  <br />
  Make sure you <b>remember</b> the new<b> usuario</b> and <b>clave</b> <br />
  as you <b>will not</b> be able to Login or <b>Administer the Forum without them</b>!!!<br />
  <br />
  claves are one way 160bit encrypted and so can NOT be retrieved.<br />
 </p>
</div><%

If blnusuarioOK = False Then
%>
<table width="98%" border="0" cellspacing="0" cellpadding="0" align="center">
 <tr>
  <td align="center" class="lgText">Sorry the usuario you requested is already taken.<br />
   Please choose another usuario.</td>
 </tr>
</table><%

End If


If blnUpdated Then
%>
<table width="98%" border="0" cellspacing="0" cellpadding="0" align="center">
 <tr>
  <td align="center" class="lgText">Your usuario and/or clave have been updated.</td>
 </tr>
</table><%

End If
%>
<form method="post" Name="frmChangeclave" action="change_admin_usuario.aspx" onSubmit="return CheckForm();">
 <table width="350" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000" height="30">
  <tr>
   <td height="2" width="483" align="center"> <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#000000">
     <tr>
      <td bgcolor="#FFFFFF"><table width="100%" border="0" cellspacing="0" cellpadding="2">
        <tr bgcolor="#F5F5FA"> 
         <td width="40%" align="right" class="text">usuario:&nbsp;&nbsp;</td>
         <td width="60%"> 
          <input type='text' Name="usuario" size="15" maxlength="15" value="<% = strusuario %>"> </td>
        </tr>
        <tr bgcolor="#F5F5FA"> 
         <td width="40%" align="right" class="text">clave:&nbsp; </td>
         <td width="60%"> 
          <input type="clave" Name="clave" size="15" maxlength="15"> </td>
        </tr>
        <tr bgcolor="#F5F5FA"> 
         <td width="40%" align="right" class="text">Confirm clave:&nbsp; </td>
         <td width="60%"> 
          <input type="clave" Name="clave2" size="15" maxlength="15"> </td>
        </tr>
        <tr bgcolor="#F5F5FA"> 
         <td width="40%" align="right"> 
          <input type="hidden" Name="mode" value="postBack"> </td>
         <td width="60%"> 
          <input type='submit' Name="Submit" value="Update Details"> <input type="reset" Name="Reset" value="Clear"> </td>
        </tr>
       </table></td>
     </tr>
    </table></td>
  </tr>
 </table>
</form>
<br />
</body>
</html>