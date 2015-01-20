using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinStore.Forms.Models;

namespace XamarinStore.Forms.UIModels
{
	public class ProductUIModel : AbstractUIModel<Product>
	{
		private string _productName;
		private string _productPrice;
		private ImageSource _productImage;

		public string ProductPrice
		{
			get { return _productPrice; }
			set { SetProperty<string>(ref _productPrice, value); }
		}
		public ImageSource ProductImage
		{
			get { return _productImage; }
			set { SetProperty<ImageSource>(ref _productImage, value); }
		}
		public string ProductName
		{
			get { return _productName; }
			set { SetProperty<string>(ref _productName, value); }
		}

		public ProductUIModel(Product innerObject) : base(innerObject)
		{
		}
	}
}
