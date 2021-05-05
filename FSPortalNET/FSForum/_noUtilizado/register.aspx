

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include virtual="/fsportalnet/includes/funcionesMail.aspx" -->
<!--#include file="functions/functions_edit_post.aspx" -->
<!--#include file="functions/functions_format_post.aspx" -->

<!--#include file="includes/emoticons_inc.aspx" -->
<!--#include file="language_files/admin_language_file_inc.aspx" -->
<!--#include file="functions/functions_hash1way.aspx" -->
<%
'****************************************************************************************
'**  Copyright Notice
'**
'**  Web Wiz Guide - Web Wiz Forums
'**
'**  Copyright 2001-2004 Bruce Corkhill All Rights Reserved.
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


'Set the response buffer to true as we maybe redirecting
Response.Buffer = True



'Make sure this page is not cached
Response.Expires = -1
Response.ExpiresAbsolute = Now() - 2
Response.AddHeader "pragma","no-cache"
Response.AddHeader "cache-control","private"
Response.CacheControl = "No-Store"



'Dimension variables
Dim strusuario                 'Holds the users usuario
Dim strclave                 'Holds the new users clave
Dim strUserCode                 'Holds the unique user code for the user
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
Dim intForumID                  'Holds the forum ID if within a forum
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
Dim strSalt                     'Holds the salt value for the clave
Dim strEncryptedclave         'Holds the encrypted clave
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
Dim blnSecurityCodeOK		'Set to true if the security code is OK
Dim strConfirmclave		'Holds the users old clave
Dim blnConfirmPassOK		'Set to false if the conformed pass is not OK



'Initalise variables
blnusuarioOK = True
blnSecurityCodeOK = True
portal.variablesForum.blnEmailOK = True
blnShowEmail = False
blnAutoLogin = True
blnAccountReactivate = False
blnWYSIWYGEditor = True
blnAttachSignature = True
blnclaveChange = False
portal.variablesForum.blnEmailBlocked = False
portal.variablesForum.blnAdminMode = False
lngUserProfileID = portal.variablesForum.lngLoggedInUserID
blnConfirmPassOK = true





'******************************************
'***	     Read in page setup		***
'******************************************

'read in the forum ID number
If isNumeric(Request.QueryString("FID")) Then
	portal.variablesForum.intForumID = CInt(Request.QueryString("FID"))
Else
	portal.variablesForum.intForumID = 0
End If

'Read in the mode of the page
strMode = Trim(Mid(Request.Form("mode"), 1, 7))

'Also see if the admin mode is enabled
If Request("M") = "A" Then portal.variablesForum.blnAdminMode = True

'Check which page part we are displaying and updating if not all	
If Request("FPN") Then 
	intUpdatePartNumber = CInt(Request("FPN"))
Else
	intUpdatePartNumber = 0
End If




'******************************************
'***  See if this is a new registration	***
'******************************************

'If this is a new registration check the user has accepted the terms of the forum
'Redirect if not been through the registration process
If Request.Form("Reg") <> "OK" AND strMode = "reg" Then
       
        'Clean up
        Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

        'Redirect
        Response.Redirect("registration_rules.aspx?FID=" & portal.variablesForum.intForumID)
End If




'Check the user is not registered already and just hitting back on their browser
If (strMode = "new" OR strMode = "reg") AND portal.variablesForum.intGroupID <> 2 Then strMode = ""


'******************************************
'***  Check permision to view page	***
'******************************************

'If the user his not activated their mem
If blnActiveMember = False Then

        'clean up before redirecting
        Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

        'redirect to insufficient permissions page
        Response.Redirect("insufficient_permission.aspx?M=ACT")
End If

'If the user has not logged in or not a new registration then redirect them to the insufficient permissions page
If (portal.variablesForum.intGroupID = 2) AND NOT (strMode = "reg" OR strMode = "new") Then

        'clean up before redirecting
        Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

        'redirect to insufficient permissions page
        Response.Redirect("insufficient_permission.aspx")
End If




'********************************************
'***  Check and setup page for admin mode ***
'********************************************

'If the admin mode is enabled see if the user is an admin or moderator
If portal.variablesForum.blnAdminMode Then

        'First see if the user is in a moderator group for any forum
        If portal.variablesForum.blnAdmin = False Then

                'Initalise the strSQL variable with an SQL statement to query the database
                strSQL = "SELECT " & portal.variablesForum.strDbTable & "Permissions.Moderate "
                strSQL = strSQL & "FROM " & portal.variablesForum.strDbTable & "Permissions "
                If portal.variablesForum.strDatabaseType = "SQLServer" Then
                	strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Permissions.Group_ID = " & portal.variablesForum.intGroupID & " AND  " & portal.variablesForum.strDbTable & "Permissions.Moderate = 1;"
                Else
                	strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Permissions.Group_ID = " & portal.variablesForum.intGroupID & " AND  " & portal.variablesForum.strDbTable & "Permissions.Moderate = True;"
        	End If
               

                'Query the database
                rsCommon=db.execute(strSQL)

                'If a record is returned then the user is a moderator in one of the forums
                If NOT rsCommon.EOF Then portal.variablesForum.blnModerator = True

                'Clean up
                rsCommon.Close
        End If


        'Get the profile ID to edit
        lngUserProfileID = CLng(Request("PF"))

        'Turn off email activation if it is enabled as it's not required for an admin edit of a profile
        portal.variablesForum.blnEmailActivation = False


        'If the user is not permitted in to use admin mode send 'em away
        If (portal.variablesForum.blnAdmin = False AND portal.variablesForum.blnModerator = False) Then

                'clean up before redirecting
                Set rsCommon = Nothing
		adoCon.Close
		Set adoCon = Nothing

                'redirect to insufficient permissions page
                Response.Redirect("insufficient_permission.aspx?FID=" & portal.variablesForum.intForumID)
        End If
End If




'******************************************
'***  Redirect to delete member page	***
'******************************************

'If the admin has selected to delete the account that is being edited then delete it
If portal.variablesForum.blnAdminMode AND portal.variablesForum.blnAdmin AND strMode = "update" AND Request.Form("delete") Then

	'clean up before redirecting
        Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

        'redirect to the deletion file
	Response.Redirect("delete_member.aspx?MID=" & lngUserProfileID)

End If



'******************************************
'***    Update or create new member	***
'******************************************

