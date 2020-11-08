using System.Net;
using System.Threading.Tasks;
using MvvmCross;
using RestSharp;
using Vikle.Core.Interfaces;
using Vikle.Core.Models;

namespace Vikle.Core.Services
{
    /// <summary>
    /// This class contains the implementation of the API client service.
    /// </summary>
    public class ApiClientService : IApiClientService
    {
        readonly IRestClient _client;

        public ApiClientService ()
        {
            _client = Mvx.IoCProvider.Resolve<IRestClient>();
        }
        
        /// <summary>
        /// Gets the user token from the web API.
        /// </summary>
        /// <param name="email">The user email</param>
        /// <param name="password">The user password</param>
        /// <returns>The login result data</returns>
        public async Task<HttpCallResult<LoginData>> GetUserToken(string email, string password)
        {
            RestRequest request = new RestRequest ("api/auth/", Method.POST);
            request.AddParameter ("username", email, ParameterType.GetOrPost);
            request.AddParameter ("password", password, ParameterType.GetOrPost);
            var response = await _client.ExecuteAsync<LoginData> (request);
            
            return ToHttpCallResult (response);
        }

        /// <summary>
        /// Gets the user information from the web API.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="token">The user token</param>
        /// <returns>The user information</returns>
        public async Task<HttpCallResult<User>> GetUserInformation(string userId, string token)
        {
            RestRequest request = new RestRequest ("api/user/", Method.GET);
            request.AddHeader ("Authorization", $"Token {token}");
            request.AddParameter ("userId", userId, ParameterType.GetOrPost);
            var response = await _client.ExecuteAsync<User> (request);
            
            return ToHttpCallResult (response);
        }
        
        HttpCallResult<TResponse> ToHttpCallResult<TResponse> (IRestResponse<TResponse> response)
        {
            return new HttpCallResult<TResponse>
            {
                Result = response.Data,
                HttpStatusCode = response.StatusCode,
                Error = response.StatusCode != HttpStatusCode.OK ||
                        response.ResponseStatus != ResponseStatus.Completed
            };
        }
    }
}