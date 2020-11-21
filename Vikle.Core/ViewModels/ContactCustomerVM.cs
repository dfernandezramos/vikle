using MvvmCross.Navigation;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class contains the implementation of the Contact Customer VM
    /// </summary>
    public class ContactCustomerVM : WorkerBaseVM<string>
    {
        public ContactCustomerVM(IMvxNavigationService mvxNavigationService) : base(mvxNavigationService)
        {
            
        }
        
        public override void Prepare(string plateNumber)
        {
            // TODO: Pending implement call to get owner id
        }
    }
}