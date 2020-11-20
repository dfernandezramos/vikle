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