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
using Vikle.Core.Interfaces;
using Vikle.Core.Models;
using Xamarin.Essentials;

namespace Vikle.Core.Services
{
    /// <summary>
    /// This class contains the implementation of the contact customer service
    /// </summary>
    public class ContactCustomerService : IContactCustomerService
    {
        IApiClientService _clientService;
        ISecureStorageService _secureStorageService;
        IXamarinEssentialsService _xamarinEssentialsService;
        
        public ContactCustomerService()
        {
            _clientService = Mvx.IoCProvider.Resolve<IApiClientService>();
            _secureStorageService = Mvx.IoCProvider.Resolve<ISecureStorageService>();
            _xamarinEssentialsService = Mvx.IoCProvider.Resolve<IXamarinEssentialsService>();
        }

        /// <summary>
        /// This method uses the phone service to call the vehicle owner
        /// </summary>
        /// <param name="phone">The customer phone number</param>
        public async Task<Result> CallCustomer(string phone)
        {
            Result result = new Result();
            
            try
            {
                _xamarinEssentialsService.MakePhoneCall(phone);
            }
            catch (ArgumentNullException)
            {
                result.Error = true;
                result.Message = Strings.WrongNumber;
            }
            catch (FeatureNotSupportedException)
            {
                result.Error = true;
                result.Message = Strings.CallsNotSupported;
            }
            catch (Exception)
            {
                result.Error = true;
                result.Message = Strings.CouldNotCall;
            }

            return result;
        }

        public async Task<Result> SendEmail(string email)
        {
            Result result = new Result();
            
            try
            {
                var message = new EmailMessage
                {
                    Subject = Strings.ReparationStatus,
                    To = new List<string> {email},
                };
                await _xamarinEssentialsService.SendEmail(message);
            }
            catch (FeatureNotSupportedException)
            {
                result.Error = true;
                result.Message = Strings.EmailsNotSupported;
            }
            catch (Exception)
            {
                result.Error = true;
                result.Message = Strings.CouldNotSendEmail;
            }

            return result;
        }

        /// <summary>
        /// This method gets the owner of the vehicle
        /// </summary>
        /// <param name="plateNumber">The vehicle identifier</param>
        /// <returns>The vehicle owner</returns>
        public async Task<Result<User>> GetVehicleOwner(string plateNumber)
        {
            Result<User> result = new Result<User> ();
            string token = await _secureStorageService.GetAsync(Constants.SS_TOKEN);
            var reparationResult = await _clientService.GetVehicleOwner(plateNumber, token);
            
            if (reparationResult.Error)
            {
                result.Error = true;
                result.Message = reparationResult?.HttpStatusCode == HttpStatusCode.Unauthorized
                    ? Strings.UserUnauthorised
                    : Strings.ServerError;
                return result;
            }
            
            result.Data = reparationResult.Result;
            return result;
            
            // TODO: Pending to remove this mock when API calls implemented
            // return new Result<User>
            // {
            //     Data = new User {
            //         Id = "1",
            //         Phone = "123456789",
            //         Email = "user@email.com",
            //         Name = "David",
            //         Surname = "Fernández Ramos"
            //     }
            // };
        }
    }
}