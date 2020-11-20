using System.Threading.Tasks;
using MvvmCross.ViewModels;
using Vikle.Core.Models;

namespace Vikle.Core.Interfaces
{
    /// <summary>
    /// This interface contains the definition of the client vehicles service.
    /// </summary>
    public interface IVehiclesService
    {
        /// <summary>
        /// Gets the current user vehicles
        /// </summary>
        /// <returns>The user vehicles information</returns>
        Task<Result<MvxObservableCollection<Vehicle>>> GetUserVehicles();
    }
}