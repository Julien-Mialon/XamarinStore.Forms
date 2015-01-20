using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XamarinStore.Forms.Helpers;
using XamarinStore.Forms.Views;

namespace XamarinStore.Forms
{
	public class App : Application
	{
		public App()
		{
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
