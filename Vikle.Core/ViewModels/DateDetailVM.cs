using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Vikle.Core.Enums;
using Vikle.Core.Interfaces;
using Vikle.Core.Models;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class implements the date detail viewmodel
    /// </summary>
    public class DateDetailVM : ClientBaseVM
    {
        private IDateDetailsService _datesService;
        private IVehiclesService _vehiclesService;
        private string _dateDetailsError;
        private bool _showDateDetailsError;
        private string _selectedVehiclePlateNumber;
        private ReparationType _reparationReason;
        private DateTime _reparationDate;
        private List<string> _plateNumbers;

        /// <summary>
        /// Gets or sets the plate numbers of the client vehicles.
        /// </summary>
        public List<string> PlateNumbers
        {
            get => _plateNumbers;
            set
            {
                _plateNumbers = value;
                RaisePropertyChanged(() => PlateNumbers);
            }
        }

        /// <summary>
        /// Gets or sets the date details error message.
        /// </summary>
        public string DateDetailsError
        {
            get => _dateDetailsError;
            set
            {
                _dateDetailsError = value;
                RaisePropertyChanged(() => DateDetailsError);
            }
        }

        /// <summary>
        /// Gets or sets a boolean indicating whether the date details error message has to be shown or not.
        /// </summary>
        public bool ShowDateDetailsError
        {
            get => _showDateDetailsError;
            set
            {
                _showDateDetailsError = value;
                RaisePropertyChanged(() => ShowDateDetailsError);
            }
        }

        /// <summary>
        /// Gets or sets the selected vehicle plate number
        /// </summary>
        public string SelectedVehiclePlateNumber
        {
            get => _selectedVehiclePlateNumber;
            set
            {
                _selectedVehiclePlateNumber = value;
                RaisePropertyChanged(() => SelectedVehiclePlateNumber);
            }
        }

        /// <summary>
        /// Gets or sets the reparation status
        /// </summary>
        public ReparationType ReparationReason
        {
            get => _reparationReason;
            set
            {
                _reparationReason = value;
                RaisePropertyChanged(() => ReparationReason);
            }
        }

        /// <summary>
        /// Gets or sets the reparation date
        /// </summary>
        public DateTime ReparationDate
        {
            get => _reparationDate;
            set
            {
                _reparationDate = value;
                RaisePropertyChanged(() => ReparationDate);
            }
        }
        
        /// <summary>
        /// Gets or sets the Update date command
        /// </summary>
        public MvxAsyncCommand UpdateDateCommand { get; set; }
        
        /// <summary>
        /// Gets or sets the close command
        /// </summary>
        public MvxAsyncCommand CloseCommand { get; set; }

        public DateDetailVM(IMvxNavigationService mvxNavigationService, IVehiclesService vehiclesService, IDateDetailsService datesService)
            : base(mvxNavigationService)
        {
            _datesService = datesService;
            _vehiclesService = vehiclesService;
            UpdateDateCommand = new MvxAsyncCommand(UpdateDate);
            CloseCommand = new MvxAsyncCommand(Close);
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            ReparationReason = ReparationType.Maintenance;
            ReparationDate = DateTime.UtcNow.AddDays(1);
            PlateNumbers = new List<string>();
            await GetPlateNumbers();
        }
        
        async Task Close(CancellationToken cancellationToken)
        {
            await _mvxNavigationService.Close(this, cancellationToken);
        }
        
        async Task UpdateDate(CancellationToken cancellationToken)
        {
            ShowDateDetailsError = false;
            Date date = new Date
            {
                ReparationDate = ReparationDate.Ticks,
                Reason = ReparationReason,
                PlateNumber = SelectedVehiclePlateNumber,
                Status = ReparationStatus.Pending,
                WorkshopId = "1"
            };

            Result result = await _datesService.SaveDate(date);
            
            if (result.Error)
            {
                DateDetailsError = result.Message;
                ShowDateDetailsError = true;
            }
            else
            {
                await Close(cancellationToken);
            }
        }

        async Task GetPlateNumbers()
        {
            ShowDateDetailsError = false;
            Result<MvxObservableCollection<Vehicle>> result = await _vehiclesService.GetUserVehicles();

            if (result.Error)
            {
                DateDetailsError = result.Message;
                ShowDateDetailsError = true;
            }
            else
            {
                if (result.Data == null || !result.Data.Any())
                {
                    DateDetailsError = Strings.NoVehicles;
                    ShowDateDetailsError = true;
                }
                else
                {
                    var plates = new List<string>();
                    
                    foreach (var vehicle in result.Data)
                    {
                        plates.Add(vehicle.PlateNumber);
                    }

                    PlateNumbers = plates;
                    SelectedVehiclePlateNumber = result.Data.First().PlateNumber;
                }
            }
        }
    }
}