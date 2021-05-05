

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />

<!--#include file="includes/emoticons_inc.aspx" -->
<!--#include file="functions/functions_format_post.aspx" -->
<!--#include file="functions/functions_edit_post.aspx" -->
<%
Response.Buffer = True


'Make sure this page is not cached
Response.Expires = -1
Response.ExpiresAbsolute = Now() - 2
Response.AddHeader "pragma","no-cache"
Response.AddHeader "cache-control","private"
Response.CacheControl = "No-Store"






'Dimension variables
Dim rsCommonT			'Holds the forum permisisons to be checked
Dim strForumName		'Holds the forum name
Dim strForumDescription		'Holds the description of the forum
Dim lngTopicID			'Holds the topic number
Dim lngMessageID		'Holds the message ID number
Dim strSubject			'Holds the topic subject
Dim strusuario 		'Holds the usuario of the thread
Dim lngUserID			'Holds the ID number of the user
Dim strUsuariosSignature		'Holds the Usuarioss signature
Dim dtmTopicDate		'Holds the date the thread was made
Dim strMessage			'Holds the message body of the thread
Dim intForumID			'Holds the ID number of the forum


'Initialise variables
lngTopicID = 0	


'Read in the Forum ID to display the Topics for
lngTopicID = CLng(Request.QueryString("TID"))