'If the Profile has already been edited then update the Profile
If strMode = "update" OR strMode = "new" Then
	
	
	'******************************************
	'***	  Check the session ID		***
	'******************************************
	
	Call checkSessionID(Request.Form("sessionID"))
	
	'******************************************
	'***	  Check security code		***
	'******************************************
	
	If strMode = "new" AND Session("lngSecurityCode") <> Trim(Mid(Request.Form("securityCode"), 1, 6)) Then 
		'Set the security code OK variable to false
		blnSecurityCodeOK = False
	End If
	

	'******************************************
	'***  Read in member details from form	***
	'******************************************

        'Read in the users details from the form
        If strMode = "new" Then strusuario = Trim(Mid(Request.Form("name"), 1, 15))
        
        
        
        'If part number = 0 (all) or part 1 (reg details) then run this code
        If intUpdatePartNumber = 0 OR intUpdatePartNumber = 1 Then
	        
	        strclave = LCase(Trim(Mid(Request.Form("clave"), 1, 15)))
	        strConfirmclave = LCase(Trim(Mid(Request.Form("oldPass"), 1, 15)))
	        strEmail = Trim(Mid(Request.Form("email"), 1, 60))
       End If
       
       
        
        'If part number = 0 (all) or part 2 (profile details) then run this code
        If intUpdatePartNumber = 0 OR intUpdatePartNumber = 2 Then
	        
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
	End If
	
	'If part number = 0 (all) or part 3 (forum preferences) then run this code
        If intUpdatePartNumber = 0 OR intUpdatePartNumber = 3 Then
	        
	        blnShowEmail = CBool(Request.Form("emailShow"))
	        blnPMNotify = CBool(Request.Form("pmNotify"))
	        blnAutoLogin = CBool(Request.Form("Login"))
	        portal.variablesForum.strDateFormat = Trim(Mid(Request.Form("funcFecha.DateFormat"), 1, 10))
	        portal.variablesForum.strTimeOffSet = Trim(Mid(Request.Form("serverOffSet"), 1, 1))
	        portal.variablesForum.intTimeOffSet = CInt(Request.Form("serverOffSetHours"))
	        portal.variablesForum.blnReplyNotify = CBool(Request.Form("replyNotify"))
	        blnWYSIWYGEditor = CBool(Request.Form("ieEditor"))
	End If
   
   
        
        'If we are in admin mode read in some extras (unless the admin or guest accounts)
        If portal.variablesForum.blnAdminMode Then
        	If lngUserProfileID > 2 Then blnUserActive = CBool(Request.Form("active"))
        	If lngUserProfileID > 2 Then intUsersGroupID = CInt(Request.Form("group"))
        	If isNumeric(Request.Form("posts")) Then lngPosts = CLng(Request.Form("posts"))
        	strMemberTitle = Trim(Mid(Request.Form("memTitle"), 1, 40))
        End If



        '******************************************
	'***     Read in the avatar		***
	'******************************************

        'If avatars are enabled then read in selected avatar
        If blnAvatar = True AND (intUpdatePartNumber = 0 OR intUpdatePartNumber = 2) Then

                strAvatar = Trim(Mid(Request.Form("txtAvatar"), 1, 95))

                'If the avatar text box is empty then read in the avatar from the list box
                If strAvatar = "http://" OR strAvatar = "" Then strAvatar = Trim(Request.Form("SelectAvatar"))

                'If there is no new avatar selected then get the old one if there is one
                If strAvatar = "" Then strAvatar = Request.Form("oldAvatar")

                'If the avatar is the blank image then the user doesn't want one
                If strAvatar = portal.variablesForum.strImagePath & "blank.gif" Then strAvatar = ""
        Else
                strAvatar = ""
        End If




        '******************************************
	'***     Clean up member details	***
	'******************************************

        'Clean up user input
        
        'If part number = 0 (all) or part 2 (profile details) then run this code
        If intUpdatePartNumber = 0 OR intUpdatePartNumber = 2 Then
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
	End If
            
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

        'Remove malicious code form the avatar link or remove it all togtaher if not a web graphic
        If strAvatar <> "" Then
               
                'If there is no . in the link then there is no extenison and so can't be an image
                If inStr(1, strAvatar, ".", 1) = 0 Then
                        strAvatar = ""
               
                'Else remove malicious code and check the extension is an image extension
                Else
                        'Call the filter for the image
                        strAvatar = checkImages(strAvatar)
                        strAvatar = func.formatInput(strAvatar)
                End If
        End If




	'******************************************
	'***     Check the usuario is OK	***
	'******************************************

        'If this is a new reg clean up the usuario
        If strMode = "new" Then

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
        End If




	'******************************************
	'*** 	 	Remove bad words	***
	'******************************************

        'Replace swear words with other words with ***
        'Initalise the SQL string with a query to read in all the words from the smut table
        strSQL = "SELECT " & portal.variablesForum.strDbTable & "Smut.* FROM " & portal.variablesForum.strDbTable & "Smut;"

        'Open the recordset
        rsCommon=db.execute(strSQL)

        'Loop through all the words to check for
        Do While NOT rsCommon.EOF

                'Read in the smut words
                strSmutWord = rsCommon("Smut")
                strSmutWordReplace = rsCommon("Word_replace")

                'Replace the swear words with the words in the database the swear words
                If strMode = "new" Then strusuario = Replace(strusuario, strSmutWord, strSmutWordReplace, 1, -1, 1)
                strRealName = Replace(strRealName, strSmutWord, strSmutWordReplace, 1, -1, 1)
                strSignature = Replace(strSignature, strSmutWord, strSmutWordReplace, 1, -1, 1)
                strAIMAddress = Replace(strAIMAddress, strSmutWord, strSmutWordReplace, 1, -1, 1)
                strMSNAddress = Replace(strMSNAddress, strSmutWord, strSmutWordReplace, 1, -1, 1)
                strYahooAddress = Replace(strYahooAddress, strSmutWord, strSmutWordReplace, 1, -1, 1)
                strOccupation = Replace(strOccupation, strSmutWord, strSmutWordReplace, 1, -1, 1)
                strInterests = Replace(strInterests, strSmutWord, strSmutWordReplace, 1, -1, 1)

                'Move to the next word in the recordset
                rsCommon.MoveNext
        Loop

        'Release the smut recordset object
        rsCommon.Close



	'******************************************
	'*** 	  Check input if new reg	***
	'******************************************

        'If this is a new reg then check the usuario and genrate usercode, setup email activation etc.
        If strMode = "new" Then

        	'******************************************
		'***   Check the usuario is availabe	***
		'******************************************

                'If the usuario is not already written off then check it's not already gone
                If blnusuarioOK Then


                        'Read in the the usuarios from the database to check that the usuario does not already exsist

                        'Initalise the strSQL variable with an SQL statement to query the database
                        strSQL = "SELECT " & "Usuarios.usuario FROM " & "Usuarios WHERE " & "Usuarios.usuario = '" & strusuario & "';"

                        'Query the database
                        rsCommon=db.execute(strSQL)

                        'If there is a record returned from the database then the usuario is already used
                        If NOT rsCommon.EOF Then blnusuarioOK = False

                        'Close the recordset
                        rsCommon.Close

                        'Remove SQL safe single quote double up set in the format SQL function
                        strusuario = Replace(strusuario, "''", "'", 1, -1, 1)


			'******************************************
			'***   Get the starting group ID	***
			'******************************************

                        'Get the starting group ID number

                        'Initalise the strSQL variable with an SQL statement to query the database
                        If portal.variablesForum.strDatabaseType = "SQLServer" Then
                        	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Group.Group_ID FROM " & portal.variablesForum.strDbTable & "Group WHERE " & portal.variablesForum.strDbTable & "Group.Starting_group = 1;"
                        Else
                        	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Group.Group_ID FROM " & portal.variablesForum.strDbTable & "Group WHERE " & portal.variablesForum.strDbTable & "Group.Starting_group = true;"
						End If

                        'Query the database
                        rsCommon=db.execute(strSQL)

                        'Get the forum starting group ID number
                        intForumStartingGroup = CInt(rsCommon("Group_ID"))

                        'Close the recordset
                        rsCommon.Close
                End If


		'******************************************
		'***  Check email domain is not banned	***
		'******************************************

                'Initalise the strSQL variable with an SQL statement to query the database
                strSQL = "SELECT " & portal.variablesForum.strDbTable & "BanList.Email FROM " & portal.variablesForum.strDbTable & "BanList WHERE " & portal.variablesForum.strDbTable & "BanList.Email Is Not Null;"

                'Query the database
                rsCommon=db.execute(strSQL)

                'Loop through the email address and check 'em out
                Do while NOT rsCommon.EOF

                        'Read in the email address to check
                        strCheckEmailAddress = rsCommon("Email")

                        'If a whildcard character is found then check that
                        If Instr(1, strCheckEmailAddress, "*", 1) > 0 Then

	                        'Remove the wildcard charcter from the email address to check
	                        strCheckEmailAddress = Replace(strCheckEmailAddress, "*", "", 1, -1, 1)

	                        'Use the same filters as that on the email address being checked
	                        strCheckEmailAddress = formatLink(strCheckEmailAddress)
	        		strCheckEmailAddress = func.formatInput(strCheckEmailAddress)

	                        'If the banned email and the email entered match up then don't let em sign up
	                        If InStr(1, strEmail, strCheckEmailAddress, 1) Then portal.variablesForum.blnEmailBlocked = True

	                'Else check the actual name doesn't match
	                Else

	                	'Use the same filters as that on the email address being checked
	                        strCheckEmailAddress = formatLink(strCheckEmailAddress)
	        		strCheckEmailAddress = func.formatInput(strCheckEmailAddress)

	                        'If the banned email and the email entered match up then don't let em sign up
	                        If strCheckEmailAddress = strEmail Then portal.variablesForum.blnEmailBlocked = True
	        	End If

                        'Move to the next record
                        rsCommon.MoveNext
                Loop

                'Close recordset
                rsCommon.Close


		'******************************************
		'***  Check email address is availabe	***
		'******************************************

                'If e-mail activation is on then check the email address is not already used
                If portal.variablesForum.blnEmailActivation = True Then
       
                        'Initalise the strSQL variable with an SQL statement to query the database
                        strSQL = "SELECT " & "Usuarios.email FROM " & "Usuarios WHERE " & "Usuarios.email = '" & strEmail & "';"

                        'Query the database
                        rsCommon=db.execute(strSQL)

                        'If there is a record returned from the database then the email address is already used
                        If NOT rsCommon.EOF Then portal.variablesForum.blnEmailOK = False

                        'Close recordset
                        rsCommon.Close

                End If

		'******************************************
		'*** 	     Create a usercode 		***
		'******************************************

                'Calculate a code for the user
                strUserCode = func.userCode(strusuario)


	'******************************************
	'***   If update, update usercode	***
	'******************************************

        'Else this is an update so just calculate a new usercode
        Else

                'Calculate a new code for the user
                strUserCode = func.userCode(strLoggedInusuario)

        End If



	'******************************************
	'*** Read in user details from database ***
	'******************************************

        'Intialise the strSQL variable with an SQL string to open a record set for the Usuarios table
        If portal.variablesForum.strDatabaseType = "SQLServer" Then
		strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "UsuariosDetails @lngUserID = " & lngUserProfileID
	Else
	        strSQL = "SELECT " & "Usuarios.* "
	        strSQL = strSQL & "FROM " & "Usuarios "
	        strSQL = strSQL & "WHERE " & "Usuarios.UsuarioID = " & lngUserProfileID
	End If

        'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
        rsCommon.CursorType = 2

        'Set the Lock Type for the records so that the record set is only locked when it is updated
        rsCommon.LockType = 3

        'Open the Usuarios table
        rsCommon=db.execute(strSQL)




	'********************************************
	'*** Update the usercode if in admin mode ***
	'********************************************

        'If there is a record and in admin mode update the user code to activate or suspend the member
        If NOT rsCommon.EOF AND portal.variablesForum.blnAdminMode Then

        	'Read in the usercode to check incase we are suspending or unsuspending the account
        	strUserCode = rsCommon("User_code")

        	'If we are suspoending the user account and it doesn't alerady contain a suspended code then add it
        	If blnUserActive = False AND InStr(1, strUserCode, "N0act", vbTextCompare) = False AND lngUserProfileID > 2 Then

        	 	strUserCode = strUserCode & "N0act"

        	'Else remove any suspended stuff from the usercode
        	ElseIf blnUserActive Then
        		strUserCode = Replace(strUserCode, "N0act", "", 1, -1, 1)
        	End If
        End If



	'********************************************
	'*** Don't let moderator update admin mem ***
	'********************************************

        'Once the Usuarios table is open if this is an update and admin mode is on and the updater is a moderator check that the account being updated is not an admin account
        If strMode = "update" AND portal.variablesForum.blnAdminMode AND portal.variablesForum.blnModerator AND NOT rsCommon.EOF Then

                'If the account being updated is an admin account and the updater is only a moderator then send 'em away
                If CInt(rsCommon("Group_ID")) = 1 Then

                        'clean up before redirecting
                        rsCommon.Close
                        Set rsCommon = Nothing
			adoCon.Close
			Set adoCon = Nothing

                        'redirect to insufficient permissions page
                        Response.Redirect("insufficient_permission.aspx?FID=" & portal.variablesForum.intForumID)
                End If
        End If


	'******************************************
	'*** 		Encrypt clave	***
	'******************************************

        'Encrypt clave
	If blnEncryptedclaves Then
		
	        If strclave <> "" Then
	
	                'If this is a new reg then generate a salt value
	                If strMode = "new" Then
	                        strSalt = getSalt(Len(strclave))
	
	                'Else this is an update so get the salt value from the db
	                Else
	                        strSalt = rsCommon("Salt")
	                End If
	
	                'Concatenate salt value to the clave
	                strEncryptedclave = strclave & strSalt
	                strConfirmclave = strConfirmclave & strSalt
	
	                'Encrypt the clave
	                strEncryptedclave = HashEncode(strEncryptedclave)
	                strConfirmclave = HashEncode(strConfirmclave)
	        End If
	
	'Else the clave is not set to be encrypted so place the un-encrypted clave into the strEncryptedclave variable
	Else
	
		strEncryptedclave = strclave
	End If
	
	
	
	
	'******************************************
	'*** 		Update clave		***
	'******************************************
	
	'If this is an update then check the user has not change their clave
	If strMode = "update" AND strclave <> "" Then
	        	
	      	'Check the old clave matches that of the confirmed clave
	        If strConfirmclave <> rsCommon("clave") AND portal.variablesForum.blnAdminMode = false Then blnConfirmPassOK = false       	
	        	
	
		'If the clave doesn't match that stored in the db then this is a clave update
	        If rsCommon("clave") <> strEncryptedclave AND blnConfirmPassOK Then
	
	                 'Generate new salt
	                 strSalt = getSalt(Len(strclave))
	
	         	'Concatenate salt value to the clave
	           	strEncryptedclave = strclave & strSalt
	
	         	'Re-Genreate encypted clave with new salt value
	            	If blnEncryptedclaves Then strEncryptedclave = HashEncode(strEncryptedclave)
	
	                'Set the changed clave boolean to true
	                blnclaveChange = True
	        End If
	  End If
	
	



	'******************************************
	'*** 	  Check for email update	***
	'******************************************

        'If e-mail activation is on then check the user has not changed there e-mail address
        If portal.variablesForum.blnEmailActivation AND portal.variablesForum.blnAdmin = False AND (strMode = "update" AND (intUpdatePartNumber = 1 OR intUpdatePartNumber = 0)) Then
                
                'If the old and new e-mail addresses don't match set the reactivation boolean to true
                If rsCommon("email") <> strEmail Then blnAccountReactivate = True
        End If




	'******************************************
	'*** 	  	Update datbase		***
	'******************************************

        'If this is new reg and the usuario and email is OK or this is an update then register the new user or update the rs
        If (strMode = "new" AND blnusuarioOK AND portal.variablesForum.blnEmailOK AND blnSecurityCodeOK AND portal.variablesForum.blnEmailBlocked = False) OR (strMode = "update" AND blnConfirmPassOK) Then

                'If this is new then create a new rs and reset session variable
                If strMode = "new" Then 
                	Session("lngSecurityCode") = null
                	rsCommon.AddNew
                End If
                

            
                'Insert the user's details into the rs
                With rsCommon
                             
                        If strMode = "new" Then .Fields("usuario") = strusuario
                        If strMode = "new" Then .Fields("Group_ID") = intForumStartingGroup
                        
                          
                        
                        'If part number = 0 (all) or part 1 (reg details) then run this code
                        If intUpdatePartNumber = 0 OR intUpdatePartNumber = 1 Then
	                        
	                        If (strMode = "update" AND blnclaveChange = True) OR  strMode = "new" Then .Fields("clave") = strEncryptedclave
	                        If (strMode = "update" AND blnclaveChange = True) OR  strMode = "new" Then .Fields("Salt") = strSalt
	                        .Fields("User_code") = strUserCode
	                        .Fields("email") = strEmail
	                End If
                        
        
                        	
                        	
                        'If part number = 0 (all) or part 2 (profile details) then run this code
                        If intUpdatePartNumber = 0 OR intUpdatePartNumber = 2 Then
                        
		             	.Fields("nombre") = strRealName
		        	.Fields("Location") = strLocation
		       		.Fields("Avatar") = strAvatar
		                
		                
		                'If this is new reg then don't include profile info in the add new
                        	If (blnLongRegForm AND strMode = "new") OR strMode <> "new" Then        
		                       
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
	                	Else
	                		.Fields("Attach_signature") = true
	                	End If
                	End If
                        
                        
                        
                        
                        'If part number = 0 (all) or part 3 (forum preferences) then run this code
                        If intUpdatePartNumber = 0 OR intUpdatePartNumber = 3 Then
	                       
	                        .Fields("Date_format") = portal.variablesForum.strDateFormat
	                        .Fields("Time_offset") = portal.variablesForum.strTimeOffSet
	                        .Fields("Time_offset_hours") = portal.variablesForum.intTimeOffSet
	                        .Fields("Reply_notify") = portal.variablesForum.blnReplyNotify
	                        .Fields("Rich_editor") = blnWYSIWYGEditor
	                        .Fields("PM_notify") = blnPMNotify
	                        .Fields("Show_email") = blnShowEmail
	                End If
                        
                        
                        
                        
                        'If the e-mail activation is on and this is a new reg or an update and the account needs reactivating then don't activate the account
                        If ((portal.variablesForum.blnEmailActivation = True AND strMode = "new") OR blnAccountReactivate = True) AND portal.variablesForum.blnModerator = False Then
                                .Fields("Active") = 0
                        Else
                                .Fields("Active") = 1
                        End If
                        
                        
                        
                        
                        'If the admin mode is enabled then add update some extra parts
                        If portal.variablesForum.blnAdminMode AND (portal.variablesForum.blnAdmin Or portal.variablesForum.blnModerator) AND strMode = "update" Then
                        	
                        	If lngUserProfileID > 2 Then .Fields("Active") = blnUserActive
                        	
                        	.Fields("Avatar_title") = strMemberTitle
				
				If func.IsEmpty(lngPosts) = False Then .Fields("No_of_posts") = lngPosts
				
                        	'If the user is also the admin then let them update some other parts
                        	If portal.variablesForum.blnAdmin AND lngUserProfileID > 2 Then
                        		.Fields("Group_ID") = intUsersGroupID
                		End If
                        End If



                        'Update the database with the new user's details (needed for MS Access which can be slow updating)
                        .Update

                        'Re-run the query to read in the updated recordset from the database
                        .Requery
                End With



		'******************************************
		'*** 	     Create usercode cookie	***
		'******************************************

                'Write a cookie with the User ID number so the user logged in throughout the forum
                'But only if not in admin modem and using all parts of part 1 of the reg form
                If (portal.variablesForum.blnAdminMode = False) AND (intUpdatePartNumber = 0 OR intUpdatePartNumber = 1) Then
                        
                        'Write the cookie with the name Forum containing the value UserID number 
   			Response.Cookies(portal.variables.strCookieName)("UID") = strUserCode

                        'If the user has selected to be remebered when they next login then set the expiry date for the cookie for 1 year
                        If blnAutoLogin = True Then

                                'Set the expiry date for 1 year (365 days)
                                'If no expiry date is set the cookie is deleted from the users system 20 minutes after they leave the forum
                                Response.Cookies(portal.variables.strCookieName).Expires = Now() + 365
                        End If
                End If




		'******************************************
		'*** 	   Send activate email   	***
		'******************************************

                'Inititlaise the subject of the e-mail that may be sent in the next if/ifelse statements
                strSubject = "" & portal.variablesForum.strTxtWelcome & " " & portal.variablesForum.strTxtEmailToThe & " " & strMainForumName

                'If the members account needs to be activated or reactivated then send the member a re-activate mail a redirect them to a page to tell them there account needs re-activating
                If (portal.variablesForum.blnEmailActivation = True AND strMode = "new") OR blnAccountReactivate = True Then

                        'Send an e-mail to enable the users account to be activated
                        'Initailise the e-mail body variable with the body of the e-mail
                        strEmailBody = portal.variablesForum.strTxtHi & " " & decodeString(strusuario)
                        strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtEmailThankYouForRegistering & " " & strMainForumName & "."
                        strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtusuario & ": - " & decodeString(strusuario)
                        strEmailBody = strEmailBody & vbCrLf & portal.variablesForum.strTxtclave & ": - " & strclave
                        strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtToActivateYourMembershipFor & " " & strMainForumName & " " & portal.variablesForum.strTxtForumClickOnTheLinkBelow & ": -"
                        strEmailBody = strEmailBody & vbCrLf & vbCrLf & strForumPath & "/activate.aspx?ID=" & Server.URLEncode(strUserCode)

                        'Send the e-mail using the Send Mail function created on the send_mail_function.inc file
                        blnSentEmail = funcMail.SendMailForo(strEmailBody, decodeString(strusuario), decodeString(strEmail), strMainForumName, decodeString(strForumEmailAddress), strSubject, strMailComponent, false)

                        'Reset server Object
                        rsCommon.Close
                        Set rsCommon = Nothing
			adoCon.Close
			Set adoCon = Nothing

                        'Redirect the reactivate page
                        If blnAccountReactivate = True Then
                                Response.Redirect("register_confirm.aspx?TP=REACT&amp;fID=" & portal.variablesForum.intForumID)
                        'Redirect to the activate page
                        Else
                                Response.Redirect("register_confirm.aspx?TP=ACT&amp;fID=" & portal.variablesForum.intForumID)
                        End If


		'******************************************
		'*** 	   Send welcome email   	***
		'******************************************

                'Send the new user a welcome e-mail if e-mail notification is turned on and the user has given an e-mail address
                ElseIf portal.variablesForum.blnEmail = True AND strEmail <> "" AND strMode = "new" Then

                        'Initailise the e-mail body variable with the body of the e-mail
                        strEmailBody = portal.variablesForum.strTxtHi & " " & decodeString(strusuario)
                        strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtEmailThankYouForRegistering & " " & strMainForumName & "."
                        strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtEmailYouCanNowUseTheForumAt & " " & strWebsiteName & " " & portal.variablesForum.strTxtEmailForumAt & " " & strForumPath
                        strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtusuario & ": - " & strusuario
                        strEmailBody = strEmailBody & vbCrLf & portal.variablesForum.strTxtclave & ": - " & decodeString(strclave)

                        'Send the e-mail using the Send Mail function created on the send_mail_function.inc file
                        blnSentEmail = funcMail.SendMailForo(strEmailBody, decodeString(strusuario), decodeString(strEmail), strMainForumName, decodeString(strForumEmailAddress), strSubject, strMailComponent, false)
                End If


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
                If strMode = "new" Then
                        Response.Redirect("register_confirm.aspx?TP=NEW&amp;fID=" & portal.variablesForum.intForumID)
                'Redirect to the update profile page
                Else
                        Response.Redirect("register_confirm.aspx?TP=UPD&amp;fID=" & portal.variablesForum.intForumID)
                End If
        
        'Else close rs
        Else
        	rsCommon.Close 
        End If
