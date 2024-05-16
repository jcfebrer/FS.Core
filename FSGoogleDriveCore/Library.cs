using System.Collections;
using System.Collections.Specialized;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FSLibraryCore;
using FSExceptionCore;

namespace FSGoogleDriveCore
{
	public class FileInfo
	{
		public string name;
		public string id;
		public bool isFolder;
	}
	public class Library
	{
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/drive-dotnet-quickstart.json
        //follow this link
        //https://developers.google.com/drive/v3/web/quickstart/dotnet

        string[] Scopes = { DriveService.Scope.Drive };
        // modify your app scripts

        string ApplicationName = "Drive API .NET Quickstart";
		DriveService driveService;
		bool fileValidation = false;
		
		public Library()
		{
			UserCredential credential;
			
			
			if (!fileValidation) {
				
				// From https://console.developers.google.com
				string clientId = "183164938406-9co52fbopsnh3vj8h0sibcirbkml61fg.apps.googleusercontent.com";
				string clientSecret = "bOnHMSbNR3vrXhU7AFtZKn10";
				
				// here is where we Request the user to give us access, or use the Refresh Token that was previously stored in %AppData%
				credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets {
					ClientId = clientId,
					ClientSecret = clientSecret
				},
					Scopes,
					"user",
					CancellationToken.None,
					new FileDataStore("MyAppsToken")).Result; 
				//Once consent is recieved, your token will be stored locally on the AppData directory, so that next time you wont be prompted for consent. 
			} else {
			

				using (var stream =
					       new FileStream("client_secret.json", FileMode.Open, FileAccess.Read)) {
					string credPath = System.Environment.GetFolderPath(
						                  System.Environment.SpecialFolder.Personal);
					credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");

					credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
						GoogleClientSecrets.FromStream(stream).Secrets,
						Scopes,
						"user",
						CancellationToken.None,
						new FileDataStore(credPath, true)).Result;
					//Console.WriteLine("Credential file saved to: " + credPath);
				}
			}

