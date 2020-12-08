using System.Collections.Generic;
using System.Net;
using System.Threading;
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
            _client.AddHandler("application/json", () => NewtonsoftJsonSerializer.Default);
        }

        /// <summary>
        /// Gets the user token from the web API.
        /// </summary>
        /// <param name="email">The user email</param>
        /// <param name="password">The user password</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The login result data</returns>
        public async Task<HttpCallResult<LoginData>> GetUserToken(string email, string password, CancellationToken cancellationToken = default)
        {
            RestRequest request = new RestRequest ("auth", Method.GET);
            request.AddParameter ("email", email, ParameterType.GetOrPost);
            request.AddParameter ("password", password, ParameterType.GetOrPost);
            var response = await _client.ExecuteAsync<LoginData> (request, cancellationToken);
            
            return ToHttpCallResult (response);
        }

        /// <summary>
        /// Gets the user information from the web API.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="token">The user token</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The user information</returns>
        public async Task<HttpCallResult<User>> GetUserInformation(string userId, string token, CancellationToken cancellationToken = default)
        {
            RestRequest request = new RestRequest ("user", Method.GET);
            request.AddHeader ("Authorization", $"Bearer {token}");
            request.AddParameter ("userId", userId, ParameterType.GetOrPost);
            var response = await _client.ExecuteAsync<User> (request, cancellationToken);
            
            return ToHttpCallResult (response);
        }

        /// <summary>
        /// This method sends a recover password request to the API.
        /// </summary>
        /// <param name="email">The email the user wants to recover the password from</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public async Task<HttpCallResult> RecoverPassword(string email, CancellationToken cancellationToken = default)
        {
            RestRequest request = new RestRequest ("auth", Method.POST);
            request.AddJsonBody(email);
            var response = await _client.ExecuteAsync (request, cancellationToken);
            
            return ToHttpCallResult (response);
        }

        /// <summary>
        /// Sets the provided signup data in the API.
        /// </summary>
        /// <param name="data">The signup data to register in the API</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public async Task<HttpCallResult> Signup(SignupData data, CancellationToken cancellationToken = default)
        {
            RestRequest request = new RestRequest ("auth", Method.PUT);
            request.AddJsonBody(data);
            var response = await _client.ExecuteAsync (request, cancellationToken);
            
            return ToHttpCallResult (response);
        }

        /// <summary>
        /// Gets the provided user vehicles information from the web API.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="token">The user token</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The user vehicles information</returns>
        public async Task<HttpCallResult<List<Vehicle>>> GetUserVehicles(string userId, string token, CancellationToken cancellationToken = default)
        {
            RestRequest request = new RestRequest ("user/vehicles", Method.GET);
            request.AddHeader ("Authorization", $"Bearer {token}");
            request.AddParameter ("userId", userId, ParameterType.GetOrPost);
            var response = await _client.ExecuteAsync<List<Vehicle>> (request, cancellationToken);
            
            return ToHttpCallResult (response);
        }

        /// <summary>
        /// Deletes the vehicle related to the calling user.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="plateNumber">The vehicle identifier</param>
        /// <param name="token">The user token</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<HttpCallResult> DeleteVehicle(string userId, string plateNumber, string token, CancellationToken cancellationToken = default)
        {
            RestRequest request = new RestRequest ("user/vehicles", Method.DELETE);
            request.AddHeader ("Authorization", $"Bearer {token}");
            request.AddParameter ("userId", userId, ParameterType.GetOrPost);
            request.AddParameter ("plateNumber", plateNumber, ParameterType.GetOrPost);
            var response = await _client.ExecuteAsync (request, cancellationToken);
            
            return ToHttpCallResult (response);
        }

        /// <summary>
        /// Updates the vehicle data in the API
        /// </summary>
        /// <param name="plateNumber">The vehicle identifier</param>
        /// <param name="vehicle">The vehicle to be updated</param>
        /// <param name="token">The user token</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public async Task<HttpCallResult> UpdateVehicle(string plateNumber, Vehicle vehicle, string token, CancellationToken cancellationToken = default)
        {
            RestRequest request = new RestRequest ($"user/vehicles/{plateNumber}", Method.POST);
            request.AddHeader ("Authorization", $"Bearer {token}");
            request.AddJsonBody(vehicle);
            var response = await _client.ExecuteAsync (request, cancellationToken);
            
            return ToHttpCallResult (response);
        }

        /// <summary>
        /// Gets the current reparation related to he provided vehicle identifier
        /// </summary>
        /// <param name="plateNumber">The vehicle identifier</param>
        /// <param name="token">The user token</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The current vehicle reparation</returns>
        public async Task<HttpCallResult<Reparation>> GetCurrentReparation(string plateNumber, string token, CancellationToken cancellationToken = default)
        {
            RestRequest request = new RestRequest ("vehicle/current", Method.GET);
            request.AddHeader ("Authorization", $"Bearer {token}");
            request.AddParameter ("plateNumber", plateNumber, ParameterType.GetOrPost);
            var response = await _client.ExecuteAsync<Reparation> (request, cancellationToken);
            
            return ToHttpCallResult (response);
        }

        /// <summary>
        /// Gets the provided vehicle reparations information from the web API.
        /// </summary>
        /// <param name="plateNumber">The user identifier</param>
        /// <param name="token">The user token</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The vehicle reparations information</returns>
        public async Task<HttpCallResult<List<Reparation>>> GetVehicleReparations(string plateNumber, string token, CancellationToken cancellationToken = default)
        {
            RestRequest request = new RestRequest ("vehicle/reparations", Method.GET);
            request.AddHeader ("Authorization", $"Bearer {token}");
            request.AddParameter ("plateNumber", plateNumber, ParameterType.GetOrPost);
            var response = await _client.ExecuteAsync<List<Reparation>> (request, cancellationToken);
            
            return ToHttpCallResult (response);
        }

        /// <summary>
        /// Gets the provided user dates information from the web API.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="token">The user token</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The user dates information</returns>
        public async Task<HttpCallResult<List<Date>>> GetUserDates(string userId, string token, CancellationToken cancellationToken = default)
        {
            RestRequest request = new RestRequest ("user/dates", Method.GET);
            request.AddHeader ("Authorization", $"Bearer {token}");
            request.AddParameter ("userId", userId, ParameterType.GetOrPost);
            var response = await _client.ExecuteAsync<List<Date>> (request, cancellationToken);
            
            return ToHttpCallResult (response);
        }

        /// <summary>
        /// Updates the date data in the API
        /// </summary>
        /// <param name="date">The date to be updated</param>
        /// <param name="token">The user token</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public async Task<HttpCallResult> UpdateDate(Date date, string token, CancellationToken cancellationToken = default)
        {
            RestRequest request = new RestRequest ("user/dates", Method.POST);
            request.AddHeader ("Authorization", $"Bearer {token}");
            request.AddJsonBody(date);
            var response = await _client.ExecuteAsync (request, cancellationToken);
            
            return ToHttpCallResult (response);
        }

        /// <summary>
        /// Gets the provided workshop current reparations
        /// </summary>
        /// <param name="workshopId">The workshop identifier</param>
        /// <param name="token">The user token</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<HttpCallResult<List<Reparation>>> GetWorkshopReparations(string workshopId, string token, CancellationToken cancellationToken = default)
        {
            RestRequest request = new RestRequest ("workshop/reparations", Method.GET);
            request.AddHeader ("Authorization", $"Bearer {token}");
            request.AddParameter ("workshopId", workshopId, ParameterType.GetOrPost);
            var response = await _client.ExecuteAsync<List<Reparation>> (request, cancellationToken);
            
            return ToHttpCallResult (response);
        }

        /// <summary>
        /// Updates the reparation data in the API
        /// </summary>
        /// <param name="reparation">The reparation to be updated</param>
        /// <param name="token">The user token</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public async Task<HttpCallResult> UpdateReparation(Reparation reparation, string token, CancellationToken cancellationToken = default)
        {
            RestRequest request = new RestRequest ("workshop/reparations", Method.POST);
            request.AddHeader ("Authorization", $"Bearer {token}");
            request.AddJsonBody(reparation);
            var response = await _client.ExecuteAsync (request, cancellationToken);
            
            return ToHttpCallResult (response);
        }

        /// <summary>
        /// Gets the user information from the provided plate number.
        /// </summary>
        /// <param name="plateNumber">The vehicle identifier</param>
        /// <param name="token">The user token</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The user information</returns>
        public async Task<HttpCallResult<User>> GetVehicleOwner(string plateNumber, string token, CancellationToken cancellationToken = default)
        {
            RestRequest request = new RestRequest ("vehicle/owner", Method.GET);
            request.AddHeader ("Authorization", $"Bearer {token}");
            request.AddParameter ("plateNumber", plateNumber, ParameterType.GetOrPost);
            var response = await _client.ExecuteAsync<User> (request, cancellationToken);
            
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