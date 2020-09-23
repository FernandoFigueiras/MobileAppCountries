using MobileAppCountries.Common.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppCountries.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string UrlBase, string servicePrefix, string controller);
    }
}
