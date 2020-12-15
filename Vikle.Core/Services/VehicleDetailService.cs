// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System.Net;
using System.Threading.Tasks;
using MvvmCross;
using Vikle.Core.Enums;
using Vikle.Core.Interfaces;
using Vikle.Core.Models;

namespace Vikle.Core.Services
{
    /// <summary>
    /// This class implements the Vehicle details service
    /// </summary>
    public class VehicleDetailService : IVehicleDetailService
    {
        readonly IApiClientService _clientService;
        readonly ISecureStorageService _secureStorageService;
        
        public VehicleDetailService()
        {
            _clientService = Mvx.IoCProvider.Resolve<IApiClientService>();
            _secureStorageService = Mvx.IoCProvider.Resolve<ISecureStorageService>();
        }
        
        /// <summary>
        /// Gets the reparation status of the provided vehicle plate number
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        public async Task<Result<ReparationStatus>> GetReparationStatus(string plateNumber)
        {
            Result<ReparationStatus> result = new Result<ReparationStatus>();
            string token = await _secureStorageService.GetAsync(Constants.SS_TOKEN);
            var reparationResult = await _clientService.GetCurrentReparation(plateNumber, token);
            
            if (reparationResult.Error)
            {
                if (reparationResult.HttpStatusCode != HttpStatusCode.NoContent)
                {
                    result.Error = true;
                    result.Message = reparationResult?.HttpStatusCode == HttpStatusCode.Unauthorized
                        ? Strings.UserUnauthorised
                        : Strings.ServerError;
                }

                return result;
            }
            
            result.Data = reparationResult.Result.Status;
            
            return result;
            
            // TODO: Remove this mock when API implemented
            // result.Data = ReparationStatus.Repairing;
            // return result;
        }

        /// <summary>
        /// Updates the provided car with the provided vehicle data model
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <param name="vehicle">The vehicle data model</param>
        public async Task<Result> UpdateVehicle(string plateNumber, Vehicle vehicle)
        {
            Result result = new Result();
            string token = await _secureStorageService.GetAsync(Constants.SS_TOKEN);
            var reparationResult = await _clientService.UpdateVehicle(plateNumber, vehicle, token);
            
            if (reparationResult.Error)
            {
                result.Error = true;
                result.Message = reparationResult?.HttpStatusCode == HttpStatusCode.Unauthorized
                    ? Strings.UserUnauthorised
                    : Strings.ServerError;
                return result;
            }
            
            return result;
        }

        /// <summary>
        /// Deletes the car related to the calling user with the provided vehicle plate number
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        public async Task<Result> DeleteVehicle(string plateNumber)
        {
            Result result = new Result();
            string token = await _secureStorageService.GetAsync(Constants.SS_TOKEN);
            string userId = await _secureStorageService.GetAsync(Constants.SS_USER_ID);
            var reparationResult = await _clientService.DeleteVehicle(userId, plateNumber, token);
            
            if (reparationResult.Error)
            {
                result.Error = true;
                result.Message = reparationResult?.HttpStatusCode == HttpStatusCode.Unauthorized
                    ? Strings.UserUnauthorised
                    : Strings.ServerError;
                return result;
            }
            
            return result;
        }
    }
}