using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vikle.Core;
using Vikle.Core.Enums;
using Vikle.Core.Models;
using Vikle.UI.Converters;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Worker
{
    /// <summary>
    /// This class contains the definition of the reparation item template for the CollectionView in the worker side.
    /// </summary>
    public partial class WSReparationCollectionItem
    {
        public WSReparationCollectionItem()
        {
            InitializeComponent();
            this.PropertyChanged += OnPropertyChanged;
            
            TopDateLabel.SetBinding(Label.TextProperty, "Date", BindingMode.Default, new LongToDateTimeConverter(), stringFormat: "{0:dd}");
            BottomDateLabel.SetBinding(Label.TextProperty, "Date", BindingMode.Default, new LongToDateTimeConverter(), stringFormat: "{0:MMMM}");
        }

        void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BindingContext))
            {
                var reparation = (Reparation)this.BindingContext;
                if (reparation == null)
                {
                    return;
                }

                switch (reparation.Status)
                {
                    case ReparationStatus.Pending:
                    case ReparationStatus.Rejected:
                        StatusColorView.Color = Color.Red;
                        break;
                    case ReparationStatus.Repairing:
                        StatusColorView.Color = (Color) Resources["BlueStrongColor"];
                        break;
                    case ReparationStatus.Repaired:
                        StatusColorView.Color = Color.Green;
                        break;
                }
            }
        }
    }
}