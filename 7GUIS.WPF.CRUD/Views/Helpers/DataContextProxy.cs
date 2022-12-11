using System.Windows;

namespace _7GUIS.WPF.CRUD.Views.Helpers;

public class DataContextProxy : Freezable
{
	public static readonly DependencyProperty DataProperty = DependencyProperty.Register("DataSource", typeof(object),
		typeof(DataContextProxy), new UIPropertyMetadata(null));

	public object DataSource
	{
		get => GetValue(DataProperty);
		set => SetValue(DataProperty, value);
	}

	#region Overrides of Freezable

	protected override Freezable CreateInstanceCore()
	{
		return new DataContextProxy();
	}

	#endregion
}