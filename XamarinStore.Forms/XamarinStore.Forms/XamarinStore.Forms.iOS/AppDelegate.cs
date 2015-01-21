using System;
using System.Collections.Generic;
using System.Linq;
using Acr.XamForms.UserDialogs;
using Acr.XamForms.UserDialogs.iOS;
using Foundation;
using UIKit;
using XLabs.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;
using System.Reflection;

namespace XamarinStore.Forms.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : XFormsApplicationDelegate
	{
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			SetupIoC();

			global::Xamarin.Forms.Forms.Init();
			LoadApplication(new App());

			var assembly = typeof(AppDelegate).GetType ().Assembly;
			foreach (var res in assembly.GetManifestResourceNames())
				System.Diagnostics.Debug.WriteLine ("Resource : " + res);

			return base.FinishedLaunching(app, options);
		}

		private void SetupIoC()
		{
			SimpleContainer resolverContainer = new SimpleContainer();

			var app = new XFormsAppiOS();
			app.Init(this);

			resolverContainer.Register<IDevice>(AppleDevice.CurrentDevice);
			resolverContainer.Register<IUserDialogService>(new UserDialogService());

			Resolver.SetResolver(resolverContainer.GetResolver());
		}
	}
}
