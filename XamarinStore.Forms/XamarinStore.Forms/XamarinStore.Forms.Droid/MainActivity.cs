﻿using Acr.XamForms.UserDialogs;
using Acr.XamForms.UserDialogs.Droid;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms.Labs;
using Xamarin.Forms.Labs.Droid;
using Xamarin.Forms.Labs.Services;
using XamarinStore.Forms.Data;

namespace XamarinStore.Forms
{
	[Activity(Label = "XamarinStore.Forms", Icon = "@drawable/icon", MainLauncher = true, 
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, Theme = "@style/NoActionBar")]
	public class MainActivity : XFormsApplicationDroid
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			global::Xamarin.Forms.Forms.Init(this, bundle);

			IDependencyContainer container = new SimpleContainer();
			container.Register<IDevice>(t => AndroidDevice.CurrentDevice);
			container.Register<IUserDialogService>(t => new UserDialogService());


			Resolver.SetResolver(container.GetResolver());


			LoadApplication(new App());
		}
	}
}

