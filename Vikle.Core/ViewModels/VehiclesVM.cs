using System;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class contains the implementation of the client vehicles viewmodel
    /// </summary>
    public class VehiclesVM : ClientBaseVM
    {
        public VehiclesVM(IMvxNavigationService mvxNavigationService) : base(mvxNavigationService)
        {
            
        }
    }
}