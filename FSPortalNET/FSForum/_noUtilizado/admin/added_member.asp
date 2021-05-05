

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include file="functions/functions_format_post.aspx" -->
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True

'Dimension variables
Dim strEmail                    'Holds the new users e-mail address
Dim intUsersGroupID             'Holds the users group ID
Dim blnShowEmail                'Boolean set to true if the user wishes there e-mail address to be shown
Dim strLocation                 'Holds the new users location
Dim strHomepage                 'Holds the new users homepage if they have one
Dim strAvatar                   'Holds the avatar image
Dim strCheckusuario            'Holds the usuarios from the database recordset to check against the new users requested usuario
Dim blnAutoLogin                'Boolean set to true if the user wants auto login trured on
Dim strImageFileExtension       'holds the file extension
Dim blnAccountReactivate        'Set to true if the users account needs to be reactivated
Dim blnSentEmail                'Set to true if the e-mail has been sent
Dim strEmailBody                'Holds the body of the welcome message e-mail
Dim strSubject                  'Holds the subject of the e-mail
Dim strSignature                'Holds the signature
Dim strICQNum                   'Holds the users ICQ Number
Dim strAIMAddress               'Holds the users AIM address
Dim strMSNAddress               'Holds the users MSN address
Dim strYahooAddress             'Holds the users Yahoo Address
Dim strOccupation               'Holds the users Occupation
Dim strInterests                'Holds the users Interests
Dim dtmDateOfBirth              'Holds the users Date Of Birth
Dim blnPMNotify                 'Set to true if the user want email notification of PM's
Dim strSmutWord                 'Holds the smut word to give better performance so we don't need to keep grabbing it form the recordset
Dim strSmutWordReplace          'Holds the smut word to be replaced with
Dim strMode                     'Holds the mode of the page
Dim blnEmailOK                  'Set to true if e-mail is not already in the database
Dim blnusuarioOK               'Set to true if the usuario requested does not already exsist
Dim intForumStartingGroup       'Holds the forum starting group ID number
Dim strEncyptedclave         'Holds the encrypted clave
Dim blnclaveChange           'Holds if the clave is changed or not
Dim blnEmailBlocked             'set to true if the email address is blocked
Dim strCheckEmailAddress        'Holds the email address to be checked
Dim lngUserProfileID            'Holds the users ID of the profile to get
Dim blnAdminMode                'Set to true if admin mode is enabled to update other members profiles
Dim blnUserActive               'Set to true if the users membership is active
Dim lngPosts                    'Holds the number of posts the user has made
Dim intDOBYear			'Holds the year of birth
Dim intDOBMonth			'Holds the month of birth
Dim intDOBDay			'Holds the day of birth
Dim strRealName			'Holds the persons real name
Dim strMemberTitle		'Holds the members title
Dim dtmServerTime		'Holds the current server time
Dim lngLoopCounter		'Holds the generic loop counter for page
Dim intUpdatePartNumber		'If an update holds which part to update
Dim strTimeOffSet		'Holds the server time off set
Dim blnAttachSignature
Dim strDateFormat
Dim intTimeOffSet
Dim blnReplyNotify
Dim blnWYSIWYGEditor
Dim strImagePath
Dim blnEmoticons


'Initlise variables
blnusuarioOK = True


