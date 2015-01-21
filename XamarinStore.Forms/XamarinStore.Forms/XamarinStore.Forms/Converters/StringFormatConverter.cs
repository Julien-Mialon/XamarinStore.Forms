using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinStore.Forms.Converters
{
	public class StringFormatConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return "";
			}

			string format = parameter as string;
			if (format == null)
			{
				return value.ToString();
			}
			return string.Format(format, value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
