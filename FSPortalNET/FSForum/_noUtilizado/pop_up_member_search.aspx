

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include virtual="/fsportalnet/includes/funcionesMail.aspx" -->
<%
Response.Buffer = True 


Dim strusuario	'Holds the users usuario


'If this is a post back then search the member list
If Request.Form("name") <> "" Then

	'Read in the usuario
	strusuario = Request.Form("name")
	
	'Take out parts of the usuario that are not permitted
	strusuario = disallowedMemberNames(strusuario)
	
	'Get rid of milisous code
	strusuario = formatSQLInput(strusuario)
	
	'Initalise the strSQL variable with an SQL statement to query the database
	strSQL = "SELECT " & "Usuarios.usuario "
	strSQL = strSQL & "FROM " & "Usuarios "
	strSQL = strSQL & "WHERE " & "Usuarios.usuario Like '" & strusuario & "%' "
	strSQL = strSQL & "ORDER BY " & "Usuarios.usuario ASC;"
		
	'Query the database
	rsCommon=db.execute(strSQL)
End If

%>
<html>
<head>

<title>Member Search</title>


     	
     	
<script  language="javascript">

//Function to check form is filled in correctly before submitting
function CheckForm () {

	var errorMsg = "";
	
	//Check for a usuario
	if (document.frmMemSearch.name.value==""){
	
		msg = "<% = portal.variablesForum.strTxtErrorDisplayLine %>\n\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine1 %>\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine2 %>\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine %>\n\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine3 %>\n";
	
		alert(msg + "\n\t<% = portal.variablesForum.strTxtErrorusuario %>");
		document.frmMemSearch.name.focus();
		return false;
	}
	
	return true;
}


//Function to place the usuario in the text box of the opening frame
function getusuario(selectedName)
{
	
	window.opener.document.<% If Request.QueryString("RP") = "BUD" Then Response.Write("frmBuddy.usuario") Else Response.Write("frmAddMessage.member") %>.focus();
	window.opener.document.<% If Request.QueryString("RP") = "BUD" Then Response.Write("frmBuddy.usuario") Else Response.Write("frmAddMessage.member") %>.value = selectedName;
	window.close();
}
</script>
<!--#include file="includes/skin_file.aspx" -->
</head>
<body bgcolor="<% = strBgColour %>" text="<% = strTextColour %>" background="<% = strBgImage %>" marginheight="0" marginwidth="0" topmargin="0" leftmargin="0">
<div align="center" class="heading">
 <% = portal.variablesForum.strTxtMemberSearch %>
</div>
<br />
<form method="post" name="frmMemSearch" action="pop_up_member_search.aspx<% If Request.QueryString("RP") = "BUD" Then Response.Write("?RP=BUD") %>" onSubmit="return CheckForm();">
 <br />
 <table width="390" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" height="30">
  <tr> 
   <td height="2" width="483" align="center"> <table width="100%" border="0" cellspacing="1" cellpadding="2">
     <tr> 
      <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" height="26"> <table width="100%" border="0" cellspacing="0" cellpadding="2">
        <tr> 
         <td width="32%" align="right"><span class="text"><% = portal.variablesForum.strTxtMemberSearch %>:</span>&nbsp;&nbsp;</td>
         <td width="68%"><input type='text' name="name" size="15" maxlength="15" value="<% = strusuario %>"> <input type='submit' name="Submit" value="<% = portal.variablesForum.strTxtSearch %>"></td>
        </tr><%
'If this is a post back then display the results
If Request.Form("name") <> "" Then

%>        
        <tr> 
         <td align="right">&nbsp;</td>
         <td>&nbsp;</td>
        </tr>
        <tr> 
        <td width="32%" align="right"><span class="text"><% = portal.variablesForum.strTxtSelectMember %>:</span>&nbsp;&nbsp;</td>
         <td width="68%"><select name="usuario"><%

	'If there are no records found then display an error message
	If rsCommon.EOF then
		
		Response.Write("<option value="""" selected>" & portal.variablesForum.strTxtNoMatchesFound & "</option>") 
	
	'Else there are matches found so display the result
	Else   
	
		'Loop through the recordset
		Do while NOT rsCommon.EOF  
		
			'Disply the usuarios found
			Response.Write("<option value=""" & rsCommon("usuario") & """>" & rsCommon("usuario") & "</option>")       
           		
           		'Jump to the next record in recordset
           		rsCommon.MoveNext
           
           	Loop
           
        End If        
        %>
          </select> <input type="button" name="Button" value="<% = portal.variablesForum.strTxtSelect %>" onClick="getusuario(frmMemSearch.usuario.options[frmMemSearch.usuario.selectedIndex].value);"></td>
        </tr><% 
        
        'Clean up
        rsCommon.Close
        
End If        

%>       
       </table></td>
     </tr>
    </table></td>
  </tr>
 </table>
</form>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
 <tr> 
  <td align="center"><a href="JavaScript:onClick=window.close()">
   <% = portal.variablesForum.strTxtCloseWindow %>
   </a> <br />
   <br />
   <br /> 
   <% 
'Reset Server Objects
Set rsCommon = Nothing 
adoCon.Close
Set adoCon = Nothing  

%>
  </td>
 </tr>
</table>
</body>
</html>
