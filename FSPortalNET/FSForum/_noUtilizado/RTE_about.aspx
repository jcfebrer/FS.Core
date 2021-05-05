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

%>
<html>
<head>
<title>About Web Wiz Rich Text Editor</title>

<!-- Web Wiz Rich Text Editor ver. <% = strRTEversion %> is written and produced by Bruce Corkhill ©2002-2004
     	If you want your own Rich Text Editor then goto http://www.richtexteditor.org -->

<style type="text/css">
<!--
html, body {
  background: ButtonFace;
  color: ButtonText;
  font: font-family: Verdana, Arial, Helvetica, sans-serif;
  font-size: 12px;
  margin: 1px;
  padding: 2px;
}
-->
</style>
<link href="includes/default_style.css" rel="stylesheet" type="text/css" />
</head>
<body OnLoad="self.focus();">
<table width="100%" height="176" border="0" cellpadding="0" cellspacing="0">
 <tr> 
  <td height="176" align="center" class="text"><span class="heading">Web Wiz Rich Text Editor</span><br />
   version <% = strRTEversion %><br /> <br />
   Free 


 replacement

 WYSIWYG Editor for text boxes.<br /> 
 <br />
   For your own free copy go to: -<br />
   <a href="http://www.richtexteditor.org" target="_blank">www.richtexteditor.org</a><br /> <br />
   <span class="smText">Copyright 2002-2004 <a href="http://www.webwizguide.info" target="_blank" class="smLink">Web Wiz Guide</a></span> <hr /> <input type="button" name="close" value="     OK     " onClick="window.close()"></td>
 </tr>
</table>



</body>
</html>
