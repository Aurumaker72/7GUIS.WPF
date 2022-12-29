using _7GUIS.WPF.CRUD.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace _7GUIS.WPF.CRUD.ViewModels;

internal class PersonViewModel : ObservableObject
{
	private readonly Person _person;

	internal PersonViewModel(Person person)
	{
		_person = person;
	}

	public string FirstName
	{
		get => _person.FirstName;
		set
		{
			_person.FirstName = value;
			OnPropertyChanged();
			OnPropertyChanged(nameof(DisplayName));
		}
	}

	public string LastName
	{
		get => _person.LastName;
		set
		{
			_person.LastName = value;
			OnPropertyChanged();
			OnPropertyChanged(nameof(DisplayName));
		}
	}
	
	public string DisplayName =>
		$"{(string.IsNullOrEmpty(FirstName) || string.IsNullOrWhiteSpace(FirstName) ? "(No first name)" : FirstName)}, {(string.IsNullOrEmpty(LastName) || string.IsNullOrWhiteSpace(LastName) ? "(No last name)" : LastName)}";
}