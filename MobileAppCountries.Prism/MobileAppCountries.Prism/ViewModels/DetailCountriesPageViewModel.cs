using MobileAppCountries.Common.Entities;
using MobileAppCountries.Common.Responses;
using MobileAppCountries.Common.Services;
using MobileAppCountries.Prism.ItemViewModel;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MobileAppCountries.Prism.ViewModels
{
    public class DetailCountriesPageViewModel : ViewModelBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        private bool _isRunning;

        private string _search;
        private List<Country> _myCoutries;
        private List<CommentEntries> _commentEntries;
        private DelegateCommand _searchCommand;
        public User User { get; set; }

        private ObservableCollection<CountryItemViewModel> _countries;

        public DetailCountriesPageViewModel(INavigationService navigationService,
            IApiService apiService, User user) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            User = user;
            Title = "Detailed Countries";
            LoadCountriesAsync();

        }

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ShowCountries));

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            User = (User)parameters["user"];
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            parameters.Add("user", User);
        }

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



        public ObservableCollection<CountryItemViewModel> Countries
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }





        private async void LoadCountriesAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Error", "ConnectionError", "Cancel");
                return;
            }

            IsRunning = true;

            string url = App.Current.Resources["UrlCountriesAPI"].ToString();
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

            _commentEntries = await LoadCommentsAsync();

            ShowCountries();

        }

        private async Task<List<CommentEntries>> LoadCommentsAsync()
        {
            string url = App.Current.Resources["UrlBlogApi"].ToString();
            Response response = await _apiService.GetCommentsAsync<CommentEntries>(
                url,
                "/Blogs",
                "/GetAllBlogEntries");

            if (response.IsSuccess)
            {
                _commentEntries = (List<CommentEntries>)response.Result;

                foreach (var country in _myCoutries)
                {
                    var list = new List<CommentEntries>();
                    foreach (var commentEntry in _commentEntries)
                    {
                        if (commentEntry.Country == country.Name)
                        {
                            list.Add(commentEntry);
                        }
                    }
                    country.Comments = list;
                }


            }

            return _commentEntries;
        }


        private void ShowCountries()
        {
            if (string.IsNullOrEmpty(Search))
            {


                Countries = new ObservableCollection<CountryItemViewModel>(_myCoutries.Select(p =>
                new CountryItemViewModel(_navigationService, _myCoutries, User)
                {
                    Name = p.Name,
                    Capital = p.Capital,
                    Flag = p.Flag,
                    Comments = new ObservableCollection<CommentEntries>(p.Comments).ToList()
                })
                    .ToList());
            }
            else
            {
                Countries = new ObservableCollection<CountryItemViewModel>(_myCoutries.Select(p =>
                new CountryItemViewModel(_navigationService, _myCoutries, User)
                {
                    Name = p.Name,
                    Capital = p.Capital,
                    Flag = p.Flag,
                    Comments = new ObservableCollection<CommentEntries>(p.Comments).ToList()
                })
                    .Where(p => p.Name.ToLower().Contains(Search.ToLower()))
                    .ToList());
            }
        }
    }
}
