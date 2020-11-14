using MvvmCross.Navigation;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class implements the reparations viewmodel for the client side
    /// </summary>
    public class ReparationsVM : ClientBaseVM<string>
    {
        public ReparationsVM(IMvxNavigationService mvxNavigationService) : base(mvxNavigationService)
        {
        }

        public override void Prepare(string plateNumber)
        {
            base.Prepare();
        }
    }
}