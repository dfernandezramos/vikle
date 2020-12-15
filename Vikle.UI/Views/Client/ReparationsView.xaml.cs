// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core.Models;
using Vikle.Core.ViewModels;
using Vikle.UI.Converters;
using Xamarin.Forms;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class contains the definition of the client vehicle reparations history.
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail)]
    public partial class ReparationsView : MvxContentPage<ReparationsVM>
    {
        private bool _onSelection;

        public ReparationsView()
        {
            InitializeComponent();
            TitleView.Title = Title;
        }
        
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            TitleView.HomeButtonCommand = ViewModel.HomeNavigationCommand;
            HistoryCollection.BindingContext = ViewModel;
            HistoryCollection.SetBinding(CollectionView.ItemsSourceProperty, nameof(ViewModel.Reparations));
            HistoryCollection.SetBinding(CollectionView.IsVisibleProperty, nameof(ViewModel.ShowHistoryError), BindingMode.Default, new InverseBoolConverter());
            HistoryCollection.SelectionChanged += async (sender, args) => await ReparationsCollectionOnSelectionChanged(sender, args);
            ErrorLabel.BindingContext = ViewModel;
            ErrorLabel.SetBinding(Label.TextProperty, nameof(ViewModel.HistoryError));
            ErrorLabel.SetBinding(Label.IsVisibleProperty, nameof(ViewModel.ShowHistoryError));
            PlateHeader.Title = ViewModel.PlateNumber;
        }

        private async Task ReparationsCollectionOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_onSelection)
            {
                _onSelection = true;
                Reparation reparation = ((Reparation) e.CurrentSelection.Single());
                await ViewModel.ReparationDetailNavigationCommand.ExecuteAsync(reparation);
                ((CollectionView) sender).SelectedItem = null;
                _onSelection = false;
            }
        }
    }
}