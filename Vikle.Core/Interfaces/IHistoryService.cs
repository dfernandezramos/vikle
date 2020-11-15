using System.Threading.Tasks;
using MvvmCross.ViewModels;
using Vikle.Core.Models;

namespace Vikle.Core.Interfaces
{
    /// <summary>
    /// This interface contains the definition of the reparations history service
    /// </summary>
    public interface IHistoryService
    {
        /// <summary>
        /// Gets the provided vehicle reparations history
        /// </summary>
        /// <param name="plateNumber">The vehicle identifier</param>
        /// <returns>A list with all the vehicle reparations</returns>
        Task<Result<MvxObservableCollection<Reparation>>> GetReparations(string plateNumber);
    }
}