'If the Profile has already been edited then update the Profile
If Request.Form("postBack") Then

	'******************************************
	'***  Read in member details from form	***
	'******************************************

        'Read in the users details from the form
        strusuario = Trim(Mid(Request.Form("name1"), 1, 15))
        strclave = LCase(Trim(Mid(Request.Form("clave1"), 1, 15)))
	strEmail = Trim(Mid(Request.Form("email"), 1, 60))
	strRealName = Trim(Mid(Request.Form("realName"), 1, 27))
	strLocation = Trim(Mid(Request.Form("location"), 1, 40))
	strHomepage = Trim(Mid(Request.Form("homepage"), 1, 48))
	strSignature = Mid(Request.Form("signature"), 1, 200)
	blnAttachSignature = CBool(Request.Form("attachSig")) 
	'Check that the ICQ number is a number before reading it in
	If isNumeric(Request.Form("ICQ")) Then strICQNum = Trim(Mid(Request.Form("ICQ"), 1, 15))
	strAIMAddress = Trim(Mid(Request.Form("AIM"), 1, 60))
	strMSNAddress = Trim(Mid(Request.Form("MSN"), 1, 60))
	strYahooAddress = Trim(Mid(Request.Form("Yahoo"), 1, 60))
	strOccupation = Mid(Request.Form("occupation"), 1, 40)
	strInterests = Mid(Request.Form("interests"), 1, 130)
	'Check the date of birth is a date before entering it
	If Request.Form("DOBday") <> 0 AND Request.Form("DOBmonth") <> 0 AND Request.Form("DOByear") <> 0 Then
	       dtmDateOfBirth = CDate(DateSerial(Request.Form("DOByear"), Request.Form("DOBmonth"), Request.Form("DOBday")))
	End If
	blnShowEmail = CBool(Request.Form("emailShow"))
	blnPMNotify = CBool(Request.Form("pmNotify"))
	blnAutoLogin = CBool(Request.Form("Login"))
	portal.variablesForum.strDateFormat = Trim(Mid(Request.Form("funcFecha.DateFormat"), 1, 10))
	portal.variablesForum.strTimeOffSet = Trim(Mid(Request.Form("serverOffSet"), 1, 1))
	portal.variablesForum.intTimeOffSet = CInt(Request.Form("serverOffSetHours"))
	portal.variablesForum.blnReplyNotify = CBool(Request.Form("replyNotify"))
	blnWYSIWYGEditor = CBool(Request.Form("ieEditor"))
	blnUserActive = CBool(Request.Form("active"))
        intUsersGroupID = CInt(Request.Form("group"))
        lngPosts = CLng(Request.Form("posts"))
        strMemberTitle = Trim(Mid(Request.Form("memTitle"), 1, 40))



        '******************************************
	'***     Read in the avatar		***
	'******************************************

       strAvatar = Trim(Mid(Request.Form("txtAvatar"), 1, 95))

       'If the avatar text box is empty then read in the avatar from the list box
       If strAvatar = "http://" OR strAvatar = "" Then strAvatar = Trim(Request.Form("SelectAvatar"))

       'If there is no new avatar selected then get the old one if there is one
       If strAvatar = "" Then strAvatar = Request.Form("oldAvatar")

       'If the avatar is the blank image then the user doesn't want one
       If strAvatar = portal.variablesForum.strImagePath & "blank.gif" Then strAvatar = ""
        


        '******************************************
	'***     Clean up member details	***
	'******************************************

        'Clean up user input
        
        
	strRealName = removeAllTags(strRealName)
	strRealName = func.formatInput(strRealName)
	strHomepage = formatLink(strHomepage)
	strHomepage = func.formatInput(strHomepage)
	strLocation = removeAllTags(strLocation)
	strLocation = func.formatInput(strLocation)
	strAIMAddress = formatLink(strAIMAddress)
	strAIMAddress = func.formatInput(strAIMAddress)
	strMSNAddress = formatLink(strMSNAddress)
	strMSNAddress = func.formatInput(strMSNAddress)
	strYahooAddress = formatLink(strYahooAddress)
	strYahooAddress = func.formatInput(strYahooAddress)
	strOccupation = removeAllTags(strOccupation)
	strOccupation = func.formatInput(strOccupation)
	strInterests = removeAllTags(strInterests)
	strInterests = func.formatInput(strInterests)
	        
	'Call the function to format the signature
	strSignature = FormatPost(strSignature)
	
	'Call the function to format forum codes
	strSignature = FormatForumCodes(strSignature)
	
	 'Call the filters to remove malcious HTML code
	 strSignature = checkHTML(strSignature)
	        
	'Strip long text strings from signature
	strSignature = removeLongText(strSignature)
	
	
	'If the user has not entered a hoempage then make sure the homepage variable is blank
	If strHomepage = "http://" Then strHomepage = ""
	
            
	portal.variablesForum.strDateFormat = removeAllTags(portal.variablesForum.strDateFormat)
        portal.variablesForum.strDateFormat = func.formatInput(portal.variablesForum.strDateFormat)
	
	strMemberTitle = removeAllTags(strMemberTitle) 
	strMemberTitle = func.formatInput(strMemberTitle)
	
	'SQL safe format call
        strEmail = formatSQLInput(strEmail)
        
        'Remove any single quotes as they should not be in email addresses
        strEmail = Replace(strEmail, "'", "", 1, -1, 1)


	'******************************************
	'***     Check the avatar is OK		***
	'******************************************
	'If there is no . in the link then there is no extenison and so can't be an image
        If inStr(1, strAvatar, ".", 1) = 0 Then
                  strAvatar = ""
               
         'Else remove malicious code and check the extension is an image extension
         Else
                'Call the filter for the image
                strAvatar = checkImages(strAvatar)
                strAvatar = func.formatInput(strAvatar)
         End If
        

	'******************************************
	'***     Check the usuario is OK	***
	'******************************************


        'Check there is a usuario
      	If strusuario = "" Then blnusuarioOK = False

        'Make sure the user has not entered disallowed usuarios
        If InStr(1, strusuario, "admin", vbTextCompare) Then blnusuarioOK = False
        If InStr(1, strusuario, "clave", vbTextCompare) Then blnusuarioOK = False
        If InStr(1, strusuario, "salt", vbTextCompare) Then blnusuarioOK = False
        If InStr(1, strusuario, "Usuarios", vbTextCompare) Then blnusuarioOK = False
        If InStr(1, strusuario, "code", vbTextCompare) Then blnusuarioOK = False
        If InStr(1, strusuario, "usuario", vbTextCompare) Then blnusuarioOK = False
        If InStr(1, strusuario, "N0act", vbTextCompare) Then blnusuarioOK = False

        'Clean up user input
        strusuario = formatSQLInput(strusuario)


        '******************************************
	'***   Check the usuario is availabe	***
	'******************************************

        'If the usuario is not already written off then check it's not already gone
        If blnusuarioOK Then

         	'Read in the the usuarios from the database to check that the usuario does not already exsist

                'Initalise the strSQL variable with an SQL statement to query the database
                strSQL = "SELECT " & "Usuarios.* FROM " & "Usuarios WHERE " & "Usuarios.usuario = '" & strusuario & "';"

                'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
	        rsCommon.CursorType = 2
	
	        'Set the Lock Type for the records so that the record set is only locked when it is updated
	        rsCommon.LockType = 3
	
	        'Open the Usuarios table
	        rsCommon=db.execute(strSQL)

                'If there is a record returned from the database then the usuario is already used
                If NOT rsCommon.EOF Then 
                	blnusuarioOK = False
                	rsCommon.Close
                End If


                'Remove SQL safe single quote double up set in the format SQL function
                strusuario = Replace(strusuario, "''", "'", 1, -1, 1)
 	End If
		

	'******************************************
	'*** 	     Create a usercode 		***
	'******************************************

        'Calculate a code for the user
        strUserCode = func.userCode(strusuario)

	

	'******************************************
	'*** 		Encrypt clave	***
	'******************************************

        'Encrypt clave
	If strclave <> "" Then
	
		'Genrate a slat value
	       	strSalt = getSalt(Len(strclave))
	
	       'Concatenate salt value to the clave
	       strEncyptedclave = strclave & strSalt
	
	       'Encrypt the clave
	       strEncyptedclave = HashEncode(strEncyptedclave)
	 End If



	'******************************************
	'*** 	  	Update datbase		***
	'******************************************

        'If this is new reg and the usuario and email is OK or this is an update then register the new user or update the rs
        If blnusuarioOK Then

            
                'Insert the user's details into the rs
                With rsCommon
                
                	.AddNew
                             
                        .Fields("usuario") = strusuario
                        .Fields("Group_ID") = intUsersGroupID
                        .Fields("clave") = strEncyptedclave
	                .Fields("Salt") = strSalt
	                .Fields("User_code") = strUserCode
	                .Fields("email") = strEmail
                        .Fields("nombre") = strRealName
		       	.Fields("Location") = strLocation
		       	.Fields("Avatar") = strAvatar
		        .Fields("Homepage") = strHomepage
		        .Fields("ICQ") = strICQNum
		        .Fields("AIM") = strAIMAddress
		        .Fields("MSN") = strMSNAddress
		        .Fields("Yahoo") = strYahooAddress
		        .Fields("Occupation") = strOccupation
		        .Fields("Interests") = strInterests
		        .Fields("DOB") = dtmDateOfBirth
		        .Fields("Signature") = strSignature
		        .Fields("Attach_signature") = blnAttachSignature
	             	.Fields("Date_format") = portal.variablesForum.strDateFormat
			.Fields("Time_offset") = portal.variablesForum.strTimeOffSet
 			.Fields("Time_offset_hours") = portal.variablesForum.intTimeOffSet
	    		.Fields("Reply_notify") = portal.variablesForum.blnReplyNotify
	          	.Fields("Rich_editor") = blnWYSIWYGEditor
	          	.Fields("PM_notify") = blnPMNotify
	       		.Fields("Show_email") = blnShowEmail 
                        .Fields("Active") = blnUserActive	
                        .Fields("Avatar_title") = strMemberTitle	
			.Fields("No_of_posts") = lngPosts
                	

                        'Update the database with the new user's details (needed for MS Access which can be slow updating)
                        .Update

                        'Re-run the query to read in the updated recordset from the database
                        .Requery
                End With


		'******************************************
		'*** 	 	 Clean up   		***
		'******************************************

                'Reset server Object
                rsCommon.Close
                Set rsCommon = Nothing
		adoCon.Close
		Set adoCon = Nothing


		'******************************************
		'*** 	 Redirect to message page	***
		'******************************************

                'Redirect the welcome new user page
                Response.Redirect("added_member.aspx")
        End If
