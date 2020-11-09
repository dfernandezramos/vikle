using System.Net;
using System.Threading.Tasks;
using MvvmCross;
using Vikle.Core.Interfaces;
using Vikle.Core.Models;

namespace Vikle.Core.Services
{
    /// <summary>
    /// This class contains the implementation of the recover password service.
    /// </summary>
    public class RecoverPasswordService : IRecoverPasswordService
    {
        IApiClientService _clientService;
        
        public RecoverPasswordService()
        {
            _clientService = Mvx.IoCProvider.Resolve<IApiClientService>();
        }
        
        /// <summary>
        /// This method calls the API with the provided email address in order to recover the password.
        /// </summary>
        /// <param name="email">The email the user wants to recover the password from</param>
        public async Task<Result> RecoverPassword(string email)
        {
            Result result = new Result();

            if (!Utils.IsValidEmail(email))
            {
                result.Error = true;
                result.Message = Strings.EnterValidEmail;
                return result;
            }

            var recoverResult = await _clientService.RecoverPassword(email);

            if (recoverResult.Error)
            {
                result.Error = true;
                result.Message = Strings.ServerError;
                return result;
            }

            return result;
        }
    }
}