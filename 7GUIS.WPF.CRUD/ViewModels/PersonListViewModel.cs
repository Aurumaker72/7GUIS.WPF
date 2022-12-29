using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using _7GUIS.WPF.CRUD.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace _7GUIS.WPF.CRUD.ViewModels;

internal partial class PersonListViewModel : ObservableObject
{
	internal PersonListViewModel()
	{
		_personViewModels.Add(new PersonViewModel(new Person { FirstName = "Emil", LastName = "Hans" }));
		_personViewModels.Add(new PersonViewModel(new Person { FirstName = "Max", LastName = "Mustermann" }));
		_personViewModels.Add(new PersonViewModel(new Person { FirstName = "Tisch", LastName = "Roman" }));

		_personViewModels.CollectionChanged += (o, e) => { UpdateFilteredPersonViewModel(); };
		UpdateFilteredPersonViewModel();
	}

	private string _filter;
	private readonly ObservableCollection<PersonViewModel> _personViewModels = new();
	private PersonViewModel? _selectedPersonViewModel;

	public ObservableCollection<PersonViewModel> FilteredPersonViewModels { get; private set; } = new();

	public PersonViewModel? SelectedPersonViewModel
	{
		get => _selectedPersonViewModel;
		set
		{
			SetProperty(ref _selectedPersonViewModel, value);
			OnPropertyChanged(nameof(HasSelection));
			DeleteCommand.NotifyCanExecuteChanged();
		}
	}

	public bool HasSelection => SelectedPersonViewModel != null;

	public string Filter
	{
		get => _filter;
		set
		{
			SetProperty(ref _filter, value);
			UpdateFilteredPersonViewModel();
		}
	}


	private void UpdateFilteredPersonViewModel()
	{
		FilteredPersonViewModels.Clear();
		foreach (var personViewModel in _personViewModels)
			if (string.IsNullOrWhiteSpace(Filter) || string.IsNullOrWhiteSpace(Filter))
			{
				FilteredPersonViewModels.Add(personViewModel);
			}
			else
			{
				if (personViewModel.DisplayName.Contains(Filter, StringComparison.InvariantCultureIgnoreCase))
					FilteredPersonViewModels.Add(personViewModel);
			}
	}

	[RelayCommand]
	private void Create()
	{
		if (_selectedPersonViewModel != null)
		{
			var indexOfSelectedPersonViewModel = _personViewModels.IndexOf(_selectedPersonViewModel);
			if (indexOfSelectedPersonViewModel != -1)
			{
				_personViewModels.Insert(indexOfSelectedPersonViewModel + 1, new PersonViewModel(new Person()));
				return;
			}
		}

		_personViewModels.Add(new PersonViewModel(new Person()));
		UpdateFilteredPersonViewModel();
	}

	[RelayCommand(CanExecute = nameof(HasSelection))]
	private void Delete() => DeletePersonViewModelCommand.Execute(_selectedPersonViewModel);


	[RelayCommand]
	private void DeletePersonViewModel(PersonViewModel personViewModel)
	{
		_personViewModels.Remove(personViewModel);
		UpdateFilteredPersonViewModel();
	}
}