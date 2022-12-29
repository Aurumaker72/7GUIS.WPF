using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace _7GUIS.WPF.CRUD.Views.Converters;

internal class InvertedBooleanToVisibilityConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is bool _bool) return _bool ? Visibility.Collapsed : Visibility.Visible;

		throw new ArgumentException($"{nameof(value)} was not of type {nameof(Boolean)}");
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}