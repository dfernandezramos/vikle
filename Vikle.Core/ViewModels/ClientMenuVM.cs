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
using Vikle.Core.Interfaces;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class contains the implementation of the Menu ViewModel for the MasterDetailPage in the Client side
    /// </summary>
    public class ClientMenuVM : MenuBaseVM
    {
        /// <summary>
        /// Gets or sets the vehicles view navigation command.
        /// </summary>
        public MvxAsyncCommand VehiclesNavigationCommand { get; set; }
        
        /// <summary>
        /// Gets or sets the dates view navigation command.
        /// </summary>
        public MvxAsyncCommand DatesNavigationCommand { get; set; }
        
        public ClientMenuVM(IMvxNavigationService mvxNavigationService) : base(mvxNavigationService)
        {
            VehiclesNavigationCommand = new MvxAsyncCommand(VehiclesNavigation);
            DatesNavigationCommand = new MvxAsyncCommand(DatesNavigation);
        }

        async Task DatesNavigation(CancellationToken cancellationToken)
        {
            Utils.CloseMenu();
            await _mvxNavigationService.Navigate<DatesVM>(cancellationToken: cancellationToken);
        }
        
        async Task VehiclesNavigation(CancellationToken cancellationToken)
        {
            Utils.CloseMenu();
            await _mvxNavigationService.Navigate<VehiclesVM>(cancellationToken: cancellationToken);
        }
    }
}