using System.Net;
using System.Threading.Tasks;
using MvvmCross;
using Vikle.Core.Interfaces;
using Vikle.Core.Models;

namespace Vikle.Core.Services
{
    /// <summary>
    /// This class contains the implementation of the date details service.
    /// </summary>
    public class DateDetailsService : IDateDetailsService
    {
        IApiClientService _clientService;
        ISecureStorageService _secureStorageService;
        
        public DateDetailsService()
        {
            _clientService = Mvx.IoCProvider.Resolve<IApiClientService>();
            _secureStorageService = Mvx.IoCProvider.Resolve<ISecureStorageService>();
        }
        
        /// <summary>
        /// Saves the introduced date details in the server API.
        /// </summary>
        /// <param name="date">The date details to stored</param>
        public async Task<Result> SaveDate(Date date)
        {
            Result result = new Result();
            string token = await _secureStorageService.GetAsync(Constants.SS_TOKEN);
            string userId = await _secureStorageService.GetAsync(Constants.SS_USER_ID);
            date.IdClient = userId;
            
            var reparationResult = await _clientService.UpdateDate(date, token);
            
            if (reparationResult.Error)
            {
                switch (reparationResult.HttpStatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        result.Message = Strings.UserUnauthorised;
                        break;
                    case HttpStatusCode.BadRequest:
                        result.Message = Strings.CarAlreadyWithDate;
                        break;
                    default:
                        result.Message = Strings.ServerError;
                        break;
                }

                result.Error = true;
                return result;
            }
            
            return result;
        }
    }
}