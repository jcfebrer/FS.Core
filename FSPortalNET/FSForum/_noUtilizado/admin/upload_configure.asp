

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
Dim strMode		'Holds the mode of the page, set to true if changes are to be made to the database
Dim strUploadComponent	'Holds the upload component to use
Dim strImageTypes	'Holds the image types
Dim intMaxImageSize	'Holds the max image size
Dim strImagePath	'Holds the path to the files
Dim strFileTypes	'Holds the file types
Dim intMaxFileSize	'Holds the max file size
Dim strFilePath		'Holds the path to the files
Dim blnAvatarEnabled	'Set to true if avatars are enabled
Dim strAvatarTypes	'Holds the avatar types
Dim intMaxAvatarSize	'Holds the max avatar size
Dim strAvatarPath	'Holds the path to the avatars

'Read in the details from the form
strUploadComponent = Request.Form("component")
strImageTypes = Request.Form("imageTypes")
intMaxImageSize	= CInt(Request.Form("imageSize"))
portal.variablesForum.strImagePath = Request.Form("imagePath")
strFileTypes = Request.Form("fileTypes")
intMaxFileSize	= CInt(Request.Form("fileSize"))
strFilePath = Request.Form("filePath")
strAvatarTypes = Request.Form("avatarTypes")
intMaxAvatarSize = CInt(Request.Form("avatarSize"))
strAvatarPath = Request.Form("avatarPath")
blnAvatarEnabled = CBool(Request.Form("avatar"))



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

'If the user is changing the upload setup then update the database
If Request.Form("postBack") Then

	With rsCommon
		'Update the recordset
		.Fields("Upload_component") = strUploadComponent
		.Fields("Upload_img_types") = strImageTypes
		.Fields("Upload_img_size") = intMaxImageSize
		.Fields("Upload_img_path") = portal.variablesForum.strImagePath
		.Fields("Upload_files_type") = strFileTypes
		.Fields("Upload_files_size") = intMaxFileSize
		.Fields("Upload_files_path") = strFilePath
		.Fields("Upload_avatar_types") = strAvatarTypes
		.Fields("Upload_avatar_size") = intMaxAvatarSize
		.Fields("Upload_avatar_path") = strAvatarPath
		.Fields("Upload_avatar") = blnAvatarEnabled
	
		'Update the database with the new user's details
		.Update
	
		'Re-run the query to read in the updated recordset from the database
		.Requery
	End With
	
	'Empty the application level variable so that the changes made are seen in the main forum
	Application("blnConfigurationSet") = false
End If

'Read in the deatils from the database
If NOT rsCommon.EOF Then

	'Read in the e-mail setup from the database
	strUploadComponent = rsCommon("Upload_component")
	strImageTypes = rsCommon("Upload_img_types")
	intMaxImageSize	= CInt(rsCommon("Upload_img_size"))
	portal.variablesForum.strImagePath = rsCommon("Upload_img_path")
	strFileTypes = rsCommon("Upload_files_type")
	intMaxFileSize	= CInt(rsCommon("Upload_files_size"))
	strFilePath = rsCommon("Upload_files_path")
	strAvatarTypes = rsCommon("Upload_avatar_types")
	intMaxAvatarSize = CInt(rsCommon("Upload_avatar_size"))
	strAvatarPath = rsCommon("Upload_avatar_path")
	blnAvatarEnabled = CBool(rsCommon("Upload_avatar"))
End If


'Release Server Objects
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
<html>
<head>
<title>Image and File Upload Configuration</title>


<!-- Web Wiz Forums is written and produced by Bruce Corkhill ©2001-2004
     	

<!-- Check the from is filled in correctly before submitting -->
<script  language="javascript">
<!-- Hide from older browsem_rs...

