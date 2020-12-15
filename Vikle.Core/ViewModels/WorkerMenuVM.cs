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

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class contains the implementation of the Menu ViewModel for the MasterDetailPage in the Worker side
    /// </summary>
    public class WorkerMenuVM : MenuBaseVM
    {
        /// <summary>
        /// Gets or sets the reparations view navigation command.
        /// </summary>
        public MvxAsyncCommand ReparationsNavigationCommand { get; set; }

        public WorkerMenuVM(IMvxNavigationService mvxNavigationService) : base(mvxNavigationService)
        {
            ReparationsNavigationCommand = new MvxAsyncCommand(CloseMenu);
        }

        async Task CloseMenu(CancellationToken cancellationToken)
        {
            Utils.CloseMenu();
        }
    }
}