End If




'******************************************
'***         Set the page mode		***
'******************************************

'If this is a new registerant then reset the mode of the page to new
If strMode = "reg" OR strMode = "new" Then
        
        'set the mode to new
        strMode = "new"
        
        
        '********** Create Security Code **********
        
        'Initliase variable
        Session("lngSecurityCode") = ""
        
        'Create a new session security code
        For lngLoopCounter = 1 to 6
        	
		'Randomise the system timer
		Randomize Timer
		
		'Place the random number onto the end of teh security code session variable
		Session("lngSecurityCode") = Session("lngSecurityCode") & CStr(CInt(Rnd * 9))
	Next

'Else this is an update
Else
        strMode = "update"
End If




'******************************************
'***     Get the user details from db	***
'******************************************

'If this is a profile update get the users details to update
If strMode = "update" Then

        'Read the various forums from the database
        'Initalise the strSQL variable with an SQL statement to query the database
        If portal.variablesForum.strDatabaseType = "SQLServer" Then
		strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "UsuariosDetails @lngUserID = " & lngUserProfileID
	Else
	        strSQL = "SELECT " & "Usuarios.* "
	        strSQL = strSQL & "FROM " & "Usuarios "
	        strSQL = strSQL & "WHERE " & "Usuarios.UsuarioID = " & lngUserProfileID
	End If

        'Query the database
        rsCommon=db.execute(strSQL)

        'If there is no matching profile returned by the recordset then redirect the user to the main forum page
        If rsCommon.EOF Then

                'Reset server Object
                rsCommon.Close
                Set rsCommon = Nothing
		adoCon.Close
		Set adoCon = Nothing

                Response.Redirect("default.aspx")
        End If

        'Read in the new user's profile from the recordset
        strusuario = rsCommon("usuario")
        strRealName = rsCommon("nombre")
        strEmail = Trim(rsCommon("email"))
        blnShowEmail = CBool(rsCommon("Show_email"))
        strHomepage = rsCommon("Homepage")
        If isNull(rsCommon("Location")) Then strLocation = "" Else strLocation = rsCommon("Location")
        strSignature = rsCommon("Signature")
        strAvatar = rsCommon("Avatar")
        strMemberTitle = rsCommon("Avatar_title")
        portal.variablesForum.strDateFormat = rsCommon("Date_format")
        portal.variablesForum.strTimeOffSet = rsCommon("Time_offset")
        portal.variablesForum.intTimeOffSet = CInt(rsCommon("Time_offset_hours"))
        portal.variablesForum.blnReplyNotify = CBool(rsCommon("Reply_notify"))
        blnAttachSignature = CBool(rsCommon("Attach_signature"))
        blnWYSIWYGEditor = CBool(rsCommon("Rich_editor"))
        strICQNum = rsCommon("ICQ")
        strAIMAddress = rsCommon("AIM")
        strMSNAddress = rsCommon("MSN")
        strYahooAddress = rsCommon("Yahoo")
        strOccupation = rsCommon("Occupation")
        strInterests = rsCommon("Interests")
        dtmDateOfBirth = rsCommon("DOB")
        blnPMNotify = CBool(rsCommon("PM_notify"))

        'If we are in admin mode then read on extra user details
        If portal.variablesForum.blnAdminMode Then
                intUsersGroupID = CInt(rsCommon("Group_ID"))
                blnUserActive = CBool(rsCommon("Active"))
                lngPosts = CLng(rsCommon("No_of_posts"))
        End If

        'Reset Server Objects
        rsCommon.Close

	'If the user has enterd a date format then place in array
	If NOT portal.variablesForum.strDateFormat = "" Then funcFecha.saryDateTimeData(0) = portal.variablesForum.strDateFormat

        'If admin mode is on and the user is only a moderator and the edited account is an admin account then the modertor can not edit the account
        If portal.variablesForum.blnAdminMode AND portal.variablesForum.blnModerator AND intUsersGroupID = 1 Then


                'clean up before redirecting
                Set rsCommon = Nothing
		adoCon.Close
		Set adoCon = Nothing

                'redirect to insufficient permissions page
                Response.Redirect("insufficient_permission.aspx?FID=" & portal.variablesForum.intForumID)
        End If


        'Split the date of biith into the various parts
        If isDate(dtmDateOfBirth) Then
	        intDOBYear = Year(dtmDateOfBirth)
		intDOBMonth = Month(dtmDateOfBirth)
		intDOBDay = Day(dtmDateOfBirth)
	End If
