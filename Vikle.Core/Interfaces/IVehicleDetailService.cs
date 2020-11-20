using System.Threading.Tasks;
using Vikle.Core.Enums;
using Vikle.Core.Models;

namespace Vikle.Core.Interfaces
{
    /// <summary>
    /// This interface contains the definition of the vehicle detail service
    /// </summary>
    public interface IVehicleDetailService
    {
        /// <summary>
        /// Gets the reparation status of the provided vehicle plate number
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <returns></returns>
        Task<Result<ReparationStatus>> GetReparationStatus(string plateNumber);
        
        /// <summary>
        /// Updates the provided car with the provided vehicle data model
        /// </summary>
        /// <param name="vehicle">The vehicle data model</param>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <returns></returns>
        Task<Result> UpdateVehicle(string plateNumber, Vehicle vehicle);
        
        /// <summary>
        /// Deletes the car related to the calling user with the provided vehicle plate number
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <returns></returns>
        Task<Result> DeleteVehicle(string plateNumber);
    }
}