using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class contains the definition for the date detail view.
    /// </summary>
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