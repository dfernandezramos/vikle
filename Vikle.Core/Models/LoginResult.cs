// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
namespace Vikle.Core.Models
{
    /// <summary>
    /// This class contains the definition of the login action result.
    /// </summary>
    public class LoginResult : Result
    {
        /// <summary>
        /// Gets or sets a boolean indicating if the successful login has worker condition. Otherwise it will have
        /// a client condition.
        /// </summary>
        public bool Worker { get; set; }
    }
}