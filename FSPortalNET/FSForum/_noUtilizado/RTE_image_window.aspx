<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'****************************************************************************************
'**  Copyright Notice
'**
'**  Web Wiz Guide - Web Wiz Rich Text Editor
'**
'**  Copyright 2002-2004 Bruce Corkhill All Rights Reserved.
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
'**  Support questions are NOT answered by e-mail ever!
'**
'**  For correspondence or non support questions contact: -
'**  info@webwizguide.info
'**
'**  or at: -
'**
'**  Web Wiz Guide, PO Box 4982, Bournemouth, BH8 8XP, United Kingdom
'**
'****************************************************************************************


'Dimension veriables
Dim strImageURL
Dim strImageAltText
Dim strAlign
Dim intBorder
Dim lngHorizontal
Dim lngVerical
Dim strBuildImageHTML


'If this a post back read in the form elements
If Request.Form("URL") <> "http://" AND Request.Form("URL") <> "" Then
	
	'Initilise variable
	strBuildImageHTML = ""
	
	'Get form elements
	strImageURL = Request.Form("URL")
	strImageAltText = Request.Form("Alt")
	strAlign = Request.Form("align")
	intBorder = Request.Form("border")
	If isNumeric(Request.Form("hoz")) Then lngHorizontal = CLng(Request.Form("hoz"))
	If isNumeric(Request.Form("vert")) Then lngVerical = CLng(Request.Form("vert"))
	
	'Build the HTML for the image insert
	strBuildImageHTML = "<img src=""" & strImageURL & """ border=""" & intBorder & """"
	If lngHorizontal <> 0 Then strBuildImageHTML = strBuildImageHTML & " hspace=""" & lngHorizontal & """"
	If lngVerical <> 0 Then strBuildImageHTML = strBuildImageHTML & " vspace=""" & lngVerical & """"
	If strImageAltText <> "" Then strBuildImageHTML = strBuildImageHTML & " alt=""" & strImageAltText & """"
	If strAlign <> ""  Then strBuildImageHTML = strBuildImageHTML & " align=""" & strAlign & """"
	strBuildImageHTML = strBuildImageHTML & " />"
End If


'If the HTML has been built then run the following JavaScript
If strBuildImageHTML <> "" Then

Response.Write("<script  language=""JavaScript"">")
	
	'If this is windows IE 5.0 use different JavaScript
	If RTEenabled = "winIE5" Then 
		
		%>
	window.opener.frames.message.focus();
	var htmlLink = window.opener.frames.message.document.selection.createRange()
	htmlLink.pasteHTML('<% = strBuildImageHTML %>');
	window.opener.frames.message.document.execCommand('paste', false, '');
	window.close();<%
	
	'Else use the following javascript
	Else
		
		%>
	window.opener.document.getElementById("message").contentWindow.focus();
	var htmlLink = window.opener.document.getElementById("message").contentWindow.document.selection.createRange()
	htmlLink.pasteHTML('<% = strBuildImageHTML %>');
	window.opener.document.getElementById("message").contentWindow.document.execCommand('paste', false, '');
	window.close();<%
	
	End If
	
Response.Write("</script>")

End If

%>
<html>
<head>
<title>Insert Image</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">

<!-- Web Wiz Rich Text Editor ver. <% = strRTEversion %> is written and produced by Bruce Corkhill ©2002-2004
     	If you want your own Rich Text Editor then goto http://www.richtexteditor.org -->

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
legend {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 12px;
}
-->
</style>
<link href="includes/default_style.css" rel="stylesheet" type="text/css" />
</head>
<body OnLoad="self.focus(); document.forms.frmImageInsrt.Submit.disabled=true;">
<table width="100%" border="0" align="center" cellpadding="1" cellspacing="0">
 <form method="post" name="frmImageInsrt">
  <tr> 
  <td colspan="2">
   <fieldset style="float: left;">
    <legend><% = portal.variablesForum.strTxtImage %></legend>
   <table width="100%" border="0" cellspacing="0" cellpadding="1">
    <tr> 
     <td width="29%" align="right" class="text"><% = portal.variablesForum.strTxtImageURL %>:</td>
     <td width="71%"><input name="URL" type='text' id="URL" size="40" value="http://" onfocus="document.forms.frmImageInsrt.Submit.disabled=false;">
     </td>
    </tr>
    <tr> 
      <td align="right" class="text"><% = portal.variablesForum.strTxtAlternativeText %>:</td>
     <td><input name="Alt" type='text' id="Alt" size="40"></td>
    </tr>
   </table>
   </fieldset>
   </td>
 </tr>
 <tr> 
  <td width="51%">
<fieldset style="float: left;">
  <legend><% = portal.variablesForum.strTxtLayout %></legend>
   <table width="100%" border="0" cellspacing="0" cellpadding="2">
    <tr> 
     <td align="right" class="text"><% = portal.variablesForum.strTxtAlignment %>:</td>
     <td><select size='1' name="align" id="align">
       <option value="" selected >Default</option>
       <option value="left">Left</option>
       <option value="right">Right</option>
       <option value="texttop">Texttop</option>
       <option value="absmiddle">Absmiddle</option>
       <option value="baseline">Baseline</option>
       <option value="absbottom">Absbottom</option>
       <option value="bottom">Bottom</option>
       <option value="middle">Middle</option>
       <option value="top">Top</option>
      </select></td>
    </tr>
    <tr> 
     <td align="right" class="text"><% = portal.variablesForum.strTxtBorder %>:</td>
     <td><select name="border" id="border">
       <option selected>0</option>
       <option>1</option>
       <option>2</option>
       <option>3</option>
       <option>4</option>
       <option>5</option>
       <option>6</option>
       <option>7</option>
       <option>8</option>
       <option>9</option>
       <option>10</option>
       <option>11</option>
       <option>12</option>
       <option>13</option>
       <option>14</option>
       <option>15</option>
      </select></td>
    </tr>
   </table>
   </fieldset>
   </td>
  <td width="49%">
  <fieldset style="float: left;">
  <legend><% = portal.variablesForum.strTxtSpacing %></legend>
   <table width="100%" border="0" cellspacing="0" cellpadding="2">
    <tr> 
     <td width="50%" align="right" class="text"><% = portal.variablesForum.strTxtHorizontal %>:</td>
      <td width="50%"><input name="hoz" type='text' id="hoz" size="4" maxlength="7" /></td>
    </tr>
    <tr> 
     <td align="right" class="text"><% = portal.variablesForum.strTxtVertical %>:</td>
      <td> 
       <input name="vert" type='text' id="vert" size="4" maxlength="7" /></td>
    </tr>
   </table>
   </fieldset>
  </td>
 </tr>
 <tr align="right"> 
  <td colspan="2">
<input type='submit' name="Submit" value="     <% = portal.variablesForum.strTxtOK %>     ">
   &nbsp; 
   <input type="button" name="cancel" value=" <% = portal.variablesForum.strTxtCancel %> " onClick="window.close()"></td>
 </tr>
 </form>
</table>
</body>
</html>
