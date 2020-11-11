using MvvmCross.Navigation;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class implements the vehicle detail viewmodel
    /// </summary>
    public class VehicleDetailVM : ClientBaseVM
    {
        public VehicleDetailVM(IMvxNavigationService mvxNavigationService) : base(mvxNavigationService)
        {
        }
    }
}