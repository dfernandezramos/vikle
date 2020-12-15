// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System.Threading.Tasks;
using Vikle.Core.Models;

namespace Vikle.Core.Interfaces
{
    /// <summary>
    /// This interface contains the definition of the contact customer service
    /// </summary>
    public interface IContactCustomerService
    {
        /// <summary>
        /// This method uses the phone service to call the vehicle owner
        /// </summary>
        /// <param name="phone">The customer phone number</param>
        Task<Result> CallCustomer(string phone);

        /// <summary>
        /// This method uses the default device email app to send a message to the specified email
        /// </summary>
        /// <param name="email">The email address</param>
        Task<Result> SendEmail(string email);
        
        /// <summary>
        /// This method gets the owner of the vehicle
        /// </summary>
        /// <param name="plateNumber">The vehicle identifier</param>
        /// <returns>The vehicle owner</returns>
        Task<Result<User>> GetVehicleOwner(string plateNumber);
    }
}