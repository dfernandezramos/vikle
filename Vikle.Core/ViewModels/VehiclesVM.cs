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
        private string _vehiclesError;
        private bool _showVehiclesError;

        /// <summary>
        /// Gets or sets the user vehicles.
        /// </summary>
        public MvxObservableCollection<Vehicle> Vehicles { get; set; }

        /// <summary>
        /// Gets or sets the command to show the selected vehicle details
        /// </summary>
        public MvxAsyncCommand<(Vehicle, bool)> ShowVehicleDetailsCommand { get; set; }
        
        /// <summary>
        /// Gets or sets the vehicles error message.
        /// </summary>
        public string VehiclesError
        {
            get => _vehiclesError;
            set
            {
                _vehiclesError = value;
                RaisePropertyChanged(() => VehiclesError);
            }
        }

        /// <summary>
        /// Gets or sets a boolean indicating whether the vehicles error message has to be shown or not.
        /// </summary>
        public bool ShowVehiclesError
        {
            get => _showVehiclesError;
            set
            {
                _showVehiclesError = value;
                RaisePropertyChanged(() => ShowVehiclesError);
            }
        }

        public VehiclesVM(IMvxNavigationService mvxNavigationService, IVehiclesService vehiclesService) : base(mvxNavigationService)
        {
            _vehiclesService = vehiclesService;
            ShowVehicleDetailsCommand = new MvxAsyncCommand<(Vehicle, bool)>(ShowVehicleDetails);
        }
        
        public override async Task Initialize()
        {
            await base.Initialize();
            await GetVehicles ();
        }
        
        async Task GetVehicles()
        {
            ShowVehiclesError = false;
            Result<MvxObservableCollection<Vehicle>> result = await _vehiclesService.GetUserVehicles();

            if (result.Error)
            {
                VehiclesError = result.Message;
                ShowVehiclesError = true;
            }
            else
            {
                Vehicles = result.Data;
            }
        }

        async Task ShowVehicleDetails((Vehicle, bool) vehicle, CancellationToken cancellationToken = default)
        {
            await _mvxNavigationService.Navigate<VehicleDetailVM, (Vehicle, bool)>(vehicle, cancellationToken: cancellationToken);
        }
    }
}