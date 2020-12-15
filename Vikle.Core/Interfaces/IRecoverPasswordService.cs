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
    /// THis interface contains the definition of the recover password service.
    /// </summary>
    public interface IRecoverPasswordService
    {
        /// <summary>
        /// This method calls the API with the provided email address in order to recover the password.
        /// </summary>
        /// <param name="email">The email the user wants to recover the password from</param>
        /// <returns>A result indicating whether this recover action was successful or not.</returns>
        Task<Result> RecoverPassword(string email);
    }
}