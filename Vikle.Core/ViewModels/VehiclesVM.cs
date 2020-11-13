using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Vikle.Core.Interfaces;
using Vikle.Core.Models;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class contains the implementation of the client vehicles viewmodel
    /// </summary>
    public class VehiclesVM : ClientBaseVM
    {
        private readonly IVehiclesService _vehiclesService;

        /// <summary>
        /// Gets or sets the user vehicles.
        /// </summary>
        public MvxObservableCollection<Vehicle> Vehicles { get; set; }

        /// <summary>
        /// Gets or sets the command to show the selected vehicle details
        /// </summary>
        public MvxAsyncCommand<(Vehicle, bool)> ShowVehicleDetailsCommand { get; set; }

        public VehiclesVM(IMvxNavigationService mvxNavigationService, IVehiclesService vehiclesService) : base(mvxNavigationService)
        {
            _vehiclesService = vehiclesService;
            ShowVehicleDetailsCommand = new MvxAsyncCommand<(Vehicle, bool)>(ShowVehicleDetails);
        }
        
        public override async Task Initialize()
        {
            await base.Initialize();
            Vehicles = await _vehiclesService.GetUserVehicles();
        }

        async Task ShowVehicleDetails((Vehicle, bool) vehicle, CancellationToken cancellationToken = default)
        {
            await _mvxNavigationService.Navigate<VehicleDetailVM>(cancellationToken: cancellationToken);
        }
    }
}