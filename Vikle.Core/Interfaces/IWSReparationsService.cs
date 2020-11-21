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