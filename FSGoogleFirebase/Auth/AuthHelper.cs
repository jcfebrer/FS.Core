using Google.Apis.Auth.OAuth2;
using System;
using System.IO;

namespace FSGoogleFirebase.Auth
{
    class AuthHelper
    {
        public static string ACCESS_TOKEN { get; set; }

        private static string jsonKeyFilePath;

        private static string[] scopes;

        //private static async Task<string> GetAccessTokenFromJSONKeyAsync(string jsonKeyFilePath, params string[] scopes)
        //{
        //    using (var stream = new FileStream(jsonKeyFilePath, FileMode.Open, FileAccess.Read))
        //    {
        //        return await GoogleCredential.FromStream(stream) // Loads key file
        //          .CreateScoped(scopes) // Gathers scopes requested
        //          .UnderlyingCredential // Gets the credentials
        //          .GetAccessTokenForRequestAsync(); // Gets the Access Token
        //    }
        //}

        private static string GetAccessTokenFromJSONKeyAsync(string jsonKeyFilePath, params string[] scopes)
        {
            using (var stream = new FileStream(jsonKeyFilePath, FileMode.Open, FileAccess.Read))
            {
                return GoogleCredential.FromStream(stream) // Loads key file
                    .CreateScoped(scopes) // Gathers scopes requested
                    .UnderlyingCredential // Gets the credentials
                    .GetAccessTokenForRequestAsync().Result; // Gets the Access Token
            }
        }

        public static void GenenateAccessToken(string jsonKeyFilePath, params string[] scopes)
        {
            AuthHelper.jsonKeyFilePath = jsonKeyFilePath;

            if (scopes.Length == 0)
                AuthHelper.scopes = new string[] { "https://www.googleapis.com/auth/firebase", "https://www.googleapis.com/auth/userinfo.email" };
            else
                AuthHelper.scopes = scopes;

            ACCESS_TOKEN = GetAccessTokenFromJSONKeyAsync(jsonKeyFilePath, AuthHelper.scopes);
        }

        public static void RefreshToken()
        {
            if (string.IsNullOrEmpty(ACCESS_TOKEN))
                throw new InvalidOperationException("Unauthorised! Generate a token first.");
            else
                ACCESS_TOKEN = GetAccessTokenFromJSONKeyAsync(jsonKeyFilePath, scopes);
        }
    }
}