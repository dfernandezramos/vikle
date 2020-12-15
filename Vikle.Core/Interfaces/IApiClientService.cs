// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vikle.Core.Models;

namespace Vikle.Core.Interfaces
{
    /// <summary>
    /// This interface contains the definition of the API client service.
    /// </summary>
    public interface IApiClientService
    {
        /// <summary>
        /// Gets the user token from the web API.
        /// </summary>
        /// <param name="email">The user email</param>
        /// <param name="password">The user password</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The login result data</returns>
        Task<HttpCallResult<LoginData>> GetUserToken (string email, string password, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the user information from the web API.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="token">The user token</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The user information</returns>
        Task<HttpCallResult<User>> GetUserInformation (string userId, string token, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method sends a recover password request to the API.
        /// </summary>
        /// <param name="email">The email the user wants to recover the password from</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task<HttpCallResult> RecoverPassword(string email, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets the provided signup data in the API.
        /// </summary>
        /// <param name="data">The signup data to register in the API</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task<HttpCallResult> Signup(SignupData data, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the provided user vehicles information from the web API.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="token">The user token</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The user vehicles information</returns>
        Task<HttpCallResult<List<Vehicle>>> GetUserVehicles (string userId, string token, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes the vehicle related to the calling user.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="plateNumber">The vehicle identifier</param>
        /// <param name="token">The user token</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task<HttpCallResult> DeleteVehicle(string userId, string plateNumber, string token, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the vehicle data in the API
        /// </summary>
        /// <param name="plateNumber">The vehicle identifier</param>
        /// <param name="vehicle">The vehicle to be updated</param>
        /// <param name="token">The user token</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task<HttpCallResult> UpdateVehicle(string plateNumber, Vehicle vehicle, string token, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the current reparation related to he provided vehicle identifier
        /// </summary>
        /// <param name="plateNumber">The vehicle identifier</param>
        /// <param name="token">The user token</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The current vehicle reparation</returns>
        Task<HttpCallResult<Reparation>> GetCurrentReparation(string plateNumber, string token, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the provided vehicle reparations information from the web API.
        /// </summary>
        /// <param name="plateNumber">The user identifier</param>
        /// <param name="token">The user token</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The vehicle reparations information</returns>
        Task<HttpCallResult<List<Reparation>>> GetVehicleReparations (string plateNumber, string token, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the provided user dates information from the web API.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="token">The user token</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The user dates information</returns>
        Task<HttpCallResult<List<Date>>> GetUserDates (string userId, string token, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the date data in the API
        /// </summary>
        /// <param name="date">The date to be updated</param>
        /// <param name="token">The user token</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task<HttpCallResult> UpdateDate(Date date, string token, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the provided workshop current reparations
        /// </summary>
        /// <param name="workshopId">The workshop identifier</param>
        /// <param name="token">The user token</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        Task<HttpCallResult<List<Reparation>>> GetWorkshopReparations(string workshopId, string token, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Updates the reparation data in the API
        /// </summary>
        /// <param name="reparation">The reparation to be updated</param>
        /// <param name="token">The user token</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task<HttpCallResult> UpdateReparation(Reparation reparation, string token, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Gets the user information from the provided plate number.
        /// </summary>
        /// <param name="plateNumber">The vehicle identifier</param>
        /// <param name="token">The user token</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The user information</returns>
        Task<HttpCallResult<User>> GetVehicleOwner (string plateNumber, string token, CancellationToken cancellationToken = default);
    }
}