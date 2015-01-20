using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinStore.Forms.UIModels;

namespace XamarinStore.Forms.ViewModels
{
	public class BaseViewModel : NotifierBase
	{
		private string _basketCountText;
		private Dictionary<string, object> _parameters; 

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

			OpenBasketCommand = new Command(OpenBasketAction);
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
			//TODO
		}
	}
}
