using System.Collections.ObjectModel;
using MvvmCross.Navigation;
using Vikle.Core.Models;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class contains the implementation of the Dates ViewModel.
    /// </summary>
    public class DatesVM : ClientBaseVM
    {
        /// <summary>
        /// Gets the reparation models of the current reparations in the Workshop.
        /// </summary>
        public ObservableCollection<Date> Dates { get; }

        public DatesVM(IMvxNavigationService mvxNavigationService) : base(mvxNavigationService)
        {
            Dates = new ObservableCollection<Date>();
        }
    }
}