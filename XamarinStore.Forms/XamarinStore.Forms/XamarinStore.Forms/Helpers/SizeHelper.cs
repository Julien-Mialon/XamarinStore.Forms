﻿using XLabs.Ioc;
using XLabs.Platform.Device;

namespace XamarinStore.Forms.Helpers
{
	public static class SizeHelper
	{
		public static int Width
		{
			get
			{
				IDevice device = Resolver.Resolve<IDevice>();
				IDisplay display = device.Display;

				if (display.Xdpi < 1)
				{
					//windows phone
					return display.Width/100;
				}
				return display.Width;
			}
		}
	}
}
