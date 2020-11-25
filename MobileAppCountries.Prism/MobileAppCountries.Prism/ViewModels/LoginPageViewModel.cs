using MobileAppCountries.Common.Entities;
using MobileAppCountries.Common.Responses;
using MobileAppCountries.Common.Services;
using MobileAppCountries.Prism.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MobileAppCountries.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private bool _isRunning;
        private bool _isEnabled;
        private string _userName;
        private string _password;
        private DelegateCommand _loginCommand;
        private DelegateCommand _registerCommand;
        private DelegateCommand _forgotPasswordCommand;
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiservice;

        public Response response { get; set; }

        public LoginPageViewModel(INavigationService navigationService,
            IApiService apiservice) : base(navigationService)
        {
            Title = "Login";
            _isEnabled = true;
            this._navigationService = navigationService;
            this._apiservice = apiservice;
        }


        public DelegateCommand LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand(LoginAsync));

        public DelegateCommand RegisterCommand =>
            _registerCommand ?? (_registerCommand = new DelegateCommand(GoToRegister));

        public DelegateCommand ForgotPasswordCommand =>
            _forgotPasswordCommand ?? (_forgotPasswordCommand = new DelegateCommand(ForgotPasswordAsync));


        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }


        private async void LoginAsync()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Invalid Email",
                    "Cancel");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Invalid Password",
                    "Cancel");
                return;
            }

            string url = App.Current.Resources["UrlBlogApi"].ToString();

            Response response = await _apiservice.LoginAsync(
              url,
              "/Users",
              "/Login",
              UserName,
              Password);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Invalid user name or password",
                    "cancel");
            }

            var jsonString = response.Result.ToString();


            User user = JsonConvert.DeserializeObject<User>(jsonString);

            if (user !=null)
            {
                user.IsLogedin = true;
            }

            var navParams = new NavigationParameters();
            navParams.Add("user", user);

            await _navigationService.NavigateAsync(nameof(MainPage), navParams);

        }


        private void ForgotPasswordAsync()
        {
            //TODO: Pending
        }

        private async void GoToRegister()
        {
            await _navigationService.NavigateAsync(nameof(RegisterPage));
        }
    }
}
