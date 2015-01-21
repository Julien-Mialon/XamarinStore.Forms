using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Acr.XamForms.UserDialogs;
using Xamarin.Forms;
using XamarinStore.Forms.Data;
using XamarinStore.Forms.Helpers;
using XamarinStore.Forms.UIModels;
using XamarinStore.Forms.Views;
using XLabs.Ioc;

namespace XamarinStore.Forms.ViewModels
{
	public class BasketPageViewModel : BaseViewModel
	{
		private List<ProductUIModel> _products;
		private bool _isBasketEmpty;

		public bool IsBasketEmpty
		{
			get { return _isBasketEmpty; }
			set { SetProperty<bool>(ref _isBasketEmpty, value); }
		}
		public List<ProductUIModel> Products
		{
			get { return _products; }
			set { SetProperty<List<ProductUIModel>>(ref _products, value); }
		}

		public ICommand CheckoutCommand { get; private set; }

		public BasketPageViewModel()
		{
			IsBasketEnabled = false;
			
			CheckoutCommand = new Command(CheckoutAction);

			Products = WebService.Shared.CurrentOrder.Products.Select(x =>
			{
				ProductUIModel result = new ProductUIModel(x)
				{
					ProductName = x.Name,
					ProductPrice = x.PriceDescription.ToUpperInvariant(),
				};
				#pragma warning disable 4014
				ImageHelper.SetImageSource(result, (p, source) => p.ProductImage = source, x.ImageForSize(320));
				#pragma warning restore 4014

				return result;
			}).ToList();

			IsBasketEmpty = Products.Count == 0;
		}

		private void CheckoutAction()
		{
			IUserDialogService dialogService = Resolver.Resolve<IUserDialogService>();
			dialogService.Toast("CheckoutAction !");

			// Maybe it should redirect you to the LoginPage hm ?
			// Navigation.PushAsync(NavigationHelper.GetPage<LoginPage>());
		}
	}
}