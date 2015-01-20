using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinStore.Forms.Converters
{
	public class StringToUpperConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string val = value as string;
			if (val != null)
			{
				return val.ToUpperInvariant();
			}
			return "";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}