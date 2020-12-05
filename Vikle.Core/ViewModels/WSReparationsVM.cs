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
    /// This class contains the implementation of the workshop reparations viewmodel.
    /// </summary>
    public class WSReparationsVM : WorkerBaseVM
    {
        readonly IWSReparationsService _wsReparationsService;
        private string _reparationsError;
        private bool _showReparationsError;
        private MvxObservableCollection<Reparation> _reparations;

        /// <summary>
        /// Gets or sets the plate number of the vehicle.
        /// </summary>
        public string PlateNumber { get; private set; } 
        
        /// <summary>
        /// Gets or sets the reparations error message.
        /// </summary>
        public string ReparationsError
        {
            get => _reparationsError;
            set
            {
                _reparationsError = value;
                RaisePropertyChanged(() => ReparationsError);
            }
        }

        /// <summary>
        /// Gets or sets a boolean indicating whether the reparations error message has to be shown or not.
        /// </summary>
        public bool ShowReparationsError
        {
            get => _showReparationsError;
            set
            {
                _showReparationsError = value;
                RaisePropertyChanged(() => ShowReparationsError);
            }
        }
        
        /// <summary>
        /// Gets or sets the workshop current reparations.
        /// </summary>
        public MvxObservableCollection<Reparation> Reparations
        {
            get => _reparations;
            set
            {
                _reparations = value;
                RaisePropertyChanged(() => Reparations);
            }
        }
        
        /// <summary>
        /// Gets or sets a boolean indicating whether this viewmodel is calling the API or not
        /// </summary>
        public bool CallingAPI { get; set; }
        
        /// <summary>
        /// Gets or sets the reparation detail navigation command
        /// </summary>
        public MvxAsyncCommand<Reparation> ReparationDetailNavigationCommand { get; set; }
        
        public WSReparationsVM(IMvxNavigationService mvxNavigationService, IWSReparationsService wsReparationsService) : base(mvxNavigationService)
        {
            _wsReparationsService = wsReparationsService;
            ReparationDetailNavigationCommand = new MvxAsyncCommand<Reparation>(ReparationDetailNavigation);
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            Task.Run(GetWorkshopReparations);
        }

        async Task GetWorkshopReparations()
        {
            CallingAPI = true;
            var workshopId = await GetWorkshopId();
            if (!string.IsNullOrEmpty(workshopId))
            {
                await GetReparations(workshopId);
            }

            CallingAPI = false;
        }

        async Task<string> GetWorkshopId()
        {
            Result<string> user = await _wsReparationsService.GetUserWorkshopId();
            
            if (user.Error)
            {
                ReparationsError = user.Message;
                ShowReparationsError = true;
                return null;
            }
            else
            {
                return user.Data;
            }
        }
        
        async Task GetReparations(string workshopId)
        {
            ShowReparationsError = false;
            Result<MvxObservableCollection<Reparation>> result = await _wsReparationsService.GetReparations(workshopId);

            if (result.Error)
            {
                ReparationsError = result.Message;
                ShowReparationsError = true;
            }
            else
            {
                Reparations = result.Data;
            }
        }

        async Task ReparationDetailNavigation(Reparation reparation, CancellationToken cancellationToken)
        {
            await _mvxNavigationService.Navigate<WSReparationDetailVM, Reparation>(reparation, cancellationToken: cancellationToken);
        }
    }
}