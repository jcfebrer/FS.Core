

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include file="functions/functions_upload.aspx" -->

<%
'Set the timeout of the page
Server.ScriptTimeout =  1000


'Set the response buffer to true as we maybe redirecting
Response.Buffer = True


'Declare variables
Dim strFileTypes	'Holds the file types allowed to be uploaded
Dim intMaxFileSize	'Holds the largest file size
Dim intForumID		'Holds the forum ID
Dim strMessageBoxType	'Holds the message box teype to return to
Dim blnFileUploaded	'Set to true if the file is uploaded
Dim strErrorMessage	'Holds the error emssage if the file is not uploaded
Dim strFileUploadPath	'Holds the path and folder the uploaded files are stored in
Dim saryFileUploadTypes	'Array holding the file types allowed to be uploaed
Dim intFileSize		'Holds the max file size
Dim strUploadComponent	'Holds the upload component used
Dim lngErrorFileSize	'Holds the file size if the file is not saved because it is to large
Dim blnExtensionOK	'Set to false if the extension of the file is not allowed
Dim strImageName	'Holds the image name
Dim strUserFolderName	'Holds the folder name safe usuario



'If the user is user is using a banned IP redirect to an error page
If bannedIP() Then
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

	'Redirect
	Response.Redirect("insufficient_permission.aspx?M=IP")
End If


'Intiliase variables
blnExtensionOK = True


'read in the forum ID and message box type
portal.variablesForum.intForumID = CInt(Request.QueryString("FID"))
strMessageBoxType = Request.QueryString("MSG")



'Read in the forum name and forum permissions from the database
'Initalise the strSQL variable with an SQL statement to query the database
If portal.variablesForum.strDatabaseType = "SQLServer" Then
	strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "ForumsAllWhereForumIs @portal.variablesForum.intForumID = " & portal.variablesForum.intForumID
Else
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.* FROM " & portal.variablesForum.strDbTable & "Forum WHERE Forum_ID = " & portal.variablesForum.intForumID & ";"
End If

'Query the database
rsCommon=db.execute(strSQL)

'Read in wether the forum is locked or not
If NOT rsCommon.EOF Then

	'Check the user is welcome in this forum
	Call forumPermisisons(portal.variablesForum.intForumID, portal.variablesForum.intGroupID, CInt(rsCommon("Read")), CInt(rsCommon("Post")), CInt(rsCommon("Reply_posts")), 0, 0, 0, 0, 0, CInt(rsCommon("Attachments")), CInt(rsCommon("Image_upload")))

'Else if there is no forum then they probally haven't come from the upload button so redirect home
Else

	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

	'Redirect
	Response.Redirect("default.aspx")
End If


'Reset Server Objects
rsCommon.Close



