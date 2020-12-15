// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
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