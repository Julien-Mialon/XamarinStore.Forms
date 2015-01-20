namespace XamarinStore.Forms.UIModels
{
	public abstract class AbstractUIModel<T> : NotifierBase
	{
		public T InnerObject { get; private set; }

		protected AbstractUIModel(T innerObject)
		{
			InnerObject = innerObject;
		}
	}
}