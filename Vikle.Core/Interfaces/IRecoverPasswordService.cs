using Vikle.Core.Models;

namespace Vikle.Core.Interfaces
{
    /// <summary>
    /// THis interface contains the definition of the recover password service.
    /// </summary>
    public interface IRecoverPasswordService
    {
        /// <summary>
        /// This method calls the API with the provided email address in order to recover the password.
        /// </summary>
        /// <param name="email">The email the user wants to recover the password from</param>
        /// <returns>A result indicating whether this recover action was successful or not.</returns>
        Result RecoverPassword(string email);
    }
}