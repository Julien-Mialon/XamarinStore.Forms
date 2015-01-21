using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinStore.Forms.Data;
using XamarinStore.Forms.Helpers;
using XamarinStore.Forms.UIModels;
using XamarinStore.Forms.Views;

namespace XamarinStore.Forms.ViewModels
{
	public class BasketPageViewModel : BaseViewModel
	{
		private List<ProductUIModel> _products;

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
		}

		private void CheckoutAction()
		{
			Navigation.PushAsync(NavigationHelper.GetPage<LoginPage>());
		}
	}
}