using MobileAppCountries.Common.Entities;
using MobileAppCountries.Common.Responses;
using MobileAppCountries.Common.Services;
using MobileAppCountries.Prism.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Xamarin.Essentials;

namespace MobileAppCountries.Prism.ViewModels
{
    public class RegisterPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private string _userName;
        private string _email;
        private string _password;
        private string _confirm;
        private string _status;
        private DelegateCommand _registerNewUser;

        public RegisterPageViewModel(INavigationService navigationService,
            IApiService apiService)
        {
            this._navigationService = navigationService;
            this._apiService = apiService;
        }


        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }


        public string Confirm
        {
            get => _confirm;
            set => SetProperty(ref _confirm, value);
        }


        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }


        public DelegateCommand RegisterNewUser => _registerNewUser ??
            (_registerNewUser = new DelegateCommand(Register));

        private async void Register()
        {

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Error", "ConnectionError", "Cancel");
                return;
            }
            string url = App.Current.Resources["UrlBlogApi"].ToString();

            Register register = new Register
            {
                UserName = UserName,
                Email = Email,
                Password = Password,
                Confirm = Password,
                Status = Status
            };

            Response response = await _apiService.RegisterAsync(
                url,
                "/Users",
                "/Register",
                register);


            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "ConnectionError", "Cancel");
                return;
            }


            await App.Current.MainPage.DisplayAlert("Success", "Successfully registered, procede to login", "OK");
            await _navigationService.NavigateAsync(nameof(LoginPage));
        }
            
    }

}
