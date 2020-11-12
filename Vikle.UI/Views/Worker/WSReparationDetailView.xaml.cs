using System.Collections.Generic;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core.ViewModels;

namespace Vikle.UI.Views.Worker
{
    /// <summary>
    /// This class contains the definition of the reparation detail view for the worker side.
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail)]
    public partial class WSReparationDetailView : MvxContentPage<WSReparationDetailVM>
    {
        public WSReparationDetailView()
        {
            InitializeComponent();

            TitleView.Title = Title;
            ReasonPicker.ItemsSource = new List<string> {"Maintenance", "Reparation", "Other"};
            StatusPicker.ItemsSource = new List<string> {"Waiting", "Repairing", "Repaired"};
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            
            TitleView.HomeButtonCommand = ViewModel.HomeNavigationCommand;
        }
    }
}