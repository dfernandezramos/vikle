using System.Collections.ObjectModel;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Vikle.Core.Models;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class contains the implementation of the Dates ViewModel.
    /// </summary>
    public class DatesVM : MvxViewModel
    {
        readonly IMvxNavigationService _mvxNavigationService;
        
        /// <summary>
        /// Gets the reparation models of the current reparations in the Workshop.
        /// </summary>
        public ObservableCollection<Date> Dates { get; }

        public DatesVM(IMvxNavigationService mvxNavigationService)
        {
            _mvxNavigationService = mvxNavigationService;
            Dates = new ObservableCollection<Date>();
        }
    }
}