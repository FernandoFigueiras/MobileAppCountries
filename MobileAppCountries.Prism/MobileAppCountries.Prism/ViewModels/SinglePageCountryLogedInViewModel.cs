using Android.Service.QuickSettings;
using MobileAppCountries.Common.Entities;
using MobileAppCountries.Common.Responses;
using MobileAppCountries.Common.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

namespace MobileAppCountries.Prism.ViewModels
{
    public class SinglePageCountryLogedInViewModel : BindableBase, INavigationAware
    {
        public User User { get; set; }
        private Country _country;
        private string _title;
        private string _blogText;
        private DelegateCommand _postComment;
        private readonly IApiService _apiService;

        public SinglePageCountryLogedInViewModel(IApiService apiService)
        {
            this._apiService = apiService;
        }

        public DelegateCommand PostComment => _postComment ??
          (_postComment = new DelegateCommand(PostBlogEntry));

        public Country Country
        {
            get => _country;

            set
            {
                SetProperty(ref _country, value);
            }
        }

        public string Title
        {
            get => _title;

            set
            {
                SetProperty(ref _title, value);
            }
        }


        public string BlogText
        {
            get => _blogText;

            set
            {
                SetProperty(ref _blogText, value);
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            parameters.Add("user", User);
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            User = (User)parameters["user"];
            Country = (Country)parameters["country"];
        }

        public void OnNavigatingTo(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }


        public async void PostBlogEntry()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Error", "ConnectionError", "Cancel");
                return;
            }
            string url = App.Current.Resources["UrlBlogApi"].ToString();

            CommentEntries commentEntry = new CommentEntries
            {
                Title = Title,
                BlogText = BlogText,
                Country = Country.Name,
                Date = DateTime.Now,
                UserId = User.Id,
            };

            string userToken = User.Token;


            Response response = await _apiService.PostCommentEntry(
              url,
              "/Blogs",
              "/InsertBlogEntry",
              commentEntry,
              userToken);


            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Cannot post", "Cancel");
                return;
            }


            await App.Current.MainPage.DisplayAlert("Success", "Successfully posted your comment", "OK");
            return;
        }
    }
}
