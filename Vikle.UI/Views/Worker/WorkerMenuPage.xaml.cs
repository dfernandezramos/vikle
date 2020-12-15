// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core.ViewModels;
using Xamarin.Forms;

namespace Vikle.UI.Views.Worker
{
    /// <summary>
    /// This class contains the implementation of the Menu page as Master for the MasterDetailPage in the Worker side
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Master)]
    public partial class WorkerMenuPage : MvxContentPage<WorkerMenuVM>
    {
        public WorkerMenuPage()
        {
            InitializeComponent();
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            
            var reparationsTapGestureRecognizer = new TapGestureRecognizer();
            reparationsTapGestureRecognizer.Tapped += async (sender, args) => await ViewModel.ReparationsNavigationCommand.ExecuteAsync();
            ReparationsButton.GestureRecognizers.Add(reparationsTapGestureRecognizer);
            
            var logoutTapGestureRecognizer = new TapGestureRecognizer();
            logoutTapGestureRecognizer.Tapped += async (sender, args) => await ViewModel.LogoutCommand.ExecuteAsync();
            LogoutButton.GestureRecognizers.Add(logoutTapGestureRecognizer);
        }
    }
}