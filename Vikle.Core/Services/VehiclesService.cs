// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
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
        public async Task<Result<MvxObservableCollection<Vehicle>>> GetUserVehicles()
        {
            Result<MvxObservableCollection<Vehicle>> result = new Result<MvxObservableCollection<Vehicle>>();
            string userId = await _secureStorageService.GetAsync(Constants.SS_USER_ID);
            string token = await _secureStorageService.GetAsync(Constants.SS_TOKEN);
            var vehiclesResult = await _clientService.GetUserVehicles(userId, token);
            
            if (vehiclesResult.Error)
            {
                result.Error = true;
                result.Message = vehiclesResult?.HttpStatusCode == HttpStatusCode.Unauthorized
                    ? Strings.UserUnauthorised
                    : Strings.ServerError;
                return result;
            }

            result.Data = new MvxObservableCollection<Vehicle> (vehiclesResult.Result);
            
            return result;
            
            // TODO: Remove this mock after implementing API
            // return new Result<MvxObservableCollection<Vehicle>>
            // {
            //     Data = new MvxObservableCollection<Vehicle> {
            //         new Vehicle {
            //                 IdClient = "1",
            //                 IdDrivers = new List<string> (),
            //                 LastITV = DateTime.Today,
            //                 LastTBDS = DateTime.Today,
            //                 Model = "Vespino",
            //                 PlateNumber = "1234 ABC",
            //                 VehicleType = VehicleType.MotorCycle,
            //                 Year = 2015
            //             },
            //         new Vehicle {
            //             IdClient = "2",
            //             IdDrivers = new List<string> (),
            //             LastITV = DateTime.Today,
            //             LastTBDS = DateTime.Today,
            //             Model = "Audi A3",
            //             PlateNumber = "5678 DEF",
            //             VehicleType = VehicleType.Car,
            //             Year = 2007
            //         }
            //     }
            // };
        }
    }
}