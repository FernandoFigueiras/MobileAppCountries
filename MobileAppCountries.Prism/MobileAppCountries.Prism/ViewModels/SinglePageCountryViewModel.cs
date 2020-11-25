using MobileAppCountries.Common.Entities;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MobileAppCountries.Prism.ViewModels
{
    public class SinglePageCountryViewModel : BindableBase, INavigatedAware
    {

        public SinglePageCountryViewModel()
        {
            
        }

        public Country Country { get; set; }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        
        {
            Country.Name = parameters.GetValue<Country>("country").Name.ToString();
        }
    }
}
