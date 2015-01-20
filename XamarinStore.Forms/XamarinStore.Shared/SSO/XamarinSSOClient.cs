using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XamarinStore.Shared.Data;

namespace XamarinStore.Forms.SSO {
    public class XamarinSSOClient {
        static Encoding encoding = Encoding.UTF8;
        string auth_api_url;

        public XamarinSSOClient () : this ("https://auth.xamarin.com")
        {
        }

        public XamarinSSOClient (string base_url)
        {
            auth_api_url = base_url + "/api/v1/auth";
        }

        protected virtual async Task<string> DoRequest (string endpoint, string method = "GET", string body = null)
        {
	        HttpRequestMessage request = RequestMaker.SetupRequest(endpoint, method);
	        if (body != null)
	        {
		        request.Content = new StringContent(body, encoding);
	        }
	        return await RequestMaker.ExecuteRequestAsync(request);
        }

        public async Task<AccountResponse> CreateToken (string email, string password)
        {
            if (String.IsNullOrWhiteSpace (email))
                throw new ArgumentNullException ("email");
            if (String.IsNullOrWhiteSpace (password))
                throw new ArgumentNullException ("password");

            var str = String.Format ("email={0}&password={1}", UrlEncode (email), UrlEncode (password));
            string json = await DoRequest (auth_api_url, "POST", str);
            return JsonConvert.DeserializeObject<AccountResponse> (json);
        }

        static string UrlEncode (string src)
        {
            if (src == null)
                return null;
            return WebUtility.UrlEncode (src);
        }
    }
}

