namespace XamarinStore.Forms.Models
{
	public class ProductColor
	{
		public ProductColor ()
		{
		}

		public string Name { get; set; }

		public string[] ImageUrls { get; set; }

		public override string ToString ()
		{
			return Name;
		}
	}
}

