

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include file="functions/functions_format_post.aspx" -->
<!--#include file="includes/emoticons_inc.aspx" -->
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True

'Make sure this page is not cached
Response.Expires = -1
Response.ExpiresAbsolute = Now() - 2
Response.AddHeader "pragma","no-cache"
Response.AddHeader "cache-control","private"
Response.CacheControl = "No-Store"


Dim strSignature 		'Holds the Users Message


'Read in the message to be previewed from the cookie set
strSignature = Mid(Request.Form("signature"), 1, 200)

'Call the function to format posts
strSignature = FormatPost(strSignature)

'Call the function to format forum codes
strSignature = FormatForumCodes(strSignature)

'Check the message for malicious HTML code
strSignature = checkHTML(strSignature)

'Strip long text strings from signature
strSignature = removeLongText(strSignature)

'If there is nothing to preview then say so
If strSignature = "" OR IsNull(strSignature) Then
	strSignature = "<br /><br /><div align=""center"">" & portal.variablesForum.strTxtThereIsNothingToPreview & "</div><br /><br />"
'Else fake a post so signature can be view in a real context
Else
	strSignature = portal.variablesForum.strTxtPostedMessage & "<!-- Signature --><br /><br />__________________<br />" & strSignature & "<!-- Signature -->"
	
	'If the post contains a flash link then format it
	If blnFlashFiles Then
		If InStr(1, strSignature, "[FLASH", 1) > 0 AND InStr(1, strSignature, "[/FLASH]", 1) > 0 Then strSignature = formatFlash(strSignature)
	End If
End If

'Clean up
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
<html>
<head>

<title>Signature Preview</title>
<HTTP-EQUIV="PRAGMA" CONTENT="NO-CACHE"> 


     	

<!--#include file="includes/skin_file.aspx" -->

</head>
<body bgcolor="<% = strBgColour %>" text="<% = strTextColour %>" background="<% = strBgImage %>" marginheight="0" marginwidth="0" topmargin="0" leftmargin="0" OnLoad="self.focus();">
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center" height="53">
  <tr> 
    <td align="center" height="17"><span class="heading"><% = portal.variablesForum.strTxtSignaturePreview %></span></td>
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
            <% = strSignature %> 
            <!-- Message body ''"" -->
           </td>
          </tr>
         </table></td>
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