

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

<title>Forum Codes</title>


     	

<!--#include file="includes/skin_file.aspx" -->
</head>
<body bgcolor="<% = strBgColour %>" text="<% = strTextColour %>" background="<% = strBgImage %>" marginheight="0" marginwidth="0" topmargin="0" leftmargin="0" OnLoad="self.focus();">
<table width="100%" border="0" cellspacing="0" cellpadding="1" align="center">
  <tr>
  <td align="center"><span class="heading"><% = portal.variablesForum.strTxtForumCodes %></span><br />
   <span class="text"><% = portal.variablesForum.strTxtYouCanUseForumCodesToFormatText %></span></td>
  </tr>
</table>
<br />
<table width="510" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr>
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableBgColour %>">
   <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
   <td>
    <table border="0" align="center" cellpadding="4" cellspacing="1" width="510">
       <tr>
        <td width="62%" class="bold" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtTypedForumCode %></td>
        <td width="38%" class="bold" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtConvetedCode %></td>
       </tr>
      </table>
     </td>
    </tr>
    <tr align="left" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
     <td colspan="2" class="text" align="center">
      <table width="100%" border="0" cellspacing="0" cellpadding="1">
       <tr>
        <td width="63%" class="bold"><% = portal.variablesForum.strTxtTextFormating %></td>
        <td width="37%">&nbsp;</td>
       </tr>
       <tr>
        <td width="63%" class="text">[B]<% = portal.variablesForum.strTxtBold %>[/B]</td>
        <td width="37%"><b class="text"><% = portal.variablesForum.strTxtBold %></b></td>
       </tr>
       <tr>
        <td width="63%" class="text">[I]<% = portal.variablesForum.strTxtItalic %>[/I]</td>
        <td width="37%"><i class="text"><% = portal.variablesForum.strTxtItalic %></i></td>
       </tr>
       <tr>
        <td width="63%" class="text">[U]<% = portal.variablesForum.strTxtUnderline %>[/U]</td>
        <td width="37%"><u class="text"><% = portal.variablesForum.strTxtUnderline %></u></td>
       </tr>
       <tr>
        <td width="63%" class="text">[CENTER]<% = portal.variablesForum.strTxtCentre %>[/CENTER]</td>
        <td width="37%" align="center" class="text"><% = portal.variablesForum.strTxtCentre %></td>
       </tr>
       <tr>
        <td width="63%" class="text">&nbsp;</td>
        <td width="37%">&nbsp;</td>
       </tr>
       <tr>
        <td width="63%" class="bold"><% = portal.variablesForum.strTxtImagesAndLinks %></td>
        <td width="37%">&nbsp;</td>
       </tr>
       <tr>
        <td width="63%" class="text">[IMG]http://myWeb.com/smiley.gif[/IMG]</td>
        <td width="37%"><img src="smileys/smiley4.gif" width="17" height="17"></td>
       </tr>
       <tr>
        <td width="63%" class="text">[URL=http://www.myWeb.com]<% = portal.variablesForum.strTxtMyLink %>[/URL]</td>
        <td width="37%"><a href="#"><% = portal.variablesForum.strTxtMyLink %></a></td>
       </tr>
       <tr>
        <td width="63%" class="text">[URL]http://www.myWeb.com[/URL]</td>
        <td width="37%"><a href="#">http://www.myWeb.com</a></td>
       </tr>
       <tr>
        <td width="63%" class="text">[EMAIL=me@myWeb.com]<% = portal.variablesForum.strTxtMyEmail %>[/EMAIL]</td>
        <td width="37%"><a href="#"><% = portal.variablesForum.strTxtMyEmail %></a></td>
       </tr>
       <tr>
        <td width="63%" class="text">&nbsp;</td>
        <td width="37%">&nbsp;</td>
       </tr>
       <tr>
        <td width="63%" class="bold"><% = portal.variablesForum.strTxtFontTypes %></td>
        <td width="37%">&nbsp;</td>
       </tr>
       <tr>
        <td width="63%" class="text">[FONT=Arial]Arial[/FONT]</td>
        <td width="37%"><font face="Arial, Helvetica, sans-serif" size="2">Arial</font></td>
       </tr>
       <tr>
        <td width="63%" class="text">[FONT=Courier]Courier[/FONT]</td>
        <td width="37%"><font face="Courier New, Courier, mono">Courier</font></td>
       </tr>
       <tr>
        <td width="63%" class="text">[FONT=Times]Times[/FONT]</td>
        <td width="37%"><font face="Times New Roman, Times, serif">Times</font></td>
       </tr>
       <tr>
        <td width="63%" class="text">[FONT=Verdana]Verdana[/FONT]</td>
        <td width="37%"><font face="Verdana, Arial, Helvetica, sans-serif" size="2">Verdana</font></td>
       </tr>
       <tr>
        <td width="63%" class="text">&nbsp;</td>
        <td width="37%">&nbsp;</td>
       </tr>
       <tr>
        <td width="63%" class="bold"><% = portal.variablesForum.strTxtFontSizes %></td>
        <td width="37%">&nbsp;</td>
       </tr>
       <tr>
        <td width="63%" class="text">[SIZE=1]<% = portal.variablesForum.strTxtFontSize %> 1[/SIZE]</td>
        <td width="37%"><font size='1'><% = portal.variablesForum.strTxtFontSize %> 1</font></td>
       </tr>
       <tr>
        <td width="63%" class="text">[SIZE=2]<% = portal.variablesForum.strTxtFontSize %> 2[/SIZE]</td>
        <td width="37%"><font size="2"><% = portal.variablesForum.strTxtFontSize %> 2</font></td>
       </tr>
       <tr>
        <td width="63%" class="text">[SIZE=3]<% = portal.variablesForum.strTxtFontSize %> 3[/SIZE]</td>
        <td width="37%"><font size="3"><% = portal.variablesForum.strTxtFontSize %> 3</font></td>
       </tr>
       <tr>
        <td width="63%" class="text">[SIZE=4]<% = portal.variablesForum.strTxtFontSize %> 4[/SIZE]</td>
        <td width="37%"><font size="4"><% = portal.variablesForum.strTxtFontSize %> 4</font></td>
       </tr>
       <tr>
        <td width="63%" class="text">[SIZE=5]<% = portal.variablesForum.strTxtFontSize %> 5[/SIZE]</td>
        <td width="37%"><font size="5"><% = portal.variablesForum.strTxtFontSize %> 5</font></td>
       </tr>
       <tr>
        <td width="63%" class="text">[SIZE=6]<% = portal.variablesForum.strTxtFontSize %> 6[/SIZE]</td>
        <td width="37%"><font size="6"><% = portal.variablesForum.strTxtFontSize %> 6</font></td>
       </tr>
       <tr>
        <td width="63%" class="text">&nbsp;</td>
        <td width="37%">&nbsp;</td>
       </tr>
       <tr>
        <td width="63%" class="bold"><% = portal.variablesForum.strTxtFontColours %></td>
        <td width="37%">&nbsp;</td>
       </tr>
       <tr>
        <td width="63%" class="text">[COLOR=BLACK]<% = portal.variablesForum.strTxtBlack & " " & portal.variablesForum.strTxtFont %>[/COLOR]</td>
        <td width="37%" class="text"><font color="black"><% = portal.variablesForum.strTxtBlack & " " & portal.variablesForum.strTxtFont %></font></td>
       </tr>
       <tr>
        <td width="63%" class="text">[COLOR=WHITE]<% = portal.variablesForum.strTxtWhite & " " & portal.variablesForum.strTxtFont %>[/COLOR]</td>
        <td width="37%" class="text"><font color="white"><% = portal.variablesForum.strTxtWhite & " " & portal.variablesForum.strTxtFont %></font></td>
       </tr>
       <tr>
        <td width="63%" class="text">[COLOR=BLUE]<% = portal.variablesForum.strTxtBlue & " " & portal.variablesForum.strTxtFont %>[/COLOR]</td>
        <td width="37%" class="text"><font color="blue"><% = portal.variablesForum.strTxtBlue & " " & portal.variablesForum.strTxtFont %></font></td>
       </tr>
       <tr>
        <td width="63%" class="text">[COLOR=RED]<% = portal.variablesForum.strTxtRed  & " " & portal.variablesForum.strTxtFont %>[/COLOR]</td>
        <td width="37%" class="text"><font color="red"><% = portal.variablesForum.strTxtRed & " " & portal.variablesForum.strTxtFont %></font></td>
       </tr>
       <tr>
        <td width="63%" class="text">[COLOR=GREEN]<% = portal.variablesForum.strTxtGreen & " " & portal.variablesForum.strTxtFont %>[/COLOR]</td>
        <td width="37%" class="text"><font color="green"><% = portal.variablesForum.strTxtGreen & " " & portal.variablesForum.strTxtFont %></font></td>
       </tr>
       <tr>
        <td width="63%" class="text">[COLOR=YELLOW]<% = portal.variablesForum.strTxtYellow & " " & portal.variablesForum.strTxtFont %>[/COLOR]</td>
        <td width="37%" class="text"><font color="yellow"><% = portal.variablesForum.strTxtYellow & " " & portal.variablesForum.strTxtFont %></font></td>
       </tr>
       <tr>
        <td width="63%" class="text">[COLOR=ORANGE]<% = portal.variablesForum.strTxtOrange & " " & portal.variablesForum.strTxtFont %>[/COLOR]</td>
        <td width="37%" class="text"><font color="orange"><% = portal.variablesForum.strTxtOrange & " " & portal.variablesForum.strTxtFont %></font></td>
       </tr>
       <tr>
        <td width="63%" class="text">[COLOR=BROWN]<% = portal.variablesForum.strTxtBrown & " " & portal.variablesForum.strTxtFont %>[/COLOR]</td>
        <td width="37%" class="text"><font color="brown"><% = portal.variablesForum.strTxtBrown & " " & portal.variablesForum.strTxtFont %></font></td>
       </tr>
       <tr>
        <td width="63%" class="text">[COLOR=MAGENTA]<% = portal.variablesForum.strTxtMagenta & " " & portal.variablesForum.strTxtFont %>[/COLOR]</td>
        <td width="37%" class="text"><font color="magenta"><% = portal.variablesForum.strTxtMagenta & " " & portal.variablesForum.strTxtFont %></font></td>
       </tr>
       <tr>
        <td width="63%" class="text">[COLOR=CYAN]<% = portal.variablesForum.strTxtCyan & " " & portal.variablesForum.strTxtFont %>[/COLOR]</td>
        <td width="37%" class="text"><font color="cyan"><% = portal.variablesForum.strTxtCyan & " " & portal.variablesForum.strTxtFont %></font></td>
       </tr>
       <tr>
        <td width="63%" class="text">[COLOR=LIME GREEN]<% = portal.variablesForum.strTxtLimeGreen & " " & portal.variablesForum.strTxtFont %>[/COLOR]</td>
        <td width="37%" class="text"><font color="limegreen"><% = portal.variablesForum.strTxtLimeGreen & " " & portal.variablesForum.strTxtFont %></font></td>
       </tr>
       <tr>
        <td width="63%" class="text">&nbsp;</td>
        <td width="37%" class="text">&nbsp;</td>
       </tr>
       <tr>
        <td width="63%" class="bold"><% = portal.variablesForum.strTxtQuoting %></td>
        <td width="37%" class="text">&nbsp;</td>
       </tr>
       <tr>
        <td width="63%" colspan="2" class="text">[Quote=usuario]<% = portal.variablesForum.strTxtQuotedMessage & " " & portal.variablesForum.strTxtWithusuario %>[/QUOTE]</td>
       </tr>
       <tr>
        <td width="63%" colspan="2" class="text">[Quote]<% = portal.variablesForum.strTxtQuotedMessage %>[/QUOTE]</td>
       </tr>
       <tr>
        <td width="63%" class="text">&nbsp;</td>
        <td width="37%" class="text">&nbsp;</td>
       </tr>
       <tr>
        <td width="63%" class="bold"><% = portal.variablesForum.strTxtCodeandFixedWidthData %></td>
        <td width="37%" class="text">&nbsp;</td>
       </tr>
       <tr>
        <td width="63%" colspan="2" class="text">[CODE]<% = portal.variablesForum.strTxtMyCodeData %>[/CODE]</td>
       </tr><%

If blnFlashFiles Then %>
       <tr>
        <td width="63%" class="text">&nbsp;</td>
        <td width="37%" class="text">&nbsp;</td>
       </tr>
       <tr>
        <td width="63%" class="bold"><% = portal.variablesForum.strTxtFlashFilesImages %></td>
        <td width="37%" class="text">&nbsp;</td>
       </tr>
       <tr>
        <td width="63%" colspan="2" class="text">[FLASH WIDTH=50 HEIGHT=50]http://www.myWeb.com/flash.swf[/FLASH]</td>
       </tr><%
End If


If blnEmoticons Then %>
       <tr>
        <td width="63%" class="text">&nbsp;</td>
        <td width="37%" class="text">&nbsp;</td>
       </tr>
       <tr>
        <td width="63%" class="bold"><% = portal.variablesForum.strTxtEmoticons %></td>
        <td width="37%" class="text">&nbsp;</td>
       </tr>
       <tr>
        <td colspan="2">
         <table width="100%" border="0" cellspacing="0" cellpadding="4"><%

'Intilise the index position (we are starting at 1 instead of position 0 in the array for simpler calculations)
intIndexPosition = 1

'Calcultae the number of outer loops to do
intNumberOfOuterLoops = UBound(saryEmoticons) / 2

'If there is a remainder add 1 to the number of loops
If UBound(saryEmoticons) MOD 2 > 0 Then intNumberOfOuterLoops = intNumberOfOuterLoops + 1

'Loop throgh th list of emoticons
For intLoop = 1 to intNumberOfOuterLoops

        Response.Write("<tr>")

	'Loop throgh th list of emoticons
	For intInnerLoop = 1 to 2

		'If there is nothing to display show an empty box
		If intIndexPosition > UBound(saryEmoticons) Then
			Response.Write("<td width=""5"" class=""text"">&nbsp;</td>")
			Response.Write("<td width=""92"" class=""text"">&nbsp;</td>")
			Response.Write("<td width=""44"" class=""text"">&nbsp;</td>")
		'Else show the emoticon
		Else
			Response.Write("<td width=""5"" class=""text""><img src=""" & saryEmoticons(intIndexPosition,3) & """ border=""0"" alt=""" & saryEmoticons(intIndexPosition,2) & """></td>")
                	Response.Write("<td width=""92"" class=""text"" nowrap=""nowrap"">" & saryEmoticons(intIndexPosition,1) & "</td>")
                	Response.Write("<td width=""44"" class=""text"">" & saryEmoticons(intIndexPosition,2) & "</td>")
              	End If

              'Minus one form the index position
              intIndexPosition = intIndexPosition + 1
	Next

	Response.Write("</tr>")
Next
%></table>
        </td>
       </tr><%
End If
      %>
      </table>
     </td>
    </tr>
   </table>
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
<div align="center"><%


%>
</div>
</body>
</html>