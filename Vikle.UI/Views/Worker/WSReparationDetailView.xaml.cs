using System.Collections.Generic;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core.Enums;
using Vikle.Core.ViewModels;
using Vikle.UI.Converters;
using Xamarin.Forms;

namespace Vikle.UI.Views.Worker
{
    /// <summary>
    /// This class contains the definition of the reparation detail view for the worker side.
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail)]
    public partial class WSReparationDetailView : MvxContentPage<WSReparationDetailVM>
    {
        public WSReparationDetailView()
        {
            InitializeComponent();

            TitleView.Title = Title;
            ReasonPicker.ItemsSource = new List<ReparationType> {
                ReparationType.Maintenance,
                ReparationType.Reparation,
                ReparationType.Other
            };
            StatusPicker.ItemsSource = new List<ReparationStatus> {
                ReparationStatus.Rejected,
                ReparationStatus.Pending,
                ReparationStatus.Repairing,
                ReparationStatus.Repaired
            };
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            
            var cancelTapGestureRecognizer = new TapGestureRecognizer();
            cancelTapGestureRecognizer.Tapped += async (sender, args) => await ViewModel.CancelCommand.ExecuteAsync();
            CancelLabel.GestureRecognizers.Add(cancelTapGestureRecognizer);
            
            TitleView.HomeButtonCommand = ViewModel.HomeNavigationCommand;
            ContinueButton.Command = ViewModel.ContinueCommand;
            ContactButton.Command = ViewModel.ContactCommand;
            ContactButton.BindingContext = ViewModel;
            ContactButton.SetBinding(Button.IsVisibleProperty, nameof(ViewModel.NewReparation), BindingMode.Default, new InverseBoolConverter());
            SecondaryHeader.Title = ViewModel.PlateNumber;
            SecondaryHeader.BindingContext = ViewModel;
            SecondaryHeader.SetBinding(IsVisibleProperty, nameof(ViewModel.NewReparation), BindingMode.Default, new InverseBoolConverter());
            PlateNumberEntry.BindingContext = ViewModel;
            PlateNumberEntry.SetBinding(Entry.IsVisibleProperty, nameof(ViewModel.NewReparation));
            PlateNumberEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.PlateNumber));
            PlateNumberLabel.BindingContext = ViewModel;
            PlateNumberLabel.SetBinding(Label.IsVisibleProperty, nameof(ViewModel.NewReparation));
            ErrorLabel.BindingContext = ViewModel;
            ErrorLabel.SetBinding(Label.IsVisibleProperty, nameof(ViewModel.ShowDetailError));
            ErrorLabel.SetBinding(Label.TextProperty, nameof(ViewModel.DetailError));
            StatusPicker.BindingContext = ViewModel;
            StatusPicker.SetBinding(Picker.SelectedItemProperty, nameof(ViewModel.ReparationStatus));
            ReasonPicker.BindingContext = ViewModel;
            ReasonPicker.SetBinding(Picker.SelectedItemProperty, nameof(ViewModel.ReparationType));
            ReparationDatePicker.BindingContext = ViewModel;
            ReparationDatePicker.SetBinding(DatePicker.DateProperty, nameof(ViewModel.ReparationDate));
        }
    }
}