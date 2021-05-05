<%
'******************************************
'***	   File Upload Function        ****
'******************************************

'Function to upload a file
Private Function fileUpload(ByVal strFileUploadPath, ByVal saryFileUploadTypes, ByVal intMaxFileSize, ByVal strUploadComponent, ByRef lngErrorFileSize, ByRef blnExtensionOK, ByRef strUserFolderName)

	'Dimension variables
	Dim objUpload		'Uplaod component
	Dim strNewFileName	'Holds the file name
	Dim strOriginalFileName	'Holds the original file name for those components that need to save the file first
	Dim objFSO		'Holds the file system object
	
	
	
	
	'******************************************
	'***   Create a folder for the user    ****
	'******************************************
	
		
	'Creat n intence of the FSO object
	Set objFSO = Server.CreateObject("Scripting.FileSystemObject")
	
	'See if the user already has a folder
	If objFSO.FolderExists(Server.MapPath(strFileUploadPath & "\" & strUserFolderName)) = False Then
			
		'If the user dosen't have a folder create them one
		objFSO.CreateFolder(Server.MapPath(strFileUploadPath & "\" & strUserFolderName))
	
	End If
	
	'Release the FSO object
	Set objFSO = Nothing
	
	'Add the usuarios folder name to the upload folder name
	strFileUploadPath = strFileUploadPath & "\" & strUserFolderName
	


	'******************************************
	'***	     Upload components         ****
	'******************************************

	'Select which upload component to use
	Select Case strUploadComponent


		'******************************************
		'***     Persits AspUpload component   ****
		'******************************************

		'Persits AspUpload upload component - tested with version 3.0
		Case "AspUpload"

			'Create upload object
			Set objUpload = Server.CreateObject("Persits.Upload.1")

			With objUpload


				'make sure files arn't over written
				.OverwriteFiles = False

				'We need to save the file before we can find out anything about it
				'** Save virtual is used as saving to memory is often disabled by the web host **
				.SaveVirtual strFileUploadPath

				'Get the file name
				strNewFileName = .Files(1).ExtractFileName

				'Replace spaces with underscores
				strNewFileName = Replace(strNewFileName, " ", "_", 1, -1, 1)

				
				'Filter file name to remove anything that isn't allowed by the filters
				strNewFileName = formatFileName(strNewFileName)

				'Check the file size is not above the max allowed size, this is done using a function not the compoent to stop an exception error
				lngErrorFileSize = fileSize(.Files(1).Size, intMaxFileSize)

				'Loop through all the allowed extensions and see if the file has one
				blnExtensionOK = fileExtension(strNewFileName, saryFileUploadTypes)

				'If the file is OK save it to disk
				If lngErrorFileSize = 0 AND blnExtensionOK = True Then


					'Create a new file name for the file with the date a time included
					strNewFileName = DateTimeNum("Year") & "-" & DateTimeNum("Month") & "-" & DateTimeNum("Day") & "_" & DateTimeNum("Hour") & DateTimeNum("Minute") & DateTimeNum("Second") & "_" & strNewFileName
				
					
					'Save the file to disk with new file name
					'** Copy virtual is used as save as is often disabled by the web host **
					.Files(1).CopyVirtual strFileUploadPath & "/" & strNewFileName

					'As a new copy of the file is saved we need to get rid of the old copy
					.Files(1).Delete

					'Pass the filename back
					fileUpload = strNewFileName


				'Else if it is not OK delete the uploaded file
				Else
					.Files(1).Delete

				End If

			End With

			'Clean up
			Set objUpload = Nothing




		'******************************************
		'***         Dundas Upload component   ****
		'******************************************

		'Dundas upload component free from http://www.dundas.com - tested with version 2.0
		Case "Dundas"

			'Create upload object
			Set objUpload = Server.CreateObject("Dundas.Upload")

			With objUpload

				'Make sure we are using a virtual directory for script
				.UseVirtualDir = True

				'Make sure the file names are not unique at this time
				.UseUniqueNames = False

				'Save the file first to memory
				.SaveToMemory()

				'Get the file name, the path mehod will be empty as we are saving to memory so use the original file path of the users system to get the name
				strNewFileName = .GetFileName(.Files(0).OriginalPath)

				'Replace spaces with underscores
				strNewFileName = Replace(strNewFileName, " ", "_", 1, -1, 1)
				
				'Filter file name to remove anything that isn't allowed by the filters
				strNewFileName = formatFileName(strNewFileName)

				'Check the file size is not above the max allowed size, this is done using a function not the compoent to stop an exception error
				lngErrorFileSize = fileSize(.Files(0).Size, intMaxFileSize)

				'Loop through all the allowed extensions and see if the file has one
				blnExtensionOK = fileExtension(strNewFileName, saryFileUploadTypes)

				'If the file is OK save it to disk
				If lngErrorFileSize = 0 AND blnExtensionOK = True Then

					'Create a new file name for the file with the date a time included
					strNewFileName = DateTimeNum("Year") & "-" & DateTimeNum("Month") & "-" & DateTimeNum("Day") & "_" & DateTimeNum("Hour") & DateTimeNum("Minute") & DateTimeNum("Second") & "_" & strNewFileName
					
					
					'Save the file to disk
					.Files(0).SaveAs strFileUploadPath & "/" & strNewFileName

					'Pass the filename back
					fileUpload = strNewFileName
				End If
			End With

			'Clean up
			Set objUpload = Nothing




		'******************************************
		'***  SoftArtisans FileUp component    ****
		'******************************************

		'SA FileUp upload component - tested with version 4
		Case "fileUp"

			'Create upload object
			Set objUpload = Server.CreateObject("SoftArtisans.FileUp")

			With objUpload

				'Over write files or an exception will occur if it already exists
				.OverWriteFiles = True

				'Set the upload path
				.Path = Server.MapPath(strFileUploadPath)

				'Get the file name, the path mehod will be empty as we are saving to memory so use the original file path of the users system to get the name
				strNewFileName = Mid(.UserFilename, InstrRev(.UserFilename, "\") + 1)

				'Replace spaces with underscores
				strNewFileName = Replace(strNewFileName, " ", "_", 1, -1, 1)
				
				'Filter file name to remove anything that isn't allowed by the filters
				strNewFileName = formatFileName(strNewFileName)

				'Check the file size is not above the max allowed size, this is done using a function not the compoent to stop an exception error
				lngErrorFileSize = fileSize(.TotalBytes, intMaxFileSize)

				'Loop through all the allowed extensions and see if the file has one
				blnExtensionOK = fileExtension(strNewFileName, saryFileUploadTypes)

				'If the file is OK save it to disk
				If lngErrorFileSize = 0 AND blnExtensionOK = True Then

					'Create a new file name for the file with the date a time included
					strNewFileName = DateTimeNum("Year") & "-" & DateTimeNum("Month") & "-" & DateTimeNum("Day") & "_" & DateTimeNum("Hour") & DateTimeNum("Minute") & DateTimeNum("Second") & "_" & strNewFileName

					'Save the file to disk
					.SaveAs strNewFileName

					'Pass the filename back
					fileUpload = strNewFileName
				End If

			End With

			'Clean up
			Set objUpload = Nothing




		'******************************************
		'***  	AspSmartUpload component       ****
		'******************************************

		'AspSmartUpload upload component free from http://www.aspxsmart.com
		Case "aspSmart"

			'Create upload object
			Set objUpload = Server.CreateObject("aspSmartUpload.SmartUpload")

			With objUpload

				'Make sure we are using a virtual directory for script
				.DenyPhysicalPath = True

				'Save the file first to memory
				.Upload

				'Get the file name, the path mehod will be empty as we are saving to memory so use the original file path of the users system to get the name
				strNewFileName = .Files(1).Filename

				'Replace spaces with underscores
				strNewFileName = Replace(strNewFileName, " ", "_", 1, -1, 1)
				
				'Filter file name to remove anything that isn't allowed by the filters
				strNewFileName = formatFileName(strNewFileName)

				'Check the file size is not above the max allowed size
				lngErrorFileSize = fileSize(.Files(1).Size, intMaxFileSize)

				'Loop through all the allowed extensions and see if the file has one
				blnExtensionOK = fileExtension(strNewFileName, saryFileUploadTypes)

				'If the file is OK save it to disk
				If lngErrorFileSize = 0 AND blnExtensionOK = True Then

					'Create a new file name for the file with the date a time included
					strNewFileName = DateTimeNum("Year") & "-" & DateTimeNum("Month") & "-" & DateTimeNum("Day") & "_" & DateTimeNum("Hour") & DateTimeNum("Minute") & DateTimeNum("Second") & "_" & strNewFileName
					
					
					'Save the file to disk
					.Files(1).SaveAs strFileUploadPath & "/" & strNewFileName

					'Pass the filename back
					fileUpload = strNewFileName
				End If

			End With

			'Clean up
			Set objUpload = Nothing



		'******************************************
		'***     AspSimpleUpload component     ****
		'******************************************

		'ASPSimpleUpload component
		Case "AspSimple"

			'Dimension variables
			Dim file	'Holds the FSO file object


			'Create upload object
			Set objUpload = Server.CreateObject("ASPSimpleUpload.Upload")

			With objUpload

				'Get the file name
				strOriginalFileName = .ExtractFileName(.Form("file"))

				'Save the amended file name
				strNewFileName = "TMP" & hexValue(7) & "_" & strOriginalFileName
				
				'Filter file name to remove anything that isn't allowed by the filters
				strNewFileName = formatFileName(strNewFileName)

				'Save the file to disk first so we can check it
				Call .SaveToWeb ("file", strFileUploadPath & "\" & strNewFileName)

				'Create the file system object
				Set objFSO = Server.CreateObject("Scripting.FileSystemObject")

				'Create a file object with the file details
				Set file = objFSO.GetFile(Server.MapPath(strFileUploadPath) & "\" & strNewFileName)

				'Check the file size is not above the max allowed size, this is done using a function not the compoent to stop an exception error
				lngErrorFileSize = fileSize(file.Size, intMaxFileSize)


				'Place the original file name back in the new filename variable
				strNewFileName = strOriginalFileName

				'Replace spaces with underscores
				strNewFileName = Replace(strNewFileName, " ", "_", 1, -1, 1)
				
				'Filter file name to remove anything that isn't allowed by the filters
				strNewFileName = formatFileName(strNewFileName)


				'Loop through all the allowed extensions and see if the file has one
				blnExtensionOK = fileExtension(strNewFileName, saryFileUploadTypes)


				'If the file is OK save it to disk
				If lngErrorFileSize = 0 AND blnExtensionOK = True Then

					'Create a new file name for the file with the date a time included
					strNewFileName = DateTimeNum("Year") & "-" & DateTimeNum("Month") & "-" & DateTimeNum("Day") & "_" & DateTimeNum("Hour") & DateTimeNum("Minute") & DateTimeNum("Second") & "_" & strNewFileName
					
					'Save the file to disk
					Call .SaveToWeb("file", strFileUploadPath & "/" & strNewFileName)

					'Pass the filename back
					fileUpload = strNewFileName
				End If
				
				'Delete the original file
				file.Delete

			End With

			'Clean up
			Set file = Nothing
			Set objFSO = Nothing
			Set objUpload = Nothing

	End Select

End Function





'******************************************
'***	Check file size function       ****
'******************************************
Function fileSize(ByVal lngFileSize, ByVal intMaxFileSize)

	'If the file size is to large place the present file size in then return the file size
	If CInt(lngFileSize / 1024) > intMaxFileSize Then

		fileSize = CInt(lngFileSize / 1024)

	'Else set the return value to 0
	Else
		fileSize = 0
	End If

End Function





'******************************************
'***	Check file ext. function       ****
'******************************************
Function fileExtension(ByVal strFileName, ByVal saryFileUploadTypes)

	'Dimension varibles
	Dim intExtensionLoopCounter

	'Intilaise return value
	fileExtension = False

	'Loop through all the allowed extensions and see if the file has one
	For intExtensionLoopCounter = 0 To UBound(saryFileUploadTypes)

		If LCase(Right(strFileName, Len(saryFileUploadTypes(intExtensionLoopCounter)))) = LCase(saryFileUploadTypes(intExtensionLoopCounter)) Then fileExtension = True
	Next

End Function





'******************************************
'***	Format file names       ****
'******************************************
'Format file names to strip caharacters that will otherwise be stripped by the filters producing dead links
Private Function formatFileName(ByVal strInputEntry)

	strInputEntry = Replace(strInputEntry, "[", "", 1, -1, 1)
	strInputEntry = Replace(strInputEntry, "]", "", 1, -1, 1)
	strInputEntry = Replace(strInputEntry, "(", "", 1, -1, 1)
	strInputEntry = Replace(strInputEntry, ")", "", 1, -1, 1)
	strInputEntry = Replace(strInputEntry, "{", "", 1, -1, 1)
	strInputEntry = Replace(strInputEntry, "}", "", 1, -1, 1)
	strInputEntry = Replace(strInputEntry, "<", "", 1, -1, 1)
	strInputEntry = Replace(strInputEntry, ">", "", 1, -1, 1)
	strInputEntry = Replace(strInputEntry, "|", "", 1, -1, 1)

	'Return
	formatFileName = strInputEntry
End Function
%>