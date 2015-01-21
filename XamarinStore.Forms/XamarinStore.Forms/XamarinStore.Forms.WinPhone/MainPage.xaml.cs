using Acr.XamForms.UserDialogs;
using Acr.XamForms.UserDialogs.WindowsPhone;
using Microsoft.Phone.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using XLabs.Platform;
using XLabs.Platform.Device;

namespace XamarinStore.Forms.WinPhone
{
	public partial class MainPage : FormsApplicationPage
	{
		public MainPage()
		{
			InitializeComponent();
			SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

			Xamarin.Forms.Forms.Init();
			LoadApplication(new Forms.App());
		}
	}
}
