using System;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Vikle.Core.Enums;
using Vikle.Core.Interfaces;
using Vikle.Core.Models;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class implements the vehicle detail viewmodel
    /// </summary>
    public class VehicleDetailVM : ClientBaseVM<(Vehicle, bool)>
    {
        private IVehicleDetailService _vehicleDetailService;
        private bool _showDetailError;
        private string _detailError;
        private string oldPlateNumber;
        private bool _editionMode;
        private ReparationStatus? _reparationStatus;
        private Vehicle _model;

        /// <summary>
        /// Gets or sets the vehicle data model.
        /// </summary>
        public Vehicle Model
        {
            get => _model;
            set
            {
                _model = value;
                RaisePropertyChanged(() => Model);
                RaiseAllPropertiesChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the view will be in edition mode or not.
        /// </summary>
        public bool EditionMode
        {
            get => _editionMode;
            set
            {
                _editionMode = value;
                RaisePropertyChanged(() => EditionMode);
            }
        }

        public string PlateNumber
        {
            get => Model.PlateNumber;
            set
            {
                Model.PlateNumber = value;
                RaisePropertyChanged(() => PlateNumber);
            }
        }
        
        /// <summary>
        /// Gets or sets the vehicle model
        /// </summary>
        public string VehicleModel
        {
            get => Model.Model;
            set
            {
                Model.Model = value;
                RaisePropertyChanged(() => VehicleModel);
            }
        }
        
        /// <summary>
        /// Gets or sets the vehicle type
        /// </summary>
        public VehicleType VehicleType
        {
            get => Model.VehicleType;
            set
            {
                Model.VehicleType = value;
                RaisePropertyChanged(() => VehicleType);
            }
        }

        /// <summary>
        /// Gets or sets the vehicle year
        /// </summary>
        public int Year
        {
            get => Model.Year;
            set
            {
                Model.Year = value;
                RaisePropertyChanged(() => Year);
            }
        }
        
        /// <summary>
        /// Gets or sets the vehicle last tbds date
        /// </summary>
        public DateTime LastTBDS
        {
            get => Model.LastTBDS;
            set
            {
                Model.LastTBDS = value;
                RaisePropertyChanged(() => LastTBDS);
            }
        }
        
        /// <summary>
        /// Gets or sets the vehicle last ITV date
        /// </summary>
        public DateTime LastITV
        {
            get => Model.LastITV;
            set
            {
                Model.LastITV = value;
                RaisePropertyChanged(() => LastITV);
            }
        }

        /// <summary>
        /// Gets or sets a boolean indicating if the car is being repaired or not
        /// </summary>
        public bool ShowReparationStatus => ReparationStatus != null &&
                                            ReparationStatus != Enums.ReparationStatus.Rejected &&
                                            ReparationStatus != Enums.ReparationStatus.Completed &&
                                            !EditionMode;

        /// <summary>
        /// Gets or sets the car reparation status in case the car is being repaired
        /// </summary>
        public ReparationStatus? ReparationStatus
        {
            get => _reparationStatus;
            set
            {
                _reparationStatus = value;
                RaisePropertyChanged(()=> ReparationStatus);
                RaisePropertyChanged(()=> ShowReparationStatus);
            }
        }

        /// <summary>
        /// Gets or sets the Delete car command
        /// </summary>
        public MvxAsyncCommand DeleteVehicleCommand { get; set; }
        
        /// <summary>
        /// Gets or sets the Update car command
        /// </summary>
        public MvxAsyncCommand UpdateVehicleCommand { get; set; }
        
        /// <summary>
        /// Gets or sets the Reparations history command
        /// </summary>
        public MvxAsyncCommand ReparationsHistoryCommand { get; set; }
        
        /// <summary>
        /// Gets or sets the edit car command
        /// </summary>
        public MvxAsyncCommand<bool> EditVehicleCommand { get; set; }
        
        /// <summary>
        /// Gets or sets the detail error message.
        /// </summary>
        public string DetailError
        {
            get => _detailError;
            set
            {
                _detailError = value;
                RaisePropertyChanged(() => DetailError);
            }
        }

        /// <summary>
        /// Gets or sets a boolean indicating whether the detail error message has to be shown or not.
        /// </summary>
        public bool ShowDetailError
        {
            get => _showDetailError;
            set
            {
                _showDetailError = value;
                RaisePropertyChanged(() => ShowDetailError);
            }
        }
        
        /// <summary>
        /// Gets or sets a boolean indicating whether the delete option is visible or not 
        /// </summary>
        public bool ShowDeleteOption => !string.IsNullOrEmpty(oldPlateNumber) && EditionMode;

        public VehicleDetailVM(IMvxNavigationService mvxNavigationService, IVehicleDetailService vehicleDetailService) : base(mvxNavigationService)
        {
            _vehicleDetailService = vehicleDetailService;
            UpdateVehicleCommand = new MvxAsyncCommand(UpdateVehicle);
            DeleteVehicleCommand = new MvxAsyncCommand(DeleteVehicle);
            EditVehicleCommand = new MvxAsyncCommand<bool>(ChangeEditionMode);
            ReparationsHistoryCommand = new MvxAsyncCommand(ShowHistory);
        }

        public override void Prepare((Vehicle, bool) reparation)
        {
            base.Prepare(reparation);

            Model = reparation.Item1;
            EditionMode = reparation.Item2;
            oldPlateNumber = PlateNumber;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            await GetReparationStatus();
        }

        async Task GetReparationStatus()
        {
            ShowDetailError = false;
            Result<ReparationStatus> result = await _vehicleDetailService.GetReparationStatus(PlateNumber);

            if (result.Error)
            {
                DetailError = result.Message;
                ShowDetailError = true;
            }
            else
            {
                ReparationStatus = result.Data;
            }
        }
        
        async Task UpdateVehicle()
        {
            ShowDetailError = false;
            
            if (Model == null || !Model.IsComplete)
            {
                DetailError = Strings.AllFieldsAreRequired;
                ShowDetailError = true;
                return;
            }

            oldPlateNumber ??= Model.PlateNumber;
            Result result = await _vehicleDetailService.UpdateVehicle(oldPlateNumber, Model);
            
            if (result.Error)
            {
                DetailError = result.Message;
                ShowDetailError = true;
            }
            else
            {
                await ChangeEditionMode();
                oldPlateNumber = PlateNumber;
            }
        }
        
        async Task DeleteVehicle()
        {
            ShowDetailError = false;
            Result result = await _vehicleDetailService.DeleteVehicle(oldPlateNumber);
            
            if (result.Error)
            {
                DetailError = result.Message;
                ShowDetailError = true;
            }
            else
            {
                await ChangeEditionMode(true);
            }
        }

        async Task ChangeEditionMode(bool closeView = false)
        {
            ShowDetailError = false;
            EditionMode = !EditionMode;
            await RaisePropertyChanged(() => ShowDeleteOption);
            
            if (closeView)
            {
                await _mvxNavigationService.Close(this);
            }
        }
        
        async Task ShowHistory(CancellationToken cancellationToken)
        {
            await _mvxNavigationService.Navigate<ReparationsVM, string>(PlateNumber, cancellationToken: cancellationToken);
        }
    }
}