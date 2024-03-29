// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
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
        private MvxObservableCollection<Vehicle> _vehicles;

        /// <summary>
        /// Gets or sets the user vehicles.
        /// </summary>
        public MvxObservableCollection<Vehicle> Vehicles
        {
            get => _vehicles;
            set
            {
                _vehicles = value;
                RaisePropertyChanged(() => Vehicles);
            }
        }

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
        
        /// <summary>
        /// Gets or sets a boolean indicating whether this viewmodel is calling the API or not
        /// </summary>
        public bool CallingAPI { get; set; }

        public VehiclesVM(IMvxNavigationService mvxNavigationService, IVehiclesService vehiclesService) : base(mvxNavigationService)
        {
            _vehiclesService = vehiclesService;
            ShowVehicleDetailsCommand = new MvxAsyncCommand<(Vehicle, bool)>(ShowVehicleDetails);
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            Task.Run(GetVehicles);
        }

        async Task GetVehicles()
        {
            CallingAPI = true;
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
            CallingAPI = false;
        }

        async Task ShowVehicleDetails((Vehicle, bool) vehicle, CancellationToken cancellationToken = default)
        {
            await _mvxNavigationService.Navigate<VehicleDetailVM, (Vehicle, bool)>(vehicle, cancellationToken: cancellationToken);
        }
    }
}