using Vikle.Core.Models;

namespace Vikle.Core.Interfaces
{
    /// <summary>
    /// This interface contains the definition of the signup service.
    /// </summary>
    public interface ISignupService
    {
        /// <summary>
        /// This method calls the API to register the user in the application.
        /// </summary>
        /// <param name="data">The user signup data</param>
        /// <returns>A result indicating whether this signup action was successful or not.</returns>
        Result SignUp(SignupData data);
    }
}