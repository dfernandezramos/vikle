using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class contains the implementation of the Menu ViewModel for the MasterDetailPage in the Worker side
    /// </summary>
    public class WorkerMenuVM : MvxViewModel
    {
        readonly IMvxNavigationService _mvxNavigationService;
        
        public WorkerMenuVM(IMvxNavigationService mvxNavigationService)
        {
            _mvxNavigationService = mvxNavigationService;
        }
    }
}