using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XamarinStore.Forms.ViewModels;

namespace XamarinStore.Forms.Helpers
{
    public static class NavigationHelper
    {
		public static Page GetPage<TPage>(Dictionary<string, object> parameters = null) 
			where TPage : Page, new()
	    {
		    Page view = new TPage();
		    BaseViewModel vm = view.BindingContext as BaseViewModel;
		    if (vm == null)
		    {
			    throw new Exception("No ViewModel attached !");
		    }
		    vm.Navigation = view.Navigation;

			vm.Initialized(parameters ?? new Dictionary<string, object>());

			return view;
	    }
    }
}
