using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Commands;
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

        /// <summary>
        /// Gets or sets the reparations view navigation command.
        /// </summary>
        public MvxAsyncCommand ReparationsNavigationCommand { get; set; }

        public WorkerMenuVM(IMvxNavigationService mvxNavigationService)
        {
            _mvxNavigationService = mvxNavigationService;
            ReparationsNavigationCommand = new MvxAsyncCommand(CloseMenu);
        }

        async Task CloseMenu(CancellationToken cancellationToken)
        {
            Utils.CloseMenu();
        }
    }
}