//Function to check form is filled in correctly before submitting
function CheckForm () {


	//Check for a image types name
	if (document.frmUpload.imageTypes.value==""){
		alert("Please enter Image file types to upload");
		document.frmUpload.imageTypes.focus();
		return false;
	}

	//Check for a path to image upload folder
	if (document.frmUpload.imagePath.value==""){
		alert("Please enter the Path to upload images to");
		document.frmUpload.imagePath.focus();
		return false;
	}

	//Check for a file types name
	if (document.frmUpload.fileTypes.value==""){
		alert("Please enter File types to upload");
		document.frmUpload.fileTypes.focus();
		return false;
	}

	//Check for a file to image upload folder
	if (document.frmUpload.filePath.value==""){
		alert("Please enter the Path to upload files to");
		document.frmUpload.filePath.focus();
		return false;
	}
	
	//Check for a avatar types name
	if (document.frmUpload.avatarTypes.value==""){
		alert("Please enter Avatar types to upload");
		document.frmUpload.avatarTypes.focus();
		return false;
	}

	//Check for a file to image upload folder
	if (document.frmUpload.avatarPath.value==""){
		alert("Please enter the Path to upload avatars to");
		document.frmUpload.avatarPath.focus();
		return false;
	}



	return true
}
// -->
</script>
<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"><span class="heading">Image and File Upload Configuration</span><br /> 
<a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
 <br />
 <table width="97%" border="0" cellspacing="1" cellpadding="4" bgcolor="#000000">
  <tr> 
   <td align="center" bgcolor="#CCCEE6" class="lgText"> Important - Please Read First!</td>
  </tr>
  <tr> 
   <td bgcolor="#EAEAF4"> 
    <p class="text">To be able to use file and image upload in your forums, you must have an upload component installed on the web server, if you are unsure about this check with your 
     web hosts, if they have any of the upload components mentioned below installed.<br />
     <br />
     If you run the web server yourself then you could download and install one of the following supported components.<br />
     <br />
     You will also need to make sure that the upload folder has write permissions and is inside the root of your website.</p>
    <ul>
     <li class="text"><span class="bold">Persits AspUpload</span> 2.x or above<br />
      Component available form <a href="http://www.aspxupload.com" target="_blank">www.aspxupload.com</a></li>
     <li class="text"><span class="bold">Dundas Upload</span> 2.0<br />
      Free component available from <a href="http://www.dundas.com" target="_blank">www.dundas.com</a></li>
     <li class="text"><span class="bold">SoftArtisans FileUp</span> 3.2 or above (<span class="bold">SA FileUp</span>)<br />
      Component available form <a href="http://www.softartisans.com" target="_blank">www.softartisans.com</a></li>
     <li class="text"><span class="bold">aspSmartUpload</span><br />
      Free component available from <a href="http://www.aspxsmart.com/" target="_blank">http://www.aspxsmart.com</a></li>
     <li class="text"><span class="bold">AspSimpleUpload</span><br />
      Free component available from <a href="http://www.aspxhelp.com/" target="_blank">http://www.aspxhelp.com</a></li>
    </ul>
    <p class="text"><span class="bold">Please note</span>: - The ASP<span class="bold"> File System Object</span> (FSO) is also required when using upload features, check with your web hosting company that 
     they have not disabled this object.</p></td>
  </tr>
 </table>
