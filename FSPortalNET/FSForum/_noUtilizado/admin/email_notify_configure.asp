

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'****************************************************************************************
'**  Copyright Notice
'**
'**  Web Wiz Guide - Web Wiz Forums
'**
'**  Copyright 2001-2004 Bruce Corkhill All Rights Reserved.
'**
'**  This program is free software; you can modify (at your own risk) any part of it
'**  under the terms of the License that accompanies this software and use it both
'**  privately and commercially.
'**
'**  All copyright notices must remain in tacked in the scripts and the
'**  outputted HTML.
'**
'**  You may use parts of this program in your own private work, but you may NOT
'**  redistribute, repackage, or sell the whole or any part of this program even
'**  if it is modified or reverse engineered in whole or in part without express
'**  permission from the Usuarios.
'**
'**  You may not pass the whole or any part of this application off as your own work.
'**
'**  All links to Web Wiz Guide and powered by logo's must remain unchanged and in place
'**  and must remain visible when the pages are viewed unless permission is first granted
'**  by the copyright holder.
'**
'**  This program is distributed in the hope that it will be useful,
'**  but WITHOUT ANY WARRANTY; without even the implied warranty of
'**  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE OR ANY OTHER
'**  WARRANTIES WHETHER EXPRESSED OR IMPLIED.
'**
'**  You should have received a copy of the License along with this program;
'**  if not, write to:- Web Wiz Guide, PO Box 4982, Bournemouth, BH8 8XP, United Kingdom.
'**
'**
'**  No official support is available for this program but you may post support questions at: -
'**  http://www.webwizguide.info/forum
'**
'**  Support questions are NOT answered by email ever!
'**
'**  For correspondence or non support questions contact: -
'**  info@webwizguide.info
'**
'**  or at: -
'**
'**  Web Wiz Guide, PO Box 4982, Bournemouth, BH8 8XP, United Kingdom
'**
'****************************************************************************************

'Set the response buffer to true
Response.Buffer = True


'Dimension variables
Dim strMode			'Holds the mode of the page, set to true if changes are to be made to the database
Dim strMailComponent		'Holds the mail component
Dim strMailServer		'Holds the outgoing mail server
Dim strWebSiteName		'Holds the web site name
Dim strForumPath 		'Holds the forum path
Dim strAdminEmail 		'Holds the forum adminsters email
Dim blnEmailNotify		'Set to true to turn email notify on
Dim blnSendPost			'Set to true if the if the user wants the forum posts sent with the mail notify
Dim blnMailActivate		'Set to true if the user wants membership to be activated by email
Dim blnEmailClient		'set to true if the email client is enalbed

'Initialise variables
portal.variablesForum.blnEmailNotify = False

'Read in the details from the form
strMailComponent = Request.Form("component")
strMailServer = Request.Form("mailServer")
strWebSiteName = Request.Form("siteName")
strForumPath = Request.Form("forumPath")
strAdminEmail = Request.Form("email")
portal.variablesForum.blnEmailNotify = CBool(Request.Form("userNotify"))
blnSendPost = CBool(Request.Form("sendPost"))
blnMailActivate = CBool(Request.Form("mailActvate"))
portal.variablesForum.blnEmailClient = CBool(Request.Form("client"))



'Initialise the SQL variable with an SQL statement to get the configuration details from the database
If portal.variablesForum.strDatabaseType = "SQLServer" Then
	strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "SelectConfiguration"
Else
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Configuration.* From " & portal.variablesForum.strDbTable & "Configuration;"
End If

'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
rsCommon.CursorType = 2

'Set the Lock Type for the records so that the record set is only locked when it is updated
rsCommon.LockType = 3

'Query the database
rsCommon=db.execute(strSQL)

'If the user is changing the email setup then update the database
If Request.Form("postBack") Then

	With rsCommon
		'Update the recordset
		.Fields("mail_component") = strMailComponent
		.Fields("mail_server") = strMailServer
		.Fields("website_name") = strWebSiteName
		.Fields("forum_path") = strForumPath
		.Fields("forum_email_address") = strAdminEmail
		.Fields("email_notify") = portal.variablesForum.blnEmailNotify
		.Fields("Email_post") = blnSendPost
		.Fields("Email_activate") = blnMailActivate
		.Fields("Email_sys") = portal.variablesForum.blnEmailClient
	
	
		'Update the database with the new user's details
		.Update
	
		'Re-run the query to read in the updated recordset from the database
		.Requery
	End With
	
	'Update variables
	Application("strMailComponent") = strMailComponent
	Application("strOutgoingMailServer") = strMailServer
	Application("strWebsiteName") = strWebSiteName
	Application("strForumPath") = strForumPath
	Application("strForumEmailAddress") = strAdminEmail
	Application("blnEmail") = portal.variablesForum.blnEmailNotify
	Application("blnSendPost") = blnSendPost
	Application("blnEmailActivation") = blnMailActivate
	Application("blnEmailMessenger") = portal.variablesForum.blnEmailClient
	
	'Empty the application level variable so that the changes made are seen in the main forum
	Application("blnConfigurationSet") = false
