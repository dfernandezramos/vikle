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
    /// This interface contains the definition of the workshop reparation detail service
    /// </summary>
    public interface IWSReparationDetailService
    {
        /// <summary>
        /// This method updates the provided reparation in the API
        /// </summary>
        /// <param name="reparation">The reparation model</param>
        Task<Result> UpdateReparation(Reparation reparation);
    }
}