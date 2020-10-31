using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class contains the definition for the vehicle detail view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehicleDetailView : ContentPage
    {
        public VehicleDetailView()
        {
            InitializeComponent();
            
            TitleView.Title = Title;
            TypePicker.ItemsSource = new List<string> {"Car", "Truck", "Motorcycle", "Other"};
            InitYearPicker();
        }

        void InitYearPicker()
        {
            var years = new List<string>();

            for (int i = 1950; i < DateTime.UtcNow.Year; i++)
            {
                years.Add(i.ToString());
            }

            YearPicker.ItemsSource = years;
        }
    }
}