End If


%>
<html>
<head>

<title>Register New User</title>

<!-- Check the from is filled in correctly before submitting -->
<script language="javascript">

//Function to check form is filled in correctly before submitting
function CheckForm () {

        //Initialise variables
        var errorMsg = "";
        var errorMsgLong = "";

        //Check for a usuario
        if (document.frmRegister.name1.value.length <= 3){
                errorMsg += "\n\tusuario \t- Your usuario must be at least 4 characters";
        }

        //Check for a clave
        if (document.frmRegister.clave1.value.length <= 3){
                errorMsg += "\n\tclave \t- Your clave must be at least 4 characters";
        }

        //Check both claves are the same
        if ((document.frmRegister.clave1.value) != (document.frmRegister.clave2.value)){
                errorMsg += "\n\tclave Error\t- The claves entered do not match";
                document.frmRegister.clave1.value = ""
                document.frmRegister.clave2.value = ""
        }

        //If an e-mail is entered check that the e-mail address is valid
        if (document.frmRegister.email.value.length >0 && (document.frmRegister.email.value.indexOf("@",0) == -1||document.frmRegister.email.value.indexOf(".",0) == -1)) {
                errorMsg +="\n\tEmail\t\t- Enter your valid email address";
          errorMsgLong += "\n- If you don't want to enter your email address then leave the email field blank"; 
        }

        //Check to make sure the user is not trying to show their email if they have not entered one
        if (document.frmRegister.email.value == "" && document.frmRegister.emailShow[0].checked == true){
                errorMsgLong += "\n- You can not show your email address if you haven\'t entered one!";
                document.frmRegister.emailShow[1].checked = true
                document.frmRegister.email.focus();
        }

	
        //Check that the signature is not above 200 chracters
        if (document.frmRegister.signature.value.length > 200){
                errorMsg += "\n\tSignature \t- Your signature has to many characters";
                errorMsgLong += "\n- You have " + document.frmRegister.signature.value.length + " characters in your signature, you must shorten it to below 200";
        }

        //If there is aproblem with the form then display an error
        if ((errorMsg != "") || (errorMsgLong != "")){
                msg = "_______________________________________________________________\n\n";
                msg += "The form has not been submitted because there are problem(s) with the form.\n";
                msg += "Please correct the problem(s) and re-submit the form.\n";
                msg += "_______________________________________________________________\n\n";
                msg += "The following field(s) need to be corrected: -\n";

                errorMsg += alert(msg + errorMsg + "\n" + errorMsgLong);
                return false;
        }

        //Reset the submition action
        document.frmRegister.action = "add_member.aspx"
        document.frmRegister.target = "_self";

        return true;
}

//Function to count the number of characters in the signature text box
function DescriptionCharCount() {
        document.frmRegister.countcharactem_rs.value = document.frmRegister.signature.value.length;
}


//Function to open preview post window
function OpenPreviewWindow(targetPage, formName){
	
	now = new Date  
	
	//Open the window first 	
   	openWin('','preview','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=680,height=400')
   		
   	//Now submit form to the new window
   	formName.action = targetPage + "?ID=" + now.getTime();	
	formName.target = "preview";
	formName.submit();
}
</script>

<link href="includes/default_style.css" rel="stylesheet" type="text/css" />
</head>

<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"><span class="heading">Register New User</span><br />
 <span class="text"><a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a></span><br />
 <br />
</div>
<div align="center">
 <p class="bold"><br /> 
  The new member has been added!!</p>
 <p><a href="add_member.aspx" target="_self">Click here to add another new member </a></p>
</div>
</body>
</html>
