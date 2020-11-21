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