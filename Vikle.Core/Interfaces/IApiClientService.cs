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
    }
}