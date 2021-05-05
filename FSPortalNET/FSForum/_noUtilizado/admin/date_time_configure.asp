

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true
Response.Buffer = True


'Dimension variables
Dim strMode		'holds the mode of the page, set to true if changes are to be made to the database
Dim strDateFormat	'Holds the date format
Dim strYearFormat	'Holds the year format
Dim intfuncFecha.TimeFormat	'Holds the time format
Dim strDateSeporator	'Holds the date seporator between the day/month/year
Dim saryMonth(12)	'Array holding each of the months
Dim strMorningID	'Holds the identifier to show for morning in 12 hour clock
Dim strAfternoonID	'Holds the identifier to show for afternoon in 12 hour clock
Dim intMonthLoopCounter	'Loop counter for the months


'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "DateTimeFormat.* From " & portal.variablesForum.strDbTable & "DateTimeFormat;"

'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
rsCommon.CursorType = 2

'Set the Lock Type for the records so that the record set is only locked when it is updated
rsCommon.LockType = 3

'Query the database
rsCommon=db.execute(strSQL)

'If the user is changing the date/time setup then update the database
If Request.Form("postBack") Then

	With rsCommon
		'Update the recordset
		.Fields("Date_Format") = Request.Form("funcFecha.DateFormat")
		.Fields("Year_format") = Request.Form("yearFormat")
		.Fields("Time_format") = Request.Form("funcFecha.TimeFormat")
		.Fields("Seporator") = Request.Form("seporator")
		.Fields("am") = Request.Form("am")
		.Fields("pm") = Request.Form("pm")

		'Upadet the months (arrays start at 0 in VBScript but for simplisity we are not using location 1)
		For intMonthLoopCounter = 1 to 12
			.Fields("Month" & intMonthLoopCounter) = Request.Form("month" & intMonthLoopCounter)
		Next

		'Update the database with the new user's details
		.Update

		'Re-run the query to read in the updated recordset from the database
		.Requery
	End With
	
	
	'Empty the application level array holding the date and time format so that any changes are visable in the main forum
	Application("saryAppDateTimeData") = null
End If

'Read in the deatils from the database
If NOT rsCommon.EOF Then

	'Read in the date/time setup from the database
	'Update the recordset
	portal.variablesForum.strDateFormat = rsCommon("Date_Format")
	strYearFormat = rsCommon("Year_format")
	intfuncFecha.TimeFormat = CInt(rsCommon("Time_format"))
	strDateSeporator = rsCommon("Seporator")
	strMorningID = rsCommon("am")
	strAfternoonID = rsCommon("pm")

	'Update the months (arrays start at 0 in VBScript but for simplisity we are not using location 1)
	For intMonthLoopCounter = 1 to 12
		saryMonth(intMonthLoopCounter) = rsCommon.Fields("Month" & intMonthLoopCounter)
	Next
End If

'Reset Server Objects
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
<html>
<head>

<title>Date and Time Settings</title>


     	

<!-- Check the from is filled in correctly before submitting -->
<script  language="javascript">
<!-- Hide from older browsem_rs...

//Function to check form is filled in correctly before submitting
function CheckForm () {

	//Intialise variables
	var errorMsg = "";
	var errorMsgLong = "";

	//Check for all the month fields having values
	for (var count = 3; count <= 15; ++count){
		if (document.frmDateTime.elements[count].value == ""){

			var monthName;

			//get the month
			if (count == 3) {monthName = "January\t";}
			else if (count == 4) {monthName = "February\t";}
			else if (count == 5) {monthName = "March\t";}
			else if (count == 6) {monthName = "April\t";}
			else if (count == 7) {monthName = "May\t";}
			else if (count == 8) {monthName = "June\t";}
			else if (count == 9) {monthName = "July\t";}
			else if (count == 10) {monthName = "August\t";}
			else if (count == 11) {monthName = "September";}
			else if (count == 12) {monthName = "October\t";}
			else if (count == 13) {monthName = "Nevember";}
			else if (count == 14) {monthName = "December";}

			//Wriet the error message
			errorMsg += "\n\t" + monthName + " \t- Enter a value for " + monthName;
		}
	}

	//If there is aproblem with the form then display an error
	if ((errorMsg != "") || (errorMsgLong != "")){
		msg = "___________________________________________________________________\n\n";
		msg += "Your settings have not been updated because there are problem(s) with the form.\n";
		msg += "Please correct the problem(s) and re-submit the form.\n";
		msg += "___________________________________________________________________\n\n";
		msg += "The following field(s) need to be corrected: -\n";

		errorMsg += alert(msg + errorMsg + "\n" + errorMsgLong);
		return false;
	}

	return true;
}
// -->
</script>
<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center">
 <p class="text"><span class="heading">Forum Date and Time Settings</span><br />
  <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
  <br />
  From here you can configure the format of date and time settings in the forum.</p>