End If

'Read in the deatils from the database
If NOT rsCommon.EOF Then

	'Read in the e-mail setup from the database
	strMailComponent = rsCommon("mail_component")
	strMailServer = rsCommon("mail_server")
	strWebSiteName = rsCommon("website_name")
	strForumPath = rsCommon("forum_path")
	strAdminEmail = rsCommon("forum_email_address")
	portal.variablesForum.blnEmailNotify = CBool(rsCommon("email_notify"))
	blnSendPost = CBool(rsCommon("Email_post"))
	blnMailActivate = CBool(rsCommon("Email_activate"))
	portal.variablesForum.blnEmailClient = CBool(rsCommon("Email_sys"))
End If


'Release Server Objects
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
<html>
<head>
<title>E-mail Notification Configuration</title>



     	

<!-- Check the from is filled in correctly before submitting -->
<script  language="javascript">
<!-- Hide from older browsem_rs...

//Function to check form is filled in correctly before submitting
function CheckForm () {

	//Check for a mail server
	if (((document.frmEmailsetup.component.value=="AspEmail") || (document.frmEmailsetup.component.value=="Jmail")) && (document.frmEmailsetup.mailServer.value=="")){
		alert("Please enter an working incoming mail server \nWithout one the Jmail/AspEmail component will fail");
		document.frmEmailsetup.mailServer.focus();
		return false;
	}

	//Check for a website name
	if (document.frmEmailsetup.siteName.value==""){
		alert("Please enter your Website Name");
		document.frmEmailsetup.siteName.focus();
		return false;
	}

	//Check for a path to the forum
	if (document.frmEmailsetup.forumPath.value==""){
		alert("Please enter the Web Address path to the Forum");
		document.frmEmailsetup.forumPath.focus();
		return false;
	}

	//Check for an email address
	if (document.frmEmailsetup.email.value==""){
		alert("Please enter your E-mail Address");
		document.frmEmailsetup.email.focus();
		return false;
	}

	//Check that the email address is valid
	if (document.frmEmailsetup.email.value.length>0&&(document.frmEmailsetup.email.value.indexOf("@",0)==-1||document.frmEmailsetup.email.value.indexOf(".",0)==-1)) {
		alert("Please enter your valid E-mail address\nWithout a valid email address the email notification will not work");
		document.frmEmailsetup.email.focus();
		return false;
	}



	return true
}
// -->
</script>
<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"><span class="heading">Email Notification Configuration</span><br /> 
<a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
 <br />
 <table width="97%" border="0" cellspacing="1" cellpadding="4" bgcolor="#000000">
  <tr> 
   <td align="center" bgcolor="#CCCEE6" class="lgText"> Important - Please Read First!</td>
  </tr>
  <tr> 
   <td bgcolor="#EAEAF4"> 
    <p class="text">To be able to use the email notification you need to have either <span class="bold">CDONTS</span>, <span class="bold">CDOSYS</span>, <span class="bold">W3 JMail</span>, 
     <span class="bold">Persists AspEmail</span>, or <span class="bold">SeverObject AspMail</span> component installed on the web server. <br />
     <br />
     Check with your web hosts as to which they have installed, free web hosts usually don't have any installed.<br />
     <br />
     <b>Windows Win2k, Win XP Pro and Win2003 users</b> - CDOSYS comes installed on Windows 2000, Windows XP Pro, and Windows 2003.<br />
     <br />
     <b>Windows NT4 and Win2k users</b> - IIS 4 and 5 on NT4 and Win2k installs the CDONTS email component by default, but you need the SMTP server that comes with IIS installed on the web server as well 
     (This is the email component that most web hosts will use).<br />
     <br />
     <b>Windows 9x users</b> - I'm afraid Windows 98 does not support the CDOSYS or CDONTS email components so if you enable this feature and try to test it on a Windows 9x system the Guestbook will crash!!<br />
     <br />
     The personal version of the JMail email component is free and can run under Win98, NT4, and Win2k, Win XP, but you must install the component on the web server and requires that you enter the address 
     of a working SMTP server.<br />
     <br />
     If your forum crashes after enabling email notification, you may have the incorrect settings, check with your hosting company as to the correct settings. Some web hosts don't allow the sending of emails 
     to non local email addresses.</p></td>
  </tr>
 </table>
