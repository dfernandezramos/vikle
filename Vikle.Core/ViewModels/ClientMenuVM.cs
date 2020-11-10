using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class contains the implementation of the Menu ViewModel for the MasterDetailPage in the Client side
    /// </summary>
    public class ClientMenuVM : MvxViewModel
    {
        readonly IMvxNavigationService _mvxNavigationService;
        
        public ClientMenuVM(IMvxNavigationService mvxNavigationService)
        {
            _mvxNavigationService = mvxNavigationService;
        }
    }
}