using Vikle.Core.Interfaces;
using Vikle.Core.Models;

namespace Vikle.Core.Services
{
    /// <summary>
    /// This class contains the implementation of the login service.
    /// </summary>
    public class LoginService : ILoginService
    {
        /// <summary>
        /// This method uses the provided credentials to log into the application.
        /// </summary>
        /// <param name="email">The user email</param>
        /// <param name="password">The user password</param>
        /// <returns>A LoginResult indicating whether this login action was successful or not.</returns>
        public LoginResult Login(string email, string password)
        {
            var result = new LoginResult();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                result.Error = true;
                result.Message = "Email and password are required";
                return result;
            }

            if (!Utils.IsValidEmail(email))
            {
                result.Error = true;
                result.Message = "Enter a valid email";
                return result;
            }
            
            // TODO: Pending API call for login
            
            return result;
        }
    }
}