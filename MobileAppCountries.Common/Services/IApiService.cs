using MobileAppCountries.Common.Entities;
using MobileAppCountries.Common.Responses;
using System.Threading.Tasks;

namespace MobileAppCountries.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string UrlBase, string servicePrefix, string controller);

        Task<Response> RegisterAsync(string UrlBase, string servicePrefix, string controller, Register register);

        Task<Response> LoginAsync(string UrlBase, string servicePrefix, string controller, string userName, string password);
    }
}
