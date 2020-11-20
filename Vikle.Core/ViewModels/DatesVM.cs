using System.Collections.ObjectModel;
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
    /// This class contains the implementation of the Dates ViewModel.
    /// </summary>
    public class DatesVM : ClientBaseVM
    {
        private string _datesError;
        private bool _showDatesError;
        private ObservableCollection<Date> _dates;
        private IDatesService _datesService;

        /// <summary>
        /// Gets the reparation models of the current reparations in the Workshop.
        /// </summary>
        public ObservableCollection<Date> Dates
        {
            get => _dates;
            set
            {
                _dates = value;
                RaisePropertyChanged(() => Dates);
            }
        }

        /// <summary>
        /// Gets or sets the dates error message.
        /// </summary>
        public string DatesError
        {
            get => _datesError;
            set
            {
                _datesError = value;
                RaisePropertyChanged(() => DatesError);
            }
        }

        /// <summary>
        /// Gets or sets a boolean indicating whether the dates error message has to be shown or not.
        /// </summary>
        public bool ShowDatesError
        {
            get => _showDatesError;
            set
            {
                _showDatesError = value;
                RaisePropertyChanged(() => ShowDatesError);
            }
        }
        
        /// <summary>
        /// Gets or sets the command to show the create a new date
        /// </summary>
        public MvxAsyncCommand NewDateDetailsCommand { get; set; }

        public DatesVM(IMvxNavigationService mvxNavigationService, IDatesService datesService) : base(mvxNavigationService)
        {
            _dates = new ObservableCollection<Date>();
            NewDateDetailsCommand = new MvxAsyncCommand(NewDateNavigation);
            _datesService = datesService;
        }
        
        public override async Task Initialize()
        {
            await base.Initialize();
            await GetDates ();
        }
        
        async Task GetDates()
        {
            ShowDatesError = false;
            Result<MvxObservableCollection<Date>> result = await _datesService.GetUserDates();

            if (result.Error)
            {
                DatesError = result.Message;
                ShowDatesError = true;
            }
            else
            {
                Dates = result.Data;
            }
        }

        async Task NewDateNavigation(CancellationToken cancellationToken)
        {
            await _mvxNavigationService.Navigate<DateDetailVM>(cancellationToken: cancellationToken);
        }
    }
}