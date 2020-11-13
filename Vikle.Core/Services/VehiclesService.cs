using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.ViewModels;
using Vikle.Core.Enums;
using Vikle.Core.Interfaces;
using Vikle.Core.Models;

namespace Vikle.Core.Services
{
    /// <summary>
    /// This class contains the implementation of the client vehicles service.
    /// </summary>
    public class VehiclesService : IVehiclesService
    {
        IApiClientService _clientService;
        ISecureStorageService _secureStorageService;
        
        public VehiclesService()
        {
            _clientService = Mvx.IoCProvider.Resolve<IApiClientService>();
            _secureStorageService = Mvx.IoCProvider.Resolve<ISecureStorageService>();
        }

        /// <summary>
        /// Gets the current user vehicles
        /// </summary>
        /// <returns>The user vehicles information</returns>
        public async Task<MvxObservableCollection<Vehicle>> GetUserVehicles()
        {
            string userId = await _secureStorageService.GetAsync(Constants.SS_USER_ID);
            string token = await _secureStorageService.GetAsync(Constants.SS_TOKEN);
            var result = await _clientService.GetUserVehicles(userId, token);
            
            if (result.Error)
            {
                // TODO: log error here
                return null;
            }
            
            return new MvxObservableCollection<Vehicle>(result.Result);
            
            // TODO: Remove this mock after implementing API
            // return new MvxObservableCollection<Vehicle>
            // {
            //     new Vehicle {
            //             IdClient = "1",
            //             IdDrivers = new List<string> (),
            //             LastITV = DateTime.Today,
            //             LastTBDS = DateTime.Today,
            //             Model = "",
            //             PlateNumber = "1234 ABC",
            //             VehicleType = VehicleType.MotorCycle,
            //             Year = 2015
            //         },
            //     new Vehicle {
            //         IdClient = "2",
            //         IdDrivers = new List<string> (),
            //         LastITV = DateTime.Today,
            //         LastTBDS = DateTime.Today,
            //         Model = "Audi A3",
            //         PlateNumber = "5678 DEF",
            //         VehicleType = VehicleType.Car,
            //         Year = 2007
            //     }
            // };
        }
    }
}