// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
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