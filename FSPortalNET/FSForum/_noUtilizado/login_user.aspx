
<% response.redirect portal.variables.paginaLogin
%>
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include file="functions/functions_hash1way.aspx" -->
<%
Response.Buffer = True


'Make sure this page is not cached
Response.Expires = -1
Response.ExpiresAbsolute = Now() - 2
Response.AddHeader "pragma","no-cache"
Response.AddHeader "cache-control","private"
Response.CacheControl = "No-Store"




'Dimension variables
Dim strusuario			'Holds the users usuario
Dim strclave			'Holds the usres clave
Dim blnAutoLogin		'Holds whether the user wnats to be automactically logged in
Dim lngUserID			'Holds the users Id number
Dim strUserCode			'Holds the users ID code
Dim intForumID			'Holds the forum ID
Dim lngLoopCounter		'Holds the loop counter
Dim blnIncorrectLogin		'Set to true if login is incorrect
Dim blnSecurityCodeOK		'Set to false if the security is not OK
Dim strReferer			'Holds the page to return to



'Intialise variables
blnAutoLogin = false
blnIncorrectLogin = false
blnSecurityCodeOK = true



'read in the forum ID number
If isNumeric(Request.QueryString("FID")) Then
	portal.variablesForum.intForumID = CInt(Request.QueryString("FID"))
Else
	portal.variablesForum.intForumID = 0
End If



'Read in the users details from the form
strusuario = Trim(Mid(Request.Form("name"), 1, 15))
strclave = LCase(Trim(Mid(Request.Form("clave"), 1, 15)))
blnAutoLogin = CBool(Request.Form("AutoLogin"))


'Take out parts of the usuario that are not permitted
strusuario = Replace(strusuario, "clave", "", 1, -1, 1)
strusuario = Replace(strusuario, "salt", "", 1, -1, 1)
strusuario = Replace(strusuario, "Usuarios", "", 1, -1, 1)
strusuario = Replace(strusuario, "code", "", 1, -1, 1)
strusuario = Replace(strusuario, "usuario", "", 1, -1, 1)

'Replace harmful SQL quotation marks with doubles
strusuario = formatSQLInput(strusuario)




'If a usuario has been entered check that the clave is correct
If (strusuario <> "" AND Request.Form("QUIK") = false) OR (Request.Form("QUIK") AND blnLongSecurityCode = false AND strusuario <> "") Then
	
	'Check the users session ID for security from hackers if the user code has been disabled
	If blnLongSecurityCode = False Then Call checkSessionID(Request.Form("sessionID"))
	
	'Check security code to pervent hackers
	If Session("lngSecurityCode") <> Trim(Mid(Request.Form("securityCode"), 1, 6)) AND blnLongSecurityCode Then blnSecurityCodeOK = False

	'Read the various forums from the database
	'Initalise the strSQL variable with an SQL statement to query the database
	strSQL = "SELECT " & "Usuarios.clave, " & "Usuarios.Salt, " & "Usuarios.usuario, " & "Usuarios.UsuarioID, " & "Usuarios.User_code "
	strSQL = strSQL & "FROM " & "Usuarios "
	strSQL = strSQL & "WHERE " & "Usuarios.usuario = '" & strusuario & "';"

	'Set the Lock Type for the records so that the record set is only locked when it is updated
	rsCommon.LockType = 3


	'Query the database
	rsCommon=db.execute(strSQL)
	
	
	'If no record is returned then the login is incorrect
	If rsCommon.EOF Then blnIncorrectLogin = true

	
	'If the query has returned a value to the recordset then check the clave is correct
	If NOT rsCommon.EOF AND blnSecurityCodeOK Then

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
			lngUserID = CLng(rsCommon("UsuarioID"))
			strusuario = rsCommon("usuario")
			
			
			'For extra security create a new user code for the user
			strUserCode = func.userCode(strusuario)
			
			'Save the new usercode back to the database
			rsCommon.Fields("User_code") = strUserCode
			rsCommon.Update

			
			'Write a cookie with the User ID number so the user logged in throughout the forum
			'Write the cookie with the name Forum containing the value UserID number
			Response.Cookies(portal.variables.strCookieName)("UID") = strUserCode
   			
			
			
			'Write a cookie saying if the user is browsing anonymously, 1 = Anonymous, 0 = Shown
			If CBool(Request.Form("NS")) = False Then
				
				Response.Cookies(portal.variables.strCookieName)("NS") = "1" 'Anonymous 
			Else
				Response.Cookies(portal.variables.strCookieName)("NS") = "0" 'Shown
			End If


			'If the user has selected to be remebered when they next login then set the expiry date for the cookie for 1 year
			If blnAutoLogin = True Then

				'Set the expiry date for 1 year
				'If no expiry date is set the cookie is deleted from the users system 20 minutes after they leave the forum
				Response.Cookies(portal.variables.strCookieName).Expires = DateAdd("yyyy", 1, Now())
			End If


			'Reset Server Objects
			rsCommon.Close
			Set rsCommon = Nothing
			adoCon.Close
			Set adoCon = Nothing


			'Go to the login test to make sure the user has cookies enabled on their browser
			'If this is a redierect form the email notify unsubscribe page to get the user to log in then redirct back there
			If Request.QueryString("M") = "Unsubscribe" Then
				
				Response.Redirect("login_user_test.aspx?TID=" & Request.QueryString("TID") & "&amp;fID=" & portal.variablesForum.intForumID & "&amp;m=Unsubscribe")
			
			'Redirect the user back to the forum they have just come from
			ElseIf portal.variablesForum.intForumID > 0 Then
				
				Response.Redirect("login_user_test.aspx?FID=" & portal.variablesForum.intForumID)
			'Return to forum homepage
			Else
				Response.Redirect("login_user_test.aspx")
			End If
		
		'Else the login was incorrect
		Else
			blnIncorrectLogin = true
		End If
	End If

	'Reset Server Objects
	rsCommon.Close
