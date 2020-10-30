using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Worker
{
    /// <summary>
    /// This class contains the definition of the reparation detail view for the worker side.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WSReparationDetailView : ContentPage
    {
        public WSReparationDetailView()
        {
            InitializeComponent();

            TitleView.Title = Title;
            ReasonPicker.ItemsSource = new List<string> {"Maintenance", "Reparation", "Other"};
            StatusPicker.ItemsSource = new List<string> {"Waiting", "Repairing", "Repaired"};
        }
    }
}