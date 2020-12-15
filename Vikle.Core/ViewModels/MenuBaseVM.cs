// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System.Threading;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Vikle.Core.Interfaces;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class contains the base viewmodel for the menus in the master detail pages
    /// </summary>
    public class MenuBaseVM : MvxViewModel
    {
        protected readonly IMvxNavigationService _mvxNavigationService;
        readonly ISecureStorageService _secureStorageService;
        
        public MvxAsyncCommand LogoutCommand { get; }
        
        public MenuBaseVM(IMvxNavigationService mvxNavigationService)
        {
            _mvxNavigationService = mvxNavigationService;
            _secureStorageService = Mvx.IoCProvider.Resolve<ISecureStorageService>();
            LogoutCommand = new MvxAsyncCommand(Logout);
        }

        async Task Logout(CancellationToken cancellationToken)
        {
            _secureStorageService.Remove(Constants.SS_TOKEN);
            _secureStorageService.Remove(Constants.SS_USER_ID);
            _secureStorageService.Remove(Constants.SS_WORKER);
            
            Utils.CloseMenu();
            await _mvxNavigationService.Navigate<WelcomeVM>(cancellationToken: cancellationToken);
        }
    }
}