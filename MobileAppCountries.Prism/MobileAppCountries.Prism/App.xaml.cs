using Prism;
using Prism.Ioc;
using MobileAppCountries.Prism.ViewModels;
using MobileAppCountries.Prism.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using MobileAppCountries.Common.Services;

namespace MobileAppCountries.Prism
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            //await NavigationService.NavigateAsync("NavigationPage/CountriesPage");
            await NavigationService.NavigateAsync(nameof(MainPage));
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<CountriesPage, CountriesPageViewModel>();
            containerRegistry.RegisterForNavigation<SinglePageCountry, SinglePageCountryViewModel>();
            containerRegistry.RegisterForNavigation<DetailCountriesPage, DetailCountriesPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<SinglePageCountryLogedIn, SinglePageCountryLogedInViewModel>();
        }
    }
}
