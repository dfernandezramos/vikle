using System.Collections.Generic;
using System.Threading.Tasks;
using Vikle.Core.Models;

namespace Vikle.Core.Interfaces
{
    /// <summary>
    /// This interface contains the definition of the API client service.
    /// </summary>
    public interface IApiClientService
    {
        /// <summary>
        /// Gets the user token from the web API.
        /// </summary>
        /// <param name="email">The user email</param>
        /// <param name="password">The user password</param>
        /// <returns>The login result data</returns>
        Task<HttpCallResult<LoginData>> GetUserToken (string email, string password);
        
        /// <summary>
        /// Gets the user information from the web API.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="token">The user token</param>
        /// <returns>The user information</returns>
        Task<HttpCallResult<User>> GetUserInformation (string userId, string token);

        /// <summary>
        /// This method sends a recover password request to the API.
        /// </summary>
        /// <param name="email">The email the user wants to recover the password from</param>
        Task<HttpCallResult> RecoverPassword(string email);

        /// <summary>
        /// Sets the provided signup data in the API.
        /// </summary>
        /// <param name="data">The signup data to register in the API</param>
        Task<HttpCallResult> Signup(SignupData data);
        
        /// <summary>
        /// Gets the provided user vehicles information from the web API.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="token">The user token</param>
        /// <returns>The user vehicles information</returns>
        Task<HttpCallResult<List<Vehicle>>> GetUserVehicles (string userId, string token);
        
        /// <summary>
        /// Deletes the vehicle related to the calling user.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="plateNumber">The vehicle identifier</param>
        /// <param name="token">The user token</param>
        Task<HttpCallResult> DeleteVehicle(string userId, string plateNumber, string token);
        
        /// <summary>
        /// Updates the vehicle data in the API
        /// </summary>
        /// <param name="plateNumber">The vehicle identifier</param>
        /// <param name="vehicle">The vehicle to be updated</param>
        /// <param name="token">The user token</param>
        Task<HttpCallResult> UpdateVehicle(string plateNumber, Vehicle vehicle, string token);
        
        /// <summary>
        /// Gets the current reparation related to he provided vehicle identifier
        /// </summary>
        /// <param name="plateNumber">The vehicle identifier</param>
        /// <param name="token">The user token</param>
        /// <returns>The current vehicle reparation</returns>
        Task<HttpCallResult<Reparation>> GetCurrentReparation(string plateNumber, string token);
        
        /// <summary>
        /// Gets the provided vehicle reparations information from the web API.
        /// </summary>
        /// <param name="plateNumber">The user identifier</param>
        /// <param name="token">The user token</param>
        /// <returns>The vehicle reparations information</returns>
        Task<HttpCallResult<List<Reparation>>> GetVehicleReparations (string plateNumber, string token);
    }
}