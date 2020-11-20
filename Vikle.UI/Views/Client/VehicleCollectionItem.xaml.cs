
using System.ComponentModel;
using Vikle.Core;
using Vikle.Core.Models;
using Xamarin.Forms;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class contains the definition of the item template for the CollectionView.
    /// </summary>
    public partial class VehicleCollectionItem
    {
        public VehicleCollectionItem()
        {
            InitializeComponent();
            this.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BindingContext))
            {
                var vehicle = (Vehicle)this.BindingContext;
                if (vehicle == null)
                {
                    return;
                }
                
                VehicleImage.Source = ImageSource.FromResource(Utils.GetVehicleImageName(vehicle.VehicleType));
            }
        }
    }
}