'Read in the file types that can be uploaded
If portal.variablesForum.blnImageUpload AND portal.variablesForum.blnRead AND (portal.variablesForum.blnPost OR portal.variablesForum.blnReply) Then

	'Initialise the SQL variable with an SQL statement to get the configuration details from the database
	If portal.variablesForum.strDatabaseType = "SQLServer" Then
		strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "SelectConfiguration"
	Else
		strSQL = "SELECT " & portal.variablesForum.strDbTable & "Configuration.* From " & portal.variablesForum.strDbTable & "Configuration;"
	End If

	'Query the database
	rsCommon=db.execute(strSQL)

	'If there be records returned get em
	If NOT rsCommon.EOF Then

		'Read in the image types and size form the database
		'Read in the configuration details from the recordset
		strUploadComponent = rsCommon("Upload_component")
		strFileUploadPath = rsCommon("Upload_img_path")
		saryFileUploadTypes = Split(Trim(strImageTypes), ";")
		strFileTypes = rsCommon("Upload_img_types")
		intMaxFileSize = CInt(rsCommon("Upload_img_size"))
		
		'Replace \ with / in upload path
		strFileUploadPath = Replace(strFileUploadPath, "\", "/", 1, -1, 1)
	End If
End If



'If this is a post back then upload the image
If Request.QueryString("PB") = "Y" Then

	'Place the usuario in a varible to get the folder name for the user
	strUserFolderName = strLoggedInusuario
		
	'Calculate the folder name safe usuario for folder
	strUserFolderName = decodeString(strUserFolderName)
	strUserFolderName = characterStrip(strUserFolderName)

	'Call upoload file function
	strImageName = fileUpload(strFileUploadPath, saryFileUploadTypes, intMaxFileSize, strUploadComponent, lngErrorFileSize, blnExtensionOK, strUserFolderName)

End If



'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

%>
<html>
<head>

<title>Image Upload</title>

<!-- Web Wiz Rich Text Editor ver. <% = strRTEversion %> is written and produced by Bruce Corkhill ©2001-2004
     	If you want your own Rich Text Editor then goto http://www.richtexteditor.org -->

<script  language="javascript">
<%
'If the image has been saved then place it in the post
If lngErrorFileSize = 0 AND blnExtensionOK = True AND strImageName <> "" Then

	'If this is Rich text editor and IE5.0 use the exeCommand to place it in the editor
	If strMessageBoxType = "RTE" AND RTEenabled = "winIE5" Then
	%>

	window.opener.frames.message.document.focus();
	window.opener.frames.message.document.execCommand('InsertImage', false, '<% = strFileUploadPath & "/" & strUserFolderName & "/" & strImageName %>');
	window.close();
<%

	'If this is Rich text editor use the exeCommand to place it in the editor
	ElseIf strMessageBoxType = "RTE" Then
		%>

	window.opener.document.getElementById("message").contentWindow.focus();
	window.opener.document.getElementById("message").contentWindow.document.execCommand('InsertImage', false, '<% = strFileUploadPath & "/" & strUserFolderName & "/" & strImageName %>');
	window.close();
<%

	'Else if they are using the basic editor place the image path into it
	ElseIf strMessageBoxType = "BSC" Then
		%>
	window.opener.document.frmAddMessage.message.focus();
	window.opener.document.frmAddMessage.message.value += "[IMG]<% = strFileUploadPath & "/" & strUserFolderName & "/" & strImageName %>[/IMG]";
	window.close();
<%
	End If
End If
%>
</script>
<style type="text/css">
<!--
html, body {
  background: ButtonFace;
  color: ButtonText;
  font: font-family: Verdana, Arial, Helvetica, sans-serif;
  font-size: 12px;
  margin: 2px;
  padding: 4px;
}
.text {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 12px;
	color: #000000;
}
.error {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 12px;
	color: #FF0000;
}
legend {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 12px;
	color: #0000FF;
}
-->
</style>
</head>
<body OnLoad="self.focus(); document.forms.frmImageUp.Submit.disabled=true;"><%

'If the user is allowed to upload then show them the form
If portal.variablesForum.blnImageUpload AND portal.variablesForum.blnRead AND (portal.variablesForum.blnPost OR portal.variablesForum.blnReply) Then	

	%>
<table width="100%" border="0" align="center" cellpadding="1" cellspacing="0">
 <form action="upload_images.aspx?MSG=<% = strMessageBoxType %>&amp;fID=<% = portal.variablesForum.intForumID %>&amp;pB=Y" method="post" enctype="multipart/form-data" name="frmImageUp" target="_self" id="frmImageUp" onSubmit="alert('<% = portal.variablesForum.strTxtPleaseWaitWhileImageIsUploaded %>')">
  <tr> 
   <td width="100%"> 
    <fieldset>
    <legend><% = portal.variablesForum.strTxtImageUpload %></legend>
    <table width="100%" border="0" cellspacing="0" cellpadding="1">
     <tr> 
      <td width="10%" align="right" class="text"><% = portal.variablesForum.strTxtImage %>:</td>
      <td width="90%"><input name="file" name="id" type="file" size="<% If BrowserType() = "Netscape 4" Then Response.Write("20") Else Response.Write("35") %>" onFocus="document.forms.frmImageUp.Submit.disabled=false;" onClick="document.forms.frmImageUp.Submit.disabled=false;" />
        </td>
     </tr>
     <tr align="center"> 
      <td colspan="2" class="text"><br /><% 
      	
      	'If the file upload has failed becuase of the wrong extension display an error message
	If blnExtensionOK = False Then

		Response.Write("<span class=""error"">" & portal.variablesForum.strTxtImageOfTheWrongFileType & ".<br />" & portal.variablesForum.strTxtImagesMustBeOfTheType & ", "  &  Replace(strFileTypes, ";", ", ", 1, -1, 1) & "</span>")

	'Else if the file upload has failed becuase the size is to large display an error message
	ElseIf lngErrorFileSize <> 0 Then

		Response.Write("<span class=""error"">" & portal.variablesForum.strTxtImageFileSizeToLarge & " " & lngErrorFileSize & "KB.<br />" & portal.variablesForum.strTxtMaximumFileSizeMustBe & " " & intMaxFileSize & "KB</span>")
	
	'Else display a message of the image types that can be uploaded
	Else
      
      		Response.Write(portal.variablesForum.strTxtImagesMustBeOfTheType & ", " & Replace(strFileTypes, ";", ", ", 1, -1, 1) & ", " & portal.variablesForum.strTxtAndHaveMaximumFileSizeOf & " " & intMaxFileSize & "KB") 
      	
      	End If
      %></td>
     </tr>
    </table>
    </fieldset></td>
  </tr>
  <tr align="right"> 
   <td> <input type='submit' name="Submit" value="     <% = portal.variablesForum.strTxtOK %>     "> &nbsp; <input type="button" name="cancel" value=" <% = portal.variablesForum.strTxtCancel %> " onClick="window.close()"></td>
  </tr>
 </form>
</table><%

End If

%>
</body>
</html>