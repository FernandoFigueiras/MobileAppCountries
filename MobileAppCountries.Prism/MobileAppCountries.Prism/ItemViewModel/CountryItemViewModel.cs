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
        private readonly List<Country> _countries;
        private DelegateCommand _selectCoutryCommand;

        public CountryItemViewModel(INavigationService navigationService, List<Country> countries)
        {
            _navigationService = navigationService;
            this._countries = countries;
        }

        public DelegateCommand SelectCountryCommand => _selectCoutryCommand ??
           (_selectCoutryCommand = new DelegateCommand(SelectCountryAsync));



        private async void SelectCountryAsync()
        {
            Country clickedCountry = new Country();

            foreach (var item in _countries)
            {
                if (this.Name == item.Name)
                {
                    clickedCountry = item;
                }
            }


            NavigationParameters parameters = new NavigationParameters
            {
                //{ "country", this }
                { "country", clickedCountry }
            };


            await _navigationService.NavigateAsync(nameof(SinglePageCountry), parameters);
        }

    }
}
