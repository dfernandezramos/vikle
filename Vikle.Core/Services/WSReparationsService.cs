using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.ViewModels;
using Vikle.Core.Enums;
using Vikle.Core.Interfaces;
using Vikle.Core.Models;

namespace Vikle.Core.Services
{
    /// <summary>
    /// This class implements the workshop reparations service
    /// </summary>
    public class WSReparationsService : IWSReparationsService
    {
        readonly IApiClientService _clientService;
        readonly ISecureStorageService _secureStorageService;
        
        public WSReparationsService()
        {
            _clientService = Mvx.IoCProvider.Resolve<IApiClientService>();
            _secureStorageService = Mvx.IoCProvider.Resolve<ISecureStorageService>();
        }
        
        /// <summary>
        /// Gets the provided user workshop identifier
        /// </summary>
        /// <returns>The workshop identifier</returns>
        public async Task<Result<string>> GetUserWorkshopId()
        {
            Result<string> result = new Result<string>();
            string userId = await _secureStorageService.GetAsync(Constants.SS_USER_ID);
            string token = await _secureStorageService.GetAsync(Constants.SS_TOKEN);
            var userData = await _clientService.GetUserInformation(userId, token);
            
            if (userData == null || userData.Error)
            {
                result.Error = true;
                result.Message = userData?.HttpStatusCode == HttpStatusCode.Forbidden
                    ? Strings.IncorrectEmailOrPassword
                    : Strings.ServerError;
                return result;
            }
            
            result.Data = userData.Result.IdWorkshop;
            return result;
            
            // TODO: Pending to remove this mock when API implemented
            // return new Result<string> {Data = "1234"};
        }

        /// <summary>
        /// This method gets the current reparations of the provided workshop identifier
        /// </summary>
        /// <param name="workshopId">The workshop identifier</param>
        /// <returns>A list with the current workshop reparations</returns>
        public async Task<Result<MvxObservableCollection<Reparation>>> GetReparations(string workshopId)
        {
            Result<MvxObservableCollection<Reparation>> result = new Result<MvxObservableCollection<Reparation>>();
            string token = await _secureStorageService.GetAsync(Constants.SS_TOKEN);
            var historyResult = await _clientService.GetWorkshopReparations(workshopId, token);
            
            if (historyResult.Error)
            {
                result.Error = true;
                result.Message = historyResult?.HttpStatusCode == HttpStatusCode.Unauthorized
                    ? Strings.UserUnauthorised
                    : Strings.ServerError;
                return result;
            }
            
            result.Data = new MvxObservableCollection<Reparation> (historyResult.Result);
            
            return result;
            
            // TODO: Remove this mock after implementing API
            // return new Result<MvxObservableCollection<Reparation>>
            // {
            //     Data = new MvxObservableCollection<Reparation> {
            //         new Reparation {
            //                 Id = "1",
            //                 PlateNumber = "1234 ABC",
            //                 AirFilter = true,
            //                 TBDS = true,
            //                 OilFilter = true,
            //                 Date = DateTime.Today,
            //                 Type = ReparationType.Maintenance,
            //                 Details = new List<string> {
            //                     "Changed air filters",
            //                     "Checked and changed TBDS",
            //                     "Changed oil filter and oil",
            //                     "Rest of the common things are ok",
            //                     "Some scratches on the left side",
            //                     "Fixed some small things on the engine",
            //                     "Changed front lights"
            //                 },
            //                 Status = ReparationStatus.Repairing
            //         },
            //         new Reparation {
            //             Id = "2",
            //             PlateNumber = "5678 DEF",
            //             Liquids = true,
            //             ITV = true,
            //             Date = DateTime.Today,
            //             Type = ReparationType.Reparation,
            //             Status = ReparationStatus.Pending
            //         }
            //     }
            // };
        }
    }
}