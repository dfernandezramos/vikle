using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Vikle.Core.ViewModels
{
    public class WorkerBaseVM : MvxViewModel
    {
        protected readonly IMvxNavigationService _mvxNavigationService;
        
        public MvxAsyncCommand HomeNavigationCommand { get; }
        
        public WorkerBaseVM(IMvxNavigationService mvxNavigationService)
        {
            _mvxNavigationService = mvxNavigationService;
            HomeNavigationCommand = new MvxAsyncCommand(HomeNavigation);
        }

        async Task HomeNavigation(CancellationToken cancellationToken)
        {
            await _mvxNavigationService.Navigate<WSReparationsVM>(cancellationToken: cancellationToken);
        }
    }
    
    public class WorkerBaseVM<T> : MvxViewModel<T>
    {
        protected readonly IMvxNavigationService _mvxNavigationService;

        public MvxAsyncCommand HomeNavigationCommand { get; }

        public WorkerBaseVM(IMvxNavigationService mvxNavigationService)
        {
            _mvxNavigationService = mvxNavigationService;
            HomeNavigationCommand = new MvxAsyncCommand(HomeNavigation);
        }

        async Task HomeNavigation(CancellationToken cancellationToken)
        {
            await _mvxNavigationService.Navigate<VehiclesVM>(cancellationToken: cancellationToken);
        }

        public override void Prepare(T reparation)
        {
            
        }
    }
}