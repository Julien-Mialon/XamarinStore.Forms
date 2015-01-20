using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Labs;
using Xamarin.Forms.Labs.Services;
using XamarinStore.Forms.Data;
using XamarinStore.Forms.Helpers;
using XamarinStore.Forms.Models;
using XamarinStore.Forms.UIModels;
using XamarinStore.Forms.Views;

namespace XamarinStore.Forms.ViewModels
{
	public class HomePageViewModel : BaseViewModel
	{
		private List<ProductUIModel> _productList;
		private ProductUIModel _selectedProduct;

		public ProductUIModel SelectedProduct
		{
			get { return _selectedProduct; }
			set
			{
				if (SetProperty<ProductUIModel>(ref _selectedProduct, value))
				{
					if (value != null)
					{
						ItemSelected(value);
					}
				}
			}
		}

		public List<ProductUIModel> ProductList
		{
			get { return _productList; }
			set { SetProperty<List<ProductUIModel>>(ref _productList, value); }
		}

		public HomePageViewModel()
		{
			LoadData();
		}

		private void ItemSelected(ProductUIModel product)
		{
			//SelectedProduct = null;

			

			Device.BeginInvokeOnMainThread(() =>
			{
				SelectedProduct = null;

				Task.Run(() =>
				{
					Device.BeginInvokeOnMainThread(() =>
					{
						Navigation.PushAsync(NavigationHelper.GetPage<ProductDetailsPage>(new Dictionary<string, object>()
						{
							{"Product", product.InnerObject}
						}));
					});
				});
			});
		}

		private async void LoadData()
		{
			List<Product> products = await WebService.Shared.GetProducts();

			int screenWidth = Resolver.Resolve<IDevice>().Display.Width;
			#pragma warning disable 4014
			WebService.Shared.PreloadImages(screenWidth);
			#pragma warning restore 4014

			ProductList = products.Select(x =>
			{
				ProductUIModel result = new ProductUIModel(x)
				{
					ProductName = x.Name,
					ProductPrice = x.PriceDescription,
				};
				LoadImageAsync(result, x.ImageForSize(screenWidth));
				return result;
			}).ToList();
		}

		private async void LoadImageAsync(ProductUIModel product, string imageUri)
		{
			await ImageHelper.SetImageSource(product, (p, source) => p.ProductImage = source, imageUri);
		}
	}
}
