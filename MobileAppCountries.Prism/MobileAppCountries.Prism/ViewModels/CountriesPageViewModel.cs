using MobileAppCountries.Common.Entities;
using MobileAppCountries.Common.Responses;
using MobileAppCountries.Common.Services;
using MobileAppCountries.Prism.ItemViewModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Net;
using Xamarin.Essentials;


namespace MobileAppCountries.Prism.ViewModels
{
    public class CountriesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        //private ObservableCollection<Product> _products;

        private bool _isRunning;

        private string _search;
        private List<Country> _myCoutries;
        private DelegateCommand _searchCommand;

        private ObservableCollection<CountryItemViewModel> _countries;



        public CountriesPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;

            Title = "Countries";
            LoadProductsAsync();
        }

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ShowCountries));


        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                ShowCountries();
            }
        }



        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }



        /*public ObservableCollection<Product> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }*/


        public ObservableCollection<CountryItemViewModel> Countries
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }



        private async void LoadProductsAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Error", "ConnectionError", "Cancel");
                return;
            }

            IsRunning = true;

            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<Country>(
                url,
                "/rest/v2",
                "/all");

            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "ConnectionError", "Cancel");
                return;
            }

            _myCoutries = (List<Country>)response.Result;
            //foreach (var item in _myCoutries)
            //{
            //    using (WebClient client = new WebClient())
            //    {
            //        var flagImage = client.DownloadString(item.Flag);
            //        item.Flag = flagImage.Replace(@"""", "");
            //    }
            //    SvgDocument svgDocument = SvgDocument.Open(item.Flag);


            //}
            ShowCountries();

        }


        /*private void ShowProducts()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Products = new ObservableCollection<Product>(_myProducts);
            }
            else
            {
                Products = new ObservableCollection<Product>(_myProducts
                    .Where(p => p.Name.ToLower().Contains(Search.ToLower())));
            }

        }*/

        private void ShowCountries()
        {
            if (string.IsNullOrEmpty(Search))
            {


                Countries = new ObservableCollection<CountryItemViewModel>(_myCoutries.Select(p =>
                new CountryItemViewModel(_navigationService)
                {
                    Name = p.Name,
                    Capital = p.Capital,
                    Flag = p.Flag,
                })
                    .ToList());
                
            }
            else
            {
                Countries = new ObservableCollection<CountryItemViewModel>(_myCoutries.Select(p =>
                new CountryItemViewModel(_navigationService)
                {
                    Name = p.Name,
                    Capital = p.Capital,
                    Flag = p.Flag,
                })
                    .Where(p => p.Name.ToLower().Contains(Search.ToLower()))
                    .ToList());
            }
        }
    }
}
