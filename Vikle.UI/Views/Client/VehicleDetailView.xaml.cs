using System;
using System.Collections.Generic;
using System.Threading;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core.Enums;
using Vikle.Core.Models;
using Vikle.Core.ViewModels;
using Xamarin.Forms;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class contains the definition for the vehicle detail view.
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail)]
    public partial class VehicleDetailView : MvxContentPage<VehicleDetailVM>
    {
        public VehicleDetailView()
        {
            InitializeComponent();
            
            TitleView.Title = Title;
            TypePicker.ItemsSource = new List<VehicleType> {
                VehicleType.Car,
                VehicleType.Truck,
                VehicleType.MotorCycle,
                VehicleType.Van,
                VehicleType.Other
            };
            InitYearPicker();
        }
        
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            TitleView.HomeButtonCommand = ViewModel.HomeNavigationCommand;
            PlateEntry.Text = ViewModel.PlateNumber;
            PlateEntry.IsEnabled = ViewModel.EditionMode;
            ModelEntry.Text = ViewModel.VehicleModel;
            ModelEntry.IsEnabled = ViewModel.EditionMode;
            TBDSDate.Date = ViewModel.LastTBDS.Date;
            TBDSDate.IsEnabled = ViewModel.EditionMode;
            ITVDate.Date = ViewModel.LastITV.Date;
            ITVDate.IsEnabled = ViewModel.EditionMode;
            TypePicker.SelectedItem = ViewModel.VehicleType;
            TypePicker.IsEnabled = ViewModel.EditionMode;
            YearPicker.SelectedItem = ViewModel.Year;
            YearPicker.IsEnabled = ViewModel.EditionMode;
            StatusBar.IsVisible = !ViewModel.ShowReparationStatus;
            ConfirmButton.IsVisible = ViewModel.EditionMode;
            CancelLabel.IsVisible = ViewModel.EditionMode;
            DeleteLabel.IsVisible = ViewModel.EditionMode;
            EditLabel.IsVisible = !ViewModel.EditionMode;
            HistoryButton.IsVisible = !ViewModel.EditionMode;
            ErrorLabel.IsVisible = ViewModel.ShowDetailError;
            ErrorLabel.Text = ViewModel.DetailError;

            HistoryButton.Command = ViewModel.ReparationsHistoryCommand;
            ConfirmButton.Command = ViewModel.UpdateVehicleCommand;
            
            var cancelTapGestureRecognizer = new TapGestureRecognizer();
            cancelTapGestureRecognizer.Tapped += async (sender, args) => await ViewModel.EditVehicleCommand.ExecuteAsync();
            CancelLabel.GestureRecognizers.Add(cancelTapGestureRecognizer);
            
            var editTapGestureRecognizer = new TapGestureRecognizer();
            editTapGestureRecognizer.Tapped += async (sender, args) => await ViewModel.EditVehicleCommand.ExecuteAsync();
            EditLabel.GestureRecognizers.Add(editTapGestureRecognizer);
            
            var deleteTapGestureRecognizer = new TapGestureRecognizer();
            deleteTapGestureRecognizer.Tapped += async (sender, args) => await ViewModel.EditVehicleCommand.ExecuteAsync();
            DeleteLabel.GestureRecognizers.Add(deleteTapGestureRecognizer);
        }

        void InitYearPicker()
        {
            var years = new List<int>();

            for (int i = 1950; i <= DateTime.UtcNow.Year; i++)
            {
                years.Add(i);
            }

            YearPicker.ItemsSource = years;
        }
    }
}