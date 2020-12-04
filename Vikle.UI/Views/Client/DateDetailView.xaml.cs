using System.Collections.Generic;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core.Enums;
using Vikle.Core.ViewModels;
using Xamarin.Forms;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class contains the definition for the date detail view.
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail)]
    public partial class DateDetailView : MvxContentPage<DateDetailVM>
    {
        public DateDetailView()
        {
            InitializeComponent();
            
            TitleView.Title = Title;
            ReasonPicker.ItemsSource = new List<ReparationType>
            {
                ReparationType.Maintenance,
                ReparationType.Reparation,
                ReparationType.Other
            };
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            
            var cancelTapGestureRecognizer = new TapGestureRecognizer();
            cancelTapGestureRecognizer.Tapped += async (sender, args) => await ViewModel.CloseCommand.ExecuteAsync();
            CancelLabel.GestureRecognizers.Add(cancelTapGestureRecognizer);
            
            TitleView.HomeButtonCommand = ViewModel.HomeNavigationCommand;
            ContinueButton.Command = ViewModel.UpdateDateCommand;
            VehiclePicker.BindingContext = ViewModel;
            VehiclePicker.SetBinding(Picker.ItemsSourceProperty, nameof(ViewModel.PlateNumbers));
            VehiclePicker.SetBinding(Picker.SelectedItemProperty, nameof(ViewModel.SelectedVehiclePlateNumber));
            ReasonPicker.BindingContext = ViewModel;
            ReasonPicker.SetBinding(Picker.SelectedItemProperty, nameof(ViewModel.ReparationReason));
            ReparationDatePicker.BindingContext = ViewModel;
            ReparationDatePicker.SetBinding(DatePicker.DateProperty, nameof(ViewModel.ReparationDate));
            ErrorLabel.BindingContext = ViewModel;
            ErrorLabel.SetBinding(Label.IsVisibleProperty, nameof(ViewModel.ShowDateDetailsError));
            ErrorLabel.SetBinding(Label.TextProperty, nameof(ViewModel.DateDetailsError));
        }
    }
}