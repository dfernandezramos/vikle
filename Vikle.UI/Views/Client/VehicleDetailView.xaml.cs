using System;
using System.Collections.Generic;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core.ViewModels;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class contains the definition for the vehicle detail view.
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail)]
    public partial class VehicleDetailView : MvxContentPage<VehicleDetailVM>
    {
        public VehicleDetailView()
        {
            InitializeComponent();
            
            TitleView.Title = Title;
            TypePicker.ItemsSource = new List<string> {"Car", "Truck", "Motorcycle", "Other"};
            InitYearPicker();
        }
        
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            TitleView.HomeButtonCommand = ViewModel.HomeNavigationCommand;
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