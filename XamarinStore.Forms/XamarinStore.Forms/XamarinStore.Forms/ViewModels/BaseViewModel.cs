using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinStore.Forms.Data;
using XamarinStore.Forms.Helpers;
using XamarinStore.Forms.UIModels;
using XamarinStore.Forms.Views;

namespace XamarinStore.Forms.ViewModels
{
	public class BaseViewModel : NotifierBase
	{
		private string _basketCountText;
		private Dictionary<string, object> _parameters;
		private bool _isBasketEnabled;

		public bool IsBasketEnabled
		{
			get { return _isBasketEnabled; }
			set { SetProperty<bool>(ref _isBasketEnabled, value); }
		}

		public string BasketCountText
		{
			get { return _basketCountText; }
			set { SetProperty<string>(ref _basketCountText, value); }
		}

		public ICommand OpenBasketCommand { get; private set; }

		public INavigation Navigation { get; set; }

		public BaseViewModel()
		{
			BasketCountText = "No items...";
			IsBasketEnabled = true;

			WebService.Shared.CurrentOrder.ProductsChanged += OnOrderChanged;
			OpenBasketCommand = new Command(OpenBasketAction);

			OnOrderChanged(this, EventArgs.Empty);
		}

		private void OnOrderChanged(object sender, EventArgs e)
		{
			int count = WebService.Shared.CurrentOrder.Products.Count;
			string str = "";
			if (count == 0)
			{
				str = "No items...";
			}
			else if (count == 1)
			{
				str = "1 item :)";
			}
			else
			{
				str = string.Format("{0} items ;-)", count);
			}
			BasketCountText = str;
		}

		public virtual void Initialized(Dictionary<string, object> parameters)
		{
			_parameters = parameters;
		}

		protected T GetParameter<T>(string key)
		{
			if (!_parameters.ContainsKey(key))
			{
				throw new ArgumentException("key: " + key);
			}

			return (T)_parameters[key];
		}

		private void OpenBasketAction()
		{
			Navigation.PushAsync(NavigationHelper.GetPage<BasketPage>());
		}
	}
}
