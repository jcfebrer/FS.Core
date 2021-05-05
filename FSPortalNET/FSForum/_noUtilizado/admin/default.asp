
<!--#include file="functions/functions_common.aspx" -->
<%
Response.Buffer = True


'Make sure this page is not cached
Response.Expires = -1
Response.ExpiresAbsolute = Now() - 2
Response.AddHeader "pragma","no-cache"
Response.AddHeader "cache-control","private"
Response.CacheControl = "No-Store"


'Dimension variables
Dim lngLoopCounter		'Holds the loop counter

'Initliase variable
Session("lngSecurityCode") = ""
	        
'Create a new session security code
For lngLoopCounter = 1 to 6
	        	
	'Randomise the system timer
	Randomize Timer
			
	'Place the random number onto the end of teh security code session variable
	Session("lngSecurityCode") = Session("lngSecurityCode") & CStr(CInt(Rnd * 9))
Next
%>  
<html>
<head>
<title>Forum Adminstration</title>
<meta name="copyright" content="Copyright (C) 2001-2004 Bruce Corkhill">

<!-- The Web Wiz Guide Login Script is written by Bruce Corkhill ©2001-2004
     	If you want your own  Login Script then goto http://www.webwizforums.com -->

<!-- Check the from is filled in correctly before submitting -->
<script language="javascript">

//Function to check form is filled in correctly before submitting
function CheckForm () {

	var errorMsg = "";

	//Check for a usuario
	if (document.frmLogin.name.value==""){
		errorMsg += "\n\tusuario \t- Enter the Administrator Forum usuario"; 	
	}
	
	//Check for a clave
	if (document.frmLogin.clave.value==""){
		errorMsg += "\n\tclave \t- Enter the Administrator Forum clave";
	}
	
	//Check for a security code
        if (document.frmLogin.securityCode.value==""){
                errorMsg += "\n\tSecurity Code \t- You must enter the 6 digit security code";
        }
	
	//If there is aproblem with the form then display an error
	if (errorMsg != ""){
		msg = "_____________________________________________________________________\n\n";
		msg += "Your Login to the Forum Admin has failed because there are problem(s) with the form.\n";
		msg += "Please correct the problem(s) and re-submit the form.\n";
		msg += "_____________________________________________________________________\n\n";
		msg += "The following field(s) need to be corrected: -\n";
		
		errorMsg += alert(msg + errorMsg + "\n\n");
		return false;
	}
	
	return true;
}
</script>
<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<table width="518" border="0" cellspacing="0" cellpadding="0" align="center">
  <tr> 
    
  <td align="center" class="heading"> Forum Adminstration Login</td>
  </tr>
</table>
<div align="center"><a href="../default.aspx" target="_top">Return to the Main Forum</a> <br />
 <br />
 <span class="text">If you have already logged into the main forum as the Administrator then just <a href="frame_set.aspx" target="_self">Click Here</a></span><br />
  <br /> 
 <table width="98%" border="0" cellspacing="0" cellpadding="1" bgcolor="#999999" align="center">
 <tr><form method="post" name="frmLogin" action="frame_set.aspx" onSubmit="return CheckForm();" onReset="return confirm('Are you sure you want to reset the form?');">
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
    <tr>
     <td bgcolor="#A6AAD3">
      <table width="100%" border="0" cellspacing="1" cellpadding="3" bgcolor="#FFFFFF">
         <tr bgcolor="#CCCEE6"> 
          <td colspan="2" class="tHeading">Forum Adminstration Login</td>
         </tr>
         <tr bgcolor="#F5F5FA" background=""> 
          <td colspan="2" bgcolor="#F5F5FA" background="" class="text">*Indicates required fields</td>
         </tr>
         <tr bgcolor="#F5F5FA" background="" > 
          <td width="50%"  bgcolor="#F5F5FA" background="" class="text">Admin usuario*</td>
          <td width="50%" bgcolor="#F5F5FA" background="" class="text"><input type='text' name="name" id="name" size="15" maxlength="15" value="" /></td>
         </tr>
         <tr bgcolor="#F5F5FA" background=""> 
          <td width="50%"  bgcolor="#F5F5FA" background="" class="text">Admin clave*</td>
          <td width="50%" valign="top"> <input type="clave" name="clave" id="clave" size="15" maxlength="15" value="" />
          </td>
         </tr>
         <tr bgcolor="#CCCEE6"> 
          <td colspan="2" class="tHeading">Security Code Confirmation (required)</td>
         </tr>
         <tr bgcolor="#F5F5FA" background=""> 
          <td width="50%" class="text" background="">Unique Security Code<br />
           <span class="smText">Cookies must be enabled on your web browser to see images. </span></td>
          <td width="50%" valign="top" background=""><img src="security_image.aspx?I=1&<% = hexValue(4) %>" /><img src="security_image.aspx?I=2&<% = hexValue(4) %>" /><img src="security_image.aspx?I=3&<% = hexValue(4) %>" /><img src="security_image.aspx?I=4&<% = hexValue(4) %>" /><img src="security_image.aspx?I=5&<% = hexValue(4) %>" /><img src="security_image.aspx?I=6&<% = hexValue(4) %>" /></td>
         </tr>
         <tr bgcolor="#F5F5FA" background=""> 
          <td width="50%" class="text" background="">Confirm Security Code* <br />
           <span class="smText">Please enter the 6 digit code shown above in image format.<br />
           Only numbers are allowed, a '0' is a numerical zero.</span></td>
          <td width="50%" valign="top" background=""><input type='text' name="securityCode" size="12" maxlength="12" autocomplete="off" /></td>
         </tr>
         <tr bgcolor="#E6E7F2" background=""> 
          <td valign="top" height="2" colspan="2" align="center" background=""><input type="hidden" name="sessionID" value="<% = Session.SessionID %>" /><input type='submit' name="Submit" value="Forum Login" /> <input type="reset" name="Reset" value="Reset Form" /> 
          </td>
         </tr>
        </table>
      </td>
    </tr>
  </table>
  </td>
 </form></tr>
</table>
</div>
<center>
 <p class="text">Use the same Administration usuario and clave as you use to login to the main forum<br />
  <br />
  If you have forgotten your clave then use the forgotten passsword form in the main forum to <br />
  email it to yourself as long as you have the email notification function turned on<br />
  <br />
  <a href="http://www.webwizguide.info" target="_blank"><img src="../forum_images/web_wiz_guide.gif" border="0" alt="Web Wiz Guide!"></a> </p>
</center>
</body>
</html>
