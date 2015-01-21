using Xamarin.Forms;

namespace XamarinStore.Forms.Views
{
	public partial class BasketPage : ContentPage
	{
		public BasketPage()
		{
			InitializeComponent();
		}

		private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
			{
				return;
			}

			ListView list = sender as ListView;
			if (list == null)
			{
				return;
			}

			list.SelectedItem = null;
		}
	}
}
