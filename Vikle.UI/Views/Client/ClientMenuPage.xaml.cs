// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class contains the implementation of the Menu page as Master for the MasterDetailPage in the Client side
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Master)]
    public partial class ClientMenuPage : MvxContentPage<ClientMenuVM>
    {
        public ClientMenuPage()
        {
            InitializeComponent();
            
            var vehiclesTapGestureRecognizer = new TapGestureRecognizer();
            vehiclesTapGestureRecognizer.Tapped += async (sender, args) => await ViewModel.VehiclesNavigationCommand.ExecuteAsync();
            VehiclesButton.GestureRecognizers.Add(vehiclesTapGestureRecognizer);
            
            var datesTapGestureRecognizer = new TapGestureRecognizer();
            datesTapGestureRecognizer.Tapped += async (sender, args) => await ViewModel.DatesNavigationCommand.ExecuteAsync();
            DatesButton.GestureRecognizers.Add(datesTapGestureRecognizer);
            
            var logoutTapGestureRecognizer = new TapGestureRecognizer();
            logoutTapGestureRecognizer.Tapped += async (sender, args) => await ViewModel.LogoutCommand.ExecuteAsync();
            LogoutButton.GestureRecognizers.Add(logoutTapGestureRecognizer);
        }
    }
}