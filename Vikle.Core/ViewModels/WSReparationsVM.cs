using System.Collections.ObjectModel;
using MvvmCross.Navigation;
using Vikle.Core.Models;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class contains the implementation of the workshop reparations viewmodel.
    /// </summary>
    public class WSReparationsVM : WorkerBaseVM
    {
        /// <summary>
        /// Gets the reparation models of the current reparations in the Workshop.
        /// </summary>
        public ObservableCollection<Reparation> Reparations { get; }
        
        public WSReparationsVM(IMvxNavigationService mvxNavigationService) : base(mvxNavigationService)
        {
            Reparations = new ObservableCollection<Reparation>();
        }
    }
}