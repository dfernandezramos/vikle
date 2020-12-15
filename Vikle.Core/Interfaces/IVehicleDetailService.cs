// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System.Threading.Tasks;
using Vikle.Core.Enums;
using Vikle.Core.Models;

namespace Vikle.Core.Interfaces
{
    /// <summary>
    /// This interface contains the definition of the vehicle detail service
    /// </summary>
    public interface IVehicleDetailService
    {
        /// <summary>
        /// Gets the reparation status of the provided vehicle plate number
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <returns></returns>
        Task<Result<ReparationStatus>> GetReparationStatus(string plateNumber);
        
        /// <summary>
        /// Updates the provided car with the provided vehicle data model
        /// </summary>
        /// <param name="vehicle">The vehicle data model</param>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <returns></returns>
        Task<Result> UpdateVehicle(string plateNumber, Vehicle vehicle);
        
        /// <summary>
        /// Deletes the car related to the calling user with the provided vehicle plate number
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <returns></returns>
        Task<Result> DeleteVehicle(string plateNumber);
    }
}