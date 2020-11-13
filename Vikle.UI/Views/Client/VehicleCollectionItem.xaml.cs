
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
            this.PropertyChanged += TempNameOnPropertyChanged;
        }

        private void TempNameOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BindingContext))
            {
                var vehicle = (Vehicle)this.BindingContext;
                VehicleImage.Source = ImageSource.FromResource(Utils.GetVehicleImageName(vehicle.VehicleType));
            }
        }
    }
}