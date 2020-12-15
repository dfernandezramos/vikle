// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MvvmCross;
using Vikle.Core.Interfaces;
using Vikle.Core.Models;

namespace Vikle.Core.Services
{
    /// <summary>
    /// This class contains the implementation of the signup service.
    /// </summary>
    public class SignupService : ISignupService
    {
        IApiClientService _clientService;
        
        public SignupService()
        {
            _clientService = Mvx.IoCProvider.Resolve<IApiClientService>();
        }
        
        /// <summary>
        /// This method calls the API to register the user in the application.
        /// </summary>
        /// <param name="data">The user signup data</param>
        /// <returns>A result indicating whether this signup action was successful or not.</returns>
        public async Task<Result> SignUp(SignupData data)
        {
            Result result = PerformDataChecks(data);

            if (result.Error)
            {
                return result;
            }

            var signupResult = await _clientService.Signup(data);
            
            if (signupResult.Error)
            {
                result.Error = true;
                result.Message = signupResult?.HttpStatusCode == HttpStatusCode.Conflict
                    ? Strings.EmailAlreadyInUse
                    : Strings.ServerError;
                return result;
            }

            return result;
        }

        Result PerformDataChecks(SignupData data)
        {
            Result result = new Result();

            if (!data.IsComplete)
            {
                result.Error = true;
                result.Message = Strings.AllFieldsAreRequired;
                return result;
            }

            if (!Utils.IsValidEmail(data.Email))
            {
                result.Error = true;
                result.Message = Strings.EnterValidEmail;
                return result;
            }
            
            if (data.Password != data.RepeatedPassword)
            {
                result.Error = true;
                result.Message = Strings.PasswordsAreNotEqual;
                return result;
            }

            result = CheckPassword(data.Password);
            
            return result;
        }

        Result CheckPassword(string password)
        {
            Result result = new Result();
            
            if (password.Length < 8)
            {
                result.Error = true;
                result.Message = Strings.IncorrectPasswordLength;
                return result;
            }
            
            if (!(password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsNumber)))
            {
                result.Error = true;
                result.Message = Strings.PasswordIncorrectFormat;
                return result;
            }

            return result;
        }
    }
}