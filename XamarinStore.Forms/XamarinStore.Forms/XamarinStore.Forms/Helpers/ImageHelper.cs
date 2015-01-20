using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinStore.Forms.Data;

namespace XamarinStore.Forms.Helpers
{
	public static class ImageHelper
	{
		private static readonly Dictionary<string, ImageSource> _imageCache = new Dictionary<string, ImageSource>();

		public static async Task SetImageSource<T>(T context, Action<T, ImageSource> setter, string url)
		{
			ImageSource source = await FromUrl(url);
			setter(context, source);
		}

		public static async Task<ImageSource> FromUrl(string url)
		{
			ImageSource source;
			if (_imageCache.TryGetValue(url, out source))
			{
				return source;
			}
			var path = await FileCache.Download(url);
			if (string.IsNullOrEmpty(path))
			{
				return null;
			}
			source = ImageSource.FromFile(path);

			_imageCache[url] = source;
			return source;
		}
	}
}
