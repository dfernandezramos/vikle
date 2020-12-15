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
    /// This class contains the implementation of the dates service.
    /// </summary>
    public class DatesService : IDatesService
    {
        readonly IApiClientService _clientService;
        readonly ISecureStorageService _secureStorageService;
        
        public DatesService()
        {
            _clientService = Mvx.IoCProvider.Resolve<IApiClientService>();
            _secureStorageService = Mvx.IoCProvider.Resolve<ISecureStorageService>();
        }

        /// <summary>
        /// Gets the current user dates
        /// </summary>
        /// <returns>The user dates information</returns>
        public async Task<Result<MvxObservableCollection<Date>>> GetUserDates()
        {
            Result<MvxObservableCollection<Date>> result = new Result<MvxObservableCollection<Date>>();
            string userId = await _secureStorageService.GetAsync(Constants.SS_USER_ID);
            string token = await _secureStorageService.GetAsync(Constants.SS_TOKEN);
            var vehiclesResult = await _clientService.GetUserDates(userId, token);
            
            if (vehiclesResult.Error)
            {
                result.Error = true;
                result.Message = vehiclesResult?.HttpStatusCode == HttpStatusCode.Unauthorized
                    ? Strings.UserUnauthorised
                    : Strings.ServerError;
                return result;
            }
            
            result.Data = new MvxObservableCollection<Date> (vehiclesResult.Result);
            
            return result;
            
            // TODO: Remove this mock after implementing API
            // return new Result<MvxObservableCollection<Date>>
            // {
            //     Data = new MvxObservableCollection<Date> {
            //         new Date {
            //                 IdClient = "1",
            //                 ReparationDate = DateTime.Today,
            //                 PlateNumber = "1234 ABC",
            //                 Id = "1",
            //                 Status = ReparationStatus.Pending
            //             },
            //         new Date {
            //             IdClient = "1",
            //             ReparationDate = DateTime.Today.AddMonths(1).AddDays(1),
            //             PlateNumber = "5678 DEF",
            //             Id = "2",
            //             Status = ReparationStatus.Repairing
            //         }
            //     }
            // };
        }
    }
}