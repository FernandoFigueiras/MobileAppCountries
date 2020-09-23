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
        private string _password;
        private DelegateCommand _loginCommand;
        private DelegateCommand _registerCommand;
        private DelegateCommand _forgotPasswordCommand;


        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Login";
            _isEnabled = true;
        }


        public DelegateCommand LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand(LoginAsync));

        public DelegateCommand RegisterCommand =>
            _registerCommand ?? (_registerCommand = new DelegateCommand(RegisterAsync));

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

        public string Email { get; set; }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }


        private async void LoginAsync()
        {
            if (string.IsNullOrEmpty(Email))
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
        }


        private void ForgotPasswordAsync()
        {
            //TODO: Pending
        }

        private void RegisterAsync()
        {
            //TODO: Pending
        }
    }
}
