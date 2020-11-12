using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class contains the implementation of the Menu ViewModel for the MasterDetailPage in the Worker side
    /// </summary>
    public class WorkerMenuVM : MenuBaseVM
    {
        /// <summary>
        /// Gets or sets the reparations view navigation command.
        /// </summary>
        public MvxAsyncCommand ReparationsNavigationCommand { get; set; }

        public WorkerMenuVM(IMvxNavigationService mvxNavigationService) : base(mvxNavigationService)
        {
            ReparationsNavigationCommand = new MvxAsyncCommand(CloseMenu);
        }

        async Task CloseMenu(CancellationToken cancellationToken)
        {
            Utils.CloseMenu();
        }
    }
}