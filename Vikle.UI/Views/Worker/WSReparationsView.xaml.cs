using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core.Models;
using Vikle.Core.ViewModels;
using Vikle.UI.Converters;
using Xamarin.Forms;
using BindingMode = Xamarin.Forms.BindingMode;
using CollectionView = Xamarin.Forms.CollectionView;

namespace Vikle.UI.Views.Worker
{
    /// <summary>
    /// This class contains the definition of the worker side reparations view.
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, NoHistory = true)]
    public partial class WSReparationsView : MvxContentPage<WSReparationsVM>
    {
        private bool _onSelection;

        public WSReparationsView()
        {
            InitializeComponent();
            TitleView.Title = Title;
            TitleView.HomeButtonVisible = false;
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (sender, args) => await ViewModel.ReparationDetailNavigationCommand.ExecuteAsync(new Reparation());
            NewReparationButton.GestureRecognizers.Add(tapGestureRecognizer);
            
            TitleView.HomeButtonCommand = ViewModel.HomeNavigationCommand;
            ReparationsCollection.BindingContext = ViewModel;
            ReparationsCollection.SetBinding(CollectionView.ItemsSourceProperty, nameof(ViewModel.Reparations));
            ReparationsCollection.SetBinding(CollectionView.IsVisibleProperty, nameof(ViewModel.ShowReparationsError), BindingMode.Default, new InverseBoolConverter());
            ReparationsCollection.SelectionChanged += async (sender, args) => await ReparationsCollectionOnSelectionChanged(sender, args);
            ErrorLabel.BindingContext = ViewModel;
            ErrorLabel.SetBinding(Label.TextProperty, nameof(ViewModel.ReparationsError));
            ErrorLabel.SetBinding(Label.IsVisibleProperty, nameof(ViewModel.ShowReparationsError));
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