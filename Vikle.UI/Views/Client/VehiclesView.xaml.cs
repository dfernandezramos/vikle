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
using Vikle.Core;
using Vikle.Core.Models;
using Vikle.Core.ViewModels;
using Xamarin.Forms;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class contains the definition for the client's vehicles view.
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, NoHistory = true)]
    public partial class VehiclesView : MvxContentPage<VehiclesVM>
    {
        bool _onSelection;
        
        public VehiclesView()
        {
            InitializeComponent();
            TitleView.Title = Title;
            TitleView.HomeButtonVisible = false;
            NewVehicleButton.Title = Strings.NewVehicle;
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (sender, args) => await ViewModel.ShowVehicleDetailsCommand.ExecuteAsync((new Vehicle(), true));
            NewVehicleButton.GestureRecognizers.Add(tapGestureRecognizer);

            ErrorLabel.BindingContext = ViewModel;
            ErrorLabel.SetBinding(Label.TextProperty, nameof(ViewModel.VehiclesError));
            ErrorLabel.SetBinding(Label.IsVisibleProperty, nameof(ViewModel.ShowVehiclesError));
            VehiclesCollection.BindingContext = ViewModel;
            VehiclesCollection.SetBinding(CollectionView.ItemsSourceProperty, nameof(ViewModel.Vehicles));
            VehiclesCollection.SelectionChanged += async (sender, args) => await VehiclesCollectionOnSelectionChanged(sender, args);
        }

        async Task VehiclesCollectionOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_onSelection)
            {
                _onSelection = true;
                Vehicle vehicle = ((Vehicle) e.CurrentSelection.Single());
                await ViewModel.ShowVehicleDetailsCommand.ExecuteAsync((vehicle, false));
                ((CollectionView) sender).SelectedItem = null;
                _onSelection = false;
            }
        }
    }
}