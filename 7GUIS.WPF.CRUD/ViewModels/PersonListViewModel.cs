using System;
using System.Collections.ObjectModel;
using _7GUIS.WPF.CRUD.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace _7GUIS.WPF.CRUD.ViewModels;

internal partial class PersonListViewModel : ObservableObject
{
	private ObservableCollection<PersonViewModel> personViewModels = new();

	public ObservableCollection<PersonViewModel> FilteredPersonViewModels => filteredPersonViewModels;

	private ObservableCollection<PersonViewModel> filteredPersonViewModels = new();

	[ObservableProperty] private PersonViewModel? selectedPersonViewModel;

	private string filter;
	public string Filter
	{
		get => filter;
		set
		{
			SetProperty(ref filter, value);
			UpdateFilteredPersonViewModel();
		}
	}

	internal PersonListViewModel()
	{
		personViewModels.Add(new PersonViewModel(new Person { FirstName = "Emil", LastName = "Hans" }));
		personViewModels.Add(new PersonViewModel(new Person { FirstName = "Max", LastName = "Mustermann" }));
		personViewModels.Add(new PersonViewModel(new Person { FirstName = "Tisch", LastName = "Roman" }));

		personViewModels.CollectionChanged += (o, e) => { UpdateFilteredPersonViewModel(); };

		UpdateFilteredPersonViewModel();
	}



	private void UpdateFilteredPersonViewModel()
	{
		filteredPersonViewModels.Clear();
		foreach (var personViewModel in personViewModels)
			if (string.IsNullOrWhiteSpace(Filter) || string.IsNullOrWhiteSpace(Filter))
			{
				filteredPersonViewModels.Add(personViewModel);
			}
			else
			{
				if (personViewModel.DisplayName.Contains(Filter, StringComparison.InvariantCultureIgnoreCase))
					filteredPersonViewModels.Add(personViewModel);
			}
		OnPropertyChanged(nameof(FilteredPersonViewModels));
	}

	[RelayCommand]
	private void Create()
	{
		if (selectedPersonViewModel != null)
		{
			var indexOfSelectedPersonViewModel = personViewModels.IndexOf(selectedPersonViewModel);
			if (indexOfSelectedPersonViewModel != -1)
			{
				personViewModels.Insert(indexOfSelectedPersonViewModel + 1, new PersonViewModel(new Person()));
				return;
			}
		}

		personViewModels.Add(new PersonViewModel(new Person()));
	}

	[RelayCommand]
	private void Delete()
	{
		if (selectedPersonViewModel != null) DeletePersonViewModelCommand.Execute(selectedPersonViewModel);
	}


	[RelayCommand]
	private void DeletePersonViewModel(PersonViewModel personViewModel)
	{
		personViewModels.Remove(personViewModel);
	}
}