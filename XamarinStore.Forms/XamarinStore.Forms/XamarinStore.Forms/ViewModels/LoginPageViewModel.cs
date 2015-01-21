using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.XamForms.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Labs.Services;
using XamarinStore.Forms.Data;
using XamarinStore.Forms.Models;

namespace XamarinStore.Forms.ViewModels
{
	public class LoginPageViewModel : BaseViewModel
	{
		//TODO : add your xamarin account email
		public const string XAMARIN_ACCOUNT_EMAIL = "mialon.julien@gmail.com";

		private bool _isInstructionsEnabled;

		private string _password;

		public string Password
		{
			get { return _password; }
			set { SetProperty<string>(ref _password, value); }
		}

		public bool IsInstructionsEnabled
		{
			get { return _isInstructionsEnabled; }
			set { SetProperty<bool>(ref _isInstructionsEnabled, value); }
		}

		public string Login
		{
			get { return XAMARIN_ACCOUNT_EMAIL; }
		}

		public string SuggestionText
		{
			get { return "\"...\""; }
		}

		public ICommand LoginCommand { get; private set; }

		public LoginPageViewModel()
		{
			IsBasketEnabled = false;

			IsInstructionsEnabled = string.IsNullOrWhiteSpace(XAMARIN_ACCOUNT_EMAIL);
			LoginCommand = new Command(LoginAction);
		}

		private async void LoginAction()
		{
			IUserDialogService dialogService = Resolver.Resolve<IUserDialogService>();
			IProgressDialog dialog = dialogService.Loading("Logging in...");

			bool loginSuccess = await WebService.Shared.Login(XAMARIN_ACCOUNT_EMAIL, Password);
			if (loginSuccess)
			{
				OrderResult canContinue = await WebService.Shared.PlaceOrder(WebService.Shared.CurrentUser, true);
				dialog.Hide();
				if (canContinue.Success)
				{
					dialogService.Toast("Connected ! Order OK");
				}
				else
				{
					dialogService.Toast("Sorry, only one shirt per person. Edit your cart and try again.", 3);
				}
			}
			else
			{
				dialog.Hide();
				dialogService.Toast("Please verify your Xamarin account credentials and try again", 3);
			}
		}
	}
}
