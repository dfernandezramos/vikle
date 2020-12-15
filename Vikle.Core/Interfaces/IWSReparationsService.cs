// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System.Threading.Tasks;
using MvvmCross.ViewModels;
using Vikle.Core.Models;

namespace Vikle.Core.Interfaces
{
    /// <summary>
    /// This interface defines the workshop reparations service
    /// </summary>
    public interface IWSReparationsService
    {
        /// <summary>
        /// Gets the provided user workshop identifier
        /// </summary>
        /// <returns>The workshop identifier</returns>
        Task<Result<string>> GetUserWorkshopId();

        /// <summary>
        /// This method gets the current reparations of the provided workshop identifier
        /// </summary>
        /// <param name="workshopId">The workshop identifier</param>
        /// <returns>A list with the current workshop reparations</returns>
        Task<Result<MvxObservableCollection<Reparation>>> GetReparations(string workshopId);
    }
}