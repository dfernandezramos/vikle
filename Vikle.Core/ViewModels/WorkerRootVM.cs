using System.Threading.Tasks;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class implements the client root viewmodel for the MasterDetailPage in the Client side
    /// </summary>
    public class WorkerRootVM : MvxNavigationViewModel
    {
        private readonly IMvxNavigationService _navigationService;
		
        public WorkerRootVM (IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            _navigationService = navigationService;
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            MvxNotifyTask.Create(async ()=> await this.InitializeViewModels());
        }

        async Task InitializeViewModels()
        {
            await _navigationService.Navigate<WorkerMenuVM>();
            await _navigationService.Navigate<WSReparationsVM>();
        }
    }
}