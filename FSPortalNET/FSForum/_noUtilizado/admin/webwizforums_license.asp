

<%
'If the user has accepted then set the session variable
If Request.Form("Registration") = "Accept" Then
	
	'Set the session variable to true
	Session("blnLicense") = True
	
	'Redirect to admin section
	Response.Redirect("frame_set.aspx")
	
End If

%>
<html>
<head>
<title>Forum Adminstration</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">


<!-- Web Wiz Forums is written and produced by Bruce Corkhill ©2001-2004
     	
     	
<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>

<body background="images/main_bg.gif" />
<div align="center">
 <p><span class="heading">Web Wiz Forums License</span></p>
 <form action="webwizforums_license.aspx" method="post" name="frmLicense" target="_top" id="frmLicense">
  <table width="100%" border="0" cellspacing="0" cellpadding="4">
  <tr> 
   <td align="center"> <textarea name="license" rows="20" cols="85"><!--#include file="includes/License.txt" --></textarea> </td>
  </tr>
  <tr> 
   <td align="center" class="text">By clicking the ACCEPT button you agree to be bound by the terms and conditions of this license.</td>
  </tr>
 </table>
 <table width="98%" border="0" cellspacing="0" cellpadding="1" align="center">
  <tr> 
   <td align="center" class="text"><input type="button" name="Button" value="Cancel" onClick="window.open('../default.aspx', '_top')">
     &nbsp; 
     <input type='submit' name="Registration" value="Accept"> </td>
  </tr>
 </table>
 </form>
 <p><br />
  <span class="smText">This nag screen can be removed by <a href="http://www.webwizguide.info/purchase/default.aspx" target="_blank" class="smLink">purchasing a link removal key for Web Wiz Forums</a>.<br />
  The link removal key will also removed 'Powered By' links from forum pages.</span><br />
 </p>
</div>
</body>
</html>
