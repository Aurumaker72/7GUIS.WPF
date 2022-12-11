using System.Windows;
using _7GUIS.WPF.CRUD.ViewModels;

namespace _7GUIS.WPF.CRUD.Views;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();

		DataContext = PersonListViewModel;
	}

	private PersonListViewModel PersonListViewModel { get; } = new();
}