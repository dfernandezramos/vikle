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
using Vikle.Core.Interfaces;
using Vikle.Core.Models;

namespace Vikle.Core.Services
{
    /// <summary>
    /// This class contains the implementation of the date details service.
    /// </summary>
    public class DateDetailsService : IDateDetailsService
    {
        IApiClientService _clientService;
        ISecureStorageService _secureStorageService;
        
        public DateDetailsService()
        {
            _clientService = Mvx.IoCProvider.Resolve<IApiClientService>();
            _secureStorageService = Mvx.IoCProvider.Resolve<ISecureStorageService>();
        }
        
        /// <summary>
        /// Saves the introduced date details in the server API.
        /// </summary>
        /// <param name="date">The date details to stored</param>
        public async Task<Result> SaveDate(Date date)
        {
            Result result = new Result();
            string token = await _secureStorageService.GetAsync(Constants.SS_TOKEN);
            string userId = await _secureStorageService.GetAsync(Constants.SS_USER_ID);
            date.IdClient = userId;
            
            var reparationResult = await _clientService.UpdateDate(date, token);
            
            if (reparationResult.Error)
            {
                switch (reparationResult.HttpStatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        result.Message = Strings.UserUnauthorised;
                        break;
                    case HttpStatusCode.BadRequest:
                        result.Message = Strings.VehicleAlreadyWithDate;
                        break;
                    default:
                        result.Message = Strings.ServerError;
                        break;
                }

                result.Error = true;
                return result;
            }
            
            return result;
        }
    }
}