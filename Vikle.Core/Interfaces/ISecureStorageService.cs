// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System.Threading.Tasks;

namespace Vikle.Core.Interfaces
{
    /// <summary>
    /// This interface wraps the Secure Storage service to allow unit testing it with a dummy service.
    /// </summary>
    public interface ISecureStorageService
    {
        /// <summary>
        /// This method sets the provided value in the provided key
        /// </summary>
        /// <param name="key">The key to store the value with</param>
        /// <param name="value">The value to be stored</param>
        Task SetAsync(string key, string value);
        
        /// <summary>
        /// This method gets the provided value in the provided key
        /// </summary>
        /// <param name="key">The key the value is stored with</param>
        /// <returns>The value of the provided key</returns>
        Task<string> GetAsync(string key);

        /// <summary>
        /// This method removes the value of the provided key in the secure storage
        /// </summary>
        /// <param name="key">The key the value is going to be deleted</param>
        void Remove(string key);
    }
}