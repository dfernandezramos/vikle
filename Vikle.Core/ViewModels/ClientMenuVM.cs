using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Vikle.Core.Interfaces;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class contains the implementation of the Menu ViewModel for the MasterDetailPage in the Client side
    /// </summary>
    public class ClientMenuVM : MenuBaseVM
    {
        /// <summary>
        /// Gets or sets the vehicles view navigation command.
        /// </summary>
        public MvxAsyncCommand VehiclesNavigationCommand { get; set; }
        
        /// <summary>
        /// Gets or sets the dates view navigation command.
        /// </summary>
        public MvxAsyncCommand DatesNavigationCommand { get; set; }
        
        public ClientMenuVM(IMvxNavigationService mvxNavigationService) : base(mvxNavigationService)
        {
            VehiclesNavigationCommand = new MvxAsyncCommand(VehiclesNavigation);
            DatesNavigationCommand = new MvxAsyncCommand(DatesNavigation);
        }

        async Task DatesNavigation(CancellationToken cancellationToken)
        {
            Utils.CloseMenu();
            await _mvxNavigationService.Navigate<DatesVM>(cancellationToken: cancellationToken);
        }
        
        async Task VehiclesNavigation(CancellationToken cancellationToken)
        {
            Utils.CloseMenu();
            await _mvxNavigationService.Navigate<VehiclesVM>(cancellationToken: cancellationToken);
        }
    }
}