			// Create Drive API service.
			driveService = new DriveService(new BaseClientService.Initializer() {
				HttpClientInitializer = credential,
				ApplicationName = ApplicationName,
			});

		}
		
		
		public string CreateFolder(string name, string folderId)
		{
			Google.Apis.Drive.v3.Data.File fileMetadata = new Google.Apis.Drive.v3.Data.File() {
				Name = name,
				MimeType = "application/vnd.google-apps.folder",
				Parents = new List<string> { folderId }
			};
			var request = driveService.Files.Create(fileMetadata);
			request.Fields = "id";
			var file = request.Execute();
			
			return file.Id;
		}
		
		
		public void MoveFile(string fileId, string folderId)
		{
			// Retrieve the existing parents to remove
			var getRequest = driveService.Files.Get(fileId);
			getRequest.Fields = "parents";
			var file = getRequest.Execute();
			var previousParents = String.Join(",", file.Parents);
			// Move the file to the new folder
			var updateRequest = driveService.Files.Update(new Google.Apis.Drive.v3.Data.File(), fileId);
			updateRequest.Fields = "id, parents";
			updateRequest.AddParents = folderId;
			updateRequest.RemoveParents = previousParents;
			file = updateRequest.Execute();
		}
		
		public IList<FileInfo> FindRoot()
		{
			return Find("'root' in parents and trashed=false");
		}
		
		public IList<FileInfo> FindFolders()
		{
			return Find("mimeType = 'application/vnd.google-apps.folder' and trashed=false");
		}
		
		public IList<FileInfo> FindFolder(string folderName)
		{
			return Find("name='" + folderName + "' and mimeType = 'application/vnd.google-apps.folder' and trashed=false");
		}
		
		public IList<FileInfo> FindFolder(string folderName, string parentFolderId)
		{
			return Find("'" + parentFolderId + "' in parents and name='" + folderName + "' and mimeType = 'application/vnd.google-apps.folder' and trashed=false");
		}
		
		public IList<FileInfo> FindImages()
		{
			return Find("mimeType='image/jpeg' and trashed=false");
		}
		
		public IList<FileInfo> FindFiles(string folderId)
		{
			return Find("'" + folderId + "' in parents and trashed=false");
		}
		
		public IList<FileInfo> Find(string searchQ)
		{
			IList<FileInfo> filesA = new List<FileInfo>();
			string pageToken = null;
			do {
				var request = driveService.Files.List();
				request.Q = searchQ;
				request.OrderBy = "name";
				request.Spaces = "drive";
				request.Fields = "nextPageToken, files(id, name, mimeType)";
				request.PageToken = pageToken;
				var result = request.Execute();
				if (result.Files != null && result.Files.Count > 0) {
					foreach (var file in result.Files) {
						FileInfo fi = new FileInfo();
						fi.name = file.Name;
						fi.id = file.Id;
						fi.isFolder = (file.MimeType == "application/vnd.google-apps.folder");
						filesA.Add(fi);
					}
				}
				pageToken = result.NextPageToken;
			} while (pageToken != null);
			
			return filesA;
		}


		public void Download(string fileId, string saveTo)
		{
			FilesResource.GetRequest request = driveService.Files.Get(fileId);
			//si el fichero a descargar fuese un documento de google, hay que utlizar la opción de exportar.
			//En este ejemplo se exporta a PDF.
			//FilesResource.ExportRequest request = driveService.Files.Export(fileId, "application/pdf");
			MemoryStream stream = new System.IO.MemoryStream();
			// Add a handler which will be notified on progress changes.
			// It will notify on each chunk download and when the
			// download is completed or failed.
			request.MediaDownloader.ProgressChanged +=
        		(IDownloadProgress progress) => {
				switch (progress.Status) {
					case DownloadStatus.Downloading:
						{
							//Console.WriteLine(progress.BytesDownloaded);
							break;
						}
					case DownloadStatus.Completed:
						{
							//Console.WriteLine("Download complete.");
							SaveStream(stream, saveTo);
							break;
						}
					case DownloadStatus.Failed:
						{
							//Console.WriteLine("Download failed.");
							throw new ExceptionUtil(progress.Exception.ToString());
						}
				}
			};
			request.Download(stream);
		}
		
		
		private void SaveStream(System.IO.MemoryStream stream, string saveTo)
		{
			using (System.IO.FileStream file = new System.IO.FileStream(saveTo, System.IO.FileMode.Create, System.IO.FileAccess.Write)) {
				stream.WriteTo(file);
			}
		}
		
		
		public string GetName(string fileId)
		{
			Google.Apis.Drive.v3.Data.File file;
			try {
				// Building the initial request.
				FilesResource.GetRequest request = driveService.Files.Get(fileId);

				request.Fields = "name";
				
				// Requesting data.
				file = request.Execute();
				
				return file.Name;
				
			} catch (ExceptionUtil ex) {
				throw new ExceptionUtil("Request Files.Get failed.", ex);
			}
		}
		
		public IList<string> GetParent(string fileId)
		{
			Google.Apis.Drive.v3.Data.File file;
			try {
				// Building the initial request.
				FilesResource.GetRequest request = driveService.Files.Get(fileId);
				
				request.Fields = "parents";

				// Requesting data.
				file = request.Execute();
				
				return file.Parents;
				
			} catch (ExceptionUtil ex) {
				throw new ExceptionUtil("Request Files.Get failed.", ex);
			}
		}
		
		public void Delete(string fileId)
		{
			try {
				// Building the initial request.
				FilesResource.DeleteRequest request = driveService.Files.Delete(fileId);

				// Requesting data.
				request.Execute();
				
			} catch (ExceptionUtil ex) {
				throw new ExceptionUtil("Request Files.Delete failed.", ex);
			}
		}
		
		public void EmptyTrash()
		{
			try {
				// Make the request.
				driveService.Files.EmptyTrash().Execute();
			} catch (ExceptionUtil ex) {
				throw new ExceptionUtil("Request Files.EmptyTrash failed.", ex);
			}
		}
		
		
		public string UploadFile(string name, Stream fileStream, string folderId)
		{
			string fileId = "";
			if (folderId == "")
				folderId = "root";
			try {
				Google.Apis.Drive.v3.Data.File fileMetadata = new Google.Apis.Drive.v3.Data.File() {
					Name = name,
					MimeType = FSLibraryCore.MimeType.GetMimeType(name),
					Parents = new List<string> { folderId }
				};
				FilesResource.CreateMediaUpload request;

				request = driveService.Files.Create(
					fileMetadata, fileStream, FSLibraryCore.MimeType.GetMimeType(name));
				request.Fields = "id";
				
				request.Upload();
			
				var file = request.ResponseBody;
				fileId = file.Id;
			} catch (ExceptionUtil ex) {
				throw new ExceptionUtil("Failed to upload file.", ex);
			}
			return fileId;
		}
		
		
		public string UploadFile(string name, string filePath, string folderId)
		{
			string fileId = "";
			if (folderId == "")
				folderId = "root";
			try {
				Google.Apis.Drive.v3.Data.File fileMetadata = new Google.Apis.Drive.v3.Data.File() {
					Name = name,
					MimeType = FSLibraryCore.MimeType.GetMimeType(filePath),
					Parents = new List<string> { folderId }
				};

				FilesResource.CreateMediaUpload request;
				
				using (FileStream stream = new System.IO.FileStream(filePath,
					                           System.IO.FileMode.Open)) {
					request = driveService.Files.Create(
						fileMetadata, stream, FSLibraryCore.MimeType.GetMimeType(filePath));
					request.Fields = "id";
				
					request.Upload();
				}
			
				var file = request.ResponseBody;
				fileId = file.Id;
			} catch (ExceptionUtil ex) {
				throw new ExceptionUtil("Failed to upload file.", ex);
			}
			return fileId;
		}
		
		public string UploadFileAsync(string name, string filePath, string folderId)
		{
			string fileId = "";
			try {
				Google.Apis.Drive.v3.Data.File fileMetadata = new Google.Apis.Drive.v3.Data.File() {
					Name = name,
					MimeType = FSLibraryCore.MimeType.GetMimeType(filePath),
					Parents = new List<string> { folderId }
				};
				FilesResource.CreateMediaUpload request;
				using (FileStream stream = new System.IO.FileStream(filePath,
					                           System.IO.FileMode.Open)) {
					request = driveService.Files.Create(
						fileMetadata, stream, FSLibraryCore.MimeType.GetMimeType(filePath));
					request.Fields = "id";
				
					request.ProgressChanged += Upload_ProgressChanged;
					request.ResponseReceived += Upload_ResponseReceived;
				
					var task = request.UploadAsync();
					task.ContinueWith(t => {
						// Remeber to clean the stream.
						stream.Dispose();
					});
				}
				
				var file = request.ResponseBody;
				fileId = file.Id;
			} catch (ExceptionUtil ex) {
				throw new ExceptionUtil("Failed to upload file.", ex);
			}
			return fileId;
		}
		

		static void Upload_ProgressChanged(Google.Apis.Upload.IUploadProgress progress)
		{
			Console.WriteLine(progress.Status + " " + progress.BytesSent);
		}
		
		static void Upload_ResponseReceived(Google.Apis.Drive.v3.Data.File file)
		{
			Console.WriteLine(file.Name + " was uploaded successfully");
		}
		
	}
}