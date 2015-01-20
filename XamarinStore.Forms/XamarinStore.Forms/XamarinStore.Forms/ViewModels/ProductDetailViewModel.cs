using System.Collections.Generic;
using XamarinStore.Forms.Models;

namespace XamarinStore.Forms.ViewModels
{
	public class ProductDetailViewModel : BaseViewModel
	{
		private Product _currentProduct;

		public Product CurrentProduct
		{
			get { return _currentProduct; }
			set { SetProperty<Product>(ref _currentProduct, value); }
		}

		private string _name;

		public string Name
		{
			get { return _name; }
			set { SetProperty<string>(ref _name, value); }
		}

		public override void Initialized(Dictionary<string, object> parameters)
		{
			base.Initialized(parameters);

			CurrentProduct = GetParameter<Product>("Product");
			Name = CurrentProduct.Name;
		}
	}
}