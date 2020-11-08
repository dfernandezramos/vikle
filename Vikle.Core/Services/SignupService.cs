using System;
using System.Linq;
using System.Text.RegularExpressions;
using Vikle.Core.Interfaces;
using Vikle.Core.Models;

namespace Vikle.Core.Services
{
    /// <summary>
    /// This class contains the implementation of the signup service.
    /// </summary>
    public class SignupService : ISignupService
    {
        /// <summary>
        /// This method calls the API to register the user in the application.
        /// </summary>
        /// <param name="data">The user signup data</param>
        /// <returns>A result indicating whether this signup action was successful or not.</returns>
        public Result SignUp(SignupData data)
        {
            Result result = PerformDataChecks(data);

            if (result.Error)
            {
                return result;
            }

            // TODO: Pending implement signup API call

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