'If there no Topic ID then redirect the user to the main forum page
If lngTopicID = 0 Then

	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

	'Redirect
	response.redirect("default.aspx"
End If


'Get the posts from the database

'Initalise the strSQL variable with an SQL statement to query the database get the thread details
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.Forum_ID, " & portal.variablesForum.strDbTable & "Thread.Thread_ID, " & portal.variablesForum.strDbTable & "Thread.Message, " & portal.variablesForum.strDbTable & "Thread.Message_date, " & portal.variablesForum.strDbTable & "Thread.Show_signature, " & portal.variablesForum.strDbTable & "Forum.Forum_name, " & portal.variablesForum.strDbTable & "Forum.Forum_description, " & "Usuarios.UsuarioID, " & "Usuarios.usuario, " & "Usuarios.Signature, " & portal.variablesForum.strDbTable & "Topic.Subject "
strSQL = strSQL & "FROM (" & portal.variablesForum.strDbTable & "Forum INNER JOIN " & portal.variablesForum.strDbTable & "Topic ON " & portal.variablesForum.strDbTable & "Forum.Forum_ID = " & portal.variablesForum.strDbTable & "Topic.Forum_ID) INNER JOIN (" & "Usuarios INNER JOIN " & portal.variablesForum.strDbTable & "Thread ON " & "Usuarios.UsuarioID = " & portal.variablesForum.strDbTable & "Thread.UsuarioID) ON " & portal.variablesForum.strDbTable & "Topic.Topic_ID = " & portal.variablesForum.strDbTable & "Thread.Topic_ID "
strSQL = strSQL & "WHERE (((" & portal.variablesForum.strDbTable & "Thread.Topic_ID)=" & lngTopicID & ")) "
strSQL = strSQL & "ORDER by " & portal.variablesForum.strDbTable & "Thread.Message_Date ASC;"

'Query the database
rsCommon=db.execute(strSQL)

'If there is no topic in the database then display the appropraite mesasage
If rsCommon.EOF Then
	'If there are no thread's to display then display the appropriate error message
	strSubject = strNoThreads

Else
	'Read in the thread subject
	strSubject = rsCommon("Subject")
	
	'Read in the forum ID to check if the user can view the post
	portal.variablesForum.intForumID = rsCommon("Forum_ID")
End If





'Create a recordset to check if the user is allowe to view posts in this forum
Set rsCommonT = Server.CreateObject("ADODB.Recordset")

'Read in the forum name and forum permssions from the database
'Initalise the strSQL variable with an SQL statement to query the database
If portal.variablesForum.strDatabaseType = "SQLServer" Then
	strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "ForumsAllWhereForumIs @portal.variablesForum.intForumID = " & portal.variablesForum.intForumID
Else
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.* FROM " & portal.variablesForum.strDbTable & "Forum WHERE " & portal.variablesForum.strDbTable & "Forum.Forum_ID = " & portal.variablesForum.intForumID & ";"
End If

'Query the database
rsCommonT=db.execute(strSQL)


'If there is a record returned by the recordset then check to see if you need a clave to enter it
If NOT rsCommonT.EOF Then

	'Check the user is welcome in this forum
	Call forumPermisisons(portal.variablesForum.intForumID, portal.variablesForum.intGroupID, CInt(rsCommonT("Read")), CInt(rsCommonT("Post")), CInt(rsCommonT("Reply_posts")), CInt(rsCommonT("Edit_posts")), CInt(rsCommonT("Delete_posts")), 0, CInt(rsCommonT("Poll_create")), CInt(rsCommonT("Vote")), CInt(rsCommonT("Attachments")), CInt(rsCommonT("Image_upload")))

	'If the user has no read writes then kick them
	If portal.variablesForum.blnRead = False Then

		'Reset Server Objects
		rsCommonT.Close
		Set rsCommonT = Nothing
		Set rsCommon = Nothing
		adoCon.Close
		Set adoCon = Nothing


		'Redirect to a page asking for the user to enter the forum clave
		response.redirect("insufficient_permission.aspx"
	End If

	'If the forum requires a clave and a logged in forum code is not found on the users machine then send them to a login page
	If rsCommonT("clave") <> "" AND func.ValorCookie(Request.Cookies(portal.variables.strCookieName),"Forum" & portal.variablesForum.intForumID) <> rsCommonT("Forum_code") Then

		'Reset Server Objects
		rsCommonT.Close
		Set rsCommonT = Nothing
		Set rsCommon = Nothing
		adoCon.Close
		Set adoCon = Nothing

		'Redirect to a page asking for the user to enter the forum clave
		response.redirect("forum_clave_form.aspx?RP=PT&amp;fID=" & portal.variablesForum.intForumID & "&amp;tID=" & lngTopicID
	End If
End If

'Clean up
rsCommonT.Close
%>
<html>
<head>

<title><% = strMainForumName %>: <% = strSubject %></title>


     	

<!--#include file="includes/skin_file.aspx" -->
<style type="text/css">
<!--
.text {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	color : #000000;
	font-size: 13px;
	font-weight: normal;
}
.bold {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	color : #000000;
	font-size: 13px;
	font-weight: bold;
}
a  {
	color : #0000FF;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	text-decoration: underline;
	font-size: 13px;
	font-weight: normal;
}

a:hover {
	color : #FF0000;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	text-decoration : underline;
	font-size: 13px;
	font-weight: normal;
}

a:visited {
	color : #990099;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	text-decoration : underline;
	font-size: 13px;
	font-weight: normal;
}

a:visited:hover {
	color : #FF0000;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	text-decoration : underline;
	font-size: 13px;
	font-weight: normal;
}
-->
</style>
</head>
<body bgcolor="#FFFFFF" text="#000000" link="#0000CC" vlink="#0000CC" alink="#FF0000" OnLoad="self.focus();">
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
  <tr>
    <td align="center"><a href="javascript:onClick=window.print()"><% = portal.variablesForum.strTxtPrintPage %></a> | <a href="JavaScript:onClick=window.close()"><% = portal.variablesForum.strTxtCloseWindow %></a></td>
  </tr>
</table>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
  <tr>
    <td class="text"> <br />
      <%
strTableQuoteBorderColour = "#CCCCCC"
strTableQuoteColour = "#FFFFFF"	      

'If there are no threads returned by the qury then display an error message
If rsCommon.EOF Then
%>
      <table width="100%" border="0" cellspacing="0" cellpadding="1">
        <tr>
          <td align="center" height="66"><font size="4"><% = strNoThreads %></font></td>
        </tr>
      </table><%
'Display the threads
Else

	'Read in threads details for the topic from the database
	lngMessageID = CLng(rsCommon("Thread_ID"))
	strMessage = rsCommon("Message")
	lngUserID = CLng(rsCommon("UsuarioID"))
	strusuario = rsCommon("usuario")
	dtmTopicDate = CDate(rsCommon("Message_date"))
	strForumName = rsCommon("Forum_name")
	strForumDescription = rsCommon("Forum_description")
	strUsuariosSignature = rsCommon("Signature")
	
	'If the poster is a guest see if they have entered their name in the GuestName table and get it
	If lngUserID = 2 Then
		
		'Initalise the strSQL variable with an SQL statement to query the database
		If portal.variablesForum.strDatabaseType = "SQLServer" Then
			strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "GuestPoster @lngThreadID = " & lngMessageID
		Else
			strSQL = "SELECT " & portal.variablesForum.strDbTable & "GuestName.Name FROM " & portal.variablesForum.strDbTable & "GuestName WHERE " & portal.variablesForum.strDbTable & "GuestName.Thread_ID = " & lngMessageID & ";"
		End If
		
		'Query the database
		rsCommonT=db.execute(strSQL)
		
		'Read in the guest posters name
		If NOT rsCommonT.EOF Then strusuario = rsCommonT("Name")
		
		'Close the recordset
		rsCommonT.Close
	End If

	'If the message has been edited remove who edited the post
	If InStr(1, strMessage, "<edited>", 1) Then strMessage = removeEditorUsuarios(strMessage)

	'Convert message to text
	strMessage = ConvertToText(strMessage)

	'If the post contains a quote or code block then format it
	If InStr(1, strMessage, "[QUOTE=", 1) > 0 AND InStr(1, strMessage, "[/QUOTE]", 1) > 0 Then strMessage = formatUserQuote(strMessage)
	If InStr(1, strMessage, "[QUOTE]", 1) > 0 AND InStr(1, strMessage, "[/QUOTE]", 1) > 0 Then strMessage = formatQuote(strMessage)
	If InStr(1, strMessage, "[CODE]", 1) > 0 AND InStr(1, strMessage, "[/CODE]", 1) > 0 Then strMessage = formatCode(strMessage)


	'If the post contains a flash link then format it
	If blnFlashFiles Then
		If InStr(1, strMessage, "[FLASH", 1) > 0 AND InStr(1, strMessage, "[/FLASH]", 1) > 0 Then strMessage = formatFlash(strMessage)
		If InStr(1, strUsuariosSignature, "[FLASH", 1) > 0 AND InStr(1, strUsuariosSignature, "[/FLASH]", 1) > 0 Then strUsuariosSignature = formatFlash(strUsuariosSignature)
	End If

	'If the user wants there signature shown then attach it to the message
	If rsCommon("Show_signature") AND strUsuariosSignature <> "" Then 
		strUsuariosSignature = ConvertToText(strUsuariosSignature)
		strMessage = strMessage & "<!-- Signature --><br /><br />-------------<br />" & strUsuariosSignature
	End If

	'Strip long text strings from message
	strMessage = removeLongText(strMessage)
    %>
      <span class="bold" style="font-size: 16px;"><% = strSubject %></span> <br />
      <br />
      <span class="bold"><% = portal.variablesForum.strTxtPrintedFrom %>: </span><% = strWebsiteName %>
      <br /><span class="bold"><% = portal.variablesForum.strTxtForumName %>: </span> <% = strForumName %>
      <br /><span class="bold"><% = portal.variablesForum.strTxtForumDiscription %>: </span> <% = strForumDescription %>
      <br /><span class="bold"><% = portal.variablesForum.strTxtURL %>: </span><% = strForumPath %>/forum_posts.aspx?TID=<% = lngTopicID %>
      <br /><span class="bold"><% = portal.variablesForum.strTxtPrintedDate %>: </span><% = funcFecha.DateFormat(Now(), funcFecha.saryDateTimeData) & " " & portal.variablesForum.strTxtAt & " " & funcFecha.TimeFormat(Now(), funcFecha.saryDateTimeData) %>
      <% If blnLCode = True Then %><br /><span class="bold"><% = portal.variablesForum.strTxtSoftwareVersion %>:</span> Web Wiz Forums <% = strVersion %> - http://www.webwizforums.com<% End If %>
      <br /><br /><br />
      <span class="bold"><% = portal.variablesForum.strTxtTopic %>:</span> <% = strSubject %>
      <hr /><br />
      <span class="bold"><% = portal.variablesForum.strTxtPostedBy %>:</span> <% = strusuario %>
      <br /><span class="bold"><% = portal.variablesForum.strTxtSubjectFolder %>:</span> <% = strSubject %>
      <br /><span class="bold"><% = portal.variablesForum.strTxtDatePosted %>:</span> <% = funcFecha.DateFormat(dtmTopicDate, funcFecha.saryDateTimeData) %>&nbsp;<% = portal.variablesForum.strTxtAt %>&nbsp;<% = funcFecha.TimeFormat(dtmTopicDate, funcFecha.saryDateTimeData) %>
      <br /><br />
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td class="text">
<%  Response.Write("	<!-- Message body -->" & vbCrLf & strMessage & vbCrLf &  "<!-- Message body ''"""" -->") %>
         </td>
        </tr>
       </table>
      <br /><br /><%
      	'Move to the next database record
	rsCommon.MoveNext

      	If NOT rsCommon.EOF Then response.write("<span class=""bold"">" & portal.variablesForum.strTxtReplies & ": </span>"
      	%>
      <hr /><%


      	'Loop round to read in all the thread's in the database
	Do While NOT rsCommon.EOF

		'Read in threads details for the topic from the database
		lngMessageID = CLng(rsCommon("Thread_ID"))
		strMessage = rsCommon("Message")
		lngUserID = CLng(rsCommon("UsuarioID"))
		strusuario = rsCommon("usuario")
		dtmTopicDate = CDate(rsCommon("Message_date"))
		strUsuariosSignature = rsCommon("Signature")
		
		'If the poster is a guest see if they have entered their name in the GuestName table and get it
		If lngUserID = 2 Then
			
			'Initalise the strSQL variable with an SQL statement to query the database
			If portal.variablesForum.strDatabaseType = "SQLServer" Then
				strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "GuestPoster @lngThreadID = " & lngMessageID
			Else
				strSQL = "SELECT " & portal.variablesForum.strDbTable & "GuestName.Name FROM " & portal.variablesForum.strDbTable & "GuestName WHERE " & portal.variablesForum.strDbTable & "GuestName.Thread_ID = " & lngMessageID & ";"
			End If
			
			'Query the database
			rsCommonT=db.execute(strSQL)
			
			'Read in the guest posters name
			If NOT rsCommonT.EOF Then strusuario = rsCommonT("Name")
			
			'Close the recordset
			rsCommonT.Close
		End If

		'If the message has been edited remove who edited the post
		If InStr(1, strMessage, "<edited>", 1) Then strMessage = removeEditorUsuarios(strMessage)

		'Convert message to text
		strMessage = ConvertToText(strMessage)
		

		'If the post contains a quote or code block then format it
		If InStr(1, strMessage, "[QUOTE=", 1) > 0 AND InStr(1, strMessage, "[/QUOTE]", 1) > 0 Then strMessage = formatUserQuote(strMessage)
		If InStr(1, strMessage, "[QUOTE]", 1) > 0 AND InStr(1, strMessage, "[/QUOTE]", 1) > 0 Then strMessage = formatQuote(strMessage)
		If InStr(1, strMessage, "[CODE]", 1) > 0 AND InStr(1, strMessage, "[/CODE]", 1) > 0 Then strMessage = formatCode(strMessage)


		'If the post contains a flash link then format it
		If blnFlashFiles Then
			If InStr(1, strMessage, "[FLASH", 1) > 0 AND InStr(1, strMessage, "[/FLASH]", 1) > 0 Then strMessage = formatFlash(strMessage)
			If InStr(1, strUsuariosSignature, "[FLASH", 1) > 0 AND InStr(1, strUsuariosSignature, "[/FLASH]", 1) > 0 Then strUsuariosSignature = formatFlash(strUsuariosSignature)
		End If
		
		'If the user wants there signature shown then attach it to the message
		If rsCommon("Show_signature") Then 
			strUsuariosSignature = ConvertToText(strUsuariosSignature)
			strMessage = strMessage & "<!-- Signature --><br /><br />-------------<br />" & strUsuariosSignature
		End If
		
		'Strip long text strings from message
		strMessage = removeLongText(strMessage)

	      %>
      <br />
      <span class="bold"><% = portal.variablesForum.strTxtPostedBy %>:</span> <% = strusuario %>
      <br />
      <span class="bold"><% = portal.variablesForum.strTxtDatePosted %>:</span> <% = funcFecha.DateFormat(dtmTopicDate, funcFecha.saryDateTimeData) %>&nbsp;<% = portal.variablesForum.strTxtAt %>&nbsp;<% = funcFecha.TimeFormat(dtmTopicDate, funcFecha.saryDateTimeData) %>
      <br /><br />
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td class="text">
<%  Response.Write("	<!-- Message body -->" & vbCrLf & strMessage & vbCrLf &  "<!-- Message body ''"""" -->") %>
         </td>
        </tr>
       </table>
      <br />
      <hr />
      <%
	      	'Move to the next database record
		rsCommon.MoveNext
	Loop
%>
    </td>
  </tr>
</table>
<br />
<%
End If

'Reset server variables
rsCommon.Close
Set rsCommon = Nothing
Set rsCommonT = Nothing
adoCon.Close
Set adoCon = Nothing
%>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
  <tr>
    <td align="center"><a href="javascript:onClick=window.print()"><% = portal.variablesForum.strTxtPrintPage %></a> | <a href="JavaScript:onClick=window.close()"><% = portal.variablesForum.strTxtCloseWindow %></a>
    <br /><br /><%
     
%>
</td>
  </tr>
</table>
</body>
</html>