using MobileAppCountries.Common.Entities;
using MobileAppCountries.Common.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppCountries.Common.Services
{
    public class ApiService : IApiService
    {

        public async Task<Response> GetListAsync<T>(string UrlBase, string servicePrefix, string controller)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(UrlBase),
                };


                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage response = await client.GetAsync(url);
                string result = await response.Content.ReadAsStringAsync();


                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                List<T> list = JsonConvert.DeserializeObject<List<T>>(result);

                return new Response
                {
                    IsSuccess = true,
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }


        public async Task<Response> RegisterAsync(string UrlBase, string servicePrefix, string controller, Register register)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(UrlBase),
                };

                var jsonObject = new
                {
                    userName = register.UserName,
                    email = register.Email,
                    password = register.Password,
                    confirm = register.Confirm,
                    status = register.Status
                };
                


                var json = JsonConvert.SerializeObject(jsonObject);
                var contentString = new StringContent(json, Encoding.Default, "application/json");

  

                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage response = await client.PostAsync(url, contentString);
                string result = await response.Content.ReadAsStringAsync();


                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }


                return new Response
                {
                    IsSuccess = true,
                    Result = result,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }


        public async Task<Response> LoginAsync(string UrlBase, string servicePrefix, string controller, string userName, string password)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(UrlBase),
                };

                var user = new
                {
                    userName = userName,
                    password = password,
                };


                var jsonObject = JsonConvert.SerializeObject(user);
                var contentString = new StringContent(jsonObject, Encoding.Default, "application/json");

                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage response = await client.PostAsync(url, contentString);
                string result = await response.Content.ReadAsStringAsync();


                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = result,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }


        public async Task<Response> GetCommentsAsync<T>(string urlBase, string servicePrefix, string controller)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                };

                

                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage response = await client.GetAsync(url);
                string result = await response.Content.ReadAsStringAsync();


                List<T> list = JsonConvert.DeserializeObject<List<T>>(result);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }


        public async Task<Response> PostCommentEntry(string urlBase, string servicePrefix, string controller, CommentEntries commentEntries, string userToken)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                };

                var jsonObject = new
                {
                    title = commentEntries.Title,
                    blogtext = commentEntries.BlogText,
                    userId = commentEntries.UserId,
                    country = commentEntries.Country
                };



                var json = JsonConvert.SerializeObject(jsonObject);
                var contentString = new StringContent(json, Encoding.Default, "application/json");

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", userToken);

                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage response = await client.PostAsync(url, contentString);
                string result = await response.Content.ReadAsStringAsync();


                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }


                return new Response
                {
                    IsSuccess = true,
                    Result = result,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