</div><br />
<form method="post" name="frmUpload" action="upload_configure.aspx" onSubmit="return CheckForm();">
 <table width="560" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000" height="277">
  <tr> 
   <td height="234" width="560"> <table width="100%" border="0" align="center" height="221" cellpadding="4" cellspacing="1">
     <tr align="left" bgcolor="#CCCEE6"> 
      <td height="30" colspan="2" class="text">*Indicates required fields</td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="12" align="left" bgcolor="#F5F5FA" class="text">Upload Component to use:<br /> <span class="smText">Check with your web hosting company which component, if any they support. Free web hosts usually 
       won't support any.</span></td>
      <td width="41%" height="12" valign="top"> 
       <select name="component">
        <option value="AspUpload"<% If strUploadComponent = "AspUpload" Then Response.Write(" selected") %>>Persits AspUpload</option>
        <option value="Dundas"<% If strUploadComponent = "Dundas" Then Response.Write(" selected") %>>Dundas Upload</option>
        <option value="fileUp"<% If strUploadComponent = "fileUp" Then Response.Write(" selected") %>>SA FileUp</option>
        <option value="aspSmart"<% If strUploadComponent = "aspSmart" Then Response.Write(" selected") %>>aspSmartUpload</option>
        <option value="AspSimple"<% If strUploadComponent = "AspSimple" Then Response.Write(" selected") %>>AspSimpleUpload</option>
       </select> </td>
     </tr>
     <tr  bgcolor="#CCCEE6"> 
      <td  height="12" colspan="2" align="left" class="tHeading">Image Upload</td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="12" align="left" class="text">Image Types*<br /> <span class="smText">Place the types of images that can be upload in posts. Separate the different image 
       types with a semi-colon.<br />
       eg. jpg;jpeg;gif;png</span></td>
      <td width="41%" height="12" valign="top"> 
       <input name="imageTypes" type='text' id="imageTypes" value="<% = strImageTypes %>" size="30" maxlength="50" > </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="2" align="left" class="text">Maximum Image File Size<br /> <span class="smText">This is the maximum file size of images in Kilobytes.</span></td>
      <td width="41%" height="2" valign="top" class="text"> 
       <select name="imageSize" id="imageSize">
        <option<% If intMaxImageSize = 5 Then Response.Write(" selected") %>>5</option>
        <option<% If intMaxImageSize = 10 Then Response.Write(" selected") %>>10</option>
        <option<% If intMaxImageSize = 15 Then Response.Write(" selected") %>>15</option>
        <option<% If intMaxImageSize = 20 Then Response.Write(" selected") %>>20</option>
        <option<% If intMaxImageSize = 25 Then Response.Write(" selected") %>>25</option>
        <option<% If intMaxImageSize = 30 Then Response.Write(" selected") %>>30</option>
        <option<% If intMaxImageSize = 35 Then Response.Write(" selected") %>>35</option>
        <option<% If intMaxImageSize = 40 Then Response.Write(" selected") %>>40</option>
        <option<% If intMaxImageSize = 45 Then Response.Write(" selected") %>>45</option>
        <option<% If intMaxImageSize = 50 Then Response.Write(" selected") %>>50</option>
        <option<% If intMaxImageSize = 55 Then Response.Write(" selected") %>>55</option>
        <option<% If intMaxImageSize = 60 Then Response.Write(" selected") %>>60</option>
        <option<% If intMaxImageSize = 65 Then Response.Write(" selected") %>>65</option>
        <option<% If intMaxImageSize = 70 Then Response.Write(" selected") %>>70</option>
        <option<% If intMaxImageSize = 75 Then Response.Write(" selected") %>>75</option>
        <option<% If intMaxImageSize = 80 Then Response.Write(" selected") %>>80</option>
        <option<% If intMaxImageSize = 85 Then Response.Write(" selected") %>>85</option>
        <option<% If intMaxImageSize = 90 Then Response.Write(" selected") %>>90</option>
        <option<% If intMaxImageSize = 95 Then Response.Write(" selected") %>>95</option>
        <option<% If intMaxImageSize = 100 Then Response.Write(" selected") %>>100</option>
        <option<% If intMaxImageSize = 125 Then Response.Write(" selected") %>>125</option>
        <option<% If intMaxImageSize = 150 Then Response.Write(" selected") %>>150</option>
        <option<% If intMaxImageSize = 175 Then Response.Write(" selected") %>>175</option>
        <option<% If intMaxImageSize = 200 Then Response.Write(" selected") %>>200</option>
        <option<% If intMaxImageSize = 250 Then Response.Write(" selected") %>>250</option>
        <option<% If intMaxImageSize = 500 Then Response.Write(" selected") %>>500</option>
       </select>
       KB </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="23" align="left" class="text">Image Upload Folder Path*<br />
       <span class="smText">This is the path from the forum files to the folder that images will be upload in. The folder must be inside of the root of your web site and have <strong>write permissions enabled</strong>.<br />
       eg. ../images/upload</span><br /> </td>
      <td width="41%" height="23" valign="top"> 
       <input name="imagePath" type='text' id="imagePath" value="<% = portal.variablesForum.strImagePath %>" size="30" maxlength="50"> &nbsp;</td>
     </tr>
     <tr  bgcolor="#CCCEE6"> 
      <td  height="7" colspan="2" align="left" class="tHeading">File Upload</td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="13" align="left" class="text">File Types*<br /> <span class="smText">Place the types of files that can be upload in posts. Separate the different file types with a semi-colon.<br />
       eg. zip;rar</span></td>
      <td width="41%" height="13" valign="top" class="text"> 
       <input name="fileTypes" type='text' id="fileTypes" value="<% = strFileTypes %>" size="30" maxlength="50" > </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td  height="13" align="left" class="text">Maximum File Size<br /> <span class="smText">This is the maximum file size of files in Kilobytes.<br />
       The max size is 2000KB as many components won't allow files above this size.</span></td>
      <td height="13" valign="top" class="text"> 
       <select name="fileSize" id="fileSize">
        <option<% If intMaxFileSize = 10 Then Response.Write(" selected") %>>10</option>
        <option<% If intMaxFileSize = 20 Then Response.Write(" selected") %>>20</option>
        <option<% If intMaxFileSize = 30 Then Response.Write(" selected") %>>30</option>
        <option<% If intMaxFileSize = 40 Then Response.Write(" selected") %>>40</option>
        <option<% If intMaxFileSize = 50 Then Response.Write(" selected") %>>50</option>
        <option<% If intMaxFileSize = 60 Then Response.Write(" selected") %>>60</option>
        <option<% If intMaxFileSize = 80 Then Response.Write(" selected") %>>80</option>
        <option<% If intMaxFileSize = 100 Then Response.Write(" selected") %>>100</option>
        <option<% If intMaxFileSize = 125 Then Response.Write(" selected") %>>125</option>
        <option<% If intMaxFileSize = 150 Then Response.Write(" selected") %>>150</option>
        <option<% If intMaxFileSize = 200 Then Response.Write(" selected") %>>200</option>
        <option<% If intMaxFileSize = 250 Then Response.Write(" selected") %>>250</option>
        <option<% If intMaxFileSize = 300 Then Response.Write(" selected") %>>300</option>
        <option<% If intMaxFileSize = 400 Then Response.Write(" selected") %>>400</option>
        <option<% If intMaxFileSize = 500 Then Response.Write(" selected") %>>500</option>
        <option<% If intMaxFileSize = 750 Then Response.Write(" selected") %>>750</option>
        <option<% If intMaxFileSize = 1000 Then Response.Write(" selected") %>>1000</option>
        <option<% If intMaxFileSize = 1500 Then Response.Write(" selected") %>>1500</option>
        <option<% If intMaxFileSize = 2000 Then Response.Write(" selected") %>>2000</option>
       </select>
       KB </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td  height="13" align="left" class="text">File Upload Folder Path* <br />
       <span class="smText">This is the path from the forum files to the folder that files will be upload in. The folder must be inside of the root of your web site and have <strong>write permissions enabled</strong>.<br />
       eg. ../images/upload</span></td>
      <td height="13" valign="top" class="text"> 
       <input name="filePath" type='text' id="filePath" value="<% = strFilePath %>" size="30" maxlength="50"></td>
     </tr>
     <tr  bgcolor="#CCCEE6"> 
      <td  height="13" colspan="2" align="left" class="tHeading">Avatar Upload<br />
       <span class="smText">Make sure you have also enabled Avatar Images from the <a href="forum_configure.aspx" target="_self" class="smLink">Forum Configuration</a> page.<br />
       <strong>For extra security avatars can only be uploaded once a user is registered, by editing their profile.</strong></span></td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td  height="13" align="left" class="text">Enable Avatar Uploading</td>
      <td height="13" valign="top" class="text">On 
       <input type="radio" name="avatar" value="True" <% If blnAvatarEnabled  Then response.write("checked" %>> &nbsp;&nbsp;Off 
       <input type="radio" name="avatar" value="False" <% If blnAvatarEnabled = False Then response.write("checked" %>></td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td  height="13" align="left" class="text">Avatar Image Types*<br /> <span class="smText">Place the types of images that can be upload in posts. Separate the different image types with a semi-colon.<br />
       eg. jpg;jpeg;gif;png</span></td>
      <td height="13" valign="top" class="text"> 
       <input name="avatarTypes" type='text' id="avatarTypes" value="<% = strAvatarTypes %>" size="30" maxlength="50" ></td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td  height="13" align="left" class="text">Maximum Avatar Image File Size<br /> <span class="smText">This is the maximum file size of images in Kilobytes.</span></td>
      <td height="13" valign="top" class="text"> 
       <select name="avatarSize" id="avatarSize">
        <option<% If intMaxAvatarSize = 5 Then Response.Write(" selected") %>>5</option>
        <option<% If intMaxAvatarSize = 10 Then Response.Write(" selected") %>>10</option>
        <option<% If intMaxAvatarSize = 15 Then Response.Write(" selected") %>>15</option>
        <option<% If intMaxAvatarSize = 20 Then Response.Write(" selected") %>>20</option>
        <option<% If intMaxAvatarSize = 25 Then Response.Write(" selected") %>>25</option>
        <option<% If intMaxAvatarSize = 30 Then Response.Write(" selected") %>>30</option>
        <option<% If intMaxAvatarSize = 35 Then Response.Write(" selected") %>>35</option>
        <option<% If intMaxAvatarSize = 40 Then Response.Write(" selected") %>>40</option>
        <option<% If intMaxAvatarSize = 45 Then Response.Write(" selected") %>>45</option>
        <option<% If intMaxAvatarSize = 50 Then Response.Write(" selected") %>>50</option>
        <option<% If intMaxAvatarSize = 55 Then Response.Write(" selected") %>>55</option>
        <option<% If intMaxAvatarSize = 60 Then Response.Write(" selected") %>>60</option>
        <option<% If intMaxAvatarSize = 65 Then Response.Write(" selected") %>>65</option>
        <option<% If intMaxAvatarSize = 70 Then Response.Write(" selected") %>>70</option>
        <option<% If intMaxAvatarSize = 75 Then Response.Write(" selected") %>>75</option>
        <option<% If intMaxAvatarSize = 80 Then Response.Write(" selected") %>>80</option>
        <option<% If intMaxAvatarSize = 85 Then Response.Write(" selected") %>>85</option>
        <option<% If intMaxAvatarSize = 90 Then Response.Write(" selected") %>>90</option>
        <option<% If intMaxAvatarSize = 95 Then Response.Write(" selected") %>>95</option>
        <option<% If intMaxAvatarSize = 100 Then Response.Write(" selected") %>>100</option>
        <option<% If intMaxAvatarSize = 125 Then Response.Write(" selected") %>>125</option>
        <option<% If intMaxAvatarSize = 150 Then Response.Write(" selected") %>>150</option>
        <option<% If intMaxAvatarSize = 175 Then Response.Write(" selected") %>>175</option>
        <option<% If intMaxAvatarSize = 200 Then Response.Write(" selected") %>>200</option>
        <option<% If intMaxAvatarSize = 250 Then Response.Write(" selected") %>>250</option>
        <option<% If intMaxAvatarSize = 500 Then Response.Write(" selected") %>>500</option>
       </select>
       Kb </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="13" align="left" class="text">Avatar Image Upload Folder Path*<br />
       <span class="smText">This is the path from the forum files to the folder that images will be upload in. The folder must be inside of the root of your web site and have <strong>write permissions enabled</strong>.<br />
       eg. ../images/upload</span></td>
      <td width="41%" height="13" valign="top" class="text"> 
       <input name="avatarPath" type='text' id="avatarPath" value="<% = strAvatarPath %>" size="30" maxlength="50"> </td>
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