End If


'If not quick login empty variables
If Request.Form("QUIK") OR blnSecurityCodeOK = false Then
	strusuario = Replace(strusuario, "''", "'")
	strclave = Replace(strclave, "''", "'")
Else
	strusuario = ""
	strclave = ""
End If



'Create security code
If blnLongSecurityCode Then 
	
	'Initliase variable
	Session("lngSecurityCode") = ""
	        
	'Create a new session security code
	For lngLoopCounter = 1 to 6
	        	
		'Randomise the system timer
		Randomize Timer
			
		'Place the random number onto the end of teh security code session variable
		Session("lngSecurityCode") = Session("lngSecurityCode") & CStr(CInt(Rnd * 9))
	Next
End If

%>
<html>
<head>

<title>Login Member</title>


     	

<!-- Check the from is filled in correctly before submitting -->
<script  language="javascript">

//Function to check form is filled in correctly before submitting
function CheckForm () {

	var errorMsg = "";

	//Check for a usuario
	if (document.frmLogin.name.value==""){
		errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorusuario %>";
	}

	//Check for a clave
	if (document.frmLogin.clave.value==""){
		errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorclave %>";
	}<%

If blnLongSecurityCode Then 
	
	%>
	
	//Check for a security code
        if (document.frmLogin.securityCode.value == ''){
                errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorSecurityCode %>";
        }<%
End If

%>

	//If there is aproblem with the form then display an error
	if (errorMsg != ""){
		msg = "<% = portal.variablesForum.strTxtErrorDisplayLine %>\n\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine1 %>\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine2 %>\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine %>\n\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine3 %>\n";

		errorMsg += alert(msg + errorMsg + "\n\n");
		return false;
	}

	return true;
}

</script>
<!-- #include file="includes/header.aspx" -->
<navigation:navigation ID="common1" runat="server" />
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="3" align="center">
  <tr>
  <td align="left" class="heading"><% = portal.variablesForum.strTxtLoginUser %></td>
</tr>
 <tr>
  <td align="left" width="71%" class="bold"><img src="<% = portal.variablesForum.strImagePath %>open_folder_icon.gif" border="0" align="middle">&nbsp;<a href="default.aspx" target="_self" class="boldLink"><% = strMainForumName %></a><% = strNavSpacer %><% = portal.variablesForum.strTxtLoginUser %><br /></td>
  </tr>
</table><br /><%

'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

'If the user has unsuccesfully tried logging in before then display a clave incorrect error
If blnIncorrectLogin OR blnSecurityCodeOK = False Then
%>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="0" align="center">
  <tr>
    <td align="center" class="error"><%
	
	'If the login has failed
	If blnIncorrectLogin Then Response.Write(portal.variablesForum.strTxtSorryusuarioclaveIncorrect & "<br />" & portal.variablesForum.strTxtPleaseTryAgain & "<br /><br />")
	
	'If the security code is incorrect
        If blnSecurityCodeOK = False Then Response.Write(Replace(portal.variablesForum.strTxtSecurityCodeDidNotMatch, "\n\n", "<br />") & "<br /><br />")
	
	%></td>
  </tr>
</table><%

End If
%>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr><form method="post" name="frmLogin" action="login_user.aspx?FID=<% = portal.variablesForum.intForumID %><% If Request.QueryString("M") = "Unsubscribe" Then Response.Write("&amp;tID=" & CLng(Request.QueryString("TID")) & "&amp;m=Unsubscribe") %>" onSubmit="return CheckForm();" onReset="return confirm('<% = strResetFormConfirm %>');">
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>">
      <table width="100%" border="0" cellspacing="1" cellpadding="3" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr bgcolor="<% = portal.variablesForum.strTableTitleColour %>">
      <td colspan="2" background="<% = portal.variablesForum.strTableTitleBgImage %>" class="tHeading"><% = portal.variablesForum.strTxtLoginUser %></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td colspan="2" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text">*<% = portal.variablesForum.strTxtRequiredFields %></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" >
         <td width="50%"  bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"><% = portal.variablesForum.strTxtusuario %>*</td>
         <td width="50%" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"><input type='text' name="name" id="name" size="15" maxlength="15" value="<% = strusuario %>" /></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"><% = portal.variablesForum.strTxtclave %>*</td>
         <td width="50%" valign="top" background="<% = portal.variablesForum.strTableBgImage %>"><input type="clave" name="clave" id="clave" size="15" maxlength="15" value="<% = strclave %>" /><%
    	
