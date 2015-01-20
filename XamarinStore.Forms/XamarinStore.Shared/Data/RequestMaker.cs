using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XamarinStore.Shared.Data
{
    public static class RequestMaker
    {
	    private const string API_KEY = "0c833t3w37jq58dj249dt675a465k6b0rz090zl3jpoa9jw8vz7y6awpj5ox0qmb";
	    
		private const int TIMEOUT_SECONDS = 30;
		private const string USER_AGENT = "XamarinSSO .NET v1.0";
		
	    private static HttpClient _httpClient;

	    static RequestMaker()
	    {
			_httpClient = new HttpClient(new HttpClientHandler()
			{
				PreAuthenticate = true,
				UseDefaultCredentials = true,
				Credentials = new NetworkCredential(API_KEY, ""),
			});
			_httpClient.DefaultRequestHeaders.Add("User-Agent", USER_AGENT);
			_httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(API_KEY + ":")));
			_httpClient.Timeout = TimeSpan.FromSeconds(TIMEOUT_SECONDS);
	    }

	    public static HttpRequestMessage SetupRequest(string endpoint, string method = "GET")
	    {
		    HttpMethod httpMethod = new HttpMethod(method);
		    HttpRequestMessage request = new HttpRequestMessage(httpMethod, endpoint);

		    return request;
	    }

	    public static async Task<string> ExecuteRequestAsync(HttpRequestMessage request)
	    {
		    HttpResponseMessage response = await _httpClient.SendAsync(request);

		    response.EnsureSuccessStatusCode();
		    return await response.Content.ReadAsStringAsync();
	    }

    }
}
