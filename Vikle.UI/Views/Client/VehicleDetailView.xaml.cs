using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehicleDetailView : ContentPage
    {
        public VehicleDetailView()
        {
            InitializeComponent();
            
            TitleView.Title = Title;
            TypePicker.ItemsSource = new List<string> {"Car", "Truck", "Motorcycle", "Other"};
        }
    }
}