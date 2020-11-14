using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Vikle.Core.ViewModels
{
    public class ClientBaseVM : MvxViewModel
    {
        protected readonly IMvxNavigationService _mvxNavigationService;

        public MvxAsyncCommand HomeNavigationCommand { get; }

        public ClientBaseVM(IMvxNavigationService mvxNavigationService)
        {
            _mvxNavigationService = mvxNavigationService;
            HomeNavigationCommand = new MvxAsyncCommand(HomeNavigation);
        }

        async Task HomeNavigation(CancellationToken cancellationToken)
        {
            await _mvxNavigationService.Navigate<VehiclesVM>(cancellationToken: cancellationToken);
        }
    }
    
    public class ClientBaseVM<T> : MvxViewModel<T>
    {
        protected readonly IMvxNavigationService _mvxNavigationService;

        public MvxAsyncCommand HomeNavigationCommand { get; }

        public ClientBaseVM(IMvxNavigationService mvxNavigationService)
        {
            _mvxNavigationService = mvxNavigationService;
            HomeNavigationCommand = new MvxAsyncCommand(HomeNavigation);
        }

        async Task HomeNavigation(CancellationToken cancellationToken)
        {
            await _mvxNavigationService.Navigate<VehiclesVM>(cancellationToken: cancellationToken);
        }

        public override void Prepare(T parameter)
        {
            
        }
    }
}