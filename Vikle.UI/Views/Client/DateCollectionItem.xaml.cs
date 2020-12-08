using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vikle.UI.Converters;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class contains the definition of the date item template for the CollectionView.
    /// </summary>
    public partial class DateCollectionItem
    {
        public DateCollectionItem()
        {
            InitializeComponent();
            
            TopDateLabel.SetBinding(Label.TextProperty, "ReparationDate", BindingMode.Default, new LongToDateTimeConverter(), stringFormat: "{0:dd}");
            BottomDateLabel.SetBinding(Label.TextProperty, "ReparationDate", BindingMode.Default, new LongToDateTimeConverter(), stringFormat: "{0:MMMM}");
        }
    }
}