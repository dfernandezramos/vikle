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
            RestRequest request = new RestRequest ("api/auth/", Method.GET);
            request.AddParameter ("email", email, ParameterType.GetOrPost);
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

        /// <summary>
        /// This method sends a recover password request to the API.
        /// </summary>
        /// <param name="email">The email the user wants to recover the password from</param>
        public async Task<HttpCallResult> RecoverPassword(string email)
        {
            RestRequest request = new RestRequest ("api/user/", Method.POST);
            request.AddParameter ("email", email, ParameterType.GetOrPost);
            var response = await _client.ExecuteAsync (request);
            
            return ToHttpCallResult (response);
        }

        /// <summary>
        /// Sets the provided signup data in the API.
        /// </summary>
        /// <param name="data">The signup data to register in the API</param>
        public async Task<HttpCallResult> Signup(SignupData data)
        {
            RestRequest request = new RestRequest ("api/auth/", Method.PUT);
            request.AddParameter ("email", data.Email, ParameterType.GetOrPost);
            request.AddParameter ("password", data.Password, ParameterType.GetOrPost);
            request.AddParameter ("name", data.Name, ParameterType.GetOrPost);
            request.AddParameter ("surname", data.Surname, ParameterType.GetOrPost);
            request.AddParameter ("phone", data.Phone, ParameterType.GetOrPost);
            var response = await _client.ExecuteAsync (request);
            
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
        
        HttpCallResult ToHttpCallResult (IRestResponse response)
        {
            return new HttpCallResult
            {
                HttpStatusCode = response.StatusCode,
                Error = response.StatusCode != HttpStatusCode.OK ||
                        response.ResponseStatus != ResponseStatus.Completed
            };
        }
    }
}