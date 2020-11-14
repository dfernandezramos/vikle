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
            
            VehiclesCollection.ItemsSource = ViewModel.Vehicles;
            VehiclesCollection.SelectionChanged += async (sender, args) => await VehiclesCollectionOnSelectionChanged(sender, args);
            
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (sender, args) => await ViewModel.ShowVehicleDetailsCommand.ExecuteAsync((new Vehicle(), true));
            NewVehicleButton.GestureRecognizers.Add(tapGestureRecognizer);

            ErrorLabel.IsVisible = ViewModel.ShowVehiclesError;
            ErrorLabel.Text = ViewModel.VehiclesError;
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