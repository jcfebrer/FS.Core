
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true
Response.Buffer = False 


'Clean up
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing




'Check to see if the user is using an access database
If portal.variablesForum.strDatabaseType <> "Access" Then
	
	'Display message to sql server usres
	response.write("This page only works on Access but your Database Type is set to SQL Server"
	Response.End
End If

'Dimension variables
Dim objJetEngine		'Holds the jet database engine object
Dim objFSO			'Holds the FSO object
Dim strCompactDB		'Holds the destination of the compacted database


%>
<html>
<head>

<title>Compact and Repair Access Database</title>


     	
     	
<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"> 
 <p class="text"><span class="heading">Compact and Repair Access Database</span><br />
  <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
  <br />
  From here you can Compact and Repair the Access database used for the Forum making the database size smaller and the forum perform faster.<br />
  This feature can also repair a damaged or corrupted database.<br />
 </p>
 <%

'If this is a post back run the compact and repair
If Request.Form("postBack") Then %>
<table width="80%" border="0" cellspacing="0" cellpadding="0">
  <tr> 
   <td class="bold"><ol><%
 
 	'Create an intence of the FSO object
 	Set objFSO = Server.CreateObject("Scripting.FileSystemObject")
 	
 	'Back up the database
	objFSO.CopyFile strDbPathAndName, Replace(strDbPathAndName, ".mdb", "-backup.mdb", 1, -1, 1)
 	
 	Response.Write("	<li class=""bold"">Database backed up to:-<br/><span class=""smText"">" & Replace(strDbPathAndName, ".mdb", "-backup.mdb", 1, -1, 1) & "</span><br /><br /></li>")




	'Create an intence of the JET engine object
	Set objJetEngine = Server.CreateObject("JRO.JetEngine")

	'Get the destination and name of the compacted database
	strCompactDB = Replace(strDbPathAndName, ".mdb", "-tmp.mdb", 1, -1, 1)

	'Compact database
	objJetEngine.CompactDatabase strCon, "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & strCompactDB
	
	'Display text that new compact db is created
	Response.Write("	<li class=""bold"">New compacted database:-<br/><span class=""smText"">" & strCompactDB & "</span><br /><br /></li>")
	
	'Release Jet object
	Set objJetEngine = Nothing
	
	
	
	
	'Delete old database
	objFSO.DeleteFile strDbPathAndName
	
	'Display text that that old db is deleted
	Response.Write("	<li class=""bold"">Old uncompacted database deleted:-<br/><span class=""smText"">" & strDbPathAndName & "</span><br /><br /></li>")
	
	
	
	'Rename temporary database to old name
	objFSO.MoveFile strCompactDB, strDbPathAndName
	
	'Display text that that old db is deleted
	Response.Write("	<li class=""bold"">Rename compacted database from:-<br/><span class=""smText"">" & strCompactDB & "</span><br />To:-<br /><span class=""smText"">" & strDbPathAndName & "</span><br /><br /></li>")
	

	'Release FSO object
	Set objFSO = Nothing
	
	
	Response.Write("	The Forums Access database is now compacted and repaired")

%></ol></td>
  </tr>
 </table>
<%
Else

%>
 <p class="text"> Please note: If the 'Compact and Repair' procedure fails a backup of your database will be created ending with '-backup.mdb'.<br />
 </p>
</div>
<form action="compact_access_db.aspx" method="post" name="frmCompact" id="frmCompact">
 <div align="center"><br />
  <input name="postBack" type="hidden" id="postBack" value="true">
  <input type='submit' name="Submit" value="Compact and Repair Database">
 </div>
</form><%

End If

%>
<br />
</body>
</html>