</div>
<form method="post" name="frmDateTime" action="date_time_configure.aspx" onSubmit="return CheckForm();">
 <table width="550" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000" height="277">
  <tr>
   <td height="234" width="680"> <table width="100%" border="0" align="center" height="233" cellpadding="4" cellspacing="1">
     <tr align="left" bgcolor="#CCCEE6">
      <td height="30" colspan="2" class="lgText">Configure Date Settings</td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td width="59%"  height="12" align="left" class="text">Date Format:</td>
      <td width="41%" height="12" valign="top"> 
       <select name="funcFecha.DateFormat">
        <option value="dd/mm/yy" <% If portal.variablesForum.strDateFormat = "dd/mm/yy" Then Response.Write("selected") %>>Day/Month/Year</option>
        <option value="mm/dd/yy" <% If portal.variablesForum.strDateFormat = "mm/dd/yy" Then Response.Write("selected") %>>Month/Day/Year</option>
        <option value="yy/mm/dd" <% If portal.variablesForum.strDateFormat = "yy/mm/dd" Then Response.Write("selected") %>>Year/Month/Day</option>
        <option value="yy/dd/mm" <% If portal.variablesForum.strDateFormat = "yy/dd/mm" Then Response.Write("selected") %>>Year/Day/Month</option>
       </select>
       </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="12" align="left" class="text">Separator:<br />
       <span class="smText">This is the separator between the date eg: 12/12/2003, 12-12-2003, etc.</span></td>
      <td width="41%" height="12" valign="top"> 
       <select name="seporator">
        <option value="&nbsp;" <% If strDateSeporator = "&nbsp;" Then Response.Write("selected") %>>&lt;space&gt;</option>
        <option value="/" <% If strDateSeporator = "/" Then Response.Write("selected") %>>/</option>
        <option value="\" <% If strDateSeporator = "\" Then Response.Write("selected") %>>\</option>
        <option value="-" <% If strDateSeporator = "-" Then Response.Write("selected") %>>-</option>
        <option value="&nbsp;-&nbsp;" <% If strDateSeporator = "&nbsp;-&nbsp;" Then Response.Write("selected") %>>&nbsp;-&nbsp;</option>
        <option value="." <% If strDateSeporator = "." Then Response.Write("selected") %>>.</option>
       </select>
       </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="12" align="left" class="text">Year Format:<br />
       <span class="smText">This is whether you want the date in 4 digits (2003) or in 2 digits (02)</span></td>
      <td width="41%" height="12" valign="top"> 
       <select name="yearFormat">
        <option value="long" <% If strYearFormat = "long" Then Response.Write("selected") %>>yyyy</option>
        <option value="short" <% If strYearFormat = "short" Then Response.Write("selected") %>>yy</option>
       </select>
       </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="2" align="left" class="text">January*:<br />
       <span class="smText">This is what you would like displayed for January eg: 01, 1, Jan, etc. </span></td>
      <td width="41%" height="2" valign="top"> 
       <input type='text' name="month1" maxlength="15" value="<% = saryMonth(1) %>" size="15" >
       </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="23" align="left" class="text">February*:<br />
       <span class="smText">This is what you would like displayed for February eg: 02, 2, Feb, etc. </span></td>
      <td width="41%" height="23" valign="top"> 
       <input type='text' name="month2" maxlength="15" value="<% = saryMonth(2) %>" size="15" >
       </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="23" align="left" class="text">March*:</td>
      <td width="41%" height="23" valign="top"> 
       <input type='text' name="month3" maxlength="15" value="<% = saryMonth(3) %>" size="15" >
       </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="23" align="left" class="text">April*:</td>
      <td width="41%" height="23" valign="top"> 
       <input type='text' name="month4" maxlength="15" value="<% = saryMonth(4) %>" size="15" >
       </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="23" align="left" class="text">May*:</td>
      <td width="41%" height="23" valign="top"> 
       <input type='text' name="month5" maxlength="15" value="<% = saryMonth(5) %>" size="15" >
       </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="23" align="left" class="text">June*:</td>
      <td width="41%" height="23" valign="top"> 
       <input type='text' name="month6" maxlength="15" value="<% = saryMonth(6) %>" size="15" >
       </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="23" align="left" class="text">July*:</td>
      <td width="41%" height="23" valign="top"> 
       <input type='text' name="month7" maxlength="15" value="<% = saryMonth(7) %>" size="15" >
       </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="23" align="left" class="text">August*:</td>
      <td width="41%" height="23" valign="top"> 
       <input type='text' name="month8" maxlength="15" value="<% = saryMonth(8) %>" size="15" >
       </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="23" align="left" class="text">September*:</td>
      <td width="41%" height="23" valign="top"> 
       <input type='text' name="month9" maxlength="15" value="<% = saryMonth(9) %>" size="15" >
       </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="23" align="left" class="text">October*:</td>
      <td width="41%" height="23" valign="top"> 
       <input type='text' name="month10" maxlength="15" value="<% = saryMonth(10) %>" size="15" >
       </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="23" align="left" class="text">November*:</td>
      <td width="41%" height="23" valign="top"> 
       <input type='text' name="month11" maxlength="15" value="<% = saryMonth(11) %>" size="15" >
       </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="23" align="left" class="text">December*:</td>
      <td width="41%" height="23" valign="top"> 
       <input type='text' name="month12" maxlength="15" value="<% = saryMonth(12) %>" size="15" >
       </td>
     </tr>
     <tr  bgcolor="#CCCEE6">
      <td  height="23" colspan="2" align="left" class="lgText">Configure Time Settings</td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="7" align="left" class="text">Date Format:<br />
       <span class="smText">For 12 hour clock set to 12 for military time (24 hour clock) set to 24</span></td>
      <td width="41%" height="7" valign="top"> 
       <select name="funcFecha.TimeFormat">
        <option value="12" <% If intfuncFecha.TimeFormat = 12 Then Response.Write("selected") %>>12 Hour Clock</option>
        <option value="24" <% If intfuncFecha.TimeFormat = 24 Then Response.Write("selected") %>>24 Hour Clock</option>
       </select>
       </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="13" align="left" class="text">Morning Identifier for 12 hour clock times:<br />
       <span class="smText">example: am</span></td>
      <td width="41%" height="13" valign="top"> 
       <input type='text' name="am" maxlength="5" value="<% = strMorningID %>" size="5" >
       </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="13" align="left" class="text">Afternoon Identifier for 12 hour clock times:<br />
       <span class="smText">example: pm</span></td>
      <td width="41%" height="13" valign="top"> 
       <input type='text' name="pm" maxlength="5" value="<% = strAfternoonID %>" size="5" >
       </td>
     </tr>
     <tr bgcolor="#F5F5FA" align="center"> 
      <td height="2" colspan="2" valign="top" > 
       <p>
        <input type="hidden" name="postBack" value="true">
        <input type='submit' name="Submit" value="Update Date and Time Formats">
        <input type="reset" name="Reset" value="Reset Form">
        </p></td>
     </tr>
    </table></td>
  </tr>
 </table>
 <div align="center"><br />
  </div>
</form>
<br />

</body>
</html>