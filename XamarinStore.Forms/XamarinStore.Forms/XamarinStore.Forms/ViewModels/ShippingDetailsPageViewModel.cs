using System;
using System.Collections.Generic;
using System.Windows.Input;
using Acr.XamForms.UserDialogs;
using Xamarin.Forms;
using XamarinStore.Forms.Data;
using XamarinStore.Forms.Helpers;
using XamarinStore.Forms.Models;
using XamarinStore.Forms.Views;
using XLabs.Ioc;

namespace XamarinStore.Forms.ViewModels
{
	public class ShippingDetailsPageViewModel : BaseViewModel
	{
		private User _currentUser;
		private string _country;

		public string Country
		{
			get { return _country; }
			set { SetProperty<string>(ref _country, value); }
		}
		public User CurrentUser
		{
			get { return _currentUser; }
			set { SetProperty<User>(ref _currentUser, value); }
		}

		public ICommand PlaceOrderCommand { get; private set; }

		public ShippingDetailsPageViewModel()
		{
			IsBasketEnabled = false;
			PlaceOrderCommand = new Command(PlaceOrderAction);

			CurrentUser = WebService.Shared.CurrentUser;
			Country = string.IsNullOrWhiteSpace(CurrentUser.Country) ? "France" : CurrentUser.Country;
		}

		private async void PlaceOrderAction()
		{
			IUserDialogService dialogService = Resolver.Resolve<IUserDialogService>();
			string countryCode = await WebService.Shared.GetCountryCode(Country);
			CurrentUser.Country = countryCode;

			Tuple<bool, string> isValid = await CurrentUser.IsInformationValid();
			if (!isValid.Item1)
			{
				dialogService.Toast(isValid.Item2, 2);
				return;
			}

			IProgressDialog dialog = dialogService.Loading("Placing order...");
			OrderResult result = await WebService.Shared.PlaceOrder(CurrentUser);
			dialog.Hide();
			if (!result.Success)
			{
				dialogService.Toast("Error: " + result.Message, 5);
			}
			else
			{
				#pragma warning disable 4014
				Navigation.PushAsync(NavigationHelper.GetPage<OrderSummaryPage>(new Dictionary<string, object>()
				{
					{"OrderResult", result}
				}));
				#pragma warning restore 4014
			}
		}
	}
}
