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
    /// This class contains the implementation of the reparations history service
    /// </summary>
    public class HistoryService : IHistoryService
    {
        readonly IApiClientService _clientService;
        readonly ISecureStorageService _secureStorageService;
        
        public HistoryService()
        {
            _clientService = Mvx.IoCProvider.Resolve<IApiClientService>();
            _secureStorageService = Mvx.IoCProvider.Resolve<ISecureStorageService>();
        }

        /// <summary>
        /// Gets the provided vehicle reparations history
        /// </summary>
        /// <param name="plateNumber">The vehicle identifier</param>
        /// <returns>A list with all the vehicle reparations</returns>
        public async Task<Result<MvxObservableCollection<Reparation>>> GetReparations(string plateNumber)
        {
            Result<MvxObservableCollection<Reparation>> result = new Result<MvxObservableCollection<Reparation>>();
            string token = await _secureStorageService.GetAsync(Constants.SS_TOKEN);
            var historyResult = await _clientService.GetVehicleReparations(plateNumber, token);
            
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
            //                 PlateNumber = plateNumber,
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
            //                 }
            //         },
            //         new Reparation {
            //             Id = "2",
            //             PlateNumber = plateNumber,
            //             Liquids = true,
            //             ITV = true,
            //             Date = DateTime.Today,
            //             Type = ReparationType.Reparation
            //         }
            //     }
            // };
        }
    }
}