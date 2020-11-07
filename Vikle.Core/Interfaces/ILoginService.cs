using Vikle.Core.Models;

namespace Vikle.Core.Interfaces
{
    /// <summary>
    /// This interface contains the definition of the login service.
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// This method uses the provided credentials to log into the application.
        /// </summary>
        /// <param name="email">The user email</param>
        /// <param name="password">The user password</param>
        /// <returns>A LoginResult indicating whether this login action was successful or not.</returns>
        LoginResult Login(string email, string password);
    }
}