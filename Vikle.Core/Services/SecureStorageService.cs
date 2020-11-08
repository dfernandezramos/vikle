using System;
using System.Threading.Tasks;
using Vikle.Core.Interfaces;
using Xamarin.Essentials;

namespace Vikle.Core.Services
{
    /// <summary>
    /// This class implements the Secure Storage service.
    /// </summary>
    public class SecureStorageService : ISecureStorageService
    {
        /// <summary>
        /// This method sets the provided value in the provided key
        /// </summary>
        /// <param name="key">The key to store the value with</param>
        /// <param name="value">The value to be stored</param>
        public async Task SetAsync(string key, string value)
        {
            try
            {
                await SecureStorage.SetAsync(key, value);
            }
            catch (Exception e)
            {
                // TODO: Write log here
            }
        }

        /// <summary>
        /// This method gets the provided value in the provided key
        /// </summary>
        /// <param name="key">The key the value is stored with</param>
        /// <returns>The value of the provided key</returns>
        public async Task<string> GetAsync(string key)
        {
            try
            {
                return await SecureStorage.GetAsync(key);
            }
            catch (Exception e)
            {
                // TODO: Write log here
                return null;
            }
        }
    }
}