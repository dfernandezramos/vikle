// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System;
using System.Net;
using System.Threading.Tasks;
using MvvmCross;
using Vikle.Core.Interfaces;
using Vikle.Core.Models;
using Xamarin.Essentials;

namespace Vikle.Core.Services
{
    /// <summary>
    /// This class contains the implementation of the login service.
    /// </summary>
    public class LoginService : ILoginService
    {
        IApiClientService _clientService;
        ISecureStorageService _secureStorageService;
        
        public LoginService()
        {
            _clientService = Mvx.IoCProvider.Resolve<IApiClientService>();
            _secureStorageService = Mvx.IoCProvider.Resolve<ISecureStorageService>();
        }
        
        /// <summary>
        /// This method uses the provided credentials to log into the application.
        /// </summary>
        /// <param name="email">The user email</param>
        /// <param name="password">The user password</param>
        /// <returns>A LoginResult indicating whether this login action was successful or not.</returns>
        public async Task<LoginResult> Login(string email, string password)
        {
            var result = PerformChecks(email, password);

            if (result.Error)
            {
                return result;
            }
            
            var loginData = await _clientService.GetUserToken(email, password);

            if (loginData == null || loginData.Error)
            {
                result.Error = true;
                result.Message = loginData?.HttpStatusCode == HttpStatusCode.Forbidden
                    ? Strings.IncorrectEmailOrPassword
                    : Strings.ServerError;
                return result;
            }

            var userData = await _clientService.GetUserInformation(loginData.Result.UserId, loginData.Result.Token);

            if (userData == null || userData.Error)
            {
                result.Error = true;
                result.Message = userData?.HttpStatusCode == HttpStatusCode.Forbidden
                    ? Strings.IncorrectEmailOrPassword
                    : Strings.ServerError;
                return result;
            }
            
            result.Worker = userData.Result.IsWorker;
            await StoreUserData(loginData.Result.UserId, loginData.Result.Token, result.Worker);

            return result;
        }

        LoginResult PerformChecks(string email, string password)
        {
            var result = new LoginResult();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                result.Error = true;
                result.Message = Strings.EmailPasswordRequired;
                return result;
            }

            if (!Utils.IsValidEmail(email))
            {
                result.Error = true;
                result.Message = Strings.EnterValidEmail;
                return result;
            }

            return result;
        }

        async Task StoreUserData(string userId, string token, bool worker)
        {
            await _secureStorageService.SetAsync(Constants.SS_TOKEN, token);
            await _secureStorageService.SetAsync(Constants.SS_USER_ID, userId);
            if (worker)
            {
                await _secureStorageService.SetAsync(Constants.SS_WORKER, "worker");
            }
        }
    }
}