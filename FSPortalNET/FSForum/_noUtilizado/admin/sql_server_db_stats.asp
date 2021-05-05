
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'DB Info MOD created by Michael Baumann for Web Wiz Forums 7.x
'Web Wiz Forums are Copyrighted by Bruce Corkhill


'Checking to see if SQL Server is being used, if no....boot him out.
If portal.variablesForum.strDatabaseType = "SQLServer" Then
	strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "DBinfo"
Else
	
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	response.write("This page only works on SQL Server but your Database Type is set to Access"
	Response.End
End If

'Getting in the results from the SProc
rsCommon=db.execute(strSQL)
While Not rsCommon.EOF
	Databasesize = rsCommon("Databasesize")
	DataLocation = rsCommon("DataLocation")
	LogLocation = rsCommon("LogLocation")
	DatabaseFileSize = rsCommon("DatabaseFileSize")
	LogFileSize = rsCommon("LogFileSize")
	MaxDBSize = rsCommon("MaxDBSize")
	MaxLogSize = rsCommon("MazLogSize")
	Edition = rsCommon("Edition")
	Cluster = rsCommon("Cluster")
	Licensing = rsCommon("Licensing")
	PLevel = rsCommon("PLevel")
	
	rsCommon.MoveNext
Wend

rsCommon.Close
%>
<html>
<head>
<title>SQL Server Database Database Info</title>
			
<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center">
 <p class="text"><span class="heading">SQL Server Database Information</span><br />
  <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
  <br />
</div>
 <table width="80%" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000">
  <tr>
   <td>
    <table width="100%" border="0" align="center" cellpadding="4" cellspacing="1">
    <tr align="left" bgcolor="#CCCEE6">
      <td colspan="4" class="tHeading">Size Information</td>
     </tr>
     
    <tr  bgcolor="#F5F5FA"> 
     <td width="31%"  height="12" align="left" class="bold">SQL Server DB Size:<span class="smText"></span></td>
      
     <td width="11%" valign="top" class="text"> 
      <% = Databasesize %>
      MB</td>
      
     <td width="26%" valign="top" class="text">&nbsp;</td>
      
     <td width="32%" height="12" valign="top" class="text">&nbsp; </td>
     </tr>
     
    <tr  bgcolor="#F5F5FA"> 
     <td width="31%"  height="12" align="left" class="bold">Database File Size:<span class="smText"></span></td>
      
     <td width="11%" valign="top" class="text"> 
      <% = DatabaseFileSize %>
      MB</td>
      
     <td width="26%" valign="top" class="bold">Log File Size</td>
      
     <td width="32%" height="12" valign="top" class="text"> 
      <% = LogFileSize %>
      MB</td>
     </tr>
     
    <tr  bgcolor="#F5F5FA"> 
     <td width="31%"  height="12" align="left" class="bold">Database File Location:<span class="smText"></span></td>
      
     <td width="11%" valign="top" class="text"> 
      <% = DataLocation %>
     </td>
      
     <td width="26%" valign="top" class="bold">Log File Location</td>
      
     <td width="32%" height="12" valign="top" class="text"> 
      <% = LogLocation %>
     </td>
     </tr>
     
    <tr  bgcolor="#F5F5FA"> 
     <td  height="2" align="left" class="bold">Maximum DB Size</td>
      
     <td valign="top" class="text"> 
      <% If MaxDBSize = "-1" THEN 
								response.write("Unrestricted" 
							ELSE
								Response.Write MaxDBSize & " MB"
							END IF
						%>
     </td>
      
     <td valign="top" class="bold">Maximum Log Size</td>
      
     <td height="2" valign="top" class="text"> 
      <% If MaxLogSize = "-1" THEN
								response.write("Unrestricted"
							ELSE
								Response.Write MaxLogSize & " MB"
							END IF	
							%>
     </td>
     </tr>
    <tr align="left" bgcolor="#CCCEE6">
      <td colspan="4" class="tHeading">Server Information</td>
     </tr>
     
    <tr  bgcolor="#F5F5FA"> 
     <td  height="2" align="left" class="bold">SQL Server Edition:</td>
      
     <td valign="top" class="text"> 
      <% = Edition %>
     </td>
      
     <td valign="top" class="bold">Service Pack:</td>
      
     <td height="2" valign="top" class="text"> 
      <% = PLevel %>
     </td>
     </tr>
     
    <tr  bgcolor="#F5F5FA"> 
     <td  height="2" align="left" class="bold">Server is Clustered?</td>
      
     <td valign="top" class="text"> 
      <% = Cluster %>
     </td>
      
     <td valign="top" class="bold">Server Licensing:</td>
      
     <td height="2" valign="top" class="text"> 
      <% = Licensing %>
     </td>
     </tr>
    </table></td>
  </tr>
 </table>
<%  
'Clean up
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

%>
<br />
</body>
</html>