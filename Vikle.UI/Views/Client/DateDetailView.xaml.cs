using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DateDetailView : ContentPage
    {
        public DateDetailView()
        {
            InitializeComponent();
            
            TitleView.Title = Title;
            ReasonPicker.ItemsSource = new List<string> {"Maintenance", "Reparation", "Other"};
        }
    }
}