

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include file="includes/emoticons_inc.aspx" -->
<%
Response.Buffer = True 

'Declare variables
Dim intIndexPosition		'Holds the idex poistion in the emiticon array
Dim intNumberOfOuterLoops	'Holds the outer loop number for rows
Dim intLoop			'Holds the loop index position
Dim intInnerLoop		'Holds the inner loop number for columns

'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
<html>
<head>

<title>Emoticon Smilies</title>


     	
		
<script  language="javascript">

//Function to add the code to the message for the smileys
function AddSmileyIcon(iconCode) {
 	var txtarea = window.opener.document.frmAddMessage.message;
 	iconCode = ' ' + iconCode + ' ';
 	if (txtarea.createTextRange && txtarea.caretPos) {
  		var caretPos = txtarea.caretPos;
  		caretPos.text = caretPos.text.charAt(caretPos.text.length - 1) == ' ' ? iconCode + ' ' : iconCode;
  		txtarea.focus();
 	} else {
  		txtarea.value  += iconCode;
  		txtarea.focus();
 	}
}

</script>
<!--#include file="includes/skin_file.aspx" -->
</head>
<body bgcolor="<% = strBgColour %>" text="<% = strTextColour %>" background="<% = strBgImage %>" marginheight="0" marginwidth="0" topmargin="0" leftmargin="0" OnLoad="self.focus();">
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
  <tr>
    <td align="center"><span class="heading"><% = portal.variablesForum.strTxtEmoticonSmilies %></span></td>
  </tr>
</table>
<br />
  <table width="350" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" height="138">
  <tr> 
      <td height="174"> 
        
      <table border="0" align="center" cellpadding="4" cellspacing="1" width="350">
        <tr align="left" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>"> 
          <td colspan="2" class="text" align="center"> 
            <table width="100%" border="0" cellspacing="0" cellpadding="4">
              <tr> 
                <td align="center" class="text"><% = portal.variablesForum.strTxtClickOnEmoticonToAdd %></td>
              </tr>
            </table>
            <table width="340" border="0" cellspacing="0" cellpadding="4"><%

'Intilise the index position (we are starting at 1 instead of position 0 in the array for simpler calculations)
intIndexPosition = 1

'Calcultae the number of outer loops to do
intNumberOfOuterLoops = UBound(saryEmoticons) / 2

'If there is a remainder add 1 to the number of loops
If UBound(saryEmoticons) MOD 2 > 0 Then intNumberOfOuterLoops = intNumberOfOuterLoops + 1

'Loop throgh th list of emoticons
For intLoop = 1 to intNumberOfOuterLoops
      
%>
             <tr><%

	'Loop throgh th list of emoticons
	For intInnerLoop = 1 to 2  
	
		'If there is nothing to display show an empty box
		If intIndexPosition > UBound(saryEmoticons) Then 
			Response.Write("<td width=""17"" class=""text"">&nbsp;</td>") 
			Response.Write("<td width=""62"" class=""text"">&nbsp;</td>")
			Response.Write("<td width=""64"" class=""text"">&nbsp;</td>")
		'Else show the emoticon
		Else 
			Response.Write("<td width=""17"" class=""text""><a href=""JavaScript:AddSmileyIcon('" & saryEmoticons(intIndexPosition,2) & "')""><img src=""" & saryEmoticons(intIndexPosition,3) & """ border=""0"" alt=""" & saryEmoticons(intIndexPosition,2) & """></a></td>")
                	Response.Write("<td width=""62"" class=""text"">" & saryEmoticons(intIndexPosition,1) & "</td>")
                	Response.Write("<td width=""64"" class=""text"">" & saryEmoticons(intIndexPosition,2) & "</td>")
              	End If
              
              'Minus one form the index position
              intIndexPosition = intIndexPosition + 1 
	Next            
%></tr><%
Next             
%></table>
          </td>
        </tr>
      </table>
    </td>
  </tr>
</table>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
  <tr>
    <td align="center" height="34"><a href="JavaScript:onClick=window.close()"><% = portal.variablesForum.strTxtCloseWindow %></a></td>
  </tr>
</table>
<div align="center">
<% 
%>
</div>
</body>
</html>