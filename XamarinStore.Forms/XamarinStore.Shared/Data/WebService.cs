using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamarinStore.Forms.Models;
using XamarinStore.Forms.SSO;
using XamarinStore.Shared.Data;
using User = XamarinStore.Forms.Models.User;

namespace XamarinStore.Forms.Data
{
	public class WebService
	{
		public static readonly WebService Shared = new WebService();

		public User CurrentUser { get; set; }
		public Order CurrentOrder { get; set; }

		XamarinSSOClient client = new XamarinSSOClient("https://auth.xamarin.com");

		public WebService()
		{
			CurrentOrder = new Order();
		}

		public async Task<bool> Login(string username, string password)
		{
			AccountResponse response;
			try
			{
				response = await client.CreateToken(username, password);
				if (response.Success)
				{
					var user = response.User;
					CurrentUser = new User
					{
						LastName = user.LastName,
						FirstName = user.FirstName,
						Email = username,
						Token = response.Token
					};
					return true;
				}
				else
				{
					//Console.WriteLine ("Login failed: {0}", response.Error);
				}
			}
			catch (Exception ex)
			{
				//Console.WriteLine ("Login failed for some reason...: {0}", ex.Message);
			}
			return false;
		}

		List<Product> products;
		public async Task<List<Product>> GetProducts()
		{
			if (products == null)
			{

				try
				{
					string extraParams = "";
					//Want to get an extra Monkey ? Uncomment the next line ;-)
					//extraParams = "?includeMonkeys=true";

					var request = CreateRequest("products" + extraParams);
					string response = await ReadResponseText(request);


					products = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(response);
					return products;
				}
				catch (Exception ex)
				{
					//Console.WriteLine (ex);
					return new List<Product>();
				}

			}
			return products;
		}
		bool hasPreloadedImages;
		public async Task PreloadImages(float imageWidth)
		{
			if (hasPreloadedImages)
				return;
			hasPreloadedImages = true;
			//Lets precach the countries too
#pragma warning disable 4014
			GetCountries();
#pragma warning restore 4014
			await Task.Factory.StartNew(async () =>
			{
				var imagUrls = products.SelectMany(x => x.ImageUrls.Select(y => Product.ImageForSize(y, imageWidth))).ToList();
				foreach (var img in imagUrls)
				{
					await FileCache.Download(img);
				}
			});


		}
		List<Country> countries = new List<Country>();
		public async Task<List<Country>> GetCountries()
		{
			try
			{
				if (countries.Count > 0)
					return countries;
				var request = CreateRequest("Countries");
				string response = await ReadResponseText(request);
				countries = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Country>>(response);
				return countries;
			}
			catch (Exception ex)
			{
				//Console.WriteLine (ex);
				return new List<Country>();
			}

		}

		public async Task<string> GetCountryCode(string country)
		{
			var c = (await GetCountries()).FirstOrDefault(x => x.Name == country) ?? new Country();

			return c.Code;
		}

		public async Task<string> GetCountryFromCode(string code)
		{
			var c = (await GetCountries()).FirstOrDefault(x => x.Code == code) ?? new Country();

			return c.Name;
		}

		//No need to await anything, and no need to spawn a task to return a list.
#pragma warning disable 1998
		public async Task<List<string>> GetStates(string country)
		{
			if (country.ToLower() == "united states")
				return new List<string> {
					"Alabama",
					"Alaska", 
					"Arizona",
					"Arkansas",
					"California",
					"Colorado",
					"Connecticut",
					"Delaware",
					"District of Columbia",
					"Florida",
					"Georgia",
					"Hawaii",
					"Idaho",
					"Illinois",
					"Indiana",
					"Iowa",
					"Kansas",
					"Kentucky",
					"Louisiana",
					"Maine",
					"Maryland",
					"Massachusetts",
					"Michigan",
					"Minnesota",
					"Mississippi",
					"Missouri",
					"Montana",
					"Nebraska",
					"Nevada",
					"New Hampshire",
					"New Jersey",
					"New Mexico",
					"New York",
					"North Carolina",
					"North Dakota",
					"Ohio",
					"Oklahoma",
					"Oregon",
					"Pennsylvania",
					"Rhode Island",
					"South Carolina",
					"South Dakota",
					"Tennessee",
					"Texas",
					"Utah",
					"Vermont",
					"Virginia",
					"Washington",
					"West Virginia",
					"Wisconsin",
					"Wyoming",
				};
			return new List<string>();
		}
#pragma warning restore 1998

		static HttpRequestMessage CreateRequest(string location, string method = "GET")
		{
			HttpRequestMessage request = RequestMaker.SetupRequest("https://xamarin-store-app.xamarin.com/api/" + location, method);
			request.Headers.Add("Accept", "application/json");
			return request;
		}

		static void AddContentToRequest(HttpRequestMessage request, string content)
		{
			request.Content = new StringContent(content, Encoding.UTF8, "application/json");
		}


		public async Task<OrderResult> PlaceOrder(User user, bool verify = false)
		{
			try
			{
				var request = CreateRequest("order" + (verify ? "?verify=1" : ""), "POST");
				AddContentToRequest(request, CurrentOrder.GetJson(user));
				
				string response = await ReadResponseText(request);
				var result = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderResult>(response);
				if (!verify && result.Success)
					CurrentOrder = new Order();
				return result;
			}
			catch (Exception ex)
			{
				return new OrderResult
				{
					Success = false,
					Message = ex.Message,
				};
			}
		}


		protected static Task<string> ReadResponseText(HttpRequestMessage request)
		{
			return RequestMaker.ExecuteRequestAsync(request);
		}
	}
}

