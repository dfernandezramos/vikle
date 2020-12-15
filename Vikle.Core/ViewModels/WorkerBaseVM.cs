// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
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
            await _mvxNavigationService.Navigate<WSReparationsVM>(cancellationToken: cancellationToken);
        }

        public override void Prepare(T param)
        {
            
        }
    }
}