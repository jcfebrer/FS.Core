

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true
Response.Buffer = True

%>
<html>
<head>
<title>Administer Forums</title>



     	

<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"><span class="heading">Administer Forums</span><br />
 <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a></div>
<form name="form1" method="post" action="update_forum_order.aspx">
 <table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
   <td align="center" class="text">From here you can add, delete, edit, or lock Categories and Forums.<br />
    <br />
    Click on Forum name or Category to Amend Details.<br /> <br />
    Select the order you would like the forums to be in from the Order drop down list and click on the Update Order button
 </table>
 <br />
 <table width="98%" border="0" cellspacing="0" cellpadding="0" bgcolor="#000000" align="center">
  <tr>
   <td> <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
     <tr>
      <td bgcolor="#CCCEE6" class="lgText" height="12"><b><span style="font-size: 12px;">Forum</span></b></td>
      <td bgcolor="#CCCEE6" class="lgText" width="9%" height="12" align="center"><b><span style="font-size: 12px;">Lock</span></b></td>
      <td bgcolor="#CCCEE6" class="lgText" width="9%" height="12" align="center"><b><span style="font-size: 12px;">Delete</span></b></td>
      <td bgcolor="#CCCEE6" class="lgText" width="10%" height="12" align="center"><b><span style="font-size: 12px;">Order</span></b></td>
     </tr><%

'Dimension variables
Dim rsForum 			'Holds the Recordset for the forum details
Dim strCategory			'Holds the categories
Dim intCatID			'Holds the category ID number
Dim intForumID			'Holds the forum ID number
Dim strForumName		'Holds the forum name
Dim strForumDiscription		'Holds the forum description
Dim blnForumLocked		'Set to true if the forum is locked
Dim intLoop			'Holds the number of times round in the Loop Counter
Dim intNumOfForums		'Holds the number of forums
Dim intForumOrder		'Holds the order number of the forum
Dim intNumOfCategories		'Holds the number of categories
Dim intCatOrder			'Holds the order number of the category


'Read the various categories from the database
'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Category.* FROM " & portal.variablesForum.strDbTable & "Category ORDER BY " & portal.variablesForum.strDbTable & "Category.Cat_order ASC;"

'Set the curson type to 1 so we can count the number of records returned
rsCommon.CursorType = 1

'Query the database
rsCommon=db.execute(strSQL)

'Check there are categories to display
If rsCommon.EOF Then

	'If there are no categories to display then display the appropriate error message
	Response.Write vbCrLf & "<td bgcolor=""#FFFFFF"" colspan=""4""><span class=""text"">There are no Categories to display. <a href=""category_details.aspx?mode=new"">Click here to create a Forum Category</a></span></td>"