</div>
<br />
<form method="post" name="frmEmailsetup" action="email_notify_configure.aspx" onSubmit="return CheckForm();">
 <table width="560" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000" height="277">
  <tr> 
   <td height="234" width="560"> <table width="100%" border="0" align="center" height="151" cellpadding="4" cellspacing="1">
     <tr align="left" bgcolor="#CCCEE6"> 
      <td height="30" colspan="2" class="text">*Indicates required fields</td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="12" align="left" class="text">Email Component to use:<br />
       <span class="smText">You must have the component you select installed on the web server, check with your web host on which they have. Usually none are available with free web hosting.</span></td>
      <td width="41%" height="12" valign="top"> 
       <select name="component">
        <option value="CDOSYS"<% If strMailComponent = "CDOSYS" Then Response.Write(" selected") %>>CDOSYS (Win2k/2k3/XP Pro)</option>
        <option value="CDONTS" <% If strMailComponent = "CDONTS" OR strMailComponent = "" Then Response.Write(" selected") %>>CDONTS (NT4/Win2k)</option>
        <option value="Jmail"<% If strMailComponent = "Jmail" Then Response.Write(" selected") %>>JMail</option>
        <option value="AspEmail"<% If strMailComponent = "AspEmail" Then Response.Write(" selected") %>>AspEmail</option>
        <option value="AspMail"<% If strMailComponent = "AspMail" Then Response.Write(" selected") %>>AspMail</option>
       </select> </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="12" align="left" class="text">Outgoing SMTP Mail Server (<b>NOT needed for CDONTS</b>):<br />
       <span class="smText">You only need this if you are using an email component other than CDONTS. It must be a working mail server or the forum will crash.</span></td>
      <td width="41%" height="12" valign="top"> 
       <input type='text' name="mailServer" maxlength="50" value="<% If strMailServer <> "" Then Response.Write(strMailServer) Else Response.Write("localhost") %>" size="30" > <br /> <span class="text">(eg. mail.myweb.com)</span></td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="12" align="left" class="text">Website name*<br />
       <span class="smText">The name of your website or Forum <br />
       eg. My Website</span></td>
      <td width="41%" height="12" valign="top"> 
       <input type='text' name="siteName" maxlength="50" value="<% = strWebsiteName %>" size="30" > </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="2" align="left" class="text">Web address path to forum*<br />
       <span class="smText">The web address that you would type into your web browsers address bar inorder to get to the forum. <br />
       eg. http://www.mywebsite.com/forum </span></td>
      <td width="41%" height="2" valign="top"> 
       <input type='text' name="forumPath" maxlength="50" value="<% = strForumPath %>" size="30" > </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="23" align="left" class="text">Your Email Address* <br /> <span class="smText">Without a valid email address you wont be able to send emails from the forum or receive email 
       notification yourself</span>.<br /> </td>
      <td width="41%" height="23" valign="top"> 
       <input type='text' name="email" maxlength="50" value="<% = strAdminEmail %>" size="30"> &nbsp;</td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="7" align="left" class="text">Email Notify<br /> <span class="smText">Allows users to receive notification of replies to their posts or new posts in forums.</span></td>
      <td width="41%" height="7" valign="top" class="text">On 
       <input type="radio" name="userNotify" value="True" <% If portal.variablesForum.blnEmailNotify = True Then response.write("checked" %>> &nbsp;&nbsp;Off 
       <input type="radio" name="userNotify" value="False" <% If portal.variablesForum.blnEmailNotify = False Then response.write("checked" %>> </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="13" align="left" class="text">Send Post with E-mail Notification<br /> <span class="smText">Allow the full message that has been posted in the forum to be sent with the email 
       notification.</span></td>
      <td width="41%" height="13" valign="top" class="text">On 
       <input type="radio" name="sendPost" value="True" <% If blnSendPost = True Then response.write("checked" %>> &nbsp;&nbsp;Off 
       <input type="radio" name="sendPost" value="False" <% If blnSendPost = False Then response.write("checked" %>> </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td  height="13" align="left" class="text">Email Activation of Membership<br /> <span class="smText">If you enable this new members will be sent an email containing a link that they will need to click 
       on to activate their forum membership.</span></td>
      <td height="13" valign="top" class="text">On 
       <input type="radio" name="mailActvate" value="True" <% If blnMailActivate = True Then response.write("checked" %>> &nbsp;&nbsp;Off 
       <input type="radio" name="mailActvate" value="False" <% If blnMailActivate = False Then response.write("checked" %>></td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="13" align="left" class="text">Built in Email Client<br />
       <span class="smText">The built in email client allows members to send emails to other forum members directly from the forum, as long as both parties have a valid email address in their profile.</span></td>
      <td width="41%" height="13" valign="top" class="text">On 
       <input type="radio" name="client" value="True" <% If portal.variablesForum.blnEmailClient = True Then response.write("checked" %>> &nbsp;&nbsp;Off 
       <input type="radio" name="client" value="False" <% If portal.variablesForum.blnEmailClient = False Then response.write("checked" %>> </td>
     </tr>
     <tr bgcolor="#F5F5FA" align="center"> 
      <td height="2" colspan="2" valign="top" > 
       <p> 
        <input type="hidden" name="postBack" value="true">
        <input type='submit' name="Submit" value="Update Details">
        <input type="reset" name="Reset" value="Reset Form">
       </p></td>
     </tr>
    </table></td>
  </tr>
 </table>
</form>
<br />
</body>
</html>