End If



'******************************************
'***  	    De-code signature		***
'******************************************

'Covert the signature back to forum codes
If strSignature <> "" Then  strSignature = EditPostConvertion(strSignature)


%>
<html>
<head>

<title><% If strMode = "update" Then Response.Write("Edit Profile") Else Response.Write("Register New User") %></title>


     	

<!-- Check the from is filled in correctly before submitting -->
<script language="javascript">

//Function to check form is filled in correctly before submitting
function CheckForm () {

        //Initialise variables
        var errorMsg = "";
        var errorMsgLong = "";
<%
'If this is new reg then make sure the user eneters a usuario and clave
If strMode ="new" Then 
	
	%>
        //Check for a usuario
        if (document.frmRegister.name.value.length <= 3){
                errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorusuarioChar %>";
        }

        //Check for a clave
        if (document.frmRegister.clave.value.length <= 3){
                errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorclaveChar %>";
        }
<%

'If this is an update only check the clave length if the user is enetring a new clave
ElseIf intUpdatePartNumber = 0 OR intUpdatePartNumber = 1 Then 
	
	%>
        //Check for a clave
        if ((document.frmRegister.clave.value.length <= 3) && (document.frmRegister.clave.value.length > 0)){
                errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorclaveChar %>";
        }<%
        
End If 

'If this is not showing the reg part or all the form then don't run the clave and email check js
If intUpdatePartNumber = 0 OR intUpdatePartNumber = 1 Then
	
	%>
        //Check both claves are the same
        if ((document.frmRegister.clave.value) != (document.frmRegister.clave2.value)){
                errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorclaveNoMatch %>";
                document.frmRegister.clave.value = ""
                document.frmRegister.clave2.value = ""
        }

        //If an e-mail is entered check that the e-mail address is valid
        if (<%

	'If e-mail activation is on check that the e-mail address entered is correct
	If portal.variablesForum.blnEmailActivation = True Then
	       
	        Response.Write("document.frmRegister.email.value == """" || ")
	Else
	        
	        Response.Write("document.frmRegister.email.value.length >0 && ")
	End If
                %>(document.frmRegister.email.value.indexOf("@",0) == -1||document.frmRegister.email.value.indexOf(".",0) == -1)) {
                errorMsg +="\n\t<% = portal.variablesForum.strTxtErrorValidEmail %>";
<%
	'If e-mail activation is not on display a long error message to the user if they enter an incorrect e-mail addres
	If NOT portal.variablesForum.blnEmailActivation = True Then Response.Write("          errorMsgLong += ""\n- " & portal.variablesForum.strTxtErrorValidEmailLong & """; ")
%>
        }

        //Check to make sure the user is not trying to show their email if they have not entered one
        if (document.frmRegister.email.value == "" && document.frmRegister.emailShow[0].checked == true){
                errorMsgLong += "\n- <% = portal.variablesForum.strTxtErrorNoEmailToShow %>";
                document.frmRegister.emailShow[1].checked = true
                document.frmRegister.email.focus();
        }
<%

End If


'If this is new reg then make sure the user eneters a usuario and clave
If strMode ="new" Then 
	%>
	//Check for a security code
        if (document.frmRegister.securityCode.value == ''){
                errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorSecurityCode %>";
        }<%

End If


'If long reg form is not on then don't need to check the lengh of the signature
If ((blnLongRegForm AND strMode = "new") OR (strMode <> "new")) AND (intUpdatePartNumber = 0 OR intUpdatePartNumber = 2) Then
%>
        //Check that the signature is not above 200 chracters
        if (document.frmRegister.signature.value.length > 200){
                errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorSignatureToLong %>";
                errorMsgLong += "\n- <% = portal.variablesForum.strTxtYouHave %> " + document.frmRegister.signature.value.length + " <% = portal.variablesForum.strTxtCharactersInYourSignatureToLong %>";
        }
<%
End If
%>
        //If there is aproblem with the form then display an error
        if ((errorMsg != "") || (errorMsgLong != "")){
                msg = "<% = portal.variablesForum.strTxtErrorDisplayLine %>\n\n";
                msg += "<% = portal.variablesForum.strTxtErrorDisplayLine1 %>\n";
                msg += "<% = portal.variablesForum.strTxtErrorDisplayLine2 %>\n";
                msg += "<% = portal.variablesForum.strTxtErrorDisplayLine %>\n\n";
                msg += "<% = portal.variablesForum.strTxtErrorDisplayLine3 %>\n";

                errorMsg += alert(msg + errorMsg + "\n" + errorMsgLong);
                return false;
        }

        //Reset the submition action
        document.frmRegister.action = "register.aspx?FID=<% = Server.HTMLEncode(portal.variablesForum.intForumID) %>"
        document.frmRegister.target = "_self";

        return true;
}

//Function to count the number of characters in the signature text box
function DescriptionCharCount() {
        document.frmRegister.countcharactem_rs.value = document.frmRegister.signature.value.length;
}
</script>
<!-- #include file="includes/header.aspx" -->
   <navigation:navigation ID="common1" runat="server" />
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="3" align="center">
  <tr>
  <td class="heading"><% If strMode = "update" Then Response.Write(portal.variablesForum.strTxtEditProfile) Else Response.Write(portal.variablesForum.strTxtRegisterNewUser) %></td>
</tr>
 <tr>
  <td width="60%" class="bold"><img src="<% = portal.variablesForum.strImagePath %>open_folder_icon.gif" border="0" align="middle">&nbsp;<a href="default.aspx" target="_self" class="boldLink"><% = strMainForumName %></a><% = strNavSpacer %><% If strMode = "update" Then Response.Write(portal.variablesForum.strTxtEditProfile) Else Response.Write(portal.variablesForum.strTxtRegisterNewUser) %></td><%

'If this is an update and email notify is on show link to email subcriptions
If strMode = "update" AND lngUserProfileID <> 2 Then
 
	Response.Write(vbCrlf & "  <td width=""40%"" align=""right"" nowrap=""nowrap""><a href=""member_control_panel.aspx")
	'If this is in admin mode then add the profile to be edited
	If portal.variablesForum.blnAdminMode Then Response.Write("?PF=" & lngUserProfileID & "&amp;m=A")
	Response.Write(""" target=""_self""><img src=""" & portal.variablesForum.strImagePath & "cp_menu.gif"" border=""0"" alt=""" & portal.variablesForum.strTxtMemberCPMenu & """></a>")

	
	Response.Write("<a href=""register.aspx")
	'If this is in admin mode then add the profile to be edited
	If portal.variablesForum.blnAdminMode Then Response.Write("?PF=" & lngUserProfileID & "&amp;m=A")
	Response.Write(""" target=""_self""><img src=""" & portal.variablesForum.strImagePath & "profile.gif"" border=""0"" alt=""" & portal.variablesForum.strTxtEditProfile & """></a>")

	'email notify is on show link to email subcriptions
	If portal.variablesForum.blnEmail Then
			
		Response.Write("<a href=""email_notify_subscriptions.aspx")	
		'If this is in admin mode then allow the admin or modertor change this users email subscriptions
		If portal.variablesForum.blnAdminMode Then Response.Write("?PF=" & lngUserProfileID & "&amp;m=A")
		Response.Write(""" target""_self""><img src=""" & portal.variablesForum.strImagePath & "email_notify.gif"" border=""0"" alt=""" & portal.variablesForum.strTxtEmailNotificationSubscriptions & """></a>")
	End If
End If

Response.Write("  </td>" & _
vbCrLf & " </tr>" & _
vbCrLf & "</table>")

'If an error has occured display what the error is, for those without JS
If blnusuarioOK = False OR portal.variablesForum.blnEmailOK = False OR portal.variablesForum.blnEmailBlocked OR blnSecurityCodeOK = False OR blnConfirmPassOK = false Then
	
	Response.Write("<br /><table width=""" & portal.variablesForum.strTableVariableWidth & """ border=""0"" cellspacing=""0"" cellpadding=""3"" align=""center"">")
	Response.Write(vbCrLf & "  <tr>")
	Response.Write(vbCrLf & "  <td align=""center"" class=""error"">")

         'If the usuario is already gone diaply an error message pop-up
        If blnusuarioOK = False Then Response.Write(Replace(portal.variablesForum.strTxtUsrenameGone, "\n\n", "<br />") & "<br /><br />")

        'If the email address is used up and email activation is on, display an error message
        If portal.variablesForum.blnEmailOK = False Then Response.Write(Replace(portal.variablesForum.strTxtEmailAddressAlreadyUsed, "\n\n", "<br />") & "<br /><br />")

        'If the email address or domain is blocked
        If portal.variablesForum.blnEmailBlocked = True Then Response.Write(portal.variablesForum.strTxtEmailAddressBlocked & "<br /><br />")
        
        'If the security code is incorrect
        If blnSecurityCodeOK = False Then Response.Write(Replace(portal.variablesForum.strTxtSecurityCodeDidNotMatch, "\n\n", "<br />") & "<br /><br />")
        
        'If the confirmed clave is incorrect
        If blnConfirmPassOK = False Then Response.Write(Replace(portal.variablesForum.strTxtConformOldPassNotMatching, "\n\n", "<br />") & "<br /><br />")


	Response.Write(" </td>" & _
	vbCrLf & "</tr>" & _
	vbCrLf & "</table>")

End If


%>
<br />
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr><form method="post" name="frmRegister" action="register.aspx?FID=<% = Server.HTMLEncode(portal.variablesForum.intForumID) %>" onReset="return confirm('<% = strResetFormConfirm %>');">
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>">
      <table width="100%" border="0" cellspacing="1" cellpadding="3" height="14" bgcolor="<% = portal.variablesForum.strTableBgColour %>"><%
 
 
 
     
'************************************
'****    Registration Details    **** 
'************************************

'If part number = 0 (all) or part 1 (reg details) then show reg details
If intUpdatePartNumber = 0 OR intUpdatePartNumber = 1 Then
	
     %>
    <tr bgcolor="<% = portal.variablesForum.strTableTitleColour %>">
      <td colspan="2" background="<% = portal.variablesForum.strTableTitleBgImage %>" class="tHeading"><% = portal.variablesForum.strTxtRegistrationDetails %></td>
     </tr>
        <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td colspan="2" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text">*<% = portal.variablesForum.strTxtRequiredFields %></td>
     </tr>
        <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" >
         <td width="50%"  bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"><% = portal.variablesForum.strTxtusuario %>*<br /><span class="smText"><% = portal.variablesForum.strTxtProfileusuarioLong  %></span></td>
         <td width="50%" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"><%

	'If this is a new registration display a filed for the usuario
	If strMode = "new" Then
        %>
         <input type='text' name="name" size="15" maxlength="15" value="<% = strusuario %>" /><%

	Else
      		Response.Write(strusuario)
	End If

%></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"><% If strMode = "new" Then Response.Write(portal.variablesForum.strTxtclave & "*") Else Response.Write(portal.variablesForum.strTxtNewclave) %></td>
         <td width="50%" valign="top" background="<% = portal.variablesForum.strTableBgImage %>"><input type="clave" name="clave" size="15" maxlength="15" /></td>
     </tr>
     <tr  bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  height="2" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% If strMode = "new" Then Response.Write(portal.variablesForum.strTxtRetypeclave & "*") Else Response.Write(portal.variablesForum.strTxtRetypeNewclave) %></td>
         <td width="50%" valign="top" height="2" background="<% = portal.variablesForum.strTableBgImage %>"><input type="clave" name="clave2" size="15" maxlength="15" /></td>
     </tr><%
      	'If update confirm old pass if changing clave
      	If strMode ="update" AND portal.variablesForum.blnAdminMode = false Then
%>   
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"><% Response.Write(portal.variablesForum.strTxtConfirmOldPass) %></td>
         <td width="50%" valign="top" background="<% = portal.variablesForum.strTableBgImage %>"><input type="clave" name="oldPass" size="15" maxlength="15" /></td>
     </tr><%
	End If   
	
	%>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtEmail %><%

	'If email activation is on then tell the user for a real email address
	If portal.variablesForum.blnEmailActivation = True Then
	
	        If strMode = "new" Then
	                Response.Write("*<br /><span class=""smText"">" & portal.variablesForum.strTxtEmailRequiredForActvation & "</span><br />")
	        Else
	                Response.Write("*<br /><span class=""smText"">" & portal.variablesForum.strTxtCahngeOfEmailReactivateAccount & "</span><br />")
	        End If
	Else
	        Response.Write("         <br /><span class=""smText"">" & portal.variablesForum.strTxtProfileEmailLong & "</span><br />")
	End If

         %></td>
         <td width="50%" valign="top" background="<% = portal.variablesForum.strTableBgImage %>">
          <input type='text' name="email" size="30" maxlength="60" value="<% = strEmail %>" />&nbsp;</td>
     </tr><%
End If 
 



'*********************************
'****      Security Code      **** 
'*********************************

'If this is a new reg then ask for a seurity code
If strMode = "new" Then
    
     %>
     <tr bgcolor="<% = portal.variablesForum.strTableTitleColour %>">
      <td colspan="2" background="<% = portal.variablesForum.strTableTitleBgImage %>" class="tHeading"><% = portal.variablesForum.strTxtSecurityCodeConfirmation %></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtUniqueSecurityCode %><br /><span class="smText"><% = portal.variablesForum.strTxtCookiesMustBeEnabled %></span></td>
         <td width="50%" valign="top" background="<% = portal.variablesForum.strTableBgImage %>"><img src="security_image.aspx?I=1&<% = hexValue(3) %>" /><img src="security_image.aspx?I=2&<% = hexValue(3) %>" /><img src="security_image.aspx?I=3&<% = hexValue(3) %>" /><img src="security_image.aspx?I=4&<% = hexValue(3) %>" /><img src="security_image.aspx?I=5&<% = hexValue(3) %>" /><img src="security_image.aspx?I=6&<% = hexValue(3) %>" /></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtConfirmSecurityCode %><br /><span class="smText"><% = portal.variablesForum.strTxtEnter6DigitCode %></span></td>
         <td width="50%" valign="top" background="<% = portal.variablesForum.strTableBgImage %>"><input type='text' name="securityCode" size="12" maxlength="12" autocomplete="off" /></td>
     </tr><%
End If


 
     
'***********************************************
'****    Profile Information (not required) **** 
'***********************************************

If intUpdatePartNumber = 0 OR intUpdatePartNumber = 2 Then  

     %>
     <tr bgcolor="<% = portal.variablesForum.strTableTitleColour %>">
      <td colspan="2" background="<% = portal.variablesForum.strTableTitleBgImage %>" class="tHeading"><% = portal.variablesForum.strTxtProfileInformation %></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtRealName %></td>
         <td width="50%" background="<% = portal.variablesForum.strTableBgImage %>"><input type='text' name="realName" size="30" maxlength="27" value="<% Response.Write strRealName %>" /></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtLocation %></td>
         <td width="50%" background="<% = portal.variablesForum.strTableBgImage %>">
          <select name=location>
        <option value="<% = strLocation %>" selected><% If strLocation = "" Or strLocation = null Then response.write("-- " & portal.variablesForum.strTxtSelectCountry & " --" Else Response.Write strLocation %></option>
        <!-- Include countires include file -->
        <!-- #include file="includes/select_countries_list.aspx" -->
       </select>
         </td>
     </tr><%
	
	'If new reg don't show everything
	If ((blnLongRegForm AND strMode = "new") OR strMode <> "new") then

%>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtHomepage %></td>
         <td width="50%" background="<% = portal.variablesForum.strTableBgImage %>"><input type='text' name="homepage" size="30" maxlength="48" value="<% If strHomepage = "" Then response.write("http://" Else Response.Write strHomepage %>" /></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtICQNumber %></td>
         <td width="50%" background="<% = portal.variablesForum.strTableBgImage %>"><input type='text' name="ICQ" size="15" maxlength="15" value="<% = strICQNum %>" /></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtAIMAddress %></td>
         <td width="50%" background="<% = portal.variablesForum.strTableBgImage %>"><input type='text' name="AIM" size="30" maxlength="60" value="<% = strAIMAddress %>" /></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtMSNMessenger %></td>
         <td width="50%" background="<% = portal.variablesForum.strTableBgImage %>"><input type='text' name="MSN" size="30" maxlength="60" value="<% = strMSNAddress %>" /></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtYahooMessenger %></td>
         <td width="50%" background="<% = portal.variablesForum.strTableBgImage %>"><input type='text' name="Yahoo" size="30" maxlength="60" value="<% = strYahooAddress %>" /></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtOccupation %></td>
         <td width="50%" background="<% = portal.variablesForum.strTableBgImage %>"><input type='text' name="occupation" size="30" maxlength="40" value="<% = strOccupation %>" /></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtInterests %></td>
         <td width="50%" background="<% = portal.variablesForum.strTableBgImage %>"><input type='text' name="interests" size="30" maxlength="130" value="<% = strInterests %>" /></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtDateOfBirth %></td>
         <td width="50%" background="<% = portal.variablesForum.strTableBgImage %>" class="text"><% = portal.variablesForum.strTxtDay %>
       <select name="DOBday">
        <option value="0" <% If intDOBDay = 0 Then Response.Write("selected") %>>----</option><%

		'Create lists day's for birthdays
		For lngLoopCounter = 1 to 31
			Response.Write(VbCrLf & "        <option value=""" & lngLoopCounter & """")
			If intDOBDay = lngLoopCounter Then Response.Write("selected") 
			Response.Write(">" & lngLoopCounter & "</option>")
		Next
        
%>       
       </select>
       <% = portal.variablesForum.strTxtCMonth %> <select name="DOBmonth">
       <option value="0" <% If intDOBMonth = 0 Then Response.Write("selected") %>>---</option><%

		'Create lists of days of the month for birthdays
		For lngLoopCounter = 1 to 12
			Response.Write(VbCrLf & "        <option value=""" & lngLoopCounter & """")
			If intDOBMonth = lngLoopCounter Then Response.Write("selected") 
			Response.Write(">" & lngLoopCounter & "</option>")
		Next
        
%>
       </select>
       <% = portal.variablesForum.strTxtCYear %> <select name="DOByear">
       <option value="0" <% If intDOBYear = 0 Then Response.Write("selected") %>>-----</option><%

		'Create lists of years for birthdays
		For lngLoopCounter = 1910 to 2004
			Response.Write(VbCrLf & "        <option value=""" & lngLoopCounter & """")
			If intDOBYear = lngLoopCounter Then Response.Write("selected") 
			Response.Write(">" & lngLoopCounter & "</option>")
		Next
        
%>       
       </select>
         </td>
     </tr><%
End If

	'------------- Avatar ---------------

	'If avatars are enabled then let the user select an avatar
	If blnAvatar = True Then
%>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td valign="top" height="2" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtSelectAvatar %>
          <br /><span class="smText"><% = portal.variablesForum.strTxtSelectAvatarDetails & intAvatarHeight & " x " & intAvatarWidth & " " & portal.variablesForum.strTxtPixels %></span></td>
         <td valign="top" height="2" background="<% = portal.variablesForum.strTableBgImage %>" >
          <table width="290" border="0" cellspacing="0" cellpadding="1">
        <tr>
         <td width="168">
          <select name="SelectAvatar" size="4" onChange="(avatar.src = SelectAvatar.options[SelectAvatar.selectedIndex].value) && (txtAvatar.value='http://') && (oldAvatar.value='')">
           <option value="<% = portal.variablesForum.strImagePath %>blank.gif" /><% = portal.variablesForum.strTxtNoneSelected %></option>
           <!-- #include file="includes/select_avatar.aspx" -->
          </select>
         </td>
         <td width="122" align="center"><img src="<%

		'If there is an avatar then display it
		If strAvatar <> "" Then
		     	Response.Write(strAvatar)
		Else
			Response.Write(portal.variablesForum.strImagePath & "blank.gif")
		End If
                %>" width="<% = intAvatarWidth %>" height="<% = intAvatarHeight %>" name="avatar">
          <input type="hidden" name="oldAvatar" value="<% = strAvatar %>" /></td>
        </tr>
        <tr>
         <td width="168">
          <input type='text' name="txtAvatar" size="30" maxlength="95" value="<%

		'If the avatar is the persons own then display the link
		If InStr(1, strAvatar, "http://") > 0 Then
			Response.Write(strAvatar)
		Else
			Response.Write("http://")
		End If
        %>" onChange="oldAvatar.value=''" />
         </td>
         <td width="122">
          <input type="button" name="preview" value="<% = portal.variablesForum.strTxtPreview %>" onClick="avatar.src = txtAvatar.value" />  
         </td>
        </tr>
       </table><%

		'If avatar uploading is enabled and the user is registered then have a link to it
		If blnAvatarUploadEnabled AND portal.variablesForum.intGroupID <> 2 AND blnActiveMember Then 
	
	%>
	<a href="javascript:openWin('upload_avatam_rs.aspx','avatars','toolbar=0,location=0,status=0,menubar=0,scrollbars=0,resizable=1,width=400,height=150')" class="smLink"><% = portal.variablesForum.strTxtAvatarUpload %></a>
<%	
		End If
%>       
         </td>
     </tr><%
	End If

'-----------------------------------------------

	
	'If new reg don't show everything
	If ((blnLongRegForm AND strMode = "new") OR strMode <> "new") then

%>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td valign="top" height="2" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtSignature %><br />
          <span class="smText"><% = portal.variablesForum.strTxtSignatureLong %>&nbsp;(max 200 characters)<br />
          <br />
          <br />
          <a href="JavaScript:openWin('forum_codes.aspx','codes','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=550,height=400')" class="smLink"><% = portal.variablesForum.strTxtForumCodes %></a> <% = portal.variablesForum.strTxtForumCodesInSignature %></span></td>
         <td valign="top" height="2" background="<% = portal.variablesForum.strTableBgImage %>">
          <textarea name="signature" cols="30" rows="3" onKeyDown="DescriptionCharCount();" onKeyUp="DescriptionCharCount();"><% = strSignature %></textarea>
         <br /><input size="3" value="0" name="countcharacters" maxlength="3" />
          <input onClick="DescriptionCharCount();" type="button" value="<% = portal.variablesForum.strTxtCharacterCount %>" name="Count" />
          &nbsp;&nbsp;<span class="smText"><a href="javascript:OpenPreviewWindow('signature_preview.aspx', document.frmRegister)" class="smLink"><% = portal.variablesForum.strTxtSignaturePreview %></a>
         </td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtAlwaysAttachMySignature %></td>
         <td width="50%" valign="top" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtYes %><input type="radio" name="attachSig" value="true" <% If blnAttachSignature = True Then response.write("checked" %> />&nbsp;&nbsp;<% = portal.variablesForum.strTxtNo %><input type="radio" name="attachSig" value="false" <% If blnAttachSignature = False Then response.write("checked" %> /></td>
     </tr><%
     
	End If
End If
 
 
 
     
'*********************************
'****    Forum Preferences    **** 
'*********************************

'If part number = 0 (all) or part 3 (forum preferences) then show reg details
If intUpdatePartNumber = 0 OR intUpdatePartNumber = 3 Then
    
     %>
     <tr bgcolor="<% = portal.variablesForum.strTableTitleColour %>">
      <td colspan="2" background="<% = portal.variablesForum.strTableTitleBgImage %>" class="tHeading"><% = portal.variablesForum.strTxtForumPreferences %></td>
     </tr><%
     
     	'If this is an update and only showing part 3 of the form with no email address entered don't show the 'show email' part of the form
     	If (intUpdatePartNumber = 3 AND strEmail <> "") OR intUpdatePartNumber = 0 Then
     		
     		%>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtShowHideEmail %>
          <br /><span class="smText"><% = portal.variablesForum.strTxtShowHideEmailLong %></span></td>
         <td width="50%" valign="top" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtYes %><input type="radio" name="emailShow" value="True" <% If blnShowEmail = True Then response.write("checked" %> />&nbsp;&nbsp;<% = portal.variablesForum.strTxtNo %><input type="radio" name="emailShow" value="False" <% If blnShowEmail = False Then response.write("checked" %> />
         </td>
     </tr><%
	
	End If

	'If email notify is on give them a choice to receive mail or not
	If portal.variablesForum.blnEmail = True Then 
		%>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtNotifyMeOfReplies %>
          <br /><span class="smText"><% = portal.variablesForum.strTxtSendsAnEmailWhenSomeoneRepliesToATopicYouHavePostedIn %></span></td>
         <td width="50%" valign="top" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtYes %><input type="radio" name="replyNotify" value="True" <% If portal.variablesForum.blnReplyNotify = True Then response.write("checked" %> />&nbsp;&nbsp;<% = portal.variablesForum.strTxtNo %><input type="radio" name="replyNotify" value="False" <% If portal.variablesForum.blnReplyNotify = False Then response.write("checked" %> />
         </td>
     </tr><%

        	'If private messageing is also on let them decide if they want to receive email notification when they get em
        	If blnPrivateMessages = True Then 
        		%>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtNotifyMeOfPrivateMessages %></td>
         <td width="50%" valign="top" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtYes %><input type="radio" name="pmNotify" value="True" <% If blnPMNotify = True Then response.write("checked" %> />&nbsp;&nbsp;<% = portal.variablesForum.strTxtNo %><input type="radio" name="pmNotify" value="False" <% If blnPMNotify = False Then response.write("checked" %> /></td>
     </tr><%
        	End If
	End If

	'If the IE WYSIWYG Editor is on let the user select if they want to use it or not
	If blnRTEEditor = True Then
%>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtEnableTheWindowsIEWYSIWYGPostEditor %></td>
         <td width="50%" valign="top" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtYes %><input type="radio" name="ieEditor" value="True" <% If blnWYSIWYGEditor = True Then response.write("checked" %> />&nbsp;&nbsp;<% = portal.variablesForum.strTxtNo %><input type="radio" name="ieEditor" value="False" <% If blnWYSIWYGEditor = False Then response.write("checked" %> /></td>
     </tr><%
	End If

     %>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtProfileAutoLogin %></td>
         <td width="50%" valign="top" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtYes %><input type="radio" name="Login" value="True" <% If blnAutoLogin = True Then response.write("checked" %> />&nbsp;&nbsp;<% = portal.variablesForum.strTxtNo %><input type="radio" name="Login" value="False" <% If blnAutoLogin = False Then response.write("checked" %> /></td>
     </tr>
      <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtTimezone %>
         <br /><span class="smText"><% = portal.variablesForum.strTxtPresentServerTimeIs %><% 
         
	'Get the current server time
	dtmServerTime = Now()
	
	'Make sure that the time and date format function isn't effected by the server time off set
	If portal.variablesForum.strTimeOffSet = "-" Then
		dtmServerTime = DateAdd("h", + portal.variablesForum.intTimeOffSet, dtmServerTime)
	ElseIf portal.variablesForum.strTimeOffSet = "+" Then
		dtmServerTime = DateAdd("h", - portal.variablesForum.intTimeOffSet, dtmServerTime)
	End If         

	'Display the current server time      
	Response.Write(funcFecha.DateFormat(dtmServerTime, funcFecha.saryDateTimeData) & " " & portal.variablesForum.strTxtAt & " " & funcFecha.TimeFormat(dtmServerTime, funcFecha.saryDateTimeData)) 

%></span></td>
         <td width="50%" valign="top" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><select name="serverOffSet">
        <option value="+" <% If portal.variablesForum.strTimeOffSet = "+" Then Response.Write("selected") %>>+</option>
        <option value="-" <% If portal.variablesForum.strTimeOffSet = "-" Then Response.Write("selected") %>>-</option>
       </select>
       <select name="serverOffSetHours"><%

	'Create list of time off-set
	For lngLoopCounter = 0 to 24
		Response.Write(VbCrLf & "        <option value=""" & lngLoopCounter & """")
		If portal.variablesForum.intTimeOffSet = lngLoopCounter Then Response.Write("selected") 
		Response.Write(">" & lngLoopCounter & "</option>")
	Next
        
%>       
       </select> <% = portal.variablesForum.strTxtHours %></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtDateFormat %></td>
         <td width="50%" valign="top" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><select name="funcFecha.DateFormat">
        <option value="dd/mm/yy" <% If funcFecha.saryDateTimeData(0) = "dd/mm/yy" Then Response.Write("selected") %>><% = portal.variablesForum.strTxtDayMonthYear %></option>
        <option value="mm/dd/yy" <% If funcFecha.saryDateTimeData(0) = "mm/dd/yy" Then Response.Write("selected") %>><% = portal.variablesForum.strTxtMonthDayYear %></option>
        <option value="yy/mm/dd" <% If funcFecha.saryDateTimeData(0) = "yy/mm/dd" Then Response.Write("selected") %>><% = portal.variablesForum.strTxtYearMonthDay %></option>
        <option value="yy/dd/mm" <% If funcFecha.saryDateTimeData(0) = "yy/dd/mm" Then Response.Write("selected") %>><% = portal.variablesForum.strTxtYearDayMonth %></option>
       </select></td>
     </tr><%
End If




'*********************************************
'****    Admin and Moderator Functions    **** 
'*********************************************

'If the admin mode is enabled then place some extra options in the edit profile (unless this is the Guest or Admin accounts)
If portal.variablesForum.blnAdminMode AND (portal.variablesForum.blnAdmin Or portal.variablesForum.blnModerator) Then
     
     %>  
     <tr bgcolor="<% = portal.variablesForum.strTableTitleColour %>">
      <td colspan="2" background="<% = portal.variablesForum.strTableTitleBgImage %>" class="tHeading"><a name="admin"></a><% = portal.variablesForum.strTxtAdminModeratorFunctions %></td>
     </tr><%
     
     	'Don't allow changing group if admin or guest account
     	If lngUserProfileID > 2 Then
     %>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtUserIsActive %></td>
         <td width="50%" valign="top" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtYes %><input type="radio" name="active" value="True" <% If blnUserActive = True Then response.write("checked" %>>&nbsp;&nbsp;<% = portal.variablesForum.strTxtNo %><input type="radio" name="active" value="False" <% If blnUserActive = False Then response.write("checked" %> />
         </td>
     </tr><%

	        'Only allow admin to change the member group
	        If portal.variablesForum.blnAdmin Then
	
	
	                'Get the forum groups from the database so admin can change the members group
	
	                'Initlise SQL query
	                strSQL = "SELECT " & portal.variablesForum.strDbTable & "Group.Group_ID, " & portal.variablesForum.strDbTable & "Group.Name, " & portal.variablesForum.strDbTable & "Group.Special_rank, " & portal.variablesForum.strDbTable & "Group.Minimum_posts FROM " & portal.variablesForum.strDbTable & "Group;"
	
	                'Query the database
	                rsCommon=db.execute(strSQL)
	
	                'If there are groups then disply them
	                If NOT rsCommon.Eof Then
		

     %>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtGroup %></td>
         <td width="50%" valign="top" class="smText" background="<% = portal.variablesForum.strTableBgImage %>">
          <select name="group"><%

	                        'Loop round to display all the groups
	                        Do While NOT rsCommon.EOF
	
	                                Dim intSelGroupID
	                                Dim strSelGroupName
	                                Dim blnSelSpecialGroup
	                                Dim lngSelMinimumRankPosts
	
	                                'Read in the recordset
	                                intSelGroupID = CInt(rsCommon("Group_ID"))
	                                strSelGroupName = rsCommon("Name")
	                                blnSelSpecialGroup = CBool(rsCommon("Special_rank"))
	                                lngSelMinimumRankPosts = CLng(rsCommon("Minimum_posts"))
	
	                                'Display the selection
	                                Response.Write("<option value=""" & intSelGroupID & """")
	
	                                'If this is the group the member is part of then have it slected
	                                If intUsersGroupID = intSelGroupID Then Response.Write(" selected")
	
	                                'Display the end of the select option
	                                If blnSelSpecialGroup Then
	                                        Response.Write(">" & strSelGroupName & " - " & portal.variablesForum.strTxtNonRankGroup & "</option>" & vbCrLf)
	                                Else
	                                        Response.Write(">" & strSelGroupName & " - " & portal.variablesForum.strTxtRankGroupMinPosts & " " & lngSelMinimumRankPosts & "</option>" & vbCrLf)
	                                End If
	
	                                'Move to the next record
	                                rsCommon.MoveNext
	
	                        Loop
%>	</select></td>
     </tr><%

                	End If
                End If
	End If
     %>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtMemberTitle %></td>
         <td width="50%" valign="top" class="smText" background="<% = portal.variablesForum.strTableBgImage %>"><input type='text' name="memTitle" size="30" maxlength="40" value="<% = strMemberTitle %>" /></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtNumberOfPosts %></td>
         <td width="50%" valign="top" class="smText" background="<% = portal.variablesForum.strTableBgImage %>"><input type='text' name="posts" size="4" maxlength="7" value="<% = lngPosts %>" /></td>
     </tr><%
     
     	'Don't allow deleting account if admin or guest account
     	If lngUserProfileID > 2 AND portal.variablesForum.blnAdmin Then
     		%>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtDeleteThisUser %></td>
         <td width="50%" valign="top" class="smText" background="<% = portal.variablesForum.strTableBgImage %>"><input type='checkbox' name="delete" value="true" /><% = portal.variablesForum.strTxtCheckThisBoxToDleteMember %></td>
     </tr><%
	End If

End If

%>
     <tr bgcolor="<% = strTableBottomRowColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td valign="top" height="2" colspan="2" align="center" background="<% = portal.variablesForum.strTableBgImage %>"><%

'If this is admin mode then set the admin stuff up        
If portal.variablesForum.blnAdminMode AND (portal.variablesForum.blnAdmin Or portal.variablesForum.blnModerator) Then
        
        %>
        <input type="hidden" name="M" value="A" />
        <input type="hidden" name="PF" value="<% = lngUserProfileID %>" /><%
End If
%>        
        <input type="hidden" name="mode" value="<% = strMode %>" />
        <input type="hidden" name="FPN" value="<% = intUpdatePartNumber %>" />
        <input type="hidden" name="sessionID" value="<% = Session.SessionID %>" />
        <input type='submit' name="Submit" value="<% If strMode = "new" Then Response.Write(portal.variablesForum.strTxtRegister) Else Response.Write(portal.variablesForum.strTxtUpdateProfile) %>" onClick="return CheckForm();" />
        <input type="reset" name="Reset" value="<% = portal.variablesForum.strTxtResetForm %>" />
      </td>
     </tr>
    </table>
      </td>
    </tr>
  </table>
  </td>
 </form></tr>
</table><br />
<div align="center"><%

'Release server objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing


'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"
%>
</div><%


'If the usuario is already gone display an error message pop-up
If blnusuarioOK = False Then
        Response.Write("<script  language=""JavaScript"">")
        Response.Write("alert('" & portal.variablesForum.strTxtUsrenameGone & "');")
        Response.Write("</script>")

End If

'If the email address is used up and email activation is on, display an error message
If portal.variablesForum.blnEmailOK = False Then
        Response.Write("<script  language=""JavaScript"">")
        Response.Write("alert('" & portal.variablesForum.strTxtEmailAddressAlreadyUsed & ".');")
        Response.Write("</script>")
End If

'If the email address or domain is blocked
If portal.variablesForum.blnEmailBlocked Then
        Response.Write("<script  language=""JavaScript"">")
        Response.Write("alert('" & portal.variablesForum.strTxtEmailAddressBlocked & ".');")
        Response.Write("</script>")
End If

'If the security code did not match
If blnSecurityCodeOK = False Then
        Response.Write("<script  language=""JavaScript"">")
        Response.Write("alert('" & portal.variablesForum.strTxtSecurityCodeDidNotMatch & ".');")
        Response.Write("</script>")
End If


'If the confirmed clave is incorrect
If blnConfirmPassOK = False Then
        Response.Write("<script  language=""JavaScript"">")
        Response.Write("alert('" & portal.variablesForum.strTxtConformOldPassNotMatching & ".');")
        Response.Write("</script>")
End If

%>

