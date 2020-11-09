using System.Collections.Generic;
using System.Threading.Tasks;
using Vikle.Core.Interfaces;
using Vikle.Core.Services;

namespace Vikle.Tests
{
    /// <summary>
    /// This service implements a dummy secure storage implementation for unit tests purposes.
    /// </summary>
    public class DummySecureStorageService : ISecureStorageService
    {
        Dictionary<string, string> SecureStorage = new Dictionary<string, string>();
        
        /// <summary>
        /// This method sets the provided value in the provided key
        /// </summary>
        /// <param name="key">The key to store the value with</param>
        /// <param name="value">The value to be stored</param>
        public async Task SetAsync(string key, string value)
        {
            SecureStorage[key] = value;
        }

        /// <summary>
        /// This method gets the provided value in the provided key
        /// </summary>
        /// <param name="key">The key the value is stored with</param>
        /// <returns>The value of the provided key</returns>
        public async Task<string> GetAsync(string key)
        {
            return SecureStorage[key];
        }
    }
}