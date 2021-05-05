<%@ Page Language="VB" AutoEventWireup="false" CodeFile="post_preview.aspx.vb" Inherits="_post_preview" %>
<%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %>

<common:common ID="common" runat="server" />
<%

Dim strMessage as string			'Holds the Users Message


'Make sure this page is not cached
Response.Expires = -1
Response.ExpiresAbsolute = Now() - 2
Response.AddHeader "pragma","no-cache"
Response.AddHeader "cache-control","private"
Response.CacheControl = "No-Store"

'Read in the message to be previewed
strMessage = Request.Form("Message")

'Place format posts posted with the WYSIWYG Editor (RTE)
If Request.Form("browser") = "RTE" Then 
	
	'Call the function to format WYSIWYG posts
	strMessage = WYSIWYGFormatPost(strMessage)

'Else standrd editor is used so convert forum codes
Else
	'Call the function to format posts
	strMessage = FormatPost(strMessage)
End If


'If the user wants forum codes enabled then format the post using them
If Request.Form("forumCodes") Then strMessage = FormatForumCodes(strMessage)

'Check the message for malicious HTML code
strMessage = checkHTML(strMessage)

'Strip long text strings from message
strMessage = removeLongText(strMessage)

'If the post contains a flash link then format it
If blnFlashFiles Then
	If InStr(1, strMessage, "[FLASH", 1) > 0 AND InStr(1, strMessage, "[/FLASH]", 1) > 0 Then strMessage = formatFlash(strMessage)
End If



'Reset server objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
<html>
<head>

<title>Post Preview</title>
<HTTP-EQUIV="PRAGMA" CONTENT="NO-CACHE"> 


     	

<!--#include file="includes/skin_file.aspx" -->

<%
'If the post contains a quote or code block then format it
'Needs to be called after the skin file include so that colours etc. are carried across
If InStr(1, strMessage, "[QUOTE=", 1) > 0 AND InStr(1, strMessage, "[/QUOTE]", 1) > 0 Then strMessage = formatUserQuote(strMessage)
If InStr(1, strMessage, "[QUOTE]", 1) > 0 AND InStr(1, strMessage, "[/QUOTE]", 1) > 0 Then strMessage = formatQuote(strMessage)
If InStr(1, strMessage, "[CODE]", 1) > 0 AND InStr(1, strMessage, "[/CODE]", 1) > 0 Then strMessage = formatCode(strMessage)


'If there is nothing to preview then say so
If strMessage = "" OR IsNull(strMessage) OR (InStr(1, strMessage, "<br />", 1) = 1 AND Len(strMessage) = 8) Then
	strMessage = "<br /><br /><div align=""center"">" & portal.variablesForum.strTxtThereIsNothingToPreview & "</div><br /><br />"
End If
%>
</head>
<body bgcolor="<% = strBgColour %>" text="<% = strTextColour %>" background="<% = strBgImage %>" marginheight="0" marginwidth="0" topmargin="0" leftmargin="0" OnLoad="self.focus();">
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center" height="53">
  <tr> 
    <td align="center" height="17"><span class="heading"><% = portal.variablesForum.strTxtPostPreview %></span></td>
  </tr>
  <tr>
    <td align="center" height="39"><a href="JavaScript:onClick=window.close()"><% = portal.variablesForum.strTxtCloseWindow %></a></td>
  </tr>
</table>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = strTablePostsBorderColour %>" align="center">
 <tr> 
  <td> 
          <table width="100%" border="0" cellspacing="0" cellpadding="2" bgcolor="<% = strTablePostsEvenRowColour %>" background="<% = strTablePostsBgImage %>" height="147">
          <tr> 
           <td class="text" valign="top"> 
            <!-- Message body -->
            <% = strMessage %> 
            <!-- Message body ''"" -->
           </td>
          </tr>
         </table>
         </td>
       </tr>
      </table>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
  <tr>
    <td align="center" height="49"><a href="JavaScript:onClick=window.close()"><% = portal.variablesForum.strTxtCloseWindow %></a></td>
  </tr>
</table>
<div align="center">
<% 
%>
</div>
</body>
</html>