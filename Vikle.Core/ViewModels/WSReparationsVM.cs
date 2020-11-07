using System;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class contains the implementation of the workshop reparations viewmodel.
    /// </summary>
    public class WSReparationsVM : MvxViewModel
    {
        readonly IMvxNavigationService _mvxNavigationService;
        
        public WSReparationsVM(IMvxNavigationService mvxNavigationService)
        {
            _mvxNavigationService = mvxNavigationService;
        }
    }
}