'If email notification is enabled then also show the forgotten clave link
If portal.variablesForum.blnEmail = True Then
	
	%> <a href="JavaScript:openWin('forgotten_clave.aspx','forgot_pass','toolbar=0,location=0,status=0,menubar=0,scrollbars=0,resizable=1,width=570,height=350')"><% = portal.variablesForum.strTxtClickHereForgottenPass %></a><%
      
End If
	  
	  %></td>
     </tr>   
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtAutoLogin %></td>
         <td width="50%" valign="top" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtYes %><input type="radio" name="AutoLogin" value="true" checked />&nbsp;&nbsp;<% = portal.variablesForum.strTxtNo %><input type="radio" name="AutoLogin" value="false" /></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtAddMToActiveUsersList %></td>
         <td width="50%" valign="top" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtYes %><input type="radio" name="NS" value="true" checked />&nbsp;&nbsp;<% = portal.variablesForum.strTxtNo %><input type="radio" name="NS" value="false" /></td>
     </tr><%

'If the image security codeis enabled then show this in the form         
If blnLongSecurityCode Then 
	
	%>
     <tr bgcolor="<% = portal.variablesForum.strTableTitleColour %>">
      <td colspan="2" background="<% = portal.variablesForum.strTableTitleBgImage %>" class="tHeading"><% = portal.variablesForum.strTxtSecurityCodeConfirmation %></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtUniqueSecurityCode %><br /><span class="smText"><% = portal.variablesForum.strTxtCookiesMustBeEnabled %></span></td>
         <td width="50%" valign="top" background="<% = portal.variablesForum.strTableBgImage %>"><img src="security_image.aspx?I=1&<% = hexValue(4) %>" /><img src="security_image.aspx?I=2&<% = hexValue(4) %>" /><img src="security_image.aspx?I=3&<% = hexValue(4) %>" /><img src="security_image.aspx?I=4&<% = hexValue(4) %>" /><img src="security_image.aspx?I=5&<% = hexValue(4) %>" /><img src="security_image.aspx?I=6&<% = hexValue(4) %>" /></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtConfirmSecurityCode %><br /><span class="smText"><% = portal.variablesForum.strTxtEnter6DigitCode %></span></td>
         <td width="50%" valign="top" background="<% = portal.variablesForum.strTableBgImage %>"><input type='text' name="securityCode" size="12" maxlength="12" autocomplete="off" /></td>
     </tr><%
     
End If

%>
     <tr bgcolor="<% = strTableBottomRowColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
       <td valign="top" height="2" colspan="2" align="center" background="<% = portal.variablesForum.strTableBgImage %>">
        <input type="hidden" name="sessionID" value="<% = Session.SessionID %>" />
        <input type='submit' name="Submit" value="<% = portal.variablesForum.strTxtLoginUser %>" />
        <input type="reset" name="Reset" value="<% = portal.variablesForum.strTxtResetForm %>" />
      </td>
     </tr>
    </table>
      </td>
    </tr>
  </table>
  </td>
 </form></tr>
</table>
<br /><br />
<table width="63%" border="0" cellspacing="0" cellpadding="0" align="center">
  <tr>
    <td align="center" class="text"><a href="registration_rules.aspx?FID=<% = portal.variablesForum.intForumID %>" target="_self"><% = strClickHereIfNotRegistered %></a><br />
      <br />
    </tr>
  </table>
  <br />
  <script>document.frmLogin.<% If Request.Form("QUIK") And blnLongSecurityCode Then Response.Write("securityCode") Else Response.Write("name") %>.focus()</script>
 <div align="center"><%
 


'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"

%></div><%


'If the user has unsuccesfully tried logging in before then display a clave incorrect error
If blnIncorrectLogin Then
        Response.Write("<script  language=""JavaScript"">")
        Response.Write("alert('" & portal.variablesForum.strTxtSorryusuarioclaveIncorrect & "\n\n" &  portal.variablesForum.strTxtPleaseTryAgain & "');")
        Response.Write("</script>")

End If

'If the security code did not match
If blnSecurityCodeOK = False Then
        Response.Write("<script  language=""JavaScript"">")
        Response.Write("alert('" & portal.variablesForum.strTxtSecurityCodeDidNotMatch & ".');")
        Response.Write("</script>")
End If
%>

