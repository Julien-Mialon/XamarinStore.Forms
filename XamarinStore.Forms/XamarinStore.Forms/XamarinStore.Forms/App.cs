using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XamarinStore.Forms.Converters;
using XamarinStore.Forms.Helpers;
using XamarinStore.Forms.Views;

namespace XamarinStore.Forms
{
	public class App : Application
	{
		public App()
		{
			Resources = new ResourceDictionary()
			{
				{"PurpleColor", Color.FromHex("FFB455B6") },
				{"BlueColor", Color.FromHex("FF3498DB") },
				{"DarkBlueColor", Color.FromHex("FF2C3E50") },
				{"GreenColor", Color.FromHex("FF77D065") },
				{"GrayColor", Color.FromHex("FF738182") },
				{"LightGrayColor", Color.FromHex("FFB4BCBC") },
			};

			// The root page of your application
			NavigationPage mainPage = new NavigationPage(NavigationHelper.GetPage<HomePage>());
			NavigationPage.SetHasNavigationBar(mainPage, false);

			MainPage = mainPage;
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

	}
}
