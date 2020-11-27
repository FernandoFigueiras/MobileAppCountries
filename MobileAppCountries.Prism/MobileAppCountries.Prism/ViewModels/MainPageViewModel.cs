using MobileAppCountries.Common.Entities;
using MobileAppCountries.Prism.Views;
using Prism.Commands;
using Prism.Navigation;

namespace MobileAppCountries.Prism.ViewModels
{
    public class MainPageViewModel : ViewModelBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _openListCountries;
        private DelegateCommand _openDetailCountries;
        private DelegateCommand _goToLogin;


        public User User { get; set; }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
            this._navigationService = navigationService;
        }


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            User = (User)parameters["user"];
        }

        public DelegateCommand OpenListCountries => _openListCountries ??
            (_openListCountries = new DelegateCommand(ShowListCountries));

        public DelegateCommand OpenDetailCountries => _openDetailCountries ??
            (_openDetailCountries = new DelegateCommand(ShowDetailCountries));

        public DelegateCommand GoToLogin => _goToLogin ??
            (_goToLogin = new DelegateCommand(OpenLoginPage));


        private async void OpenLoginPage()
        {
            await _navigationService.NavigateAsync(nameof(LoginPage));
        }

        private async void ShowDetailCountries()
        {
            if (User != null)
            {
                var navParams = new NavigationParameters();

                navParams.Add("user", User);

                await _navigationService.NavigateAsync(nameof(DetailCountriesPage), navParams);
            }
            else
            {
                await _navigationService.NavigateAsync(nameof(DetailCountriesPage));
            }

        }

        private async void ShowListCountries()
        {

            if (User != null)
            {
                var navParams = new NavigationParameters();

                navParams.Add("user", User);


                await _navigationService.NavigateAsync(nameof(CountriesPage), navParams);
            }
            else
            {
                await _navigationService.NavigateAsync(nameof(CountriesPage));
            }

        }
    }
}
