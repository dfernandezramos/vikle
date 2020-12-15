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
    /// This interface contains the definition of the date details service.
    /// </summary>
    public interface IDateDetailsService
    {
        /// <summary>
        /// Saves the introduced date details in the server API.
        /// </summary>
        /// <param name="date">The date details to stored</param>
        Task<Result> SaveDate (Date date);
    }
}