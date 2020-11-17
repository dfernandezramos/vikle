using System.Threading.Tasks;
using MvvmCross.ViewModels;
using Vikle.Core.Models;

namespace Vikle.Core.Interfaces
{
    /// <summary>
    /// This interface contains the definition of the dates service.
    /// </summary>
    public interface IDatesService
    {
        /// <summary>
        /// Gets the current user dates
        /// </summary>
        /// <returns>The user vehicles information</returns>
        Task<Result<MvxObservableCollection<Date>>> GetUserDates();
    }
}