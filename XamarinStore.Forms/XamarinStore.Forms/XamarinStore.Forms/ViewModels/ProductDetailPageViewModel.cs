using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinStore.Forms.Data;
using XamarinStore.Forms.Models;
using XLabs.Ioc;
using XLabs.Platform.Device;

namespace XamarinStore.Forms.ViewModels
{
	public class ProductDetailPageViewModel : BaseViewModel
	{
		private Product _currentProduct;
		private ImageSource _productImageSource;
		private ProductSize _currentSize;
		private ProductColor _currentColor;

		public ProductColor CurrentColor
		{
			get { return _currentColor; }
			set { SetProperty<ProductColor>(ref _currentColor, value); }
		}

		public ImageSource ProductImageSource
		{
			get { return _productImageSource; }
			set { SetProperty<ImageSource>(ref _productImageSource, value); }
		}
		public ProductSize CurrentSize
		{
			get { return _currentSize; }
			set { SetProperty<ProductSize>(ref _currentSize, value); }
		}

		public Product CurrentProduct
		{
			get { return _currentProduct; }
			set { SetProperty<Product>(ref _currentProduct, value); }
		}

		public ICommand OrderCommand { get; private set; }

		public ProductDetailPageViewModel()
		{
			OrderCommand = new Command(OrderAction);
		}

		private void OrderAction()
		{
			Product orderedProduct = CurrentProduct.Clone();
			orderedProduct.Color = CurrentColor;
			orderedProduct.Size = CurrentSize;

			WebService.Shared.CurrentOrder.Add(orderedProduct);
			Navigation.PopAsync();
		}

		public override void Initialized(Dictionary<string, object> parameters)
		{
			base.Initialized(parameters);

			CurrentProduct = GetParameter<Product>("Product");
			CurrentSize = CurrentProduct.Sizes.First();
			CurrentColor = CurrentProduct.Colors.First();

			ProductImageSource = CurrentProduct.ImageForSize(Resolver.Resolve<IDevice>().Display.Width);
		}
	}
}