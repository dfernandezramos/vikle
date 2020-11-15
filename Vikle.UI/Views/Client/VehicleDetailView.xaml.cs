using System;
using System.Collections.Generic;
using System.ComponentModel;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core.Enums;
using Vikle.Core.ViewModels;
using Vikle.UI.Converters;
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
            
            ViewModel.PropertyChanged += ViewModelOnPropertyChanged;
            SetBindings();
            SetGestureRecognizers();
        }

        void SetBindings()
        {
            TitleView.HomeButtonCommand = ViewModel.HomeNavigationCommand;
            PlateEntry.BindingContext = ViewModel;
            PlateEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.PlateNumber));
            PlateEntry.SetBinding(Entry.IsEnabledProperty, nameof(ViewModel.EditionMode));
            ModelEntry.BindingContext = ViewModel;
            ModelEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.VehicleModel));
            ModelEntry.SetBinding(Entry.IsEnabledProperty, nameof(ViewModel.EditionMode));
            TBDSDate.BindingContext = ViewModel;
            TBDSDate.SetBinding(DatePicker.DateProperty, nameof(ViewModel.LastTBDS.Date));
            TBDSDate.SetBinding(DatePicker.IsEnabledProperty, nameof(ViewModel.EditionMode));
            ITVDate.BindingContext = ViewModel;
            ITVDate.SetBinding(DatePicker.DateProperty, nameof(ViewModel.LastITV.Date));
            ITVDate.SetBinding(DatePicker.IsEnabledProperty, nameof(ViewModel.EditionMode));
            TypePicker.BindingContext = ViewModel;
            TypePicker.SetBinding(Picker.SelectedItemProperty, nameof(ViewModel.VehicleType));
            TypePicker.SetBinding(Picker.IsEnabledProperty, nameof(ViewModel.EditionMode));
            YearPicker.BindingContext = ViewModel;
            YearPicker.SetBinding(Picker.SelectedItemProperty, nameof(ViewModel.Year));
            YearPicker.SetBinding(Picker.IsEnabledProperty, nameof(ViewModel.EditionMode));
            StatusBar.BindingContext = ViewModel;
            StatusBar.SetBinding(Picker.IsVisibleProperty, nameof(ViewModel.ShowReparationStatus), BindingMode.Default, new InverseBoolConverter());
            ConfirmButton.BindingContext = ViewModel;
            ConfirmButton.SetBinding(Button.IsVisibleProperty, nameof(ViewModel.EditionMode));
            CancelLabel.BindingContext = ViewModel;
            CancelLabel.SetBinding(Label.IsVisibleProperty, nameof(ViewModel.EditionMode));
            DeleteLabel.BindingContext = ViewModel;
            DeleteLabel.SetBinding(Label.IsVisibleProperty, nameof(ViewModel.EditionMode));
            EditLabel.BindingContext = ViewModel;
            EditLabel.SetBinding(Label.IsVisibleProperty, nameof(ViewModel.EditionMode));
            HistoryButton.BindingContext = ViewModel;
            HistoryButton.SetBinding(Button.IsVisibleProperty, nameof(ViewModel.EditionMode));
            ErrorLabel.BindingContext = ViewModel;
            ErrorLabel.SetBinding(Label.IsVisibleProperty, nameof(ViewModel.ShowDetailError));
            ErrorLabel.SetBinding(Label.TextProperty, nameof(ViewModel.DetailError));
            HistoryButton.Command = ViewModel.ReparationsHistoryCommand;
            ConfirmButton.Command = ViewModel.UpdateVehicleCommand;
        }

        void SetGestureRecognizers()
        {
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

        void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ViewModel != null && e.PropertyName == nameof(ViewModel.ReparationStatus))
            {
                if (ViewModel.ReparationStatus == null)
                {
                    return;
                }
                
                StatusBar.Pending.Active = false;
                StatusBar.Repairing.Active = false;
                StatusBar.Repaired.Active = false;

                switch (ViewModel.ReparationStatus)
                {
                    case ReparationStatus.Pending:
                        StatusBar.Pending.Active = true;
                        break;
                    case ReparationStatus.Repairing:
                        StatusBar.Repairing.Active = true;
                        break;
                    case ReparationStatus.Repaired:
                        StatusBar.Repaired.Active = true;
                        break;
                }
            }
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