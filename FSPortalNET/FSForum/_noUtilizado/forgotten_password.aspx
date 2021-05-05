

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include virtual="/fsportalnet/includes/funcionesMail.aspx" -->
<!--#include file="functions/functions_hash1way.aspx" -->
<%
Response.Buffer = True 


'Dimension variables
Dim objCDOMail			'Holds the CDO mail object
Dim objJMail			'Holds the Jmail object
Dim strusuario			'Holds the users usuario
Dim strclave			'Holds the usres clave
Dim strEmailAddress		'Holds the users e-mail address
Dim strReturnPage		'Holds the page to return to 
Dim blnInvalidusuario 		'Set to true if the usuario entered does not exsit
Dim blnInvalidEmail 		'Set to true if the user has not given there e-mail address	
Dim blnEmailSent		'Set to true if the e-mail has been sent
Dim strEmailBody		'Holds the body of the e-mail message	
Dim strSubject			'Holds the subject of the e-mail
Dim strSalt			'Holds the salt value for the clave
Dim strEncyptedclave		'Holds the encrypted clave
Dim strUserCode			'Holds the user code for the user


'Intialise variables
blnInvalidusuario = False
blnInvalidEmail = False
portal.variablesForum.blnEmailSent = False

'If e-mail notify is not turned on then close the window
If portal.variablesForum.blnEmail = False Then
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	response.redirect("default.aspx"
End If

'Read in the users details from the form
strusuario = Trim(Mid(Request.Form("name"), 1, 15))
strEmailAddress = Trim(Mid(Request.Form("email"), 1, 60))

'Take out parts of the usuario that are not permitted
strusuario = disallowedMemberNames(strusuario)

'Replace harmful SQL quotation marks with doubles
strusuario = formatSQLInput(strusuario)
strEmailAddress = formatSQLInput(strEmailAddress)

'Remove single quotes as they should not be in email addresses
strEmailAddress = Replace(strEmailAddress, "'", "", 1, -1, 1)
   
   
'If a usuario has been entered check that the clave is correct
If NOT strusuario = "" Then
	
	'Initalise the strSQL variable with an SQL statement to query the database
	strSQL = "SELECT " & "Usuarios.usuario, " & "Usuarios.clave, " & "Usuarios.User_code, " & "Usuarios.Salt, " & "Usuarios.email "
	strSQL = strSQL & "FROM " & "Usuarios "
	strSQL = strSQL & "WHERE " & "Usuarios.usuario = '" & strusuario & "' AND " & "Usuarios.email = '" & strEmailAddress & "';"
	
	'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
	rsCommon.CursorType = 2
	
	'Set the Lock Type for the records so that the record set is only locked when it is updated
	rsCommon.LockType = 3
	
	'Query the database
	rsCommon=db.execute(strSQL)
	
	
	
	'If the query has returned a value to the recordset then generate new clave and send it to the user in an email
	If NOT rsCommon.EOF Then
	
		'Read in the users email address from the recordset
		strEmailAddress = rsCommon("email")
		
		'If there is a clave in the db to send to change the clave and email the user
		If NOT strEmailAddress = "" Then
			
			'Genrate a new user code for the user
			strUserCode = func.userCode(rsCommon("usuario"))
			
			'Generate a new clave using an 8 character long hex values
			strclave = hexValue(8)
			
			'If pass is to be encrypted then do so
			If blnEncryptedclaves Then
				
				'Create a salt value for the new clave
				strSalt = getSalt(8)
				
				'Concatenate salt value to the clave
				strEncyptedclave = LCase(strclave) & strSalt
				
				'Encrypt the clave
				strEncyptedclave = HashEncode(strEncyptedclave) 
			
			'Else the clave is not to be encrypted
			Else
				strEncyptedclave = LCase(strclave)
			End If
			
			
			'Save new clave back to the database with the salt
			rsCommon.Fields("clave") = strEncyptedclave
			rsCommon.Fields("Salt") = strSalt	
			rsCommon.Fields("User_code") = strUserCode		
			
			'Update the database with the new clave
			rsCommon.Update
			
		
		
			'Initailise the e-mail body variable with the body of the e-mail
			strEmailBody = portal.variablesForum.strTxtHi
			strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtEmailclaveRequest & " " & strMainForumName & "."
			strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtEmailclaveRequest2 & " " & strclave
			strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtEmailclaveRequest3
			strEmailBody = strEmailBody & vbCrLf & vbCrLf & "   " & strForumPath
			
			'Initalise the subject of the e-mail
			strSubject = portal.variablesForum.strTxtForumLostclaveRequest
			
			'Send the e-mail using the Send Mail function created on the send_mail_function.inc file
			portal.variablesForum.blnEmailSent = funcMail.SendMailForo(strEmailBody, decodeString(strusuario), decodeString(strEmailAddress), strWebsiteName, decodeString(strForumEmailAddress), strSubject, strMailComponent, false)
			
		Else
			'Set the Invalid e-mail variable to True
			blnInvalidEmail = True	
		End If
	
	
	Else
		'Set the Invalid usuario variable to True
		blnInvalidusuario = True		
		
	End If
	
	'Clean up
	rsCommon.Close
End If
	


'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>  
<html>
<head>
<title>Forgotten clave</title>



     	
		
<!-- Check the from is filled in correctly before submitting -->
<script  language="javascript">
<!-- Hide from older browsem_rs...

//Function to check form is filled in correctly before submitting
function CheckForm () {

	var errorMsg = "";
	
	//Check for a usuario
	if (document.frmMailPass.name.value==""){
	
		msg = "<% = portal.variablesForum.strTxtErrorDisplayLine %>\n\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine1 %>\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine2 %>\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine %>\n\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine3 %>\n";
	
		alert(msg + "\n\t<% = portal.variablesForum.strTxtErrorusuario %>");
		document.frmMailPass.name.focus();
		return false;
	}
	
	return true
}
// -->
</script>
<!--#include file="includes/skin_file.aspx" -->
</head>
<body bgcolor="<% = strBgColour %>" text="<% = strTextColour %>" background="<% = strBgImage %>" marginheight="0" marginwidth="0" topmargin="0" leftmargin="0" OnLoad="self.focus();">
<div align="center" class="heading"><% = portal.variablesForum.strTxtForgottenclave %></div><br /><%

'If the user has entered a usuario that does not exsit then display an error message
If blnInvalidusuario = True Then
%>
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="0" align="center">
    <tr> 
    <td align="center" class="error"><% = portal.variablesForum.strTxtNoRecordOfusuario %><br /><% = portal.variablesForum.strTxtPleaseTryAgain %></td>
    </tr>
  </table><%
  
'If there is no e-mail address for the user then display an error message
ElseIf blnInvalidEmail = True Then
%>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="0" align="center">
  <tr>
    <td align="center"> 
      <p class="text"><% = portal.variablesForum.strTxtNoEmailAddressInProfile %><br />
        <br /><% = portal.variablesForum.strTxtReregisterForForum %><br /><br /><br /></p>
      </td>
  </tr>
</table><%

'If the clave has been e-mailed to the user then let them know
ElseIf portal.variablesForum.blnEmailSent = True Then
%>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="0" align="center">
    <tr> 
    <td align="center"><span class="text"><% = portal.variablesForum.strTxtclaveEmailToYou %> 
      </span><br /><br /><br />
    </td>
    </tr>
  </table><%
  
Else
%>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="0" align="center">
    <tr> 
      
    <td align="center" class="text"><% = portal.variablesForum.strTxtPleaseEnterYourusuario %></td>
    </tr>
  </table><%
  
End If

If blnInvalidEmail = False AND portal.variablesForum.blnEmailSent = False Then
%>
<form method="post" name="frmMailPass" action="forgotten_clave.aspx" onSubmit="return CheckForm();">
  <br />
  <table width="390" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" height="30">
    <tr> 
      <td height="2" width="483" align="center"> 
        <table width="100%" border="0" cellspacing="1" cellpadding="2">
          <tr>
            <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" height="26"> 
              <table width="100%" border="0" cellspacing="0" cellpadding="2">
                <tr> 
                  <td align="right" width="30%"><span class="text"><% = portal.variablesForum.strTxtusuario %>:</span>&nbsp;&nbsp;</td>
                  <td width="70%"> 
                    <input type='text' name="name" size="15" maxlength="15" value="<% = strusuario %>">
                  </td>
                </tr>
                <tr> 
                  <td align="right" width="30%"><span class="text"><% = portal.variablesForum.strTxtEmail %>:</span>&nbsp;&nbsp;</td>
                  <td width="70%"> 
                    <input type='text' name="email" size="30" maxlength="60" value="<% = strEmailAddress %>">
                  </td>
                </tr>
                <tr> 
                  <td align="right" width="30%">&nbsp;</td>
                  <td width="70%">
                    <input type='submit' name="Submit" value="<% = portal.variablesForum.strTxtEmailclave %>">
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
<table width="75%" border="0" cellspacing="0" cellpadding="0" align="center">
  <tr>
    <td align="center" height="51" class="text"><% = portal.variablesForum.strTxtValidEmailRequired %></td>
    </tr>
  </table><%
  
End If

%><table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
  <tr>
    <td align="center"><a href="JavaScript:onClick=window.close()"><% = portal.variablesForum.strTxtCloseWindow %></a>
    <br /><br />
<% 
%>
    </td>
  </tr>
</table>
</body>
</html>