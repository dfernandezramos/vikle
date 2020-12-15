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
    /// This class contains the implementation of the workshop reparation detail service
    /// </summary>
    public class WSReparationDetailService : IWSReparationDetailService
    {
        readonly IApiClientService _clientService;
        readonly ISecureStorageService _secureStorageService;
        
        public WSReparationDetailService()
        {
            _clientService = Mvx.IoCProvider.Resolve<IApiClientService>();
            _secureStorageService = Mvx.IoCProvider.Resolve<ISecureStorageService>();
        }
        
        /// <summary>
        /// This method updates the provided reparation in the API
        /// </summary>
        /// <param name="reparation">The reparation model</param>
        public async Task<Result> UpdateReparation(Reparation reparation)
        {
            Result result = new Result();
            string token = await _secureStorageService.GetAsync(Constants.SS_TOKEN);
            var reparationResult = await _clientService.UpdateReparation(reparation, token);
            
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