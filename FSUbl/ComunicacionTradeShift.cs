using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace FSUbl
{
	public class ComunicacionTradeShift
	{
        FSNetwork.OAuth OAuthBase = new FSNetwork.OAuth();

		// The API demands that we declare what client we are using
		string clientName = "Tradeshift.Net Example Client Beta 1.0"; 
		string dataType = "text/xml"; //"application/json"
        // Your OAuth credentials go here. Ask tradeshift support in case you have yet not received them
        string token = "5XS@T9buJHr3EhUTgvKx-9CNw2dFmu";
        string tokenSecret = "@h8nnD4hXK6A66emBHnPaNbPfSCz@ysZbEhGk9RJ";
        string consumerKey = "aldana@transbidenor.com";
        string consumerSecret = "4bdXMPW9WvVL";

        string tenant_id = "f505bedd-ddc9-43de-84d8-64c1602956d5";

		// The root of the tradeshift sandbox API
        //https://api.tradeshift.com/tradeshift/rest
		string baseUrl = "https://api-sandbox.tradeshift.com/tradeshift/rest/external/";
 
		public void Start()
		{
            Guid tradeshiftUserTenantId = new Guid(tenant_id);

			ListConnections(tradeshiftUserTenantId);
			ListDocuments(tradeshiftUserTenantId);

		}

		public void ListConnections(Guid tradeshiftTenantId)
		{
			string url = string.Concat(baseUrl, "network/connections/");

			try {
				HttpWebRequest request = CreateOAuthRequest(new Uri(url), tradeshiftTenantId, "GET");
				StreamReader response = new StreamReader(request.GetResponse().GetResponseStream());
				System.Console.Write(response.ReadToEnd());
				response.Close();
			} catch (WebException e) {
				System.Console.WriteLine(e.Message);
			}

		}

		public void ListDocuments(Guid tradeshiftTenantId)
		{
			string url = string.Concat(baseUrl, "documents/");

			try {
				HttpWebRequest request = CreateOAuthRequest(new Uri(url), tradeshiftTenantId, "GET");
				StreamReader response = new StreamReader(request.GetResponse().GetResponseStream());
				System.Console.Write(response.ReadToEnd());
				response.Close();
			} catch (WebException e) {
				System.Console.WriteLine(e.Message);
			}

		}

		private HttpWebRequest CreateOAuthRequest(Uri url, Guid tradeshiftTenantId, string method)
		{
			string timeStamp = OAuthBase.GenerateTimeStamp();
			string nonce = OAuthBase.GenerateNonce();
			string normalizedUrl = string.Empty;
			string normalizedRequestParameters = string.Empty;

			string sig = OAuthBase.GenerateSignature(url, consumerKey, consumerSecret, token, tokenSecret, method, timeStamp, nonce, FSNetwork.OAuth.SignatureTypes.HMACSHA1, out normalizedUrl,
				           out normalizedRequestParameters);
			string oAuthHeader = string.Format("OAuth oauth_version=1.0, oauth_signature_method=HMAC-SHA1, oauth_nonce={0}, oauth_timestamp={1}, oauth_consumer_key={2}, oauth_signature={3}", nonce, timeStamp, consumerKey, UrlEncodeUpperCase(sig));

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = method;
			request.Accept = dataType;
			request.Headers["User-Agent"] = clientName;
			request.Headers.Add("X-Tradeshift-TenantId", tradeshiftTenantId.ToString());
			request.Headers.Add("Authorization", oAuthHeader);

			return request;
		}

		// Note: This is by no means a complete implementation of the percentage style URL encoder, needed to generate proper
		// OAuth headers, but good enough for demo purposes
		private string UrlEncodeUpperCase(string url)
		{

			// Encodes the string, and replaces all letters in percentage encodings to CAPS
			string newurl = HttpUtility.UrlEncode(url);
			newurl = Regex.Replace(newurl, "(%[0-9a-f][0-9a-f])", c => c.Value.ToUpper());

			// Note: UrlEncode uses old URL encoding standards, and replaces spaces with + instead of proper percentage encoding
			// where you'd replace spaces with %20, which won't be accepted when calculating the OAuth sign, so we change it back
			return newurl.Replace("+", "%20");
		}
	}
}
