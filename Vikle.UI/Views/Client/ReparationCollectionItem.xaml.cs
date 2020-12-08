using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vikle.Core;
using Vikle.Core.Models;
using Vikle.UI.Converters;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class contains the definition of the reparation item template for the CollectionView.
    /// </summary>
    public partial class ReparationCollectionItem
    {
        public ReparationCollectionItem()
        {
            InitializeComponent();
            
            TopDateLabel.SetBinding(Label.TextProperty, "Date", BindingMode.Default, new LongToDateTimeConverter(), stringFormat: "{0:dd}");
            BottomDateLabel.SetBinding(Label.TextProperty, "Date", BindingMode.Default, new LongToDateTimeConverter(), stringFormat: "{0:MMMM}");
            SubDateLabel.SetBinding(Label.TextProperty, "Date", BindingMode.Default, new LongToDateTimeConverter(), stringFormat: "{0:yyyy}");
        }
    }
}