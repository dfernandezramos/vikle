using Vikle.Core.Interfaces;
using Vikle.Core.Models;

namespace Vikle.Core.Services
{
    /// <summary>
    /// This class contains the implementation of the recover password service.
    /// </summary>
    public class RecoverPasswordService : IRecoverPasswordService
    {
        /// <summary>
        /// This method calls the API with the provided email address in order to recover the password.
        /// </summary>
        /// <param name="email">The email the user wants to recover the password from</param>
        public Result RecoverPassword(string email)
        {
            Result result = new Result();

            if (!Utils.IsValidEmail(email))
            {
                result.Error = true;
                result.Message = "Enter a valid email";
                return result;
            }
            
            // TODO: Pending implement API call to regenerate the password

            return result;
        }
    }
}