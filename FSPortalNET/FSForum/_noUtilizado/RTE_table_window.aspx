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
Dim lngRows
Dim lngCols
Dim lngWidth
Dim strWidthValue
Dim strAlign
Dim lngBorder
Dim lngPad
Dim lngSpace
Dim strBuildTableHTML
Dim lngRowsLoopCounter
Dim lngColsLoopCounter

'Intalise varibales
lngWidth = 100
lngCols = 1
lngWidth = 1


'If this a post back read in the form elements
If isNumeric(Request.Form("rows")) AND isNumeric(Request.Form("cols")) AND Request.Form("postBack") Then
	
	'Get form elements
	If isNumeric(Request.Form("rows")) Then lngRows = CLng(Request.Form("rows"))
	If isNumeric(Request.Form("cols")) Then lngCols = CLng(Request.Form("cols"))
	If isNumeric(Request.Form("width")) Then lngWidth = CLng(Request.Form("width"))
	strWidthValue = Request.Form("range")
	strAlign = Request.Form("align")
	If isNumeric(Request.Form("border")) Then lngBorder = CLng(Request.Form("border"))
	If isNumeric(Request.Form("pad")) Then lngPad = CLng(Request.Form("pad"))
	If isNumeric(Request.Form("space")) Then lngSpace = CLng(Request.Form("space"))
	
	
	'Build table main elements
	strBuildTableHTML = "<table width=""" & lngWidth & strWidthValue & """"
	If lngBorder <> "" Then strBuildTableHTML = strBuildTableHTML & " border=""" & lngBorder & """"
	If lngSpace <> "" Then strBuildTableHTML = strBuildTableHTML & " cellspacing=""" & lngSpace & """"
	If lngPad <> "" Then strBuildTableHTML = strBuildTableHTML & " cellpadding=""" & lngPad & """"
	strBuildTableHTML = strBuildTableHTML & ">"
	
	'Build table rows
	For lngRowsLoopCounter = 1 to lngRows
		strBuildTableHTML = strBuildTableHTML & "<tr>"
		
		'Build columns
		For lngColsLoopCounter = 1 to lngCols
		
			strBuildTableHTML = strBuildTableHTML & "<td>"
			strBuildTableHTML = strBuildTableHTML & "</td>"
		
		Next
		
		'Reset the column loop counter
		lngColsLoopCounter = 0
		
		'Close the table row
		strBuildTableHTML = strBuildTableHTML & "</tr>"
	Next
	
	strBuildTableHTML = strBuildTableHTML & "</table>"
End If


'If the HTML has been built then run the following JavaScript
If strBuildTableHTML <> "" Then

Response.Write("<script  language=""JavaScript"">")
	
	'If this is windows IE 5.0 use different JavaScript
	If RTEenabled = "winIE5" Then 
		
		%>
	window.opener.frames.message.focus();
	var htmlLink = window.opener.frames.message.document.selection.createRange()
	htmlLink.pasteHTML('<% = strBuildTableHTML %>');
	window.opener.frames.message.document.execCommand('paste', false, '');
	window.close();<%
	
	'Else use the following javascript
	Else
		
		%>
	window.opener.document.getElementById("message").contentWindow.focus();
	var htmlLink = window.opener.document.getElementById("message").contentWindow.document.selection.createRange()
	htmlLink.pasteHTML('<% = strBuildTableHTML %>');
	window.opener.document.getElementById("message").contentWindow.document.execCommand('paste', false, '');
	window.close();<%
	End If
	
Response.Write("</script>")

End If

%>
<html>
<head>
<title>Insert Table</title>

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
<body OnLoad="self.focus();">
<table width="100%" border="0" align="center" cellpadding="1" cellspacing="0">
 <form method="post">
  <tr> 
  <td colspan="2">
   <fieldset style="float: left;">
    <legend>Table</legend>
    <table width="100%" border="0" cellspacing="0" cellpadding="1">
     <tr> 
      <td width="28%" align="right" class="text"><% = portal.variablesForum.strTxtRows %>:</td>
      <td width="4%"><input name="rows" type='text' id="rows" value="2" size="4" maxlength="7" /></td>
      <td width="23%" align="right">&nbsp;</td>
      <td width="45%">&nbsp; </td>
     </tr>
     <tr> 
      <td align="right" class="text"><% = portal.variablesForum.strTxtColumns %>:</td>
      <td><input name="cols" type='text' id="cols" value="2" size="4" maxlength="7" /></td>
      <td align="right" class="text"><% = portal.variablesForum.strTxtWidth %>:</td>
      <td><input name="width" type='text' id="width" value="100" size="4" maxlength="7" />
       <select name="range" id="range">
        <option value="%" selected>%</option>
        <option><% = portal.variablesForum.strTxtpixels %></option>
       </select> </td>
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
        <option value="" selected>Default</option>
        <option value="left">Left</option>
        <option value="center">Center</option>
        <option value="right">Right</option>
       </select></td>
    </tr>
    <tr> 
     <td align="right" class="text"><% = portal.variablesForum.strTxtBorder %>:</td>
     <td><input name="border" type='text' id="border" value="1" size="4" maxlength="7" /></td>
    </tr>
   </table>
   </fieldset>
   </td>
  <td width="49%">
  <fieldset style="float: left;">
  <legend><% = portal.variablesForum.strTxtSpacing %></legend>
   <table width="100%" border="0" cellspacing="0" cellpadding="2">
    <tr> 
      <td width="50%" align="right" class="text"><% = portal.variablesForum.strTxtCellPad %>:</td>
      <td width="50%"><input name="pad" type='text' id="pad" value="1" size="4" maxlength="7" /></td>
    </tr>
    <tr> 
      <td align="right" class="text"><% = portal.variablesForum.strTxtCellSpace %>:</td>
      <td> 
       <input name="space" type='text' id="space" value="1" size="4" maxlength="7" /></td>
    </tr>
   </table>
   </fieldset>
  </td>
 </tr>
 <tr align="right"> 
  <td colspan="2">
  <input type="hidden" name="postBack" value="true">
  <input type='submit' name="Submit" value="     <% = portal.variablesForum.strTxtOK %>     ">
   &nbsp; 
   <input type="button" name="cancel" value=" <% = portal.variablesForum.strTxtCancel %> " onClick="window.close()"></td>
 </tr>
 </form>
</table>
</body>
</html>