'Else there the are categories so write the HTML to display categories and the forum names and a discription
Else
	'Create a recordset to get the forum details
	Set rsForum = Server.CreateObject("ADODB.Recordset")

	'Get the number of categories
	intNumOfCategories = rsCommon.RecordCount

	'Loop round to read in all the categories in the database
	Do While NOT rsCommon.EOF

		'Get the category name from the database
		strCategory = rsCommon("Cat_name")
		intCatID = CInt(rsCommon("Cat_ID"))
		intCatOrder = CInt(rsCommon("Cat_order"))


		'Display the category name
		
		%>
     <tr bgcolor="#EAEAF4"> 
      <td colspan="2"><a href="category_details.aspx?mode=edit&amp;catID=<% = intCatID %>" target="_self"><b> 
       <% = strCategory %>
       </b></a></td>
      <td align="center"><a href="delete_category.aspx?CatID=<% = intCatID %>" onClick="return confirm('Are you sure you want to Delete this Category?\n\nWARNING: Deleting this category will permanently  remove all Forum(s) in this Category and all the Posts!')"><img src="images/delete_icon.gif" width="15" height="16" border="0" alt="Delete"></a></td>
      <td align="center"> 
       <select name="catOrder<% = intCatID %>"><%
           'loop round to display the number of forums for the order select list
           For intLoop = 1 to intNumOfCategories
		Response.Write("<option value=""" & intLoop & """ ")

			'If the loop number is the same as the order number make this one selected
			If intCatOrder = intLoop Then
				Response.Write("selected")
			End If

		Response.Write(">" & intLoop & "</option>")
           Next
           %>
       </select> </td>
     </tr><%
		'Read the various forums from the database
		'Initalise the strSQL variable with an SQL statement to query the database
		strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.* FROM " & portal.variablesForum.strDbTable & "Forum WHERE " & portal.variablesForum.strDbTable & "Forum.Cat_ID = " & intCatID & " ORDER BY " & portal.variablesForum.strDbTable & "Forum.Forum_Order ASC;"

		rsForum.CursorType = 1

		'Query the database
		rsForum=db.execute(strSQL)

		'Check there are forum's to display
		If rsForum.EOF Then

			'If there are no forum's to display then display the appropriate error message
			Response.Write vbCrLf & "<td bgcolor=""#FFFFFF"" colspan=""4""><span class=""text"">There are no Forum's to display. <a href=""forum_details.aspx?mode=new"">Click here to create a Forum</a></span></td>"

		'Else there the are forum's to write the HTML to display it the forum names and a discription
		Else

			'Get the number of categories
			intNumOfForums = rsForum.RecordCount

			'Loop round to read in all the forums in the database
			Do While NOT rsForum.EOF

				'Read in forum details from the database
				portal.variablesForum.intForumID = CInt(rsForum("Forum_ID"))
				strForumName = rsForum("Forum_name")
				strForumDiscription = rsForum("Forum_description")
				intForumOrder = CInt(rsForum("Forum_order"))
				blnForumLocked = CBool(rsForum("Locked"))

				'Write the HTML of the forum descriptions and hyperlinks to the forums
				%>
     <tr bgcolor="#F5F5FA"> 
      <td bgcolor="#F5F5FA" class="text"><a href="forum_details.aspx?mode=edit&amp;fID=<% = portal.variablesForum.intForumID %>" target="_self"> 
       <% = strForumName %>
       </a><br />
       <span class="smText"><% = strForumDiscription %></span></td>
      <td width="9%" align="center" class="text"> 
       <%

		            	'If the forum is locked and the user is admin let them unlock it
				If blnForumLocked = True Then
				  	Response.Write ("	<a href=""../lock_forum.aspx?code=2&amp;mode=UnLock&amp;fID=" & portal.variablesForum.intForumID & """ OnClick=""return confirm('Are you sure you want to Un-Lock this Forum?')""><img src=""images/forum_locked_icon.gif"" width=""11"" height=""14"" border=""0"" align=""baseline"" alt=""Un-Lock Forum""></a>")
				'If the forum is not lovked and this is the admin then let them lock it
				ElseIf blnForumLocked = False Then
				  	Response.Write ("	<a href=""../lock_forum.aspx?code=2&amp;mode=Lock&amp;fID=" & portal.variablesForum.intForumID & """ OnClick=""return confirm('Are you sure you want to Lock this Forum?')""><img src=""images/forum_unlock_icon.gif"" width=""15"" height=""14"" border=""0"" align=""baseline"" alt=""Lock Forum""></a>")
				End If

               %>
      </td>
      <td width="9%" align="center" class="text"> <a href="delete_forum.aspx?FID=<% = portal.variablesForum.intForumID %>" OnClick="return confirm('Are you sure you want to Delete this Forum?\n\nWARNING: Deleting this forum will permanently  remove all Posts in this Forum!')"><img src="images/delete_icon.gif" width="15" height="16" border="0" alt="Delete"></a></td>
      <td width="10%" class="text"  align="center"> 
       <select name="forumOrder<% = portal.variablesForum.intForumID %>"><%

           'loop round to display the number of forums for the order select list
           For intLoop = 1 to intNumOfForums

		Response.Write("<option value=""" & intLoop & """ ")

			'If the loop number is the same as the order number make this one selected
			If intForumOrder = intLoop Then
				Response.Write("selected")
			End If

		Response.Write(">" & intLoop & "</option>")
           Next
           %>
       </select> </td>
     </tr><%


				'Move to the next database record
				rsForum.MoveNext
			'Loop back round for next forum
			Loop
		End If

		'Close recordsets
		rsForum.Close

		'Move to the next database record
		rsCommon.MoveNext
	'Loop back round for next category
	Loop
End If

Set rsForum = Nothing
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
    </table></td>
  </tr>
 </table>
 <div align="center">
  <table width="98%" border="0" cellspacing="0" cellpadding="3">
   <tr align="right"> 
    <td width="100%"><input type='submit' name="Submit" value="Update Order"></td>
   </tr>
  </table>
 </div>
</form>
<div align="center">
 <table width="98%" border="0" cellspacing="0" cellpadding="3">
  <tr align="center"> 
   <td width="50%"><form action="category_details.aspx?mode=new" method="post" name="form2" target="_self">
     <input type='submit' name="Submit" value="Create New Forum Category"> 
    </form>
   </td>
   <td width="50%"><form action="forum_details.aspx?mode=new" method="post" name="form3" target="_self">
     <input type='submit' name="Submit" value="Create New Forum">
    </form>
   </td>
  </tr>
 </table>
</div>
</body>
</html>