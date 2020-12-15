// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core;
using Vikle.Core.ViewModels;
using Xamarin.Forms;
using CollectionView = Xamarin.Forms.CollectionView;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class implements the client dates view.
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail)]
    public partial class DatesView : MvxContentPage<DatesVM>
    {
        private bool _onSelection;

        public DatesView()
        {
            InitializeComponent();
            TitleView.Title = Title;
            NewDateButton.Title = Strings.NewDate;
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (sender, args) => await ViewModel.NewDateDetailsCommand.ExecuteAsync();
            NewDateButton.GestureRecognizers.Add(tapGestureRecognizer);

            ErrorLabel.BindingContext = ViewModel;
            ErrorLabel.SetBinding(Label.TextProperty, nameof(ViewModel.DatesError));
            ErrorLabel.SetBinding(Label.IsVisibleProperty, nameof(ViewModel.ShowDatesError));
            DatesCollection.BindingContext = ViewModel;
            DatesCollection.SetBinding(CollectionView.ItemsSourceProperty, nameof(ViewModel.Dates));
            TitleView.HomeButtonCommand = ViewModel.HomeNavigationCommand;
        }
    }
}