using System.Net;
using System.Threading.Tasks;
using MvvmCross;
using Vikle.Core.Interfaces;
using Vikle.Core.Models;

namespace Vikle.Core.Services
{
    /// <summary>
    /// This class contains the implementation of the login service.
    /// </summary>
    public class LoginService : ILoginService
    {
        private IApiClientService _clientService;
        
        public LoginService()
        {
            _clientService = Mvx.IoCProvider.Resolve<IApiClientService>();
        }
        
        /// <summary>
        /// This method uses the provided credentials to log into the application.
        /// </summary>
        /// <param name="email">The user email</param>
        /// <param name="password">The user password</param>
        /// <returns>A LoginResult indicating whether this login action was successful or not.</returns>
        public async Task<LoginResult> Login(string email, string password)
        {
            var result = PerformChecks(email, password);

            if (result.Error)
            {
                return result;
            }
            
            var loginData = await _clientService.GetUserToken(email, password);

            if (loginData == null || loginData.Error)
            {
                result.Error = true;
                result.Message = loginData?.HttpStatusCode == HttpStatusCode.Forbidden
                    ? Strings.IncorrectEmailOrPassword
                    : Strings.ServerError;
                return result;
            }

            var userData = await _clientService.GetUserInformation(loginData.Result.UserId, loginData.Result.Token);

            if (userData == null || userData.Error)
            {
                result.Error = true;
                result.Message = userData?.HttpStatusCode == HttpStatusCode.Forbidden
                    ? Strings.IncorrectEmailOrPassword
                    : Strings.ServerError;
                return result;
            }

            StoreUserData(userData.Result);
            result.Worker = userData.Result.IsWorker;

            return result;
        }

        LoginResult PerformChecks(string email, string password)
        {
            var result = new LoginResult();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                result.Error = true;
                result.Message = Strings.EmailPasswordRequired;
                return result;
            }

            if (!Utils.IsValidEmail(email))
            {
                result.Error = true;
                result.Message = Strings.EnterValidEmail;
                return result;
            }

            return result;
        }

        void StoreUserData(User user)
        {
            // TODO: Pending save globally user data
        }
    }
}