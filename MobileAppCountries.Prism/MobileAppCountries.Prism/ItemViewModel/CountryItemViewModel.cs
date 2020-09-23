using MobileAppCountries.Common.Entities;
using MobileAppCountries.Prism.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppCountries.Prism.ItemViewModel
{
    public class CountryItemViewModel : Country
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectProductCommand;

        public CountryItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectCountryCommand => _selectProductCommand ??
           (_selectProductCommand = new DelegateCommand(SelectCountryAsync));



        private async void SelectCountryAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
               { "country", this }
            };


            await _navigationService.NavigateAsync(nameof(CountriesPage), parameters);
        }

    }
}
