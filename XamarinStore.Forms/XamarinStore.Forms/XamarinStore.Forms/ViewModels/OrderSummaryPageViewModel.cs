using System.Collections.Generic;
using XamarinStore.Forms.Models;

namespace XamarinStore.Forms.ViewModels
{
	public class OrderSummaryPageViewModel : BaseViewModel
	{
		private OrderResult _orderResult;

		public OrderResult OrderResult
		{
			get { return _orderResult; }
			set { SetProperty<OrderResult>(ref _orderResult, value); }
		}

		public OrderSummaryPageViewModel()
		{
			IsBasketEnabled = false;
		}

		public override void Initialized(Dictionary<string, object> parameters)
		{
			base.Initialized(parameters);

			OrderResult = GetParameter<OrderResult>("OrderResult");
		}
	}
}
